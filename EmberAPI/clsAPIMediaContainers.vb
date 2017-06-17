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
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Xml

Namespace MediaContainers

    <Serializable()>
    Public Class Audio

#Region "Fields"

        Private _bitrate As String = String.Empty
        Private _channels As String = String.Empty
        Private _codec As String = String.Empty
        Private _haspreferred As Boolean = False
        Private _language As String = String.Empty
        Private _longlanguage As String = String.Empty

#End Region 'Fields

#Region "Properties"

        <XmlElement("bitrate")>
        Public Property Bitrate() As String
            Get
                Return _bitrate.Trim()
            End Get
            Set(ByVal Value As String)
                _bitrate = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property BitrateSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_bitrate)
            End Get
        End Property

        <XmlElement("channels")>
        Public Property Channels() As String
            Get
                Return _channels.Trim()
            End Get
            Set(ByVal Value As String)
                _channels = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property ChannelsSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_channels)
            End Get
        End Property

        <XmlElement("codec")>
        Public Property Codec() As String
            Get
                Return _codec.Trim()
            End Get
            Set(ByVal Value As String)
                _codec = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property CodecSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_codec)
            End Get
        End Property

        <XmlIgnore>
        Public Property HasPreferred() As Boolean
            Get
                Return _haspreferred
            End Get
            Set(ByVal value As Boolean)
                _haspreferred = value
            End Set
        End Property

        <XmlElement("language")>
        Public Property Language() As String
            Get
                Return _language.Trim()
            End Get
            Set(ByVal Value As String)
                _language = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        <XmlElement("longlanguage")>
        Public Property LongLanguage() As String
            Get
                Return _longlanguage.Trim()
            End Get
            Set(ByVal value As String)
                _longlanguage = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property LongLanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_longlanguage)
            End Get
        End Property

#End Region 'Properties

    End Class


    <Serializable()>
    <XmlRoot("episodedetails")>
    Public Class EpisodeDetails
        Implements ICloneable

