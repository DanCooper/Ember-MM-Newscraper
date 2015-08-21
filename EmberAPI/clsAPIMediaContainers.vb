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
        Private _fanart As New MediaContainers.Image
        Private _fileInfo As New MediaInfo.Fileinfo
        Private _gueststars As New List(Of Person)
        Private _imdb As String
        Private _lastplayed As String
        Private _localfile As String
        Private _playcount As String
        Private _plot As String
        Private _poster As New MediaContainers.Image
        Private _posterurl As String
        Private _rating As String
        Private _runtime As String
        Private _scrapersource As String
        Private _season As Integer
        Private _seasoncombined As Integer
        Private _seasondvd As Integer
        Private _subepisode As Integer
        Private _title As String
        Private _tmdb As String
        Private _tvdb As String
        Private _videosource As String
        Private _votes As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._title)
            End Get
        End Property

        <XmlElement("runtime")> _
        Public Property Runtime() As String
            Get
                Return Me._runtime
            End Get
            Set(ByVal value As String)
                Me._runtime = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RuntimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._runtime)
            End Get
        End Property

        <XmlElement("aired")> _
        Public Property Aired() As String
            Get
                Return Me._aired
            End Get
            Set(ByVal value As String)
                Me._aired = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property AiredSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._aired)
            End Get
        End Property

        <XmlElement("rating")> _
        Public Property Rating() As String
            Get
                Return Me._rating.Replace(",", ".")
            End Get
            Set(ByVal value As String)
                Me._rating = value.Replace(",", ".")
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._rating)
            End Get
        End Property

        <XmlElement("videosource")> _
        Public Property VideoSource() As String
            Get
                Return Me._videosource
            End Get
            Set(ByVal value As String)
                Me._videosource = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property VideoSourceSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._videosource)
            End Get
        End Property

        <XmlElement("votes")> _
        Public Property Votes() As String
            Get
                Return Me._votes
            End Get
            Set(ByVal value As String)
                Me._votes = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property VotesSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._votes)
            End Get
        End Property

        <XmlElement("season")> _
        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SeasonSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._season.ToString)
            End Get
        End Property

        <XmlElement("episode")> _
        Public Property Episode() As Integer
            Get
                Return Me._episode
            End Get
            Set(ByVal value As Integer)
                Me._episode = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property EpisodeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._episode.ToString)
            End Get
        End Property

        <XmlElement("subepisode")> _
        Public Property SubEpisode() As Integer
            Get
                Return Me._subepisode
            End Get
            Set(ByVal value As Integer)
                Me._subepisode = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SubEpisodeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._subepisode.ToString) AndAlso Me._subepisode > 0
            End Get
        End Property

        <XmlElement("displayseason")> _
        Public Property DisplaySeason() As Integer
            Get
                Return Me._displayseason
            End Get
            Set(ByVal value As Integer)
                Me._displayseason = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property DisplaySeasonSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._displayseason.ToString) AndAlso Me._displayseason > -1
            End Get
        End Property

        <XmlElement("displayepisode")> _
        Public Property DisplayEpisode() As Integer
            Get
                Return Me._displayepisode
            End Get
            Set(ByVal value As Integer)
                Me._displayepisode = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property DisplayEpisodeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._displayepisode.ToString) AndAlso Me._displayepisode > -1
            End Get
        End Property

        <XmlElement("plot")> _
        Public Property Plot() As String
            Get
                Return Me._plot.Trim
            End Get
            Set(ByVal value As String)
                Me._plot = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._plot)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Episode.Credits [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property OldCredits() As String
            Get
                Return String.Join(" / ", _credits.ToArray)
            End Get
            Set(ByVal value As String)
                _credits.Clear()
                AddCredit(value)
            End Set
        End Property

        <XmlElement("credits")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CreditsSpecified() As Boolean
            Get
                Return Me._credits.Count > 0
            End Get
        End Property

        <XmlElement("playcount")> _
        Public Property Playcount() As String
            Get
                Return Me._playcount
            End Get
            Set(ByVal value As String)
                Me._playcount = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlaycountSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._playcount)
            End Get
        End Property

        <XmlElement("lastplayed")> _
        Public Property LastPlayed() As String
            Get
                Return Me._lastplayed
            End Get
            Set(ByVal value As String)
                Me._lastplayed = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property LastPlayedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._lastplayed)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Episode.Directors [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Director() As String
            Get
                Return String.Join(" / ", _directors.ToArray)
            End Get
            Set(ByVal value As String)
                _directors.Clear()
                AddDirector(value)
            End Set
        End Property

        <XmlElement("director")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property DirectorSpecified() As Boolean
            Get
                Return Me._directors.Count > 0
            End Get
        End Property

        <XmlElement("actor")> _
        Public Property Actors() As List(Of Person)
            Get
                Return Me._actors
            End Get
            Set(ByVal Value As List(Of Person))
                Me._actors = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ActorsSpecified() As Boolean
            Get
                Return Me._actors.Count > 0
            End Get
        End Property

        <XmlElement("gueststars")> _
        Public Property GuestStars() As List(Of Person)
            Get
                Return Me._gueststars
            End Get
            Set(ByVal Value As List(Of Person))
                Me._gueststars = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property GuestStarsSpecified() As Boolean
            Get
                Return Me._gueststars.Count > 0
            End Get
        End Property

        <XmlElement("fileinfo")> _
        Public Property FileInfo() As MediaInfo.Fileinfo
            Get
                Return Me._fileInfo
            End Get
            Set(ByVal value As MediaInfo.Fileinfo)
                Me._fileInfo = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property FileInfoSpecified() As Boolean
            Get
                If Me._fileInfo.StreamDetails.Video.Count > 0 OrElse _
                Me._fileInfo.StreamDetails.Audio.Count > 0 OrElse _
                 Me._fileInfo.StreamDetails.Subtitle.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Poster() As MediaContainers.Image
            Get
                Return Me._poster
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._poster = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property PosterURL() As String
            Get
                Return Me._posterurl
            End Get
            Set(ByVal value As String)
                Me._posterurl = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property LocalFile() As String
            Get
                Return Me._localfile
            End Get
            Set(ByVal value As String)
                Me._localfile = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property Fanart() As MediaContainers.Image
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._fanart = value
            End Set
        End Property

        <XmlElement("dateadded")> _
        Public Property DateAdded() As String
            Get
                Return Me._dateadded
            End Get
            Set(ByVal value As String)
                Me._dateadded = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property DateAddedSpecified() As Boolean
            Get
                Return Me._dateadded IsNot Nothing
            End Get
        End Property

        <XmlElement("uniqueid")> _
        Public Property TVDB() As String
            Get
                Return Me._tvdb
            End Get
            Set(ByVal value As String)
                Me._tvdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tvdb)
            End Get
        End Property

        <XmlElement("imdb")> _
        Public Property IMDB() As String
            Get
                Return Me._imdb
            End Get
            Set(ByVal value As String)
                Me._imdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._imdb)
            End Get
        End Property

        <XmlElement("tmdb")> _
        Public Property TMDB() As String
            Get
                Return Me._tmdb
            End Get
            Set(ByVal value As String)
                Me._tmdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdb)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Scrapersource() As String
            Get
                Return Me._scrapersource
            End Get
            Set(ByVal value As String)
                Me._scrapersource = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property EpisodeAbsolute() As Integer
            Get
                Return Me._episodeabsolute
            End Get
            Set(ByVal value As Integer)
                Me._episodeabsolute = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property EpisodeCombined() As Double
            Get
                Return Me._episodecombined
            End Get
            Set(ByVal value As Double)
                Me._episodecombined = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property EpisodeDVD() As Double
            Get
                Return Me._episodedvd
            End Get
            Set(ByVal value As Double)
                Me._episodedvd = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property SeasonCombined() As Integer
            Get
                Return Me._seasoncombined
            End Get
            Set(ByVal value As Integer)
                Me._seasoncombined = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property SeasonDVD() As Integer
            Get
                Return Me._seasondvd
            End Get
            Set(ByVal value As Integer)
                Me._seasondvd = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._actors.Clear()
            Me._aired = String.Empty
            Me._credits.Clear()
            Me._dateadded = String.Empty
            Me._directors.Clear()
            Me._displayepisode = -1
            Me._displayseason = -1
            Me._episode = -1
            Me._episodeabsolute = -1
            Me._episodecombined = -1
            Me._episodedvd = -1
            Me._fanart = New MediaContainers.Image
            Me._fileInfo = New MediaInfo.Fileinfo
            Me._gueststars.Clear()
            Me._imdb = String.Empty
            Me._lastplayed = String.Empty
            Me._localfile = String.Empty
            Me._playcount = String.Empty
            Me._plot = String.Empty
            Me._poster = New MediaContainers.Image
            Me._posterurl = String.Empty
            Me._rating = String.Empty
            Me._runtime = String.Empty
            Me._season = -1
            Me._seasoncombined = -1
            Me._seasondvd = -1
            Me._subepisode = -1
            Me._tmdb = String.Empty
            Me._tvdb = String.Empty
            Me._title = String.Empty
            Me._videosource = String.Empty
            Me._votes = String.Empty
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

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class EpisodeGuide

#Region "Fields"

        Private _url As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("url")> _
        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal Value As String)
                Me._url = Value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._url = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class Fanart

#Region "Fields"

        Private _thumb As New List(Of Thumb)
        Private _url As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("thumb")> _
        Public Property Thumb() As List(Of Thumb)
            Get
                Return Me._thumb
            End Get
            Set(ByVal value As List(Of Thumb))
                Me._thumb = value
            End Set
        End Property

        <XmlAttribute("url")> _
        Public Property URL() As String
            Get
                Return Me._url
            End Get
            Set(ByVal value As String)
                Me._url = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._thumb.Clear()
            Me._url = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    <XmlRoot("movie")> _
    Public Class Movie
        Implements IComparable(Of Movie)

#Region "Fields"

        Private _title As String
        Private _originaltitle As String
        Private _sorttitle As String
        Private _year As String
        Private _releaseDate As String
        Private _top250 As String
        Private _countries As New List(Of String)
        Private _rating As String
        Private _votes As String
        Private _mpaa As String
        Private _certifications As New List(Of String)
        Private _tags As New List(Of String)
        Private _genres As New List(Of String)
        Private _studios As New List(Of String)
        Private _directors As New List(Of String)
        Private _credits As New List(Of String)
        Private _tagline As String
        Private _outline As String
        Private _plot As String
        Private _runtime As String
        Private _trailer As String
        Private _playcount As String
        'Private _watched As String
        Private _actors As New List(Of Person)
        Private _thumb As New List(Of String)
        Private _fanart As New Fanart
        Private _xsets As New List(Of [Set])
        Private _ysets As New SetContainer
        Private _fileInfo As New MediaInfo.Fileinfo
        Private _lev As Integer
        Private _videosource As String
        Private _tmdbcolid As String
        Private _dateadded As String
        Private _scrapersource As String
        Private _datemodified As String
        Private _lastplayed As String
