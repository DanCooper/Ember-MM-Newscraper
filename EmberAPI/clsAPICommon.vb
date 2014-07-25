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

Public Class Globals

#Region "Fields"

    Public backdrop_names(3) As Structures.v3Size
    Public poster_names(5) As Structures.v3Size

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        poster_names(0).description = "thumb"
        poster_names(0).index = Enums.PosterSize.Small
        poster_names(0).size = "w92"
        poster_names(0).width = 92
        poster_names(1).description = "w154"
        poster_names(1).index = -1 'not used in combo box
        poster_names(1).size = "w154"
        poster_names(1).width = 154
        poster_names(2).description = "cover"
        poster_names(2).index = Enums.PosterSize.Mid
        poster_names(2).size = "w185"
        poster_names(2).width = 185
        poster_names(3).description = "w342"
        poster_names(3).index = -1 'not used in combo box
        poster_names(3).size = "w342"
        poster_names(3).width = 342
        poster_names(4).description = "mid"
        poster_names(4).index = Enums.PosterSize.Lrg
        poster_names(4).size = "w500"
        poster_names(4).width = 500
        poster_names(5).description = "original"
        poster_names(5).index = Enums.PosterSize.Xlrg
        poster_names(5).size = "original"
        poster_names(5).width = 0

        backdrop_names(0).description = "thumb"
        backdrop_names(0).index = Enums.PosterSize.Small
        backdrop_names(0).size = "w300"
        backdrop_names(0).width = 300
        backdrop_names(1).description = "poster"
        backdrop_names(1).index = Enums.PosterSize.Mid
        backdrop_names(1).size = "w780"
        backdrop_names(1).width = 780
        backdrop_names(2).description = "w1280"
        backdrop_names(2).index = Enums.PosterSize.Lrg
        backdrop_names(2).size = "w1280"
        backdrop_names(2).width = 1280
        backdrop_names(3).description = "original"
        backdrop_names(3).index = Enums.PosterSize.Xlrg
        backdrop_names(3).size = "original"
        backdrop_names(3).width = 0
    End Sub
#End Region 'Methods

End Class 'Globals

Public Class Enums

