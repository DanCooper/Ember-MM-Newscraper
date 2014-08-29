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

        Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Structures.DBMovie) As ModuleResult

        Sub SaveSetup(ByVal DoDispose As Boolean)

#End Region 'Methods

    End Interface

	'    Public Interface EmberMovieScraperModule

	'#Region "Events"

	'        Event ModuleSettingsChanged()

	'        Event MovieScraperEvent(ByVal eType As Enums.MovieScraperEventType, ByVal Parameter As Object)

	'        Event PostScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

	'		Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer)

	'		Event SetupNeedsRestart()

	'        Sub ScraperOrderChanged()

	'        Sub PostScraperOrderChanged()

	'#End Region 'Events

	'#Region "Properties"

	'        ReadOnly Property IsPostScraper() As Boolean

	'        ReadOnly Property IsScraper() As Boolean

	'        ReadOnly Property ModuleName() As String

	'        ReadOnly Property ModuleVersion() As String

	'        Property PostScraperEnabled() As Boolean

	'        Property ScraperEnabled() As Boolean

	'#End Region 'Properties

	'#Region "Methods"

	'        Function DownloadTrailer(ByRef DBMovie As Structures.DBMovie, ByRef sURL As String) As ModuleResult

	'        Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef sStudio As List(Of String)) As ModuleResult

	'        Sub Init(ByVal sAssemblyName As String)

	'        Function InjectSetupPostScraper() As Containers.SettingsPanel

    '        Function QueryPostScraperCapabilities(ByVal cap As Enums.ScraperCapabilities) As Boolean

    '        Function InjectSetupScraper() As Containers.SettingsPanel

    '        Function PostScraper(ByRef DBMovie As Structures.DBMovie, ByVal ScrapeType As Enums.ScrapeType) As ModuleResult

    '        Sub SaveSetupPostScraper(ByVal DoDispose As Boolean)

    '        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

    '        'Movie is byref because some scrapper may run to update only some fields (defined in Scraper Setup)
    '        'Options is byref to allow field blocking in scraper chain
    '        Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions) As ModuleResult

    '        Function SelectImageOfType(ByRef DBMovie As Structures.DBMovie, ByVal _DLType As Enums.MovieImageType, ByRef pResults As Containers.ImgResult, Optional ByVal _isEdit As Boolean = False, Optional ByVal preload As Boolean = False) As ModuleResult

    '#End Region 'Methods

    '    End Interface



    ' ################################################################################
    ' #                             EMBER MEDIA MANAGER                              #
    ' ################################################################################
    ' ################################################################################
    ' # NOTE: the following interfaces could be avoided with a single type			 #
    ' #  and some generic functions with generic Object return and type casting		 #
    ' #																				 #
    ' # IT HAS BEEN DONE ON PURPOSE as we have seen the mess it has been created in  #
    ' # multi purpose Scrapers (data + image + trailers).							 #
    ' # In this way evey scraper will have one precise object and development, debug #
    ' #  and maintenance will be far easier											 #
    ' #																				 #
    ' # In the libraries there will be replicated code to manage the five lists		 #
    ' #	3 for movies and 2 for TV episodes. Even if not optimal this is easier to	 #
    ' #	manage and debug															 #
    ' #																				 #
    ' ################################################################################

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

        Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef sStudio As List(Of String)) As ModuleResult

        Sub Init(ByVal sAssemblyName As String)

        Function InjectSetupScraper() As Containers.SettingsPanel

        Sub SaveSetupScraper(ByVal DoDispose As Boolean)

        'Movie is byref because some scrapper may run to update only some fields (defined in Scraper Setup)
        'Options is byref to allow field blocking in scraper chain
        Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As ModuleResult

        'New scraper handling: DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper; aMovie as ByRef to fill with new scraped data
        Function ScraperNew(ByRef DBMovie As Structures.DBMovie, ByRef nMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As ModuleResult

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
        Function Scraper(ByRef DBMovieSet As Structures.DBMovieSet, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_MovieSet) As ModuleResult

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

        Function Scraper(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Interfaces.ModuleResult

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

        Function Scraper(ByRef DBMovieSet As Structures.DBMovieSet, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Interfaces.ModuleResult

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

        Function Scraper(ByVal DBMovie As Structures.DBMovie, ByRef URLList As List(Of Themes)) As ModuleResult

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

        Function Scraper(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef URLList As List(Of Trailers)) As Interfaces.ModuleResult

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

        Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As ModuleResult

        Function GetLangs(ByVal sMirror As String, ByRef Langs As clsXMLTVDBLanguages) As ModuleResult

        Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByRef epDetails As MediaContainers.EpisodeDetails) As ModuleResult

        Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef Image As Images) As ModuleResult

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

        <Obsolete("BoolProperty has been marked Obsolete in v1.4, and will be removed shortly", True)> _
        Public BoolProperty As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class