#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sID As String, ByVal sTitle As String, ByVal sYear As String, ByVal iLev As Integer)
            Me.Clear()
            Me.MovieID.ID = sID
            Me._title = sTitle
            Me._year = sYear
            Me._lev = iLev
        End Sub

        Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"
        <XmlElement("id")> _
        Public MovieID As New _MovieID

        <XmlElement("title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._title)
            End Get
        End Property

        <XmlElement("originaltitle")> _
        Public Property OriginalTitle() As String
            Get
                Return Me._originaltitle
            End Get
            Set(ByVal value As String)
                Me._originaltitle = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property OriginalTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._originaltitle)
            End Get
        End Property

        <XmlElement("sorttitle")> _
        Public Property SortTitle() As String
            Get
                Return Me._sorttitle
            End Get
            Set(ByVal value As String)
                Me._sorttitle = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SortTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._sorttitle) AndAlso Not Me._sorttitle = StringUtils.SortTokens_Movie(Me._title)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property ID() As String
            Get
                Return Me.MovieID.ID
            End Get
            Set(ByVal value As String)
                Me.MovieID.ID = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property IDMovieDB() As String
            Get
                Return Me.MovieID.IDMovieDB
            End Get
            Set(ByVal value As String)
                Me.MovieID.IDMovieDB = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property IDMovieDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me.MovieID.IDMovieDB)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property TMDBID() As String
            Get
                Return Me.MovieID.IDTMDB
            End Get
            Set(ByVal value As String)
                Me.MovieID.IDTMDB = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me.MovieID.IDTMDB)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property IMDBID() As String
            Get
                Return MovieID.ID.Replace("tt", String.Empty).Trim
            End Get
            Set(ByVal value As String)
                Me.MovieID.ID = value
            End Set
        End Property

        <XmlElement("year")> _
        Public Property Year() As String
            Get
                Return Me._year
            End Get
            Set(ByVal value As String)
                Me._year = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property YearSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._year)
            End Get
        End Property

        <XmlElement("releasedate")> _
        Public Property ReleaseDate() As String
            Get
                Return Me._releaseDate
            End Get
            Set(ByVal value As String)
                Me._releaseDate = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ReleaseDateSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._releaseDate)
            End Get
        End Property

        <XmlElement("top250")> _
        Public Property Top250() As String
            Get
                Return Me._top250
            End Get
            Set(ByVal value As String)
                Me._top250 = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property Top250Specified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._top250)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Countries [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Country() As String
            Get
                Return String.Join(" / ", _countries.ToArray)
            End Get
            Set(ByVal value As String)
                _countries.Clear()
                AddCountry(value)
            End Set
        End Property

        <XmlElement("country")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CountriesSpecified() As Boolean
            Get
                Return Me._countries.Count > 0
            End Get
        End Property

        <XmlElement("rating")> _
        Public Property Rating() As String
            Get
                Return Me._rating.Replace(",", ".")
            End Get
            Set(ByVal value As String)
                Me._rating = value.Replace(",", ".")
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._rating)
            End Get
        End Property

        <XmlElement("votes")> _
        Public Property Votes() As String
            Get
                Return Me._votes
            End Get
            Set(ByVal value As String)
                Me._votes = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property VotesSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._votes)
            End Get
        End Property

        <XmlElement("mpaa")> _
        Public Property MPAA() As String
            Get
                Return Me._mpaa
            End Get
            Set(ByVal value As String)
                Me._mpaa = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property MPAASpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._mpaa)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Certifications [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Certification() As String
            Get
                Return String.Join(" / ", _certifications.ToArray)
            End Get
            Set(ByVal value As String)
                _certifications.Clear()
                AddCertification(value)
            End Set
        End Property

        <XmlElement("certification")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CertificationsSpecified() As Boolean
            Get
                Return Me._certifications.Count > 0
            End Get
        End Property

        <XmlElement("tag")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property TagsSpecified() As Boolean
            Get
                Return Me._tags.Count > 0
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Genres [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Genre() As String
            Get
                Return String.Join(" / ", _genres.ToArray)
            End Get
            Set(ByVal value As String)
                _genres.Clear()
                AddGenre(value)
            End Set
        End Property

        <XmlElement("genre")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property GenresSpecified() As Boolean
            Get
                Return Me._genres.Count > 0
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Studios [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Studio() As String
            Get
                Return String.Join(" / ", _studios.ToArray)
            End Get
            Set(ByVal value As String)
                _studios.Clear()
                AddStudio(value)
            End Set
        End Property

        <XmlElement("studio")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property StudiosSpecified() As Boolean
            Get
                Return Me._studios.Count > 0
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Directors [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Director() As String
            Get
                Return String.Join(" / ", _directors.ToArray)
            End Get
            Set(ByVal value As String)
                _directors.Clear()
                AddDirector(value)
            End Set
        End Property

        <XmlElement("director")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property DirectorsSpecified() As Boolean
            Get
                Return Me._directors.Count > 0
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Credits [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property OldCredits() As String
            Get
                Return String.Join(" / ", _credits.ToArray)
            End Get
            Set(ByVal value As String)
                _credits.Clear()
                AddCredit(value)
            End Set
        End Property

        <XmlElement("credits")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CreditsSpecified() As Boolean
            Get
                Return Me._credits.Count > 0
            End Get
        End Property

        <XmlElement("tagline")> _
        Public Property Tagline() As String
            Get
                Return Me._tagline.Trim
            End Get
            Set(ByVal value As String)
                Me._tagline = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TaglineSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tagline)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Scrapersource() As String
            Get
                Return Me._scrapersource
            End Get
            Set(ByVal value As String)
                Me._scrapersource = value
            End Set
        End Property

        <XmlElement("outline")> _
        Public Property Outline() As String
            Get
                Return Me._outline.Trim
            End Get
            Set(ByVal value As String)
                Me._outline = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property OutlineSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._outline)
            End Get
        End Property

        <XmlElement("plot")> _
        Public Property Plot() As String
            Get
                Return Me._plot.Trim
            End Get
            Set(ByVal value As String)
                Me._plot = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._plot)
            End Get
        End Property

        <XmlElement("runtime")> _
        Public Property Runtime() As String
            Get
                Return Me._runtime
            End Get
            Set(ByVal value As String)
                Me._runtime = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RuntimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._runtime)
            End Get
        End Property

        <XmlElement("trailer")> _
        Public Property Trailer() As String
            Get
                Return Me._trailer
            End Get
            Set(ByVal value As String)
                Me._trailer = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TrailerSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._trailer)
            End Get
        End Property

        <XmlElement("playcount")> _
        Public Property PlayCount() As String
            Get
                Return Me._playcount
            End Get
            Set(ByVal value As String)
                Me._playcount = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlayCountSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._playcount) AndAlso Not Me._playcount = "0"
            End Get
        End Property

        <XmlElement("lastplayed")> _
        Public Property LastPlayed() As String
            Get
                Return Me._lastplayed
            End Get
            Set(ByVal value As String)
                Me._lastplayed = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property LastPlayedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._lastplayed)
            End Get
        End Property

        <XmlElement("dateadded")> _
        Public Property DateAdded() As String
            Get
                Return Me._dateadded
            End Get
            Set(ByVal value As String)
                Me._dateadded = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property DateAddedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._dateadded)
            End Get
        End Property

        <XmlElement("datemodified")> _
        Public Property DateModified() As String
            Get
                Return Me._datemodified
            End Get
            Set(ByVal value As String)
                Me._datemodified = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property DateModifiedSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._datemodified)
            End Get
        End Property

        '<XmlElement("watched")> _
        'Public Property Watched() As String
        '    Get
        '        Return Me._watched
        '    End Get
        '    Set(ByVal value As String)
        '        Me._watched = value
        '    End Set
        'End Property

        '<XmlIgnore()> _
        'Public ReadOnly Property WatchedSpecified() As Boolean
        '    Get
        '        Return Not String.IsNullOrEmpty(Me._watched)
        '    End Get
        'End Property

        <XmlElement("actor")> _
        Public Property Actors() As List(Of Person)
            Get
                Return Me._actors
            End Get
            Set(ByVal Value As List(Of Person))
                Me._actors = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ActorsSpecified() As Boolean
            Get
                Return Me._actors.Count > 0
            End Get
        End Property

        <XmlElement("thumb")> _
        Public Property Thumb() As List(Of String)
            Get
                Return Me._thumb
            End Get
            Set(ByVal value As List(Of String))
                Me._thumb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ThumbSpecified() As Boolean
            Get
                Return Me._thumb.Count > 0
            End Get
        End Property

        <XmlElement("fanart")> _
        Public Property Fanart() As Fanart
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As Fanart)
                Me._fanart = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property FanartSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._fanart.URL)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Sets() As List(Of [Set])
            Get
                Return If(Master.eSettings.MovieYAMJCompatibleSets, Me._ysets.Sets, Me._xsets)
            End Get
            Set(ByVal value As List(Of [Set]))
                If Master.eSettings.MovieYAMJCompatibleSets Then
                    Me._ysets.Sets = value
                Else
                    Me._xsets = value
                End If
            End Set
        End Property

        <XmlElement("set")> _
        Public Property XSets() As List(Of [Set])
            Get
                Return Me._xsets
            End Get
            Set(ByVal value As List(Of [Set]))
                _xsets = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property XSetsSpecified() As Boolean
            Get
                Return Me._xsets.Count > 0
            End Get
        End Property

        <XmlElement("sets")> _
        Public Property YSets() As SetContainer
            Get
                Return _ysets
            End Get
            Set(ByVal value As SetContainer)
                _ysets = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property YSetsSpecified() As Boolean
            Get
                Return Me._ysets.Sets.Count > 0
            End Get
        End Property

        <XmlElement("fileinfo")> _
        Public Property FileInfo() As MediaInfo.Fileinfo
            Get
                Return Me._fileInfo
            End Get
            Set(ByVal value As MediaInfo.Fileinfo)
                Me._fileInfo = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property FileInfoSpecified() As Boolean
            Get
                If Me._fileInfo.StreamDetails.Video IsNot Nothing AndAlso _
                (Me._fileInfo.StreamDetails.Video.Count > 0 OrElse _
                 Me._fileInfo.StreamDetails.Audio.Count > 0 OrElse _
                 Me._fileInfo.StreamDetails.Subtitle.Count > 0) Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Lev() As Integer
            Get
                Return Me._lev
            End Get
            Set(ByVal value As Integer)
                Me._lev = value
            End Set
        End Property

        <XmlElement("videosource")> _
        Public Property VideoSource() As String
            Get
                Return Me._videosource
            End Get
            Set(ByVal value As String)
                Me._videosource = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property VideoSourceSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._videosource)
            End Get
        End Property

        <XmlElement("tmdbcolid")> _
        Public Property TMDBColID() As String
            Get
                Return Me._tmdbcolid
            End Get
            Set(ByVal value As String)
                Me._tmdbcolid = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBColIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdbcolid)
            End Get
        End Property

        <Serializable()> _
        Class _MovieID
            Private _imdbid As String
            Private _moviedb As String
            Private _tmdbid As String

            Sub New()
                Me.Clear()
            End Sub

            Public Sub Clear()
                _imdbid = String.Empty
                _moviedb = String.Empty
                _tmdbid = String.Empty
            End Sub

            <XmlText()> _
            Public Property ID() As String
                Get
                    Return If(Not String.IsNullOrEmpty(_imdbid), If(_imdbid.Substring(0, 2) = "tt", If(Not String.IsNullOrEmpty(_moviedb) AndAlso _imdbid.Trim = "tt-1", _imdbid.Replace("tt", String.Empty), _imdbid.Trim), If(Not _imdbid.Trim = "tt-1", If(Not String.IsNullOrEmpty(_imdbid), String.Concat("tt", _imdbid), String.Empty), _imdbid)), String.Empty)
                End Get
                Set(ByVal value As String)
                    _imdbid = If(Not String.IsNullOrEmpty(value), If(value.Substring(0, 2) = "tt", value.Trim, String.Concat("tt", value.Trim)), String.Empty)
                End Set
            End Property

            <XmlIgnore()> _
            Public ReadOnly Property IDSpecified() As Boolean
                Get
                    Return Not String.IsNullOrEmpty(_imdbid) AndAlso Not _imdbid = "tt"
                End Get
            End Property

            <XmlAttribute("moviedb")> _
            Public Property IDMovieDB() As String
                Get
                    Return _moviedb.Trim
                End Get
                Set(ByVal value As String)
                    Me._moviedb = value.Trim
                End Set
            End Property

            <XmlIgnore()> _
            Public ReadOnly Property IDMovieDBSpecified() As Boolean
                Get
                    Return Not String.IsNullOrEmpty(Me._moviedb)
                End Get
            End Property

            <XmlAttribute("TMDB")> _
            Public Property IDTMDB() As String
                Get
                    Return _tmdbid.Trim
                End Get
                Set(ByVal value As String)
                    Me._tmdbid = value.Trim
                End Set
            End Property

            <XmlIgnore()> _
            Public ReadOnly Property IDTMDBSpecified() As Boolean
                Get
                    Return Not String.IsNullOrEmpty(Me._tmdbid)
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
            'Me._imdbid = String.Empty
            Me._actors.Clear()
            Me._certifications.Clear()
            Me._countries.Clear()
            Me._credits.Clear()
            Me._dateadded = String.Empty
            Me._datemodified = String.Empty
            Me._directors.Clear()
            Me._fanart = New Fanart
            Me._fileInfo = New MediaInfo.Fileinfo
            Me._tags.Clear()
            Me._genres.Clear()
            Me._lev = 0
            Me._mpaa = String.Empty
            Me._originaltitle = String.Empty
            Me._outline = String.Empty
            Me._playcount = String.Empty
            Me._plot = String.Empty
            Me._rating = String.Empty
            Me._releaseDate = String.Empty
            Me._runtime = String.Empty
            Me._scrapersource = String.Empty
            Me._sorttitle = String.Empty
            Me._studios.Clear()
            Me._tagline = String.Empty
            Me._thumb.Clear()
            Me._title = String.Empty
            Me._tmdbcolid = String.Empty
            Me._top250 = String.Empty
            Me._trailer = String.Empty
            Me._videosource = String.Empty
            Me._votes = String.Empty
            Me._xsets.Clear()
            Me._year = String.Empty
            Me._ysets = New SetContainer
            Me.MovieID.Clear()
        End Sub

        Public Sub ClearForOfflineHolder()
            'Me._imdbid = String.Empty
            'Me._title = String.Empty
            Me._originaltitle = String.Empty
            Me._sorttitle = String.Empty
            'Me._year = String.Empty
            Me._rating = String.Empty
            Me._votes = String.Empty
            Me._mpaa = String.Empty
            Me._top250 = String.Empty
            Me._countries.Clear()
            Me._outline = String.Empty
            Me._plot = String.Empty
            Me._tagline = String.Empty
            Me._trailer = String.Empty
            Me._certifications.Clear()
            Me._tags.Clear()
            Me._genres.Clear()
            Me._runtime = String.Empty
            Me._releaseDate = String.Empty
            Me._studios.Clear()
            Me._directors.Clear()
            Me._credits.Clear()
            Me._playcount = String.Empty
            Me._thumb.Clear()
            Me._fanart = New Fanart
            Me._actors.Clear()
            ' Me._fileInfo = New MediaInfo.Fileinfo
            Me._ysets = New SetContainer
            Me._xsets.Clear()
            Me._lev = 0
            'Me._videosource = String.Empty
            Me._dateadded = String.Empty
            Me._datemodified = String.Empty
            Me.MovieID.Clear()
        End Sub

        Public Function CompareTo(ByVal other As Movie) As Integer Implements IComparable(Of Movie).CompareTo
            Dim retVal As Integer = (Me.Lev).CompareTo(other.Lev)
            If retVal = 0 Then
                retVal = (Me.Year).CompareTo(other.Year) * -1
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

