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

#Region "Enums"

    Public Enum InterfaceType As Integer
        Data_Movie
        Data_Movieset
        Data_TV
        Generic
        Image_Movie
        Image_Movieset
        Image_TV
        Theme_Movie
        Theme_TV
        Trailer_Movie
    End Enum

#End Region 'Enums

#Region "Interfaces"

    ' Interfaces for external addons
    Public Interface IGenericAddon

#Region "Properties"

        ReadOnly Property IsBusy() As Boolean

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

        ReadOnly Property Type() As List(Of Enums.ModuleEventType)

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event GenericEvent(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object))
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Function Run(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object), ByRef SingleObjekt As Object, ByRef DBElement As Database.DBElement) As ModuleResult

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

    Public Interface IScraperAddon_Data_Movie

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal Type As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef Studios As List(Of String)) As ModuleResult

        Function GetTMDbID(ByVal IMDBID As String, ByRef TMDBID As String) As ModuleResult

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function Run(ByRef DBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As ModuleResult_Data_Movie

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Data_MovieSet

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Function GetCollectionID(ByVal sIMDBID As String, ByRef sCollectionID As String) As ModuleResult

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function Run(ByRef DBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As ModuleResult_Data_MovieSet

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Data_TV

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function Run_TVEpisode(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As ModuleResult_Data_TVEpisode

        Function Run_TVSeason(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As ModuleResult_Data_TVSeason

        Function Run_TVShow(ByRef oDBTV As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As ModuleResult_Data_TVShow

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Image_Movie

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameters As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean

        Function Run(ByRef DBElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Image_Movieset

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean

        Function Run(ByRef DBMovieSet As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Image_TV

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean

        Function Run(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Theme_Movie

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function Run(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.Theme)) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Theme_TV

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function Run(ByRef DBTV As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.Theme)) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface IScraperAddon_Trailer_Movie

#Region "Properties"

        Property IsEnabled() As Boolean

        Property Order() As Integer

        Property SettingsPanel As Containers.SettingsPanel

#End Region 'Properties

#Region "Events"

        Event NeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event SettingsChanged()
        Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

        Sub Init()

        Sub InjectSettingsPanel()

        Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)

        Function Run(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.Trailer)) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

#End Region 'Interfaces

#Region "Nested Types"
    ''' <summary>
    ''' This structure is returned by most scraper interfaces to represent the
    ''' status of the operation that was requested
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ModuleResult

#Region "Fields"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public breakChain As Boolean
        ''' <summary>
        ''' An error has occurred in the module, and its operation has been cancelled. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Cancelled As Boolean

#End Region 'Fields

    End Structure
    ''' <summary>
    ''' This structure is returned by movie data scraper interfaces to represent the
    ''' status of the operation that was requested
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ModuleResult_Data_Movie

#Region "Fields"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public breakChain As Boolean
        ''' <summary>
        ''' An error has occurred in the module, and its operation has been cancelled. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Cancelled As Boolean

        Public Result As MediaContainers.MainDetails

#End Region 'Fields

    End Structure
    ''' <summary>
    ''' This structure is returned by movieset data scraper interfaces to represent the
    ''' status of the operation that was requested
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ModuleResult_Data_MovieSet

#Region "Fields"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public breakChain As Boolean
        ''' <summary>
        ''' An error has occurred in the module, and its operation has been cancelled. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Cancelled As Boolean

        Public Result As MediaContainers.MainDetails

#End Region 'Fields

    End Structure
    ''' <summary>
    ''' This structure is returned by tv episode data scraper interfaces to represent the
    ''' status of the operation that was requested
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ModuleResult_Data_TVEpisode

#Region "Fields"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public breakChain As Boolean
        ''' <summary>
        ''' An error has occurred in the module, and its operation has been cancelled. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Cancelled As Boolean

        Public Result As MediaContainers.MainDetails

#End Region 'Fields

    End Structure
    ''' <summary>
    ''' This structure is returned by tv season data scraper interfaces to represent the
    ''' status of the operation that was requested
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ModuleResult_Data_TVSeason

#Region "Fields"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public breakChain As Boolean
        ''' <summary>
        ''' An error has occurred in the module, and its operation has been cancelled. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Cancelled As Boolean

        Public Result As MediaContainers.MainDetails

#End Region 'Fields

    End Structure
    ''' <summary>
    ''' This structure is returned by tv show data scraper interfaces to represent the
    ''' status of the operation that was requested
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ModuleResult_Data_TVShow

#Region "Fields"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public breakChain As Boolean
        ''' <summary>
        ''' An error has occurred in the module, and its operation has been cancelled. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Cancelled As Boolean

        Public Result As MediaContainers.MainDetails

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class