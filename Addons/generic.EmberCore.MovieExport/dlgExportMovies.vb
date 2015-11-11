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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports EmberAPI
Imports NLog

Public Class dlgExportMovies

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwSaveAll As New System.ComponentModel.BackgroundWorker

    Private AllTVShowList As String = String.Empty
    Private allTVShows As New List(Of String)
    Private alSets As New List(Of String)
    Private base_template As String
    Private bCancelled As Boolean = False
    Private bFiltered As Boolean = False
    Private DontSaveExtra As Boolean = False
    Dim FilterMovies As New List(Of Long)
    Private HTMLBody As New StringBuilder
    Private isCL As Boolean = False
    Private TempPath As String = Path.Combine(Master.TempPath, "Export")
    Private use_filter As Boolean = False
    Private workerCanceled As Boolean = False
    Private MovieList As New List(Of Database.DBElement)
    Private TVShowList As New List(Of Database.DBElement)

    'HTML settings
    Private tExportSettings As New ExportSettings

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Shared Sub CLExport(ByVal filename As String, Optional ByVal template As String = "template", Optional ByVal resizePoster As Integer = 0)
        Try
            Dim MySelf As New dlgExportMovies
            If Not Directory.Exists(Path.GetDirectoryName(filename)) Then
                Return
            End If
            MySelf.isCL = True
            MySelf.bwLoadInfo = New System.ComponentModel.BackgroundWorker
            MySelf.bwLoadInfo.WorkerSupportsCancellation = True
            MySelf.bwLoadInfo.WorkerReportsProgress = True
            MySelf.bwLoadInfo.RunWorkerAsync()
            While MySelf.bwLoadInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
            MySelf.BuildHTML(False, String.Empty, String.Empty, template, False)
            Dim srcPath As String = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html", Path.DirectorySeparatorChar, template, Path.DirectorySeparatorChar)
            MySelf.SaveAll(String.Empty, srcPath, filename, resizePoster)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub CopyDirectory(ByVal SourcePath As String, ByVal DestPath As String, Optional ByVal Overwrite As Boolean = False)
        Dim SourceDir As DirectoryInfo = New DirectoryInfo(SourcePath)
        Dim DestDir As DirectoryInfo = New DirectoryInfo(DestPath)
        Dim IsRoot As Boolean = False

        ' the source directory must exist, otherwise throw an exception
        If SourceDir.Exists Then

            'is this a root directory?
            If DestDir.Root.FullName = DestDir.FullName Then
                IsRoot = True
            End If

            ' if destination SubDir's parent SubDir does not exist throw an exception (also check it isn't the root)
            If Not IsRoot AndAlso Not DestDir.Parent.Exists Then
                Throw New DirectoryNotFoundException _
                    ("Destination directory does not exist: " + DestDir.Parent.FullName)
            End If

            If Not DestDir.Exists Then
                DestDir.Create()
            End If

            ' copy all the files of the current directory
            Dim ChildFile As FileInfo
            For Each ChildFile In SourceDir.GetFiles()
                If (ChildFile.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden OrElse Path.GetExtension(ChildFile.FullName) = ".html" Then Continue For
                If Overwrite Then
                    ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), True)
                Else
                    ' if Overwrite = false, copy the file only if it does not exist
                    ' this is done to avoid an IOException if a file already exists
                    ' this way the other files can be copied anyway...
                    If Not File.Exists(Path.Combine(DestDir.FullName, ChildFile.Name)) Then
                        ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), False)
                    End If
                End If
            Next

            ' copy all the sub-directories by recursively calling this same routine
            Dim SubDir As DirectoryInfo
            For Each SubDir In SourceDir.GetDirectories()
                If (SubDir.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
                CopyDirectory(SubDir.FullName, Path.Combine(DestDir.FullName,
                    SubDir.Name), Overwrite)
            Next
        Else
            Throw New DirectoryNotFoundException("Source directory does not exist: " + SourceDir.FullName)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Me.Close()
        If bwSaveAll.IsBusy Then
            bwSaveAll.CancelAsync()
        End If
        btnCancel.Enabled = False
    End Sub

    Private Function ProcessPattern_Settings(ByVal strPattern As String) As String
        tExportSettings.Clear()

        Dim regSettings As Match = Regex.Match(strPattern, "<exportsettings>.*?</exportsettings>", RegexOptions.Singleline)
        If regSettings.Success Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As TextReader = New StringReader(regSettings.Value)
                xmlSer = New XmlSerializer(GetType(ExportSettings))
                tExportSettings = DirectCast(xmlSer.Deserialize(xmlSR), ExportSettings)
            End Using
            strPattern = strPattern.Replace(regSettings.Value, String.Empty)
        End If

        Return strPattern
    End Function

    Private Sub BuildHTML(ByVal bSearch As Boolean, ByVal strFilter As String, ByVal strIn As String, ByVal template As String, ByVal doNavigate As Boolean)
        Try
            ' Build HTML Documment in Code ... ugly but will work until new option

            HTMLBody.Length = 0


            Dim AllMovieSetList As String = String.Empty
            Try
                AllMovieSetList = GetAllMovieSets()
            Catch ex As Exception

            End Try

            Dim tVid As New MediaInfo.Video
            Dim tAud As New MediaInfo.Audio
            Dim tRes As String = String.Empty
            Dim htmlPath As String = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html", Path.DirectorySeparatorChar, template, Path.DirectorySeparatorChar, Master.eSettings.GeneralLanguage, ".html")
            Dim pattern As String = String.Empty
            If Not File.Exists(htmlPath) Then
                htmlPath = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html", Path.DirectorySeparatorChar, template, Path.DirectorySeparatorChar, "English_(en_US).html")
            End If
            If Not File.Exists(htmlPath) Then
                Return
            End If

            pattern = File.ReadAllText(htmlPath)
            pattern = ProcessPattern_Settings(pattern)

            If bSearch Then
                bFiltered = True
            Else
                bFiltered = False
            End If

            'here we transform the main pattern string into a content map
            Dim contentMap As New List(Of ContentPart)
            Dim part As ContentPart
            Dim nextPart = pattern.IndexOf("<$$")
            Dim endPart As Integer

            While pattern.Length > 0
                If nextPart > 0 Then
                    'create the non-looped section
                    part = New ContentPart With {.isLooped = False}
                    part.Content = pattern.Substring(0, nextPart)
                    contentMap.Add(part)
                    pattern = pattern.Substring(nextPart)
                    nextPart = pattern.IndexOf("<$$")
                ElseIf nextPart = -1 Then
                    'no more sections found, add the remaining html code
                    part = New ContentPart With {.isLooped = False}
                    part.Content = pattern
                    contentMap.Add(part)
                    pattern = String.Empty
                Else
                    If pattern.IndexOf("<$$MOVIE>") = 0 Then
                        'create the looped section for movies
                        endPart = pattern.IndexOf("</$$MOVIE>")
                        part = New ContentPart With {.ContentType = Enums.ContentType.Movie, .isLooped = True}
                        part.Content = pattern.Substring(9, endPart - 9)
                        contentMap.Add(part)
                        pattern = pattern.Substring(endPart + 10)
                        nextPart = pattern.IndexOf("<$$")
                    ElseIf pattern.IndexOf("<$$TVSHOW>") = 0 Then
                        'create the looped section for tv shows
                        endPart = pattern.IndexOf("</$$TVSHOW>")
                        part = New ContentPart With {.ContentType = Enums.ContentType.TVShow, .isLooped = True}
                        part.Content = pattern.Substring(10, endPart - 10)

                        'check if a <$$TVSEASON> part is inside the <$$TVSHOW> part
                        Dim nextPart_TVSeason As Integer = part.Content.IndexOf("<$$TVSEASON>")
                        Dim endPart_TVSeason As Integer = part.Content.IndexOf("</$$TVSEASON>")
                        If Not nextPart_TVSeason = -1 AndAlso Not endPart_TVSeason = -1 Then
                            Dim part_TVSeason As New ContentPart With {.ContentType = Enums.ContentType.TVSeason, .isLooped = True}
                            part_TVSeason.Content = part.Content.Substring(nextPart_TVSeason + 12, endPart_TVSeason - nextPart_TVSeason - 13)

                            'check if a <$$TVEPISODE> part is inside the <$$TVSEASON> part
                            Dim nextPart_TVEpisode As Integer = part_TVSeason.Content.IndexOf("<$$TVEPISODE>")
                            Dim endPart_TVEpisode As Integer = part_TVSeason.Content.IndexOf("</$$TVEPISODE>")
                            If Not nextPart_TVEpisode = -1 AndAlso Not endPart_TVEpisode = -1 Then
                                Dim part_TVEpisode As New ContentPart With {.ContentType = Enums.ContentType.TVEpisode, .isLooped = True}
                                part_TVEpisode.Content = part_TVSeason.Content.Substring(nextPart_TVEpisode + 13, endPart_TVEpisode - nextPart_TVEpisode - 14)

                                part_TVSeason.innerContentPart = part_TVEpisode
                                part_TVSeason.Content = Regex.Replace(part_TVSeason.Content, "<\$\$TVEPISODE>.*?</\$\$TVEPISODE>", "<$$$TVEPISODELOOP>", RegexOptions.Singleline)
                            End If

                            part.innerContentPart = part_TVSeason
                            part.Content = Regex.Replace(part.Content, "<\$\$TVSEASON>.*?</\$\$TVSEASON>", "<$$$TVSEASONLOOP>", RegexOptions.Singleline)
                        End If

                        contentMap.Add(part)
                        pattern = pattern.Substring(endPart + 11)
                        nextPart = pattern.IndexOf("<$$")
                    End If
                End If
            End While

            Dim counter As Integer = 1
            For Each part In contentMap
                If part.isLooped Then
                    If part.ContentType = Enums.ContentType.Movie Then
                        FilterMovies.Clear()
                        For Each tMovie As Database.DBElement In MovieList
                            Dim row As String = part.Content

                            Dim _audBitrate As String = String.Empty
                            Dim _audChannels As String = String.Empty
                            Dim _audDetails As String = String.Empty
                            Dim _audLanguage As String = String.Empty
                            Dim _audLongLanguage As String = String.Empty
                            Dim _subLanguage As String = String.Empty
                            Dim _subLongLanguage As String = String.Empty
                            Dim _subType As String = String.Empty
                            Dim _vidAspect As String = String.Empty
                            Dim _vidBitrate As String = String.Empty
                            Dim _vidDetails As String = String.Empty
                            Dim _vidDimensions As String = String.Empty
                            Dim _vidDuration As String = String.Empty
                            Dim _vidFileSize As String = String.Empty
                            Dim _vidHeight As String = String.Empty
                            Dim _vidLanguage As String = String.Empty
                            Dim _vidLongLanguage As String = String.Empty
                            Dim _vidMultiViewCount As String = String.Empty
                            Dim _vidMultiViewLayout As String = String.Empty
                            Dim _vidScantype As String = String.Empty
                            Dim _vidStereoMode As String = String.Empty
                            Dim _vidWidth As String = String.Empty

                            If tMovie.Movie.FileInfo IsNot Nothing Then
                                If tMovie.Movie.FileInfo.StreamDetails.Video.Count > 0 Then
                                    tVid = NFO.GetBestVideo(tMovie.Movie.FileInfo)
                                    tRes = NFO.GetResFromDimensions(tVid)

                                    _vidBitrate = tVid.Bitrate
                                    _vidFileSize = CStr(tVid.Filesize)
                                    _vidMultiViewCount = tVid.MultiViewCount
                                    _vidMultiViewLayout = tVid.MultiViewLayout
                                    _vidAspect = tVid.Aspect
                                    _vidDuration = tVid.Duration
                                    _vidHeight = tVid.Height
                                    _vidLanguage = tVid.Language
                                    _vidLongLanguage = tVid.LongLanguage
                                    _vidScantype = tVid.Scantype
                                    _vidStereoMode = tVid.StereoMode
                                    _vidWidth = tVid.Width
                                    _vidDetails = String.Format("{0} / {1}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes), If(String.IsNullOrEmpty(tVid.Codec), Master.eLang.GetString(138, "Unknown"), tVid.Codec)).ToUpper
                                    _vidDimensions = NFO.GetDimensionsFromVideo(tVid)
                                End If

                                If tMovie.Movie.FileInfo.StreamDetails.Audio.Count > 0 Then
                                    tAud = NFO.GetBestAudio(tMovie.Movie.FileInfo, False)

                                    _audBitrate = tAud.Bitrate
                                    _audChannels = tAud.Channels
                                    _audLanguage = tAud.Language
                                    _audLongLanguage = tAud.LongLanguage
                                    _audDetails = String.Format("{0}ch / {1}", If(String.IsNullOrEmpty(tAud.Channels), Master.eLang.GetString(138, "Unknown"), tAud.Channels), If(String.IsNullOrEmpty(tAud.Codec), Master.eLang.GetString(138, "Unknown"), tAud.Codec)).ToUpper
                                End If

                                If tMovie.Movie.FileInfo.StreamDetails.Subtitle.Count > 0 Then
                                    Dim subtitleinfo As MediaInfo.Subtitle
                                    For c = 0 To tMovie.Movie.FileInfo.StreamDetails.Subtitle.Count - 1
                                        subtitleinfo = tMovie.Movie.FileInfo.StreamDetails.Subtitle(c)
                                        If Not subtitleinfo Is Nothing Then
                                            If Not String.IsNullOrEmpty(subtitleinfo.Language) Then
                                                _subLanguage = _subLanguage & ";" & subtitleinfo.Language
                                            End If
                                            If Not String.IsNullOrEmpty(subtitleinfo.LongLanguage) Then
                                                _subLongLanguage = _subLongLanguage & ";" & subtitleinfo.LongLanguage
                                            End If
                                            If Not String.IsNullOrEmpty(subtitleinfo.SubsType) Then
                                                _subType = _subType & ";" & subtitleinfo.SubsType
                                            End If
                                        End If
                                    Next
                                End If

                            End If

                            'now check if we need to include this movie
                            If bSearch Then
                                If strIn = Master.eLang.GetString(318, "Source Folder") Then
                                    Dim found As Boolean = False
                                    For Each u As String In strFilter.Split(Convert.ToChar(";"))
                                        If tMovie.Source.Name = u Then
                                            found = True
                                            Exit For
                                        End If
                                    Next
                                    '_curMovie.IsMark = False
                                    If Not found Then Continue For
                                Else
                                    If (strIn = Master.eLang.GetString(319, "Video Flag") AndAlso StringUtils.Wildcard.IsMatch(_vidDetails, strFilter)) OrElse
                                       (strIn = Master.eLang.GetString(320, "Audio Flag") AndAlso StringUtils.Wildcard.IsMatch(_audDetails, strFilter)) OrElse
                                       (strIn = Master.eLang.GetString(21, "Title") AndAlso StringUtils.Wildcard.IsMatch(tMovie.Movie.Title, strFilter)) OrElse
                                       (strIn = Master.eLang.GetString(278, "Year") AndAlso StringUtils.Wildcard.IsMatch(tMovie.Movie.Year, strFilter)) Then
                                        'included - build the output
                                    Else
                                        'filtered out - exclude this one
                                        '_curMovie.IsMark = False
                                        Continue For
                                    End If
                                End If
                            End If
                            FilterMovies.Add(tMovie.ID)

                            'Special Strings
                            row = row.Replace("<$COUNT>", counter.ToString)
                            row = row.Replace("<$DIRNAME>", StringUtils.HtmlEncode(Path.GetDirectoryName(tMovie.Filename)))
                            row = row.Replace("<$FILENAME>", StringUtils.HtmlEncode(Path.GetFileName(tMovie.Filename)))
                            row = row.Replace("<$NOW>", System.DateTime.Now.ToLongDateString) 'Save Build Date. might be useful info!
                            row = row.Replace("<$PATH>", String.Empty)
                            row = row.Replace("<$FILESIZE>", StringUtils.HtmlEncode(FileSize(tMovie.Filename)))

                            'Images
                            row = row.Replace("<$BANNER_FILE>", String.Concat("export/", counter.ToString, "-banner.jpg"))
                            row = row.Replace("<$CLEARART_FILE>", String.Concat("export/", counter.ToString, "-clearart.jpg"))
                            row = row.Replace("<$CLEARLOGO_FILE>", String.Concat("export/", counter.ToString, "-clearlogo.jpg"))
                            row = row.Replace("<$DISCART_FILE>", String.Concat("export/", counter.ToString, "-discart.jpg"))
                            row = row.Replace("<$FANART_FILE>", String.Concat("export/", counter.ToString, "-fanart.jpg"))
                            row = row.Replace("<$LANDSCAPE_FILE>", String.Concat("export/", counter.ToString, "-landscape.jpg"))
                            row = row.Replace("<$POSTER_FILE>", String.Concat("export/", counter.ToString, "-poster.jpg"))

                            'Title
                            Dim Title As String = String.Empty
                            If Not String.IsNullOrEmpty(tMovie.Movie.Title) Then
                                Title = tMovie.Movie.Title
                            Else
                                Title = tMovie.ListTitle
                            End If

                            'Actors
                            Dim ActorsList As New List(Of String)
                            For Each tActor As MediaContainers.Person In tMovie.Movie.Actors
                                ActorsList.Add(tActor.Name)
                            Next

                            'Fields
                            row = row.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList.ToArray)))
                            row = row.Replace("<$CERTIFICATIONS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Certifications.ToArray)))
                            row = row.Replace("<$COUNTRIES>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Countries.ToArray)))
                            row = row.Replace("<$CREDITS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Credits.ToArray)))
                            row = row.Replace("<$DATEADDED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tMovie.DateAdded).ToString("dd.MM.yyyy")))
                            row = row.Replace("<$DATEMODIFIED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tMovie.DateModified).ToString("dd.MM.yyyy")))
                            row = row.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Directors.ToArray)))
                            row = row.Replace("<$GENRES>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Genres.ToArray)))
                            row = row.Replace("<$IMDBID>", StringUtils.HtmlEncode(tMovie.Movie.ID))
                            row = row.Replace("<$LANGUAGE>", StringUtils.HtmlEncode(tMovie.Movie.Language))
                            row = row.Replace("<$LASTPLAYED>", StringUtils.HtmlEncode(tMovie.Movie.LastPlayed))
                            row = row.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(tMovie.ListTitle))
                            row = row.Replace("<$MPAA>", StringUtils.HtmlEncode(tMovie.Movie.MPAA))
                            row = row.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(tMovie.Movie.OriginalTitle))
                            row = row.Replace("<$OUTLINE>", StringUtils.HtmlEncode(tMovie.Movie.Outline))
                            row = row.Replace("<$PLAYCOUNT>", CStr(tMovie.Movie.PlayCount))
                            row = row.Replace("<$PLOT>", StringUtils.HtmlEncode(tMovie.Movie.Plot))
                            row = row.Replace("<$RATING>", StringUtils.HtmlEncode(If(tMovie.Movie.RatingSpecified, Double.Parse(tMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
                            row = row.Replace("<$RELEASEDATE>", StringUtils.HtmlEncode(tMovie.Movie.ReleaseDate))
                            row = row.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tMovie.Movie.Runtime))
                            row = row.Replace("<$STUDIOS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Studios.ToArray)))
                            row = row.Replace("<$TAGLINE>", StringUtils.HtmlEncode(tMovie.Movie.Tagline))
                            row = row.Replace("<$TAGS>", If(tMovie.Movie.TagsSpecified, StringUtils.HtmlEncode((String.Join(" / ", tMovie.Movie.Tags.ToArray))), String.Empty))
                            row = row.Replace("<$TITLE>", StringUtils.HtmlEncode(Title))
                            row = row.Replace("<$TMDBCOLID>", StringUtils.HtmlEncode(tMovie.Movie.TMDBColID))
                            row = row.Replace("<$TMDBID>", StringUtils.HtmlEncode(tMovie.Movie.TMDBID))
                            row = row.Replace("<$TOP250>", StringUtils.HtmlEncode(tMovie.Movie.Top250))
                            row = row.Replace("<$TRAILER>", StringUtils.HtmlEncode(tMovie.Movie.Trailer))
                            row = row.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(tMovie.Movie.VideoSource))
                            row = row.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tMovie.Movie.VotesSpecified, Double.Parse(tMovie.Movie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))
                            row = row.Replace("<$YEAR>", tMovie.Movie.Year)

                            'Audio
                            row = row.Replace("<$AUDIO>", _audDetails)
                            row = row.Replace("<$AUDIOBITRATE>", _audBitrate)
                            row = row.Replace("<$AUDIOCHANNELS>", _audChannels)
                            row = row.Replace("<$AUDIOLANGUAGE>", _audLanguage)
                            row = row.Replace("<$AUDIOLONGLANGUAGE>", _audLongLanguage)

                            'Video
                            row = row.Replace("<$VIDEO>", _vidDetails)
                            row = row.Replace("<$VIDEOASPECT>", _vidAspect)
                            row = row.Replace("<$VIDEOBITRATE>", _vidBitrate)
                            row = row.Replace("<$VIDEODIMENSIONS>", _vidDimensions)
                            row = row.Replace("<$VIDEODURATION>", _vidDuration)
                            row = row.Replace("<$VIDEOFILESIZE>", _vidFileSize)
                            row = row.Replace("<$VIDEOHEIGHT>", _vidHeight)
                            row = row.Replace("<$VIDEOLANGUAGE>", _vidLanguage)
                            row = row.Replace("<$VIDEOLONGLANGUAGE>", _vidLongLanguage)
                            row = row.Replace("<$VIDEOMULTIVIEW>", _vidMultiViewCount)
                            row = row.Replace("<$VIDEOSCANTYPE>", _vidScantype)
                            row = row.Replace("<$VIDEOSTEREOMODE>", _vidStereoMode)
                            row = row.Replace("<$VIDEOWIDTH>", _vidWidth)

                            'Subtitle
                            row = row.Replace("<$SUBTITLELANGUAGE>", _subLanguage)
                            row = row.Replace("<$SUBTITLELONGLANGUAGE>", _subLongLanguage)
                            row = row.Replace("<$SUBTITLETYPE>", _subType)

                            'cocotus, 2013/02 Added support for new MediaInfo-fields
                            row = row.Replace("<$MOVIESETS>", StringUtils.HtmlEncode(AllMovieSetList)) 'A long string of all moviesets, seperated with ;!
                            row = row.Replace("<$TVSHOWS>", StringUtils.HtmlEncode(AllTVShowList)) 'A long string of all tvshows, seperated with |!
                            row = row.Replace("<$SET>", StringUtils.HtmlEncode(GetMovieSets(tMovie))) 'All sets which movie belongs to, seperated with ;!

                            'Flags
                            row = GetAVImages(tMovie, row)
                            HTMLBody.Append(row)
                            counter += 1
                        Next


                    ElseIf part.ContentType = Enums.ContentType.TVShow Then
                        'FilterMovies.Clear()
                        For Each tShow As Database.DBElement In TVShowList
                            Dim row As String = part.Content

                            Dim TVEpisodeLoop As New StringBuilder
                            Dim TVSeasonLoop As New StringBuilder

                            If part.innerContentPart IsNot Nothing Then
                                Dim innerContent As ContentPart = DirectCast(part.innerContentPart, ContentPart)

                                If innerContent.ContentType = Enums.ContentType.TVSeason Then
                                    Dim counter_season As Integer = 1
                                    For Each tSeason As Database.DBElement In tShow.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                                        Dim row_Season As String = innerContent.Content

                                        If innerContent.innerContentPart IsNot Nothing Then
                                            Dim innerContent2 As ContentPart = DirectCast(innerContent.innerContentPart, ContentPart)

                                            If innerContent2.ContentType = Enums.ContentType.TVEpisode Then
                                                Dim innerEpisodeLoop As New StringBuilder
                                                Dim counter_episode As Integer = 1

                                                For Each tEpisode As Database.DBElement In tShow.Episodes.Where(Function(f) f.TVEpisode.Season = tSeason.TVSeason.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                                                    Dim row_Episode As String = innerContent2.Content

                                                    'Special Strings
                                                    row_Episode = row_Episode.Replace("<$COUNT>", counter.ToString)
                                                    row_Episode = row_Episode.Replace("<$COUNT_EPISODE>", counter_episode.ToString)

                                                    'Actors
                                                    Dim ActorsList_Episode As New List(Of String)
                                                    For Each tActor As MediaContainers.Person In tEpisode.TVEpisode.Actors
                                                        ActorsList_Episode.Add(tActor.Name)
                                                    Next

                                                    'Guest Stars
                                                    Dim GuestStarsList_Episode As New List(Of String)
                                                    For Each tActor As MediaContainers.Person In tEpisode.TVEpisode.GuestStars
                                                        GuestStarsList_Episode.Add(tActor.Name)
                                                    Next

                                                    'Fields
                                                    row_Episode = row_Episode.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList_Episode.ToArray)))
                                                    row_Episode = row_Episode.Replace("<$AIRED>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Aired))
                                                    row_Episode = row_Episode.Replace("<$CREDITS>", StringUtils.HtmlEncode(String.Join(" / ", tEpisode.TVEpisode.Credits.ToArray)))
                                                    row_Episode = row_Episode.Replace("<$DATEADDED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tEpisode.DateAdded).ToString("dd.MM.yyyy")))
                                                    row_Episode = row_Episode.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tEpisode.TVEpisode.Directors.ToArray)))
                                                    row_Episode = row_Episode.Replace("<$EPISODE>", StringUtils.HtmlEncode(CStr(tEpisode.TVEpisode.Episode)))
                                                    row_Episode = row_Episode.Replace("<$GUESTSTARS>", StringUtils.HtmlEncode(String.Join(" / ", GuestStarsList_Episode.ToArray)))
                                                    row_Episode = row_Episode.Replace("<$IMDBID>", StringUtils.HtmlEncode(tEpisode.TVEpisode.IMDB))
                                                    row_Episode = row_Episode.Replace("<$LASTPLAYED>", StringUtils.HtmlEncode(tEpisode.TVEpisode.LastPlayed))
                                                    row_Episode = row_Episode.Replace("<$PLOT>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Plot))
                                                    row_Episode = row_Episode.Replace("<$RATING>", StringUtils.HtmlEncode(If(tEpisode.TVEpisode.RatingSpecified, Double.Parse(tEpisode.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
                                                    row_Episode = row_Episode.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Runtime))
                                                    row_Episode = row_Episode.Replace("<$SEASON>", StringUtils.HtmlEncode(CStr(tEpisode.TVEpisode.Season)))
                                                    row_Episode = row_Episode.Replace("<$TITLE>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Title))
                                                    row_Episode = row_Episode.Replace("<$TMDBID>", StringUtils.HtmlEncode(tEpisode.TVEpisode.TMDB))
                                                    row_Episode = row_Episode.Replace("<$TVDBID>", StringUtils.HtmlEncode(tEpisode.TVEpisode.TVDB))
                                                    row_Episode = row_Episode.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(tEpisode.TVEpisode.VideoSource))
                                                    row_Episode = row_Episode.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tEpisode.TVEpisode.VotesSpecified, Double.Parse(tEpisode.TVEpisode.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))

                                                    innerEpisodeLoop.Append(row_Episode)
                                                    TVEpisodeLoop.Append(row_Episode)
                                                    counter_episode += 1
                                                Next

                                                'InnerContent
                                                row_Season = row_Season.Replace("<$$TVEPISODELOOP>", innerEpisodeLoop.ToString)
                                            End If
                                        End If

                                        'Special Strings
                                        row_Season = row_Season.Replace("<$COUNT>", counter.ToString)
                                        row_Season = row_Season.Replace("<$COUNT_SEASON>", counter_season.ToString)

                                        'Images
                                        row_Season = row_Season.Replace("<$BANNER_FILE>", ExportImage(tSeason.ImagesContainer.Banner, counter, Enums.ModifierType.SeasonBanner, counter_season))
                                        row_Season = row_Season.Replace("<$FANART_FILE>", String.Concat("export/", counter.ToString, "-fanart.jpg"))
                                        row_Season = row_Season.Replace("<$LANDSCAPE_FILE>", String.Concat("export/", counter.ToString, "-landscape.jpg"))
                                        row_Season = row_Season.Replace("<$POSTER_FILE>", String.Concat("export/", counter.ToString, "-poster.jpg"))

                                        'Fields
                                        row_Season = row_Season.Replace("<$AIRED>", StringUtils.HtmlEncode(tSeason.TVSeason.Aired))
                                        row_Season = row_Season.Replace("<$PLOT>", StringUtils.HtmlEncode(tSeason.TVSeason.Plot))
                                        row_Season = row_Season.Replace("<$SEASON>", StringUtils.HtmlEncode(CStr(tSeason.TVSeason.Season)))
                                        row_Season = row_Season.Replace("<$TITLE>", StringUtils.HtmlEncode(tSeason.TVSeason.Title))
                                        row_Season = row_Season.Replace("<$TMDBID>", StringUtils.HtmlEncode(tSeason.TVSeason.TMDB))
                                        row_Season = row_Season.Replace("<$TVDBID>", StringUtils.HtmlEncode(tSeason.TVSeason.TVDB))

                                        TVSeasonLoop.Append(row_Season)
                                        counter_season += 1
                                    Next
                                End If
                            End If

                            'FilterMovies.Add(_curTVShow.ID)

                            'InnerContent
                            row = row.Replace("<$$TVEPISODELOOP>", TVEpisodeLoop.ToString)
                            row = row.Replace("<$$TVSEASONLOOP>", TVSeasonLoop.ToString)

                            'Special Strings
                            row = row.Replace("<$COUNT>", counter.ToString)
                            row = row.Replace("<$NOW>", System.DateTime.Now.ToLongDateString) 'Save Build Date. might be useful info!
                            row = row.Replace("<$PATH>", StringUtils.HtmlEncode(tShow.ShowPath))

                            'Images
                            row = row.Replace("<$BANNER_FILE>", String.Concat("export/", counter.ToString, "-banner.jpg"))
                            row = row.Replace("<$CHARACTERART_FILE>", String.Concat("export/", counter.ToString, "-characterart.jpg"))
                            row = row.Replace("<$CLEARART_FILE>", String.Concat("export/", counter.ToString, "-clearart.jpg"))
                            row = row.Replace("<$CLEARLOGO_FILE>", String.Concat("export/", counter.ToString, "-clearlogo.jpg"))
                            row = row.Replace("<$FANART_FILE>", String.Concat("export/", counter.ToString, "-fanart.jpg"))
                            row = row.Replace("<$LANDSCAPE_FILE>", String.Concat("export/", counter.ToString, "-landscape.jpg"))
                            row = row.Replace("<$POSTER_FILE>", String.Concat("export/", counter.ToString, "-poster.jpg"))

                            'Title
                            Dim Title As String = String.Empty
                            If Not String.IsNullOrEmpty(tShow.TVShow.Title) Then
                                Title = tShow.TVShow.Title
                            Else
                                Title = tShow.ListTitle
                            End If

                            'Actors
                            Dim ActorsList As New List(Of String)
                            For Each tActor As MediaContainers.Person In tShow.TVShow.Actors
                                ActorsList.Add(tActor.Name)
                            Next

                            'Fields
                            row = row.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList.ToArray)))
                            row = row.Replace("<$CERTIFICATIONS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Certifications.ToArray)))
                            row = row.Replace("<$COUNTRIES>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Countries.ToArray)))
                            row = row.Replace("<$CREATORS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Creators.ToArray)))
                            row = row.Replace("<$DATEADDED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tShow.DateAdded).ToString("dd.MM.yyyy")))
                            row = row.Replace("<$DATEMODIFIED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tShow.DateModified).ToString("dd.MM.yyyy")))
                            row = row.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Directors.ToArray)))
                            row = row.Replace("<$GENRES>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Genres.ToArray)))
                            row = row.Replace("<$IMDBID>", StringUtils.HtmlEncode(tShow.TVShow.IMDB))
                            row = row.Replace("<$LANGUAGE>", StringUtils.HtmlEncode(tShow.TVShow.Language))
                            row = row.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(tShow.ListTitle))
                            row = row.Replace("<$MPAA>", StringUtils.HtmlEncode(tShow.TVShow.MPAA))
                            row = row.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(tShow.TVShow.OriginalTitle))
                            row = row.Replace("<$PLOT>", StringUtils.HtmlEncode(tShow.TVShow.Plot))
                            row = row.Replace("<$PREMIERED>", StringUtils.HtmlEncode(tShow.TVShow.Premiered))
                            row = row.Replace("<$RATING>", StringUtils.HtmlEncode(If(tShow.TVShow.RatingSpecified, Double.Parse(tShow.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
                            row = row.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tShow.TVShow.Runtime))
                            row = row.Replace("<$STATUS>", StringUtils.HtmlEncode(tShow.TVShow.Status))
                            row = row.Replace("<$STUDIOS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Studios.ToArray)))
                            row = row.Replace("<$TAGS>", If(tShow.TVShow.TagsSpecified, StringUtils.HtmlEncode((String.Join(" / ", tShow.TVShow.Tags.ToArray))), String.Empty))
                            row = row.Replace("<$TITLE>", StringUtils.HtmlEncode(Title))
                            row = row.Replace("<$TMDBID>", StringUtils.HtmlEncode(tShow.TVShow.TMDB))
                            row = row.Replace("<$TVDBID>", tShow.TVShow.TVDB)
                            row = row.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tShow.TVShow.VotesSpecified, Double.Parse(tShow.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))

                            'Flags
                            row = GetAVImages(tShow, row)
                            HTMLBody.Append(row)
                            counter += 1
                        Next
                    End If
                Else
                    HTMLBody.Append(part.Content)
                End If
            Next

            If Not isCL Then
                'just name it index.htm
                'Dim outFile As String = Path.Combine(Me.TempPath, String.Concat(Master.eSettings.Language, ".html"))
                Dim outFile As String = Path.Combine(TempPath, "index.htm")
                DontSaveExtra = False
                SaveAll(If(doNavigate, Master.eLang.GetString(321, "Preparing preview. Please wait..."), String.Empty), Path.GetDirectoryName(htmlPath), outFile)
                If doNavigate Then LoadHTML()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function GetMovieSets(_curMovie As Database.DBElement) As String
        Dim ReturnString As String = String.Empty
        Try

            If _curMovie.Movie.Sets.Count > 0 Then
                For i = 0 To _curMovie.Movie.Sets.Count - 1
                    If i > 0 Then
                        ReturnString = ReturnString + "|" + _curMovie.Movie.Sets.Item(i).Title
                    Else
                        ReturnString = _curMovie.Movie.Sets.Item(i).Title
                    End If

                Next
            End If
            Return ReturnString
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return ReturnString
        End Try

    End Function

    Private Function GetAllMovieSets() As String
        Dim ReturnString As String = String.Empty
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT SetName FROM sets;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        If Not alSets.Contains(SQLreader("SetName").ToString) AndAlso Not String.IsNullOrEmpty(SQLreader("SetName").ToString) Then
                            alSets.Add(SQLreader("SetName").ToString)
                        End If
                    End While
                End If
            End Using
        End Using
        If alSets.Count > 0 Then
            For i = 0 To alSets.Count - 1
                If i > 0 Then
                    ReturnString = ReturnString + "|" + alSets(i)
                Else
                    ReturnString = alSets(i)
                End If
            Next
        End If
        Return ReturnString
    End Function

    Private Function GetAllTVShows() As String
        Dim ReturnString As String = String.Empty
        allTVShows.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tvshow ORDER BY ListTitle COLLATE NOCASE;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        If Not String.IsNullOrEmpty(SQLreader("Title").ToString) Then
                            If Not String.IsNullOrEmpty(SQLreader("idShow").ToString) Then
                                allTVShows.Add(SQLreader("idShow").ToString & "*" & SQLreader("Title").ToString & GetSeasonInfo(SQLreader("idShow").ToString))
                            End If
                        End If
                    End While
                End If
            End Using
        End Using
        If allTVShows.Count > 0 Then
            For i = 0 To allTVShows.Count - 1
                If i > 0 Then
                    ReturnString = ReturnString + "|" + allTVShows(i)
                Else
                    ReturnString = allTVShows(i)
                End If
            Next
        End If
        Return ReturnString
    End Function

    Private Function GetSeasonInfo(ByVal Id As String) As String
        Dim ReturnString As String = String.Empty

        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idShow")
            SQLcommand.CommandText = "SELECT * FROM seasonslist WHERE idShow = (?);"
            parID.Value = Id
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()

                        If Not String.IsNullOrEmpty(SQLreader("Season").ToString) AndAlso Not SQLreader("Season").ToString.Contains("999") Then
                            If Not String.IsNullOrEmpty(SQLreader("PosterPath").ToString) Then
                                ExportSeasonImage(SQLreader("idShow").ToString & "s" & SQLreader("Season").ToString, SQLreader("PosterPath").ToString, TempPath)
                            End If
                            ReturnString = ReturnString + "*" + SQLreader("idShow").ToString & "s" & SQLreader("Season").ToString
                        End If
                    End While
                End If
            End Using
        End Using
        Return ReturnString
    End Function

    Private Sub ExportSeasonImage(ByVal filename As String, ByVal source As String, ByVal temppath As String)
        Try

            Dim finalpath As String = Path.Combine(temppath, "export")
            Directory.CreateDirectory(finalpath)

            Try
                Dim posterfile As String = Path.Combine(finalpath, String.Concat(filename, "-poster.jpg"))
                If File.Exists(source) Then
                    'cocotus, 2013/02 Export HTML expanded: configurable resizable images

                    'old method
                    'If new_width > 0 Then
                    '    Dim im As New Images
                    '    im.FromFile(_curMovie.PosterPath)
                    '    ImageUtils.ResizeImage(im.Image, new_width, new_width, False, Color.Black.ToArgb)
                    '    im.Save(posterfile)
                    'Else
                    '    File.Copy(_curMovie.PosterPath, posterfile, True)
                    'End If

                    'If strPosterSize = "original" Then
                    File.Copy(source, posterfile, True)
                    'Else
                    '    'Now we do some image processing to make the output file smaller!
                    '    Try
                    '        Dim img As Image
                    '        Dim sFileName As String = source
                    '        Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                    '        img = Image.FromStream(fs)
                    '        fs.Close()
                    '        '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                    '        Dim imgresized As Image = ImageUtils.ResizeImage(img, PosterSize)
                    '        img.Dispose()
                    '        '2. Step: Now use jpeg compression to make file even smaller...
                    '        ImageUtils.JPEGCompression(imgresized, posterfile, CInt(clsAdvancedSettings.GetSetting("ExportImageQuality", "70")))
                    '        imgresized.Dispose()

                    '    Catch ex As Exception
                    '        'The old method, used here when anything goes wrong
                    '        File.Copy(source, posterfile, True)
                    '    End Try
                    'End If

                    'cocotus end

                End If

            Catch
            End Try
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        MovieList.Clear()
        ' Load nfo movies using path from DB
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim _tmpMovie As New Database.DBElement
            Dim _ID As Integer
            Dim iProg As Integer = 0
            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idMovie) AS mcount FROM movie;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
            End Using
            SQLNewcommand.CommandText = String.Concat("SELECT idMovie FROM movie ORDER BY ListTitle COLLATE NOCASE;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        _ID = Convert.ToInt32(SQLreader("idMovie"))
                        _tmpMovie = Master.DB.LoadMovieFromDB(_ID)
                        MovieList.Add(_tmpMovie)
                        bwLoadInfo.ReportProgress(iProg, _tmpMovie.ListTitle) '  show File
                        iProg += 1
                        If bwLoadInfo.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If
                    End While
                    e.Result = True
                Else
                    e.Cancel = True
                End If
            End Using
        End Using

        TVShowList.Clear()
        ' Load nfo tv shows using path from DB
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim _tmpTVShow As New Database.DBElement
            Dim _ID As Integer
            Dim iProg As Integer = 0
            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idShow) AS mcount FROM tvshow;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
            End Using
            SQLNewcommand.CommandText = String.Concat("SELECT idShow FROM tvshow ORDER BY ListTitle COLLATE NOCASE;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        _ID = Convert.ToInt32(SQLreader("idShow"))
                        _tmpTVShow = Master.DB.LoadTVShowFromDB(_ID, True, True)
                        TVShowList.Add(_tmpTVShow)
                        bwLoadInfo.ReportProgress(iProg, _tmpTVShow.ListTitle) '  show File
                        iProg += 1
                        If bwLoadInfo.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If
                    End While
                    e.Result = True
                Else
                    e.Cancel = True
                End If
            End Using
        End Using
    End Sub

    Private Sub bwLoadInfo_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadInfo.ProgressChanged
        If Not isCL Then
            If e.ProgressPercentage >= 0 Then
                pbCompile.Value = e.ProgressPercentage
                lblFile.Text = e.UserState.ToString
            Else
                pbCompile.Maximum = Convert.ToInt32(e.UserState)
            End If
        End If
    End Sub

    Private Sub bwLoadInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadInfo.RunWorkerCompleted
        If Not isCL Then
            bCancelled = e.Cancelled
            If Not e.Cancelled Then
                If Not isCL Then
                    BuildHTML(False, String.Empty, String.Empty, base_template, False)
                End If
                LoadHTML()
            Else
                wbPreview.DocumentText = String.Concat("<center><h1 style=""color:Red;"">", Master.eLang.GetString(284, "Canceled"), "</h1></center>")
            End If
            pnlCancel.Visible = False
        End If
    End Sub

    Private Sub bwSaveAll_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwSaveAll.DoWork
        Try
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Dim destPathShort As String = Path.GetDirectoryName(Args.destPath)
            'Only create extra files once for each template... dont do it when applyng filters
            If Not DontSaveExtra Then
                FileUtils.Delete.DeleteDirectory(TempPath)
                CopyDirectory(Args.srcPath, destPathShort, True)
                If tExportSettings.Flags Then
                    Args.srcPath = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Flags", Path.DirectorySeparatorChar)
                    Directory.CreateDirectory(Path.Combine(destPathShort, "flags"))
                    CopyDirectory(Args.srcPath, Path.Combine(destPathShort, "flags"), True)
                End If
                If bwSaveAll.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                If tExportSettings.MainPosters Then
                    ExportPoster(destPathShort, Args.resizePoster)
                End If
                If bwSaveAll.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                If tExportSettings.MainFanarts Then
                    ExportFanart(destPathShort)
                End If
                If clsAdvancedSettings.GetBooleanSetting("ExportMissingEpisodes", False) = True Then
                    Try
                        AllTVShowList = GetAllTVShows()
                    Catch ex As Exception

                    End Try
                End If
                If bwSaveAll.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                DontSaveExtra = True
            Else
                If File.Exists(Args.destPath) Then
                    System.IO.File.Delete(Args.destPath)
                End If
            End If
            Dim myStream As Stream = File.OpenWrite(Args.destPath)
            If myStream IsNot Nothing Then
                myStream.Write(System.Text.Encoding.ASCII.GetBytes(HTMLBody.ToString), 0, HTMLBody.ToString.Length)
                myStream.Close()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwSaveAll_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwSaveAll.RunWorkerCompleted
        workerCanceled = e.Cancelled
    End Sub


    Private Sub cbTemplate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTemplate.SelectedIndexChanged
        base_template = cbTemplate.Text
        DontSaveExtra = False
        ' Dim sFilter As String = String.Empty
        ' If cbSearch.Text = Master.eLang.GetString(318, "Source Folder") Then
        '     For Each s As String In lstSources.CheckedItems
        '         sFilter = String.Concat(sFilter, If(sFilter = String.Empty, String.Empty, ";"), s.ToString)
        '     Next
        ' Else
        '     sFilter = txtSearch.Text
        ' End If

        'BuildHTML(use_filter, sFilter, cbSearch.Text, base_template, True)


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If bwSaveAll.IsBusy Then
            bwSaveAll.CancelAsync()
        End If
        While bwSaveAll.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub dlgExportMovies_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bwLoadInfo.IsBusy Then
            DoCancel()
            While bwLoadInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End If
        FileUtils.Delete.DeleteDirectory(TempPath)
    End Sub

    Private Sub dlgExportMovies_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
        Dim di As DirectoryInfo = New DirectoryInfo(String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html"))
        If di.Exists Then
            For Each i As DirectoryInfo In di.GetDirectories
                If Not (i.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                    cbTemplate.Items.Add(i.Name)
                End If
            Next
            If cbTemplate.Items.Count > 0 Then
                RemoveHandler cbTemplate.SelectedIndexChanged, AddressOf cbTemplate_SelectedIndexChanged
                cbTemplate.SelectedIndex = 0
                base_template = cbTemplate.Text
                AddHandler cbTemplate.SelectedIndexChanged, AddressOf cbTemplate_SelectedIndexChanged
            End If
        End If
    End Sub

    Private Sub dlgMoviesReport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' Show Cancel Panel
        btnCancel.Visible = True
        lblCompiling.Visible = True
        pbCompile.Visible = True
        pbCompile.Style = ProgressBarStyle.Continuous
        lblCanceling.Visible = False
        pnlCancel.Visible = True
        Application.DoEvents()

        Activate()

        'Start worker
        bwLoadInfo = New System.ComponentModel.BackgroundWorker
        bwLoadInfo.WorkerSupportsCancellation = True
        bwLoadInfo.WorkerReportsProgress = True
        bwLoadInfo.RunWorkerAsync()
    End Sub

    Private Sub DoCancel()
        bwLoadInfo.CancelAsync()
        btnCancel.Visible = False
        lblCompiling.Visible = False
        pbCompile.Style = ProgressBarStyle.Marquee
        pbCompile.MarqueeAnimationSpeed = 25
        lblCanceling.Visible = True
        lblFile.Visible = False
    End Sub

    Private Function ExportImage(ByVal tImage As MediaContainers.Image, ByVal iCount As Integer, ByVal tImageType As Enums.ModifierType, Optional ByVal iCount_Season As Integer = -1, Optional ByVal iCount_Episode As Integer = -1) As String
        Dim strPath As String = String.Empty

        If File.Exists(tImage.LocalFilePath) Then
            Select Case tImageType
                Case Enums.ModifierType.EpisodeFanart
                    If tExportSettings.EpisodeFanarts AndAlso Not iCount = -1 AndAlso Not iCount_Episode = -1 AndAlso Not iCount_Season = -1 Then

                    End If
                Case Enums.ModifierType.EpisodePoster
                Case Enums.ModifierType.MainBanner
                Case Enums.ModifierType.MainCharacterArt
                Case Enums.ModifierType.MainClearArt
                Case Enums.ModifierType.MainClearLogo
                Case Enums.ModifierType.MainDiscArt
                Case Enums.ModifierType.MainFanart
                Case Enums.ModifierType.MainLandscape
                Case Enums.ModifierType.MainPoster
            End Select
        End If

        Return strPath
    End Function

    Private Sub ExportFanart(ByVal fpath As String)
        Try
            Dim counter As Integer = 1
            Dim finalpath As String = Path.Combine(fpath, "export")
            Directory.CreateDirectory(finalpath)

            For Each _curMovie As Database.DBElement In MovieList.Where(Function(y) FilterMovies.Contains(y.ID))
                Try
                    Dim fanartfile As String = Path.Combine(finalpath, String.Concat(counter.ToString, "-fanart.jpg"))
                    If File.Exists(_curMovie.ImagesContainer.Fanart.LocalFilePath) Then

                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images

                        'old method, just copy/no image conversion
                        'File.Copy(_curMovie.FanartPath, fanartfile, True)

                        'If strFanartSize = "original" Then
                        '    File.Copy(_curMovie.ImagesContainer.Fanart.LocalFilePath, fanartfile, True)
                        'Else
                        '    'Now we do some image processing to make the output file smaller!
                        '    Try
                        '        Dim img As Image
                        '        Dim sFileName As String = _curMovie.ImagesContainer.Fanart.LocalFilePath
                        '        Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                        '        img = Image.FromStream(fs)
                        '        fs.Close()
                        '        '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                        '        Dim imgresized As Image = ImageUtils.ResizeImage(img, FanartSize)
                        '        img.Dispose()
                        '        '2. Step: Now use jpeg compression to make file even smaller...
                        '        ImageUtils.JPEGCompression(imgresized, fanartfile, CInt(clsAdvancedSettings.GetSetting("ExportImageQuality", "70")))
                        '        imgresized.Dispose()

                        '    Catch ex As Exception
                        '        'The old method, used here when anything goes wrong
                        '        File.Copy(_curMovie.ImagesContainer.Fanart.LocalFilePath, fanartfile, True)
                        '    End Try
                        'End If
                        'cocotus end


                    End If
                    counter += 1
                Catch
                End Try

                If bwSaveAll.CancellationPending Then
                    Return
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ExportPoster(ByVal fpath As String, ByVal new_width As Integer)
        Try
            Dim counter As Integer = 1
            Dim finalpath As String = Path.Combine(fpath, "export")
            Directory.CreateDirectory(finalpath)
            For Each _curMovie As Database.DBElement In MovieList.Where(Function(y) FilterMovies.Contains(y.ID))
                Try
                    Dim posterfile As String = Path.Combine(finalpath, String.Concat(counter.ToString, "-poster.jpg"))
                    If File.Exists(_curMovie.ImagesContainer.Poster.LocalFilePath) Then                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images
                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images

                        'old method
                        'If new_width > 0 Then
                        '    Dim im As New Images
                        '    im.FromFile(_curMovie.PosterPath)
                        '    ImageUtils.ResizeImage(im.Image, new_width, new_width, False, Color.Black.ToArgb)
                        '    im.Save(posterfile)
                        'Else
                        '    File.Copy(_curMovie.PosterPath, posterfile, True)
                        'End If

                        'If strPosterSize = "original" Then
                        File.Copy(_curMovie.ImagesContainer.Poster.LocalFilePath, posterfile, True)
                        'Else
                        '    'Now we do some image processing to make the output file smaller!
                        '    Try
                        '        Dim img As Image
                        '        Dim sFileName As String = _curMovie.ImagesContainer.Poster.LocalFilePath
                        '        Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                        '        img = Image.FromStream(fs)
                        '        fs.Close()
                        '        '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                        '        Dim imgresized As Image = ImageUtils.ResizeImage(img, PosterSize)
                        '        img.Dispose()
                        '        '2. Step: Now use jpeg compression to make file even smaller...
                        '        ImageUtils.JPEGCompression(imgresized, posterfile, CInt(clsAdvancedSettings.GetSetting("ExportImageQuality", "70")))
                        '        imgresized.Dispose()

                        '    Catch ex As Exception
                        '        'The old method, used here when anything goes wrong
                        '        File.Copy(_curMovie.ImagesContainer.Poster.LocalFilePath, posterfile, True)
                        '    End Try
                        'End If

                        'cocotus end
                    End If

                Catch
                End Try
                counter += 1
                If bwSaveAll.CancellationPending Then
                    Return
                End If

            Next

            For Each _curTVShow As Database.DBElement In TVShowList '.Where(Function(y) FilterMovies.Contains(y.ID))
                Try
                    Dim posterfile As String = Path.Combine(finalpath, String.Concat(counter.ToString, "-poster.jpg"))
                    If File.Exists(_curTVShow.ImagesContainer.Poster.LocalFilePath) Then                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images
                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images

                        'old method
                        'If new_width > 0 Then
                        '    Dim im As New Images
                        '    im.FromFile(_curMovie.PosterPath)
                        '    ImageUtils.ResizeImage(im.Image, new_width, new_width, False, Color.Black.ToArgb)
                        '    im.Save(posterfile)
                        'Else
                        'File.Copy(_curMovie.PosterPath, posterfile, True)
                        'End If

                        'If strPosterSize = "original" Then
                        File.Copy(_curTVShow.ImagesContainer.Poster.LocalFilePath, posterfile, True)
                        'Else
                        '    'Now we do some image processing to make the output file smaller!
                        '    Try
                        '        Dim img As Image
                        '        Dim sFileName As String = _curTVShow.ImagesContainer.Poster.LocalFilePath
                        '        Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                        '        img = Image.FromStream(fs)
                        '        fs.Close()
                        '        '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                        '        Dim imgresized As Image = ImageUtils.ResizeImage(img, PosterSize)
                        '        img.Dispose()
                        '        '2. Step: Now use jpeg compression to make file even smaller...
                        '        ImageUtils.JPEGCompression(imgresized, posterfile, CInt(clsAdvancedSettings.GetSetting("ExportImageQuality", "70")))
                        '        imgresized.Dispose()

                        '    Catch ex As Exception
                        '        'The old method, used here when anything goes wrong
                        '        File.Copy(_curTVShow.ImagesContainer.Poster.LocalFilePath, posterfile, True)
                        '    End Try
                        'End If

                        'cocotus end
                    End If

                Catch
                End Try
                counter += 1
                If bwSaveAll.CancellationPending Then
                    Return
                End If

            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function GetAVImages(ByVal AVMovie As Database.DBElement, ByVal line As String) As String
        If APIXML.lFlags.Count > 0 Then
            Try
                Dim fiAV As MediaInfo.Fileinfo = AVMovie.Movie.FileInfo
                Dim tVideo As MediaInfo.Video = NFO.GetBestVideo(fiAV)
                Dim tAudio As MediaInfo.Audio = NFO.GetBestAudio(fiAV, False)

                Dim vresFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = NFO.GetResFromDimensions(tVideo).ToLower AndAlso f.Type = APIXML.FlagType.VideoResolution)
                If vresFlag IsNot Nothing Then
                    line = line.Replace("<$FLAG_VRES>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
                Else
                    vresFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoResolution)
                    If vresFlag IsNot Nothing Then
                        line = line.Replace("<$FLAG_VRES>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
                    End If
                End If

                'Dim vsourceFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = APIXML.GetFileSource(AVMovie.Filename) AndAlso f.Type = APIXML.FlagType.VideoSource)
                Dim vsourceFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name.ToLower = AVMovie.VideoSource AndAlso f.Type = APIXML.FlagType.VideoSource)
                If vsourceFlag IsNot Nothing Then
                    line = line.Replace("<$FLAG_VSOURCE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
                Else
                    vsourceFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoSource)
                    If vsourceFlag IsNot Nothing Then
                        line = line.Replace("<$FLAG_VSOURCE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
                    End If
                End If

                Dim vcodecFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = tVideo.Codec.ToLower AndAlso f.Type = APIXML.FlagType.VideoCodec)
                If vcodecFlag IsNot Nothing Then
                    line = line.Replace("<$FLAG_VTYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
                Else
                    vcodecFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoCodec)
                    If vcodecFlag IsNot Nothing Then
                        line = line.Replace("<$FLAG_VTYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
                    End If
                End If

                Dim acodecFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = tAudio.Codec.ToLower AndAlso f.Type = APIXML.FlagType.AudioCodec)
                If acodecFlag IsNot Nothing Then
                    line = line.Replace("<$FLAG_ATYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
                Else
                    acodecFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = APIXML.FlagType.AudioCodec)
                    If acodecFlag IsNot Nothing Then
                        line = line.Replace("<$FLAG_ATYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
                    End If
                End If

                Dim achanFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = tAudio.Channels AndAlso f.Type = APIXML.FlagType.AudioChan)
                If achanFlag IsNot Nothing Then
                    line = line.Replace("<$FLAG_ACHAN>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
                Else
                    achanFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = APIXML.FlagType.AudioChan)
                    If achanFlag IsNot Nothing Then
                        line = line.Replace("<$FLAG_ACHAN>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
                    End If
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End If
        Return line
    End Function

    Private Sub LoadHTML()
        Warning(True, Master.eLang.GetString(326, "Loading. Please wait..."))
        '   Dim tmphtml As String = Path.Combine(Me.TempPath, String.Concat(Master.eSettings.Language, ".html"))
        Dim tmphtml As String = Path.Combine(TempPath, "index.htm")
        Try
            wbPreview.Navigate(tmphtml)
            btnSave.Enabled = True
        Catch ex As Exception
            btnSave.Enabled = False
        End Try
    End Sub

    Function FileSize(ByVal fPath As String) As String
        Dim fSize As Long = 0
        If StringUtils.IsStacked(Path.GetFileNameWithoutExtension(fPath), True) OrElse FileUtils.Common.isVideoTS(fPath) OrElse FileUtils.Common.isBDRip(fPath) Then
            Try
                Dim sExt As String = Path.GetExtension(fPath).ToLower
                Dim oFile As String = StringUtils.CleanStackingMarkers(fPath, False)
                Dim sFile As New List(Of String)
                Dim bIsVTS As Boolean = False

                If sExt = ".ifo" OrElse sExt = ".bup" OrElse sExt = ".vob" Then
                    bIsVTS = True
                End If

                If bIsVTS Then
                    Try
                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(fPath).FullName, "VTS*.VOB"))
                    Catch
                    End Try
                ElseIf sExt = ".m2ts" Then
                    Try
                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(fPath).FullName, "*.m2ts"))
                    Catch
                    End Try
                Else
                    Try
                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(fPath).FullName, StringUtils.CleanStackingMarkers(Path.GetFileName(fPath), True)))
                    Catch
                    End Try
                End If

                For Each tFile As String In sFile
                    fSize += New FileInfo(tFile).Length
                Next
            Catch ex As Exception
            End Try
        Else
            fSize = New FileInfo(fPath).Length
        End If

        Select Case fSize
            Case 0 To 1023
                Return fSize & " Bytes"
            Case 1024 To 1048575
                Return String.Concat((fSize / 1024).ToString("###0.00"), " KB")
            Case 1048576 To 1043741824
                Return String.Concat((fSize / 1024 ^ 2).ToString("###0.00"), " MB")
            Case Is > 1043741824
                Return String.Concat((fSize / 1024 ^ 3).ToString("###0.00"), " GB")
        End Select

        Return "0 bytes"
    End Function

    Private Sub SaveAll(ByVal sWarning As String, ByVal srcPath As String, ByVal destPath As String, Optional ByVal resizePoster As Integer = 200)


        wbPreview.Visible = False
        If Not String.IsNullOrEmpty(sWarning) Then Warning(True, sWarning)
        cbFilter.Enabled = False
        cbTemplate.Enabled = False
        btnBuild.Enabled = False
        btnSave.Enabled = False
        bwSaveAll = New System.ComponentModel.BackgroundWorker
        bwSaveAll.WorkerReportsProgress = True
        bwSaveAll.WorkerSupportsCancellation = True
        bwSaveAll.RunWorkerAsync(New Arguments With {.srcPath = srcPath, .destPath = destPath, .resizePoster = resizePoster})
        While bwSaveAll.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        cbFilter.Enabled = True
        cbTemplate.Enabled = True
        btnBuild.Enabled = True
        btnSave.Enabled = True
        If pnlCancel.Visible Then Warning(False)
        If Not workerCanceled Then
            wbPreview.Visible = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'cocotus  Use settings instead of opening filedialog which causes problems!
        'old
        'Dim saveHTML As New SaveFileDialog()
        'Dim myStream As Stream
        'saveHTML.Filter = "HTML files (*.htm)|*.htm"
        'saveHTML.FilterIndex = 2
        'saveHTML.RestoreDirectory = True

        'If saveHTML.ShowDialog() = DialogResult.OK Then
        '    myStream = saveHTML.OpenFile()
        '    myStream.Close()
        '    If Not IsNothing(myStream) Then
        '        DontSaveExtra = False 'Force Full Save
        '        Dim srcPath As String = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html", Path.DirectorySeparatorChar, base_template, Path.DirectorySeparatorChar)
        '        Me.SaveAll(Master.eLang.GetString(327, "Saving all files. Please wait..."), srcPath, saveHTML.FileName)
        '    End If
        'End If
        'new simply copy from temp to user defined directory
        Cursor = Cursors.WaitCursor
        If Directory.Exists(clsAdvancedSettings.GetSetting("ExportPath", String.Empty)) Then
            CopyDirectory(TempPath, clsAdvancedSettings.GetSetting("ExportPath", String.Empty), True)
            btnSave.Enabled = False
            MessageBox.Show((Master.eLang.GetString(1003, "Template saved to:") & clsAdvancedSettings.GetSetting("ExportPath", String.Empty)), Master.eLang.GetString(361, "Finished!"), MessageBoxButtons.OK)
        Else
            btnSave.Enabled = False
            MessageBox.Show((Master.eLang.GetString(221, "Export Path is not valid:") & clsAdvancedSettings.GetSetting("ExportPath", String.Empty)), Master.eLang.GetString(816, "An Error Has Occurred"), MessageBoxButtons.OK)
        End If
        Cursor = Cursors.Default
    End Sub


    Private Sub SetUp()
        Text = Master.eLang.GetString(328, "Export Movies")
        btnSave.Text = Master.eLang.GetString(273, "Save")
        btnClose.Text = Master.eLang.GetString(19, "Close")

        lblCompiling.Text = Master.eLang.GetString(177, "Compiling Movie List...")
        lblCanceling.Text = Master.eLang.GetString(178, "Canceling Compilation...")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        lblTemplate.Text = Master.eLang.GetString(334, "Template")
        btnBuild.Text = Master.eLang.GetString(1004, "Generate HTML...")
        btnSave.Enabled = False

    End Sub

    Private Sub Warning(ByVal show As Boolean, Optional ByVal txt As String = "")
        Try
            btnCancel.Visible = True
            btnCancel.Enabled = True
            lblCompiling.Visible = True
            pbCompile.Visible = True
            pbCompile.Style = ProgressBarStyle.Marquee
            pbCompile.MarqueeAnimationSpeed = 25
            lblCanceling.Visible = False
            pnlCancel.Visible = show
            lblFile.Visible = False
            lblCompiling.Text = txt
            Application.DoEvents()
            pnlCancel.BringToFront()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wbPreview_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbPreview.DocumentCompleted
        If Not bCancelled Then
            If Not cbTemplate.Text = String.Empty Then btnSave.Enabled = True
        End If
        Warning(False)
    End Sub

    Private Sub btnBuild_Click(sender As Object, e As EventArgs) Handles btnBuild.Click
        If cbFilter.Text = "Filter 1" Then
            strFilter = clsAdvancedSettings.GetSetting("ExportFilter1", String.Empty)
        ElseIf cbFilter.Text = "Filter 2" Then
            strFilter = clsAdvancedSettings.GetSetting("ExportFilter2", String.Empty)
        ElseIf cbFilter.Text = "Filter 3" Then
            strFilter = clsAdvancedSettings.GetSetting("ExportFilter3", String.Empty)
        Else
            strFilter = String.Empty
        End If
        If Not strFilter = String.Empty Then
            use_filter = True
            Dim sFiltertype As String = String.Empty
            Dim parts As String() = strFilter.Split(New String() {"#"}, StringSplitOptions.None)
            strFilter = parts(0)
            If parts.Length > 1 Then
                sFiltertype = parts(1)
            End If
            BuildHTML(use_filter, strFilter, sFiltertype, base_template, True)
        Else
            BuildHTML(False, String.Empty, String.Empty, base_template, True)
        End If
    End Sub

    Dim strFilter As String = String.Empty
    'The following resizing method of image requires Size object, so lets create that...
    Dim FanartSize As New Size(1024, 576)
    Dim PosterSize As New Size(200, 300)

    'cocotus end

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim destPath As String
        Dim resizePoster As Integer
        Dim srcPath As String

