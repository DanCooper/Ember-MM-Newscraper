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
Imports System.Net
Imports System.Text

Public Class dlgErrorViewer

#Region "Fields"

    Private sBuilder As StringBuilder

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Sub Show(ByVal owner As Form)
        If Visible Then
            BuildErrorLog()
            Activate()
        Else
            MyBase.Owner = owner
            MyBase.Show(owner)
        End If
    End Sub

    Public Sub UpdateLog()
        BuildErrorLog()
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy.Click
        Select Case btnCopy.Tag.ToString
            Case "p"
                Dim bReturn As String = String.Empty
                Using wc As New WebClient
                    ServicePointManager.Expect100Continue = False
                    Dim nvColl As New Specialized.NameValueCollection
                    nvColl.Add("paste_code", sBuilder.ToString)
                    nvColl.Add("paste_subdomain", "embermm")
                    wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                    bReturn = Encoding.ASCII.GetString(wc.UploadValues("http://pastebin.com/api_public.php", "POST", nvColl))
                    nvColl = Nothing
                End Using

                If Not String.IsNullOrEmpty(bReturn) OrElse Not bReturn.ToLower.Contains("error") Then
                    txtPastebinURL.Text = bReturn
                Else
                    txtPastebinURL.Text = Master.eLang.GetString(807, "An error occurred when attempting to send data to Pastebin.com")
                End If
            Case Else
                Clipboard.SetText(sBuilder.ToString)
        End Select
    End Sub
    ''' <summary>
    ''' Builds the text that will be displayed in the Error Viewer dialog, and places it in the textbox. 
    ''' It also enables the action buttons (copy to clipboard/pastebin)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildErrorLog()
        btnCopy.Enabled = False

        sBuilder = New StringBuilder

        sBuilder.AppendLine("================= <Assembly Versions> =================")
        sBuilder.AppendLine(String.Empty)
        sBuilder.AppendLine("Platform: x86")
        For Each v As Addons.VersionItem In Addons.VersionList
            sBuilder.AppendLine(String.Format("{0} (Revision: {1})", v.AssemblyFileName, v.Version))
        Next
        sBuilder.AppendLine(String.Empty)
        sBuilder.AppendLine("================= <Assembly Versions> =================")

        sBuilder.AppendLine(String.Empty)
        sBuilder.AppendLine(String.Empty)

        txtError.Text = sBuilder.ToString

        If txtError.Lines.Count > 50 Then
            btnCopy.Text = Master.eLang.GetString(805, "Send to PasteBin.com")
            btnCopy.Tag = "p"
            btnCopy.Visible = True
            txtPastebinURL.Visible = True
            lblPastebinURL.Visible = True
        Else
            btnCopy.Text = Master.eLang.GetString(806, "Copy to Clipboard")
            btnCopy.Tag = "c"
            btnCopy.Visible = True
            txtPastebinURL.Visible = False
            lblPastebinURL.Visible = False
        End If

        btnCopy.Enabled = True
    End Sub

    Private Sub dlgErrorViewer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()
        BuildErrorLog()
    End Sub

    Private Sub llblURL_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles llblURL.LinkClicked
        Process.Start("http://bugs.embermediamanager.org/thebuggenie/embermediamanager/issues/open")
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        Close()
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(808, "Error Log Viewer")
        lblInfo.Text = Master.eLang.GetString(809, "Before submitting bug reports, please verify that the bug has not already been reported. You can view a listing of all known bugs here:")
        llblURL.Text = Master.eLang.GetString(810, "https://sourceforge.net/apps/trac/emm-r/")
        lblPastebinURL.Text = Master.eLang.GetString(811, "PasteBin URL:")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
    End Sub

#End Region 'Methods

End Class