#End Region 'Methods

    End Class

    <Serializable()> _
    <XmlRoot("movieset")> _
    Public Class MovieSet

#Region "Fields"

        Private _plot As String
        Private _title As String
        Private _tmdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sID As String, ByVal sTitle As String, ByVal sPlot As String)
            Me.Clear()
            Me._plot = sPlot
            Me._title = sTitle
            Me._tmdb = sID
        End Sub

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._title)
            End Get
        End Property

        <XmlElement("id")> _
        Public Property TMDB() As String
            Get
                Return Me._tmdb.Trim
            End Get
            Set(ByVal value As String)
                Me._tmdb = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdb)
            End Get
        End Property

        <XmlElement("plot")> _
        Public Property Plot() As String
            Get
                Return Me._plot.Trim
            End Get
            Set(ByVal value As String)
                Me._plot = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._plot)
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

    <Serializable()> _
    Public Class Person

#Region "Fields"

        Private _id As Long
        Private _imdb As String
        Private _name As String
        Private _order As Integer
        Private _role As String
        Private _thumbpath As String
        Private _thumburl As String
        Private _tmdb As String
        Private _tvdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sName As String)
            Me._name = sName
        End Sub

        Public Sub New(ByVal sName As String, ByVal sRole As String, ByVal sThumb As String)
            Me._name = sName
            Me._role = sRole
            Me._thumburl = sThumb
        End Sub

        Public Sub New()
            Me.Clean()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlIgnore()> _
        Public Property ID() As Long
            Get
                Return Me._id
            End Get
            Set(ByVal Value As Long)
                Me._id = Value
            End Set
        End Property

        <XmlElement("name")> _
        Public Property Name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal Value As String)
                Me._name = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._name)
            End Get
        End Property

        <XmlElement("role")> _
        Public Property Role() As String
            Get
                Return Me._role
            End Get
            Set(ByVal Value As String)
                Me._role = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RoleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._role)
            End Get
        End Property

        <XmlElement("order")> _
        Public Property Order() As Integer
            Get
                Return Me._order
            End Get
            Set(ByVal Value As Integer)
                Me._order = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property OrderSpecified() As Boolean
            Get
                Return Me._order > -1
            End Get
        End Property

        <XmlElement("thumb")> _
        Public Property ThumbURL() As String
            Get
                Return Me._thumburl
            End Get
            Set(ByVal Value As String)
                Me._thumburl = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ThumbURLSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._thumburl)
            End Get
        End Property

        <XmlIgnore()> _
        Public Property ThumbPath() As String
            Get
                Return Me._thumbpath
            End Get
            Set(ByVal Value As String)
                Me._thumbpath = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ThumbPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._thumbpath)
            End Get
        End Property

        <XmlElement("imdbid")> _
        Public Property IMDB() As String
            Get
                Return Me._imdb
            End Get
            Set(ByVal Value As String)
                Me._imdb = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._imdb)
            End Get
        End Property

        <XmlElement("tmdbid")> _
        Public Property TMDB() As String
            Get
                Return Me._tmdb
            End Get
            Set(ByVal Value As String)
                Me._tmdb = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdb)
            End Get
        End Property

        <XmlElement("tvdbid")> _
        Public Property TVDB() As String
            Get
                Return Me._tvdb
            End Get
            Set(ByVal Value As String)
                Me._tvdb = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tvdb)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clean()
            Me._id = -1
            Me._name = String.Empty
            Me._order = -1
            Me._role = String.Empty
            Me._thumbpath = String.Empty
            Me._thumburl = String.Empty
            Me._tmdb = String.Empty
        End Sub

        Public Overrides Function ToString() As String
            Return Me._name
        End Function

#End Region 'Methods

    End Class

    <Serializable()> _
    <XmlRoot("seasondetails")> _
    Public Class SeasonDetails

#Region "Fields"

        Private _aired As String
        Private _plot As String
        Private _season As Integer
        Private _title As String
        Private _tmdb As String
        Private _tvdb As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("aired")> _
        Public Property Aired() As String
            Get
                Return Me._aired
            End Get
            Set(ByVal value As String)
                Me._aired = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property AiredSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._aired)
            End Get
        End Property

        <XmlElement("title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._title)
            End Get
        End Property

        <XmlElement("season")> _
        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SeasonSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._season.ToString)
            End Get
        End Property

        <XmlElement("plot")> _
        Public Property Plot() As String
            Get
                Return Me._plot.Trim
            End Get
            Set(ByVal value As String)
                Me._plot = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._plot)
            End Get
        End Property

        <XmlElement("tvdb")> _
        Public Property TVDB() As String
            Get
                Return Me._tvdb
            End Get
            Set(ByVal value As String)
                Me._tvdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tvdb)
            End Get
        End Property

        <XmlElement("tmdb")> _
        Public Property TMDB() As String
            Get
                Return Me._tmdb
            End Get
            Set(ByVal value As String)
                Me._tmdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdb)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._aired = String.Empty
            Me._plot = String.Empty
            Me._season = -1
            Me._tmdb = String.Empty
            Me._tvdb = String.Empty
            Me._title = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    <XmlRoot("seasons")> _
    Public Class Seasons

#Region "Fields"

        Private _seasons As New List(Of SeasonDetails)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("seasondetails")> _
        Public Property Seasons() As List(Of SeasonDetails)
            Get
                Return Me._seasons
            End Get
            Set(ByVal value As List(Of SeasonDetails))
                Me._seasons = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SeasonsSpecified() As Boolean
            Get
                Return Me._seasons.Count > 0
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._seasons.Clear()
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class SetContainer