#End Region 'Fields

    End Structure

    Private Structure ContentPart
        Dim Content As String
        Dim ContentType As Enums.ContentType
        Dim innerContentPart As Object
        Dim isLooped As Boolean
    End Structure

    <Serializable()>
    <XmlRoot("exportsettings")>
    Class ExportSettings

#Region "Fields"

        Private _episodeposters As Boolean
        Private _episodeposters_maxheight As Integer
        Private _episodeposters_maxwidth As Integer
        Private _episodefanarts As Boolean
        Private _episodefanarts_maxheight As Integer
        Private _episodefanarts_maxwidth As Integer
        Private _flags As Boolean
        Private _mainbanners As Boolean
        Private _mainbanners_maxheight As Integer
        Private _mainbanners_maxwidth As Integer
        Private _maincharacterarts As Boolean
        Private _maincleararts As Boolean
        Private _mainclearlogos As Boolean
        Private _maindiscarts As Boolean
        Private _mainfanarts As Boolean
        Private _mainfanarts_maxheight As Integer
        Private _mainfanarts_maxwidth As Integer
        Private _mainlandscapes As Boolean
        Private _mainlandscapes_maxheight As Integer
        Private _mainlandscapes_maxwidth As Integer
        Private _mainposters As Boolean
        Private _mainposters_maxheight As Integer
        Private _mainposters_maxwidth As Integer
        Private _seasonbanners As Boolean
        Private _seasonbanners_maxheight As Integer
        Private _seasonbanners_maxwidth As Integer
        Private _seasonfanarts As Boolean
        Private _seasonfanarts_maxheight As Integer
        Private _seasonfanarts_maxwidth As Integer
        Private _seasonlandscapes As Boolean
        Private _seasonlandscapes_maxheight As Integer
        Private _seasonlandscapes_maxwidth As Integer
        Private _seasonposters As Boolean
        Private _seasonposters_maxheight As Integer
        Private _seasonposters_maxwidth As Integer