#Region "Fields"

        Private _actors As New List(Of Person)
        Private _aired As String
        Private _credits As New List(Of String)
        Private _dateadded As String
        Private _directors As New List(Of String)
        Private _displayepisode As Integer
        Private _displayseason As Integer
        Private _episode As Integer
        Private _episodeabsolute As Integer
        Private _episodecombined As Double
        Private _episodedvd As Double
        Private _fileInfo As New Fileinfo
        Private _gueststars As New List(Of Person)
        Private _imdb As String
        Private _lastplayed As String
        Private _locked As Boolean
        Private _playcount As Integer
        Private _plot As String
        Private _rating As String
        Private _runtime As String
        Private _scrapersource As String
        Private _season As Integer
        Private _seasoncombined As Integer
        Private _seasondvd As Integer
        Private _subepisode As Integer
        Private _thumbposter As New Image
        Private _title As String
        Private _tmdb As String
        Private _tvdb As String
        Private _uniqueids As New List(Of Uniqueid)
        Private _userrating As Integer
        Private _videosource As String
        Private _votes As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("id")>
        Public Property TVDB() As String
            Get
                Return _tvdb
            End Get
            Set(ByVal value As String)
                _tvdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

        <XmlElement("imdb")>
        Public Property IMDB() As String
            Get
                Return _imdb
            End Get
            Set(ByVal value As String)
                _imdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb)
            End Get
        End Property

        <XmlElement("tmdb")>
        Public Property TMDB() As String
            Get
                Return _tmdb
            End Get
            Set(ByVal value As String)
                _tmdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AnyUniqueIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb) OrElse Not String.IsNullOrEmpty(_tmdb) OrElse Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

        <XmlElement("uniqueid")>
        Public Property UniqueIDs() As List(Of Uniqueid)
            Get
                Return _uniqueids
            End Get
            Set(ByVal value As List(Of Uniqueid))
                If value Is Nothing Then
                    _uniqueids.Clear()
                Else
                    _uniqueids = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property UniqueIDsSpecified() As Boolean
            Get
                Return _uniqueids.Count > 0
            End Get
        End Property

        <XmlElement("title")>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_title)
            End Get
        End Property

        <XmlElement("runtime")>
        Public Property Runtime() As String
            Get
                Return _runtime
            End Get
            Set(ByVal value As String)
                _runtime = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RuntimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_runtime) AndAlso Not _runtime = "0"
            End Get
        End Property

        <XmlElement("aired")>
        Public Property Aired() As String
            Get
                Return _aired
            End Get
            Set(ByVal value As String)
                _aired = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AiredSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_aired)
            End Get
        End Property

        <XmlElement("rating")>
        Public Property Rating() As String
            Get
                Return _rating.Replace(",", ".")
            End Get
            Set(ByVal value As String)
                _rating = value.Replace(",", ".")
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_rating) AndAlso Not String.IsNullOrEmpty(_votes)
            End Get
        End Property

        <XmlElement("votes")>
        Public Property Votes() As String
            Get
                Return _votes
            End Get
            Set(ByVal value As String)
                _votes = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property VotesSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_votes) AndAlso Not String.IsNullOrEmpty(_rating)
            End Get
        End Property

        <XmlElement("userrating")>
        Public Property UserRating() As Integer
            Get
                Return _userrating
            End Get
            Set(ByVal value As Integer)
                _userrating = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property UserRatingSpecified() As Boolean
            Get
                Return Not _userrating = 0
            End Get
        End Property

        <XmlElement("videosource")>
        Public Property VideoSource() As String
            Get
                Return _videosource
            End Get
            Set(ByVal value As String)
                _videosource = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property VideoSourceSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_videosource)
            End Get
        End Property

        <XmlElement("season")>
        Public Property Season() As Integer
            Get
                Return _season
            End Get
            Set(ByVal value As Integer)
                _season = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SeasonSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_season.ToString)
            End Get
        End Property

        <XmlElement("episode")>
        Public Property Episode() As Integer
            Get
                Return _episode
            End Get
            Set(ByVal value As Integer)
                _episode = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property EpisodeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_episode.ToString)
            End Get
        End Property

        <XmlElement("subepisode")>
        Public Property SubEpisode() As Integer
            Get
                Return _subepisode
            End Get
            Set(ByVal value As Integer)
                _subepisode = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SubEpisodeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_subepisode.ToString) AndAlso _subepisode > 0
            End Get
        End Property

        <XmlElement("displayseason")>
        Public Property DisplaySeason() As Integer
            Get
                Return _displayseason
            End Get
            Set(ByVal value As Integer)
                _displayseason = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DisplaySeasonSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_displayseason.ToString) AndAlso _displayseason > -1
            End Get
        End Property

        <XmlElement("displayepisode")>
        Public Property DisplayEpisode() As Integer
            Get
                Return _displayepisode
            End Get
            Set(ByVal value As Integer)
                _displayepisode = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DisplayEpisodeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_displayepisode.ToString) AndAlso _displayepisode > -1
            End Get
        End Property

        <XmlElement("plot")>
        Public Property Plot() As String
            Get
                Return _plot.Trim
            End Get
            Set(ByVal value As String)
                _plot = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_plot)
            End Get
        End Property

        <XmlElement("credits")>
        Public Property Credits() As List(Of String)
            Get
                Return _credits
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _credits.Clear()
                Else
                    _credits = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CreditsSpecified() As Boolean
            Get
                Return _credits.Count > 0
            End Get
        End Property

        <XmlElement("playcount")>
        Public Property Playcount() As Integer
            Get
                Return _playcount
            End Get
            Set(ByVal value As Integer)
                _playcount = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlaycountSpecified() As Boolean
            Get
                Return _playcount > 0
            End Get
        End Property

        <XmlElement("lastplayed")>
        Public Property LastPlayed() As String
            Get
                Return _lastplayed
            End Get
            Set(ByVal value As String)
                _lastplayed = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LastPlayedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_lastplayed)
            End Get
        End Property

        <XmlElement("director")>
        Public Property Directors() As List(Of String)
            Get
                Return _directors
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _directors.Clear()
                Else
                    _directors = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DirectorsSpecified() As Boolean
            Get
                Return _directors.Count > 0
            End Get
        End Property

        <XmlElement("actor")>
        Public Property Actors() As List(Of Person)
            Get
                Return _actors
            End Get
            Set(ByVal Value As List(Of Person))
                _actors = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ActorsSpecified() As Boolean
            Get
                Return _actors.Count > 0
            End Get
        End Property

        <XmlElement("gueststar")>
        Public Property GuestStars() As List(Of Person)
            Get
                Return _gueststars
            End Get
            Set(ByVal Value As List(Of Person))
                _gueststars = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property GuestStarsSpecified() As Boolean
            Get
                Return _gueststars.Count > 0
            End Get
        End Property

        <XmlElement("fileinfo")>
        Public Property FileInfo() As Fileinfo
            Get
                Return _fileInfo
            End Get
            Set(ByVal value As Fileinfo)
                _fileInfo = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property FileInfoSpecified() As Boolean
            Get
                If _fileInfo.StreamDetails.Video.Count > 0 OrElse
                _fileInfo.StreamDetails.Audio.Count > 0 OrElse
                 _fileInfo.StreamDetails.Subtitle.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        ''' <summary>
        ''' Poster Thumb for preview in search results
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlIgnore()>
        Public Property ThumbPoster() As Image
            Get
                Return _thumbposter
            End Get
            Set(ByVal value As Image)
                _thumbposter = value
            End Set
        End Property

        <XmlElement("dateadded")>
        Public Property DateAdded() As String
            Get
                Return _dateadded
            End Get
            Set(ByVal value As String)
                _dateadded = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DateAddedSpecified() As Boolean
            Get
                Return _dateadded IsNot Nothing
            End Get
        End Property

        <XmlElement("locked")>
        Public Property Locked() As Boolean
            Get
                Return _locked
            End Get
            Set(ByVal value As Boolean)
                _locked = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property Scrapersource() As String
            Get
                Return _scrapersource
            End Get
            Set(ByVal value As String)
                _scrapersource = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property EpisodeAbsolute() As Integer
            Get
                Return _episodeabsolute
            End Get
            Set(ByVal value As Integer)
                _episodeabsolute = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property EpisodeCombined() As Double
            Get
                Return _episodecombined
            End Get
            Set(ByVal value As Double)
                _episodecombined = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property EpisodeDVD() As Double
            Get
                Return _episodedvd
            End Get
            Set(ByVal value As Double)
                _episodedvd = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property SeasonCombined() As Integer
            Get
                Return _seasoncombined
            End Get
            Set(ByVal value As Integer)
                _seasoncombined = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property SeasonDVD() As Integer
            Get
                Return _seasondvd
            End Get
            Set(ByVal value As Integer)
                _seasondvd = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _actors.Clear()
            _aired = String.Empty
            _credits.Clear()
            _dateadded = String.Empty
            _directors.Clear()
            _displayepisode = -1
            _displayseason = -1
            _episode = -1
            _episodeabsolute = -1
            _episodecombined = -1
            _episodedvd = -1
            _fileInfo = New Fileinfo
            _gueststars.Clear()
            _imdb = String.Empty
            _lastplayed = String.Empty
            _locked = False
            _playcount = 0
            _plot = String.Empty
            _rating = String.Empty
            _runtime = String.Empty
            _scrapersource = String.Empty
            _season = -1
            _seasoncombined = -1
            _seasondvd = -1
            _subepisode = -1
            _thumbposter = New Image
            _tmdb = String.Empty
            _tvdb = String.Empty
            _title = String.Empty
            _uniqueids.Clear()
            _userrating = 0
            _videosource = String.Empty
            _votes = String.Empty
        End Sub

        Public Sub AddCreditsFromString(ByVal value As String)
            _credits.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains("/") Then
                Dim values As String() = value.Split(New [Char]() {"/"c})
                For Each credit As String In values
                    credit = credit.Trim
                    If Not _credits.Contains(credit) And Not value = "See more" Then
                        _credits.Add(credit)
                    End If
                Next
            Else
                value = value.Trim
                If Not _credits.Contains(value) And Not value = "See more" Then
                    _credits.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddDirectorsFromString(ByVal value As String)
            _directors.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains("/") Then
                Dim values As String() = value.Split(New [Char]() {"/"c})
                For Each director As String In values
                    director = director.Trim
                    If Not _directors.Contains(director) And Not value = "See more" Then
                        _directors.Add(director)
                    End If
                Next
            Else
                value = value.Trim
                If Not _directors.Contains(value) And Not value = "See more" Then
                    _directors.Add(value.Trim)
                End If
            End If
        End Sub

        Public Function CloneDeep() As Object Implements ICloneable.Clone
            Dim Stream As New MemoryStream(50000)
            Dim Formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            ' Serialisierung über alle Objekte hinweg in einen Stream 
            Formatter.Serialize(Stream, Me)
            ' Zurück zum Anfang des Streams und... 
            Stream.Seek(0, SeekOrigin.Begin)
            ' ...aus dem Stream in ein Objekt deserialisieren 
            CloneDeep = Formatter.Deserialize(Stream)
            Stream.Close()
        End Function

        Public Sub CreateCachePaths_ActorsThumbs()
            Dim sPath As String = Path.Combine(Master.TempPath, "Global")

            For Each tActor As Person In Actors
                tActor.Thumb.CacheOriginalPath = Path.Combine(sPath, String.Concat("actorthumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                If Not String.IsNullOrEmpty(tActor.Thumb.URLThumb) Then
                    tActor.Thumb.CacheThumbPath = Path.Combine(sPath, String.Concat("actorthumbs\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                End If
            Next
        End Sub

        Public Sub SaveAllActorThumbs(ByRef DBElement As Database.DBElement)
            If Not DBElement.FilenameSpecified Then Return

            If ActorsSpecified AndAlso Master.eSettings.TVEpisodeActorThumbsAnyEnabled Then
                Images.SaveTVEpisodeActorThumbs(DBElement)
            Else
                'Images.DeleteTVEpisodeActorThumbs(DBElement) 'TODO: find a way to only remove actor thumbs that not needed in other episodes with same actor thumbs path
                DBElement.ActorThumbs.Clear()
            End If
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class EpisodeGuide

#Region "Fields"

        Private _url As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("url")>
        Public Property URL() As String
            Get
                Return _url
            End Get
            Set(ByVal Value As String)
                _url = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _url = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Fanart

#Region "Fields"

        Private _thumb As New List(Of Thumb)
        Private _url As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("thumb")>
        Public Property Thumb() As List(Of Thumb)
            Get
                Return _thumb
            End Get
            Set(ByVal value As List(Of Thumb))
                _thumb = value
            End Set
        End Property

        <XmlAttribute("url")>
        Public Property URL() As String
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _thumb.Clear()
            _url = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    <XmlRoot("fileinfo")>
    Public Class Fileinfo

#Region "Fields"

        Private _streamdetails As New StreamData

#End Region 'Fields

#Region "Properties"

        <XmlElement("streamdetails")>
        Property StreamDetails() As StreamData
            Get
                Return _streamdetails
            End Get
            Set(ByVal value As StreamData)
                _streamdetails = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property StreamDetailsSpecified() As Boolean
            Get
                Return (_streamdetails.Video IsNot Nothing AndAlso _streamdetails.Video.Count > 0) OrElse
                (_streamdetails.Audio IsNot Nothing AndAlso _streamdetails.Audio.Count > 0) OrElse
                (_streamdetails.Subtitle IsNot Nothing AndAlso _streamdetails.Subtitle.Count > 0)
            End Get
        End Property

#End Region 'Properties

    End Class

    <Serializable()>
    <XmlRoot("movie")>
    Public Class Movie
        Implements ICloneable
        Implements IComparable(Of Movie)

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _actors As New List(Of Person)
        Private _certifications As New List(Of String)
        Private _countries As New List(Of String)
        Private _credits As New List(Of String)
        Private _dateadded As String
        Private _datemodified As String
        Private _directors As New List(Of String)
        Private _fanart As New Fanart
        Private _fileInfo As New Fileinfo
        Private _genres As New List(Of String)
        Private _imdb As String
        Private _language As String
        Private _lastplayed As String
        Private _lev As Integer
        Private _locked As Boolean
        Private _mpaa As String
        Private _originaltitle As String
        Private _outline As String
        Private _playcount As Integer
        Private _plot As String
        Private _rating As String
        Private _releaseDate As String
        Private _runtime As String
        Private _scrapersource As String
        Private _sets As New List(Of SetDetails)
        Private _sorttitle As String
        Private _studios As New List(Of String)
        Private _tagline As String
        Private _tags As New List(Of String)
        Private _thumb As New List(Of String)
        Private _thumbposter As New Image
        Private _title As String
        Private _tmdb As String
        Private _tmdbcolid As String
        Private _top250 As Integer
        Private _trailer As String
        Private _uniqueids As New List(Of UniqueId)
        Private _userrating As Integer
        Private _videosource As String
        Private _votes As String
        Private _year As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

        Public Sub New(ByVal strIMDB As String, ByVal strTitle As String, ByVal strYear As String, ByVal iLev As Integer)
            Clear()
            _imdb = strIMDB
            _title = strTitle
            _year = strYear
            _lev = iLev
        End Sub

#End Region 'Constructors

#Region "Properties"
        <XmlElement("id")>
        Public Property IMDB() As String
            Get
                Return _imdb
            End Get
            Set(ByVal value As String)
                _imdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb)
            End Get
        End Property

        <XmlElement("tmdb")>
        Public Property TMDB() As String
            Get
                Return _tmdb
            End Get
            Set(ByVal value As String)
                _tmdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property


        <XmlIgnore()>
        Public ReadOnly Property AnyUniqueIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb) OrElse Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

        <XmlElement("tmdbcolid")>
        Public Property TMDBColID() As String
            Get
                Return _tmdbcolid
            End Get
            Set(ByVal value As String)
                _tmdbcolid = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBColIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdbcolid)
            End Get
        End Property

        <XmlElement("uniqueid")>
        Public Property UniqueIDs() As List(Of UniqueId)
            Get
                Return _uniqueids
            End Get
            Set(ByVal value As List(Of UniqueId))
                If value Is Nothing Then
                    _uniqueids.Clear()
                Else
                    _uniqueids = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property UniqueIDsSpecified() As Boolean
            Get
                Return _uniqueids.Count > 0
            End Get
        End Property

        <XmlElement("title")>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_title)
            End Get
        End Property

        <XmlElement("originaltitle")>
        Public Property OriginalTitle() As String
            Get
                Return _originaltitle
            End Get
            Set(ByVal value As String)
                _originaltitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OriginalTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_originaltitle)
            End Get
        End Property

        <XmlElement("sorttitle")>
        Public Property SortTitle() As String
            Get
                Return _sorttitle
            End Get
            Set(ByVal value As String)
                _sorttitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SortTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_sorttitle)
            End Get
        End Property

        <XmlElement("language")>
        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal value As String)
                _language = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        <XmlElement("year")>
        Public Property Year() As String
            Get
                Return _year
            End Get
            Set(ByVal value As String)
                _year = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property YearSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_year)
            End Get
        End Property

        <XmlElement("releasedate")>
        Public Property ReleaseDate() As String
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As String)
                _releaseDate = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ReleaseDateSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_releaseDate)
            End Get
        End Property

        <XmlElement("top250")>
        Public Property Top250() As Integer
            Get
                Return _top250
            End Get
            Set(ByVal value As Integer)
                _top250 = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property Top250Specified() As Boolean
            Get
                Return _top250 > 0
            End Get
        End Property

        <XmlElement("country")>
        Public Property Countries() As List(Of String)
            Get
                Return _countries
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _countries.Clear()
                Else
                    _countries = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CountriesSpecified() As Boolean
            Get
                Return _countries.Count > 0
            End Get
        End Property

        <XmlElement("rating")>
        Public Property Rating() As String
            Get
                Return _rating.Replace(",", ".")
            End Get
            Set(ByVal value As String)
                _rating = value.Replace(",", ".")
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_rating) AndAlso Not String.IsNullOrEmpty(_votes)
            End Get
        End Property

        <XmlElement("votes")>
        Public Property Votes() As String
            Get
                Return _votes
            End Get
            Set(ByVal value As String)
                _votes = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property VotesSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_votes) AndAlso Not String.IsNullOrEmpty(_rating)
            End Get
        End Property

        <XmlElement("userrating")>
        Public Property UserRating() As Integer
            Get
                Return _userrating
            End Get
            Set(ByVal value As Integer)
                _userrating = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property UserRatingSpecified() As Boolean
            Get
                Return Not _userrating = 0
            End Get
        End Property

        <XmlElement("mpaa")>
        Public Property MPAA() As String
            Get
                Return _mpaa
            End Get
            Set(ByVal value As String)
                _mpaa = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property MPAASpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_mpaa)
            End Get
        End Property

        <XmlElement("certification")>
        Public Property Certifications() As List(Of String)
            Get
                Return _certifications
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _certifications.Clear()
                Else
                    _certifications = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CertificationsSpecified() As Boolean
            Get
                Return _certifications.Count > 0
            End Get
        End Property

        <XmlElement("tag")>
        Public Property Tags() As List(Of String)
            Get
                Return _tags
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _tags.Clear()
                Else
                    _tags = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TagsSpecified() As Boolean
            Get
                Return _tags.Count > 0
            End Get
        End Property

        <XmlElement("genre")>
        Public Property Genres() As List(Of String)
            Get
                Return _genres
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _genres.Clear()
                Else
                    _genres = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property GenresSpecified() As Boolean
            Get
                Return _genres.Count > 0
            End Get
        End Property

        <XmlElement("studio")>
        Public Property Studios() As List(Of String)
            Get
                Return _studios
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _studios.Clear()
                Else
                    _studios = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property StudiosSpecified() As Boolean
            Get
                Return _studios.Count > 0
            End Get
        End Property

        <XmlElement("director")>
        Public Property Directors() As List(Of String)
            Get
                Return _directors
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _directors.Clear()
                Else
                    _directors = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DirectorsSpecified() As Boolean
            Get
                Return _directors.Count > 0
            End Get
        End Property

        <XmlElement("credits")>
        Public Property Credits() As List(Of String)
            Get
                Return _credits
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _credits.Clear()
                Else
                    _credits = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CreditsSpecified() As Boolean
            Get
                Return _credits.Count > 0
            End Get
        End Property

        <XmlElement("tagline")>
        Public Property Tagline() As String
            Get
                Return _tagline.Trim
            End Get
            Set(ByVal value As String)
                _tagline = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TaglineSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tagline)
            End Get
        End Property

        <XmlIgnore()>
        Public Property Scrapersource() As String
            Get
                Return _scrapersource
            End Get
            Set(ByVal value As String)
                _scrapersource = value
            End Set
        End Property

        <XmlElement("outline")>
        Public Property Outline() As String
            Get
                Return _outline.Trim
            End Get
            Set(ByVal value As String)
                _outline = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OutlineSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_outline)
            End Get
        End Property

        <XmlElement("plot")>
        Public Property Plot() As String
            Get
                Return _plot.Trim
            End Get
            Set(ByVal value As String)
                _plot = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_plot)
            End Get
        End Property

        <XmlElement("runtime")>
        Public Property Runtime() As String
            Get
                Return _runtime
            End Get
            Set(ByVal value As String)
                _runtime = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RuntimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_runtime) AndAlso Not _runtime = "0"
            End Get
        End Property

        <XmlElement("trailer")>
        Public Property Trailer() As String
            Get
                Return _trailer
            End Get
            Set(ByVal value As String)
                _trailer = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TrailerSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_trailer)
            End Get
        End Property

        <XmlElement("playcount")>
        Public Property PlayCount() As Integer
            Get
                Return _playcount
            End Get
            Set(ByVal value As Integer)
                _playcount = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlayCountSpecified() As Boolean
            Get
                Return _playcount > 0
            End Get
        End Property

        <XmlElement("lastplayed")>
        Public Property LastPlayed() As String
            Get
                Return _lastplayed
            End Get
            Set(ByVal value As String)
                _lastplayed = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LastPlayedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_lastplayed)
            End Get
        End Property

        <XmlElement("dateadded")>
        Public Property DateAdded() As String
            Get
                Return _dateadded
            End Get
            Set(ByVal value As String)
                _dateadded = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DateAddedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_dateadded)
            End Get
        End Property

        <XmlElement("datemodified")>
        Public Property DateModified() As String
            Get
                Return _datemodified
            End Get
            Set(ByVal value As String)
                _datemodified = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DateModifiedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_datemodified)
            End Get
        End Property

        <XmlElement("actor")>
        Public Property Actors() As List(Of Person)
            Get
                Return _actors
            End Get
            Set(ByVal Value As List(Of Person))
                _actors = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ActorsSpecified() As Boolean
            Get
                Return _actors.Count > 0
            End Get
        End Property

        <XmlElement("thumb")>
        Public Property Thumb() As List(Of String)
            Get
                Return _thumb
            End Get
            Set(ByVal value As List(Of String))
                _thumb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ThumbSpecified() As Boolean
            Get
                Return _thumb.Count > 0
            End Get
        End Property
        ''' <summary>
        ''' Poster Thumb for preview in search results
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlIgnore()>
        Public Property ThumbPoster() As Image
            Get
                Return _thumbposter
            End Get
            Set(ByVal value As Image)
                _thumbposter = value
            End Set
        End Property

        <XmlElement("fanart")>
        Public Property Fanart() As Fanart
            Get
                Return _fanart
            End Get
            Set(ByVal value As Fanart)
                _fanart = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property FanartSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_fanart.URL)
            End Get
        End Property

        <XmlIgnore()>
        Public Property Sets() As List(Of SetDetails)
            Get
                Return _sets
            End Get
            Set(value As List(Of SetDetails))
                AddSet(value)
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SetsSpecified() As Boolean
            Get
                Return Sets.Count > 0
            End Get
        End Property

        <XmlAnyElement("set")>
        Public Property Set_Kodi() As Object
            Get
                Return CreateSetNode()
            End Get
            Set(ByVal value As Object)
                AddSet(value)
            End Set
        End Property

        <XmlElement("sets")>
        Public Property Sets_YAMJ() As SetContainer
            Get
                Return CreateSetYAMJ()
            End Get
            Set(ByVal value As SetContainer)
                AddSet(value)
            End Set
        End Property

        <XmlElement("fileinfo")>
        Public Property FileInfo() As Fileinfo
            Get
                Return _fileInfo
            End Get
            Set(ByVal value As Fileinfo)
                _fileInfo = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property FileInfoSpecified() As Boolean
            Get
                If _fileInfo.StreamDetails.Video IsNot Nothing AndAlso
                (_fileInfo.StreamDetails.Video.Count > 0 OrElse
                 _fileInfo.StreamDetails.Audio.Count > 0 OrElse
                 _fileInfo.StreamDetails.Subtitle.Count > 0) Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        <XmlIgnore()>
        Public Property Lev() As Integer
            Get
                Return _lev
            End Get
            Set(ByVal value As Integer)
                _lev = value
            End Set
        End Property

        <XmlElement("videosource")>
        Public Property VideoSource() As String
            Get
                Return _videosource
            End Get
            Set(ByVal value As String)
                _videosource = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property VideoSourceSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_videosource)
            End Get
        End Property

        <XmlElement("locked")>
        Public Property Locked() As Boolean
            Get
                Return _locked
            End Get
            Set(ByVal value As Boolean)
                _locked = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub AddSet(ByVal tSetDetails As SetDetails)
            If tSetDetails IsNot Nothing AndAlso tSetDetails.TitleSpecified Then
                Dim tSet = From bSet As SetDetails In Sets Where bSet.ID = tSetDetails.ID
                Dim iSet = From bset As SetDetails In Sets Where bset.TMDB = tSetDetails.TMDB

                If tSet.Count > 0 Then
                    _sets.Remove(tSet(0))
                End If

                If iSet.Count > 0 Then
                    _sets.Remove(iSet(0))
                End If

                _sets.Add(tSetDetails)
            End If
        End Sub
        ''' <summary>
        ''' converts both versions of moviesets declaration in movie.nfo to a proper Sets object
        ''' </summary>
        ''' <remarks>      
        ''' <set>setname</set>        
        ''' 
        ''' <set>
        '''     <name>setname</name>
        '''     <overview>plot</overview>
        ''' </set>       
        ''' </remarks>
        ''' <param name="xmlObject"></param>
        Public Sub AddSet(ByVal xmlObject As Object)
            Try
                If xmlObject IsNot Nothing AndAlso TryCast(xmlObject, XmlElement) IsNot Nothing Then
                    Dim nSetInfo As New SetDetails
                    Dim xElement As XmlElement = CType(xmlObject, XmlElement)
                    For Each xChild In xElement.ChildNodes
                        Dim xNode = CType(xChild, XmlNode)
                        Select Case xNode.NodeType
                            Case XmlNodeType.Element
                                Select Case xNode.Name
                                    Case "name"
                                        nSetInfo.Title = xNode.InnerText
                                    Case "overview"
                                        nSetInfo.Plot = xNode.InnerText
                                    Case "tmdb"
                                        nSetInfo.TMDB = xNode.InnerText
                                End Select
                            Case XmlNodeType.Text
                                nSetInfo.Title = xNode.InnerText
                        End Select
                    Next
                    If nSetInfo.TitleSpecified Then AddSet(nSetInfo)
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Sub AddSet(ByVal tSetContainer As SetContainer)
            If tSetContainer IsNot Nothing AndAlso tSetContainer.SetsSpecified Then
                For Each xSetDetail As SetDetails In tSetContainer.Sets.Where(Function(f) f.TitleSpecified)
                    AddSet(xSetDetail)
                Next
            End If
        End Sub

        Public Sub AddTag(ByVal value As String)
            If String.IsNullOrEmpty(value) Then Return
            If Not _tags.Contains(value) Then
                _tags.Add(value.Trim)
            End If
        End Sub

        Public Sub AddCertificationsFromString(ByVal value As String)
            _certifications.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each certification As String In values
                    certification = certification.Trim
                    If Not _certifications.Contains(certification) Then
                        _certifications.Add(certification)
                    End If
                Next
            Else
                If Not _certifications.Contains(value) Then
                    _certifications.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddGenresFromString(ByVal value As String)
            _genres.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each genre As String In values
                    genre = genre.Trim
                    If Not _genres.Contains(genre) Then
                        _genres.Add(genre)
                    End If
                Next
            Else
                If Not _genres.Contains(value) Then
                    _genres.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddStudiosFromString(ByVal value As String)
            _studios.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each studio As String In values
                    studio = studio.Trim
                    If Not _studios.Contains(studio) Then
                        _studios.Add(studio)
                    End If
                Next
            Else
                If Not _studios.Contains(value) Then
                    _studios.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddDirectorsFromString(ByVal value As String)
            _directors.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each director As String In values
                    director = director.Trim
                    If Not _directors.Contains(director) And Not value = "See more" Then
                        _directors.Add(director)
                    End If
                Next
            Else
                value = value.Trim
                If Not _directors.Contains(value) And Not value = "See more" Then
                    _directors.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddCreditsFromString(ByVal value As String)
            _credits.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each credit As String In values
                    credit = credit.Trim
                    If Not _credits.Contains(credit) And Not value = "See more" Then
                        _credits.Add(credit)
                    End If
                Next
            Else
                value = value.Trim
                If Not _credits.Contains(value) And Not value = "See more" Then
                    _credits.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddCountriesFromString(ByVal value As String)
            _countries.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each country As String In values
                    country = country.Trim
                    If Not _countries.Contains(country) Then
                        _countries.Add(country)
                    End If
                Next
            Else
                value = value.Trim
                If Not _countries.Contains(value) Then
                    _countries.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub Clear()
            _actors.Clear()
            _certifications.Clear()
            _countries.Clear()
            _credits.Clear()
            _dateadded = String.Empty
            _datemodified = String.Empty
            _directors.Clear()
            _fanart = New Fanart
            _fileInfo = New Fileinfo
            _genres.Clear()
            _imdb = String.Empty
            _language = String.Empty
            _lev = 0
            _locked = False
            _mpaa = String.Empty
            _originaltitle = String.Empty
            _outline = String.Empty
            _playcount = 0
            _plot = String.Empty
            _rating = String.Empty
            _releaseDate = String.Empty
            _runtime = String.Empty
            _scrapersource = String.Empty
            _sets.Clear()
            _sorttitle = String.Empty
            _studios.Clear()
            _tagline = String.Empty
            _tags.Clear()
            _thumb.Clear()
            _thumbposter = New Image
            _title = String.Empty
            _tmdb = String.Empty
            _tmdbcolid = String.Empty
            _top250 = 0
            _trailer = String.Empty
            _uniqueids.Clear()
            _userrating = 0
            _videosource = String.Empty
            _votes = String.Empty
            _year = String.Empty
        End Sub

        Public Function CloneDeep() As Object Implements ICloneable.Clone
            Dim Stream As New MemoryStream(50000)
            Dim Formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            ' Serialisierung über alle Objekte hinweg in einen Stream 
            Formatter.Serialize(Stream, Me)
            ' Zurück zum Anfang des Streams und... 
            Stream.Seek(0, SeekOrigin.Begin)
            ' ...aus dem Stream in ein Objekt deserialisieren 
            CloneDeep = Formatter.Deserialize(Stream)
            Stream.Close()
        End Function

        Public Sub CreateCachePaths_ActorsThumbs()
            Dim sPath As String = Path.Combine(Master.TempPath, "Global")

            For Each tActor As Person In Actors
                tActor.Thumb.CacheOriginalPath = Path.Combine(sPath, String.Concat("actorthumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                If Not String.IsNullOrEmpty(tActor.Thumb.URLThumb) Then
                    tActor.Thumb.CacheThumbPath = Path.Combine(sPath, String.Concat("actorthumbs\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                End If
            Next
        End Sub

        Public Function CreateSetNode() As XmlDocument
            If _sets.Count > 0 AndAlso _sets.Item(0).TitleSpecified Then
                Dim firstSet As SetDetails = _sets.Item(0)

                If Master.eSettings.MovieScraperCollectionsExtendedInfo Then
                    'creates a set node like:
                    '<set> 
                    '  <name>Die Hard Collection</name>
                    '  <overview>Hardest cop ever!</overview>
                    '  <tmdb>1570</tmdb>
                    '</set>

                    Dim XmlDoc As New XmlDocument

                    'Write down the XML declaration
                    Dim XmlDeclaration As XmlDeclaration = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)

                    'Create the root element
                    Dim RootNode As XmlElement = XmlDoc.CreateElement("set")
                    XmlDoc.InsertBefore(XmlDeclaration, XmlDoc.DocumentElement)
                    XmlDoc.AppendChild(RootNode)

                    'Create a new <name> element and add it to the root node
                    Dim NodeName As XmlElement = XmlDoc.CreateElement("name")
                    RootNode.AppendChild(NodeName)
                    Dim NodeName_Text As XmlText = XmlDoc.CreateTextNode(firstSet.Title)
                    NodeName.AppendChild(NodeName_Text)

                    If firstSet.PlotSpecified Then
                        'Create a new <overview> element and add it to the root node
                        Dim NodeOverview As XmlElement = XmlDoc.CreateElement("overview")
                        RootNode.AppendChild(NodeOverview)
                        Dim NodeOverview_Text As XmlText = XmlDoc.CreateTextNode(firstSet.Plot)
                        NodeOverview.AppendChild(NodeOverview_Text)
                    End If

                    If firstSet.TMDBSpecified Then
                        'Create a new <tmdb> element and add it to the root node
                        Dim NodeTMDB As XmlElement = XmlDoc.CreateElement("tmdb")
                        RootNode.AppendChild(NodeTMDB)
                        Dim NodeTMDB_Text As XmlText = XmlDoc.CreateTextNode(firstSet.TMDB)
                        NodeTMDB.AppendChild(NodeTMDB_Text)
                    End If

                    Return XmlDoc
                Else
                    'creates a set node like:
                    '<set>Die Hard Collection</set>

                    Dim XmlDoc As New XmlDocument

                    'Write down the XML declaration
                    Dim XmlDeclaration As XmlDeclaration = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)

                    'Create the root element
                    Dim RootNode As XmlElement = XmlDoc.CreateElement("set")
                    XmlDoc.InsertBefore(XmlDeclaration, XmlDoc.DocumentElement)
                    XmlDoc.AppendChild(RootNode)
                    Dim RootNode_Text As XmlText = XmlDoc.CreateTextNode(firstSet.Title)
                    RootNode.AppendChild(RootNode_Text)

                    Return XmlDoc
                End If
            Else
                Return Nothing
            End If
        End Function

        Public Function CreateSetYAMJ() As SetContainer
            If Master.eSettings.MovieScraperCollectionsYAMJCompatibleSets AndAlso Sets.Count > 0 Then
                Return New SetContainer With {.Sets = Sets}
            Else
                Return Nothing
            End If
        End Function

        Public Function CompareTo(ByVal other As Movie) As Integer Implements IComparable(Of Movie).CompareTo
            Dim retVal As Integer = (Lev).CompareTo(other.Lev)
            If retVal = 0 Then
                retVal = (Year).CompareTo(other.Year) * -1
            End If
            Return retVal
        End Function

        Public Sub RemoveSet(ByVal SetName As String)
            Dim tSet = From bSet As SetDetails In Sets Where bSet.Title = SetName
            If tSet.Count > 0 Then
                Sets.Remove(tSet(0))
            End If
        End Sub

        Public Sub RemoveSet(ByVal SetID As Long)
            Dim tSet = From bSet As SetDetails In Sets Where bSet.ID = SetID
            If tSet.Count > 0 Then
                Sets.Remove(tSet(0))
            End If
        End Sub

        Public Sub SaveAllActorThumbs(ByRef DBElement As Database.DBElement)
            If ActorsSpecified AndAlso Master.eSettings.MovieActorThumbsAnyEnabled Then
                Images.SaveMovieActorThumbs(DBElement)
            Else
                Images.Delete_Movie(DBElement, Enums.ModifierType.MainActorThumbs, False)
                DBElement.ActorThumbs.Clear()
            End If
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class MovieInSet
        Implements IComparable(Of MovieInSet)

#Region "Fields"

        Private _dbmovie As Database.DBElement
        Private _order As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property DBMovie() As Database.DBElement
            Get
                Return _dbmovie
            End Get
            Set(ByVal value As Database.DBElement)
                _dbmovie = value
            End Set
        End Property

        Public ReadOnly Property ListTitle() As String
            Get
                Return _dbmovie.ListTitle
            End Get
        End Property

        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal value As Integer)
                _order = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _dbmovie = New Database.DBElement(Enums.ContentType.Movie)
            _order = 0
        End Sub

        Public Function CompareTo(ByVal other As MovieInSet) As Integer Implements IComparable(Of MovieInSet).CompareTo
            Return (Order).CompareTo(other.Order)
        End Function

#End Region 'Methods

    End Class

    <Serializable()>
    <XmlRoot("movieset")>
    Public Class MovieSet

#Region "Fields"

        Private _language As String
        Private _locked As Boolean
        Private _oldtitle As String
        Private _plot As String
        Private _title As String
        Private _tmdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal strID As String, ByVal strTitle As String, ByVal strPlot As String)
            Clear()
            _plot = strPlot
            _title = strTitle
            _tmdb = strID
        End Sub

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("title")>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_title)
            End Get
        End Property

        <XmlElement("id")>
        Public Property TMDB() As String
            Get
                Return _tmdb.Trim
            End Get
            Set(ByVal value As String)
                _tmdb = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

        <XmlElement("plot")>
        Public Property Plot() As String
            Get
                Return _plot.Trim
            End Get
            Set(ByVal value As String)
                _plot = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_plot)
            End Get
        End Property

        <XmlElement("language")>
        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal value As String)
                _language = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        <XmlElement("locked")>
        Public Property Locked() As Boolean
            Get
                Return _locked
            End Get
            Set(ByVal value As Boolean)
                _locked = value
            End Set
        End Property
        ''' <summary>
        ''' Old Title before edit or scraping. Needed to remove no longer valid images and NFO.
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore()>
        Public Property OldTitle() As String
            Get
                Return _oldtitle
            End Get
            Set(ByVal value As String)
                _oldtitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AnyUniqueIDSpecified() As Boolean
            Get
                Return TMDBSpecified
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleHasChanged() As Boolean
            Get
                Return Not _oldtitle = _title
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _locked = False
            _oldtitle = String.Empty
            _plot = String.Empty
            _title = String.Empty
            _tmdb = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Person

