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

<Serializable()> _
Public Class Settings

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    'TODO Dekker500 This class is a MONSTER. It needs to be broken down to a more manageable granularity

    Private _cleandotfanartjpg As Boolean
    Private _cleanextrathumbs As Boolean
    Private _cleanfanartjpg As Boolean
    Private _cleanfolderjpg As Boolean
    Private _cleanmoviejpg As Boolean
    Private _cleanmovienfo As Boolean
    Private _cleanmovienfob As Boolean
    Private _cleanmovietbn As Boolean
    Private _cleanmovietbnb As Boolean
    Private _cleanmoviefanartjpg As Boolean
    Private _cleanmovienamejpg As Boolean
    Private _cleanposterjpg As Boolean
    Private _cleanpostertbn As Boolean
    Private _embermodules As List(Of ModulesManager._XMLEmberModuleClass)
    Private _filesystemcleanerwhitelist As Boolean
    Private _filesystemcleanerwhitelistexts As List(Of String)
    Private _filesystemexpertcleaner As Boolean
    Private _filesystemnostackexts As List(Of String)
    Private _filesystemvalidexts As List(Of String)
    Private _filesystemvalidthemeexts As List(Of String)
    Private _generalcheckupdates As Boolean
    Private _generalcreationdate As Boolean
    Private _generaldaemondrive As String
    Private _generaldaemonpath As String
    Private _generalfilterpanelstate As Boolean
    Private _generalhidebanner As Boolean
    Private _generalhidecharacterart As Boolean
    Private _generalhideclearart As Boolean
    Private _generalhideclearlogo As Boolean
    Private _generalhidediscart As Boolean
    Private _generalhidefanart As Boolean
    Private _generalhidefanartsmall As Boolean
    Private _generalhidelandscape As Boolean
    Private _generalhideposter As Boolean
    Private _generalimagesglassoverlay As Boolean
    Private _generalinfopanelanim As Boolean
    Private _generallanguage As String
    Private _generalmainsplitterpanelstate As Integer
    Private _generalmovieinfopanelstate As Integer
    Private _generalmoviesetinfopanelstate As Integer
    Private _generalmovietheme As String
    Private _generalmoviesettheme As String
    Private _generaloverwritenfo As Boolean
    Private _generalseasonsplitterpanelstate As Integer
    Private _generalshowgenrestext As Boolean
    Private _generalshowimgdims As Boolean
    Private _generalshowsplitterpanelstate As Integer
    Private _generalsourcefromfolder As Boolean
    Private _generaltvepisodetheme As String
    Private _generaltvshowinfopanelstate As Integer
    Private _generaltvshowtheme As String
    Private _generalwindowloc As New Point
    Private _generalwindowsize As New Size
    Private _generalwindowstate As FormWindowState
    Private _genrefilter As String
    Private _movieactorthumbsoverwrite As Boolean
    Private _movieactorthumbsqual As Integer
    Private _moviebackdropsauto As Boolean
    Private _moviebackdropspath As String
    Private _moviebannercol As Boolean
    Private _moviebannerheight As Integer
    Private _moviebanneroverwrite As Boolean
    Private _moviebannerprefonly As Boolean
    Private _moviebannerpreftype As Enums.MovieBannerType
    Private _moviebannerqual As Integer
    Private _moviebannerresize As Boolean
    Private _moviebannerwidth As Integer
    Private _moviecleandb As Boolean
    Private _movieclearartcol As Boolean
    Private _movieclearartoverwrite As Boolean
    Private _movieclearlogocol As Boolean
    Private _movieclearlogooverwrite As Boolean
    Private _movieclickscrape As Boolean
    Private _movieclickscrapeask As Boolean
    Private _moviediscartcol As Boolean
    Private _moviediscartoverwrite As Boolean
    Private _moviedisplayyear As Boolean
    Private _movieefanartscol As Boolean
    Private _movieefanartsheight As Integer
    Private _movieefanartslimit As Integer
    Private _movieefanartsoverwrite As Boolean
    Private _movieefanartsprefonly As Boolean
    Private _movieefanartsprefsize As Enums.FanartSize
    Private _movieefanartsqual As Integer
    Private _movieefanartsresize As Boolean
    Private _movieefanartswidth As Integer
    Private _movieethumbscol As Boolean
    Private _movieethumbsheight As Integer
    Private _movieethumbslimit As Integer
    Private _movieethumbsoverwrite As Boolean
    Private _movieethumbsprefonly As Boolean
    Private _movieethumbsprefsize As Enums.FanartSize
    Private _movieethumbsqual As Integer
    Private _movieethumbsresize As Boolean
    Private _movieethumbswidth As Integer
    Private _moviefanartcol As Boolean
    Private _moviefanartheight As Integer
    Private _moviefanartoverwrite As Boolean
    Private _moviefanartprefonly As Boolean
    Private _moviefanartprefsize As Enums.FanartSize
    Private _moviefanartqual As Integer
    Private _moviefanartresize As Boolean
    Private _moviefanartwidth As Integer
    Private _moviefiltercustom As List(Of String)
    Private _moviefiltercustomisempty As Boolean
    Private _moviegeneralflaglang As String
    Private _moviegeneralignorelastscan As Boolean
    Private _moviegeneralmarknew As Boolean
    Private _movieimdburl As String
    Private _movielandscapecol As Boolean
    Private _movielandscapeoverwrite As Boolean
    Private _movielevtolerance As Integer
    Private _movielockgenre As Boolean
    Private _movielocklanguagea As Boolean
    Private _movielocklanguagev As Boolean
    Private _movielockmpaa As Boolean
    Private _movielockoutline As Boolean
    Private _movielockplot As Boolean
    Private _movielockrating As Boolean
    Private _movielockstudio As Boolean
    Private _movielocktagline As Boolean
    Private _movielocktitle As Boolean
    Private _movielocktrailer As Boolean
    Private _moviemetadataperfiletype As List(Of MetadataPerType)
    Private _moviemissingbanner As Boolean
    Private _moviemissingclearart As Boolean
    Private _moviemissingclearlogo As Boolean
    Private _moviemissingdiscart As Boolean
    Private _moviemissingefanarts As Boolean
    Private _moviemissingethumbs As Boolean
    Private _moviemissingfanart As Boolean
    Private _moviemissinglandscape As Boolean
    Private _moviemissingnfo As Boolean
    Private _moviemissingposter As Boolean
    Private _moviemissingsubs As Boolean
    Private _moviemissingtheme As Boolean
    Private _moviemissingtrailer As Boolean
    Private _moviemoviesetspath As String
    Private _movienfocol As Boolean
    Private _movienosaveimagestonfo As Boolean
    Private _moviepostercol As Boolean
    Private _movieposterheight As Integer
    Private _movieposteroverwrite As Boolean
    Private _movieposterprefonly As Boolean
    Private _movieposterprefsize As Enums.PosterSize
    Private _movieposterqual As Integer
    Private _movieposterresize As Boolean
    Private _movieposterwidth As Integer
    Private _moviepropercase As Boolean
    Private _moviescanordermodify As Boolean
    Private _moviescrapercast As Boolean
    Private _moviescrapercastlimit As Integer
    Private _moviescrapercastwithimgonly As Boolean
    Private _moviescrapercertformpaa As Boolean
    Private _moviescrapercertification As Boolean
    Private _moviescrapercertlang As String
    Private _moviescrapercollection As Boolean
    Private _moviescrapercountry As Boolean
    Private _moviescrapercrew As Boolean
    Private _moviescraperdirector As Boolean
    Private _moviescraperdurationruntimeformat As String
    Private _moviescraperforcetitle As String
    Private _moviescraperfullcast As Boolean
    Private _moviescraperfullcrew As Boolean
    Private _moviescrapergenre As Boolean
    Private _moviescrapergenrelimit As Integer
    Private _moviescrapermetadataifoscan As Boolean
    Private _moviescrapermetadatascan As Boolean
    Private _moviescrapermpaa As Boolean
    Private _moviescrapermusicby As Boolean
    Private _moviescraperonlyvalueformpaa As Boolean
    Private _moviescraperoutline As Boolean
    Private _moviescraperoutlineforplot As Boolean
    Private _moviescraperoutlinelimit As Integer
    Private _moviescraperoutlineplotenglishoverwrite As Boolean
    Private _moviescraperplot As Boolean
    Private _moviescraperplotforoutline As Boolean
    Private _moviescraperproducers As Boolean
    Private _moviescraperrating As Boolean
    Private _moviescraperrelease As Boolean
    Private _moviescraperruntime As Boolean
    Private _moviescraperstudio As Boolean
    Private _moviescrapertagline As Boolean
    Private _moviescrapertitle As Boolean
    Private _moviescrapertitlefallback As Boolean
    Private _moviescrapertop250 As Boolean
    Private _moviescrapertrailer As Boolean
    Private _moviescraperusemdduration As Boolean
    Private _moviescraperusempaafsk As Boolean
    Private _moviescrapervotes As Boolean
    Private _moviescraperwriters As Boolean
    Private _moviescraperyear As Boolean
    Private _moviesetbannercol As Boolean
    Private _moviesetbanneroverwrite As Boolean
    Private _moviesetclearartcol As Boolean
    Private _moviesetclearartoverwrite As Boolean
    Private _moviesetclearlogocol As Boolean
    Private _moviesetclearlogooverwrite As Boolean
    Private _moviesetdiscartcol As Boolean
    Private _moviesetdiscartoverwrite As Boolean
    Private _moviesetfanartcol As Boolean
    Private _moviesetfanartoverwrite As Boolean
    Private _moviesetlandscapecol As Boolean
    Private _moviesetlandscapeoverwrite As Boolean
    Private _moviesetnfocol As Boolean
    Private _moviesetpostercol As Boolean
    Private _moviesetposteroverwrite As Boolean
    Private _moviesets As New List(Of String)
    Private _movieskiplessthan As Integer
    Private _movieskipstackedsizecheck As Boolean
    Private _moviesortbeforescan As Boolean
    Private _moviesorttokens As List(Of String)
    Private _moviesorttokensisempty As Boolean
    Private _moviesubcol As Boolean
    Private _moviethemecol As Boolean
    Private _moviethemeenable As Boolean
    Private _moviethemeoverwrite As Boolean
    Private _movietrailercol As Boolean
    Private _movietrailerdeleteexisting As Boolean
    Private _movietrailerenable As Boolean
    Private _movietraileroverwrite As Boolean
    Private _movietrailerminqual As Enums.TrailerQuality
    Private _movietrailerprefqual As Enums.TrailerQuality
    Private _moviewatchedcol As Boolean
    Private _ommdummyformat As Integer
    Private _ommdummytagline As String
    Private _ommdummytop As String
    Private _ommdummyusebackground As Boolean
    Private _ommdummyusefanart As Boolean
    Private _ommdummyuseoverlay As Boolean
    Private _ommmediastubtagline As String
    Private _password As String
    Private _proxycredentials As NetworkCredential
    Private _proxyport As Integer
    Private _proxyuri As String
    Private _sortpath As String
    Private _traktpassword As String
    Private _traktusername As String
    Private _tvasbannerheight As Integer
    Private _tvasbanneroverwrite As Boolean
    Private _tvasbannerpreftype As Enums.TVShowBannerType
    Private _tvasbannerqual As Integer
    Private _tvasbannerresize As Boolean
    Private _tvasbannerwidth As Integer
    Private _tvasfanartheight As Integer
    Private _tvasfanartoverwrite As Boolean
    Private _tvasfanartprefsize As Enums.TVFanartSize
    Private _tvasfanartqual As Integer
    Private _tvasfanartresize As Boolean
    Private _tvasfanartwidth As Integer
    Private _tvaslandscapeoverwrite As Boolean
    Private _tvasposterheight As Integer
    Private _tvasposteroverwrite As Boolean
    Private _tvasposterprefsize As Enums.TVPosterSize
    Private _tvasposterqual As Integer
    Private _tvasposterresize As Boolean
    Private _tvasposterwidth As Integer
    Private _tvcleandb As Boolean
    Private _tvdisplaymissingepisodes As Boolean
    Private _tvepisodefanartcol As Boolean
    Private _tvepisodefanartheight As Integer
    Private _tvepisodefanartoverwrite As Boolean
    Private _tvepisodefanartprefsize As Enums.TVFanartSize
    Private _tvepisodefanartqual As Integer
    Private _tvepisodefanartresize As Boolean
    Private _tvepisodefanartwidth As Integer
    Private _tvepisodefiltercustom As List(Of String)
    Private _tvepisodefiltercustomisempty As Boolean
    Private _tvepisodenfocol As Boolean
    Private _tvepisodenofilter As Boolean
    Private _tvepisodepostercol As Boolean
    Private _tvepisodeposterheight As Integer
    Private _tvepisodeposteroverwrite As Boolean
    Private _tvepisodeposterqual As Integer
    Private _tvepisodeposterresize As Boolean
    Private _tvepisodeposterwidth As Integer
    Private _tvepisodepropercase As Boolean
    Private _tvepisodewatchedcol As Boolean
    Private _tvgeneraldisplayasposter As Boolean
    Private _tvgeneralflaglang As String
    Private _tvgeneralignorelastscan As Boolean
    Private _tvgenerallanguage As String
    Private _tvgenerallanguages As List(Of Containers.TVLanguage)
    Private _tvgeneralmarknewepisodes As Boolean
    Private _tvgeneralmarknewshows As Boolean
    Private _tvlockepisodeplot As Boolean
    Private _tvlockepisoderating As Boolean
    Private _tvlockepisodetitle As Boolean
    Private _tvlockshowgenre As Boolean
    Private _tvlockshowplot As Boolean
    Private _tvlockshowrating As Boolean
    Private _tvlockshowstatus As Boolean
    Private _tvlockshowstudio As Boolean
    Private _tvlockshowtitle As Boolean
    Private _tvmetadataperfiletype As List(Of MetadataPerType)
    Private _tvscanordermodify As Boolean
    Private _tvscraperdurationruntimeformat As String
    Private _tvscraperepisodeactors As Boolean
    Private _tvscraperepisodeaired As Boolean
    Private _tvscraperepisodecredits As Boolean
    Private _tvscraperepisodedirector As Boolean
    Private _tvscraperepisodeepisode As Boolean
    Private _tvscraperepisodeplot As Boolean
    Private _tvscraperepisoderating As Boolean
    Private _tvscraperepisodeseason As Boolean
    Private _tvscraperepisodetitle As Boolean
    Private _tvscrapermetadatascan As Boolean
    Private _tvscraperoptionsordering As Enums.Ordering
    Private _tvscraperratingregion As String
    Private _tvscrapershowactors As Boolean
    Private _tvscrapershowepiguideurl As Boolean
    Private _tvscrapershowgenre As Boolean
    Private _tvscrapershowmpaa As Boolean
    Private _tvscrapershowplot As Boolean
    Private _tvscrapershowpremiered As Boolean
    Private _tvscrapershowrating As Boolean
    Private _tvscrapershowstatus As Boolean
    Private _tvscrapershowstudio As Boolean
    Private _tvscrapershowtitle As Boolean
    Private _tvscraperupdatetime As Enums.TVScraperUpdateTime
    Private _tvscraperusemdduration As Boolean
    Private _tvseasonbannercol As Boolean
    Private _tvseasonbannerheight As Integer
    Private _tvseasonbanneroverwrite As Boolean
    Private _tvseasonbannerpreftype As Enums.TVSeasonBannerType
    Private _tvseasonbannerqual As Integer
    Private _tvseasonbannerresize As Boolean
    Private _tvseasonbannerwidth As Integer
    Private _tvseasonfanartcol As Boolean
    Private _tvseasonfanartheight As Integer
    Private _tvseasonfanartoverwrite As Boolean
    Private _tvseasonfanartprefsize As Enums.TVFanartSize
    Private _tvseasonfanartqual As Integer
    Private _tvseasonfanartresize As Boolean
    Private _tvseasonfanartwidth As Integer
    Private _tvseasonlandscapecol As Boolean
    Private _tvseasonlandscapeoverwrite As Boolean
    Private _tvseasonpostercol As Boolean
    Private _tvseasonposterheight As Integer
    Private _tvseasonposteroverwrite As Boolean
    Private _tvseasonposterprefsize As Enums.TVPosterSize
    Private _tvseasonposterqual As Integer
    Private _tvseasonposterresize As Boolean
    Private _tvseasonposterwidth As Integer
    Private _tvshowbannercol As Boolean
    Private _tvshowbannerheight As Integer
    Private _tvshowbanneroverwrite As Boolean
    Private _tvshowbannerpreftype As Enums.TVShowBannerType
    Private _tvshowbannerqual As Integer
    Private _tvshowbannerresize As Boolean
    Private _tvshowbannerwidth As Integer
    Private _tvshowcharacterartcol As Boolean
    Private _tvshowcharacterartoverwrite As Boolean
    Private _tvshowclearartcol As Boolean
    Private _tvshowclearartoverwrite As Boolean
    Private _tvshowclearlogocol As Boolean
    Private _tvshowclearlogooverwrite As Boolean
    Private _tvshowefanartscol As Boolean
    Private _tvshowfanartcol As Boolean
    Private _tvshowfanartheight As Integer
    Private _tvshowfanartoverwrite As Boolean
    Private _tvshowfanartprefsize As Enums.TVFanartSize
    Private _tvshowfanartqual As Integer
    Private _tvshowfanartresize As Boolean
    Private _tvshowfanartwidth As Integer
    Private _tvshowfiltercustom As List(Of String)
    Private _tvshowfiltercustomisempty As Boolean
    Private _tvshowlandscapecol As Boolean
    Private _tvshowlandscapeoverwrite As Boolean
    Private _tvshownfocol As Boolean
    Private _tvshowpostercol As Boolean
    Private _tvshowposterheight As Integer
    Private _tvshowposteroverwrite As Boolean
    Private _tvshowposterprefsize As Enums.TVPosterSize
    Private _tvshowposterqual As Integer
    Private _tvshowposterresize As Boolean
    Private _tvshowposterwidth As Integer
    Private _tvshowpropercase As Boolean
    Private _tvshowregexes As List(Of TVShowRegEx)
    Private _tvskiplessthan As Integer
    Private _tvshowthemecol As Boolean
    Private _username As String
    Private _usetrakt As Boolean
    Private _version As String

    '***************************************************
    '******************* Movie Part ********************
    '***************************************************

    '*************** XBMC Frodo settings ***************
    Private _movieusefrodo As Boolean
    Private _movieactorthumbsfrodo As Boolean
    Private _moviebannerfrodo As Boolean
    Private _movieclearartfrodo As Boolean
    Private _movieclearlogofrodo As Boolean
    Private _moviediscartfrodo As Boolean
    Private _movieextrafanartsfrodo As Boolean
    Private _movieextrathumbsfrodo As Boolean
    Private _moviefanartfrodo As Boolean
    Private _movielandscapefrodo As Boolean
    Private _movienfofrodo As Boolean
    Private _movieposterfrodo As Boolean
    Private _movietrailerfrodo As Boolean

    '*************** XBMC Eden settings ***************
    Private _movieuseeden As Boolean
    Private _movieactorthumbseden As Boolean
    Private _moviebannereden As Boolean
    Private _moviecleararteden As Boolean
    Private _movieclearlogoeden As Boolean
    Private _moviediscarteden As Boolean
    Private _movieextrafanartseden As Boolean
    Private _movieextrathumbseden As Boolean
    Private _moviefanarteden As Boolean
    Private _movielandscapeeden As Boolean
    Private _movienfoeden As Boolean
    Private _moviepostereden As Boolean
    Private _movietrailereden As Boolean

    '************* XBMC optional settings *************
    Private _moviexbmctrailerformat As Boolean
    Private _moviexbmcprotectvtsbdmv As Boolean

    '*************** XBMC theme settings ***************
    Private _moviexbmcthemeenable As Boolean
    Private _moviexbmcthemecustom As Boolean
    Private _moviexbmcthememovie As Boolean
    Private _moviexbmcthemesub As Boolean
    Private _moviexbmcthemecustompath As String
    Private _moviexbmcthemesubdir As String

    '****************** YAMJ settings *****************
    Private _movieuseyamj As Boolean
    Private _movieactorthumbsyamj As Boolean
    Private _moviebanneryamj As Boolean
    Private _movieclearartyamj As Boolean
    Private _movieclearlogoyamj As Boolean
    Private _moviediscartyamj As Boolean
    Private _movieextrafanartsyamj As Boolean
    Private _movieextrathumbsyamj As Boolean
    Private _moviefanartyamj As Boolean
    Private _movielandscapeyamj As Boolean
    Private _movienfoyamj As Boolean
    Private _movieposteryamj As Boolean
    Private _movietraileryamj As Boolean
    Private _movieyamjcompatiblesets As Boolean
    Private _movieyamjwatchedfile As Boolean
    Private _movieyamjwatchedfolder As String

    '****************** NMJ settings ******************
    Private _movieusenmj As Boolean
    Private _movieactorthumbsnmj As Boolean
    Private _moviebannernmj As Boolean
    Private _movieclearartnmj As Boolean
    Private _movieclearlogonmj As Boolean
    Private _moviediscartnmj As Boolean
    Private _movieextrafanartsnmj As Boolean
    Private _movieextrathumbsnmj As Boolean
    Private _moviefanartnmj As Boolean
    Private _movielandscapenmj As Boolean
    Private _movienfonmj As Boolean
    Private _movieposternmj As Boolean
    Private _movietrailernmj As Boolean

    '***************** Boxee settings ******************
    Private _movieuseboxee As Boolean
    Private _moviefanartboxee As Boolean
    Private _movienfoboxee As Boolean
    Private _movieposterboxee As Boolean

    '***************** Expert settings ****************
    Private _movieuseexpert As Boolean

    '***************** Expert Single ****************
    Private _movieactorthumbsexpertsingle As Boolean
    Private _movieactorthumbsextexpertsingle As String
    Private _moviebannerexpertsingle As String
    Private _movieclearartexpertsingle As String
    Private _movieclearlogoexpertsingle As String
    Private _moviediscartexpertsingle As String
    Private _movieextrafanartsexpertsingle As Boolean
    Private _movieextrathumbsexpertsingle As Boolean
    Private _moviefanartexpertsingle As String
    Private _movielandscapeexpertsingle As String
    Private _movienfoexpertsingle As String
    Private _movieposterexpertsingle As String
    Private _moviestackexpertsingle As Boolean
    Private _movietrailerexpertsingle As String
    Private _movieunstackexpertsingle As Boolean

    '***************** Expert Multi ****************
    Private _movieactorthumbsexpertmulti As Boolean
    Private _movieactorthumbsextexpertmulti As String
    Private _moviebannerexpertmulti As String
    Private _movieclearartexpertmulti As String
    Private _movieclearlogoexpertmulti As String
    Private _moviediscartexpertmulti As String
    Private _moviefanartexpertmulti As String
    Private _movielandscapeexpertmulti As String
    Private _movienfoexpertmulti As String
    Private _movieposterexpertmulti As String
    Private _moviestackexpertmulti As Boolean
    Private _movietrailerexpertmulti As String
    Private _movieunstackexpertmulti As Boolean

    '***************** Expert VTS ****************
    Private _movieactorthumbsexpertvts As Boolean
    Private _movieactorthumbsextexpertvts As String
    Private _moviebannerexpertvts As String
    Private _movieclearartexpertvts As String
    Private _movieclearlogoexpertvts As String
    Private _moviediscartexpertvts As String
    Private _movieextrafanartsexpertvts As Boolean
    Private _movieextrathumbsexpertvts As Boolean
    Private _moviefanartexpertvts As String
    Private _movielandscapeexpertvts As String
    Private _movienfoexpertvts As String
    Private _movieposterexpertvts As String
    Private _movierecognizevtsexpertvts As Boolean
    Private _movietrailerexpertvts As String
    Private _movieusebasedirectoryexpertvts As Boolean

    '***************** Expert BDMV ****************
    Private _movieactorthumbsexpertbdmv As Boolean
    Private _movieactorthumbsextexpertbdmv As String
    Private _moviebannerexpertbdmv As String
    Private _movieclearartexpertbdmv As String
    Private _movieclearlogoexpertbdmv As String
    Private _moviediscartexpertbdmv As String
    Private _movieextrafanartsexpertbdmv As Boolean
    Private _movieextrathumbsexpertbdmv As Boolean
    Private _moviefanartexpertbdmv As String
    Private _movielandscapeexpertbdmv As String
    Private _movienfoexpertbdmv As String
    Private _movieposterexpertbdmv As String
    Private _movietrailerexpertbdmv As String
    Private _movieusebasedirectoryexpertbdmv As Boolean

    '***************************************************
    '****************** TV Show Part *******************
    '***************************************************

    '*************** XBMC Frodo settings ***************
    Private _tvusefrodo As Boolean
    Private _tvepisodeactorthumbsfrodo As Boolean
    Private _tvepisodeposterfrodo As Boolean
    Private _tvseasonbannerfrodo As Boolean
    Private _tvseasonfanartfrodo As Boolean
    Private _tvseasonposterfrodo As Boolean
    Private _tvshowactorthumbsfrodo As Boolean
    Private _tvshowbannerfrodo As Boolean
    Private _tvshowfanartfrodo As Boolean
    Private _tvshowposterfrodo As Boolean

    '*************** XBMC Eden settings ****************
    Private _tvuseeden As Boolean

    '************* XBMC optional settings **************
    Private _tvseasonlandscapexbmc As Boolean
    Private _tvshowcharacterartxbmc As Boolean
    Private _tvshowclearartxbmc As Boolean
    Private _tvshowclearlogoxbmc As Boolean
    Private _tvshowextrafanartsxbmc As Boolean
    Private _tvshowlandscapexbmc As Boolean
    Private _tvshowtvthemefolderxbmc As String
    Private _tvshowtvthemexbmc As Boolean

    '****************** YAMJ settings ******************
    Private _tvuseyamj As Boolean
    Private _tvepisodeposteryamj As Boolean
    Private _tvseasonbanneryamj As Boolean
    Private _tvseasonfanartyamj As Boolean
    Private _tvseasonposteryamj As Boolean
    Private _tvshowbanneryamj As Boolean
    Private _tvshowfanartyamj As Boolean
    Private _tvshowposteryamj As Boolean

    '****************** NMJ settings *******************

    '************** NMT optional settings **************

    '***************** Boxee settings ******************
    Private _tvuseboxee As Boolean
    Private _tvepisodeposterboxee As Boolean
    Private _tvseasonposterboxee As Boolean
    Private _tvshowbannerboxee As Boolean
    Private _tvshowfanartboxee As Boolean
    Private _tvshowposterboxee As Boolean

    '***************** Expert settings *****************

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