#End Region 'Fields

#Region "Properties"

        <XmlElement("episodefanarts")>
        Public Property EpisodeFanarts() As Boolean
            Get
                Return _episodefanarts
            End Get
            Set(ByVal value As Boolean)
                _episodefanarts = value
            End Set
        End Property

        <XmlElement("episodefanarts_maxheight")>
        Public Property EpisodeFanarts_MaxHeight() As Integer
            Get
                Return _episodefanarts_maxheight
            End Get
            Set(ByVal value As Integer)
                _episodefanarts_maxheight = value
            End Set
        End Property

        <XmlElement("episodefanarts_maxwidth")>
        Public Property EpisodeFanarts_MaxWidth() As Integer
            Get
                Return _episodefanarts_maxwidth
            End Get
            Set(ByVal value As Integer)
                _episodefanarts_maxwidth = value
            End Set
        End Property

        <XmlElement("episodeposters")>
        Public Property EpisodePosters() As Boolean
            Get
                Return _episodeposters
            End Get
            Set(ByVal value As Boolean)
                _episodeposters = value
            End Set
        End Property

        <XmlElement("episodeposters_maxheight")>
        Public Property EpisodePosters_MaxHeight() As Integer
            Get
                Return _episodeposters_maxheight
            End Get
            Set(ByVal value As Integer)
                _episodeposters_maxheight = value
            End Set
        End Property

        <XmlElement("episodeposters_maxwidth")>
        Public Property EpisodePosters_MaxWidth() As Integer
            Get
                Return _episodeposters_maxwidth
            End Get
            Set(ByVal value As Integer)
                _episodeposters_maxwidth = value
            End Set
        End Property

        <XmlElement("flags")>
        Public Property Flags() As Boolean
            Get
                Return _flags
            End Get
            Set(ByVal value As Boolean)
                _flags = value
            End Set
        End Property

        <XmlElement("mainbanners")>
        Public Property MainBanners() As Boolean
            Get
                Return _mainbanners
            End Get
            Set(ByVal value As Boolean)
                _mainbanners = value
            End Set
        End Property

        <XmlElement("mainbanners_maxheight")>
        Public Property MainBanners_MaxHeight() As Integer
            Get
                Return _mainbanners_maxheight
            End Get
            Set(ByVal value As Integer)
                _mainbanners_maxheight = value
            End Set
        End Property

        <XmlElement("mainbanners_maxwidth")>
        Public Property MainBanners_MaxWidth() As Integer
            Get
                Return _mainbanners_maxwidth
            End Get
            Set(ByVal value As Integer)
                _mainbanners_maxwidth = value
            End Set
        End Property

        <XmlElement("maincharacterarts")>
        Public Property MainCharacterarts() As Boolean
            Get
                Return _maincharacterarts
            End Get
            Set(ByVal value As Boolean)
                _maincharacterarts = value
            End Set
        End Property

        <XmlElement("maincleararts")>
        Public Property MainClearArts() As Boolean
            Get
                Return _maincleararts
            End Get
            Set(ByVal value As Boolean)
                _maincleararts = value
            End Set
        End Property

        <XmlElement("mainclearlogos")>
        Public Property MainClearLogos() As Boolean
            Get
                Return _mainclearlogos
            End Get
            Set(ByVal value As Boolean)
                _mainclearlogos = value
            End Set
        End Property

        <XmlElement("maindiscarts")>
        Public Property MainDiscArts() As Boolean
            Get
                Return _maindiscarts
            End Get
            Set(ByVal value As Boolean)
                _maindiscarts = value
            End Set
        End Property

        <XmlElement("mainfanarts")>
        Public Property MainFanarts() As Boolean
            Get
                Return _mainfanarts
            End Get
            Set(ByVal value As Boolean)
                _mainfanarts = value
            End Set
        End Property

        <XmlElement("mainfanarts_maxheight")>
        Public Property MainFanarts_MaxHeight() As Integer
            Get
                Return _mainfanarts_maxheight
            End Get
            Set(ByVal value As Integer)
                _mainfanarts_maxheight = value
            End Set
        End Property

        <XmlElement("mainfanarts_maxwidth")>
        Public Property MainFanarts_MaxWidth() As Integer
            Get
                Return _mainfanarts_maxwidth
            End Get
            Set(ByVal value As Integer)
                _mainfanarts_maxwidth = value
            End Set
        End Property

        <XmlElement("mainlandscapes")>
        Public Property MainLandscapes() As Boolean
            Get
                Return _mainlandscapes
            End Get
            Set(ByVal value As Boolean)
                _mainlandscapes = value
            End Set
        End Property

        <XmlElement("mainlandscapes_maxheight")>
        Public Property MainLandscapes_MaxHeight() As Integer
            Get
                Return _mainlandscapes_maxheight
            End Get
            Set(ByVal value As Integer)
                _mainlandscapes_maxheight = value
            End Set
        End Property

        <XmlElement("mainlandscapes_maxwidth")>
        Public Property MainLandscapes_MaxWidth() As Integer
            Get
                Return _mainlandscapes_maxwidth
            End Get
            Set(ByVal value As Integer)
                _mainlandscapes_maxwidth = value
            End Set
        End Property

        <XmlElement("mainposters")>
        Public Property MainPosters() As Boolean
            Get
                Return _mainposters
            End Get
            Set(ByVal value As Boolean)
                _mainposters = value
            End Set
        End Property

        <XmlElement("mainposters_maxheight")>
        Public Property MainPosters_MaxHeight() As Integer
            Get
                Return _mainposters_maxheight
            End Get
            Set(ByVal value As Integer)
                _mainposters_maxheight = value
            End Set
        End Property

        <XmlElement("mainposters_maxwidth")>
        Public Property MainPosters_MaxWidth() As Integer
            Get
                Return _mainposters_maxwidth
            End Get
            Set(ByVal value As Integer)
                _mainposters_maxwidth = value
            End Set
        End Property

        <XmlElement("seasonbanners")>
        Public Property SeasonBanners() As Boolean
            Get
                Return _seasonbanners
            End Get
            Set(ByVal value As Boolean)
                _seasonbanners = value
            End Set
        End Property

        <XmlElement("seasonbanners_maxheight")>
        Public Property SeasonBanners_MaxHeight() As Integer
            Get
                Return _seasonbanners_maxheight
            End Get
            Set(ByVal value As Integer)
                _seasonbanners_maxheight = value
            End Set
        End Property

        <XmlElement("seasonbanners_maxwidth")>
        Public Property SeasonBanners_MaxWidth() As Integer
            Get
                Return _seasonbanners_maxwidth
            End Get
            Set(ByVal value As Integer)
                _seasonbanners_maxwidth = value
            End Set
        End Property

        <XmlElement("seasonfanarts")>
        Public Property SeasonFanarts() As Boolean
            Get
                Return _seasonfanarts
            End Get
            Set(ByVal value As Boolean)
                _seasonfanarts = value
            End Set
        End Property

        <XmlElement("seasonfanarts_maxheight")>
        Public Property SeasonFanarts_MaxHeight() As Integer
            Get
                Return _seasonfanarts_maxheight
            End Get
            Set(ByVal value As Integer)
                _seasonfanarts_maxheight = value
            End Set
        End Property

        <XmlElement("seasonfanarts_maxwidth")>
        Public Property SeasonFanarts_MaxWidth() As Integer
            Get
                Return _seasonfanarts_maxwidth
            End Get
            Set(ByVal value As Integer)
                _seasonfanarts_maxwidth = value
            End Set
        End Property

        <XmlElement("seasonlandscapes")>
        Public Property SeasonLandscapes() As Boolean
            Get
                Return _seasonlandscapes
            End Get
            Set(ByVal value As Boolean)
                _seasonlandscapes = value
            End Set
        End Property

        <XmlElement("seasonlandscapes_maxheight")>
        Public Property SeasonLandscapes_MaxHeight() As Integer
            Get
                Return _seasonlandscapes_maxheight
            End Get
            Set(ByVal value As Integer)
                _seasonlandscapes_maxheight = value
            End Set
        End Property

        <XmlElement("seasonlandscapes_maxwidth")>
        Public Property SeasonLandscapes_MaxWidth() As Integer
            Get
                Return _seasonlandscapes_maxwidth
            End Get
            Set(ByVal value As Integer)
                _seasonlandscapes_maxwidth = value
            End Set
        End Property

        <XmlElement("seasonposters")>
        Public Property SeasonPosters() As Boolean
            Get
                Return _seasonposters
            End Get
            Set(ByVal value As Boolean)
                _seasonposters = value
            End Set
        End Property

        <XmlElement("seasonposters_maxheight")>
        Public Property SeasonPosters_MaxHeight() As Integer
            Get
                Return _seasonposters_maxheight
            End Get
            Set(ByVal value As Integer)
                _seasonposters_maxheight = value
            End Set
        End Property

        <XmlElement("seasonposters_maxwidth")>
        Public Property SeasonPosters_MaxWidth() As Integer
            Get
                Return _seasonposters_maxwidth
            End Get
            Set(ByVal value As Integer)
                _seasonposters_maxwidth = value
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
            _episodefanarts = False
            _episodefanarts_maxheight = -1
            _episodefanarts_maxwidth = -1
            _episodeposters = False
            _episodeposters_maxheight = -1
            _episodeposters_maxwidth = -1
            _flags = False
            _mainbanners = False
            _mainbanners_maxheight = -1
            _mainbanners_maxwidth = -1
            _maincharacterarts = False
            _maincleararts = False
            _mainclearlogos = False
            _maindiscarts = False
            _mainfanarts = False
            _mainfanarts_maxheight = -1
            _mainfanarts_maxwidth = -1
            _mainlandscapes = False
            _mainlandscapes_maxheight = -1
            _mainlandscapes_maxwidth = -1
            _mainposters = False
            _mainposters_maxheight = -1
            _mainposters_maxwidth = -1
            _seasonbanners = False
            _seasonbanners_maxheight = -1
            _seasonbanners_maxwidth = -1
            _seasonfanarts = False
            _seasonfanarts_maxheight = -1
            _seasonfanarts_maxwidth = -1
            _seasonlandscapes = False
            _seasonlandscapes_maxheight = -1
            _seasonlandscapes_maxwidth = -1
            _seasonposters = False
            _seasonposters_maxheight = -1
            _seasonposters_maxwidth = -1
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class