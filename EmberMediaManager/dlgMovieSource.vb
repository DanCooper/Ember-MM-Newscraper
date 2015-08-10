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

Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Public Class dlgMovieSource

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private currNameText As String = String.Empty
    Private currPathText As String = String.Empty
    Private prevNameText As String = String.Empty
    Private prevPathText As String = String.Empty
    Private _id As Integer = -1
    Private autoName As Boolean = True
    Private tmppath As String
    Private prevPath As String = String.Empty
    Private prevSource As String = String.Empty
#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal id As Integer) As Windows.Forms.DialogResult
        '//
        ' Overload to pass data
        '\\

        Me._id = id

        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal SearchPath As String) As Windows.Forms.DialogResult
        Me.tmppath = SearchPath
        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal SearchPath As String, ByVal FolderPath As String) As Windows.Forms.DialogResult
        Me.tmppath = SearchPath
        Me.txtSourcePath.Text = FolderPath
        Return MyBase.ShowDialog()
    End Function

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            With Me.fbdBrowse
                If Not String.IsNullOrEmpty(Me.txtSourcePath.Text) Then
                    .SelectedPath = Me.txtSourcePath.Text
                Else
                    .SelectedPath = Me.tmppath
                End If
                prevPath = txtSourcePath.Text
                prevSource = txtSourceName.Text
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath) Then
                        Me.txtSourcePath.Text = .SelectedPath
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CheckConditions()
        Dim isValid As Boolean = False

        Try
            If String.IsNullOrEmpty(Me.txtSourceName.Text) Then
                pbValid.Image = My.Resources.invalid
            Else
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Concat("SELECT ID FROM Sources WHERE Name LIKE """, Me.txtSourceName.Text.Trim, """ AND ID != ", Me._id, ";")
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        If SQLreader.HasRows Then
                            SQLreader.Read()
                            If String.IsNullOrEmpty(SQLreader("ID").ToString) Then
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
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        If Not String.IsNullOrEmpty(Me.txtSourcePath.Text) AndAlso Directory.Exists(Me.txtSourcePath.Text.Trim) AndAlso isValid Then
            Me.OK_Button.Enabled = True
        End If
    End Sub

    Private Sub chkSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSingle.CheckedChanged
        Me.chkUseFolderName.Enabled = Me.chkSingle.Checked

        If Not Me.chkSingle.Checked Then Me.chkUseFolderName.Checked = False
    End Sub

    Private Sub chkUseFolderName_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseFolderName.CheckedChanged
        If chkUseFolderName.Checked Then
            Me.chkGetYear.Text = Master.eLang.GetString(585, "Get year from folder name")
        Else
            Me.chkGetYear.Text = Master.eLang.GetString(584, "Get year from file name")
        End If
    End Sub

    Private Sub dlgMovieSource_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
        Try
            If Me._id >= 0 Then
                Dim s As Structures.MovieSource = Master.MovieSources.FirstOrDefault(Function(y) y.id = Me._id.ToString)
                If Not s.id Is Nothing Then
                    Me.txtSourceName.Text = s.Name
                    Me.txtSourcePath.Text = s.Path
                    Me.chkExclude.Checked = s.Exclude
                    Me.chkScanRecursive.Checked = s.Recursive
                    Me.chkSingle.Checked = s.IsSingle
                    Me.chkUseFolderName.Checked = s.UseFolderName
                    Me.chkGetYear.Checked = s.GetYear
                    Me.autoName = False
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgMovieSource_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Try
            '2014/07/12 Fix for duplicate entries in database when editing path of existing sources http://bugs.embermediamanager.org/thebuggenie/embermediamanager/issues/89
            'Delete all movies from "old" source before updating, else "old" path will still stay in database!
            'only delete entries when path was changed!
            If prevPath <> "" AndAlso prevPath <> currPathText Then
                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                        parSource.Value = prevSource
                        SQLcommand.CommandText = "SELECT idMovie FROM movie WHERE source = (?);"
                        Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLReader.Read
                                Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("idMovie")), True)
                            End While
                        End Using
                        SQLcommand.ExecuteNonQuery()
                    End Using
                    SQLtransaction.Commit()
                End Using
            End If


            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If Me._id >= 0 Then
                        SQLcommand.CommandText = String.Concat("UPDATE sources SET name = (?), path = (?), recursive = (?), foldername = (?), single = (?), lastscan = (?), exclude = (?), getyear = (?) WHERE ID =", Me._id, ";")
                    Else
                        SQLcommand.CommandText = "INSERT OR REPLACE INTO sources (name, path, recursive, foldername, single, LastScan, Exclude, GetYear) VALUES (?,?,?,?,?,?,?,?);"
                    End If
                    Dim parName As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parName", DbType.String, 0, "name")
                    Dim parPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPath", DbType.String, 0, "path")
                    Dim parRecur As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRecur", DbType.Boolean, 0, "recursive")
                    Dim parFolder As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parFolder", DbType.Boolean, 0, "foldername")
                    Dim parSingle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSingle", DbType.Boolean, 0, "single")
                    Dim parLastScan As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLastScan", DbType.String, 0, "LastScan")
                    Dim parExclude As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parExclude", DbType.Boolean, 0, "Exclude")
                    Dim parGetYear As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parGetYear", DbType.Boolean, 0, "GetYear")
                    parName.Value = txtSourceName.Text.Trim
                    parPath.Value = Regex.Replace(txtSourcePath.Text.Trim, "^(\\)+\\\\", "\\")
                    parRecur.Value = chkScanRecursive.Checked
                    parFolder.Value = chkUseFolderName.Checked
                    parSingle.Value = chkSingle.Checked
                    parLastScan.Value = DateTime.Now
                    parExclude.Value = chkExclude.Checked
                    parGetYear.Value = chkGetYear.Checked

                    SQLcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Finally
            Functions.GetListOfSources()
        End Try

        Me.Close()
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(198, "Movie Source")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.lblHint.Text = Master.eLang.GetString(114, "* This MUST be enabled to use extrathumbs and file naming options like movie.tbn, fanart.jpg, etc.")
        Me.lblSourceName.Text = Master.eLang.GetString(199, "Source Name:")
        Me.lblSourcePath.Text = Master.eLang.GetString(200, "Source Path:")
        Me.gbSourceOptions.Text = Master.eLang.GetString(201, "Source Options")
        Me.chkExclude.Text = Master.eLang.GetString(164, "Exclude path from library updates")
        Me.chkGetYear.Text = Master.eLang.GetString(585, "Get year from folder name")
        Me.chkSingle.Text = Master.eLang.GetString(202, "Movies are in separate folders *")
        Me.chkUseFolderName.Text = Master.eLang.GetString(203, "Use Folder Name for Initial Listing")
        Me.chkScanRecursive.Text = Master.eLang.GetString(204, "Scan Recursively")
        Me.fbdBrowse.Description = Master.eLang.GetString(205, "Select the parent folder for your movie folders/files.")
    End Sub

    Private Sub tmrName_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrName.Tick
        Me.tmrWait.Enabled = False
        Me.CheckConditions()
        Me.tmrName.Enabled = False
    End Sub

    Private Sub tmrPathWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPathWait.Tick
        If Me.prevPathText = Me.currPathText Then
            Me.tmrPath.Enabled = True
        Else
            If String.IsNullOrEmpty(txtSourceName.Text) OrElse Me.autoName Then
                Me.txtSourceName.Text = FileUtils.Common.GetDirectory(Me.txtSourcePath.Text)
                Me.autoName = True
            End If
            Me.prevPathText = Me.currPathText
        End If
    End Sub

    Private Sub tmrPath_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPath.Tick
        Me.tmrPathWait.Enabled = False
        Me.CheckConditions()
        Me.tmrPath.Enabled = False
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrWait.Tick
        If Me.prevNameText = Me.currNameText Then
            Me.tmrName.Enabled = True
        Else
            Me.prevNameText = Me.currNameText
        End If
    End Sub

    Private Sub txtSourceName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSourceName.KeyPress
        Me.autoName = False
    End Sub

    Private Sub txtSourceName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSourceName.TextChanged
        Me.OK_Button.Enabled = False
        Me.currNameText = Me.txtSourceName.Text

        Me.tmrWait.Enabled = False
        Me.tmrWait.Enabled = True
    End Sub

    Private Sub txtSourcePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSourcePath.TextChanged
        Me.OK_Button.Enabled = False
        Me.currPathText = Me.txtSourcePath.Text
        Me.tmrPathWait.Enabled = False
        Me.tmrPathWait.Enabled = True
    End Sub

#End Region 'Methods

End Class