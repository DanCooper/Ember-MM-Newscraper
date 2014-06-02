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
Imports EmberAPI
Imports NLog

Public Class dlgExportMovies

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwSaveAll As New System.ComponentModel.BackgroundWorker

    Private base_template As String
    Private bCancelled As Boolean = False
    Private bexportFanart As Boolean = False
    Private bexportFlags As Boolean = False
    Private bexportPosters As Boolean = False
    Private bFiltered As Boolean = False
    Private DontSaveExtra As Boolean = False
    Dim FilterMovies As New List(Of Long)
    Private HTMLBody As New StringBuilder
    Private isCL As Boolean = False
    Private TempPath As String = Path.Combine(Master.TempPath, "Export")
    Private use_filter As Boolean = False
    Private workerCanceled As Boolean = False
    Private _movies As New List(Of Structures.DBMovie)

    Private AllTVShowList As String = ""
#End Region 'Fields

#Region "Methods"

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
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
                CopyDirectory(SubDir.FullName, Path.Combine(DestDir.FullName, _
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

    Private Sub BuildHTML(ByVal bSearch As Boolean, ByVal strFilter As String, ByVal strIn As String, ByVal template As String, ByVal doNavigate As Boolean)
        Try
            ' Build HTML Documment in Code ... ugly but will work until new option

            HTMLBody.Length = 0

            Me.bexportPosters = False
            Me.bexportFanart = False
            Me.bexportFlags = False

            Dim AllMovieSetList As String = ""
            Try
                AllMovieSetList = GetAllMovieSets()
            Catch ex As Exception

            End Try

            Dim tVid As New MediaInfo.Video
            Dim tAud As New MediaInfo.Audio
            Dim tRes As String = String.Empty
            Dim htmlPath As String = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html", Path.DirectorySeparatorChar, template, Path.DirectorySeparatorChar, Master.eSettings.GeneralLanguage, ".html")
            Dim pattern As String
            If Not File.Exists(htmlPath) Then
                htmlPath = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html", Path.DirectorySeparatorChar, template, Path.DirectorySeparatorChar, "English_(en_US).html")
            End If
            If Not File.Exists(htmlPath) Then
                Return
            End If
            pattern = File.ReadAllText(htmlPath)
            If pattern.Contains("<$NEED_POSTERS>") Then
                Me.bexportPosters = True

                'cocotus, 2013/02 Export HTML expanded: configurable resizable images
                '++++++POSTER++++++++
                strPosterSize = AdvancedSettings.GetSetting("ExportPosterHeight", "300")

                'now consider the user selection
                If strPosterSize <> "" Then
                    If strPosterSize = "300" Then
                        PosterSize.Width = 200
                        PosterSize.Height = 300
                    ElseIf strPosterSize = "400" Then
                        PosterSize.Width = 267
                        PosterSize.Height = 400
                    ElseIf strPosterSize = "600" Then
                        PosterSize.Width = 400
                        PosterSize.Height = 600
                    ElseIf strPosterSize = "800" Then
                        PosterSize.Width = 533
                        PosterSize.Height = 800
                    ElseIf strPosterSize = "original" Then
                        PosterSize.Width = 1
                        PosterSize.Height = 1
                    Else
                        PosterSize.Width = 200
                        PosterSize.Height = 300
                    End If
                End If

                'cocotus end
                pattern = pattern.Replace("<$NEED_POSTERS>", String.Empty)
            End If
            If pattern.Contains("<$NEED_FANART>") Then
                Me.bexportFanart = True
                'cocotus, 2013/02 Export HTML expanded: configurable resizable images
                '++++++Fanart++++++++
                strFanartSize = AdvancedSettings.GetSetting("ExportFanartWidth", "800")

                'now consider the user selection
                If strFanartSize <> "" Then
                    If strFanartSize = "400" Then
                        FanartSize.Width = 400
                        FanartSize.Height = 225
                    ElseIf strFanartSize = "600" Then
                        FanartSize.Width = 600
                        FanartSize.Height = 338
                    ElseIf strFanartSize = "800" Then
                        FanartSize.Width = 800
                        FanartSize.Height = 450
                    ElseIf strFanartSize = "1200" Then
                        FanartSize.Width = 1200
                        FanartSize.Height = 675
                    ElseIf strFanartSize = "original" Then
                        FanartSize.Width = 1
                        FanartSize.Height = 1
                    Else
                        FanartSize.Width = 800
                        FanartSize.Height = 450
                    End If
                End If
                'cocotus end
                pattern = pattern.Replace("<$NEED_FANART>", String.Empty)
            End If
            If pattern.Contains("<$NEED_FLAGS>") Then
                Me.bexportFlags = True
                pattern = pattern.Replace("<$NEED_FLAGS>", String.Empty)
            End If

            If bSearch Then
                bFiltered = True
            Else
                bFiltered = False
            End If

            'here we transform the pattern string into a content map
            Dim contentMap As New List(Of ContentMapPart)
            Dim s As Integer
            Dim e As Integer
            Dim part As ContentMapPart
            Do While pattern.IndexOf("<$MOVIE>") >= 0
                'create the non-looped section
                s = pattern.IndexOf("<$MOVIE>")
                part = New ContentMapPart
                part.isLooped = False
                part.content = pattern.Substring(0, s)
                contentMap.Add(part)
                'create the looped section
                e = pattern.IndexOf("<$/MOVIE>")
                part = New ContentMapPart
                part.isLooped = True
                part.content = pattern.Substring(s + 8, e - s - 8)
                contentMap.Add(part)
                ''truncate it to after the looped section
                pattern = pattern.Substring(e + 9)
            Loop
            part = New ContentMapPart
            part.content = pattern
            part.isLooped = False
            contentMap.Add(part)
            'the contentmap is now a list of parts indicating if they loop, and containing their content

            For Each part In contentMap
                If part.isLooped Then
                    Dim counter As Integer = 1
                    FilterMovies.Clear()
                    For Each _curMovie As Structures.DBMovie In _movies
                        Dim _vidDetails As String = String.Empty
                        Dim _vidDimensions As String = String.Empty

                        Dim row As String = part.content

                        'cocotus, 2013/02 Added support for new MediaInfo-fields
                        'We want to use new mediainfo fields in template to -> make them avalaible here!
                        Dim _vidBitrate As String = String.Empty
                        Dim _vidEncodedSettings As String = String.Empty
                        Dim _vidMultiView As String = String.Empty
                        Dim _audBitrate As String = String.Empty
                        'cocotus end

                        Dim _audDetails As String = String.Empty
                        If Not IsNothing(_curMovie.Movie.FileInfo) Then
                            If _curMovie.Movie.FileInfo.StreamDetails.Video.Count > 0 Then
                                tVid = NFO.GetBestVideo(_curMovie.Movie.FileInfo)
                                tRes = NFO.GetResFromDimensions(tVid)

                                'cocotus, 2013/02 Added support for new MediaInfo-fields
                                If Not String.IsNullOrEmpty(tVid.Bitrate) Then
                                    _vidBitrate = tVid.Bitrate
                                End If
                                If Not String.IsNullOrEmpty(tVid.EncodedSettings) Then
                                    _vidEncodedSettings = tVid.EncodedSettings
                                End If
                                If Not String.IsNullOrEmpty(tVid.MultiView) Then
                                    _vidMultiView = tVid.MultiView
                                End If
                                'cocotus end

                                _vidDimensions = NFO.GetDimensionsFromVideo(tVid)
                                _vidDetails = String.Format("{0} / {1}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes), If(String.IsNullOrEmpty(tVid.Codec), Master.eLang.GetString(138, "Unknown"), tVid.Codec)).ToUpper
                            End If

                            If _curMovie.Movie.FileInfo.StreamDetails.Audio.Count > 0 Then
                                tAud = NFO.GetBestAudio(_curMovie.Movie.FileInfo, False)

                                'cocotus, 2013/02 Added support for new MediaInfo-fields
                                If Not String.IsNullOrEmpty(tAud.Bitrate) Then
                                    _audBitrate = tAud.Bitrate
                                End If
                                'cocotus end

                                _audDetails = String.Format("{0} / {1}ch", If(String.IsNullOrEmpty(tAud.Codec), Master.eLang.GetString(138, "Unknown"), tAud.Codec), If(String.IsNullOrEmpty(tAud.Channels), Master.eLang.GetString(138, "Unknown"), tAud.Channels)).ToUpper
                            End If
                        End If

                        'now check if we need to include this movie
                        If bSearch Then
                            If strIn = Master.eLang.GetString(318, "Source Folder") Then
                                Dim found As Boolean = False
                                For Each u As String In strFilter.Split(Convert.ToChar(";"))
                                    If _curMovie.Source = u Then
                                        found = True
                                        Exit For
                                    End If
                                Next
                                '_curMovie.IsMark = False
                                If Not found Then Continue For
                            Else
                                If (strIn = Master.eLang.GetString(319, "Video Flag") AndAlso StringUtils.Wildcard.IsMatch(_vidDetails, strFilter)) OrElse _
                                   (strIn = Master.eLang.GetString(320, "Audio Flag") AndAlso StringUtils.Wildcard.IsMatch(_audDetails, strFilter)) OrElse _
                                   (strIn = Master.eLang.GetString(21, "Title") AndAlso StringUtils.Wildcard.IsMatch(_curMovie.Movie.Title, strFilter)) OrElse _
                                   (strIn = Master.eLang.GetString(278, "Year") AndAlso StringUtils.Wildcard.IsMatch(_curMovie.Movie.Year, strFilter)) Then
                                    'included - build the output
                                Else
                                    'filtered out - exclude this one
                                    '_curMovie.IsMark = False
                                    Continue For
                                End If
                            End If
                        End If
                        FilterMovies.Add(_curMovie.ID)
                        Dim uni As New UnicodeEncoding()

                        row = row.Replace("<$MOVIE_PATH>", String.Empty)
                        row = row.Replace("<$POSTER_FILE>", String.Concat("export/", counter.ToString, ".jpg"))
                        row = row.Replace("<$FANART_FILE>", String.Concat("export/", counter.ToString, "-fanart.jpg"))
                        If Not String.IsNullOrEmpty(_curMovie.Movie.Title) Then
                            row = row.Replace("<$MOVIENAME>", StringUtils.HtmlEncode(_curMovie.Movie.Title))
                        Else
                            row = row.Replace("<$MOVIENAME>", StringUtils.HtmlEncode(_curMovie.ListTitle))
                        End If
                        row = row.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(_curMovie.Movie.OriginalTitle))
                        row = row.Replace("<$ACTORS>", StringUtils.HtmlEncode(Functions.ListToStringWithSeparator(_curMovie.Movie.Actors, ",")))
                        row = row.Replace("<$DIRECTOR>", StringUtils.HtmlEncode(_curMovie.Movie.Director))
                        row = row.Replace("<$CERTIFICATION>", StringUtils.HtmlEncode(_curMovie.Movie.Certification))
                        row = row.Replace("<$IMDBID>", StringUtils.HtmlEncode(_curMovie.Movie.IMDBID))
                        row = row.Replace("<$MPAA>", StringUtils.HtmlEncode(_curMovie.Movie.MPAA))
                        row = row.Replace("<$RELEASEDATE>", StringUtils.HtmlEncode(_curMovie.Movie.ReleaseDate))
                        row = row.Replace("<$RUNTIME>", StringUtils.HtmlEncode(_curMovie.Movie.Runtime))
                        row = row.Replace("<$TAGLINE>", StringUtils.HtmlEncode(_curMovie.Movie.Tagline))
                        row = row.Replace("<$RATING>", StringUtils.HtmlEncode(_curMovie.Movie.Rating))
                        row = row.Replace("<$VOTES>", StringUtils.HtmlEncode(_curMovie.Movie.Votes))
                        row = row.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(_curMovie.ListTitle))
                        row = row.Replace("<$YEAR>", _curMovie.Movie.Year)
                        row = row.Replace("<$COUNTRY>", StringUtils.HtmlEncode(_curMovie.Movie.Country))
                        row = row.Replace("<$COUNT>", counter.ToString)
                        row = row.Replace("<$FILENAME>", StringUtils.HtmlEncode(Path.GetFileName(_curMovie.Filename)))
                        row = row.Replace("<$DIRNAME>", StringUtils.HtmlEncode(Path.GetDirectoryName(_curMovie.Filename)))
                        row = row.Replace("<$OUTLINE>", StringUtils.HtmlEncode(_curMovie.Movie.Outline))
                        row = row.Replace("<$PLOT>", StringUtils.HtmlEncode(_curMovie.Movie.Plot))
                        row = row.Replace("<$GENRES>", StringUtils.HtmlEncode(_curMovie.Movie.Genre))
                        row = row.Replace("<$VIDEO>", _vidDetails)
                        row = row.Replace("<$VIDEO_DIMENSIONS>", _vidDimensions)
                        row = row.Replace("<$AUDIO>", _audDetails)
                        row = row.Replace("<$SIZE>", StringUtils.HtmlEncode(MovieSize(_curMovie.Filename).ToString))
                        row = row.Replace("<$DATEADD>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(_curMovie.DateAdd).ToString("dd.MM.yyyy")))

                        'cocotus, 2013/02 Added support for new MediaInfo-fields
                        row = row.Replace("<$MOVIESETS>", StringUtils.HtmlEncode(AllMovieSetList)) 'A long string of all moviesets, seperated with ;!
                        row = row.Replace("<$SET>", StringUtils.HtmlEncode(GetMovieSets(_curMovie))) 'All sets which movie belongs to, seperated with ;!
                        row = row.Replace("<$VIDEOBITRATE>", _vidBitrate)
                        row = row.Replace("<$VIDEOMULTIVIEW>", _vidMultiView)
                        row = row.Replace("<$VIDEOENCODINGSETTINGS>", _vidEncodedSettings)
                        row = row.Replace("<$AUDIOBITRATE>", _audBitrate)
                        'Unlocking more fields to use in templates!
                        row = row.Replace("<$NOW>", System.DateTime.Now.ToLongDateString) 'Save Build Date. might be useful info!
                        row = row.Replace("<$COUNTRY>", StringUtils.HtmlEncode(_curMovie.Movie.Country))
                        row = row.Replace("<$IDMOVIEDB>", StringUtils.HtmlEncode(_curMovie.Movie.IDMovieDB))
                        row = row.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(_curMovie.Movie.OriginalTitle))
                        row = row.Replace("<$PLAYCOUNT>", StringUtils.HtmlEncode(_curMovie.Movie.PlayCount))
                        row = row.Replace("<$STUDIO>", StringUtils.HtmlEncode(_curMovie.Movie.Studio))
                        row = row.Replace("<$TOP250>", StringUtils.HtmlEncode(_curMovie.Movie.Top250))
                        row = row.Replace("<$TRAILER>", StringUtils.HtmlEncode(_curMovie.Movie.Trailer))
                        row = row.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(_curMovie.Movie.VideoSource))
                        'row = row.Replace("<$WATCHED>", StringUtils.HtmlEncode(_curMovie.Movie.Watched))
                        'cocotus end
                        row = GetAVImages(_curMovie, row)
                        HTMLBody.Append(row)
                        counter += 1
                    Next
                Else
                    HTMLBody.Append(part.content)
                End If
            Next

            If Not Me.isCL Then
                'just name it index.html
                'Dim outFile As String = Path.Combine(Me.TempPath, String.Concat(Master.eSettings.Language, ".html"))
                Dim outFile As String = Path.Combine(Me.TempPath, "index.htm")
                DontSaveExtra = False
                Me.SaveAll(If(doNavigate, Master.eLang.GetString(321, "Preparing preview. Please wait..."), String.Empty), Path.GetDirectoryName(htmlPath), outFile)
                If doNavigate Then LoadHTML()
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function GetMovieSets(_curMovie As Structures.DBMovie) As String
        Dim ReturnString As String = ""
        Try

            If _curMovie.Movie.Sets.Count > 0 Then
                For i = 0 To _curMovie.Movie.Sets.Count - 1
                    If i > 0 Then
                        ReturnString = ReturnString + "|" + _curMovie.Movie.Sets.Item(i).Set
                    Else
                        ReturnString = _curMovie.Movie.Sets.Item(i).Set
                    End If

                Next
            End If
            Return ReturnString
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return ReturnString
        End Try

    End Function
    Private alSets As New List(Of String)
    '  Private lMovies As New List(Of Movies)
    Private Function GetAllMovieSets() As String
        '//
        ' Start thread to load movie information from nfo
        '\\



        Dim ReturnString As String = ""
        Try

            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT SetName FROM MoviesSets;")
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
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return ""
        End Try
    End Function

    Private allTVShows As New List(Of String)
    '  Private lMovies As New List(Of Movies)
    Private Function GetAllTVShows() As String
        '//
        ' Start thread to load movie information from nfo
        '\\
        Dim ReturnString As String = ""
        Try

            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT * FROM TVShows ORDER BY Title COLLATE NOCASE;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            If Not allTVShows.Contains(SQLreader("Title").ToString) AndAlso Not String.IsNullOrEmpty(SQLreader("Title").ToString) Then
                                If Not String.IsNullOrEmpty(SQLreader("ID").ToString) Then
                                    allTVShows.Add(SQLreader("ID").ToString & "*" & SQLreader("Title").ToString & GetSeasonInfo(SQLreader("ID").ToString))
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
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return ""
        End Try
    End Function
    Private Function GetSeasonInfo(ByVal Id As String) As String
        Dim ReturnString As String = ""
        Try
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "id")
                SQLcommand.CommandText = "SELECT * FROM TVSeason WHERE TVShowID = (?);"
                parID.Value = Id
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()

                            If Not String.IsNullOrEmpty(SQLreader("Season").ToString) AndAlso Not SQLreader("Season").ToString.Contains("999") Then
                                If Not String.IsNullOrEmpty(SQLreader("PosterPath").ToString) Then
                                    ExportSeasonImage(SQLreader("TVShowID").ToString & "s" & SQLreader("Season").ToString, SQLreader("PosterPath").ToString, TempPath)
                                End If
                                ReturnString = ReturnString + "*" + SQLreader("TVShowID").ToString & "s" & SQLreader("Season").ToString
                            End If
                        End While
                    End If
                End Using
            End Using
            Return ReturnString
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return ReturnString
        End Try

    End Function

    Private Sub ExportSeasonImage(ByVal filename As String, ByVal source As String, ByVal temppath As String)
        Try

            Dim finalpath As String = Path.Combine(temppath, "export")
            Directory.CreateDirectory(finalpath)

            Try
                Dim posterfile As String = Path.Combine(finalpath, String.Concat(filename, ".jpg"))
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

                    If strPosterSize = "original" Then
                        File.Copy(source, posterfile, True)
                    Else
                        'Now we do some image processing to make the output file smaller!
                        Try
                            Dim img As Image
                            Dim sFileName As String = source
                            Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                            img = Image.FromStream(fs)
                            fs.Close()
                            '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                            Dim imgresized As Image = ImageUtils.ResizeImage(img, PosterSize)
                            img.Dispose()
                            '2. Step: Now use jpeg compression to make file even smaller...
                            ImageUtils.JPEGCompression(imgresized, posterfile, CInt(AdvancedSettings.GetSetting("ExportImageQuality", "70")))
                            imgresized.Dispose()

                        Catch ex As Exception
                            'The old method, used here when anything goes wrong
                            File.Copy(source, posterfile, True)
                        End Try
                    End If

                    'cocotus end

                End If

            Catch
            End Try
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub


    Private Sub bwLoadInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        '//
        ' Thread to load movieinformation (from nfo)
        '\\
        Try
            ' Clean up Movies List if any
            _movies.Clear()
            ' Load nfo movies using path from DB
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim _tmpMovie As New Structures.DBMovie
                Dim _ID As Integer
                Dim iProg As Integer = 0
                SQLNewcommand.CommandText = String.Concat("SELECT COUNT(id) AS mcount FROM movies;")
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    Me.bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
                End Using
                SQLNewcommand.CommandText = String.Concat("SELECT ID FROM movies ORDER BY SortTitle COLLATE NOCASE;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            _ID = Convert.ToInt32(SQLreader("ID"))
                            _tmpMovie = Master.DB.LoadMovieFromDB(_ID)
                            _movies.Add(_tmpMovie)
                            Me.bwLoadInfo.ReportProgress(iProg, _tmpMovie.ListTitle) '  show File
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
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadInfo_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadInfo.ProgressChanged
        If Not Me.isCL Then
            If e.ProgressPercentage >= 0 Then
                Me.pbCompile.Value = e.ProgressPercentage
                Me.lblFile.Text = e.UserState.ToString
            Else
                Me.pbCompile.Maximum = Convert.ToInt32(e.UserState)
            End If
        End If
    End Sub

    Private Sub bwLoadInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadInfo.RunWorkerCompleted
        '//
        ' Thread finished: display it if not cancelled
        '\\
        If Not Me.isCL Then
            bCancelled = e.Cancelled
            If Not e.Cancelled Then
                If Not Me.isCL Then
                    BuildHTML(False, String.Empty, String.Empty, base_template, False)
                End If
                LoadHTML()
            Else
                wbMovieList.DocumentText = String.Concat("<center><h1 style=""color:Red;"">", Master.eLang.GetString(284, "Canceled"), "</h1></center>")
            End If
            Me.pnlCancel.Visible = False
        End If
    End Sub

    Private Sub bwSaveAll_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwSaveAll.DoWork
        Try

            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Dim destPathShort As String = Path.GetDirectoryName(Args.destPath)
            'Only create extra files once for each template... dont do it when applyng filters
            If Not DontSaveExtra Then
                FileUtils.Delete.DeleteDirectory(Me.TempPath)
                CopyDirectory(Args.srcPath, destPathShort, True)
                If Me.bexportFlags Then
                    Args.srcPath = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Flags", Path.DirectorySeparatorChar)
                    Directory.CreateDirectory(Path.Combine(destPathShort, "Flags"))
                    CopyDirectory(Args.srcPath, Path.Combine(destPathShort, "Flags"), True)
                End If
                If bwSaveAll.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                If Me.bexportPosters Then
                    Me.ExportPoster(destPathShort, Args.resizePoster)
                End If
                If bwSaveAll.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                If Me.bexportFanart Then
                    Me.ExportFanart(destPathShort)
                End If
                If AdvancedSettings.GetBooleanSetting("ExportTVShows", False) = True Then
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
            If Not IsNothing(myStream) Then
                myStream.Write(System.Text.Encoding.ASCII.GetBytes(HTMLBody.ToString), 0, HTMLBody.ToString.Length)
                myStream.Close()
            End If
        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

    Private Sub Close_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Close_Button.Click
        If bwSaveAll.IsBusy Then
            bwSaveAll.CancelAsync()
        End If
        While bwSaveAll.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub dlgExportMovies_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.bwLoadInfo.IsBusy Then
            Me.DoCancel()
            While Me.bwLoadInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End If
        FileUtils.Delete.DeleteDirectory(Me.TempPath)
    End Sub

    Private Sub dlgExportMovies_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
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

        Me.Activate()

        'Start worker
        Me.bwLoadInfo = New System.ComponentModel.BackgroundWorker
        Me.bwLoadInfo.WorkerSupportsCancellation = True
        Me.bwLoadInfo.WorkerReportsProgress = True
        Me.bwLoadInfo.RunWorkerAsync()
    End Sub

    Private Sub DoCancel()
        Me.bwLoadInfo.CancelAsync()
        btnCancel.Visible = False
        lblCompiling.Visible = False
        pbCompile.Style = ProgressBarStyle.Marquee
        pbCompile.MarqueeAnimationSpeed = 25
        lblCanceling.Visible = True
        lblFile.Visible = False
    End Sub

    Private Sub ExportFanart(ByVal fpath As String)
        Try
            Dim counter As Integer = 1
            Dim finalpath As String = Path.Combine(fpath, "export")
            Directory.CreateDirectory(finalpath)

            For Each _curMovie As Structures.DBMovie In _movies.Where(Function(y) FilterMovies.Contains(y.ID))
                Try
                    Dim fanartfile As String = Path.Combine(finalpath, String.Concat(counter.ToString, "-fanart.jpg"))
                    If File.Exists(_curMovie.FanartPath) Then

                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images

                        'old method, just copy/no image conversion
                        'File.Copy(_curMovie.FanartPath, fanartfile, True)

                        If strFanartSize = "original" Then
                            File.Copy(_curMovie.FanartPath, fanartfile, True)
                        Else
                            'Now we do some image processing to make the output file smaller!
                            Try
                                Dim img As Image
                                Dim sFileName As String = _curMovie.FanartPath
                                Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                                img = Image.FromStream(fs)
                                fs.Close()
                                '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                                Dim imgresized As Image = ImageUtils.ResizeImage(img, FanartSize)
                                img.Dispose()
                                '2. Step: Now use jpeg compression to make file even smaller...
                                ImageUtils.JPEGCompression(imgresized, fanartfile, CInt(AdvancedSettings.GetSetting("ExportImageQuality", "70")))
                                imgresized.Dispose()

                            Catch ex As Exception
                                'The old method, used here when anything goes wrong
                                File.Copy(_curMovie.FanartPath, fanartfile, True)
                            End Try
                        End If
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
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ExportPoster(ByVal fpath As String, ByVal new_width As Integer)
        Try
            Dim counter As Integer = 1
            Dim finalpath As String = Path.Combine(fpath, "export")
            Directory.CreateDirectory(finalpath)
            For Each _curMovie As Structures.DBMovie In _movies.Where(Function(y) FilterMovies.Contains(y.ID))
                Try
                    Dim posterfile As String = Path.Combine(finalpath, String.Concat(counter.ToString, ".jpg"))
                    If File.Exists(_curMovie.PosterPath) Then                        'cocotus, 2013/02 Export HTML expanded: configurable resizable images
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

                        If strPosterSize = "original" Then
                            File.Copy(_curMovie.PosterPath, posterfile, True)
                        Else
                            'Now we do some image processing to make the output file smaller!
                            Try
                                Dim img As Image
                                Dim sFileName As String = _curMovie.PosterPath
                                Dim fs As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
                                img = Image.FromStream(fs)
                                fs.Close()
                                '1. Step: Resizing, Image method needs Size and IMAGE object we just created, so now we can start!
                                Dim imgresized As Image = ImageUtils.ResizeImage(img, PosterSize)
                                img.Dispose()
                                '2. Step: Now use jpeg compression to make file even smaller...
                                ImageUtils.JPEGCompression(imgresized, posterfile, CInt(AdvancedSettings.GetSetting("ExportImageQuality", "70")))
                                imgresized.Dispose()

                            Catch ex As Exception
                                'The old method, used here when anything goes wrong
                                File.Copy(_curMovie.PosterPath, posterfile, True)
                            End Try
                        End If

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
            Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function GetAVImages(ByVal AVMovie As Structures.DBMovie, ByVal line As String) As String
        If APIXML.lFlags.Count > 0 Then
            Try
                Dim fiAV As MediaInfo.Fileinfo = AVMovie.Movie.FileInfo
                Dim tVideo As MediaInfo.Video = NFO.GetBestVideo(fiAV)
                Dim tAudio As MediaInfo.Audio = NFO.GetBestAudio(fiAV, False)

                Dim vresFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = NFO.GetResFromDimensions(tVideo).ToLower AndAlso f.Type = APIXML.FlagType.VideoResolution)
                If Not IsNothing(vresFlag) Then
                    line = line.Replace("<$FLAG_VRES>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
                Else
                    vresFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoResolution)
                    If Not IsNothing(vresFlag) Then
                        line = line.Replace("<$FLAG_VRES>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
                    End If
                End If

                'Dim vsourceFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = APIXML.GetFileSource(AVMovie.Filename) AndAlso f.Type = APIXML.FlagType.VideoSource)
                Dim vsourceFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name.ToLower = AVMovie.FileSource AndAlso f.Type = APIXML.FlagType.VideoSource)
                If Not IsNothing(vsourceFlag) Then
                    line = line.Replace("<$FLAG_VSOURCE>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
                Else
                    vsourceFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoSource)
                    If Not IsNothing(vsourceFlag) Then
                        line = line.Replace("<$FLAG_VSOURCE>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
                    End If
                End If

                Dim vcodecFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = tVideo.Codec.ToLower AndAlso f.Type = APIXML.FlagType.VideoCodec)
                If Not IsNothing(vcodecFlag) Then
                    line = line.Replace("<$FLAG_VTYPE>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
                Else
                    vcodecFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoCodec)
                    If Not IsNothing(vcodecFlag) Then
                        line = line.Replace("<$FLAG_VTYPE>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
                    End If
                End If

                Dim acodecFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = tAudio.Codec.ToLower AndAlso f.Type = APIXML.FlagType.AudioCodec)
                If Not IsNothing(acodecFlag) Then
                    line = line.Replace("<$FLAG_ATYPE>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
                Else
                    acodecFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = APIXML.FlagType.AudioCodec)
                    If Not IsNothing(acodecFlag) Then
                        line = line.Replace("<$FLAG_ATYPE>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
                    End If
                End If

                Dim achanFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = tAudio.Channels AndAlso f.Type = APIXML.FlagType.AudioChan)
                If Not IsNothing(achanFlag) Then
                    line = line.Replace("<$FLAG_ACHAN>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
                Else
                    achanFlag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = APIXML.FlagType.AudioChan)
                    If Not IsNothing(achanFlag) Then
                        line = line.Replace("<$FLAG_ACHAN>", String.Concat("Flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
                    End If
                End If

            Catch ex As Exception
                Logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try
        End If
        Return line
    End Function

    Private Sub LoadHTML()
        Warning(True, Master.eLang.GetString(326, "Loading. Please wait..."))
        '   Dim tmphtml As String = Path.Combine(Me.TempPath, String.Concat(Master.eSettings.Language, ".html"))
        Dim tmphtml As String = Path.Combine(Me.TempPath, "index.htm")
        Try
            wbMovieList.Navigate(tmphtml)
            Save_Button.Enabled = True
        Catch ex As Exception
            Save_Button.Enabled = False
        End Try
    End Sub

    Function MovieSize(ByVal spath As String) As Long
        Dim MovieFilesSize As Long = 0
        If StringUtils.IsStacked(Path.GetFileNameWithoutExtension(spath), True) OrElse FileUtils.Common.isVideoTS(spath) OrElse FileUtils.Common.isBDRip(spath) Then
            Try
                Dim sExt As String = Path.GetExtension(spath).ToLower
                Dim oFile As String = StringUtils.CleanStackingMarkers(spath, False)
                Dim sFile As New List(Of String)
                Dim bIsVTS As Boolean = False

                If sExt = ".ifo" OrElse sExt = ".bup" OrElse sExt = ".vob" Then
                    bIsVTS = True
                End If

                If bIsVTS Then
                    Try
                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(spath).FullName, "VTS*.VOB"))
                    Catch
                    End Try
                ElseIf sExt = ".m2ts" Then
                    Try
                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(spath).FullName, "*.m2ts"))
                    Catch
                    End Try
                Else
                    Try
                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(spath).FullName, StringUtils.CleanStackingMarkers(Path.GetFileName(spath), True)))
                    Catch
                    End Try
                End If

                For Each File As String In sFile
                    MovieFilesSize += FileLen(File)
                Next
            Catch ex As Exception
            End Try
        End If
        Return MovieFilesSize
    End Function

    Private Sub SaveAll(ByVal sWarning As String, ByVal srcPath As String, ByVal destPath As String, Optional ByVal resizePoster As Integer = 200)


        wbMovieList.Visible = False
        If Not String.IsNullOrEmpty(sWarning) Then Warning(True, sWarning)
        cbo_SelectedFilter.Enabled = False
        cbTemplate.Enabled = False
        btn_BuildHTML.Enabled = False
        Save_Button.Enabled = False
        Me.bwSaveAll = New System.ComponentModel.BackgroundWorker
        Me.bwSaveAll.WorkerReportsProgress = True
        Me.bwSaveAll.WorkerSupportsCancellation = True
        Me.bwSaveAll.RunWorkerAsync(New Arguments With {.srcPath = srcPath, .destPath = destPath, .resizePoster = resizePoster})
        While bwSaveAll.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        cbo_SelectedFilter.Enabled = True
        cbTemplate.Enabled = True
        btn_BuildHTML.Enabled = True
        Save_Button.Enabled = True
        If pnlCancel.Visible Then Warning(False)
        If Not workerCanceled Then
            wbMovieList.Visible = True
        End If
    End Sub

    Private Sub Save_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save_Button.Click
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
        If IO.Directory.Exists(AdvancedSettings.GetSetting("ExportPath", "")) Then
            CopyDirectory(TempPath, AdvancedSettings.GetSetting("ExportPath", ""), True)
            Save_Button.Enabled = False
            MessageBox.Show((Master.eLang.GetString(1003, "Template saved to:") & AdvancedSettings.GetSetting("ExportPath", "")), Master.eLang.GetString(361, "Finished!"), MessageBoxButtons.OK)
        Else
            Save_Button.Enabled = False
            MessageBox.Show((Master.eLang.GetString(221, "Export Path is not valid:") & AdvancedSettings.GetSetting("ExportPath", "")), Master.eLang.GetString(816, "An Error Has Occurred"), MessageBoxButtons.OK)
        End If
    End Sub


    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(328, "Export Movies")
        Me.Save_Button.Text = Master.eLang.GetString(273, "Save")
        Me.Close_Button.Text = Master.eLang.GetString(19, "Close")

        Me.lblCompiling.Text = Master.eLang.GetString(177, "Compiling Movie List...")
        Me.lblCanceling.Text = Master.eLang.GetString(178, "Canceling Compilation...")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.Label2.Text = Master.eLang.GetString(334, "Template")
        Me.btn_BuildHTML.Text = Master.eLang.GetString(1004, "Generate HTML...")
        Save_Button.Enabled = False

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

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbMovieList.DocumentCompleted
        If Not bCancelled Then
            If Not cbTemplate.Text = String.Empty Then Me.Save_Button.Enabled = True
        End If
        Warning(False)
    End Sub

    Private Sub btn_BuildHTML_Click(sender As Object, e As EventArgs) Handles btn_BuildHTML.Click
        If cbo_SelectedFilter.Text = "Filter 1" Then
            strFilter = AdvancedSettings.GetSetting("ExportFilter1", "")
        ElseIf cbo_SelectedFilter.Text = "Filter 2" Then
            strFilter = AdvancedSettings.GetSetting("ExportFilter2", "")
        ElseIf cbo_SelectedFilter.Text = "Filter 3" Then
            strFilter = AdvancedSettings.GetSetting("ExportFilter3", "")
        Else
            strFilter = ""
        End If
        If Not strFilter = "" Then
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
    'cocotus, 2013/02 Export HTML expanded: configurable resizable images
    'contains selected poster/fanart sizes
    Dim strFanartSize As String = ""
    Dim strPosterSize As String = ""

    Dim strFilter As String = ""
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

    Private Structure ContentMapPart
        Dim isLooped As Boolean
        Dim content As String
    End Structure

#End Region 'Nested Types

End Class