#Region "Fields"

        Private _id As Long
        Private _imdb As String
        Private _name As String
        Private _order As Integer
        Private _role As String
        Private _thumb As Image
        Private _tmdb As String
        Private _tvdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clean()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlIgnore()>
        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal Value As Long)
                _id = Value
            End Set
        End Property

        <XmlElement("name")>
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_name)
            End Get
        End Property

        <XmlElement("role")>
        Public Property Role() As String
            Get
                Return _role
            End Get
            Set(ByVal Value As String)
                _role = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RoleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_role)
            End Get
        End Property

        <XmlElement("order")>
        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal Value As Integer)
                _order = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OrderSpecified() As Boolean
            Get
                Return _order > -1
            End Get
        End Property

        <XmlIgnore()>
        Public Property Thumb() As Image
            Get
                Return _thumb
            End Get
            Set(ByVal Value As Image)
                _thumb = Value
            End Set
        End Property

        <XmlElement("thumb")>
        Public Property URLOriginal() As String
            Get
                Return _thumb.URLOriginal
            End Get
            Set(ByVal Value As String)
                _thumb.URLOriginal = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property URLOriginalSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_thumb.URLOriginal)
            End Get
        End Property

        <XmlIgnore()>
        Public Property LocalFilePath() As String
            Get
                Return _thumb.LocalFilePath
            End Get
            Set(ByVal Value As String)
                _thumb.LocalFilePath = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LocalFilePathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_thumb.LocalFilePath)
            End Get
        End Property

        <XmlElement("imdbid")>
        Public Property IMDB() As String
            Get
                Return _imdb
            End Get
            Set(ByVal Value As String)
                _imdb = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb)
            End Get
        End Property

        <XmlElement("tmdbid")>
        Public Property TMDB() As String
            Get
                Return _tmdb
            End Get
            Set(ByVal Value As String)
                _tmdb = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

        <XmlElement("tvdbid")>
        Public Property TVDB() As String
            Get
                Return _tvdb
            End Get
            Set(ByVal Value As String)
                _tvdb = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clean()
            _id = -1
            _imdb = String.Empty
            _name = String.Empty
            _order = -1
            _role = String.Empty
            _thumb = New Image
            _tmdb = String.Empty
            _tvdb = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    <XmlRoot("seasondetails")>
    Public Class SeasonDetails

#Region "Fields"

        Private _aired As String
        Private _locked As Boolean
        Private _plot As String
        Private _scrapersource As String
        Private _season As Integer
        Private _title As String
        Private _tmdb As String
        Private _tvdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("aired")>
        Public Property Aired() As String
            Get
                Return _aired
            End Get
            Set(ByVal value As String)
                _aired = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AiredSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_aired)
            End Get
        End Property

        <XmlIgnore()>
        Public Property Scrapersource() As String
            Get
                Return _scrapersource
            End Get
            Set(ByVal value As String)
                _scrapersource = value
            End Set
        End Property

        <XmlElement("season")>
        Public Property Season() As Integer
            Get
                Return _season
            End Get
            Set(ByVal value As Integer)
                _season = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SeasonSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_season.ToString) AndAlso Not _season = -1
            End Get
        End Property

        <XmlElement("title")>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_title)
            End Get
        End Property

        <XmlElement("plot")>
        Public Property Plot() As String
            Get
                Return _plot.Trim
            End Get
            Set(ByVal value As String)
                _plot = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_plot)
            End Get
        End Property

        <XmlElement("tmdb")>
        Public Property TMDB() As String
            Get
                Return _tmdb
            End Get
            Set(ByVal value As String)
                _tmdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

        <XmlElement("tvdb")>
        Public Property TVDB() As String
            Get
                Return _tvdb
            End Get
            Set(ByVal value As String)
                _tvdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

        <XmlElement("locked")>
        Public Property Locked() As Boolean
            Get
                Return _locked
            End Get
            Set(ByVal value As Boolean)
                _locked = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AnyUniqueIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb) OrElse Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _aired = String.Empty
            _locked = False
            _plot = String.Empty
            _scrapersource = String.Empty
            _season = -1
            _tmdb = String.Empty
            _tvdb = String.Empty
            _title = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    <XmlRoot("seasons")>
    Public Class Seasons

#Region "Fields"

        Private _seasons As New List(Of SeasonDetails)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("seasondetails")>
        Public Property Seasons() As List(Of SeasonDetails)
            Get
                Return _seasons
            End Get
            Set(ByVal value As List(Of SeasonDetails))
                _seasons = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SeasonsSpecified() As Boolean
            Get
                Return _seasons.Count > 0
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _seasons.Clear()
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Thumb

#Region "Fields"

        Private _preview As String
        Private _text As String

#End Region 'Fields

#Region "Properties"

        <XmlAttribute("preview")>
        Public Property Preview() As String
            Get
                Return _preview
            End Get
            Set(ByVal Value As String)
                _preview = Value
            End Set
        End Property

        <XmlText()>
        Public Property [Text]() As String
            Get
                Return _text
            End Get
            Set(ByVal Value As String)
                _text = Value
            End Set
        End Property

#End Region 'Properties

    End Class

    <Serializable()>
    <XmlRoot("tvshow")>
    Public Class TVShow
        Implements ICloneable

