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

Imports NLog

Public Class frmSettingsHolder

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Dim isSelected As Boolean = False

#End Region 'Fields

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal state As Boolean)
    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub btnPathBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPathBrowse.Click
        Dim fl As New FolderBrowserDialog
        fl.ShowDialog()
        txtPath.Text = fl.SelectedPath
    End Sub

    Private Sub btnPathEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPathEdit.Click
        lvPaths.SelectedItems(0).SubItems(0).Text = txtName.Text
        lvPaths.SelectedItems(0).SubItems(1).Text = txtPath.Text
        lvPaths.SelectedItems(0).SubItems(2).Text = CType(cbType.SelectedItem, KeyValuePair(Of String, Enums.ContentType)).Value.ToString
        txtName.Text = String.Empty
        txtPath.Text = String.Empty
        cbType.SelectedIndex = -1
        isSelected = False
        CheckButtons()
    End Sub

    Private Sub btnPathNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPathNew.Click
        Dim li As New ListViewItem With {
            .Text = txtName.Text
        }
        li.SubItems.Add(txtPath.Text)
        li.SubItems.Add(CType(cbType.SelectedItem, KeyValuePair(Of String, Enums.ContentType)).Value.ToString)
        lvPaths.Items.Add(li)
        txtName.Text = String.Empty
        txtPath.Text = String.Empty
        cbType.SelectedIndex = -1
        CheckButtons()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnPathRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPathRemove.Click
        lvPaths.Items.RemoveAt(lvPaths.SelectedItems(0).Index)
        txtName.Text = String.Empty
        txtPath.Text = String.Empty
        cbType.SelectedIndex = -1
        isSelected = False
        CheckButtons()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnTeraCopyPathBrowse_Click(sender As Object, e As EventArgs) Handles btnTeraCopyPathBrowse.Click
        Try
            With ofdBrowse
                .FileName = "TeraCopy.exe"
                .Filter = "TeraCopy|*.exe"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) Then
                        txtTeraCopyPath.Text = .FileName
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Sub CheckButtons()
        If Not String.IsNullOrEmpty(txtName.Text) AndAlso Not String.IsNullOrEmpty(txtPath.Text) AndAlso Not cbType.SelectedIndex = -1 AndAlso Not isSelected Then
            btnPathNew.Enabled = True
        Else
            btnPathNew.Enabled = False
        End If
    End Sub

    Private Sub chkEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked)
    End Sub

    Private Sub chkTeraCopyEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkTeraCopyEnable.CheckedChanged
        txtTeraCopyPath.Enabled = chkTeraCopyEnable.Checked
        btnTeraCopyPathBrowse.Enabled = chkTeraCopyEnable.Checked
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub lblTeraCopyLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTeraCopyLink.LinkClicked
        Process.Start("https://codesector.com/teracopy")
    End Sub

    Private Sub lvPaths_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvPaths.SelectedIndexChanged
        If lvPaths.SelectedItems.Count > 0 Then
            isSelected = True
            btnPathRemove.Enabled = True
            btnPathEdit.Enabled = True
            txtName.Text = lvPaths.SelectedItems(0).SubItems(0).Text
            txtPath.Text = lvPaths.SelectedItems(0).SubItems(1).Text

            Select Case lvPaths.SelectedItems(0).SubItems(2).Text
                Case "Movie"
                    cbType.SelectedIndex = 0
                Case "Show"
                    cbType.SelectedIndex = 1
            End Select
        Else
            isSelected = False
            btnPathRemove.Enabled = False
            btnPathEdit.Enabled = False
        End If
        CheckButtons()
    End Sub

    Public Sub New()
        InitializeComponent()
        Setup()
        LoadContentTypes()
    End Sub

    Private Sub Setup()
        ColumnHeader1.Text = Master.eLang.GetString(232, "Name")
        ColumnHeader2.Text = Master.eLang.GetString(410, "Path")
        ColumnHeader3.Text = Master.eLang.GetString(1288, "Type")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkTeraCopyEnable.Text = Master.eLang.GetString(332, "Use TeraCopy to copy/move files")
        lblName.Text = Master.eLang.GetString(232, "Name")
        lblPath.Text = Master.eLang.GetString(410, "Path")
        lblTeraCopyPath.Text = Master.eLang.GetString(333, "Path to TeraCopy:")
        lblType.Text = Master.eLang.GetString(1288, "Type")
    End Sub

    Private Sub LoadContentTypes()
        Dim items As New Dictionary(Of String, Enums.ContentType) From {
            {Master.eLang.GetString(1379, "Movie"), Enums.ContentType.Movie},
            {Master.eLang.GetString(700, "TV Show"), Enums.ContentType.TVShow}
        }
        cbType.DataSource = items.ToList
        cbType.DisplayMember = "Key"
        cbType.ValueMember = "Value"

        cbType.SelectedIndex = -1
    End Sub

    Private Sub cbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbType.SelectedIndexChanged
        CheckButtons()
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtName.TextChanged
        CheckButtons()
    End Sub

    Private Sub txtPath_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPath.TextChanged
        CheckButtons()
    End Sub

    Private Sub txtTeraCopyPath_TextChanged(sender As Object, e As EventArgs) Handles txtTeraCopyPath.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class