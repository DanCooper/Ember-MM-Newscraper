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

Public Class dlgRenameManual_TVShow

#Region "Fields"

    Friend WithEvents bwRename As New System.ComponentModel.BackgroundWorker

    Private _DBElement As New Database.DBElement

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal DBElement As Database.DBElement)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
        _DBElement = DBElement
    End Sub

    Private Sub bwRename_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRename.DoWork
        FileFolderRenamer.RenameSingle_Show(_DBElement, txtFolder.Text, False, True, True)
    End Sub

    Private Sub bwRename_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRename.RunWorkerCompleted
        Cursor.Current = Cursors.Default
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgRenameManual_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetUp()
        Dim tFolder As String = String.Empty
        If Not String.IsNullOrEmpty(_DBElement.ShowPath) Then
            tFolder = Path.GetFileName(_DBElement.ShowPath)
        Else
            tFolder = _DBElement.TVShow.Title
        End If
        txtFolder.Text = tFolder.Trim
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Cursor.Current = Cursors.WaitCursor
        OK_Button.Enabled = False
        Cancel_Button.Enabled = False
        txtFolder.Enabled = False
        pnlStatus.Visible = True
        Application.DoEvents()
        Me.bwRename = New System.ComponentModel.BackgroundWorker
        Me.bwRename.RunWorkerAsync()
    End Sub

    Sub SetUp()
        Me.Text = String.Concat(Master.eLang.GetString(263, "Manual Rename"), " | ", _DBElement.TVShow.Title)
        Me.lblFolder.Text = Master.eLang.GetString(13, "Folder Name")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(19, "Close")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.lblStatus.Text = Master.eLang.GetString(272, "Renaming Directory/Files...")
        Me.txtTitle.Text = _DBElement.TVShow.Title
    End Sub

    Private Sub txtFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolder.TextChanged
        If Not String.IsNullOrEmpty(txtFolder.Text) Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class