#Region "Fields"

        Private _set As New List(Of [Set])

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("set")> _
        Public Property Sets() As List(Of [Set])
            Get
                Return _set
            End Get
            Set(ByVal value As List(Of [Set]))
                _set = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SetsSpecified() As Boolean
            Get
                Return Me._set.Count > 0
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._set = New List(Of [Set])
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class Thumb

#Region "Fields"

        Private _preview As String
        Private _text As String

#End Region 'Fields

#Region "Properties"

        <XmlAttribute("preview")> _
        Public Property Preview() As String
            Get
                Return Me._preview
            End Get
            Set(ByVal Value As String)
                Me._preview = Value
            End Set
        End Property

        <XmlText()> _
        Public Property [Text]() As String
            Get
                Return Me._text
            End Get
            Set(ByVal Value As String)
                Me._text = Value
            End Set
        End Property

#End Region 'Properties

    End Class

    <Serializable()> _
    <XmlRoot("tvshow")> _
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
        Private _knownepisodes As New List(Of MediaContainers.EpisodeDetails)
        Private _knownseasons As New List(Of MediaContainers.SeasonDetails)
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
            Me.Clear()
            Me._tvdb = sTVDBID
            Me._title = sTitle
            Me._premiered = sPremiered
        End Sub

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlElement("title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._title)
            End Get
        End Property

        <XmlElement("originaltitle")> _
        Public Property OriginalTitle() As String
            Get
                Return Me._originaltitle
            End Get
            Set(ByVal value As String)
                Me._originaltitle = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property OriginalTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._originaltitle)
            End Get
        End Property

        <XmlElement("sorttitle")> _
        Public Property SortTitle() As String
            Get
                Return Me._sorttitle
            End Get
            Set(ByVal value As String)
                Me._sorttitle = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SortTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._sorttitle) AndAlso Not Me._sorttitle = StringUtils.SortTokens_TV(Me._title)
            End Get
        End Property

        <XmlElement("id")> _
        Public Property TVDB() As String
            Get
                Return Me._tvdb.Trim
            End Get
            Set(ByVal value As String)
                If Integer.TryParse(value, 0) Then Me._tvdb = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TVDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tvdb)
            End Get
        End Property

        <XmlElement("imdb")> _
        Public Property IMDB() As String
            Get
                Return Me._imdb
            End Get
            Set(ByVal value As String)
                Me._imdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property IMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._imdb)
            End Get
        End Property

        <XmlElement("tmdb")> _
        Public Property TMDB() As String
            Get
                Return Me._tmdb
            End Get
            Set(ByVal value As String)
                Me._tmdb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdb)
            End Get
        End Property

        <XmlElement("boxeeTvDb")> _
        Public Property BoxeeTvDb() As String
            Get
                Return Me._boxeeTvDb
            End Get
            Set(ByVal value As String)
                If Integer.TryParse(value, 0) Then Me._boxeeTvDb = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property BoxeeTvDbSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._boxeeTvDb)
            End Get
        End Property

        <XmlElement("episodeguide")> _
        Public Property EpisodeGuide() As EpisodeGuide
            Get
                Return Me._episodeguide
            End Get
            Set(ByVal value As EpisodeGuide)
                Me._episodeguide = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property EpisodeGuideSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._episodeguide.URL)
            End Get
        End Property

        <XmlElement("rating")> _
        Public Property Rating() As String
            Get
                Return Me._rating.Replace(",", ".")
            End Get
            Set(ByVal value As String)
                Me._rating = value.Replace(",", ".")
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RatingSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._rating)
            End Get
        End Property

        <XmlElement("votes")> _
        Public Property Votes() As String
            Get
                Return Me._votes
            End Get
            Set(ByVal value As String)
                Me._votes = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property VotesSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._votes)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use TVShow.Genres [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Genre() As String
            Get
                Return String.Join(" / ", _genres.ToArray)
            End Get
            Set(ByVal value As String)
                _genres.Clear()
                AddGenre(value)
            End Set
        End Property

        <XmlElement("genre")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property GenresSpecified() As Boolean
            Get
                Return _genres.Count > 0
            End Get
        End Property

        <XmlElement("director")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property DirectorsSpecified() As Boolean
            Get
                Return _directors.Count > 0
            End Get
        End Property

        <XmlElement("mpaa")> _
        Public Property MPAA() As String
            Get
                Return Me._mpaa
            End Get
            Set(ByVal value As String)
                Me._mpaa = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property MPAASpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._mpaa)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Certifications [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Certification() As String
            Get
                Return String.Join(" / ", _certifications.ToArray)
            End Get
            Set(ByVal value As String)
                _certifications.Clear()
                AddCertification(value)
            End Set
        End Property

        <XmlElement("certification")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CertificationsSpecified() As Boolean
            Get
                Return Me._certifications.Count > 0
            End Get
        End Property

        <Obsolete("This property is depreciated. Use Movie.Countries [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Country() As String
            Get
                Return String.Join(" / ", _countries.ToArray)
            End Get
            Set(ByVal value As String)
                _countries.Clear()
                AddCountry(value)
            End Set
        End Property

        <XmlElement("country")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CountriesSpecified() As Boolean
            Get
                Return Me._countries.Count > 0
            End Get
        End Property

        <XmlElement("premiered")> _
        Public Property Premiered() As String
            Get
                Return Me._premiered
            End Get
            Set(ByVal value As String)
                Me._premiered = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PremieredSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._premiered)
            End Get
        End Property

        <Obsolete("This property is depreciated. Use TVShow.Studios [List(Of String)] instead.")> _
        <XmlIgnore()> _
        Public Property Studio() As String
            Get
                Return String.Join(" / ", _studios.ToArray)
            End Get
            Set(ByVal value As String)
                _studios.Clear()
                AddStudio(value)
            End Set
        End Property

        <XmlElement("studio")> _
        Public Property Studios() As List(Of String)
            Get
                Return Me._studios
            End Get
            Set(ByVal value As List(Of String))
                Me._studios = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property StudiosSpecified() As Boolean
            Get
                Return _studios.Count > 0
            End Get
        End Property

        <XmlElement("status")> _
        Public Property Status() As String
            Get
                Return Me._status
            End Get
            Set(ByVal value As String)
                Me._status = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property StatusSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._status)
            End Get
        End Property

        <XmlElement("plot")> _
        Public Property Plot() As String
            Get
                Return Me._plot.Trim
            End Get
            Set(ByVal value As String)
                Me._plot = value.Trim
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property PlotSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._plot)
            End Get
        End Property

        <XmlElement("tag")> _
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

        <XmlElement("runtime")> _
        Public Property Runtime() As String
            Get
                Return Me._runtime
            End Get
            Set(ByVal value As String)
                Me._runtime = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property RuntimeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._runtime)
            End Get
        End Property

        <XmlElement("actor")> _
        Public Property Actors() As List(Of Person)
            Get
                Return Me._actors
            End Get
            Set(ByVal Value As List(Of Person))
                Me._actors = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property ActorsSpecified() As Boolean
            Get
                Return Me._actors.Count > 0
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Scrapersource() As String
            Get
                Return Me._scrapersource
            End Get
            Set(ByVal value As String)
                Me._scrapersource = value
            End Set
        End Property

        <XmlElement("creator")> _
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

        <XmlIgnore()> _
        Public ReadOnly Property CreatedBySpecified() As Boolean
            Get
                Return _creators.Count > 0
            End Get
        End Property

        <XmlElement("seasons")> _
        Public Property Seasons() As Seasons
            Get
                Return Me._seasons
            End Get
            Set(ByVal value As Seasons)
                Me._seasons = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property SeasonsSpecified() As Boolean
            Get
                Return Me._seasons.Seasons.Count > 0
            End Get
        End Property

        <XmlIgnore()> _
        Public Property KnownEpisodes() As List(Of MediaContainers.EpisodeDetails)
            Get
                Return Me._knownepisodes
            End Get
            Set(ByVal value As List(Of MediaContainers.EpisodeDetails))
                Me._knownepisodes = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property KnownEpisodesSpecified() As Boolean
            Get
                Return Me._knownepisodes.Count > 0
            End Get
        End Property

        <XmlIgnore()> _
        Public Property KnownSeasons() As List(Of MediaContainers.SeasonDetails)
            Get
                Return Me._knownseasons
            End Get
            Set(ByVal value As List(Of MediaContainers.SeasonDetails))
                Me._knownseasons = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property KnownSeasonsSpecified() As Boolean
            Get
                Return Me._knownseasons.Count > 0
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

        Public Sub BlankId()
            Me._tvdb = Nothing
        End Sub

        Public Sub BlankBoxeeId()
            Me._boxeeTvDb = Nothing
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
        Private _ischecked As Boolean
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
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property CacheOriginalPath() As String
            Get
                Return Me._cacheoriginalpath
            End Get
            Set(ByVal value As String)
                Me._cacheoriginalpath = value
            End Set
        End Property

        Public Property CacheThumbPath() As String
            Get
                Return Me._cachethumbpath
            End Get
            Set(ByVal value As String)
                Me._cachethumbpath = value
            End Set
        End Property

        Public Property Disc() As Integer
            Get
                Return Me._disc
            End Get
            Set(ByVal value As Integer)
                Me._disc = value
            End Set
        End Property

        Public Property DiscType() As String
            Get
                Return Me._disctype
            End Get
            Set(ByVal value As String)
                Me._disctype = If(value.ToLower = "3d", "3D", If(value.ToLower = "cd", "CD", If(value.ToLower = "dvd", "DVD", If(value.ToLower = "bluray", "BluRay", value))))
            End Set
        End Property

        Public Property Episode() As Integer
            Get
                Return Me._episode
            End Get
            Set(ByVal value As Integer)
                Me._episode = value
            End Set
        End Property

        Public Property Height() As String
            Get
                Return Me._height
            End Get
            Set(ByVal value As String)
                Me._height = value
                DetectImageSize(value)
            End Set
        End Property

        Public Property ImageOriginal() As Images
            Get
                Return Me._imageoriginal
            End Get
            Set(ByVal value As Images)
                Me._imageoriginal = value
            End Set
        End Property

        Public Property ImageThumb() As Images
            Get
                Return Me._imagethumb
            End Get
            Set(ByVal value As Images)
                Me._imagethumb = value
            End Set
        End Property

        Public Property Index() As Integer
            Get
                Return Me._index
            End Get
            Set(ByVal value As Integer)
                Me._index = value
            End Set
        End Property

        Public Property IsChecked() As Boolean
            Get
                Return Me._ischecked
            End Get
            Set(ByVal value As Boolean)
                Me._ischecked = value
            End Set
        End Property

        Public Property Likes() As Integer
            Get
                Return Me._likes
            End Get
            Set(ByVal value As Integer)
                Me._likes = value
            End Set
        End Property

        Public Property LocalFilePath() As String
            Get
                Return Me._localfilepath
            End Get
            Set(ByVal value As String)
                Me._localfilepath = value
            End Set
        End Property

        Public Property LongLang() As String
            Get
                Return Me._longlang
            End Get
            Set(ByVal value As String)
                Me._longlang = value
            End Set
        End Property

        Public ReadOnly Property MovieBannerSize() As Enums.MovieBannerSize
            Get
                Return Me._moviebannersize
            End Get
        End Property

        Public ReadOnly Property MovieFanartSize() As Enums.MovieFanartSize
            Get
                Return Me._moviefanartsize
            End Get
        End Property

        Public ReadOnly Property MoviePosterSize() As Enums.MoviePosterSize
            Get
                Return Me._moviepostersize
            End Get
        End Property

        Public Property Scraper() As String
            Get
                Return Me._scraper
            End Get
            Set(ByVal value As String)
                Me._scraper = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

        Public Property ShortLang() As String
            Get
                Return Me._shortlang
            End Get
            Set(ByVal value As String)
                Me._shortlang = value
            End Set
        End Property

        Public ReadOnly Property TVBannerSize() As Enums.TVBannerSize
            Get
                Return Me._tvbannersize
            End Get
        End Property

        Public Property TVBannerType() As Enums.TVBannerType
            Get
                Return Me._tvbannertype
            End Get
            Set(ByVal value As Enums.TVBannerType)
                Me._tvbannertype = value
            End Set
        End Property

        Public ReadOnly Property TVEpisodePosterSize() As Enums.TVEpisodePosterSize
            Get
                Return Me._tvepisodepostersize
            End Get
        End Property

        Public ReadOnly Property TVFanartSize() As Enums.TVFanartSize
            Get
                Return Me._tvfanartsize
            End Get
        End Property

        Public ReadOnly Property TVPosterSize() As Enums.TVPosterSize
            Get
                Return Me._tvpostersize
            End Get
        End Property

        Public ReadOnly Property TVSeasonPosterSize() As Enums.TVSeasonPosterSize
            Get
                Return Me._tvseasonpostersize
            End Get
        End Property

        Public Property URLOriginal() As String
            Get
                Return Me._urloriginal
            End Get
            Set(ByVal value As String)
                Me._urloriginal = value
            End Set
        End Property

        Public Property URLThumb() As String
            Get
                Return Me._urlthumb
            End Get
            Set(ByVal value As String)
                Me._urlthumb = value
            End Set
        End Property

        Public Property VoteAverage() As String
            Get
                Return Me._voteaverage
            End Get
            Set(ByVal value As String)
                Me._voteaverage = value
            End Set
        End Property

        Public Property VoteCount() As Integer
            Get
                Return Me._votecount
            End Get
            Set(ByVal value As Integer)
                Me._votecount = value
            End Set
        End Property

        Public Property Width() As String
            Get
                Return Me._width
            End Get
            Set(ByVal value As String)
                Me._width = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._cachethumbpath = String.Empty
            Me._disc = 0
            Me._disctype = String.Empty
            Me._episode = -1
            Me._height = String.Empty
            Me._imageoriginal = New Images
            Me._imagethumb = New Images
            Me._index = 0
            Me._ischecked = False
            Me._likes = 0
            Me._localfilepath = String.Empty
            Me._longlang = String.Empty
            Me._moviebannersize = Enums.MovieBannerSize.Any
            Me._moviefanartsize = Enums.MovieFanartSize.Any
            Me._moviepostersize = Enums.MoviePosterSize.Any
            Me._scraper = String.Empty
            Me._season = -1
            Me._shortlang = String.Empty
            Me._tvbannersize = Enums.TVBannerSize.Any
            Me._tvbannertype = Enums.TVBannerType.Any
            Me._tvepisodepostersize = Enums.TVEpisodePosterSize.Any
            Me._tvfanartsize = Enums.TVFanartSize.Any
            Me._tvpostersize = Enums.TVPosterSize.Any
            Me._tvseasonpostersize = Enums.TVSeasonPosterSize.Any
            Me._urloriginal = String.Empty
            Me._urlthumb = String.Empty
            Me._voteaverage = String.Empty
            Me._votecount = 0
            Me._width = String.Empty
        End Sub

        Private Sub DetectImageSize(ByRef strHeigth As String)
            Select Case strHeigth
                Case "2100"
                    Me._moviepostersize = Enums.MoviePosterSize.HD2100
                Case "1500"
                    Me._moviepostersize = Enums.MoviePosterSize.HD1500
                    Me._tvpostersize = Enums.TVPosterSize.HD1500
                    Me._tvseasonpostersize = Enums.TVSeasonPosterSize.HD1500
                Case "1426"
                    Me._moviepostersize = Enums.MoviePosterSize.HD1426
                    Me._tvpostersize = Enums.TVPosterSize.HD1426
                    Me._tvseasonpostersize = Enums.TVSeasonPosterSize.HD1426
                Case "1000"
                    Me._tvpostersize = Enums.TVPosterSize.HD1000
                Case "1080"
                    Me._moviefanartsize = Enums.MovieFanartSize.HD1080
                    Me._tvepisodepostersize = Enums.TVEpisodePosterSize.HD1080
                    Me._tvfanartsize = Enums.TVFanartSize.HD1080
                Case "720"
                    Me._moviefanartsize = Enums.MovieFanartSize.HD720
                    Me._tvfanartsize = Enums.TVFanartSize.HD720
                Case "578"
                    Me._tvseasonpostersize = Enums.TVSeasonPosterSize.HD578
                Case "225"
                    Me._tvepisodepostersize = Enums.TVEpisodePosterSize.SD225
                Case "185"
                    Me._moviebannersize = Enums.MovieBannerSize.HD185
                    Me._tvbannersize = Enums.TVBannerSize.HD185
                Case "140"
                    Me._tvbannersize = Enums.TVBannerSize.HD140
                Case Else
                    Me._moviebannersize = Enums.MovieBannerSize.Any
                    Me._moviefanartsize = Enums.MovieFanartSize.Any
                    Me._moviepostersize = Enums.MoviePosterSize.Any
                    Me._tvbannersize = Enums.TVBannerSize.Any
                    Me._tvepisodepostersize = Enums.TVEpisodePosterSize.Any
                    Me._tvfanartsize = Enums.TVFanartSize.Any
                    Me._tvpostersize = Enums.TVPosterSize.Any
                    Me._tvseasonpostersize = Enums.TVSeasonPosterSize.Any
            End Select
        End Sub

        Public Function CompareTo(ByVal other As [Image]) As Integer Implements IComparable(Of [Image]).CompareTo
            Dim retVal As Integer = (Me.ShortLang).CompareTo(other.ShortLang)
            Return retVal
        End Function

