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
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports NLog

Public Class Containers

#Region "Nested Types"


    <System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False, ElementName:="CommandFile")> _
    Public Class InstallCommands
        '''<remarks/>

#Region "Fields"

        Private transactionField As New List(Of CommandsTransaction)

        Private noTransactionField As New List(Of CommandsNoTransactionCommand)

#End Region 'Fields

#Region "Properties"

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute("transaction")> _
        Public Property transaction() As List(Of CommandsTransaction)
            Get
                Return Me.transactionField
            End Get
            Set(value As List(Of CommandsTransaction))
                Me.transactionField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute("noTransaction")> _
        Public Property noTransaction() As List(Of CommandsNoTransactionCommand)
            Get
                Return Me.noTransactionField
            End Get
            Set(value As List(Of CommandsNoTransactionCommand))
                Me.noTransactionField = value
            End Set
        End Property
#End Region

#Region "Methods"

        Public Sub Save(ByVal fpath As String)
            Dim xmlSer As New XmlSerializer(GetType(InstallCommands))
            Using xmlSW As New StreamWriter(fpath)
                xmlSer.Serialize(xmlSW, Me)
            End Using
        End Sub

        Public Shared Function Load(ByVal fpath As String) As Containers.InstallCommands
            If Not File.Exists(fpath) Then Return New Containers.InstallCommands
            Dim xmlSer As XmlSerializer
            xmlSer = New XmlSerializer(GetType(Containers.InstallCommands))
            Using xmlSW As New StreamReader(fpath)
                Return DirectCast(xmlSer.Deserialize(xmlSW), Containers.InstallCommands)
            End Using
        End Function
#End Region 'Methods

    End Class

    '''<remarks/>
    <System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
    Partial Public Class CommandsTransaction

        Private commandField As New List(Of CommandsTransactionCommand)

        Private nameField As String

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute("command")> _
        Public Property command() As List(Of CommandsTransactionCommand)
            Get
                Return Me.commandField
            End Get
            Set(value As List(Of CommandsTransactionCommand))
                Me.commandField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property name() As String
            Get
                Return Me.nameField
            End Get
            Set(value As String)
                Me.nameField = value
            End Set
        End Property
    End Class 'CommandsTransaction

    '''<remarks/>
    <System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
    Partial Public Class CommandsTransactionCommand

        Private descriptionField As String

        Private executeField As String

        Private typeField As String

        '''<remarks/>
        Public Property description() As String
            Get
                Return Me.descriptionField
            End Get
            Set(value As String)
                Me.descriptionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property execute() As String
            Get
                Return Me.executeField
            End Get
            Set(value As String)
                Me.executeField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property type() As String
            Get
                Return Me.typeField
            End Get
            Set(value As String)
                Me.typeField = value
            End Set
        End Property

    End Class 'CommandsTransactionCommand

    '''<remarks/>
    <System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
    Partial Public Class CommandsNoTransactionCommand

        Private descriptionField As String

        Private executeField As String

        Private typeField As String

        '''<remarks/>
        Public Property description() As String
            Get
                Return Me.descriptionField
            End Get
            Set(value As String)
                Me.descriptionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property execute() As String
            Get
                Return Me.executeField
            End Get
            Set(value As String)
                Me.executeField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property type() As String
            Get
                Return Me.typeField
            End Get
            Set(value As String)
                Me.typeField = value
            End Set
        End Property

    End Class 'CommandsNoTransactionCommand

    Public Class ImgResult

#Region "Fields"

        Dim _fanart As New MediaContainers.Fanart
        Dim _imagepath As String
        Dim _posters As New List(Of String)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Fanart() As MediaContainers.Fanart
            Get
                Return _fanart
            End Get
            Set(ByVal value As MediaContainers.Fanart)
                _fanart = value
            End Set
        End Property

        Public Property ImagePath() As String
            Get
                Return _imagepath
            End Get
            Set(ByVal value As String)
                _imagepath = value
            End Set
        End Property

        Public Property Posters() As List(Of String)
            Get
                Return _posters
            End Get
            Set(ByVal value As List(Of String))
                _posters = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _imagepath = String.Empty
            _posters.Clear()
            _fanart.Clear()
        End Sub

#End Region 'Methods

    End Class 'ImgResult

    Public Class SettingsPanel

#Region "Fields"

        Dim _imageindex As Integer
        Dim _image As Image
        Dim _name As String
        Dim _order As Integer
        Dim _panel As Panel
        Dim _parent As String
        Dim _prefix As String
        Dim _text As String
        Dim _type As String

#End Region 'Fields

#Region "Constructors"
        ''' <summary>
        ''' Overload the default New() method to provide proper initialization of fields
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ImageIndex() As Integer
            Get
                Return Me._imageindex
            End Get
            Set(ByVal value As Integer)
                Me._imageindex = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnore()> _
        Public Property Image() As Image
            Get
                Return Me._image
            End Get
            Set(ByVal value As Image)
                Me._image = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal value As String)
                Me._name = value
            End Set
        End Property

        Public Property Order() As Integer
            Get
                Return Me._order
            End Get
            Set(ByVal value As Integer)
                Me._order = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnore()> _
        Public Property Panel() As Panel
            Get
                Return Me._panel
            End Get
            Set(ByVal value As Panel)
                Me._panel = value
            End Set
        End Property

        Public Property Parent() As String
            Get
                Return Me._parent
            End Get
            Set(ByVal value As String)
                Me._parent = value
            End Set
        End Property

        Public Property Prefix() As String
            Get
                Return Me._prefix
            End Get
            Set(ByVal value As String)
                Me._prefix = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return Me._text
            End Get
            Set(ByVal value As String)
                Me._text = value
            End Set
        End Property

        Public Property Type() As String
            Get
                Return Me._type
            End Get
            Set(ByVal value As String)
                Me._type = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._imageindex = 0
            Me._image = Nothing
            Me._name = String.Empty
            Me._order = 0
            Me._panel = New Panel
            Me._parent = String.Empty
            Me._prefix = String.Empty
            Me._text = String.Empty
            Me._type = String.Empty
        End Sub

#End Region 'Methods

    End Class 'SettingsPanel

    Public Class Addon
#Region "Fields"
        Private _id As Integer
        Private _name As String
        Private _author As String
        Private _description As String
        Private _category As String
        Private _version As Single
        Private _mineversion As Single
        Private _maxeversion As Single
        Private _screenshotpath As String
        Private _screenshotimage As Image
        Private _files As Generic.SortedList(Of String, String)
        Private _deletefiles As List(Of String)
#End Region 'Fields

#Region "Properties"
        Public Property ID() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal value As String)
                Me._name = value
            End Set
        End Property

        Public Property Author() As String
            Get
                Return Me._author
            End Get
            Set(ByVal value As String)
                Me._author = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return Me._description
            End Get
            Set(ByVal value As String)
                Me._description = value
            End Set
        End Property

        Public Property Category() As String
            Get
                Return Me._category
            End Get
            Set(ByVal value As String)
                Me._category = value
            End Set
        End Property

        Public Property Version() As Single
            Get
                Return Me._version
            End Get
            Set(ByVal value As Single)
                Me._version = value
            End Set
        End Property

        Public Property MinEVersion() As Single
            Get
                Return Me._mineversion
            End Get
            Set(ByVal value As Single)
                Me._mineversion = value
            End Set
        End Property

        Public Property MaxEVersion() As Single
            Get
                Return Me._maxeversion
            End Get
            Set(ByVal value As Single)
                Me._maxeversion = value
            End Set
        End Property

        Public Property ScreenShotPath() As String
            Get
                Return Me._screenshotpath
            End Get
            Set(ByVal value As String)
                Me._screenshotpath = value
            End Set
        End Property

        Public Property ScreenShotImage() As Image
            Get
                Return Me._screenshotimage
            End Get
            Set(ByVal value As Image)
                Me._screenshotimage = value
            End Set
        End Property

        Public Property Files() As Generic.SortedList(Of String, String)
            Get
                Return Me._files
            End Get
            Set(ByVal value As Generic.SortedList(Of String, String))
                Me._files = value
            End Set
        End Property

        Public Property DeleteFiles() As List(Of String)
            Get
                Return Me._deletefiles
            End Get
            Set(ByVal value As List(Of String))
                Me._deletefiles = value
            End Set
        End Property
#End Region 'Properties

#Region "Constructors"
        Public Sub New()
            Me.Clear()
        End Sub
#End Region 'Constructors

#Region "Methods"
        Public Sub Clear()
            Me._id = -1
            Me._name = String.Empty
            Me._author = String.Empty
            Me._description = String.Empty
            Me._category = String.Empty
            Me._version = -1
            Me._mineversion = -1
            Me._maxeversion = -1
            Me._screenshotpath = String.Empty
            Me._screenshotimage = Nothing
            Me._files = New Generic.SortedList(Of String, String)
            Me._deletefiles = New List(Of String)
        End Sub
#End Region 'Methods

    End Class 'Addon

#End Region 'Nested Types

End Class 'Containers

Public Class Enums

