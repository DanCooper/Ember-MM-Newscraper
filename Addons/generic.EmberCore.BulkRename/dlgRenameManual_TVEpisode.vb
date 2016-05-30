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

Imports System.IO
Imports EmberAPI

Public Class dlgRenameManual_TVEpisode

#Region "Fields"

    Friend WithEvents bwRename As New System.ComponentModel.BackgroundWorker

    Private _DBElement As New Database.DBElement(Enums.ContentType.TVEpisode)

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal DBElement As Database.DBElement)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _DBElement = DBElement
    End Sub

    Private Sub bwRename_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRename.DoWork
        FileFolderRenamer.RenameSingle_TVEpisode(_DBElement, txtFolder.Text, txtFile.Text, False, True, True)
    End Sub

    Private Sub bwRename_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRename.RunWorkerCompleted
        Cursor.Current = Cursors.Default
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub dlgRenameManual_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        SetUp()
        If FileUtils.Common.isVideoTS(_DBElement.Filename) Then
            txtFile.Text = "$F"
            txtFile.Visible = False
            txtFolder.Text = Directory.GetParent(Directory.GetParent(_DBElement.Filename).FullName).Name
        ElseIf FileUtils.Common.isBDRip(_DBElement.Filename) Then
            txtFile.Text = "$F"
            txtFile.Visible = False
            txtFolder.Text = Directory.GetParent(Directory.GetParent(Directory.GetParent(_DBElement.Filename).FullName).FullName).Name
        Else
            Dim FileName = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(_DBElement.Filename)).Trim
            Dim stackMark As String = Path.GetFileNameWithoutExtension(_DBElement.Filename).Replace(FileName, String.Empty).ToLower
            If Not FileName.ToLower = "video_ts" Then
                If Not stackMark = String.Empty AndAlso _DBElement.TVEpisode.Title.ToLower.EndsWith(stackMark) Then
                    FileName = Path.GetFileNameWithoutExtension(_DBElement.Filename)
                End If
                txtFolder.Text = Directory.GetParent(_DBElement.Filename).Name
                txtFile.Text = FileName
            Else
                txtFile.Text = "$F"
                txtFile.Visible = False
                txtFolder.Text = Directory.GetParent(_DBElement.Filename).Name
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        Cursor.Current = Cursors.WaitCursor
        OK_Button.Enabled = False
        Cancel_Button.Enabled = False
        txtFolder.Enabled = False
        txtFile.Enabled = False
        pnlStatus.Visible = True
        Application.DoEvents()
        bwRename = New System.ComponentModel.BackgroundWorker
        bwRename.RunWorkerAsync()
    End Sub

    Sub SetUp()
        Text = String.Concat(Master.eLang.GetString(263, "Manual Rename"), " | ", _DBElement.TVEpisode.Title)
        Label1.Text = Master.eLang.GetString(13, "Folder Name")
        Label2.Text = Master.eLang.GetString(15, "File Name")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(19, "Close")
        lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Label3.Text = Master.eLang.GetString(272, "Renaming Directory/Files...")
        txtTitle.Text = _DBElement.TVEpisode.Title
    End Sub

    Private Sub txtFile_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFile.TextChanged
        If Not String.IsNullOrEmpty(txtFolder.Text) AndAlso Not String.IsNullOrEmpty(txtFile.Text) Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

    Private Sub txtFolder_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolder.TextChanged
        If Not String.IsNullOrEmpty(txtFolder.Text) AndAlso Not String.IsNullOrEmpty(txtFile.Text) Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class