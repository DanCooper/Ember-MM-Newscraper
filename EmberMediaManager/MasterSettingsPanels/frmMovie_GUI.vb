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

    Private MovieGeneralMediaListSorting As New List(Of Settings.ListSorting)

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
        With Master.eSettings
            .MovieClickScrape = chkMovieClickScrape.Checked
            .MovieClickScrapeAsk = chkMovieClickScrapeAsk.Checked
            If .MovieFilterCustom.Count <= 0 Then .MovieFilterCustomIsEmpty = True
            .MovieGeneralCustomMarker1Color = btnMovieGeneralCustomMarker1.BackColor.ToArgb
            .MovieGeneralCustomMarker2Color = btnMovieGeneralCustomMarker2.BackColor.ToArgb
            .MovieGeneralCustomMarker3Color = btnMovieGeneralCustomMarker3.BackColor.ToArgb
            .MovieGeneralCustomMarker4Color = btnMovieGeneralCustomMarker4.BackColor.ToArgb
            .MovieGeneralCustomMarker1Name = txtMovieGeneralCustomMarker1.Text
            .MovieGeneralCustomMarker2Name = txtMovieGeneralCustomMarker2.Text
            .MovieGeneralCustomMarker3Name = txtMovieGeneralCustomMarker3.Text
            .MovieGeneralCustomMarker4Name = txtMovieGeneralCustomMarker4.Text
            .MovieGeneralCustomScrapeButtonEnabled = rbMovieGeneralCustomScrapeButtonEnabled.Checked
            .MovieGeneralCustomScrapeButtonModifierType = CType(cbMovieGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .MovieGeneralCustomScrapeButtonScrapeType = CType(cbMovieGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .MovieGeneralFlagLang = If(cbMovieLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, cbMovieLanguageOverlay.Text)
            .MovieGeneralMediaListSorting.Clear()
            .MovieGeneralMediaListSorting.AddRange(MovieGeneralMediaListSorting)
            .MovieLevTolerance = If(Not String.IsNullOrEmpty(txtMovieLevTolerance.Text), Convert.ToInt32(txtMovieLevTolerance.Text), 0)
            .MovieSortTokens.Clear()
            .MovieSortTokens.AddRange(lstMovieSortTokens.Items.OfType(Of String).ToList)
            If .MovieSortTokens.Count <= 0 Then .MovieSortTokensIsEmpty = True
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            btnMovieGeneralCustomMarker1.BackColor = Color.FromArgb(.MovieGeneralCustomMarker1Color)
            btnMovieGeneralCustomMarker2.BackColor = Color.FromArgb(.MovieGeneralCustomMarker2Color)
            btnMovieGeneralCustomMarker3.BackColor = Color.FromArgb(.MovieGeneralCustomMarker3Color)
            btnMovieGeneralCustomMarker4.BackColor = Color.FromArgb(.MovieGeneralCustomMarker4Color)
            cbMovieGeneralCustomScrapeButtonModifierType.SelectedValue = .MovieGeneralCustomScrapeButtonModifierType
            cbMovieGeneralCustomScrapeButtonScrapeType.SelectedValue = .MovieGeneralCustomScrapeButtonScrapeType
            cbMovieLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.MovieGeneralFlagLang), Master.eLang.Disabled, .MovieGeneralFlagLang)
            chkMovieClickScrape.Checked = .MovieClickScrape
            chkMovieClickScrapeAsk.Checked = .MovieClickScrapeAsk
            If .MovieGeneralCustomScrapeButtonEnabled Then
                rbMovieGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbMovieGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            txtMovieGeneralCustomMarker1.Text = .MovieGeneralCustomMarker1Name
            txtMovieGeneralCustomMarker2.Text = .MovieGeneralCustomMarker2Name
            txtMovieGeneralCustomMarker3.Text = .MovieGeneralCustomMarker3Name
            txtMovieGeneralCustomMarker4.Text = .MovieGeneralCustomMarker4Name

            If .MovieLevTolerance > 0 Then
                chkMovieLevTolerance.Checked = True
                txtMovieLevTolerance.Enabled = True
                txtMovieLevTolerance.Text = .MovieLevTolerance.ToString
            End If
            chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked

            MovieGeneralMediaListSorting.AddRange(.MovieGeneralMediaListSorting)

            LoadMovieGeneralMediaListSorting()
            RefreshMovieSortTokens()
        End With
    End Sub

    Private Sub Setup()
        chkMovieClickScrapeAsk.Text = Master.eLang.GetString(852, "Show Results Dialog")
        colMovieGeneralMediaListSortingLabel.Text = Master.eLang.GetString(1331, "Column")
        lblMovieLanguageOverlay.Text = String.Concat(Master.eLang.GetString(436, "Display best Audio Stream with the following Language"), ":")
        chkMovieClickScrape.Text = Master.eLang.GetString(849, "Enable Click-Scrape")
        colMovieGeneralMediaListSortingHide.Text = Master.eLang.GetString(465, "Hide")
        gbMovieGeneralMainWindowOpts.Text = Master.eLang.GetString(1152, "Main Window")
        gbMovieGeneralMiscOpts.Text = Master.eLang.GetString(429, "Miscellaneous")
        gbMovieGeneralMediaListSorting.Text = Master.eLang.GetString(490, "Movie List Sorting")
        gbMovieGeneralMediaListSortTokensOpts.Text = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        chkMovieLevTolerance.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        gbMovieGeneralCustomMarker.Text = Master.eLang.GetString(1190, "Custom Marker")
        gbMovieGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        lblMovieGeneralCustomMarker1.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #1")
        lblMovieGeneralCustomMarker2.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #2")
        lblMovieGeneralCustomMarker3.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #3")
        lblMovieGeneralCustomMarker4.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #4")
        lblMovieLevTolerance.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")

        LoadCustomScraperButtonModifierTypes_Movie()
    End Sub

    Private Sub btnMovieSortTokenAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not String.IsNullOrEmpty(txtMovieSortToken.Text) Then
            If Not lstMovieSortTokens.Items.Contains(txtMovieSortToken.Text) Then
                lstMovieSortTokens.Items.Add(txtMovieSortToken.Text)
                Handle_SettingsChanged()
                txtMovieSortToken.Text = String.Empty
                txtMovieSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnMovieGeneralCustomMarker1_Click(sender As Object, e As EventArgs)
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker1.BackColor = .Color
                    Handle_SettingsChanged()
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker2_Click(sender As Object, e As EventArgs)
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker2.BackColor = .Color
                    Handle_SettingsChanged()
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker3_Click(sender As Object, e As EventArgs)
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker3.BackColor = .Color
                    Handle_SettingsChanged()
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker4_Click(sender As Object, e As EventArgs)
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker4.BackColor = .Color
                    Handle_SettingsChanged()
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralMediaListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not lvMovieGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    MovieGeneralMediaListSorting.Remove(selItem)
                    MovieGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    RenumberMovieGeneralMediaListSorting()
                    LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    lvMovieGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                Handle_SettingsChanged()
                lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems(0).Index < (lvMovieGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    MovieGeneralMediaListSorting.Remove(selItem)
                    MovieGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    RenumberMovieGeneralMediaListSorting()
                    LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    lvMovieGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                Handle_SettingsChanged()
                lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub lvMovieGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvMovieGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvMovieGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)

                LoadMovieGeneralMediaListSorting()

                lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(topIndex)
                lvMovieGeneralMediaListSorting.Items(selIndex).Selected = True
                lvMovieGeneralMediaListSorting.ResumeLayout()
            End If

            Handle_SettingsChanged()
            lvMovieGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub btnMovieGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieListSorting, True)
        MovieGeneralMediaListSorting.Clear()
        MovieGeneralMediaListSorting.AddRange(Master.eSettings.MovieGeneralMediaListSorting)
        LoadMovieGeneralMediaListSorting()
        Handle_SettingsChanged()
    End Sub

    Private Sub btnMovieSortTokenRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveMovieSortToken()
    End Sub

    Private Sub btnMovieSortTokenReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSortTokens, True)
        RefreshMovieSortTokens()
        Handle_SettingsChanged()
    End Sub

    Private Sub chkMovieClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
        Handle_SettingsChanged()
    End Sub
    Private Sub chkMovieLevTolerance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieLevTolerance.Enabled = chkMovieLevTolerance.Checked
        If Not chkMovieLevTolerance.Checked Then txtMovieLevTolerance.Text = String.Empty
    End Sub

    Private Sub chkMovieProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        Handle_NeedsReload_Movie()
    End Sub

    Private Sub LoadLangs()
        cbMovieLanguageOverlay.Items.Add(Master.eLang.Disabled)
        cbMovieLanguageOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
    End Sub

    Private Sub LoadMovieGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        lvMovieGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In MovieGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_Movie()
        Dim items As New Dictionary(Of String, Enums.ModifierType)
        items.Add(Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All)
        items.Add(Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs)
        items.Add(Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner)
        items.Add(Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt)
        items.Add(Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo)
        items.Add(Master.eLang.GetString(1124, "DiscArt Only"), Enums.ModifierType.MainDiscArt)
        items.Add(Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts)
        items.Add(Master.eLang.GetString(74, "Extrathumbs Only"), Enums.ModifierType.MainExtrathumbs)
        items.Add(Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart)
        items.Add(Master.eLang.GetString(303, "KeyArt Only"), Enums.ModifierType.MainKeyArt)
        items.Add(Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape)
        items.Add(Master.eLang.GetString(76, "Meta Data Only"), Enums.ModifierType.MainMeta)
        items.Add(Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO)
        items.Add(Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster)
        items.Add(Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme)
        items.Add(Master.eLang.GetString(75, "Trailer Only"), Enums.ModifierType.MainTrailer)
        cbMovieGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonScrapeTypes()
        Dim strAll As String = Master.eLang.GetString(68, "All")
        Dim strFilter As String = Master.eLang.GetString(624, "Current Filter")
        Dim strMarked As String = Master.eLang.GetString(48, "Marked")
        Dim strMissing As String = Master.eLang.GetString(40, "Missing Items")
        Dim strNew As String = Master.eLang.GetString(47, "New")

        Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Dim strAuto As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")

        Dim items As New Dictionary(Of String, Enums.ScrapeType)
        items.Add(String.Concat(strAll, " - ", strAuto), Enums.ScrapeType.AllAuto)
        items.Add(String.Concat(strAll, " - ", strAsk), Enums.ScrapeType.AllAsk)
        items.Add(String.Concat(strAll, " - ", strSkip), Enums.ScrapeType.AllSkip)
        items.Add(String.Concat(strMissing, " - ", strAuto), Enums.ScrapeType.MissingAuto)
        items.Add(String.Concat(strMissing, " - ", strAsk), Enums.ScrapeType.MissingAsk)
        items.Add(String.Concat(strMissing, " - ", strSkip), Enums.ScrapeType.MissingSkip)
        items.Add(String.Concat(strNew, " - ", strAuto), Enums.ScrapeType.NewAuto)
        items.Add(String.Concat(strNew, " - ", strAsk), Enums.ScrapeType.NewAsk)
        items.Add(String.Concat(strNew, " - ", strSkip), Enums.ScrapeType.NewSkip)
        items.Add(String.Concat(strMarked, " - ", strAuto), Enums.ScrapeType.MarkedAuto)
        items.Add(String.Concat(strMarked, " - ", strAsk), Enums.ScrapeType.MarkedAsk)
        items.Add(String.Concat(strMarked, " - ", strSkip), Enums.ScrapeType.MarkedSkip)
        items.Add(String.Concat(strFilter, " - ", strAuto), Enums.ScrapeType.FilterAuto)
        items.Add(String.Concat(strFilter, " - ", strAsk), Enums.ScrapeType.FilterAsk)
        items.Add(String.Concat(strFilter, " - ", strSkip), Enums.ScrapeType.FilterSkip)
        cbMovieGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
    End Sub

    Private Sub lstMovieSortTokens_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveMovieSortToken()
    End Sub

    Private Sub RefreshMovieSortTokens()
        lstMovieSortTokens.Items.Clear()
        lstMovieSortTokens.Items.AddRange(Master.eSettings.MovieSortTokens.ToArray)
    End Sub

    Private Sub RemoveMovieSortToken()
        If lstMovieSortTokens.Items.Count > 0 AndAlso lstMovieSortTokens.SelectedItems.Count > 0 Then
            While lstMovieSortTokens.SelectedItems.Count > 0
                lstMovieSortTokens.Items.Remove(lstMovieSortTokens.SelectedItems(0))
            End While
            Handle_SettingsChanged()
        End If
    End Sub

    Private Sub RenumberMovieGeneralMediaListSorting()
        For i As Integer = 0 To MovieGeneralMediaListSorting.Count - 1
            MovieGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub rbMovieGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs)
        If rbMovieGeneralCustomScrapeButtonDisabled.Checked Then
            cbMovieGeneralCustomScrapeButtonModifierType.Enabled = False
            cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
            txtMovieGeneralCustomScrapeButtonModifierType.Enabled = False
            txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        Handle_SettingsChanged()
    End Sub

    Private Sub rbMovieGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs)
        If rbMovieGeneralCustomScrapeButtonEnabled.Checked Then
            cbMovieGeneralCustomScrapeButtonModifierType.Enabled = True
            cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = True
            txtMovieGeneralCustomScrapeButtonModifierType.Enabled = True
            txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        Handle_SettingsChanged()
    End Sub

#End Region 'Methods

End Class