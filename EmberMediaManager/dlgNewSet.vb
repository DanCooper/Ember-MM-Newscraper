Imports EmberAPI

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

Public Class dlgNewSet

#Region "Fields"

    Private tmpDBElement As New Database.DBElement

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As Database.DBElement
        Get
            Return tmpDBElement
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBMovieSet As Database.DBElement) As DialogResult
        Me.tmpDBElement = DBMovieSet
        Return MyBase.ShowDialog()
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgNewSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetUp()

        If Me.cbLanguage.Items.Count > 0 Then
            Me.cbLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.MovieGeneralLanguage).name
        End If
    End Sub

    Private Sub dlgNewSet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
        Me.txtTitle.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        tmpDBElement.MovieSet.Title = txtTitle.Text.Trim
        tmpDBElement.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbLanguage.Text).abbreviation
        tmpDBElement.ListTitle = StringUtils.SortTokens_MovieSet(txtTitle.Text.Trim)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        CheckConditions()
    End Sub

    Private Sub cbMovieGeneralLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLanguage.SelectedIndexChanged
        CheckConditions()
    End Sub

    Private Sub CheckConditions()
        If Not String.IsNullOrEmpty(txtTitle.Text) AndAlso Not String.IsNullOrEmpty(cbLanguage.Text) Then
            'check if the MovieSet Name is already existing
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idSet FROM sets WHERE SetName LIKE """, Me.txtTitle.Text.Trim, """;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        btnOK.Enabled = False
                        txtTitle.ForeColor = Color.Red
                    Else
                        btnOK.Enabled = True
                        txtTitle.ForeColor = Color.Black
                    End If
                End Using
            End Using
        Else
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.lblLanguage.Text = String.Concat(Master.eLang.GetString(610, "Language"), ":")
        Me.lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")

        Me.cbLanguage.Items.Clear()
        Me.cbLanguage.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)
    End Sub

#End Region 'Methods

End Class