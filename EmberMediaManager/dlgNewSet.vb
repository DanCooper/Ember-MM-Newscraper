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

Public Class dlgNewSet

#Region "Fields"

    Private tmpDBElement As Database.DBElement

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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBMovieSet As Database.DBElement) As DialogResult
        tmpDBElement = DBMovieSet
        Return ShowDialog()
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub dlgNewSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUp()

        If cbLanguage.Items.Count > 0 Then
            Dim tLang As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = Master.eSettings.MovieGeneralLanguage)
            If tLang IsNot Nothing Then
                cbLanguage.Text = tLang.Description
            Else
                tLang = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(Master.eSettings.MovieGeneralLanguage))
                If tLang IsNot Nothing Then
                    cbLanguage.Text = tLang.Description
                End If
            End If
        End If
    End Sub

    Private Sub dlgNewSet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
        txtTitle.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        tmpDBElement.MovieSet.Title = txtTitle.Text.Trim
        tmpDBElement.Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbLanguage.Text).Abbreviation
        tmpDBElement.ListTitle = StringUtils.SortTokens_MovieSet(txtTitle.Text.Trim)

        DialogResult = DialogResult.OK
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
                SQLcommand.CommandText = String.Concat("SELECT idSet FROM sets WHERE SetName LIKE """, txtTitle.Text.Trim, """;")
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
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        lblLanguage.Text = String.Concat(Master.eLang.GetString(610, "Language"), ":")
        lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")

        cbLanguage.Items.Clear()
        cbLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
    End Sub

#End Region 'Methods

End Class