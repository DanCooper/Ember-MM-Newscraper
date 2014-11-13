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

Imports System.IO
Imports System.Xml.Serialization
Imports System.Net
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports NLog
Public Class Settings

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private Shared _XMLSettings As New clsXMLSettings

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
    End Sub

#End Region 'Constructors

#Region "Enumerations"

    Public Enum EpRetrieve As Integer
        FromDirectory = 0
        FromFilename = 1
        FromSeasonResult = 2
    End Enum

#End Region 'Enumerations

    'Trick: all the data is now in the shared private variable _XMLSettings. To avoid changing EVERY reference to a settings
    ' we create here property stubs that read the corresponding property of the _XMLSettings
#Region "Properties"

    Public Property MovieScraperCastLimit() As Integer
        Get
            Return Settings._XMLSettings.MovieScraperCastLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviescrapercastlimit = value
        End Set
    End Property

    Public Property RestartScraper() As Boolean
        Get
            Return Settings._XMLSettings.RestartScraper
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.RestartScraper = value
        End Set
    End Property


    Public Property MovieActorThumbsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsOverwrite = value
        End Set
    End Property

    Public Property TVASPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.TVASPosterHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVASPosterHeight = value
        End Set
    End Property

    Public Property TVASPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.TVASPosterWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVASPosterWidth = value
        End Set
    End Property

    Public Property GeneralShowGenresText() As Boolean
        Get
            Return Settings._XMLSettings.GeneralShowGenresText
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralShowGenresText = value
        End Set
    End Property

    Public Property TVGeneralLanguage() As String
        Get
            Return Settings._XMLSettings.TVGeneralLanguage
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TVGeneralLanguage = If(String.IsNullOrEmpty(value), "en", value)
        End Set
    End Property

    Public Property TVGeneralLanguages() As clsXMLTVDBLanguages
        Get
            Return Settings._XMLSettings.TVGeneralLanguages
        End Get
        Set(ByVal value As clsXMLTVDBLanguages)
            Settings._XMLSettings.TVGeneralLanguages = value
        End Set
    End Property

    Public Property MovieClickScrape() As Boolean
        Get
            Return Settings._XMLSettings.MovieClickScrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClickScrape = value
        End Set
    End Property

    Public Property MovieClickScrapeAsk() As Boolean
        Get
            Return Settings._XMLSettings.MovieClickScrapeAsk
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClickScrapeAsk = value
        End Set
    End Property

    Public Property TVEpisodeClickScrape() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeClickScrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeClickScrape = value
        End Set
    End Property

    Public Property TVEpisodeClickScrapeAsk() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeClickScrapeAsk
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeClickScrapeAsk = value
        End Set
    End Property

    Public Property TVSeasonClickScrape() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonClickScrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonClickScrape = value
        End Set
    End Property

    Public Property TVSeasonClickScrapeAsk() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonClickScrapeAsk
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonClickScrapeAsk = value
        End Set
    End Property

    Public Property TVShowClickScrape() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClickScrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClickScrape = value
        End Set
    End Property

    Public Property TVShowClickScrapeAsk() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClickScrapeAsk
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClickScrapeAsk = value
        End Set
    End Property

    Public Property MovieBackdropsAuto() As Boolean
        Get
            Return Settings._XMLSettings.MovieBackdropsAuto
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBackdropsAuto = value
        End Set
    End Property

    Public Property MovieIMDBURL() As String
        Get
            Return Settings._XMLSettings.MovieIMDBURL
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieIMDBURL = value
        End Set
    End Property

    Public Property MovieBackdropsPath() As String
        Get
            Return Settings._XMLSettings.MovieBackdropsPath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieBackdropsPath = value
        End Set
    End Property

    Public Property MovieScraperCastWithImgOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCastWithImgOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCastWithImgOnly = value
        End Set
    End Property

    Public Property MovieScraperCertLang() As String
        Get
            Return Settings._XMLSettings.MovieScraperCertLang
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieScraperCertLang = value
        End Set
    End Property

    Public Property GeneralCheckUpdates() As Boolean
        Get
            Return Settings._XMLSettings.GeneralCheckUpdates
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralCheckUpdates = value
        End Set
    End Property

    Public Property MovieCleanDB() As Boolean
        Get
            Return Settings._XMLSettings.MovieCleanDB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieCleanDB = value
        End Set
    End Property

    Public Property MovieSetCleanDB() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetCleanDB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetCleanDB = value
        End Set
    End Property

    Public Property MovieSetCleanFiles() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetCleanFiles
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetCleanFiles = value
        End Set
    End Property

    Public Property CleanDotFanartJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanDotFanartJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanDotFanartJPG = value
        End Set
    End Property

    Public Property CleanExtrathumbs() As Boolean
        Get
            Return Settings._XMLSettings.CleanExtrathumbs
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanExtrathumbs = value
        End Set
    End Property

    Public Property CleanFanartJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanFanartJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanFanartJPG = value
        End Set
    End Property

    Public Property CleanFolderJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanFolderJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanFolderJPG = value
        End Set
    End Property

    Public Property CleanMovieFanartJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieFanartJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieFanartJPG = value
        End Set
    End Property

    Public Property CleanMovieJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieJPG = value
        End Set
    End Property

    Public Property CleanMovieNameJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieNameJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieNameJPG = value
        End Set
    End Property

    Public Property CleanMovieNFO() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieNFO
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieNFO = value
        End Set
    End Property

    Public Property CleanMovieNFOB() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieNFOB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieNFOB = value
        End Set
    End Property

    Public Property CleanMovieTBN() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieTBN
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieTBN = value
        End Set
    End Property

    Public Property CleanMovieTBNB() As Boolean
        Get
            Return Settings._XMLSettings.CleanMovieTBNB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanMovieTBNB = value
        End Set
    End Property

    Public Property CleanPosterJPG() As Boolean
        Get
            Return Settings._XMLSettings.CleanPosterJPG
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanPosterJPG = value
        End Set
    End Property

    Public Property CleanPosterTBN() As Boolean
        Get
            Return Settings._XMLSettings.CleanPosterTBN
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.CleanPosterTBN = value
        End Set
    End Property

    Public Property FileSystemCleanerWhitelistExts() As List(Of String)
        Get
            Return Settings._XMLSettings.FileSystemCleanerWhitelistExts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.FileSystemCleanerWhitelistExts = value
        End Set
    End Property

    Public Property FileSystemCleanerWhitelist() As Boolean
        Get
            Return Settings._XMLSettings.FileSystemCleanerWhitelist
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.FileSystemCleanerWhitelist = value
        End Set
    End Property

    Public Property MovieTrailerDeleteExisting() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerDeleteExisting
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerDeleteExisting = value
        End Set
    End Property

    Public Property TVDisplayMissingEpisodes() As Boolean
        Get
            Return Settings._XMLSettings.TVDisplayMissingEpisodes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVDisplayMissingEpisodes = value
        End Set
    End Property

    Public Property TVDisplayStatus() As Boolean
        Get
            Return Settings._XMLSettings.TVDisplayStatus
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVDisplayStatus = value
        End Set
    End Property

    Public Property MovieDisplayYear() As Boolean
        Get
            Return Settings._XMLSettings.MovieDisplayYear
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieDisplayYear = value
        End Set
    End Property

    Public Property TVScraperOptionsOrdering() As Enums.Ordering
        Get
            Return Settings._XMLSettings.TVScraperOptionsOrdering
        End Get
        Set(ByVal value As Enums.Ordering)
            Settings._XMLSettings.TVScraperOptionsOrdering = value
        End Set
    End Property

    <XmlArray("EmberModules")> _
    <XmlArrayItem("Module")> _
    Public Property EmberModules() As List(Of ModulesManager._XMLEmberModuleClass)
        Get
            Return Settings._XMLSettings.EmberModules
        End Get
        Set(ByVal value As List(Of ModulesManager._XMLEmberModuleClass))
            Settings._XMLSettings.EmberModules = value
        End Set
    End Property

    Public Property MovieScraperMetaDataIFOScan() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperMetaDataIFOScan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperMetaDataIFOScan = value
        End Set
    End Property

    Public Property TVEpisodeFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.TVEpisodeFanartHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVEpisodeFanartHeight = value
        End Set
    End Property

    Public Property TVEpisodeFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.TVEpisodeFanartWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVEpisodeFanartWidth = value
        End Set
    End Property

    Public Property TVEpisodeFilterCustom() As List(Of String)
        Get
            Return Settings._XMLSettings.TVEpisodeFilterCustom
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.TVEpisodeFilterCustom = value
        End Set
    End Property

    Public Property TVEpisodeFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeFanartCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeFanartCol = value
        End Set
    End Property

    Public Property TVEpisodeNfoCol() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeNfoCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeNfoCol = value
        End Set
    End Property

    Public Property TVEpisodePosterCol() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodePosterCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodePosterCol = value
        End Set
    End Property

    Public Property TVLockEpisodePlot() As Boolean
        Get
            Return Settings._XMLSettings.TVLockEpisodePlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockEpisodePlot = value
        End Set
    End Property

    Public Property TVLockEpisodeRating() As Boolean
        Get
            Return Settings._XMLSettings.TVLockEpisodeRating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockEpisodeRating = value
        End Set
    End Property

    Public Property TVLockEpisodeRuntime() As Boolean
        Get
            Return Settings._XMLSettings.TVLockEpisodeRuntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockEpisodeRuntime = value
        End Set
    End Property

    Public Property TVLockEpisodeTitle() As Boolean
        Get
            Return Settings._XMLSettings.TVLockEpisodeTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockEpisodeTitle = value
        End Set
    End Property

    Public Property TVLockEpisodeVotes() As Boolean
        Get
            Return Settings._XMLSettings.TVLockEpisodeVotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockEpisodeVotes = value
        End Set
    End Property

    Public Property TVEpisodePosterHeight() As Integer
        Get
            Return Settings._XMLSettings.TVEpisodePosterHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVEpisodePosterHeight = value
        End Set
    End Property

    Public Property TVEpisodePosterWidth() As Integer
        Get
            Return Settings._XMLSettings.TVEpisodePosterWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVEpisodePosterWidth = value
        End Set
    End Property

    Public Property TVEpisodeProperCase() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeProperCase
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeProperCase = value
        End Set
    End Property

    Public Property TVEpisodeWatchedCol() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeWatchedCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeWatchedCol = value
        End Set
    End Property

    Public Property FileSystemExpertCleaner() As Boolean
        Get
            Return Settings._XMLSettings.FileSystemExpertCleaner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.FileSystemExpertCleaner = value
        End Set
    End Property

    Public Property TVShowEFanartsHeight() As Integer
        Get
            Return Settings._XMLSettings.TVShowEFanartsHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowEFanartsHeight = value
        End Set
    End Property

    Public Property MovieEFanartsHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieEFanartsHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieEFanartsHeight = value
        End Set
    End Property

    Public Property MovieEThumbsHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieEThumbsHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieEThumbsHeight = value
        End Set
    End Property

    Public Property MovieEThumbsLimit() As Integer
        Get
            Return Settings._XMLSettings.MovieEThumbsLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieEThumbsLimit = value
        End Set
    End Property

    Public Property TVShowEFanartsLimit() As Integer
        Get
            Return Settings._XMLSettings.TVShowEFanartsLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowEFanartsLimit = value
        End Set
    End Property

    Public Property MovieEFanartsLimit() As Integer
        Get
            Return Settings._XMLSettings.MovieEFanartsLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieEFanartsLimit = value
        End Set
    End Property

    Public Property MovieFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieFanartHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieFanartHeight = value
        End Set
    End Property

    Public Property MovieSetFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieSetFanartHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetFanartHeight = value
        End Set
    End Property

    Public Property TVShowEFanartsPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.TVShowEFanartsPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowEFanartsPrefOnly = value
        End Set
    End Property

    Public Property MovieEFanartsPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieEFanartsPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEFanartsPrefOnly = value
        End Set
    End Property

    Public Property MovieEThumbsPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieEThumbsPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEThumbsPrefOnly = value
        End Set
    End Property

    Public Property MovieFanartPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartPrefOnly = value
        End Set
    End Property

    Public Property TVShowEFanartsWidth() As Integer
        Get
            Return Settings._XMLSettings.TVShowEFanartsWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowEFanartsWidth = value
        End Set
    End Property

    Public Property MovieEFanartsWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieEFanartsWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieEFanartsWidth = value
        End Set
    End Property

    Public Property MovieEThumbsWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieEThumbsWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieEThumbsWidth = value
        End Set
    End Property

    Public Property MovieFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieFanartWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieFanartWidth = value
        End Set
    End Property

    Public Property MovieSetFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieSetFanartWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetFanartWidth = value
        End Set
    End Property

    Public Property MovieScraperTop250() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperTop250
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperTop250 = value
        End Set
    End Property

    Public Property MovieScraperCollectionID() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCollectionID
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCollectionID = value
        End Set
    End Property

    Public Property MovieScraperCollectionsAuto() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCollectionsAuto
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCollectionsAuto = value
        End Set
    End Property

    Public Property MovieScraperCountry() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCountry
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCountry = value
        End Set
    End Property

    Public Property MovieScraperCast() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCast
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCast = value
        End Set
    End Property

    Public Property MovieScraperCert() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCert
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCert = value
        End Set
    End Property

    Public Property MovieScraperMPAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperMPAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperMPAA = value
        End Set
    End Property

    Public Property MovieScraperDirector() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperDirector
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperDirector = value
        End Set
    End Property

    Public Property MovieScraperGenre() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperGenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperGenre = value
        End Set
    End Property

    Public Property MovieScraperOriginalTitle() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperOriginalTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperOriginalTitle = value
        End Set
    End Property


    Public Property MovieScraperOutline() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperOutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperOutline = value
        End Set
    End Property

    Public Property MovieScraperPlot() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperPlot = value
        End Set
    End Property


    Public Property MovieScraperRating() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperRating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperRating = value
        End Set
    End Property

    Public Property MovieScraperRelease() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperRelease
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperRelease = value
        End Set
    End Property

    Public Property MovieScraperRuntime() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperRuntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperRuntime = value
        End Set
    End Property

    Public Property MovieScraperStudio() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperStudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperStudio = value
        End Set
    End Property
    Public Property MovieScraperStudioLimit() As Integer
        Get
            Return Settings._XMLSettings.MovieScraperStudioLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieScraperStudioLimit = value
        End Set
    End Property

    Public Property MovieScraperStudioWithImgOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperStudioWithImgOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperStudioWithImgOnly = value
        End Set
    End Property

    Public Property MovieScraperTagline() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperTagline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperTagline = value
        End Set
    End Property

    Public Property MovieScraperTitle() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperTitle = value
        End Set
    End Property

    Public Property MovieScraperTrailer() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperTrailer
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperTrailer = value
        End Set
    End Property

    Public Property MovieScraperVotes() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperVotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperVotes = value
        End Set
    End Property

    Public Property MovieScraperCredits() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCredits
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCredits = value
        End Set
    End Property

    Public Property MovieScraperYear() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperYear
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperYear = value
        End Set
    End Property

    Public Property MovieFilterCustom() As List(Of String)
        Get
            Return Settings._XMLSettings.MovieFilterCustom
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.MovieFilterCustom = value
        End Set
    End Property

    Public Property GeneralFilterPanelStateMovie() As Boolean
        Get
            Return Settings._XMLSettings.GeneralFilterPanelStateMovie
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralFilterPanelStateMovie = value
        End Set
    End Property

    Public Property GeneralFilterPanelStateMovieSet() As Boolean
        Get
            Return Settings._XMLSettings.GeneralFilterPanelStateMovieSet
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralFilterPanelStateMovieSet = value
        End Set
    End Property

    Public Property GeneralFilterPanelStateShow() As Boolean
        Get
            Return Settings._XMLSettings.GeneralFilterPanelStateShow
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralFilterPanelStateShow = value
        End Set
    End Property

    Public Property MovieGeneralFlagLang() As String
        Get
            Return Settings._XMLSettings.MovieGeneralFlagLang
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieGeneralFlagLang = value
        End Set
    End Property

    Public Property MovieScraperCleanFields() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCleanFields
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCleanFields = value
        End Set
    End Property

    Public Property MovieScraperCleanPlotOutline() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCleanPlotOutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCleanPlotOutline = value
        End Set
    End Property


    Public Property GenreFilter() As String
        Get
            Return Settings._XMLSettings.GenreFilter
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GenreFilter = value
        End Set
    End Property

    Public Property MovieScraperGenreLimit() As Integer
        Get
            Return Settings._XMLSettings.MovieScraperGenreLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieScraperGenreLimit = value
        End Set
    End Property

    Public Property MovieGeneralIgnoreLastScan() As Boolean
        Get
            Return Settings._XMLSettings.MovieGeneralIgnoreLastScan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieGeneralIgnoreLastScan = value
        End Set
    End Property

    Public Property GeneralInfoPanelAnim() As Boolean
        Get
            Return Settings._XMLSettings.GeneralInfoPanelAnim
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralInfoPanelAnim = value
        End Set
    End Property

    Public Property GeneralMovieInfoPanelState() As Integer
        Get
            Return Settings._XMLSettings.GeneralMovieInfoPanelState
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralMovieInfoPanelState = value
        End Set
    End Property

    Public Property GeneralMovieSetInfoPanelState() As Integer
        Get
            Return Settings._XMLSettings.GeneralMovieSetInfoPanelState
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralMovieSetInfoPanelState = value
        End Set
    End Property

    Public Property GeneralLanguage() As String
        Get
            Return Settings._XMLSettings.GeneralLanguage
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralLanguage = value
        End Set
    End Property

    Public Property MovieLevTolerance() As Integer
        Get
            Return Settings._XMLSettings.MovieLevTolerance
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieLevTolerance = value
        End Set
    End Property
    Public Property MovieLockActors() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockActors
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockActors = value
        End Set
    End Property

    Public Property MovieLockCollectionID() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockCollectionID
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockCollectionID = value
        End Set
    End Property

    Public Property MovieLockCollections() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockCollections
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockCollections = value
        End Set
    End Property

    Public Property MovieLockCountry() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockCountry
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockCountry = value
        End Set
    End Property

    Public Property MovieLockDirector() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockDirector
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockDirector = value
        End Set
    End Property

    Public Property MovieLockGenre() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockGenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockGenre = value
        End Set
    End Property

    Public Property MovieScraperUseDetailView() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperUseDetailView
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperUseDetailView = value
        End Set
    End Property
    Public Property MovieScraperReleaseFormat() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperReleaseFormat
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperReleaseFormat = value
        End Set
    End Property

    Public Property MovieLockOutline() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockOutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockOutline = value
        End Set
    End Property

    Public Property MovieLockPlot() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockPlot = value
        End Set
    End Property


    Public Property MovieLockRating() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockRating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockRating = value
        End Set
    End Property

    Public Property MovieLockReleaseDate() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockReleaseDate
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockReleaseDate = value
        End Set
    End Property

    Public Property MovieLockLanguageV() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockLanguageV
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockLanguageV = value
        End Set
    End Property
    Public Property MovieLockLanguageA() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockLanguageA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockLanguageA = value
        End Set
    End Property

    Public Property MovieLockMPAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockMPAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockMPAA = value
        End Set
    End Property

    Public Property MovieLockCert() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockCert
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockCert = value
        End Set
    End Property

    Public Property MovieLockRuntime() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockRuntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockRuntime = value
        End Set
    End Property

    Public Property MovieLockTags() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockTags
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockTags = value
        End Set
    End Property

    Public Property MovieLockTop250() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockTop250
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockTop250 = value
        End Set
    End Property

    Public Property MovieLockVotes() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockVotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockVotes = value
        End Set
    End Property

    Public Property MovieLockCredits() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockCredits
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockCredits = value
        End Set
    End Property

    Public Property MovieLockYear() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockYear
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockYear = value
        End Set
    End Property

    Public Property MovieScraperCertFSK() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCertFSK
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCertFSK = value
        End Set
    End Property
    Public Property MovieLockStudio() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockStudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockStudio = value
        End Set
    End Property

    Public Property MovieLockTagline() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockTagline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockTagline = value
        End Set
    End Property

    Public Property MovieLockTitle() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockTitle = value
        End Set
    End Property
    Public Property MovieLockOriginalTitle() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockOriginalTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockOriginalTitle = value
        End Set
    End Property
    Public Property MovieLockTrailer() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockTrailer
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockTrailer = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker1Color() As Integer
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker1Color
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieGeneralCustomMarker1Color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker2Color() As Integer
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker2Color
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieGeneralCustomMarker2Color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker3Color() As Integer
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker3Color
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieGeneralCustomMarker3Color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker4Color() As Integer
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker4Color
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieGeneralCustomMarker4Color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker1Name() As String
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker1Name
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieGeneralCustomMarker1Name = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker2Name() As String
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker2Name
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieGeneralCustomMarker2Name = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker3Name() As String
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker3Name
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieGeneralCustomMarker3Name = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker4Name() As String
        Get
            Return Settings._XMLSettings.MovieGeneralCustomMarker4Name
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieGeneralCustomMarker4Name = value
        End Set
    End Property

    Public Property MovieGeneralMarkNew() As Boolean
        Get
            Return Settings._XMLSettings.MovieGeneralMarkNew
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieGeneralMarkNew = value
        End Set
    End Property

    Public Property MovieSetGeneralMarkNew() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetGeneralMarkNew
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetGeneralMarkNew = value
        End Set
    End Property

    Public Property TVGeneralMarkNewEpisodes() As Boolean
        Get
            Return Settings._XMLSettings.TVGeneralMarkNewEpisodes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVGeneralMarkNewEpisodes = value
        End Set
    End Property

    Public Property TVGeneralMarkNewShows() As Boolean
        Get
            Return Settings._XMLSettings.TVGeneralMarkNewShows
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVGeneralMarkNewShows = value
        End Set
    End Property

    Public Property MovieMetadataPerFileType() As List(Of MetadataPerType)
        Get
            Return Settings._XMLSettings.MovieMetadataPerFileType
        End Get
        Set(ByVal value As List(Of MetadataPerType))
            Settings._XMLSettings.MovieMetadataPerFileType = value
        End Set
    End Property

    Public Property MovieMissingBanner() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingBanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingBanner = value
        End Set
    End Property

    Public Property MovieMissingClearArt() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingClearArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingClearArt = value
        End Set
    End Property

    Public Property MovieMissingClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingClearLogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingClearLogo = value
        End Set
    End Property

    Public Property MovieMissingDiscArt() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingDiscArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingDiscArt = value
        End Set
    End Property

    Public Property MovieMissingEThumbs() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingEThumbs
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingEThumbs = value
        End Set
    End Property

    Public Property MovieMissingEFanarts() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingEFanarts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingEFanarts = value
        End Set
    End Property

    Public Property MovieMissingFanart() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingFanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingFanart = value
        End Set
    End Property

    Public Property MovieMissingLandscape() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingLandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingLandscape = value
        End Set
    End Property

    Public Property MovieMissingNFO() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingNFO
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingNFO = value
        End Set
    End Property

    Public Property MovieMissingPoster() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingPoster
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingPoster = value
        End Set
    End Property

    Public Property MovieMissingSubs() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingSubs
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingSubs = value
        End Set
    End Property

    Public Property MovieMissingTheme() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingTheme
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingTheme = value
        End Set
    End Property

    Public Property MovieMissingTrailer() As Boolean
        Get
            Return Settings._XMLSettings.MovieMissingTrailer
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMissingTrailer = value
        End Set
    End Property

    Public Property MovieBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerCol = value
        End Set
    End Property

    Public Property MovieClearArtCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearArtCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearArtCol = value
        End Set
    End Property

    Public Property MovieMoviesetCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieMoviesetCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieMoviesetCol = value
        End Set
    End Property

    Public Property MovieClearLogoCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearLogoCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearLogoCol = value
        End Set
    End Property

    Public Property MovieDiscArtCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieDiscArtCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieDiscArtCol = value
        End Set
    End Property

    Public Property MovieEFanartsCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieEFanartsCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEFanartsCol = value
        End Set
    End Property

    Public Property MovieEThumbsCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieEThumbsCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEThumbsCol = value
        End Set
    End Property

    Public Property MovieFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartCol = value
        End Set
    End Property

    Public Property MovieLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieLandscapeCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLandscapeCol = value
        End Set
    End Property

    Public Property MovieNFOCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieNFOCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNFOCol = value
        End Set
    End Property

    Public Property MoviePosterCol() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterCol = value
        End Set
    End Property

    Public Property MovieSubCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSubCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSubCol = value
        End Set
    End Property

    Public Property MovieSetBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetBannerCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetBannerCol = value
        End Set
    End Property

    Public Property MovieSetBannerPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetBannerPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetBannerPrefOnly = value
        End Set
    End Property

    Public Property MovieSetBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetBannerResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetBannerResize = value
        End Set
    End Property

    Public Property MovieSetClearArtCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClearArtCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClearArtCol = value
        End Set
    End Property

    Public Property MovieSetClearLogoCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClearLogoCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClearLogoCol = value
        End Set
    End Property

    Public Property MovieSetClickScrape() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClickScrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClickScrape = value
        End Set
    End Property

    Public Property MovieSetClickScrapeAsk() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClickScrapeAsk
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClickScrapeAsk = value
        End Set
    End Property

    Public Property MovieSetDiscArtCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetDiscArtCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetDiscArtCol = value
        End Set
    End Property

    Public Property MovieSetFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetFanartCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetFanartCol = value
        End Set
    End Property

    Public Property MovieSetFanartPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetFanartPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetFanartPrefOnly = value
        End Set
    End Property

    Public Property MovieSetFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetFanartResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetFanartResize = value
        End Set
    End Property

    Public Property MovieSetLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetLandscapeCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetLandscapeCol = value
        End Set
    End Property

    Public Property MovieSetLockPlot() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetLockPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetLockPlot = value
        End Set
    End Property

    Public Property MovieSetLockTitle() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetLockTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetLockTitle = value
        End Set
    End Property

    Public Property MovieSetNfoCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetNfoCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetNfoCol = value
        End Set
    End Property

    Public Property MovieSetPosterCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetPosterCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetPosterCol = value
        End Set
    End Property

    Public Property MovieSetPosterPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetPosterPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetPosterPrefOnly = value
        End Set
    End Property

    Public Property MovieSetPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetPosterResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetPosterResize = value
        End Set
    End Property

    Public Property MovieSetMissingBanner() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingBanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingBanner = value
        End Set
    End Property

    Public Property MovieSetMissingClearArt() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingClearArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingClearArt = value
        End Set
    End Property

    Public Property MovieSetMissingClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingClearLogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingClearLogo = value
        End Set
    End Property

    Public Property MovieSetMissingDiscArt() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingDiscArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingDiscArt = value
        End Set
    End Property

    Public Property MovieSetMissingFanart() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingFanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingFanart = value
        End Set
    End Property

    Public Property MovieSetMissingLandscape() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingLandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingLandscape = value
        End Set
    End Property

    Public Property MovieSetMissingNFO() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingNFO
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingNFO = value
        End Set
    End Property

    Public Property MovieSetMissingPoster() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetMissingPoster
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetMissingPoster = value
        End Set
    End Property

    Public Property MovieSetScraperPlot() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetScraperPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetScraperPlot = value
        End Set
    End Property

    Public Property MovieSetScraperTitle() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetScraperTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetScraperTitle = value
        End Set
    End Property

    Public Property GeneralMovieTheme() As String
        Get
            Return Settings._XMLSettings.GeneralMovieTheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralMovieTheme = value
        End Set
    End Property

    Public Property GeneralMovieSetTheme() As String
        Get
            Return Settings._XMLSettings.GeneralMovieSetTheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralMovieSetTheme = value
        End Set
    End Property

    Public Property GeneralDaemonPath() As String
        Get
            Return Settings._XMLSettings.GeneralDaemonPath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralDaemonPath = value
        End Set
    End Property

    Public Property GeneralDaemonDrive() As String
        Get
            Return Settings._XMLSettings.GeneralDaemonDrive
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralDaemonDrive = value
        End Set
    End Property

    Public Property GeneralDoubleClickScrape() As Boolean
        Get
            Return Settings._XMLSettings.GeneralDoubleClickScrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralDoubleClickScrape = value
        End Set
    End Property

    Public Property MovieThemeCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieThemeCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieThemeCol = value
        End Set
    End Property

    Public Property MovieTrailerCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerCol = value
        End Set
    End Property

    Public Property MovieTrailerDefaultSearch() As String
        Get
            Return Settings._XMLSettings.MovieTrailerDefaultSearch
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieTrailerDefaultSearch = value
        End Set
    End Property

    Public Property MovieWatchedCol() As Boolean
        Get
            Return Settings._XMLSettings.MovieWatchedCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieWatchedCol = value
        End Set
    End Property

    Public Property GeneralHideBanner() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideBanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideBanner = value
        End Set
    End Property

    Public Property GeneralHideCharacterArt() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideCharacterArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideCharacterArt = value
        End Set
    End Property

    Public Property GeneralHideClearArt() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideClearArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideClearArt = value
        End Set
    End Property

    Public Property GeneralHideClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideClearLogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideClearLogo = value
        End Set
    End Property

    Public Property GeneralHideDiscArt() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideDiscArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideDiscArt = value
        End Set
    End Property

    Public Property GeneralHideFanart() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideFanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideFanart = value
        End Set
    End Property

    Public Property GeneralHideFanartSmall() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideFanartSmall
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideFanartSmall = value
        End Set
    End Property

    Public Property GeneralHideLandscape() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHideLandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHideLandscape = value
        End Set
    End Property

    Public Property GeneralHidePoster() As Boolean
        Get
            Return Settings._XMLSettings.GeneralHidePoster
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralHidePoster = value
        End Set
    End Property

    Public Property TVEpisodeFilterCustomIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeFilterCustomIsEmpty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeFilterCustomIsEmpty = value
        End Set
    End Property

    Public Property TVEpisodeNoFilter() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeNoFilter
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeNoFilter = value
        End Set
    End Property

    Public Property MovieFilterCustomIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.MovieFilterCustomIsEmpty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFilterCustomIsEmpty = value
        End Set
    End Property

    Public Property MovieNoSaveImagesToNfo() As Boolean
        Get
            Return Settings._XMLSettings.MovieNoSaveImagesToNfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNoSaveImagesToNfo = value
        End Set
    End Property

    Public Property TVShowFilterCustomIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFilterCustomIsEmpty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFilterCustomIsEmpty = value
        End Set
    End Property

    Public Property FileSystemNoStackExts() As List(Of String)
        Get
            Return Settings._XMLSettings.FileSystemNoStackExts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.FileSystemNoStackExts = value
        End Set
    End Property

    Public Property MovieSortTokensIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.MovieSortTokensIsEmpty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSortTokensIsEmpty = value
        End Set
    End Property

    Public Property MovieSetSortTokensIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetSortTokensIsEmpty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetSortTokensIsEmpty = value
        End Set
    End Property

    Public Property TVSortTokensIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.TVSortTokensIsEmpty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSortTokensIsEmpty = value
        End Set
    End Property

    Public Property OMMDummyFormat() As Integer
        Get
            Return Settings._XMLSettings.OMMDummyFormat
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.OMMDummyFormat = value
        End Set
    End Property

    Public Property OMMDummyTagline() As String
        Get
            Return Settings._XMLSettings.OMMDummyTagline
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.OMMDummyTagline = value
        End Set
    End Property

    Public Property OMMDummyTop() As String
        Get
            Return Settings._XMLSettings.OMMDummyTop
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.OMMDummyTop = value
        End Set
    End Property

    Public Property OMMDummyUseBackground() As Boolean
        Get
            Return Settings._XMLSettings.OMMDummyUseBackground
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.OMMDummyUseBackground = value
        End Set
    End Property

    Public Property OMMDummyUseFanart() As Boolean
        Get
            Return Settings._XMLSettings.OMMDummyUseFanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.OMMDummyUseFanart = value
        End Set
    End Property

    Public Property OMMDummyUseOverlay() As Boolean
        Get
            Return Settings._XMLSettings.OMMDummyUseOverlay
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.OMMDummyUseOverlay = value
        End Set
    End Property

    Public Property OMMMediaStubTagline() As String
        Get
            Return Settings._XMLSettings.OMMMediaStubTagline
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.OMMMediaStubTagline = value
        End Set
    End Property

    Public Property MovieScraperCertOnlyValue() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCertOnlyValue
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCertOnlyValue = value
        End Set
    End Property

    Public Property MovieScraperOutlineForPlot() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperOutlineForPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperOutlineForPlot = value
        End Set
    End Property

    Public Property MovieScraperOutlinePlotEnglishOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperOutlinePlotEnglishOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperOutlinePlotEnglishOverwrite = value
        End Set
    End Property

    Public Property TVASBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVASBannerOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASBannerOverwrite = value
        End Set
    End Property

    Public Property TVASFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVASFanartOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASFanartOverwrite = value
        End Set
    End Property

    Public Property TVASLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVASLandscapeOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASLandscapeOverwrite = value
        End Set
    End Property

    Public Property TVASPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVASPosterOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASPosterOverwrite = value
        End Set
    End Property

    Public Property TVEpisodeFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeFanartOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeFanartOverwrite = value
        End Set
    End Property

    Public Property TVEpisodePosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodePosterOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodePosterOverwrite = value
        End Set
    End Property

    Public Property TVShowEFanartsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowEFanartsOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowEFanartsOverwrite = value
        End Set
    End Property

    Public Property MovieEFanartsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieEFanartsOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEFanartsOverwrite = value
        End Set
    End Property

    Public Property MovieEThumbsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieEThumbsOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEThumbsOverwrite = value
        End Set
    End Property

    Public Property MovieFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartOverwrite = value
        End Set
    End Property

    Public Property GeneralOverwriteNfo() As Boolean
        Get
            Return Settings._XMLSettings.GeneralOverwriteNfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralOverwriteNfo = value
        End Set
    End Property

    Public Property MoviePosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterOverwrite = value
        End Set
    End Property

    Public Property TVSeasonBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonBannerOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonBannerOverwrite = value
        End Set
    End Property

    Public Property TVShowCharacterArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowCharacterArtOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowCharacterArtOverwrite = value
        End Set
    End Property

    Public Property TVShowClearArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClearArtOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClearArtOverwrite = value
        End Set
    End Property

    Public Property TVShowClearLogoOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClearLogoOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClearLogoOverwrite = value
        End Set
    End Property

    Public Property TVSeasonLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonLandscapeCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonLandscapeCol = value
        End Set
    End Property

    Public Property TVSeasonLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonLandscapeOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonLandscapeOverwrite = value
        End Set
    End Property

    Public Property TVShowLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowLandscapeOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowLandscapeOverwrite = value
        End Set
    End Property

    Public Property TVSeasonFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonFanartOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonFanartOverwrite = value
        End Set
    End Property

    Public Property TVSeasonPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonPosterOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonPosterOverwrite = value
        End Set
    End Property

    Public Property TVShowBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowBannerOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowBannerOverwrite = value
        End Set
    End Property

    Public Property TVShowFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFanartOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFanartOverwrite = value
        End Set
    End Property

    Public Property TVShowPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.TVShowPosterOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowPosterOverwrite = value
        End Set
    End Property

    Public Property MovieBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerOverwrite = value
        End Set
    End Property

    Public Property MovieDiscArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieDiscArtOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieDiscArtOverwrite = value
        End Set
    End Property

    Public Property MovieLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieLandscapeOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLandscapeOverwrite = value
        End Set
    End Property

    Public Property MovieClearArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearArtOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearArtOverwrite = value
        End Set
    End Property

    Public Property MovieClearLogoOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearLogoOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearLogoOverwrite = value
        End Set
    End Property

    Public Property MovieSetBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetBannerOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetBannerOverwrite = value
        End Set
    End Property

    Public Property MovieSetClearArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClearArtOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClearArtOverwrite = value
        End Set
    End Property

    Public Property MovieSetClearLogoOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClearLogoOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClearLogoOverwrite = value
        End Set
    End Property

    Public Property MovieSetDiscArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetDiscArtOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetDiscArtOverwrite = value
        End Set
    End Property

    Public Property MovieSetFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetFanartOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetFanartOverwrite = value
        End Set
    End Property

    Public Property MovieSetLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetLandscapeOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetLandscapeOverwrite = value
        End Set
    End Property

    Public Property MovieSetPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetPosterOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetPosterOverwrite = value
        End Set
    End Property

    Public Property MovieBannerPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerPrefOnly = value
        End Set
    End Property

    Public Property MovieBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerResize = value
        End Set
    End Property

    Public Property MovieTrailerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerOverwrite = value
        End Set
    End Property

    Public Property MovieThemeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.MovieThemeOverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieThemeOverwrite = value
        End Set
    End Property
    Public Property MovieScraperPlotForOutline() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperPlotForOutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperPlotForOutline = value
        End Set
    End Property

    Public Property MovieScraperOutlineLimit() As Integer
        Get
            Return Settings._XMLSettings.MovieScraperOutlineLimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieScraperOutlineLimit = value
        End Set
    End Property

    Public Property GeneralImagesGlassOverlay() As Boolean
        Get
            Return Settings._XMLSettings.GeneralImagesGlassOverlay
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralImagesGlassOverlay = value
        End Set
    End Property

    Public Property MoviePosterHeight() As Integer
        Get
            Return Settings._XMLSettings.MoviePosterHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MoviePosterHeight = value
        End Set
    End Property

    Public Property MovieSetPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieSetPosterHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetPosterHeight = value
        End Set
    End Property

    Public Property MoviePosterPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterPrefOnly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterPrefOnly = value
        End Set
    End Property

    Public Property MoviePosterWidth() As Integer
        Get
            Return Settings._XMLSettings.MoviePosterWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MoviePosterWidth = value
        End Set
    End Property

    Public Property MovieSetPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieSetPosterWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetPosterWidth = value
        End Set
    End Property

    Public Property TVASPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Settings._XMLSettings.TVASPosterPrefSize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Settings._XMLSettings.TVASPosterPrefSize = value
        End Set
    End Property

    Public Property TVEpisodeFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.TVEpisodeFanartPrefSize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.TVEpisodeFanartPrefSize = value
        End Set
    End Property

    Public Property MovieFanartPrefSize() As Enums.FanartSize
        Get
            Return Settings._XMLSettings.MovieFanartPrefSize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.MovieFanartPrefSize = value
        End Set
    End Property

    Public Property MovieSetFanartPrefSize() As Enums.FanartSize
        Get
            Return Settings._XMLSettings.MovieSetFanartPrefSize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.MovieSetFanartPrefSize = value
        End Set
    End Property

    Public Property MovieEFanartsPrefSize() As Enums.FanartSize
        Get
            Return Settings._XMLSettings.MovieEFanartsPrefSize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.MovieEFanartsPrefSize = value
        End Set
    End Property

    Public Property MovieEThumbsPrefSize() As Enums.FanartSize
        Get
            Return Settings._XMLSettings.MovieEThumbsPrefSize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.MovieEThumbsPrefSize = value
        End Set
    End Property

    Public Property MoviePosterPrefSize() As Enums.PosterSize
        Get
            Return Settings._XMLSettings.MoviePosterPrefSize
        End Get
        Set(ByVal value As Enums.PosterSize)
            Settings._XMLSettings.MoviePosterPrefSize = value
        End Set
    End Property

    Public Property MovieSetPosterPrefSize() As Enums.PosterSize
        Get
            Return Settings._XMLSettings.MovieSetPosterPrefSize
        End Get
        Set(ByVal value As Enums.PosterSize)
            Settings._XMLSettings.MovieSetPosterPrefSize = value
        End Set
    End Property

    Public Property TVSeasonFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.TVSeasonFanartPrefSize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.TVSeasonFanartPrefSize = value
        End Set
    End Property

    Public Property TVASFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.TVASFanartPrefSize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.TVASFanartPrefSize = value
        End Set
    End Property

    Public Property TVEpisodePosterPrefSize() As Enums.TVEpisodePosterSize
        Get
            Return Settings._XMLSettings.TVEpisodePosterPrefSize
        End Get
        Set(ByVal value As Enums.TVEpisodePosterSize)
            Settings._XMLSettings.TVEpisodePosterPrefSize = value
        End Set
    End Property

    Public Property TVSeasonPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Settings._XMLSettings.TVSeasonPosterPrefSize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Settings._XMLSettings.TVSeasonPosterPrefSize = value
        End Set
    End Property

    Public Property TVShowBannerPrefType() As Enums.TVShowBannerType
        Get
            Return Settings._XMLSettings.TVShowBannerPrefType
        End Get
        Set(ByVal value As Enums.TVShowBannerType)
            Settings._XMLSettings.TVShowBannerPrefType = value
        End Set
    End Property

    Public Property MovieBannerPrefType() As Enums.MovieBannerType
        Get
            Return Settings._XMLSettings.MovieBannerPrefType
        End Get
        Set(ByVal value As Enums.MovieBannerType)
            Settings._XMLSettings.MovieBannerPrefType = value
        End Set
    End Property

    Public Property MovieSetBannerPrefType() As Enums.MovieBannerType
        Get
            Return Settings._XMLSettings.MovieSetBannerPrefType
        End Get
        Set(ByVal value As Enums.MovieBannerType)
            Settings._XMLSettings.MovieSetBannerPrefType = value
        End Set
    End Property

    Public Property TVASBannerPrefType() As Enums.TVShowBannerType
        Get
            Return Settings._XMLSettings.TVASBannerPrefType
        End Get
        Set(ByVal value As Enums.TVShowBannerType)
            Settings._XMLSettings.TVASBannerPrefType = value
        End Set
    End Property

    Public Property TVSeasonBannerPrefType() As Enums.TVSeasonBannerType
        Get
            Return Settings._XMLSettings.TVSeasonBannerPrefType
        End Get
        Set(ByVal value As Enums.TVSeasonBannerType)
            Settings._XMLSettings.TVSeasonBannerPrefType = value
        End Set
    End Property

    Public Property TVShowEFanartsPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.TVShowEFanartsPrefSize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.TVShowEFanartsPrefSize = value
        End Set
    End Property

    Public Property TVShowFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.TVShowFanartPrefSize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.TVShowFanartPrefSize = value
        End Set
    End Property

    Public Property TVShowPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Settings._XMLSettings.TVShowPosterPrefSize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Settings._XMLSettings.TVShowPosterPrefSize = value
        End Set
    End Property

    Public Property MovieTrailerMinQual() As Enums.TrailerQuality
        Get
            Return Settings._XMLSettings.MovieTrailerMinQual
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            Settings._XMLSettings.MovieTrailerMinQual = value
        End Set
    End Property

    Public Property MovieTrailerPrefQual() As Enums.TrailerQuality
        Get
            Return Settings._XMLSettings.MovieTrailerPrefQual
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            Settings._XMLSettings.MovieTrailerPrefQual = value
        End Set
    End Property

    Public Property MovieProperCase() As Boolean
        Get
            Return Settings._XMLSettings.MovieProperCase
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieProperCase = value
        End Set
    End Property

    Public Property ProxyCreds() As NetworkCredential
        Get
            Return Settings._XMLSettings.ProxyCredentials
        End Get
        Set(ByVal value As NetworkCredential)
            Settings._XMLSettings.ProxyCredentials = value
        End Set
    End Property

    Public Property ProxyPort() As Integer
        Get
            Return Settings._XMLSettings.ProxyPort
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.ProxyPort = value
        End Set
    End Property

    Public Property ProxyURI() As String
        Get
            Return Settings._XMLSettings.ProxyURI
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.ProxyURI = value
        End Set
    End Property

    Public Property TVASBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.TVASBannerResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASBannerResize = value
        End Set
    End Property

    Public Property TVASPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.TVASPosterResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASPosterResize = value
        End Set
    End Property

    Public Property TVASFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.TVASFanartResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVASFanartResize = value
        End Set
    End Property

    Public Property TVEpisodeFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeFanartResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeFanartResize = value
        End Set
    End Property

    Public Property TVEpisodePosterResize() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodePosterResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodePosterResize = value
        End Set
    End Property

    Public Property TVShowEFanartsResize() As Boolean
        Get
            Return Settings._XMLSettings.TVShowEFanartsResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowEFanartsResize = value
        End Set
    End Property

    Public Property MovieEFanartsResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieEFanartsResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEFanartsResize = value
        End Set
    End Property

    Public Property MovieEThumbsResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieEThumbsResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieEThumbsResize = value
        End Set
    End Property

    Public Property MovieFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartResize = value
        End Set
    End Property

    Public Property MoviePosterResize() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterResize = value
        End Set
    End Property

    Public Property TVSeasonBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonBannerResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonBannerResize = value
        End Set
    End Property

    Public Property TVSeasonFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonFanartResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonFanartResize = value
        End Set
    End Property

    Public Property TVSeasonPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonPosterResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonPosterResize = value
        End Set
    End Property

    Public Property TVShowBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.TVShowBannerResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowBannerResize = value
        End Set
    End Property

    Public Property TVShowFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFanartResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFanartResize = value
        End Set
    End Property

    Public Property TVShowPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.TVShowPosterResize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowPosterResize = value
        End Set
    End Property

    Public Property MovieScraperDurationRuntimeFormat() As String
        Get
            Return Settings._XMLSettings.MovieScraperDurationRuntimeFormat
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieScraperDurationRuntimeFormat = value
        End Set
    End Property

    Public Property TVScraperDurationRuntimeFormat() As String
        Get
            Return Settings._XMLSettings.TVScraperDurationRuntimeFormat
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TVScraperDurationRuntimeFormat = value
        End Set
    End Property

    Public Property MovieScraperMetaDataScan() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperMetaDataScan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperMetaDataScan = value
        End Set
    End Property

    Public Property MovieScanOrderModify() As Boolean
        Get
            Return Settings._XMLSettings.MovieScanOrderModify
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScanOrderModify = value
        End Set
    End Property

    Public Property TVScraperMetaDataScan() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperMetaDataScan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperMetaDataScan = value
        End Set
    End Property

    Public Property TVScraperEpisodeActors() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeActors
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeActors = value
        End Set
    End Property

    Public Property TVScraperEpisodeAired() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeAired
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeAired = value
        End Set
    End Property

    Public Property TVScraperEpisodeCredits() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeCredits
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeCredits = value
        End Set
    End Property

    Public Property TVScraperEpisodeDirector() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeDirector
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeDirector = value
        End Set
    End Property

    Public Property TVScraperEpisodeEpisode() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeEpisode
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeEpisode = value
        End Set
    End Property

    Public Property TVScraperEpisodePlot() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodePlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodePlot = value
        End Set
    End Property

    Public Property TVScraperEpisodeRating() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeRating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeRating = value
        End Set
    End Property

    Public Property TVScraperEpisodeRuntime() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeRuntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeRuntime = value
        End Set
    End Property

    Public Property TVScraperEpisodeSeason() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeSeason
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeSeason = value
        End Set
    End Property

    Public Property TVScraperEpisodeTitle() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeTitle = value
        End Set
    End Property

    Public Property TVScraperEpisodeVotes() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperEpisodeVotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperEpisodeVotes = value
        End Set
    End Property

    Public Property TVScraperShowActors() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowActors
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowActors = value
        End Set
    End Property

    Public Property TVScraperShowEpiGuideURL() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowEpiGuideURL
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowEpiGuideURL = value
        End Set
    End Property

    Public Property TVScraperShowGenre() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowGenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowGenre = value
        End Set
    End Property

    Public Property TVScraperShowMPAA() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowMPAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowMPAA = value
        End Set
    End Property

    Public Property TVScraperShowPlot() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowPlot = value
        End Set
    End Property

    Public Property TVScraperShowPremiered() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowPremiered
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowPremiered = value
        End Set
    End Property

    Public Property TVScraperShowRating() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowRating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowRating = value
        End Set
    End Property

    Public Property TVScraperShowRuntime() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowRuntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowRuntime = value
        End Set
    End Property

    Public Property TVScraperShowStatus() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowStatus
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowStatus = value
        End Set
    End Property

    Public Property TVScraperShowStudio() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowStudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowStudio = value
        End Set
    End Property

    Public Property TVScraperShowTitle() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowTitle = value
        End Set
    End Property

    Public Property TVScraperShowVotes() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperShowVotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperShowVotes = value
        End Set
    End Property

    Public Property TVSeasonFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonFanartCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonFanartCol = value
        End Set
    End Property

    Public Property TVSeasonFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.TVSeasonFanartHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSeasonFanartHeight = value
        End Set
    End Property

    Public Property TVSeasonFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.TVSeasonFanartWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSeasonFanartWidth = value
        End Set
    End Property

    Public Property TVSeasonBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonBannerCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonBannerCol = value
        End Set
    End Property

    Public Property TVASBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.TVASBannerWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVASBannerWidth = value
        End Set
    End Property

    Public Property TVSeasonBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.TVSeasonBannerWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSeasonBannerWidth = value
        End Set
    End Property

    Public Property TVASFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.TVASFanartWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVASFanartWidth = value
        End Set
    End Property

    Public Property TVShowBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.TVShowBannerWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowBannerWidth = value
        End Set
    End Property

    Public Property MovieBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieBannerWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieBannerWidth = value
        End Set
    End Property

    Public Property MovieSetBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.MovieSetBannerWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetBannerWidth = value
        End Set
    End Property

    Public Property TVASBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.TVASBannerHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVASBannerHeight = value
        End Set
    End Property

    Public Property TVSeasonBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.TVSeasonBannerHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSeasonBannerHeight = value
        End Set
    End Property

    Public Property TVASFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.TVASFanartHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVASFanartHeight = value
        End Set
    End Property

    Public Property TVShowBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.TVShowBannerHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowBannerHeight = value
        End Set
    End Property

    Public Property MovieBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieBannerHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieBannerHeight = value
        End Set
    End Property

    Public Property MovieSetBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.MovieSetBannerHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetBannerHeight = value
        End Set
    End Property

    Public Property TVSeasonPosterCol() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonPosterCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonPosterCol = value
        End Set
    End Property

    Public Property TVSeasonPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.TVSeasonPosterHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSeasonPosterHeight = value
        End Set
    End Property

    Public Property TVSeasonPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.TVSeasonPosterWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSeasonPosterWidth = value
        End Set
    End Property

    Public Property MovieSets() As List(Of String)
        Get
            Return Settings._XMLSettings.MovieSets
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.MovieSets = value
        End Set
    End Property

    Public Property GeneralShowImgDims() As Boolean
        Get
            Return Settings._XMLSettings.GeneralShowImgDims
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralShowImgDims = value
        End Set
    End Property

    Public Property GeneralShowImgNames() As Boolean
        Get
            Return Settings._XMLSettings.GeneralShowImgNames
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralShowImgNames = value
        End Set
    End Property

    Public Property TVShowFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFanartCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFanartCol = value
        End Set
    End Property

    Public Property TVShowFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.TVShowFanartHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowFanartHeight = value
        End Set
    End Property

    Public Property TVShowFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.TVShowFanartWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowFanartWidth = value
        End Set
    End Property

    Public Property TVShowFilterCustom() As List(Of String)
        Get
            Return Settings._XMLSettings.TVShowFilterCustom
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.TVShowFilterCustom = value
        End Set
    End Property

    Public Property GeneralTVShowInfoPanelState() As Integer
        Get
            Return Settings._XMLSettings.GeneralTVShowInfoPanelState
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralTVShowInfoPanelState = value
        End Set
    End Property

    Public Property TVLockShowGenre() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowGenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowGenre = value
        End Set
    End Property

    Public Property TVLockShowPlot() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowPlot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowPlot = value
        End Set
    End Property

    Public Property TVLockShowRating() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowRating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowRating = value
        End Set
    End Property

    Public Property TVLockShowRuntime() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowRuntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowRuntime = value
        End Set
    End Property

    Public Property TVLockShowStatus() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowStatus
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowStatus = value
        End Set
    End Property

    Public Property TVLockShowStudio() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowStudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowStudio = value
        End Set
    End Property

    Public Property TVLockShowTitle() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowTitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowTitle = value
        End Set
    End Property

    Public Property TVLockShowVotes() As Boolean
        Get
            Return Settings._XMLSettings.TVLockShowVotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVLockShowVotes = value
        End Set
    End Property

    Public Property TVShowBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowBannerCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowBannerCol = value
        End Set
    End Property

    Public Property TVShowCharacterArtCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowCharacterArtCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowCharacterArtCol = value
        End Set
    End Property

    Public Property TVShowClearArtCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClearArtCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClearArtCol = value
        End Set
    End Property

    Public Property TVShowClearLogoCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClearLogoCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClearLogoCol = value
        End Set
    End Property

    Public Property TVShowEFanartsCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowEFanartsCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowEFanartsCol = value
        End Set
    End Property

    Public Property TVShowLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowLandscapeCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowLandscapeCol = value
        End Set
    End Property

    Public Property TVShowThemeCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowThemeCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowThemeCol = value
        End Set
    End Property

    Public Property TVShowNfoCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowNfoCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowNfoCol = value
        End Set
    End Property

    Public Property TVShowPosterCol() As Boolean
        Get
            Return Settings._XMLSettings.TVShowPosterCol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowPosterCol = value
        End Set
    End Property

    Public Property TVShowPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.TVShowPosterHeight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowPosterHeight = value
        End Set
    End Property

    Public Property TVShowPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.TVShowPosterWidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVShowPosterWidth = value
        End Set
    End Property

    Public Property TVShowProperCase() As Boolean
        Get
            Return Settings._XMLSettings.TVShowProperCase
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowProperCase = value
        End Set
    End Property

    Public Property TVScraperRatingRegion() As String
        Get
            Return Settings._XMLSettings.TVScraperRatingRegion
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TVScraperRatingRegion = value
        End Set
    End Property

    Public Property MovieSkipLessThan() As Integer
        Get
            Return Settings._XMLSettings.MovieSkipLessThan
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSkipLessThan = value
        End Set
    End Property

    Public Property MovieSkipStackedSizeCheck() As Boolean
        Get
            Return Settings._XMLSettings.MovieSkipStackedSizeCheck
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSkipStackedSizeCheck = value
        End Set
    End Property

    Public Property TVSkipLessThan() As Integer
        Get
            Return Settings._XMLSettings.TVSkipLessThan
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.TVSkipLessThan = value
        End Set
    End Property

    Public Property MovieSortBeforeScan() As Boolean
        Get
            Return Settings._XMLSettings.MovieSortBeforeScan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSortBeforeScan = value
        End Set
    End Property

    Public Property SortPath() As String
        Get
            Return Settings._XMLSettings.SortPath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.SortPath = value
        End Set
    End Property

    Public Property MovieSortTokens() As List(Of String)
        Get
            Return Settings._XMLSettings.MovieSortTokens
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.MovieSortTokens = value
        End Set
    End Property

    Public Property MovieSetSortTokens() As List(Of String)
        Get
            Return Settings._XMLSettings.MovieSetSortTokens
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.MovieSetSortTokens = value
        End Set
    End Property

    Public Property TVSortTokens() As List(Of String)
        Get
            Return Settings._XMLSettings.TVSortTokens
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.TVSortTokens = value
        End Set
    End Property

    Public Property GeneralSourceFromFolder() As Boolean
        Get
            Return Settings._XMLSettings.GeneralSourceFromFolder
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralSourceFromFolder = value
        End Set
    End Property

    Public Property GeneralMainFilterSortColumn_Movies() As Integer
        Get
            Return Settings._XMLSettings.GeneralMainFilterSortColumn_Movies
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralMainFilterSortColumn_Movies = value
        End Set
    End Property

    Public Property GeneralMainFilterSortOrder_Movies() As Integer
        Get
            Return Settings._XMLSettings.GeneralMainFilterSortOrder_Movies
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralMainFilterSortOrder_Movies = value
        End Set
    End Property

    Public Property GeneralMainSplitterPanelState() As Integer
        Get
            Return Settings._XMLSettings.GeneralMainSplitterPanelState
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralMainSplitterPanelState = value
        End Set
    End Property

    Public Property GeneralShowSplitterPanelState() As Integer
        Get
            Return Settings._XMLSettings.GeneralShowSplitterPanelState
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralShowSplitterPanelState = value
        End Set
    End Property

    Public Property GeneralSeasonSplitterPanelState() As Integer
        Get
            Return Settings._XMLSettings.GeneralSeasonSplitterPanelState
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.GeneralSeasonSplitterPanelState = value
        End Set
    End Property

    Public Property TVCleanDB() As Boolean
        Get
            Return Settings._XMLSettings.TVCleanDB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVCleanDB = value
        End Set
    End Property

    Public Property GeneralTVEpisodeTheme() As String
        Get
            Return Settings._XMLSettings.GeneralTVEpisodeTheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralTVEpisodeTheme = value
        End Set
    End Property

    Public Property TVGeneralFlagLang() As String
        Get
            Return Settings._XMLSettings.TVGeneralFlagLang
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TVGeneralFlagLang = value
        End Set
    End Property

    Public Property TVGeneralIgnoreLastScan() As Boolean
        Get
            Return Settings._XMLSettings.TVGeneralIgnoreLastScan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVGeneralIgnoreLastScan = value
        End Set
    End Property

    Public Property TVMetadataPerFileType() As List(Of MetadataPerType)
        Get
            Return Settings._XMLSettings.TVMetadataPerFileType
        End Get
        Set(ByVal value As List(Of MetadataPerType))
            Settings._XMLSettings.TVMetadataPerFileType = value
        End Set
    End Property

    Public Property TVScanOrderModify() As Boolean
        Get
            Return Settings._XMLSettings.TVScanOrderModify
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScanOrderModify = value
        End Set
    End Property

    Public Property TVShowRegexes() As List(Of TVShowRegEx)
        Get
            Return Settings._XMLSettings.TVShowRegexes
        End Get
        Set(ByVal value As List(Of TVShowRegEx))
            Settings._XMLSettings.TVShowRegexes = value
        End Set
    End Property

    Public Property GeneralTVShowTheme() As String
        Get
            Return Settings._XMLSettings.GeneralTVShowTheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralTVShowTheme = value
        End Set
    End Property

    Public Property TVScraperUpdateTime() As Enums.TVScraperUpdateTime
        Get
            Return Settings._XMLSettings.TVScraperUpdateTime
        End Get
        Set(ByVal value As Enums.TVScraperUpdateTime)
            Settings._XMLSettings.TVScraperUpdateTime = value
        End Set
    End Property

    Public Property MovieTrailerEnable() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerEnable
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerEnable = value
        End Set
    End Property

    Public Property MovieThemeEnable() As Boolean
        Get
            Return Settings._XMLSettings.MovieThemeEnable
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieThemeEnable = value
        End Set
    End Property

    Public Property MovieScraperCertForMPAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCertForMPAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCertForMPAA = value
        End Set
    End Property

    Public Property MovieScraperCertForMPAAFallback() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperCertForMPAAFallback
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperCertForMPAAFallback = value
        End Set
    End Property

    Public Property MovieScraperUseMDDuration() As Boolean
        Get
            Return Settings._XMLSettings.MovieScraperUseMDDuration
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieScraperUseMDDuration = value
        End Set
    End Property

    Public Property TVScraperUseMDDuration() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperUseMDDuration
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperUseMDDuration = value
        End Set
    End Property

    Public Property TVScraperUseSRuntimeForEp() As Boolean
        Get
            Return Settings._XMLSettings.TVScraperUseSRuntimeForEp
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVScraperUseSRuntimeForEp = value
        End Set
    End Property

    Public Property FileSystemValidExts() As List(Of String)
        Get
            Return Settings._XMLSettings.FileSystemValidExts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.FileSystemValidExts = value
        End Set
    End Property

    Public Property FileSystemValidSubtitlesExts() As List(Of String)
        Get
            Return Settings._XMLSettings.FileSystemValidSubtitlesExts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.FileSystemValidSubtitlesExts = value
        End Set
    End Property

    Public Property FileSystemValidThemeExts() As List(Of String)
        Get
            Return Settings._XMLSettings.FileSystemValidThemeExts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.FileSystemValidThemeExts = value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return Settings._XMLSettings.Version
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.Version = value
        End Set
    End Property

    Public Property GeneralWindowLoc() As Point
        Get
            Return Settings._XMLSettings.GeneralWindowLoc
        End Get
        Set(ByVal value As Point)
            Settings._XMLSettings.GeneralWindowLoc = value
        End Set
    End Property

    Public Property GeneralWindowSize() As Size
        Get
            Return Settings._XMLSettings.GeneralWindowSize
        End Get
        Set(ByVal value As Size)
            Settings._XMLSettings.GeneralWindowSize = value
        End Set
    End Property

    Public Property GeneralWindowState() As FormWindowState
        Get
            Return Settings._XMLSettings.GeneralWindowState
        End Get
        Set(ByVal value As FormWindowState)
            Settings._XMLSettings.GeneralWindowState = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return Settings._XMLSettings.Username
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.Username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Settings._XMLSettings.Password
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.Password = value
        End Set
    End Property

    Public Property TraktUsername() As String
        Get
            Return Settings._XMLSettings.TraktUsername
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TraktUsername = value
        End Set
    End Property

    Public Property TraktPassword() As String
        Get
            Return Settings._XMLSettings.TraktPassword
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TraktPassword = value
        End Set
    End Property

    Public Property UseTrakt() As Boolean
        Get
            Return Settings._XMLSettings.UseTrakt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.UseTrakt = value
        End Set
    End Property

    Public Property GeneralDateTime() As Enums.DateTime
        Get
            Return Settings._XMLSettings.GeneralDateTime
        End Get
        Set(ByVal value As Enums.DateTime)
            Settings._XMLSettings.GeneralDateTime = value
        End Set
    End Property

    Public Property GeneralDateAddedIgnoreNFO() As Boolean
        Get
            Return Settings._XMLSettings.GeneralDateAddedIgnoreNFO
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.GeneralDateAddedIgnoreNFO = value
        End Set
    End Property

    Public Property MovieUseFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseFrodo = value
        End Set
    End Property

    Public Property MovieActorThumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsFrodo = value
        End Set
    End Property

    Public Property MovieBannerAD() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerAD
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerAD = value
        End Set
    End Property

    Public Property MovieClearArtAD() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearArtAD
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearArtAD = value
        End Set
    End Property

    Public Property MovieClearLogoAD() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearLogoAD
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearLogoAD = value
        End Set
    End Property

    Public Property MovieDiscArtAD() As Boolean
        Get
            Return Settings._XMLSettings.MovieDiscArtAD
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieDiscArtAD = value
        End Set
    End Property

    Public Property MovieExtrafanartsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrafanartsFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrafanartsFrodo = value
        End Set
    End Property

    Public Property MovieExtrathumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrathumbsFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrathumbsFrodo = value
        End Set
    End Property

    Public Property MovieFanartFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartFrodo = value
        End Set
    End Property

    Public Property MovieLandscapeAD() As Boolean
        Get
            Return Settings._XMLSettings.MovieLandscapeAD
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLandscapeAD = value
        End Set
    End Property

    Public Property MovieNFOFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieNFOFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNFOFrodo = value
        End Set
    End Property

    Public Property MoviePosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterFrodo = value
        End Set
    End Property

    Public Property MovieTrailerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerFrodo = value
        End Set
    End Property

    Public Property MovieUseEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseEden = value
        End Set
    End Property

    Public Property MovieActorThumbsEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsEden = value
        End Set
    End Property

    Public Property MovieBannerEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerEden = value
        End Set
    End Property

    Public Property MovieClearArtEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearArtEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearArtEden = value
        End Set
    End Property

    Public Property MovieClearLogoEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieClearLogoEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieClearLogoEden = value
        End Set
    End Property

    Public Property MovieDiscArtEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieDiscArtEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieDiscArtEden = value
        End Set
    End Property

    Public Property MovieExtrafanartsEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrafanartsEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrafanartsEden = value
        End Set
    End Property

    Public Property MovieExtrathumbsEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrathumbsEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrathumbsEden = value
        End Set
    End Property

    Public Property MovieFanartEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartEden = value
        End Set
    End Property

    Public Property MovieLandscapeEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieLandscapeEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLandscapeEden = value
        End Set
    End Property

    Public Property MovieNFOEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieNFOEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNFOEden = value
        End Set
    End Property

    Public Property MoviePosterEden() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterEden = value
        End Set
    End Property

    Public Property MovieTrailerEden() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerEden = value
        End Set
    End Property

    Public Property MovieXBMCThemeEnable() As Boolean
        Get
            Return Settings._XMLSettings.MovieXBMCThemeEnable
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieXBMCThemeEnable = value
        End Set
    End Property

    Public Property MovieXBMCThemeCustom() As Boolean
        Get
            Return Settings._XMLSettings.MovieXBMCThemeCustom
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieXBMCThemeCustom = value
        End Set
    End Property

    Public Property MovieXBMCThemeMovie() As Boolean
        Get
            Return Settings._XMLSettings.MovieXBMCThemeMovie
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieXBMCThemeMovie = value
        End Set
    End Property

    Public Property MovieXBMCThemeSub() As Boolean
        Get
            Return Settings._XMLSettings.MovieXBMCThemeSub
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieXBMCThemeSub = value
        End Set
    End Property

    Public Property MovieXBMCThemeCustomPath() As String
        Get
            Return Settings._XMLSettings.MovieXBMCThemeCustomPath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieXBMCThemeCustomPath = value
        End Set
    End Property

    Public Property MovieXBMCThemeSubDir() As String
        Get
            Return Settings._XMLSettings.MovieXBMCThemeSubDir
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieXBMCThemeSubDir = value
        End Set
    End Property

    Public Property MovieXBMCTrailerFormat() As Boolean
        Get
            Return Settings._XMLSettings.MovieXBMCTrailerFormat
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieXBMCTrailerFormat = value
        End Set
    End Property

    Public Property MovieXBMCProtectVTSBDMV() As Boolean
        Get
            Return Settings._XMLSettings.MovieXBMCProtectVTSBDMV
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieXBMCProtectVTSBDMV = value
        End Set
    End Property

    Public Property MovieUseYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseYAMJ = value
        End Set
    End Property

    Public Property MovieBannerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerYAMJ = value
        End Set
    End Property

    Public Property MovieFanartYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartYAMJ = value
        End Set
    End Property

    Public Property MovieNFOYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieNFOYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNFOYAMJ = value
        End Set
    End Property

    Public Property MoviePosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterYAMJ = value
        End Set
    End Property

    Public Property MovieTrailerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerYAMJ = value
        End Set
    End Property

    Public Property MovieYAMJCompatibleSets() As Boolean
        Get
            Return Settings._XMLSettings.MovieYAMJCompatibleSets
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieYAMJCompatibleSets = value
        End Set
    End Property

    Public Property MovieYAMJWatchedFile() As Boolean
        Get
            Return Settings._XMLSettings.MovieYAMJWatchedFile
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieYAMJWatchedFile = value
        End Set
    End Property

    Public Property MovieYAMJWatchedFolder() As String
        Get
            Return Settings._XMLSettings.MovieYAMJWatchedFolder
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieYAMJWatchedFolder = value
        End Set
    End Property

    Public Property MovieUseNMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseNMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseNMJ = value
        End Set
    End Property

    Public Property MovieBannerNMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieBannerNMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieBannerNMJ = value
        End Set
    End Property

    Public Property MovieFanartNMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartNMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartNMJ = value
        End Set
    End Property

    Public Property MovieNFONMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieNFONMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNFONMJ = value
        End Set
    End Property

    Public Property MoviePosterNMJ() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterNMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterNMJ = value
        End Set
    End Property

    Public Property MovieTrailerNMJ() As Boolean
        Get
            Return Settings._XMLSettings.MovieTrailerNMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieTrailerNMJ = value
        End Set
    End Property

    Public Property MovieUseBoxee() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseBoxee = value
        End Set
    End Property

    Public Property MovieFanartBoxee() As Boolean
        Get
            Return Settings._XMLSettings.MovieFanartBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieFanartBoxee = value
        End Set
    End Property

    Public Property MovieNFOBoxee() As Boolean
        Get
            Return Settings._XMLSettings.MovieNFOBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieNFOBoxee = value
        End Set
    End Property

    Public Property MoviePosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.MoviePosterBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MoviePosterBoxee = value
        End Set
    End Property

    Public Property MovieUseExpert() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseExpert
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseExpert = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsExpertSingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsExpertSingle = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieActorThumbsExtExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieActorThumbsExtExpertSingle = value
        End Set
    End Property

    Public Property MovieBannerExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieBannerExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieBannerExpertSingle = value
        End Set
    End Property

    Public Property MovieClearArtExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieClearArtExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearArtExpertSingle = value
        End Set
    End Property

    Public Property MovieClearLogoExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieClearLogoExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearLogoExpertSingle = value
        End Set
    End Property

    Public Property MovieDiscArtExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieDiscArtExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieDiscArtExpertSingle = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrafanartsExpertSingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrafanartsExpertSingle = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrathumbsExpertSingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrathumbsExpertSingle = value
        End Set
    End Property

    Public Property MovieFanartExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieFanartExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieFanartExpertSingle = value
        End Set
    End Property

    Public Property MovieLandscapeExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieLandscapeExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieLandscapeExpertSingle = value
        End Set
    End Property

    Public Property MovieNFOExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieNFOExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieNFOExpertSingle = value
        End Set
    End Property

    Public Property MoviePosterExpertSingle() As String
        Get
            Return Settings._XMLSettings.MoviePosterExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MoviePosterExpertSingle = value
        End Set
    End Property

    Public Property MovieStackExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.MovieStackExpertSingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieStackExpertSingle = value
        End Set
    End Property

    Public Property MovieTrailerExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieTrailerExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieTrailerExpertSingle = value
        End Set
    End Property

    Public Property MovieUnstackExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.MovieUnstackExpertSingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUnstackExpertSingle = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertMulti() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsExpertMulti
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsExpertMulti = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieActorThumbsExtExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieActorThumbsExtExpertMulti = value
        End Set
    End Property

    Public Property MovieBannerExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieBannerExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieBannerExpertMulti = value
        End Set
    End Property

    Public Property MovieClearArtExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieClearArtExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearArtExpertMulti = value
        End Set
    End Property

    Public Property MovieClearLogoExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieClearLogoExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearLogoExpertMulti = value
        End Set
    End Property

    Public Property MovieDiscArtExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieDiscArtExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieDiscArtExpertMulti = value
        End Set
    End Property

    Public Property MovieFanartExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieFanartExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieFanartExpertMulti = value
        End Set
    End Property

    Public Property MovieLandscapeExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieLandscapeExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieLandscapeExpertMulti = value
        End Set
    End Property

    Public Property MovieNFOExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieNFOExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieNFOExpertMulti = value
        End Set
    End Property

    Public Property MoviePosterExpertMulti() As String
        Get
            Return Settings._XMLSettings.MoviePosterExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MoviePosterExpertMulti = value
        End Set
    End Property

    Public Property MovieStackExpertMulti() As Boolean
        Get
            Return Settings._XMLSettings.MovieStackExpertMulti
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieStackExpertMulti = value
        End Set
    End Property

    Public Property MovieTrailerExpertMulti() As String
        Get
            Return Settings._XMLSettings.MovieTrailerExpertMulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieTrailerExpertMulti = value
        End Set
    End Property

    Public Property MovieUnstackExpertMulti() As Boolean
        Get
            Return Settings._XMLSettings.MovieUnstackExpertMulti
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUnstackExpertMulti = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsExpertVTS
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsExpertVTS = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieActorThumbsExtExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieActorThumbsExtExpertVTS = value
        End Set
    End Property

    Public Property MovieBannerExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieBannerExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieBannerExpertVTS = value
        End Set
    End Property

    Public Property MovieClearArtExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieClearArtExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearArtExpertVTS = value
        End Set
    End Property

    Public Property MovieClearLogoExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieClearLogoExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearLogoExpertVTS = value
        End Set
    End Property

    Public Property MovieDiscArtExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieDiscArtExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieDiscArtExpertVTS = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrafanartsExpertVTS
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrafanartsExpertVTS = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrathumbsExpertVTS
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrathumbsExpertVTS = value
        End Set
    End Property

    Public Property MovieFanartExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieFanartExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieFanartExpertVTS = value
        End Set
    End Property

    Public Property MovieLandscapeExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieLandscapeExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieLandscapeExpertVTS = value
        End Set
    End Property

    Public Property MovieNFOExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieNFOExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieNFOExpertVTS = value
        End Set
    End Property

    Public Property MoviePosterExpertVTS() As String
        Get
            Return Settings._XMLSettings.MoviePosterExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MoviePosterExpertVTS = value
        End Set
    End Property

    Public Property MovieRecognizeVTSExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.MovieRecognizeVTSExpertVTS
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieRecognizeVTSExpertVTS = value
        End Set
    End Property

    Public Property MovieTrailerExpertVTS() As String
        Get
            Return Settings._XMLSettings.MovieTrailerExpertVTS
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieTrailerExpertVTS = value
        End Set
    End Property

    Public Property MovieUseBaseDirectoryExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseBaseDirectoryExpertVTS
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseBaseDirectoryExpertVTS = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.MovieActorThumbsExpertBDMV
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieActorThumbsExpertBDMV = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieActorThumbsExtExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieActorThumbsExtExpertBDMV = value
        End Set
    End Property

    Public Property MovieBannerExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieBannerExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieBannerExpertBDMV = value
        End Set
    End Property

    Public Property MovieClearArtExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieClearArtExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearArtExpertBDMV = value
        End Set
    End Property

    Public Property MovieClearLogoExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieClearLogoExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieClearLogoExpertBDMV = value
        End Set
    End Property

    Public Property MovieDiscArtExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieDiscArtExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieDiscArtExpertBDMV = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrafanartsExpertBDMV
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrafanartsExpertBDMV = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.MovieExtrathumbsExpertBDMV
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieExtrathumbsExpertBDMV = value
        End Set
    End Property

    Public Property MovieFanartExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieFanartExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieFanartExpertBDMV = value
        End Set
    End Property

    Public Property MovieLandscapeExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieLandscapeExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieLandscapeExpertBDMV = value
        End Set
    End Property

    Public Property MovieNFOExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieNFOExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieNFOExpertBDMV = value
        End Set
    End Property

    Public Property MoviePosterExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MoviePosterExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MoviePosterExpertBDMV = value
        End Set
    End Property

    Public Property MovieTrailerExpertBDMV() As String
        Get
            Return Settings._XMLSettings.MovieTrailerExpertBDMV
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieTrailerExpertBDMV = value
        End Set
    End Property

    Public Property MovieUseBaseDirectoryExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.MovieUseBaseDirectoryExpertBDMV
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieUseBaseDirectoryExpertBDMV = value
        End Set
    End Property

    Public Property MovieSetUseMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetUseMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetUseMSAA = value
        End Set
    End Property

    Public Property MovieSetBannerMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetBannerMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetBannerMSAA = value
        End Set
    End Property

    Public Property MovieSetClearArtMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClearArtMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClearArtMSAA = value
        End Set
    End Property

    Public Property MovieSetClearLogoMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetClearLogoMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetClearLogoMSAA = value
        End Set
    End Property

    Public Property MovieSetFanartMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetFanartMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetFanartMSAA = value
        End Set
    End Property

    Public Property MovieSetLandscapeMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetLandscapeMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetLandscapeMSAA = value
        End Set
    End Property

    Public Property MovieSetNFOMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetNFOMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetNFOMSAA = value
        End Set
    End Property

    Public Property MovieSetPathMSAA() As String
        Get
            Return Settings._XMLSettings.MovieSetPathMSAA
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetPathMSAA = value
        End Set
    End Property

    Public Property MovieSetPosterMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetPosterMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetPosterMSAA = value
        End Set
    End Property

    Public Property MovieSetUseExpert() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetUseExpert
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetUseExpert = value
        End Set
    End Property

    Public Property MovieSetBannerExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetBannerExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetBannerExpertSingle = value
        End Set
    End Property

    Public Property MovieSetClearArtExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetClearArtExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetClearArtExpertSingle = value
        End Set
    End Property

    Public Property MovieSetClearLogoExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetClearLogoExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetClearLogoExpertSingle = value
        End Set
    End Property

    Public Property MovieSetFanartExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetFanartExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetFanartExpertSingle = value
        End Set
    End Property

    Public Property MovieSetLandscapeExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetLandscapeExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetLandscapeExpertSingle = value
        End Set
    End Property

    Public Property MovieSetNFOExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetNFOExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetNFOExpertSingle = value
        End Set
    End Property

    Public Property MovieSetPathExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetPathExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetPathExpertSingle = value
        End Set
    End Property

    Public Property MovieSetPosterExpertSingle() As String
        Get
            Return Settings._XMLSettings.MovieSetPosterExpertSingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetPosterExpertSingle = value
        End Set
    End Property

    Public Property MovieSetBannerExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetBannerExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetBannerExpertParent = value
        End Set
    End Property

    Public Property MovieSetClearArtExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetClearArtExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetClearArtExpertParent = value
        End Set
    End Property

    Public Property MovieSetClearLogoExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetClearLogoExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetClearLogoExpertParent = value
        End Set
    End Property

    Public Property MovieSetFanartExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetFanartExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetFanartExpertParent = value
        End Set
    End Property

    Public Property MovieSetLandscapeExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetLandscapeExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetLandscapeExpertParent = value
        End Set
    End Property

    Public Property MovieSetNFOExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetNFOExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetNFOExpertParent = value
        End Set
    End Property

    Public Property MovieSetPosterExpertParent() As String
        Get
            Return Settings._XMLSettings.MovieSetPosterExpertParent
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.MovieSetPosterExpertParent = value
        End Set
    End Property

    Public Property TVUseBoxee() As Boolean
        Get
            Return Settings._XMLSettings.TVUseBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVUseBoxee = value
        End Set
    End Property

    Public Property TVUseEden() As Boolean
        Get
            Return Settings._XMLSettings.TVUseEden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVUseEden = value
        End Set
    End Property

    Public Property TVUseFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVUseFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVUseFrodo = value
        End Set
    End Property

    Public Property TVUseYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVUseYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVUseYAMJ = value
        End Set
    End Property

    Public Property TVShowBannerBoxee() As Boolean
        Get
            Return Settings._XMLSettings.TVShowBannerBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowBannerBoxee = value
        End Set
    End Property

    Public Property TVShowBannerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVShowBannerFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowBannerFrodo = value
        End Set
    End Property

    Public Property TVShowBannerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVShowBannerYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowBannerYAMJ = value
        End Set
    End Property

    Public Property TVShowExtrafanartsXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVShowExtrafanartsXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowExtrafanartsXBMC = value
        End Set
    End Property

    Public Property TVShowFanartBoxee() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFanartBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFanartBoxee = value
        End Set
    End Property

    Public Property TVShowFanartFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFanartFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFanartFrodo = value
        End Set
    End Property

    Public Property TVShowFanartYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVShowFanartYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowFanartYAMJ = value
        End Set
    End Property

    Public Property TVShowPosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.TVShowPosterBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowPosterBoxee = value
        End Set
    End Property

    Public Property TVShowPosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVShowPosterFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowPosterFrodo = value
        End Set
    End Property

    Public Property TVShowPosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVShowPosterYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowPosterYAMJ = value
        End Set
    End Property

    Public Property TVShowActorThumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVShowActorThumbsFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowActorThumbsFrodo = value
        End Set
    End Property

    Public Property TVSeasonPosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonPosterBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonPosterBoxee = value
        End Set
    End Property

    Public Property TVSeasonPosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonPosterFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonPosterFrodo = value
        End Set
    End Property

    Public Property TVSeasonPosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonPosterYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonPosterYAMJ = value
        End Set
    End Property

    Public Property TVSeasonFanartFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonFanartFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonFanartFrodo = value
        End Set
    End Property

    Public Property TVSeasonFanartYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonFanartYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonFanartYAMJ = value
        End Set
    End Property

    Public Property TVSeasonBannerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonBannerFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonBannerFrodo = value
        End Set
    End Property

    Public Property TVSeasonBannerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonBannerYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonBannerYAMJ = value
        End Set
    End Property

    Public Property TVEpisodePosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodePosterBoxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodePosterBoxee = value
        End Set
    End Property

    Public Property TVEpisodePosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodePosterFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodePosterFrodo = value
        End Set
    End Property

    Public Property TVEpisodePosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodePosterYAMJ
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodePosterYAMJ = value
        End Set
    End Property

    Public Property TVEpisodeActorThumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.TVEpisodeActorThumbsFrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVEpisodeActorThumbsFrodo = value
        End Set
    End Property

    Public Property TVShowClearLogoXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClearLogoXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClearLogoXBMC = value
        End Set
    End Property

    Public Property TVShowClearArtXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVShowClearArtXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowClearArtXBMC = value
        End Set
    End Property

    Public Property TVShowCharacterArtXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVShowCharacterArtXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowCharacterArtXBMC = value
        End Set
    End Property

    Public Property TVShowTVThemeXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVShowTVThemeXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowTVThemeXBMC = value
        End Set
    End Property

    Public Property TVShowTVThemeFolderXBMC() As String
        Get
            Return Settings._XMLSettings.TVShowTVThemeFolderXBMC
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.TVShowTVThemeFolderXBMC = value
        End Set
    End Property

    Public Property TVShowLandscapeXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVShowLandscapeXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowLandscapeXBMC = value
        End Set
    End Property

    Public Property TVSeasonLandscapeXBMC() As Boolean
        Get
            Return Settings._XMLSettings.TVSeasonLandscapeXBMC
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVSeasonLandscapeXBMC = value
        End Set
    End Property

    Public Property TVShowMissingBanner() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingBanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingBanner = value
        End Set
    End Property

    Public Property TVShowMissingCharacterArt() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingCharacterArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingCharacterArt = value
        End Set
    End Property

    Public Property TVShowMissingClearArt() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingClearArt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingClearArt = value
        End Set
    End Property

    Public Property TVShowMissingClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.TVShowmissingclearlogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowmissingclearlogo = value
        End Set
    End Property

    Public Property TVShowMissingEFanarts() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingEFanarts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingEFanarts = value
        End Set
    End Property

    Public Property TVShowMissingFanart() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingFanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingFanart = value
        End Set
    End Property

    Public Property TVShowMissingLandscape() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingLandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingLandscape = value
        End Set
    End Property

    Public Property TVShowMissingNFO() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingNFO
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingNFO = value
        End Set
    End Property

    Public Property TVShowMissingPoster() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingPoster
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingPoster = value
        End Set
    End Property

    Public Property TVShowMissingTheme() As Boolean
        Get
            Return Settings._XMLSettings.TVShowMissingTheme
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.TVShowMissingTheme = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Defines all default settings for a new installation
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        'Make it simple: load a default values XML file
        Try
            Dim configpath As String = FileUtils.Common.ReturnSettingsFile("Defaults", "DefaultSettings.xml")

            Dim objStreamReader As New StreamReader(configpath)
            Dim xXMLSettings As New XmlSerializer(_XMLSettings.GetType)

            _XMLSettings = CType(xXMLSettings.Deserialize(objStreamReader), clsXMLSettings)
            objStreamReader.Close()
            ' error - someone removed the variable that holds the default TVDB languages. In case TVDB is not online to check them
            '_tvscraperlanguages.Sort(AddressOf CompareLanguagesLong)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)

        End Try
    End Sub

    Public Sub Load()
        Try
            'Cocotus, Load from central "Settings" folder if it exists!
            Dim configpath As String = FileUtils.Common.ReturnSettingsFile("Settings", "Settings.xml")

            If File.Exists(configpath) Then
                'old
                '  Dim strmReader As New StreamReader(Path.Combine(Functions.AppPath, "Settings.xml"))
                Dim objStreamReader As New StreamReader(configpath)
                Dim xXMLSettings As New XmlSerializer(_XMLSettings.GetType)

                _XMLSettings = CType(xXMLSettings.Deserialize(objStreamReader), clsXMLSettings)
                objStreamReader.Close()
                'Now we deserialize just the data in a local, shared, variable. So we can reference to us
                Master.eSettings = Me
            Else
                Master.eSettings = New Settings
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Master.eSettings = New Settings
        End Try

        If Not Master.eSettings.Version = String.Format("r{0}", My.Application.Info.Version.Revision) Then
            SetDefaultsForLists(Enums.DefaultType.All, False)
        End If

        ' Fix added to avoid to have no movie NFO saved
        If Not (Master.eSettings.MovieUseBoxee Or Master.eSettings.MovieUseEden Or Master.eSettings.MovieUseExpert Or Master.eSettings.MovieUseFrodo Or Master.eSettings.MovieUseNMJ Or Master.eSettings.MovieUseYAMJ) Then
            Master.eSettings.MovieUseFrodo = True
            Master.eSettings.MovieActorThumbsFrodo = True
            Master.eSettings.MovieBannerAD = True
            Master.eSettings.MovieClearArtAD = True
            Master.eSettings.MovieClearLogoAD = True
            Master.eSettings.MovieDiscArtAD = True
            Master.eSettings.MovieExtrafanartsFrodo = True
            Master.eSettings.MovieExtrathumbsFrodo = True
            Master.eSettings.MovieFanartFrodo = True
            Master.eSettings.MovieLandscapeAD = True
            Master.eSettings.MovieNFOFrodo = True
            Master.eSettings.MoviePosterFrodo = True
            Master.eSettings.MovieXBMCThemeEnable = True
            Master.eSettings.MovieXBMCThemeMovie = True
            Master.eSettings.MovieTrailerFrodo = True
            Master.eSettings.MovieXBMCTrailerFormat = True
        End If

        ' Fix added to avoid to have no tv show NFO saved
        If Not (Master.eSettings.TVUseBoxee OrElse Master.eSettings.TVUseFrodo OrElse Master.eSettings.TVUseYAMJ) Then
            Master.eSettings.TVUseFrodo = True
            Master.eSettings.TVEpisodeActorThumbsFrodo = True
            Master.eSettings.TVEpisodePosterFrodo = True
            Master.eSettings.TVSeasonBannerFrodo = True
            Master.eSettings.TVSeasonFanartFrodo = True
            Master.eSettings.TVSeasonLandscapeXBMC = True
            Master.eSettings.TVSeasonPosterFrodo = True
            Master.eSettings.TVShowActorThumbsFrodo = True
            Master.eSettings.TVShowBannerFrodo = True
            Master.eSettings.TVShowCharacterArtXBMC = True
            Master.eSettings.TVShowClearArtXBMC = True
            Master.eSettings.TVShowClearLogoXBMC = True
            Master.eSettings.TVShowFanartFrodo = True
            Master.eSettings.TVShowLandscapeXBMC = True
            Master.eSettings.TVShowPosterFrodo = True
        End If

    End Sub

    Public Sub Save()
        Try
            Dim xmlSerial As New XmlSerializer(GetType(Settings))

            'Cocotus All XML Config files in new Setting-folder!
            Dim configpath As String = ""
            If Directory.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar)) Then
                configpath = String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Settings.xml")
                'still Settings.xml is on old place (root)
            Else
                configpath = Path.Combine(Functions.AppPath, "Settings.xml")
            End If

            'old
            '  Dim xmlWriter As New StreamWriter(Path.Combine(Functions.AppPath, "Settings.xml"))

            Dim xmlWriter As New StreamWriter(configpath)
            xmlSerial.Serialize(xmlWriter, Master.eSettings)
            xmlWriter.Close()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub SetDefaultsForLists(ByVal Type As Enums.DefaultType, ByVal Force As Boolean)
        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MovieFilters) AndAlso (Force OrElse (Master.eSettings.MovieFilterCustom.Count <= 0 AndAlso Not Master.eSettings.MovieFilterCustomIsEmpty)) Then
            Master.eSettings.MovieFilterCustom.Clear()
            Master.eSettings.MovieFilterCustom.Add("[\W_]\(?\d{4}\)?.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]blu[\W_]?ray.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]bd[\W_]?rip.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dvd.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]720.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]1080.*") 'not really needed because the year title will catch this one, but just in case a user doesn't want the year filter but wants to filter 1080
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]ac3.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dts.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]divx.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]xvid.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dc[\W_]?.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dir(ector'?s?)?\s?cut.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]extended.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]hd(tv)?.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]unrated.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]uncut.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]([a-z]{3}|multi)[sd]ub.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]\[offline\].*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]ntsc.*")
            Master.eSettings.MovieFilterCustom.Add("[\W_]PAL[\W_]?.*")
            Master.eSettings.MovieFilterCustom.Add("\.[->] ")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ShowFilters) AndAlso (Force OrElse (Master.eSettings.TVShowFilterCustom.Count <= 0 AndAlso Not Master.eSettings.TVShowFilterCustomIsEmpty)) Then
            Master.eSettings.TVShowFilterCustom.Clear()
            Master.eSettings.TVShowFilterCustom.Add("[\W_]\(?\d{4}\)?.*")
            'would there ever be season or episode info in the show folder name??
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?s[0-9]+[\W_]*e[0-9]+(\])*")
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?[0-9]+x[0-9]+(\])*")
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?s(eason)?[\W_]*[0-9]+(\])*")
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?e(pisode)?[\W_]*[0-9]+(\])*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]blu[\W_]?ray.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]bd[\W_]?rip.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dvd.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]720.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]1080.*") 'not really needed because the year title will catch this one, but just in case a user doesn't want the year filter but wants to filter 1080
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]ac3.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dts.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]divx.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]xvid.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dc[\W_]?.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dir(ector'?s?)?\s?cut.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]extended.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]hd(tv)?.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]unrated.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]uncut.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]([a-z]{3}|multi)[sd]ub.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]\[offline\].*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]ntsc.*")
            Master.eSettings.TVShowFilterCustom.Add("[\W_]PAL[\W_]?.*")
            Master.eSettings.TVShowFilterCustom.Add("\.[->] ")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.EpFilters) AndAlso (Force OrElse (Master.eSettings.TVEpisodeFilterCustom.Count <= 0 AndAlso Not Master.eSettings.TVEpisodeFilterCustomIsEmpty)) Then
            Master.eSettings.TVEpisodeFilterCustom.Clear()
            Master.eSettings.TVEpisodeFilterCustom.Add("[\W_]\(?\d{4}\)?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?s[0-9]+[\W_]*([-e][0-9]+)+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?[0-9]+([-x][0-9]+)+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?s(eason)?[\W_]*[0-9]+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?e(pisode)?[\W_]*[0-9]+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]blu[\W_]?ray.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]bd[\W_]?rip.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dvd.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]720.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]1080.*") 'not really needed because the year title will catch this one, but just in case a user doesn't want the year filter but wants to filter 1080
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]ac3.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dts.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]divx.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]xvid.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dc[\W_]?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dir(ector'?s?)?\s?cut.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]extended.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]hd(tv)?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]unrated.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]uncut.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]([a-z]{3}|multi)[sd]ub.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]\[offline\].*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]ntsc.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("[\W_]PAL[\W_]?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("\.[->] ")
        End If

        If Type = Enums.DefaultType.All AndAlso Master.eSettings.MovieSortTokens.Count <= 0 AndAlso Not Master.eSettings.MovieSortTokensIsEmpty Then
            Master.eSettings.MovieSortTokens.Add("the[\W_]")
            Master.eSettings.MovieSortTokens.Add("a[\W_]")
            Master.eSettings.MovieSortTokens.Add("an[\W_]")
        End If

        If Type = Enums.DefaultType.All AndAlso Master.eSettings.MovieSetSortTokens.Count <= 0 AndAlso Not Master.eSettings.MovieSetSortTokensIsEmpty Then
            Master.eSettings.MovieSetSortTokens.Add("the[\W_]")
            Master.eSettings.MovieSetSortTokens.Add("a[\W_]")
            Master.eSettings.MovieSetSortTokens.Add("an[\W_]")
        End If

        If Type = Enums.DefaultType.All AndAlso Master.eSettings.TVSortTokens.Count <= 0 AndAlso Not Master.eSettings.TVSortTokensIsEmpty Then
            Master.eSettings.TVSortTokens.Add("the[\W_]")
            Master.eSettings.TVSortTokens.Add("a[\W_]")
            Master.eSettings.TVSortTokens.Add("an[\W_]")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidExts.Count <= 0) Then
            Master.eSettings.FileSystemValidExts.Clear()
            Master.eSettings.FileSystemValidExts.AddRange(Strings.Split(".avi,.divx,.mkv,.iso,.mpg,.mp4,.mpeg,.wmv,.wma,.mov,.mts,.m2t,.img,.dat,.bin,.cue,.ifo,.vob,.dvb,.evo,.asf,.asx,.avs,.nsv,.ram,.ogg,.ogm,.ogv,.flv,.swf,.nut,.viv,.rar,.m2ts,.dvr-ms,.ts,.m4v,.rmvb,.webm,.disc,.3gpp", ","))
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidSubtitleExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidSubtitlesExts.Count <= 0) Then
            Master.eSettings.FileSystemValidSubtitlesExts.Clear()
            Master.eSettings.FileSystemValidSubtitlesExts.AddRange(Strings.Split(".sst,.srt,.sub,.ssa,.aqt,.smi,.sami,.jss,.mpl,.rt,.idx,.ass", ","))
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidThemeExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidThemeExts.Count <= 0) Then
            Master.eSettings.FileSystemValidThemeExts.Clear()
            Master.eSettings.FileSystemValidThemeExts.AddRange(Strings.Split(".flac,.m4a,.mp3,.wav,.wma", ","))
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ShowRegex) AndAlso (Force OrElse Master.eSettings.TVShowRegexes.Count <= 0) Then
            Master.eSettings.TVShowRegexes.Clear()
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 0, .SeasonRegex = "(s(eason[\W_]*)?(?<season>[0-9]+))([\W_]*(\.?(-|(e(pisode[\W_]*)?))[0-9]+)+)?", .SeasonFromDirectory = False, .EpisodeRegex = "(-|(e(pisode[\W_]*)?))(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromSeasonResult})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 1, .SeasonRegex = "(([0-9]{4}-[0-9]{2}(-[0-9]{2})?)|([0-9]{2}-[0-9]{2}-[0-9]{4})|((?<season>[0-9]+)([x-][0-9]+)+))", .SeasonFromDirectory = False, .EpisodeRegex = "[x-](?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromSeasonResult})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 2, .SeasonRegex = "(([0-9]{4}-[0-9]{2}(-[0-9]{2})?)|([0-9]{2}-[0-9]{2}-[0-9]{4})|((?<season>[0-9]+)(-?[0-9]{2,})+(?![0-9])))", .SeasonFromDirectory = False, .EpisodeRegex = "(\([0-9]{4}\))|((([0-9]+|-)(?<episode>[0-9]{2,})))", .EpisodeRetrieve = EpRetrieve.FromSeasonResult})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 3, .SeasonRegex = "(?<season>specials?)$", .SeasonFromDirectory = True, .EpisodeRegex = "[^a-zA-Z]e(pisode[\W_]*)?(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromFilename})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 4, .SeasonRegex = "^(s(eason)?)?[\W_]*(?<season>[0-9]+)$", .SeasonFromDirectory = True, .EpisodeRegex = "[^a-zA-Z]e(pisode[\W_]*)?(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromFilename})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 5, .SeasonRegex = "[^\w]s(eason)?[\W_]*(?<season>[0-9]+)", .SeasonFromDirectory = True, .EpisodeRegex = "[^a-zA-Z]e(pisode[\W_]*)?(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromFilename})
        End If
    End Sub

    Public Function MovieActorThumbsAnyEnabled() As Boolean
        Return MovieActorThumbsEden OrElse MovieActorThumbsFrodo OrElse _
            (MovieUseExpert AndAlso ((MovieActorThumbsExpertBDMV AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertBDMV)) OrElse (MovieActorThumbsExpertMulti AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertMulti)) OrElse (MovieActorThumbsExpertSingle AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertSingle)) OrElse (MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertVTS))))
    End Function

    Public Function MovieBannerAnyEnabled() As Boolean
        Return MovieBannerEden OrElse MovieBannerAD OrElse MovieBannerNMJ OrElse MovieBannerYAMJ OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieBannerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieBannerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieBannerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieBannerExpertVTS)))
    End Function

    Public Function MovieClearArtAnyEnabled() As Boolean
        Return MovieClearArtEden OrElse MovieClearArtAD OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertVTS)))
    End Function

    Public Function MovieClearLogoAnyEnabled() As Boolean
        Return MovieClearLogoEden OrElse MovieClearLogoAD OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearLogoExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertVTS)))
    End Function

    Public Function MovieDiscArtAnyEnabled() As Boolean
        Return MovieDiscArtEden OrElse MovieDiscArtAD OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieDiscArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertVTS)))
    End Function

    Public Function MovieEFanartsAnyEnabled() As Boolean
        Return MovieExtrafanartsEden OrElse MovieExtrafanartsFrodo OrElse _
            (MovieUseExpert AndAlso (MovieExtrafanartsExpertBDMV OrElse MovieExtrafanartsExpertSingle OrElse MovieExtrafanartsExpertVTS))
    End Function

    Public Function MovieEThumbsAnyEnabled() As Boolean
        Return MovieExtrathumbsEden OrElse MovieExtrathumbsFrodo OrElse _
            (MovieUseExpert AndAlso (MovieExtrathumbsExpertBDMV OrElse MovieExtrathumbsExpertSingle OrElse MovieExtrathumbsExpertVTS))
    End Function

    Public Function MovieFanartAnyEnabled() As Boolean
        Return MovieFanartBoxee OrElse MovieFanartEden OrElse MovieFanartFrodo OrElse MovieFanartNMJ OrElse MovieFanartYAMJ OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieFanartExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieFanartExpertMulti) OrElse Not String.IsNullOrEmpty(MovieFanartExpertSingle) OrElse Not String.IsNullOrEmpty(MovieFanartExpertVTS)))
    End Function

    Public Function MovieLandscapeAnyEnabled() As Boolean
        Return MovieLandscapeEden OrElse MovieLandscapeAD OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieLandscapeExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertMulti) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertSingle) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertVTS)))
    End Function

    Public Function MovieMissingItemsAnyEnabled() As Boolean
        Return MovieMissingBanner OrElse MovieMissingClearArt OrElse MovieMissingClearLogo OrElse MovieMissingDiscArt OrElse MovieMissingEFanarts OrElse _
            MovieMissingEThumbs OrElse MovieMissingFanart OrElse MovieMissingLandscape OrElse MovieMissingNFO OrElse MovieMissingPoster OrElse _
            MovieMissingSubs OrElse MovieMissingTheme OrElse MovieMissingTrailer
    End Function

    Public Function MoviePosterAnyEnabled() As Boolean
        Return MoviePosterBoxee OrElse MoviePosterEden OrElse MoviePosterFrodo OrElse MoviePosterNMJ OrElse MoviePosterYAMJ OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MoviePosterExpertBDMV) OrElse Not String.IsNullOrEmpty(MoviePosterExpertMulti) OrElse Not String.IsNullOrEmpty(MoviePosterExpertSingle) OrElse Not String.IsNullOrEmpty(MoviePosterExpertVTS)))
    End Function

    Public Function MovieThemeAnyEnabled() As Boolean
        Return MovieXBMCThemeEnable AndAlso (MovieXBMCThemeMovie OrElse (MovieXBMCThemeCustom AndAlso Not String.IsNullOrEmpty(MovieXBMCThemeCustomPath) OrElse (MovieXBMCThemeSub AndAlso Not String.IsNullOrEmpty(MovieXBMCThemeSubDir))))
    End Function

    Public Function MovieTrailerAnyEnabled() As Boolean
        Return MovieTrailerEden OrElse MovieTrailerFrodo OrElse MovieTrailerNMJ OrElse MovieTrailerYAMJ OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieTrailerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertVTS)))
    End Function

    Public Function MovieSetBannerAnyEnabled() As Boolean
        Return MovieSetBannerMSAA OrElse _
            (MovieSetUseExpert AndAlso (Not String.IsNullOrEmpty(MovieSetPosterExpertParent) OrElse Not String.IsNullOrEmpty(MovieSetPosterExpertSingle)))
    End Function

    Public Function MovieSetClearArtAnyEnabled() As Boolean
        Return MovieSetClearArtMSAA OrElse _
            (MovieSetUseExpert AndAlso (Not String.IsNullOrEmpty(MovieSetClearArtExpertParent) OrElse Not String.IsNullOrEmpty(MovieSetClearArtExpertSingle)))
    End Function

    Public Function MovieSetClearLogoAnyEnabled() As Boolean
        Return MovieSetClearLogoMSAA OrElse _
            (MovieSetUseExpert AndAlso (Not String.IsNullOrEmpty(MovieSetClearLogoExpertParent) OrElse Not String.IsNullOrEmpty(MovieSetClearLogoExpertSingle)))
    End Function

    Public Function MovieSetDiscArtAnyEnabled() As Boolean
        Return False
    End Function

    Public Function MovieSetFanartAnyEnabled() As Boolean
        Return MovieSetFanartMSAA OrElse _
            (MovieSetUseExpert AndAlso (Not String.IsNullOrEmpty(MovieSetFanartExpertParent) OrElse Not String.IsNullOrEmpty(MovieSetFanartExpertSingle)))
    End Function

    Public Function MovieSetLandscapeAnyEnabled() As Boolean
        Return MovieSetLandscapeMSAA OrElse _
            (MovieSetUseExpert AndAlso (Not String.IsNullOrEmpty(MovieSetLandscapeExpertParent) OrElse Not String.IsNullOrEmpty(MovieSetLandscapeExpertSingle)))
    End Function

    Public Function MovieSetMissingItemsAnyEnabled() As Boolean
        Return MovieSetMissingBanner OrElse MovieSetMissingClearArt OrElse MovieSetMissingClearLogo OrElse MovieSetMissingDiscArt OrElse _
            MovieSetMissingFanart OrElse MovieSetMissingLandscape OrElse MovieSetMissingNFO OrElse MovieSetMissingPoster
    End Function

    Public Function MovieSetPosterAnyEnabled() As Boolean
        Return MovieSetPosterMSAA OrElse _
            (MovieSetUseExpert AndAlso (Not String.IsNullOrEmpty(MovieSetPosterExpertParent) OrElse Not String.IsNullOrEmpty(MovieSetPosterExpertSingle)))
    End Function

    Public Function TVASAnyEnabled() As Boolean
        Return TVASBannerAnyEnabled() OrElse TVASFanartAnyEnabled() OrElse TVASLandscapeAnyEnabled() OrElse TVASPosterAnyEnabled()
    End Function

    Public Function TVASBannerAnyEnabled() As Boolean
        Return TVSeasonBannerFrodo
    End Function

    Public Function TVASFanartAnyEnabled() As Boolean
        Return TVSeasonFanartFrodo
    End Function

    Public Function TVASLandscapeAnyEnabled() As Boolean
        Return TVSeasonLandscapeXBMC
    End Function

    Public Function TVASPosterAnyEnabled() As Boolean
        Return TVSeasonPosterFrodo
    End Function

    Public Function TVEpisodePosterAnyEnabled() As Boolean
        Return TVEpisodePosterBoxee OrElse TVEpisodePosterFrodo OrElse TVEpisodePosterYAMJ
    End Function

    Public Function TVEpisodeFanartAnyEnabled() As Boolean
        Return False
    End Function

    Public Function TVSeasonBannerAnyEnabled() As Boolean
        Return TVSeasonBannerFrodo OrElse TVSeasonBannerYAMJ
    End Function

    Public Function TVSeasonFanartAnyEnabled() As Boolean
        Return TVSeasonFanartFrodo OrElse TVSeasonFanartYAMJ
    End Function

    Public Function TVSeasonLandscapeAnyEnabled() As Boolean
        Return TVSeasonLandscapeXBMC
    End Function

    Public Function TVSeasonPosterAnyEnabled() As Boolean
        Return TVSeasonPosterBoxee OrElse TVSeasonPosterFrodo OrElse TVSeasonPosterYAMJ
    End Function

    Public Function TVShowBannerAnyEnabled() As Boolean
        Return TVShowBannerBoxee OrElse TVShowBannerFrodo OrElse TVShowBannerYAMJ
    End Function

    Public Function TVShowCharacterArtAnyEnabled() As Boolean
        Return TVShowCharacterArtXBMC
    End Function

    Public Function TVShowClearArtAnyEnabled() As Boolean
        Return TVShowClearArtXBMC
    End Function

    Public Function TVShowClearLogoAnyEnabled() As Boolean
        Return TVShowClearLogoXBMC
    End Function

    Public Function TVShowEFanartsAnyEnabled() As Boolean
        Return TVShowExtrafanartsXBMC
    End Function

    Public Function TVShowFanartAnyEnabled() As Boolean
        Return TVShowFanartBoxee OrElse TVShowFanartFrodo OrElse TVShowFanartYAMJ
    End Function

    Public Function TVShowLandscapeAnyEnabled() As Boolean
        Return TVShowLandscapeXBMC
    End Function

    Public Function TVShowMissingItemsAnyEnabled() As Boolean
        Return TVShowMissingBanner OrElse TVShowMissingCharacterArt OrElse TVShowMissingClearArt OrElse TVShowMissingClearLogo OrElse _
            TVShowMissingEFanarts OrElse TVShowMissingFanart OrElse TVShowMissingLandscape OrElse TVShowMissingNFO OrElse _
            TVShowMissingPoster OrElse TVShowMissingTheme
    End Function

    Public Function TVShowPosterAnyEnabled() As Boolean
        Return TVShowPosterBoxee OrElse TVShowPosterFrodo OrElse TVShowPosterYAMJ
    End Function

    Public Function TVShowTVThemeAnyEnabled() As Boolean
        Return TVShowTVThemeXBMC
    End Function

    Private Shared Function CompareLanguagesLong( _
        ByVal x As TVDBLanguagesLanguage, ByVal y As TVDBLanguagesLanguage) As Integer

        If x Is Nothing Then
            If y Is Nothing Then
                ' If x is Nothing and y is Nothing, they're
                ' equal.
                Return 0
            Else
                ' If x is Nothing and y is not Nothing, y
                ' is greater.
                Return -1
            End If
        Else
            ' If x is not Nothing...
            '
            If y Is Nothing Then
                ' ...and y is Nothing, x is greater.
                Return 1
            Else
                ' ...and y is not Nothing, compare the
                ' lengths of the two strings.
                '

                'Dim retval As Integer = _
                '    x.LongLang.Length.CompareTo(y.LongLang.Length)

                'If retval <> 0 Then
                '    ' If the strings are not of equal length,
                '    ' the longer string is greater.
                '    '
                '    Return retval
                'Else
                '    ' If the strings are of equal length,
                '    ' sort them with ordinary string comparison.
                '    '
                Return x.name.CompareTo(y.name)
                'End If
            End If
        End If

    End Function

    Private Shared Function CompareLanguagesShort( _
        ByVal x As TVDBLanguagesLanguage, ByVal y As TVDBLanguagesLanguage) As Integer

        If x Is Nothing Then
            If y Is Nothing Then
                ' If x is Nothing and y is Nothing, they're
                ' equal.
                Return 0
            Else
                ' If x is Nothing and y is not Nothing, y
                ' is greater.
                Return -1
            End If
        Else
            ' If x is not Nothing...
            '
            If y Is Nothing Then
                ' ...and y is Nothing, x is greater.
                Return 1
            Else
                ' ...and y is not Nothing, compare the
                ' lengths of the two strings.
                '

                'Dim retval As Integer = _
                '    x.ShortLang.Length.CompareTo(y.ShortLang.Length)

                'If retval <> 0 Then
                '    ' If the strings are not of equal length,
                '    ' the longer string is greater.
                '    '
                '    Return retval
                'Else
                '    ' If the strings are of equal length,
                '    ' sort them with ordinary string comparison.
                '    '
                Return x.abbreviation.CompareTo(y.abbreviation)
                'End If
            End If
        End If

    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class MetadataPerType

