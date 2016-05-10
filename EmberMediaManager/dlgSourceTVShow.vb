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

Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class dlgSourceTVShow

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private currNameText As String = String.Empty
    Private currPathText As String = String.Empty
    Private prevNameText As String = String.Empty
    Private prevPathText As String = String.Empty
    Private _id As Integer = -1
    Private autoName As Boolean = True
    Private tmppath As String

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal id As Integer) As Windows.Forms.DialogResult
        _id = id
        btnBrowse.Enabled = False
        txtSourcePath.Enabled = False

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String) As Windows.Forms.DialogResult
        tmppath = strSearchPath

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String, ByVal strFolderPath As String) As Windows.Forms.DialogResult
        tmppath = strSearchPath
        txtSourcePath.Text = strFolderPath

        Return ShowDialog()
    End Function

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With fbdBrowse
            If Not String.IsNullOrEmpty(txtSourcePath.Text) Then
                .SelectedPath = txtSourcePath.Text
            Else
                .SelectedPath = tmppath
            End If
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath) Then
                    txtSourcePath.Text = .SelectedPath
                End If
            End If
        End With
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub cbSourceLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceLanguage.SelectedIndexChanged
        OK_Button.Enabled = False
        tmrWait.Enabled = False
        tmrWait.Enabled = True
    End Sub

    Private Sub cbSourceOrdering_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceOrdering.SelectedIndexChanged
        OK_Button.Enabled = False
        tmrWait.Enabled = False
        tmrWait.Enabled = True
    End Sub

    Private Sub cbSourceEpisodeSorting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceEpisodeSorting.SelectedIndexChanged
        OK_Button.Enabled = False
        tmrWait.Enabled = False
        tmrWait.Enabled = True
    End Sub

    Private Sub CheckConditions()
        Dim isValid As Boolean = False

        If String.IsNullOrEmpty(txtSourceName.Text) Then
            pbValid.Image = My.Resources.invalid
        Else
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idSource FROM tvshowsource WHERE strName LIKE """, txtSourceName.Text.Trim, """ AND idSource != ", _id, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        SQLreader.Read()
                        If String.IsNullOrEmpty(SQLreader("idSource").ToString) Then
                            pbValid.Image = My.Resources.invalid
                        Else
                            pbValid.Image = My.Resources.valid
                            isValid = True
                        End If
                    Else
                        pbValid.Image = My.Resources.valid
                        isValid = True
                    End If
                End Using
            End Using
        End If

        If Not String.IsNullOrEmpty(txtSourcePath.Text) AndAlso Directory.Exists(txtSourcePath.Text.Trim) AndAlso
            Not String.IsNullOrEmpty(cbSourceLanguage.Text) AndAlso Not String.IsNullOrEmpty(cbSourceOrdering.Text) AndAlso
            Not String.IsNullOrEmpty(cbSourceEpisodeSorting.Text) AndAlso isValid Then
            OK_Button.Enabled = True
        End If
    End Sub

    Private Sub dlgMovieSource_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()

        If Not _id = -1 Then
            Dim s As Database.DBSource = Master.TVShowSources.FirstOrDefault(Function(y) y.ID = _id)
            If s IsNot Nothing Then
                autoName = False
                If cbSourceLanguage.Items.Count > 0 Then
                    Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = s.Language)
                    If tLanguage IsNot Nothing Then
                        cbSourceLanguage.Text = tLanguage.Description
                    Else
                        tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(s.Language))
                        If tLanguage IsNot Nothing Then
                            cbSourceLanguage.Text = tLanguage.Description
                        Else
                            cbSourceLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                        End If
                    End If
                End If
                cbSourceOrdering.SelectedIndex = s.Ordering
                cbSourceEpisodeSorting.SelectedIndex = s.EpisodeSorting
                chkExclude.Checked = s.Exclude
                txtSourceName.Text = s.Name
                txtSourcePath.Text = s.Path
            End If
        Else
            If cbSourceLanguage.Items.Count > 0 Then
                cbSourceLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = Master.eSettings.TVGeneralLanguage).Description
            End If
            cbSourceEpisodeSorting.SelectedIndex = Enums.EpisodeSorting.Episode
            cbSourceOrdering.SelectedIndex = Enums.Ordering.Standard
        End If
    End Sub

    Private Sub dlgTVSource_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
        txtSourcePath.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                If Not _id = -1 Then
                    SQLcommand.CommandText = String.Concat("UPDATE tvshowsource SET strName = (?), strPath = (?), strLanguage = (?), iOrdering = (?), bExclude = (?), iEpisodeSorting = (?) WHERE idSource =", _id, ";")
                Else
                    SQLcommand.CommandText = "INSERT OR REPLACE INTO tvshowsource (strName, strPath, strLanguage, iOrdering, bExclude, iEpisodeSorting) VALUES (?,?,?,?,?,?);"
                End If
                Dim parName As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parName", DbType.String, 0, "strName")
                Dim parPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPath", DbType.String, 0, "strPath")
                Dim parLanguage As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "strLanguage")
                Dim parOrdering As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOrdering", DbType.Int16, 0, "iOrdering")
                Dim parExclude As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parExclude", DbType.Boolean, 0, "bExclude")
                Dim parEpisodeSorting As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeSorting", DbType.Int16, 0, "iEpisodeSorting")
                parName.Value = txtSourceName.Text.Trim
                parPath.Value = Regex.Replace(txtSourcePath.Text.Trim, "^(\\)+\\\\", "\\")
                parExclude.Value = chkExclude.Checked
                If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
                    parLanguage.Value = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                Else
                    parLanguage.Value = "en-US"
                End If
                If Not String.IsNullOrEmpty(cbSourceOrdering.Text) Then
                    parOrdering.Value = DirectCast(cbSourceOrdering.SelectedIndex, Enums.Ordering)
                Else
                    parOrdering.Value = Enums.Ordering.Standard
                End If
                If Not String.IsNullOrEmpty(cbSourceEpisodeSorting.Text) Then
                    parEpisodeSorting.Value = DirectCast(cbSourceEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)
                Else
                    parEpisodeSorting.Value = Enums.EpisodeSorting.Episode
                End If

                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
        End Using

        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(705, "TV Source")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        chkExclude.Text = Master.eLang.GetString(164, "Exclude path from library updates")
        gbSourceOptions.Text = Master.eLang.GetString(201, "Source Options")
        lblSourceEpisodeSorting.Text = String.Concat(Master.eLang.GetString(364, "Show Episodes by"), ":")
        lblSourceLanguage.Text = String.Concat(Master.eLang.GetString(1166, "Default Language"), ":")
        lblSourceName.Text = Master.eLang.GetString(199, "Source Name:")
        lblSourceOrdering.Text = String.Concat(Master.eLang.GetString(797, "Default Episode Ordering"), ":")
        lblSourcePath.Text = Master.eLang.GetString(200, "Source Path:")
        fbdBrowse.Description = Master.eLang.GetString(706, "Select the parent folder for your TV Series folders/files.")

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)

        cbSourceOrdering.Items.Clear()
        cbSourceOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(1067, "DVD"), Master.eLang.GetString(839, "Absolute"), Master.eLang.GetString(1332, "Day Of Year")})

        cbSourceEpisodeSorting.Items.Clear()
        cbSourceEpisodeSorting.Items.AddRange(New String() {Master.eLang.GetString(755, "Episode #"), Master.eLang.GetString(728, "Aired")})
    End Sub

    Private Sub tmrName_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrName.Tick
        tmrWait.Enabled = False
        CheckConditions()
        tmrName.Enabled = False
    End Sub

    Private Sub tmrPathWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPathWait.Tick
        If prevPathText = currPathText Then
            tmrPath.Enabled = True
        Else
            If String.IsNullOrEmpty(txtSourceName.Text) OrElse autoName Then
                txtSourceName.Text = FileUtils.Common.GetDirectory(txtSourcePath.Text)
                autoName = True
            End If
            prevPathText = currPathText
        End If
    End Sub

    Private Sub tmrPath_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPath.Tick
        tmrPathWait.Enabled = False
        CheckConditions()
        tmrPath.Enabled = False
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrWait.Tick
        If prevNameText = currNameText Then
            tmrName.Enabled = True
        Else
            prevNameText = currNameText
        End If
    End Sub

    Private Sub txtSourceName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSourceName.KeyPress
        autoName = False
    End Sub

    Private Sub txtSourceName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSourceName.TextChanged
        OK_Button.Enabled = False
        currNameText = txtSourceName.Text

        tmrWait.Enabled = False
        tmrWait.Enabled = True
    End Sub

    Private Sub txtSourcePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSourcePath.TextChanged
        OK_Button.Enabled = False
        currPathText = txtSourcePath.Text
        tmrPathWait.Enabled = False
        tmrPathWait.Enabled = True
    End Sub

#End Region 'Methods

End Class