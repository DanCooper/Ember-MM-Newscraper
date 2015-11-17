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
        'btnFilterSave.Text = Master.eLang.GetString(1001, "Save Filter")
        'btnFilterReset.Text = Master.eLang.GetString(1002, "Reset Filter")
        cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkExportMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        'gbFilterOpts.Text = Master.eLang.GetString(998, "Filter Settings")
        gbGeneralOpts.Text = Master.eLang.GetString(38, "General Settings")
        'gbImageOpts.Text = Master.eLang.GetString(999, "Image Settings")
        'lblFilter.Text = Master.eLang.GetString(994, "Generate Filter")
        'lblImageFanartWidth.Text = Master.eLang.GetString(996, "Fanart Width [px]")
        'lblImagePosterHeight.Text = Master.eLang.GetString(997, "Poster Height [px]")
        'lblImageQuality.Text = Master.eLang.GetString(478, "Quality:")
        'lblIn.Text = Master.eLang.GetString(331, "in")
        txt_exportmoviepath.Text = Master.eLang.GetString(995, "Export Path")
        'cbSearch.Items.AddRange(New Object() {Master.eLang.GetString(318, "Source Folder"), Master.eLang.GetString(21, "Title"), Master.eLang.GetString(278, "Year"), Master.eLang.GetString(319, "Video Flag"), Master.eLang.GetString(320, "Audio Flag")})
        'lstSources.Items.Clear()
        'For Each s As Database.DBSource In Master.MovieSources
        '    lstSources.Items.Add(s.Name)
        'Next
    End Sub

    Private Sub txt_exportmoviepath_TextChanged(sender As Object, e As EventArgs) Handles txt_exportmoviepath.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkExportMissingEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkExportMissingEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnExportPath_Click(sender As Object, e As EventArgs) Handles btnExportPath.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = "Select ExportPath"
            If dlg.ShowDialog() = DialogResult.OK Then
                txt_exportmoviepath.Text = dlg.SelectedPath
            End If
        End Using
    End Sub

#End Region 'Methods

End Class