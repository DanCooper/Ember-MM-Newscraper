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

Public Class Interfaces

#Region "Interfaces"

    Public Interface IAddon

#Region "Properties"

        ReadOnly Property Capabilities_AddonEventTypes() As List(Of Enums.AddonEventType)

        ReadOnly Property Capabilities_ScraperCapatibilities() As List(Of Enums.ScraperCapatibility)

        ReadOnly Property IsBusy() As Boolean

        Property IsEnabled() As Boolean

        Property SettingsPanels As List(Of Containers.SettingsPanel)

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Function Run(ByRef dbElement As Database.DBElement, ByVal type As Enums.AddonEventType, ByVal lstCommandLineParams As List(Of Object)) As AddonResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IMasterSettingsPanel

#Region "Events"

        Event NeedsDBClean_Movie()
        Event NeedsDBClean_TV()
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ID">ID of the source or -1 for all sources</param>
        Event NeedsDBUpdate_Movie(ByVal id As Long)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ID">ID of the source or -1 for all sources</param>
        Event NeedsDBUpdate_TV(ByVal id As Long)
        Event NeedsReload_Movie()
        Event NeedsReload_MovieSet()
        Event NeedsReload_TVEpisode()
        Event NeedsReload_TVShow()
        Event NeedsRestart()
        Event SettingsChanged()

#End Region 'Events

#Region "Methods"

        Sub DoDispose()

        Function InjectSettingsPanel() As Containers.SettingsPanel

        Sub SaveSettings()

#End Region 'Methods

    End Interface

#End Region 'Interfaces

#Region "Nested Types"

    Public Class AddonResult

#Region "Fields"

        Public bBreakChain As Boolean
        Public bCancelled As Boolean
        Public bPreCheckSuccessful As Boolean
        Public ScraperResult_Data As MediaContainers.MainDetails = Nothing
        Public ScraperResult_ImageContainer As New MediaContainers.SearchResultsContainer
        Public ScraperResult_Themes As New List(Of MediaContainers.Theme)
        Public ScraperResult_Trailers As New List(Of MediaContainers.Trailer)
        Public SearchResults As New List(Of MediaContainers.MainDetails)

#End Region 'Fields

    End Class

#End Region 'Nested Types

End Class