#Region "Enumerations"

    Public Enum DefaultType As Integer
        All = 0
        MovieFilters = 1
        ShowFilters = 2
        EpFilters = 3
        ValidExts = 4
        ShowRegex = 5
        TrailerCodec = 6
        ValidThemeExts = 7
    End Enum

    Public Enum DelType As Integer
        Movies = 0
        Shows = 1
        Seasons = 2
        Episodes = 3
    End Enum

    Public Enum FanartSize As Integer
        Xlrg = 0
        Lrg = 1
        Mid = 2
        Small = 3
    End Enum
    ''' <summary>
    ''' Enum representing possible scrape data types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MovieModType As Integer
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
    End Enum
    ''' <summary>
    ''' Enum representing possible scrape data types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TVModType As Integer
        All = 0
        DoSearch = 1
        AllSeasonsBanner = 2
        AllSeasonsFanart = 3
        AllSeasonsLandscape = 4
        AllSeasonsPoster = 5
        EpisodeFanart = 6
        EpisodeMeta = 7
        EpisodeNfo = 8
        EpisodePoster = 9
        SeasonBanner = 10
        SeasonFanart = 11
        SeasonLandscape = 12
        SeasonPoster = 13
        ShowBanner = 14
        ShowCharacterArt = 15
        ShowClearArt = 16
        ShowClearLogo = 17
        ShowEFanarts = 18
        ShowFanart = 19
        ShowLandscape = 20
        ShowNfo = 21
        ShowPoster = 22
        ShowTheme = 23
    End Enum
    ''' <summary>
    ''' Enum representing possible scraper capabilities
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScraperCapabilities
        Poster = 0
        Fanart = 1
        Trailer = 2
        ActorThumbs = 3
        Banner = 4
        CharacterArt = 5
        ClearArt = 6
        ClearLogo = 7
        DiscArt = 8
        Landscape = 9
        Theme = 10
    End Enum

    Public Enum ModuleEventType As Integer
        Generic = 0
        Notification = 1
        MovieScraperRDYtoSave = 2       ' Called when scraper finishs but before save
        RenameMovie = 3                 ' Called when need to rename a Movie ... from several places
        RenameMovieManual = 4           ' Will call only First Register Module (use Master.currMovie)
        MovieFrameExtrator = 5
        TVFrameExtrator = 6
        RandomFrameExtrator = 7
        CommandLine = 8                 ' Command Line Module Call
        MovieSync = 9
        ShowMovie = 10                  ' Called after displaying Movie  (not in place yet)
        ShowTVShow = 11                 ' Called after displaying TVShow (not in place yet)
        BeforeEditMovie = 12            ' Called when Manual editing or reading from nfo
        OnMovieNFOSave = 13
        OnMoviePosterSave = 14
        OnMovieFanartSave = 15
        OnMoviePosterDelete = 16
        OnMovieFanartDelete = 17
        TVImageNaming = 18
        MovieImageNaming = 19
        SyncModuleSettings = 20
        OnTVShowNFOSave = 21
        OnTVShowNFORead = 22
        OnMovieLandscapeSave = 23
        OnMovieBannerSave = 24
        OnMovieClearArtSave = 25
        OnMovieClearLogoSave = 26
        OnMovieDiscArtSave = 27
        OnMovieThemeSave = 28
        OnMovieTrailerSave = 29
    End Enum

    Public Enum MovieScraperEventType As Integer
        NFOItem = 0
        PosterItem = 1
        FanartItem = 2
        TrailerItem = 3
        EThumbsItem = 4
        SortTitle = 5
        ListTitle = 6
        BannerItem = 7
        LandscapeItem = 8
        ThemeItem = 9
        ClearArtItem = 10
        ClearLogoItem = 11
        DiscArtItem = 12
        EFanartsItem = 13
    End Enum

    Public Enum MovieSetScraperEventType As Integer
        NFOItem = 0
        PosterItem = 1
        FanartItem = 2
        SetName = 6
        BannerItem = 7
        LandscapeItem = 8
        ClearArtItem = 10
        ClearLogoItem = 11
        DiscArtItem = 12
    End Enum
    ''' <summary>
    ''' Enum representing valid TV series ordering.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Ordering As Integer
        Standard = 0
        DVD = 1
        Absolute = 2
    End Enum
    ''' <summary>
    ''' Enum represeting valid poster sizes
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PosterSize As Integer
        Xlrg = 0
        Lrg = 1
        Mid = 2
        Small = 3
        Wide = 4
    End Enum
    ''' <summary>
    ''' Enum representing which Movies/TVShows should be scraped,
    ''' and whether results should be automatically chosen or asked of the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScrapeType As Integer
        SingleScrape = 0
        FullAuto = 1
        FullAsk = 2
        FullSkip = 3
        UpdateAuto = 4
        UpdateAsk = 5
        UpdateSkip = 6
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
        None = 99
    End Enum

    Public Enum TVFanartSize As Integer
        HD1080 = 0      'TVDB has only 1280x720 and 1920x1080
        HD720 = 1
    End Enum

    Public Enum TVPosterSize As Integer
        HD1000 = 0      'TVDB has only 680x1000
    End Enum

    Public Enum TVEpisodePosterSize As Integer
        SD225 = 0      'TVDB has only 400 x 300 (400x225 for 16:9 images)
    End Enum

    Public Enum TVSeasonPosterType As Integer
        None = 0
        Poster = 1
        Wide = 2
    End Enum

    Public Enum MovieBannerType As Integer 'TODO: add correct fanart.tv types
        None = 0
        Blank = 1
        Graphical = 2
        Text = 3
    End Enum

    Public Enum TVSeasonBannerType As Integer
        Blank = 0       'will leave the title and show logo off the banner
        Graphical = 1   'will show the series name in the show's official font or will display the actual logo for the show
        Text = 2        'will show the series name as plain text in an Arial font
    End Enum

    Public Enum TVShowBannerType As Integer
        None = 0
        Blank = 1       'will leave the title and show logo off the banner
        Graphical = 2   'will show the series name in the show's official font or will display the actual logo for the show
        Text = 3        'will show the series name as plain text in an Arial font
    End Enum
    ''' <summary>
    ''' Enum representing the trailer codec options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerCodec As Integer
        MP4 = 0
        WebM = 1
        v3GP = 2
        FLV = 3
        OTHERS = 4
    End Enum
    ''' <summary>
    ''' Enum representing the trailer quality options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerQuality As Integer
        All = 0
        HD1080p = 1
        HD720p = 2
        HQ480p = 3
        SQ360p = 4
        SQ240p = 5
        SQ144p = 6
        OTHERS = 7
    End Enum
    ''' <summary>
    ''' Enum represeting valid movie image types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MovieImageType As Integer
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
    Public Enum TVImageType As Integer
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

    Public Enum TVScraperEventType As Integer
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
    Public Shared Function LocksToOptions() As Structures.MovieScrapeOptions
        Dim options As New Structures.MovieScrapeOptions
        With options
            .bCast = True
            .bCert = True
            .bCleanPlotOutline = True
            .bCollection = True
            .bCountry = True
            .bDirector = True
            .bFullCast = True
            .bFullCrew = True
            .bGenre = Not Master.eSettings.MovieLockGenre    'Dekker500 This used to just be =True
            .bLanguageA = Not Master.eSettings.MovieLockLanguageA
            .bLanguageV = Not Master.eSettings.MovieLockLanguageV
            .bMPAA = True
            .bMusicBy = True
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
            .buseMPAAForFSK = Not Master.eSettings.MovieScraperUseMPAAFSK
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
            .bCert = Master.eSettings.MovieScraperCertification
            .bCollection = Master.eSettings.MovieScraperCollection
            .bCountry = Master.eSettings.MovieScraperCountry
            .bDirector = Master.eSettings.MovieScraperDirector
            .bFullCast = Master.eSettings.MovieScraperFullCast
            .bFullCrew = Master.eSettings.MovieScraperFullCrew
            .bGenre = Master.eSettings.MovieScraperGenre
            .bMPAA = Master.eSettings.MovieScraperMPAA
            .bMusicBy = Master.eSettings.MovieScraperMusicBy
            .bOtherCrew = Master.eSettings.MovieScraperCrew
            .bOutline = Master.eSettings.MovieScraperOutline
            .bPlot = Master.eSettings.MovieScraperPlot
            .bProducers = Master.eSettings.MovieScraperProducers
            .bRating = Master.eSettings.MovieScraperRating
            .bRelease = Master.eSettings.MovieScraperRelease
            .bRuntime = Master.eSettings.MovieScraperRuntime
            .bStudio = Master.eSettings.MovieScraperStudio
            .bTagline = Master.eSettings.MovieScraperTagline
            .bTitle = Master.eSettings.MovieScraperTitle
            .bTop250 = Master.eSettings.MovieScraperTop250
            .bTrailer = Master.eSettings.MovieScraperTrailer
            .bVotes = Master.eSettings.MovieScraperVotes
            .bWriters = Master.eSettings.MovieScraperWriters
            .bYear = Master.eSettings.MovieScraperYear
        End With

        With Master.DefaultTVOptions
            .bEpActors = Master.eSettings.TVScraperEpisodeActors
            .bEpAired = Master.eSettings.TVScraperEpisodeAired
            .bEpCredits = Master.eSettings.TVScraperEpisodeCredits
            .bEpDirector = Master.eSettings.TVScraperEpisodeDirector
            .bEpEpisode = Master.eSettings.TVScraperEpisodeEpisode
            .bEpPlot = Master.eSettings.TVScraperEpisodePlot
            .bEpRating = Master.eSettings.TVScraperEpisodeRating
            .bEpSeason = Master.eSettings.TVScraperEpisodeSeason
            .bEpTitle = Master.eSettings.TVScraperEpisodeTitle
            .bShowActors = Master.eSettings.TVScraperShowActors
            .bShowEpisodeGuide = Master.eSettings.TVScraperShowEpiGuideURL
            .bShowGenre = Master.eSettings.TVScraperShowGenre
            .bShowMPAA = Master.eSettings.TVScraperShowMPAA
            .bShowPlot = Master.eSettings.TVScraperShowPlot
            .bShowPremiered = Master.eSettings.TVScraperShowPremiered
            .bShowRating = Master.eSettings.TVScraperShowRating
            .bShowStatus = Master.eSettings.TVScraperShowStatus
            .bShowStudio = Master.eSettings.TVScraperShowStudio
            .bShowTitle = Master.eSettings.TVScraperShowTitle
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
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Failed trying to identify last thumb from path: " & sPath, ex)
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
            Dim dInfo As New DirectoryInfo(ShowPath)

            For Each sDir As DirectoryInfo In dInfo.GetDirectories
                For Each rShow As Settings.TVShowRegEx In Master.eSettings.TVShowRegexes.Where(Function(s) s.SeasonFromDirectory = True)
                    For Each sMatch As Match In Regex.Matches(FileUtils.Common.GetDirectory(sDir.FullName), rShow.SeasonRegex, RegexOptions.IgnoreCase)
                        Try
                            If (IsNumeric(sMatch.Groups("season").Value) AndAlso iSeason = Convert.ToInt32(sMatch.Groups("season").Value)) OrElse (Regex.IsMatch(sMatch.Groups("season").Value, "specials?", RegexOptions.IgnoreCase) AndAlso iSeason = 0) Then
                                Return sDir.FullName
                            End If
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name & vbTab & " Failed to determine path for season " & iSeason & " in path: " & ShowPath, ex)
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
        For Each rShow As Settings.TVShowRegEx In Master.eSettings.TVShowRegexes.Where(Function(s) s.SeasonFromDirectory = True)
            If Regex.IsMatch(FileUtils.Common.GetDirectory(sPath), rShow.SeasonRegex, RegexOptions.IgnoreCase) Then Return True
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
    Public Shared Function ScrapeModifierAndAlso(ByVal Options As Structures.ScrapeModifier, ByVal Options2 As Structures.ScrapeModifier) As Structures.ScrapeModifier
        Dim filterModifier As New Structures.ScrapeModifier
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
    Public Shared Function MovieScrapeOptionsAndAlso(ByVal Options As Structures.MovieScrapeOptions, ByVal Options2 As Structures.MovieScrapeOptions) As Structures.MovieScrapeOptions
        Dim filterOptions As New Structures.MovieScrapeOptions
        filterOptions.bCast = Options.bCast AndAlso Options2.bCast
        filterOptions.bCert = Options.bCert AndAlso Options2.bCert
        filterOptions.bCleanPlotOutline = Options.bCleanPlotOutline AndAlso Options2.bCleanPlotOutline
        filterOptions.bCollection = Options.bCollection AndAlso Options2.bCollection
        filterOptions.bCountry = Options.bCountry AndAlso Options2.bCountry
        filterOptions.bDirector = Options.bDirector AndAlso Options2.bDirector
        filterOptions.bFullCast = Options.bFullCast AndAlso Options2.bFullCast
        filterOptions.bFullCrew = Options.bFullCrew AndAlso Options2.bFullCrew
        filterOptions.bGenre = Options.bGenre AndAlso Options2.bGenre
        filterOptions.bLanguageA = Options.bLanguageA AndAlso Options2.bLanguageA
        filterOptions.bLanguageV = Options.bLanguageV AndAlso Options2.bLanguageV
        filterOptions.bMPAA = Options.bMPAA AndAlso Options2.bMPAA
        filterOptions.bMusicBy = Options.bMusicBy AndAlso Options2.bMusicBy
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
        filterOptions.buseMPAAForFSK = Options.buseMPAAForFSK AndAlso Options2.buseMPAAForFSK
        Return filterOptions
    End Function
    ''' <summary>
    ''' Determine the Structures.MovieSetScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.MovieSetScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.MovieSetScrapeOptions</param>
    ''' <returns>Structures.MovieSetScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function MovieSetScrapeOptionsAndAlso(ByVal Options As Structures.MovieSetScrapeOptions, ByVal Options2 As Structures.MovieSetScrapeOptions) As Structures.MovieSetScrapeOptions
        Dim filterOptions As New Structures.MovieSetScrapeOptions
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
        filterOptions.bEpPlot = Options.bEpPlot AndAlso Options2.bEpPlot
        filterOptions.bEpRating = Options.bEpRating AndAlso Options2.bEpRating
        filterOptions.bEpSeason = Options.bEpSeason AndAlso Options2.bEpSeason
        filterOptions.bEpTitle = Options.bEpTitle AndAlso Options2.bEpTitle
        filterOptions.bShowActors = Options.bShowActors AndAlso Options2.bShowActors
        filterOptions.bShowEpisodeGuide = Options.bShowEpisodeGuide AndAlso Options2.bShowEpisodeGuide
        filterOptions.bShowGenre = Options.bShowGenre AndAlso Options2.bShowGenre
        filterOptions.bShowMPAA = Options.bShowMPAA AndAlso Options2.bShowMPAA
        filterOptions.bShowPlot = Options.bShowPlot AndAlso Options2.bShowPlot
        filterOptions.bShowPremiered = Options.bShowPremiered AndAlso Options2.bShowPremiered
        filterOptions.bShowRating = Options.bShowRating AndAlso Options2.bShowRating
        filterOptions.bShowStatus = Options.bShowStatus AndAlso Options2.bShowStatus
        filterOptions.bShowStudio = Options.bShowStudio AndAlso Options2.bShowStudio
        filterOptions.bShowTitle = Options.bShowTitle AndAlso Options2.bShowTitle
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
    Public Shared Sub SetScraperMod(ByVal MType As Enums.MovieModType, ByVal MValue As Boolean, Optional ByVal DoClear As Boolean = True)
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
                Case Enums.MovieModType.All
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
                Case Enums.MovieModType.ActorThumbs
                    .ActorThumbs = MValue
                Case Enums.MovieModType.Banner
                    .Banner = MValue
                Case Enums.MovieModType.CharacterArt
                    .CharacterArt = MValue
                Case Enums.MovieModType.ClearArt
                    .ClearArt = MValue
                Case Enums.MovieModType.ClearLogo
                    .ClearLogo = MValue
                Case Enums.MovieModType.DiscArt
                    .DiscArt = MValue
                Case Enums.MovieModType.DoSearch
                    .DoSearch = MValue
                Case Enums.MovieModType.EFanarts
                    .EFanarts = MValue
                Case Enums.MovieModType.EThumbs
                    .EThumbs = MValue
                Case Enums.MovieModType.Fanart
                    .Fanart = MValue
                Case Enums.MovieModType.Landscape
                    .Landscape = MValue
                Case Enums.MovieModType.Meta
                    .Meta = MValue
                Case Enums.MovieModType.NFO
                    .NFO = MValue
                Case Enums.MovieModType.Poster
                    .Poster = MValue
                Case Enums.MovieModType.Trailer
                    .Trailer = MValue
                Case Enums.MovieModType.Theme
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
                    Dim localFileName = uriDestination.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped)
                    If (Not File.Exists(localFileName)) Then
                        logger.Error("Destination is a file, but it does not exist <{0}>", Destination)
                        Return False
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
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not launch <" & Destination & ">", ex)
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
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not launch <" & dllPath & ">", ex)
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
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Could not launch <" & Process_Name & ">", ex)
        End Try

        Return OutputString
    End Function
