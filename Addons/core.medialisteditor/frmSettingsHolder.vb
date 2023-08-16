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
    Private needSave As Boolean = False

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
        Setup()
        GetViews()
        DataGridView_MainTabs_Load()
    End Sub

    Private Sub Setup()
        btnCustomTabAdd.Text = Master.eLang.GetString(28, "Add")
        btnCustomTabRemove.Text = Master.eLang.GetString(30, "Remove")
        btnCustomMediaListAdd.Text = Master.eLang.GetString(28, "Add")
        btnCustomMediaListRemove.Text = Master.eLang.GetString(30, "Remove")
        colCustomTabList.HeaderText = Master.eLang.GetString(1394, "List")
        colCustomTabName.HeaderText = Master.eLang.GetString(232, "Name")
        gbCustomMediaList.Text = Master.eLang.GetString(1390, "Custom Media Lists")
        gbCustomTab.Text = Master.eLang.GetString(1393, "Custom Tabs")
        lblCustomMediaListHelp.Text = Master.eLang.GetString(1392, "Use CTRL + Return for new lines.")
        lblCustomMediaListType.Text = Master.eLang.GetString(1288, "Type")
        lblCustomMediaListURL.Text = Master.eLang.GetString(1383, "Complete overview of Ember datatables:")
        lblPrefix.Text = Master.eLang.GetString(1391, "Prefix")
        lnkCustomMediaListURL.Text = Master.eLang.GetString(1384, "Ember Database")
    End Sub

    Private Sub GetViews()
        btnCustomMediaListRemove.Enabled = False
        txtCustomMediaListName.Enabled = False
        txtCustomMediaListName.Text = String.Empty
        txtCustomMediaListQuery.Enabled = False
        txtCustomMediaListQuery.Text = String.Empty

        cbCustomMediaList.Items.Clear()
        colCustomTabList.Items.Clear()
        colCustomTabList.Items.Add("movielist")
        colCustomTabList.Items.Add("setslist")
        colCustomTabList.Items.Add("tvshowlist")
        For Each ViewName In Master.DB.View_GetList(Enums.ContentType.None)
            cbCustomMediaList.Items.Add(ViewName)
            colCustomTabList.Items.Add(ViewName)
        Next
        cbCustomMediaList.SelectedIndex = -1
        cbCustomMediaListType.SelectedIndex = -1

    End Sub

    Private Sub cbCustomMediaList_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCustomMediaList.SelectedIndexChanged
        If Not cbCustomMediaList.SelectedIndex = -1 Then
            cbCustomMediaListType.SelectedIndex = -1
            btnCustomMediaListRemove.Enabled = True
            txtCustomMediaListName.Text = String.Empty
            txtCustomMediaListQuery.Text = String.Empty
            Dim SQL As Database.SQLViewProperty = Master.DB.View_GetProperty(cbCustomMediaList.SelectedItem.ToString)
            If Not String.IsNullOrEmpty(SQL.Name) AndAlso Not String.IsNullOrEmpty(SQL.Statement) Then
                txtCustomMediaListName.Enabled = True
                txtCustomMediaListQuery.Enabled = True
                Dim SQLPrefixName As Match = Regex.Match(SQL.Name, "(?<PREFIX>movie-|movieset-|tvshow-|seasons-|episode-)(?<NAME>.*)", RegexOptions.Singleline)
                Dim SQLQuery As Match = Regex.Match(SQL.Statement, "(?<QUERY>SELECT.*)", RegexOptions.Singleline)
                txtCustomMediaListPrefix.Text = SQLPrefixName.Groups(1).Value.ToString
                txtCustomMediaListName.Text = SQLPrefixName.Groups(2).Value.ToString
                txtCustomMediaListQuery.Text = SQLQuery.Groups(1).Value.ToString.Trim
            Else
                txtCustomMediaListName.Enabled = False
                txtCustomMediaListQuery.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCustomMediaListAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomMediaListAdd.Click
        If Not String.IsNullOrEmpty(txtCustomMediaListName.Text) OrElse String.IsNullOrEmpty(txtCustomMediaListQuery.Text) Then
            Master.DB.View_Delete(String.Concat(txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text))
            If Master.DB.View_Add(String.Concat("CREATE VIEW '", txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text, "' AS ", txtCustomMediaListQuery.Text)) Then
                MessageBox.Show("Added View sucessfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetViews()
            Else
                MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnCustomMediaListRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomMediaListRemove.Click
        If Not String.IsNullOrEmpty(txtCustomMediaListName.Text) Then
            Dim ViewName As String = String.Concat(txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text)
            Master.DB.View_Delete(ViewName)
            For Each dRow As DataGridViewRow In dgvCustomTab.Rows
                If dRow.Cells(1).Value.ToString = ViewName Then
                    dgvCustomTab.Rows.RemoveAt(dRow.Index)
                End If
            Next
            GetViews()
        End If
    End Sub

    Private Sub cbCustomMediaListType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCustomMediaListType.SelectedIndexChanged
        If Not cbCustomMediaListType.SelectedIndex = -1 Then
            If Not needSave Then
                cbCustomMediaList.SelectedIndex = -1
                txtCustomMediaListName.Enabled = True
                txtCustomMediaListPrefix.Text = String.Concat(cbCustomMediaListType.SelectedItem.ToString, "-")
                txtCustomMediaListName.Enabled = True
                txtCustomMediaListName.Text = String.Empty
                txtCustomMediaListQuery.Enabled = True
                txtCustomMediaListQuery.Text = String.Concat("SELECT * FROM ", cbCustomMediaListType.SelectedItem.ToString, "list")
            End If
        End If
    End Sub

    Private Sub DataGridView_MainTabs_Add(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomTabAdd.Click
        Dim i As Integer = dgvCustomTab.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvCustomTab.Rows(i).Tag = False
        dgvCustomTab.CurrentCell = dgvCustomTab.Rows(i).Cells(0)
        dgvCustomTab.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub DataGridView_MainTabs_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvCustomTab.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub DataGridView_MainTabs_Load()
        dgvCustomTab.Rows.Clear()
        For Each sett In Master.eSettings.GeneralMainTabSorting.OrderBy(Function(f) f.Order)
            If colCustomTabList.Items.Contains(sett.DefaultList) Then
                dgvCustomTab.Rows.Add(New Object() {sett.Title, sett.DefaultList})
            End If
        Next
        dgvCustomTab.ClearSelection()
    End Sub

    Private Sub DataGridView_MainTabs_Remove(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomTabRemove.Click
        If dgvCustomTab.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvCustomTab.Rows(dgvCustomTab.SelectedCells(0).RowIndex).Tag) Then
            dgvCustomTab.Rows.RemoveAt(dgvCustomTab.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub DataGridView_MainTabs_Save()
        Dim nMainTabs As New List(Of Settings.MainTabSorting)
        Dim i As Integer = 0
        For Each r As DataGridViewRow In dgvCustomTab.Rows
            If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso (nMainTabs.FindIndex(Function(f) f.Title = r.Cells(0).Value.ToString) = -1) Then
                Dim tabType As Enums.ContentType
                Select Case True
                    Case r.Cells(1).Value.ToString.StartsWith("movie-"), r.Cells(1).Value.ToString = "movielist"
                        tabType = Enums.ContentType.Movie
                    Case r.Cells(1).Value.ToString.StartsWith("sets-"), r.Cells(1).Value.ToString = "setslist"
                        tabType = Enums.ContentType.MovieSet
                    Case r.Cells(1).Value.ToString.StartsWith("tvshow-"), r.Cells(1).Value.ToString = "tvshowlist"
                        tabType = Enums.ContentType.TV
                End Select
                nMainTabs.Add(New Settings.MainTabSorting With {.ContentType = tabType, .DefaultList = r.Cells(1).Value.ToString, .Order = i, .Title = r.Cells(0).Value.ToString})
                i += 1
            End If
        Next
        Master.eSettings.GeneralMainTabSorting = nMainTabs
    End Sub


    Private Sub DataGridView_MainTabs_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvCustomTab.SelectionChanged
        If dgvCustomTab.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvCustomTab.Rows(dgvCustomTab.SelectedCells(0).RowIndex).Tag) Then
            btnCustomTabRemove.Enabled = True
        Else
            btnCustomTabRemove.Enabled = False
        End If
    End Sub

    Private Sub lnkCustomMediaListURL_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCustomMediaListURL.LinkClicked
        lnkCustomMediaListURL.LinkVisited = True
        Process.Start("http://embermediamanager.org/databasemodel/index.html")
    End Sub

    Private Sub txtCustomMediaListName_TextChanged(sender As Object, e As EventArgs) Handles txtCustomMediaListName.TextChanged
        ValidateSQL()
    End Sub

    Private Sub txtCustomMediaListQuery_TextChanged(sender As Object, e As EventArgs) Handles txtCustomMediaListQuery.TextChanged
        ValidateSQL()
    End Sub

    Public Sub SaveChanges()
        DataGridView_MainTabs_Save()
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

End Class