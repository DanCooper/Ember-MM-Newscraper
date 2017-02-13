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
Imports NLog

Public Class Containers

#Region "Nested Types"


    <XmlType(AnonymousType:=True),
     XmlRoot([Namespace]:="", IsNullable:=False, ElementName:="CommandFile")>
    Public Class InstallCommands
        '''<remarks/>

#Region "Fields"

        Private transactionField As New List(Of CommandsTransaction)

        Private noTransactionField As New List(Of CommandsNoTransactionCommand)

#End Region 'Fields

#Region "Properties"

        '''<remarks/>
        <XmlElement("transaction")>
        Public Property transaction() As List(Of CommandsTransaction)
            Get
                Return transactionField
            End Get
            Set(value As List(Of CommandsTransaction))
                transactionField = value
            End Set
        End Property

        '''<remarks/>
        <XmlElement("noTransaction")>
        Public Property noTransaction() As List(Of CommandsNoTransactionCommand)
            Get
                Return noTransactionField
            End Get
            Set(value As List(Of CommandsNoTransactionCommand))
                noTransactionField = value
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
    <XmlType(AnonymousType:=True)>
    Partial Public Class CommandsTransaction

        Private commandField As New List(Of CommandsTransactionCommand)

        Private nameField As String

        '''<remarks/>
        <XmlElement("command")>
        Public Property command() As List(Of CommandsTransactionCommand)
            Get
                Return commandField
            End Get
            Set(value As List(Of CommandsTransactionCommand))
                commandField = value
            End Set
        End Property

        '''<remarks/>
        <XmlAttribute()>
        Public Property name() As String
            Get
                Return nameField
            End Get
            Set(value As String)
                nameField = value
            End Set
        End Property
    End Class 'CommandsTransaction

    '''<remarks/>
    <XmlType(AnonymousType:=True)>
    Partial Public Class CommandsTransactionCommand

        Private descriptionField As String

        Private executeField As String

        Private typeField As String

        '''<remarks/>
        Public Property description() As String
            Get
                Return descriptionField
            End Get
            Set(value As String)
                descriptionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property execute() As String
            Get
                Return executeField
            End Get
            Set(value As String)
                executeField = value
            End Set
        End Property

        '''<remarks/>
        <XmlAttribute()>
        Public Property type() As String
            Get
                Return typeField
            End Get
            Set(value As String)
                typeField = value
            End Set
        End Property

    End Class 'CommandsTransactionCommand

    '''<remarks/>
    <XmlType(AnonymousType:=True)>
    Partial Public Class CommandsNoTransactionCommand

        Private descriptionField As String

        Private executeField As String

        Private typeField As String

        '''<remarks/>
        Public Property description() As String
            Get
                Return descriptionField
            End Get
            Set(value As String)
                descriptionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property execute() As String
            Get
                Return executeField
            End Get
            Set(value As String)
                executeField = value
            End Set
        End Property

        '''<remarks/>
        <XmlAttribute()>
        Public Property type() As String
            Get
                Return typeField
            End Get
            Set(value As String)
                typeField = value
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
            Clear()
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
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ImageIndex() As Integer
            Get
                Return _imageindex
            End Get
            Set(ByVal value As Integer)
                _imageindex = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property Image() As Image
            Get
                Return _image
            End Get
            Set(ByVal value As Image)
                _image = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal value As Integer)
                _order = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property Panel() As Panel
            Get
                Return _panel
            End Get
            Set(ByVal value As Panel)
                _panel = value
            End Set
        End Property

        Public Property Parent() As String
            Get
                Return _parent
            End Get
            Set(ByVal value As String)
                _parent = value
            End Set
        End Property

        Public Property Prefix() As String
            Get
                Return _prefix
            End Get
            Set(ByVal value As String)
                _prefix = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _imageindex = 0
            _image = Nothing
            _name = String.Empty
            _order = 0
            _panel = New Panel
            _parent = String.Empty
            _prefix = String.Empty
            _text = String.Empty
            _type = String.Empty
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
        Private _files As SortedList(Of String, String)
        Private _deletefiles As List(Of String)
#End Region 'Fields

#Region "Properties"
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property Author() As String
            Get
                Return _author
            End Get
            Set(ByVal value As String)
                _author = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Public Property Category() As String
            Get
                Return _category
            End Get
            Set(ByVal value As String)
                _category = value
            End Set
        End Property

        Public Property Version() As Single
            Get
                Return _version
            End Get
            Set(ByVal value As Single)
                _version = value
            End Set
        End Property

        Public Property MinEVersion() As Single
            Get
                Return _mineversion
            End Get
            Set(ByVal value As Single)
                _mineversion = value
            End Set
        End Property

        Public Property MaxEVersion() As Single
            Get
                Return _maxeversion
            End Get
            Set(ByVal value As Single)
                _maxeversion = value
            End Set
        End Property

        Public Property ScreenShotPath() As String
            Get
                Return _screenshotpath
            End Get
            Set(ByVal value As String)
                _screenshotpath = value
            End Set
        End Property

        Public Property ScreenShotImage() As Image
            Get
                Return _screenshotimage
            End Get
            Set(ByVal value As Image)
                _screenshotimage = value
            End Set
        End Property

        Public Property Files() As SortedList(Of String, String)
            Get
                Return _files
            End Get
            Set(ByVal value As SortedList(Of String, String))
                _files = value
            End Set
        End Property

        Public Property DeleteFiles() As List(Of String)
            Get
                Return _deletefiles
            End Get
            Set(ByVal value As List(Of String))
                _deletefiles = value
            End Set
        End Property
#End Region 'Properties

#Region "Constructors"
        Public Sub New()
            Clear()
        End Sub
#End Region 'Constructors

#Region "Methods"
        Public Sub Clear()
            _id = -1
            _name = String.Empty
            _author = String.Empty
            _description = String.Empty
            _category = String.Empty
            _version = -1
            _mineversion = -1
            _maxeversion = -1
            _screenshotpath = String.Empty
            _screenshotimage = Nothing
            _files = New SortedList(Of String, String)
            _deletefiles = New List(Of String)
        End Sub
#End Region 'Methods

    End Class 'Addon

#End Region 'Nested Types

End Class 'Containers

Public Class Enums

#Region "Enumerations"

    Public Enum SortMethod_MovieSet As Integer
        Year = 0    'default in Kodi, so have to be on the first position of enumeration
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
        ctime = 1
        mtime = 2
        Newer = 3
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

    Public Enum ContentType As Integer
        None = 0
        Generic = 1
        Movie = 2
        MovieSet = 3
        Music = 4
        TV = 5
        TVEpisode = 6
        TVSeason = 7
        TVShow = 8
    End Enum

    Public Enum ModifierType As Integer
        All = 0
        AllSeasonsBanner = 1
        AllSeasonsFanart = 2
        AllSeasonsLandscape = 3
        AllSeasonsPoster = 4
        DoSearch = 5
        EpisodeActorThumbs = 6
        EpisodeFanart = 7
        EpisodePoster = 8
        EpisodeMeta = 9
        EpisodeNFO = 10
        EpisodeSubtitle = 11
        EpisodeWatchedFile = 12
        MainActorThumbs = 13
        MainBanner = 14
        MainCharacterArt = 15
        MainClearArt = 16
        MainClearLogo = 17
        MainDiscArt = 18
        MainExtrafanarts = 19
        MainExtrathumbs = 20
        MainFanart = 21
        MainLandscape = 22
        MainMeta = 23
        MainNFO = 24
        MainPoster = 25
        MainSubtitle = 26
        MainTheme = 27
        MainTrailer = 28
        MainWatchedFile = 29
        SeasonBanner = 30
        SeasonFanart = 31
        SeasonLandscape = 32
        SeasonNFO = 33
        SeasonPoster = 34
        withEpisodes = 35
        withSeasons = 36
    End Enum

    Public Enum ModuleEventType As Integer
        ''' <summary>
        ''' Called after edit movie
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_Movie = 0
        ''' <summary>
        ''' Called after edit movieset
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_MovieSet = 1
        ''' <summary>
        ''' Called after edit episode
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVEpisode = 2
        ''' <summary>
        ''' Called after edit season
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVSeason = 3
        ''' <summary>
        ''' Called after edit show
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVShow = 4
        ''' <summary>
        ''' Called after update DB process
        ''' </summary>
        ''' <remarks></remarks>
        AfterUpdateDB_TV = 5
        ''' <summary>
        ''' Called after update DB process
        ''' </summary>
        ''' <remarks></remarks>
        AfterUpdateDB_Movie = 6
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_Movie = 7
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_MovieSet = 8
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_TVEpisode = 9
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_TVSeason = 10
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_TVShow = 11
        ''' <summary>
        ''' Command Line Module Call
        ''' </summary>
        ''' <remarks></remarks>
        CommandLine = 12
        FrameExtrator_Movie = 13
        FrameExtrator_TVEpisode = 14
        Generic = 15
        MediaPlayer_Audio = 16
        MediaPlayer_Video = 17
        MediaPlayerPlay_Audio = 18
        MediaPlayerPlay_Video = 19
        MediaPlayerPlaylistAdd_Audio = 20
        MediaPlayerPlaylistAdd_Video = 21
        MediaPlayerPlaylistClear_Audio = 22
        MediaPlayerPlaylistClear_Video = 23
        MediaPlayerStop_Audio = 24
        MediaPlayerStop_Video = 25
        Notification = 26
        OnBannerSave_Movie = 24
        OnClearArtSave_Movie = 28
        OnClearLogoSave_Movie = 29
        OnDiscArtSave_Movie = 30
        OnFanartDelete_Movie = 31
        OnFanartSave_Movie = 32
        OnLandscapeSave_Movie = 33
        OnNFORead_TVShow = 34
        OnNFOSave_Movie = 35
        OnNFOSave_TVShow = 36
        OnPosterDelete_Movie = 37
        OnPosterSave_Movie = 38
        OnThemeSave_Movie = 39
        OnTrailerSave_Movie = 40
        RandomFrameExtrator = 41
        Remove_Movie = 42
        Remove_MovieSet = 43
        Remove_TVEpisode = 44
        Remove_TVSeason = 45
        Remove_TVShow = 46
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_Movie = 47
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVEpisode = 48
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_Movie = 49
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVEpisode = 50
        ShowMovie = 51
        ShowTVShow = 52
        SyncModuleSettings = 53
        Sync_Movie = 54
        Sync_MovieSet = 55
        Sync_TVEpisode = 56
        Sync_TVSeason = 57
        Sync_TVShow = 58
        Task = 59
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVShow = 60
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVShow = 61
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVSeason = 62
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVSeason = 63
        DuringUpdateDB_TV = 64
    End Enum
    ''' <summary>
    ''' Enum representing valid TV series ordering.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EpisodeOrdering As Integer
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

    Public Enum MovieBannerSize As Integer
        HD185 = 0       'Fanart.tv has only 1000x185
        Any = 99
    End Enum

    Public Enum MovieFanartSize As Integer
        UHD2160 = 0
        QHD1440 = 1
        HD1080 = 2
        HD720 = 3
        Thumb = 4
        Any = 99
    End Enum

    Public Enum MoviePosterSize As Integer
        HD3000 = 0
        HD2100 = 1
        HD1500 = 2
        HD1426 = 3
        Any = 99
    End Enum

    Public Enum ScannerEventType As Integer
        None = 0
        Added_Movie = 1
        Added_TVEpisode = 2
        Added_TVShow = 3
        CleaningDatabase = 4
        PreliminaryTasks = 5
        Refresh_TVShow = 6
    End Enum

    Public Enum ScraperEventType As Integer
        BannerItem = 0
        CharacterArtItem = 1
        ClearArtItem = 2
        ClearLogoItem = 3
        DiscArtItem = 4
        ExtrafanartsItem = 5
        ExtrathumbsItem = 6
        FanartItem = 7
        LandscapeItem = 8
        NFOItem = 9
        PosterItem = 10
        ThemeItem = 11
        TrailerItem = 12
    End Enum
    ''' <summary>
    ''' Enum representing which Movies/TVShows should be scraped,
    ''' and whether results should be automatically chosen or asked of the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScrapeType As Integer
        AllAuto = 0
        AllAsk = 1
        AllSkip = 2
        MissingAuto = 3
        MissingAsk = 4
        MissingSkip = 5
        NewAuto = 6
        NewAsk = 7
        NewSkip = 8
        MarkedAuto = 9
        MarkedAsk = 10
        MarkedSkip = 11
        FilterAuto = 12
        FilterAsk = 13
        FilterSkip = 14
        SingleScrape = 15
        SingleField = 16
        SingleAuto = 17
        SelectedAuto = 18
        SelectedAsk = 19
        SelectedSkip = 20
        None = 99
    End Enum

    Public Enum TaskManagerEventType As Integer
        RefreshRow = 0
        SimpleMessage = 1
    End Enum

    Public Enum TaskManagerType As Integer
        CopyBackdrops = 0
        Reload = 1
        SetWatchedState = 2
    End Enum

    Public Enum TVBannerSize As Integer
        HD185 = 0       'Fanart.tv only 1000x185 (season and tv show banner)
        HD140 = 1       'TVDB has only 758x140 (season and tv show banner)
        Any = 99
    End Enum

    Public Enum TVBannerType As Integer
        Blank = 0       'will leave the title and show logo off the banner
        Graphical = 1   'will show the series name in the show's official font or will display the actual logo for the show
        Text = 2        'will show the series name as plain text in an Arial font
        Any = 99
    End Enum

    Public Enum TVFanartSize As Integer
        UHD2160 = 0
        QHD1440 = 1
        HD1080 = 2      'Fanart.tv has only 1920x1080
        HD720 = 3      'TVDB has 1280x720 and 1920x1080
        Any = 99
    End Enum

    Public Enum TVPosterSize As Integer
        HD3000 = 0
        HD1500 = 1
        HD1426 = 2      'Fanart.tv has only 1000x1426
        HD1000 = 3      'TVDB has only 680x1000
        Any = 99
    End Enum

    Public Enum TVEpisodePosterSize As Integer
        UHD2160 = 0
        HD1080 = 1
        HD720 = 2
        SD225 = 3      'TVDB has only 400 x 300 (400x225 for 16:9 images)
        Any = 99
    End Enum

    Public Enum TVSeasonPosterSize As Integer
        HD1500 = 0
        HD1426 = 1
        HD578 = 2
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer codec options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerAudioCodec As Integer
        MP4 = 0
        WebM = 1
        UNKNOWN = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer quality options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerAudioQuality As Integer
        AAC256kbps = 0
        AAC128kbps = 1
        AAC48kbps = 2
        Vorbis192kbps = 3
        Vorbis128kbps = 4
        UNKNOWN = 5
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer type options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerType As Integer
        Clip = 0
        Featurette = 1
        Teaser = 2
        Trailer = 3
        Any = 99
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
        HD2160p = 0
        HD2160p60fps = 1
        HD1440p = 2
        HD1080p = 3
        HD1080p60fps = 4
        HD720p = 5
        HD720p60fps = 6
        HQ480p = 7 'or 576 for 4:3 media
        SQ360p = 8
        SQ240p = 9 'or 270
        SQ144p = 10
        SQ144p15fps = 11
        UNKNOWN = 12
        Any = 99
    End Enum

