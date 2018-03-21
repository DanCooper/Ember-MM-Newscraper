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
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class frmSettingsHolder

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged()
    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Methods"

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        SetUp()
        LoadCustomMediaList()
        LoadMainTabDefaultList()
        LoadMainTabs()
    End Sub

    Private Sub SetUp()
        btnMainTabAdd.Text = Master.eLang.GetString(28, "Add")
        btnMainTabRemove.Text = Master.eLang.GetString(30, "Remove")
        btnCustomMediaListAdd.Text = Master.eLang.GetString(28, "Add")
        btnCustomMediaListRemove.Text = Master.eLang.GetString(30, "Remove")
        colMainTabDefaultList.HeaderText = Master.eLang.GetString(1394, "List")
        colMainTabTitle.HeaderText = Master.eLang.GetString(21, "Title")
        gbCustomMediaList.Text = Master.eLang.GetString(1390, "Custom Media Lists")
        gbMainTab.Text = Master.eLang.GetString(1393, "Main Tabs")
        lblCustomMediaListHelp.Text = Master.eLang.GetString(1392, "Use CTRL + Return for new lines.")
        lblCustomMediaListType.Text = Master.eLang.GetString(1288, "Type")
        lblCustomMediaListURL.Text = Master.eLang.GetString(1383, "Complete overview of Ember datatables:")
        lblPrefix.Text = Master.eLang.GetString(1391, "Prefix")
        lnkCustomMediaListURL.Text = Master.eLang.GetString(1384, "Ember Database")
    End Sub

    Private Sub btnCustomMediaListAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomMediaListAdd.Click
        If Not String.IsNullOrEmpty(txtCustomMediaListName.Text) OrElse String.IsNullOrEmpty(txtCustomMediaListQuery.Text) Then
            Master.DB.DeleteView(String.Concat(txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text))
            If Master.DB.AddView(String.Concat("CREATE VIEW '", txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text, "' AS ", txtCustomMediaListQuery.Text)) Then
                MessageBox.Show("Added View sucessfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCustomMediaList()
                LoadMainTabDefaultList()
            Else
                MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnCustomMediaListRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomMediaListRemove.Click
        If Not String.IsNullOrEmpty(txtCustomMediaListName.Text) Then
            Dim ViewName As String = String.Concat(txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text)
            Master.DB.DeleteView(ViewName)
            For Each dRow As DataGridViewRow In dgvMainTab.Rows
                If dRow.Cells(1).Value.ToString = ViewName Then
                    dgvMainTab.Rows.RemoveAt(dRow.Index)
                End If
            Next
            LoadCustomMediaList()
        End If
    End Sub

    Private Sub btnMainTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainTabAdd.Click
        Dim i As Integer = dgvMainTab.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvMainTab.Rows(i).Tag = False
        dgvMainTab.CurrentCell = dgvMainTab.Rows(i).Cells(0)
        dgvMainTab.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnMainTabDown_Click(sender As Object, e As EventArgs) Handles btnMainTabDown.Click
        If dgvMainTab.SelectedCells.Count = 1 AndAlso dgvMainTab.SelectedCells(0).OwningRow.Index < (dgvMainTab.Rows.Count - 1) Then
            Dim currRow = dgvMainTab.CurrentRow
            Dim currIndex = dgvMainTab.CurrentRow.Index
            dgvMainTab.Rows.Remove(currRow)
            dgvMainTab.Rows.Insert(currIndex + 1, currRow)
            dgvMainTab.CurrentCell = dgvMainTab(0, currIndex + 1)
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnMainTabRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMainTabRemove.Click
        If dgvMainTab.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMainTab.Rows(dgvMainTab.SelectedCells(0).RowIndex).Tag) Then
            dgvMainTab.Rows.RemoveAt(dgvMainTab.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub btnMainTabUp_Click(sender As Object, e As EventArgs) Handles btnMainTabUp.Click
        If dgvMainTab.SelectedCells.Count = 1 AndAlso dgvMainTab.SelectedCells(0).OwningRow.Index > 0 Then
            Dim currRow = dgvMainTab.CurrentRow
            Dim currIndex = dgvMainTab.CurrentRow.Index
            dgvMainTab.Rows.Remove(currRow)
            dgvMainTab.Rows.Insert(currIndex - 1, currRow)
            dgvMainTab.CurrentCell = dgvMainTab(0, currIndex - 1)
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbCustomMediaList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCustomMediaList.SelectedIndexChanged
        If Not cbCustomMediaList.SelectedIndex = -1 Then
            cbCustomMediaListType.SelectedIndex = -1
            btnCustomMediaListRemove.Enabled = True
            txtCustomMediaListName.Text = String.Empty
            txtCustomMediaListQuery.Text = String.Empty
            Dim SQL As Database.SQLViewProperty = Master.DB.GetViewDetails(cbCustomMediaList.SelectedItem.ToString)
            If Not String.IsNullOrEmpty(SQL.Name) AndAlso Not String.IsNullOrEmpty(SQL.Statement) Then
                txtCustomMediaListName.Enabled = True
                txtCustomMediaListQuery.Enabled = True
                Dim SQLPrefixName As Match = Regex.Match(SQL.Name, "(?<PREFIX>movie-|movieset-|tvshow-|seasons-|episode-)(?<NAME>.*)", RegexOptions.Singleline)
                Dim SQLQuery As Match = Regex.Match(SQL.Statement, "(?<QUERY>SELECT.*)", RegexOptions.Singleline)
                txtCustomMediaListPrefix.Text = SQLPrefixName.Groups("PREFIX").Value.ToString
                txtCustomMediaListName.Text = SQLPrefixName.Groups("NAME").Value.ToString
                txtCustomMediaListQuery.Text = SQLQuery.Groups("QUERY").Value.ToString.Trim
            Else
                txtCustomMediaListName.Enabled = False
                txtCustomMediaListQuery.Enabled = False
            End If
        End If
    End Sub

    Private Sub cbCustomMediaListType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCustomMediaListType.SelectedIndexChanged
        If Not cbCustomMediaListType.SelectedIndex = -1 Then
            cbCustomMediaList.SelectedIndex = -1
            txtCustomMediaListName.Enabled = True
            txtCustomMediaListPrefix.Text = String.Concat(cbCustomMediaListType.SelectedItem.ToString, "-")
            txtCustomMediaListName.Enabled = True
            txtCustomMediaListName.Text = String.Empty
            txtCustomMediaListQuery.Enabled = True
            txtCustomMediaListQuery.Text = String.Concat("SELECT * FROM ", cbCustomMediaListType.SelectedItem.ToString, "list")
        End If
    End Sub

    Private Sub dgvMainTabs_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMainTab.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub dgvMainTabs_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMainTab.SelectionChanged
        If dgvMainTab.SelectedCells.Count > 0 Then
            btnMainTabDown.Enabled = dgvMainTab.SelectedCells(0).OwningRow.Index < dgvMainTab.Rows.Count - 1
            btnMainTabRemove.Enabled = True
            btnMainTabUp.Enabled = dgvMainTab.SelectedCells(0).OwningRow.Index > 0
        Else
            btnMainTabDown.Enabled = False
            btnMainTabRemove.Enabled = False
            btnMainTabUp.Enabled = False
        End If
    End Sub

    Private Sub lnkCustomMediaListURL_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCustomMediaListURL.LinkClicked
        lnkCustomMediaListURL.LinkVisited = True
        Process.Start("http://embermediamanager.org/databasemodel/index.html")
    End Sub

    Private Sub LoadCustomMediaList()
        btnCustomMediaListRemove.Enabled = False
        txtCustomMediaListName.Enabled = False
        txtCustomMediaListName.Text = String.Empty
        txtCustomMediaListQuery.Enabled = False
        txtCustomMediaListQuery.Text = String.Empty

        cbCustomMediaList.Items.Clear()
        For Each ViewName In Master.DB.GetViewList(Enums.ContentType.None, True)
            cbCustomMediaList.Items.Add(ViewName)
        Next
        cbCustomMediaList.SelectedIndex = -1
        cbCustomMediaListType.SelectedIndex = -1
    End Sub

    Private Sub LoadMainTabDefaultList()
        colMainTabDefaultList.Items.Clear()
        For Each ViewName In Master.DB.GetViewList(Enums.ContentType.None, False)
            colMainTabDefaultList.Items.Add(ViewName)
        Next
    End Sub

    Private Sub LoadMainTabs()
        dgvMainTab.Rows.Clear()
        For Each nMainTab In Master.eSettings.GeneralMainTabSorting.OrderBy(Function(f) f.Order)
            dgvMainTab.Rows.Add(New Object() {nMainTab.Title, nMainTab.DefaultList})
        Next
        dgvMainTab.ClearSelection()
    End Sub

    Public Sub SaveChanges()
        Master.eSettings.GeneralMainTabSorting.Clear()
        Dim i As Integer = 0
        For Each r As DataGridViewRow In dgvMainTab.Rows
            If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso
                    Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) AndAlso
                    (Master.eSettings.GeneralMainTabSorting.FindIndex(Function(f) f.Title = r.Cells(0).Value.ToString) = -1) Then
                Dim nContentType As Enums.ContentType
                Select Case True
                    Case r.Cells(1).Value.ToString.StartsWith("movie")
                        nContentType = Enums.ContentType.Movie
                    Case r.Cells(1).Value.ToString.StartsWith("set")
                        nContentType = Enums.ContentType.MovieSet
                    Case r.Cells(1).Value.ToString.StartsWith("tvshow")
                        nContentType = Enums.ContentType.TV
                End Select
                Master.eSettings.GeneralMainTabSorting.Add(New Settings.MainTabSorting With {
                                                           .ContentType = nContentType,
                                                           .DefaultList = r.Cells(1).Value.ToString,
                                                           .Order = i,
                                                           .Title = r.Cells(0).Value.ToString})
                i += 1
            End If
        Next
    End Sub

    Private Sub txtCustomMediaListName_TextChanged(sender As Object, e As EventArgs) Handles txtCustomMediaListName.TextChanged
        ValidateSQL()
    End Sub

    Private Sub txtCustomMediaListQuery_TextChanged(sender As Object, e As EventArgs) Handles txtCustomMediaListQuery.TextChanged
        ValidateSQL()
    End Sub

    Private Sub ValidateSQL()
        If Not String.IsNullOrEmpty(txtCustomMediaListName.Text) AndAlso Not String.IsNullOrEmpty(txtCustomMediaListPrefix.Text) AndAlso Not String.IsNullOrEmpty(txtCustomMediaListQuery.Text) AndAlso
            txtCustomMediaListQuery.Text.ToLower.StartsWith("select ") AndAlso txtCustomMediaListQuery.Text.ToLower.Contains(String.Concat(txtCustomMediaListPrefix.Text.Replace("-", String.Empty), "list")) Then
            btnCustomMediaListAdd.Enabled = True
            btnCustomMediaListRemove.Enabled = True
        Else
            btnCustomMediaListAdd.Enabled = False
            btnCustomMediaListRemove.Enabled = False
        End If
        If Not String.IsNullOrEmpty(txtCustomMediaListPrefix.Text) AndAlso Not String.IsNullOrEmpty(txtCustomMediaListName.Text) Then
            btnCustomMediaListRemove.Enabled = True
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
