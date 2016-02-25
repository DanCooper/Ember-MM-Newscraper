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

Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
Imports System.IO

Namespace MediaContainers


    <Serializable()> _
    <XmlRoot("episodedetails")> _
    Public Class EpisodeDetails

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
        Private _fileInfo As New MediaInfo.Fileinfo
        Private _gueststars As New List(Of Person)
        Private _imdb As String
        Private _lastplayed As String
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
        Private _videosource As String
        Private _votes As String

#End Region 'Fields

#Region "Constructors"

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

        <Obsolete("This property is depreciated. Use Episode.Credits [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property OldCredits() As String
            Get
                Return String.Join(" / ", _credits.ToArray)
            End Get
            Set(ByVal value As String)
                _credits.Clear()
                AddCredit(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Episode.Directors [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Director() As String
            Get
                Return String.Join(" / ", _directors.ToArray)
            End Get
            Set(ByVal value As String)
                _directors.Clear()
                AddDirector(value)
            End Set
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
        Public Property FileInfo() As MediaInfo.Fileinfo
            Get
                Return _fileInfo
            End Get
            Set(ByVal value As MediaInfo.Fileinfo)
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

        <XmlElement("uniqueid")>
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
            _fileInfo = New MediaInfo.Fileinfo
            _gueststars.Clear()
            _imdb = String.Empty
            _lastplayed = String.Empty
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
            _videosource = String.Empty
            _votes = String.Empty
        End Sub

        Public Sub AddCredit(ByVal value As String)
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

        Public Sub AddDirector(ByVal value As String)
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
    <XmlRoot("movie")>
    Public Class Movie
        Implements IComparable(Of Movie)

#Region "Fields"

        Private _actors As New List(Of Person)
        Private _certifications As New List(Of String)
        Private _countries As New List(Of String)
        Private _credits As New List(Of String)
        Private _dateadded As String
        Private _datemodified As String
        Private _directors As New List(Of String)
        Private _fanart As New Fanart
        Private _fileInfo As New MediaInfo.Fileinfo
        Private _genres As New List(Of String)
        Private _language As String
        Private _lastplayed As String
        Private _lev As Integer
        Private _mpaa As String
        Private _originaltitle As String
        Private _outline As String
        Private _playcount As Integer
        Private _plot As String
        Private _rating As String
        Private _releaseDate As String
        Private _runtime As String
        Private _scrapersource As String
        Private _sorttitle As String
        Private _studios As New List(Of String)
        Private _tagline As String
        Private _tags As New List(Of String)
        Private _thumb As New List(Of String)
        Private _thumbposter As New Image
        Private _title As String
        Private _tmdbcolid As String
        Private _top250 As String
        Private _trailer As String
        Private _videosource As String
        Private _votes As String
        Private _xsets As New List(Of [Set])
        Private _year As String
        Private _ysets As New SetContainer

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sID As String, ByVal sTitle As String, ByVal sYear As String, ByVal iLev As Integer)
            Clear()
            MovieID.ID = sID
            _title = sTitle
            _year = sYear
            _lev = iLev
        End Sub

        Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"
        <XmlElement("id")>
        Public MovieID As New _MovieID

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
                Return Not String.IsNullOrEmpty(_sorttitle) AndAlso Not _sorttitle = StringUtils.SortTokens_Movie(_title)
            End Get
        End Property

        <XmlIgnore()>
        Public Property ID() As String
            Get
                Return MovieID.ID
            End Get
            Set(ByVal value As String)
                MovieID.ID = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(MovieID.ID)
            End Get
        End Property

        <XmlIgnore()>
        Public Property TMDBID() As String
            Get
                Return MovieID.IDTMDB
            End Get
            Set(ByVal value As String)
                MovieID.IDTMDB = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property TMDBIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(MovieID.IDTMDB)
            End Get
        End Property

        <XmlIgnore()>
        Public Property IMDBID() As String
            Get
                Return MovieID.ID.Replace("tt", String.Empty).Trim
            End Get
            Set(ByVal value As String)
                MovieID.ID = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property IMDBIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(MovieID.ID)
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
        Public Property Top250() As String
            Get
                Return _top250
            End Get
            Set(ByVal value As String)
                _top250 = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property Top250Specified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_top250)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Countries [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Country() As String
            Get
                Return String.Join(" / ", _countries.ToArray)
            End Get
            Set(ByVal value As String)
                _countries.Clear()
                AddCountry(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Certifications [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Certification() As String
            Get
                Return String.Join(" / ", _certifications.ToArray)
            End Get
            Set(ByVal value As String)
                _certifications.Clear()
                AddCertification(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Genres [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Genre() As String
            Get
                Return String.Join(" / ", _genres.ToArray)
            End Get
            Set(ByVal value As String)
                _genres.Clear()
                AddGenre(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Studios [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Studio() As String
            Get
                Return String.Join(" / ", _studios.ToArray)
            End Get
            Set(ByVal value As String)
                _studios.Clear()
                AddStudio(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Directors [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Director() As String
            Get
                Return String.Join(" / ", _directors.ToArray)
            End Get
            Set(ByVal value As String)
                _directors.Clear()
                AddDirector(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Credits [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property OldCredits() As String
            Get
                Return String.Join(" / ", _credits.ToArray)
            End Get
            Set(ByVal value As String)
                _credits.Clear()
                AddCredit(value)
            End Set
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
        Public Property Sets() As List(Of [Set])
            Get
                Return If(Master.eSettings.MovieYAMJCompatibleSets, _ysets.Sets, _xsets)
            End Get
            Set(ByVal value As List(Of [Set]))
                If Master.eSettings.MovieYAMJCompatibleSets Then
                    _ysets.Sets = value
                Else
                    _xsets = value
                End If
            End Set
        End Property

        <XmlElement("set")>
        Public Property XSets() As List(Of [Set])
            Get
                Return _xsets
            End Get
            Set(ByVal value As List(Of [Set]))
                _xsets = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property XSetsSpecified() As Boolean
            Get
                Return _xsets.Count > 0
            End Get
        End Property

        <XmlElement("sets")>
        Public Property YSets() As SetContainer
            Get
                Return _ysets
            End Get
            Set(ByVal value As SetContainer)
                _ysets = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property YSetsSpecified() As Boolean
            Get
                Return _ysets.Sets.Count > 0
            End Get
        End Property

        <XmlElement("fileinfo")>
        Public Property FileInfo() As MediaInfo.Fileinfo
            Get
                Return _fileInfo
            End Get
            Set(ByVal value As MediaInfo.Fileinfo)
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

        <Serializable()>
        Class _MovieID
            Private _imdbid As String
            Private _tmdbid As String

            Sub New()
                Clear()
            End Sub

            Public Sub Clear()
                _imdbid = String.Empty
                _tmdbid = String.Empty
            End Sub

            <XmlText()>
            Public Property ID() As String
                Get
                    Return If(Not String.IsNullOrEmpty(_imdbid), If(_imdbid.Substring(0, 2) = "tt", If(_imdbid.Trim = "tt-1", _imdbid.Replace("tt", String.Empty), _imdbid.Trim), If(Not _imdbid.Trim = "tt-1", If(Not String.IsNullOrEmpty(_imdbid), String.Concat("tt", _imdbid), String.Empty), _imdbid)), String.Empty)
                End Get
                Set(ByVal value As String)
                    _imdbid = If(Not String.IsNullOrEmpty(value), If(value.Substring(0, 2) = "tt", value.Trim, String.Concat("tt", value.Trim)), String.Empty)
                End Set
            End Property

            <XmlIgnore()>
            Public ReadOnly Property IDSpecified() As Boolean
                Get
                    Return Not String.IsNullOrEmpty(_imdbid) AndAlso Not _imdbid = "tt"
                End Get
            End Property

            <XmlAttribute("TMDB")>
            Public Property IDTMDB() As String
                Get
                    Return _tmdbid.Trim
                End Get
                Set(ByVal value As String)
                    _tmdbid = value.Trim
                End Set
            End Property

            <XmlIgnore()>
            Public ReadOnly Property IDTMDBSpecified() As Boolean
                Get
                    Return Not String.IsNullOrEmpty(_tmdbid)
                End Get
            End Property

        End Class

#End Region 'Properties

#Region "Methods"

        Public Sub AddSet(ByVal SetID As Long, ByVal SetName As String, ByVal Order As Integer, ByVal SetTMDBColID As String)
            Dim tSet = From bSet As [Set] In Sets Where bSet.ID = SetID
            Dim iSet = From bset As [Set] In Sets Where bset.TMDBColID = SetTMDBColID

            If tSet.Count > 0 Then
                Sets.Remove(tSet(0))
            End If

            If iSet.Count > 0 Then
                Sets.Remove(iSet(0))
            End If

            Sets.Add(New [Set] With {.ID = SetID, .Title = SetName, .Order = If(Order > 0, Order.ToString, String.Empty), .TMDBColID = SetTMDBColID})
        End Sub

        Public Sub AddTag(ByVal value As String)
            If String.IsNullOrEmpty(value) Then Return
            If Not _tags.Contains(value) Then
                _tags.Add(value.Trim)
            End If
        End Sub

        Public Sub AddCertification(ByVal value As String)
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

        Public Sub AddGenre(ByVal value As String)
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

        Public Sub AddStudio(ByVal value As String)
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

        Public Sub AddDirector(ByVal value As String)
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

        Public Sub AddCredit(ByVal value As String)
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

        Public Sub AddCountry(ByVal value As String)
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
            MovieID.Clear()
            _actors.Clear()
            _certifications.Clear()
            _countries.Clear()
            _credits.Clear()
            _dateadded = String.Empty
            _datemodified = String.Empty
            _directors.Clear()
            _fanart = New Fanart
            _fileInfo = New MediaInfo.Fileinfo
            _genres.Clear()
            _language = String.Empty
            _lev = 0
            _mpaa = String.Empty
            _originaltitle = String.Empty
            _outline = String.Empty
            _playcount = 0
            _plot = String.Empty
            _rating = String.Empty
            _releaseDate = String.Empty
            _runtime = String.Empty
            _scrapersource = String.Empty
            _sorttitle = String.Empty
            _studios.Clear()
            _tagline = String.Empty
            _tags.Clear()
            _thumb.Clear()
            _thumbposter = New Image
            _title = String.Empty
            _tmdbcolid = String.Empty
            _top250 = String.Empty
            _trailer = String.Empty
            _videosource = String.Empty
            _votes = String.Empty
            _xsets.Clear()
            _year = String.Empty
            _ysets = New SetContainer
        End Sub

        Public Sub CreateCachePaths_ActorsThumbs()
            Dim sPath As String = Path.Combine(Master.TempPath, "Global")

            For Each tActor As Person In Actors
                tActor.Thumb.CacheOriginalPath = Path.Combine(sPath, String.Concat("actorthumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                If Not String.IsNullOrEmpty(tActor.Thumb.URLThumb) Then
                    tActor.Thumb.CacheThumbPath = Path.Combine(sPath, String.Concat("actorthumbs\_thumbs", Path.DirectorySeparatorChar, Path.GetFileName(tActor.Thumb.URLOriginal)))
                End If
            Next
        End Sub

        Public Function CompareTo(ByVal other As Movie) As Integer Implements IComparable(Of Movie).CompareTo
            Dim retVal As Integer = (Lev).CompareTo(other.Lev)
            If retVal = 0 Then
                retVal = (Year).CompareTo(other.Year) * -1
            End If
            Return retVal
        End Function

        Public Sub RemoveSet(ByVal SetName As String)
            Dim tSet = From bSet As [Set] In Sets Where bSet.Title = SetName
            If tSet.Count > 0 Then
                Sets.Remove(tSet(0))
            End If
        End Sub

        Public Sub RemoveSet(ByVal SetID As Long)
            Dim tSet = From bSet As [Set] In Sets Where bSet.ID = SetID
            If tSet.Count > 0 Then
                Sets.Remove(tSet(0))
            End If
        End Sub

        Public Sub SaveAllActorThumbs(ByRef DBElement As Database.DBElement)
            If ActorsSpecified AndAlso Master.eSettings.MovieActorThumbsAnyEnabled Then
                Images.SaveMovieActorThumbs(DBElement)
            Else
                Images.Delete_Movie(DBElement, Enums.ModifierType.MainActorThumbs)
                DBElement.ActorThumbs.Clear()
            End If
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    <XmlRoot("movieset")>
    Public Class MovieSet

#Region "Fields"

        Private _plot As String
        Private _title As String
        Private _tmdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sID As String, ByVal sTitle As String, ByVal sPlot As String)
            Clear()
            _plot = sPlot
            _title = sTitle
            _tmdb = sID
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

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
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

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _aired = String.Empty
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
    Public Class SetContainer

#Region "Fields"

        Private _set As New List(Of [Set])

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("set")>
        Public Property Sets() As List(Of [Set])
            Get
                Return _set
            End Get
            Set(ByVal value As List(Of [Set]))
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
            _set = New List(Of [Set])
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
        Private _studio As String
        Private _studios As New List(Of String)
        Private _tags As New List(Of String)
        Private _title As String
        Private _tmdb As String
        Private _tvdb As String
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
                Return Not String.IsNullOrEmpty(_sorttitle) AndAlso Not _sorttitle = StringUtils.SortTokens_TV(_title)
            End Get
        End Property

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

        <Obsolete("This property is depreciated. Use TVShow.Genres [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Genre() As String
            Get
                Return String.Join(" / ", _genres.ToArray)
            End Get
            Set(ByVal value As String)
                _genres.Clear()
                AddGenre(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Certifications [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Certification() As String
            Get
                Return String.Join(" / ", _certifications.ToArray)
            End Get
            Set(ByVal value As String)
                _certifications.Clear()
                AddCertification(value)
            End Set
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

        <Obsolete("This property is depreciated. Use Movie.Countries [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Country() As String
            Get
                Return String.Join(" / ", _countries.ToArray)
            End Get
            Set(ByVal value As String)
                _countries.Clear()
                AddCountry(value)
            End Set
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

        <Obsolete("This property is depreciated. Use TVShow.Studios [List(Of String)] instead.")>
        <XmlIgnore()>
        Public Property Studio() As String
            Get
                Return String.Join(" / ", _studios.ToArray)
            End Get
            Set(ByVal value As String)
                _studios.Clear()
                AddStudio(value)
            End Set
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

        Public Sub AddCertification(ByVal value As String)
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

        Public Sub AddCountry(ByVal value As String)
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

        Public Sub AddGenre(ByVal value As String)
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

        Public Sub AddStudio(ByVal value As String)
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
            _studio = String.Empty
            _studios.Clear()
            _tags.Clear()
            _title = String.Empty
            _tmdb = String.Empty
            _votes = String.Empty
        End Sub

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
            _tvdb = Nothing
        End Sub

        Public Sub BlankBoxeeId()
            _boxeeTvDb = Nothing
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
                Case "1426"
                    _moviepostersize = Enums.MoviePosterSize.HD1426
                    _tvpostersize = Enums.TVPosterSize.HD1426
                    _tvseasonpostersize = Enums.TVSeasonPosterSize.HD1426
                Case "1000"
                    _tvpostersize = Enums.TVPosterSize.HD1000
                Case "1080"
                    _moviefanartsize = Enums.MovieFanartSize.HD1080
                    _tvepisodepostersize = Enums.TVEpisodePosterSize.HD1080
                    _tvfanartsize = Enums.TVFanartSize.HD1080
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

        Public Function LoadAndCache(ByVal tContentType As Enums.ContentType, Optional ByVal needFullsize As Boolean = False, Optional ByVal LoadBitmap As Boolean = False) As Boolean
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
                        ImageOriginal.FromFile(LocalFilePath, LoadBitmap)
                    ElseIf ImageThumb.HasMemoryStream AndAlso Not needFullsize AndAlso LoadBitmap Then
                        ImageThumb.FromMemoryStream()
                    ElseIf ImageOriginal.HasMemoryStream AndAlso LoadBitmap Then
                        ImageOriginal.FromMemoryStream()
                    ElseIf File.Exists(CacheThumbPath) AndAlso Not needFullsize Then
                        ImageThumb.FromFile(CacheThumbPath, LoadBitmap)
                    ElseIf File.Exists(CacheOriginalPath) Then
                        ImageOriginal.FromFile(CacheOriginalPath, LoadBitmap)
                    Else
                        If Not String.IsNullOrEmpty(URLThumb) AndAlso Not needFullsize Then
                            ImageThumb.FromWeb(URLThumb, LoadBitmap)
                            If doCache AndAlso Not String.IsNullOrEmpty(CacheThumbPath) AndAlso ImageThumb.HasMemoryStream Then
                                Directory.CreateDirectory(Directory.GetParent(CacheThumbPath).FullName)
                                ImageThumb.Save(CacheThumbPath)
                            End If
                        ElseIf Not String.IsNullOrEmpty(URLOriginal) Then
                            ImageOriginal.FromWeb(URLOriginal, LoadBitmap)
                            If doCache AndAlso Not String.IsNullOrEmpty(CacheOriginalPath) AndAlso ImageOriginal.HasMemoryStream Then
                                Directory.CreateDirectory(Directory.GetParent(CacheOriginalPath).FullName)
                                ImageOriginal.Save(CacheOriginalPath)
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

        Public Sub LoadAllImages(ByVal Type As Enums.ContentType, ByVal LoadBitmap As Boolean, ByVal exclExtraImages As Boolean)
            Banner.LoadAndCache(Type, True, LoadBitmap)
            CharacterArt.LoadAndCache(Type, True, LoadBitmap)
            ClearArt.LoadAndCache(Type, True, LoadBitmap)
            ClearLogo.LoadAndCache(Type, True, LoadBitmap)
            DiscArt.LoadAndCache(Type, True, LoadBitmap)
            Fanart.LoadAndCache(Type, True, LoadBitmap)
            Landscape.LoadAndCache(Type, True, LoadBitmap)
            Poster.LoadAndCache(Type, True, LoadBitmap)

            If Not exclExtraImages Then
                For Each tImg As Image In Extrafanarts
                    tImg.LoadAndCache(Type, True, LoadBitmap)
                Next
                For Each tImg As Image In Extrathumbs
                    tImg.LoadAndCache(Type, True, LoadBitmap)
                Next
            End If
        End Sub

        Public Sub SaveAllImages(ByRef DBElement As Database.DBElement)
            Dim tContentType As Enums.ContentType = DBElement.ContentType

            Select Case tContentType
                Case Enums.ContentType.Movie

                    'Movie Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        Banner.LocalFilePath = Banner.ImageOriginal.SaveAsMovieBanner(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainBanner)
                        Banner = New Image
                    End If

                    'Movie ClearArt
                    If ClearArt.LoadAndCache(tContentType, True) Then
                        ClearArt.LocalFilePath = ClearArt.ImageOriginal.SaveAsMovieClearArt(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainClearArt)
                        ClearArt = New Image
                    End If

                    'Movie ClearLogo
                    If ClearLogo.LoadAndCache(tContentType, True) Then
                        ClearLogo.LocalFilePath = ClearLogo.ImageOriginal.SaveAsMovieClearLogo(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainClearLogo)
                        ClearLogo = New Image
                    End If

                    'Movie DiscArt
                    If DiscArt.LoadAndCache(tContentType, True) Then
                        DiscArt.LocalFilePath = DiscArt.ImageOriginal.SaveAsMovieDiscArt(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainDiscArt)
                        DiscArt = New Image
                    End If

                    'Movie Extrafanarts
                    If Extrafanarts.Count > 0 Then
                        DBElement.ExtrafanartsPath = Images.SaveMovieExtrafanarts(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainExtrafanarts)
                        Extrafanarts = New List(Of Image)
                        DBElement.ExtrafanartsPath = String.Empty
                    End If

                    'Movie Extrathumbs
                    If Extrathumbs.Count > 0 Then
                        DBElement.ExtrathumbsPath = Images.SaveMovieExtrathumbs(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainExtrathumbs)
                        Extrathumbs = New List(Of Image)
                        DBElement.ExtrathumbsPath = String.Empty
                    End If

                    'Movie Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        Fanart.LocalFilePath = Fanart.ImageOriginal.SaveAsMovieFanart(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainFanart)
                        Fanart = New Image
                    End If

                    'Movie Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        Landscape.LocalFilePath = Landscape.ImageOriginal.SaveAsMovieLandscape(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainLandscape)
                        Landscape = New Image
                    End If

                    'Movie Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        Poster.LocalFilePath = Poster.ImageOriginal.SaveAsMoviePoster(DBElement)
                    Else
                        Images.Delete_Movie(DBElement, Enums.ModifierType.MainPoster)
                        Poster = New Image
                    End If

                Case Enums.ContentType.MovieSet

                    'MovieSet Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        Banner.LocalFilePath = Banner.ImageOriginal.SaveAsMovieSetBanner(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainBanner)
                        Banner = New Image
                    End If

                    'MovieSet ClearArt
                    If ClearArt.LoadAndCache(tContentType, True) Then
                        ClearArt.LocalFilePath = ClearArt.ImageOriginal.SaveAsMovieSetClearArt(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainClearArt)
                        ClearArt = New Image
                    End If

                    'MovieSet ClearLogo
                    If ClearLogo.LoadAndCache(tContentType, True) Then
                        ClearLogo.LocalFilePath = ClearLogo.ImageOriginal.SaveAsMovieSetClearLogo(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainClearLogo)
                        ClearLogo = New Image
                    End If

                    'MovieSet DiscArt
                    If DiscArt.LoadAndCache(tContentType, True) Then
                        DiscArt.LocalFilePath = DiscArt.ImageOriginal.SaveAsMovieSetDiscArt(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainDiscArt)
                        DiscArt = New Image
                    End If

                    'MovieSet Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        Fanart.LocalFilePath = Fanart.ImageOriginal.SaveAsMovieSetFanart(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainFanart)
                        Fanart = New Image
                    End If

                    'MovieSet Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        Landscape.LocalFilePath = Landscape.ImageOriginal.SaveAsMovieSetLandscape(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainLandscape)
                        Landscape = New Image
                    End If

                    'MovieSet Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        Poster.LocalFilePath = Poster.ImageOriginal.SaveAsMovieSetPoster(DBElement)
                    Else
                        Images.Delete_MovieSet(DBElement, Enums.ModifierType.MainPoster)
                        Poster = New Image
                    End If

                Case Enums.ContentType.TVEpisode

                    'Episode Fanart
                    If Fanart.LoadAndCache(tContentType, True) Then
                        Fanart.LocalFilePath = Fanart.ImageOriginal.SaveAsTVEpisodeFanart(DBElement)
                    Else
                        Images.Delete_TVEpisode(DBElement, Enums.ModifierType.EpisodeFanart)
                        Fanart = New Image
                    End If

                    'Episode Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        Poster.LocalFilePath = Poster.ImageOriginal.SaveAsTVEpisodePoster(DBElement)
                    Else
                        Images.Delete_TVEpisode(DBElement, Enums.ModifierType.EpisodePoster)
                        Poster = New Image
                    End If

                Case Enums.ContentType.TVSeason

                    'Season Banner
                    If Banner.LoadAndCache(tContentType, True) Then
                        If DBElement.TVSeason.Season = 999 Then
                            Banner.LocalFilePath = Banner.ImageOriginal.SaveAsTVAllSeasonsBanner(DBElement)
                        Else
                            Banner.LocalFilePath = Banner.ImageOriginal.SaveAsTVSeasonBanner(DBElement)
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
                            Fanart.LocalFilePath = Fanart.ImageOriginal.SaveAsTVAllSeasonsFanart(DBElement)
                        Else
                            Fanart.LocalFilePath = Fanart.ImageOriginal.SaveAsTVSeasonFanart(DBElement)
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
                            Landscape.LocalFilePath = Landscape.ImageOriginal.SaveAsTVAllSeasonsLandscape(DBElement)
                        Else
                            Landscape.LocalFilePath = Landscape.ImageOriginal.SaveAsTVSeasonLandscape(DBElement)
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
                            Poster.LocalFilePath = Poster.ImageOriginal.SaveAsTVAllSeasonsPoster(DBElement)
                        Else
                            Poster.LocalFilePath = Poster.ImageOriginal.SaveAsTVSeasonPoster(DBElement)
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
                        Banner.LocalFilePath = Banner.ImageOriginal.SaveAsTVShowBanner(DBElement)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainBanner)
                        Banner = New Image
                    End If

                    'Show CharacterArt
                    If CharacterArt.LoadAndCache(tContentType, True) Then
                        CharacterArt.LocalFilePath = CharacterArt.ImageOriginal.SaveAsTVShowCharacterArt(DBElement)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainCharacterArt)
                        CharacterArt = New Image
                    End If

                    'Show ClearArt
                    If ClearArt.LoadAndCache(tContentType, True) Then
                        ClearArt.LocalFilePath = ClearArt.ImageOriginal.SaveAsTVShowClearArt(DBElement)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainClearArt)
                        ClearArt = New Image
                    End If

                    'Show ClearLogo
                    If ClearLogo.LoadAndCache(tContentType, True) Then
                        ClearLogo.LocalFilePath = ClearLogo.ImageOriginal.SaveAsTVShowClearLogo(DBElement)
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
                        Fanart.LocalFilePath = Fanart.ImageOriginal.SaveAsTVShowFanart(DBElement)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainFanart)
                        Fanart = New Image
                    End If

                    'Show Landscape
                    If Landscape.LoadAndCache(tContentType, True) Then
                        Landscape.LocalFilePath = Landscape.ImageOriginal.SaveAsTVShowLandscape(DBElement)
                    Else
                        Images.Delete_TVShow(DBElement, Enums.ModifierType.MainLandscape)
                        Landscape = New Image
                    End If

                    'Show Poster
                    If Poster.LoadAndCache(tContentType, True) Then
                        Poster.LocalFilePath = Poster.ImageOriginal.SaveAsTVShowPoster(DBElement)
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
                    sID = tDBElement.Movie.ID
                    If String.IsNullOrEmpty(sID) Then
                        sID = tDBElement.Movie.TMDBID
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

        Public Sub Sort(ByVal tDBElement As Database.DBElement)
            Dim cSettings As New Settings

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    cSettings.GetBlankImages = Master.eSettings.MovieImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.MovieImagesGetEnglishImages
                    cSettings.MediaLanguage = tDBElement.Language
                    cSettings.MediaLanguageOnly = Master.eSettings.MovieImagesMediaLanguageOnly
                Case Enums.ContentType.MovieSet
                    cSettings.GetBlankImages = Master.eSettings.MovieSetImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.MovieSetImagesGetEnglishImages
                    cSettings.MediaLanguage = tDBElement.Language
                    cSettings.MediaLanguageOnly = Master.eSettings.MovieSetImagesMediaLanguageOnly
                Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                    cSettings.GetBlankImages = Master.eSettings.TVImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.TVImagesGetEnglishImages
                    cSettings.MediaLanguage = tDBElement.Language
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

            'first order list by userrating (favorite images on top), then quality enumeration (0,1,2,3,4..) ascending, then put PrefSize images on top of list and later as workaround remove "Any" Quality(=0) to bottom of list because otherwise Any images would be shown before HD images (1,2,3)
            Dim sortedqualityimages As New List(Of Image)
            'quality sorting of moviefanarts
            If Not Master.eSettings.MovieExtrafanartsPrefSize = Enums.MovieFanartSize.Any Then
                sortedqualityimages = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieFanartSize).OrderByDescending(Function(y) y.MovieFanartSize = Master.eSettings.MovieExtrafanartsPrefSize).OrderBy(Function(u) u.MovieFanartSize = Enums.MovieFanartSize.Any).ToList()
            Else
                sortedqualityimages = _mainfanarts.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MovieFanartSize).OrderBy(Function(u) u.MovieFanartSize = Enums.MovieFanartSize.Any).ToList()
            End If
            If sortedqualityimages IsNot Nothing Then
                _mainfanarts.Clear()
                _mainfanarts.AddRange(sortedqualityimages)
                sortedqualityimages.Clear()
            End If
            'quality sorting of movieposters
            If Not Master.eSettings.MoviePosterPrefSize = Enums.MoviePosterSize.Any Then
                sortedqualityimages = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MoviePosterSize).OrderByDescending(Function(y) y.MoviePosterSize = Master.eSettings.MoviePosterPrefSize).OrderBy(Function(u) u.MoviePosterSize = Enums.MoviePosterSize.Any).ToList()
            Else
                sortedqualityimages = _mainposters.OrderByDescending(Function(z) z.VoteAverage).OrderBy(Function(x) x.MoviePosterSize).OrderBy(Function(u) u.MoviePosterSize = Enums.MoviePosterSize.Any).ToList()
            End If
            If sortedqualityimages IsNot Nothing Then
                _mainposters.Clear()
                _mainposters.AddRange(sortedqualityimages)
                sortedqualityimages.Clear()
            End If

            'sort all List(Of Image) by preferred language/en/Blank/String.Empty/others
            _episodeposters = FilterImages(_episodeposters, cSettings)
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

        Private Function FilterImages(ByRef ImagesList As List(Of Image), ByVal cSettings As Settings) As List(Of Image)
            Dim FilteredList As New List(Of Image)

            For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = cSettings.MediaLanguage)
                FilteredList.Add(tmpImage)
            Next

            If (cSettings.GetEnglishImages OrElse Not cSettings.MediaLanguageOnly) AndAlso Not cSettings.MediaLanguage = "en" Then
                For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = "en")
                    FilteredList.Add(tmpImage)
                Next
            End If

            If cSettings.GetBlankImages OrElse Not cSettings.MediaLanguageOnly Then
                For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = Master.eLang.GetString(1168, "Blank"))
                    FilteredList.Add(tmpImage)
                Next
                For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = String.Empty)
                    FilteredList.Add(tmpImage)
                Next
            End If

            If Not cSettings.MediaLanguageOnly Then
                For Each tmpImage As Image In ImagesList.Where(Function(f) Not f.ShortLang = cSettings.MediaLanguage AndAlso
                                                                   Not f.ShortLang = "en" AndAlso
                                                                   Not f.ShortLang = Master.eLang.GetString(1168, "Blank") AndAlso
                                                                   Not f.ShortLang = String.Empty)
                    FilteredList.Add(tmpImage)
                Next
            End If

            Return FilteredList
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Settings

#Region "Fields"

            Dim GetBlankImages As Boolean
            Dim GetEnglishImages As Boolean
            Dim MediaLanguage As String
            Dim MediaLanguageOnly As Boolean

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

    <Serializable()>
    Public Class [Set]

#Region "Fields"

        Private _id As Long
        Private _order As String
        Private _title As String
        Private _tmdbcolid As String

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

        <XmlAttribute("order")>
        Public Property Order() As String
            Get
                Return _order
            End Get
            Set(ByVal value As String)
                _order = value
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property OrderSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_order)
            End Get
        End Property

        <XmlAttribute("tmdbcolid")>
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

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _id = -1
            _title = String.Empty
            _order = String.Empty
            _tmdbcolid = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class [Theme]

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"
        Public Property Quality As String
        Public Property URL As String ' path to image (local or url)
        Public Property WebTheme As Themes
        Public Property ShortLang As String
        Public Property LongLang As String

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _URL = String.Empty
            _Quality = String.Empty
            _WebTheme = New Themes
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class [Trailer]

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

        Public Sub SaveAllTrailers(ByRef DBElement As Database.DBElement)
            Dim tContentType As Enums.ContentType = DBElement.ContentType

            With DBElement
                Select Case tContentType
                    Case Enums.ContentType.Movie

                        'Movie Trailer
                        If .Trailer.TrailerOriginal IsNot Nothing AndAlso .Trailer.TrailerOriginal.hasMemoryStream Then
                            .Trailer.LocalFilePath = .Trailer.TrailerOriginal.SaveAsMovieTrailer(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Trailer.URLVideoStream) Then
                            .Trailer.TrailerOriginal.FromWeb(.Trailer)
                            .Trailer.LocalFilePath = .Trailer.TrailerOriginal.SaveAsMovieTrailer(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Trailer.LocalFilePath) Then
                            .Trailer.TrailerOriginal.FromFile(.Trailer.LocalFilePath)
                            .Trailer.LocalFilePath = .Trailer.TrailerOriginal.SaveAsMovieTrailer(DBElement)
                        Else
                            Trailers.DeleteMovieTrailers(DBElement)
                            .Trailer = New Trailer
                        End If
                End Select
            End With
        End Sub

#End Region 'Methods

    End Class

End Namespace

