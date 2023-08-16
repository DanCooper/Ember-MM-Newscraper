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

Imports NLog
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Drawing

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

#End Region 'Nested Types

End Class

Public Class Enums

#Region "Enumerations"

    Public Enum AddonEventType As Integer
        AfterEdit_Movie
        AfterEdit_Movieset
        AfterEdit_TVEpisode
        AfterEdit_TVSeason
        AfterEdit_TVShow
        AfterUpdateDB_Movie
        AfterUpdateDB_TV
        BeforeEdit_Movie
        BeforeEdit_Movieset
        BeforeEdit_TVEpisode
        BeforeEdit_TVSeason
        BeforeEdit_TVShow
        CommandLine
        DuringUpdateDB_TV
        FrameExtrator_Movie
        FrameExtrator_TVEpisode
        Generic
        OnBannerSave_Movie
        OnClearArtSave_Movie
        OnClearLogoSave_Movie
        OnDiscArtSave_Movie
        OnFanartDelete_Movie
        OnFanartSave_Movie
        OnLandscapeSave_Movie
        OnNFORead_TVShow
        OnNFOSave_Movie
        OnNFOSave_TVShow
        OnPosterDelete_Movie
        OnPosterSave_Movie
        OnThemeSave_Movie
        OnTrailerSave_Movie
        RandomFrameExtrator
        Remove_Movie
        Remove_MovieSet
        Remove_TVEpisode
        Remove_TVSeason
        Remove_TVShow
        Scrape_Movie
        'Scrape_Movie_PreCheck
        Scrape_Movieset
        'Scrape_Movieset_PreCheck
        Scrape_TVEpisode
        'Scrape_TVEpisode_PreCheck
        Scrape_TVSeason
        'Scrape_TVSeason_PreCheck
        Scrape_TVShow
        'Scrape_TVShow_PreCheck
        ScraperMulti_Movie
        ScraperMulti_TVEpisode
        ScraperMulti_TVSeason
        ScraperMulti_TVShow
        ScraperSingle_Movie
        ScraperSingle_TVEpisode
        ScraperSingle_TVSeason
        ScraperSingle_TVShow
        Search_Movie
        Search_Movieset
        Search_TVEpisode
        Search_TVSeason
        Search_TVShow
        ShowMovie
        ShowTVShow
        SyncModuleSettings
        Sync_Movie
        Sync_MovieSet
        Sync_TVEpisode
        Sync_TVSeason
        Sync_TVShow
        Task
    End Enum
    ''' <summary>
    ''' Enum representing the audio bitrates
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum AudioBitrate As Integer
        Q512kbps = 0
        Q384kbps = 1
        Q256kbps = 2
        Q192kbps = 3
        Q128kbps = 4
        Q64kbps = 5
        Q48kbps = 6
        UNKNOWN = 7
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the audio codecs
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum AudioCodec As Integer
        AAC
        AAC_SPATIAL
        AC3_SPATIAL
        DTSE_SPATIAL
        EC3_SPATIAL
        MP3
        Opus
        Opus_SPATIAL
        Vorbis
        Vorbis_SPATIAL
        UNKNOWN
        Any
    End Enum

    Public Enum ContentType As Integer
        Generic
        Movie
        Movieset
        MusicVideo
        None
        Person
        Source_Movie
        Source_TVShow
        TV
        TVEpisode
        TVSeason
        TVShow
    End Enum

    Public Enum DefaultType As Integer
        All
        AudioCodecMapping
        Generic
        ListSorting_Movie
        ListSorting_Movieset
        ListSorting_TVEpisode
        ListSorting_TVSeason
        ListSorting_TVShow
        MainTabSorting
        SortTokens
        TVShowMatching
        TitleBlackList_TVSeason
        TitleFilters_Movie
        TitleFilters_TVEpisode
        TitleFilters_TVShow
        TrailerCodec
        ValidSubtitleExts
        ValidThemeExts
        ValidVideoExts
        VideoCodecMapping
    End Enum
    ''' <summary>
    ''' 0 results in using the current datetime when adding a video
    ''' 1 results in prefering to use the files mtime (if it's valid) and only using the file's ctime if the mtime isn't valid
    ''' 2 results in using the newer datetime of the file's mtime and ctime
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DateTimeStamp As Integer
        Now = 0
        ctime = 1
        mtime = 2
        Newer = 3
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
    ''' <summary>
    ''' Known image sizes
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep the quality sorting</remarks>
    Public Enum ImageSize As Integer
        ''' <summary>
        ''' Movie Poster 2000x3000 / TVShow Poster 2000x3000
        ''' </summary>
        HD3000 = 0
        ''' <summary>
        ''' Movie Fanart 3840x2160 / Episode Poster 3840x2160 / TVShow Fanart 3840x2160
        ''' </summary>
        UHD2160 = 1
        ''' <summary>
        ''' Movie Poster 1400x2100
        ''' </summary>
        HD2100 = 2
        ''' <summary>
        ''' Movie Poster 1000x1500 / TVShow Poster 1000x1500 / Season Poster 1000x1500
        ''' </summary>
        HD1500 = 3
        ''' <summary>
        ''' Movie Fanart 2560x1440 / TVShow Fanart 2560x1440
        ''' </summary>
        QHD1440 = 4
        ''' <summary>
        ''' Movie Poster 1000x1426 / TVShow Poster 1000x1426 / Season Poster 1000x1426
        ''' </summary>
        HD1426 = 5
        ''' <summary>
        ''' Movie Fanart 1920x1080 / Episode Poster 1920x1080 / TVShow Fanart 1920x1080
        ''' </summary>
        HD1080 = 6
        ''' <summary>
        ''' Movie DiscArt 1000x1000 / TVShow Poster 680x1000
        ''' </summary>
        HD1000 = 7
        ''' <summary>
        ''' Movie Fanart 1280x720 / Episode Poster 1280x720 / TVShow Fanart 1280x720
        ''' </summary>
        HD720 = 8
        ''' <summary>
        ''' Season Poster 400x578
        ''' </summary>
        HD578 = 9
        ''' <summary>
        ''' Movie ClearArtHD 1000x562 / Movie Landscape 1000x562 / TVShow ClearArtHD 1000x562 / TVShow Landscape 1000x562
        ''' </summary>
        HD562 = 10
        ''' <summary>
        ''' TV CharacterArt 512x512
        ''' </summary>
        HD512 = 11
        ''' <summary>
        ''' Movie ClearLogoHD 800x310 / TVShow ClearLogoHD 800x310
        ''' </summary>
        HD310 = 12
        ''' <summary>
        ''' Movie ClearArtSD 500x281 / TVShow ClearArtSD 500x281
        ''' </summary>
        SD281 = 13
        ''' <summary>
        ''' Episode Poster 400x300 (400x225 for 16:9 images)
        ''' </summary>
        SD225 = 14
        ''' <summary>
        ''' Movie Banner 1000x185 / TVShow Banner 1000x185
        ''' </summary>        
        HD185 = 15
        ''' <summary>
        ''' Movie ClearLogoSD 400x155 / TVShow ClearLogoSD 400x155
        ''' </summary>
        SD155 = 16
        ''' <summary>
        ''' TVShow Banner 758x140
        ''' </summary>
        HD140 = 17
        ''' <summary>
        ''' Any size that does not correspond to a standard
        ''' </summary>
        Any = 99
    End Enum

    Public Enum ModifierType As Integer
        All
        AllSeasonsBanner
        AllSeasonsFanart
        AllSeasonsLandscape
        AllSeasonsPoster
        DoSearch
        EpisodeActorThumbs
        EpisodeFanart
        EpisodePoster
        EpisodeMeta
        EpisodeNFO
        EpisodeSubtitle
        EpisodeWatchedFile
        MainActorThumbs
        MainBanner
        MainCharacterArt
        MainClearArt
        MainClearLogo
        MainDiscArt
        MainExtrafanarts
        MainExtrathumbs
        MainFanart
        MainKeyart
        MainLandscape
        MainMetaData
        MainNFO
        MainPoster
        MainSubtitle
        MainTheme
        MainTrailer
        MainWatchedFile
        SeasonBanner
        SeasonFanart
        SeasonLandscape
        SeasonNFO
        SeasonPoster
        withEpisodes
        withSeasons
    End Enum

    Public Enum ScannerEventType As Integer
        None
        Added_Movie
        Added_TVEpisode
        Added_TVShow
        CleaningDatabase
        CurrentSource
        PreliminaryTasks
        Refresh_TVShow
        ScannerEnded
        ScannerStarted
    End Enum

    Public Enum ScraperCapatibility As Integer
        '
        'Movie
        '
        Movie_Data_Actors
        Movie_Data_Certifications
        Movie_Data_Collection
        Movie_Data_Countries
        Movie_Data_Credits
        Movie_Data_Directors
        Movie_Data_Genres
        Movie_Data_MPAA
        Movie_Data_OriginalTitle
        Movie_Data_Outline
        Movie_Data_Plot
        Movie_Data_Premiered
        Movie_Data_Ratings
        Movie_Data_Runtime
        Movie_Data_Studios
        Movie_Data_Tagline
        Movie_Data_Tags
        Movie_Data_Title
        Movie_Data_Top250
        Movie_Data_UserRating
        Movie_Image_Banner
        Movie_Image_Clearart
        Movie_Image_Clearlogo
        Movie_Image_Discart
        Movie_Image_Fanart
        'Movie_Image_Keyart
        Movie_Image_Landscape
        Movie_Image_Poster
        Movie_Trailer
        Movie_Theme
        '
        'Movieset
        '
        Movieset_Data_Plot
        Movieset_Data_Title
        Movieset_Image_Banner
        Movieset_Image_Clearart
        Movieset_Image_Clearlogo
        Movieset_Image_Discart
        Movieset_Image_Fanart
        'Movieset_Image_Keyart
        Movieset_Image_Landscape
        Movieset_Image_Poster
        '
        'TVEpisode
        '
        TVEpisode_Data_Actors
        TVEpisode_Data_Aired
        TVEpisode_Data_Directors
        TVEpisode_Data_Credits
        TVEpisode_Data_GuestStars
        TVEpisode_Data_Plot
        TVEpisode_Data_Rating
        TVEpisode_Data_Runtime
        TVEpisode_Data_Title
        TVEpisode_Data_UserRating
        TVEpisode_Image_Fanart
        TVEpisode_Image_Poster
        '
        'TVSeason
        '
        TVSeason_Data_Aired
        TVSeason_Data_Plot
        TVSeason_Data_Title
        TVSeason_Image_Banner
        TVSeason_Image_Fanart
        TVSeason_Image_Landscape
        TVSeason_Image_Poster
        '
        'TVShow
        '
        TVShow_Data_Actors
        TVShow_Data_Certification
        TVShow_Data_Countries
        TVShow_Data_Creators
        TVShow_Data_EpisodeGuide
        TVShow_Data_Genres
        TVShow_Data_MPAA
        TVShow_Data_OriginalTitle
        TVShow_Data_Plot
        TVShow_Data_Premiered
        TVShow_Data_Rating
        TVShow_Data_Runtime
        TVShow_Data_Status
        TVShow_Data_Studios
        TVShow_Data_Tags
        TVShow_Data_Title
        TVShow_Data_UserRating
        TVShow_Image_Banner
        TVShow_Image_CharacteraArt
        TVShow_Image_Clearart
        TVShow_Image_Clearlogo
        TVShow_Image_Fanart
        'TVShow_Image_Keyart
        TVShow_Image_Landscape
        TVShow_Image_Poster
        TVShow_Theme
    End Enum
    ''' <summary>
    ''' Enum representing which Movies/TVShows should be scraped,
    ''' and whether results should be automatically chosen or asked of the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScrapeType As Integer
        ''' <summary>
        ''' Uses the first search result if no Unique ID is available
        ''' </summary>
        Auto
        ''' <summary>
        ''' Shows the Search Results dialog if more than one search result has been returned and no Unique ID is available
        ''' </summary>
        Ask
        ''' <summary>
        ''' Resets all data and run a search based on the file or folder name
        ''' </summary>
        Change
        ''' <summary>
        ''' Shows all the dialogs to let the user select the preferred content
        ''' </summary>
        Manually
        ''' <summary>
        ''' Shows the Search Result dialog (if needed) and the Edit dialog to confirm the scraper result
        ''' </summary>
        SemiManually
        ''' <summary>
        ''' Skips scraping if more than one search result has been returned and no Unique ID is available
        ''' </summary>
        Skip
        ''' <summary>
        ''' Safety trigger if no ScrapeType has been defined (default value for all processes)
        ''' </summary>
        None = 99
    End Enum

    Public Enum SelectionType As Integer
        ''' <summary>
        ''' Uses all database entries specified by ContentType
        ''' </summary>
        All
        ''' <summary>
        ''' Uses the current media list filter (only displayed media list entries)
        ''' </summary>
        Filtered
        ''' <summary>
        ''' Uses all marked database entries specified by ContentType
        ''' </summary>
        Marked
        ''' <summary>
        ''' Uses all database entries specified by ContentType and scrape only missing data fields, image types, themes and trailers
        ''' </summary>
        MissingContent
        ''' <summary>
        ''' Uses all database entries with the flag "New" specified by ContentType
        ''' </summary>
        [New]
        ''' <summary>
        ''' Safety trigger if no ScrapeType has been defined (default value for all processes)
        ''' </summary>
        None
        ''' <summary>
        ''' Uses all selected entries of the current media list
        ''' </summary>
        Selected
    End Enum

    Public Enum SettingsPanelType As Integer
        Addon
        Miscellaneous
        Movie
        MovieFileNaming
        MovieGUI
        MovieImage
        MovieInformation
        'MovieSearch
        MovieSource
        Movieset
        MoviesetFileNaming
        MoviesetInformation
        'MoviesetSearch
        MoviesetSource
        MoviesetGUI
        MoviesetImage
        MovieTheme
        MovieTrailer
        None
        Options
        OptionsConnection
        OptionsFileSystem
        OptionsGlobal
        OptionsGeneral
        OptionsGUI
        TV
        TVFileNaming
        TVInformation
        'TVSearch
        TVSource
        TVGUI
        TVImage
        TVTheme
    End Enum

    Public Enum SortMethod_MovieSet As Integer
        Year = 0    'default in Kodi, so have to be on the first position of enumeration
        Title = 1
    End Enum

    Public Enum TaskManagerEventType As Integer
        RefreshRow = 0
        SimpleMessage = 1
        TaskManagerEnded = 2
        TaskManagerStarted = 3
    End Enum

    Public Enum TVBannerType As Integer
        Blank = 0       'will leave the title and show logo off the banner
        Graphical = 1   'will show the series name in the show's official font or will display the actual logo for the show
        Text = 2        'will show the series name as plain text in an Arial font
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the video codecs
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum VideoCodec As Integer
        H264 = 0
        VP9 = 1
        H263 = 2
        VP8 = 3
        VP9_HDR = 4
        AV1 = 5
        UNKNOWN = 4
    End Enum
    ''' <summary>
    ''' Enum representing the video resolutions
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum VideoResolution As Integer
        HD2160p = 0
        HD2160p60fps = 1
        HD1440p60fps = 2
        HD1440p = 3
        HD1080p = 4
        HD1080p60fps = 5
        HD720p = 6
        HD720p60fps = 7
        HQ480p = 8 'or 576 for 4:3 media
        SQ360p = 9
        SQ240p = 10 'or 270
        SQ144p = 11
        SQ144p15fps = 12
        UNKNOWN = 13
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the video type
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum VideoType As Integer
        Any
        Clip
        Featurette
        Teaser
        Trailer
    End Enum

