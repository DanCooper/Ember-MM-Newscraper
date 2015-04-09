Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Text.RegularExpressions

Public Class frmMediaListEditor

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private needSave As Boolean = False

#End Region 'Fields

    Public Event ModuleSettingsChanged()

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        SetUp()
        GetViews()
        LoadCustomTabs()
    End Sub

    Private Sub SetUp()
        'btnAddView.Text = Master.eLang.GetString(28, "Add")
        'btnRemoveView.Text = Master.eLang.GetString(30, "Remove")
        'gpMediaListCurrent.Text = Master.eLang.GetString(624, "Current Filter")
        'lbl_FilterType.Text = Master.eLang.GetString(1378, "Type")
        'lblHelp.Text = Master.eLang.GetString(1382, "Result of query must contain either field idMovie (Movie-Filter), idSet(Set-Filter) or idShow(Show-Filter) or/and idMedia!")
        'lbl_FilterURL.Text = Master.eLang.GetString(1383, "Complete overview of Ember datatables:")
        'linklbl_FilterURL.Text = Master.eLang.GetString(1384, "Ember Database")
    End Sub

    Private Sub GetViews()
        Me.btnRemoveView.Enabled = False
        Me.txtView_Name.Enabled = False
        Me.txtView_Name.Text = String.Empty
        Me.txtView_Query.Enabled = False
        Me.txtView_Query.Text = String.Empty

        cbMediaList.Items.Clear()
        colCustomTabsList.Items.Clear()
        For Each ViewName In Master.DB.GetViewList(Enums.Content_Type.None)
            cbMediaList.Items.Add(ViewName)
            colCustomTabsList.Items.Add(ViewName)
        Next
        cbMediaList.SelectedIndex = -1
        cbMediaListType.SelectedIndex = -1

    End Sub

    Private Sub cbMediaList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMediaList.SelectedIndexChanged
        If Not cbMediaList.SelectedIndex = -1 Then
            Me.cbMediaListType.SelectedIndex = -1
            Me.btnRemoveView.Enabled = True
            Me.txtView_Name.Text = String.Empty
            Me.txtView_Query.Text = String.Empty
            Dim SQL As Database.SQLViewProperty = Master.DB.GetViewDetails(cbMediaList.SelectedItem.ToString)
            If Not String.IsNullOrEmpty(SQL.Name) AndAlso Not String.IsNullOrEmpty(SQL.Statement) Then
                Me.txtView_Name.Enabled = True
                Me.txtView_Query.Enabled = True
                Dim SQLPrefixName As Match = Regex.Match(SQL.Name, "(?<PREFIX>movie-|movieset-|tvshow-|seasons-|episode-)(?<NAME>.*)", RegexOptions.Singleline)
                Dim SQLQuery As Match = Regex.Match(SQL.Statement, "(?<QUERY>SELECT.*)", RegexOptions.Singleline)
                Me.txtView_Prefix.Text = SQLPrefixName.Groups(1).Value.ToString
                Me.txtView_Name.Text = SQLPrefixName.Groups(2).Value.ToString
                Me.txtView_Query.Text = SQLQuery.Groups(1).Value.ToString.Trim
            Else
                Me.txtView_Name.Enabled = False
                Me.txtView_Query.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnAddFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddView.Click
        If Not String.IsNullOrEmpty(txtView_Name.Text) OrElse String.IsNullOrEmpty(Me.txtView_Query.Text) Then
            Master.DB.DeleteView(String.Concat(Me.txtView_Prefix.Text, Me.txtView_Name.Text))
            If Master.DB.AddView(String.Concat("CREATE VIEW '", Me.txtView_Prefix.Text, txtView_Name.Text, "' AS ", txtView_Query.Text)) Then
                MessageBox.Show("Added View sucessfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetViews()
            Else
                MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnRemoveFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveView.Click
        If Not String.IsNullOrEmpty(Me.txtView_Name.Text) Then
            Master.DB.DeleteView(String.Concat(Me.txtView_Prefix.Text, Me.txtView_Name.Text))
            GetViews()
        End If
    End Sub

    Private Sub cbMediaListType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMediaListType.SelectedIndexChanged
        If Not Me.cbMediaListType.SelectedIndex = -1 Then
            If Not needSave Then
                Me.cbMediaList.SelectedIndex = -1
                Me.txtView_Name.Enabled = True
                Me.txtView_Prefix.Text = String.Concat(Me.cbMediaListType.SelectedItem.ToString, "-")
                Me.txtView_Name.Enabled = True
                Me.txtView_Name.Text = String.Empty
                Me.txtView_Query.Enabled = True
                Me.txtView_Query.Text = String.Concat("SELECT * FROM ", cbMediaListType.SelectedItem.ToString, "list")
            End If
        End If
    End Sub

    Private Sub linklbl_FilterURL_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklbl_FilterURL.LinkClicked
        linklbl_FilterURL.LinkVisited = True
        System.Diagnostics.Process.Start("http://embermediamanager.org/databasemodel/index.html")
    End Sub

    Private Sub txtView_Name_TextChanged(sender As Object, e As EventArgs) Handles txtView_Name.TextChanged
        ValidateSQL()
    End Sub

    Private Sub txtView_Query_TextChanged(sender As Object, e As EventArgs) Handles txtView_Query.TextChanged
        ValidateSQL()
    End Sub

    Private Sub ValidateSQL()
        If Not String.IsNullOrEmpty(Me.txtView_Name.Text) AndAlso Not String.IsNullOrEmpty(Me.txtView_Prefix.Text) AndAlso Not String.IsNullOrEmpty(Me.txtView_Query.Text) AndAlso _
            Me.txtView_Query.Text.ToLower.StartsWith("select ") AndAlso Me.txtView_Query.Text.ToLower.Contains(String.Concat(Me.txtView_Prefix.Text.Replace("-", String.Empty), "list")) Then
            Me.btnAddView.Enabled = True
            Me.btnRemoveView.Enabled = True
        Else
            Me.btnAddView.Enabled = False
            Me.btnRemoveView.Enabled = False
        End If
        If Not String.IsNullOrEmpty(Me.txtView_Prefix.Text) AndAlso Not String.IsNullOrEmpty(Me.txtView_Name.Text) Then
            Me.btnRemoveView.Enabled = True
        End If
    End Sub

    Public Sub SaveChanges()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In clsAdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("CustomTabs:"))
            deleteitem.Add(sett.Name)
        Next
        Using settings = New clsAdvancedSettings()
            For Each s As String In deleteitem
                settings.CleanSetting(s, "*EmberAPP")
            Next

            Dim CustomTabs As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each r As DataGridViewRow In dgvCustomTabs.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso (CustomTabs.FindIndex(Function(f) f.Name = r.Cells(0).Value.ToString) = -1) Then
                    CustomTabs.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = r.Cells(0).Value.ToString, .Value = r.Cells(1).Value.ToString})
                End If
            Next
            If CustomTabs IsNot Nothing Then
                settings.SetComplexSetting("CustomTabs", CustomTabs, "*EmberAPP")
            End If
        End Using
    End Sub

    Private Sub LoadCustomTabs()
        dgvCustomTabs.Rows.Clear()
        Dim CustomTabs As List(Of AdvancedSettingsComplexSettingsTableItem) = clsAdvancedSettings.GetComplexSetting("CustomTabs", "*EmberAPP")
        If CustomTabs IsNot Nothing Then
            For Each sett In CustomTabs
                Dim i As Integer = dgvCustomTabs.Rows.Add(New Object() {sett.Name, sett.Value})
            Next
        End If
        dgvCustomTabs.ClearSelection()
    End Sub

    Private Sub btnCustomTabsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomTabsAdd.Click
        Dim i As Integer = dgvCustomTabs.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvCustomTabs.Rows(i).Tag = False
        dgvCustomTabs.CurrentCell = dgvCustomTabs.Rows(i).Cells(0)
        dgvCustomTabs.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

#Region "Nested Types"

#End Region 'Nested Types

End Class
