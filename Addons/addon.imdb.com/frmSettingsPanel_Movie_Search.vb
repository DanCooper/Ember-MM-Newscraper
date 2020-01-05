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

Public Class frmSettingsPanel_Movie_Search

#Region "Events"

    Public Event NeedsRestart()
    Public Event SettingsChanged()

#End Region 'Events

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub Setup()
        chkPartialTitles.Text = Master.eLang.GetString(1183, "Partial Titles")
        chkPopularTitles.Text = Master.eLang.GetString(1182, "Popular Titles")
        chkShortTitles.Text = Master.eLang.GetString(837, "Short Titles")
        chkTvTitles.Text = Master.eLang.GetString(1184, "TV Movie Titles")
        chkVideoTitles.Text = Master.eLang.GetString(1185, "Video Titles")
        gbScraper.Text = Master.eLang.GetString(1186, "Scraper Options")
    End Sub

#End Region 'Methods

End Class