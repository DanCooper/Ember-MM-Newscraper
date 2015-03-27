Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports NLog

Public Class frmFilterEditor

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private xmlFilter As xFilters

#End Region 'Fields

    Public Event ModuleSettingsChanged()

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        SetUp()
        If File.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Queries.xml")) Then
            xmlFilter = xFilters.Load(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Queries.xml"))
        End If
        GetFilter(0)
    End Sub

    Private Sub SetUp()
        btnAddFilter.Text = Master.eLang.GetString(28, "Add")
        btnRemoveFilter.Text = Master.eLang.GetString(30, "Remove")
        gpb_Filter.Text = Master.eLang.GetString(624, "Current Filter")
        lbl_Filter.Text = Master.eLang.GetString(330, "Filter")
        lbl_FilterName.Text = Master.eLang.GetString(232, "Name")
        lbl_FilterQuery.Text = Master.eLang.GetString(1377, "Query")
        lbl_FilterType.Text = Master.eLang.GetString(1378, "Type")
        cbo_FilterType.Items.Add(Master.eLang.GetString(1379, "movie"))
        cbo_FilterType.Items.Add(Master.eLang.GetString(1380, "tvshow"))
        cbo_FilterType.Items.Add(Master.eLang.GetString(1381, "movieset"))
        lbl_FilterHelp.Text = Master.eLang.GetString(1382, "Result of query must contain either field idMovie (Movie-Filter), idSet(Set-Filter) or idShow(Show-Filter) or/and idMedia!")
        lbl_FilterURL.Text = Master.eLang.GetString(1383, "Complete overview of Ember datatables:")
        linklbl_FilterURL.Text = Master.eLang.GetString(1384, "Ember Database")
        cbo_FilterType.SelectedIndex = 0
    End Sub

    Private Sub GetFilter(ByVal index As Integer)
        cb_Filter.Items.Clear()
        If Not xmlFilter Is Nothing Then
            For Each FilterName In xmlFilter.listOfFilter
                cb_Filter.Items.Add(FilterName.name)
            Next
        End If
        If cb_Filter.Items.Count > 0 Then
            cb_Filter.SelectedIndex = index
        Else
            cb_Filter.SelectedIndex = -1
        End If
    End Sub

    Public Sub SaveChanges()
        Dim aPath As String = ""
        If Directory.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar)) Then
            aPath = String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Queries.xml")
        End If
        If Not String.IsNullOrEmpty(aPath) Then
            xmlFilter.Save(aPath)
            Dim objStreamReader As New StreamReader(aPath)
            Dim xFilter As New XmlSerializer(APIXML.FilterXML.GetType)

            APIXML.FilterXML = CType(xFilter.Deserialize(objStreamReader), clsXMLFilter)
            objStreamReader.Close()
        End If
    End Sub

    Private Sub cb_Filter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Filter.SelectedIndexChanged
        btnRemoveFilter.Enabled = False
        txt_FilterName.Text = ""
        txt_FilterQuery.Text = ""
        cbo_FilterType.SelectedIndex = -1
        If Not xmlFilter Is Nothing AndAlso cb_Filter.SelectedIndex >= 0 Then
            For Each sett As xFilter In xmlFilter.listOfFilter.Where(Function(y) y.name.Contains(cb_Filter.SelectedItem.ToString))
                txt_FilterName.Text = sett.name
                txt_FilterQuery.Text = sett.query
                Select Case sett.type
                    Case "movie"
                        cbo_FilterType.SelectedIndex = 0
                    Case "tvshow"
                        cbo_FilterType.SelectedIndex = 1
                    Case "movieset"
                        cbo_FilterType.SelectedIndex = 2
                End Select
                btnRemoveFilter.Enabled = True
                Exit For
            Next
        End If
    End Sub

    Private Sub btnAddFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFilter.Click
        'AddNewView()
        Master.DB.DeleteView("dsssssssssssssssssssssssfewgreger w tzt ttrzwrt ")
        If String.IsNullOrEmpty(txt_FilterName.Text) OrElse String.IsNullOrEmpty(txt_FilterQuery.Text) OrElse String.IsNullOrEmpty(cbo_FilterType.Text) Then
            Dim response As String = Master.eLang.GetString(1385, "Name, query and type of filter needs to be set!")
            MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
        Else
            If txt_FilterQuery.Text.ToUpper.Contains("UPDATE") OrElse txt_FilterQuery.Text.ToUpper.Contains("INSERT") OrElse txt_FilterQuery.Text.ToUpper.Contains("DELETE") Then
                MessageBox.Show(Master.eLang.GetString(1386, "Invalid SQL!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                Exit Sub
            ElseIf Master.DB.IsValid_SQL(txt_FilterQuery.Text) = False Then
                Exit Sub
            End If

            btnRemoveFilter.Enabled = True
            If xmlFilter IsNot Nothing Then
                For Each sett As xFilter In xmlFilter.listOfFilter.Where(Function(y) y.name.Equals(txt_FilterName.Text))
                    sett.name = txt_FilterName.Text
                    sett.query = txt_FilterQuery.Text
                    Select Case cbo_FilterType.SelectedIndex
                        Case 0
                            sett.type = "movie"
                        Case 1
                            sett.type = "tvshow"
                        Case 2
                            sett.type = "movieset"
                    End Select
                    cb_Filter.SelectedText = txt_FilterName.Text
                    RaiseEvent ModuleSettingsChanged()
                    MessageBox.Show(Master.eLang.GetString(1376, "Saved changes") & "!", Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                    Exit Sub
                Next
            Else
                xmlFilter = New xFilters
            End If
            Dim g As New xFilter
            g.name = txt_FilterName.Text
            g.query = txt_FilterQuery.Text
            Select Case cbo_FilterType.SelectedIndex
                Case 0
                    g.type = "movie"
                Case 1
                    g.type = "tvshow"
                Case 2
                    g.type = "movieset"
            End Select
            xmlFilter.listOfFilter.Add(g)
            GetFilter(xmlFilter.listOfFilter.Count - 1)
            MessageBox.Show(Master.eLang.GetString(1376, "Saved changes") & "!", Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
            RaiseEvent ModuleSettingsChanged()
        End If

    End Sub

    Private Sub IsValidSQL(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFilter.Click
        If Not xmlFilter Is Nothing Then
            xmlFilter.listOfFilter.RemoveAt(cb_Filter.SelectedIndex)
            GetFilter(0)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub btnRemoveFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFilter.Click
        If Not xmlFilter Is Nothing Then
            xmlFilter.listOfFilter.RemoveAt(cb_Filter.SelectedIndex)
            GetFilter(0)
            RaiseEvent ModuleSettingsChanged()
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

    Private Sub AddNewView()
        Master.DB.AddView()
    End Sub

#Region "Nested Types"
    <XmlRoot("filters")> _
    Class xFilters
        <XmlElement("filter")> _
        Public listOfFilter As New List(Of xFilter)

        Public Shared Function Load(ByVal fpath As String) As xFilters
            Dim conf As xFilters = New xFilters
            Try
                If Not File.Exists(fpath) Then Return New xFilters
                Dim xmlSer As XmlSerializer
                xmlSer = New XmlSerializer(GetType(xFilters))
                Using xmlSW As New StreamReader(Path.Combine(Functions.AppPath, fpath))
                    conf = DirectCast(xmlSer.Deserialize(xmlSW), xFilters)
                End Using
            Catch ex As Exception
            End Try
            Return conf
        End Function
        Public Sub Save(ByVal fpath As String)
            Dim xmlSer As New XmlSerializer(GetType(xFilters))
            Using xmlSW As New StreamWriter(fpath)
                xmlSer.Serialize(xmlSW, Me)
            End Using
        End Sub
    End Class
    Class xFilter
        <XmlAttribute("name")> _
        Public name As String
        <XmlAttribute("type")> _
        Public type As String
        <XmlText()> _
        <XmlElement()> _
        Public query As String
    End Class
#End Region 'Nested Types

End Class
