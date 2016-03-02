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
Imports EmberAPI

Public Class dlgSortFiles

#Region "Fields"

    Private _hitgo As Boolean = False

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With fbdBrowse
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath) Then
                    txtPath.Text = .SelectedPath
                End If
            End If
        End With
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        '//
        ' Convert a file source into a folder source by separating everything into separate folders
        '\\

        If Not String.IsNullOrEmpty(txtPath.Text) AndAlso Directory.Exists(txtPath.Text) Then
            If MessageBox.Show(String.Concat(Master.eLang.GetString(220, "WARNING: If you continue, all files will be sorted into separate folders."), Environment.NewLine, Environment.NewLine, Master.eLang.GetString(101, "Are you sure you want to continue?")), Master.eLang.GetString(104, "Are you sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                _hitgo = True
                FileUtils.FileSorter.SortFiles(txtPath.Text)
                lblStatus.Text = "Done!"
                pbStatus.Value = 0

                Master.eSettings.SortPath = txtPath.Text
            End If
        Else
            MessageBox.Show(Master.eLang.GetString(221, "The folder you entered does not exist. Please enter a valid path."), Master.eLang.GetString(222, "Directory Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPath.Focus()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = If(_hitgo, System.Windows.Forms.DialogResult.OK, System.Windows.Forms.DialogResult.Cancel)
        Close()
    End Sub

    Private Sub dlgSortFiles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler FileUtils.FileSorter.ProgressUpdated, AddressOf UpdateProgress
        pbStatus.Maximum = 100
        SetUp()
        txtPath.Text = Master.eSettings.SortPath
    End Sub

    Private Sub dlgSortFiles_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(213, "Sort Files Into Folders")
        Cancel_Button.Text = Master.eLang.GetString(19, "Close")
        btnGo.Text = Master.eLang.GetString(214, "Go")
        gbStatus.Text = Master.eLang.GetString(215, "Status")
        lblStatus.Text = Master.eLang.GetString(216, "Enter Path and Press ""Go"" to Begin.")
        lblPathToSort.Text = Master.eLang.GetString(217, "Path to Sort:")
        fbdBrowse.Description = Master.eLang.GetString(218, "Select the folder which contains the files you wish to sort.")
    End Sub
    ''' <summary>
    ''' Event handler to display status text and progress bar updates during file moving operations
    ''' </summary>
    ''' <param name="iPercent">Percentage completed</param>
    ''' <param name="sStatus"></param>
    ''' <remarks></remarks>
    Private Sub UpdateProgress(ByVal iPercent As Integer, ByVal sStatus As String)
        lblStatus.Text = sStatus
        pbStatus.Value = iPercent
    End Sub

#End Region 'Methods

End Class