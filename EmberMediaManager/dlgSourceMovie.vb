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

Public Class dlgSourceMovie

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private bAutoName As Boolean = True
    Private strCurrName As String = String.Empty
    Private strCurrPath As String = String.Empty
    Private strPrevName As String = String.Empty
    Private strPrevPath As String = String.Empty
    Private strTempPath As String
    Private _id As Long = -1

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Public Overloads Function ShowDialog(ByVal id As Integer) As DialogResult
        _id = id
        btnBrowse.Enabled = False
        chkScanRecursive.Enabled = False
        chkSingle.Enabled = False
        txtSourcePath.Enabled = False

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String) As DialogResult
        strTempPath = strSearchPath

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String, ByVal strFolderPath As String) As DialogResult
        strTempPath = strSearchPath
        txtSourcePath.Text = strFolderPath

        Return ShowDialog()
    End Function

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowse.Click
        With fbdBrowse
            If Not String.IsNullOrEmpty(txtSourcePath.Text) Then
                .SelectedPath = txtSourcePath.Text
            Else
                .SelectedPath = strTempPath
            End If
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath) Then
                    txtSourcePath.Text = .SelectedPath
                End If
            End If
        End With
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub cbSourceLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceLanguage.SelectedIndexChanged
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
                SQLcommand.CommandText = String.Concat("SELECT idSource FROM moviesource WHERE name LIKE """, txtSourceName.Text.Trim, """ AND idSource != ", _id, ";")
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
            For Each tSource In Master.DB.Load_AllSources_Movie.Where(Function(f) Not f.ID = _id)
                'check if the path contains another source or is inside another source

                Dim strOtherSource As String = tSource.Path.ToLower
                Dim strCurrentSource As String = txtSourcePath.Text.Trim.ToLower
                'add a directory separator at the end of the path to distinguish between
                'D:\Movies
                'D:\Movies Shared
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
            Not String.IsNullOrEmpty(cbSourceLanguage.Text) AndAlso bIsValid_SourceName AndAlso bIsValid_SourcePath Then
            OK_Button.Enabled = True
        End If
    End Sub

    Private Sub chkSingle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSingle.CheckedChanged
        chkUseFolderName.Enabled = chkSingle.Checked

        If Not chkSingle.Checked Then chkUseFolderName.Checked = False
    End Sub

    Private Sub chkUseFolderName_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseFolderName.CheckedChanged
        If chkUseFolderName.Checked Then
            chkGetYear.Text = Master.eLang.GetString(585, "Get year from folder name")
        Else
            chkGetYear.Text = Master.eLang.GetString(584, "Get year from file name")
        End If
    End Sub

    Private Sub dlgSourceMovie_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()

        If Not _id = -1 Then
            Dim s As Database.DBSource = Master.DB.Load_AllSources_Movie.FirstOrDefault(Function(y) y.ID = _id)
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
                chkExclude.Checked = s.Exclude
                chkGetYear.Checked = s.GetYear
                chkScanRecursive.Checked = s.ScanRecursive
                chkSingle.Checked = s.IsSingle
                chkUseFolderName.Checked = s.UseFolderName
                txtSourceName.Text = s.Name
                txtSourcePath.Text = s.Path
            End If
        Else
            If cbSourceLanguage.Items.Count > 0 Then
                Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = Master.eSettings.Movie.SourceSettings.DefaultLanguage)
                If tLanguage IsNot Nothing Then
                    cbSourceLanguage.Text = tLanguage.Description
                Else
                    tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(Master.eSettings.Movie.SourceSettings.DefaultLanguage))
                    If tLanguage IsNot Nothing Then
                        cbSourceLanguage.Text = tLanguage.Description
                    Else
                        cbSourceLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dlgSourceMovie_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
        txtSourcePath.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        Dim strSourcePath As String = Regex.Replace(txtSourcePath.Text.Trim, "^(\\)+\\\\", "\\")
        Dim strLanguage As String = "en-US"
        If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
            strLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
        End If

        Dim nSource As New Database.DBSource With {
            .Exclude = chkExclude.Checked,
            .GetYear = chkGetYear.Checked,
            .ID = _id,
            .IsSingle = chkSingle.Checked,
            .Language = strLanguage,
            .Name = txtSourceName.Text.Trim,
            .Path = strSourcePath,
            .ScanRecursive = chkScanRecursive.Checked,
            .UseFolderName = chkUseFolderName.Checked}

        Master.DB.Save_Source_Movie(nSource)

        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(198, "Movie Source")
        OK_Button.Text = Master.eLang.OK
        Cancel_Button.Text = Master.eLang.Cancel
        lblHint.Text = Master.eLang.GetString(114, "* This MUST be enabled to use extrathumbs and file naming options like movie.tbn, fanart.jpg, etc.")
        lblSourceName.Text = Master.eLang.GetString(199, "Source Name:")
        lblSourcePath.Text = Master.eLang.GetString(200, "Source Path:")
        gbSourceOptions.Text = Master.eLang.GetString(201, "Source Options")
        lblSourceLanguage.Text = String.Concat(Master.eLang.GetString(1166, "Default Language"), ":")
        chkExclude.Text = Master.eLang.GetString(164, "Exclude path from library updates")
        chkGetYear.Text = Master.eLang.GetString(585, "Get year from folder name")
        chkSingle.Text = Master.eLang.GetString(202, "Movies are in separate folders *")
        chkUseFolderName.Text = Master.eLang.GetString(203, "Use Folder Name for Initial Listing")
        chkScanRecursive.Text = Master.eLang.GetString(204, "Scan Recursively")
        fbdBrowse.Description = Master.eLang.GetString(205, "Select the parent folder for your movie folders/files.")

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
    End Sub

    Private Sub tmrName_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrName.Tick
        tmrWait.Enabled = False
        CheckConditions()
        tmrName.Enabled = False
    End Sub

    Private Sub tmrPathWait_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrPathWait.Tick
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

    Private Sub tmrPath_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrPath.Tick
        tmrPathWait.Enabled = False
        CheckConditions()
        tmrPath.Enabled = False
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait.Tick
        If strPrevName = strCurrName Then
            tmrName.Enabled = True
        Else
            strPrevName = strCurrName
        End If
    End Sub

    Private Sub txtSourceName_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSourceName.KeyPress
        bAutoName = False
    End Sub

    Private Sub txtSourceName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSourceName.TextChanged
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

    Private Sub txtSourcePath_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSourcePath.TextChanged
        OK_Button.Enabled = False
        strCurrPath = txtSourcePath.Text
        tmrPathWait.Enabled = False
        tmrPathWait.Enabled = True
    End Sub

#End Region 'Methods

End Class