#End Region 'Enumerations

End Class

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

    Public Shared Function ConvertStringToColor(ByVal value As String) As Color
        If Not String.IsNullOrEmpty(value) Then
            If Integer.TryParse(value, 0) Then
                Return Color.FromArgb(Convert.ToInt32(value))
            ElseIf Color.FromName(value).IsKnownColor OrElse value.StartsWith("#") Then
                Return ColorTranslator.FromHtml(value)
            Else
                logger.Error(String.Concat("No valid color value: ", value))
                Return New Color
            End If
        End If
    End Function
    ''' <summary>
    ''' Convert a VB-styled DateTime to a valid Unix-style timestamp
    ''' </summary>
    ''' <param name="data">A valid VB-style DateTime</param>
    ''' <returns>A value representing the DateTime as a unix-style timestamp <c>Double</c></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertToUnixTimestamp(ByVal data As DateTime) As Double
        Dim origin As Date = New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim diff As TimeSpan = data - origin
        Return Math.Floor(diff.TotalSeconds)
    End Function

    Public Shared Function ConvertToProperDateTime(ByVal strDateTime As String) As String
        If String.IsNullOrEmpty(strDateTime) Then Return String.Empty

        Dim parsedDateTime As Date
        If Date.TryParse(strDateTime, parsedDateTime) Then
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
        With Master.DefaultOptions_Movie
            .Actors = Master.eSettings.Movie.InformationSettings.Actors.Enabled
            .Certifications = Master.eSettings.Movie.InformationSettings.Certifications.Enabled
            .Collection = Master.eSettings.Movie.InformationSettings.Collection.Enabled
            .Countries = Master.eSettings.Movie.InformationSettings.Countries.Enabled
            .Credits = Master.eSettings.Movie.InformationSettings.Credits.Enabled
            .Directors = Master.eSettings.Movie.InformationSettings.Directors.Enabled
            .Genres = Master.eSettings.Movie.InformationSettings.Genres.Enabled
            .MPAA = Master.eSettings.Movie.InformationSettings.MPAA.Enabled
            .OriginalTitle = Master.eSettings.Movie.InformationSettings.OriginalTitle.Enabled
            .Outline = Master.eSettings.Movie.InformationSettings.Outline.Enabled
            .Plot = Master.eSettings.Movie.InformationSettings.Plot.Enabled
            .Premiered = Master.eSettings.Movie.InformationSettings.Premiered.Enabled
            .Ratings = Master.eSettings.Movie.InformationSettings.Ratings.Enabled
            .Runtime = Master.eSettings.Movie.InformationSettings.Runtime.Enabled
            .Studios = Master.eSettings.Movie.InformationSettings.Studios.Enabled
            .Tagline = Master.eSettings.Movie.InformationSettings.Tagline.Enabled
            .Tags = Master.eSettings.Movie.InformationSettings.Tags.Enabled
            .Title = Master.eSettings.Movie.InformationSettings.Title.Enabled
            .Top250 = Master.eSettings.Movie.InformationSettings.Top250.Enabled
            .TrailerLink = Master.eSettings.Movie.InformationSettings.TrailerLink.Enabled
            .UserRating = Master.eSettings.Movie.InformationSettings.UserRating.Enabled
        End With

        With Master.DefaultOptions_Movieset
            .Plot = Master.eSettings.Movieset.InformationSettings.Plot.Enabled
            .Title = Master.eSettings.Movieset.InformationSettings.Title.Enabled
        End With

        With Master.DefaultOptions_TV
            .Episodes.Actors = Master.eSettings.TVScraperEpisodeActors
            .Episodes.Aired = Master.eSettings.TVScraperEpisodeAired
            .Episodes.Credits = Master.eSettings.TVScraperEpisodeCredits
            .Episodes.Directors = Master.eSettings.TVScraperEpisodeDirector
            .Episodes.GuestStars = Master.eSettings.TVScraperEpisodeGuestStars
            .Episodes.Plot = Master.eSettings.TVScraperEpisodePlot
            .Episodes.Ratings = Master.eSettings.TVScraperEpisodeRating
            .Episodes.Runtime = Master.eSettings.TVScraperEpisodeRuntime
            .Episodes.Title = Master.eSettings.TVScraperEpisodeTitle
            .Episodes.UserRating = Master.eSettings.TVScraperEpisodeUserRating
            .Actors = Master.eSettings.TVScraperShowActors
            .Certifications = Master.eSettings.TVScraperShowCert
            .Countries = Master.eSettings.TVScraperShowCountry
            .Creators = Master.eSettings.TVScraperShowCreators
            .EpisodeGuideURL = Master.eSettings.TVScraperShowEpiGuideURL
            .Genres = Master.eSettings.TVScraperShowGenre
            .MPAA = Master.eSettings.TVScraperShowMPAA
            .OriginalTitle = Master.eSettings.TVScraperShowOriginalTitle
            .Plot = Master.eSettings.TVScraperShowPlot
            .Premiered = Master.eSettings.TVScraperShowPremiered
            .Ratings = Master.eSettings.TVScraperShowRating
            .Runtime = Master.eSettings.TVScraperShowRuntime
            .Status = Master.eSettings.TVScraperShowStatus
            .Studios = Master.eSettings.TVScraperShowStudio
            .Tagline = Master.eSettings.TVScraperShowTagline
            .Title = Master.eSettings.TVScraperShowTitle
            .UserRating = Master.eSettings.TVScraperShowUserRating
            .Seasons.Aired = Master.eSettings.TVScraperSeasonAired
            .Seasons.Plot = Master.eSettings.TVScraperSeasonPlot
            .Seasons.Title = Master.eSettings.TVScraperSeasonTitle
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
        Return String.Concat(AppPath, "Bin", Path.DirectorySeparatorChar, "ffmpeg.exe")
    End Function

    ''' <summary>
    ''' Get a path to the FFProbe included with the Ember distribution
    ''' </summary>
    ''' <returns>A path to an instance of FFProbe</returns>
    ''' <remarks>Windows distributions have FFProbe in the Bin subdirectory.
    ''' Note that no validation is done to ensure that FFProbe actually exists.</remarks>
    Public Shared Function GetFFProbe() As String
        Return String.Concat(AppPath, "Bin", Path.DirectorySeparatorChar, "ffprobe.exe")
    End Function
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

    Public Shared Function GetDateTimeStampOptions() As List(Of KeyValuePair(Of String, Enums.DateTimeStamp))
        Return New Dictionary(Of String, Enums.DateTimeStamp) From {
            {Master.eLang.GetString(1210, "Current DateTime when adding"), Enums.DateTimeStamp.Now},
            {Master.eLang.GetString(1227, "ctime (fallback to mtime)"), Enums.DateTimeStamp.ctime},
            {Master.eLang.GetString(1211, "mtime (fallback to ctime)"), Enums.DateTimeStamp.mtime},
            {Master.eLang.GetString(1212, "Newer of mtime and ctime"), Enums.DateTimeStamp.Newer}
        }.ToList
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
    ''' Determine the Structures.ScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.ScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.ScrapeOptions</param>
    ''' <returns>StructuresScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function ScrapeOptionsAndAlso(ByVal Options As Structures.ScrapeOptions, ByVal Options2 As Structures.ScrapeOptions) As Structures.ScrapeOptions
        Dim ScrapeOptions = ScrapeOptionsAndAlso_Internal(Options, Options2)
        ScrapeOptions.Episodes = ScrapeOptionsAndAlso_Internal(Options.Episodes, Options2.Episodes)
        ScrapeOptions.Seasons = ScrapeOptionsAndAlso_Internal(Options.Seasons, Options2.Seasons)
        Return ScrapeOptions
    End Function
    ''' <summary>
    ''' Determine the Structures.ScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.ScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.ScrapeOptions</param>
    ''' <returns>StructuresScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Private Shared Function ScrapeOptionsAndAlso_Internal(ByVal Options As Structures.ScrapeOptionsBase, ByVal Options2 As Structures.ScrapeOptionsBase) As Structures.ScrapeOptions
        Return New Structures.ScrapeOptions With {
            .Actors = Options.Actors AndAlso Options2.Actors,
            .Aired = Options.Aired AndAlso Options2.Aired,
            .Certifications = Options.Certifications AndAlso Options2.Certifications,
            .Collection = Options.Collection AndAlso Options2.Collection,
            .Countries = Options.Countries AndAlso Options2.Countries,
            .Creators = Options.Creators AndAlso Options2.Creators,
            .Credits = Options.Credits AndAlso Options2.Credits,
            .Directors = Options.Directors AndAlso Options2.Directors,
            .Edition = Options.Edition AndAlso Options2.Edition,
            .EpisodeGuideURL = Options.EpisodeGuideURL AndAlso Options2.EpisodeGuideURL,
            .Genres = Options.Genres AndAlso Options2.Genres,
            .GuestStars = Options.GuestStars AndAlso Options2.GuestStars,
            .MPAA = Options.MPAA AndAlso Options2.MPAA,
            .OriginalTitle = Options.OriginalTitle AndAlso Options2.OriginalTitle,
            .Outline = Options.Outline AndAlso Options2.Outline,
            .Plot = Options.Plot AndAlso Options2.Plot,
            .Premiered = Options.Premiered AndAlso Options2.Premiered,
            .Ratings = Options.Ratings AndAlso Options2.Ratings,
            .Runtime = Options.Runtime AndAlso Options2.Runtime,
            .Status = Options.Status AndAlso Options2.Status,
            .Studios = Options.Studios AndAlso Options2.Studios,
            .Tagline = Options.Tagline AndAlso Options2.Tagline,
            .Tags = Options.Tags AndAlso Options2.Tags,
            .Title = Options.Title AndAlso Options2.Title,
            .Top250 = Options.Top250 AndAlso Options2.Top250,
            .TrailerLink = Options.TrailerLink AndAlso Options2.TrailerLink,
            .UserRating = Options.UserRating AndAlso Options2.UserRating,
            .VideoSource = Options.VideoSource AndAlso Options2.VideoSource
        }
    End Function

    Public Shared Function ScrapeModifiersAndAlso(ByVal options As Structures.ScrapeModifiers, ByVal options2 As Structures.ScrapeModifiers) As Structures.ScrapeModifiers
        Dim FilteredModifiers As New Structures.ScrapeModifiers With {
            .AllSeasonsBanner = options.AllSeasonsBanner AndAlso options2.AllSeasonsBanner,
            .AllSeasonsFanart = options.AllSeasonsFanart AndAlso options2.AllSeasonsFanart,
            .AllSeasonsLandscape = options.AllSeasonsLandscape AndAlso options2.AllSeasonsLandscape,
            .AllSeasonsPoster = options.AllSeasonsPoster AndAlso options2.AllSeasonsPoster,
            .DoSearch = options.DoSearch AndAlso options2.DoSearch,
            .Episodes = New Structures.ScrapeModifiersBase With {
            .Actorthumbs = options.Episodes.Actorthumbs AndAlso options2.Episodes.Actorthumbs,
            .Fanart = options.Episodes.Fanart AndAlso options2.Episodes.Fanart,
            .Metadata = options.Episodes.Metadata AndAlso options2.Episodes.Metadata,
            .Information = options.Episodes.Information AndAlso options2.Episodes.Information,
            .Poster = options.Episodes.Poster AndAlso options2.Episodes.Poster,
            .Subtitles = options.Episodes.Subtitles AndAlso options2.Episodes.Subtitles,
            .WatchedFile = options.Episodes.WatchedFile AndAlso options2.Episodes.WatchedFile},
            .Actorthumbs = options.Actorthumbs AndAlso options2.Actorthumbs,
            .Banner = options.Banner AndAlso options2.Banner,
            .Characterart = options.Characterart AndAlso options2.Characterart,
            .Clearart = options.Clearart AndAlso options2.Clearart,
            .Clearlogo = options.Clearlogo AndAlso options2.Clearlogo,
            .Discart = options.Discart AndAlso options2.Discart,
            .Extrafanarts = options.Extrafanarts AndAlso options2.Extrafanarts,
            .Extrathumbs = options.Extrathumbs AndAlso options2.Extrathumbs,
            .Fanart = options.Fanart AndAlso options2.Fanart,
            .Keyart = options.Keyart AndAlso options2.Keyart,
            .Landscape = options.Landscape AndAlso options2.Landscape,
            .Information = options.Information AndAlso options2.Information,
            .Poster = options.Poster AndAlso options2.Poster,
            .Subtitles = options.Subtitles AndAlso options2.Subtitles,
            .Theme = options.Theme AndAlso options2.Theme,
            .Trailer = options.Trailer AndAlso options2.Trailer,
            .WatchedFile = options.WatchedFile AndAlso options2.WatchedFile,
            .Seasons = New Structures.ScrapeModifiersBase With {
            .Banner = options.Seasons.Banner AndAlso options2.Seasons.Banner,
            .Fanart = options.Seasons.Fanart AndAlso options2.Seasons.Fanart,
            .Landscape = options.Seasons.Landscape AndAlso options2.Seasons.Landscape,
            .Information = options.Seasons.Information AndAlso options2.Seasons.Information,
            .Poster = options.Seasons.Poster AndAlso options2.Seasons.Poster},
            .withEpisodes = options.withEpisodes AndAlso options2.withEpisodes,
            .withSeasons = options.withSeasons AndAlso options2.withSeasons
        }
        Return FilteredModifiers
    End Function

    Public Shared Function ScrapeModifiersAnyEnabled(ByVal options As Structures.ScrapeModifiers) As Boolean
        With options
            If .AllSeasonsBanner OrElse
                .AllSeasonsFanart OrElse
                .AllSeasonsLandscape OrElse
                .AllSeasonsPoster OrElse
                .Episodes.Actorthumbs OrElse
                .Episodes.Fanart OrElse
                .Episodes.Metadata OrElse
                .Episodes.Information OrElse
                .Episodes.Poster OrElse
                .Episodes.Subtitles OrElse
                .Episodes.WatchedFile OrElse
                .Actorthumbs OrElse
                .Banner OrElse
                .Characterart OrElse
                .Clearart OrElse
                .Clearlogo OrElse
                .Discart OrElse
                .Extrafanarts OrElse
                .Extrathumbs OrElse
                .Fanart OrElse
                .Keyart OrElse
                .Landscape OrElse
                .Metadata OrElse
                .Information OrElse
                .Poster OrElse
                .Subtitles OrElse
                .Theme OrElse
                .Trailer OrElse
                .WatchedFile OrElse
                .Seasons.Banner OrElse
                .Seasons.Fanart OrElse
                .Seasons.Landscape OrElse
                .Seasons.Information OrElse
                .Seasons.Poster Then
                Return True
            End If
        End With

        Return False
    End Function

    Public Shared Sub SetScrapeModifiers(ByRef scrapeModifiers As Structures.ScrapeModifiers, ByVal type As Enums.ModifierType, ByVal enabled As Boolean)
        With scrapeModifiers
            Select Case type
                Case Enums.ModifierType.All
                    .AllSeasonsBanner = enabled
                    .AllSeasonsFanart = enabled
                    .AllSeasonsLandscape = enabled
                    .AllSeasonsPoster = enabled
                    '.DoSearch should not be set here as it is only needed for a re-search of a movie (first scraping or movie change).
                    .Episodes.Actorthumbs = enabled
                    .Episodes.Fanart = enabled
                    .Episodes.Metadata = enabled
                    .Episodes.Information = enabled
                    .Episodes.Poster = enabled
                    .Episodes.Subtitles = enabled
                    .Episodes.WatchedFile = enabled
                    .Actorthumbs = enabled
                    .Banner = enabled
                    .Characterart = enabled
                    .Clearart = enabled
                    .Clearlogo = enabled
                    .Discart = enabled
                    .Extrafanarts = enabled
                    .Extrathumbs = enabled
                    .Fanart = enabled
                    .Keyart = enabled
                    .Landscape = enabled
                    .Metadata = enabled
                    .Information = enabled
                    .Poster = enabled
                    .Subtitles = enabled
                    .Theme = enabled
                    .Trailer = enabled
                    .WatchedFile = enabled
                    .Seasons.Banner = enabled
                    .Seasons.Fanart = enabled
                    .Seasons.Landscape = enabled
                    .Seasons.Information = enabled
                    .Seasons.Poster = enabled
                '.withEpisodes should not be set here
                '.withSeasons should not be set here
                Case Enums.ModifierType.AllSeasonsBanner
                    .AllSeasonsBanner = enabled
                Case Enums.ModifierType.AllSeasonsFanart
                    .AllSeasonsFanart = enabled
                Case Enums.ModifierType.AllSeasonsLandscape
                    .AllSeasonsLandscape = enabled
                Case Enums.ModifierType.AllSeasonsPoster
                    .AllSeasonsPoster = enabled
                Case Enums.ModifierType.DoSearch
                    .DoSearch = enabled
                Case Enums.ModifierType.EpisodeActorThumbs
                    .Episodes.Actorthumbs = enabled
                Case Enums.ModifierType.EpisodeFanart
                    .Episodes.Fanart = enabled
                Case Enums.ModifierType.EpisodeMeta
                    .Episodes.Metadata = enabled
                Case Enums.ModifierType.EpisodeNFO
                    .Episodes.Information = enabled
                Case Enums.ModifierType.EpisodePoster
                    .Episodes.Poster = enabled
                Case Enums.ModifierType.EpisodeSubtitle
                    .Episodes.Subtitles = enabled
                Case Enums.ModifierType.EpisodeWatchedFile
                    .Episodes.WatchedFile = enabled
                Case Enums.ModifierType.MainActorThumbs
                    .Actorthumbs = enabled
                Case Enums.ModifierType.MainBanner
                    .Banner = enabled
                Case Enums.ModifierType.MainCharacterArt
                    .Characterart = enabled
                Case Enums.ModifierType.MainClearArt
                    .Clearart = enabled
                Case Enums.ModifierType.MainClearLogo
                    .Clearlogo = enabled
                Case Enums.ModifierType.MainDiscArt
                    .Discart = enabled
                Case Enums.ModifierType.MainExtrafanarts
                    .Extrafanarts = enabled
                Case Enums.ModifierType.MainExtrathumbs
                    .Extrathumbs = enabled
                Case Enums.ModifierType.MainFanart
                    .Fanart = enabled
                Case Enums.ModifierType.MainKeyart
                    .Keyart = enabled
                Case Enums.ModifierType.MainLandscape
                    .Landscape = enabled
                Case Enums.ModifierType.MainMetaData
                    .Metadata = enabled
                Case Enums.ModifierType.MainNFO
                    .Information = enabled
                Case Enums.ModifierType.MainPoster
                    .Poster = enabled
                Case Enums.ModifierType.MainSubtitle
                    .Subtitles = enabled
                Case Enums.ModifierType.MainTheme
                    .Theme = enabled
                Case Enums.ModifierType.MainTrailer
                    .Trailer = enabled
                Case Enums.ModifierType.MainWatchedFile
                    .WatchedFile = enabled
                Case Enums.ModifierType.SeasonBanner
                    .Seasons.Banner = enabled
                Case Enums.ModifierType.SeasonFanart
                    .Seasons.Fanart = enabled
                Case Enums.ModifierType.SeasonLandscape
                    .Seasons.Landscape = enabled
                Case Enums.ModifierType.SeasonNFO
                    .Seasons.Information = enabled
                Case Enums.ModifierType.SeasonPoster
                    .Seasons.Poster = enabled
                Case Enums.ModifierType.withEpisodes
                    .withEpisodes = enabled
                Case Enums.ModifierType.withSeasons
                    .withSeasons = enabled
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
                        Process.Start(uriDestination.LocalPath)
                        Return True
                    End If
                End If
            End If

            'If we got this far, everything is OK, so we can go ahead and launch it!
            Process.Start(uriDestination.AbsoluteUri())
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not launch <" & Process_Name & ">")
        End Try

        Return OutputString
    End Function
