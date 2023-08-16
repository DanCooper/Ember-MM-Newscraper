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

    Public Event ModuleSettingsChanged()
    Public Event ModuleSetupChanged(ByVal state As Boolean)

#End Region 'Events

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub Setup()
        With Addon.Localisation
            chkEnabled.Text = .CommonWordsList.Enabled
            chkGetWatchedStateBeforeEdit_Movie.Text = .CommonWordsList.Before_Edit
            chkGetWatchedStateBeforeEdit_TVEpisode.Text = .CommonWordsList.Before_Edit
            chkGetWatchedStateScraperMulti_Movie.Text = .CommonWordsList.During_Multi_Scraping
            chkGetWatchedStateScraperMulti_TVEpisode.Text = .CommonWordsList.During_Multi_Scraping
            chkGetWatchedStateScraperSingle_Movie.Text = .CommonWordsList.During_Single_Scraping
            chkGetWatchedStateScraperSingle_TVEpisode.Text = .CommonWordsList.During_Single_Scraping
            gbGetWatchedState.Text = .GetString(2, "Get Watched-State")
            gbGetWatchedStateMovies.Text = .CommonWordsList.Movies
            gbGetWatchedStateTVEpisodes.Text = .CommonWordsList.Episodes
        End With
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub Enabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleSetupChanged(chkEnabled.Checked)
    End Sub

    Private Sub EnableApplyButton() Handles _
        chkGetWatchedStateBeforeEdit_Movie.CheckedChanged,
        chkGetWatchedStateBeforeEdit_TVEpisode.CheckedChanged,
        chkGetWatchedStateScraperMulti_Movie.CheckedChanged,
        chkGetWatchedStateScraperMulti_TVEpisode.CheckedChanged,
        chkGetWatchedStateScraperSingle_Movie.CheckedChanged,
        chkGetWatchedStateScraperSingle_TVEpisode.CheckedChanged

        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class