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


Public Class Notifications

#Region "Events"

    Public Shared Event ShowNotification(ByVal timeout As Integer, ByVal title As String, ByVal message As String, ByVal icon As Windows.Forms.ToolTipIcon)

#End Region 'Events

#Region "Enumerations"

    Public Enum Type As Integer
        Added_Movie
        Added_Movieset
        Added_TVEpisode
        Added_TVShow
        [Error]
        Information
        Scraped_Movie
        Scraped_Movieset
        Scraped_TVEpisode
        Scraped_TVSeason
        Scraped_TVShow
        Warning
    End Enum

#End Region 'Enumerations

#Region "Methods"

    Private Shared Function IsEnabled(ByVal type As Type) As Boolean
        With Master.eSettings
            Select Case type
                Case Type.Added_Movie
                    Return .GeneralNotificationAddedMovie
                Case Type.Added_Movieset
                    Return .GeneralNotificationAddedMovieset
                Case Type.Added_TVEpisode
                    Return .GeneralNotificationAddedTVEpisode
                Case Type.Added_TVShow
                    Return .GeneralNotificationAddedTVShow
                Case Type.Error
                    Return .GeneralNotificationError
                Case Type.Information
                    Return .GeneralNotificationInformation
                Case Type.Scraped_Movie
                    Return .GeneralNotificationScrapedMovie
                Case Type.Scraped_Movieset
                    Return .GeneralNotificationScrapedMovieset
                Case Type.Scraped_TVEpisode
                    Return .GeneralNotificationScrapedTVEpisode
                Case Type.Scraped_TVSeason
                    Return .GeneralNotificationScrapedTVSeason
                Case Type.Scraped_TVShow
                    Return .GeneralNotificationScrapedTVShow
                Case Type.Warning
                    Return .GeneralNotificationWarning
                Case Else
                    Return False
            End Select
        End With
    End Function

    Public Shared Sub NewNotification(ByVal [type] As Type, ByVal message As String)
        NewNotification(type, String.Empty, message)
    End Sub

    Public Shared Sub NewNotification(ByVal [type] As Type, ByVal title As String, ByVal message As String)
        If IsEnabled(type) Then
            Dim iTimeout As Integer = 5000
            Dim nIcon As Windows.Forms.ToolTipIcon = Windows.Forms.ToolTipIcon.Info
            Dim strTitle As String = title
            Select Case type
                Case Type.Added_Movie
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(817, "New Movie Added"))
                Case Type.Added_Movieset
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(477, "New MovieSet Added"))
                Case Type.Added_TVEpisode
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(818, "New Episode Added"))
                Case Type.Added_TVShow
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(476, "New Show Added"))
                Case Type.Error
                    iTimeout = 15000
                    nIcon = Windows.Forms.ToolTipIcon.Error
                Case Type.Information
                Case Type.Scraped_Movie
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(813, "Movie Scraped"))
                Case Type.Scraped_Movieset
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(1204, "MovieSet Scraped"))
                Case Type.Scraped_TVEpisode
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(883, "Episode Scraped"))
                Case Type.Scraped_TVSeason
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(247, "Season Scraped"))
                Case Type.Scraped_TVShow
                    strTitle = If(Not String.IsNullOrEmpty(title), title, Master.eLang.GetString(248, "Show Scraped"))
                Case Type.Warning
                    iTimeout = 15000
                    nIcon = Windows.Forms.ToolTipIcon.Warning
            End Select
            RaiseEvent ShowNotification(iTimeout, strTitle, message, nIcon)
        End If
    End Sub

#End Region 'Methods

End Class