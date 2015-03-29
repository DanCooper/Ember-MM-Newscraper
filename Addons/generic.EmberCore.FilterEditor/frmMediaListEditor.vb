Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Text.RegularExpressions

Public Class frmMediaListEditor

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private bolHasChanged As Boolean = False

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
        cbMediaListType.Items.Add(Master.eLang.GetString(1379, "movie"))
        cbMediaListType.Items.Add(Master.eLang.GetString(1380, "tvshow"))
        cbMediaListType.Items.Add(Master.eLang.GetString(1381, "movieset"))
        lbl_FilterHelp.Text = Master.eLang.GetString(1382, "Result of query must contain either field idMovie (Movie-Filter), idSet(Set-Filter) or idShow(Show-Filter) or/and idMedia!")
        lbl_FilterURL.Text = Master.eLang.GetString(1383, "Complete overview of Ember datatables:")
        linklbl_FilterURL.Text = Master.eLang.GetString(1384, "Ember Database")
        cbMediaListType.SelectedIndex = 0
    End Sub

    Private Sub GetViews()
        Me.txtViewName.Text = String.Empty
        Me.txtViewQuery.Text = String.Empty
        cbMediaList.Items.Clear()
        For Each ViewName In Master.DB.GetViewList
            cbMediaList.Items.Add(ViewName)
        Next
        cbMediaList.SelectedIndex = -1
    End Sub

    Private Sub cbMediaList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMediaList.SelectedIndexChanged
        If String.IsNullOrEmpty(cbMediaList.SelectedItem.ToString) Then
            Me.txtViewName.Text = String.Empty
            Me.txtViewQuery.Text = String.Empty
        Else
            Me.txtViewName.Text = String.Empty
            Me.txtViewQuery.Text = String.Empty
            Dim SQL As String = Master.DB.GetViewDetails(cbMediaList.SelectedItem.ToString)
            If Not String.IsNullOrEmpty(SQL) Then
                Dim SQLStatement As Match = Regex.Match(SQL, "CREATE VIEW '?(?<VIEWNAME>.*?)'?\s?AS.*?(?<QUERY>SELECT.*)", RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Me.txtViewName.Text = SQLStatement.Groups(1).Value.ToString
                Me.txtViewQuery.Text = SQLStatement.Groups(2).Value.ToString.Trim
            End If
        End If
    End Sub

    Private Sub btnAddFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddView.Click
        If Not String.IsNullOrEmpty(txtViewName.Text) OrElse String.IsNullOrEmpty(Me.txtViewQuery.Text) Then
            Master.DB.DeleteView(txtViewName.Text)
            If Master.DB.AddView(String.Concat("CREATE VIEW '", txtViewName.Text, "' AS ", txtViewQuery.Text)) Then
                MessageBox.Show("Added View sucessfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GetViews()
            Else
                MessageBox.Show("Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnRemoveFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveView.Click
        If Not String.IsNullOrEmpty(txtViewName.Text) Then
            Master.DB.DeleteView(Me.txtViewName.Text)
            GetViews()
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
        System.Diagnostics.Process.Start("https://dl.dropboxusercontent.com/u/7856680/EmberDatabase/Tables/Index.html")
    End Sub

    Private Sub AddView()
        Master.DB.AddView(Me.txtViewQuery.Text)
    End Sub

#Region "Nested Types"

#End Region 'Nested Types

End Class
