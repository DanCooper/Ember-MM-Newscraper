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
            Return Settings._XMLSettings.moviescrapercastlimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviescrapercastlimit = value
        End Set
    End Property

    Public Property RestartScraper() As Boolean
        Get
            Return Settings._XMLSettings.restartscraper
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.restartscraper = value
        End Set
    End Property


    Public Property MovieActorThumbsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbsoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbsoverwrite = value
        End Set
    End Property

    Public Property TVASPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.tvasposterheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasposterheight = value
        End Set
    End Property

    Public Property MovieActorThumbsQual() As Integer
        Get
            Return Settings._XMLSettings.movieactorthumbsqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieactorthumbsqual = value
        End Set
    End Property

    Public Property TVASPosterQual() As Integer
        Get
            Return Settings._XMLSettings.tvasposterqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasposterqual = value
        End Set
    End Property

    Public Property TVASBannerQual() As Integer
        Get
            Return Settings._XMLSettings.tvasbannerqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasbannerqual = value
        End Set
    End Property

    Public Property TVASFanartQual() As Integer
        Get
            Return Settings._XMLSettings.tvasfanartqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasfanartqual = value
        End Set
    End Property

    Public Property TVSeasonBannerQual() As Integer
        Get
            Return Settings._XMLSettings.tvseasonbannerqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonbannerqual = value
        End Set
    End Property

    Public Property TVShowBannerQual() As Integer
        Get
            Return Settings._XMLSettings.tvshowbannerqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowbannerqual = value
        End Set
    End Property

    Public Property TVASPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.tvasposterwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasposterwidth = value
        End Set
    End Property

    Public Property GeneralShowGenresText() As Boolean
        Get
            Return Settings._XMLSettings.generalshowgenrestext
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalshowgenrestext = value
        End Set
    End Property

    Public Property TVGeneralLanguage() As String
        Get
            Return Settings._XMLSettings.tvgenerallanguage
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.tvgenerallanguage = If(String.IsNullOrEmpty(value), "en", value)
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
            Return Settings._XMLSettings.movieclickscrape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclickscrape = value
        End Set
    End Property

    Public Property MovieClickScrapeAsk() As Boolean
        Get
            Return Settings._XMLSettings.movieclickscrapeask
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclickscrapeask = value
        End Set
    End Property

    Public Property MovieBackdropsAuto() As Boolean
        Get
            Return Settings._XMLSettings.moviebackdropsauto
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebackdropsauto = value
        End Set
    End Property

    Public Property MovieIMDBURL() As String
        Get
            Return Settings._XMLSettings.movieimdburl
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieimdburl = value
        End Set
    End Property

    Public Property MovieBackdropsPath() As String
        Get
            Return Settings._XMLSettings.moviebackdropspath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviebackdropspath = value
        End Set
    End Property

    Public Property MovieMoviesetsPath() As String
        Get
            Return Settings._XMLSettings.moviemoviesetspath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviemoviesetspath = value
        End Set
    End Property

    Public Property MovieScraperCastWithImgOnly() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercastwithimgonly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercastwithimgonly = value
        End Set
    End Property

    Public Property MovieScraperCertLang() As String
        Get
            Return Settings._XMLSettings.moviescrapercertlang
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviescrapercertlang = value
        End Set
    End Property

    Public Property GeneralCheckUpdates() As Boolean
        Get
            Return Settings._XMLSettings.generalcheckupdates
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalcheckupdates = value
        End Set
    End Property

    Public Property MovieCleanDB() As Boolean
        Get
            Return Settings._XMLSettings.moviecleandb
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviecleandb = value
        End Set
    End Property

    Public Property CleanDotFanartJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleandotfanartJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleandotfanartJpg = value
        End Set
    End Property

    Public Property CleanExtrathumbs() As Boolean
        Get
            Return Settings._XMLSettings.cleanextrathumbs
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanextrathumbs = value
        End Set
    End Property

    Public Property CleanFanartJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleanfanartJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanfanartJpg = value
        End Set
    End Property

    Public Property CleanFolderJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleanfolderJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanfolderJpg = value
        End Set
    End Property

    Public Property CleanMovieFanartJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleanmoviefanartJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanmoviefanartJpg = value
        End Set
    End Property

    Public Property CleanMovieJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleanmovieJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanmovieJpg = value
        End Set
    End Property

    Public Property CleanMovieNameJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleanmovienameJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanmovienameJpg = value
        End Set
    End Property

    Public Property CleanMovieNFO() As Boolean
        Get
            Return Settings._XMLSettings.cleanmovieNfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanmovieNfo = value
        End Set
    End Property

    Public Property CleanMovieNFOB() As Boolean
        Get
            Return Settings._XMLSettings.cleanmovieNfoB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanmovieNfoB = value
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
            Return Settings._XMLSettings.cleanmovieTbnB
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanmovieTbnB = value
        End Set
    End Property

    Public Property CleanPosterJPG() As Boolean
        Get
            Return Settings._XMLSettings.cleanposterJpg
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanposterJpg = value
        End Set
    End Property

    Public Property CleanPosterTBN() As Boolean
        Get
            Return Settings._XMLSettings.cleanposterTbn
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.cleanposterTbn = value
        End Set
    End Property

    Public Property FileSystemCleanerWhitelistExts() As List(Of String)
        Get
            Return Settings._XMLSettings.filesystemcleanerwhitelistexts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.filesystemcleanerwhitelistexts = value
        End Set
    End Property

    Public Property FileSystemCleanerWhitelist() As Boolean
        Get
            Return Settings._XMLSettings.filesystemcleanerwhitelist
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.filesystemcleanerwhitelist = value
        End Set
    End Property

    Public Property MovieTrailerDeleteExisting() As Boolean
        Get
            Return Settings._XMLSettings.movietrailerdeleteexisting
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietrailerdeleteexisting = value
        End Set
    End Property

    Public Property TVGeneralDisplayASPoster() As Boolean
        Get
            Return Settings._XMLSettings.tvgeneraldisplayasposter
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvgeneraldisplayasposter = value
        End Set
    End Property

    Public Property TVDisplayMissingEpisodes() As Boolean
        Get
            Return Settings._XMLSettings.tvdisplaymissingepisodes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvdisplaymissingepisodes = value
        End Set
    End Property

    Public Property MovieDisplayYear() As Boolean
        Get
            Return Settings._XMLSettings.moviedisplayyear
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviedisplayyear = value
        End Set
    End Property

    Public Property TVScraperOptionsOrdering() As Enums.Ordering
        Get
            Return Settings._XMLSettings.tvscraperoptionsordering
        End Get
        Set(ByVal value As Enums.Ordering)
            Settings._XMLSettings.tvscraperoptionsordering = value
        End Set
    End Property

    <XmlArray("EmberModules")> _
    <XmlArrayItem("Module")> _
    Public Property EmberModules() As List(Of ModulesManager._XMLEmberModuleClass)
        Get
            Return Settings._XMLSettings.emberModules
        End Get
        Set(ByVal value As List(Of ModulesManager._XMLEmberModuleClass))
            Settings._XMLSettings.emberModules = value
        End Set
    End Property

    Public Property MovieScraperMetaDataIFOScan() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapermetadataifoscan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapermetadataifoscan = value
        End Set
    End Property

    Public Property TVEpisodeFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.tvepisodefanartheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvepisodefanartheight = value
        End Set
    End Property

    Public Property TVEpisodeFanartQual() As Integer
        Get
            Return Settings._XMLSettings.tvepisodefanartqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvepisodefanartqual = value
        End Set
    End Property

    Public Property TVEpisodeFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.tvepisodefanartwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvepisodefanartwidth = value
        End Set
    End Property

    Public Property TVEpisodeFilterCustom() As List(Of String)
        Get
            Return Settings._XMLSettings.tvepisodefiltercustom
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.tvepisodefiltercustom = value
        End Set
    End Property

    Public Property TVEpisodeFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodefanartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodefanartcol = value
        End Set
    End Property

    Public Property TVEpisodeNfoCol() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodenfocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodenfocol = value
        End Set
    End Property

    Public Property TVEpisodePosterCol() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodepostercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodepostercol = value
        End Set
    End Property

    Public Property TVLockEpisodePlot() As Boolean
        Get
            Return Settings._XMLSettings.tvlockepisodeplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockepisodeplot = value
        End Set
    End Property

    Public Property TVLockEpisodeRating() As Boolean
        Get
            Return Settings._XMLSettings.tvlockepisoderating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockepisoderating = value
        End Set
    End Property

    Public Property TVLockEpisodeTitle() As Boolean
        Get
            Return Settings._XMLSettings.tvlockepisodetitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockepisodetitle = value
        End Set
    End Property

    Public Property TVEpisodePosterHeight() As Integer
        Get
            Return Settings._XMLSettings.tvepisodeposterheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvepisodeposterheight = value
        End Set
    End Property

    Public Property TVEpisodePosterQual() As Integer
        Get
            Return Settings._XMLSettings.tvepisodeposterqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvepisodeposterqual = value
        End Set
    End Property

    Public Property TVEpisodePosterWidth() As Integer
        Get
            Return Settings._XMLSettings.tvepisodeposterwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvepisodeposterwidth = value
        End Set
    End Property

    Public Property TVEpisodeProperCase() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodepropercase
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodepropercase = value
        End Set
    End Property

    Public Property TVEpisodeWatchedCol() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodewatchedcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodewatchedcol = value
        End Set
    End Property

    Public Property FileSystemExpertCleaner() As Boolean
        Get
            Return Settings._XMLSettings.filesystemexpertcleaner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.filesystemexpertcleaner = value
        End Set
    End Property

    Public Property MovieEFanartsHeight() As Integer
        Get
            Return Settings._XMLSettings.movieefanartsheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieefanartsheight = value
        End Set
    End Property

    Public Property MovieEThumbsHeight() As Integer
        Get
            Return Settings._XMLSettings.movieethumbsheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieethumbsheight = value
        End Set
    End Property

    Public Property MovieEThumbsLimit() As Integer
        Get
            Return Settings._XMLSettings.movieethumbslimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieethumbslimit = value
        End Set
    End Property

    Public Property MovieEFanartsLimit() As Integer
        Get
            Return Settings._XMLSettings.movieefanartslimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieefanartslimit = value
        End Set
    End Property

    Public Property MovieFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.moviefanartheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviefanartheight = value
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

    Public Property MovieEFanartsPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.movieefanartsprefonly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieefanartsprefonly = value
        End Set
    End Property

    Public Property MovieEThumbsPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.movieethumbsprefonly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieethumbsprefonly = value
        End Set
    End Property

    Public Property MovieFanartPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartprefonly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartprefonly = value
        End Set
    End Property

    Public Property MovieFanartQual() As Integer
        Get
            Return Settings._XMLSettings.moviefanartqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviefanartqual = value
        End Set
    End Property

    Public Property MovieSetFanartQual() As Integer
        Get
            Return Settings._XMLSettings.MovieSetFanartQual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetFanartQual = value
        End Set
    End Property

    Public Property MovieEFanartsWidth() As Integer
        Get
            Return Settings._XMLSettings.movieefanartswidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieefanartswidth = value
        End Set
    End Property

    Public Property MovieEThumbsWidth() As Integer
        Get
            Return Settings._XMLSettings.movieethumbswidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieethumbswidth = value
        End Set
    End Property

    Public Property MovieFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.moviefanartwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviefanartwidth = value
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
            Return Settings._XMLSettings.moviescrapertop250
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapertop250 = value
        End Set
    End Property

    Public Property MovieScraperCollection() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercollection
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercollection = value
        End Set
    End Property

    Public Property MovieScraperCountry() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercountry
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercountry = value
        End Set
    End Property

    Public Property MovieScraperCast() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercast
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercast = value
        End Set
    End Property

    Public Property MovieScraperCertification() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercertification
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercertification = value
        End Set
    End Property

    Public Property MovieScraperCrew() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercrew
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercrew = value
        End Set
    End Property

    Public Property MovieScraperDirector() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperdirector
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperdirector = value
        End Set
    End Property

    Public Property MovieScraperGenre() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapergenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapergenre = value
        End Set
    End Property

    Public Property MovieScraperMPAA() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapermpaa
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapermpaa = value
        End Set
    End Property

    Public Property MovieScraperMusicBy() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapermusicby
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapermusicby = value
        End Set
    End Property

    Public Property MovieScraperOutline() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperoutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperoutline = value
        End Set
    End Property

    Public Property MovieScraperPlot() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperplot = value
        End Set
    End Property

    Public Property MovieScraperProducers() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperproducers
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperproducers = value
        End Set
    End Property

    Public Property MovieScraperRating() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperrating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperrating = value
        End Set
    End Property

    Public Property MovieScraperRelease() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperrelease
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperrelease = value
        End Set
    End Property

    Public Property MovieScraperRuntime() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperruntime
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperruntime = value
        End Set
    End Property

    Public Property MovieScraperStudio() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperstudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperstudio = value
        End Set
    End Property

    Public Property MovieScraperTagline() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapertagline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapertagline = value
        End Set
    End Property

    Public Property MovieScraperTitle() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapertitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapertitle = value
        End Set
    End Property

    Public Property MovieScraperTrailer() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapertrailer
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapertrailer = value
        End Set
    End Property

    Public Property MovieScraperVotes() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapervotes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapervotes = value
        End Set
    End Property

    Public Property MovieScraperWriters() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperwriters
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperwriters = value
        End Set
    End Property

    Public Property MovieScraperYear() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperyear
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperyear = value
        End Set
    End Property

    Public Property MovieFilterCustom() As List(Of String)
        Get
            Return Settings._XMLSettings.moviefiltercustom
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.moviefiltercustom = value
        End Set
    End Property

    Public Property GeneralFilterPanelState() As Boolean
        Get
            Return Settings._XMLSettings.generalfilterpanelstate
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalfilterpanelstate = value
        End Set
    End Property

    Public Property MovieGeneralFlagLang() As String
        Get
            Return Settings._XMLSettings.moviegeneralflaglang
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviegeneralflaglang = value
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
            Return Settings._XMLSettings.genrefilter
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.genrefilter = value
        End Set
    End Property

    Public Property MovieScraperGenreLimit() As Integer
        Get
            Return Settings._XMLSettings.moviescrapergenrelimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviescrapergenrelimit = value
        End Set
    End Property

    Public Property MovieGeneralIgnoreLastScan() As Boolean
        Get
            Return Settings._XMLSettings.moviegeneralignorelastscan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviegeneralignorelastscan = value
        End Set
    End Property

    Public Property GeneralInfoPanelAnim() As Boolean
        Get
            Return Settings._XMLSettings.generalinfopanelanim
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalinfopanelanim = value
        End Set
    End Property

    Public Property GeneralMovieInfoPanelState() As Integer
        Get
            Return Settings._XMLSettings.generalmovieinfopanelstate
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.generalmovieinfopanelstate = value
        End Set
    End Property

    Public Property GeneralMovieSetInfoPanelState() As Integer
        Get
            Return Settings._XMLSettings.generalmoviesetinfopanelstate
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.generalmoviesetinfopanelstate = value
        End Set
    End Property

    Public Property GeneralLanguage() As String
        Get
            Return Settings._XMLSettings.generallanguage
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generallanguage = value
        End Set
    End Property

    Public Property MovieLevTolerance() As Integer
        Get
            Return Settings._XMLSettings.movielevtolerance
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movielevtolerance = value
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

    Public Property MovieLockCollection() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockCollection
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockCollection = value
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
            Return Settings._XMLSettings.movielockgenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielockgenre = value
        End Set
    End Property

    Public Property MovieLockMusicBy() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockMusicBy
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockMusicBy = value
        End Set
    End Property

    Public Property MovieLockOtherCrew() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockOtherCrew
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockOtherCrew = value
        End Set
    End Property

    Public Property MovieLockOutline() As Boolean
        Get
            Return Settings._XMLSettings.movielockoutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielockoutline = value
        End Set
    End Property

    Public Property MovieLockPlot() As Boolean
        Get
            Return Settings._XMLSettings.movielockplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielockplot = value
        End Set
    End Property

    Public Property MovieLockProducers() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockProducers
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockProducers = value
        End Set
    End Property

    Public Property MovieLockRating() As Boolean
        Get
            Return Settings._XMLSettings.movielockrating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielockrating = value
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
            Return Settings._XMLSettings.movielocklanguagev
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielocklanguagev = value
        End Set
    End Property
    Public Property MovieLockLanguageA() As Boolean
        Get
            Return Settings._XMLSettings.movielocklanguagea
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielocklanguagea = value
        End Set
    End Property
    Public Property MovieLockMPAA() As Boolean
        Get
            Return Settings._XMLSettings.movielockmpaa
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielockmpaa = value
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

    Public Property MovieLockWriters() As Boolean
        Get
            Return Settings._XMLSettings.MovieLockWriters
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieLockWriters = value
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

    Public Property MovieScraperUseMPAAFSK() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperusempaafsk
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperusempaafsk = value
        End Set
    End Property
    Public Property MovieLockStudio() As Boolean
        Get
            Return Settings._XMLSettings.movielockstudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielockstudio = value
        End Set
    End Property

    Public Property MovieLockTagline() As Boolean
        Get
            Return Settings._XMLSettings.movielocktagline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielocktagline = value
        End Set
    End Property

    Public Property MovieLockTitle() As Boolean
        Get
            Return Settings._XMLSettings.movielocktitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielocktitle = value
        End Set
    End Property

    Public Property MovieLockTrailer() As Boolean
        Get
            Return Settings._XMLSettings.movielocktrailer
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielocktrailer = value
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
            Return Settings._XMLSettings.moviegeneralmarknew
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviegeneralmarknew = value
        End Set
    End Property

    Public Property TVGeneralMarkNewEpisodes() As Boolean
        Get
            Return Settings._XMLSettings.tvgeneralmarknewepisodes
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvgeneralmarknewepisodes = value
        End Set
    End Property

    Public Property TVGeneralMarkNewShows() As Boolean
        Get
            Return Settings._XMLSettings.tvgeneralmarknewshows
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvgeneralmarknewshows = value
        End Set
    End Property

    Public Property MovieMetadataPerFileType() As List(Of MetadataPerType)
        Get
            Return Settings._XMLSettings.moviemetadataperfiletype
        End Get
        Set(ByVal value As List(Of MetadataPerType))
            Settings._XMLSettings.moviemetadataperfiletype = value
        End Set
    End Property

    Public Property MovieMissingBanner() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingbanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingbanner = value
        End Set
    End Property

    Public Property MovieMissingClearArt() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingclearart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingclearart = value
        End Set
    End Property

    Public Property MovieMissingClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingclearlogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingclearlogo = value
        End Set
    End Property

    Public Property MovieMissingDiscArt() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingdiscart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingdiscart = value
        End Set
    End Property

    Public Property MovieMissingEThumbs() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingethumbs
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingethumbs = value
        End Set
    End Property

    Public Property MovieMissingEFanarts() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingefanarts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingefanarts = value
        End Set
    End Property

    Public Property MovieMissingFanart() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingfanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingfanart = value
        End Set
    End Property

    Public Property MovieMissingLandscape() As Boolean
        Get
            Return Settings._XMLSettings.moviemissinglandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissinglandscape = value
        End Set
    End Property

    Public Property MovieMissingNFO() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingnfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingnfo = value
        End Set
    End Property

    Public Property MovieMissingPoster() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingposter
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingposter = value
        End Set
    End Property

    Public Property MovieMissingSubs() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingsubs
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingsubs = value
        End Set
    End Property

    Public Property MovieMissingTheme() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingtheme
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingtheme = value
        End Set
    End Property

    Public Property MovieMissingTrailer() As Boolean
        Get
            Return Settings._XMLSettings.moviemissingtrailer
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviemissingtrailer = value
        End Set
    End Property

    Public Property MovieBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.moviebannercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebannercol = value
        End Set
    End Property

    Public Property MovieClearArtCol() As Boolean
        Get
            Return Settings._XMLSettings.movieclearartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearartcol = value
        End Set
    End Property

    Public Property MovieClearLogoCol() As Boolean
        Get
            Return Settings._XMLSettings.movieclearlogocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearlogocol = value
        End Set
    End Property

    Public Property MovieDiscArtCol() As Boolean
        Get
            Return Settings._XMLSettings.moviediscartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviediscartcol = value
        End Set
    End Property

    Public Property MovieEFanartsCol() As Boolean
        Get
            Return Settings._XMLSettings.movieefanartscol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieefanartscol = value
        End Set
    End Property

    Public Property MovieEThumbsCol() As Boolean
        Get
            Return Settings._XMLSettings.movieethumbscol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieethumbscol = value
        End Set
    End Property

    Public Property MovieFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartcol = value
        End Set
    End Property

    Public Property MovieLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.movielandscapecol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielandscapecol = value
        End Set
    End Property

    Public Property MovieNFOCol() As Boolean
        Get
            Return Settings._XMLSettings.movienfocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienfocol = value
        End Set
    End Property

    Public Property MoviePosterCol() As Boolean
        Get
            Return Settings._XMLSettings.moviepostercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviepostercol = value
        End Set
    End Property

    Public Property MovieSubCol() As Boolean
        Get
            Return Settings._XMLSettings.moviesubcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesubcol = value
        End Set
    End Property

    Public Property MovieSetBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.moviesetbannercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetbannercol = value
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
            Return Settings._XMLSettings.moviesetclearartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetclearartcol = value
        End Set
    End Property

    Public Property MovieSetClearLogoCol() As Boolean
        Get
            Return Settings._XMLSettings.moviesetclearlogocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetclearlogocol = value
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
            Return Settings._XMLSettings.moviesetdiscartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetdiscartcol = value
        End Set
    End Property

    Public Property MovieSetFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.moviesetfanartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetfanartcol = value
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
            Return Settings._XMLSettings.moviesetlandscapecol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetlandscapecol = value
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
            Return Settings._XMLSettings.moviesetnfocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetnfocol = value
        End Set
    End Property

    Public Property MovieSetPosterCol() As Boolean
        Get
            Return Settings._XMLSettings.moviesetpostercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetpostercol = value
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
            Return Settings._XMLSettings.MovieSetmissingbanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingbanner = value
        End Set
    End Property

    Public Property MovieSetMissingClearArt() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissingclearart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingclearart = value
        End Set
    End Property

    Public Property MovieSetMissingClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissingclearlogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingclearlogo = value
        End Set
    End Property

    Public Property MovieSetMissingDiscArt() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissingdiscart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingdiscart = value
        End Set
    End Property

    Public Property MovieSetMissingFanart() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissingfanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingfanart = value
        End Set
    End Property

    Public Property MovieSetMissingLandscape() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissinglandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissinglandscape = value
        End Set
    End Property

    Public Property MovieSetMissingNFO() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissingnfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingnfo = value
        End Set
    End Property

    Public Property MovieSetMissingPoster() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetmissingposter
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetmissingposter = value
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
            Return Settings._XMLSettings.generalmovietheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generalmovietheme = value
        End Set
    End Property

    Public Property GeneralMovieSetTheme() As String
        Get
            Return Settings._XMLSettings.generalmoviesettheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generalmoviesettheme = value
        End Set
    End Property

    Public Property GeneralDaemonPath() As String
        Get
            Return Settings._XMLSettings.generaldaemonpath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generaldaemonpath = value
        End Set
    End Property

    Public Property GeneralDaemonDrive() As String
        Get
            Return Settings._XMLSettings.generaldaemondrive
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generaldaemondrive = value
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
            Return Settings._XMLSettings.moviethemecol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviethemecol = value
        End Set
    End Property

    Public Property MovieTrailerCol() As Boolean
        Get
            Return Settings._XMLSettings.movietrailercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietrailercol = value
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
            Return Settings._XMLSettings.moviewatchedcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviewatchedcol = value
        End Set
    End Property

    Public Property GeneralHideBanner() As Boolean
        Get
            Return Settings._XMLSettings.generalhidebanner
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhidebanner = value
        End Set
    End Property

    Public Property GeneralHideCharacterArt() As Boolean
        Get
            Return Settings._XMLSettings.generalhidecharacterart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhidecharacterart = value
        End Set
    End Property

    Public Property GeneralHideClearArt() As Boolean
        Get
            Return Settings._XMLSettings.generalhideclearart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhideclearart = value
        End Set
    End Property

    Public Property GeneralHideClearLogo() As Boolean
        Get
            Return Settings._XMLSettings.generalhideclearlogo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhideclearlogo = value
        End Set
    End Property

    Public Property GeneralHideDiscArt() As Boolean
        Get
            Return Settings._XMLSettings.generalhidediscart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhidediscart = value
        End Set
    End Property

    Public Property GeneralHideFanart() As Boolean
        Get
            Return Settings._XMLSettings.generalhidefanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhidefanart = value
        End Set
    End Property

    Public Property GeneralHideFanartSmall() As Boolean
        Get
            Return Settings._XMLSettings.generalhidefanartsmall
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhidefanartsmall = value
        End Set
    End Property

    Public Property GeneralHideLandscape() As Boolean
        Get
            Return Settings._XMLSettings.generalhidelandscape
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhidelandscape = value
        End Set
    End Property

    Public Property GeneralHidePoster() As Boolean
        Get
            Return Settings._XMLSettings.generalhideposter
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalhideposter = value
        End Set
    End Property

    Public Property TVEpisodeFilterCustomIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodefiltercustomisempty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodefiltercustomisempty = value
        End Set
    End Property

    Public Property TVEpisodeNoFilter() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodenofilter
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodenofilter = value
        End Set
    End Property

    Public Property MovieFilterCustomIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.moviefiltercustomisempty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefiltercustomisempty = value
        End Set
    End Property

    Public Property MovieNoSaveImagesToNfo() As Boolean
        Get
            Return Settings._XMLSettings.movienosaveimagestonfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienosaveimagestonfo = value
        End Set
    End Property

    Public Property TVShowFilterCustomIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfiltercustomisempty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfiltercustomisempty = value
        End Set
    End Property

    Public Property FileSystemNoStackExts() As List(Of String)
        Get
            Return Settings._XMLSettings.filesystemnostackexts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.filesystemnostackexts = value
        End Set
    End Property

    Public Property MovieSortTokensIsEmpty() As Boolean
        Get
            Return Settings._XMLSettings.moviesorttokensisempty
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesorttokensisempty = value
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

    Public Property OMMDummyFormat() As Integer
        Get
            Return Settings._XMLSettings.ommdummyformat
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.ommdummyformat = value
        End Set
    End Property

    Public Property OMMDummyTagline() As String
        Get
            Return Settings._XMLSettings.ommdummytagline
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.ommdummytagline = value
        End Set
    End Property

    Public Property OMMDummyTop() As String
        Get
            Return Settings._XMLSettings.ommdummytop
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.ommdummytop = value
        End Set
    End Property

    Public Property OMMDummyUseBackground() As Boolean
        Get
            Return Settings._XMLSettings.ommdummyusebackground
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.ommdummyusebackground = value
        End Set
    End Property

    Public Property OMMDummyUseFanart() As Boolean
        Get
            Return Settings._XMLSettings.ommdummyusefanart
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.ommdummyusefanart = value
        End Set
    End Property

    Public Property OMMDummyUseOverlay() As Boolean
        Get
            Return Settings._XMLSettings.ommdummyuseoverlay
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.ommdummyuseoverlay = value
        End Set
    End Property

    Public Property OMMMediaStubTagline() As String
        Get
            Return Settings._XMLSettings.ommmediastubtagline
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.ommmediastubtagline = value
        End Set
    End Property

    Public Property MovieScraperOnlyValueForMPAA() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperonlyvalueformpaa
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperonlyvalueformpaa = value
        End Set
    End Property

    Public Property MovieScraperOutlineForPlot() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperoutlineforplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperoutlineforplot = value
        End Set
    End Property

    Public Property MovieScraperOutlinePlotEnglishOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperoutlineplotenglishoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperoutlineplotenglishoverwrite = value
        End Set
    End Property

    Public Property TVASBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvasbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvasbanneroverwrite = value
        End Set
    End Property

    Public Property TVASFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvasfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvasfanartoverwrite = value
        End Set
    End Property

    Public Property TVASLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvaslandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvaslandscapeoverwrite = value
        End Set
    End Property

    Public Property TVASPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvasposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvasposteroverwrite = value
        End Set
    End Property

    Public Property TVEpisodeFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodefanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodefanartoverwrite = value
        End Set
    End Property

    Public Property TVEpisodePosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodeposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodeposteroverwrite = value
        End Set
    End Property

    Public Property MovieEFanartsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movieefanartsoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieefanartsoverwrite = value
        End Set
    End Property

    Public Property MovieEThumbsOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movieethumbsoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieethumbsoverwrite = value
        End Set
    End Property

    Public Property MovieFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartoverwrite = value
        End Set
    End Property

    Public Property GeneralOverwriteNfo() As Boolean
        Get
            Return Settings._XMLSettings.generaloverwritenfo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generaloverwritenfo = value
        End Set
    End Property

    Public Property MoviePosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movieposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposteroverwrite = value
        End Set
    End Property

    Public Property TVSeasonBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonbanneroverwrite = value
        End Set
    End Property

    Public Property TVShowCharacterArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowcharacterartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowcharacterartoverwrite = value
        End Set
    End Property

    Public Property TVShowClearArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowclearartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowclearartoverwrite = value
        End Set
    End Property

    Public Property TVShowClearLogoOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowclearlogooverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowclearlogooverwrite = value
        End Set
    End Property

    Public Property TVSeasonLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonlandscapecol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonlandscapecol = value
        End Set
    End Property

    Public Property TVSeasonLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonlandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonlandscapeoverwrite = value
        End Set
    End Property

    Public Property TVShowLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowlandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowlandscapeoverwrite = value
        End Set
    End Property

    Public Property TVSeasonFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonfanartoverwrite = value
        End Set
    End Property

    Public Property TVSeasonPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonposteroverwrite = value
        End Set
    End Property

    Public Property TVShowBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowbanneroverwrite = value
        End Set
    End Property

    Public Property TVShowFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfanartoverwrite = value
        End Set
    End Property

    Public Property TVShowPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.tvshowposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowposteroverwrite = value
        End Set
    End Property

    Public Property MovieBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviebanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebanneroverwrite = value
        End Set
    End Property

    Public Property MovieDiscArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviediscartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviediscartoverwrite = value
        End Set
    End Property

    Public Property MovieLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movielandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielandscapeoverwrite = value
        End Set
    End Property

    Public Property MovieClearArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movieclearartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearartoverwrite = value
        End Set
    End Property

    Public Property MovieClearLogoOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movieclearlogooverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearlogooverwrite = value
        End Set
    End Property

    Public Property MovieSetBannerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetbanneroverwrite = value
        End Set
    End Property

    Public Property MovieSetClearArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetclearartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetclearartoverwrite = value
        End Set
    End Property

    Public Property MovieSetClearLogoOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetclearlogooverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetclearlogooverwrite = value
        End Set
    End Property

    Public Property MovieSetDiscArtOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetdiscartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetdiscartoverwrite = value
        End Set
    End Property

    Public Property MovieSetFanartOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetfanartoverwrite = value
        End Set
    End Property

    Public Property MovieSetLandscapeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetlandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetlandscapeoverwrite = value
        End Set
    End Property

    Public Property MovieSetPosterOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviesetposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesetposteroverwrite = value
        End Set
    End Property

    Public Property MovieBannerPrefOnly() As Boolean
        Get
            Return Settings._XMLSettings.moviebannerprefonly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebannerprefonly = value
        End Set
    End Property

    Public Property MovieBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.moviebannerresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebannerresize = value
        End Set
    End Property

    Public Property MovieTrailerOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.movietraileroverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietraileroverwrite = value
        End Set
    End Property

    Public Property MovieThemeOverwrite() As Boolean
        Get
            Return Settings._XMLSettings.moviethemeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviethemeoverwrite = value
        End Set
    End Property
    Public Property MovieScraperPlotForOutline() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperplotforoutline
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperplotforoutline = value
        End Set
    End Property

    Public Property MovieScraperOutlineLimit() As Integer
        Get
            Return Settings._XMLSettings.moviescraperoutlinelimit
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviescraperoutlinelimit = value
        End Set
    End Property

    Public Property GeneralImagesGlassOverlay() As Boolean
        Get
            Return Settings._XMLSettings.generalimagesglassoverlay
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalimagesglassoverlay = value
        End Set
    End Property

    Public Property MoviePosterHeight() As Integer
        Get
            Return Settings._XMLSettings.movieposterheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieposterheight = value
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
            Return Settings._XMLSettings.movieposterprefonly
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposterprefonly = value
        End Set
    End Property

    Public Property MovieBannerQual() As Integer
        Get
            Return Settings._XMLSettings.moviebannerqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviebannerqual = value
        End Set
    End Property

    Public Property MovieSetBannerQual() As Integer
        Get
            Return Settings._XMLSettings.MovieSetBannerQual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetBannerQual = value
        End Set
    End Property

    Public Property MovieEThumbsQual() As Integer
        Get
            Return Settings._XMLSettings.movieethumbsqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieethumbsqual = value
        End Set
    End Property

    Public Property MovieEFanartsQual() As Integer
        Get
            Return Settings._XMLSettings.movieefanartsqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieefanartsqual = value
        End Set
    End Property

    Public Property MoviePosterQual() As Integer
        Get
            Return Settings._XMLSettings.movieposterqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieposterqual = value
        End Set
    End Property

    Public Property MovieSetPosterQual() As Integer
        Get
            Return Settings._XMLSettings.MovieSetPosterQual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.MovieSetPosterQual = value
        End Set
    End Property

    Public Property MoviePosterWidth() As Integer
        Get
            Return Settings._XMLSettings.movieposterwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieposterwidth = value
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
            Return Settings._XMLSettings.tvasposterprefsize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Settings._XMLSettings.tvasposterprefsize = value
        End Set
    End Property

    Public Property TVEpisodeFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.tvepisodefanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.tvepisodefanartprefsize = value
        End Set
    End Property

    Public Property MovieFanartPrefSize() As Enums.FanartSize
        Get
            Return Settings._XMLSettings.moviefanartprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.moviefanartprefsize = value
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
            Return Settings._XMLSettings.movieefanartsprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.movieefanartsprefsize = value
        End Set
    End Property

    Public Property MovieEThumbsPrefSize() As Enums.FanartSize
        Get
            Return Settings._XMLSettings.movieethumbsprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Settings._XMLSettings.movieethumbsprefsize = value
        End Set
    End Property

    Public Property MoviePosterPrefSize() As Enums.PosterSize
        Get
            Return Settings._XMLSettings.movieposterprefsize
        End Get
        Set(ByVal value As Enums.PosterSize)
            Settings._XMLSettings.movieposterprefsize = value
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
            Return Settings._XMLSettings.tvseasonfanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.tvseasonfanartprefsize = value
        End Set
    End Property

    Public Property TVASFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.tvasfanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.tvasfanartprefsize = value
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
            Return Settings._XMLSettings.tvseasonposterprefsize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Settings._XMLSettings.tvseasonposterprefsize = value
        End Set
    End Property

    Public Property TVShowBannerPrefType() As Enums.TVShowBannerType
        Get
            Return Settings._XMLSettings.tvshowbannerpreftype
        End Get
        Set(ByVal value As Enums.TVShowBannerType)
            Settings._XMLSettings.tvshowbannerpreftype = value
        End Set
    End Property

    Public Property MovieBannerPrefType() As Enums.MovieBannerType
        Get
            Return Settings._XMLSettings.moviebannerpreftype
        End Get
        Set(ByVal value As Enums.MovieBannerType)
            Settings._XMLSettings.moviebannerpreftype = value
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
            Return Settings._XMLSettings.tvasbannerpreftype
        End Get
        Set(ByVal value As Enums.TVShowBannerType)
            Settings._XMLSettings.tvasbannerpreftype = value
        End Set
    End Property

    Public Property TVSeasonBannerPrefType() As Enums.TVSeasonBannerType
        Get
            Return Settings._XMLSettings.tvseasonbannerpreftype
        End Get
        Set(ByVal value As Enums.TVSeasonBannerType)
            Settings._XMLSettings.tvseasonbannerpreftype = value
        End Set
    End Property

    Public Property TVShowFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Settings._XMLSettings.tvshowfanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Settings._XMLSettings.tvshowfanartprefsize = value
        End Set
    End Property

    Public Property TVShowPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Settings._XMLSettings.tvshowposterprefsize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Settings._XMLSettings.tvshowposterprefsize = value
        End Set
    End Property

    Public Property MovieTrailerMinQual() As Enums.TrailerQuality
        Get
            Return Settings._XMLSettings.movietrailerminqual
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            Settings._XMLSettings.movietrailerminqual = value
        End Set
    End Property

    Public Property MovieTrailerPrefQual() As Enums.TrailerQuality
        Get
            Return Settings._XMLSettings.movietrailerprefqual
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            Settings._XMLSettings.movietrailerprefqual = value
        End Set
    End Property

    Public Property MovieProperCase() As Boolean
        Get
            Return Settings._XMLSettings.moviepropercase
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviepropercase = value
        End Set
    End Property

    Public Property ProxyCreds() As NetworkCredential
        Get
            Return Settings._XMLSettings.proxycredentials
        End Get
        Set(ByVal value As NetworkCredential)
            Settings._XMLSettings.proxycredentials = value
        End Set
    End Property

    Public Property ProxyPort() As Integer
        Get
            Return Settings._XMLSettings.proxyport
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.proxyport = value
        End Set
    End Property

    Public Property ProxyURI() As String
        Get
            Return Settings._XMLSettings.proxyuri
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.proxyuri = value
        End Set
    End Property

    Public Property TVASBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.tvasbannerresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvasbannerresize = value
        End Set
    End Property

    Public Property TVASPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.tvasposterresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvasposterresize = value
        End Set
    End Property

    Public Property TVASFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.tvasfanartresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvasfanartresize = value
        End Set
    End Property

    Public Property TVEpisodeFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodefanartresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodefanartresize = value
        End Set
    End Property

    Public Property TVEpisodePosterResize() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodeposterresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodeposterresize = value
        End Set
    End Property

    Public Property MovieEFanartsResize() As Boolean
        Get
            Return Settings._XMLSettings.movieefanartsresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieefanartsresize = value
        End Set
    End Property

    Public Property MovieEThumbsResize() As Boolean
        Get
            Return Settings._XMLSettings.movieethumbsresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieethumbsresize = value
        End Set
    End Property

    Public Property MovieFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartresize = value
        End Set
    End Property

    Public Property MoviePosterResize() As Boolean
        Get
            Return Settings._XMLSettings.movieposterresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposterresize = value
        End Set
    End Property

    Public Property TVSeasonBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonbannerresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonbannerresize = value
        End Set
    End Property

    Public Property TVSeasonFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonfanartresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonfanartresize = value
        End Set
    End Property

    Public Property TVSeasonPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonposterresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonposterresize = value
        End Set
    End Property

    Public Property TVShowBannerResize() As Boolean
        Get
            Return Settings._XMLSettings.tvshowbannerresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowbannerresize = value
        End Set
    End Property

    Public Property TVShowFanartResize() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfanartresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfanartresize = value
        End Set
    End Property

    Public Property TVShowPosterResize() As Boolean
        Get
            Return Settings._XMLSettings.tvshowposterresize
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowposterresize = value
        End Set
    End Property

    Public Property MovieScraperDurationRuntimeFormat() As String
        Get
            Return Settings._XMLSettings.moviescraperdurationruntimeformat
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviescraperdurationruntimeformat = value
        End Set
    End Property

    Public Property TVScraperDurationRuntimeFormat() As String
        Get
            Return Settings._XMLSettings.tvscraperdurationruntimeformat
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.tvscraperdurationruntimeformat = value
        End Set
    End Property

    Public Property MovieScraperMetaDataScan() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapermetadatascan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapermetadatascan = value
        End Set
    End Property

    Public Property MovieScanOrderModify() As Boolean
        Get
            Return Settings._XMLSettings.moviescanordermodify
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescanordermodify = value
        End Set
    End Property

    Public Property TVScraperMetaDataScan() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapermetadatascan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapermetadatascan = value
        End Set
    End Property

    Public Property TVScraperEpisodeActors() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodeactors
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodeactors = value
        End Set
    End Property

    Public Property TVScraperEpisodeAired() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodeaired
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodeaired = value
        End Set
    End Property

    Public Property TVScraperEpisodeCredits() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodecredits
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodecredits = value
        End Set
    End Property

    Public Property TVScraperEpisodeDirector() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodedirector
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodedirector = value
        End Set
    End Property

    Public Property TVScraperEpisodeEpisode() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodeepisode
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodeepisode = value
        End Set
    End Property

    Public Property TVScraperEpisodePlot() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodeplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodeplot = value
        End Set
    End Property

    Public Property TVScraperEpisodeRating() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisoderating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisoderating = value
        End Set
    End Property

    Public Property TVScraperEpisodeSeason() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodeseason
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodeseason = value
        End Set
    End Property

    Public Property TVScraperEpisodeTitle() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperepisodetitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperepisodetitle = value
        End Set
    End Property

    Public Property TVScraperShowActors() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowactors
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowactors = value
        End Set
    End Property

    Public Property TVScraperShowEpiGuideURL() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowepiguideurl
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowepiguideurl = value
        End Set
    End Property

    Public Property TVScraperShowGenre() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowgenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowgenre = value
        End Set
    End Property

    Public Property TVScraperShowMPAA() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowmpaa
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowmpaa = value
        End Set
    End Property

    Public Property TVScraperShowPlot() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowplot = value
        End Set
    End Property

    Public Property TVScraperShowPremiered() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowpremiered
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowpremiered = value
        End Set
    End Property

    Public Property TVScraperShowRating() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowrating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowrating = value
        End Set
    End Property

    Public Property TVScraperShowStatus() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowstatus
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowstatus = value
        End Set
    End Property

    Public Property TVScraperShowStudio() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowstudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowstudio = value
        End Set
    End Property

    Public Property TVScraperShowTitle() As Boolean
        Get
            Return Settings._XMLSettings.tvscrapershowtitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscrapershowtitle = value
        End Set
    End Property

    Public Property TVSeasonFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonfanartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonfanartcol = value
        End Set
    End Property

    Public Property TVSeasonFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.tvseasonfanartheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonfanartheight = value
        End Set
    End Property

    Public Property TVSeasonFanartQual() As Integer
        Get
            Return Settings._XMLSettings.tvseasonfanartqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonfanartqual = value
        End Set
    End Property

    Public Property TVSeasonFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.tvseasonfanartwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonfanartwidth = value
        End Set
    End Property

    Public Property TVSeasonBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonbannercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonbannercol = value
        End Set
    End Property

    Public Property TVASBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.tvasbannerwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasbannerwidth = value
        End Set
    End Property

    Public Property TVSeasonBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.tvseasonbannerwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonbannerwidth = value
        End Set
    End Property

    Public Property TVASFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.tvasfanartwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasfanartwidth = value
        End Set
    End Property

    Public Property TVShowBannerWidth() As Integer
        Get
            Return Settings._XMLSettings.tvshowbannerwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowbannerwidth = value
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
            Return Settings._XMLSettings.tvasbannerheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasbannerheight = value
        End Set
    End Property

    Public Property TVSeasonBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.tvseasonbannerheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonbannerheight = value
        End Set
    End Property

    Public Property TVASFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.tvasfanartheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvasfanartheight = value
        End Set
    End Property

    Public Property TVShowBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.tvshowbannerheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowbannerheight = value
        End Set
    End Property

    Public Property MovieBannerHeight() As Integer
        Get
            Return Settings._XMLSettings.moviebannerheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.moviebannerheight = value
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
            Return Settings._XMLSettings.tvseasonpostercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonpostercol = value
        End Set
    End Property

    Public Property TVSeasonPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.tvseasonposterheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonposterheight = value
        End Set
    End Property

    Public Property TVSeasonPosterQual() As Integer
        Get
            Return Settings._XMLSettings.tvseasonposterqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonposterqual = value
        End Set
    End Property

    Public Property TVSeasonPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.tvseasonposterwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvseasonposterwidth = value
        End Set
    End Property

    Public Property MovieSets() As List(Of String)
        Get
            Return Settings._XMLSettings.moviesets
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.moviesets = value
        End Set
    End Property

    Public Property GeneralShowImgDims() As Boolean
        Get
            Return Settings._XMLSettings.generalshowimgdims
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalshowimgdims = value
        End Set
    End Property

    Public Property TVShowFanartCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfanartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfanartcol = value
        End Set
    End Property

    Public Property TVShowFanartHeight() As Integer
        Get
            Return Settings._XMLSettings.tvshowfanartheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowfanartheight = value
        End Set
    End Property

    Public Property TVShowFanartQual() As Integer
        Get
            Return Settings._XMLSettings.tvshowfanartqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowfanartqual = value
        End Set
    End Property

    Public Property TVShowFanartWidth() As Integer
        Get
            Return Settings._XMLSettings.tvshowfanartwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowfanartwidth = value
        End Set
    End Property

    Public Property TVShowFilterCustom() As List(Of String)
        Get
            Return Settings._XMLSettings.tvshowfiltercustom
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.tvshowfiltercustom = value
        End Set
    End Property

    Public Property GeneralTVShowInfoPanelState() As Integer
        Get
            Return Settings._XMLSettings.generaltvshowinfopanelstate
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.generaltvshowinfopanelstate = value
        End Set
    End Property

    Public Property TVLockShowGenre() As Boolean
        Get
            Return Settings._XMLSettings.tvlockshowgenre
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockshowgenre = value
        End Set
    End Property

    Public Property TVLockShowPlot() As Boolean
        Get
            Return Settings._XMLSettings.tvlockshowplot
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockshowplot = value
        End Set
    End Property

    Public Property TVLockShowRating() As Boolean
        Get
            Return Settings._XMLSettings.tvlockshowrating
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockshowrating = value
        End Set
    End Property

    Public Property TVLockShowStatus() As Boolean
        Get
            Return Settings._XMLSettings.tvlockshowstatus
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockshowstatus = value
        End Set
    End Property

    Public Property TVLockShowStudio() As Boolean
        Get
            Return Settings._XMLSettings.tvlockshowstudio
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockshowstudio = value
        End Set
    End Property

    Public Property TVLockShowTitle() As Boolean
        Get
            Return Settings._XMLSettings.tvlockshowtitle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvlockshowtitle = value
        End Set
    End Property

    Public Property TVShowBannerCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowbannercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowbannercol = value
        End Set
    End Property

    Public Property TVShowCharacterArtCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowcharacterartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowcharacterartcol = value
        End Set
    End Property

    Public Property TVShowClearArtCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowclearartcol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowclearartcol = value
        End Set
    End Property

    Public Property TVShowClearLogoCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowclearlogocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowclearlogocol = value
        End Set
    End Property

    Public Property TVShowEFanartsCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowefanartscol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowefanartscol = value
        End Set
    End Property

    Public Property TVShowLandscapeCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowlandscapecol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowlandscapecol = value
        End Set
    End Property

    Public Property TVShowThemeCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowthemecol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowthemecol = value
        End Set
    End Property

    Public Property TVShowNfoCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshownfocol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshownfocol = value
        End Set
    End Property

    Public Property TVShowPosterCol() As Boolean
        Get
            Return Settings._XMLSettings.tvshowpostercol
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowpostercol = value
        End Set
    End Property

    Public Property TVShowPosterHeight() As Integer
        Get
            Return Settings._XMLSettings.tvshowposterheight
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowposterheight = value
        End Set
    End Property

    Public Property TVShowPosterQual() As Integer
        Get
            Return Settings._XMLSettings.tvshowposterqual
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowposterqual = value
        End Set
    End Property

    Public Property TVShowPosterWidth() As Integer
        Get
            Return Settings._XMLSettings.tvshowposterwidth
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvshowposterwidth = value
        End Set
    End Property

    Public Property TVShowProperCase() As Boolean
        Get
            Return Settings._XMLSettings.tvshowpropercase
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowpropercase = value
        End Set
    End Property

    Public Property TVScraperRatingRegion() As String
        Get
            Return Settings._XMLSettings.tvscraperratingregion
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.tvscraperratingregion = value
        End Set
    End Property

    Public Property MovieSkipLessThan() As Integer
        Get
            Return Settings._XMLSettings.movieskiplessthan
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.movieskiplessthan = value
        End Set
    End Property

    Public Property MovieSkipStackedSizeCheck() As Boolean
        Get
            Return Settings._XMLSettings.movieskipstackedsizecheck
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieskipstackedsizecheck = value
        End Set
    End Property

    Public Property TVSkipLessThan() As Integer
        Get
            Return Settings._XMLSettings.tvskiplessthan
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.tvskiplessthan = value
        End Set
    End Property

    Public Property MovieSortBeforeScan() As Boolean
        Get
            Return Settings._XMLSettings.moviesortbeforescan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviesortbeforescan = value
        End Set
    End Property

    Public Property SortPath() As String
        Get
            Return Settings._XMLSettings.sortpath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.sortpath = value
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

    Public Property GeneralSourceFromFolder() As Boolean
        Get
            Return Settings._XMLSettings.generalsourcefromfolder
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.generalsourcefromfolder = value
        End Set
    End Property

    Public Property GeneralMainFilterSortDate() As String
        Get
            Return Settings._XMLSettings.GeneralMainFilterSortDate
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralMainFilterSortDate = value
        End Set
    End Property

    Public Property GeneralMainFilterSortTitle() As String
        Get
            Return Settings._XMLSettings.GeneralMainFilterSortTitle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralMainFilterSortTitle = value
        End Set
    End Property
    Public Property GeneralMainFilterIMDBRating() As String
        Get
            Return Settings._XMLSettings.GeneralMainFilterIMDBRating
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.GeneralMainFilterIMDBRating = value
        End Set
    End Property
    Public Property GeneralMainSplitterPanelState() As Integer
        Get
            Return Settings._XMLSettings.generalmainsplitterpanelstate
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.generalmainsplitterpanelstate = value
        End Set
    End Property
    Public Property GeneralShowSplitterPanelState() As Integer
        Get
            Return Settings._XMLSettings.generalshowsplitterpanelstate
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.generalshowsplitterpanelstate = value
        End Set
    End Property

    Public Property GeneralSeasonSplitterPanelState() As Integer
        Get
            Return Settings._XMLSettings.generalseasonsplitterpanelstate
        End Get
        Set(ByVal value As Integer)
            Settings._XMLSettings.generalseasonsplitterpanelstate = value
        End Set
    End Property

    Public Property TVCleanDB() As Boolean
        Get
            Return Settings._XMLSettings.tvcleandb
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvcleandb = value
        End Set
    End Property

    Public Property GeneralTVEpisodeTheme() As String
        Get
            Return Settings._XMLSettings.generaltvepisodetheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generaltvepisodetheme = value
        End Set
    End Property

    Public Property TVGeneralFlagLang() As String
        Get
            Return Settings._XMLSettings.tvgeneralflaglang
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.tvgeneralflaglang = value
        End Set
    End Property

    Public Property TVGeneralIgnoreLastScan() As Boolean
        Get
            Return Settings._XMLSettings.tvgeneralignorelastscan
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvgeneralignorelastscan = value
        End Set
    End Property

    Public Property TVMetadataPerFileType() As List(Of MetadataPerType)
        Get
            Return Settings._XMLSettings.tvmetadataperfiletype
        End Get
        Set(ByVal value As List(Of MetadataPerType))
            Settings._XMLSettings.tvmetadataperfiletype = value
        End Set
    End Property

    Public Property TVScanOrderModify() As Boolean
        Get
            Return Settings._XMLSettings.tvscanordermodify
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscanordermodify = value
        End Set
    End Property

    Public Property TVShowRegexes() As List(Of TVShowRegEx)
        Get
            Return Settings._XMLSettings.tvshowregexes
        End Get
        Set(ByVal value As List(Of TVShowRegEx))
            Settings._XMLSettings.tvshowregexes = value
        End Set
    End Property

    Public Property GeneralTVShowTheme() As String
        Get
            Return Settings._XMLSettings.generaltvshowtheme
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.generaltvshowtheme = value
        End Set
    End Property

    Public Property TVScraperUpdateTime() As Enums.TVScraperUpdateTime
        Get
            Return Settings._XMLSettings.tvscraperupdatetime
        End Get
        Set(ByVal value As Enums.TVScraperUpdateTime)
            Settings._XMLSettings.tvscraperupdatetime = value
        End Set
    End Property

    Public Property MovieTrailerEnable() As Boolean
        Get
            Return Settings._XMLSettings.movietrailerenable
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietrailerenable = value
        End Set
    End Property

    Public Property MovieThemeEnable() As Boolean
        Get
            Return Settings._XMLSettings.moviethemeenable
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviethemeenable = value
        End Set
    End Property

    Public Property MovieScraperCertForMPAA() As Boolean
        Get
            Return Settings._XMLSettings.moviescrapercertformpaa
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescrapercertformpaa = value
        End Set
    End Property

    Public Property MovieScraperUseMDDuration() As Boolean
        Get
            Return Settings._XMLSettings.moviescraperusemdduration
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviescraperusemdduration = value
        End Set
    End Property

    Public Property TVScraperUseMDDuration() As Boolean
        Get
            Return Settings._XMLSettings.tvscraperusemdduration
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvscraperusemdduration = value
        End Set
    End Property

    Public Property FileSystemValidExts() As List(Of String)
        Get
            Return Settings._XMLSettings.filesystemvalidexts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.filesystemvalidexts = value
        End Set
    End Property

    Public Property FileSystemValidThemeExts() As List(Of String)
        Get
            Return Settings._XMLSettings.filesystemvalidthemeexts
        End Get
        Set(ByVal value As List(Of String))
            Settings._XMLSettings.filesystemvalidthemeexts = value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return Settings._XMLSettings.version
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.version = value
        End Set
    End Property

    Public Property GeneralWindowLoc() As Point
        Get
            Return Settings._XMLSettings.generalwindowloc
        End Get
        Set(ByVal value As Point)
            Settings._XMLSettings.generalwindowloc = value
        End Set
    End Property

    Public Property GeneralWindowSize() As Size
        Get
            Return Settings._XMLSettings.generalwindowsize
        End Get
        Set(ByVal value As Size)
            Settings._XMLSettings.generalwindowsize = value
        End Set
    End Property

    Public Property GeneralWindowState() As FormWindowState
        Get
            Return Settings._XMLSettings.generalwindowstate
        End Get
        Set(ByVal value As FormWindowState)
            Settings._XMLSettings.generalwindowstate = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return Settings._XMLSettings.username
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Settings._XMLSettings.password
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.password = value
        End Set
    End Property

    Public Property TraktUsername() As String
        Get
            Return Settings._XMLSettings.traktusername
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.traktusername = value
        End Set
    End Property

    Public Property TraktPassword() As String
        Get
            Return Settings._XMLSettings.traktpassword
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.traktpassword = value
        End Set
    End Property

    Public Property UseTrakt() As Boolean
        Get
            Return Settings._XMLSettings.usetrakt
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.usetrakt = value
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
            Settings._XMLSettings.movieusefrodo = value
        End Set
    End Property

    Public Property MovieActorThumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbsfrodo = value
        End Set
    End Property

    Public Property MovieBannerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.moviebannerfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebannerfrodo = value
        End Set
    End Property

    Public Property MovieClearArtFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movieclearartfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearartfrodo = value
        End Set
    End Property

    Public Property MovieClearLogoFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movieclearlogofrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearlogofrodo = value
        End Set
    End Property

    Public Property MovieDiscArtFrodo() As Boolean
        Get
            Return Settings._XMLSettings.moviediscartfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviediscartfrodo = value
        End Set
    End Property

    Public Property MovieExtrafanartsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movieextrafanartsfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrafanartsfrodo = value
        End Set
    End Property

    Public Property MovieExtrathumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movieextrathumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrathumbsfrodo = value
        End Set
    End Property

    Public Property MovieFanartFrodo() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartfrodo = value
        End Set
    End Property

    Public Property MovieLandscapeFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movielandscapefrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielandscapefrodo = value
        End Set
    End Property

    Public Property MovieNFOFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movienfofrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienfofrodo = value
        End Set
    End Property

    Public Property MoviePosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movieposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposterfrodo = value
        End Set
    End Property

    Public Property MovieTrailerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.movietrailerfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietrailerfrodo = value
        End Set
    End Property

    Public Property MovieUseEden() As Boolean
        Get
            Return Settings._XMLSettings.movieuseeden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieuseeden = value
        End Set
    End Property

    Public Property MovieActorThumbsEden() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbseden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbseden = value
        End Set
    End Property

    Public Property MovieBannerEden() As Boolean
        Get
            Return Settings._XMLSettings.moviebannereden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebannereden = value
        End Set
    End Property

    Public Property MovieClearArtEden() As Boolean
        Get
            Return Settings._XMLSettings.moviecleararteden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviecleararteden = value
        End Set
    End Property

    Public Property MovieClearLogoEden() As Boolean
        Get
            Return Settings._XMLSettings.movieclearlogoeden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieclearlogoeden = value
        End Set
    End Property

    Public Property MovieDiscArtEden() As Boolean
        Get
            Return Settings._XMLSettings.moviediscarteden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviediscarteden = value
        End Set
    End Property

    Public Property MovieExtrafanartsEden() As Boolean
        Get
            Return Settings._XMLSettings.movieextrafanartseden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrafanartseden = value
        End Set
    End Property

    Public Property MovieExtrathumbsEden() As Boolean
        Get
            Return Settings._XMLSettings.movieextrathumbseden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrathumbseden = value
        End Set
    End Property

    Public Property MovieFanartEden() As Boolean
        Get
            Return Settings._XMLSettings.moviefanarteden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanarteden = value
        End Set
    End Property

    Public Property MovieLandscapeEden() As Boolean
        Get
            Return Settings._XMLSettings.movielandscapeeden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movielandscapeeden = value
        End Set
    End Property

    Public Property MovieNFOEden() As Boolean
        Get
            Return Settings._XMLSettings.movienfoeden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienfoeden = value
        End Set
    End Property

    Public Property MoviePosterEden() As Boolean
        Get
            Return Settings._XMLSettings.moviepostereden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviepostereden = value
        End Set
    End Property

    Public Property MovieTrailerEden() As Boolean
        Get
            Return Settings._XMLSettings.movietrailereden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietrailereden = value
        End Set
    End Property

    Public Property MovieXBMCThemeEnable() As Boolean
        Get
            Return Settings._XMLSettings.moviexbmcthemeenable
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviexbmcthemeenable = value
        End Set
    End Property

    Public Property MovieXBMCThemeCustom() As Boolean
        Get
            Return Settings._XMLSettings.moviexbmcthemecustom
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviexbmcthemecustom = value
        End Set
    End Property

    Public Property MovieXBMCThemeMovie() As Boolean
        Get
            Return Settings._XMLSettings.moviexbmcthememovie
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviexbmcthememovie = value
        End Set
    End Property

    Public Property MovieXBMCThemeSub() As Boolean
        Get
            Return Settings._XMLSettings.moviexbmcthemesub
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviexbmcthemesub = value
        End Set
    End Property

    Public Property MovieXBMCThemeCustomPath() As String
        Get
            Return Settings._XMLSettings.moviexbmcthemecustompath
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviexbmcthemecustompath = value
        End Set
    End Property

    Public Property MovieXBMCThemeSubDir() As String
        Get
            Return Settings._XMLSettings.moviexbmcthemesubdir
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviexbmcthemesubdir = value
        End Set
    End Property

    Public Property MovieXBMCTrailerFormat() As Boolean
        Get
            Return Settings._XMLSettings.moviexbmctrailerformat
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviexbmctrailerformat = value
        End Set
    End Property

    Public Property MovieXBMCProtectVTSBDMV() As Boolean
        Get
            Return Settings._XMLSettings.moviexbmcprotectvtsbdmv
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviexbmcprotectvtsbdmv = value
        End Set
    End Property

    Public Property MovieUseYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.movieuseyamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieuseyamj = value
        End Set
    End Property

    Public Property MovieBannerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.moviebanneryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebanneryamj = value
        End Set
    End Property

    Public Property MovieFanartYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartyamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartyamj = value
        End Set
    End Property

    Public Property MovieNFOYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.movienfoyamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienfoyamj = value
        End Set
    End Property

    Public Property MoviePosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.movieposteryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposteryamj = value
        End Set
    End Property

    Public Property MovieTrailerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.movietraileryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietraileryamj = value
        End Set
    End Property

    Public Property MovieYAMJCompatibleSets() As Boolean
        Get
            Return Settings._XMLSettings.movieyamjcompatiblesets
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieyamjcompatiblesets = value
        End Set
    End Property

    Public Property MovieYAMJWatchedFile() As Boolean
        Get
            Return Settings._XMLSettings.movieyamjwatchedfile
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieyamjwatchedfile = value
        End Set
    End Property

    Public Property MovieYAMJWatchedFolder() As String
        Get
            Return Settings._XMLSettings.movieyamjwatchedfolder
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieyamjwatchedfolder = value
        End Set
    End Property

    Public Property MovieUseNMJ() As Boolean
        Get
            Return Settings._XMLSettings.movieusenmj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieusenmj = value
        End Set
    End Property

    Public Property MovieBannerNMJ() As Boolean
        Get
            Return Settings._XMLSettings.moviebannernmj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviebannernmj = value
        End Set
    End Property

    Public Property MovieFanartNMJ() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartnmj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartnmj = value
        End Set
    End Property

    Public Property MovieNFONMJ() As Boolean
        Get
            Return Settings._XMLSettings.movienfonmj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienfonmj = value
        End Set
    End Property

    Public Property MoviePosterNMJ() As Boolean
        Get
            Return Settings._XMLSettings.movieposternmj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposternmj = value
        End Set
    End Property

    Public Property MovieTrailerNMJ() As Boolean
        Get
            Return Settings._XMLSettings.movietrailernmj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movietrailernmj = value
        End Set
    End Property

    Public Property MovieUseBoxee() As Boolean
        Get
            Return Settings._XMLSettings.movieuseboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieuseboxee = value
        End Set
    End Property

    Public Property MovieFanartBoxee() As Boolean
        Get
            Return Settings._XMLSettings.moviefanartboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviefanartboxee = value
        End Set
    End Property

    Public Property MovieNFOBoxee() As Boolean
        Get
            Return Settings._XMLSettings.movienfoboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movienfoboxee = value
        End Set
    End Property

    Public Property MoviePosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.movieposterboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieposterboxee = value
        End Set
    End Property

    Public Property MovieUseExpert() As Boolean
        Get
            Return Settings._XMLSettings.movieuseexpert
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieuseexpert = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbsexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbsexpertsingle = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertSingle() As String
        Get
            Return Settings._XMLSettings.movieactorthumbsextexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieactorthumbsextexpertsingle = value
        End Set
    End Property

    Public Property MovieBannerExpertSingle() As String
        Get
            Return Settings._XMLSettings.moviebannerexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviebannerexpertsingle = value
        End Set
    End Property

    Public Property MovieClearArtExpertSingle() As String
        Get
            Return Settings._XMLSettings.movieclearartexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearartexpertsingle = value
        End Set
    End Property

    Public Property MovieClearLogoExpertSingle() As String
        Get
            Return Settings._XMLSettings.movieclearlogoexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearlogoexpertsingle = value
        End Set
    End Property

    Public Property MovieDiscArtExpertSingle() As String
        Get
            Return Settings._XMLSettings.moviediscartexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviediscartexpertsingle = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.movieextrafanartsexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrafanartsexpertsingle = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.movieextrathumbsexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrathumbsexpertsingle = value
        End Set
    End Property

    Public Property MovieFanartExpertSingle() As String
        Get
            Return Settings._XMLSettings.moviefanartexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviefanartexpertsingle = value
        End Set
    End Property

    Public Property MovieLandscapeExpertSingle() As String
        Get
            Return Settings._XMLSettings.movielandscapeexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movielandscapeexpertsingle = value
        End Set
    End Property

    Public Property MovieNFOExpertSingle() As String
        Get
            Return Settings._XMLSettings.movienfoexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movienfoexpertsingle = value
        End Set
    End Property

    Public Property MoviePosterExpertSingle() As String
        Get
            Return Settings._XMLSettings.movieposterexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieposterexpertsingle = value
        End Set
    End Property

    Public Property MovieStackExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.moviestackexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviestackexpertsingle = value
        End Set
    End Property

    Public Property MovieTrailerExpertSingle() As String
        Get
            Return Settings._XMLSettings.movietrailerexpertsingle
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movietrailerexpertsingle = value
        End Set
    End Property

    Public Property MovieUnstackExpertSingle() As Boolean
        Get
            Return Settings._XMLSettings.movieunstackexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieunstackexpertsingle = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertMulti() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbsexpertmulti
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbsexpertmulti = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertMulti() As String
        Get
            Return Settings._XMLSettings.movieactorthumbsextexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieactorthumbsextexpertmulti = value
        End Set
    End Property

    Public Property MovieBannerExpertMulti() As String
        Get
            Return Settings._XMLSettings.moviebannerexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviebannerexpertmulti = value
        End Set
    End Property

    Public Property MovieClearArtExpertMulti() As String
        Get
            Return Settings._XMLSettings.movieclearartexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearartexpertmulti = value
        End Set
    End Property

    Public Property MovieClearLogoExpertMulti() As String
        Get
            Return Settings._XMLSettings.movieclearlogoexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearlogoexpertmulti = value
        End Set
    End Property

    Public Property MovieDiscArtExpertMulti() As String
        Get
            Return Settings._XMLSettings.moviediscartexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviediscartexpertmulti = value
        End Set
    End Property

    Public Property MovieFanartExpertMulti() As String
        Get
            Return Settings._XMLSettings.moviefanartexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviefanartexpertmulti = value
        End Set
    End Property

    Public Property MovieLandscapeExpertMulti() As String
        Get
            Return Settings._XMLSettings.movielandscapeexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movielandscapeexpertmulti = value
        End Set
    End Property

    Public Property MovieNFOExpertMulti() As String
        Get
            Return Settings._XMLSettings.movienfoexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movienfoexpertmulti = value
        End Set
    End Property

    Public Property MoviePosterExpertMulti() As String
        Get
            Return Settings._XMLSettings.movieposterexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieposterexpertmulti = value
        End Set
    End Property

    Public Property MovieStackExpertMulti() As Boolean
        Get
            Return Settings._XMLSettings.moviestackexpertmulti
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.moviestackexpertmulti = value
        End Set
    End Property

    Public Property MovieTrailerExpertMulti() As String
        Get
            Return Settings._XMLSettings.movietrailerexpertmulti
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movietrailerexpertmulti = value
        End Set
    End Property

    Public Property MovieUnstackExpertMulti() As Boolean
        Get
            Return Settings._XMLSettings.movieunstackexpertmulti
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieunstackexpertmulti = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbsexpertvts = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertVTS() As String
        Get
            Return Settings._XMLSettings.movieactorthumbsextexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieactorthumbsextexpertvts = value
        End Set
    End Property

    Public Property MovieBannerExpertVTS() As String
        Get
            Return Settings._XMLSettings.moviebannerexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviebannerexpertvts = value
        End Set
    End Property

    Public Property MovieClearArtExpertVTS() As String
        Get
            Return Settings._XMLSettings.movieclearartexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearartexpertvts = value
        End Set
    End Property

    Public Property MovieClearLogoExpertVTS() As String
        Get
            Return Settings._XMLSettings.movieclearlogoexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearlogoexpertvts = value
        End Set
    End Property

    Public Property MovieDiscArtExpertVTS() As String
        Get
            Return Settings._XMLSettings.moviediscartexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviediscartexpertvts = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.movieextrafanartsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrafanartsexpertvts = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.movieextrathumbsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrathumbsexpertvts = value
        End Set
    End Property

    Public Property MovieFanartExpertVTS() As String
        Get
            Return Settings._XMLSettings.moviefanartexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviefanartexpertvts = value
        End Set
    End Property

    Public Property MovieLandscapeExpertVTS() As String
        Get
            Return Settings._XMLSettings.movielandscapeexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movielandscapeexpertvts = value
        End Set
    End Property

    Public Property MovieNFOExpertVTS() As String
        Get
            Return Settings._XMLSettings.movienfoexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movienfoexpertvts = value
        End Set
    End Property

    Public Property MoviePosterExpertVTS() As String
        Get
            Return Settings._XMLSettings.movieposterexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieposterexpertvts = value
        End Set
    End Property

    Public Property MovieRecognizeVTSExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.movierecognizevtsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movierecognizevtsexpertvts = value
        End Set
    End Property

    Public Property MovieTrailerExpertVTS() As String
        Get
            Return Settings._XMLSettings.movietrailerexpertvts
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movietrailerexpertvts = value
        End Set
    End Property

    Public Property MovieUseBaseDirectoryExpertVTS() As Boolean
        Get
            Return Settings._XMLSettings.movieusebasedirectoryexpertvts
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieusebasedirectoryexpertvts = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.movieactorthumbsexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieactorthumbsexpertbdmv = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movieactorthumbsextexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieactorthumbsextexpertbdmv = value
        End Set
    End Property

    Public Property MovieBannerExpertBDMV() As String
        Get
            Return Settings._XMLSettings.moviebannerexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviebannerexpertbdmv = value
        End Set
    End Property

    Public Property MovieClearArtExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movieclearartexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearartexpertbdmv = value
        End Set
    End Property

    Public Property MovieClearLogoExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movieclearlogoexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieclearlogoexpertbdmv = value
        End Set
    End Property

    Public Property MovieDiscArtExpertBDMV() As String
        Get
            Return Settings._XMLSettings.moviediscartexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviediscartexpertbdmv = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.movieextrafanartsexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrafanartsexpertbdmv = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.movieextrathumbsexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieextrathumbsexpertbdmv = value
        End Set
    End Property

    Public Property MovieFanartExpertBDMV() As String
        Get
            Return Settings._XMLSettings.moviefanartexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.moviefanartexpertbdmv = value
        End Set
    End Property

    Public Property MovieLandscapeExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movielandscapeexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movielandscapeexpertbdmv = value
        End Set
    End Property

    Public Property MovieNFOExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movienfoexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movienfoexpertbdmv = value
        End Set
    End Property

    Public Property MoviePosterExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movieposterexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movieposterexpertbdmv = value
        End Set
    End Property

    Public Property MovieTrailerExpertBDMV() As String
        Get
            Return Settings._XMLSettings.movietrailerexpertbdmv
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.movietrailerexpertbdmv = value
        End Set
    End Property

    Public Property MovieUseBaseDirectoryExpertBDMV() As Boolean
        Get
            Return Settings._XMLSettings.movieusebasedirectoryexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.movieusebasedirectoryexpertbdmv = value
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

    Public Property MovieSetDiscArtMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetDiscArtMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetDiscArtMSAA = value
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

    Public Property MovieSetPosterMSAA() As Boolean
        Get
            Return Settings._XMLSettings.MovieSetPosterMSAA
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.MovieSetPosterMSAA = value
        End Set
    End Property

    Public Property TVUseBoxee() As Boolean
        Get
            Return Settings._XMLSettings.tvuseboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvuseboxee = value
        End Set
    End Property

    Public Property TVUseEden() As Boolean
        Get
            Return Settings._XMLSettings.tvuseeden
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvuseeden = value
        End Set
    End Property

    Public Property TVUseFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvusefrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvusefrodo = value
        End Set
    End Property

    Public Property TVUseYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvuseyamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvuseyamj = value
        End Set
    End Property

    Public Property TVShowBannerBoxee() As Boolean
        Get
            Return Settings._XMLSettings.tvshowbannerboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowbannerboxee = value
        End Set
    End Property

    Public Property TVShowBannerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvshowbannerfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowbannerfrodo = value
        End Set
    End Property

    Public Property TVShowBannerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvshowbanneryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowbanneryamj = value
        End Set
    End Property

    Public Property TVShowExtrafanartsXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvshowextrafanartsxbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowextrafanartsxbmc = value
        End Set
    End Property

    Public Property TVShowFanartBoxee() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfanartboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfanartboxee = value
        End Set
    End Property

    Public Property TVShowFanartFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfanartfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfanartfrodo = value
        End Set
    End Property

    Public Property TVShowFanartYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvshowfanartyamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowfanartyamj = value
        End Set
    End Property

    Public Property TVShowPosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.tvshowposterboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowposterboxee = value
        End Set
    End Property

    Public Property TVShowPosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvshowposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowposterfrodo = value
        End Set
    End Property

    Public Property TVShowPosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvshowposteryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowposteryamj = value
        End Set
    End Property

    Public Property TVShowActorThumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvshowactorthumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowactorthumbsfrodo = value
        End Set
    End Property

    Public Property TVSeasonPosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonposterboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonposterboxee = value
        End Set
    End Property

    Public Property TVSeasonPosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonposterfrodo = value
        End Set
    End Property

    Public Property TVSeasonPosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonposteryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonposteryamj = value
        End Set
    End Property

    Public Property TVSeasonFanartFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonfanartfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonfanartfrodo = value
        End Set
    End Property

    Public Property TVSeasonFanartYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonfanartyamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonfanartyamj = value
        End Set
    End Property

    Public Property TVSeasonBannerFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonbannerfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonbannerfrodo = value
        End Set
    End Property

    Public Property TVSeasonBannerYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonbanneryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonbanneryamj = value
        End Set
    End Property

    Public Property TVEpisodePosterBoxee() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodeposterboxee
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodeposterboxee = value
        End Set
    End Property

    Public Property TVEpisodePosterFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodeposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodeposterfrodo = value
        End Set
    End Property

    Public Property TVEpisodePosterYAMJ() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodeposteryamj
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodeposteryamj = value
        End Set
    End Property

    Public Property TVEpisodeActorThumbsFrodo() As Boolean
        Get
            Return Settings._XMLSettings.tvepisodeactorthumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvepisodeactorthumbsfrodo = value
        End Set
    End Property

    Public Property TVShowClearLogoXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvshowclearlogoxbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowclearlogoxbmc = value
        End Set
    End Property

    Public Property TVShowClearArtXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvshowclearartxbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowclearartxbmc = value
        End Set
    End Property

    Public Property TVShowCharacterArtXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvshowcharacterartxbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowcharacterartxbmc = value
        End Set
    End Property

    Public Property TVShowTVThemeXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvshowtvthemexbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowtvthemexbmc = value
        End Set
    End Property

    Public Property TVShowTVThemeFolderXBMC() As String
        Get
            Return Settings._XMLSettings.tvshowtvthemefolderxbmc
        End Get
        Set(ByVal value As String)
            Settings._XMLSettings.tvshowtvthemefolderxbmc = value
        End Set
    End Property

    Public Property TVShowLandscapeXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvshowlandscapexbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvshowlandscapexbmc = value
        End Set
    End Property

    Public Property TVSeasonLandscapeXBMC() As Boolean
        Get
            Return Settings._XMLSettings.tvseasonlandscapexbmc
        End Get
        Set(ByVal value As Boolean)
            Settings._XMLSettings.tvseasonlandscapexbmc = value
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
            Master.eSettings.MovieBannerFrodo = True
            Master.eSettings.MovieClearArtFrodo = True
            Master.eSettings.MovieClearLogoFrodo = True
            Master.eSettings.MovieDiscArtFrodo = True
            Master.eSettings.MovieExtrafanartsFrodo = True
            Master.eSettings.MovieExtrathumbsFrodo = True
            Master.eSettings.MovieFanartFrodo = True
            Master.eSettings.MovieLandscapeFrodo = True
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

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidExts.Count <= 0) Then
            Master.eSettings.FileSystemValidExts.Clear()
            Master.eSettings.FileSystemValidExts.AddRange(Strings.Split(".avi,.divx,.mkv,.iso,.mpg,.mp4,.mpeg,.wmv,.wma,.mov,.mts,.m2t,.img,.dat,.bin,.cue,.ifo,.vob,.dvb,.evo,.asf,.asx,.avs,.nsv,.ram,.ogg,.ogm,.ogv,.flv,.swf,.nut,.viv,.rar,.m2ts,.dvr-ms,.ts,.m4v,.rmvb,.webm,.disc,.3gpp", ","))
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
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 3, .SeasonRegex = "^(?<season>specials?)$", .SeasonFromDirectory = True, .EpisodeRegex = "[^a-zA-Z]e(pisode[\W_]*)?(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromFilename})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 4, .SeasonRegex = "^(s(eason)?)?[\W_]*(?<season>[0-9]+)$", .SeasonFromDirectory = True, .EpisodeRegex = "[^a-zA-Z]e(pisode[\W_]*)?(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromFilename})
            Master.eSettings.TVShowRegexes.Add(New TVShowRegEx With {.ID = 5, .SeasonRegex = "[^\w]s(eason)?[\W_]*(?<season>[0-9]+)", .SeasonFromDirectory = True, .EpisodeRegex = "[^a-zA-Z]e(pisode[\W_]*)?(?<episode>[0-9]+)", .EpisodeRetrieve = EpRetrieve.FromFilename})
        End If
    End Sub

    Public Function MovieActorThumbsAnyEnabled() As Boolean
        Return MovieActorThumbsEden OrElse MovieActorThumbsFrodo OrElse _
            (MovieUseExpert AndAlso ((MovieActorThumbsExpertBDMV AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertBDMV)) OrElse (MovieActorThumbsExpertMulti AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertMulti)) OrElse (MovieActorThumbsExpertSingle AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertSingle)) OrElse (MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertVTS))))
    End Function

    Public Function MovieBannerAnyEnabled() As Boolean
        Return MovieBannerEden OrElse MovieBannerFrodo OrElse MovieBannerNMJ OrElse MovieBannerYAMJ OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieBannerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieBannerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieBannerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieBannerExpertVTS)))
    End Function

    Public Function MovieClearArtAnyEnabled() As Boolean
        Return MovieClearArtEden OrElse MovieClearArtFrodo OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertVTS)))
    End Function

    Public Function MovieClearLogoAnyEnabled() As Boolean
        Return MovieClearLogoEden OrElse MovieClearLogoFrodo OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearLogoExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertVTS)))
    End Function

    Public Function MovieDiscArtAnyEnabled() As Boolean
        Return MovieDiscArtEden OrElse MovieDiscArtFrodo OrElse _
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
        Return MovieLandscapeEden OrElse MovieLandscapeFrodo OrElse _
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieLandscapeExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertMulti) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertSingle) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertVTS)))
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
        Return MovieSetBannerMSAA
    End Function

    Public Function MovieSetClearArtAnyEnabled() As Boolean
        Return MovieSetClearArtMSAA
    End Function

    Public Function MovieSetClearLogoAnyEnabled() As Boolean
        Return MovieSetClearLogoMSAA
    End Function

    Public Function MovieSetDiscArtAnyEnabled() As Boolean
        Return MovieSetDiscArtMSAA
    End Function

    Public Function MovieSetFanartAnyEnabled() As Boolean
        Return MovieSetFanartMSAA
    End Function

    Public Function MovieSetLandscapeAnyEnabled() As Boolean
        Return MovieSetLandscapeMSAA
    End Function

    Public Function MovieSetPosterAnyEnabled() As Boolean
        Return MovieSetPosterMSAA
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