#Region "Fields"

        Private _actors As New List(Of Person)
        Private _boxeeTvDb As String
        Private _certifications As New List(Of String)
        Private _countries As New List(Of String)
        Private _creators As New List(Of String)
        Private _directors As New List(Of String)
        Private _episodeguide As New EpisodeGuide
        Private _genres As New List(Of String)
        Private _imdb As String
        Private _knownepisodes As New List(Of EpisodeDetails)
        Private _knownseasons As New List(Of SeasonDetails)
        Private _language As String
        Private _locked As Boolean
        Private _mpaa As String
        Private _originaltitle As String
        Private _plot As String
        Private _premiered As String
        Private _rating As String
        Private _runtime As String
        Private _scrapersource As String
        Private _seasons As New Seasons
        Private _sorttitle As String
        Private _status As String
        Private _studios As New List(Of String)
        Private _tags As New List(Of String)
        Private _title As String
        Private _tmdb As String
        Private _tvdb As String
        Private _uniqueids As New List(Of Uniqueid)
        Private _userrating As Integer
        Private _votes As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sTVDBID As String, ByVal sTitle As String, ByVal sPremiered As String)
            Clear()
            _tvdb = sTVDBID
            _title = sTitle
            _premiered = sPremiered
        End Sub

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("id")>
        Public Property TVDB() As String
            Get
                Return _tvdb.Trim
            End Get
            Set(ByVal value As String)
                If Integer.TryParse(value, 0) Then _tvdb = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

        <XmlElement("imdb")>
        Public Property IMDB() As String
            Get
                Return _imdb
            End Get
            Set(ByVal value As String)
                _imdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb)
            End Get
        End Property

        <XmlElement("tmdb")>
        Public Property TMDB() As String
            Get
                Return _tmdb
            End Get
            Set(ByVal value As String)
                _tmdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property AnyUniqueIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_imdb) OrElse Not String.IsNullOrEmpty(_tmdb) OrElse Not String.IsNullOrEmpty(_tvdb)
            End Get
        End Property

        <XmlElement("uniqueid")>
        Public Property UniqueIDs() As List(Of UniqueId)
            Get
                Return _uniqueids
            End Get
            Set(ByVal value As List(Of UniqueId))
                If value Is Nothing Then
                    _uniqueids.Clear()
                Else
                    _uniqueids = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property UniqueIDsSpecified() As Boolean
            Get
                Return _uniqueids.Count > 0
            End Get
        End Property

        <XmlElement("title")>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_title)
            End Get
        End Property

        <XmlElement("originaltitle")>
        Public Property OriginalTitle() As String
            Get
                Return _originaltitle
            End Get
            Set(ByVal value As String)
                _originaltitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OriginalTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_originaltitle)
            End Get
        End Property

        <XmlElement("sorttitle")>
        Public Property SortTitle() As String
            Get
                Return _sorttitle
            End Get
            Set(ByVal value As String)
                _sorttitle = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SortTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_sorttitle)
            End Get
        End Property

        <XmlElement("language")>
        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal value As String)
                _language = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        <XmlElement("boxeeTvDb")>
        Public Property BoxeeTvDb() As String
            Get
                Return _boxeeTvDb
            End Get
            Set(ByVal value As String)
                If Integer.TryParse(value, 0) Then _boxeeTvDb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property BoxeeTvDbSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_boxeeTvDb)
            End Get
        End Property

        <XmlElement("episodeguide")>
        Public Property EpisodeGuide() As EpisodeGuide
            Get
                Return _episodeguide
            End Get
            Set(ByVal value As EpisodeGuide)
                _episodeguide = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property EpisodeGuideSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_episodeguide.URL)
            End Get
        End Property

        <XmlElement("rating")>
        Public Property Rating() As String
            Get
                Return _rating.Replace(",", ".")
            End Get
            Set(ByVal value As String)
                _rating = value.Replace(",", ".")
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_rating) AndAlso Not String.IsNullOrEmpty(_votes)
            End Get
        End Property

        <XmlElement("votes")>
        Public Property Votes() As String
            Get
                Return _votes
            End Get
            Set(ByVal value As String)
                _votes = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property VotesSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_votes) AndAlso Not String.IsNullOrEmpty(_rating)
            End Get
        End Property

        <XmlElement("userrating")>
        Public Property UserRating() As Integer
            Get
                Return _userrating
            End Get
            Set(ByVal value As Integer)
                _userrating = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property UserRatingSpecified() As Boolean
            Get
                Return Not _userrating = 0
            End Get
        End Property

        <XmlElement("genre")>
        Public Property Genres() As List(Of String)
            Get
                Return _genres
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _genres.Clear()
                Else
                    _genres = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property GenresSpecified() As Boolean
            Get
                Return _genres.Count > 0
            End Get
        End Property

        <XmlElement("director")>
        Public Property Directors() As List(Of String)
            Get
                Return _directors
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _directors.Clear()
                Else
                    _directors = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DirectorsSpecified() As Boolean
            Get
                Return _directors.Count > 0
            End Get
        End Property

        <XmlElement("mpaa")>
        Public Property MPAA() As String
            Get
                Return _mpaa
            End Get
            Set(ByVal value As String)
                _mpaa = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property MPAASpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_mpaa)
            End Get
        End Property

        <XmlElement("certification")>
        Public Property Certifications() As List(Of String)
            Get
                Return _certifications
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _certifications.Clear()
                Else
                    _certifications = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CertificationsSpecified() As Boolean
            Get
                Return _certifications.Count > 0
            End Get
        End Property

        <XmlElement("country")>
        Public Property Countries() As List(Of String)
            Get
                Return _countries
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _countries.Clear()
                Else
                    _countries = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CountriesSpecified() As Boolean
            Get
                Return _countries.Count > 0
            End Get
        End Property

        <XmlElement("premiered")>
        Public Property Premiered() As String
            Get
                Return _premiered
            End Get
            Set(ByVal value As String)
                _premiered = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PremieredSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_premiered)
            End Get
        End Property

        <XmlElement("studio")>
        Public Property Studios() As List(Of String)
            Get
                Return _studios
            End Get
            Set(ByVal value As List(Of String))
                _studios = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property StudiosSpecified() As Boolean
            Get
                Return _studios.Count > 0
            End Get
        End Property

        <XmlElement("status")>
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property StatusSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_status)
            End Get
        End Property

        <XmlElement("plot")>
        Public Property Plot() As String
            Get
                Return _plot.Trim
            End Get
            Set(ByVal value As String)
                _plot = value.Trim
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_plot)
            End Get
        End Property

        <XmlElement("tag")>
        Public Property Tags() As List(Of String)
            Get
                Return _tags
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _tags.Clear()
                Else
                    _tags = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TagsSpecified() As Boolean
            Get
                Return _tags.Count > 0
            End Get
        End Property

        <XmlElement("runtime")>
        Public Property Runtime() As String
            Get
                Return _runtime
            End Get
            Set(ByVal value As String)
                _runtime = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property RuntimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_runtime) AndAlso Not _runtime = "0"
            End Get
        End Property

        <XmlElement("actor")>
        Public Property Actors() As List(Of Person)
            Get
                Return _actors
            End Get
            Set(ByVal Value As List(Of Person))
                _actors = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ActorsSpecified() As Boolean
            Get
                Return _actors.Count > 0
            End Get
        End Property

        <XmlIgnore()>
        Public Property Scrapersource() As String
            Get
                Return _scrapersource
            End Get
            Set(ByVal value As String)
                _scrapersource = value
            End Set
        End Property

        <XmlElement("creator")>
        Public Property Creators() As List(Of String)
            Get
                Return _creators
            End Get
            Set(ByVal value As List(Of String))
                If value Is Nothing Then
                    _creators.Clear()
                Else
                    _creators = value
                End If
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property CreatorsSpecified() As Boolean
            Get
                Return _creators.Count > 0
            End Get
        End Property

        <XmlElement("seasons")>
        Public Property Seasons() As Seasons
            Get
                Return _seasons
            End Get
            Set(ByVal value As Seasons)
                _seasons = value
            End Set
        End Property

        <XmlElement("locked")>
        Public Property Locked() As Boolean
            Get
                Return _locked
            End Get
            Set(ByVal value As Boolean)
                _locked = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SeasonsSpecified() As Boolean
            Get
                Return _seasons.Seasons.Count > 0
            End Get
        End Property

        <XmlIgnore()>
        Public Property KnownEpisodes() As List(Of EpisodeDetails)
            Get
                Return _knownepisodes
            End Get
            Set(ByVal value As List(Of EpisodeDetails))
                _knownepisodes = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property KnownEpisodesSpecified() As Boolean
            Get
                Return _knownepisodes.Count > 0
            End Get
        End Property

        <XmlIgnore()>
        Public Property KnownSeasons() As List(Of SeasonDetails)
            Get
                Return _knownseasons
            End Get
            Set(ByVal value As List(Of SeasonDetails))
                _knownseasons = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property KnownSeasonsSpecified() As Boolean
            Get
                Return _knownseasons.Count > 0
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub AddCertificationsFromString(ByVal value As String)
            _certifications.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each certification As String In values
                    certification = certification.Trim
                    If Not _certifications.Contains(certification) Then
                        _certifications.Add(certification)
                    End If
                Next
            Else
                If Not _certifications.Contains(value) Then
                    _certifications.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddCountriesFromString(ByVal value As String)
            _countries.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains(" / ") Then
                Dim values As String() = Regex.Split(value, " / ")
                For Each country As String In values
                    country = country.Trim
                    If Not _countries.Contains(country) Then
                        _countries.Add(country)
                    End If
                Next
            Else
                value = value.Trim
                If Not _countries.Contains(value) Then
                    _countries.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddGenresFromString(ByVal value As String)
            _genres.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains("/") Then
                Dim values As String() = value.Split(New [Char]() {"/"c})
                For Each genre As String In values
                    genre = genre.Trim
                    If Not _genres.Contains(genre) Then
                        _genres.Add(genre)
                    End If
                Next
            Else
                If Not _genres.Contains(value) Then
                    _genres.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub AddStudiosFromString(ByVal value As String)
            _studios.Clear()
            If String.IsNullOrEmpty(value) Then Return

            If value.Contains("/") Then
                Dim values As String() = value.Split(New [Char]() {"/"c})
                For Each studio As String In values
                    studio = studio.Trim
                    If Not _studios.Contains(studio) Then
                        _studios.Add(studio)
                    End If
                Next
            Else
                If Not _studios.Contains(value) Then
                    _studios.Add(value.Trim)
                End If
            End If
        End Sub

        Public Sub Clear()
            _actors.Clear()
            _boxeeTvDb = String.Empty
            _certifications.Clear()
            _countries.Clear()
            _creators.Clear()
            _directors.Clear()
            _episodeguide.URL = String.Empty
            _genres.Clear()
            _tvdb = String.Empty
            _imdb = String.Empty
            _knownepisodes.Clear()
            _knownseasons.Clear()
            _language = String.Empty
            _locked = False
            _mpaa = String.Empty
            _originaltitle = String.Empty
            _plot = String.Empty
            _premiered = String.Empty
            _rating = String.Empty
            _runtime = String.Empty
            _scrapersource = String.Empty
            _seasons.Clear()
            _sorttitle = String.Empty
            _status = String.Empty
            _studios.Clear()
            _tags.Clear()
            _title = String.Empty
            _tmdb = String.Empty
            _uniqueids.Clear()
            _userrating = 0
            _votes = String.Empty
        End Sub

        Public Function CloneDeep() As Object Implements ICloneable.Clone
            Dim Stream As New MemoryStream(50000)
            Dim Formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            ' Serialisierung über alle Objekte hinweg in einen Stream 
            Formatter.Serialize(Stream, Me)
            ' Zurück zum Anfang des Streams und... 
            Stream.Seek(0, SeekOrigin.Begin)
            ' ...aus dem Stream in ein Objekt deserialisieren 
            CloneDeep = Formatter.Deserialize(Stream)
            Stream.Close()
        End Function

        Public Sub CreateCachePaths_ActorsThumbs()
            Dim sPath As String = Path.Combine(Master.TempPath, "Global")

            For Each tActor As Person In Actors
                tActor.Thumb.CacheOriginalPath = Path.Combine(sPath, String.Concat("actorthumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                If Not String.IsNullOrEmpty(tActor.Thumb.URLThumb) Then
                    tActor.Thumb.CacheThumbPath = Path.Combine(sPath, String.Concat("actorthumbs\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                End If
            Next
        End Sub

        Public Sub BlankId()
            _tvdb = String.Empty
        End Sub

        Public Sub BlankBoxeeId()
            _boxeeTvDb = String.Empty
        End Sub

        Public Sub SaveAllActorThumbs(ByRef DBElement As Database.DBElement)
            If ActorsSpecified AndAlso Master.eSettings.TVShowActorThumbsAnyEnabled Then
                Images.SaveTVShowActorThumbs(DBElement)
            Else
                Images.Delete_TVShow(DBElement, Enums.ModifierType.MainActorThumbs)
                DBElement.ActorThumbs.Clear()
            End If
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class [Image]
        Implements IComparable(Of [Image])

#Region "Fields"

        Private _cacheoriginalpath As String
        Private _cachethumbpath As String
        Private _disc As Integer
        Private _disctype As String
        Private _episode As Integer
        Private _height As String
        Private _imageoriginal As Images
        Private _imagethumb As Images
        Private _index As Integer
        Private _isduplicate As Boolean
        Private _likes As Integer
        Private _localfilepath As String
        Private _longlang As String
        Private _moviebannersize As Enums.MovieBannerSize
        Private _moviefanartsize As Enums.MovieFanartSize
        Private _moviepostersize As Enums.MoviePosterSize
        Private _scraper As String
        Private _season As Integer
        Private _shortlang As String
        Private _tvbannersize As Enums.TVBannerSize
        Private _tvbannertype As Enums.TVBannerType
        Private _tvepisodepostersize As Enums.TVEpisodePosterSize
        Private _tvfanartsize As Enums.TVFanartSize
        Private _tvpostersize As Enums.TVPosterSize
        Private _tvseasonpostersize As Enums.TVSeasonPosterSize
        Private _urloriginal As String
        Private _urlthumb As String
        Private _voteaverage As String
        Private _votecount As Integer
        Private _width As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property CacheOriginalPath() As String
            Get
                Return _cacheoriginalpath
            End Get
            Set(ByVal value As String)
                _cacheoriginalpath = value
            End Set
        End Property

        Public Property CacheThumbPath() As String
            Get
                Return _cachethumbpath
            End Get
            Set(ByVal value As String)
                _cachethumbpath = value
            End Set
        End Property

        Public Property Disc() As Integer
            Get
                Return _disc
            End Get
            Set(ByVal value As Integer)
                _disc = value
            End Set
        End Property

        Public Property DiscType() As String
            Get
                Return _disctype
            End Get
            Set(ByVal value As String)
                _disctype = If(value.ToLower = "3d", "3D", If(value.ToLower = "cd", "CD", If(value.ToLower = "dvd", "DVD", If(value.ToLower = "bluray", "BluRay", value))))
            End Set
        End Property

        Public Property Episode() As Integer
            Get
                Return _episode
            End Get
            Set(ByVal value As Integer)
                _episode = value
            End Set
        End Property

        Public Property Height() As String
            Get
                Return _height
            End Get
            Set(ByVal value As String)
                _height = value
                DetectImageSize(value)
            End Set
        End Property

        Public Property ImageOriginal() As Images
            Get
                Return _imageoriginal
            End Get
            Set(ByVal value As Images)
                _imageoriginal = value
            End Set
        End Property

        Public Property ImageThumb() As Images
            Get
                Return _imagethumb
            End Get
            Set(ByVal value As Images)
                _imagethumb = value
            End Set
        End Property

        Public Property Index() As Integer
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        Public Property IsDuplicate() As Boolean
            Get
                Return _isduplicate
            End Get
            Set(ByVal value As Boolean)
                _isduplicate = value
            End Set
        End Property

        Public Property Likes() As Integer
            Get
                Return _likes
            End Get
            Set(ByVal value As Integer)
                _likes = value
            End Set
        End Property

        Public Property LocalFilePath() As String
            Get
                Return _localfilepath
            End Get
            Set(ByVal value As String)
                _localfilepath = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property LocalFilePathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_localfilepath)
            End Get
        End Property

        Public Property LongLang() As String
            Get
                Return _longlang
            End Get
            Set(ByVal value As String)
                _longlang = value
            End Set
        End Property

        Public ReadOnly Property MovieBannerSize() As Enums.MovieBannerSize
            Get
                Return _moviebannersize
            End Get
        End Property

        Public ReadOnly Property MovieFanartSize() As Enums.MovieFanartSize
            Get
                Return _moviefanartsize
            End Get
        End Property

        Public ReadOnly Property MoviePosterSize() As Enums.MoviePosterSize
            Get
                Return _moviepostersize
            End Get
        End Property

        Public Property Scraper() As String
            Get
                Return _scraper
            End Get
            Set(ByVal value As String)
                _scraper = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return _season
            End Get
            Set(ByVal value As Integer)
                _season = value
            End Set
        End Property

        Public Property ShortLang() As String
            Get
                Return _shortlang
            End Get
            Set(ByVal value As String)
                _shortlang = value
            End Set
        End Property

        Public ReadOnly Property TVBannerSize() As Enums.TVBannerSize
            Get
                Return _tvbannersize
            End Get
        End Property

        Public Property TVBannerType() As Enums.TVBannerType
            Get
                Return _tvbannertype
            End Get
            Set(ByVal value As Enums.TVBannerType)
                _tvbannertype = value
            End Set
        End Property

        Public ReadOnly Property TVEpisodePosterSize() As Enums.TVEpisodePosterSize
            Get
                Return _tvepisodepostersize
            End Get
        End Property

        Public ReadOnly Property TVFanartSize() As Enums.TVFanartSize
            Get
                Return _tvfanartsize
            End Get
        End Property

        Public ReadOnly Property TVPosterSize() As Enums.TVPosterSize
            Get
                Return _tvpostersize
            End Get
        End Property

        Public ReadOnly Property TVSeasonPosterSize() As Enums.TVSeasonPosterSize
            Get
                Return _tvseasonpostersize
            End Get
        End Property

        Public Property URLOriginal() As String
            Get
                Return _urloriginal
            End Get
            Set(ByVal value As String)
                _urloriginal = value
            End Set
        End Property

        Public Property URLThumb() As String
            Get
                Return _urlthumb
            End Get
            Set(ByVal value As String)
                _urlthumb = value
            End Set
        End Property

        Public Property VoteAverage() As String
            Get
                Return _voteaverage
            End Get
            Set(ByVal value As String)
                _voteaverage = value
            End Set
        End Property

        Public Property VoteCount() As Integer
            Get
                Return _votecount
            End Get
            Set(ByVal value As Integer)
                _votecount = value
            End Set
        End Property

        Public Property Width() As String
            Get
                Return _width
            End Get
            Set(ByVal value As String)
                _width = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _cacheoriginalpath = String.Empty
            _cachethumbpath = String.Empty
            _disc = 0
            _disctype = String.Empty
            _episode = -1
            _height = String.Empty
            _imageoriginal = New Images
            _imagethumb = New Images
            _index = 0
            _isduplicate = False
            _likes = 0
            _localfilepath = String.Empty
            _longlang = String.Empty
            _moviebannersize = Enums.MovieBannerSize.Any
            _moviefanartsize = Enums.MovieFanartSize.Any
            _moviepostersize = Enums.MoviePosterSize.Any
            _scraper = String.Empty
            _season = -1
            _shortlang = String.Empty
            _tvbannersize = Enums.TVBannerSize.Any
            _tvbannertype = Enums.TVBannerType.Any
            _tvepisodepostersize = Enums.TVEpisodePosterSize.Any
            _tvfanartsize = Enums.TVFanartSize.Any
            _tvpostersize = Enums.TVPosterSize.Any
            _tvseasonpostersize = Enums.TVSeasonPosterSize.Any
            _urloriginal = String.Empty
            _urlthumb = String.Empty
            _voteaverage = String.Empty
            _votecount = 0
            _width = String.Empty
        End Sub

        Private Sub DetectImageSize(ByRef strHeigth As String)
            Select Case strHeigth
                Case "3000"
                    _moviepostersize = Enums.MoviePosterSize.HD3000
                    _tvpostersize = Enums.TVPosterSize.HD3000
                Case "2160"
                    _moviefanartsize = Enums.MovieFanartSize.UHD2160
                    _tvepisodepostersize = Enums.TVEpisodePosterSize.UHD2160
                    _tvfanartsize = Enums.TVFanartSize.UHD2160
                Case "2100"
                    _moviepostersize = Enums.MoviePosterSize.HD2100
                Case "1500"
                    _moviepostersize = Enums.MoviePosterSize.HD1500
                    _tvpostersize = Enums.TVPosterSize.HD1500
                    _tvseasonpostersize = Enums.TVSeasonPosterSize.HD1500
                Case "1440"
                    _moviefanartsize = Enums.MovieFanartSize.QHD1440
                    _tvfanartsize = Enums.TVFanartSize.QHD1440
                Case "1426"
                    _moviepostersize = Enums.MoviePosterSize.HD1426
                    _tvpostersize = Enums.TVPosterSize.HD1426
                    _tvseasonpostersize = Enums.TVSeasonPosterSize.HD1426
                Case "1080"
                    _moviefanartsize = Enums.MovieFanartSize.HD1080
                    _tvepisodepostersize = Enums.TVEpisodePosterSize.HD1080
                    _tvfanartsize = Enums.TVFanartSize.HD1080
                Case "1000"
                    _tvpostersize = Enums.TVPosterSize.HD1000
                Case "720"
                    _moviefanartsize = Enums.MovieFanartSize.HD720
                    _tvepisodepostersize = Enums.TVEpisodePosterSize.HD720
                    _tvfanartsize = Enums.TVFanartSize.HD720
                Case "578"
                    _tvseasonpostersize = Enums.TVSeasonPosterSize.HD578
                Case "225", "300" '225 = 16:9 / 300 = 4:3
                    _tvepisodepostersize = Enums.TVEpisodePosterSize.SD225
                Case "185"
                    _moviebannersize = Enums.MovieBannerSize.HD185
                    _tvbannersize = Enums.TVBannerSize.HD185
                Case "140"
                    _tvbannersize = Enums.TVBannerSize.HD140
                Case Else
                    _moviebannersize = Enums.MovieBannerSize.Any
                    _moviefanartsize = Enums.MovieFanartSize.Any
                    _moviepostersize = Enums.MoviePosterSize.Any
                    _tvbannersize = Enums.TVBannerSize.Any
                    _tvepisodepostersize = Enums.TVEpisodePosterSize.Any
                    _tvfanartsize = Enums.TVFanartSize.Any
                    _tvpostersize = Enums.TVPosterSize.Any
                    _tvseasonpostersize = Enums.TVSeasonPosterSize.Any
            End Select
        End Sub

        Public Function LoadAndCache(ByVal tContentType As Enums.ContentType, ByVal needFullsize As Boolean, Optional ByVal LoadBitmap As Boolean = False) As Boolean
            Dim doCache As Boolean = False

            Select Case tContentType
                Case Enums.ContentType.Movie
                    doCache = Master.eSettings.MovieImagesCacheEnabled
                Case Enums.ContentType.MovieSet
                    doCache = Master.eSettings.MovieSetImagesCacheEnabled
                Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                    doCache = Master.eSettings.TVImagesCacheEnabled
            End Select

            If Not ((ImageOriginal.HasMemoryStream OrElse (ImageThumb.HasMemoryStream AndAlso Not needFullsize)) AndAlso Not LoadBitmap) Then
                If (ImageOriginal.Image Is Nothing AndAlso needFullsize) OrElse (ImageThumb.Image Is Nothing AndAlso Not needFullsize) Then
                    If File.Exists(LocalFilePath) AndAlso Not ImageOriginal.HasMemoryStream Then
                        ImageOriginal.LoadFromFile(LocalFilePath, LoadBitmap)
                    ElseIf ImageThumb.HasMemoryStream AndAlso Not needFullsize AndAlso LoadBitmap Then
                        ImageThumb.LoadFromMemoryStream()
                    ElseIf ImageOriginal.HasMemoryStream AndAlso LoadBitmap Then
                        ImageOriginal.LoadFromMemoryStream()
                    ElseIf File.Exists(CacheThumbPath) AndAlso Not needFullsize Then
                        ImageThumb.LoadFromFile(CacheThumbPath, LoadBitmap)
                    ElseIf File.Exists(CacheOriginalPath) Then
                        ImageOriginal.LoadFromFile(CacheOriginalPath, LoadBitmap)
                    Else
                        If Not String.IsNullOrEmpty(URLThumb) AndAlso Not needFullsize Then
                            ImageThumb.LoadFromWeb(URLThumb, LoadBitmap)
                            If doCache AndAlso Not String.IsNullOrEmpty(CacheThumbPath) AndAlso ImageThumb.HasMemoryStream Then
                                Directory.CreateDirectory(Directory.GetParent(CacheThumbPath).FullName)
                                ImageThumb.SaveToFile(CacheThumbPath)
                            End If
                        ElseIf Not String.IsNullOrEmpty(URLOriginal) Then
                            ImageOriginal.LoadFromWeb(URLOriginal, LoadBitmap)
                            If doCache AndAlso Not String.IsNullOrEmpty(CacheOriginalPath) AndAlso ImageOriginal.HasMemoryStream Then
                                Directory.CreateDirectory(Directory.GetParent(CacheOriginalPath).FullName)
                                ImageOriginal.SaveToFile(CacheOriginalPath)
                            End If
                        End If
                    End If
                End If
            End If

            If (ImageOriginal.Image IsNot Nothing OrElse (ImageThumb.Image IsNot Nothing AndAlso Not needFullsize)) OrElse
                (Not LoadBitmap AndAlso (ImageOriginal.HasMemoryStream OrElse (ImageThumb.HasMemoryStream AndAlso Not needFullsize))) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function CompareTo(ByVal other As [Image]) As Integer Implements IComparable(Of [Image]).CompareTo
            Try
                Dim retVal As Integer = (ShortLang).CompareTo(other.ShortLang)
                Return retVal
            Catch ex As Exception
                Return 0
            End Try
        End Function

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class ImagesContainer

#Region "Fields"

        Private _banner As New Image
        Private _characterart As New Image
        Private _clearart As New Image
        Private _clearlogo As New Image
        Private _discart As New Image
        Private _extrafanarts As New List(Of Image)
        Private _extrathumbs As New List(Of Image)
        Private _fanart As New Image
        Private _landscape As New Image
        Private _poster As New Image

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Banner() As Image
            Get
                Return _banner
            End Get
            Set(ByVal value As Image)
                _banner = value
            End Set
        End Property

        Public Property CharacterArt() As Image
            Get
                Return _characterart
            End Get
            Set(ByVal value As Image)
                _characterart = value
            End Set
        End Property

        Public Property ClearArt() As Image
            Get
                Return _clearart
            End Get
            Set(ByVal value As Image)
                _clearart = value
            End Set
        End Property

        Public Property ClearLogo() As Image
            Get
                Return _clearlogo
            End Get
            Set(ByVal value As Image)
                _clearlogo = value
            End Set
        End Property

        Public Property DiscArt() As Image
            Get
                Return _discart
            End Get
            Set(ByVal value As Image)
                _discart = value
            End Set
        End Property

        Public Property Extrafanarts() As List(Of Image)
            Get
                Return _extrafanarts
            End Get
            Set(ByVal value As List(Of Image))
                _extrafanarts = value
            End Set
        End Property

        Public Property Extrathumbs() As List(Of Image)
            Get
                Return _extrathumbs
            End Get
            Set(ByVal value As List(Of Image))
                _extrathumbs = value
            End Set
        End Property

        Public Property Fanart() As Image
            Get
                Return _fanart
            End Get
            Set(ByVal value As Image)
                _fanart = value
            End Set
        End Property

        Public Property Landscape() As Image
            Get
                Return _landscape
            End Get
            Set(ByVal value As Image)
                _landscape = value
            End Set
        End Property

        Public Property Poster() As Image
            Get
                Return _poster
            End Get
            Set(ByVal value As Image)
                _poster = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _banner = New Image
            _characterart = New Image
            _clearart = New Image
            _clearlogo = New Image
            _discart = New Image
            _extrafanarts = New List(Of Image)
            _extrathumbs = New List(Of Image)
            _fanart = New Image
            _landscape = New Image
            _poster = New Image
        End Sub

        Public Sub LoadAllImages(ByVal Type As Enums.ContentType, ByVal LoadBitmap As Boolean, ByVal withExtraImages As Boolean)
            Banner.LoadAndCache(Type, True, LoadBitmap)
            CharacterArt.LoadAndCache(Type, True, LoadBitmap)
            ClearArt.LoadAndCache(Type, True, LoadBitmap)
            ClearLogo.LoadAndCache(Type, True, LoadBitmap)
            DiscArt.LoadAndCache(Type, True, LoadBitmap)
            Fanart.LoadAndCache(Type, True, LoadBitmap)
            Landscape.LoadAndCache(Type, True, LoadBitmap)
            Poster.LoadAndCache(Type, True, LoadBitmap)

            If withExtraImages Then
                For Each tImg As Image In Extrafanarts
                    tImg.LoadAndCache(Type, True, LoadBitmap)
                Next
                For Each tImg As Image In Extrathumbs
                    tImg.LoadAndCache(Type, True, LoadBitmap)
                Next
            End If
        End Sub

        Public Sub SaveAllImages(ByRef DBElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
            If Not DBElement.FilenameSpecified AndAlso (DBElement.ContentType = Enums.ContentType.Movie OrElse DBElement.ContentType = Enums.ContentType.TVEpisode) Then Return

            Dim tContentType As Enums.ContentType = DBElement.ContentType

            Select Case tContentType
                Case Enums.ContentType.Movie

                    'Movie Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainBanner, ForceFileCleanup)
                        Banner.LocalFilePath = Banner.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainBanner)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainBanner, ForceFileCleanup)
                        Banner = New Image
                    End If

                    'Movie ClearArt
                    If ClearArt.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainClearArt, ForceFileCleanup)
                        ClearArt.LocalFilePath = ClearArt.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainClearArt)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainClearArt, ForceFileCleanup)
                        ClearArt = New Image
                    End If

                    'Movie ClearLogo
                    If ClearLogo.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainClearLogo, ForceFileCleanup)
                        ClearLogo.LocalFilePath = ClearLogo.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainClearLogo)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainClearLogo, ForceFileCleanup)
                        ClearLogo = New Image
                    End If

                    'Movie DiscArt
                    If DiscArt.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainDiscArt, ForceFileCleanup)
                        DiscArt.LocalFilePath = DiscArt.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainDiscArt)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainDiscArt, ForceFileCleanup)
                        DiscArt = New Image
                    End If

                    'Movie Extrafanarts
                    If Extrafanarts.Count > 0 Then
                        DBElement.ExtrafanartsPath = Images.SaveMovieExtrafanarts(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainExtrafanarts, ForceFileCleanup)
                        Extrafanarts = New List(Of Image)
                        DBElement.ExtrafanartsPath = String.Empty
                    End If

                    'Movie Extrathumbs
                    If Extrathumbs.Count > 0 Then
                        DBElement.ExtrathumbsPath = Images.SaveMovieExtrathumbs(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainExtrathumbs, ForceFileCleanup)
                        Extrathumbs = New List(Of Image)
                        DBElement.ExtrathumbsPath = String.Empty
                    End If

                    'Movie Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainFanart, ForceFileCleanup)
                        Fanart.LocalFilePath = Fanart.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainFanart)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainFanart, ForceFileCleanup)
                        Fanart = New Image
                    End If

                    'Movie Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainLandscape, ForceFileCleanup)
                        Landscape.LocalFilePath = Landscape.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainLandscape)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainLandscape, ForceFileCleanup)
                        Landscape = New Image
                    End If

                    'Movie Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        If ForceFileCleanup Then Images.Delete_Movie(DBElement, Enums.ModifierType.MainPoster, ForceFileCleanup)
                        Poster.LocalFilePath = Poster.ImageOriginal.Save_Movie(DBElement, Enums.ModifierType.MainPoster)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainPoster, ForceFileCleanup)
                        Poster = New Image
                    End If

                Case Enums.ContentType.MovieSet

                    'MovieSet Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainBanner, True)
                        Banner.LocalFilePath = Banner.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainBanner)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainBanner, DBElement.MovieSet.TitleHasChanged)
                        Banner = New Image
                    End If

                    'MovieSet ClearArt
                    If ClearArt.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainClearArt, True)
                        ClearArt.LocalFilePath = ClearArt.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainClearArt)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainClearArt, DBElement.MovieSet.TitleHasChanged)
                        ClearArt = New Image
                    End If

                    'MovieSet ClearLogo
                    If ClearLogo.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainClearLogo, True)
                        ClearLogo.LocalFilePath = ClearLogo.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainClearLogo)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainClearLogo, DBElement.MovieSet.TitleHasChanged)
                        ClearLogo = New Image
                    End If

                    'MovieSet DiscArt
                    If DiscArt.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainDiscArt, True)
                        DiscArt.LocalFilePath = DiscArt.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainDiscArt)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainDiscArt, DBElement.MovieSet.TitleHasChanged)
                        DiscArt = New Image
                    End If

                    'MovieSet Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainFanart, True)
                        Fanart.LocalFilePath = Fanart.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainFanart)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainFanart, DBElement.MovieSet.TitleHasChanged)
                        Fanart = New Image
                    End If

                    'MovieSet Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainLandscape, True)
                        Landscape.LocalFilePath = Landscape.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainLandscape)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainLandscape, DBElement.MovieSet.TitleHasChanged)
                        Landscape = New Image
                    End If

                    'MovieSet Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        If DBElement.MovieSet.TitleHasChanged Then Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainPoster, True)
                        Poster.LocalFilePath = Poster.ImageOriginal.Save_MovieSet(DBElement, Enums.ModifierType.MainPoster)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainPoster, DBElement.MovieSet.TitleHasChanged)
                        Poster = New Image
                    End If

                Case Enums.ContentType.TVEpisode

                    'Episode Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        Fanart.LocalFilePath = Fanart.ImageOriginal.Save_TVEpisode(DBElement, Enums.ModifierType.EpisodeFanart)
                    Else
                        Images.Delete_TVEpisode(DBElement, Enums.ModifierType.EpisodeFanart)
                        Fanart = New Image
                    End If

                    'Episode Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        Poster.LocalFilePath = Poster.ImageOriginal.Save_TVEpisode(DBElement, Enums.ModifierType.EpisodePoster)
                    Else
                        Images.Delete_TVEpisode(DBElement, Enums.ModifierType.EpisodePoster)
                        Poster = New Image
                    End If

                Case Enums.ContentType.TVSeason

                    'Season Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        If DBElement.TVSeason.Season = 999 Then
                            Banner.LocalFilePath = Banner.ImageOriginal.Save_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsBanner)
                        Else
                            Banner.LocalFilePath = Banner.ImageOriginal.Save_TVSeason(DBElement, Enums.ModifierType.SeasonBanner)
                        End If
                    Else
                        If DBElement.TVSeason.Season = 999 Then
                            Images.Delete_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsBanner)
                            Banner = New Image
                        Else
                            Images.Delete_TVSeason(DBElement, Enums.ModifierType.SeasonBanner)
                            Banner = New Image
                        End If
                    End If

                    'Season Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        If DBElement.TVSeason.Season = 999 Then
                            Fanart.LocalFilePath = Fanart.ImageOriginal.Save_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsFanart)
                        Else
                            Fanart.LocalFilePath = Fanart.ImageOriginal.Save_TVSeason(DBElement, Enums.ModifierType.SeasonFanart)
                        End If
                    Else
                        If DBElement.TVSeason.Season = 999 Then
                            Images.Delete_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsFanart)
                            Fanart = New Image
                        Else
                            Images.Delete_TVSeason(DBElement, Enums.ModifierType.SeasonFanart)
                            Fanart = New Image
                        End If
                    End If

                    'Season Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        If DBElement.TVSeason.Season = 999 Then
                            Landscape.LocalFilePath = Landscape.ImageOriginal.Save_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsLandscape)
                        Else
                            Landscape.LocalFilePath = Landscape.ImageOriginal.Save_TVSeason(DBElement, Enums.ModifierType.SeasonLandscape)
                        End If
                    Else
                        If DBElement.TVSeason.Season = 999 Then
                            Images.Delete_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsLandscape)
                            Landscape = New Image
                        Else
                            Images.Delete_TVSeason(DBElement, Enums.ModifierType.SeasonLandscape)
                            Landscape = New Image
                        End If
                    End If

                    'Season Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        If DBElement.TVSeason.Season = 999 Then
                            Poster.LocalFilePath = Poster.ImageOriginal.Save_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsPoster)
                        Else
                            Poster.LocalFilePath = Poster.ImageOriginal.Save_TVSeason(DBElement, Enums.ModifierType.SeasonPoster)
                        End If
                    Else
                        If DBElement.TVSeason.Season = 999 Then
                            Images.Delete_TVAllSeasons(DBElement, Enums.ModifierType.AllSeasonsPoster)
                            Poster = New Image
                        Else
                            Images.Delete_TVSeason(DBElement, Enums.ModifierType.SeasonPoster)
                            Poster = New Image
                        End If
                    End If

                Case Enums.ContentType.TVShow

                    'Show Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        Banner.LocalFilePath = Banner.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainBanner)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainBanner)
                        Banner = New Image
                    End If

                    'Show CharacterArt
                    If CharacterArt.LoadAndCache(tContentType, True) Then
                        CharacterArt.LocalFilePath = CharacterArt.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainCharacterArt)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainCharacterArt)
                        CharacterArt = New Image
                    End If

                    'Show ClearArt
                    If ClearArt.LoadAndCache(tContentType, True) Then
                        ClearArt.LocalFilePath = ClearArt.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainClearArt)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainClearArt)
                        ClearArt = New Image
                    End If

                    'Show ClearLogo
                    If ClearLogo.LoadAndCache(tContentType, True) Then
                        ClearLogo.LocalFilePath = ClearLogo.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainClearLogo)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainClearLogo)
                        ClearLogo = New Image
                    End If

                    'Show Extrafanarts
                    If Extrafanarts.Count > 0 Then
                        DBElement.ExtrafanartsPath = Images.SaveTVShowExtrafanarts(DBElement)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainExtrafanarts)
                        Extrafanarts = New List(Of Image)
                        DBElement.ExtrafanartsPath = String.Empty
                    End If

                    'Show Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        Fanart.LocalFilePath = Fanart.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainFanart)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainFanart)
                        Fanart = New Image
                    End If

                    'Show Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        Landscape.LocalFilePath = Landscape.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainLandscape)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainLandscape)
                        Landscape = New Image
                    End If

                    'Show Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        Poster.LocalFilePath = Poster.ImageOriginal.Save_TVShow(DBElement, Enums.ModifierType.MainPoster)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainPoster)
                        Poster = New Image
                    End If
            End Select
        End Sub

        Public Sub SortExtrathumbs()
            Dim newList As New List(Of Image)
            Dim newIndex As Integer = 0
            For Each tImg As Image In Extrathumbs.OrderBy(Function(f) f.Index)
                tImg.Index = newIndex
                newList.Add(tImg)
                newIndex += 1
            Next
            Extrathumbs = newList
        End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

    End Class

    <Serializable()>
    Public Class EpisodeOrSeasonImagesContainer

