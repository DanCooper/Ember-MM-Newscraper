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

Public Class frmSettingsHolder

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean)
    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(cbEnabled.Checked)
    End Sub

    Public Sub New()
        InitializeComponent()
        SetUp()
    End Sub

    Private Sub SetUp()
        btnFilterSave.Text = Master.eLang.GetString(1001, "Save Filter")
        btnFilterReset.Text = Master.eLang.GetString(1002, "Reset Filter")
        cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkExportMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        gbFilterOpts.Text = Master.eLang.GetString(998, "Filter Settings")
        gbGeneralOpts.Text = Master.eLang.GetString(38, "General Settings")
        gbImageOpts.Text = Master.eLang.GetString(999, "Image Settings")
        lblFilter.Text = Master.eLang.GetString(994, "Generate Filter")
        lblImageFanartWidth.Text = Master.eLang.GetString(996, "Fanart Width [px]")
        lblImagePosterHeight.Text = Master.eLang.GetString(997, "Poster Height [px]")
        lblImageQuality.Text = Master.eLang.GetString(478, "Quality:")
        lblIn.Text = Master.eLang.GetString(331, "in")
        txt_exportmoviepath.Text = Master.eLang.GetString(995, "Export Path")
        cbSearch.Items.AddRange(New Object() {Master.eLang.GetString(318, "Source Folder"), Master.eLang.GetString(21, "Title"), Master.eLang.GetString(278, "Year"), Master.eLang.GetString(319, "Video Flag"), Master.eLang.GetString(320, "Audio Flag")})
        lstSources.Items.Clear()
        For Each s As Database.DBSource In Master.MovieSources
            lstSources.Items.Add(s.Name)
        Next
    End Sub

    Private Sub txt_exportmoviepath_TextChanged(sender As Object, e As EventArgs) Handles txt_exportmoviepath.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkExportMissingEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkExportMissingEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbo_exportmoviefanart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_exportmoviefanart.SelectedIndexChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbo_exportmovieposter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_exportmovieposter.SelectedIndexChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub lbl_exportmoviefilter1saved_TextChanged(sender As Object, e As EventArgs) Handles lbl_exportmoviefilter1saved.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub lbl_exportmoviefilter2saved_TextChanged(sender As Object, e As EventArgs) Handles lbl_exportmoviefilter2saved.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub lbl_exportmoviefilter3saved_TextChanged(sender As Object, e As EventArgs) Handles lbl_exportmoviefilter3saved.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbo_exportmoviequality_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_exportmoviequality.SelectedIndexChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSearch.SelectedIndexChanged
        txtSearch.Text = ""
        If ((cbSearch.Text = Master.eLang.GetString(318, "Source Folder") AndAlso lstSources.CheckedItems.Count > 0) OrElse txtSearch.Text <> "") AndAlso cbSearch.Text <> "" Then
            btnFilterSave.Enabled = True
        Else
            btnFilterSave.Enabled = False
        End If
        If cbSearch.Text = Master.eLang.GetString(318, "Source Folder") Then
            'cbFilterSource.Visible = True
            btnSource.Visible = True
            txtSearch.ReadOnly = True
        Else
            'cbFilterSource.Visible = False
            btnSource.Visible = False
            txtSearch.ReadOnly = False
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text <> "" AndAlso cbSearch.Text <> "" Then
            btnFilterSave.Enabled = True
        Else
            btnFilterSave.Enabled = False
        End If
    End Sub

    Private Sub btnFilterSave_Click(sender As Object, e As EventArgs) Handles btnFilterSave.Click
        Dim sFilter As String = String.Empty
        If cbSearch.Text = Master.eLang.GetString(318, "Source Folder") Then
            For Each s As String In lstSources.CheckedItems
                sFilter = String.Concat(sFilter, If(sFilter = String.Empty, String.Empty, ";"), s.ToString)
            Next
        Else
            sFilter = txtSearch.Text
        End If
        sFilter = sFilter & "#" & cbSearch.Text

        If cbo_exportmoviefilter.Text = "Filter 1" Then
            lbl_exportmoviefilter1saved.Text = sFilter
        ElseIf cbo_exportmoviefilter.Text = "Filter 2" Then
            lbl_exportmoviefilter2saved.Text = sFilter
        ElseIf cbo_exportmoviefilter.Text = "Filter 3" Then
            lbl_exportmoviefilter3saved.Text = sFilter
        End If
        txtSearch.Text = ""
    End Sub

    Private Sub btnSource_Click(sender As Object, e As EventArgs) Handles btnSource.Click
        If btnSource.ImageIndex = 0 Then
            lstSources.Visible = True
            btnSource.ImageIndex = 1
        Else
            lstSources.Visible = False
            btnSource.ImageIndex = 0
            Dim sFilter As String = String.Empty
            If cbSearch.Text = Master.eLang.GetString(318, "Source Folder") Then
                For Each s In lstSources.CheckedItems
                    sFilter = String.Concat(sFilter, If(sFilter = String.Empty, String.Empty, ";"), s.ToString)
                Next
                txtSearch.Text = sFilter
            End If
        End If
    End Sub

    Private Sub btnExportPath_Click(sender As Object, e As EventArgs) Handles btnExportPath.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = "Select ExportPath"
            If dlg.ShowDialog() = DialogResult.OK Then
                txt_exportmoviepath.Text = dlg.SelectedPath
            End If
        End Using
    End Sub

    Private Sub btnFilterReset_Click(sender As Object, e As EventArgs) Handles btnFilterReset.Click
        If cbo_exportmoviefilter.Text = "Filter 1" Then
            lbl_exportmoviefilter1saved.Text = "-"
        ElseIf cbo_exportmoviefilter.Text = "Filter 2" Then
            lbl_exportmoviefilter2saved.Text = "-"
        ElseIf cbo_exportmoviefilter.Text = "Filter 3" Then
            lbl_exportmoviefilter3saved.Text = "-"
        End If
        txtSearch.Text = ""
    End Sub

#End Region 'Methods

End Class