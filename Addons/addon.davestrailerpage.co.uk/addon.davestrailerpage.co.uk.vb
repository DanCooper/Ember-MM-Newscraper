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
' ###############################################################################

Imports EmberAPI
Imports NLog

Public Class Addon
    Implements Interfaces.IAddon

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Capabilities_AddonEventTypes As List(Of Enums.AddonEventType) Implements Interfaces.IAddon.Capabilities_AddonEventTypes
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {
                                                     Enums.AddonEventType.Scrape_Movie
                                                     })
        End Get
    End Property

    Public ReadOnly Property Capabilities_ScraperCapatibilities As List(Of Enums.ScraperCapatibility) Implements Interfaces.IAddon.Capabilities_ScraperCapatibilities
        Get
            Return New List(Of Enums.ScraperCapatibility)(New Enums.ScraperCapatibility() {
                                                          Enums.ScraperCapatibility.Movie_Trailer
                                                          })
        End Get
    End Property

    Public ReadOnly Property IsBusy As Boolean Implements Interfaces.IAddon.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Property IsEnabled As Boolean Implements Interfaces.IAddon.IsEnabled
        Get
            Return True
        End Get
        Set(value As Boolean)
            Return
        End Set
    End Property

    Public Property SettingsPanels As New List(Of Containers.SettingsPanel) Implements Interfaces.IAddon.SettingsPanels

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart As Interfaces.IAddon.NeedsRestartEventHandler Implements Interfaces.IAddon.NeedsRestart
    Public Event SettingsChanged As Interfaces.IAddon.SettingsChangedEventHandler Implements Interfaces.IAddon.SettingsChanged
    Public Event StateChanged As Interfaces.IAddon.StateChangedEventHandler Implements Interfaces.IAddon.StateChanged

#End Region 'Events 

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IAddon.Init
        Return
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IAddon.InjectSettingsPanel
        Return
    End Sub

    Public Sub SaveSetup(DoDispose As Boolean) Implements Interfaces.IAddon.SaveSetup
        Return
    End Sub

    Public Function Run(ByRef dbElement As Database.DBElement, type As Enums.AddonEventType, lstCommandLineParams As List(Of Object)) As Interfaces.AddonResult Implements Interfaces.IAddon.Run
        _Logger.Trace("[Apple] [Run] [Started]")
        Dim nAddonResult As New Interfaces.AddonResult
        Select Case type
            '
            'PreCheck
            '
            Case Enums.AddonEventType.Scrape_Movie_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.OriginalTitleSpecified OrElse dbElement.MainDetails.TitleSpecified
            '
            'Scraping
            '
            Case Enums.AddonEventType.Scrape_Movie
                Dim strTitle As String = String.Empty
                If dbElement.MainDetails.OriginalTitleSpecified Then
                    strTitle = dbElement.MainDetails.OriginalTitle
                ElseIf dbElement.MainDetails.TitleSpecified Then
                    strTitle = dbElement.MainDetails.Title
                End If
                If Not String.IsNullOrEmpty(strTitle) Then nAddonResult.ScraperResult_Trailers = Scraper.GetMovieTrailers(strTitle, dbElement.MainDetails.UniqueIDs.IMDbId)
        End Select
        _Logger.Trace("[Apple] [Run] [Started]")
        Return nAddonResult
    End Function

#End Region 'Interface Methods

End Class