#Region "Fields"

        Private _alreadysaved As Boolean
        Private _banner As Image
        Private _episode As Integer
        Private _fanart As Image
        Private _landscape As Image
        Private _poster As Image
        Private _season As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AlreadySaved() As Boolean
            Get
                Return _alreadysaved
            End Get
            Set(ByVal value As Boolean)
                _alreadysaved = value
            End Set
        End Property

        Public Property Banner() As Image
            Get
                Return _banner
            End Get
            Set(ByVal value As Image)
                _banner = value
            End Set
        End Property

        Public Property Episode() As Integer
            Get
                Return _episode
            End Get
            Set(ByVal value As Integer)
                _episode = value
            End Set
        End Property

        Public Property Fanart() As Image
            Get
                Return _fanart
            End Get
            Set(ByVal value As Image)
                _fanart = value
            End Set
        End Property

        Public Property Landscape() As Image
            Get
                Return _landscape
            End Get
            Set(ByVal value As Image)
                _landscape = value
            End Set
        End Property

        Public Property Poster() As Image
            Get
                Return _poster
            End Get
            Set(ByVal value As Image)
                _poster = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return _season
            End Get
            Set(ByVal value As Integer)
                _season = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _alreadysaved = False
            _banner = New Image
            _episode = -1
            _fanart = New Image
            _landscape = New Image
            _poster = New Image
            _season = -1
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class PreferredImagesContainer

