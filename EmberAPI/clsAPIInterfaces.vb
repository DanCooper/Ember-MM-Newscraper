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

        Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByVal _params As List(Of Object), ByVal _refparam As Object, ByVal _dbmovie As Structures.DBMovie) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' return parameters will add in ReturnObject
        ' _params
        '_refparam 
        '_dbmovie 

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Data_Movie

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

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

        Function GetMovieStudio(ByVal DBMovie As Structures.DBMovie, ByVal studio As List(Of String)) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' return objects
        ' DBMovie
        ' studio
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
        ''' <param name="Options"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Scraper(ByVal oDBMovie As Structures.DBMovie, ByVal nMovie As MediaContainers.Movie, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' oDBMovie
        ' nMovie
        ' ScrapeType
        ' Options 

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Data_MovieSet

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperEvent(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)

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

        'MovieSet is byref because some scrapper may run to update only some fields (defined in Scraper Setup)
        'Options is byref to allow field blocking in scraper chain
        Function Scraper(ByVal DBMovieSet As Structures.DBMovieSet, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_MovieSet) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' DBMovieSet
        ' ScrapeType
        ' Options

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Image_Movie

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

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

        Function QueryScraperCapabilities(ByVal cap As Enums.ScraperCapabilities) As Boolean

        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

        Function Scraper(ByVal DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByVal ImageList As List(Of MediaContainers.Image)) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' DBMovie
        ' ImageList

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Image_MovieSet

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperEvent(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object)

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

        Function QueryScraperCapabilities(ByVal cap As Enums.ScraperCapabilities) As Boolean

        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

        Function Scraper(ByVal DBMovieSet As Structures.DBMovieSet, ByVal Type As Enums.ScraperCapabilities, ByVal ImageList As List(Of MediaContainers.Image)) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' DBMovieSet
        ' ImageList


#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Theme_Movie

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

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

        Function Scraper(ByVal DBMovie As Structures.DBMovie, ByVal URLList As List(Of Themes)) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' URLList

        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

    Public Interface ScraperModule_Trailer_Movie

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object)

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

        Function Scraper(ByVal DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByVal URLList As List(Of Trailers)) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' DBMovie
        ' URLList

        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface


    Public Interface ScraperModule_TV

#Region "Events"

        Event ModuleSettingsChanged()

        Event SetupPostScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

        Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

        Event TVScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)

        Event SetupNeedsRestart()

#End Region 'Events

#Region "Properties"

        ReadOnly Property IsBusy() As Boolean

        ReadOnly Property IsPostScraper() As Boolean

        ReadOnly Property IsScraper() As Boolean

        ReadOnly Property ModuleName() As String

        ReadOnly Property ModuleVersion() As String

        Property PosterScraperEnabled() As Boolean

        Property ScraperEnabled() As Boolean

#End Region 'Properties

#Region "Methods"

        Sub CancelAsync()

        Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByVal epDet As MediaContainers.EpisodeDetails) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' epDet

        Function GetLangs(ByVal sMirror As String, ByVal Langs As clsXMLTVDBLanguages) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' Langs

        Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByVal Image As Images) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' Image 

        Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal epDetails As MediaContainers.EpisodeDetails) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' epDetails

        Sub Init(ByVal sAssemblyName As String)

        Function InjectSetupPostScraper() As Containers.SettingsPanel

        Function InjectSetupScraper() As Containers.SettingsPanel

        Function PosterScraper(ByRef DBTV As Structures.DBTV, ByVal ScrapeType As Enums.ScrapeType) As ModuleResult

        Function SaveImages() As ModuleResult

        Sub SaveSetupPosterScraper(ByVal DoDispose As Boolean)

        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

        Function ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As ModuleResult

        Function Scraper(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal ShowLang As String, ByVal SourceLang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As ModuleResult

        Function ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As ModuleResult

#End Region 'Methods

    End Interface

    '	Public Interface EmberTVScraperModule_Data

    '#Region "Events"

    '        Event ModuleSettingsChanged()

    '		Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

    '		Event TVScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)

    '        Event SetupNeedsRestart()

    '#End Region 'Events

    '#Region "Properties"

    '        ReadOnly Property IsBusy() As Boolean

    '		ReadOnly Property ModuleName() As String

    '		ReadOnly Property ModuleVersion() As String

    '		Property ScraperEnabled() As Boolean

    '#End Region	'Properties

    '#Region "Methods"

    '		Sub ScraperOrderChanged()

    '		Sub CancelAsync()

    '        Function ChangeEpisode(ShowID As Integer, TVDBID As String, Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As ModuleResult

    '        Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByRef epDetails As MediaContainers.EpisodeDetails) As ModuleResult

    '        Function GetLangs(sMirror As String, ByRef Langs As List(Of Containers.TVLanguage)) As ModuleResult

    '        Sub Init(ByVal sAssemblyName As String)

    '		Function InjectSetupScraper() As Containers.SettingsPanel

    '		Sub SaveSetupScraper(ByVal DoDispose As Boolean)

    '		Function ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As ModuleResult

    '		Function Scraper(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As ModuleResult

    '		Function ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As ModuleResult

    '#End Region	'Methods

    '	End Interface

    '	Public Interface EmberTVScraperModule_Poster

    '#Region "Events"

    '		Event ModuleSettingsChanged()

    '		Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

    '		Event TVScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)

    '#End Region	'Events

    '#Region "Properties"

    '		ReadOnly Property IsBusy() As Boolean

    '		ReadOnly Property ModuleName() As String

    '		ReadOnly Property ModuleVersion() As String

    '		Property ScraperEnabled() As Boolean

    '#End Region	'Properties

    '#Region "Methods"

    '		Sub ScraperOrderChanged()

    '		Sub CancelAsync()

    '		Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As ModuleResult

    '		Function GetLangs(ByVal sMirror As String, ByRef Langs As List(Of Containers.TVLanguage)) As ModuleResult

    '		Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef Image As Images) As ModuleResult

    '		Sub Init(ByVal sAssemblyName As String)

    '		Function InjectSetupScraper() As Containers.SettingsPanel

    '		Function Scraper(ByRef DBTV As Structures.DBTV, ByVal ScrapeType As Enums.ScrapeType) As ModuleResult

    '		Function SaveImages() As ModuleResult

    '		Sub SaveSetupScraper(ByVal DoDispose As Boolean)

    '#End Region	'Methods

    '	End Interface

    Public Interface ScraperModule_Theme_TV

#Region "Events"

        Event ModuleSettingsChanged()

        Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

        Event SetupNeedsRestart()

        Event TVScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal Parameter As Object)

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

        Function Scraper(ByVal DBTV As Structures.DBTV, ByVal URLList As List(Of Themes)) As Threading.Tasks.Task(Of Interfaces.ModuleResult)
        ' Return Objects are
        ' URLList

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
        Public ReturnObj As List(Of Object)

        <Obsolete("BoolProperty has been marked Obsolete in v1.4, and will be removed shortly", True)> _
        Public BoolProperty As Boolean

#End Region 'Fields

#Region "Constructors"
        Public Sub New(ByVal bc As Boolean)
            ReturnObj = New List(Of Object)
        End Sub

#End Region 'Constructors
    End Structure

#End Region 'Nested Types

End Class