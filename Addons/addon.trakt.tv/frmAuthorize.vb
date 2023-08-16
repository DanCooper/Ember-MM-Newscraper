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

Public Class frmAuthorize

#Region "Properties"

    Public ReadOnly Property Result As String
        Get
            Return txtPIN.Text.Trim
        End Get
    End Property

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Setup()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal strAutorizeURL As String) As DialogResult
        txtAutorizeURL.Text = strAutorizeURL
        Return ShowDialog()
    End Function

    Private Sub Setup()
        With Addon.Localisation
            btnOK.Text = .CommonWordsList.OK
            btnOpen.Text = .GetString(3, "Open in Browser")
            lblInfo.Text = .GetString(4, "The Trakt addon CAN NOT be used without authorizing it to access your trakt.tv account.")
            lblPIN.Text = .GetString(5, "PIN Code")
        End With
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub AutorizeURL_Click(sender As Object, e As EventArgs) Handles txtAutorizeURL.Click
        txtAutorizeURL.SelectAll()
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Open_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Functions.Launch(txtAutorizeURL.Text.Trim)
        txtPIN.Focus()
    End Sub

    Private Sub PIN_TextChanged(sender As Object, e As EventArgs) Handles txtPIN.TextChanged
        btnOK.Enabled = Not String.IsNullOrEmpty(txtPIN.Text.Trim)
    End Sub

#End Region 'Methods

End Class