#End Region 'Methods

End Class

Public Class Structures

#Region "Nested Types"

    Public Structure CustomUpdaterStruct
        Dim Canceled As Boolean
        Dim ScrapeOptions As ScrapeOptions
        Dim ScrapeModifiers As ScrapeModifiers
        Dim ScrapeType As Enums.ScrapeType
        Dim SelectionType As Enums.SelectionType
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

    <Serializable>
    Public Class ScrapeModifiers
        Inherits ScrapeModifiersBase

#Region "Fields"

        Public Episodes As New ScrapeModifiersBase
        Public Seasons As New ScrapeModifiersBase

#End Region 'Fields

#Region "Properties"

        Public ReadOnly Property AnyEnabled As Boolean
            Get
                Return AllSeasonsBanner OrElse
                    AllSeasonsFanart OrElse
                    AllSeasonsLandscape OrElse
                    AllSeasonsPoster OrElse
                    DoSearch OrElse
                    Episodes.Actorthumbs OrElse
                    Episodes.Fanart OrElse
                    Episodes.Poster OrElse
                    Episodes.Metadata OrElse
                    Episodes.Information OrElse
                    Episodes.Subtitles OrElse
                    Episodes.WatchedFile OrElse
                    Actorthumbs OrElse
                    Banner OrElse
                    Characterart OrElse
                    Clearart OrElse
                    Clearlogo OrElse
                    Discart OrElse
                    Extrafanarts OrElse
                    Extrathumbs OrElse
                    Fanart OrElse
                    Keyart OrElse
                    Landscape OrElse
                    Metadata OrElse
                    Information OrElse
                    Poster OrElse
                    Subtitles OrElse
                    Theme OrElse
                    Trailer OrElse
                    WatchedFile OrElse
                    Seasons.Banner OrElse
                    Seasons.Fanart OrElse
                    Seasons.Landscape OrElse
                    Seasons.Information OrElse
                    Seasons.Poster OrElse
                    withEpisodes OrElse
                    withSeasons
            End Get
        End Property

