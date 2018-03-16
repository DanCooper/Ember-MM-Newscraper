Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Text.RegularExpressions

Public Class frmSettingsHolder

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
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
        SetUp()
        GetViews()
        LoadCustomTabs()
    End Sub

    Private Sub SetUp()
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
        Me.btnCustomMediaListRemove.Enabled = False
        Me.txtCustomMediaListName.Enabled = False
        Me.txtCustomMediaListName.Text = String.Empty
        Me.txtCustomMediaListQuery.Enabled = False
        Me.txtCustomMediaListQuery.Text = String.Empty

        cbCustomMediaList.Items.Clear()
        colCustomTabList.Items.Clear()
        For Each ViewName In Master.DB.GetViewList(Enums.ContentType.None)
            cbCustomMediaList.Items.Add(ViewName)
            colCustomTabList.Items.Add(ViewName)
        Next
        cbCustomMediaList.SelectedIndex = -1
        cbCustomMediaListType.SelectedIndex = -1

    End Sub

    Private Sub cbCustomMediaList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCustomMediaList.SelectedIndexChanged
        If Not cbCustomMediaList.SelectedIndex = -1 Then
            Me.cbCustomMediaListType.SelectedIndex = -1
            Me.btnCustomMediaListRemove.Enabled = True
            Me.txtCustomMediaListName.Text = String.Empty
            Me.txtCustomMediaListQuery.Text = String.Empty
            Dim SQL As Database.SQLViewProperty = Master.DB.GetViewDetails(cbCustomMediaList.SelectedItem.ToString)
            If Not String.IsNullOrEmpty(SQL.Name) AndAlso Not String.IsNullOrEmpty(SQL.Statement) Then
                Me.txtCustomMediaListName.Enabled = True
                Me.txtCustomMediaListQuery.Enabled = True
                Dim SQLPrefixName As Match = Regex.Match(SQL.Name, "(?<PREFIX>movie-|movieset-|tvshow-|seasons-|episode-)(?<NAME>.*)", RegexOptions.Singleline)
                Dim SQLQuery As Match = Regex.Match(SQL.Statement, "(?<QUERY>SELECT.*)", RegexOptions.Singleline)
                Me.txtCustomMediaListPrefix.Text = SQLPrefixName.Groups(1).Value.ToString
                Me.txtCustomMediaListName.Text = SQLPrefixName.Groups(2).Value.ToString
                Me.txtCustomMediaListQuery.Text = SQLQuery.Groups(1).Value.ToString.Trim
            Else
                Me.txtCustomMediaListName.Enabled = False
                Me.txtCustomMediaListQuery.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCustomMediaListAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomMediaListAdd.Click
        If Not String.IsNullOrEmpty(txtCustomMediaListName.Text) OrElse String.IsNullOrEmpty(Me.txtCustomMediaListQuery.Text) Then
            Master.DB.DeleteView(String.Concat(Me.txtCustomMediaListPrefix.Text, Me.txtCustomMediaListName.Text))
            If Master.DB.AddView(String.Concat("CREATE VIEW '", Me.txtCustomMediaListPrefix.Text, txtCustomMediaListName.Text, "' AS ", txtCustomMediaListQuery.Text)) Then
                MessageBox.Show("Added View sucessfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetViews()
            Else
                MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnCustomMediaListRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomMediaListRemove.Click
        If Not String.IsNullOrEmpty(Me.txtCustomMediaListName.Text) Then
            Dim ViewName As String = String.Concat(Me.txtCustomMediaListPrefix.Text, Me.txtCustomMediaListName.Text)
            Master.DB.DeleteView(ViewName)
            For Each dRow As DataGridViewRow In dgvCustomTab.Rows
                If dRow.Cells(1).Value.ToString = ViewName Then
                    dgvCustomTab.Rows.RemoveAt(dRow.Index)
                End If
            Next
            GetViews()
        End If
    End Sub

    Private Sub cbCustomMediaListType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCustomMediaListType.SelectedIndexChanged
        If Not Me.cbCustomMediaListType.SelectedIndex = -1 Then
            If Not needSave Then
                Me.cbCustomMediaList.SelectedIndex = -1
                Me.txtCustomMediaListName.Enabled = True
                Me.txtCustomMediaListPrefix.Text = String.Concat(Me.cbCustomMediaListType.SelectedItem.ToString, "-")
                Me.txtCustomMediaListName.Enabled = True
                Me.txtCustomMediaListName.Text = String.Empty
                Me.txtCustomMediaListQuery.Enabled = True
                Me.txtCustomMediaListQuery.Text = String.Concat("SELECT * FROM ", cbCustomMediaListType.SelectedItem.ToString, "list")
            End If
        End If
    End Sub

    Private Sub lnkCustomMediaListURL_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCustomMediaListURL.LinkClicked
        lnkCustomMediaListURL.LinkVisited = True
        System.Diagnostics.Process.Start("http://embermediamanager.org/databasemodel/index.html")
    End Sub

    Private Sub txtCustomMediaListName_TextChanged(sender As Object, e As EventArgs) Handles txtCustomMediaListName.TextChanged
        ValidateSQL()
    End Sub

    Private Sub txtCustomMediaListQuery_TextChanged(sender As Object, e As EventArgs) Handles txtCustomMediaListQuery.TextChanged
        ValidateSQL()
    End Sub

    Private Sub ValidateSQL()
        If Not String.IsNullOrEmpty(Me.txtCustomMediaListName.Text) AndAlso Not String.IsNullOrEmpty(Me.txtCustomMediaListPrefix.Text) AndAlso Not String.IsNullOrEmpty(Me.txtCustomMediaListQuery.Text) AndAlso _
            Me.txtCustomMediaListQuery.Text.ToLower.StartsWith("select ") AndAlso Me.txtCustomMediaListQuery.Text.ToLower.Contains(String.Concat(Me.txtCustomMediaListPrefix.Text.Replace("-", String.Empty), "list")) Then
            Me.btnCustomMediaListAdd.Enabled = True
            Me.btnCustomMediaListRemove.Enabled = True
        Else
            Me.btnCustomMediaListAdd.Enabled = False
            Me.btnCustomMediaListRemove.Enabled = False
        End If
        If Not String.IsNullOrEmpty(Me.txtCustomMediaListPrefix.Text) AndAlso Not String.IsNullOrEmpty(Me.txtCustomMediaListName.Text) Then
            Me.btnCustomMediaListRemove.Enabled = True
        End If
    End Sub

    Public Sub SaveChanges()
        dgvCustomTab.ClearSelection()
        ValidateTabs()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("CustomTabs:"))
            deleteitem.Add(sett.Name)
        Next
        Using settings = New AdvancedSettings()
            For Each s As String In deleteitem
                settings.CleanSetting(s, "*EmberAPP")
            Next

            Dim CustomTabs As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each r As DataGridViewRow In dgvCustomTab.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso
                    Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) AndAlso
                    (CustomTabs.FindIndex(Function(f) f.Name = r.Cells(0).Value.ToString) = -1) Then
                    CustomTabs.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = r.Cells(0).Value.ToString, .Value = r.Cells(1).Value.ToString})
                End If
            Next
            If CustomTabs IsNot Nothing Then
                settings.SetComplexSetting("CustomTabs", CustomTabs, "*EmberAPP")
            End If
        End Using
    End Sub

    Private Sub LoadCustomTabs()
        dgvCustomTab.Rows.Clear()
        Dim CustomTabs As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("CustomTabs", "*EmberAPP")
        If CustomTabs IsNot Nothing Then
            For Each sett In CustomTabs
                If colCustomTabList.Items.Contains(sett.Value) Then
                    dgvCustomTab.Rows.Add(New Object() {sett.Name, sett.Value})
                End If
            Next
        End If
        dgvCustomTab.ClearSelection()
    End Sub

    Private Sub btnCustomTabAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomTabAdd.Click
        Dim i As Integer = dgvCustomTab.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvCustomTab.Rows(i).Tag = False
        dgvCustomTab.CurrentCell = dgvCustomTab.Rows(i).Cells(0)
        dgvCustomTab.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnCustomTabRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomTabRemove.Click
        If dgvCustomTab.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvCustomTab.Rows(dgvCustomTab.SelectedCells(0).RowIndex).Tag) Then
            dgvCustomTab.Rows.RemoveAt(dgvCustomTab.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub dgvCustomTab_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCustomTab.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub dgvCustomTab_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCustomTab.LostFocus
        ValidateTabs()
    End Sub

    Private Sub dgvCustomTab_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCustomTab.SelectionChanged
        If dgvCustomTab.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvCustomTab.Rows(dgvCustomTab.SelectedCells(0).RowIndex).Tag) Then
            btnCustomTabRemove.Enabled = True
        Else
            btnCustomTabRemove.Enabled = False
        End If
    End Sub

    Private Sub ValidateTabs()
        For Each r As DataGridViewRow In dgvCustomTab.Rows
            If String.IsNullOrEmpty(r.Cells(0).Value.ToString) OrElse String.IsNullOrEmpty(r.Cells(1).Value.ToString) Then
                r.DefaultCellStyle = New DataGridViewCellStyle With {.BackColor = Drawing.Color.Red}
            Else
                r.DefaultCellStyle = New DataGridViewCellStyle
            End If
        Next
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
