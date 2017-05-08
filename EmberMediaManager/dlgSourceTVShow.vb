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
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class dlgSourceTVShow

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private bAutoName As Boolean = True
    Private strCurrName As String = String.Empty
    Private strCurrPath As String = String.Empty
    Private strPrevName As String = String.Empty
    Private strPrevPath As String = String.Empty
    Private strTmpPath As String
    Private _id As Integer = -1

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal id As Integer) As DialogResult
        _id = id
        btnBrowse.Enabled = False
        chkSingle.Enabled = False
        txtSourcePath.Enabled = False

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String) As DialogResult
        strTmpPath = strSearchPath

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String, ByVal strFolderPath As String) As DialogResult
        strTmpPath = strSearchPath
        txtSourcePath.Text = strFolderPath

        Return ShowDialog()
    End Function

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With fbdBrowse
            If Not String.IsNullOrEmpty(txtSourcePath.Text) Then
                .SelectedPath = txtSourcePath.Text
            Else
                .SelectedPath = strTmpPath
            End If
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath) Then
                    txtSourcePath.Text = .SelectedPath
                End If
            End If
        End With
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
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
        Dim bIsValid_SourceName As Boolean = False
        Dim bIsValid_SourcePath As Boolean = True

        If String.IsNullOrEmpty(txtSourceName.Text) Then
            pbValidSourceName.Image = My.Resources.invalid
        Else
            'check duplicate source names
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idSource FROM tvshowsource WHERE strName LIKE """, txtSourceName.Text.Trim, """ AND idSource != ", _id, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        pbValidSourceName.Image = My.Resources.invalid
                        bIsValid_SourceName = False
                    Else
                        pbValidSourceName.Image = My.Resources.valid
                        bIsValid_SourceName = True
                    End If
                End Using
            End Using
        End If

        If String.IsNullOrEmpty(txtSourcePath.Text) OrElse Not Directory.Exists(txtSourcePath.Text.Trim) Then
            bIsValid_SourcePath = False
            pbValidSourcePath.Image = My.Resources.invalid
        Else
            For Each tSource In Master.TVShowSources.Where(Function(f) Not f.ID = _id)
                'check if the path contains another source or is inside another source

                Dim strOtherSource As String = tSource.Path.ToLower
                Dim strCurrentSource As String = txtSourcePath.Text.Trim.ToLower
                'add a directory separator at the end of the path to distinguish between
                'D:\TV Shows
                'D:\TV Shows Shared
                '(needed for "LocalPath.ToLower.StartsWith(tLocalSource)"
                If strOtherSource.Contains(Path.DirectorySeparatorChar) Then
                    strOtherSource = If(strOtherSource.EndsWith(Path.DirectorySeparatorChar), strOtherSource, String.Concat(strOtherSource, Path.DirectorySeparatorChar)).Trim
                ElseIf strOtherSource.Contains(Path.AltDirectorySeparatorChar) Then
                    strOtherSource = If(strOtherSource.EndsWith(Path.AltDirectorySeparatorChar), strOtherSource, String.Concat(strOtherSource, Path.AltDirectorySeparatorChar)).Trim
                End If
                If strCurrentSource.Contains(Path.DirectorySeparatorChar) Then
                    strCurrentSource = If(strCurrentSource.EndsWith(Path.DirectorySeparatorChar), strCurrentSource, String.Concat(strCurrentSource, Path.DirectorySeparatorChar)).Trim
                ElseIf strCurrentSource.Contains(Path.AltDirectorySeparatorChar) Then
                    strCurrentSource = If(strCurrentSource.EndsWith(Path.AltDirectorySeparatorChar), strCurrentSource, String.Concat(strCurrentSource, Path.AltDirectorySeparatorChar)).Trim
                End If

                If strOtherSource.StartsWith(strCurrentSource) OrElse
                    strCurrentSource.Contains(strOtherSource) Then
                    bIsValid_SourcePath = False
                    pbValidSourcePath.Image = My.Resources.invalid
                    Exit For
                End If
            Next
        End If

        If bIsValid_SourcePath Then pbValidSourcePath.Image = My.Resources.valid

        If Not String.IsNullOrEmpty(txtSourcePath.Text) AndAlso Directory.Exists(txtSourcePath.Text.Trim) AndAlso
            Not String.IsNullOrEmpty(cbSourceLanguage.Text) AndAlso Not String.IsNullOrEmpty(cbSourceOrdering.Text) AndAlso
            Not String.IsNullOrEmpty(cbSourceEpisodeSorting.Text) AndAlso bIsValid_SourceName AndAlso bIsValid_SourcePath Then
            OK_Button.Enabled = True
        End If
    End Sub

    Private Sub dlgSourceTVShow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()

        If Not _id = -1 Then
            Dim s As Database.DBSource = Master.TVShowSources.FirstOrDefault(Function(y) y.ID = _id)
            If s IsNot Nothing Then
                bAutoName = False
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
                chkSingle.Checked = s.IsSingle
                txtSourceName.Text = s.Name
                txtSourcePath.Text = s.Path
            End If
        Else
            If cbSourceLanguage.Items.Count > 0 Then
                Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = Master.eSettings.TVGeneralLanguage)
                If tLanguage IsNot Nothing Then
                    cbSourceLanguage.Text = tLanguage.Description
                Else
                    tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(Master.eSettings.TVGeneralLanguage))
                    If tLanguage IsNot Nothing Then
                        cbSourceLanguage.Text = tLanguage.Description
                    Else
                        cbSourceLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            End If
            cbSourceEpisodeSorting.SelectedIndex = Enums.EpisodeSorting.Episode
            cbSourceOrdering.SelectedIndex = Enums.EpisodeOrdering.Standard
        End If
    End Sub

    Private Sub dlgSourceTVShow_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
        txtSourcePath.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim strSourcePath As String = Regex.Replace(txtSourcePath.Text.Trim, "^(\\)+\\\\", "\\")

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                If Not _id = -1 Then
                    SQLcommand.CommandText = String.Concat("UPDATE tvshowsource SET strName = (?), strPath = (?), strLanguage = (?), iOrdering = (?), bExclude = (?), iEpisodeSorting = (?) , bSingle = (?) WHERE idSource =", _id, ";")
                Else
                    SQLcommand.CommandText = "INSERT OR REPLACE INTO tvshowsource (strName, strPath, strLanguage, iOrdering, bExclude, iEpisodeSorting, bSingle) VALUES (?,?,?,?,?,?,?);"
                End If
                Dim parName As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parName", DbType.String, 0, "strName")
                Dim parPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPath", DbType.String, 0, "strPath")
                Dim parLanguage As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "strLanguage")
                Dim parOrdering As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOrdering", DbType.Int16, 0, "iOrdering")
                Dim parExclude As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parExclude", DbType.Boolean, 0, "bExclude")
                Dim parEpisodeSorting As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeSorting", DbType.Int16, 0, "iEpisodeSorting")
                Dim parSingle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSingle", DbType.Boolean, 0, "bSingle")
                parName.Value = txtSourceName.Text.Trim
                parPath.Value = strSourcePath
                parExclude.Value = chkExclude.Checked
                parSingle.Value = chkSingle.Checked
                If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
                    parLanguage.Value = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
                Else
                    parLanguage.Value = "en-US"
                End If
                If Not String.IsNullOrEmpty(cbSourceOrdering.Text) Then
                    parOrdering.Value = DirectCast(cbSourceOrdering.SelectedIndex, Enums.EpisodeOrdering)
                Else
                    parOrdering.Value = Enums.EpisodeOrdering.Standard
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

        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(705, "TV Source")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        chkExclude.Text = Master.eLang.GetString(164, "Exclude path from library updates")
        chkSingle.Text = Master.eLang.GetString(1048, "Selected folder contains a single TV Show")
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
        If strPrevPath = strCurrPath Then
            tmrPath.Enabled = True
        Else
            If String.IsNullOrEmpty(txtSourceName.Text) OrElse bAutoName Then
                Try
                    If Not String.IsNullOrEmpty(txtSourcePath.Text) Then
                        Dim dInfo As DirectoryInfo = New DirectoryInfo(txtSourcePath.Text)
                        If dInfo IsNot Nothing AndAlso Not String.IsNullOrEmpty(dInfo.Name) Then
                            txtSourceName.Text = dInfo.Name
                            bAutoName = True
                        End If
                    End If
                Catch ex As Exception
                    txtSourceName.Text = String.Empty
                    bAutoName = True
                End Try
            End If
            strPrevPath = strCurrPath
        End If
    End Sub

    Private Sub tmrPath_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPath.Tick
        tmrPathWait.Enabled = False
        CheckConditions()
        tmrPath.Enabled = False
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrWait.Tick
        If strPrevName = strCurrName Then
            tmrName.Enabled = True
        Else
            strPrevName = strCurrName
        End If
    End Sub

    Private Sub txtSourceName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSourceName.KeyPress
        bAutoName = False
    End Sub

    Private Sub txtSourceName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSourceName.TextChanged
        OK_Button.Enabled = False
        strCurrName = txtSourceName.Text

        tmrWait.Enabled = False
        tmrWait.Enabled = True
    End Sub

    Private Sub txtSourcePath_Leave(sender As Object, e As EventArgs) Handles txtSourcePath.Leave
        Try
            Dim dInfo As DirectoryInfo = New DirectoryInfo(txtSourcePath.Text)
            If Not txtSourcePath.Text = dInfo.FullName Then
                txtSourcePath.Text = dInfo.FullName
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSourcePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSourcePath.TextChanged
        OK_Button.Enabled = False
        strCurrPath = txtSourcePath.Text
        tmrPathWait.Enabled = False
        tmrPathWait.Enabled = True
    End Sub

#End Region 'Methods

End Class