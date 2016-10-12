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
Imports System.IO

Public Class dlgRenameManual_TVShow

#Region "Fields"

    Friend WithEvents bwRename As New System.ComponentModel.BackgroundWorker

    Private _DBElement As New Database.DBElement(Enums.ContentType.TVShow)

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
        FileFolderRenamer.RenameSingle_TVShow(_DBElement, txtFolder.Text, String.Empty, String.Empty, False, True, True)
    End Sub

    Private Sub bwRename_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRename.RunWorkerCompleted
        Cursor.Current = Cursors.Default
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub dlgRenameManual_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        SetUp()
        Dim tFolder As String = String.Empty
        If Not String.IsNullOrEmpty(_DBElement.ShowPath) Then
            tFolder = Path.GetFileName(_DBElement.ShowPath)
        Else
            tFolder = _DBElement.TVShow.Title
        End If
        txtFolder.Text = tFolder.Trim
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        Cursor.Current = Cursors.WaitCursor
        btnOK.Enabled = False
        btnCancel.Enabled = False
        txtFolder.Enabled = False
        pnlStatus.Visible = True
        Application.DoEvents()
        bwRename = New System.ComponentModel.BackgroundWorker
        bwRename.RunWorkerAsync()
    End Sub

    Sub SetUp()
        Text = String.Concat(Master.eLang.GetString(263, "Manual Rename"), " | ", _DBElement.TVShow.Title)
        lblFolder.Text = Master.eLang.GetString(13, "Folder Name")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnCancel.Text = Master.eLang.GetString(19, "Close")
        lblTitle.Text = Master.eLang.GetString(246, "Title:")
        lblStatus.Text = Master.eLang.GetString(272, "Renaming Directory/Files...")
        txtTitle.Text = _DBElement.TVShow.Title
    End Sub

    Private Sub txtFolder_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolder.TextChanged
        btnOK.Enabled = Not String.IsNullOrEmpty(txtFolder.Text)
    End Sub

#End Region 'Methods

End Class