#End Region 'Methods

    End Class

    <Serializable()> _
    Public Class ImagesContainer

#Region "Fields"

        Private _extrafanarts As New List(Of Image)
        Private _extrathumbs As New List(Of Image)
        Private _banner As New Image
        Private _characterart As New Image
        Private _clearart As New Image
        Private _clearlogo As New Image
        Private _discart As New Image
        Private _fanart As New Image
        Private _landscape As New Image
        Private _poster As New Image

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Banner() As Image
            Get
                Return Me._banner
            End Get
            Set(ByVal value As Image)
                Me._banner = value
            End Set
        End Property

        Public Property CharacterArt() As Image
            Get
                Return Me._characterart
            End Get
            Set(ByVal value As Image)
                Me._characterart = value
            End Set
        End Property

        Public Property ClearArt() As Image
            Get
                Return Me._clearart
            End Get
            Set(ByVal value As Image)
                Me._clearart = value
            End Set
        End Property

        Public Property ClearLogo() As Image
            Get
                Return Me._clearlogo
            End Get
            Set(ByVal value As Image)
                Me._clearlogo = value
            End Set
        End Property

        Public Property DiscArt() As Image
            Get
                Return Me._discart
            End Get
            Set(ByVal value As Image)
                Me._discart = value
            End Set
        End Property

        Public Property Extrafanarts() As List(Of Image)
            Get
                Return Me._extrafanarts
            End Get
            Set(ByVal value As List(Of Image))
                Me._extrafanarts = value
            End Set
        End Property

        Public Property Extrathumbs() As List(Of Image)
            Get
                Return Me._extrathumbs
            End Get
            Set(ByVal value As List(Of Image))
                Me._extrathumbs = value
            End Set
        End Property

        Public Property Fanart() As Image
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As Image)
                Me._fanart = value
            End Set
        End Property

        Public Property Landscape() As Image
            Get
                Return Me._landscape
            End Get
            Set(ByVal value As Image)
                Me._landscape = value
            End Set
        End Property

        Public Property Poster() As Image
            Get
                Return Me._poster
            End Get
            Set(ByVal value As Image)
                Me._poster = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._banner = New MediaContainers.Image
            Me._characterart = New MediaContainers.Image
            Me._clearart = New MediaContainers.Image
            Me._clearlogo = New MediaContainers.Image
            Me._discart = New MediaContainers.Image
            Me._extrafanarts = New List(Of Image)
            Me._extrathumbs = New List(Of Image)
            Me._fanart = New MediaContainers.Image
            Me._landscape = New MediaContainers.Image
            Me._poster = New MediaContainers.Image
        End Sub

        Public Sub SaveAllImages(ByRef DBElement As Database.DBElement, ByRef Type As Enums.ContentType)
            With DBElement.ImagesContainer

                Select Case Type
                    Case Enums.ContentType.Movie

                        'Movie Banner
                        If .Banner.ImageOriginal.Image IsNot Nothing Then
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsMovieBanner(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Banner.URLOriginal) Then
                            .Banner.ImageOriginal.FromWeb(.Banner.URLOriginal)
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsMovieBanner(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) Then
                            .Banner.ImageOriginal.FromFile(DBElement.ImagesContainer.Banner.LocalFilePath)
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsMovieBanner(DBElement)
                        Else
                            Images.DeleteMovieBanner(DBElement)
                            .Banner = New Image
                        End If

                        'Movie ClearArt
                        If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsMovieClearArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.ClearArt.URLOriginal) Then
                            .ClearArt.ImageOriginal.FromWeb(.ClearArt.URLOriginal)
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsMovieClearArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) Then
                            .ClearArt.ImageOriginal.FromFile(DBElement.ImagesContainer.ClearArt.LocalFilePath)
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsMovieClearArt(DBElement)
                        Else
                            Images.DeleteMovieClearArt(DBElement)
                            .ClearArt = New Image
                        End If

                        'Movie ClearLogo
                        If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsMovieClearLogo(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.ClearLogo.URLOriginal) Then
                            .ClearLogo.ImageOriginal.FromWeb(.ClearLogo.URLOriginal)
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsMovieClearLogo(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) Then
                            .ClearLogo.ImageOriginal.FromFile(DBElement.ImagesContainer.ClearLogo.LocalFilePath)
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsMovieClearLogo(DBElement)
                        Else
                            Images.DeleteMovieClearLogo(DBElement)
                            .ClearLogo = New Image
                        End If

                        'Movie DiscArt
                        If .DiscArt.ImageOriginal.Image IsNot Nothing Then
                            .DiscArt.LocalFilePath = .DiscArt.ImageOriginal.SaveAsMovieDiscArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.DiscArt.URLOriginal) Then
                            .DiscArt.ImageOriginal.FromWeb(.DiscArt.URLOriginal)
                            .DiscArt.LocalFilePath = .DiscArt.ImageOriginal.SaveAsMovieDiscArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.DiscArt.LocalFilePath) Then
                            .DiscArt.ImageOriginal.FromFile(DBElement.ImagesContainer.DiscArt.LocalFilePath)
                            .DiscArt.LocalFilePath = .DiscArt.ImageOriginal.SaveAsMovieDiscArt(DBElement)
                        Else
                            Images.DeleteMovieDiscArt(DBElement)
                            .DiscArt = New Image
                        End If

                        'Movie Extrafanarts
                        If .Extrafanarts.Count > 0 Then
                            DBElement.ExtrafanartsPath = Images.SaveMovieExtrafanarts(DBElement)
                        Else
                            Images.DeleteMovieExtrafanarts(DBElement)
                            .Extrafanarts = New List(Of Image)
                            DBElement.ExtrafanartsPath = String.Empty
                        End If

                        'Movie Extrathumbs
                        If .Extrathumbs.Count > 0 Then
                            DBElement.ExtrathumbsPath = Images.SaveMovieExtrathumbs(DBElement)
                        Else
                            Images.DeleteMovieExtrathumbs(DBElement)
                            .Extrathumbs = New List(Of Image)
                            DBElement.ExtrathumbsPath = String.Empty
                        End If

                        'Movie Fanart
                        If .Fanart.ImageOriginal.Image IsNot Nothing Then
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsMovieFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Fanart.URLOriginal) Then
                            .Fanart.ImageOriginal.FromWeb(.Fanart.URLOriginal)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsMovieFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) Then
                            .Fanart.ImageOriginal.FromFile(DBElement.ImagesContainer.Fanart.LocalFilePath)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsMovieFanart(DBElement)
                        Else
                            Images.DeleteMovieFanart(DBElement)
                            .Fanart = New Image
                        End If

                        'Movie Landscape
                        If .Landscape.ImageOriginal.Image IsNot Nothing Then
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsMovieLandscape(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Landscape.URLOriginal) Then
                            .Landscape.ImageOriginal.FromWeb(.Landscape.URLOriginal)
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsMovieLandscape(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) Then
                            .Landscape.ImageOriginal.FromFile(DBElement.ImagesContainer.Landscape.LocalFilePath)
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsMovieLandscape(DBElement)
                        Else
                            Images.DeleteMovieLandscape(DBElement)
                            .Landscape = New Image
                        End If

                        'Movie Poster
                        If .Poster.ImageOriginal.Image IsNot Nothing Then
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsMoviePoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Poster.URLOriginal) Then
                            .Poster.ImageOriginal.FromWeb(.Poster.URLOriginal)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsMoviePoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) Then
                            .Poster.ImageOriginal.FromFile(DBElement.ImagesContainer.Poster.LocalFilePath)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsMoviePoster(DBElement)
                        Else
                            Images.DeleteMoviePoster(DBElement)
                            .Poster = New Image
                        End If

                    Case Enums.ContentType.MovieSet

                        'MovieSet Banner
                        If .Banner.ImageOriginal.Image IsNot Nothing Then
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsMovieSetBanner(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Banner.URLOriginal) Then
                            .Banner.ImageOriginal.FromWeb(.Banner.URLOriginal)
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsMovieSetBanner(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) Then
                            .Banner.ImageOriginal.FromFile(DBElement.ImagesContainer.Banner.LocalFilePath)
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsMovieSetBanner(DBElement)
                        Else
                            Images.DeleteMovieSetBanner(DBElement)
                            .Banner = New Image
                        End If

                        'MovieSet ClearArt
                        If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsMovieSetClearArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.ClearArt.URLOriginal) Then
                            .ClearArt.ImageOriginal.FromWeb(.ClearArt.URLOriginal)
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsMovieSetClearArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) Then
                            .ClearArt.ImageOriginal.FromFile(DBElement.ImagesContainer.ClearArt.LocalFilePath)
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsMovieSetClearArt(DBElement)
                        Else
                            Images.DeleteMovieSetClearArt(DBElement)
                            .ClearArt = New Image
                        End If

                        'MovieSet ClearLogo
                        If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsMovieSetClearLogo(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.ClearLogo.URLOriginal) Then
                            .ClearLogo.ImageOriginal.FromWeb(.ClearLogo.URLOriginal)
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsMovieSetClearLogo(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) Then
                            .ClearLogo.ImageOriginal.FromFile(DBElement.ImagesContainer.ClearLogo.LocalFilePath)
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsMovieSetClearLogo(DBElement)
                        Else
                            Images.DeleteMovieSetClearLogo(DBElement)
                            .ClearLogo = New Image
                        End If

                        'MovieSet DiscArt
                        If .DiscArt.ImageOriginal.Image IsNot Nothing Then
                            .DiscArt.LocalFilePath = .DiscArt.ImageOriginal.SaveAsMovieSetDiscArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.DiscArt.URLOriginal) Then
                            .DiscArt.ImageOriginal.FromWeb(.DiscArt.URLOriginal)
                            .DiscArt.LocalFilePath = .DiscArt.ImageOriginal.SaveAsMovieSetDiscArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.DiscArt.LocalFilePath) Then
                            .DiscArt.ImageOriginal.FromFile(DBElement.ImagesContainer.DiscArt.LocalFilePath)
                            .DiscArt.LocalFilePath = .DiscArt.ImageOriginal.SaveAsMovieSetDiscArt(DBElement)
                        Else
                            Images.DeleteMovieSetDiscArt(DBElement)
                            .DiscArt = New Image
                        End If

                        'MovieSet Fanart
                        If .Fanart.ImageOriginal.Image IsNot Nothing Then
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsMovieSetFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Fanart.URLOriginal) Then
                            .Fanart.ImageOriginal.FromWeb(.Fanart.URLOriginal)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsMovieSetFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) Then
                            .Fanart.ImageOriginal.FromFile(DBElement.ImagesContainer.Fanart.LocalFilePath)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsMovieSetFanart(DBElement)
                        Else
                            Images.DeleteMovieSetFanart(DBElement)
                            .Fanart = New Image
                        End If

                        'MovieSet Landscape
                        If .Landscape.ImageOriginal.Image IsNot Nothing Then
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsMovieSetLandscape(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Landscape.URLOriginal) Then
                            .Landscape.ImageOriginal.FromWeb(.Landscape.URLOriginal)
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsMovieSetLandscape(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) Then
                            .Landscape.ImageOriginal.FromFile(DBElement.ImagesContainer.Landscape.LocalFilePath)
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsMovieSetLandscape(DBElement)
                        Else
                            Images.DeleteMovieSetLandscape(DBElement)
                            .Landscape = New Image
                        End If

                        'MovieSet Poster
                        If .Poster.ImageOriginal.Image IsNot Nothing Then
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsMovieSetPoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Poster.URLOriginal) Then
                            .Poster.ImageOriginal.FromWeb(.Poster.URLOriginal)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsMovieSetPoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) Then
                            .Poster.ImageOriginal.FromFile(DBElement.ImagesContainer.Poster.LocalFilePath)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsMovieSetPoster(DBElement)
                        Else
                            Images.DeleteMovieSetPoster(DBElement)
                            .Poster = New Image
                        End If

                    Case Enums.ContentType.TVEpisode

                        'Episode Fanart
                        If .Fanart.ImageOriginal.Image IsNot Nothing Then
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVEpisodeFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Fanart.URLOriginal) Then
                            .Fanart.ImageOriginal.FromWeb(.Fanart.URLOriginal)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVEpisodeFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) Then
                            .Fanart.ImageOriginal.FromFile(DBElement.ImagesContainer.Fanart.LocalFilePath)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVEpisodeFanart(DBElement)
                        Else
                            Images.DeleteTVEpisodeFanart(DBElement)
                            .Fanart = New Image
                        End If

                        'Episode Poster
                        If .Poster.ImageOriginal.Image IsNot Nothing Then
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVEpisodePoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Poster.URLOriginal) Then
                            .Poster.ImageOriginal.FromWeb(.Poster.URLOriginal)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVEpisodePoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) Then
                            .Poster.ImageOriginal.FromFile(DBElement.ImagesContainer.Poster.LocalFilePath)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVEpisodePoster(DBElement)
                        Else
                            Images.DeleteTVEpisodePoster(DBElement)
                            .Poster = New Image
                        End If

                    Case Enums.ContentType.TVSeason

                        'Season Banner
                        If .Banner.ImageOriginal.Image IsNot Nothing Then
                            If DBElement.TVSeason.Season = 999 Then
                                .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVASBanner(DBElement)
                            Else
                                .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVSeasonBanner(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(.Banner.URLOriginal) Then
                            .Banner.ImageOriginal.FromWeb(.Banner.URLOriginal)
                            If DBElement.TVSeason.Season = 999 Then
                                .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVASBanner(DBElement)
                            Else
                                .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVSeasonBanner(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) Then
                            .Banner.ImageOriginal.FromFile(DBElement.ImagesContainer.Banner.LocalFilePath)
                            If DBElement.TVSeason.Season = 999 Then
                                .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVASBanner(DBElement)
                            Else
                                .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVSeasonBanner(DBElement)
                            End If
                        Else
                            If DBElement.TVSeason.Season = 999 Then
                                .Banner.ImageOriginal.DeleteTVASBanner(DBElement)
                                .Banner.LocalFilePath = String.Empty
                            Else
                                Images.DeleteTVSeasonBanner(DBElement)
                                .Banner = New Image
                            End If
                        End If

                        'Season Fanart
                        If .Fanart.ImageOriginal.Image IsNot Nothing Then
                            If DBElement.TVSeason.Season = 999 Then
                                .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVASFanart(DBElement)
                            Else
                                .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVSeasonFanart(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(.Fanart.URLOriginal) Then
                            .Fanart.ImageOriginal.FromWeb(.Fanart.URLOriginal)
                            If DBElement.TVSeason.Season = 999 Then
                                .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVASFanart(DBElement)
                            Else
                                .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVSeasonFanart(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) Then
                            .Banner.ImageOriginal.FromFile(DBElement.ImagesContainer.Fanart.LocalFilePath)
                            If DBElement.TVSeason.Season = 999 Then
                                .Fanart.LocalFilePath = .Banner.ImageOriginal.SaveAsTVASFanart(DBElement)
                            Else
                                .Fanart.LocalFilePath = .Banner.ImageOriginal.SaveAsTVSeasonFanart(DBElement)
                            End If
                        Else
                            If DBElement.TVSeason.Season = 999 Then
                                .Fanart.ImageOriginal.DeleteTVASFanart(DBElement)
                                .Fanart.LocalFilePath = String.Empty
                            Else
                                Images.DeleteTVSeasonFanart(DBElement)
                                .Fanart = New Image
                            End If
                        End If

                        'Season Landscape
                        If .Landscape.ImageOriginal.Image IsNot Nothing Then
                            If DBElement.TVSeason.Season = 999 Then
                                .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVASLandscape(DBElement)
                            Else
                                .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVSeasonLandscape(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(.Landscape.URLOriginal) Then
                            .Landscape.ImageOriginal.FromWeb(.Landscape.URLOriginal)
                            If DBElement.TVSeason.Season = 999 Then
                                .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVASLandscape(DBElement)
                            Else
                                .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVSeasonLandscape(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) Then
                            .Landscape.ImageOriginal.FromFile(DBElement.ImagesContainer.Landscape.LocalFilePath)
                            If DBElement.TVSeason.Season = 999 Then
                                .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVASLandscape(DBElement)
                            Else
                                .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVSeasonLandscape(DBElement)
                            End If
                        Else
                            If DBElement.TVSeason.Season = 999 Then
                                .Landscape.ImageOriginal.DeleteTVASLandscape(DBElement)
                                .Landscape.LocalFilePath = String.Empty
                            Else
                                Images.DeleteTVSeasonLandscape(DBElement)
                                .Landscape = New Image
                            End If
                        End If

                        'Season Poster
                        If .Poster.ImageOriginal.Image IsNot Nothing Then
                            If DBElement.TVSeason.Season = 999 Then
                                .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVASPoster(DBElement)
                            Else
                                .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVSeasonPoster(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(.Poster.URLOriginal) Then
                            .Poster.ImageOriginal.FromWeb(.Poster.URLOriginal)
                            If DBElement.TVSeason.Season = 999 Then
                                .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVASPoster(DBElement)
                            Else
                                .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVSeasonPoster(DBElement)
                            End If
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) Then
                            .Poster.ImageOriginal.FromFile(DBElement.ImagesContainer.Poster.LocalFilePath)
                            If DBElement.TVSeason.Season = 999 Then
                                .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVASPoster(DBElement)
                            Else
                                .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVSeasonPoster(DBElement)
                            End If
                        Else
                            If DBElement.TVSeason.Season = 999 Then
                                .Poster.ImageOriginal.DeleteTVASPoster(DBElement)
                                .Poster.LocalFilePath = String.Empty
                            Else
                                Images.DeleteTVSeasonPoster(DBElement)
                                .Poster = New Image
                            End If
                        End If

                    Case Enums.ContentType.TVShow

                        'Show Banner
                        If .Banner.ImageOriginal.Image IsNot Nothing Then
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVShowBanner(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Banner.URLOriginal) Then
                            .Banner.ImageOriginal.FromWeb(.Banner.URLOriginal)
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVShowBanner(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) Then
                            .Banner.ImageOriginal.FromFile(DBElement.ImagesContainer.Banner.LocalFilePath)
                            .Banner.LocalFilePath = .Banner.ImageOriginal.SaveAsTVShowBanner(DBElement)
                        Else
                            Images.DeleteTVShowBanner(DBElement)
                            .Banner = New Image
                        End If

                        'Show CharacterArt
                        If .CharacterArt.ImageOriginal.Image IsNot Nothing Then
                            .CharacterArt.LocalFilePath = .CharacterArt.ImageOriginal.SaveAsTVShowCharacterArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.CharacterArt.URLOriginal) Then
                            .CharacterArt.ImageOriginal.FromWeb(.CharacterArt.URLOriginal)
                            .CharacterArt.LocalFilePath = .CharacterArt.ImageOriginal.SaveAsTVShowCharacterArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.CharacterArt.LocalFilePath) Then
                            .CharacterArt.ImageOriginal.FromFile(DBElement.ImagesContainer.CharacterArt.LocalFilePath)
                            .CharacterArt.LocalFilePath = .CharacterArt.ImageOriginal.SaveAsTVShowCharacterArt(DBElement)
                        Else
                            Images.DeleteTVShowCharacterArt(DBElement)
                            .CharacterArt = New Image
                        End If

                        'Show ClearArt
                        If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsTVShowClearArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.ClearArt.URLOriginal) Then
                            .ClearArt.ImageOriginal.FromWeb(.ClearArt.URLOriginal)
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsTVShowClearArt(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) Then
                            .ClearArt.ImageOriginal.FromFile(DBElement.ImagesContainer.ClearArt.LocalFilePath)
                            .ClearArt.LocalFilePath = .ClearArt.ImageOriginal.SaveAsTVShowClearArt(DBElement)
                        Else
                            Images.DeleteTVShowClearArt(DBElement)
                            .ClearArt = New Image
                        End If

                        'Show ClearLogo
                        If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsTVShowClearLogo(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.ClearLogo.URLOriginal) Then
                            .ClearLogo.ImageOriginal.FromWeb(.ClearLogo.URLOriginal)
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsTVShowClearLogo(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) Then
                            .ClearLogo.ImageOriginal.FromFile(DBElement.ImagesContainer.ClearLogo.LocalFilePath)
                            .ClearLogo.LocalFilePath = .ClearLogo.ImageOriginal.SaveAsTVShowClearLogo(DBElement)
                        Else
                            Images.DeleteTVShowClearLogo(DBElement)
                            .ClearLogo = New Image
                        End If

                        'Show Extrafanarts
                        If .Extrafanarts.Count > 0 Then
                            DBElement.ExtrafanartsPath = Images.SaveTVShowExtrafanarts(DBElement)
                        Else
                            Images.DeleteTVShowExtrafanarts(DBElement)
                            .Extrafanarts = New List(Of Image)
                            DBElement.ExtrafanartsPath = String.Empty
                        End If

                        'Show Fanart
                        If .Fanart.ImageOriginal.Image IsNot Nothing Then
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVShowFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Fanart.URLOriginal) Then
                            .Fanart.ImageOriginal.FromWeb(.Fanart.URLOriginal)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVShowFanart(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) Then
                            .Fanart.ImageOriginal.FromFile(DBElement.ImagesContainer.Fanart.LocalFilePath)
                            .Fanart.LocalFilePath = .Fanart.ImageOriginal.SaveAsTVShowFanart(DBElement)
                        Else
                            Images.DeleteTVShowFanart(DBElement)
                            .Fanart = New Image
                        End If

                        'Show Landscape
                        If .Landscape.ImageOriginal.Image IsNot Nothing Then
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVShowLandscape(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Landscape.URLOriginal) Then
                            .Landscape.ImageOriginal.FromWeb(.Landscape.URLOriginal)
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVShowLandscape(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) Then
                            .Landscape.ImageOriginal.FromFile(DBElement.ImagesContainer.Landscape.LocalFilePath)
                            .Landscape.LocalFilePath = .Landscape.ImageOriginal.SaveAsTVShowLandscape(DBElement)
                        Else
                            Images.DeleteTVShowLandscape(DBElement)
                            .Landscape = New Image
                        End If

                        'Show Poster
                        If .Poster.ImageOriginal.Image IsNot Nothing Then
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVShowPoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(.Poster.URLOriginal) Then
                            .Poster.ImageOriginal.FromWeb(.Poster.URLOriginal)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVShowPoster(DBElement)
                        ElseIf Not String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) Then
                            .Poster.ImageOriginal.FromFile(DBElement.ImagesContainer.Poster.LocalFilePath)
                            .Poster.LocalFilePath = .Poster.ImageOriginal.SaveAsTVShowPoster(DBElement)
                        Else
                            Images.DeleteTVShowPoster(DBElement)
                            .Poster = New Image
                        End If
                End Select
            End With
        End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

    End Class

    <Serializable()> _
    Public Class EpisodeOrSeasonImagesContainer

