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

Public Class dlgStudioSelect

#Region "Fields"

    Private _CurrMovie As Database.DBElement = Nothing
    Private _studio As String = String.Empty

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal CurrMovie As Database.DBElement) As String
        '//
        ' Overload to pass data
        '\\

        _CurrMovie = CurrMovie

        If ShowDialog() = DialogResult.OK Then
            Return _studio
        Else
            Return String.Empty
        End If
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub dlgStudioSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUp()
        'Dim DBMovie As New Database.DBElement
        'DBMovie.Movie = New MediaContainers.Movie
        'DBMovie.Movie.IMDBID = Me._MovieId
        Dim alStudio As List(Of String) = ModulesManager.Instance.GetMovieStudio(_CurrMovie)
        ' If alStudio.Count = 0 Then alStudio.Add(_CurrMovie.Movie.Studio)
        If alStudio.Count = 0 Then alStudio.AddRange(_CurrMovie.Movie.Studios)
        For i As Integer = 0 To alStudio.Count - 1
            ilStudios.Images.Add(alStudio(i).ToString, APIXML.GetStudioImage(alStudio(i).ToString))
            lvStudios.Items.Add(alStudio(i).ToString, i)
        Next
    End Sub

    Private Sub dlgStudioSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub lvStudios_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvStudios.SelectedIndexChanged
        If lvStudios.SelectedItems.Count > 0 Then
            _studio = lvStudios.SelectedItems(0).Text
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(223, "Select Studio")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

#End Region 'Methods

End Class