#End Region 'Methods

End Class 'Functions

Public Class Structures

#Region "Nested Types"
    ''' <summary>
    ''' Structure representing poster/image metadata
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure v3Size
        Dim size As String
        Dim description As String
        Dim index As Integer
        Dim width As Integer
    End Structure

    Public Structure CustomUpdaterStruct
        Dim Canceled As Boolean
        Dim Options As MovieScrapeOptions
        Dim ScrapeType As Enums.ScrapeType
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
        Dim DateAdd As Long
        Dim DiscArtPath As String
        Dim EFanartsPath As String
        Dim efList As List(Of String)
        Dim etList As List(Of String)
        Dim EThumbsPath As String
        Dim FanartPath As String
        Dim FileSource As String
        Dim Filename As String
        Dim ID As Long
        Dim IsLock As Boolean
        Dim IsMark As Boolean
        Dim IsMarkCustom1 As Boolean
        Dim IsMarkCustom2 As Boolean
        Dim IsMarkCustom3 As Boolean
        Dim IsMarkCustom4 As Boolean
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
    End Structure
    ''' <summary>
    ''' Structure representing a TV show in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBTV
        Dim ClearShowEFanarts As Boolean
        Dim efList As List(Of String)
        Dim EpFanartPath As String
        Dim EpID As Long
        Dim EpNeedsSave As Boolean
        Dim EpNfoPath As String
        Dim EpPosterPath As String
        Dim Filename As String
        Dim IsLockEp As Boolean
        Dim IsLockSeason As Boolean
        Dim IsLockShow As Boolean
        Dim IsMarkEp As Boolean
        Dim IsMarkSeason As Boolean
        Dim IsMarkShow As Boolean
        Dim Ordering As Enums.Ordering
        Dim SeasonBannerPath As String
        Dim SeasonFanartPath As String
        Dim SeasonLandscapePath As String
        Dim SeasonPosterPath As String
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
    End Structure

    Public Structure Scans
        Dim Movies As Boolean
        Dim MovieSets As Boolean
        Dim TV As Boolean
    End Structure

    Public Structure ScrapeInfo
        Dim CurrentImage As Images
        Dim Ordering As Enums.Ordering
        Dim iEpisode As Integer
        Dim ImageType As Enums.TVImageType
        Dim iSeason As Integer
        Dim Options As Structures.TVScrapeOptions
        Dim ShowLang As String
        Dim SourceLang As String
        Dim ShowID As Integer
        Dim ShowTitle As String
        Dim TVDBID As String
        Dim WithCurrent As Boolean
        Dim ScrapeType As Enums.ScrapeType
    End Structure

    Public Structure ScrapeModifier
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
    ''' <summary>
    ''' Structure representing posible scrape fields for movies
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Structure MovieScrapeOptions
        Dim bCast As Boolean
        Dim bCert As Boolean
        Dim bCollection As Boolean
        Dim bDirector As Boolean
        Dim bFullCast As Boolean
        Dim bFullCrew As Boolean
        Dim bGenre As Boolean
        Dim bMPAA As Boolean
        Dim bMusicBy As Boolean
        Dim bOtherCrew As Boolean
        Dim bOutline As Boolean
        Dim bPlot As Boolean
        Dim bProducers As Boolean
        Dim bRating As Boolean
        Dim bLanguageV As Boolean
        Dim bLanguageA As Boolean
        Dim buseMPAAForFSK As Boolean
        Dim bCleanPlotOutline As Boolean
        Dim bRelease As Boolean
        Dim bRuntime As Boolean
        Dim bStatus As Boolean
        Dim bStudio As Boolean
        Dim bTagline As Boolean
        Dim bTitle As Boolean
        Dim bTop250 As Boolean
        Dim bCountry As Boolean
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
    Public Structure MovieSetScrapeOptions
        Dim bPlot As Boolean
        Dim bTitle As Boolean
    End Structure

    Public Structure SettingsResult
        Dim DidCancel As Boolean
        Dim NeedsRefresh As Boolean
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
        Dim bEpPlot As Boolean
        Dim bEpRating As Boolean
        Dim bEpSeason As Boolean
        Dim bEpTitle As Boolean
        Dim bShowActors As Boolean
        Dim bShowEpisodeGuide As Boolean
        Dim bShowGenre As Boolean
        Dim bShowMPAA As Boolean
        Dim bShowPlot As Boolean
        Dim bShowPremiered As Boolean
        Dim bShowRating As Boolean
        Dim bShowStatus As Boolean
        Dim bShowStudio As Boolean
        Dim bShowTitle As Boolean
    End Structure

    Public Structure ModulesMenus
        Dim IfNoMovies As Boolean
        Dim IfNoTVShow As Boolean
    End Structure

#End Region 'Nested Types

End Class 'Structures