#Region "Fields"

        Private _alreadysaved As Boolean
        Private _banner As MediaContainers.Image
        Private _episode As Integer
        Private _fanart As MediaContainers.Image
        Private _landscape As MediaContainers.Image
        Private _poster As MediaContainers.Image
        Private _season As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AlreadySaved() As Boolean
            Get
                Return Me._alreadysaved
            End Get
            Set(ByVal value As Boolean)
                Me._alreadysaved = value
            End Set
        End Property

        Public Property Banner() As MediaContainers.Image
            Get
                Return Me._banner
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._banner = value
            End Set
        End Property

        Public Property Episode() As Integer
            Get
                Return Me._episode
            End Get
            Set(ByVal value As Integer)
                Me._episode = value
            End Set
        End Property

        Public Property Fanart() As MediaContainers.Image
            Get
                Return Me._fanart
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._fanart = value
            End Set
        End Property

        Public Property Landscape() As MediaContainers.Image
            Get
                Return Me._landscape
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._landscape = value
            End Set
        End Property

        Public Property Poster() As MediaContainers.Image
            Get
                Return Me._poster
            End Get
            Set(ByVal value As MediaContainers.Image)
                Me._poster = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._alreadysaved = False
            Me._banner = New MediaContainers.Image
            Me._episode = -1
            Me._fanart = New MediaContainers.Image
            Me._landscape = New MediaContainers.Image
            Me._poster = New MediaContainers.Image
            Me._season = -1
        End Sub

