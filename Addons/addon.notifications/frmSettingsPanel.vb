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

Public Class frmSettingsPanel

#Region "Events"

    Public Event SettingsChanged()
    Public Event StateChanged(ByVal State As Boolean)

#End Region 'Events

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub chkAddNewMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnNewMovie.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent StateChanged(chkEnabled.Checked)
    End Sub

    Private Sub chkOnError_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnError.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkOnMovieScraped_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnMovieScraped.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkOnNewEp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnNewEpisode.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Setup()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkOnError.Text = Master.eLang.GetString(476, "On Error")
        chkOnNewMovie.Text = Master.eLang.GetString(477, "On New Movie Added")
        chkOnNewEpisode.Text = Master.eLang.GetString(486, "On New Episode Added")
        chkOnMovieScraped.Text = Master.eLang.GetString(485, "On Movie Scraped")
    End Sub

#End Region 'Methods

End Class