#Region "Enumerations"

    Public Enum SortMethod_MovieSet As Integer
        Year = 0    'default in Kodi
        Title = 1
    End Enum
    ''' <summary>
    ''' 0 results in using the current datetime when adding a video
    ''' 1 results in prefering to use the files mtime (if it's valid) and only using the file's ctime if the mtime isn't valid
    ''' 2 results in using the newer datetime of the file's mtime and ctime
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DateTime As Integer
        Now = 0
        mtime = 1
        Newer = 2
    End Enum

    Public Enum DefaultType As Integer
        All = 0
        MovieFilters = 1
        ShowFilters = 2
        EpFilters = 3
        ValidExts = 4
        TVShowMatching = 5
        TrailerCodec = 6
        ValidThemeExts = 7
        ValidSubtitleExts = 8
        MovieListSorting = 9
        MovieSetListSorting = 10
        TVEpisodeListSorting = 11
        TVSeasonListSorting = 12
        TVShowListSorting = 13
        MovieSortTokens = 14
        MovieSetSortTokens = 15
        TVSortTokens = 16
    End Enum

    Public Enum DelType As Integer
        Movies = 0
        Shows = 1
        Seasons = 2
        Episodes = 3
    End Enum

    Public Enum Content_Type As Integer
        None = 0
        Generic = 1
        Movie = 2
        MovieSet = 3
        TV = 4
        Music = 5
        Episode = 6
        Season = 7
        Show = 8
    End Enum
    ''' <summary>
    ''' Enum representing possible scrape data types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ModType_Movie As Integer
        NFO = 0
        Poster = 1
        Fanart = 2
        EThumbs = 3
        Trailer = 4
        Meta = 5
        All = 6
        DoSearch = 7
        ActorThumbs = 8
        EFanarts = 9
        Banner = 10
        DiscArt = 11
        ClearLogo = 12
        ClearArt = 13
        Landscape = 14
        WatchedFile = 15
        CharacterArt = 16
        Theme = 17
        Subtitle = 18
    End Enum
    ''' <summary>
    ''' Enum representing possible scrape data types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ModType_TV As Integer
        All = 0
        DoSearch = 1
        ActorThumbs = 2
        AllSeasonsBanner = 3
        AllSeasonsFanart = 4
        AllSeasonsLandscape = 5
        AllSeasonsPoster = 6
        EpisodeFanart = 7
        EpisodeMeta = 8
        EpisodeNfo = 9
        EpisodePoster = 10
        SeasonBanner = 11
        SeasonFanart = 12
        SeasonLandscape = 13
        SeasonPoster = 14
        ShowBanner = 15
        ShowCharacterArt = 16
        ShowClearArt = 17
        ShowClearLogo = 18
        ShowEFanarts = 19
        ShowFanart = 20
        ShowLandscape = 21
        ShowNfo = 22
        ShowPoster = 23
        ShowTheme = 24
    End Enum
    ''' <summary>
    ''' Enum representing possible scraper capabilities
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScraperCapabilities_Movie_MovieSet
        All = 0
        ActorThumbs = 1
        Banner = 2
        ClearArt = 3
        ClearLogo = 4
        DiscArt = 5
        Fanart = 6
        Landscape = 7
        Poster = 8
        Theme = 9
        Trailer = 10
    End Enum
    ''' <summary>
    ''' Enum representing possible scraper capabilities
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScraperCapabilities_TV
        All = 0
        AllSeasonsBanner = 1
        AllSeasonsFanart = 2
        AllSeasonsLandscape = 3
        AllSeasonsPoster = 4
        EpisodeFanart = 5
        EpisodePoster = 6
        SeasonBanner = 7
        SeasonFanart = 8
        SeasonLandscape = 9
        SeasonPoster = 10
        ShowBanner = 11
        ShowCharacterArt = 12
        ShowClearArt = 13
        ShowClearLogo = 14
        ShowEFanarts = 15
        ShowFanart = 16
        ShowLandscape = 17
        ShowPoster = 18
        ShowTheme = 19
    End Enum

    Public Enum ModuleEventType As Integer
        ''' <summary>
        ''' Called after edit movie, movie is already saved to DB
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_Movie = 0
        ''' <summary>
        ''' Called after edit episode, episode is already saved to DB
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVEpisode = 1
        ''' <summary>
        ''' Called after edit season, season is already saved to DB
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVSeason = 2
        ''' <summary>
        ''' Called after edit show, show is already saved to DB
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVShow = 3
        ''' <summary>
        ''' Called after update DB process
        ''' </summary>
        ''' <remarks></remarks>
        AfterUpdateDB_TV = 4
        ''' <summary>
        ''' Called after update DB process
        ''' </summary>
        ''' <remarks></remarks>
        AfterUpdateDB_Movie = 5
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_Movie = 6
        ''' <summary>
        ''' Command Line Module Call
        ''' </summary>
        ''' <remarks></remarks>
        CommandLine = 7
        FrameExtrator_Movie = 8
        FrameExtrator_TVEpisode = 9
        Generic = 10
        MediaPlayer_Audio = 11
        MediaPlayer_Video = 12
        MediaPlayerPlay_Audio = 13
        MediaPlayerPlay_Video = 14
        MediaPlayerPlaylistAdd_Audio = 15
        MediaPlayerPlaylistAdd_Video = 16
        MediaPlayerPlaylistClear_Audio = 17
        MediaPlayerPlaylistClear_Video = 18
        MediaPlayerStop_Audio = 19
        MediaPlayerStop_Video = 20
        MovieImageNaming = 21
        Notification = 22
        OnBannerSave_Movie = 23
        OnClearArtSave_Movie = 24
        OnClearLogoSave_Movie = 25
        OnDiscArtSave_Movie = 26
        OnFanartDelete_Movie = 27
        OnFanartSave_Movie = 28
        OnLandscapeSave_Movie = 29
        OnNFORead_TVShow = 30
        OnNFOSave_Movie = 31
        OnNFOSave_TVShow = 32
        OnPosterDelete_Movie = 33
        OnPosterSave_Movie = 34
        OnThemeSave_Movie = 35
        OnTrailerSave_Movie = 36
        RandomFrameExtrator = 37
        ''' <summary>
        ''' Called when auto scraper finishs but before save to DB
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_Movie = 38
        ''' <summary>
        ''' Called when auto scraper finishs but before save to DB
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVEpisode = 39
        ''' <summary>
        ''' Called when single scraper finishs, movie is already saved to DB
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_Movie = 40
        ''' <summary>
        ''' Called when single scraper finishs, episode is already saved to DB
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVEpisode = 41
        ShowMovie = 42
        ShowTVShow = 43
        SyncModuleSettings = 44
        Sync_Movie = 45
        Task = 46
        TVImageNaming = 47
    End Enum

    Public Enum ScraperEventType_Movie As Integer
        BannerItem = 0
        Certification = 1
        ClearArtItem = 2
        ClearLogoItem = 3
        Country = 4
        Credits = 5
        Director = 6
        DiscArtItem = 7
        EFanartsItem = 8
        EThumbsItem = 9
        FanartItem = 10
        Genre = 11
        IMDBID = 12
        LandscapeItem = 13
        DateModified = 14
        ListTitle = 15
        MPAA = 16
        MoviePath = 17
        NFOItem = 18
        OriginalTitle = 19
        Outline = 20
        Playcount = 21
        Plot = 22
        PosterItem = 23
        Rating = 24
        ReleaseDate = 25
        Runtime = 26
        MovieSet = 27
        SortTitle = 28
        Studio = 29
        TMDBColID = 30
        TMDBID = 31
        Tagline = 32
        ThemeItem = 33
        Title = 34
        Top250 = 35
        Trailer = 36
        TrailerItem = 37
        Votes = 38
        Year = 39
    End Enum

    Public Enum ScraperEventType_MovieSet As Integer
        NFOItem = 0
        PosterItem = 1
        FanartItem = 2
        ListTitle = 6
        BannerItem = 7
        LandscapeItem = 8
        ClearArtItem = 10
        ClearLogoItem = 11
        DiscArtItem = 12
        Title = 13
    End Enum
    ''' <summary>
    ''' Enum representing valid TV series ordering.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Ordering As Integer
        Standard = 0
        DVD = 1
        Absolute = 2
        DayOfYear = 3
    End Enum
    ''' <summary>
    ''' Enum representing Order of displaying Episodes
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EpisodeSorting As Integer
        Episode = 0
        Aired = 1
    End Enum
    ''' <summary>
    ''' Enum representing which Movies/TVShows should be scraped,
    ''' and whether results should be automatically chosen or asked of the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScrapeType_Movie_MovieSet_TV As Integer
        SingleScrape = 0
        FullAuto = 1
        FullAsk = 2
        FullSkip = 3
        MissAuto = 4
        MissAsk = 5
        MissSkip = 6
        CleanFolders = 7
        NewAuto = 8
        NewAsk = 9
        NewSkip = 10
        MarkAuto = 11
        MarkAsk = 12
        MarkSkip = 13
        FilterAuto = 14
        FilterAsk = 15
        FilterSkip = 16
        CopyBackdrops = 17
        SingleField = 18
        SingleAuto = 19
        None = 99
    End Enum

    Public Enum MovieBannerSize As Integer
        Any = 0
        HD185 = 1       'Fanart.tv has only 1000x185
    End Enum

    Public Enum MovieFanartSize As Integer
        Any = 0
        HD1080 = 1
        HD720 = 2
        Thumb = 3
    End Enum

    Public Enum MoviePosterSize As Integer
        Any = 0
        HD2100 = 1
        HD1500 = 2
        HD1426 = 3
    End Enum

    Public Enum TVBannerSize As Integer
        Any = 0
        HD185 = 1       'Fanart.tv only 1000x185 (season and tv show banner)
        HD140 = 2       'TVDB has only 758x140 (season and tv show banner)
    End Enum

    Public Enum TVBannerType As Integer
        Any = 0
        Blank = 1       'will leave the title and show logo off the banner
        Graphical = 2   'will show the series name in the show's official font or will display the actual logo for the show
        Text = 3        'will show the series name as plain text in an Arial font
    End Enum

    Public Enum TVFanartSize As Integer
        Any = 0
        HD1080 = 1      'Fanart.tv has only 1920x1080
        HD720 = 2       'TVDB has 1280x720 and 1920x1080
    End Enum

    Public Enum TVPosterSize As Integer
        Any = 0
        HD1426 = 1      'Fanart.tv has only 1000x1426
        HD1000 = 2      'TVDB has only 680x1000
    End Enum

    Public Enum TVEpisodePosterSize As Integer
        Any = 0
        SD225 = 1      'TVDB has only 400 x 300 (400x225 for 16:9 images)
    End Enum

    Public Enum TVSeasonPosterSize As Integer
        Any = 0
        HD1426 = 1
        HD578 = 2
    End Enum
    ''' <summary>
    ''' Enum representing the trailer codec options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerAudioCodec As Integer
        MP4 = 0
        WebM = 1
        UNKNOWN = 3
    End Enum
    ''' <summary>
    ''' Enum representing the trailer quality options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerAudioQuality As Integer
        Any = 0
        AAC256kbps = 1
        AAC128kbps = 2
        AAC48kbps = 3
        Vorbis192kbps = 4
        Vorbis128kbps = 5
        UNKNOWN = 6
    End Enum
    ''' <summary>
    ''' Enum representing the trailer codec options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerVideoCodec As Integer
        MP4 = 0
        WebM = 1
        v3GP = 2
        FLV = 3
        UNKNOWN = 4
    End Enum
    ''' <summary>
    ''' Enum representing the trailer quality options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerVideoQuality As Integer
        Any = 0
        HD2160p = 1
        HD2160p60fps = 1
        HD1440p = 3
        HD1080p = 4
        HD1080p60fps = 5
        HD720p = 6
        HD720p60fps = 7
        HQ480p = 8
        SQ360p = 9
        SQ240p = 10
        SQ144p = 11
        SQ144p15fps = 12
        UNKNOWN = 13
    End Enum
    ''' <summary>
    ''' Enum represeting valid movie image types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ImageType_Movie As Integer
        Banner = 0
        CharacterArt = 1
        ClearArt = 2
        ClearLogo = 3
        DiscArt = 4
        EFanarts = 5
        EThumbs = 6
        Fanart = 7
        Landscape = 8
        Poster = 9
    End Enum
    ''' <summary>
    ''' Enum representing valid TV image types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ImageType_TV As Integer
        All = 0
        AllSeasonsBanner = 1
        AllSeasonsFanart = 2
        AllSeasonsLandscape = 3
        AllSeasonsPoster = 4
        EpisodeFanart = 5
        EpisodePoster = 6
        SeasonBanner = 7
        SeasonFanart = 8
        SeasonLandscape = 9
        SeasonPoster = 10
        ShowBanner = 11
        ShowCharacterArt = 12
        ShowClearArt = 13
        ShowClearLogo = 14
        ShowFanart = 15
        ShowLandscape = 16
        ShowPoster = 17
    End Enum

    Public Enum ScraperEventType_TV As Integer
        Progress = 0
        SearchResultsDownloaded = 1
        StartingDownload = 2
        ShowDownloaded = 3
        SavingStarted = 4
        ScraperDone = 5
        LoadingEpisodes = 6
        Searching = 7
        SelectImages = 8
        Verifying = 9
        Cancelled = 10
        SaveAuto = 11
    End Enum

    Public Enum TVScraperUpdateTime As Integer
        Week = 0
        BiWeekly = 1
        Month = 2
        Never = 3
        Always = 4
    End Enum

#End Region 'Enumerations

End Class 'Enums

Public Class Functions
#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the base directory that the assembly resolver uses to probe for assemblies (like the current application executable)
    ''' </summary>
    ''' <returns>Path of the directory containing the Ember executable</returns>
    Public Shared Function AppPath() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory
    End Function
    ''' <summary>
    ''' Determine whether we are running a 64-bit instance
    ''' </summary>
    ''' <returns><c>True</c> if we are running a 64-bit instance</returns>
    ''' <remarks>Note that the value of IntPtr.Size is 4 in a 32-bit process, and 8 in a 64-bit process</remarks>
    Public Shared Function Check64Bit() As Boolean
        Return (IntPtr.Size = 8)
    End Function
    ''' <summary>
    ''' Are we running on a Windows operating system?
    ''' </summary>
    ''' <returns><c>True</c>if we are running on Windows, <c>False</c> otherwise</returns>
    Public Shared Function CheckIfWindows() As Boolean
        Dim os As OperatingSystem = Environment.OSVersion
        Dim pid As PlatformID = os.Platform
        If pid = PlatformID.Win32NT OrElse pid = PlatformID.Win32Windows OrElse pid = PlatformID.Win32S OrElse pid = PlatformID.WinCE Then
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' Determine whether this instance is intended as a beta test version
    ''' </summary>
    ''' <returns><c>True</c> if this instance is a beta version, <c>False</c> otherwise</returns>
    ''' <remarks>The defining test for being a beta version is the existance of the file "Beta.Tester" in the same directory as
    ''' the Ember executable.</remarks>
    Public Shared Function IsBetaEnabled() As Boolean
        If File.Exists(Path.Combine(AppPath, "Beta.Tester")) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Check for the lastest version of Ember
    ''' </summary>
    ''' <returns>Latest version as integer</returns>
    ''' <remarks>Not implemented yet. This method is currently a stub, and always returns False</remarks>
    Public Shared Function CheckNeedUpdate() As Boolean
        'TODO STUB - Not implemented yet
        Dim needUpdate As Boolean = False
        'Dim sHTTP As New HTTP
        'Dim platform As String = "x86"
        'Dim updateXML As String = sHTTP.DownloadData(String.Format("http://pcjco.dommel.be/emm-r/{0}/versions.xml", If(IsBetaEnabled(), "updatesbeta", "updates")))
        'sHTTP = Nothing
        'If updateXML.Length > 0 Then
        '    For Each v As ModulesManager.VersionItem In ModulesManager.VersionList
        '        Dim vl As ModulesManager.VersionItem = v
        '        Dim n As String = String.Empty
        '        Dim xmlUpdate As XDocument
        '        Try
        '            xmlUpdate = XDocument.Parse(updateXML)
        '        Catch
        '            Return False
        '        End Try
        '        Dim xUdpate = From xUp In xmlUpdate...<Config>...<Modules>...<File> Where (xUp.<Version>.Value <> "" AndAlso xUp.<Name>.Value = vl.AssemblyFileName AndAlso xUp.<Platform>.Value = platform) Select xUp.<Version>.Value
        '        Try
        '            If Convert.ToInt16(xUdpate(0)) > Convert.ToInt16(v.Version) Then
        '                v.NeedUpdate = True
        '                needUpdate = True
        '            End If
        '        Catch ex As Exception
        '        End Try
        '    Next
        'End If
        Return needUpdate
    End Function
    ''' <summary>
    ''' Convert a Unix Timestamp to a VB DateTime
    ''' </summary>
    ''' <param name="timestamp">A valid unix-style timestamp</param>
    ''' <returns>An appropriately formatted DateTime representing the supplied timestamp</returns>
    ''' <remarks></remarks>
    ''' <exception cref="ArgumentException"> thrown when <paramref name="timestamp"/> is <c>Double.NAN</c>.</exception>
    ''' <exception cref="ArgumentOutOfRangeException"> thrown when <paramref name="timestamp"/> is <c>Double.NegativeInfinity</c> or <c>Double.PositiveInfinity</c>,
    ''' or if resulting <c>DateTime</c> would be outside the bounds of Jan 1, 0001 and Dec 31, 9999</exception>
    Public Shared Function ConvertFromUnixTimestamp(ByVal timestamp As Double) As DateTime
        If timestamp.CompareTo(Double.NaN) = 0 Then
            Throw New ArgumentException("Parameter was not a number (Double.NAN)", "timestamp")
        End If
        'Input can not be: NaN (not a number), Positive Infinity, Negative Infinity
        If timestamp.CompareTo(Double.NegativeInfinity) = 0 OrElse timestamp.CompareTo(Double.PositiveInfinity) = 0 Then
            Throw New ArgumentOutOfRangeException("timestamp", timestamp, "timestamp must be a valid discreet value.")
        End If
        'Values outside these ranges exceed the DateTime capacity of Jan 1, 0001 and Dec 31, 9999
        If timestamp > 253402300799.0R OrElse timestamp < -62135596800.0R Then
            Throw New ArgumentOutOfRangeException("timestamp", timestamp, "timestamp must resolve between Jan 1, 0001 and Dec 31, 9999")
        End If
        Dim origin As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Return origin.AddSeconds(timestamp)
    End Function
    ''' <summary>
    ''' Convert a VB-styled DateTime to a valid Unix-style timestamp
    ''' </summary>
    ''' <param name="data">A valid VB-style DateTime</param>
    ''' <returns>A value representing the DateTime as a unix-style timestamp <c>Double</c></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertToUnixTimestamp(ByVal data As DateTime) As Double
        Dim origin As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim diff As System.TimeSpan = data - origin
        Return Math.Floor(diff.TotalSeconds)
    End Function
    ' TODO DOC Need appropriate header and tests
    Public Shared Function LocksToOptions() As Structures.ScrapeOptions_Movie
        Dim options As New Structures.ScrapeOptions_Movie
        With options
            .bCast = True
            .bCert = True
            .bCollectionID = True
            .bCountry = True
            .bDirector = True
            .bFullCrew = True
            .bGenre = Not Master.eSettings.MovieLockGenre    'Dekker500 This used to just be =True
            .bMPAA = True
            .bMusicBy = True
            .bOriginalTitle = True
            .bOtherCrew = True
            .bOutline = Not Master.eSettings.MovieLockOutline
            .bPlot = Not Master.eSettings.MovieLockPlot
            .bProducers = True
            .bRating = Not Master.eSettings.MovieLockRating
            .bRelease = True
            .bRuntime = True
            .bStudio = Not Master.eSettings.MovieLockStudio
            .bTagline = Not Master.eSettings.MovieLockTagline
            .bTitle = Not Master.eSettings.MovieLockTitle
            .bTop250 = True
            .bTrailer = Not Master.eSettings.MovieLockTrailer
            .bVotes = True
            .bWriters = True
            .bYear = True
        End With
        Return options
    End Function
    ''' <summary>
    ''' Create a collection of default Movie and TV scrape options
    ''' based off the currently selected options. 
    ''' These default options are Master.DefaultOptions and Master.DefaultTVOptions
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub CreateDefaultOptions()
        'TODO need proper unit test
        With Master.DefaultMovieOptions
            'TODO Dekker500 - These seem to be missing Add fields to Master!!!???
            '.bLanguageA = Master.eSettings.FieldLanguageA
            '.bLanguageV = Master.eSettings.FieldLanguageV
            '.buseMPAAForFSK = Master.eSettings.UseMPAAForFSK
            .bCast = Master.eSettings.MovieScraperCast
            .bCert = Master.eSettings.MovieScraperCert
            .bCollectionID = Master.eSettings.MovieScraperCollectionID
            .bCountry = Master.eSettings.MovieScraperCountry
            .bDirector = Master.eSettings.MovieScraperDirector
            .bGenre = Master.eSettings.MovieScraperGenre
            .bMPAA = Master.eSettings.MovieScraperMPAA
            .bOriginalTitle = Master.eSettings.MovieScraperOriginalTitle
            .bOutline = Master.eSettings.MovieScraperOutline
            .bPlot = Master.eSettings.MovieScraperPlot
            .bRating = Master.eSettings.MovieScraperRating
            .bRelease = Master.eSettings.MovieScraperRelease
            .bRuntime = Master.eSettings.MovieScraperRuntime
            .bStudio = Master.eSettings.MovieScraperStudio
            .bTagline = Master.eSettings.MovieScraperTagline
            .bTitle = Master.eSettings.MovieScraperTitle
            .bTop250 = Master.eSettings.MovieScraperTop250
            .bTrailer = Master.eSettings.MovieScraperTrailer
            .bVotes = Master.eSettings.MovieScraperVotes
            .bWriters = Master.eSettings.MovieScraperCredits
            .bYear = Master.eSettings.MovieScraperYear
        End With

        With Master.DefaultMovieSetOptions
            .bPlot = Master.eSettings.MovieSetScraperPlot
            .bTitle = Master.eSettings.MovieSetScraperTitle
        End With

        With Master.DefaultTVOptions
            .bEpActors = Master.eSettings.TVScraperEpisodeActors
            .bEpAired = Master.eSettings.TVScraperEpisodeAired
            .bEpCredits = Master.eSettings.TVScraperEpisodeCredits
            .bEpDirector = Master.eSettings.TVScraperEpisodeDirector
            .bEpEpisode = Master.eSettings.TVScraperEpisodeEpisode
            .bEpGuestStars = Master.eSettings.TVScraperEpisodeGuestStars
            .bEpPlot = Master.eSettings.TVScraperEpisodePlot
            .bEpRating = Master.eSettings.TVScraperEpisodeRating
            .bEpRuntime = Master.eSettings.TVScraperEpisodeRuntime
            .bEpSeason = Master.eSettings.TVScraperEpisodeSeason
            .bEpTitle = Master.eSettings.TVScraperEpisodeTitle
            .bEpVotes = Master.eSettings.TVScraperEpisodeVotes
            .bShowActors = Master.eSettings.TVScraperShowActors
            .bShowEpisodeGuide = Master.eSettings.TVScraperShowEpiGuideURL
            .bShowGenre = Master.eSettings.TVScraperShowGenre
            .bShowMPAA = Master.eSettings.TVScraperShowMPAA
            .bShowPlot = Master.eSettings.TVScraperShowPlot
            .bShowPremiered = Master.eSettings.TVScraperShowPremiered
            .bShowRating = Master.eSettings.TVScraperShowRating
            .bShowRuntime = Master.eSettings.TVScraperShowRuntime
            .bShowStatus = Master.eSettings.TVScraperShowStatus
            .bShowStudio = Master.eSettings.TVScraperShowStudio
            .bShowTitle = Master.eSettings.TVScraperShowTitle
            .bShowVotes = Master.eSettings.TVScraperShowVotes
        End With
    End Sub
    ''' <summary>
    ''' Sets the DoubleBuffered property for the supplied DataGridView.
    ''' This should have the effect of reducing any flicker during re-draw operations
    ''' </summary>
    ''' <param name="cDGV">DataGridView for which the <c>DoubleBuffered</c> property is to be set.</param>
    ''' <remarks></remarks>
    Public Shared Sub DGVDoubleBuffer(ByRef cDGV As DataGridView)
        Dim conType As Type = cDGV.GetType
        Dim pi As System.Reflection.PropertyInfo = conType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        pi.SetValue(cDGV, True, Nothing)
    End Sub
    ''' <summary>
    ''' Retrieves the Ember version
    ''' </summary>
    ''' <returns>A string representing the four-part period-separated version number</returns>
    ''' <remarks>An example of the string returned would be "1.4.0.0", without the quotes, of course</remarks>
    Public Shared Function EmberAPIVersion() As String
        Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion
    End Function

    ''' <summary>
    ''' Get the changelog for the latest version
    ''' </summary>
    ''' <returns>Changelog as string</returns>
    ''' <remarks>Not implemented yet. Always returns "Unavailable"</remarks>
    Public Shared Function GetChangelog() As String
        'TODO STUB - Not implemented yet

        'Try
        '    Dim sHTTP As New HTTP
        '    Dim strChangelog As String = sHTTP.DownloadData(String.Format("http://pcjco.dommel.be/emm-r/{0}/WhatsNew.txt", If(IsBetaEnabled(), "updatesbeta", "updates")))
        '    sHTTP = Nothing

        '    If strChangelog.Length > 0 Then
        '        Return strChangelog
        '    End If
        'Catch ex As Exception
        '    logger.Error(GetType(Functions),ex.Message, ex.StackTrace, "Error")
        'End Try
        Return "Unavailable"
    End Function

    ''' <summary>
    ''' Get the number of the last sequential extrathumb to make sure we're not overwriting current ones.
    ''' </summary>
    ''' <param name="sPath">Full path to extrathumbs directory</param>
    ''' <returns>Last detected number of the discovered extrathumbs.</returns>
    Public Shared Function GetExtraModifier(ByVal sPath As String) As Integer
        Dim iMod As Integer = 0
        Dim lThumbs As New List(Of String)

        Try
            If Directory.Exists(sPath) Then

                Try
                    lThumbs.AddRange(Directory.GetFiles(sPath, "thumb*.jpg"))
                Catch
                End Try

                If lThumbs.Count > 0 Then
                    Dim cur As Integer = 0
                    For Each t As String In lThumbs
                        cur = Convert.ToInt32(Regex.Match(t, "(\d+).jpg").Groups(1).ToString)
                        iMod = Math.Max(iMod, cur)
                    Next
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Failed trying to identify last thumb from path: " & sPath, ex)
        End Try

        Return iMod
    End Function
    ''' <summary>
    ''' Get a path to the ffmpeg included with the Ember distribution
    ''' </summary>
    ''' <returns>A path to an instance of ffmpeg</returns>
    ''' <remarks>Windows distributions have ffmpeg in the Bin subdirectory.
    ''' Note that no validation is done to ensure that ffmpeg actually exists.</remarks>
    Public Shared Function GetFFMpeg() As String
        If Master.isWindows Then
            Return String.Concat(Functions.AppPath, "Bin", Path.DirectorySeparatorChar, "ffmpeg.exe")
        Else
            Return "ffmpeg"
        End If
    End Function

    ''' <summary>
    ''' Populate Master.SourcesList with a list of paths to all (media?) sources stored in the database
    ''' </summary>
    Public Shared Sub GetListOfSources()
        Master.SourcesList.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT sources.Path FROM sources;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Master.SourcesList.Add(SQLreader("Path").ToString)
                End While
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Determines the path to the desired season of a given show
    ''' </summary>
    ''' <param name="ShowPath">The root path for a TV show</param>
    ''' <param name="iSeason">The desired season number for which a path is desired</param>
    ''' <returns>A path to the TV show's desired season number, or <c>String.Empty</c> if none is found</returns>
    ''' <remarks></remarks>
    Public Shared Function GetSeasonDirectoryFromShowPath(ByVal ShowPath As String, ByVal iSeason As Integer) As String
        If Directory.Exists(ShowPath) Then
            Dim SeasonFolderPattern As New List(Of String)
            SeasonFolderPattern.Add("(?<season>specials?)$")
            SeasonFolderPattern.Add("^(s(eason)?)?[\W_]*(?<season>[0-9]+)$")
            SeasonFolderPattern.Add("[^\w]s(eason)?[\W_]*(?<season>[0-9]+)")
            Dim dInfo As New DirectoryInfo(ShowPath)

            For Each sDir As DirectoryInfo In dInfo.GetDirectories
                For Each pattern In SeasonFolderPattern
                    For Each sMatch As Match In Regex.Matches(FileUtils.Common.GetDirectory(sDir.FullName), pattern, RegexOptions.IgnoreCase)
                        Try
                            If (Integer.TryParse(sMatch.Groups("season").Value, 0) AndAlso iSeason = Convert.ToInt32(sMatch.Groups("season").Value)) OrElse (Regex.IsMatch(sMatch.Groups("season").Value, "specials?", RegexOptions.IgnoreCase) AndAlso iSeason = 0) Then
                                Return sDir.FullName
                            End If
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & " Failed to determine path for season " & iSeason & " in path: " & ShowPath, ex)
                        End Try
                    Next
                Next
            Next
        End If
        'no matches
        Return String.Empty
    End Function
    ''' <summary>
    ''' Determine whether user has specified a subset of the scrapable items (Master.GlobalScrapeMod).
    ''' </summary>
    ''' <returns><c>True</c> if at least one modifier has been selected, <c>False</c>if no item has been selected.</returns>
    Public Shared Function HasModifier() As Boolean
        With Master.GlobalScrapeMod
            If .EThumbs OrElse .EFanarts OrElse .Fanart OrElse .Meta OrElse .NFO OrElse .Poster OrElse .Trailer Then Return True
        End With

        Return False
    End Function
    ''' <summary>
    ''' Determine whether the supplied path is already defined as a TV Show season subdirectory
    ''' </summary>
    ''' <param name="sPath">The path to look for</param>
    ''' <returns><c>True</c> if the supplied path is found in the list of configured TV Show season directories, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function IsSeasonDirectory(ByVal sPath As String) As Boolean
        'TODO Warning - Potential for false positives and false negatives as paths can be defined in different ways to arrive at the same destination
        Dim SeasonFolderPattern As New List(Of String)
        SeasonFolderPattern.Add("(?<season>specials?)$")
        SeasonFolderPattern.Add("^(s(eason)?)?[\W_]*(?<season>[0-9]+)$")
        SeasonFolderPattern.Add("[^\w]s(eason)?[\W_]*(?<season>[0-9]+)")
        For Each pattern In SeasonFolderPattern
            If Regex.IsMatch(FileUtils.Common.GetDirectory(sPath), pattern, RegexOptions.IgnoreCase) Then Return True
        Next
        'no matches
        Return False
    End Function
    ''' <summary>
    ''' Convert a List(of T) to a string of separated values
    ''' </summary>
    ''' <param name="source">List(of T)</param>
    ''' <param name="separator">Character or string to use as a value separator</param>
    ''' <returns>String of separated List values.
    ''' If the list is empty, an empty string will be returned.
    ''' If the separator is empty or missing, assume "," is the separator</returns>
    Public Shared Function ListToStringWithSeparator(Of T)(ByVal source As IList(Of T), ByVal separator As String) As String
        If source Is Nothing Then Return String.Empty
        If String.IsNullOrEmpty(separator) Then separator = ","

        Dim values As String() = source.Cast(Of Object)().Where(Function(n) n IsNot Nothing).Select(Function(s) s.ToString).ToArray

        Return String.Join(separator, values)
    End Function
    ''' <summary>
    ''' Set the DoubleBuffered property of the supplied Panel. This will tell the control to redraw its surface using a 
    ''' secondary buffer to reduce/prevent flicker.
    ''' </summary>
    ''' <param name="cPNL">Panel control to DoubleBuffer</param>
    ''' <remarks></remarks>
    Public Shared Sub PNLDoubleBuffer(ByRef cPNL As Panel)
        If cPNL Is Nothing Then Throw New ArgumentNullException("Source parameter cannot be nothing")
        Dim conType As Type = cPNL.GetType
        Dim pi As System.Reflection.PropertyInfo = conType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        pi.SetValue(cPNL, True, Nothing)
    End Sub

    ''' <summary>
    ''' Constrain a number to the nearest multiple. 
    ''' </summary>
    ''' <param name="iNumber">Number to quantize</param>
    ''' <param name="iMultiple">Multiple of constraint. This value can not be zero.</param>
    Public Shared Function Quantize(ByVal iNumber As Integer, ByVal iMultiple As Integer) As Integer
        If iMultiple = 0 Then Throw New ArgumentOutOfRangeException("Multiple must be greater than zero (0)")
        Return Convert.ToInt32(System.Math.Round(iNumber / iMultiple, 0) * iMultiple)
    End Function
    ''' <summary>
    ''' Reads bytes from a given stream and returns them as a Byte array
    ''' </summary>
    ''' <param name="rStream">Stream to be read from</param>
    ''' <returns>Byte array representing the contents of the supplied Stream</returns>
    ''' <remarks>Stream is read using a 4k buffer</remarks>
    Public Shared Function ReadStreamToEnd(ByVal rStream As Stream) As Byte()
        If rStream Is Nothing Then Throw New ArgumentNullException("Source parameter cannot be Nothing")
        Dim StreamBuffer(4096) As Byte
        Dim BlockSize As Integer = 0
        Using mStream As MemoryStream = New MemoryStream()
            Do
                BlockSize = rStream.Read(StreamBuffer, 0, StreamBuffer.Length)
                If BlockSize > 0 Then mStream.Write(StreamBuffer, 0, BlockSize)
            Loop While BlockSize > 0
            Return mStream.ToArray
        End Using
    End Function
    ''' <summary>
    ''' Determine the Structures.ScrapeModifier options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.ScrapeModifier</param>
    ''' <param name="Options2">Secondary Structures.ScrapeModifier</param>
    ''' <returns>Structures.ScrapeModifier representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function ScrapeModifierAndAlso(ByVal Options As Structures.ScrapeModifier_Movie_MovieSet, ByVal Options2 As Structures.ScrapeModifier_Movie_MovieSet) As Structures.ScrapeModifier_Movie_MovieSet
        Dim filterModifier As New Structures.ScrapeModifier_Movie_MovieSet
        filterModifier.DoSearch = Options.DoSearch AndAlso Options2.DoSearch
        filterModifier.EThumbs = Options.EThumbs AndAlso Options2.EThumbs
        filterModifier.EFanarts = Options.EFanarts AndAlso Options2.EFanarts
        filterModifier.Fanart = Options.Fanart AndAlso Options2.Fanart
        filterModifier.Meta = Options.Meta AndAlso Options2.Meta
        filterModifier.NFO = Options.NFO AndAlso Options2.NFO
        filterModifier.Poster = Options.Poster AndAlso Options2.Poster
        filterModifier.Trailer = Options.Trailer AndAlso Options2.Trailer
        filterModifier.ActorThumbs = Options.ActorThumbs AndAlso Options2.ActorThumbs
        Return filterModifier
    End Function
    ''' <summary>
    ''' Determine the Structures.MovieScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.MovieScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.MovieScrapeOptions</param>
    ''' <returns>Structures.MovieScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function MovieScrapeOptionsAndAlso(ByVal Options As Structures.ScrapeOptions_Movie, ByVal Options2 As Structures.ScrapeOptions_Movie) As Structures.ScrapeOptions_Movie
        Dim filterOptions As New Structures.ScrapeOptions_Movie
        filterOptions.bCast = Options.bCast AndAlso Options2.bCast
        filterOptions.bCert = Options.bCert AndAlso Options2.bCert
        filterOptions.bCollectionID = Options.bCollectionID AndAlso Options2.bCollectionID
        filterOptions.bCountry = Options.bCountry AndAlso Options2.bCountry
        filterOptions.bDirector = Options.bDirector AndAlso Options2.bDirector
        filterOptions.bFullCrew = Options.bFullCrew AndAlso Options2.bFullCrew
        filterOptions.bGenre = Options.bGenre AndAlso Options2.bGenre
        filterOptions.bMPAA = Options.bMPAA AndAlso Options2.bMPAA
        filterOptions.bMusicBy = Options.bMusicBy AndAlso Options2.bMusicBy
        filterOptions.bOriginalTitle = Options.bOriginalTitle AndAlso Options2.bOriginalTitle
        filterOptions.bOtherCrew = Options.bOtherCrew AndAlso Options2.bOtherCrew
        filterOptions.bOutline = Options.bOutline AndAlso Options2.bOutline
        filterOptions.bPlot = Options.bPlot AndAlso Options2.bPlot
        filterOptions.bProducers = Options.bProducers AndAlso Options2.bProducers
        filterOptions.bRating = Options.bRating AndAlso Options2.bRating
        filterOptions.bRelease = Options.bRelease AndAlso Options2.bRelease
        filterOptions.bRuntime = Options.bRuntime AndAlso Options2.bRuntime
        filterOptions.bStudio = Options.bStudio AndAlso Options2.bStudio
        filterOptions.bTagline = Options.bTagline AndAlso Options2.bTagline
        filterOptions.bTitle = Options.bTitle AndAlso Options2.bTitle
        filterOptions.bTop250 = Options.bTop250 AndAlso Options2.bTop250
        filterOptions.bTrailer = Options.bTrailer AndAlso Options2.bTrailer
        filterOptions.bVotes = Options.bVotes AndAlso Options2.bVotes
        filterOptions.bWriters = Options.bWriters AndAlso Options2.bWriters
        filterOptions.bYear = Options.bYear AndAlso Options2.bYear
        Return filterOptions
    End Function
    ''' <summary>
    ''' Determine the Structures.MovieSetScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.MovieSetScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.MovieSetScrapeOptions</param>
    ''' <returns>Structures.MovieSetScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function MovieSetScrapeOptionsAndAlso(ByVal Options As Structures.ScrapeOptions_MovieSet, ByVal Options2 As Structures.ScrapeOptions_MovieSet) As Structures.ScrapeOptions_MovieSet
        Dim filterOptions As New Structures.ScrapeOptions_MovieSet
        filterOptions.bPlot = Options.bPlot AndAlso Options2.bPlot
        filterOptions.bTitle = Options.bTitle AndAlso Options2.bTitle
        Return filterOptions
    End Function
    ''' <summary>
    ''' Determine the Structures.TVScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.TVScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.TVScrapeOptions</param>
    ''' <returns>Structures.TVScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function TVScrapeOptionsAndAlso(ByVal Options As Structures.TVScrapeOptions, ByVal Options2 As Structures.TVScrapeOptions) As Structures.TVScrapeOptions
        Dim filterOptions As New Structures.TVScrapeOptions
        filterOptions.bEpActors = Options.bEpActors AndAlso Options2.bEpActors
        filterOptions.bEpAired = Options.bEpAired AndAlso Options2.bEpAired
        filterOptions.bEpCredits = Options.bEpCredits AndAlso Options2.bEpCredits
        filterOptions.bEpDirector = Options.bEpDirector AndAlso Options2.bEpDirector
        filterOptions.bEpEpisode = Options.bEpEpisode AndAlso Options2.bEpEpisode
        filterOptions.bEpGuestStars = Options.bEpGuestStars AndAlso Options2.bEpGuestStars
        filterOptions.bEpPlot = Options.bEpPlot AndAlso Options2.bEpPlot
        filterOptions.bEpRating = Options.bEpRating AndAlso Options2.bEpRating
        filterOptions.bEpRuntime = Options.bEpRuntime AndAlso Options2.bEpRuntime
        filterOptions.bEpSeason = Options.bEpSeason AndAlso Options2.bEpSeason
        filterOptions.bEpTitle = Options.bEpTitle AndAlso Options2.bEpTitle
        filterOptions.bEpVotes = Options.bEpVotes AndAlso Options2.bEpVotes
        filterOptions.bShowActors = Options.bShowActors AndAlso Options2.bShowActors
        filterOptions.bShowEpisodeGuide = Options.bShowEpisodeGuide AndAlso Options2.bShowEpisodeGuide
        filterOptions.bShowGenre = Options.bShowGenre AndAlso Options2.bShowGenre
        filterOptions.bShowMPAA = Options.bShowMPAA AndAlso Options2.bShowMPAA
        filterOptions.bShowPlot = Options.bShowPlot AndAlso Options2.bShowPlot
        filterOptions.bShowPremiered = Options.bShowPremiered AndAlso Options2.bShowPremiered
        filterOptions.bShowRating = Options.bShowRating AndAlso Options2.bShowRating
        filterOptions.bShowRuntime = Options.bShowRuntime AndAlso Options2.bShowRuntime
        filterOptions.bShowStatus = Options.bShowStatus AndAlso Options2.bShowStatus
        filterOptions.bShowStudio = Options.bShowStudio AndAlso Options2.bShowStudio
        filterOptions.bShowTitle = Options.bShowTitle AndAlso Options2.bShowTitle
        filterOptions.bShowVotes = Options.bShowVotes AndAlso Options2.bShowVotes
        Return filterOptions
    End Function
    ''' <summary>
    ''' Sets the Master.GlobalScrapeMod to the given MValue
    ''' </summary>
    ''' <param name="MType">The Enums.ModType that should be changed. Note that this could be All.</param>
    ''' <param name="MValue">The <c>Boolean</c> value that you wish to change the ModType to.</param>
    ''' <param name="DoClear">If <c>True</c>, pre-initialize all Mod values to False before setting the options. 
    ''' If <c>False</c>, leave the existing options untouched wile setting the options</param>
    ''' <remarks></remarks>
    Public Shared Sub SetScraperMod(ByVal MType As Enums.ModType_Movie, ByVal MValue As Boolean, Optional ByVal DoClear As Boolean = True)
        With Master.GlobalScrapeMod
            If DoClear Then
                .ActorThumbs = False
                .Banner = False
                .CharacterArt = False
                .ClearArt = False
                .ClearLogo = False
                .DiscArt = False
                .DoSearch = False
                .EFanarts = False
                .EThumbs = False
                .Fanart = False
                .Landscape = False
                .Meta = False
                .NFO = False
                .Poster = False
                .Trailer = False
                .Theme = False
            End If

            Select Case MType
                Case Enums.ModType_Movie.All
                    '.DoSearch should not be set here as it is only needed for a re-search of a movie (first scraping or movie change).
                    .ActorThumbs = MValue
                    .Banner = MValue
                    .CharacterArt = MValue
                    .ClearArt = MValue
                    .ClearLogo = MValue
                    .DiscArt = MValue
                    .EFanarts = MValue
                    .EThumbs = MValue
                    .Fanart = MValue
                    .Landscape = MValue
                    .Meta = MValue
                    .NFO = MValue
                    .Poster = MValue
                    .Trailer = MValue
                    .Theme = MValue
                Case Enums.ModType_Movie.ActorThumbs
                    .ActorThumbs = MValue
                Case Enums.ModType_Movie.Banner
                    .Banner = MValue
                Case Enums.ModType_Movie.CharacterArt
                    .CharacterArt = MValue
                Case Enums.ModType_Movie.ClearArt
                    .ClearArt = MValue
                Case Enums.ModType_Movie.ClearLogo
                    .ClearLogo = MValue
                Case Enums.ModType_Movie.DiscArt
                    .DiscArt = MValue
                Case Enums.ModType_Movie.DoSearch
                    .DoSearch = MValue
                Case Enums.ModType_Movie.EFanarts
                    .EFanarts = MValue
                Case Enums.ModType_Movie.EThumbs
                    .EThumbs = MValue
                Case Enums.ModType_Movie.Fanart
                    .Fanart = MValue
                Case Enums.ModType_Movie.Landscape
                    .Landscape = MValue
                Case Enums.ModType_Movie.Meta
                    .Meta = MValue
                Case Enums.ModType_Movie.NFO
                    .NFO = MValue
                Case Enums.ModType_Movie.Poster
                    .Poster = MValue
                Case Enums.ModType_Movie.Trailer
                    .Trailer = MValue
                Case Enums.ModType_Movie.Theme
                    .Theme = MValue
            End Select

        End With
    End Sub
    ''' <summary>
    ''' This method launches the user's default web browser to the supplied destination
    ''' </summary>
    ''' <param name="Destination">URL or file to be launched. Note that care should be taken when launching files, as it exposes
    ''' the user to a high security risk.</param>
    ''' <param name="AllowLocalFiles">If <c>False</c>, no action will be taken if the destination points to a local file.
    ''' This protects the user's machine from malformed URLs</param>
    ''' <returns><c>True</c> if process was launched, or <c>False</c> if an error prevented the launch from occurring.
    ''' Note that a process may be launched but produce no visible results. This flag only indicates that the process was called.</returns>
    ''' <remarks>Note that if the supplied string is not a valid URI, 
    ''' or if it refers to a local file,
    ''' a log message will be generated but no further action will be taken.
    ''' This is to prevent malformed URIs from attacking the user's machine.</remarks>
    Public Shared Function Launch(ByRef Destination As String, Optional ByRef AllowLocalFiles As Boolean = False) As Boolean
        If String.IsNullOrEmpty(Destination) Then
            logger.Error("Blank destination")
            Return False
        End If
        Try
            Dim uriDestination As New Uri(Destination)
            If uriDestination.IsFile() Then
                If (Not AllowLocalFiles) Then
                    logger.Error("Destination is a file, which is not permitted for security reasons <{0}>", Destination)
                    Return False
                Else
                    If (Not File.Exists(uriDestination.LocalPath)) Then
                        logger.Error("Destination is a file, but it does not exist <{0}>", Destination)
                        Return False
                    Else
                        If Master.isWindows Then
                            Process.Start(uriDestination.LocalPath)
                        Else
                            Using Explorer As New Process
                                Explorer.StartInfo.FileName = "xdg-open"
                                Explorer.StartInfo.Arguments = uriDestination.LocalPath
                                Explorer.Start()
                            End Using
                        End If
                        Return True
                    End If
                End If
            End If

            'If we got this far, everything is OK, so we can go ahead and launch it!
            If Master.isWindows Then
                Process.Start(uriDestination.AbsoluteUri())
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = uriDestination.AbsoluteUri()
                    Explorer.Start()
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & Destination & ">", ex)
            Return False
        End Try
        'If you got here, everything went fine
        Return True
    End Function

    ''' <summary>
    ''' Check version of the MediaInfo dll. If newer than 0.7.11 then don't try to scan disc images with it.
    ''' </summary>
    Public Shared Sub TestMediaInfoDLL()
        'TODO Warning - MediaInfo is newer than this method tests for - is this method required? Looks like it will ALWAYS return False
        Dim dllPath As String = "Invalid path"
        Try

            ' Since MediaInfo support ISO now -> FileVersion Check no longer needed!
            Master.CanScanDiscImage = True

            ' 'just assume dll is there since we're distributing full package... if it's not, user has bigger problems
            'dllPath = String.Concat(AppPath, "Bin", Path.DirectorySeparatorChar, "MediaInfo.DLL")
            'Dim fVersion As FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(dllPath)

            ''ISO Handling -> Use MediaInfo-Rar(if exists) to scan RAR and ISO files!
            'Dim mediainfoRaRPath As String = String.Concat(Functions.AppPath, "Bin", Path.DirectorySeparatorChar, "mediainfo-rar\mediainfo-rar.exe")
            'If File.Exists(mediainfoRaRPath) OrElse (fVersion.FileMinorPart <= 7 AndAlso fVersion.FileBuildPart <= 11) Then
            '    Master.CanScanDiscImage = True
            'Else
            '    Master.CanScanDiscImage = False
            'End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & dllPath & ">", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Run console commands from Ember (used for calling mediainfo-rar.exe for scanning ISO files)
    ''' </summary>
    ''' <param name="Process_Name">The name i.e "vcmount.exe" of the process</param>
    ''' <param name="Process_Arguments">Optional arguments, i.e "/u"</param>
    ''' <param name="Read_Output">If <c>True</c>, returns console outputs like mediainfo-rar.exe scanresults </param>
    ''' <param name="Process_Hide">If <c>True</c>, hide console window </param>
    ''' <param name="Process_TimeOut">Timeout value - closes after specific time frame</param>
    Public Shared Function Run_Process(ByVal Process_Name As String, Optional Process_Arguments As String = Nothing, Optional Read_Output As Boolean = False, Optional Process_Hide As Boolean = False, Optional Process_TimeOut As Integer = 30000) As String

        Dim OutputString As String = String.Empty

        Try
            Using My_Process As New Process()
                AddHandler My_Process.OutputDataReceived, Sub(sender As Object, LineOut As DataReceivedEventArgs)
                                                              OutputString = OutputString & LineOut.Data ' & vbCrLf
                                                          End Sub

                Dim My_Process_Info As New ProcessStartInfo()
                My_Process_Info.FileName = Process_Name ' Process filename
                My_Process_Info.Arguments = Process_Arguments ' Process arguments
                My_Process_Info.CreateNoWindow = Process_Hide ' Show or hide the process Window
                My_Process_Info.UseShellExecute = False ' Don't use system shell to execute the process
                My_Process_Info.RedirectStandardOutput = Read_Output '  Redirect Output
                My_Process_Info.RedirectStandardError = Read_Output ' Redirect non Output
                My_Process.EnableRaisingEvents = True ' Raise events
                My_Process.StartInfo = My_Process_Info

                My_Process.Start() ' Run the process NOW

                If Read_Output = True Then
                    System.Threading.Thread.Sleep(500)
                    My_Process.BeginOutputReadLine()
                    System.Threading.Thread.Sleep(1000)
                    Do
                        'TODO?!
                    Loop Until My_Process.HasExited
                End If
                '    RemoveHandler My_Process.OutputDataReceived, AddressOf proc_DataReceived
                My_Process.WaitForExit(Process_TimeOut) ' Wait X ms to kill the process (Default value is 30000 ms)
                My_Process.Close()
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & Process_Name & ">", ex)
        End Try

        Return OutputString
    End Function
#End Region 'Methods

End Class 'Functions

Public Class Structures

#Region "Nested Types"

    Public Structure CustomUpdaterStruct
        Dim Canceled As Boolean
        Dim Options As ScrapeOptions_Movie
        Dim ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV
    End Structure
    ''' <summary>
    ''' Structure representing a movie source path and its metadata
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure MovieSource
        Dim id As String
        Dim Name As String
        Dim Path As String
        Dim Recursive As Boolean
        Dim UseFolderName As Boolean
        Dim IsSingle As Boolean
        Dim Exclude As Boolean
        Dim GetYear As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing a TV source path and its metadata
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure TVSource
        Dim id As String
        Dim Name As String
        Dim Path As String
        Dim Language As String
        Dim Ordering As Enums.Ordering
        Dim Exclude As Boolean
        Dim EpisodeSorting As Enums.EpisodeSorting
    End Structure
    ''' <summary>
    ''' Structure representing a movie in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBMovie
        Dim BannerPath As String
        Dim ClearArtPath As String
        Dim ClearLogoPath As String
        Dim DVDProfilerCaseType As String
        Dim DVDProfilerLocation As String
        Dim DVDProfilerMediaType As String
        Dim DVDProfilerSlot As String
        Dim DVDProfilerTitle As String
        Dim DateAdded As Long
        Dim DateModified As Long
        Dim DiscArtPath As String
        Dim EFanartsPath As String
        Dim efList As List(Of String)
        Dim etList As List(Of String)
        Dim EThumbsPath As String
        Dim FanartPath As String
        Dim VideoSource As String
        Dim Filename As String
        Dim ID As Long
        Dim IsLock As Boolean
        Dim IsMark As Boolean
        Dim IsMarkCustom1 As Boolean
        Dim IsMarkCustom2 As Boolean
        Dim IsMarkCustom3 As Boolean
        Dim IsMarkCustom4 As Boolean
        Dim IsOnline As Boolean
        Dim IsSingle As Boolean
        Dim LandscapePath As String
        Dim ListTitle As String
        Dim Movie As MediaContainers.Movie
        Dim NeedsSave As Boolean
        Dim NfoPath As String
        Dim OfflineHolderFoldername As String
        Dim OriginalTitle As String
        Dim OutOfTolerance As Boolean
        Dim PosterPath As String
        Dim RemoveActorThumbs As Boolean
        Dim RemoveBanner As Boolean
        Dim RemoveClearArt As Boolean
        Dim RemoveClearLogo As Boolean
        Dim RemoveDiscArt As Boolean
        Dim RemoveEFanarts As Boolean
        Dim RemoveEThumbs As Boolean
        Dim RemoveFanart As Boolean
        Dim RemoveLandscape As Boolean
        Dim RemovePoster As Boolean
        Dim RemoveTheme As Boolean
        Dim RemoveTrailer As Boolean
        Dim Source As String
        Dim SubPath As String
        Dim Subtitles As List(Of MediaInfo.Subtitle)
        Dim ThemePath As String
        Dim TrailerPath As String
        Dim UseFolder As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing a movieset in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBMovieSet
        Dim BannerPath As String
        Dim ClearArtPath As String
        Dim ClearLogoPath As String
        Dim DiscArtPath As String
        Dim FanartPath As String
        Dim ID As Long
        Dim IsLock As Boolean
        Dim IsMark As Boolean
        Dim RemoveBanner As Boolean
        Dim RemoveClearArt As Boolean
        Dim RemoveClearLogo As Boolean
        Dim RemoveDiscArt As Boolean
        Dim RemoveFanart As Boolean
        Dim RemoveLandscape As Boolean
        Dim RemovePoster As Boolean
        Dim ListTitle As String
        Dim LandscapePath As String
        Dim NfoPath As String
        Dim PosterPath As String
        Dim MovieSet As MediaContainers.MovieSet
        Dim Movies As List(Of Structures.DBMovie)
        Dim SortMethod As Enums.SortMethod_MovieSet
    End Structure

    ''' <summary>
    ''' Structure representing a tag in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBMovieTag
        Dim ID As Integer
        Dim Title As String
        Dim Movies As List(Of Structures.DBMovie)
    End Structure

    ''' <summary>
    ''' Structure representing a TV show in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBTV
        Dim ClearShowEFanarts As Boolean
        Dim DateAdded As Double
        Dim efList As List(Of String)
        Dim EpFanartPath As String
        Dim EpID As Long
        Dim EpisodeSorting As Enums.EpisodeSorting
        Dim EpNeedsSave As Boolean
        Dim EpNfoPath As String
        Dim EpPosterPath As String
        Dim EpSubtitles As List(Of MediaInfo.Subtitle)
        Dim Filename As String
        Dim FilenameID As Long
        Dim IsLockEp As Boolean
        Dim IsLockSeason As Boolean
        Dim IsLockShow As Boolean
        Dim IsMarkEp As Boolean
        Dim IsMarkSeason As Boolean
        Dim IsMarkShow As Boolean
        Dim IsOnlineEp As Boolean
        Dim isOnlineShow As Boolean
        Dim ListTitle As String
        Dim Ordering As Enums.Ordering
        Dim RemoveActorThumbs As Boolean
        Dim SeasonBannerPath As String
        Dim SeasonFanartPath As String
        Dim SeasonID As Long
        Dim SeasonLandscapePath As String
        Dim SeasonPosterPath As String
        Dim SeasonTVDB As String
        Dim ShowBannerPath As String
        Dim ShowCharacterArtPath As String
        Dim ShowClearArtPath As String
        Dim ShowClearLogoPath As String
        Dim ShowEFanartsPath As String
        Dim ShowFanartPath As String
        Dim ShowID As Long
        Dim ShowLandscapePath As String
        Dim ShowLanguage As String
        Dim ShowNeedsSave As Boolean
        Dim ShowNfoPath As String
        Dim ShowPath As String
        Dim ShowPosterPath As String
        Dim ShowThemePath As String
        Dim Source As String
        Dim TVEp As MediaContainers.EpisodeDetails
        Dim TVShow As MediaContainers.TVShow
        Dim VideoSource As String
    End Structure

    Public Structure Scans
        Dim Movies As Boolean
        Dim MovieSets As Boolean
        Dim SpecificFolder As Boolean
        Dim TV As Boolean
    End Structure

    Public Structure ScrapeInfo
        Dim Aired As String
        Dim CurrentImage As Images
        Dim Ordering As Enums.Ordering
        Dim iEpisode As Integer
        Dim ImageType As Enums.ImageType_TV
        Dim iSeason As Integer
        Dim Options As Structures.TVScrapeOptions
        Dim ShowLang As String
        Dim SourceLang As String
        Dim ShowID As Integer
        Dim ShowTitle As String
        Dim TVDBID As String
        Dim WithCurrent As Boolean
        Dim ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV
    End Structure

    Public Structure ScrapeModifier_Movie_MovieSet
        Dim DoSearch As Boolean
        Dim EThumbs As Boolean
        Dim EFanarts As Boolean
        Dim Fanart As Boolean
        Dim Meta As Boolean
        Dim NFO As Boolean
        Dim Poster As Boolean
        Dim Trailer As Boolean
        Dim ActorThumbs As Boolean
        Dim Banner As Boolean
        Dim CharacterArt As Boolean
        Dim ClearArt As Boolean
        Dim ClearLogo As Boolean
        Dim DiscArt As Boolean
        Dim Landscape As Boolean
        Dim Theme As Boolean
    End Structure

    Public Structure ScrapeModifier_TV
        Dim AllSeasonsBanner As Boolean
        Dim AllSeasonsFanart As Boolean
        Dim AllSeasonsLandscape As Boolean
        Dim AllSeasonsPoster As Boolean
        Dim DoSearch As Boolean
        Dim EpisodeFanart As Boolean
        Dim EpisodePoster As Boolean
        Dim Meta As Boolean
        Dim NFO As Boolean
        Dim SeasonBanner As Boolean
        Dim SeasonFanart As Boolean
        Dim SeasonLandscape As Boolean
        Dim SeasonPoster As Boolean
        Dim ShowActorThumbs As Boolean
        Dim ShowBanner As Boolean
        Dim ShowCharacterArt As Boolean
        Dim ShowClearArt As Boolean
        Dim ShowClearLogo As Boolean
        Dim ShowEFanarts As Boolean
        Dim ShowFanart As Boolean
        Dim ShowLandscape As Boolean
        Dim ShowPoster As Boolean
        Dim ShowTheme As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing posible scrape fields for movies
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Structure ScrapeOptions_Movie
        Dim bCast As Boolean
        Dim bCert As Boolean
        Dim bCollectionID As Boolean
        Dim bDirector As Boolean
        Dim bFullCrew As Boolean
        Dim bGenre As Boolean
        Dim bMPAA As Boolean
        Dim bMusicBy As Boolean
        Dim bOriginalTitle As Boolean
        Dim bOtherCrew As Boolean
        Dim bOutline As Boolean
        Dim bPlot As Boolean
        Dim bProducers As Boolean
        Dim bRating As Boolean
        Dim bRelease As Boolean
        Dim bRuntime As Boolean
        Dim bStatus As Boolean
        Dim bStudio As Boolean
        Dim bTagline As Boolean
        Dim bTitle As Boolean
        Dim bTop250 As Boolean
        Dim bCountry As Boolean
        Dim bTags As Boolean
        Dim bTrailer As Boolean
        Dim bVotes As Boolean
        Dim bWriters As Boolean
        Dim bYear As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing posible scrape fields for moviesets
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Structure ScrapeOptions_MovieSet
        Dim bPlot As Boolean
        Dim bTitle As Boolean
    End Structure

    Public Structure SettingsResult
        Dim DidCancel As Boolean
        Dim NeedsRefresh_Movie As Boolean
        Dim NeedsRefresh_MovieSet As Boolean
        Dim NeedsRefresh_TV As Boolean
        Dim NeedsUpdate As Boolean
        Dim NeedsRestart As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing possible scrape options for TV shows
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure TVScrapeOptions
        Dim bEpActors As Boolean
        Dim bEpAired As Boolean
        Dim bEpCredits As Boolean
        Dim bEpDirector As Boolean
        Dim bEpEpisode As Boolean
        Dim bEpGuestStars As Boolean
        Dim bEpPlot As Boolean
        Dim bEpRating As Boolean
        Dim bEpRuntime As Boolean
        Dim bEpSeason As Boolean
        Dim bEpTitle As Boolean
        Dim bEpVotes As Boolean
        Dim bShowActors As Boolean
        Dim bShowEpisodeGuide As Boolean
        Dim bShowGenre As Boolean
        Dim bShowMPAA As Boolean
        Dim bShowPlot As Boolean
        Dim bShowPremiered As Boolean
        Dim bShowRating As Boolean
        Dim bShowRuntime As Boolean
        Dim bShowStatus As Boolean
        Dim bShowStudio As Boolean
        Dim bShowTitle As Boolean
        Dim bShowVotes As Boolean
    End Structure

    Public Structure ModulesMenus
        Dim ForMovies As Boolean
        Dim ForMovieSets As Boolean
        Dim ForTVShows As Boolean
        Dim IfTabMovies As Boolean      'Only if Movies Tab is selected
        Dim IfTabMovieSets As Boolean   'Only if MovieSets Tab is selected
        Dim IfTabTVShows As Boolean     'Only if TV Shows Tab is selected
        Dim IfNoMovies As Boolean       'Show also if the Movies list is empty
        Dim IfNoMovieSets As Boolean    'Show also if the MovieSets list is empty
        Dim IfNoTVShows As Boolean      'Show also if the TV Shows list is empty
    End Structure

    Public Structure MainTabType
        Dim ContentName As String
        Dim ContentType As Enums.Content_Type
        Dim DefaultList As String
    End Structure

#End Region 'Nested Types

End Class 'Structures