#Region "Fields"

        Private _episodes As New List(Of EpisodeOrSeasonImagesContainer)
        Private _imagescontainer As New ImagesContainer
        Private _seasons As New List(Of EpisodeOrSeasonImagesContainer)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Episodes() As List(Of EpisodeOrSeasonImagesContainer)
            Get
                Return _episodes
            End Get
            Set(ByVal value As List(Of EpisodeOrSeasonImagesContainer))
                _episodes = value
            End Set
        End Property

        Public Property ImagesContainer() As ImagesContainer
            Get
                Return _imagescontainer
            End Get
            Set(ByVal value As ImagesContainer)
                _imagescontainer = value
            End Set
        End Property

        Public Property Seasons() As List(Of EpisodeOrSeasonImagesContainer)
            Get
                Return _seasons
            End Get
            Set(ByVal value As List(Of EpisodeOrSeasonImagesContainer))
                _seasons = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _episodes.Clear()
            _imagescontainer.Clear()
            _seasons.Clear()
        End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

    End Class

    <Serializable()>
    Public Class SearchResultsContainer

#Region "Fields"

        Private _episodefanarts As New List(Of Image)
        Private _episodeposters As New List(Of Image)
        Private _seasonbanners As New List(Of Image)
        Private _seasonfanarts As New List(Of Image)
        Private _seasonlandscapes As New List(Of Image)
        Private _seasonposters As New List(Of Image)
        Private _mainbanners As New List(Of Image)
        Private _maincharacterarts As New List(Of Image)
        Private _maincleararts As New List(Of Image)
        Private _mainclearlogos As New List(Of Image)
        Private _maindiscarts As New List(Of Image)
        Private _mainfanarts As New List(Of Image)
        Private _mainlandscapes As New List(Of Image)
        Private _mainposters As New List(Of Image)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property EpisodeFanarts() As List(Of Image)
            Get
                Return _episodefanarts
            End Get
            Set(ByVal value As List(Of Image))
                _episodefanarts = value
            End Set
        End Property

        Public Property EpisodePosters() As List(Of Image)
            Get
                Return _episodeposters
            End Get
            Set(ByVal value As List(Of Image))
                _episodeposters = value
            End Set
        End Property

        Public Property SeasonBanners() As List(Of Image)
            Get
                Return _seasonbanners
            End Get
            Set(ByVal value As List(Of Image))
                _seasonbanners = value
            End Set
        End Property

        Public Property SeasonFanarts() As List(Of Image)
            Get
                Return _seasonfanarts
            End Get
            Set(ByVal value As List(Of Image))
                _seasonfanarts = value
            End Set
        End Property

        Public Property SeasonLandscapes() As List(Of Image)
            Get
                Return _seasonlandscapes
            End Get
            Set(ByVal value As List(Of Image))
                _seasonlandscapes = value
            End Set
        End Property

        Public Property SeasonPosters() As List(Of Image)
            Get
                Return _seasonposters
            End Get
            Set(ByVal value As List(Of Image))
                _seasonposters = value
            End Set
        End Property

        Public Property MainBanners() As List(Of Image)
            Get
                Return _mainbanners
            End Get
            Set(ByVal value As List(Of Image))
                _mainbanners = value
            End Set
        End Property

        Public Property MainCharacterArts() As List(Of Image)
            Get
                Return _maincharacterarts
            End Get
            Set(ByVal value As List(Of Image))
                _maincharacterarts = value
            End Set
        End Property

        Public Property MainClearArts() As List(Of Image)
            Get
                Return _maincleararts
            End Get
            Set(ByVal value As List(Of Image))
                _maincleararts = value
            End Set
        End Property

        Public Property MainClearLogos() As List(Of Image)
            Get
                Return _mainclearlogos
            End Get
            Set(ByVal value As List(Of Image))
                _mainclearlogos = value
            End Set
        End Property

        Public Property MainDiscArts() As List(Of Image)
            Get
                Return _maindiscarts
            End Get
            Set(ByVal value As List(Of Image))
                _maindiscarts = value
            End Set
        End Property

        Public Property MainFanarts() As List(Of Image)
            Get
                Return _mainfanarts
            End Get
            Set(ByVal value As List(Of Image))
                _mainfanarts = value
            End Set
        End Property

        Public Property MainLandscapes() As List(Of Image)
            Get
                Return _mainlandscapes
            End Get
            Set(ByVal value As List(Of Image))
                _mainlandscapes = value
            End Set
        End Property

        Public Property MainPosters() As List(Of Image)
            Get
                Return _mainposters
            End Get
            Set(ByVal value As List(Of Image))
                _mainposters = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _episodefanarts.Clear()
            _episodeposters.Clear()
            _seasonbanners.Clear()
            _seasonfanarts.Clear()
            _seasonlandscapes.Clear()
            _seasonposters.Clear()
            _mainbanners.Clear()
            _maincharacterarts.Clear()
            _maincleararts.Clear()
            _mainclearlogos.Clear()
            _maindiscarts.Clear()
            _mainfanarts.Clear()
            _mainlandscapes.Clear()
            _mainposters.Clear()
        End Sub

        Public Sub CreateCachePaths(ByRef tDBElement As Database.DBElement)
            Dim sID As String = String.Empty
            Dim sPath As String = String.Empty

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    sID = tDBElement.Movie.IMDB
                    If String.IsNullOrEmpty(sID) Then
                        sID = tDBElement.Movie.TMDB
                    End If
                    If String.IsNullOrEmpty(sID) Then
                        sID = "Unknown"
                    End If
                    sPath = Path.Combine(Master.TempPath, String.Concat("Movies", Path.DirectorySeparatorChar, sID))
                Case Enums.ContentType.MovieSet
                    sID = tDBElement.MovieSet.TMDB
                    If String.IsNullOrEmpty(sID) Then
                        sID = "Unknown"
                    End If
                    sPath = Path.Combine(Master.TempPath, String.Concat("MovieSets", Path.DirectorySeparatorChar, sID))
                Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                    sID = tDBElement.TVShow.TVDB
                    If String.IsNullOrEmpty(sID) Then
                        sID = "Unknown"
                    End If
                    sPath = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, sID))
                Case Else
                    Throw New ArgumentOutOfRangeException("wrong tContentType", "value must be Movie, MovieSet or TV")
                    Return
            End Select

            For Each tImg As Image In EpisodeFanarts
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("episodefanarts", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("episodefanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In EpisodePosters
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("episodeposters", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("episodeposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainBanners
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("mainbanners", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("mainbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainCharacterArts
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("maincharacterarts", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("maincharacterarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainClearArts
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("maincleararts", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("maincleararts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainClearLogos
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("mainclearlogos", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("mainclearlogos\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainDiscArts
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("maindiscarts", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("maindiscarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainFanarts
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("mainfanarts", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("mainfanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainLandscapes
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("mainlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("mainlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In MainPosters
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("mainposters", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("mainposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In SeasonBanners
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("seasonbanners", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("seasonbanners\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In SeasonFanarts
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("seasonfanarts", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("seasonfanarts\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In SeasonLandscapes
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("seasonlandscapes", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("seasonlandscapes\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next

            For Each tImg As Image In SeasonPosters
                tImg.CacheOriginalPath = Path.Combine(sPath, String.Concat("seasonposters", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                If Not String.IsNullOrEmpty(tImg.URLThumb) Then
                    tImg.CacheThumbPath = Path.Combine(sPath, String.Concat("seasonposters\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tImg.URLOriginal)))
                End If
            Next
        End Sub

        Public Sub SortAndFilter(ByVal tDBElement As Database.DBElement)
            Dim cSettings As New FilterSettings

            cSettings.ContentType = tDBElement.ContentType
            cSettings.MediaLanguage = tDBElement.Language_Main

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    cSettings.ForcedLanguage = Master.eSettings.MovieImagesForcedLanguage
                    cSettings.ForceLanguage = Master.eSettings.MovieImagesForceLanguage
                    cSettings.GetBlankImages = Master.eSettings.MovieImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.MovieImagesGetEnglishImages
                    cSettings.MediaLanguageOnly = Master.eSettings.MovieImagesMediaLanguageOnly
                Case Enums.ContentType.MovieSet
                    cSettings.ForcedLanguage = Master.eSettings.MovieSetImagesForcedLanguage
                    cSettings.ForceLanguage = Master.eSettings.MovieSetImagesForceLanguage
                    cSettings.GetBlankImages = Master.eSettings.MovieSetImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.MovieSetImagesGetEnglishImages
                    cSettings.MediaLanguageOnly = Master.eSettings.MovieSetImagesMediaLanguageOnly
                Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                    cSettings.ForcedLanguage = Master.eSettings.TVImagesForcedLanguage
                    cSettings.ForceLanguage = Master.eSettings.TVImagesForceLanguage
                    cSettings.GetBlankImages = Master.eSettings.TVImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.TVImagesGetEnglishImages
                    cSettings.MediaLanguageOnly = Master.eSettings.TVImagesMediaLanguageOnly
            End Select

            'sort all List(Of Image) by Image.ShortLang
            _episodefanarts.Sort()
            _episodeposters.Sort()
            _seasonbanners.Sort()
            _seasonfanarts.Sort()
            _seasonlandscapes.Sort()
            _seasonposters.Sort()
            _mainbanners.Sort()
            _maincharacterarts.Sort()
            _maincleararts.Sort()
            _mainclearlogos.Sort()
            _maindiscarts.Sort()
            _mainfanarts.Sort()
            _mainlandscapes.Sort()
            _mainposters.Sort()

            'sort all List(Of Image) by Votes/Size/Type
            SortImages(cSettings)

            'filter all List(Of Image) by preferred language/en/Blank/String.Empty/others
            'Language preference settings aren't needed for sorting episode posters since here we only care about size of image (unlike poster/banner)
            '_episodeposters = FilterImages(_episodeposters, cSettings)
            _seasonbanners = FilterImages(_seasonbanners, cSettings)
            _seasonlandscapes = FilterImages(_seasonlandscapes, cSettings)
            _seasonposters = FilterImages(_seasonposters, cSettings)
            _mainbanners = FilterImages(_mainbanners, cSettings)
            _maincharacterarts = FilterImages(_maincharacterarts, cSettings)
            _maincleararts = FilterImages(_maincleararts, cSettings)
            _mainclearlogos = FilterImages(_mainclearlogos, cSettings)
            _maindiscarts = FilterImages(_maindiscarts, cSettings)
            'Language preference settings aren't needed for sorting fanarts since here we only care about size of image (unlike poster/banner)
            ' _mainfanarts = FilterImages(_mainfanarts, cSettings)
            _mainlandscapes = FilterImages(_mainlandscapes, cSettings)
            _mainposters = FilterImages(_mainposters, cSettings)
        End Sub

        Private Sub SortImages(ByVal cSettings As FilterSettings)
            Select Case cSettings.ContentType
                Case Enums.ContentType.Movie
                    'Movie Banner
                    If Not Master.eSettings.MovieBannerPrefSize = Enums.MovieBannerSize.Any Then
                        _mainbanners = _mainbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieBannerSize).OrderByDescending(Function(y) y.MovieBannerSize = Master.eSettings.MovieBannerPrefSize).ToList()
                    Else
                        _mainbanners = _mainbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieBannerSize).ToList()
                    End If
                    'Movie ClearArt
                    _maincleararts = _maincleararts.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'Movie ClearLogo
                    _mainclearlogos = _mainclearlogos.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'Movie DiscArt
                    _maindiscarts = _maindiscarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.DiscType).ToList()
                    'Movie Fanart
                    If Not Master.eSettings.MovieFanartPrefSize = Enums.MovieFanartSize.Any Then
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieFanartSize).OrderByDescending(Function(y) y.MovieFanartSize = Master.eSettings.MovieFanartPrefSize).ToList()
                    Else
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieFanartSize).ToList()
                    End If
                    'Movie Landscape
                    _mainlandscapes = _mainlandscapes.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'Movie Poster
                    If Not Master.eSettings.MoviePosterPrefSize = Enums.MoviePosterSize.Any Then
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MoviePosterSize).OrderByDescending(Function(y) y.MoviePosterSize = Master.eSettings.MoviePosterPrefSize).ToList()
                    Else
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MoviePosterSize).ToList()
                    End If
                Case Enums.ContentType.MovieSet
                    'MovieSet Banner
                    If Not Master.eSettings.MovieSetBannerPrefSize = Enums.MovieBannerSize.Any Then
                        _mainbanners = _mainbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieBannerSize).OrderByDescending(Function(y) y.MovieBannerSize = Master.eSettings.MovieSetBannerPrefSize).ToList()
                    Else
                        _mainbanners = _mainbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieBannerSize).ToList()
                    End If
                    'MovieSet ClearArt
                    _maincleararts = _maincleararts.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'MovieSet ClearLogo
                    _mainclearlogos = _mainclearlogos.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'MovieSet DiscArt
                    _maindiscarts = _maindiscarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.DiscType).ToList()
                    'MovieSet Fanart
                    If Not Master.eSettings.MovieSetFanartPrefSize = Enums.MovieFanartSize.Any Then
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieFanartSize).OrderByDescending(Function(y) y.MovieFanartSize = Master.eSettings.MovieSetFanartPrefSize).ToList()
                    Else
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieFanartSize).ToList()
                    End If
                    'MovieSet Landscape
                    _mainlandscapes = _mainlandscapes.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'MovieSet Poster
                    If Not Master.eSettings.MovieSetPosterPrefSize = Enums.MoviePosterSize.Any Then
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MoviePosterSize).OrderByDescending(Function(y) y.MoviePosterSize = Master.eSettings.MovieSetPosterPrefSize).ToList()
                    Else
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MoviePosterSize).ToList()
                    End If
                Case Enums.ContentType.TV, Enums.ContentType.TVShow
                    'TVShow Banner
                    If Not Master.eSettings.TVShowBannerPrefSize = Enums.TVBannerSize.Any Then
                        _mainbanners = _mainbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVBannerSize).OrderByDescending(Function(y) y.TVBannerSize = Master.eSettings.TVShowBannerPrefSize).ToList()
                    Else
                        _mainbanners = _mainbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVBannerSize).ToList()
                    End If
                    'TVShow CharacterArt
                    _maincharacterarts = _maincharacterarts.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'TVShow ClearArt
                    _maincleararts = _maincleararts.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'TVShow ClearLogo
                    _mainclearlogos = _mainclearlogos.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'TVShow Fanart
                    If Not Master.eSettings.TVShowFanartPrefSize = Enums.TVFanartSize.Any Then
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).OrderByDescending(Function(y) y.TVFanartSize = Master.eSettings.TVShowFanartPrefSize).ToList()
                    Else
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).ToList()
                    End If
                    'TVShow Landscape
                    _mainlandscapes = _mainlandscapes.OrderByDescending(Function(z) z.VoteAverage).ToList()
                    'TVShow Poster
                    If Not Master.eSettings.TVShowPosterPrefSize = Enums.TVPosterSize.Any Then
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVPosterSize).OrderByDescending(Function(y) y.TVPosterSize = Master.eSettings.TVShowPosterPrefSize).ToList()
                    Else
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVPosterSize).ToList()
                    End If
                Case Enums.ContentType.TVEpisode
                    'TVShow Fanart (TVEpisode preferred sorting)
                    If Not Master.eSettings.TVEpisodeFanartPrefSize = Enums.TVFanartSize.Any Then
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).OrderByDescending(Function(y) y.TVFanartSize = Master.eSettings.TVEpisodeFanartPrefSize).ToList()
                    Else
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).ToList()
                    End If
                Case Enums.ContentType.TVSeason
                    'TVShow Fanart (TVSeason preferred sorting)
                    If Not Master.eSettings.TVSeasonFanartPrefSize = Enums.TVFanartSize.Any Then
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).OrderByDescending(Function(y) y.TVFanartSize = Master.eSettings.TVSeasonFanartPrefSize).ToList()
                    Else
                        _mainfanarts = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).ToList()
                    End If
                    'TVShow Poster (TVSeason preferred sorting)
                    If Not Master.eSettings.TVSeasonPosterPrefSize = Enums.TVSeasonPosterSize.Any Then
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVSeasonPosterSize).OrderByDescending(Function(y) y.TVSeasonPosterSize = Master.eSettings.TVSeasonPosterPrefSize).ToList()
                    Else
                        _mainposters = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVSeasonPosterSize).ToList()
                    End If
            End Select

            'Unique image containers

            'TVEpisode Fanart
            If Not Master.eSettings.TVEpisodeFanartPrefSize = Enums.TVFanartSize.Any Then
                _episodefanarts = _episodefanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).OrderByDescending(Function(y) y.TVFanartSize = Master.eSettings.TVEpisodeFanartPrefSize).ToList()
            Else
                _episodefanarts = _episodefanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).ToList()
            End If
            'TVEpisode Poster
            If Not Master.eSettings.TVEpisodePosterPrefSize = Enums.TVEpisodePosterSize.Any Then
                _episodeposters = _episodeposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVEpisodePosterSize).OrderByDescending(Function(y) y.TVEpisodePosterSize = Master.eSettings.TVEpisodePosterPrefSize).ToList()
            Else
                _episodeposters = _episodeposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVEpisodePosterSize).ToList()
            End If
            'TVSeason Banner
            If Not Master.eSettings.TVSeasonBannerPrefSize = Enums.TVBannerSize.Any Then
                _seasonbanners = _seasonbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVBannerSize).OrderByDescending(Function(y) y.TVBannerSize = Master.eSettings.TVSeasonBannerPrefSize).ToList()
            Else
                _seasonbanners = _seasonbanners.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVBannerSize).ToList()
            End If
            'TVSeason Fanart
            If Not Master.eSettings.TVSeasonFanartPrefSize = Enums.TVFanartSize.Any Then
                _seasonfanarts = _seasonfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).OrderByDescending(Function(y) y.TVFanartSize = Master.eSettings.TVSeasonFanartPrefSize).ToList()
            Else
                _seasonfanarts = _seasonfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVFanartSize).ToList()
            End If
            'TVSeason Landscape
            _seasonlandscapes = _seasonlandscapes.OrderByDescending(Function(z) z.VoteAverage).ToList()
            'TVSeason Poster
            If Not Master.eSettings.TVSeasonPosterPrefSize = Enums.TVSeasonPosterSize.Any Then
                _seasonposters = _seasonposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVSeasonPosterSize).OrderByDescending(Function(y) y.TVSeasonPosterSize = Master.eSettings.TVSeasonPosterPrefSize).ToList()
            Else
                _seasonposters = _seasonposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.TVSeasonPosterSize).ToList()
            End If
        End Sub

        Private Function FilterImages(ByRef ImagesList As List(Of Image), ByVal cSettings As FilterSettings) As List(Of Image)
            Dim FilteredList As New List(Of Image)

            If cSettings.ForceLanguage AndAlso Not String.IsNullOrEmpty(cSettings.ForcedLanguage) Then
                FilteredList.AddRange(ImagesList.Where(Function(f) f.ShortLang = cSettings.ForcedLanguage))
            End If

            If Not (cSettings.ForceLanguage AndAlso cSettings.ForcedLanguage = cSettings.MediaLanguage) Then
                FilteredList.AddRange(ImagesList.Where(Function(f) f.ShortLang = cSettings.MediaLanguage))
            End If

            If (cSettings.GetEnglishImages OrElse Not cSettings.MediaLanguageOnly) AndAlso
                Not (cSettings.ForceLanguage AndAlso cSettings.ForcedLanguage = "en") AndAlso
                Not cSettings.MediaLanguage = "en" Then
                FilteredList.AddRange(ImagesList.Where(Function(f) f.ShortLang = "en"))
            End If

            If cSettings.GetBlankImages OrElse Not cSettings.MediaLanguageOnly Then
                FilteredList.AddRange(ImagesList.Where(Function(f) f.LongLang = Master.eLang.GetString(1168, "Blank")))
                FilteredList.AddRange(ImagesList.Where(Function(f) f.ShortLang = String.Empty))
            End If

            If Not cSettings.MediaLanguageOnly Then
                FilteredList.AddRange(ImagesList.Where(Function(f) Not f.ShortLang = If(cSettings.ForceLanguage, cSettings.ForcedLanguage, String.Empty) AndAlso
                                                           Not f.ShortLang = cSettings.MediaLanguage AndAlso
                                                           Not f.ShortLang = "en" AndAlso
                                                           Not f.LongLang = Master.eLang.GetString(1168, "Blank") AndAlso
                                                           Not f.ShortLang = String.Empty))
            End If

            Return FilteredList
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure FilterSettings