#Region "Fields"

        Private _filetype As String
        Private _metadata As MediaInfo.Fileinfo

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property FileType() As String
            Get
                Return Me._filetype
            End Get
            Set(ByVal value As String)
                Me._filetype = value
            End Set
        End Property

        Public Property MetaData() As MediaInfo.Fileinfo
            Get
                Return Me._metadata
            End Get
            Set(ByVal value As MediaInfo.Fileinfo)
                Me._metadata = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._filetype = String.Empty
            Me._metadata = New MediaInfo.Fileinfo
        End Sub

#End Region 'Methods

    End Class

    Public Class TVShowRegEx

#Region "Fields"

        Private _episoderegex As String
        Private _episoderetrieve As EpRetrieve
        Private _id As Integer
        Private _seasonfromdirectory As Boolean
        Private _seasonregex As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property EpisodeRegex() As String
            Get
                Return Me._episoderegex
            End Get
            Set(ByVal value As String)
                Me._episoderegex = value
            End Set
        End Property

        Public Property EpisodeRetrieve() As EpRetrieve
            Get
                Return Me._episoderetrieve
            End Get
            Set(ByVal value As EpRetrieve)
                Me._episoderetrieve = value
            End Set
        End Property

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        Public Property SeasonFromDirectory() As Boolean
            Get
                Return Me._seasonfromdirectory
            End Get
            Set(ByVal value As Boolean)
                Me._seasonfromdirectory = value
            End Set
        End Property

        Public Property SeasonRegex() As String
            Get
                Return Me._seasonregex
            End Get
            Set(ByVal value As String)
                Me._seasonregex = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._id = -1
            Me._seasonregex = String.Empty
            Me._seasonfromdirectory = True
            Me._episoderegex = String.Empty
            Me._episoderetrieve = EpRetrieve.FromSeasonResult
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class