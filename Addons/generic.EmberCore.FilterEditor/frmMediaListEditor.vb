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
    End Sub

    Private Sub SetUp()
        btnAddView.Text = Master.eLang.GetString(28, "Add")
        btnRemoveView.Text = Master.eLang.GetString(30, "Remove")
        gpMediaList.Text = Master.eLang.GetString(624, "Current Filter")
        lbl_MediaLists.Text = Master.eLang.GetString(330, "Filter")
        lbl_FilterType.Text = Master.eLang.GetString(1378, "Type")
        lblHelp.Text = Master.eLang.GetString(1382, "Result of query must contain either field idMovie (Movie-Filter), idSet(Set-Filter) or idShow(Show-Filter) or/and idMedia!")
        lbl_FilterURL.Text = Master.eLang.GetString(1383, "Complete overview of Ember datatables:")
        linklbl_FilterURL.Text = Master.eLang.GetString(1384, "Ember Database")
    End Sub

    Private Sub GetViews()
        Me.btnRemoveView.Enabled = False
        Me.txtView_Name.Text = String.Empty
        Me.txtView_Query.Text = String.Empty
        cbMediaList.Items.Clear()
        For Each ViewName In Master.DB.GetViewList(Enums.Content_Type.None)
            cbMediaList.Items.Add(ViewName)
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
            Dim SQL As Dictionary(Of String, String) = Master.DB.GetViewDetails(cbMediaList.SelectedItem.ToString)
            If SQL.Count = 1 Then
                Dim SQLPrefixName As Match = Regex.Match(SQL.Keys(0).ToString, "(?<PREFIX>movie-|movieset-|tvshow-|seasons-|episode-)(?<NAME>.*)", RegexOptions.Singleline)
                Dim SQLQuery As Match = Regex.Match(SQL.Values(0).ToString, "(?<QUERY>SELECT.*)", RegexOptions.Singleline)
                Me.txtView_Prefix.Text = SQLPrefixName.Groups(1).Value.ToString
                Me.txtView_Name.Text = SQLPrefixName.Groups(2).Value.ToString
                Me.txtView_Query.Text = SQLQuery.Groups(1).Value.ToString.Trim
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
                Me.txtView_Prefix.Text = String.Concat(Me.cbMediaListType.SelectedItem.ToString, "-")
                Me.txtView_Name.Text = String.Empty
                Me.txtView_Query.Text = String.Concat("SELECT * FROM ", cbMediaListType.SelectedItem.ToString, "list")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Open link using default browser
    ''' </summary>
    ''' <param name="sender">Linklabel click event</param>
    ''' <remarks>
    ''' 2015/02/14 Cocotus - First implementation
    Private Sub linklbl_FilterURL_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklbl_FilterURL.LinkClicked
        linklbl_FilterURL.LinkVisited = True
        System.Diagnostics.Process.Start("http://embermediamanager.org/databasemodel/index.html")
    End Sub

    Private Sub AddView()
        Master.DB.AddView(Me.txtView_Query.Text)
    End Sub

#Region "Nested Types"

#End Region 'Nested Types

End Class
