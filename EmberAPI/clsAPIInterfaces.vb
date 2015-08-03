Imports System.Drawing

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

#Region "Nested Interfaces"

    ' Interfaces for external Modules
    Public Interface GenericModule

#Region "Events"

        Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        Event ModuleSettingsChanged()
        Event ModuleSetupChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer)
        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        Property Enabled() As Boolean
        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType)
        ReadOnly Property ModuleVersion() As String

#End Region 'Properties

#Region "Methods"

        Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String)
        Function InjectSetup() As Containers.SettingsPanel
        Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Database.DBElement, ByRef _dbtv As Structures.DBTV, ByRef _dbmovieset As Database.DBElement) As ModuleResult
        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Data_Movie

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef sStudio As List(Of String)) As ModuleResult
        Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As ModuleResult
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="oDBMovie">Clone of original DBMovie. To fill with new IMDB or TMDB ID's for subsequent scrapers.</param>
        ''' <param name="nMovie">New and empty Movie container to fill with new scraped data</param>
        ''' <param name="ScrapeType">What kind of data is being requested from the scrape(global scraper settings)</param>
        ''' <param name="ScrapeOptions"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Scraper(ByRef oDBMovie As Database.DBElement, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_Movie) As ModuleResult

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Data_MovieSet

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Function GetCollectionID(ByVal sIMDBID As String, ByRef sCollectionID As String) As ModuleResult
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="oDBMovieSet">Clone of original DBMovieSet. To fill with new TMDB ID's for subsequent scrapers.</param>
        ''' <param name="nMovieSet">New and empty MovieSet container to fill with new scraped data</param>
        ''' <param name="ScrapeModifier"></param>
        ''' <param name="ScrapeType"></param>
        ''' <param name="ScrapeOptions"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Scraper(ByRef oDBMovieSet As Database.DBElement, ByRef nMovieSet As MediaContainers.MovieSet, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_MovieSet) As ModuleResult

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Data_TV

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="oDBTV">Clone of original DBTV. To fill with new TVDB, IMDB or TMDB ID's for subsequent scrapers.</param>
        ''' <param name="nShow">New and empty Show container to fill with new scraped data</param>
        ''' <param name="ScrapeType">What kind of data is being requested from the scrape(global scraper settings)</param>
        ''' <param name="ScrapeOptions"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Scraper_TVShow(ByRef oDBTV As Structures.DBTV, ByRef nShow As MediaContainers.TVShow, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_TV) As ModuleResult
        ''' <summary>
        ''' Get single episode information
        ''' </summary>
        ''' <param name="oDBTVEpisode"></param>
        ''' <param name="nEpisode"></param>
        ''' <param name="ScrapeOptions"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Scraper_TVEpisode(ByRef oDBTVEpisode As Structures.DBTV, ByRef nEpisode As MediaContainers.EpisodeDetails, ByVal ScrapeOptions As Structures.ScrapeOptions_TV) As ModuleResult

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Image_Movie

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()
        Event ImagesDownloaded(ByVal Images As List(Of MediaContainers.Image))
        Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)
        Function Scraper(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_Movie_MovieSet, ByVal ScrapeModifier As Structures.ScrapeModifier) As Interfaces.ModuleResult

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Image_MovieSet

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()
        Event ImagesDownloaded(ByVal Posters As List(Of MediaContainers.Image))
        Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)
        Function Scraper(ByRef DBMovieSet As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_Movie_MovieSet, ByVal ScrapeModifier As Structures.ScrapeModifier) As Interfaces.ModuleResult

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Image_TV

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()
        Event ImagesDownloaded(ByVal Images As List(Of MediaContainers.Image))
        Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)
        Function Scraper(ByRef DBTV As Structures.DBTV, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_TV, ByVal ScrapeModifier As Structures.ScrapeModifier) As Interfaces.ModuleResult

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Theme_Movie

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Function Scraper(ByVal DBMovie As Database.DBElement, ByRef URLList As List(Of Themes)) As ModuleResult
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Theme_TV

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Function Scraper(ByVal DBTV As Structures.DBTV, ByRef URLList As List(Of Themes)) As ModuleResult
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)


        'Sub ScraperOrderChanged()

        'Sub CancelAsync()

        'Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As ModuleResult

        'Function GetLangs(ByVal sMirror As String, ByRef Langs As List(Of Containers.TVLanguage)) As ModuleResult

        'Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef Image As Images) As ModuleResult

        'Sub Init(ByVal sAssemblyName As String)

        'Function InjectSetupScraper() As Containers.SettingsPanel

        'Function Scraper(ByRef DBTV As Structures.DBTV, ByVal ScrapeType As Enums.ScrapeType) As ModuleResult

        'Function SaveImages() As ModuleResult

        'Sub SaveSetupScraper(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Trailer_Movie

#Region "Events"

        Event ModuleSettingsChanged()
        Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)
        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        ReadOnly Property ModuleName() As String
        ReadOnly Property ModuleVersion() As String
        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub ScraperOrderChanged()
        Sub Init(ByVal sAssemblyName As String)
        Function InjectSetupScraper() As Containers.SettingsPanel
        Function Scraper(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.Trailer)) As Interfaces.ModuleResult
        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

#End Region 'Nested Interfaces

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

#End Region 'Nested Types

End Class