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
        Me.btnBrowse.Enabled = False
        Me.txtSourcePath.Enabled = False

        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String) As Windows.Forms.DialogResult
        Me.tmppath = strSearchPath

        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal strSearchPath As String, ByVal strFolderPath As String) As Windows.Forms.DialogResult
        Me.tmppath = strSearchPath
        Me.txtSourcePath.Text = strFolderPath

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

        If String.IsNullOrEmpty(Me.txtSourceName.Text) Then
            pbValid.Image = My.Resources.invalid
        Else
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idSource FROM tvshowsource WHERE strName LIKE """, Me.txtSourceName.Text.Trim, """ AND idSource != ", Me._id, ";")
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

        If Not String.IsNullOrEmpty(Me.txtSourcePath.Text) AndAlso Directory.Exists(Me.txtSourcePath.Text.Trim) AndAlso _
            Not String.IsNullOrEmpty(Me.cbSourceLanguage.Text) AndAlso Not String.IsNullOrEmpty(Me.cbSourceOrdering.Text) AndAlso _
            Not String.IsNullOrEmpty(Me.cbSourceEpisodeSorting.Text) AndAlso isValid Then
            Me.OK_Button.Enabled = True
        End If
    End Sub

    Private Sub dlgMovieSource_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()

        If Not Me._id = -1 Then
            Dim s As Database.DBSource = Master.TVShowSources.FirstOrDefault(Function(y) y.ID = Me._id)
            If s IsNot Nothing Then
                Me.autoName = False
                If Me.cbSourceLanguage.Items.Count > 0 Then
                    Me.cbSourceLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = s.Language).name
                End If
                Me.cbSourceOrdering.SelectedIndex = s.Ordering
                Me.cbSourceEpisodeSorting.SelectedIndex = s.EpisodeSorting
                Me.chkExclude.Checked = s.Exclude
                Me.txtSourceName.Text = s.Name
                Me.txtSourcePath.Text = s.Path
            End If
        Else
            If Me.cbSourceLanguage.Items.Count > 0 Then
                Me.cbSourceLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVGeneralLanguage).name
            End If
            Me.cbSourceEpisodeSorting.SelectedIndex = Enums.EpisodeSorting.Episode
            Me.cbSourceOrdering.SelectedIndex = Enums.Ordering.Standard
        End If
    End Sub

    Private Sub dlgTVSource_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
        Me.txtSourcePath.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                If Not Me._id = -1 Then
                    SQLcommand.CommandText = String.Concat("UPDATE tvshowsource SET strName = (?), strPath = (?), strLanguage = (?), iOrdering = (?), bExclude = (?), iEpisodeSorting = (?) WHERE idSource =", Me._id, ";")
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
                    parLanguage.Value = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbSourceLanguage.Text).abbreviation
                Else
                    parLanguage.Value = "en"
                End If
                If Not String.IsNullOrEmpty(cbSourceOrdering.Text) Then
                    parOrdering.Value = DirectCast(Me.cbSourceOrdering.SelectedIndex, Enums.Ordering)
                Else
                    parOrdering.Value = Enums.Ordering.Standard
                End If
                If Not String.IsNullOrEmpty(cbSourceEpisodeSorting.Text) Then
                    parEpisodeSorting.Value = DirectCast(Me.cbSourceEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)
                Else
                    parEpisodeSorting.Value = Enums.EpisodeSorting.Episode
                End If

                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
        End Using

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