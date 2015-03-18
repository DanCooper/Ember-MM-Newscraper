Imports System.Windows.Forms
Imports System.IO
Imports System.Xml.Serialization
Imports EmberAPI

Public Class dlgTVRegExProfiles

#Region "Fields"

    Public ShowRegex As New List(Of Settings.regexp)
    Private MyTVShowRegExProfiles As New TVShowRegExProfiles

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If lvProfiles.SelectedItems.Count > 0 Then
                ShowRegex.Clear()
                ShowRegex.AddRange(MyTVShowRegExProfiles.Profiles.FirstOrDefault(Function(y) y.Name = lvProfiles.SelectedItems(0).Text).ShowRegex)
            End If
        Catch ex As Exception
        End Try
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgTVRegExProfiles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
        GetProfiles()
        PopulateList()
    End Sub

    Private Sub lstProfiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvProfiles.SelectedIndexChanged
        If lvProfiles.SelectedItems.Count > 0 Then
            Try
                txtDescription.Text = lvProfiles.Items(lvProfiles.SelectedIndices(0)).Tag.ToString
                OK_Button.Enabled = True
            Catch ex As Exception
            End Try
        Else
            txtDescription.Text = String.Empty
            OK_Button.Enabled = False
        End If
    End Sub

    Sub PopulateList()
        lvProfiles.Items.Clear()
        For Each s As TVShowRegExProfile In MyTVShowRegExProfiles.Profiles
            lvProfiles.Items.Add(s.Name).Tag = s.Description.Replace("\n", Environment.NewLine)
        Next
    End Sub

    Public Function GetProfiles() As Boolean
        Dim sHTTP As New HTTP
        Dim xmlSer As XmlSerializer = Nothing
        Dim updateXML As String = sHTTP.DownloadData("http://pcjco.dommel.be/emm-r/updates/setup/TVRegExProfiles.xml")
        sHTTP = Nothing
        If updateXML.Length > 0 Then
            Using xmlSR As StringReader = New StringReader(updateXML)
                xmlSer = New XmlSerializer(GetType(TVShowRegExProfiles))
                MyTVShowRegExProfiles = DirectCast(xmlSer.Deserialize(xmlSR), TVShowRegExProfiles)
            End Using
        End If
    End Function

    Sub SetUp()
        Me.Text = Master.eLang.GetString(819, "TV RegEx Profiles")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        lvProfiles.Columns(0).Text = Master.eLang.GetString(820, "RegEx Profile")
        Me.lblDescription.Text = Master.eLang.GetString(172, "Description:")
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Class TVShowRegExProfiles
        Public Profiles As New List(Of TVShowRegExProfile)
    End Class
    Class TVShowRegExProfile
        Public Name As String
        Public Description As String
        Public ShowRegex As New List(Of Settings.regexp)
    End Class

#End Region 'Nested Types

End Class