#End Region 'Enumerations

End Class 'Enums

Public Class Functions

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

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

    Public Shared Function ConvertToProperDateTime(ByVal strDateTime As String) As String
        If String.IsNullOrEmpty(strDateTime) Then Return String.Empty

        Dim parsedDateTime As DateTime
        If DateTime.TryParse(strDateTime, parsedDateTime) Then
            Return parsedDateTime.ToString("yyyy-MM-dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Create a collection of default Movie and TV scrape options
    ''' based off the currently selected options. 
    ''' These default options are Master.DefaultOptions and Master.DefaultTVOptions
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub CreateDefaultOptions()
        'TODO need proper unit test
        With Master.DefaultOptions_Movie
            .bMainActors = Master.eSettings.MovieScraperCast
            .bMainCertifications = Master.eSettings.MovieScraperCert
            .bMainCollectionID = Master.eSettings.MovieScraperCollectionID
            .bMainCountries = Master.eSettings.MovieScraperCountry
            .bMainDirectors = Master.eSettings.MovieScraperDirector
            .bMainGenres = Master.eSettings.MovieScraperGenre
            .bMainMPAA = Master.eSettings.MovieScraperMPAA
            .bMainOriginalTitle = Master.eSettings.MovieScraperOriginalTitle
            .bMainOutline = Master.eSettings.MovieScraperOutline
            .bMainPlot = Master.eSettings.MovieScraperPlot
            .bMainRating = Master.eSettings.MovieScraperRating
            .bMainRelease = Master.eSettings.MovieScraperRelease
            .bMainRuntime = Master.eSettings.MovieScraperRuntime
            .bMainStudios = Master.eSettings.MovieScraperStudio
            .bMainTagline = Master.eSettings.MovieScraperTagline
            .bMainTitle = Master.eSettings.MovieScraperTitle
            .bMainTop250 = Master.eSettings.MovieScraperTop250
            .bMainTrailer = Master.eSettings.MovieScraperTrailer
            .bMainWriters = Master.eSettings.MovieScraperCredits
            .bMainYear = Master.eSettings.MovieScraperYear
        End With

        With Master.DefaultOptions_MovieSet
            .bMainPlot = Master.eSettings.MovieSetScraperPlot
            .bMainTitle = Master.eSettings.MovieSetScraperTitle
        End With

        With Master.DefaultOptions_TV
            .bEpisodeActors = Master.eSettings.TVScraperEpisodeActors
            .bEpisodeAired = Master.eSettings.TVScraperEpisodeAired
            .bEpisodeCredits = Master.eSettings.TVScraperEpisodeCredits
            .bEpisodeDirectors = Master.eSettings.TVScraperEpisodeDirector
            .bEpisodeGuestStars = Master.eSettings.TVScraperEpisodeGuestStars
            .bEpisodePlot = Master.eSettings.TVScraperEpisodePlot
            .bEpisodeRating = Master.eSettings.TVScraperEpisodeRating
            .bEpisodeRuntime = Master.eSettings.TVScraperEpisodeRuntime
            .bEpisodeTitle = Master.eSettings.TVScraperEpisodeTitle
            .bMainActors = Master.eSettings.TVScraperShowActors
            .bMainCertifications = Master.eSettings.TVScraperShowCert
            .bMainCountries = Master.eSettings.TVScraperShowCountry
            .bMainCreators = Master.eSettings.TVScraperShowCreators
            .bMainEpisodeGuide = Master.eSettings.TVScraperShowEpiGuideURL
            .bMainGenres = Master.eSettings.TVScraperShowGenre
            .bMainMPAA = Master.eSettings.TVScraperShowMPAA
            .bMainOriginalTitle = Master.eSettings.TVScraperShowOriginalTitle
            .bMainPlot = Master.eSettings.TVScraperShowPlot
            .bMainPremiered = Master.eSettings.TVScraperShowPremiered
            .bMainRating = Master.eSettings.TVScraperShowRating
            .bMainRuntime = Master.eSettings.TVScraperShowRuntime
            .bMainStatus = Master.eSettings.TVScraperShowStatus
            .bMainStudios = Master.eSettings.TVScraperShowStudio
            .bMainTitle = Master.eSettings.TVScraperShowTitle
            .bSeasonAired = Master.eSettings.TVScraperSeasonAired
            .bSeasonPlot = Master.eSettings.TVScraperSeasonPlot
            .bSeasonTitle = Master.eSettings.TVScraperSeasonTitle
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
    Public Shared Function GetExtrafanartsModifier(ByVal sPath As String) As Integer
        Dim iMod As Integer = 0
        Dim lThumbs As New List(Of String)

        Try
            If Directory.Exists(sPath) Then

                Try
                    lThumbs.AddRange(Directory.GetFiles(sPath, "extrafanart*.jpg"))
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Failed trying to identify last Extrafanart from path: " & sPath)
        End Try

        Return iMod
    End Function

    ''' <summary>
    ''' Get the number of the last sequential extrathumb to make sure we're not overwriting current ones.
    ''' </summary>
    ''' <param name="sPath">Full path to extrathumbs directory</param>
    ''' <returns>Last detected number of the discovered extrathumbs.</returns>
    Public Shared Function GetExtrathumbsModifier(ByVal sPath As String) As Integer
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Failed trying to identify last Extrathumb from path: " & sPath)
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
    ''' Get a path to the FFProbe included with the Ember distribution
    ''' </summary>
    ''' <returns>A path to an instance of FFProbe</returns>
    ''' <remarks>Windows distributions have FFProbe in the Bin subdirectory.
    ''' Note that no validation is done to ensure that FFProbe actually exists.</remarks>
    Public Shared Function GetFFProbe() As String
        If Master.isWindows Then
            Return String.Concat(Functions.AppPath, "Bin", Path.DirectorySeparatorChar, "ffprobe.exe")
        Else
            Return "ffprobe"
        End If
    End Function

    ''' <summary>
    ''' Populate Master.SourcesList with a list of paths to all (media?) sources stored in the database
    ''' </summary>
    Public Shared Sub GetListOfSources()
        Master.SourcesList.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strPath FROM moviesource;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Master.SourcesList.Add(SQLreader("strPath").ToString)
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
                            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & " Failed to determine path for season " & iSeason & " in path: " & ShowPath)
                        End Try
                    Next
                Next
            Next
        End If
        'no matches
        Return String.Empty
    End Function

    Public Shared Function GetWindowsClientVersion() As String
        Dim major As Integer = System.Environment.OSVersion.Version.Major
        Dim minor As Integer = System.Environment.OSVersion.Version.Minor
        Dim build As Integer = System.Environment.OSVersion.Version.Build
        If major = 4 AndAlso minor = 0 AndAlso build = 950 Then
            Return "Win95 Release 1"
        ElseIf major = 4 AndAlso minor = 0 AndAlso build = 1111 Then
            Return "Win95 Release 2"
        ElseIf major = 4 AndAlso minor = 3 AndAlso (build = 1212 OrElse build = 1213 OrElse build = 1214) Then
            Return "Win95 Release 2.1"
        ElseIf major = 4 AndAlso minor = 10 AndAlso build = 1998 Then
            Return "Win98"
        ElseIf major = 4 AndAlso minor = 10 AndAlso build = 2222 Then
            Return "Win98 Second Edition"
        ElseIf major = 4 AndAlso minor = 90 Then
            Return "WinMe"
        ElseIf major = 5 AndAlso minor = 0 Then
            Return "Win2000"
        ElseIf major = 5 AndAlso minor = 1 AndAlso build = 2600 Then
            Return "WinXP"
        ElseIf major = 6 AndAlso minor = 0 Then
            Return "Vista"
        ElseIf major = 6 AndAlso minor = 1 Then
            Return "Win7"
        ElseIf major = 6 AndAlso minor = 2 AndAlso build = 9200 Then
            Return "Win8"
        ElseIf major = 6 AndAlso minor = 2 AndAlso build = 9600 Then
            Return "Win8"
        ElseIf major = 10 AndAlso minor = 0 AndAlso build = 10240 Then
            Return "Win10"
        Else
            Return "Can not find os version."
        End If
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
    ''' Determine the Structures.MovieScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.MovieScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.MovieScrapeOptions</param>
    ''' <returns>Structures.MovieScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function ScrapeOptionsAndAlso(ByVal Options As Structures.ScrapeOptions, ByVal Options2 As Structures.ScrapeOptions) As Structures.ScrapeOptions
        Dim FilteredOptions As New Structures.ScrapeOptions
        FilteredOptions.bEpisodeActors = Options.bEpisodeActors AndAlso Options2.bEpisodeActors
        FilteredOptions.bEpisodeAired = Options.bEpisodeAired AndAlso Options2.bEpisodeAired
        FilteredOptions.bEpisodeCredits = Options.bEpisodeCredits AndAlso Options2.bEpisodeCredits
        FilteredOptions.bEpisodeDirectors = Options.bEpisodeDirectors AndAlso Options2.bEpisodeDirectors
        FilteredOptions.bEpisodeGuestStars = Options.bEpisodeGuestStars AndAlso Options2.bEpisodeGuestStars
        FilteredOptions.bEpisodePlot = Options.bEpisodePlot AndAlso Options2.bEpisodePlot
        FilteredOptions.bEpisodeRating = Options.bEpisodeRating AndAlso Options2.bEpisodeRating
        FilteredOptions.bEpisodeRuntime = Options.bEpisodeRuntime AndAlso Options2.bEpisodeRuntime
        FilteredOptions.bEpisodeTitle = Options.bEpisodeTitle AndAlso Options2.bEpisodeTitle
        FilteredOptions.bMainActors = Options.bMainActors AndAlso Options2.bMainActors
        FilteredOptions.bMainCertifications = Options.bMainCertifications AndAlso Options2.bMainCertifications
        FilteredOptions.bMainCollectionID = Options.bMainCollectionID AndAlso Options2.bMainCollectionID
        FilteredOptions.bMainCountries = Options.bMainCountries AndAlso Options2.bMainCountries
        FilteredOptions.bMainCreators = Options.bMainCreators AndAlso Options2.bMainCreators
        FilteredOptions.bMainDirectors = Options.bMainDirectors AndAlso Options2.bMainDirectors
        FilteredOptions.bMainEpisodeGuide = Options.bMainEpisodeGuide AndAlso Options2.bMainEpisodeGuide
        FilteredOptions.bMainGenres = Options.bMainGenres AndAlso Options2.bMainGenres
        FilteredOptions.bMainMPAA = Options.bMainMPAA AndAlso Options2.bMainMPAA
        FilteredOptions.bMainOriginalTitle = Options.bMainOriginalTitle AndAlso Options2.bMainOriginalTitle
        FilteredOptions.bMainOutline = Options.bMainOutline AndAlso Options2.bMainOutline
        FilteredOptions.bMainPlot = Options.bMainPlot AndAlso Options2.bMainPlot
        FilteredOptions.bMainPremiered = Options.bMainPremiered AndAlso Options2.bMainPremiered
        FilteredOptions.bMainRating = Options.bMainRating AndAlso Options2.bMainRating
        FilteredOptions.bMainRelease = Options.bMainRelease AndAlso Options2.bMainRelease
        FilteredOptions.bMainRuntime = Options.bMainRuntime AndAlso Options2.bMainRuntime
        FilteredOptions.bMainStatus = Options.bMainStatus AndAlso Options2.bMainStatus
        FilteredOptions.bMainStudios = Options.bMainStudios AndAlso Options2.bMainStudios
        FilteredOptions.bMainTagline = Options.bMainTagline AndAlso Options2.bMainTagline
        FilteredOptions.bMainTitle = Options.bMainTitle AndAlso Options2.bMainTitle
        FilteredOptions.bMainTop250 = Options.bMainTop250 AndAlso Options2.bMainTop250
        FilteredOptions.bMainTrailer = Options.bMainTrailer AndAlso Options2.bMainTrailer
        FilteredOptions.bMainWriters = Options.bMainWriters AndAlso Options2.bMainWriters
        FilteredOptions.bMainYear = Options.bMainYear AndAlso Options2.bMainYear
        FilteredOptions.bSeasonAired = Options.bSeasonAired AndAlso Options2.bSeasonAired
        FilteredOptions.bSeasonPlot = Options.bSeasonPlot AndAlso Options2.bSeasonPlot
        FilteredOptions.bSeasonTitle = Options.bSeasonTitle AndAlso Options2.bSeasonTitle
        Return FilteredOptions
    End Function

    Public Shared Function ScrapeModifiersAndAlso(ByVal Options As Structures.ScrapeModifiers, ByVal Options2 As Structures.ScrapeModifiers) As Structures.ScrapeModifiers
        Dim FilteredModifiers As New Structures.ScrapeModifiers
        FilteredModifiers.AllSeasonsBanner = Options.AllSeasonsBanner AndAlso Options2.AllSeasonsBanner
        FilteredModifiers.AllSeasonsFanart = Options.AllSeasonsFanart AndAlso Options2.AllSeasonsFanart
        FilteredModifiers.AllSeasonsLandscape = Options.AllSeasonsLandscape AndAlso Options2.AllSeasonsLandscape
        FilteredModifiers.AllSeasonsPoster = Options.AllSeasonsPoster AndAlso Options2.AllSeasonsPoster
        FilteredModifiers.DoSearch = Options.DoSearch AndAlso Options2.DoSearch
        FilteredModifiers.EpisodeActorThumbs = Options.EpisodeActorThumbs AndAlso Options2.EpisodeActorThumbs
        FilteredModifiers.EpisodeFanart = Options.EpisodeFanart AndAlso Options2.EpisodeFanart
        FilteredModifiers.EpisodeMeta = Options.EpisodeMeta AndAlso Options2.EpisodeMeta
        FilteredModifiers.EpisodeNFO = Options.EpisodeNFO AndAlso Options2.EpisodeNFO
        FilteredModifiers.EpisodePoster = Options.EpisodePoster AndAlso Options2.EpisodePoster
        FilteredModifiers.EpisodeSubtitles = Options.EpisodeSubtitles AndAlso Options2.EpisodeSubtitles
        FilteredModifiers.EpisodeWatchedFile = Options.EpisodeWatchedFile AndAlso Options2.EpisodeWatchedFile
        FilteredModifiers.MainActorthumbs = Options.MainActorthumbs AndAlso Options2.MainActorthumbs
        FilteredModifiers.MainBanner = Options.MainBanner AndAlso Options2.MainBanner
        FilteredModifiers.MainCharacterArt = Options.MainCharacterArt AndAlso Options2.MainCharacterArt
        FilteredModifiers.MainClearArt = Options.MainClearArt AndAlso Options2.MainClearArt
        FilteredModifiers.MainClearLogo = Options.MainClearLogo AndAlso Options2.MainClearLogo
        FilteredModifiers.MainDiscArt = Options.MainDiscArt AndAlso Options2.MainDiscArt
        FilteredModifiers.MainExtrafanarts = Options.MainExtrafanarts AndAlso Options2.MainExtrafanarts
        FilteredModifiers.MainExtrathumbs = Options.MainExtrathumbs AndAlso Options2.MainExtrathumbs
        FilteredModifiers.MainFanart = Options.MainFanart AndAlso Options2.MainFanart
        FilteredModifiers.MainLandscape = Options.MainLandscape AndAlso Options2.MainLandscape
        FilteredModifiers.MainNFO = Options.MainNFO AndAlso Options2.MainNFO
        FilteredModifiers.MainPoster = Options.MainPoster AndAlso Options2.MainPoster
        FilteredModifiers.MainSubtitles = Options.MainSubtitles AndAlso Options2.MainSubtitles
        FilteredModifiers.MainTheme = Options.MainTheme AndAlso Options2.MainTheme
        FilteredModifiers.MainTrailer = Options.MainTrailer AndAlso Options2.MainTrailer
        FilteredModifiers.MainWatchedFile = Options.MainWatchedFile AndAlso Options2.MainWatchedFile
        FilteredModifiers.SeasonBanner = Options.SeasonBanner AndAlso Options2.SeasonBanner
        FilteredModifiers.SeasonFanart = Options.SeasonFanart AndAlso Options2.SeasonFanart
        FilteredModifiers.SeasonLandscape = Options.SeasonLandscape AndAlso Options2.SeasonLandscape
        FilteredModifiers.SeasonNFO = Options.SeasonNFO AndAlso Options2.SeasonNFO
        FilteredModifiers.SeasonPoster = Options.SeasonPoster AndAlso Options2.SeasonPoster
        FilteredModifiers.withEpisodes = Options.withEpisodes AndAlso Options2.withEpisodes
        FilteredModifiers.withSeasons = Options.withSeasons AndAlso Options2.withSeasons
        Return FilteredModifiers
    End Function

    Public Shared Function ScrapeModifiersAnyEnabled(ByVal Options As Structures.ScrapeModifiers) As Boolean
        With Options
            If .AllSeasonsBanner OrElse
                .AllSeasonsFanart OrElse
                .AllSeasonsLandscape OrElse
                .AllSeasonsPoster OrElse
                .EpisodeActorThumbs OrElse
                .EpisodeFanart OrElse
                .EpisodeMeta OrElse
                .EpisodeNFO OrElse
                .EpisodePoster OrElse
                .EpisodeSubtitles OrElse
                .EpisodeWatchedFile OrElse
                .MainActorthumbs OrElse
                .MainBanner OrElse
                .MainCharacterArt OrElse
                .MainClearArt OrElse
                .MainClearLogo OrElse
                .MainDiscArt OrElse
                .MainExtrafanarts OrElse
                .MainExtrathumbs OrElse
                .MainFanart OrElse
                .MainLandscape OrElse
                .MainMeta OrElse
                .MainNFO OrElse
                .MainPoster OrElse
                .MainSubtitles OrElse
                .MainTheme OrElse
                .MainTrailer OrElse
                .MainWatchedFile OrElse
                .SeasonBanner OrElse
                .SeasonFanart OrElse
                .SeasonLandscape OrElse
                .SeasonNFO OrElse
                .SeasonPoster Then
                Return True
            End If
        End With

        Return False
    End Function

    Public Shared Sub SetScrapeModifiers(ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal MType As Enums.ModifierType, ByVal MValue As Boolean)
        With ScrapeModifiers
            Select Case MType
                Case Enums.ModifierType.All
                    .AllSeasonsBanner = MValue
                    .AllSeasonsFanart = MValue
                    .AllSeasonsLandscape = MValue
                    .AllSeasonsPoster = MValue
                    '.DoSearch should not be set here as it is only needed for a re-search of a movie (first scraping or movie change).
                    .EpisodeActorThumbs = MValue
                    .EpisodeFanart = MValue
                    .EpisodeMeta = MValue
                    .EpisodeNFO = MValue
                    .EpisodePoster = MValue
                    .EpisodeSubtitles = MValue
                    .EpisodeWatchedFile = MValue
                    .MainActorthumbs = MValue
                    .MainBanner = MValue
                    .MainCharacterArt = MValue
                    .MainClearArt = MValue
                    .MainClearLogo = MValue
                    .MainDiscArt = MValue
                    .MainExtrafanarts = MValue
                    .MainExtrathumbs = MValue
                    .MainFanart = MValue
                    .MainLandscape = MValue
                    .MainMeta = MValue
                    .MainNFO = MValue
                    .MainPoster = MValue
                    .MainSubtitles = MValue
                    .MainTheme = MValue
                    .MainTrailer = MValue
                    .MainWatchedFile = MValue
                    .SeasonBanner = MValue
                    .SeasonFanart = MValue
                    .SeasonLandscape = MValue
                    .SeasonNFO = MValue
                    .SeasonPoster = MValue
                '.withEpisodes should not be set here
                '.withSeasons should not be set here
                Case Enums.ModifierType.AllSeasonsBanner
                    .AllSeasonsBanner = MValue
                Case Enums.ModifierType.AllSeasonsFanart
                    .AllSeasonsFanart = MValue
                Case Enums.ModifierType.AllSeasonsLandscape
                    .AllSeasonsLandscape = MValue
                Case Enums.ModifierType.AllSeasonsPoster
                    .AllSeasonsPoster = MValue
                Case Enums.ModifierType.DoSearch
                    .DoSearch = MValue
                Case Enums.ModifierType.EpisodeActorThumbs
                    .EpisodeActorThumbs = MValue
                Case Enums.ModifierType.EpisodeFanart
                    .EpisodeFanart = MValue
                Case Enums.ModifierType.EpisodeMeta
                    .EpisodeMeta = MValue
                Case Enums.ModifierType.EpisodeNFO
                    .EpisodeNFO = MValue
                Case Enums.ModifierType.EpisodePoster
                    .EpisodePoster = MValue
                Case Enums.ModifierType.EpisodeSubtitle
                    .EpisodeSubtitles = MValue
                Case Enums.ModifierType.EpisodeWatchedFile
                    .EpisodeWatchedFile = MValue
                Case Enums.ModifierType.MainActorThumbs
                    .MainActorthumbs = MValue
                Case Enums.ModifierType.MainBanner
                    .MainBanner = MValue
                Case Enums.ModifierType.MainCharacterArt
                    .MainCharacterArt = MValue
                Case Enums.ModifierType.MainClearArt
                    .MainClearArt = MValue
                Case Enums.ModifierType.MainClearLogo
                    .MainClearLogo = MValue
                Case Enums.ModifierType.MainDiscArt
                    .MainDiscArt = MValue
                Case Enums.ModifierType.MainExtrafanarts
                    .MainExtrafanarts = MValue
                Case Enums.ModifierType.MainExtrathumbs
                    .MainExtrathumbs = MValue
                Case Enums.ModifierType.MainFanart
                    .MainFanart = MValue
                Case Enums.ModifierType.MainLandscape
                    .MainLandscape = MValue
                Case Enums.ModifierType.MainMeta
                    .MainMeta = MValue
                Case Enums.ModifierType.MainNFO
                    .MainNFO = MValue
                Case Enums.ModifierType.MainPoster
                    .MainPoster = MValue
                Case Enums.ModifierType.MainSubtitle
                    .MainSubtitles = MValue
                Case Enums.ModifierType.MainTheme
                    .MainTheme = MValue
                Case Enums.ModifierType.MainTrailer
                    .MainTrailer = MValue
                Case Enums.ModifierType.MainWatchedFile
                    .MainWatchedFile = MValue
                Case Enums.ModifierType.SeasonBanner
                    .SeasonBanner = MValue
                Case Enums.ModifierType.SeasonFanart
                    .SeasonFanart = MValue
                Case Enums.ModifierType.SeasonLandscape
                    .SeasonLandscape = MValue
                Case Enums.ModifierType.SeasonNFO
                    .SeasonNFO = MValue
                Case Enums.ModifierType.SeasonPoster
                    .SeasonPoster = MValue
                Case Enums.ModifierType.withEpisodes
                    .withEpisodes = MValue
                Case Enums.ModifierType.withSeasons
                    .withSeasons = MValue
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & Destination & ">")
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & dllPath & ">")
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & Process_Name & ">")
        End Try

        Return OutputString
    End Function
#End Region 'Methods

End Class 'Functions

Public Class Structures

#Region "Nested Types"

    Public Structure CustomUpdaterStruct
        Dim Canceled As Boolean
        Dim ScrapeOptions As ScrapeOptions
        Dim ScrapeModifiers As ScrapeModifiers
        Dim ScrapeType As Enums.ScrapeType
    End Structure
    ''' <summary>
    ''' Structure representing a tag in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBMovieTag
        Dim ID As Integer
        Dim Movies As List(Of Database.DBElement)
        Dim Title As String
    End Structure

    Public Structure ScanOrClean
        Dim Movies As Boolean
        Dim MovieSets As Boolean
        Dim SpecificFolder As Boolean
        Dim TV As Boolean
    End Structure

    Public Structure ScrapeModifiers
        Dim AllSeasonsBanner As Boolean
        Dim AllSeasonsFanart As Boolean
        Dim AllSeasonsLandscape As Boolean
        Dim AllSeasonsPoster As Boolean
        Dim DoSearch As Boolean
        Dim EpisodeActorThumbs As Boolean
        Dim EpisodeFanart As Boolean
        Dim EpisodePoster As Boolean
        Dim EpisodeMeta As Boolean
        Dim EpisodeNFO As Boolean
        Dim EpisodeSubtitles As Boolean
        Dim EpisodeWatchedFile As Boolean
        Dim MainActorthumbs As Boolean
        Dim MainBanner As Boolean
        Dim MainCharacterArt As Boolean
        Dim MainClearArt As Boolean
        Dim MainClearLogo As Boolean
        Dim MainDiscArt As Boolean
        Dim MainExtrafanarts As Boolean
        Dim MainExtrathumbs As Boolean
        Dim MainFanart As Boolean
        Dim MainLandscape As Boolean
        Dim MainMeta As Boolean
        Dim MainNFO As Boolean
        Dim MainPoster As Boolean
        Dim MainSubtitles As Boolean
        Dim MainTheme As Boolean
        Dim MainTrailer As Boolean
        Dim MainWatchedFile As Boolean
        Dim SeasonBanner As Boolean
        Dim SeasonFanart As Boolean
        Dim SeasonLandscape As Boolean
        Dim SeasonNFO As Boolean
        Dim SeasonPoster As Boolean
        Dim withEpisodes As Boolean
        Dim withSeasons As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing posible scrape fields
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Structure ScrapeOptions
        Dim bEpisodeActors As Boolean
        Dim bEpisodeAired As Boolean
        Dim bEpisodeCredits As Boolean
        Dim bEpisodeDirectors As Boolean
        Dim bEpisodeGuestStars As Boolean
        Dim bEpisodePlot As Boolean
        Dim bEpisodeRating As Boolean
        Dim bEpisodeRuntime As Boolean
        Dim bEpisodeTitle As Boolean
        Dim bMainActors As Boolean
        Dim bMainCertifications As Boolean
        Dim bMainCollectionID As Boolean
        Dim bMainCreators As Boolean
        Dim bMainDirectors As Boolean
        Dim bMainEpisodeGuide As Boolean
        Dim bMainGenres As Boolean
        Dim bMainMPAA As Boolean
        Dim bMainOriginalTitle As Boolean
        Dim bMainOutline As Boolean
        Dim bMainPlot As Boolean
        Dim bMainPremiered As Boolean
        Dim bMainRating As Boolean
        Dim bMainRelease As Boolean
        Dim bMainRuntime As Boolean
        Dim bMainStatus As Boolean
        Dim bMainStudios As Boolean
        Dim bMainTagline As Boolean
        Dim bMainTitle As Boolean
        Dim bMainTop250 As Boolean
        Dim bMainCountries As Boolean
        Dim bMainTags As Boolean
        Dim bMainTrailer As Boolean
        Dim bMainWriters As Boolean
        Dim bMainYear As Boolean
        Dim bSeasonAired As Boolean
        Dim bSeasonPlot As Boolean
        Dim bSeasonTitle As Boolean
    End Structure

    Public Structure SettingsResult
        Dim DidCancel As Boolean
        Dim NeedsDBClean_Movie As Boolean
        Dim NeedsDBClean_TV As Boolean
        Dim NeedsDBUpdate_Movie As Boolean
        Dim NeedsDBUpdate_TV As Boolean
        Dim NeedsReload_Movie As Boolean
        Dim NeedsReload_MovieSet As Boolean
        Dim NeedsReload_TVEpisode As Boolean
        Dim NeedsReload_TVShow As Boolean
        Dim NeedsRestart As Boolean
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
        Dim ContentType As Enums.ContentType
        Dim DefaultList As String
    End Structure

#End Region 'Nested Types

End Class 'Structures