#End Region 'Properties

    End Class

    <Serializable>
    Public Class ScrapeModifiersBase

#Region "Fields"

        Public AllSeasonsBanner As Boolean
        Public AllSeasonsFanart As Boolean
        Public AllSeasonsLandscape As Boolean
        Public AllSeasonsPoster As Boolean
        Public DoSearch As Boolean
        Public Actorthumbs As Boolean
        Public Banner As Boolean
        Public Characterart As Boolean
        Public Clearart As Boolean
        Public Clearlogo As Boolean
        Public Discart As Boolean
        Public Extrafanarts As Boolean
        Public Extrathumbs As Boolean
        Public Fanart As Boolean
        Public Keyart As Boolean
        Public Landscape As Boolean
        Public Metadata As Boolean
        Public Information As Boolean
        Public Poster As Boolean
        Public Subtitles As Boolean
        Public Theme As Boolean
        Public Trailer As Boolean
        Public WatchedFile As Boolean
        Public withEpisodes As Boolean
        Public withSeasons As Boolean

#End Region 'Fields

    End Class
    ''' <summary>
    ''' Structure representing posible scrape fields
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable>
    Public Class ScrapeOptions
        Inherits ScrapeOptionsBase

#Region "Fields"

        Public Episodes As New ScrapeOptionsBase
        Public Seasons As New ScrapeOptionsBase