#End Region 'Methods

    End Class

    <Serializable()> _
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
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property EpisodeFanarts() As List(Of Image)
            Get
                Return Me._episodefanarts
            End Get
            Set(ByVal value As List(Of Image))
                Me._episodefanarts = value
            End Set
        End Property

        Public Property EpisodePosters() As List(Of Image)
            Get
                Return Me._episodeposters
            End Get
            Set(ByVal value As List(Of Image))
                Me._episodeposters = value
            End Set
        End Property

        Public Property SeasonBanners() As List(Of Image)
            Get
                Return Me._seasonbanners
            End Get
            Set(ByVal value As List(Of Image))
                Me._seasonbanners = value
            End Set
        End Property

        Public Property SeasonFanarts() As List(Of Image)
            Get
                Return Me._seasonfanarts
            End Get
            Set(ByVal value As List(Of Image))
                Me._seasonfanarts = value
            End Set
        End Property

        Public Property SeasonLandscapes() As List(Of Image)
            Get
                Return Me._seasonlandscapes
            End Get
            Set(ByVal value As List(Of Image))
                Me._seasonlandscapes = value
            End Set
        End Property

        Public Property SeasonPosters() As List(Of Image)
            Get
                Return Me._seasonposters
            End Get
            Set(ByVal value As List(Of Image))
                Me._seasonposters = value
            End Set
        End Property

        Public Property MainBanners() As List(Of Image)
            Get
                Return Me._mainbanners
            End Get
            Set(ByVal value As List(Of Image))
                Me._mainbanners = value
            End Set
        End Property

        Public Property MainCharacterArts() As List(Of Image)
            Get
                Return Me._maincharacterarts
            End Get
            Set(ByVal value As List(Of Image))
                Me._maincharacterarts = value
            End Set
        End Property

        Public Property MainClearArts() As List(Of Image)
            Get
                Return Me._maincleararts
            End Get
            Set(ByVal value As List(Of Image))
                Me._maincleararts = value
            End Set
        End Property

        Public Property MainClearLogos() As List(Of Image)
            Get
                Return Me._mainclearlogos
            End Get
            Set(ByVal value As List(Of Image))
                Me._mainclearlogos = value
            End Set
        End Property

        Public Property MainDiscArts() As List(Of Image)
            Get
                Return Me._maindiscarts
            End Get
            Set(ByVal value As List(Of Image))
                Me._maindiscarts = value
            End Set
        End Property

        Public Property MainFanarts() As List(Of Image)
            Get
                Return Me._mainfanarts
            End Get
            Set(ByVal value As List(Of Image))
                Me._mainfanarts = value
            End Set
        End Property

        Public Property MainLandscapes() As List(Of Image)
            Get
                Return Me._mainlandscapes
            End Get
            Set(ByVal value As List(Of Image))
                Me._mainlandscapes = value
            End Set
        End Property

        Public Property MainPosters() As List(Of Image)
            Get
                Return Me._mainposters
            End Get
            Set(ByVal value As List(Of Image))
                Me._mainposters = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._episodefanarts.Clear()
            Me._episodeposters.Clear()
            Me._seasonbanners.Clear()
            Me._seasonfanarts.Clear()
            Me._seasonlandscapes.Clear()
            Me._seasonposters.Clear()
            Me._mainbanners.Clear()
            Me._maincharacterarts.Clear()
            Me._maincleararts.Clear()
            Me._mainclearlogos.Clear()
            Me._maindiscarts.Clear()
            Me._mainfanarts.Clear()
            Me._mainlandscapes.Clear()
            Me._mainposters.Clear()
        End Sub

        Public Sub Sort(ByRef ContentType As Enums.ContentType)
            Dim cSettings As New Settings

            Select Case ContentType
                Case Enums.ContentType.Movie
                    cSettings.GetBlankImages = Master.eSettings.MovieImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.MovieImagesGetEnglishImages
                    cSettings.PrefLanguage = Master.eSettings.MovieImagesPrefLanguage
                    cSettings.PrefLanguageOnly = Master.eSettings.MovieImagesPrefLanguageOnly
                Case Enums.ContentType.MovieSet
                    cSettings.GetBlankImages = Master.eSettings.MovieSetImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.MovieSetImagesGetEnglishImages
                    cSettings.PrefLanguage = Master.eSettings.MovieSetImagesPrefLanguage
                    cSettings.PrefLanguageOnly = Master.eSettings.MovieSetImagesPrefLanguageOnly
                Case Enums.ContentType.TV
                    cSettings.GetBlankImages = Master.eSettings.TVImagesGetBlankImages
                    cSettings.GetEnglishImages = Master.eSettings.TVImagesGetEnglishImages
                    cSettings.PrefLanguage = Master.eSettings.TVImagesPrefLanguage
                    cSettings.PrefLanguageOnly = Master.eSettings.TVImagesPrefLanguageOnly
            End Select

            'sort all List(Of Image) by Image.ShortLang
            Me._episodefanarts.Sort()
            Me._episodeposters.Sort()
            Me._seasonbanners.Sort()
            Me._seasonfanarts.Sort()
            Me._seasonlandscapes.Sort()
            Me._seasonposters.Sort()
            Me._mainbanners.Sort()
            Me._maincharacterarts.Sort()
            Me._maincleararts.Sort()
            Me._mainclearlogos.Sort()
            Me._maindiscarts.Sort()
            Me._mainfanarts.Sort()
            Me._mainlandscapes.Sort()
            Me._mainposters.Sort()

            'sort all List(Of Image) by preffered language/en/Blank/String.Empty/others
            'Fanarts are not filtered, most of all have no language specification
            Me._episodeposters = SortImages(Me._episodeposters, cSettings)
            Me._seasonbanners = SortImages(Me._seasonbanners, cSettings)
            Me._seasonlandscapes = SortImages(Me._seasonlandscapes, cSettings)
            Me._seasonposters = SortImages(Me._seasonposters, cSettings)
            Me._mainbanners = SortImages(Me._mainbanners, cSettings)
            Me._maincharacterarts = SortImages(Me._maincharacterarts, cSettings)
            Me._maincleararts = SortImages(Me._maincleararts, cSettings)
            Me._mainclearlogos = SortImages(Me._mainclearlogos, cSettings)
            Me._maindiscarts = SortImages(Me._maindiscarts, cSettings)
            Me._mainlandscapes = SortImages(Me._mainlandscapes, cSettings)
            Me._mainposters = SortImages(Me._mainposters, cSettings)
        End Sub

        Private Function SortImages(ByRef ImagesList As List(Of Image), ByVal cSettings As Settings) As List(Of Image)
            Dim SortedList As New List(Of Image)

            For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = cSettings.PrefLanguage)
                SortedList.Add(tmpImage)
            Next

            If (cSettings.GetEnglishImages OrElse Not cSettings.PrefLanguageOnly) AndAlso Not cSettings.PrefLanguage = "en" Then
                For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = "en")
                    SortedList.Add(tmpImage)
                Next
            End If

            If cSettings.GetBlankImages OrElse Not cSettings.PrefLanguageOnly Then
                For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = Master.eLang.GetString(1168, "Blank"))
                    SortedList.Add(tmpImage)
                Next
                For Each tmpImage As Image In ImagesList.Where(Function(f) f.ShortLang = String.Empty)
                    SortedList.Add(tmpImage)
                Next
            End If

            If Not cSettings.PrefLanguageOnly Then
                For Each tmpImage As Image In ImagesList.Where(Function(f) Not f.ShortLang = cSettings.PrefLanguage OrElse _
                                                                   Not f.ShortLang = "en" OrElse _
                                                                   Not f.ShortLang = Master.eLang.GetString(1168, "Blank") OrElse _
                                                                   Not f.ShortLang = String.Empty)
                    SortedList.Add(tmpImage)
                Next
            End If

            Return SortedList
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Settings

