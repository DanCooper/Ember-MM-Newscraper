Imports System.Xml.Serialization
Imports System.Net
Imports System.Windows.Forms
Imports EmberAPI.Settings
Imports System.Drawing

'''<remarks/>
<System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="Settings")> _
Partial Public Class clsXMLSettings
#Region "Fields"
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
    Private _generaldateaddedignorenfo As Boolean
    Private _generaldatetime As Enums.DateTime
    Private _generaldaemondrive As String
    Private _generaldaemonpath As String
    Private _generaldoubleclickscrape As Boolean
    Private _generalfilterpanelstatemovie As Boolean
    Private _generalfilterpanelstatemovieset As Boolean
    Private _generalfilterpanelstateshow As Boolean
    Private _generalmainfiltersortdate As String
    Private _generalmainfiltersorttitle As String
    Private _generalmainfilterIMDBrating As String
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
    Private _generalshowimgnames As Boolean
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
    Private _moviegeneralcustommarker1color As Integer
    Private _moviegeneralcustommarker2color As Integer
    Private _moviegeneralcustommarker3color As Integer
    Private _moviegeneralcustommarker4color As Integer
    Private _moviegeneralcustommarker1name As String
    Private _moviegeneralcustommarker2name As String
    Private _moviegeneralcustommarker3name As String
    Private _moviegeneralcustommarker4name As String
    Private _moviegeneralflaglang As String
    Private _moviegeneralignorelastscan As Boolean
    Private _moviegeneralmarknew As Boolean
    Private _movieimdburl As String
    Private _movielandscapecol As Boolean
    Private _movielandscapeoverwrite As Boolean
    Private _movielevtolerance As Integer
    Private _movielockactors As Boolean
    Private _movielockcollectionid As Boolean
    Private _movielockcollections As Boolean
    Private _movielockcountry As Boolean
    Private _movielockdirector As Boolean
    Private _movielockgenre As Boolean
    Private _movielocklanguagea As Boolean
    Private _movielocklanguagev As Boolean
    Private _movielockmpaa As Boolean
    Private _movielockoriginaltitle As Boolean
    Private _movielockoutline As Boolean
    Private _movielockplot As Boolean
    Private _movielockrating As Boolean
    Private _movielockreleasedate As Boolean
    Private _movielockruntime As Boolean
    Private _movielockstudio As Boolean
    Private _movielocktags As Boolean
    Private _movielocktagline As Boolean
    Private _movielocktitle As Boolean
    Private _movielocktop250 As Boolean
    Private _movielocktrailer As Boolean
    Private _movielockvotes As Boolean
    Private _movielockcredits As Boolean
    Private _movielockyear As Boolean
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
    Private _moviescrapercert As Boolean
    Private _moviescrapercertlang As String
    Private _moviescrapercleanfields As Boolean
    Private _moviescrapercleanplotoutline As Boolean
    Private _moviescrapercollectionid As Boolean
    Private _moviescrapercollectionsauto As Boolean
    Private _moviescrapercountry As Boolean
    Private _moviescraperdirector As Boolean
    Private _moviescraperdurationruntimeformat As String
    Private _moviescraperformatreleasedate As Boolean
    Private _moviescrapergenre As Boolean
    Private _moviescrapergenrelimit As Integer
    Private _moviescrapermetadataifoscan As Boolean
    Private _moviescrapermetadatascan As Boolean
    Private _moviescraperoriginaltitle As Boolean
    Private _moviescrapercertonlyvalue As Boolean
    Private _moviescraperoutline As Boolean
    Private _moviescraperoutlineforplot As Boolean
    Private _moviescraperoutlinelimit As Integer
    Private _moviescraperoutlineplotenglishoverwrite As Boolean
    Private _moviescraperplot As Boolean
    Private _moviescraperplotforoutline As Boolean
    Private _moviescraperrating As Boolean
    Private _moviescraperrelease As Boolean
    Private _moviescraperruntime As Boolean
    Private _moviescraperstudio As Boolean
    Private _moviescrapertagline As Boolean
    Private _moviescrapertitle As Boolean
    Private _moviescrapertop250 As Boolean
    Private _moviescrapertrailer As Boolean
    Private _moviescraperusedetailview As Boolean
    Private _moviescraperusemdduration As Boolean
    Private _moviescrapercertfsk As Boolean
    Private _moviescrapervotes As Boolean
    Private _moviescrapercredits As Boolean
    Private _moviescraperyear As Boolean
    Private _moviesetbannercol As Boolean
    Private _moviesetbannerheight As Integer
    Private _moviesetbanneroverwrite As Boolean
    Private _moviesetbannerprefonly As Boolean
    Private _moviesetbannerpreftype As Enums.MovieBannerType
    Private _moviesetbannerqual As Integer
    Private _moviesetbannerresize As Boolean
    Private _moviesetbannerwidth As Integer
    Private _moviesetclearartcol As Boolean
    Private _moviesetclearartoverwrite As Boolean
    Private _moviesetclearlogocol As Boolean
    Private _moviesetclearlogooverwrite As Boolean
    Private _moviesetclickscrape As Boolean
    Private _moviesetclickscrapeask As Boolean
    Private _moviesetdiscartcol As Boolean
    Private _moviesetdiscartoverwrite As Boolean
    Private _moviesetfanartcol As Boolean
    Private _moviesetfanartheight As Integer
    Private _moviesetfanartoverwrite As Boolean
    Private _moviesetfanartprefonly As Boolean
    Private _moviesetfanartprefsize As Enums.FanartSize
    Private _moviesetfanartqual As Integer
    Private _moviesetfanartresize As Boolean
    Private _moviesetfanartwidth As Integer
    Private _moviesetlandscapecol As Boolean
    Private _moviesetlandscapeoverwrite As Boolean
    Private _moviesetlockplot As Boolean
    Private _moviesetlocktitle As Boolean
    Private _moviesetmissingbanner As Boolean
    Private _moviesetmissingclearart As Boolean
    Private _moviesetmissingclearlogo As Boolean
    Private _moviesetmissingdiscart As Boolean
    Private _moviesetmissingfanart As Boolean
    Private _moviesetmissinglandscape As Boolean
    Private _moviesetmissingnfo As Boolean
    Private _moviesetmissingposter As Boolean
    Private _moviesetnfocol As Boolean
    Private _moviesetpostercol As Boolean
    Private _moviesetposterheight As Integer
    Private _moviesetposteroverwrite As Boolean
    Private _moviesetposterprefonly As Boolean
    Private _moviesetposterprefsize As Enums.PosterSize
    Private _moviesetposterqual As Integer
    Private _moviesetposterresize As Boolean
    Private _moviesetposterwidth As Integer
    Private _moviesets As New List(Of String)
    Private _moviesetscraperplot As Boolean
    Private _moviesetscrapertitle As Boolean
    Private _movieskiplessthan As Integer
    Private _movieskipstackedsizecheck As Boolean
    Private _moviesortbeforescan As Boolean
    Private _moviesorttokens As List(Of String)
    Private _moviesetsorttokens As List(Of String)
    Private _moviesorttokensisempty As Boolean
    Private _moviesetsorttokensisempty As Boolean
    Private _moviesubcol As Boolean
    Private _moviethemecol As Boolean
    Private _moviethemeenable As Boolean
    Private _moviethemeoverwrite As Boolean
    Private _movietrailercol As Boolean
    Private _movietrailerdefaultsearch As String
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
    Private _tvdisplaystatus As Boolean
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
    Private _tvgeneralflaglang As String
    Private _tvgeneralignorelastscan As Boolean
    Private _tvgenerallanguage As String
    Private _tvgenerallanguages As clsXMLTVDBLanguages
    Private _tvgeneralmarknewepisodes As Boolean
    Private _tvgeneralmarknewshows As Boolean
    Private _tvlockepisodeplot As Boolean
    Private _tvlockepisoderating As Boolean
    Private _tvlockepisoderuntime As Boolean
    Private _tvlockepisodetitle As Boolean
    Private _tvlockshowgenre As Boolean
    Private _tvlockshowplot As Boolean
    Private _tvlockshowrating As Boolean
    Private _tvlockshowruntime As Boolean
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
    Private _tvscraperepisoderuntime As Boolean
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
    Private _tvscrapershowruntime As Boolean
    Private _tvscrapershowstatus As Boolean
    Private _tvscrapershowstudio As Boolean
    Private _tvscrapershowtitle As Boolean
    Private _tvscraperupdatetime As Enums.TVScraperUpdateTime
    Private _tvscraperusemdduration As Boolean
    Private _tvscraperusesruntimeforep As Boolean
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
    Private _tvepisodeposterprefsize As Enums.TVEpisodePosterSize
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
    Private _tvshowmissingbanner As Boolean
    Private _tvshowmissingcharacterart As Boolean
    Private _tvshowmissingclearart As Boolean
    Private _tvshowmissingclearlogo As Boolean
    Private _tvshowmissingefanarts As Boolean
    Private _tvshowmissingfanart As Boolean
    Private _tvshowmissinglandscape As Boolean
    Private _tvshowmissingnfo As Boolean
    Private _tvshowmissingposter As Boolean
    Private _tvshowmissingtheme As Boolean
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
    Private _tvsorttokens As List(Of String)
    Private _tvsorttokensisempty As Boolean
    Private _tvshowthemecol As Boolean
    Private _username As String
    Private _usetrakt As Boolean
    Private _version As String
    Private _restartscraper As Boolean


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
    '***************** MovieSet Part *******************
    '***************************************************

    '*************** XBMC MSAA settings ***************
    Private _moviesetusemsaa As Boolean
    Private _moviesetbannermsaa As Boolean
    Private _moviesetclearartmsaa As Boolean
    Private _moviesetclearlogomsaa As Boolean
    Private _moviesetdiscartmsaa As Boolean
    Private _moviesetfanartmsaa As Boolean
    Private _moviesetlandscapemsaa As Boolean
    Private _moviesetnfomsaa As Boolean
    Private _moviesetpostermsaa As Boolean


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
#End Region

#Region "Properties"

    Public Property movieactorthumbsnmj() As Boolean
        Get
            Return Me._movieactorthumbsnmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsnmj = value
        End Set
    End Property

    Public Property restartscraper() As Boolean
        Get
            Return Me._restartscraper
        End Get
        Set(ByVal value As Boolean)
            Me._restartscraper = value
        End Set
    End Property

    Public Property movieactorthumbsyamj() As Boolean
        Get
            Return Me._movieactorthumbsyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieactorthumbsyamj = value
        End Set
    End Property

    Public Property movieclearartnmj() As Boolean
        Get
            Return Me._movieclearartnmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearartnmj = value
        End Set
    End Property

    Public Property movieclearartyamj() As Boolean
        Get
            Return Me._movieclearartyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearartyamj = value
        End Set
    End Property

    Public Property movieclearlogonmj() As Boolean
        Get
            Return Me._movieclearlogonmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearlogonmj = value
        End Set
    End Property

    Public Property movieclearlogoyamj() As Boolean
        Get
            Return Me._movieclearlogoyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieclearlogoyamj = value
        End Set
    End Property

    Public Property moviediscartnmj() As Boolean
        Get
            Return Me._moviediscartnmj
        End Get
        Set(ByVal value As Boolean)
            Me._moviediscartnmj = value
        End Set
    End Property

    Public Property moviediscartyamj() As Boolean
        Get
            Return Me._moviediscartyamj
        End Get
        Set(ByVal value As Boolean)
            Me._moviediscartyamj = value
        End Set
    End Property

    Public Property movieextrafanartsnmj() As Boolean
        Get
            Return Me._movieextrafanartsnmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartsnmj = value
        End Set
    End Property

    Public Property movieextrafanartsyamj() As Boolean
        Get
            Return Me._movieextrafanartsyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrafanartsyamj = value
        End Set
    End Property

    Public Property movieextrathumbsnmj() As Boolean
        Get
            Return Me._movieextrathumbsnmj
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbsnmj = value
        End Set
    End Property

    Public Property movieextrathumbsyamj() As Boolean
        Get
            Return Me._movieextrathumbsyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movieextrathumbsyamj = value
        End Set
    End Property

    Public Property movielandscapenmj() As Boolean
        Get
            Return Me._movielandscapenmj
        End Get
        Set(ByVal value As Boolean)
            Me._movielandscapenmj = value
        End Set
    End Property

    Public Property movielandscapeyamj() As Boolean
        Get
            Return Me._movielandscapeyamj
        End Get
        Set(ByVal value As Boolean)
            Me._movielandscapeyamj = value
        End Set
    End Property

    Public Property proxycredentials() As NetworkCredential
        Get
            Return Me._proxycredentials
        End Get
        Set(ByVal value As NetworkCredential)
            Me._proxycredentials = value
        End Set
    End Property

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

    Public Property TVGeneralLanguages() As clsXMLTVDBLanguages
        Get
            Return Me._tvgenerallanguages
        End Get
        Set(ByVal value As clsXMLTVDBLanguages)
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
            Return Me._cleandotfanartjpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleandotfanartjpg = value
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
            Return Me._cleanfanartjpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanfanartjpg = value
        End Set
    End Property

    Public Property CleanFolderJPG() As Boolean
        Get
            Return Me._cleanfolderjpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanfolderjpg = value
        End Set
    End Property

    Public Property CleanMovieFanartJPG() As Boolean
        Get
            Return Me._cleanmoviefanartjpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmoviefanartjpg = value
        End Set
    End Property

    Public Property CleanMovieJPG() As Boolean
        Get
            Return Me._cleanmoviejpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmoviejpg = value
        End Set
    End Property

    Public Property CleanMovieNameJPG() As Boolean
        Get
            Return Me._cleanmovienamejpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovienamejpg = value
        End Set
    End Property

    Public Property CleanMovieNFO() As Boolean
        Get
            Return Me._cleanmovienfo
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovienfo = value
        End Set
    End Property

    Public Property CleanMovieNFOB() As Boolean
        Get
            Return Me._cleanmovienfob
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovienfob = value
        End Set
    End Property

    Public Property CleanMovieTBN() As Boolean
        Get
            Return _cleanmovietbn
        End Get
        Set(ByVal value As Boolean)
            _cleanmovietbn = value
        End Set
    End Property

    Public Property CleanMovieTBNB() As Boolean
        Get
            Return Me._cleanmovietbnb
        End Get
        Set(ByVal value As Boolean)
            Me._cleanmovietbnb = value
        End Set
    End Property

    Public Property CleanPosterJPG() As Boolean
        Get
            Return Me._cleanposterjpg
        End Get
        Set(ByVal value As Boolean)
            Me._cleanposterjpg = value
        End Set
    End Property

    Public Property CleanPosterTBN() As Boolean
        Get
            Return Me._cleanpostertbn
        End Get
        Set(ByVal value As Boolean)
            Me._cleanpostertbn = value
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

    Public Property TVDisplayMissingEpisodes() As Boolean
        Get
            Return Me._tvdisplaymissingepisodes
        End Get
        Set(ByVal value As Boolean)
            Me._tvdisplaymissingepisodes = value
        End Set
    End Property

    Public Property TVDisplayStatus() As Boolean
        Get
            Return Me._tvdisplaystatus
        End Get
        Set(ByVal value As Boolean)
            Me._tvdisplaystatus = value
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
            Return Me._embermodules
        End Get
        Set(ByVal value As List(Of ModulesManager._XMLEmberModuleClass))
            Me._embermodules = value
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

    Public Property TVLockEpisodeRuntime() As Boolean
        Get
            Return Me._tvlockepisoderuntime
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockepisoderuntime = value
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

    Public Property MovieSetFanartHeight() As Integer
        Get
            Return Me._moviesetfanartheight
        End Get
        Set(ByVal value As Integer)
            Me._moviesetfanartheight = value
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

    Public Property MovieSetFanartQual() As Integer
        Get
            Return Me._moviesetfanartqual
        End Get
        Set(ByVal value As Integer)
            Me._moviesetfanartqual = value
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

    Public Property MovieSetFanartWidth() As Integer
        Get
            Return Me._moviesetfanartwidth
        End Get
        Set(ByVal value As Integer)
            Me._moviesetfanartwidth = value
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

    Public Property MovieScraperCollectionID() As Boolean
        Get
            Return Me._moviescrapercollectionid
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercollectionid = value
        End Set
    End Property

    Public Property MovieScraperCollectionsAuto() As Boolean
        Get
            Return Me._moviescrapercollectionsauto
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercollectionsauto = value
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

    Public Property MovieScraperCert() As Boolean
        Get
            Return Me._moviescrapercert
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercert = value
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

    Public Property MovieScraperFormatReleasedate() As Boolean
        Get
            Return Me._moviescraperformatreleasedate
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperformatreleasedate = value
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

    Public Property MovieScraperOriginalTitle() As Boolean
        Get
            Return Me._moviescraperoriginaltitle
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperoriginaltitle = value
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
    Public Property MovieScraperUseDetailView() As Boolean
        Get
            Return Me._moviescraperusedetailview
        End Get
        Set(ByVal value As Boolean)
            Me._moviescraperusedetailview = value
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

    Public Property MovieScraperCredits() As Boolean
        Get
            Return Me._moviescrapercredits
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercredits = value
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

    Public Property GeneralFilterPanelStateMovie() As Boolean
        Get
            Return Me._generalfilterpanelstatemovie
        End Get
        Set(ByVal value As Boolean)
            Me._generalfilterpanelstatemovie = value
        End Set
    End Property

    Public Property GeneralFilterPanelStateMovieSet() As Boolean
        Get
            Return Me._generalfilterpanelstatemovieset
        End Get
        Set(ByVal value As Boolean)
            Me._generalfilterpanelstatemovieset = value
        End Set
    End Property

    Public Property GeneralFilterPanelStateShow() As Boolean
        Get
            Return Me._generalfilterpanelstateshow
        End Get
        Set(ByVal value As Boolean)
            Me._generalfilterpanelstateshow = value
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

    Public Property MovieScraperCleanFields() As Boolean
        Get
            Return Me._moviescrapercleanfields
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercleanfields = value
        End Set
    End Property

    Public Property MovieScraperCleanPlotOutline() As Boolean
        Get
            Return Me._moviescrapercleanplotoutline
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercleanplotoutline = value
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

    Public Property MovieLockActors() As Boolean
        Get
            Return Me._MovieLockActors
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockActors = value
        End Set
    End Property

    Public Property MovieLockCollectionID() As Boolean
        Get
            Return Me._movielockcollectionid
        End Get
        Set(ByVal value As Boolean)
            Me._movielockcollectionid = value
        End Set
    End Property

    Public Property MovieLockCollections() As Boolean
        Get
            Return Me._movielockcollections
        End Get
        Set(ByVal value As Boolean)
            Me._movielockcollections = value
        End Set
    End Property

    Public Property MovieLockCountry() As Boolean
        Get
            Return Me._MovieLockCountry
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockCountry = value
        End Set
    End Property

    Public Property MovieLockDirector() As Boolean
        Get
            Return Me._MovieLockDirector
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockDirector = value
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

    Public Property MovieLockReleaseDate() As Boolean
        Get
            Return Me._MovieLockReleaseDate
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockReleaseDate = value
        End Set
    End Property

    Public Property MovieLockRuntime() As Boolean
        Get
            Return Me._movielockruntime
        End Get
        Set(ByVal value As Boolean)
            Me._movielockruntime = value
        End Set
    End Property
    Public Property MovieLockTags() As Boolean
        Get
            Return Me._movielocktags
        End Get
        Set(ByVal value As Boolean)
            Me._movielocktags = value
        End Set
    End Property

    Public Property MovieLockTop250() As Boolean
        Get
            Return Me._MovieLockTop250
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockTop250 = value
        End Set
    End Property

    Public Property MovieLockVotes() As Boolean
        Get
            Return Me._MovieLockVotes
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockVotes = value
        End Set
    End Property

    Public Property MovieLockCredits() As Boolean
        Get
            Return Me._movielockcredits
        End Get
        Set(ByVal value As Boolean)
            Me._movielockcredits = value
        End Set
    End Property

    Public Property MovieLockYear() As Boolean
        Get
            Return Me._MovieLockYear
        End Get
        Set(ByVal value As Boolean)
            Me._MovieLockYear = value
        End Set
    End Property

    Public Property MovieScraperCertFSK() As Boolean
        Get
            Return Me._moviescrapercertfsk
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercertfsk = value
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
    Public Property MovieLockOriginalTitle() As Boolean
        Get
            Return Me._movielockoriginaltitle
        End Get
        Set(ByVal value As Boolean)
            Me._movielockoriginaltitle = value
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

    Public Property MovieGeneralCustomMarker1Color() As Integer
        Get
            Return Me._moviegeneralcustommarker1color
        End Get
        Set(ByVal value As Integer)
            Me._moviegeneralcustommarker1color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker2Color() As Integer
        Get
            Return Me._moviegeneralcustommarker2color
        End Get
        Set(ByVal value As Integer)
            Me._moviegeneralcustommarker2color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker3Color() As Integer
        Get
            Return Me._moviegeneralcustommarker3color
        End Get
        Set(ByVal value As Integer)
            Me._moviegeneralcustommarker3color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker4Color() As Integer
        Get
            Return Me._moviegeneralcustommarker4color
        End Get
        Set(ByVal value As Integer)
            Me._moviegeneralcustommarker4color = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker1Name() As String
        Get
            Return Me._moviegeneralcustommarker1name
        End Get
        Set(ByVal value As String)
            Me._moviegeneralcustommarker1name = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker2Name() As String
        Get
            Return Me._moviegeneralcustommarker2name
        End Get
        Set(ByVal value As String)
            Me._moviegeneralcustommarker2name = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker3Name() As String
        Get
            Return Me._moviegeneralcustommarker3name
        End Get
        Set(ByVal value As String)
            Me._moviegeneralcustommarker3name = value
        End Set
    End Property

    Public Property MovieGeneralCustomMarker4Name() As String
        Get
            Return Me._moviegeneralcustommarker4name
        End Get
        Set(ByVal value As String)
            Me._moviegeneralcustommarker4name = value
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

    Public Property MovieSetBannerPrefOnly() As Boolean
        Get
            Return Me._moviesetbannerprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetbannerprefonly = value
        End Set
    End Property

    Public Property MovieSetBannerResize() As Boolean
        Get
            Return Me._moviesetbannerresize
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetbannerresize = value
        End Set
    End Property

    Public Property MovieSetFanartPrefOnly() As Boolean
        Get
            Return Me._moviesetfanartprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetfanartprefonly = value
        End Set
    End Property

    Public Property MovieSetFanartResize() As Boolean
        Get
            Return Me._moviesetfanartresize
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetfanartresize = value
        End Set
    End Property

    Public Property MovieSetPosterPrefOnly() As Boolean
        Get
            Return Me._moviesetPosterprefonly
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetPosterprefonly = value
        End Set
    End Property

    Public Property MovieSetPosterResize() As Boolean
        Get
            Return Me._moviesetPosterresize
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetPosterresize = value
        End Set
    End Property

    Public Property MovieSetClickScrape() As Boolean
        Get
            Return Me._moviesetclickscrape
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclickscrape = value
        End Set
    End Property

    Public Property MovieSetClickScrapeAsk() As Boolean
        Get
            Return Me._moviesetclickscrapeask
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclickscrapeask = value
        End Set
    End Property

    Public Property MovieSetLockPlot() As Boolean
        Get
            Return Me._moviesetlockplot
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetlockplot = value
        End Set
    End Property

    Public Property MovieSetLockTitle() As Boolean
        Get
            Return Me._moviesetlocktitle
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetlocktitle = value
        End Set
    End Property

    Public Property MovieSetScraperPlot() As Boolean
        Get
            Return Me._moviesetscraperplot
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetscraperplot = value
        End Set
    End Property

    Public Property MovieSetScraperTitle() As Boolean
        Get
            Return Me._moviesetscrapertitle
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetscrapertitle = value
        End Set
    End Property

    Public Property MovieSetMissingBanner() As Boolean
        Get
            Return Me._MovieSetmissingbanner
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingbanner = value
        End Set
    End Property

    Public Property MovieSetMissingClearArt() As Boolean
        Get
            Return Me._MovieSetmissingclearart
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingclearart = value
        End Set
    End Property

    Public Property MovieSetMissingClearLogo() As Boolean
        Get
            Return Me._MovieSetmissingclearlogo
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingclearlogo = value
        End Set
    End Property

    Public Property MovieSetMissingDiscArt() As Boolean
        Get
            Return Me._MovieSetmissingdiscart
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingdiscart = value
        End Set
    End Property

    Public Property MovieSetMissingFanart() As Boolean
        Get
            Return Me._MovieSetmissingfanart
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingfanart = value
        End Set
    End Property

    Public Property MovieSetMissingLandscape() As Boolean
        Get
            Return Me._MovieSetmissinglandscape
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissinglandscape = value
        End Set
    End Property

    Public Property MovieSetMissingNFO() As Boolean
        Get
            Return Me._MovieSetmissingnfo
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingnfo = value
        End Set
    End Property

    Public Property MovieSetMissingPoster() As Boolean
        Get
            Return Me._MovieSetmissingposter
        End Get
        Set(ByVal value As Boolean)
            Me._MovieSetmissingposter = value
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

    Public Property GeneralDoubleClickScrape() As Boolean
        Get
            Return Me._generaldoubleclickscrape
        End Get
        Set(ByVal value As Boolean)
            Me._generaldoubleclickscrape = value
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

    Public Property MovieTrailerDefaultSearch() As String
        Get
            Return Me._movietrailerdefaultsearch
        End Get
        Set(ByVal value As String)
            Me._movietrailerdefaultsearch = value
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

    Public Property MovieSetSortTokensIsEmpty() As Boolean
        Get
            Return Me._moviesetsorttokensisempty
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetsorttokensisempty = value
        End Set
    End Property

    Public Property TVSortTokensIsEmpty() As Boolean
        Get
            Return Me._tvsorttokensisempty
        End Get
        Set(ByVal value As Boolean)
            Me._tvsorttokensisempty = value
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

    Public Property MovieScraperCertOnlyValue() As Boolean
        Get
            Return Me._moviescrapercertonlyvalue
        End Get
        Set(ByVal value As Boolean)
            Me._moviescrapercertonlyvalue = value
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

    Public Property MovieSetPosterHeight() As Integer
        Get
            Return Me._moviesetposterheight
        End Get
        Set(ByVal value As Integer)
            Me._moviesetposterheight = value
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

    Public Property MovieSetBannerQual() As Integer
        Get
            Return Me._moviesetbannerqual
        End Get
        Set(ByVal value As Integer)
            Me._moviesetbannerqual = value
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

    Public Property MovieSetPosterQual() As Integer
        Get
            Return Me._moviesetposterqual
        End Get
        Set(ByVal value As Integer)
            Me._moviesetposterqual = value
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

    Public Property MovieSetPosterWidth() As Integer
        Get
            Return Me._moviesetposterwidth
        End Get
        Set(ByVal value As Integer)
            Me._moviesetposterwidth = value
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

    Public Property MovieSetFanartPrefSize() As Enums.FanartSize
        Get
            Return Me._moviesetfanartprefsize
        End Get
        Set(ByVal value As Enums.FanartSize)
            Me._moviesetfanartprefsize = value
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

    Public Property MovieSetPosterPrefSize() As Enums.PosterSize
        Get
            Return Me._moviesetposterprefsize
        End Get
        Set(ByVal value As Enums.PosterSize)
            Me._moviesetposterprefsize = value
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

    Public Property TVEpisodePosterPrefSize() As Enums.TVEpisodePosterSize

        Get
            Return Me._tvepisodeposterprefsize
        End Get
        Set(ByVal value As Enums.TVEpisodePosterSize)
            Me._tvepisodeposterprefsize = value
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

    Public Property MovieSetBannerPrefType() As Enums.MovieBannerType
        Get
            Return Me._moviesetbannerpreftype
        End Get
        Set(ByVal value As Enums.MovieBannerType)
            Me._moviesetbannerpreftype = value
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

    Public Property TVScraperEpisodeRuntime() As Boolean
        Get
            Return Me._tvscraperepisoderuntime
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperepisoderuntime = value
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

    Public Property TVScraperShowRuntime() As Boolean
        Get
            Return Me._tvscrapershowruntime
        End Get
        Set(ByVal value As Boolean)
            Me._tvscrapershowruntime = value
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

    Public Property MovieSetBannerWidth() As Integer
        Get
            Return Me._moviesetbannerwidth
        End Get
        Set(ByVal value As Integer)
            Me._moviesetbannerwidth = value
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

    Public Property MovieSetBannerHeight() As Integer
        Get
            Return Me._moviesetbannerheight
        End Get
        Set(ByVal value As Integer)
            Me._moviesetbannerheight = value
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

    Public Property GeneralShowImgNames() As Boolean
        Get
            Return Me._generalshowimgnames
        End Get
        Set(ByVal value As Boolean)
            Me._generalshowimgnames = value
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

    Public Property TVLockShowRuntime() As Boolean
        Get
            Return Me._tvlockshowruntime
        End Get
        Set(ByVal value As Boolean)
            Me._tvlockshowruntime = value
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

    Public Property MovieSetSortTokens() As List(Of String)
        Get
            Return Me._moviesetsorttokens
        End Get
        Set(ByVal value As List(Of String))
            Me._moviesetsorttokens = value
        End Set
    End Property

    Public Property TVSortTokens() As List(Of String)
        Get
            Return Me._tvsorttokens
        End Get
        Set(ByVal value As List(Of String))
            Me._tvsorttokens = value
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

    Public Property GeneralMainFilterSortDate() As String
        Get
            Return Me._generalmainfiltersortdate
        End Get
        Set(ByVal value As String)
            Me._generalmainfiltersortdate = value
        End Set
    End Property

    Public Property GeneralMainFilterSortTitle() As String
        Get
            Return Me._generalmainfiltersorttitle
        End Get
        Set(ByVal value As String)
            Me._generalmainfiltersorttitle = value
        End Set
    End Property

    Public Property GeneralMainFilterIMDBRating() As String
        Get
            Return Me._generalmainfilterIMDBrating
        End Get
        Set(ByVal value As String)
            Me._generalmainfilterIMDBrating = value
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

    Public Property TVScraperUseSRuntimeForEp() As Boolean
        Get
            Return Me._tvscraperusesruntimeforep
        End Get
        Set(ByVal value As Boolean)
            Me._tvscraperusesruntimeforep = value
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

    Public Property GeneralDateTime() As Enums.DateTime
        Get
            Return Me._generaldatetime
        End Get
        Set(ByVal value As Enums.DateTime)
            Me._generaldatetime = value
        End Set
    End Property

    Public Property GeneralDateAddedIgnoreNFO() As Boolean
        Get
            Return Me._generaldateaddedignorenfo
        End Get
        Set(ByVal value As Boolean)
            Me._generaldateaddedignorenfo = value
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

    Public Property MovieSetUseMSAA() As Boolean
        Get
            Return Me._moviesetusemsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetusemsaa = value
        End Set
    End Property

    Public Property MovieSetBannerMSAA() As Boolean
        Get
            Return Me._moviesetbannermsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetbannermsaa = value
        End Set
    End Property

    Public Property MovieSetClearArtMSAA() As Boolean
        Get
            Return Me._moviesetclearartmsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclearartmsaa = value
        End Set
    End Property

    Public Property MovieSetClearLogoMSAA() As Boolean
        Get
            Return Me._moviesetclearlogomsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetclearlogomsaa = value
        End Set
    End Property

    Public Property MovieSetDiscArtMSAA() As Boolean
        Get
            Return Me._moviesetdiscartmsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetdiscartmsaa = value
        End Set
    End Property

    Public Property MovieSetFanartMSAA() As Boolean
        Get
            Return Me._moviesetfanartmsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetfanartmsaa = value
        End Set
    End Property

    Public Property MovieSetLandscapeMSAA() As Boolean
        Get
            Return Me._moviesetlandscapemsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetlandscapemsaa = value
        End Set
    End Property

    Public Property MovieSetNFOMSAA() As Boolean
        Get
            Return Me._moviesetnfomsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetnfomsaa = value
        End Set
    End Property

    Public Property MovieSetPosterMSAA() As Boolean
        Get
            Return Me._moviesetpostermsaa
        End Get
        Set(ByVal value As Boolean)
            Me._moviesetpostermsaa = value
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

    Public Property TVShowMissingBanner() As Boolean
        Get
            Return Me._tvshowmissingbanner
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingbanner = value
        End Set
    End Property

    Public Property TVShowMissingCharacterArt() As Boolean
        Get
            Return Me._tvshowmissingclearart
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingclearart = value
        End Set
    End Property

    Public Property TVShowMissingClearArt() As Boolean
        Get
            Return Me._tvshowmissingclearart
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingclearart = value
        End Set
    End Property

    Public Property TVShowMissingClearLogo() As Boolean
        Get
            Return Me._tvshowmissingclearlogo
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingclearlogo = value
        End Set
    End Property

    Public Property TVShowMissingEFanarts() As Boolean
        Get
            Return Me._tvshowmissingefanarts
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingefanarts = value
        End Set
    End Property

    Public Property TVShowMissingFanart() As Boolean
        Get
            Return Me._tvshowmissingfanart
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingfanart = value
        End Set
    End Property

    Public Property TVShowMissingLandscape() As Boolean
        Get
            Return Me._tvshowmissinglandscape
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissinglandscape = value
        End Set
    End Property

    Public Property TVShowMissingNFO() As Boolean
        Get
            Return Me._tvshowmissingnfo
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingnfo = value
        End Set
    End Property

    Public Property TVShowMissingPoster() As Boolean
        Get
            Return Me._tvshowmissingposter
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingposter = value
        End Set
    End Property

    Public Property TVShowMissingTheme() As Boolean
        Get
            Return Me._tvshowmissingtheme
        End Get
        Set(ByVal value As Boolean)
            Me._tvshowmissingtheme = value
        End Set
    End Property

#End Region 'Properties

End Class