#End Region 'Fields

    End Class
    ''' <summary>
    ''' Structure representing posible scrape fields
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable>
    Public Class ScrapeOptionsBase

#Region "Fields"

        Public Actors As Boolean
        Public Aired As Boolean
        Public Certifications As Boolean
        Public Collection As Boolean
        Public Countries As Boolean
        Public Creators As Boolean
        Public Credits As Boolean
        Public Directors As Boolean
        Public Edition As Boolean
        Public EpisodeGuideURL As Boolean
        Public Genres As Boolean
        Public GuestStars As Boolean
        Public MPAA As Boolean
        Public OriginalTitle As Boolean
        Public Outline As Boolean
        Public Plot As Boolean
        Public Premiered As Boolean
        Public Ratings As Boolean
        Public Runtime As Boolean
        Public Status As Boolean
        Public Studios As Boolean
        Public Tagline As Boolean
        Public Tags As Boolean
        Public Title As Boolean
        Public Top250 As Boolean
        Public TrailerLink As Boolean
        Public UserRating As Boolean
        Public VideoSource As Boolean

#End Region 'Fields

    End Class

    Public Class SettingsResult

#Region "Properties"

        Public ReadOnly Property AnythingToDo As Boolean
            Get
                Return NeedsDBClean_Movie OrElse
                    NeedsDBClean_TV OrElse
                    NeedsDBUpdate_Movie.Count > 0 OrElse
                    NeedsDBUpdate_TV.Count > 0 OrElse
                    NeedsReload_Movie OrElse
                    NeedsReload_Movieset OrElse
                    NeedsReload_TVEpisode OrElse
                    NeedsReload_TVShow
            End Get
        End Property
        Public Property DidCancel As Boolean
        Public Property NeedsDBClean_Movie As Boolean
        Public Property NeedsDBClean_TV As Boolean
        Public Property NeedsDBUpdate_Movie As New List(Of Long)
        Public Property NeedsDBUpdate_TV As New List(Of Long)
        Public Property NeedsReload_Movie As Boolean
        Public Property NeedsReload_Movieset As Boolean
        Public Property NeedsReload_TVEpisode As Boolean
        Public Property NeedsReload_TVShow As Boolean
        Public Property NeedsRestart As Boolean

#End Region 'Properties
    End Class

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

#End Region 'Nested Types

End Class