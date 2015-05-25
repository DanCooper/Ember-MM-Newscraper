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

Public Class dlgTVSource

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
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal id As Integer) As Windows.Forms.DialogResult
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

    Private Sub cbSourceLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceLanguage.SelectedIndexChanged
        Me.OK_Button.Enabled = False
        Me.tmrWait.Enabled = False
        Me.tmrWait.Enabled = True
    End Sub

    Private Sub cbSourceOrdering_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceOrdering.SelectedIndexChanged
        Me.OK_Button.Enabled = False
        Me.tmrWait.Enabled = False
        Me.tmrWait.Enabled = True
    End Sub

    Private Sub cbSourceEpisodeSorting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSourceEpisodeSorting.SelectedIndexChanged
        Me.OK_Button.Enabled = False
        Me.tmrWait.Enabled = False
        Me.tmrWait.Enabled = True
    End Sub

    Private Sub CheckConditions()
        Dim isValid As Boolean = False

        Try
            If String.IsNullOrEmpty(Me.txtSourceName.Text) Then
                pbValid.Image = My.Resources.invalid
            Else
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Concat("SELECT ID FROM TVSources WHERE Name LIKE """, Me.txtSourceName.Text.Trim, """ AND ID != ", Me._id, ";")
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

        If Not String.IsNullOrEmpty(Me.txtSourcePath.Text) AndAlso Directory.Exists(Me.txtSourcePath.Text.Trim) AndAlso _
            Me.cbSourceLanguage.Text <> String.Empty AndAlso Me.cbSourceOrdering.Text <> String.Empty AndAlso _
            Me.cbSourceEpisodeSorting.Text <> String.Empty AndAlso isValid Then
            Me.OK_Button.Enabled = True
        End If

    End Sub

    Private Sub dlgMovieSource_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
        Try
            If Me._id >= 0 Then
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Concat("SELECT ID, Name, Path, LastScan, Language, Ordering, Exclude, EpisodeSorting FROM TVSources WHERE ID = ", Me._id, ";")
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        If SQLreader.HasRows() Then
                            SQLreader.Read()
                            Me.txtSourceName.Text = SQLreader("Name").ToString
                            Me.txtSourcePath.Text = SQLreader("Path").ToString
                            If Me.cbSourceLanguage.Items.Count > 0 Then
                                Me.cbSourceLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = SQLreader("Language").ToString).name
                            End If
                            Me.cbSourceOrdering.SelectedIndex = DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.Ordering)
                            Me.cbSourceEpisodeSorting.SelectedIndex = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                            Me.chkExclude.Checked = Convert.ToBoolean(SQLreader("Exclude"))
                            Me.autoName = False
                        End If
                    End Using
                End Using
            Else
                If Me.cbSourceLanguage.Items.Count > 0 Then
                    Me.cbSourceLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVGeneralLanguage).name
                End If
                Me.cbSourceEpisodeSorting.SelectedIndex = Enums.EpisodeSorting.Episode
                Me.cbSourceOrdering.SelectedIndex = Enums.Ordering.Standard
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgTVSource_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If Me._id >= 0 Then
                        SQLcommand.CommandText = String.Concat("UPDATE TVSources SET Name = (?), Path = (?), Language = (?), Ordering = (?), Exclude = (?), EpisodeSorting = (?) WHERE ID =", Me._id, ";")
                    Else
                        SQLcommand.CommandText = "INSERT OR REPLACE INTO TVSources (Name, Path, Language, Ordering, Exclude, EpisodeSorting) VALUES (?,?,?,?,?,?);"
                    End If
                    Dim parName As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parName", DbType.String, 0, "Name")
                    Dim parPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPath", DbType.String, 0, "Path")
                    Dim parLanguage As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "Language")
                    Dim parOrdering As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOrdering", DbType.Int16, 0, "Ordering")
                    Dim parExclude As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parExclude", DbType.Boolean, 0, "Exclude")
                    Dim parEpisodeSorting As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeSorting", DbType.Int16, 0, "EpisodeSorting")

                    parName.Value = txtSourceName.Text.Trim
                    parPath.Value = Regex.Replace(txtSourcePath.Text.Trim, "^(\\)+\\\\", "\\")
                    If cbSourceLanguage.Text <> String.Empty Then
                        parLanguage.Value = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbSourceLanguage.Text).abbreviation
                    Else
                        parLanguage.Value = "en"
                    End If
                    If cbSourceOrdering.Text <> String.Empty Then
                        parOrdering.Value = DirectCast(Me.cbSourceOrdering.SelectedIndex, Enums.Ordering)
                    Else
                        parOrdering.Value = Enums.Ordering.Standard
                    End If
                    If cbSourceEpisodeSorting.Text <> String.Empty Then
                        parEpisodeSorting.Value = DirectCast(Me.cbSourceEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)
                    Else
                        parEpisodeSorting.Value = Enums.EpisodeSorting.Episode
                    End If
                    parExclude.Value = chkExclude.Checked

                    SQLcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(705, "TV Source")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.chkExclude.Text = Master.eLang.GetString(164, "Exclude path from library updates")
        Me.gbSourceOptions.Text = Master.eLang.GetString(201, "Source Options")
        Me.lblSourceEpisodeSorting.Text = String.Concat(Master.eLang.GetString(364, "Show Episodes by"), ":")
        Me.lblSourceLanguage.Text = String.Concat(Master.eLang.GetString(1166, "Default Language"), ":")
        Me.lblSourceName.Text = Master.eLang.GetString(199, "Source Name:")
        Me.lblSourceOrdering.Text = String.Concat(Master.eLang.GetString(797, "Default Episode Ordering"), ":")
        Me.lblSourcePath.Text = Master.eLang.GetString(200, "Source Path:")
        Me.fbdBrowse.Description = Master.eLang.GetString(706, "Select the parent folder for your TV Series folders/files.")

        Me.cbSourceLanguage.Items.Clear()
        Me.cbSourceLanguage.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

        Me.cbSourceOrdering.Items.Clear()
        Me.cbSourceOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(1067, "DVD"), Master.eLang.GetString(839, "Absolute"), Master.eLang.GetString(1332, "Day Of Year")})

        Me.cbSourceEpisodeSorting.Items.Clear()
        Me.cbSourceEpisodeSorting.Items.AddRange(New String() {Master.eLang.GetString(755, "Episode #"), Master.eLang.GetString(728, "Aired")})
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