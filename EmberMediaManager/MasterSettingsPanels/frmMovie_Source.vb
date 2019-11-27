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
        lvMovieSources.ListViewItemSorter = New ListViewItemComparer(1)
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
            .Title = Master.eLang.GetString(602, "Sources"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            .GeneralDateAddedIgnoreNFO = chkGeneralDateAddedIgnoreNFO.Checked
            .GeneralDateTime = CType(cbGeneralDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            .MovieCleanDB = chkMovieCleanDB.Checked
            If Not String.IsNullOrEmpty(cbMovieGeneralLang.Text) Then
                .MovieGeneralLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbMovieGeneralLang.Text).Abbreviation
            End If
            If Not String.IsNullOrEmpty(txtMovieSkipLessThan.Text) AndAlso Integer.TryParse(txtMovieSkipLessThan.Text, 0) Then
                .MovieSkipLessThan = Convert.ToInt32(txtMovieSkipLessThan.Text)
            Else
                .MovieSkipLessThan = 0
            End If
            .MovieFilterCustom.Clear()
            .MovieFilterCustom.AddRange(lstMovieFilters.Items.OfType(Of String).ToList)
            .MovieSortBeforeScan = chkMovieSortBeforeScan.Checked
            .MovieGeneralMarkNew = chkMovieGeneralMarkNew.Checked
            .MovieProperCase = chkMovieProperCase.Checked
            .GeneralSourceFromFolder = chkGeneralSourceFromFolder.Checked
            .GeneralOverwriteNfo = chkGeneralOverwriteNfo.Checked
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            cbGeneralDateTime.SelectedValue = .GeneralDateTime
            chkGeneralDateAddedIgnoreNFO.Checked = .GeneralDateAddedIgnoreNFO
            chkMovieCleanDB.Checked = .MovieCleanDB
            chkMovieSortBeforeScan.Checked = .MovieSortBeforeScan
            txtMovieSkipLessThan.Text = .MovieSkipLessThan.ToString
            chkMovieGeneralMarkNew.Checked = .MovieGeneralMarkNew
            chkMovieProperCase.Checked = .MovieProperCase
            chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
            chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo

            cbMovieGeneralLang.Items.Clear()
            cbMovieGeneralLang.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
            If cbMovieGeneralLang.Items.Count > 0 Then
                If Not String.IsNullOrEmpty(.MovieGeneralLanguage) Then
                    Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = .MovieGeneralLanguage)
                    If tLanguage IsNot Nothing Then
                        cbMovieGeneralLang.Text = tLanguage.Description
                    Else
                        tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.MovieGeneralLanguage))
                        If tLanguage IsNot Nothing Then
                            cbMovieGeneralLang.Text = tLanguage.Description
                        Else
                            cbMovieGeneralLang.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                        End If
                    End If
                Else
                    cbMovieGeneralLang.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                End If
            End If
        End With

        RefreshMovieFilters()
        RefreshMovieSources()
    End Sub

    Private Sub Setup()
        chkGeneralDateAddedIgnoreNFO.Text = Master.eLang.GetString(1209, "Ignore <dateadded> from NFO")
        gbGeneralDateAdded.Text = Master.eLang.GetString(792, "Adding Date")
        lblMovieSourcesDefaultsLanguage.Text = String.Concat(Master.eLang.GetString(1166, "Default Language"), ":")
        gbMovieSourcesDefaultsOpts.Text = Master.eLang.GetString(252, "Defaults for new Sources")
        colMovieSourcesExclude.Text = Master.eLang.GetString(264, "Exclude")
        colMovieSourcesGetYear.Text = Master.eLang.GetString(586, "Get Year")
        colMovieSourcesLanguage.Text = Master.eLang.GetString(610, "Language")
        gbImportOpts.Text = Master.eLang.GetString(559, "Import Options")
        colMovieSourcesName.Text = Master.eLang.GetString(232, "Name")
        colMovieSourcesPath.Text = Master.eLang.GetString(410, "Path")
        colMovieSourcesRecur.Text = Master.eLang.GetString(411, "Recursive")
        colMovieSourcesSingle.Text = Master.eLang.GetString(413, "Single Video")
        colMovieSourcesFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        btnMovieSourceAdd.Text = Master.eLang.GetString(407, "Add Source")
        btnMovieSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        btnMovieSourceRemove.Text = Master.eLang.GetString(30, "Remove")
        chkMovieCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        chkMovieSortBeforeScan.Text = Master.eLang.GetString(712, "Sort files into folder before each library update")
        lblMovieSkipLessThan.Text = Master.eLang.GetString(540, "Skip files smaller than:")
        lblMovieSkipLessThanMB.Text = Master.eLang.GetString(539, "MB")
        chkMovieGeneralMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        chkMovieProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        gbMovieGeneralFiltersOpts.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        lblGeneralOverwriteNfo.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")

        LoadGeneralDateTime()
    End Sub

    Private Sub btnMovieFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not String.IsNullOrEmpty(txtMovieFilter.Text) Then
            lstMovieFilters.Items.Add(txtMovieFilter.Text)
            txtMovieFilter.Text = String.Empty
            Handle_SettingsChanged()
            Handle_NeedsReload_Movie()
        End If

        txtMovieFilter.Focus()
    End Sub

    Private Sub btnMovieFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItem IsNot Nothing AndAlso lstMovieFilters.SelectedIndex < (lstMovieFilters.Items.Count - 1) Then
                Dim iIndex As Integer = lstMovieFilters.SelectedIndices(0)
                lstMovieFilters.Items.Insert(iIndex + 2, lstMovieFilters.SelectedItems(0))
                lstMovieFilters.Items.RemoveAt(iIndex)
                lstMovieFilters.SelectedIndex = iIndex + 1
                Handle_SettingsChanged()
                Handle_NeedsReload_Movie()
                lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItem IsNot Nothing AndAlso lstMovieFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstMovieFilters.SelectedIndices(0)
                lstMovieFilters.Items.Insert(iIndex - 1, lstMovieFilters.SelectedItems(0))
                lstMovieFilters.Items.RemoveAt(iIndex + 1)
                lstMovieFilters.SelectedIndex = iIndex - 1
                Handle_SettingsChanged()
                Handle_NeedsReload_Movie()
                lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveMovieFilter()
    End Sub

    Private Sub btnMovieFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        If MessageBox.Show(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieFilters, True)
            RefreshMovieFilters()
            Handle_SettingsChanged()
        End If
    End Sub

    Private Sub lstMovieFilters_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveMovieFilter()
    End Sub

    Private Sub RemoveMovieFilter()
        If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItems.Count > 0 Then
            While lstMovieFilters.SelectedItems.Count > 0
                lstMovieFilters.Items.Remove(lstMovieFilters.SelectedItems(0))
            End While
            Handle_SettingsChanged()
            Handle_NeedsReload_Movie()
        End If
    End Sub

    Private Sub RefreshMovieFilters()
        lstMovieFilters.Items.Clear()
        lstMovieFilters.Items.AddRange(Master.eSettings.MovieFilterCustom.ToArray)
    End Sub

    Private Sub btnMovieSourceEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lvMovieSources.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgSourceMovie
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = DialogResult.OK Then
                    RefreshMovieSources()
                    RaiseEvent NeedsReload_Movie()  'TODO: Check if we have to use Reload or DBUpdate
                    Handle_SettingsChanged()
                End If
            End Using
        End If
    End Sub

    Private Sub btnMovieSourceAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Using dSource As New dlgSourceMovie
            If dSource.ShowDialog = DialogResult.OK Then
                RefreshMovieSources()
                Handle_SettingsChanged()
                RaiseEvent NeedsDBUpdate_Movie()
            End If
        End Using
    End Sub

    Private Sub btnMovieSourceRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveMovieSource()
    End Sub

    Private Sub lvMovieSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        lvMovieSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvMovieSources_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        If lvMovieSources.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgSourceMovie
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = DialogResult.OK Then
                    RefreshMovieSources()
                    RaiseEvent NeedsReload_Movie()  'TODO: Check if we have to use Reload or DBUpdate
                    Handle_SettingsChanged()
                End If
            End Using
        End If
    End Sub

    Private Sub lvMovieSources_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveMovieSource()
    End Sub

    Private Sub LoadGeneralDateTime()
        cbGeneralDateTime.DataSource = Functions.GetDateTimeStampOptions()
        cbGeneralDateTime.DisplayMember = "Key"
        cbGeneralDateTime.ValueMember = "Value"
    End Sub

    Private Sub RefreshMovieSources()
        Dim lvItem As ListViewItem
        lvMovieSources.Items.Clear()
        For Each s As Database.DBSource In Master.DB.Load_AllSources_Movie
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(s.Language)
            lvItem.SubItems.Add(If(s.ScanRecursive, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.UseFolderName, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.IsSingle, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.GetYear, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RemoveMovieSource()
        If lvMovieSources.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lvMovieSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "idSource")
                        While lvMovieSources.SelectedItems.Count > 0
                            parSource.Value = lvMovieSources.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM moviesource WHERE idSource = (?);")
                            SQLcommand.ExecuteNonQuery()
                            lvMovieSources.Items.Remove(lvMovieSources.SelectedItems(0))
                        End While
                    End Using
                    SQLtransaction.Commit()
                End Using

                lvMovieSources.Sort()
                lvMovieSources.EndUpdate()
                lvMovieSources.Refresh()

                Handle_SettingsChanged()
            End If
        End If
    End Sub

    Private Sub txtMovieSkipLessThan_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        RaiseEvent NeedsDBClean_Movie()
        RaiseEvent NeedsDBUpdate_Movie()
    End Sub

#End Region 'Methods

#Region "Nested Classes"

    Class ListViewItemComparer
        Implements IComparer
        Private col As Integer

        Public Sub New()
            col = 0
        End Sub

        Public Sub New(ByVal column As Integer)
            col = column
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare
            Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        End Function
    End Class

#End Region 'Nested Classes

End Class