#Region "Properties"

    Public Property MovieScraperCastLimit() As Integer
        Get
            Return Me._moviescrapercastlimit
        End Get
        Set(ByVal value As Integer)
            Me._moviescrapercastlimit = value
        End Set
    End Property

    Public Property MovieActorThumbsOverwrite() As Boolean
        Get
            Return Me._movieactorthumbsoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsoverwrite = value
        End Set
    End Property

    Public Property TVASPosterHeight() As Integer
        Get
            Return Me._tvasposterheight
        End Get
        Set(ByVal value As Integer)
            Me._tvasposterheight = value
        End Set
    End Property

    Public Property MovieActorThumbsQual() As Integer
        Get
            Return Me._movieactorthumbsqual
        End Get
        Set(ByVal value As Integer)
            Me._movieactorthumbsqual = value
        End Set
    End Property

    Public Property TVASPosterQual() As Integer
        Get
            Return Me._tvasposterqual
        End Get
        Set(ByVal value As Integer)
            Me._tvasposterqual = value
        End Set
    End Property

    Public Property TVASBannerQual() As Integer
        Get
            Return Me._tvasbannerqual
        End Get
        Set(ByVal value As Integer)
            Me._tvasbannerqual = value
        End Set
    End Property

    Public Property TVASFanartQual() As Integer
        Get
            Return Me._tvasfanartqual
        End Get
        Set(ByVal value As Integer)
            Me._tvasfanartqual = value
        End Set
    End Property

    Public Property TVSeasonBannerQual() As Integer
        Get
            Return Me._tvseasonbannerqual
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonbannerqual = value
        End Set
    End Property

    Public Property TVShowBannerQual() As Integer
        Get
            Return Me._tvshowbannerqual
        End Get
        Set(ByVal value As Integer)
            Me._tvshowbannerqual = value
        End Set
    End Property

    Public Property TVASPosterWidth() As Integer
        Get
            Return Me._tvasposterwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvasposterwidth = value
        End Set
    End Property

    Public Property GeneralShowGenresText() As Boolean
        Get
            Return Me._generalshowgenrestext
        End Get
        Set(ByVal value As Boolean)
            Me._generalshowgenrestext = value
        End Set
    End Property

    Public Property TVGeneralLanguage() As String
        Get
            Return Me._tvgenerallanguage
        End Get
        Set(ByVal value As String)
            Me._tvgenerallanguage = If(String.IsNullOrEmpty(value), "en", value)
        End Set
    End Property

    Public Property TVGeneralLanguages() As List(Of Containers.TVLanguage)
        Get
            Return Me._tvgenerallanguages
        End Get
        Set(ByVal value As List(Of Containers.TVLanguage))
            Me._tvgenerallanguages = value
        End Set
    End Property

    Public Property MovieClickScrape() As Boolean
        Get
            Return Me._movieclickscrape
        End Get
        Set(ByVal value As Boolean)
            Me._movieclickscrape = value
        End Set
    End Property

    Public Property MovieClickScrapeAsk() As Boolean
        Get
            Return Me._movieclickscrapeask
        End Get
        Set(ByVal value As Boolean)
            Me._movieclickscrapeask = value
        End Set
    End Property

    Public Property MovieBackdropsAuto() As Boolean
        Get
            Return Me._moviebackdropsauto
        End Get
        Set(ByVal value As Boolean)
            Me._moviebackdropsauto = value
        End Set
    End Property

    Public Property MovieIMDBURL() As String
        Get
            Return Me._movieimdburl
        End Get
        Set(ByVal value As String)
            Me._movieimdburl = value
        End Set
    End Property

    Public Property MovieBackdropsPath() As String
        Get
            Return Me._moviebackdropspath
        End Get
        Set(ByVal value As String)
            Me._moviebackdropspath = value
        End Set
    End Property

    Public Property MovieMoviesetsPath() As String
        Get
            Return Me._moviemoviesetspath
        End Get
        Set(ByVal value As String)
            Me._moviemoviesetspath = value
        End Set
    End Property

    Public Property MovieScraperCastWithImgOnly() As Boolean
        Get
            Return Me._moviescrapercastwithimgonly
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercastwithimgonly = value
        End Set
    End Property

    Public Property MovieScraperCertLang() As String
        Get
            Return Me._moviescrapercertlang
        End Get
        Set(ByVal value As String)
            Me._moviescrapercertlang = value
        End Set
    End Property

    Public Property GeneralCheckUpdates() As Boolean
        Get
            Return Me._generalcheckupdates
        End Get
        Set(ByVal value As Boolean)
            Me._generalcheckupdates = value
        End Set
    End Property

    Public Property MovieCleanDB() As Boolean
        Get
            Return Me._moviecleandb
        End Get
        Set(ByVal value As Boolean)
            Me._moviecleandb = value
        End Set
    End Property

    Public Property CleanDotFanartJPG() As Boolean
        Get
            Return Me._cleandotfanartJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleandotfanartJpg = value
        End Set
    End Property

    Public Property CleanExtrathumbs() As Boolean
        Get
            Return Me._cleanextrathumbs
        End Get
        Set(ByVal value As Boolean)
            Me._cleanextrathumbs = value
        End Set
    End Property

    Public Property CleanFanartJPG() As Boolean
        Get
            Return Me._cleanfanartJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanfanartJpg = value
        End Set
    End Property

    Public Property CleanFolderJPG() As Boolean
        Get
            Return Me._cleanfolderJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanfolderJpg = value
        End Set
    End Property

    Public Property CleanMovieFanartJPG() As Boolean
        Get
            Return Me._cleanmoviefanartJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmoviefanartJpg = value
        End Set
    End Property

    Public Property CleanMovieJPG() As Boolean
        Get
            Return Me._cleanmovieJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovieJpg = value
        End Set
    End Property

    Public Property CleanMovieNameJPG() As Boolean
        Get
            Return Me._cleanmovienameJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovienameJpg = value
        End Set
    End Property

    Public Property CleanMovieNFO() As Boolean
        Get
            Return Me._cleanmovieNfo
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovieNfo = value
        End Set
    End Property

    Public Property CleanMovieNFOB() As Boolean
        Get
            Return Me._cleanmovieNfoB
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovieNfoB = value
        End Set
    End Property

    Public Property CleanMovieTBN() As Boolean
        Get
            Return _cleanmovieTbn
        End Get
        Set(ByVal value As Boolean)
            _cleanmovieTbn = value
        End Set
    End Property

    Public Property CleanMovieTBNB() As Boolean
        Get
            Return Me._cleanmovieTbnB
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovieTbnB = value
        End Set
    End Property

    Public Property CleanPosterJPG() As Boolean
        Get
            Return Me._cleanposterJpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanposterJpg = value
        End Set
    End Property

    Public Property CleanPosterTBN() As Boolean
        Get
            Return Me._cleanposterTbn
        End Get
        Set(ByVal value As Boolean)
            Me._cleanposterTbn = value
        End Set
    End Property

    Public Property FileSystemCleanerWhitelistExts() As List(Of String)
        Get
            Return Me._filesystemcleanerwhitelistexts
        End Get
        Set(ByVal value As List(Of String))
            Me._filesystemcleanerwhitelistexts = value
        End Set
    End Property

    Public Property FileSystemCleanerWhitelist() As Boolean
        Get
            Return Me._filesystemcleanerwhitelist
        End Get
        Set(ByVal value As Boolean)
            Me._filesystemcleanerwhitelist = value
        End Set
    End Property

    Public Property MovieTrailerDeleteExisting() As Boolean
        Get
            Return Me._movietrailerdeleteexisting
        End Get
        Set(ByVal value As Boolean)
            Me._movietrailerdeleteexisting = value
        End Set
    End Property

    Public Property TVGeneralDisplayASPoster() As Boolean
        Get
            Return Me._tvgeneraldisplayasposter
        End Get
        Set(ByVal value As Boolean)
            Me._tvgeneraldisplayasposter = value
        End Set
    End Property

    Public Property TVDisplayMissingEpisodes() As Boolean
        Get
            Return Me._tvdisplaymissingepisodes
        End Get
        Set(ByVal value As Boolean)
            Me._tvdisplaymissingepisodes = value
        End Set
    End Property

    Public Property MovieDisplayYear() As Boolean
        Get
            Return Me._moviedisplayyear
        End Get
        Set(ByVal value As Boolean)
            Me._moviedisplayyear = value
        End Set
    End Property

    Public Property TVScraperOptionsOrdering() As Enums.Ordering
        Get
            Return Me._tvscraperoptionsordering
        End Get
        Set(ByVal value As Enums.Ordering)
            Me._tvscraperoptionsordering = value
        End Set
    End Property

    <XmlArray("EmberModules")> _
    <XmlArrayItem("Module")> _
    Public Property EmberModules() As List(Of ModulesManager._XMLEmberModuleClass)
        Get
            Return Me._emberModules
        End Get
        Set(ByVal value As List(Of ModulesManager._XMLEmberModuleClass))
            Me._emberModules = value
        End Set
    End Property

    Public Property MovieScraperMetaDataIFOScan() As Boolean
        Get
            Return Me._moviescrapermetadataifoscan
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapermetadataifoscan = value
        End Set
    End Property

    Public Property TVEpisodeFanartHeight() As Integer
        Get
            Return Me._tvepisodefanartheight
        End Get
        Set(ByVal value As Integer)
            Me._tvepisodefanartheight = value
        End Set
    End Property

    Public Property TVEpisodeFanartQual() As Integer
        Get
            Return Me._tvepisodefanartqual
        End Get
        Set(ByVal value As Integer)
            Me._tvepisodefanartqual = value
        End Set
    End Property

    Public Property TVEpisodeFanartWidth() As Integer
        Get
            Return Me._tvepisodefanartwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvepisodefanartwidth = value
        End Set
    End Property

    Public Property TVEpisodeFilterCustom() As List(Of String)
        Get
            Return Me._tvepisodefiltercustom
        End Get
        Set(ByVal value As List(Of String))
            Me._tvepisodefiltercustom = value
        End Set
    End Property

    Public Property TVEpisodeFanartCol() As Boolean
        Get
            Return Me._tvepisodefanartcol
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodefanartcol = value
        End Set
    End Property

    Public Property TVEpisodeNfoCol() As Boolean
        Get
            Return Me._tvepisodenfocol
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodenfocol = value
        End Set
    End Property

    Public Property TVEpisodePosterCol() As Boolean
        Get
            Return Me._tvepisodepostercol
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodepostercol = value
        End Set
    End Property

    Public Property TVLockEpisodePlot() As Boolean
        Get
            Return Me._tvlockepisodeplot
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockepisodeplot = value
        End Set
    End Property

    Public Property TVLockEpisodeRating() As Boolean
        Get
            Return Me._tvlockepisoderating
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockepisoderating = value
        End Set
    End Property

    Public Property TVLockEpisodeTitle() As Boolean
        Get
            Return Me._tvlockepisodetitle
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockepisodetitle = value
        End Set
    End Property

    Public Property TVEpisodePosterHeight() As Integer
        Get
            Return Me._tvepisodeposterheight
        End Get
        Set(ByVal value As Integer)
            Me._tvepisodeposterheight = value
        End Set
    End Property

    Public Property TVEpisodePosterQual() As Integer
        Get
            Return Me._tvepisodeposterqual
        End Get
        Set(ByVal value As Integer)
            Me._tvepisodeposterqual = value
        End Set
    End Property

    Public Property TVEpisodePosterWidth() As Integer
        Get
            Return Me._tvepisodeposterwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvepisodeposterwidth = value
        End Set
    End Property

    Public Property TVEpisodeProperCase() As Boolean
        Get
            Return Me._tvepisodepropercase
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodepropercase = value
        End Set
    End Property

    Public Property TVEpisodeWatchedCol() As Boolean
        Get
            Return Me._tvepisodewatchedcol
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodewatchedcol = value
        End Set
    End Property

    Public Property FileSystemExpertCleaner() As Boolean
        Get
            Return Me._filesystemexpertcleaner
        End Get
        Set(ByVal value As Boolean)
            Me._filesystemexpertcleaner = value
        End Set
    End Property

    Public Property MovieEFanartsHeight() As Integer
        Get
            Return Me._movieefanartsheight
        End Get
        Set(ByVal value As Integer)
            Me._movieefanartsheight = value
        End Set
    End Property

    Public Property MovieEThumbsHeight() As Integer
        Get
            Return Me._movieethumbsheight
        End Get
        Set(ByVal value As Integer)
            Me._movieethumbsheight = value
        End Set
    End Property

    Public Property MovieEThumbsLimit() As Integer
        Get
            Return Me._movieethumbslimit
        End Get
        Set(ByVal value As Integer)
            Me._movieethumbslimit = value
        End Set
    End Property

    Public Property MovieEFanartsLimit() As Integer
        Get
            Return Me._movieefanartslimit
        End Get
        Set(ByVal value As Integer)
            Me._movieefanartslimit = value
        End Set
    End Property

    Public Property MovieFanartHeight() As Integer
        Get
            Return Me._moviefanartheight
        End Get
        Set(ByVal value As Integer)
            Me._moviefanartheight = value
        End Set
    End Property

    Public Property MovieEFanartsPrefOnly() As Boolean
        Get
            Return Me._movieefanartsprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._movieefanartsprefonly = value
        End Set
    End Property

    Public Property MovieEThumbsPrefOnly() As Boolean
        Get
            Return Me._movieethumbsprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._movieethumbsprefonly = value
        End Set
    End Property

    Public Property MovieFanartPrefOnly() As Boolean
        Get
            Return Me._moviefanartprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartprefonly = value
        End Set
    End Property

    Public Property MovieFanartQual() As Integer
        Get
            Return Me._moviefanartqual
        End Get
        Set(ByVal value As Integer)
            Me._moviefanartqual = value
        End Set
    End Property

    Public Property MovieEFanartsWidth() As Integer
        Get
            Return Me._movieefanartswidth
        End Get
        Set(ByVal value As Integer)
            Me._movieefanartswidth = value
        End Set
    End Property

    Public Property MovieEThumbsWidth() As Integer
        Get
            Return Me._movieethumbswidth
        End Get
        Set(ByVal value As Integer)
            Me._movieethumbswidth = value
        End Set
    End Property

    Public Property MovieFanartWidth() As Integer
        Get
            Return Me._moviefanartwidth
        End Get
        Set(ByVal value As Integer)
            Me._moviefanartwidth = value
        End Set
    End Property

    Public Property MovieScraperTop250() As Boolean
        Get
            Return Me._moviescrapertop250
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapertop250 = value
        End Set
    End Property

    Public Property MovieScraperCollection() As Boolean
        Get
            Return Me._moviescrapercollection
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercollection = value
        End Set
    End Property

    Public Property MovieScraperCountry() As Boolean
        Get
            Return Me._moviescrapercountry
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercountry = value
        End Set
    End Property

    Public Property MovieScraperCast() As Boolean
        Get
            Return Me._moviescrapercast
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercast = value
        End Set
    End Property

    Public Property MovieScraperCertification() As Boolean
        Get
            Return Me._moviescrapercertification
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercertification = value
        End Set
    End Property

    Public Property MovieScraperCrew() As Boolean
        Get
            Return Me._moviescrapercrew
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercrew = value
        End Set
    End Property

    Public Property MovieScraperDirector() As Boolean
        Get
            Return Me._moviescraperdirector
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperdirector = value
        End Set
    End Property

    Public Property MovieScraperGenre() As Boolean
        Get
            Return Me._moviescrapergenre
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapergenre = value
        End Set
    End Property

    Public Property MovieScraperMPAA() As Boolean
        Get
            Return Me._moviescrapermpaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapermpaa = value
        End Set
    End Property

    Public Property MovieScraperMusicBy() As Boolean
        Get
            Return Me._moviescrapermusicby
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapermusicby = value
        End Set
    End Property

    Public Property MovieScraperOutline() As Boolean
        Get
            Return Me._moviescraperoutline
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperoutline = value
        End Set
    End Property

    Public Property MovieScraperPlot() As Boolean
        Get
            Return Me._moviescraperplot
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperplot = value
        End Set
    End Property

    Public Property MovieScraperProducers() As Boolean
        Get
            Return Me._moviescraperproducers
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperproducers = value
        End Set
    End Property

    Public Property MovieScraperRating() As Boolean
        Get
            Return Me._moviescraperrating
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperrating = value
        End Set
    End Property

    Public Property MovieScraperRelease() As Boolean
        Get
            Return Me._moviescraperrelease
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperrelease = value
        End Set
    End Property

    Public Property MovieScraperRuntime() As Boolean
        Get
            Return Me._moviescraperruntime
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperruntime = value
        End Set
    End Property

    Public Property MovieScraperStudio() As Boolean
        Get
            Return Me._moviescraperstudio
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperstudio = value
        End Set
    End Property

    Public Property MovieScraperTagline() As Boolean
        Get
            Return Me._moviescrapertagline
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapertagline = value
        End Set
    End Property

    Public Property MovieScraperTitle() As Boolean
        Get
            Return Me._moviescrapertitle
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapertitle = value
        End Set
    End Property

    Public Property MovieScraperTrailer() As Boolean
        Get
            Return Me._moviescrapertrailer
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapertrailer = value
        End Set
    End Property

    Public Property MovieScraperVotes() As Boolean
        Get
            Return Me._moviescrapervotes
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapervotes = value
        End Set
    End Property

    Public Property MovieScraperWriters() As Boolean
        Get
            Return Me._moviescraperwriters
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperwriters = value
        End Set
    End Property

    Public Property MovieScraperYear() As Boolean
        Get
            Return Me._moviescraperyear
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperyear = value
        End Set
    End Property

    Public Property MovieFilterCustom() As List(Of String)
        Get
            Return Me._moviefiltercustom
        End Get
        Set(ByVal value As List(Of String))
            Me._moviefiltercustom = value
        End Set
    End Property

    Public Property GeneralFilterPanelState() As Boolean
        Get
            Return Me._generalfilterpanelstate
        End Get
        Set(ByVal value As Boolean)
            Me._generalfilterpanelstate = value
        End Set
    End Property

    Public Property MovieGeneralFlagLang() As String
        Get
            Return Me._moviegeneralflaglang
        End Get
        Set(ByVal value As String)
            Me._moviegeneralflaglang = value
        End Set
    End Property

    Public Property MovieScraperForceTitle() As String
        Get
            Return Me._moviescraperforcetitle
        End Get
        Set(ByVal value As String)
            Me._moviescraperforcetitle = value
        End Set
    End Property

    Public Property MovieScraperFullCast() As Boolean
        Get
            Return Me._moviescraperfullcast
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperfullcast = value
        End Set
    End Property

    Public Property MovieScraperFullCrew() As Boolean
        Get
            Return Me._moviescraperfullcrew
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperfullcrew = value
        End Set
    End Property

    Public Property GenreFilter() As String
        Get
            Return Me._genrefilter
        End Get
        Set(ByVal value As String)
            Me._genrefilter = value
        End Set
    End Property

    Public Property MovieScraperGenreLimit() As Integer
        Get
            Return Me._moviescrapergenrelimit
        End Get
        Set(ByVal value As Integer)
            Me._moviescrapergenrelimit = value
        End Set
    End Property

    Public Property MovieGeneralIgnoreLastScan() As Boolean
        Get
            Return Me._moviegeneralignorelastscan
        End Get
        Set(ByVal value As Boolean)
            Me._moviegeneralignorelastscan = value
        End Set
    End Property

    Public Property GeneralInfoPanelAnim() As Boolean
        Get
            Return Me._generalinfopanelanim
        End Get
        Set(ByVal value As Boolean)
            Me._generalinfopanelanim = value
        End Set
    End Property

    Public Property GeneralMovieInfoPanelState() As Integer
        Get
            Return Me._generalmovieinfopanelstate
        End Get
        Set(ByVal value As Integer)
            Me._generalmovieinfopanelstate = value
        End Set
    End Property

    Public Property GeneralMovieSetInfoPanelState() As Integer
        Get
            Return Me._generalmoviesetinfopanelstate
        End Get
        Set(ByVal value As Integer)
            Me._generalmoviesetinfopanelstate = value
        End Set
    End Property

    Public Property GeneralLanguage() As String
        Get
            Return Me._generallanguage
        End Get
        Set(ByVal value As String)
            Me._generallanguage = value
        End Set
    End Property

    Public Property MovieLevTolerance() As Integer
        Get
            Return Me._movielevtolerance
        End Get
        Set(ByVal value As Integer)
            Me._movielevtolerance = value
        End Set
    End Property

    Public Property MovieLockGenre() As Boolean
        Get
            Return Me._movielockgenre
        End Get
        Set(ByVal value As Boolean)
            Me._movielockgenre = value
        End Set
    End Property

    Public Property MovieLockOutline() As Boolean
        Get
            Return Me._movielockoutline
        End Get
        Set(ByVal value As Boolean)
            Me._movielockoutline = value
        End Set
    End Property

    Public Property MovieLockPlot() As Boolean
        Get
            Return Me._movielockplot
        End Get
        Set(ByVal value As Boolean)
            Me._movielockplot = value
        End Set
    End Property

    Public Property MovieLockRating() As Boolean
        Get
            Return Me._movielockrating
        End Get
        Set(ByVal value As Boolean)
            Me._movielockrating = value
        End Set
    End Property

    Public Property MovieLockLanguageV() As Boolean
        Get
            Return Me._movielocklanguagev
        End Get
        Set(ByVal value As Boolean)
            Me._movielocklanguagev = value
        End Set
    End Property
    Public Property MovieLockLanguageA() As Boolean
        Get
            Return Me._movielocklanguagea
        End Get
        Set(ByVal value As Boolean)
            Me._movielocklanguagea = value
        End Set
    End Property
    Public Property MovieLockMPAA() As Boolean
        Get
            Return Me._movielockmpaa
        End Get
        Set(ByVal value As Boolean)
            Me._movielockmpaa = value
        End Set
    End Property
    Public Property MovieScraperUseMPAAFSK() As Boolean
        Get
            Return Me._moviescraperusempaafsk
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperusempaafsk = value
        End Set
    End Property
    Public Property MovieLockStudio() As Boolean
        Get
            Return Me._movielockstudio
        End Get
        Set(ByVal value As Boolean)
            Me._movielockstudio = value
        End Set
    End Property

    Public Property MovieLockTagline() As Boolean
        Get
            Return Me._movielocktagline
        End Get
        Set(ByVal value As Boolean)
            Me._movielocktagline = value
        End Set
    End Property

    Public Property MovieLockTitle() As Boolean
        Get
            Return Me._movielocktitle
        End Get
        Set(ByVal value As Boolean)
            Me._movielocktitle = value
        End Set
    End Property

    Public Property MovieLockTrailer() As Boolean
        Get
            Return Me._movielocktrailer
        End Get
        Set(ByVal value As Boolean)
            Me._movielocktrailer = value
        End Set
    End Property

    Public Property MovieGeneralMarkNew() As Boolean
        Get
            Return Me._moviegeneralmarknew
        End Get
        Set(ByVal value As Boolean)
            Me._moviegeneralmarknew = value
        End Set
    End Property

    Public Property TVGeneralMarkNewEpisodes() As Boolean
        Get
            Return Me._tvgeneralmarknewepisodes
        End Get
        Set(ByVal value As Boolean)
            Me._tvgeneralmarknewepisodes = value
        End Set
    End Property

    Public Property TVGeneralMarkNewShows() As Boolean
        Get
            Return Me._tvgeneralmarknewshows
        End Get
        Set(ByVal value As Boolean)
            Me._tvgeneralmarknewshows = value
        End Set
    End Property

    Public Property MovieMetadataPerFileType() As List(Of MetadataPerType)
        Get
            Return Me._moviemetadataperfiletype
        End Get
        Set(ByVal value As List(Of MetadataPerType))
            Me._moviemetadataperfiletype = value
        End Set
    End Property

    Public Property MovieMissingBanner() As Boolean
        Get
            Return Me._moviemissingbanner
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingbanner = value
        End Set
    End Property

    Public Property MovieMissingClearArt() As Boolean
        Get
            Return Me._moviemissingclearart
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingclearart = value
        End Set
    End Property

    Public Property MovieMissingClearLogo() As Boolean
        Get
            Return Me._moviemissingclearlogo
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingclearlogo = value
        End Set
    End Property

    Public Property MovieMissingDiscArt() As Boolean
        Get
            Return Me._moviemissingdiscart
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingdiscart = value
        End Set
    End Property

    Public Property MovieMissingEThumbs() As Boolean
        Get
            Return Me._moviemissingethumbs
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingethumbs = value
        End Set
    End Property

    Public Property MovieMissingEFanarts() As Boolean
        Get
            Return Me._moviemissingefanarts
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingefanarts = value
        End Set
    End Property

    Public Property MovieMissingFanart() As Boolean
        Get
            Return Me._moviemissingfanart
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingfanart = value
        End Set
    End Property

    Public Property MovieMissingLandscape() As Boolean
        Get
            Return Me._moviemissinglandscape
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissinglandscape = value
        End Set
    End Property

    Public Property MovieMissingNFO() As Boolean
        Get
            Return Me._moviemissingnfo
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingnfo = value
        End Set
    End Property

    Public Property MovieMissingPoster() As Boolean
        Get
            Return Me._moviemissingposter
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingposter = value
        End Set
    End Property

    Public Property MovieMissingSubs() As Boolean
        Get
            Return Me._moviemissingsubs
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingsubs = value
        End Set
    End Property

    Public Property MovieMissingTheme() As Boolean
        Get
            Return Me._moviemissingtheme
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingtheme = value
        End Set
    End Property

    Public Property MovieMissingTrailer() As Boolean
        Get
            Return Me._moviemissingtrailer
        End Get
        Set(ByVal value As Boolean)
            Me._moviemissingtrailer = value
        End Set
    End Property

    Public Property MovieBannerCol() As Boolean
        Get
            Return Me._moviebannercol
        End Get
        Set(ByVal value As Boolean)
            Me._moviebannercol = value
        End Set
    End Property

    Public Property MovieClearArtCol() As Boolean
        Get
            Return Me._movieclearartcol
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearartcol = value
        End Set
    End Property

    Public Property MovieClearLogoCol() As Boolean
        Get
            Return Me._movieclearlogocol
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearlogocol = value
        End Set
    End Property

    Public Property MovieDiscArtCol() As Boolean
        Get
            Return Me._moviediscartcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviediscartcol = value
        End Set
    End Property

    Public Property MovieEFanartsCol() As Boolean
        Get
            Return Me._movieefanartscol
        End Get
        Set(ByVal value As Boolean)
            Me._movieefanartscol = value
        End Set
    End Property

    Public Property MovieEThumbsCol() As Boolean
        Get
            Return Me._movieethumbscol
        End Get
        Set(ByVal value As Boolean)
            Me._movieethumbscol = value
        End Set
    End Property

    Public Property MovieFanartCol() As Boolean
        Get
            Return Me._moviefanartcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartcol = value
        End Set
    End Property

    Public Property MovieLandscapeCol() As Boolean
        Get
            Return Me._movielandscapecol
        End Get
        Set(ByVal value As Boolean)
            Me._movielandscapecol = value
        End Set
    End Property

    Public Property MovieNFOCol() As Boolean
        Get
            Return Me._movienfocol
        End Get
        Set(ByVal value As Boolean)
            Me._movienfocol = value
        End Set
    End Property

    Public Property MoviePosterCol() As Boolean
        Get
            Return Me._moviepostercol
        End Get
        Set(ByVal value As Boolean)
            Me._moviepostercol = value
        End Set
    End Property

    Public Property MovieSubCol() As Boolean
        Get
            Return Me._moviesubcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesubcol = value
        End Set
    End Property

    Public Property MovieSetBannerCol() As Boolean
        Get
            Return Me._moviesetbannercol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetbannercol = value
        End Set
    End Property

    Public Property MovieSetClearArtCol() As Boolean
        Get
            Return Me._moviesetclearartcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclearartcol = value
        End Set
    End Property

    Public Property MovieSetClearLogoCol() As Boolean
        Get
            Return Me._moviesetclearlogocol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclearlogocol = value
        End Set
    End Property

    Public Property MovieSetDiscArtCol() As Boolean
        Get
            Return Me._moviesetdiscartcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetdiscartcol = value
        End Set
    End Property

    Public Property MovieSetFanartCol() As Boolean
        Get
            Return Me._moviesetfanartcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetfanartcol = value
        End Set
    End Property

    Public Property MovieSetLandscapeCol() As Boolean
        Get
            Return Me._moviesetlandscapecol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetlandscapecol = value
        End Set
    End Property

    Public Property MovieSetNfoCol() As Boolean
        Get
            Return Me._moviesetnfocol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetnfocol = value
        End Set
    End Property

    Public Property MovieSetPosterCol() As Boolean
        Get
            Return Me._moviesetpostercol
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetpostercol = value
        End Set
    End Property

    Public Property GeneralMovieTheme() As String
        Get
            Return Me._generalmovietheme
        End Get
        Set(ByVal value As String)
            Me._generalmovietheme = value
        End Set
    End Property

    Public Property GeneralMovieSetTheme() As String
        Get
            Return Me._generalmoviesettheme
        End Get
        Set(ByVal value As String)
            Me._generalmoviesettheme = value
        End Set
    End Property

    Public Property GeneralDaemonPath() As String
        Get
            Return Me._generaldaemonpath
        End Get
        Set(ByVal value As String)
            Me._generaldaemonpath = value
        End Set
    End Property

    Public Property GeneralDaemonDrive() As String
        Get
            Return Me._generaldaemondrive
        End Get
        Set(ByVal value As String)
            Me._generaldaemondrive = value
        End Set
    End Property

    Public Property MovieThemeCol() As Boolean
        Get
            Return Me._moviethemecol
        End Get
        Set(ByVal value As Boolean)
            Me._moviethemecol = value
        End Set
    End Property

    Public Property MovieTrailerCol() As Boolean
        Get
            Return Me._movietrailercol
        End Get
        Set(ByVal value As Boolean)
            Me._movietrailercol = value
        End Set
    End Property

    Public Property MovieWatchedCol() As Boolean
        Get
            Return Me._moviewatchedcol
        End Get
        Set(ByVal value As Boolean)
            Me._moviewatchedcol = value
        End Set
    End Property

    Public Property GeneralHideBanner() As Boolean
        Get
            Return Me._generalhidebanner
        End Get
        Set(ByVal value As Boolean)
            Me._generalhidebanner = value
        End Set
    End Property

    Public Property GeneralHideCharacterArt() As Boolean
        Get
            Return Me._generalhidecharacterart
        End Get
        Set(ByVal value As Boolean)
            Me._generalhidecharacterart = value
        End Set
    End Property

    Public Property GeneralHideClearArt() As Boolean
        Get
            Return Me._generalhideclearart
        End Get
        Set(ByVal value As Boolean)
            Me._generalhideclearart = value
        End Set
    End Property

    Public Property GeneralHideClearLogo() As Boolean
        Get
            Return Me._generalhideclearlogo
        End Get
        Set(ByVal value As Boolean)
            Me._generalhideclearlogo = value
        End Set
    End Property

    Public Property GeneralHideDiscArt() As Boolean
        Get
            Return Me._generalhidediscart
        End Get
        Set(ByVal value As Boolean)
            Me._generalhidediscart = value
        End Set
    End Property

    Public Property GeneralHideFanart() As Boolean
        Get
            Return Me._generalhidefanart
        End Get
        Set(ByVal value As Boolean)
            Me._generalhidefanart = value
        End Set
    End Property

    Public Property GeneralHideFanartSmall() As Boolean
        Get
            Return Me._generalhidefanartsmall
        End Get
        Set(ByVal value As Boolean)
            Me._generalhidefanartsmall = value
        End Set
    End Property

    Public Property GeneralHideLandscape() As Boolean
        Get
            Return Me._generalhidelandscape
        End Get
        Set(ByVal value As Boolean)
            Me._generalhidelandscape = value
        End Set
    End Property

    Public Property GeneralHidePoster() As Boolean
        Get
            Return Me._generalhideposter
        End Get
        Set(ByVal value As Boolean)
            Me._generalhideposter = value
        End Set
    End Property

    Public Property TVEpisodeFilterCustomIsEmpty() As Boolean
        Get
            Return Me._tvepisodefiltercustomisempty
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodefiltercustomisempty = value
        End Set
    End Property

    Public Property TVEpisodeNoFilter() As Boolean
        Get
            Return Me._tvepisodenofilter
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodenofilter = value
        End Set
    End Property

    Public Property MovieFilterCustomIsEmpty() As Boolean
        Get
            Return Me._moviefiltercustomisempty
        End Get
        Set(ByVal value As Boolean)
            Me._moviefiltercustomisempty = value
        End Set
    End Property

    Public Property MovieNoSaveImagesToNfo() As Boolean
        Get
            Return Me._movienosaveimagestonfo
        End Get
        Set(ByVal value As Boolean)
            Me._movienosaveimagestonfo = value
        End Set
    End Property

    Public Property TVShowFilterCustomIsEmpty() As Boolean
        Get
            Return Me._tvshowfiltercustomisempty
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfiltercustomisempty = value
        End Set
    End Property

    Public Property FileSystemNoStackExts() As List(Of String)
        Get
            Return Me._filesystemnostackexts
        End Get
        Set(ByVal value As List(Of String))
            Me._filesystemnostackexts = value
        End Set
    End Property

    Public Property MovieSortTokensIsEmpty() As Boolean
        Get
            Return Me._moviesorttokensisempty
        End Get
        Set(ByVal value As Boolean)
            Me._moviesorttokensisempty = value
        End Set
    End Property

    Public Property OMMDummyFormat() As Integer
        Get
            Return Me._ommdummyformat
        End Get
        Set(ByVal value As Integer)
            Me._ommdummyformat = value
        End Set
    End Property

    Public Property OMMDummyTagline() As String
        Get
            Return Me._ommdummytagline
        End Get
        Set(ByVal value As String)
            Me._ommdummytagline = value
        End Set
    End Property

    Public Property OMMDummyTop() As String
        Get
            Return Me._ommdummytop
        End Get
        Set(ByVal value As String)
            Me._ommdummytop = value
        End Set
    End Property

    Public Property OMMDummyUseBackground() As Boolean
        Get
            Return Me._ommdummyusebackground
        End Get
        Set(ByVal value As Boolean)
            Me._ommdummyusebackground = value
        End Set
    End Property

    Public Property OMMDummyUseFanart() As Boolean
        Get
            Return Me._ommdummyusefanart
        End Get
        Set(ByVal value As Boolean)
            Me._ommdummyusefanart = value
        End Set
    End Property

    Public Property OMMDummyUseOverlay() As Boolean
        Get
            Return Me._ommdummyuseoverlay
        End Get
        Set(ByVal value As Boolean)
            Me._ommdummyuseoverlay = value
        End Set
    End Property

    Public Property OMMMediaStubTagline() As String
        Get
            Return Me._ommmediastubtagline
        End Get
        Set(ByVal value As String)
            Me._ommmediastubtagline = value
        End Set
    End Property

    Public Property MovieScraperOnlyValueForMPAA() As Boolean
        Get
            Return Me._moviescraperonlyvalueformpaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperonlyvalueformpaa = value
        End Set
    End Property

    Public Property MovieScraperOutlineForPlot() As Boolean
        Get
            Return Me._moviescraperoutlineforplot
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperoutlineforplot = value
        End Set
    End Property

    Public Property MovieScraperOutlinePlotEnglishOverwrite() As Boolean
        Get
            Return Me._moviescraperoutlineplotenglishoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperoutlineplotenglishoverwrite = value
        End Set
    End Property

    Public Property TVASBannerOverwrite() As Boolean
        Get
            Return Me._tvasbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvasbanneroverwrite = value
        End Set
    End Property

    Public Property TVASFanartOverwrite() As Boolean
        Get
            Return Me._tvasfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvasfanartoverwrite = value
        End Set
    End Property

    Public Property TVASLandscapeOverwrite() As Boolean
        Get
            Return Me._tvaslandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvaslandscapeoverwrite = value
        End Set
    End Property

    Public Property TVASPosterOverwrite() As Boolean
        Get
            Return Me._tvasposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvasposteroverwrite = value
        End Set
    End Property

    Public Property TVEpisodeFanartOverwrite() As Boolean
        Get
            Return Me._tvepisodefanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodefanartoverwrite = value
        End Set
    End Property

    Public Property TVEpisodePosterOverwrite() As Boolean
        Get
            Return Me._tvepisodeposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodeposteroverwrite = value
        End Set
    End Property

    Public Property MovieEFanartsOverwrite() As Boolean
        Get
            Return Me._movieefanartsoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movieefanartsoverwrite = value
        End Set
    End Property

    Public Property MovieEThumbsOverwrite() As Boolean
        Get
            Return Me._movieethumbsoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movieethumbsoverwrite = value
        End Set
    End Property

    Public Property MovieFanartOverwrite() As Boolean
        Get
            Return Me._moviefanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartoverwrite = value
        End Set
    End Property

    Public Property GeneralOverwriteNfo() As Boolean
        Get
            Return Me._generaloverwritenfo
        End Get
        Set(ByVal value As Boolean)
            Me._generaloverwritenfo = value
        End Set
    End Property

    Public Property MoviePosterOverwrite() As Boolean
        Get
            Return Me._movieposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movieposteroverwrite = value
        End Set
    End Property

    Public Property TVSeasonBannerOverwrite() As Boolean
        Get
            Return Me._tvseasonbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonbanneroverwrite = value
        End Set
    End Property

    Public Property TVShowCharacterArtOverwrite() As Boolean
        Get
            Return Me._tvshowcharacterartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowcharacterartoverwrite = value
        End Set
    End Property

    Public Property TVShowClearArtOverwrite() As Boolean
        Get
            Return Me._tvshowclearartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowclearartoverwrite = value
        End Set
    End Property

    Public Property TVShowClearLogoOverwrite() As Boolean
        Get
            Return Me._tvshowclearlogooverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowclearlogooverwrite = value
        End Set
    End Property

    Public Property TVSeasonLandscapeCol() As Boolean
        Get
            Return Me._tvseasonlandscapecol
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonlandscapecol = value
        End Set
    End Property

    Public Property TVSeasonLandscapeOverwrite() As Boolean
        Get
            Return Me._tvseasonlandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonlandscapeoverwrite = value
        End Set
    End Property

    Public Property TVShowLandscapeOverwrite() As Boolean
        Get
            Return Me._tvshowlandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowlandscapeoverwrite = value
        End Set
    End Property

    Public Property TVSeasonFanartOverwrite() As Boolean
        Get
            Return Me._tvseasonfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonfanartoverwrite = value
        End Set
    End Property

    Public Property TVSeasonPosterOverwrite() As Boolean
        Get
            Return Me._tvseasonposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonposteroverwrite = value
        End Set
    End Property

    Public Property TVShowBannerOverwrite() As Boolean
        Get
            Return Me._tvshowbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowbanneroverwrite = value
        End Set
    End Property

    Public Property TVShowFanartOverwrite() As Boolean
        Get
            Return Me._tvshowfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfanartoverwrite = value
        End Set
    End Property

    Public Property TVShowPosterOverwrite() As Boolean
        Get
            Return Me._tvshowposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowposteroverwrite = value
        End Set
    End Property

    Public Property MovieBannerOverwrite() As Boolean
        Get
            Return Me._moviebanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviebanneroverwrite = value
        End Set
    End Property

    Public Property MovieDiscArtOverwrite() As Boolean
        Get
            Return Me._moviediscartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviediscartoverwrite = value
        End Set
    End Property

    Public Property MovieLandscapeOverwrite() As Boolean
        Get
            Return Me._movielandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movielandscapeoverwrite = value
        End Set
    End Property

    Public Property MovieClearArtOverwrite() As Boolean
        Get
            Return Me._movieclearartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearartoverwrite = value
        End Set
    End Property

    Public Property MovieClearLogoOverwrite() As Boolean
        Get
            Return Me._movieclearlogooverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearlogooverwrite = value
        End Set
    End Property

    Public Property MovieSetBannerOverwrite() As Boolean
        Get
            Return Me._moviesetbanneroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetbanneroverwrite = value
        End Set
    End Property

    Public Property MovieSetClearArtOverwrite() As Boolean
        Get
            Return Me._moviesetclearartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclearartoverwrite = value
        End Set
    End Property

    Public Property MovieSetClearLogoOverwrite() As Boolean
        Get
            Return Me._moviesetclearlogooverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclearlogooverwrite = value
        End Set
    End Property

    Public Property MovieSetDiscArtOverwrite() As Boolean
        Get
            Return Me._moviesetdiscartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetdiscartoverwrite = value
        End Set
    End Property

    Public Property MovieSetFanartOverwrite() As Boolean
        Get
            Return Me._moviesetfanartoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetfanartoverwrite = value
        End Set
    End Property

    Public Property MovieSetLandscapeOverwrite() As Boolean
        Get
            Return Me._moviesetlandscapeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetlandscapeoverwrite = value
        End Set
    End Property

    Public Property MovieSetPosterOverwrite() As Boolean
        Get
            Return Me._moviesetposteroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetposteroverwrite = value
        End Set
    End Property

    Public Property MovieBannerPrefOnly() As Boolean
        Get
            Return Me._moviebannerprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._moviebannerprefonly = value
        End Set
    End Property

    Public Property MovieBannerResize() As Boolean
        Get
            Return Me._moviebannerresize
        End Get
        Set(ByVal value As Boolean)
            Me._moviebannerresize = value
        End Set
    End Property

    Public Property MovieTrailerOverwrite() As Boolean
        Get
            Return Me._movietraileroverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._movietraileroverwrite = value
        End Set
    End Property

    Public Property MovieThemeOverwrite() As Boolean
        Get
            Return Me._moviethemeoverwrite
        End Get
        Set(ByVal value As Boolean)
            Me._moviethemeoverwrite = value
        End Set
    End Property
    Public Property MovieScraperPlotForOutline() As Boolean
        Get
            Return Me._moviescraperplotforoutline
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperplotforoutline = value
        End Set
    End Property

    Public Property MovieScraperOutlineLimit() As Integer
        Get
            Return Me._moviescraperoutlinelimit
        End Get
        Set(ByVal value As Integer)
            Me._moviescraperoutlinelimit = value
        End Set
    End Property

    Public Property GeneralImagesGlassOverlay() As Boolean
        Get
            Return Me._generalimagesglassoverlay
        End Get
        Set(ByVal value As Boolean)
            Me._generalimagesglassoverlay = value
        End Set
    End Property

    Public Property MoviePosterHeight() As Integer
        Get
            Return Me._movieposterheight
        End Get
        Set(ByVal value As Integer)
            Me._movieposterheight = value
        End Set
    End Property

    Public Property MoviePosterPrefOnly() As Boolean
        Get
            Return Me._movieposterprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._movieposterprefonly = value
        End Set
    End Property

    Public Property MovieBannerQual() As Integer
        Get
            Return Me._moviebannerqual
        End Get
        Set(ByVal value As Integer)
            Me._moviebannerqual = value
        End Set
    End Property

    Public Property MovieEThumbsQual() As Integer
        Get
            Return Me._movieethumbsqual
        End Get
        Set(ByVal value As Integer)
            Me._movieethumbsqual = value
        End Set
    End Property

    Public Property MovieEFanartsQual() As Integer
        Get
            Return Me._movieefanartsqual
        End Get
        Set(ByVal value As Integer)
            Me._movieefanartsqual = value
        End Set
    End Property

    Public Property MoviePosterQual() As Integer
        Get
            Return Me._movieposterqual
        End Get
        Set(ByVal value As Integer)
            Me._movieposterqual = value
        End Set
    End Property

    Public Property MoviePosterWidth() As Integer
        Get
            Return Me._movieposterwidth
        End Get
        Set(ByVal value As Integer)
            Me._movieposterwidth = value
        End Set
    End Property

    Public Property TVASPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Me._tvasposterprefsize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Me._tvasposterprefsize = value
        End Set
    End Property

    Public Property TVEpisodeFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Me._tvepisodefanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Me._tvepisodefanartprefsize = value
        End Set
    End Property

    Public Property MovieFanartPrefSize() As Enums.FanartSize
        Get
            Return Me._moviefanartprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Me._moviefanartprefsize = value
        End Set
    End Property

    Public Property MovieEFanartsPrefSize() As Enums.FanartSize
        Get
            Return Me._movieefanartsprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Me._movieefanartsprefsize = value
        End Set
    End Property

    Public Property MovieEThumbsPrefSize() As Enums.FanartSize
        Get
            Return Me._movieethumbsprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Me._movieethumbsprefsize = value
        End Set
    End Property

    Public Property MoviePosterPrefSize() As Enums.PosterSize
        Get
            Return Me._movieposterprefsize
        End Get
        Set(ByVal value As Enums.PosterSize)
            Me._movieposterprefsize = value
        End Set
    End Property

    Public Property TVSeasonFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Me._tvseasonfanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Me._tvseasonfanartprefsize = value
        End Set
    End Property

    Public Property TVASFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Me._tvasfanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Me._tvasfanartprefsize = value
        End Set
    End Property

    Public Property TVSeasonPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Me._tvseasonposterprefsize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Me._tvseasonposterprefsize = value
        End Set
    End Property

    Public Property TVShowBannerPrefType() As Enums.TVShowBannerType
        Get
            Return Me._tvshowbannerpreftype
        End Get
        Set(ByVal value As Enums.TVShowBannerType)
            Me._tvshowbannerpreftype = value
        End Set
    End Property

    Public Property MovieBannerPrefType() As Enums.MovieBannerType
        Get
            Return Me._moviebannerpreftype
        End Get
        Set(ByVal value As Enums.MovieBannerType)
            Me._moviebannerpreftype = value
        End Set
    End Property

    Public Property TVASBannerPrefType() As Enums.TVShowBannerType
        Get
            Return Me._tvasbannerpreftype
        End Get
        Set(ByVal value As Enums.TVShowBannerType)
            Me._tvasbannerpreftype = value
        End Set
    End Property

    Public Property TVSeasonBannerPrefType() As Enums.TVSeasonBannerType
        Get
            Return Me._tvseasonbannerpreftype
        End Get
        Set(ByVal value As Enums.TVSeasonBannerType)
            Me._tvseasonbannerpreftype = value
        End Set
    End Property

    Public Property TVShowFanartPrefSize() As Enums.TVFanartSize
        Get
            Return Me._tvshowfanartprefsize
        End Get
        Set(ByVal value As Enums.TVFanartSize)
            Me._tvshowfanartprefsize = value
        End Set
    End Property

    Public Property TVShowPosterPrefSize() As Enums.TVPosterSize
        Get
            Return Me._tvshowposterprefsize
        End Get
        Set(ByVal value As Enums.TVPosterSize)
            Me._tvshowposterprefsize = value
        End Set
    End Property

    Public Property MovieTrailerMinQual() As Enums.TrailerQuality
        Get
            Return Me._movietrailerminqual
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            Me._movietrailerminqual = value
        End Set
    End Property

    Public Property MovieTrailerPrefQual() As Enums.TrailerQuality
        Get
            Return Me._movietrailerprefqual
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            Me._movietrailerprefqual = value
        End Set
    End Property

    Public Property MovieProperCase() As Boolean
        Get
            Return Me._moviepropercase
        End Get
        Set(ByVal value As Boolean)
            Me._moviepropercase = value
        End Set
    End Property

    Public Property ProxyCreds() As NetworkCredential
        Get
            Return Me._proxycredentials
        End Get
        Set(ByVal value As NetworkCredential)
            Me._proxycredentials = value
        End Set
    End Property

    Public Property ProxyPort() As Integer
        Get
            Return Me._proxyport
        End Get
        Set(ByVal value As Integer)
            Me._proxyport = value
        End Set
    End Property

    Public Property ProxyURI() As String
        Get
            Return Me._proxyuri
        End Get
        Set(ByVal value As String)
            Me._proxyuri = value
        End Set
    End Property

    Public Property TVASBannerResize() As Boolean
        Get
            Return Me._tvasbannerresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvasbannerresize = value
        End Set
    End Property

    Public Property TVASPosterResize() As Boolean
        Get
            Return Me._tvasposterresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvasposterresize = value
        End Set
    End Property

    Public Property TVASFanartResize() As Boolean
        Get
            Return Me._tvasfanartresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvasfanartresize = value
        End Set
    End Property

    Public Property TVEpisodeFanartResize() As Boolean
        Get
            Return Me._tvepisodefanartresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodefanartresize = value
        End Set
    End Property

    Public Property TVEpisodePosterResize() As Boolean
        Get
            Return Me._tvepisodeposterresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodeposterresize = value
        End Set
    End Property

    Public Property MovieEFanartsResize() As Boolean
        Get
            Return Me._movieefanartsresize
        End Get
        Set(ByVal value As Boolean)
            Me._movieefanartsresize = value
        End Set
    End Property

    Public Property MovieEThumbsResize() As Boolean
        Get
            Return Me._movieethumbsresize
        End Get
        Set(ByVal value As Boolean)
            Me._movieethumbsresize = value
        End Set
    End Property

    Public Property MovieFanartResize() As Boolean
        Get
            Return Me._moviefanartresize
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartresize = value
        End Set
    End Property

    Public Property MoviePosterResize() As Boolean
        Get
            Return Me._movieposterresize
        End Get
        Set(ByVal value As Boolean)
            Me._movieposterresize = value
        End Set
    End Property

    Public Property TVSeasonBannerResize() As Boolean
        Get
            Return Me._tvseasonbannerresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonbannerresize = value
        End Set
    End Property

    Public Property TVSeasonFanartResize() As Boolean
        Get
            Return Me._tvseasonfanartresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonfanartresize = value
        End Set
    End Property

    Public Property TVSeasonPosterResize() As Boolean
        Get
            Return Me._tvseasonposterresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonposterresize = value
        End Set
    End Property

    Public Property TVShowBannerResize() As Boolean
        Get
            Return Me._tvshowbannerresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowbannerresize = value
        End Set
    End Property

    Public Property TVShowFanartResize() As Boolean
        Get
            Return Me._tvshowfanartresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfanartresize = value
        End Set
    End Property

    Public Property TVShowPosterResize() As Boolean
        Get
            Return Me._tvshowposterresize
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowposterresize = value
        End Set
    End Property

    Public Property MovieScraperDurationRuntimeFormat() As String
        Get
            Return Me._moviescraperdurationruntimeformat
        End Get
        Set(ByVal value As String)
            Me._moviescraperdurationruntimeformat = value
        End Set
    End Property

    Public Property TVScraperDurationRuntimeFormat() As String
        Get
            Return Me._tvscraperdurationruntimeformat
        End Get
        Set(ByVal value As String)
            Me._tvscraperdurationruntimeformat = value
        End Set
    End Property

    Public Property MovieScraperMetaDataScan() As Boolean
        Get
            Return Me._moviescrapermetadatascan
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapermetadatascan = value
        End Set
    End Property

    Public Property MovieScanOrderModify() As Boolean
        Get
            Return Me._moviescanordermodify
        End Get
        Set(ByVal value As Boolean)
            Me._moviescanordermodify = value
        End Set
    End Property

    Public Property TVScraperMetaDataScan() As Boolean
        Get
            Return Me._tvscrapermetadatascan
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapermetadatascan = value
        End Set
    End Property

    Public Property TVScraperEpisodeActors() As Boolean
        Get
            Return Me._tvscraperepisodeactors
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodeactors = value
        End Set
    End Property

    Public Property TVScraperEpisodeAired() As Boolean
        Get
            Return Me._tvscraperepisodeaired
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodeaired = value
        End Set
    End Property

    Public Property TVScraperEpisodeCredits() As Boolean
        Get
            Return Me._tvscraperepisodecredits
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodecredits = value
        End Set
    End Property

    Public Property TVScraperEpisodeDirector() As Boolean
        Get
            Return Me._tvscraperepisodedirector
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodedirector = value
        End Set
    End Property

    Public Property TVScraperEpisodeEpisode() As Boolean
        Get
            Return Me._tvscraperepisodeepisode
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodeepisode = value
        End Set
    End Property

    Public Property TVScraperEpisodePlot() As Boolean
        Get
            Return Me._tvscraperepisodeplot
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodeplot = value
        End Set
    End Property

    Public Property TVScraperEpisodeRating() As Boolean
        Get
            Return Me._tvscraperepisoderating
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisoderating = value
        End Set
    End Property

    Public Property TVScraperEpisodeSeason() As Boolean
        Get
            Return Me._tvscraperepisodeseason
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodeseason = value
        End Set
    End Property

    Public Property TVScraperEpisodeTitle() As Boolean
        Get
            Return Me._tvscraperepisodetitle
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisodetitle = value
        End Set
    End Property

    Public Property TVScraperShowActors() As Boolean
        Get
            Return Me._tvscrapershowactors
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowactors = value
        End Set
    End Property

    Public Property TVScraperShowEpiGuideURL() As Boolean
        Get
            Return Me._tvscrapershowepiguideurl
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowepiguideurl = value
        End Set
    End Property

    Public Property TVScraperShowGenre() As Boolean
        Get
            Return Me._tvscrapershowgenre
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowgenre = value
        End Set
    End Property

    Public Property TVScraperShowMPAA() As Boolean
        Get
            Return Me._tvscrapershowmpaa
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowmpaa = value
        End Set
    End Property

    Public Property TVScraperShowPlot() As Boolean
        Get
            Return Me._tvscrapershowplot
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowplot = value
        End Set
    End Property

    Public Property TVScraperShowPremiered() As Boolean
        Get
            Return Me._tvscrapershowpremiered
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowpremiered = value
        End Set
    End Property

    Public Property TVScraperShowRating() As Boolean
        Get
            Return Me._tvscrapershowrating
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowrating = value
        End Set
    End Property

    Public Property TVScraperShowStatus() As Boolean
        Get
            Return Me._tvscrapershowstatus
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowstatus = value
        End Set
    End Property

    Public Property TVScraperShowStudio() As Boolean
        Get
            Return Me._tvscrapershowstudio
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowstudio = value
        End Set
    End Property

    Public Property TVScraperShowTitle() As Boolean
        Get
            Return Me._tvscrapershowtitle
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowtitle = value
        End Set
    End Property

    Public Property TVSeasonFanartCol() As Boolean
        Get
            Return Me._tvseasonfanartcol
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonfanartcol = value
        End Set
    End Property

    Public Property TVSeasonFanartHeight() As Integer
        Get
            Return Me._tvseasonfanartheight
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonfanartheight = value
        End Set
    End Property

    Public Property TVSeasonFanartQual() As Integer
        Get
            Return Me._tvseasonfanartqual
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonfanartqual = value
        End Set
    End Property

    Public Property TVSeasonFanartWidth() As Integer
        Get
            Return Me._tvseasonfanartwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonfanartwidth = value
        End Set
    End Property

    Public Property TVSeasonBannerCol() As Boolean
        Get
            Return Me._tvseasonbannercol
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonbannercol = value
        End Set
    End Property

    Public Property TVASBannerWidth() As Integer
        Get
            Return Me._tvasbannerwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvasbannerwidth = value
        End Set
    End Property

    Public Property TVSeasonBannerWidth() As Integer
        Get
            Return Me._tvseasonbannerwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonbannerwidth = value
        End Set
    End Property

    Public Property TVASFanartWidth() As Integer
        Get
            Return Me._tvasfanartwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvasfanartwidth = value
        End Set
    End Property

    Public Property TVShowBannerWidth() As Integer
        Get
            Return Me._tvshowbannerwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvshowbannerwidth = value
        End Set
    End Property

    Public Property MovieBannerWidth() As Integer
        Get
            Return Me._moviebannerwidth
        End Get
        Set(ByVal value As Integer)
            Me._moviebannerwidth = value
        End Set
    End Property

    Public Property TVASBannerHeight() As Integer
        Get
            Return Me._tvasbannerheight
        End Get
        Set(ByVal value As Integer)
            Me._tvasbannerheight = value
        End Set
    End Property

    Public Property TVSeasonBannerHeight() As Integer
        Get
            Return Me._tvseasonbannerheight
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonbannerheight = value
        End Set
    End Property

    Public Property TVASFanartHeight() As Integer
        Get
            Return Me._tvasfanartheight
        End Get
        Set(ByVal value As Integer)
            Me._tvasfanartheight = value
        End Set
    End Property

    Public Property TVShowBannerHeight() As Integer
        Get
            Return Me._tvshowbannerheight
        End Get
        Set(ByVal value As Integer)
            Me._tvshowbannerheight = value
        End Set
    End Property

    Public Property MovieBannerHeight() As Integer
        Get
            Return Me._moviebannerheight
        End Get
        Set(ByVal value As Integer)
            Me._moviebannerheight = value
        End Set
    End Property

    Public Property TVSeasonPosterCol() As Boolean
        Get
            Return Me._tvseasonpostercol
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonpostercol = value
        End Set
    End Property

    Public Property TVSeasonPosterHeight() As Integer
        Get
            Return Me._tvseasonposterheight
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonposterheight = value
        End Set
    End Property

    Public Property TVSeasonPosterQual() As Integer
        Get
            Return Me._tvseasonposterqual
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonposterqual = value
        End Set
    End Property

    Public Property TVSeasonPosterWidth() As Integer
        Get
            Return Me._tvseasonposterwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvseasonposterwidth = value
        End Set
    End Property

    Public Property MovieSets() As List(Of String)
        Get
            Return Me._moviesets
        End Get
        Set(ByVal value As List(Of String))
            Me._moviesets = value
        End Set
    End Property

    Public Property GeneralShowImgDims() As Boolean
        Get
            Return Me._generalshowimgdims
        End Get
        Set(ByVal value As Boolean)
            Me._generalshowimgdims = value
        End Set
    End Property

    Public Property TVShowFanartCol() As Boolean
        Get
            Return Me._tvshowfanartcol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfanartcol = value
        End Set
    End Property

    Public Property TVShowFanartHeight() As Integer
        Get
            Return Me._tvshowfanartheight
        End Get
        Set(ByVal value As Integer)
            Me._tvshowfanartheight = value
        End Set
    End Property

    Public Property TVShowFanartQual() As Integer
        Get
            Return Me._tvshowfanartqual
        End Get
        Set(ByVal value As Integer)
            Me._tvshowfanartqual = value
        End Set
    End Property

    Public Property TVShowFanartWidth() As Integer
        Get
            Return Me._tvshowfanartwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvshowfanartwidth = value
        End Set
    End Property

    Public Property TVShowFilterCustom() As List(Of String)
        Get
            Return Me._tvshowfiltercustom
        End Get
        Set(ByVal value As List(Of String))
            Me._tvshowfiltercustom = value
        End Set
    End Property

    Public Property GeneralTVShowInfoPanelState() As Integer
        Get
            Return Me._generaltvshowinfopanelstate
        End Get
        Set(ByVal value As Integer)
            Me._generaltvshowinfopanelstate = value
        End Set
    End Property

    Public Property TVLockShowGenre() As Boolean
        Get
            Return Me._tvlockshowgenre
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowgenre = value
        End Set
    End Property

    Public Property TVLockShowPlot() As Boolean
        Get
            Return Me._tvlockshowplot
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowplot = value
        End Set
    End Property

    Public Property TVLockShowRating() As Boolean
        Get
            Return Me._tvlockshowrating
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowrating = value
        End Set
    End Property

    Public Property TVLockShowStatus() As Boolean
        Get
            Return Me._tvlockshowstatus
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowstatus = value
        End Set
    End Property

    Public Property TVLockShowStudio() As Boolean
        Get
            Return Me._tvlockshowstudio
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowstudio = value
        End Set
    End Property

    Public Property TVLockShowTitle() As Boolean
        Get
            Return Me._tvlockshowtitle
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowtitle = value
        End Set
    End Property

    Public Property TVShowBannerCol() As Boolean
        Get
            Return Me._tvshowbannercol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowbannercol = value
        End Set
    End Property

    Public Property TVShowCharacterArtCol() As Boolean
        Get
            Return Me._tvshowcharacterartcol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowcharacterartcol = value
        End Set
    End Property

    Public Property TVShowClearArtCol() As Boolean
        Get
            Return Me._tvshowclearartcol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowclearartcol = value
        End Set
    End Property

    Public Property TVShowClearLogoCol() As Boolean
        Get
            Return Me._tvshowclearlogocol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowclearlogocol = value
        End Set
    End Property

    Public Property TVShowEFanartsCol() As Boolean
        Get
            Return Me._tvshowefanartscol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowefanartscol = value
        End Set
    End Property

    Public Property TVShowLandscapeCol() As Boolean
        Get
            Return Me._tvshowlandscapecol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowlandscapecol = value
        End Set
    End Property

    Public Property TVShowThemeCol() As Boolean
        Get
            Return Me._tvshowthemecol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowthemecol = value
        End Set
    End Property

    Public Property TVShowNfoCol() As Boolean
        Get
            Return Me._tvshownfocol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshownfocol = value
        End Set
    End Property

    Public Property TVShowPosterCol() As Boolean
        Get
            Return Me._tvshowpostercol
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowpostercol = value
        End Set
    End Property

    Public Property TVShowPosterHeight() As Integer
        Get
            Return Me._tvshowposterheight
        End Get
        Set(ByVal value As Integer)
            Me._tvshowposterheight = value
        End Set
    End Property

    Public Property TVShowPosterQual() As Integer
        Get
            Return Me._tvshowposterqual
        End Get
        Set(ByVal value As Integer)
            Me._tvshowposterqual = value
        End Set
    End Property

    Public Property TVShowPosterWidth() As Integer
        Get
            Return Me._tvshowposterwidth
        End Get
        Set(ByVal value As Integer)
            Me._tvshowposterwidth = value
        End Set
    End Property

    Public Property TVShowProperCase() As Boolean
        Get
            Return Me._tvshowpropercase
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowpropercase = value
        End Set
    End Property

    Public Property TVScraperRatingRegion() As String
        Get
            Return Me._tvscraperratingregion
        End Get
        Set(ByVal value As String)
            Me._tvscraperratingregion = value
        End Set
    End Property

    Public Property MovieSkipLessThan() As Integer
        Get
            Return Me._movieskiplessthan
        End Get
        Set(ByVal value As Integer)
            Me._movieskiplessthan = value
        End Set
    End Property

    Public Property MovieSkipStackedSizeCheck() As Boolean
        Get
            Return Me._movieskipstackedsizecheck
        End Get
        Set(ByVal value As Boolean)
            Me._movieskipstackedsizecheck = value
        End Set
    End Property

    Public Property TVSkipLessThan() As Integer
        Get
            Return Me._tvskiplessthan
        End Get
        Set(ByVal value As Integer)
            Me._tvskiplessthan = value
        End Set
    End Property

    Public Property MovieSortBeforeScan() As Boolean
        Get
            Return Me._moviesortbeforescan
        End Get
        Set(ByVal value As Boolean)
            Me._moviesortbeforescan = value
        End Set
    End Property

    Public Property SortPath() As String
        Get
            Return Me._sortpath
        End Get
        Set(ByVal value As String)
            Me._sortpath = value
        End Set
    End Property

    Public Property MovieSortTokens() As List(Of String)
        Get
            Return Me._moviesorttokens
        End Get
        Set(ByVal value As List(Of String))
            Me._moviesorttokens = value
        End Set
    End Property

    Public Property GeneralSourceFromFolder() As Boolean
        Get
            Return Me._generalsourcefromfolder
        End Get
        Set(ByVal value As Boolean)
            Me._generalsourcefromfolder = value
        End Set
    End Property

    Public Property GeneralMainSplitterPanelState() As Integer
        Get
            Return Me._generalmainsplitterpanelstate
        End Get
        Set(ByVal value As Integer)
            Me._generalmainsplitterpanelstate = value
        End Set
    End Property

    Public Property GeneralShowSplitterPanelState() As Integer
        Get
            Return Me._generalshowsplitterpanelstate
        End Get
        Set(ByVal value As Integer)
            Me._generalshowsplitterpanelstate = value
        End Set
    End Property

    Public Property GeneralSeasonSplitterPanelState() As Integer
        Get
            Return Me._generalseasonsplitterpanelstate
        End Get
        Set(ByVal value As Integer)
            Me._generalseasonsplitterpanelstate = value
        End Set
    End Property

    Public Property TVCleanDB() As Boolean
        Get
            Return Me._tvcleandb
        End Get
        Set(ByVal value As Boolean)
            Me._tvcleandb = value
        End Set
    End Property

    Public Property GeneralTVEpisodeTheme() As String
        Get
            Return Me._generaltvepisodetheme
        End Get
        Set(ByVal value As String)
            Me._generaltvepisodetheme = value
        End Set
    End Property

    Public Property TVGeneralFlagLang() As String
        Get
            Return Me._tvgeneralflaglang
        End Get
        Set(ByVal value As String)
            Me._tvgeneralflaglang = value
        End Set
    End Property

    Public Property TVGeneralIgnoreLastScan() As Boolean
        Get
            Return Me._tvgeneralignorelastscan
        End Get
        Set(ByVal value As Boolean)
            Me._tvgeneralignorelastscan = value
        End Set
    End Property

    Public Property TVMetadataPerFileType() As List(Of MetadataPerType)
        Get
            Return Me._tvmetadataperfiletype
        End Get
        Set(ByVal value As List(Of MetadataPerType))
            Me._tvmetadataperfiletype = value
        End Set
    End Property

    Public Property TVScanOrderModify() As Boolean
        Get
            Return Me._tvscanordermodify
        End Get
        Set(ByVal value As Boolean)
            Me._tvscanordermodify = value
        End Set
    End Property

    Public Property TVShowRegexes() As List(Of TVShowRegEx)
        Get
            Return Me._tvshowregexes
        End Get
        Set(ByVal value As List(Of TVShowRegEx))
            Me._tvshowregexes = value
        End Set
    End Property

    Public Property GeneralTVShowTheme() As String
        Get
            Return Me._generaltvshowtheme
        End Get
        Set(ByVal value As String)
            Me._generaltvshowtheme = value
        End Set
    End Property

    Public Property TVScraperUpdateTime() As Enums.TVScraperUpdateTime
        Get
            Return Me._tvscraperupdatetime
        End Get
        Set(ByVal value As Enums.TVScraperUpdateTime)
            Me._tvscraperupdatetime = value
        End Set
    End Property

    Public Property MovieTrailerEnable() As Boolean
        Get
            Return Me._movietrailerenable
        End Get
        Set(ByVal value As Boolean)
            Me._movietrailerenable = value
        End Set
    End Property

    Public Property MovieThemeEnable() As Boolean
        Get
            Return Me._moviethemeenable
        End Get
        Set(ByVal value As Boolean)
            Me._moviethemeenable = value
        End Set
    End Property

    Public Property MovieScraperCertForMPAA() As Boolean
        Get
            Return Me._moviescrapercertformpaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercertformpaa = value
        End Set
    End Property

    Public Property MovieScraperUseMDDuration() As Boolean
        Get
            Return Me._moviescraperusemdduration
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperusemdduration = value
        End Set
    End Property

    Public Property TVScraperUseMDDuration() As Boolean
        Get
            Return Me._tvscraperusemdduration
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperusemdduration = value
        End Set
    End Property

    Public Property FileSystemValidExts() As List(Of String)
        Get
            Return Me._filesystemvalidexts
        End Get
        Set(ByVal value As List(Of String))
            Me._filesystemvalidexts = value
        End Set
    End Property

    Public Property FileSystemValidThemeExts() As List(Of String)
        Get
            Return Me._filesystemvalidthemeexts
        End Get
        Set(ByVal value As List(Of String))
            Me._filesystemvalidthemeexts = value
        End Set
    End Property

    Public Property Version() As String
        Get
            Return Me._version
        End Get
        Set(ByVal value As String)
            Me._version = value
        End Set
    End Property

    Public Property GeneralWindowLoc() As Point
        Get
            Return Me._generalwindowloc
        End Get
        Set(ByVal value As Point)
            Me._generalwindowloc = value
        End Set
    End Property

    Public Property GeneralWindowSize() As Size
        Get
            Return Me._generalwindowsize
        End Get
        Set(ByVal value As Size)
            Me._generalwindowsize = value
        End Set
    End Property

    Public Property GeneralWindowState() As FormWindowState
        Get
            Return Me._generalwindowstate
        End Get
        Set(ByVal value As FormWindowState)
            Me._generalwindowstate = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me._password
        End Get
        Set(ByVal value As String)
            Me._password = value
        End Set
    End Property

    Public Property TraktUsername() As String
        Get
            Return Me._traktusername
        End Get
        Set(ByVal value As String)
            Me._traktusername = value
        End Set
    End Property

    Public Property TraktPassword() As String
        Get
            Return Me._traktpassword
        End Get
        Set(ByVal value As String)
            Me._traktpassword = value
        End Set
    End Property

    Public Property UseTrakt() As Boolean
        Get
            Return Me._usetrakt
        End Get
        Set(ByVal value As Boolean)
            Me._usetrakt = value
        End Set
    End Property

    Public Property GeneralCreationDate() As Boolean
        Get
            Return Me._generalcreationdate
        End Get
        Set(ByVal value As Boolean)
            Me._generalcreationdate = value
        End Set
    End Property

    Public Property MovieUseFrodo() As Boolean
        Get
            Return Me._movieusefrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieusefrodo = value
        End Set
    End Property

    Public Property MovieActorThumbsFrodo() As Boolean
        Get
            Return Me._movieactorthumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsfrodo = value
        End Set
    End Property

    Public Property MovieBannerFrodo() As Boolean
        Get
            Return Me._moviebannerfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._moviebannerfrodo = value
        End Set
    End Property

    Public Property MovieClearArtFrodo() As Boolean
        Get
            Return Me._movieclearartfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearartfrodo = value
        End Set
    End Property

    Public Property MovieClearLogoFrodo() As Boolean
        Get
            Return Me._movieclearlogofrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearlogofrodo = value
        End Set
    End Property

    Public Property MovieDiscArtFrodo() As Boolean
        Get
            Return Me._moviediscartfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._moviediscartfrodo = value
        End Set
    End Property

    Public Property MovieExtrafanartsFrodo() As Boolean
        Get
            Return Me._movieextrafanartsfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartsfrodo = value
        End Set
    End Property

    Public Property MovieExtrathumbsFrodo() As Boolean
        Get
            Return Me._movieextrathumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbsfrodo = value
        End Set
    End Property

    Public Property MovieFanartFrodo() As Boolean
        Get
            Return Me._moviefanartfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartfrodo = value
        End Set
    End Property

    Public Property MovieLandscapeFrodo() As Boolean
        Get
            Return Me._movielandscapefrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movielandscapefrodo = value
        End Set
    End Property

    Public Property MovieNFOFrodo() As Boolean
        Get
            Return Me._movienfofrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movienfofrodo = value
        End Set
    End Property

    Public Property MoviePosterFrodo() As Boolean
        Get
            Return Me._movieposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movieposterfrodo = value
        End Set
    End Property

    Public Property MovieTrailerFrodo() As Boolean
        Get
            Return Me._movietrailerfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._movietrailerfrodo = value
        End Set
    End Property

    Public Property MovieUseEden() As Boolean
        Get
            Return Me._movieuseeden
        End Get
        Set(ByVal value As Boolean)
            Me._movieuseeden = value
        End Set
    End Property

    Public Property MovieActorThumbsEden() As Boolean
        Get
            Return Me._movieactorthumbseden
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbseden = value
        End Set
    End Property

    Public Property MovieBannerEden() As Boolean
        Get
            Return Me._moviebannereden
        End Get
        Set(ByVal value As Boolean)
            Me._moviebannereden = value
        End Set
    End Property

    Public Property MovieClearArtEden() As Boolean
        Get
            Return Me._moviecleararteden
        End Get
        Set(ByVal value As Boolean)
            Me._moviecleararteden = value
        End Set
    End Property

    Public Property MovieClearLogoEden() As Boolean
        Get
            Return Me._movieclearlogoeden
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearlogoeden = value
        End Set
    End Property

    Public Property MovieDiscArtEden() As Boolean
        Get
            Return Me._moviediscarteden
        End Get
        Set(ByVal value As Boolean)
            Me._moviediscarteden = value
        End Set
    End Property

    Public Property MovieExtrafanartsEden() As Boolean
        Get
            Return Me._movieextrafanartseden
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartseden = value
        End Set
    End Property

    Public Property MovieExtrathumbsEden() As Boolean
        Get
            Return Me._movieextrathumbseden
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbseden = value
        End Set
    End Property

    Public Property MovieFanartEden() As Boolean
        Get
            Return Me._moviefanarteden
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanarteden = value
        End Set
    End Property

    Public Property MovieLandscapeEden() As Boolean
        Get
            Return Me._movielandscapeeden
        End Get
        Set(ByVal value As Boolean)
            Me._movielandscapeeden = value
        End Set
    End Property

    Public Property MovieNFOEden() As Boolean
        Get
            Return Me._movienfoeden
        End Get
        Set(ByVal value As Boolean)
            Me._movienfoeden = value
        End Set
    End Property

    Public Property MoviePosterEden() As Boolean
        Get
            Return Me._moviepostereden
        End Get
        Set(ByVal value As Boolean)
            Me._moviepostereden = value
        End Set
    End Property

    Public Property MovieTrailerEden() As Boolean
        Get
            Return Me._movietrailereden
        End Get
        Set(ByVal value As Boolean)
            Me._movietrailereden = value
        End Set
    End Property

    Public Property MovieXBMCThemeEnable() As Boolean
        Get
            Return Me._moviexbmcthemeenable
        End Get
        Set(ByVal value As Boolean)
            Me._moviexbmcthemeenable = value
        End Set
    End Property

    Public Property MovieXBMCThemeCustom() As Boolean
        Get
            Return Me._moviexbmcthemecustom
        End Get
        Set(ByVal value As Boolean)
            Me._moviexbmcthemecustom = value
        End Set
    End Property

    Public Property MovieXBMCThemeMovie() As Boolean
        Get
            Return Me._moviexbmcthememovie
        End Get
        Set(ByVal value As Boolean)
            Me._moviexbmcthememovie = value
        End Set
    End Property

    Public Property MovieXBMCThemeSub() As Boolean
        Get
            Return Me._moviexbmcthemesub
        End Get
        Set(ByVal value As Boolean)
            Me._moviexbmcthemesub = value
        End Set
    End Property

    Public Property MovieXBMCThemeCustomPath() As String
        Get
            Return Me._moviexbmcthemecustompath
        End Get
        Set(ByVal value As String)
            Me._moviexbmcthemecustompath = value
        End Set
    End Property

    Public Property MovieXBMCThemeSubDir() As String
        Get
            Return Me._moviexbmcthemesubdir
        End Get
        Set(ByVal value As String)
            Me._moviexbmcthemesubdir = value
        End Set
    End Property

    Public Property MovieXBMCTrailerFormat() As Boolean
        Get
            Return Me._moviexbmctrailerformat
        End Get
        Set(ByVal value As Boolean)
            Me._moviexbmctrailerformat = value
        End Set
    End Property

    Public Property MovieXBMCProtectVTSBDMV() As Boolean
        Get
            Return Me._moviexbmcprotectvtsbdmv
        End Get
        Set(ByVal value As Boolean)
            Me._moviexbmcprotectvtsbdmv = value
        End Set
    End Property

    Public Property MovieUseYAMJ() As Boolean
        Get
            Return Me._movieuseyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieuseyamj = value
        End Set
    End Property

    Public Property MovieBannerYAMJ() As Boolean
        Get
            Return Me._moviebanneryamj
        End Get
        Set(ByVal value As Boolean)
            Me._moviebanneryamj = value
        End Set
    End Property

    Public Property MovieFanartYAMJ() As Boolean
        Get
            Return Me._moviefanartyamj
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartyamj = value
        End Set
    End Property

    Public Property MovieNFOYAMJ() As Boolean
        Get
            Return Me._movienfoyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movienfoyamj = value
        End Set
    End Property

    Public Property MoviePosterYAMJ() As Boolean
        Get
            Return Me._movieposteryamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieposteryamj = value
        End Set
    End Property

    Public Property MovieTrailerYAMJ() As Boolean
        Get
            Return Me._movietraileryamj
        End Get
        Set(ByVal value As Boolean)
            Me._movietraileryamj = value
        End Set
    End Property

    Public Property MovieYAMJCompatibleSets() As Boolean
        Get
            Return Me._movieyamjcompatiblesets
        End Get
        Set(ByVal value As Boolean)
            Me._movieyamjcompatiblesets = value
        End Set
    End Property

    Public Property MovieYAMJWatchedFile() As Boolean
        Get
            Return Me._movieyamjwatchedfile
        End Get
        Set(ByVal value As Boolean)
            Me._movieyamjwatchedfile = value
        End Set
    End Property

    Public Property MovieYAMJWatchedFolder() As String
        Get
            Return Me._movieyamjwatchedfolder
        End Get
        Set(ByVal value As String)
            Me._movieyamjwatchedfolder = value
        End Set
    End Property

    Public Property MovieUseNMJ() As Boolean
        Get
            Return Me._movieusenmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieusenmj = value
        End Set
    End Property

    Public Property MovieBannerNMJ() As Boolean
        Get
            Return Me._moviebannernmj
        End Get
        Set(ByVal value As Boolean)
            Me._moviebannernmj = value
        End Set
    End Property

    Public Property MovieFanartNMJ() As Boolean
        Get
            Return Me._moviefanartnmj
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartnmj = value
        End Set
    End Property

    Public Property MovieNFONMJ() As Boolean
        Get
            Return Me._movienfonmj
        End Get
        Set(ByVal value As Boolean)
            Me._movienfonmj = value
        End Set
    End Property

    Public Property MoviePosterNMJ() As Boolean
        Get
            Return Me._movieposternmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieposternmj = value
        End Set
    End Property

    Public Property MovieTrailerNMJ() As Boolean
        Get
            Return Me._movietrailernmj
        End Get
        Set(ByVal value As Boolean)
            Me._movietrailernmj = value
        End Set
    End Property

    Public Property MovieUseBoxee() As Boolean
        Get
            Return Me._movieuseboxee
        End Get
        Set(ByVal value As Boolean)
            Me._movieuseboxee = value
        End Set
    End Property

    Public Property MovieFanartBoxee() As Boolean
        Get
            Return Me._moviefanartboxee
        End Get
        Set(ByVal value As Boolean)
            Me._moviefanartboxee = value
        End Set
    End Property

    Public Property MovieNFOBoxee() As Boolean
        Get
            Return Me._movienfoboxee
        End Get
        Set(ByVal value As Boolean)
            Me._movienfoboxee = value
        End Set
    End Property

    Public Property MoviePosterBoxee() As Boolean
        Get
            Return Me._movieposterboxee
        End Get
        Set(ByVal value As Boolean)
            Me._movieposterboxee = value
        End Set
    End Property

    Public Property MovieUseExpert() As Boolean
        Get
            Return Me._movieuseexpert
        End Get
        Set(ByVal value As Boolean)
            Me._movieuseexpert = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertSingle() As Boolean
        Get
            Return Me._movieactorthumbsexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsexpertsingle = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertSingle() As String
        Get
            Return Me._movieactorthumbsextexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movieactorthumbsextexpertsingle = value
        End Set
    End Property

    Public Property MovieBannerExpertSingle() As String
        Get
            Return Me._moviebannerexpertsingle
        End Get
        Set(ByVal value As String)
            Me._moviebannerexpertsingle = value
        End Set
    End Property

    Public Property MovieClearArtExpertSingle() As String
        Get
            Return Me._movieclearartexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movieclearartexpertsingle = value
        End Set
    End Property

    Public Property MovieClearLogoExpertSingle() As String
        Get
            Return Me._movieclearlogoexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movieclearlogoexpertsingle = value
        End Set
    End Property

    Public Property MovieDiscArtExpertSingle() As String
        Get
            Return Me._moviediscartexpertsingle
        End Get
        Set(ByVal value As String)
            Me._moviediscartexpertsingle = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertSingle() As Boolean
        Get
            Return Me._movieextrafanartsexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartsexpertsingle = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertSingle() As Boolean
        Get
            Return Me._movieextrathumbsexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbsexpertsingle = value
        End Set
    End Property

    Public Property MovieFanartExpertSingle() As String
        Get
            Return Me._moviefanartexpertsingle
        End Get
        Set(ByVal value As String)
            Me._moviefanartexpertsingle = value
        End Set
    End Property

    Public Property MovieLandscapeExpertSingle() As String
        Get
            Return Me._movielandscapeexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movielandscapeexpertsingle = value
        End Set
    End Property

    Public Property MovieNFOExpertSingle() As String
        Get
            Return Me._movienfoexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movienfoexpertsingle = value
        End Set
    End Property

    Public Property MoviePosterExpertSingle() As String
        Get
            Return Me._movieposterexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movieposterexpertsingle = value
        End Set
    End Property

    Public Property MovieStackExpertSingle() As Boolean
        Get
            Return Me._moviestackexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Me._moviestackexpertsingle = value
        End Set
    End Property

    Public Property MovieTrailerExpertSingle() As String
        Get
            Return Me._movietrailerexpertsingle
        End Get
        Set(ByVal value As String)
            Me._movietrailerexpertsingle = value
        End Set
    End Property

    Public Property MovieUnstackExpertSingle() As Boolean
        Get
            Return Me._movieunstackexpertsingle
        End Get
        Set(ByVal value As Boolean)
            Me._movieunstackexpertsingle = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertMulti() As Boolean
        Get
            Return Me._movieactorthumbsexpertmulti
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsexpertmulti = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertMulti() As String
        Get
            Return Me._movieactorthumbsextexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movieactorthumbsextexpertmulti = value
        End Set
    End Property

    Public Property MovieBannerExpertMulti() As String
        Get
            Return Me._moviebannerexpertmulti
        End Get
        Set(ByVal value As String)
            Me._moviebannerexpertmulti = value
        End Set
    End Property

    Public Property MovieClearArtExpertMulti() As String
        Get
            Return Me._movieclearartexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movieclearartexpertmulti = value
        End Set
    End Property

    Public Property MovieClearLogoExpertMulti() As String
        Get
            Return Me._movieclearlogoexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movieclearlogoexpertmulti = value
        End Set
    End Property

    Public Property MovieDiscArtExpertMulti() As String
        Get
            Return Me._moviediscartexpertmulti
        End Get
        Set(ByVal value As String)
            Me._moviediscartexpertmulti = value
        End Set
    End Property

    Public Property MovieFanartExpertMulti() As String
        Get
            Return Me._moviefanartexpertmulti
        End Get
        Set(ByVal value As String)
            Me._moviefanartexpertmulti = value
        End Set
    End Property

    Public Property MovieLandscapeExpertMulti() As String
        Get
            Return Me._movielandscapeexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movielandscapeexpertmulti = value
        End Set
    End Property

    Public Property MovieNFOExpertMulti() As String
        Get
            Return Me._movienfoexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movienfoexpertmulti = value
        End Set
    End Property

    Public Property MoviePosterExpertMulti() As String
        Get
            Return Me._movieposterexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movieposterexpertmulti = value
        End Set
    End Property

    Public Property MovieStackExpertMulti() As Boolean
        Get
            Return Me._moviestackexpertmulti
        End Get
        Set(ByVal value As Boolean)
            Me._moviestackexpertmulti = value
        End Set
    End Property

    Public Property MovieTrailerExpertMulti() As String
        Get
            Return Me._movietrailerexpertmulti
        End Get
        Set(ByVal value As String)
            Me._movietrailerexpertmulti = value
        End Set
    End Property

    Public Property MovieUnstackExpertMulti() As Boolean
        Get
            Return Me._movieunstackexpertmulti
        End Get
        Set(ByVal value As Boolean)
            Me._movieunstackexpertmulti = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertVTS() As Boolean
        Get
            Return Me._movieactorthumbsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsexpertvts = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertVTS() As String
        Get
            Return Me._movieactorthumbsextexpertvts
        End Get
        Set(ByVal value As String)
            Me._movieactorthumbsextexpertvts = value
        End Set
    End Property

    Public Property MovieBannerExpertVTS() As String
        Get
            Return Me._moviebannerexpertvts
        End Get
        Set(ByVal value As String)
            Me._moviebannerexpertvts = value
        End Set
    End Property

    Public Property MovieClearArtExpertVTS() As String
        Get
            Return Me._movieclearartexpertvts
        End Get
        Set(ByVal value As String)
            Me._movieclearartexpertvts = value
        End Set
    End Property

    Public Property MovieClearLogoExpertVTS() As String
        Get
            Return Me._movieclearlogoexpertvts
        End Get
        Set(ByVal value As String)
            Me._movieclearlogoexpertvts = value
        End Set
    End Property

    Public Property MovieDiscArtExpertVTS() As String
        Get
            Return Me._moviediscartexpertvts
        End Get
        Set(ByVal value As String)
            Me._moviediscartexpertvts = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertVTS() As Boolean
        Get
            Return Me._movieextrafanartsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartsexpertvts = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertVTS() As Boolean
        Get
            Return Me._movieextrathumbsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbsexpertvts = value
        End Set
    End Property

    Public Property MovieFanartExpertVTS() As String
        Get
            Return Me._moviefanartexpertvts
        End Get
        Set(ByVal value As String)
            Me._moviefanartexpertvts = value
        End Set
    End Property

    Public Property MovieLandscapeExpertVTS() As String
        Get
            Return Me._movielandscapeexpertvts
        End Get
        Set(ByVal value As String)
            Me._movielandscapeexpertvts = value
        End Set
    End Property

    Public Property MovieNFOExpertVTS() As String
        Get
            Return Me._movienfoexpertvts
        End Get
        Set(ByVal value As String)
            Me._movienfoexpertvts = value
        End Set
    End Property

    Public Property MoviePosterExpertVTS() As String
        Get
            Return Me._movieposterexpertvts
        End Get
        Set(ByVal value As String)
            Me._movieposterexpertvts = value
        End Set
    End Property

    Public Property MovieRecognizeVTSExpertVTS() As Boolean
        Get
            Return Me._movierecognizevtsexpertvts
        End Get
        Set(ByVal value As Boolean)
            Me._movierecognizevtsexpertvts = value
        End Set
    End Property

    Public Property MovieTrailerExpertVTS() As String
        Get
            Return Me._movietrailerexpertvts
        End Get
        Set(ByVal value As String)
            Me._movietrailerexpertvts = value
        End Set
    End Property

    Public Property MovieUseBaseDirectoryExpertVTS() As Boolean
        Get
            Return Me._movieusebasedirectoryexpertvts
        End Get
        Set(ByVal value As Boolean)
            Me._movieusebasedirectoryexpertvts = value
        End Set
    End Property

    Public Property MovieActorThumbsExpertBDMV() As Boolean
        Get
            Return Me._movieactorthumbsexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsexpertbdmv = value
        End Set
    End Property

    Public Property MovieActorThumbsExtExpertBDMV() As String
        Get
            Return Me._movieactorthumbsextexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movieactorthumbsextexpertbdmv = value
        End Set
    End Property

    Public Property MovieBannerExpertBDMV() As String
        Get
            Return Me._moviebannerexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._moviebannerexpertbdmv = value
        End Set
    End Property

    Public Property MovieClearArtExpertBDMV() As String
        Get
            Return Me._movieclearartexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movieclearartexpertbdmv = value
        End Set
    End Property

    Public Property MovieClearLogoExpertBDMV() As String
        Get
            Return Me._movieclearlogoexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movieclearlogoexpertbdmv = value
        End Set
    End Property

    Public Property MovieDiscArtExpertBDMV() As String
        Get
            Return Me._moviediscartexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._moviediscartexpertbdmv = value
        End Set
    End Property

    Public Property MovieExtrafanartsExpertBDMV() As Boolean
        Get
            Return Me._movieextrafanartsexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartsexpertbdmv = value
        End Set
    End Property

    Public Property MovieExtrathumbsExpertBDMV() As Boolean
        Get
            Return Me._movieextrathumbsexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbsexpertbdmv = value
        End Set
    End Property

    Public Property MovieFanartExpertBDMV() As String
        Get
            Return Me._moviefanartexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._moviefanartexpertbdmv = value
        End Set
    End Property

    Public Property MovieLandscapeExpertBDMV() As String
        Get
            Return Me._movielandscapeexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movielandscapeexpertbdmv = value
        End Set
    End Property

    Public Property MovieNFOExpertBDMV() As String
        Get
            Return Me._movienfoexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movienfoexpertbdmv = value
        End Set
    End Property

    Public Property MoviePosterExpertBDMV() As String
        Get
            Return Me._movieposterexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movieposterexpertbdmv = value
        End Set
    End Property

    Public Property MovieTrailerExpertBDMV() As String
        Get
            Return Me._movietrailerexpertbdmv
        End Get
        Set(ByVal value As String)
            Me._movietrailerexpertbdmv = value
        End Set
    End Property

    Public Property MovieUseBaseDirectoryExpertBDMV() As Boolean
        Get
            Return Me._movieusebasedirectoryexpertbdmv
        End Get
        Set(ByVal value As Boolean)
            Me._movieusebasedirectoryexpertbdmv = value
        End Set
    End Property

    Public Property TVUseBoxee() As Boolean
        Get
            Return Me._tvuseboxee
        End Get
        Set(ByVal value As Boolean)
            Me._tvuseboxee = value
        End Set
    End Property

    Public Property TVUseEden() As Boolean
        Get
            Return Me._tvuseeden
        End Get
        Set(ByVal value As Boolean)
            Me._tvuseeden = value
        End Set
    End Property

    Public Property TVUseFrodo() As Boolean
        Get
            Return Me._tvusefrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvusefrodo = value
        End Set
    End Property

    Public Property TVUseYAMJ() As Boolean
        Get
            Return Me._tvuseyamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvuseyamj = value
        End Set
    End Property

    Public Property TVShowBannerBoxee() As Boolean
        Get
            Return Me._tvshowbannerboxee
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowbannerboxee = value
        End Set
    End Property

    Public Property TVShowBannerFrodo() As Boolean
        Get
            Return Me._tvshowbannerfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowbannerfrodo = value
        End Set
    End Property

    Public Property TVShowBannerYAMJ() As Boolean
        Get
            Return Me._tvshowbanneryamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowbanneryamj = value
        End Set
    End Property

    Public Property TVShowExtrafanartsXBMC() As Boolean
        Get
            Return Me._tvshowextrafanartsxbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowextrafanartsxbmc = value
        End Set
    End Property

    Public Property TVShowFanartBoxee() As Boolean
        Get
            Return Me._tvshowfanartboxee
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfanartboxee = value
        End Set
    End Property

    Public Property TVShowFanartFrodo() As Boolean
        Get
            Return Me._tvshowfanartfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfanartfrodo = value
        End Set
    End Property

    Public Property TVShowFanartYAMJ() As Boolean
        Get
            Return Me._tvshowfanartyamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowfanartyamj = value
        End Set
    End Property

    Public Property TVShowPosterBoxee() As Boolean
        Get
            Return Me._tvshowposterboxee
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowposterboxee = value
        End Set
    End Property

    Public Property TVShowPosterFrodo() As Boolean
        Get
            Return Me._tvshowposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowposterfrodo = value
        End Set
    End Property

    Public Property TVShowPosterYAMJ() As Boolean
        Get
            Return Me._tvshowposteryamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowposteryamj = value
        End Set
    End Property

    Public Property TVShowActorThumbsFrodo() As Boolean
        Get
            Return Me._tvshowactorthumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowactorthumbsfrodo = value
        End Set
    End Property

    Public Property TVSeasonPosterBoxee() As Boolean
        Get
            Return Me._tvseasonposterboxee
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonposterboxee = value
        End Set
    End Property

    Public Property TVSeasonPosterFrodo() As Boolean
        Get
            Return Me._tvseasonposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonposterfrodo = value
        End Set
    End Property

    Public Property TVSeasonPosterYAMJ() As Boolean
        Get
            Return Me._tvseasonposteryamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonposteryamj = value
        End Set
    End Property

    Public Property TVSeasonFanartFrodo() As Boolean
        Get
            Return Me._tvseasonfanartfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonfanartfrodo = value
        End Set
    End Property

    Public Property TVSeasonFanartYAMJ() As Boolean
        Get
            Return Me._tvseasonfanartyamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonfanartyamj = value
        End Set
    End Property

    Public Property TVSeasonBannerFrodo() As Boolean
        Get
            Return Me._tvseasonbannerfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonbannerfrodo = value
        End Set
    End Property

    Public Property TVSeasonBannerYAMJ() As Boolean
        Get
            Return Me._tvseasonbanneryamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonbanneryamj = value
        End Set
    End Property

    Public Property TVEpisodePosterBoxee() As Boolean
        Get
            Return Me._tvepisodeposterboxee
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodeposterboxee = value
        End Set
    End Property

    Public Property TVEpisodePosterFrodo() As Boolean
        Get
            Return Me._tvepisodeposterfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodeposterfrodo = value
        End Set
    End Property

    Public Property TVEpisodePosterYAMJ() As Boolean
        Get
            Return Me._tvepisodeposteryamj
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodeposteryamj = value
        End Set
    End Property

    Public Property TVEpisodeActorThumbsFrodo() As Boolean
        Get
            Return Me._tvepisodeactorthumbsfrodo
        End Get
        Set(ByVal value As Boolean)
            Me._tvepisodeactorthumbsfrodo = value
        End Set
    End Property

    Public Property TVShowClearLogoXBMC() As Boolean
        Get
            Return Me._tvshowclearlogoxbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowclearlogoxbmc = value
        End Set
    End Property

    Public Property TVShowClearArtXBMC() As Boolean
        Get
            Return Me._tvshowclearartxbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowclearartxbmc = value
        End Set
    End Property

    Public Property TVShowCharacterArtXBMC() As Boolean
        Get
            Return Me._tvshowcharacterartxbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowcharacterartxbmc = value
        End Set
    End Property

    Public Property TVShowTVThemeXBMC() As Boolean
        Get
            Return Me._tvshowtvthemexbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowtvthemexbmc = value
        End Set
    End Property

    Public Property TVShowTVThemeFolderXBMC() As String
        Get
            Return Me._tvshowtvthemefolderxbmc
        End Get
        Set(ByVal value As String)
            Me._tvshowtvthemefolderxbmc = value
        End Set
    End Property

    Public Property TVShowLandscapeXBMC() As Boolean
        Get
            Return Me._tvshowlandscapexbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowlandscapexbmc = value
        End Set
    End Property

    Public Property TVSeasonLandscapeXBMC() As Boolean
        Get
            Return Me._tvseasonlandscapexbmc
        End Get
        Set(ByVal value As Boolean)
            Me._tvseasonlandscapexbmc = value
        End Set
    End Property

    Public Property MovieScraperTitleFallback() As Boolean
        Get
            Return Me._moviescrapertitlefallback
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapertitlefallback = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Defines all default settings for a new installation
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        Dim cLang As Containers.TVLanguage
        Dim xmlTVDB As XDocument

        Me._cleandotfanartjpg = False
        Me._cleanextrathumbs = False
        Me._cleanfanartjpg = False
        Me._cleanfolderjpg = False
        Me._cleanmoviejpg = False
        Me._cleanmovienfo = False
        Me._cleanmovienfob = False
        Me._cleanmovietbn = False
        Me._cleanmovietbnb = False
        Me._cleanmoviefanartjpg = False
        Me._cleanmovienamejpg = False
        Me._cleanposterjpg = False
        Me._cleanpostertbn = False
        Me._embermodules = New List(Of ModulesManager._XMLEmberModuleClass)
        Me._filesystemcleanerwhitelist = False
        Me._filesystemcleanerwhitelistexts = New List(Of String)
        Me._filesystemexpertcleaner = False
        Me._filesystemnostackexts = New List(Of String)
        Me._filesystemvalidexts = New List(Of String)
        Me._filesystemvalidthemeexts = New List(Of String)
        Me._generalcheckupdates = True
        Me._generalcreationdate = False
        Me._generaldaemondrive = String.Empty
        Me._generaldaemonpath = String.Empty
        Me._generalfilterpanelstate = False
        Me._generalhidebanner = False
        Me._generalhidecharacterart = False
        Me._generalhideclearart = False
        Me._generalhideclearlogo = False
        Me._generalhidediscart = False
        Me._generalhidefanart = False
        Me._generalhidefanartsmall = False
        Me._generalhidelandscape = False
        Me._generalhideposter = False
        Me._generalimagesglassoverlay = False
        Me._generalinfopanelanim = False
        Me._generallanguage = "English_(en_US)"
        Me._generalmainsplitterpanelstate = 364
        Me._generalmovieinfopanelstate = 0
        Me._generalmovietheme = "Default"
        Me._generalmoviesettheme = "Default"
        Me._generaloverwritenfo = False
        Me._generalseasonsplitterpanelstate = 200
        Me._generalshowgenrestext = True
        Me._generalshowimgdims = True
        Me._generalshowsplitterpanelstate = 200
        Me._generalsourcefromfolder = True
        Me._generaltvepisodetheme = "Default"
        Me._generaltvshowinfopanelstate = 0
        Me._generaltvshowtheme = "Default"
        Me._generalwindowloc = New Point(If(Screen.PrimaryScreen.WorkingArea.Width <= 1024, 0, Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Width - 1024) / 2)), If(Screen.PrimaryScreen.WorkingArea.Height <= 768, 0, Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Height - 768) / 2)))
        Me._generalwindowsize = New Size(1024, 768)
        Me._generalwindowstate = FormWindowState.Maximized
        Me._genrefilter = "English"
        Me._movieactorthumbseden = False
        Me._movieactorthumbsexpertbdmv = False
        Me._movieactorthumbsexpertmulti = False
        Me._movieactorthumbsexpertsingle = False
        Me._movieactorthumbsexpertvts = False
        Me._movieactorthumbsextexpertbdmv = String.Empty
        Me._movieactorthumbsextexpertmulti = String.Empty
        Me._movieactorthumbsextexpertsingle = String.Empty
        Me._movieactorthumbsextexpertvts = String.Empty
        Me._movieactorthumbsfrodo = False
        Me._movieactorthumbsnmj = False
        Me._movieactorthumbsoverwrite = True
        Me._movieactorthumbsqual = 0
        Me._movieactorthumbsyamj = False
        Me._moviebackdropsauto = False
        Me._moviebackdropspath = String.Empty
        Me._moviebannercol = False
        Me._moviebannereden = False
        Me._moviebannerexpertbdmv = String.Empty
        Me._moviebannerexpertmulti = String.Empty
        Me._moviebannerexpertsingle = String.Empty
        Me._moviebannerexpertvts = String.Empty
        Me._moviebannerfrodo = False
        Me._moviebannerheight = 0
        Me._moviebannernmj = False
        Me._moviebanneroverwrite = True
        Me._moviebannerprefonly = False
        Me._moviebannerpreftype = Enums.MovieBannerType.Graphical
        Me._moviebannerqual = 0
        Me._moviebannerresize = False
        Me._moviebannerwidth = 0
        Me._moviebanneryamj = False
        Me._moviecleandb = True
        Me._movieclearartcol = False
        Me._moviecleararteden = False
        Me._movieclearartexpertbdmv = String.Empty
        Me._movieclearartexpertmulti = String.Empty
        Me._movieclearartexpertsingle = String.Empty
        Me._movieclearartexpertvts = String.Empty
        Me._movieclearartfrodo = False
        Me._movieclearartnmj = False
        Me._movieclearartoverwrite = True
        Me._movieclearartyamj = False
        Me._movieclearlogocol = False
        Me._movieclearlogoeden = False
        Me._movieclearlogoexpertbdmv = String.Empty
        Me._movieclearlogoexpertmulti = String.Empty
        Me._movieclearlogoexpertsingle = String.Empty
        Me._movieclearlogoexpertvts = String.Empty
        Me._movieclearlogofrodo = False
        Me._movieclearlogonmj = False
        Me._movieclearlogooverwrite = True
        Me._movieclearlogoyamj = False
        Me._movieclickscrape = False
        Me._movieclickscrapeask = False
        Me._moviediscartcol = False
        Me._moviediscarteden = False
        Me._moviediscartexpertbdmv = String.Empty
        Me._moviediscartexpertmulti = String.Empty
        Me._moviediscartexpertsingle = String.Empty
        Me._moviediscartexpertvts = String.Empty
        Me._moviediscartfrodo = False
        Me._moviediscartnmj = False
        Me._moviediscartoverwrite = True
        Me._moviediscartyamj = False
        Me._moviedisplayyear = False
        Me._movieefanartscol = False
        Me._movieefanartsheight = 0
        Me._movieefanartslimit = 4
        Me._movieefanartsoverwrite = True
        Me._movieefanartsprefonly = False
        Me._movieefanartsprefsize = Enums.FanartSize.Xlrg
        Me._movieefanartsqual = 0
        Me._movieefanartsresize = False
        Me._movieefanartswidth = 0
        Me._movieethumbscol = False
        Me._movieethumbsheight = 0
        Me._movieethumbslimit = 4
        Me._movieethumbsoverwrite = True
        Me._movieethumbsprefonly = False
        Me._movieethumbsprefsize = Enums.FanartSize.Xlrg
        Me._movieethumbsqual = 0
        Me._movieethumbsresize = False
        Me._movieethumbswidth = 0
        Me._movieextrafanartseden = False
        Me._movieextrafanartsexpertbdmv = False
        Me._movieextrafanartsexpertsingle = False
        Me._movieextrafanartsexpertvts = False
        Me._movieextrafanartsfrodo = False
        Me._movieextrafanartsnmj = False
        Me._movieextrafanartsyamj = False
        Me._movieextrathumbseden = False
        Me._movieextrathumbsexpertbdmv = False
        Me._movieextrathumbsexpertsingle = False
        Me._movieextrathumbsexpertvts = False
        Me._movieextrathumbsfrodo = False
        Me._movieextrathumbsnmj = False
        Me._movieextrathumbsyamj = False
        Me._moviefanartboxee = False
        Me._moviefanartcol = False
        Me._moviefanarteden = False
        Me._moviefanartexpertbdmv = String.Empty
        Me._moviefanartexpertmulti = String.Empty
        Me._moviefanartexpertsingle = String.Empty
        Me._moviefanartexpertvts = String.Empty
        Me._moviefanartfrodo = False
        Me._moviefanartheight = 0
        Me._moviefanartnmj = False
        Me._moviefanartoverwrite = True
        Me._moviefanartprefonly = False
        Me._moviefanartprefsize = Enums.FanartSize.Xlrg
        Me._moviefanartqual = 0
        Me._moviefanartresize = False
        Me._moviefanartwidth = 0
        Me._moviefanartyamj = False
        Me._moviefiltercustom = New List(Of String)
        Me._moviefiltercustomisempty = False
        Me._moviegeneralflaglang = String.Empty
        Me._moviegeneralignorelastscan = True
        Me._moviegeneralmarknew = False
        Me._movieimdburl = "akas.imdb.com"
        Me._movielandscapecol = False
        Me._movielandscapeeden = False
        Me._movielandscapeexpertbdmv = String.Empty
        Me._movielandscapeexpertmulti = String.Empty
        Me._movielandscapeexpertsingle = String.Empty
        Me._movielandscapeexpertvts = String.Empty
        Me._movielandscapefrodo = False
        Me._movielandscapenmj = False
        Me._movielandscapeoverwrite = True
        Me._movielandscapeyamj = False
        Me._movielevtolerance = 0
        Me._movielockgenre = False
        Me._movielocklanguagea = False
        Me._movielocklanguagev = False
        Me._movielockmpaa = False
        Me._movielockoutline = False
        Me._movielockplot = False
        Me._movielockrating = False
        Me._movielockstudio = False
        Me._movielocktagline = False
        Me._movielocktitle = False
        Me._movielocktrailer = False
        Me._moviemetadataperfiletype = New List(Of MetadataPerType)
        Me._moviemissingbanner = True
        Me._moviemissingclearart = True
        Me._moviemissingclearlogo = True
        Me._moviemissingdiscart = True
        Me._moviemissingefanarts = True
        Me._moviemissingethumbs = True
        Me._moviemissingfanart = True
        Me._moviemissinglandscape = True
        Me._moviemissingnfo = True
        Me._moviemissingposter = True
        Me._moviemissingsubs = False
        Me._moviemissingtheme = True
        Me._moviemissingtrailer = True
        Me._moviemoviesetspath = String.Empty
        Me._movienfoboxee = False
        Me._movienfocol = False
        Me._movienfoeden = False
        Me._movienfoexpertbdmv = String.Empty
        Me._movienfoexpertmulti = String.Empty
        Me._movienfoexpertsingle = String.Empty
        Me._movienfoexpertvts = String.Empty
        Me._movienfofrodo = False
        Me._movienfonmj = False
        Me._movienfoyamj = False
        Me._movienosaveimagestonfo = False
        Me._movieposterboxee = False
        Me._moviepostercol = False
        Me._moviepostereden = False
        Me._movieposterexpertbdmv = String.Empty
        Me._movieposterexpertmulti = String.Empty
        Me._movieposterexpertsingle = String.Empty
        Me._movieposterexpertvts = String.Empty
        Me._movieposterfrodo = False
        Me._movieposterheight = 0
        Me._movieposternmj = False
        Me._movieposteroverwrite = True
        Me._movieposterprefonly = False
        Me._movieposterprefsize = Enums.PosterSize.Xlrg
        Me._movieposterqual = 0
        Me._movieposterresize = False
        Me._movieposterwidth = 0
        Me._movieposteryamj = False
        Me._moviepropercase = True
        Me._movierecognizevtsexpertvts = False
        Me._moviescanordermodify = False
        Me._moviescrapercast = True
        Me._moviescrapercastlimit = 0
        Me._moviescrapercastwithimgonly = False
        Me._moviescrapercertformpaa = False
        Me._moviescrapercertification = True
        Me._moviescrapercertlang = String.Empty
        Me._moviescrapercollection = True
        Me._moviescrapercountry = True
        Me._moviescrapercrew = True
        Me._moviescraperdirector = True
        Me._moviescraperdurationruntimeformat = "<m>"
        Me._moviescraperforcetitle = String.Empty
        Me._moviescraperfullcast = True
        Me._moviescraperfullcrew = False
        Me._moviescrapergenre = True
        Me._moviescrapergenrelimit = 0
        Me._moviescrapermetadataifoscan = True
        Me._moviescrapermetadatascan = True
        Me._moviescrapermpaa = True
        Me._moviescrapermusicby = True
        Me._moviescraperonlyvalueformpaa = False
        Me._moviescraperoutline = True
        Me._moviescraperoutlineforplot = False
        Me._moviescraperoutlinelimit = 350
        Me._moviescraperoutlineplotenglishoverwrite = False 'TODO: check
        Me._moviescraperplot = True
        Me._moviescraperplotforoutline = True
        Me._moviescraperproducers = True
        Me._moviescraperrating = True
        Me._moviescraperrelease = True
        Me._moviescraperruntime = True
        Me._moviescraperstudio = True
        Me._moviescrapertagline = True
        Me._moviescrapertitle = True
        Me._moviescrapertitlefallback = True 'TODO: check
        Me._moviescrapertop250 = True
        Me._moviescrapertrailer = True
        Me._moviescraperusemdduration = True
        Me._moviescraperusempaafsk = False 'TODO: check
        Me._moviescrapervotes = True
        Me._moviescraperwriters = True
        Me._moviescraperyear = True
        Me._moviesetbannercol = False
        Me._moviesetbanneroverwrite = True
        Me._moviesetclearartcol = False
        Me._moviesetclearartoverwrite = True
        Me._moviesetclearlogocol = False
        Me._moviesetclearlogooverwrite = True
        Me._moviesetdiscartcol = False
        Me._moviesetdiscartoverwrite = True
        Me._moviesetfanartcol = False
        Me._moviesetfanartoverwrite = True
        Me._moviesetlandscapecol = False
        Me._moviesetlandscapeoverwrite = True
        Me._moviesetnfocol = False
        Me._moviesetpostercol = False
        Me._moviesetposteroverwrite = True
        Me._moviesets = New List(Of String)
        Me._movieskiplessthan = 0
        Me._movieskipstackedsizecheck = False
        Me._moviesortbeforescan = False
        Me._moviesorttokens = New List(Of String)
        Me._moviesorttokensisempty = False
        Me._moviestackexpertmulti = False
        Me._moviestackexpertsingle = False
        Me._moviesubcol = False
        Me._moviethemecol = False
        Me._moviethemeenable = True
        Me._moviethemeoverwrite = True
        Me._movietrailercol = False
        Me._movietrailerdeleteexisting = True
        Me._movietrailereden = False
        Me._movietrailerenable = True
        Me._movietrailerexpertbdmv = String.Empty
        Me._movietrailerexpertmulti = String.Empty
        Me._movietrailerexpertsingle = String.Empty
        Me._movietrailerexpertvts = String.Empty
        Me._movietrailerfrodo = False
        Me._movietrailerminqual = Enums.TrailerQuality.All
        Me._movietrailernmj = False
        Me._movietraileroverwrite = True
        Me._movietrailerprefqual = Enums.TrailerQuality.HD720p
        Me._movietraileryamj = False
        Me._movieunstackexpertmulti = False
        Me._movieunstackexpertsingle = False
        Me._movieusebasedirectoryexpertbdmv = False
        Me._movieusebasedirectoryexpertvts = False
        Me._movieuseboxee = False
        Me._movieuseeden = False
        Me._movieuseexpert = False
        Me._movieusefrodo = False
        Me._movieusenmj = False
        Me._movieuseyamj = False
        Me._moviewatchedcol = False
        Me._moviexbmcthemecustom = False
        Me._moviexbmcthemecustompath = String.Empty
        Me._moviexbmcthemeenable = False
        Me._moviexbmcthememovie = False
        Me._moviexbmcthemesub = False
        Me._moviexbmcthemesubdir = "Themes"
        Me._moviexbmcprotectvtsbdmv = False
        Me._moviexbmctrailerformat = False
        Me._movieyamjcompatiblesets = False
        Me._movieyamjwatchedfile = False
        Me._movieyamjwatchedfolder = String.Empty
        Me._ommdummyformat = 0 'TODO: check
        Me._ommdummytagline = String.Empty 'TODO: check
        Me._ommdummytop = "0" 'TODO: check
        Me._ommdummyusebackground = True 'TODO: check
        Me._ommdummyusefanart = True 'TODO: check
        Me._ommdummyuseoverlay = True 'TODO: check
        Me._ommmediastubtagline = String.Empty 'TODO: check
        Me._password = String.Empty
        Me._proxycredentials = New NetworkCredential
        Me._proxyport = -1
        Me._proxyuri = String.Empty
        Me._sortpath = String.Empty
        Me._traktpassword = String.Empty
        Me._traktusername = String.Empty
        Me._tvasbannerheight = 0
        Me._tvasbanneroverwrite = True
        Me._tvasbannerpreftype = Enums.TVShowBannerType.Graphical
        Me._tvasbannerqual = 0
        Me._tvasbannerresize = False
        Me._tvasbannerwidth = 0
        Me._tvasfanartheight = 0
        Me._tvasfanartoverwrite = True
        Me._tvasfanartprefsize = Enums.TVFanartSize.HD1080
        Me._tvasfanartqual = 0
        Me._tvasfanartresize = False
        Me._tvasfanartwidth = 0
        Me._tvaslandscapeoverwrite = True
        Me._tvasposterheight = 0
        Me._tvasposteroverwrite = True
        Me._tvasposterprefsize = Enums.TVPosterSize.HD1000
        Me._tvasposterqual = 0
        Me._tvasposterresize = False
        Me._tvasposterwidth = 0
        Me._tvcleandb = True
        Me._tvdisplaymissingepisodes = True
        Me._tvepisodeactorthumbsfrodo = False
        Me._tvepisodefanartcol = False
        Me._tvepisodefanartheight = 0
        Me._tvepisodefanartoverwrite = True
        Me._tvepisodefanartprefsize = Enums.TVFanartSize.HD1080
        Me._tvepisodefanartqual = 0
        Me._tvepisodefanartresize = False
        Me._tvepisodefanartwidth = 0
        Me._tvepisodefiltercustom = New List(Of String)
        Me._tvepisodefiltercustomisempty = False
        Me._tvepisodenfocol = False
        Me._tvepisodenofilter = True
        Me._tvepisodepostercol = False
        Me._tvepisodeposterfrodo = False
        Me._tvepisodeposterheight = 0
        Me._tvepisodeposteroverwrite = True
        Me._tvepisodeposterqual = 0
        Me._tvepisodeposterresize = False
        Me._tvepisodeposterwidth = 0
        Me._tvepisodepropercase = True
        Me._tvepisodewatchedcol = False
        Me._tvgeneraldisplayasposter = True
        Me._tvgeneralflaglang = String.Empty
        Me._tvgeneralignorelastscan = True
        Me._tvgenerallanguage = "en"
        Me._tvgenerallanguages = New List(Of Containers.TVLanguage)
        Me._tvgeneralmarknewepisodes = False
        Me._tvgeneralmarknewshows = False
        Me._tvlockepisodeplot = False
        Me._tvlockepisoderating = False
        Me._tvlockepisodetitle = False
        Me._tvlockshowgenre = False
        Me._tvlockshowplot = False
        Me._tvlockshowrating = False
        Me._tvlockshowstatus = False
        Me._tvlockshowstudio = False
        Me._tvlockshowtitle = False
        Me._tvmetadataperfiletype = New List(Of MetadataPerType)
        Me._tvscanordermodify = False
        Me._tvscraperdurationruntimeformat = "<m>"
        Me._tvscraperepisodeactors = True
        Me._tvscraperepisodeaired = True
        Me._tvscraperepisodecredits = True
        Me._tvscraperepisodedirector = True
        Me._tvscraperepisodeepisode = True
        Me._tvscraperepisodeplot = True
        Me._tvscraperepisoderating = True
        Me._tvscraperepisodeseason = True
        Me._tvscraperepisodetitle = True
        Me._tvscrapermetadatascan = True
        Me._tvscraperoptionsordering = Enums.Ordering.Standard
        Me._tvscraperratingregion = "usa"
        Me._tvscrapershowactors = True
        Me._tvscrapershowepiguideurl = True
        Me._tvscrapershowgenre = True
        Me._tvscrapershowmpaa = True
        Me._tvscrapershowplot = True
        Me._tvscrapershowpremiered = True
        Me._tvscrapershowrating = True
        Me._tvscrapershowstatus = True
        Me._tvscrapershowstudio = True
        Me._tvscrapershowtitle = True
        Me._tvscraperupdatetime = Enums.TVScraperUpdateTime.Always
        Me._tvscraperusemdduration = True
        Me._tvseasonbannercol = False
        Me._tvseasonbannerfrodo = False
        Me._tvseasonbannerheight = 0
        Me._tvseasonbanneroverwrite = True
        Me._tvseasonbannerpreftype = Enums.TVSeasonBannerType.Graphical
        Me._tvseasonbannerqual = 0
        Me._tvseasonbannerresize = False
        Me._tvseasonbannerwidth = 0
        Me._tvseasonbanneryamj = False
        Me._tvseasonfanartcol = False
        Me._tvseasonfanartfrodo = False
        Me._tvseasonfanartheight = 0
        Me._tvseasonfanartoverwrite = True
        Me._tvseasonfanartprefsize = Enums.TVFanartSize.HD1080
        Me._tvseasonfanartqual = 0
        Me._tvseasonfanartresize = False
        Me._tvseasonfanartwidth = 0
        Me._tvseasonfanartyamj = False
        Me._tvseasonlandscapecol = False
        Me._tvseasonlandscapeoverwrite = True
        Me._tvseasonlandscapexbmc = False
        Me._tvseasonpostercol = False
        Me._tvseasonposterfrodo = False
        Me._tvseasonposterheight = 0
        Me._tvseasonposteroverwrite = True
        Me._tvseasonposterprefsize = Enums.TVPosterSize.HD1000
        Me._tvseasonposterqual = 0
        Me._tvseasonposterresize = False
        Me._tvseasonposterwidth = 0
        Me._tvseasonposteryamj = False
        Me._tvshowactorthumbsfrodo = False
        Me._tvshowbannercol = False
        Me._tvshowbannerfrodo = False
        Me._tvshowbannerheight = 0
        Me._tvshowbanneroverwrite = True
        Me._tvshowbannerpreftype = Enums.TVShowBannerType.Graphical
        Me._tvshowbannerqual = 0
        Me._tvshowbannerresize = False
        Me._tvshowbannerwidth = 0
        Me._tvshowbanneryamj = False
        Me._tvshowcharacterartcol = False
        Me._tvshowcharacterartoverwrite = True
        Me._tvshowcharacterartxbmc = False
        Me._tvshowclearartcol = False
        Me._tvshowclearartoverwrite = True
        Me._tvshowclearartxbmc = False
        Me._tvshowclearlogocol = False
        Me._tvshowclearlogooverwrite = True
        Me._tvshowclearlogoxbmc = False
        Me._tvshowextrafanartsxbmc = False
        Me._tvshowefanartscol = False
        Me._tvshowfanartcol = False
        Me._tvshowfanartfrodo = False
        Me._tvshowfanartheight = 0
        Me._tvshowfanartoverwrite = True
        Me._tvshowfanartprefsize = Enums.TVFanartSize.HD1080
        Me._tvshowfanartqual = 0
        Me._tvshowfanartresize = False
        Me._tvshowfanartwidth = 0
        Me._tvshowfanartyamj = False
        Me._tvshowfiltercustom = New List(Of String)
        Me._tvshowfiltercustomisempty = False
        Me._tvshowlandscapecol = False
        Me._tvshowlandscapeoverwrite = True
        Me._tvshowlandscapexbmc = False
        Me._tvshownfocol = False
        Me._tvshowpostercol = False
        Me._tvshowposterfrodo = False
        Me._tvshowposterheight = 0
        Me._tvshowposteroverwrite = True
        Me._tvshowposterprefsize = Enums.TVPosterSize.HD1000
        Me._tvshowposterqual = 0
        Me._tvshowposterresize = False
        Me._tvshowposterwidth = 0
        Me._tvshowposteryamj = False
        Me._tvshowpropercase = True
        Me._tvshowregexes = New List(Of TVShowRegEx)
        Me._tvshowtvthemefolderxbmc = String.Empty
        Me._tvshowtvthemexbmc = False
        Me._tvskiplessthan = 0
        Me._tvshowthemecol = False
        Me._tvuseboxee = False
        Me._tvusefrodo = False
        Me._tvuseyamj = False
        Me._username = String.Empty
        Me._usetrakt = False
        Me._version = String.Empty

        'TODO: i have tried to remove that no longer needed code, but it ends in a resource error. I don't know why in hell...
        Try
            xmlTVDB = XDocument.Parse(My.Resources.Languages_2)
            Dim xLangs = From xLanguages In xmlTVDB.Descendants("Language")
            For Each xL As XElement In xLangs
                cLang = New Containers.TVLanguage
                cLang.LongLang = xL.Element("name").Value
                cLang.ShortLang = xL.Element("abbreviation").Value
                '_tvscraperlanguages.Add(cLang) 
            Next
            '_tvscraperlanguages.Sort(AddressOf CompareLanguagesLong)
        Catch

        End Try
    End Sub

    Public Sub Load()
        Try
            Dim xmlSerial As New XmlSerializer(GetType(Settings))

            'Cocotus, Load from central "Settings" folder if it exists!
            Dim configpath As String = String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Settings.xml")

            'Settings.xml is still at old place (root) -> move to new place if there's no Settings.xml !
            If File.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Settings.xml")) = False AndAlso File.Exists(Path.Combine(Functions.AppPath, "Settings.xml")) AndAlso Directory.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar)) Then
                File.Move(Path.Combine(Functions.AppPath, "Settings.xml"), String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Settings.xml"))
                'New Settings folder doesn't exist -> do it the old way...
            ElseIf Directory.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar)) = False Then
                configpath = Path.Combine(Functions.AppPath, "Settings.xml")
            End If

            'old
            '  If File.Exists(Path.Combine(Functions.AppPath, "Settings.xml")) Then

            If File.Exists(configpath) Then
                'old
                '  Dim strmReader As New StreamReader(Path.Combine(Functions.AppPath, "Settings.xml"))
                Dim strmReader As New StreamReader(configpath)
                Master.eSettings = DirectCast(xmlSerial.Deserialize(strmReader), Settings)
                strmReader.Close()
            Else
                Master.eSettings = New Settings
            End If
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidExts.Count <= 0) Then
            Master.eSettings.FileSystemValidExts.Clear()
            Master.eSettings.FileSystemValidExts.AddRange(Strings.Split(".avi,.divx,.mkv,.iso,.mpg,.mp4,.mpeg,.wmv,.wma,.mov,.mts,.m2t,.img,.dat,.bin,.cue,.ifo,.vob,.dvb,.evo,.asf,.asx,.avs,.nsv,.ram,.ogg,.ogm,.ogv,.flv,.swf,.nut,.viv,.rar,.m2ts,.dvr-ms,.ts,.m4v,.rmvb,.webm,.disc", ","))
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
        ByVal x As Containers.TVLanguage, ByVal y As Containers.TVLanguage) As Integer

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
                Return x.LongLang.CompareTo(y.LongLang)
                'End If
            End If
        End If

    End Function

    Private Shared Function CompareLanguagesShort( _
        ByVal x As Containers.TVLanguage, ByVal y As Containers.TVLanguage) As Integer

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
                Return x.ShortLang.CompareTo(y.ShortLang)
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