#Region "Fields"

            Dim ContentType As Enums.ContentType
            Dim ForceLanguage As Boolean
            Dim ForcedLanguage As String
            Dim GetBlankImages As Boolean
            Dim GetEnglishImages As Boolean
            Dim MediaLanguage As String
            Dim MediaLanguageOnly As Boolean

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class
    ''' <summary>
    ''' Container for YAMJ sets
    ''' </summary>
    <Serializable()>
    Public Class SetContainer

#Region "Fields"

        Private _set As New List(Of SetDetails)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("set")>
        Public Property Sets() As List(Of SetDetails)
            Get
                Return _set
            End Get
            Set(ByVal value As List(Of SetDetails))
                _set = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property SetsSpecified() As Boolean
            Get
                Return _set.Count > 0
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _set = New List(Of SetDetails)
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class SetDetails

#Region "Fields"

        Private _id As Long
        Private _order As Integer
        Private _plot As String
        Private _title As String
        Private _tmdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlIgnore()>
        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal value As Long)
                _id = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not _id = -1
            End Get
        End Property

        <XmlAttribute("order")>
        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal value As Integer)
                _order = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OrderSpecified() As Boolean
            Get
                Return Not _order = -1
            End Get
        End Property

        <XmlIgnore()>
        Public Property Plot() As String
            Get
                Return _plot
            End Get
            Set(ByVal value As String)
                _plot = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_plot)
            End Get
        End Property

        <XmlText()>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_title)
            End Get
        End Property

        <XmlIgnore()>
        Public Property TMDB() As String
            Get
                Return _tmdb
            End Get
            Set(ByVal value As String)
                _tmdb = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_tmdb)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _id = -1
            _order = -1
            _plot = String.Empty
            _title = String.Empty
            _tmdb = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    <XmlRoot("streamdata")>
    Public Class StreamData

#Region "Fields"

        Private _audio As New List(Of Audio)
        Private _subtitle As New List(Of Subtitle)
        Private _video As New List(Of Video)

#End Region 'Fields

#Region "Properties"

        <XmlElement("audio")>
        Public Property Audio() As List(Of Audio)
            Get
                Return _audio
            End Get
            Set(ByVal Value As List(Of Audio))
                _audio = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property AudioSpecified() As Boolean
            Get
                Return _audio.Count > 0
            End Get
        End Property

        <XmlElement("subtitle")>
        Public Property Subtitle() As List(Of Subtitle)
            Get
                Return _subtitle
            End Get
            Set(ByVal Value As List(Of Subtitle))
                _subtitle = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property SubtitleSpecified() As Boolean
            Get
                Return _subtitle.Count > 0
            End Get
        End Property

        <XmlElement("video")>
        Public Property Video() As List(Of Video)
            Get
                Return _video
            End Get
            Set(ByVal Value As List(Of Video))
                _video = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property VideoSpecified() As Boolean
            Get
                Return _video.Count > 0
            End Get
        End Property

#End Region 'Properties

    End Class

    <Serializable()>
    Public Class Subtitle

#Region "Fields"

        Private _language As String = String.Empty
        Private _longlanguage As String = String.Empty
        Private _subs_foced As Boolean = False
        Private _subs_path As String = String.Empty
        Private _subs_type As String = String.Empty
        Private _toremove As Boolean = False            'trigger to delete local/external subtitle files

#End Region 'Fields

#Region "Properties"

        <XmlElement("language")>
        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal Value As String)
                _language = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        <XmlElement("longlanguage")>
        Public Property LongLanguage() As String
            Get
                Return _longlanguage
            End Get
            Set(ByVal value As String)
                _longlanguage = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property LongLanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_longlanguage)
            End Get
        End Property

        <XmlElement("forced")>
        Public Property SubsForced() As Boolean
            Get
                Return _subs_foced
            End Get
            Set(ByVal value As Boolean)
                _subs_foced = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property SubsForcedSpecified() As Boolean
            Get
                Return _subs_foced
            End Get
        End Property

        <XmlElement("path")>
        Public Property SubsPath() As String
            Get
                Return _subs_path
            End Get
            Set(ByVal value As String)
                _subs_path = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property SubsPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_subs_path)
            End Get
        End Property

        <XmlElement("type")>
        Public Property SubsType() As String
            Get
                Return _subs_type
            End Get
            Set(ByVal value As String)
                _subs_type = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property SubsTypeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_subs_type)
            End Get
        End Property

        <XmlIgnore>
        Public Property toRemove() As Boolean
            Get
                Return _toremove
            End Get
            Set(ByVal value As Boolean)
                _toremove = value
            End Set
        End Property

#End Region 'Properties

    End Class

    <Serializable()>
    Public Class Theme

#Region "Fields"

        Private _bitrate As String
        Private _description As String
        Private _duration As String
        Private _localfilepath As String
        Private _scraper As String
        Private _source As String
        Private _themeoriginal As New Themes
        Private _urlaudiostream As String
        Private _urlwebsite As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Bitrate() As String
            Get
                Return _bitrate
            End Get
            Set(ByVal value As String)
                _bitrate = value
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

        Public Property Duration() As String
            Get
                Return _duration
            End Get
            Set(ByVal value As String)
                _duration = value
            End Set
        End Property

        Public Property LocalFilePath() As String
            Get
                Return _localfilepath
            End Get
            Set(ByVal value As String)
                _localfilepath = value
            End Set
        End Property

        Public ReadOnly Property LocalFilePathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_localfilepath)
            End Get
        End Property

        Public Property Scraper() As String
            Get
                Return _scraper
            End Get
            Set(ByVal value As String)
                _scraper = value
            End Set
        End Property

        Public Property Source() As String
            Get
                Return _source
            End Get
            Set(ByVal value As String)
                _source = value
            End Set
        End Property
        ''' <summary>
        ''' Download audio URL of the theme
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property URLAudioStream() As String
            Get
                Return _urlaudiostream
            End Get
            Set(ByVal value As String)
                _urlaudiostream = value
            End Set
        End Property

        Public ReadOnly Property URLAudioStreamSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_urlaudiostream)
            End Get
        End Property
        ''' <summary>
        ''' Website URL of the theme for preview in browser
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property URLWebsite() As String
            Get
                Return _urlwebsite
            End Get
            Set(ByVal value As String)
                _urlwebsite = value
            End Set
        End Property

        Public ReadOnly Property URLWebsiteSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_urlwebsite)
            End Get
        End Property

        Public Property ThemeOriginal() As Themes
            Get
                Return _themeoriginal
            End Get
            Set(ByVal value As Themes)
                _themeoriginal = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _bitrate = String.Empty
            _description = String.Empty
            _duration = String.Empty
            _localfilepath = String.Empty
            _scraper = String.Empty
            _source = String.Empty
            _themeoriginal = New Themes
            _urlaudiostream = String.Empty
            _urlwebsite = String.Empty
        End Sub

        Public Function LoadAndCache() As Boolean

            If Not ThemeOriginal.hasMemoryStream Then
                If File.Exists(LocalFilePath) Then
                    ThemeOriginal.LoadFromFile(LocalFilePath)
                ElseIf URLAudioStreamSpecified Then
                    ThemeOriginal.LoadFromWeb(Me)
                End If
            End If

            If ThemeOriginal.hasMemoryStream Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub SaveAllThemes(ByRef tDBElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
            Dim tContentType As Enums.ContentType = tDBElement.ContentType

            With tDBElement
                Select Case tContentType
                    Case Enums.ContentType.Movie
                        If .Theme.LoadAndCache() Then
                            Themes.Delete_Movie(tDBElement, ForceFileCleanup)
                            .Theme.LocalFilePath = .Theme.ThemeOriginal.Save_Movie(tDBElement)
                        Else
                            Themes.Delete_Movie(tDBElement, ForceFileCleanup)
                            .Theme = New Theme
                        End If

                    Case Enums.ContentType.TVShow
                        If .Theme.LoadAndCache() Then
                            Themes.Delete_TVShow(tDBElement) ', ForceFileCleanup)
                            .Theme.LocalFilePath = .Theme.ThemeOriginal.Save_TVShow(tDBElement)
                        Else
                            Themes.Delete_TVShow(tDBElement) ', ForceFileCleanup)
                            .Theme = New Theme
                        End If
                End Select
            End With
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Trailer

#Region "Fields"

        Private _duration As String
        Private _isDash As Boolean
        Private _localfilepath As String
        Private _longlang As String
        Private _quality As Enums.TrailerVideoQuality
        Private _scraper As String
        Private _shortlang As String
        Private _source As String
        Private _title As String
        Private _traileroriginal As New Trailers
        Private _type As Enums.TrailerType
        Private _urlaudiostream As String
        Private _urlvideostream As String
        Private _urlwebsite As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Duration() As String
            Get
                Return _duration
            End Get
            Set(ByVal value As String)
                _duration = value
            End Set
        End Property
        ''' <summary>
        ''' If is a Dash video, we need also an audio URL to merge video and audio
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property isDash() As Boolean
            Get
                Return _isDash
            End Get
            Set(ByVal value As Boolean)
                _isDash = value
            End Set
        End Property

        Public Property LocalFilePath() As String
            Get
                Return _localfilepath
            End Get
            Set(ByVal value As String)
                _localfilepath = value
            End Set
        End Property

        Public ReadOnly Property LocalFilePathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_localfilepath)
            End Get
        End Property

        Public Property LongLang() As String
            Get
                Return _longlang
            End Get
            Set(ByVal value As String)
                _longlang = value
            End Set
        End Property

        Public Property Quality() As Enums.TrailerVideoQuality
            Get
                Return _quality
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                _quality = value
            End Set
        End Property

        Public Property Scraper() As String
            Get
                Return _scraper
            End Get
            Set(ByVal value As String)
                _scraper = value
            End Set
        End Property

        Public Property ShortLang() As String
            Get
                Return _shortlang
            End Get
            Set(ByVal value As String)
                _shortlang = value
            End Set
        End Property

        Public Property Source() As String
            Get
                Return _source
            End Get
            Set(ByVal value As String)
                _source = value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        Public Property Type() As Enums.TrailerType
            Get
                Return _type
            End Get
            Set(ByVal value As Enums.TrailerType)
                _type = value
            End Set
        End Property
        ''' <summary>
        ''' Download audio URL of the trailer
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property URLAudioStream() As String
            Get
                Return _urlaudiostream
            End Get
            Set(ByVal value As String)
                _urlaudiostream = value
            End Set
        End Property
        ''' <summary>
        ''' Download video URL of the trailer
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property URLVideoStream() As String
            Get
                Return _urlvideostream
            End Get
            Set(ByVal value As String)
                _urlvideostream = value
            End Set
        End Property

        Public ReadOnly Property URLVideoStreamSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_urlvideostream)
            End Get
        End Property
        ''' <summary>
        ''' Website URL of the trailer for preview in browser
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property URLWebsite() As String
            Get
                Return _urlwebsite
            End Get
            Set(ByVal value As String)
                _urlwebsite = value
            End Set
        End Property

        Public ReadOnly Property URLWebsiteSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_urlwebsite)
            End Get
        End Property

        Public Property TrailerOriginal() As Trailers
            Get
                Return _traileroriginal
            End Get
            Set(ByVal value As Trailers)
                _traileroriginal = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _duration = String.Empty
            _isDash = False
            _localfilepath = String.Empty
            _longlang = String.Empty
            _quality = Enums.TrailerVideoQuality.Any
            _scraper = String.Empty
            _shortlang = String.Empty
            _source = String.Empty
            _title = String.Empty
            _traileroriginal = New Trailers
            _type = Enums.TrailerType.Any
            _urlaudiostream = String.Empty
            _urlvideostream = String.Empty
            _urlwebsite = String.Empty
        End Sub

        Public Function LoadAndCache() As Boolean

            If Not TrailerOriginal.hasMemoryStream Then
                If File.Exists(LocalFilePath) Then
                    TrailerOriginal.LoadFromFile(LocalFilePath)
                ElseIf URLVideoStreamSpecified Then
                    TrailerOriginal.LoadFromWeb(Me)
                End If
            End If

            If TrailerOriginal.hasMemoryStream Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub SaveAllTrailers(ByRef tDBElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
            Dim tContentType As Enums.ContentType = tDBElement.ContentType

            With tDBElement
                Select Case tContentType
                    Case Enums.ContentType.Movie

                        'Movie Trailer
                        If .Trailer.LoadAndCache() Then
                            Trailers.Delete_Movie(tDBElement, ForceFileCleanup)
                            .Trailer.LocalFilePath = .Trailer.TrailerOriginal.Save_Movie(tDBElement)
                        Else
                            Trailers.Delete_Movie(tDBElement, ForceFileCleanup)
                            .Trailer = New Trailer
                        End If
                End Select
            End With
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Uniqueid

#Region "Fields"

        Private _id As Long
        Private _isdefault As Boolean
        Private _type As String
        Private _value As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clean()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlIgnore()>
        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal Value As Long)
                _id = Value
            End Set
        End Property

        <XmlAttribute("type")>
        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal Value As String)
                _type = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TypeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_type)
            End Get
        End Property

        <XmlAttribute("default")>
        Public Property IsDefault() As Boolean
            Get
                Return _isdefault
            End Get
            Set(ByVal Value As Boolean)
                _isdefault = Value
            End Set
        End Property

        <XmlText()>
        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal Value As String)
                _value = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clean()
            _id = -1
            _isdefault = False
            _type = "unknown"
            _value = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class Video

#Region "Fields"

        Private _aspect As String = String.Empty
        Private _bitrate As String = String.Empty
        Private _codec As String = String.Empty
        Private _duration As String = String.Empty
        Private _encoded_Settings As String = String.Empty
        Private _height As String = String.Empty
        Private _language As String = String.Empty
        Private _longlanguage As String = String.Empty
        Private _multiview_count As String = String.Empty
        Private _multiview_layout As String = String.Empty
        Private _scantype As String = String.Empty
        'XBMC multiview layout type (http://wiki.xbmc.org/index.php?title=3D)
        Private _stereomode As String = String.Empty
        Private _width As String = String.Empty
        Private _filesize As Double = 0

#End Region 'Fields

#Region "Properties"

        <XmlElement("aspect")>
        Public Property Aspect() As String
            Get
                Return _aspect.Trim()
            End Get
            Set(ByVal Value As String)
                _aspect = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property AspectSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_aspect)
            End Get
        End Property

        <XmlElement("bitrate")>
        Public Property Bitrate() As String
            Get
                Return _bitrate.Trim()
            End Get
            Set(ByVal Value As String)
                _bitrate = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property BitrateSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_bitrate)
            End Get
        End Property

        <XmlElement("codec")>
        Public Property Codec() As String
            Get
                Return _codec.Trim()
            End Get
            Set(ByVal Value As String)
                _codec = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property CodecSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_codec)
            End Get
        End Property

        <XmlElement("durationinseconds")>
        Public Property Duration() As String
            Get
                Return _duration.Trim()
            End Get
            Set(ByVal Value As String)
                _duration = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property DurationSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_duration)
            End Get
        End Property

        <XmlElement("height")>
        Public Property Height() As String
            Get
                Return _height.Trim()
            End Get
            Set(ByVal Value As String)
                _height = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property HeightSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_height)
            End Get
        End Property

        <XmlElement("language")>
        Public Property Language() As String
            Get
                Return _language.Trim()
            End Get
            Set(ByVal Value As String)
                _language = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        <XmlElement("longlanguage")>
        Public Property LongLanguage() As String
            Get
                Return _longlanguage.Trim()
            End Get
            Set(ByVal value As String)
                _longlanguage = value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property LongLanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_longlanguage)
            End Get
        End Property

        <XmlElement("multiview_count")>
        Public Property MultiViewCount() As String
            Get
                Return _multiview_count.Trim()
            End Get
            Set(ByVal Value As String)
                _multiview_count = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property MultiViewCountSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_multiview_count)
            End Get
        End Property

        <XmlElement("multiview_layout")>
        Public Property MultiViewLayout() As String
            Get
                Return _multiview_layout.Trim()
            End Get
            Set(ByVal Value As String)
                _multiview_layout = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property MultiViewLayoutSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_multiview_layout)
            End Get
        End Property

        <XmlElement("scantype")>
        Public Property Scantype() As String
            Get
                Return _scantype.Trim()
            End Get
            Set(ByVal Value As String)
                _scantype = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property ScantypeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_scantype)
            End Get
        End Property

        <XmlIgnore>
        Public ReadOnly Property ShortStereoMode() As String
            Get
                Return ConvertVStereoToShort(_stereomode).Trim()
            End Get
        End Property

        <XmlElement("stereomode")>
        Public Property StereoMode() As String
            Get
                Return _stereomode.Trim()
            End Get
            Set(ByVal Value As String)
                _stereomode = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property StereoModeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_stereomode)
            End Get
        End Property

        <XmlElement("width")>
        Public Property Width() As String
            Get
                Return _width.Trim()
            End Get
            Set(ByVal Value As String)
                _width = Value
            End Set
        End Property

        <XmlIgnore>
        Public ReadOnly Property WidthSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_width)
            End Get
        End Property

        <XmlElement("filesize")>
        Public Property Filesize() As Double
            Get
                Return _filesize
            End Get
            Set(ByVal Value As Double)
                'for now save filesize in bytes(default)
                _filesize = Value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property FilesizeSpecified() As Boolean
            Get
                If _filesize = 0 Then
                    Return False
                Else
                    Return True
                End If
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Shared Function ConvertVStereoToShort(ByVal sFormat As String) As String
            If Not String.IsNullOrEmpty(sFormat) Then
                Dim tFormat As String = String.Empty
                Select Case sFormat.ToLower
                    Case "bottom_top"
                        tFormat = "tab"
                    Case "left_right", "right_left"
                        tFormat = "sbs"
                    Case Else
                        tFormat = "unknown"
                End Select

                Return tFormat
            Else
                Return String.Empty
            End If
        End Function

#End Region 'Methods

    End Class

End Namespace

