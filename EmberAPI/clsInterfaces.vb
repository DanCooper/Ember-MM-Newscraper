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

#Region "Enumerations"

    Public Enum ResultStatus
        BreakChain
        Cancelled
        NoResult
        Skipped
        Successful
    End Enum

#End Region 'Enumerations

#Region "Nested Interfaces"

    Public Interface IAddon

#Region "Events"

        'Event NeedsRestart()
        'Event SettingsChanged()
        'Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer)

#End Region 'Events

#Region "Properties"

        ReadOnly Property Capabilities_AddonEventTypes() As List(Of Enums.AddonEventType)

        ReadOnly Property Capabilities_ScraperCapatibilities() As List(Of Enums.ScraperCapatibility)

        ReadOnly Property IsBusy() As Boolean

        Property IsEnabled_Generic() As Boolean

        Property IsEnabled_Data_Movie() As Boolean

        Property IsEnabled_Data_Movieset() As Boolean

        Property IsEnabled_Data_TV() As Boolean

        Property IsEnabled_Image_Movie() As Boolean

        Property IsEnabled_Image_Movieset() As Boolean

        Property IsEnabled_Image_TV() As Boolean

        Property IsEnabled_Theme_Movie() As Boolean

        Property IsEnabled_Theme_TV() As Boolean

        Property IsEnabled_Trailer_Movie() As Boolean

        Property SettingsPanels As List(Of ISettingsPanel)

#End Region 'Properties

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanels()

        Function Run(ByRef dbElement As Database.DBElement, ByVal type As Enums.AddonEventType, ByVal lstCommandLineParams As List(Of Object)) As AddonResult

        Sub SaveSettings(ByVal doDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface ISettingsPanel

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
        Event NeedsReload_Movieset()
        Event NeedsReload_TVEpisode()
        Event NeedsReload_TVShow()

        Event NeedsRestart()
        Event SettingsChanged()
        Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer)

#End Region 'Events

#Region "Properties"

        ReadOnly Property ChildType As Enums.SettingsPanelType

        ReadOnly Property Image As Drawing.Image

        Property ImageIndex As Integer

        Property IsEnabled As Boolean

        ReadOnly Property MainPanel As Panel

        Property Order As Integer

        ReadOnly Property ParentType As Enums.SettingsPanelType
        ''' <summary>
        ''' Language-dependent title of the module
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property Title As String

        Property UniqueId As String

#End Region 'Properties

#Region "Methods"

        Sub DoDispose()

        Sub OrderChanged(ByVal totalCount As Integer)

        Sub SaveSettings()

#End Region 'Methods

    End Interface
    ''' <summary>
    ''' Interface to use the generic search results dialog (EmberAPI.dlgSearchResults.vb)
    ''' </summary>
    Public Interface ISearchResultsDialog

#Region "Enumerations"

        Enum TaskType
            GetInfo_Movie
            GetInfo_Movieset
            GetInfo_TVShow
            Search_By_Title_Movie
            Search_By_Title_Movieset
            Search_By_Title_TVShow
            Search_By_UniqueId_Movie
            Search_By_UniqueId_Movieset
            Search_By_UniqueId_TVShow
        End Enum

#End Region 'Enumerations

#Region "Events"

        Event GetInfoFinished(ByVal mainInfo As MediaContainers.MainDetails)

        Event SearchFinished(ByVal searchResults As List(Of MediaContainers.MainDetails))

#End Region 'Events

#Region "Properties"

        ReadOnly Property SearchResults As List(Of MediaContainers.MainDetails)

#End Region 'Properties

#Region "Methods"

        Sub CancelAsync()

        Function GetInfo(ByVal uniqueId As String, ByVal filteredOptions As Structures.ScrapeOptions, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal type As Enums.ContentType) As MediaContainers.MainDetails

        Sub GetInfoAsync(ByVal uniqueId As String, ByRef filteredOptions As Structures.ScrapeOptions, ByVal type As Enums.ContentType)

        Sub SearchAsync_By_Title(ByVal title As String, ByVal type As Enums.ContentType, Optional ByVal year As Integer = 0)

        Sub SearchAsync_By_UniqueId(ByVal uniqueId As String, ByVal type As Enums.ContentType)

#End Region 'Methods

    End Interface

#End Region 'Nested Interfaces

#Region "Nested Types"

    Public Class AddonResult

#Region "Properties"

        Public ReadOnly Property Status As ResultStatus = ResultStatus.NoResult

        Public Property ScraperResult_Data As MediaContainers.MainDetails = Nothing

        Public Property ScraperResult_ImageContainer As New MediaContainers.SearchResultsContainer

        Public Property ScraperResult_Themes As New List(Of MediaContainers.MediaFile)

        Public Property ScraperResult_Trailers As New List(Of MediaContainers.MediaFile)

        Public Property SearchResults As New List(Of MediaContainers.MainDetails)

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            Status = ResultStatus.NoResult
        End Sub

        Public Sub New(ByVal resultStatus As ResultStatus)
            Status = resultStatus
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class