#Region "Fields"

            Dim GetBlankImages As Boolean
            Dim GetEnglishImages As Boolean
            Dim PrefLanguage As String
            Dim PrefLanguageOnly As Boolean

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

    <Serializable()> _
    Public Class [Set]

#Region "Fields"

        Private _id As Long
        Private _order As String
        Private _title As String
        Private _tmdbcolid As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        <XmlIgnore()> _
        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal value As Long)
                _id = value
            End Set
        End Property

        <XmlAttribute("order")> _
        Public Property Order() As String
            Get
                Return _order
            End Get
            Set(ByVal value As String)
                _order = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property OrderSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._order)
            End Get
        End Property

        <XmlAttribute("tmdbcolid")> _
        Public Property TMDBColID() As String
            Get
                Return _tmdbcolid
            End Get
            Set(ByVal value As String)
                _tmdbcolid = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TMDBColIDSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._tmdbcolid)
            End Get
        End Property

        <XmlText()> _
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property TitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._title)
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

    Public Class [Theme]

#Region "Constructors"

        Public Sub New()
            Me.Clear()
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
            Me._URL = String.Empty
            Me._Quality = String.Empty
            Me._WebTheme = New Themes
        End Sub

#End Region 'Methods

    End Class

    Public Class [Trailer]

#Region "Fields"

        Private _audiourl As String
        Private _duration As String
        Private _isDash As Boolean
        Private _longlang As String
        Private _quality As Enums.TrailerVideoQuality
        Private _scraper As String
        Private _shortlang As String
        Private _source As String
        Private _title As String
        Private _type As Enums.TrailerType
        Private _videourl As String
        Private _webtrailer As New Trailers
        Private _weburl As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"
        ''' <summary>
        ''' download audio URL of the trailer
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AudioURL() As String
            Get
                Return _audiourl
            End Get
            Set(ByVal value As String)
                _audiourl = value
            End Set
        End Property

        Public Property Duration() As String
            Get
                Return Me._duration
            End Get
            Set(ByVal value As String)
                Me._duration = value
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

        Public Property LongLang() As String
            Get
                Return Me._longlang
            End Get
            Set(ByVal value As String)
                Me._longlang = value
            End Set
        End Property

        Public Property Quality() As Enums.TrailerVideoQuality
            Get
                Return Me._quality
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                Me._quality = value
            End Set
        End Property

        Public Property Scraper() As String
            Get
                Return Me._scraper
            End Get
            Set(ByVal value As String)
                Me._scraper = value
            End Set
        End Property

        Public Property ShortLang() As String
            Get
                Return Me._shortlang
            End Get
            Set(ByVal value As String)
                Me._shortlang = value
            End Set
        End Property

        Public Property Source() As String
            Get
                Return Me._source
            End Get
            Set(ByVal value As String)
                Me._source = value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        Public Property Type() As Enums.TrailerType
            Get
                Return Me._type
            End Get
            Set(ByVal value As Enums.TrailerType)
                Me._type = value
            End Set
        End Property
        ''' <summary>
        ''' download video URL of the trailer
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property VideoURL() As String
            Get
                Return _videourl
            End Get
            Set(ByVal value As String)
                _videourl = value
            End Set
        End Property

        Public Property WebTrailer() As Trailers
            Get
                Return Me._webtrailer
            End Get
            Set(ByVal value As Trailers)
                Me._webtrailer = value
            End Set
        End Property
        ''' <summary>
        ''' website URL of the trailer for preview in browser
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WebURL() As String
            Get
                Return _weburl
            End Get
            Set(ByVal value As String)
                _weburl = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._audiourl = String.Empty
            Me._duration = String.Empty
            Me._isDash = False
            Me._longlang = String.Empty
            Me._quality = Enums.TrailerVideoQuality.Any
            Me._scraper = String.Empty
            Me._shortlang = String.Empty
            Me._source = String.Empty
            Me._title = String.Empty
            Me._type = Enums.TrailerType.Any
            Me._videourl = String.Empty
            Me._webtrailer = New Trailers
            Me._weburl = String.Empty
        End Sub

#End Region 'Methods

    End Class

End Namespace

