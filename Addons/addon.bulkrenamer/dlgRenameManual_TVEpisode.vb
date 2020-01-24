﻿' ################################################################################
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
        Renamer.RenameSingle_TVEpisode(_DBElement, txtFolder.Text, txtFile.Text, False, True, True)
    End Sub

    Private Sub bwRename_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRename.RunWorkerCompleted
        Cursor.Current = Cursors.Default
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub dlgRenameManual_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Setup()
        If _DBElement.FileItem.bIsBDMV OrElse _DBElement.FileItem.bIsVideoTS Then
            txtFile.Text = "$F"
            txtFile.Visible = False
            txtFolder.Text = _DBElement.FileItem.MainPath.Name
        Else
            'TODO: fix stackMark part
            Dim FileName = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(_DBElement.FileItem.FirstPathFromStack)).Trim
            Dim stackMark As String = Path.GetFileNameWithoutExtension(_DBElement.FileItem.FirstPathFromStack).Replace(FileName, String.Empty).ToLower
            If Not FileName.ToLower = "video_ts" Then
                If Not stackMark = String.Empty AndAlso _DBElement.MainDetails.Title.ToLower.EndsWith(stackMark) Then
                    FileName = Path.GetFileNameWithoutExtension(_DBElement.FileItem.FirstPathFromStack)
                End If
                txtFolder.Text = _DBElement.FileItem.MainPath.Name
                txtFile.Text = FileName
            Else
                txtFile.Text = "$F"
                txtFile.Visible = False
                txtFolder.Text = _DBElement.FileItem.MainPath.Name
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        Cursor.Current = Cursors.WaitCursor
        btnOK.Enabled = False
        btnCancel.Enabled = False
        txtFolder.Enabled = False
        txtFile.Enabled = False
        pnlStatus.Visible = True
        Application.DoEvents()
        bwRename = New System.ComponentModel.BackgroundWorker
        bwRename.RunWorkerAsync()
    End Sub

    Sub Setup()
        Text = String.Concat(Master.eLang.GetString(263, "Manual Rename"), " | ", _DBElement.MainDetails.Title)
        Label1.Text = Master.eLang.GetString(13, "Folder Name")
        Label2.Text = Master.eLang.GetString(15, "File Name")
        btnOK.Text = Master.eLang.OK
        btnCancel.Text = Master.eLang.Close
        lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")
        Label3.Text = Master.eLang.GetString(272, "Renaming Directory/Files...")
        txtTitle.Text = _DBElement.MainDetails.Title
    End Sub

    Private Sub txtFile_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFile.TextChanged
        btnOK.Enabled = ValidateFolderFile()
    End Sub

    Private Sub txtFolder_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFolder.TextChanged
        btnOK.Enabled = ValidateFolderFile()
    End Sub

    Private Function ValidateFolderFile() As Boolean
        Return Not String.IsNullOrEmpty(txtFile.Text) AndAlso Not String.IsNullOrEmpty(txtFolder.Text)
    End Function

#End Region 'Methods

End Class