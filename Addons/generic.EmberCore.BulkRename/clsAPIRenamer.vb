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
Imports EmberAPI
Imports NLog

Public Class FileFolderRenamer

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public MovieFolders As New List(Of String)
    Public TVShowFolders As New List(Of String)

    Private _episodes As New List(Of FileRename)
    Private _movies As New List(Of FileRename)

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Flags")

        _movies.Clear()
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Concat("SELECT strPath FROM moviesource;")
            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                While SQLReader.Read
                    MovieFolders.Add(If(SQLReader("strPath").ToString.EndsWith(Path.DirectorySeparatorChar), SQLReader("strPath").ToString, String.Concat(SQLReader("strPath").ToString, Path.DirectorySeparatorChar)))
                End While
            End Using
        End Using

        'put them in order so when we're checking for basepath the last one used will be the longest one
        'case:
        'Source 1 = C:/Movies/BluRay/FullRips
        'Source 2 = C:/Movies/BluRay
        'stupid to add sources this way, but possible
        MovieFolders.Sort()

        _episodes.Clear()
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Concat("SELECT strPath FROM tvshowsource;")
            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                While SQLReader.Read
                    TVShowFolders.Add(If(SQLReader("strPath").ToString.EndsWith(Path.DirectorySeparatorChar), SQLReader("strPath").ToString, String.Concat(SQLReader("strPath").ToString, Path.DirectorySeparatorChar)))
                End While
            End Using
        End Using

        'put them in order so when we're checking for basepath the last one used will be the longest one
        'case:
        'Source 1 = C:/Movies/BluRay/FullRips
        'Source 2 = C:/Movies/BluRay
        'stupid to add sources this way, but possible
        MovieFolders.Sort()
        TVShowFolders.Sort()
    End Sub

#End Region 'Constructors

#Region "Delegates"

    Public Delegate Function ShowProgress(ByVal movie As String, ByVal iProgress As Integer) As Boolean

#End Region 'Delegates

#Region "Methods"

    Public Sub Add_Movie(ByVal _movie As FileRename)
        _movies.Add(_movie)
    End Sub

    Public Sub Add_TVEpisode(ByVal _episode As FileRename)
        _episodes.Add(_episode)
    End Sub

    Private Shared Function ApplyPattern(ByVal pattern As String, ByVal flag As String, ByVal v As String) As String
        pattern = pattern.Replace(String.Concat("$", flag), v)
        If Not v = String.Empty Then
            pattern = pattern.Replace(String.Concat("$-", flag), v)
            pattern = pattern.Replace(String.Concat("$+", flag), v)
            pattern = pattern.Replace(String.Concat("$^", flag), v)

        Else
            Dim pos = -1
            Dim size = 3
            Dim nextC = pattern.IndexOf(String.Concat("$+", flag))
            If nextC >= 0 Then
                If nextC + 3 < pattern.Length Then size += 1
                pos = nextC
            End If
            Dim prevC = pattern.IndexOf(String.Concat("$-", flag))
            If prevC >= 0 Then
                If prevC + 3 < pattern.Length Then size += 1
                If prevC > 0 Then
                    prevC -= 1
                End If
                pos = prevC
            End If
            Dim bothC = pattern.IndexOf(String.Concat("$^", flag))
            If bothC >= 0 Then
                If bothC + 3 < pattern.Length Then size += 1
                If bothC > 0 Then
                    size += 1
                    bothC -= 1
                End If
                pos = bothC
            End If

            If Not pos = -1 Then pattern = pattern.Remove(pos, size)
        End If
        Return pattern
    End Function

    Public Sub DoRename_Movies(Optional ByVal sfunction As ShowProgress = Nothing)
        Dim _movieDB As Database.DBElement = Nothing
        Dim iProg As Integer = 0
        Try
            For Each f As FileFolderRenamer.FileRename In _movies.Where(Function(s) s.DoRename AndAlso Not s.FileExist AndAlso Not s.IsLock)
                iProg += 1
                If Not f.ID = -1 Then
                    _movieDB = Master.DB.Load_Movie(f.ID)
                    DoRenameSingle_Movie(f, _movieDB, True, False, True, sfunction, iProg)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub DoRename_TVEpisodes(Optional ByVal sfunction As ShowProgress = Nothing)
        Dim _tvDB As Database.DBElement = Nothing
        Dim iProg As Integer = 0
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each f As FileFolderRenamer.FileRename In _episodes.Where(Function(s) s.DoRename AndAlso Not s.FileExist AndAlso Not s.IsLock)
                    iProg += 1
                    If Not f.ID = -1 Then
                        _tvDB = Master.DB.Load_TVEpisode(f.ID, True)
                        DoRenameSingle_TVEpisode(f, _tvDB, True, False, True, sfunction, iProg)
                    End If
                Next
                SQLtransaction.Commit()
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub DoRenameSingle_Movie(ByVal _frename As FileRename, ByRef _movie As Database.DBElement, ByVal BatchMode As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean, Optional ByVal sfunction As ShowProgress = Nothing, Optional ByVal iProg As Integer = 0)
        logger.Trace(String.Format("[{0}] [{1}] [Start]", Reflection.MethodBase.GetCurrentMethod.ReflectedType, Reflection.MethodBase.GetCurrentMethod.Name))
        Try
            If Not _movie.IsLock AndAlso Not _frename.FileExist AndAlso Not _frename.DirExist Then
                Dim getError As Boolean = False
                Dim srcDir As String = Path.Combine(_frename.BasePath, _frename.Path)
                Dim destDir As String = Path.Combine(_frename.BasePath, _frename.NewPath)
                Dim srcFilenamePath As String = Path.Combine(_frename.BasePath, _frename.Path, _frename.OldFileName)
                Dim dstFilenamePath As String = Path.Combine(_frename.BasePath, _frename.NewPath, _frename.NewFileName)

                'Rename Directory
                If Not srcDir = destDir Then
                    Try
                        If sfunction IsNot Nothing Then
                            If Not sfunction(_frename.NewPath, iProg) Then Return
                        End If

                        If Not _movie.IsSingle Then
                            Directory.CreateDirectory(destDir)
                        Else
                            If srcDir.ToLower = destDir.ToLower Then
                                Directory.Move(srcDir, String.Concat(destDir, ".$emm"))
                                Directory.Move(String.Concat(destDir, ".$emm"), destDir)
                            Else
                                If Not Directory.Exists(Directory.GetParent(destDir).FullName) Then Directory.CreateDirectory(Directory.GetParent(destDir).FullName)
                                Directory.Move(srcDir, destDir)
                            End If
                        End If
                    Catch ex As Exception
                        If ShowError Then
                            MessageBox.Show(String.Format(Master.eLang.GetString(144, "An error occured while attempting to rename the directory:{0}{0}{1}{0}{0}Please ensure that you are not accessing this directory or any of its files from another program (including browsing via Windows Explorer)."), Environment.NewLine, ex.Message), Master.eLang.GetString(165, "Unable to Rename Directory"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            logger.Error("Dir: <{0}> - <{1}>", srcDir, destDir)
                        End If
                        getError = True
                    End Try

                End If

                'Rename Files
                If Not getError AndAlso Not _frename.IsVideoTS AndAlso Not _frename.IsBDMV Then
                    If (Not _frename.NewFileName = _frename.OldFileName) OrElse (_frename.Path = String.Empty AndAlso Not _frename.NewPath = String.Empty) OrElse Not _movie.IsSingle Then
                        Dim di As DirectoryInfo

                        If _frename.IsSingle Then
                            di = New DirectoryInfo(destDir)
                        Else
                            di = New DirectoryInfo(srcDir)
                        End If

                        Dim lFi As New List(Of FileInfo)
                        Try
                            lFi.AddRange(di.GetFiles())
                        Catch
                        End Try
                        If lFi.Count > 0 Then
                            Dim srcFile As String
                            Dim dstFile As String
                            For Each lFile As FileInfo In lFi.OrderBy(Function(s) s.Name)
                                srcFile = lFile.FullName
                                dstFile = Path.Combine(destDir, lFile.Name.Replace(_frename.OldFileName.Trim, _frename.NewFileName.Trim))
                                If Not srcFile = dstFile Then
                                    Try
                                        If sfunction IsNot Nothing Then
                                            If Not sfunction(_frename.NewFileName, iProg) Then Return
                                        End If

                                        If srcFile.ToLower = dstFile.ToLower Then
                                            File.Move(srcFile, String.Concat(dstFile, ".$emm$"))
                                            File.Move(String.Concat(dstFile, ".$emm$"), dstFile)
                                        Else
                                            If lFile.Name.StartsWith(_frename.OldFileName, StringComparison.OrdinalIgnoreCase) Then
                                                File.Move(srcFile, dstFile)
                                            End If
                                        End If

                                    Catch ex As Exception
                                        If ShowError Then
                                            MessageBox.Show(String.Format(Master.eLang.GetString(166, "An error occured while attempting to rename a file:{0}{0}{1}{0}{0}Please ensure that you are not accessing this file from another program."), Environment.NewLine, ex.Message), Master.eLang.GetString(171, "Unable to Rename File"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Else
                                            logger.Error("File <{0}> - <{1}>", srcFile, dstFile)
                                        End If
                                        getError = True
                                    End Try
                                End If
                            Next
                        End If
                    End If
                End If

                If Not getError Then
                    UpdatePaths_Movie(_movie, srcDir, destDir, _frename.OldFileName, _frename.NewFileName)

                    If toDB Then
                        Master.DB.Save_Movie(_movie, BatchMode, False, False, False)
                    End If

                    If Not _frename.IsSingle Then
                        Dim fileCount As Integer = 0
                        Dim dirCount As Integer = 0

                        If Directory.Exists(srcDir) Then
                            Dim di As DirectoryInfo = New DirectoryInfo(srcDir)

                            Try
                                fileCount = di.GetFiles().Count
                            Catch
                            End Try

                            Try
                                dirCount = di.GetDirectories().Count
                            Catch
                            End Try

                            If fileCount = 0 AndAlso dirCount = 0 Then
                                di.Delete()
                            End If
                        End If
                    End If
                End If
            Else
                Dim strErrorMessage As String = String.Format("{0}{2}{2}{1}{2}{2}{3}: {4}{2}{5}: {6}{2}{7}: {8}",
                                                              Master.eLang.GetString(171, "Unable to Rename File"),
                                                              _frename.OldFullFileName,
                                                              Environment.NewLine,
                                                              Master.eLang.GetString(43, "Locked"),
                                                              _frename.IsLock.ToString,
                                                              Master.eLang.GetString(1084, "File already exists"),
                                                              _frename.FileExist.ToString,
                                                              Master.eLang.GetString(1085, "Directory already exists or is not empty"),
                                                              _frename.DirExist.ToString)
                If ShowError Then
                    'MessageBox.Show(Master.eLang.GetString(171, "Unable to Rename File"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MessageBox.Show(strErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                logger.Error(String.Format("[{0}] [{1}] [{2}]", Reflection.MethodBase.GetCurrentMethod.ReflectedType, Reflection.MethodBase.GetCurrentMethod.Name, strErrorMessage.Replace(Environment.NewLine, "; ").Replace("; ;", ";")))
            End If
            logger.Trace(String.Format("[{0}] [{1}] [Done]", Reflection.MethodBase.GetCurrentMethod.ReflectedType, Reflection.MethodBase.GetCurrentMethod.Name))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            logger.Trace(String.Format("[{0}] [{1}] [Abort]", Reflection.MethodBase.GetCurrentMethod.ReflectedType, Reflection.MethodBase.GetCurrentMethod.Name))
        End Try
    End Sub

    Private Shared Sub DoRenameSingle_TVEpisode(ByVal _frename As FileRename, ByRef _tv As Database.DBElement, ByVal BatchMode As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean, Optional ByVal sfunction As ShowProgress = Nothing, Optional ByVal iProg As Integer = 0)
        Try
            If Not _tv.IsLock AndAlso Not _frename.FileExist Then
                Dim getError As Boolean = False
                Dim srcDir As String = Path.Combine(_frename.BasePath, _frename.Path)
                Dim destDir As String = Path.Combine(_frename.BasePath, _frename.NewPath)
                Dim srcFilenamePath As String = Path.Combine(_frename.BasePath, _frename.Path, _frename.OldFileName)
                Dim dstFilenamePath As String = Path.Combine(_frename.BasePath, _frename.NewPath, _frename.NewFileName)

                'Rename/Create Directory
                If Not srcDir = destDir Then
                    Try
                        If sfunction IsNot Nothing Then
                            If Not sfunction(_frename.NewPath, iProg) Then Return
                        End If

                        If srcDir.ToLower = destDir.ToLower Then
                            Directory.Move(srcDir, String.Concat(destDir, ".$emm"))
                            Directory.Move(String.Concat(destDir, ".$emm"), destDir)
                        Else
                            If Not Directory.Exists(destDir) Then Directory.CreateDirectory(destDir)
                        End If

                        'copy actor thumbs folder
                        If Directory.Exists(Path.Combine(srcDir, ".actors")) Then
                            FileUtils.Common.DirectoryCopy(Path.Combine(srcDir, ".actors"), Path.Combine(destDir, ".actors"), True, True)
                        End If
                    Catch ex As Exception
                        If ShowError Then
                            MessageBox.Show(String.Format(Master.eLang.GetString(144, "An error occured while attempting to rename the directory:{0}{0}{1}{0}{0}Please ensure that you are not accessing this directory or any of its files from another program (including browsing via Windows Explorer)."), Environment.NewLine, ex.Message), Master.eLang.GetString(165, "Unable to Rename Directory"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            logger.Error("Dir: <{0}> - <{1}>", srcDir, destDir)
                        End If
                        getError = True
                    End Try
                End If

                'Rename Files
                If Not _frename.IsVideoTS AndAlso Not _frename.IsBDMV Then
                    If (Not srcFilenamePath = dstFilenamePath) OrElse (_frename.Path = String.Empty AndAlso Not _frename.NewPath = String.Empty) Then 'OrElse Not _tv.IsSingle Then
                        Dim di As DirectoryInfo

                        di = New DirectoryInfo(srcDir)

                        Dim lFi As New List(Of FileInfo)

                        Try
                            lFi.AddRange(di.GetFiles())
                        Catch
                        End Try
                        If lFi.Count > 0 Then
                            Dim srcFile As String
                            Dim dstFile As String
                            For Each lFile As FileInfo In lFi.OrderBy(Function(s) s.Name)
                                srcFile = lFile.FullName
                                dstFile = Path.Combine(destDir, lFile.Name.Replace(_frename.OldFileName.Trim, _frename.NewFileName.Trim))
                                If Not srcFile = dstFile Then
                                    Try
                                        If sfunction IsNot Nothing Then
                                            If Not sfunction(_frename.NewFileName, iProg) Then Return
                                        End If

                                        If srcFile.ToLower = dstFile.ToLower Then
                                            File.Move(srcFile, String.Concat(dstFile, ".$emm$"))
                                            File.Move(String.Concat(dstFile, ".$emm$"), dstFile)
                                        Else
                                            If lFile.Name.StartsWith(_frename.OldFileName, StringComparison.OrdinalIgnoreCase) Then
                                                File.Move(srcFile, dstFile)
                                            End If
                                        End If

                                    Catch ex As Exception
                                        If ShowError Then
                                            MessageBox.Show(String.Format(Master.eLang.GetString(166, "An error occured while attempting to rename a file:{0}{0}{1}{0}{0}Please ensure that you are not accessing this file from another program."), Environment.NewLine, ex.Message), Master.eLang.GetString(171, "Unable to Rename File"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Else
                                            logger.Error("File <{0}> - <{1}>", srcFile, dstFile)
                                        End If
                                        getError = True
                                    End Try
                                End If
                            Next
                        End If
                    End If
                End If

                If Not getError Then
                    UpdatePaths_TVEpisode(_tv, srcDir, destDir, _frename.OldFileName, _frename.NewFileName)

                    If toDB Then
                        Master.DB.Save_TVEpisode(_tv, BatchMode, False, False, False, True)
                    End If

                    Dim fileCount As Integer = 0
                    Dim dirCount As Integer = 0

                    If Directory.Exists(srcDir) Then
                        Dim di As DirectoryInfo = New DirectoryInfo(srcDir)

                        Try
                            fileCount = di.GetFiles().Count
                        Catch
                        End Try

                        Try
                            dirCount = di.GetDirectories().Count
                        Catch
                        End Try

                        If fileCount = 0 AndAlso dirCount = 0 OrElse
                            fileCount = 0 AndAlso dirCount = 1 AndAlso di.GetDirectories.First.Name = ".actors" Then
                            di.Delete(True)
                        End If
                    End If
                End If
            Else
                Dim strErrorMessage As String = String.Format("{0}{2}{2}{1}{2}{2}{3}: {4}{2}{5}: {6}",
                                                              Master.eLang.GetString(171, "Unable to Rename File"),
                                                              _frename.OldFullFileName,
                                                              Environment.NewLine,
                                                              Master.eLang.GetString(43, "Locked"),
                                                              _frename.IsLock.ToString,
                                                              Master.eLang.GetString(1084, "File already exists"),
                                                              _frename.FileExist.ToString)
                If ShowError Then
                    'MessageBox.Show(Master.eLang.GetString(171, "Unable to Rename File"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MessageBox.Show(strErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                logger.Error(strErrorMessage.Replace(Environment.NewLine, "; ").Replace("; ;", ";"))
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub DoRenameSingle_TVShow(ByVal _frename As FileRename, ByRef _tv As Database.DBElement, ByVal BatchMode As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Try
            If Not _tv.IsLock AndAlso Not _frename.DirExist Then
                Dim getError As Boolean = False
                Dim srcDir As String = Path.Combine(_frename.BasePath, _frename.Path)
                Dim destDir As String = Path.Combine(_frename.BasePath, _frename.NewPath)

                'Rename Directory
                If Not srcDir = destDir Then
                    Try
                        If srcDir.ToLower = destDir.ToLower Then
                            Directory.Move(srcDir, String.Concat(destDir, ".$emm"))
                            Directory.Move(String.Concat(destDir, ".$emm"), destDir)
                        Else
                            If Not Directory.Exists(Directory.GetParent(destDir).FullName) Then Directory.CreateDirectory(Directory.GetParent(destDir).FullName)
                            Directory.Move(srcDir, destDir)
                        End If
                    Catch ex As Exception
                        If ShowError Then
                            MessageBox.Show(String.Format(Master.eLang.GetString(144, "An error occured while attempting to rename the directory:{0}{0}{1}{0}{0}Please ensure that you are not accessing this directory or any of its files from another program (including browsing via Windows Explorer)."), Environment.NewLine, ex.Message), Master.eLang.GetString(165, "Unable to Rename Directory"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            logger.Error("Dir: <{0}> - <{1}>", srcDir, destDir)
                        End If
                        getError = True
                    End Try
                End If

                If Not getError Then
                    'update tvshow paths
                    UpdatePaths_TVShow(_tv, srcDir, destDir)

                    'update season paths
                    For Each aSeason As Database.DBElement In _tv.Seasons
                        UpdatePaths_TVShow(aSeason, srcDir, destDir)
                    Next

                    'update episode paths
                    For Each aEpisode As Database.DBElement In _tv.Episodes
                        UpdatePaths_TVShow(aEpisode, srcDir, destDir)
                    Next

                    If toDB Then
                        Master.DB.Save_TVShow(_tv, BatchMode, False, False, True)
                    End If

                    Dim fileCount As Integer = 0
                    Dim dirCount As Integer = 0

                    If Directory.Exists(srcDir) Then
                        Dim di As DirectoryInfo = New DirectoryInfo(srcDir)

                        Try
                            fileCount = di.GetFiles().Count
                        Catch
                        End Try

                        Try
                            dirCount = di.GetDirectories().Count
                        Catch
                        End Try

                        If fileCount = 0 AndAlso dirCount = 0 Then
                            di.Delete()
                        End If
                    End If
                End If
            Else
                If ShowError Then
                    MessageBox.Show(Master.eLang.GetString(165, "Unable to Rename Folder"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function GetCountAll_Movies() As Integer
        Return _movies.Count
    End Function

    Public Function GetCountAll_TVEpisodes() As Integer
        Return _episodes.Count
    End Function

    Public Function GetCountLocked_Movies() As Integer
        Dim c As Integer = c
        For Each f As FileRename In _movies
            If f.IsLock Then c += 1
        Next
        Return c
    End Function

    Public Function GetCountLocked_TVEpisodes() As Integer
        Dim c As Integer = c
        For Each f As FileRename In _episodes
            If f.IsLock Then c += 1
        Next
        Return c
    End Function

    Public Function GetCountMax_Movies() As Integer
        Dim Renamed = From rList In _movies Where rList.DoRename = True
        Return Renamed.Count
    End Function

    Public Function GetCountMax_TVEpisodes() As Integer
        Dim Renamed = From rList In _episodes Where rList.DoRename = True
        Return Renamed.Count
    End Function

    Public Shared Function GetInfo_Movie(ByVal _DBElement As Database.DBElement) As FileRename
        Dim MovieFile As New FileRename

        'ID
        MovieFile.ID = _DBElement.ID

        'Countries
        If _DBElement.Movie.CountriesSpecified Then
            MovieFile.Country = String.Join(" / ", _DBElement.Movie.Countries.ToArray)
        End If

        'Director
        If _DBElement.Movie.DirectorsSpecified Then
            MovieFile.Director = String.Join(" / ", _DBElement.Movie.Directors.ToArray)
        End If

        'Genres
        If _DBElement.Movie.GenresSpecified Then
            MovieFile.Genre = String.Join(" / ", _DBElement.Movie.Genres.ToArray)
        End If

        'IMDB
        If _DBElement.Movie.IMDBSpecified Then
            MovieFile.IMDB = _DBElement.Movie.IMDB
        End If

        'IsLock
        MovieFile.IsLock = _DBElement.IsLock

        'IsSingle
        MovieFile.IsSingle = _DBElement.IsSingle

        'ListTitle
        If _DBElement.ListTitleSpecified Then
            MovieFile.ListTitle = _DBElement.ListTitle
        End If

        'MovieSets
        If _DBElement.Movie.SetsSpecified Then
            MovieFile.Collection = _DBElement.Movie.Sets.Item(0).Title
        End If

        'MPAA
        If _DBElement.Movie.MPAASpecified Then
            MovieFile.MPAA = SelectMPAA(_DBElement.Movie.MPAA)
        End If

        'OriginalTitle
        If _DBElement.Movie.OriginalTitleSpecified Then
            MovieFile.OriginalTitle = _DBElement.Movie.OriginalTitle
        End If

        'Rating
        If _DBElement.Movie.RatingSpecified Then
            MovieFile.Rating = _DBElement.Movie.Rating
        End If

        'SortTitle
        If _DBElement.Movie.SortTitleSpecified Then
            MovieFile.SortTitle = _DBElement.Movie.SortTitle
        Else
            MovieFile.SortTitle = _DBElement.ListTitle
        End If

        'Title
        If _DBElement.Movie.TitleSpecified Then
            MovieFile.Title = _DBElement.Movie.Title
        Else
            MovieFile.Title = _DBElement.ListTitle
        End If

        'VideoSource
        If _DBElement.Movie.VideoSourceSpecified Then
            MovieFile.VideoSource = _DBElement.Movie.VideoSource
        End If

        'Year
        If _DBElement.Movie.YearSpecified Then
            MovieFile.Year = _DBElement.Movie.Year
        End If

        If _DBElement.Movie.FileInfoSpecified Then
            'Resolution
            If _DBElement.Movie.FileInfo.StreamDetails.VideoSpecified Then
                Dim tVid As MediaContainers.Video = NFO.GetBestVideo(_DBElement.Movie.FileInfo)
                Dim tRes As String = NFO.GetResFromDimensions(tVid)
                MovieFile.Resolution = String.Format("{0}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes))
            End If

            If _DBElement.Movie.FileInfo.StreamDetails.AudioSpecified Then
                Dim tAud As MediaContainers.Audio = NFO.GetBestAudio(_DBElement.Movie.FileInfo, False)

                'Audio Channels
                If tAud.ChannelsSpecified Then
                    MovieFile.AudioChannels = tAud.Channels
                End If

                'Audio Codec
                If tAud.CodecSpecified Then
                    MovieFile.AudioCodec = tAud.Codec
                End If
            End If

            'MultiViewCount
            If _DBElement.Movie.FileInfo.StreamDetails.VideoSpecified Then
                If Not String.IsNullOrEmpty(_DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) AndAlso CDbl(_DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) > 1 Then
                    MovieFile.MultiViewCount = "3d"
                End If
            End If

            'MultiViewLayout
            If _DBElement.Movie.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).MultiViewLayoutSpecified Then
                    MovieFile.MultiViewLayout = _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).MultiViewLayout
                End If
            End If

            'StereoMode
            If _DBElement.Movie.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).StereoModeSpecified Then
                    MovieFile.StereoMode = _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).StereoMode
                    MovieFile.ShortStereoMode = _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).ShortStereoMode
                End If
            End If

            'Video Codec
            If _DBElement.Movie.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).CodecSpecified Then
                    MovieFile.VideoCodec = _DBElement.Movie.FileInfo.StreamDetails.Video.Item(0).Codec
                End If
            End If
        End If

        'Path
        MovieFile.BasePath = _DBElement.Source.Path
        MovieFile.OldFullPath = FileUtils.Common.GetMainPath(_DBElement.Filename).FullName
        MovieFile.Parent = FileUtils.Common.GetMainPath(_DBElement.Filename).Name
        If MovieFile.BasePath = FileUtils.Common.GetMainPath(_DBElement.Filename).FullName Then
            MovieFile.OldPath = String.Empty
            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
        Else
            MovieFile.OldPath = Directory.GetParent(FileUtils.Common.GetMainPath(_DBElement.Filename).FullName).FullName.Replace(MovieFile.BasePath, String.Empty)
        End If
        MovieFile.IsBDMV = FileUtils.Common.isBDRip(_DBElement.Filename)
        MovieFile.IsVideoTS = FileUtils.Common.isVideoTS(_DBElement.Filename)

        MovieFile.Path = Path.Combine(MovieFile.OldPath, MovieFile.Parent)
        MovieFile.Path = If(MovieFile.Path.StartsWith(Path.DirectorySeparatorChar), MovieFile.Path.Substring(1), MovieFile.Path)

        'File Name
        MovieFile.Extension = Path.GetExtension(_DBElement.Filename)
        MovieFile.OldFullFileName = _DBElement.Filename
        If MovieFile.IsBDMV Then
            MovieFile.OldFileName = String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")
        ElseIf MovieFile.IsVideoTS Then
            MovieFile.OldFileName = "VIDEO_TS"
        Else
            If Path.GetFileName(_DBElement.Filename.ToLower) = "video_ts.ifo" Then
                MovieFile.OldFileName = Path.GetFileNameWithoutExtension(_DBElement.Filename)
            Else
                MovieFile.OldFileName = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(_DBElement.Filename))
                Dim stackMark As String = Path.GetFileNameWithoutExtension(_DBElement.Filename).Replace(MovieFile.OldFileName, String.Empty).ToLower
                If Not stackMark = String.Empty AndAlso _DBElement.Movie.Title.ToLower.EndsWith(stackMark) Then
                    MovieFile.OldFileName = Path.GetFileNameWithoutExtension(_DBElement.Filename)
                End If
            End If
        End If

        Return MovieFile
    End Function

    Public Shared Function GetInfo_TVEpisode(ByVal _DBElement As Database.DBElement) As FileRename
        Dim EpisodeFile As New FileRename

        'get list of all episodes for multi-episode files
        Dim aSeasonsEpisodes As New List(Of SeasonsEpisodes)

        If _DBElement.FilenameIDSpecified Then

            'first step: get a list of all seasons
            Dim aSeasonsList As New List(Of Integer)
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT Season FROM episode WHERE idFile = ", _DBElement.FilenameID, ";")
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        If Not aSeasonsList.Contains(Convert.ToInt32(SQLReader("Season"))) Then aSeasonsList.Add(Convert.ToInt32(SQLReader("Season")))
                    End While
                End Using
                aSeasonsList.Sort()
            End Using

            'second step: get all episodes per season
            For Each aSeason As Integer In aSeasonsList
                Dim aEpisodesList As New List(Of Episode)
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT idEpisode, Episode, Title, SubEpisode FROM episode WHERE idFile = ", _DBElement.FilenameID, " AND Season = ", aSeason, ";")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            Dim aEpisode As New Episode
                            aEpisode.ID = Convert.ToInt32(SQLReader("idEpisode"))
                            aEpisode.Episode = Convert.ToInt32(SQLReader("Episode"))
                            If Not DBNull.Value.Equals(SQLReader("SubEpisode")) Then aEpisode.SubEpisode = Convert.ToInt32(SQLReader("SubEpisode"))
                            aEpisode.Title = SQLReader("Title").ToString
                            aEpisodesList.Add(aEpisode)
                        End While
                    End Using
                    aEpisodesList.Sort()
                End Using
                Dim aSeasonEpisodesList As New SeasonsEpisodes With {.Season = aSeason, .Episodes = aEpisodesList}
                aSeasonsEpisodes.Add(aSeasonEpisodesList)
            Next
        End If

        EpisodeFile.SeasonsEpisodes.AddRange(aSeasonsEpisodes)

        If EpisodeFile.SeasonsEpisodes.Count > 1 Then
            EpisodeFile.IsMultiEpisode = True
        Else
            For Each se In EpisodeFile.SeasonsEpisodes
                If se.Episodes.Count > 1 Then
                    EpisodeFile.IsMultiEpisode = True
                    Exit For
                End If
            Next
        End If

        'ID
        EpisodeFile.ID = _DBElement.ID

        'Aired
        If _DBElement.TVEpisode.AiredSpecified Then
            EpisodeFile.Aired = _DBElement.TVEpisode.Aired
        End If

        'Episode Title
        If Not EpisodeFile.IsMultiEpisode Then
            If _DBElement.TVEpisode.TitleSpecified Then
                EpisodeFile.Title = _DBElement.TVEpisode.Title
            End If
        Else
            Dim lTitles As New List(Of String)
            For Each lSeason In EpisodeFile.SeasonsEpisodes
                For Each lEpisode In lSeason.Episodes
                    lTitles.Add(lEpisode.Title)
                Next
            Next
            EpisodeFile.Title = String.Join(" - ", lTitles)
        End If

        'IsLock
        EpisodeFile.IsLock = _DBElement.IsLock

        'Rating
        If Not EpisodeFile.IsMultiEpisode Then
            If _DBElement.TVEpisode.RatingSpecified Then
                EpisodeFile.Rating = _DBElement.TVEpisode.Rating
            End If
        Else
            EpisodeFile.Rating = String.Empty
        End If

        'Show ListTitle
        If _DBElement.ListTitle IsNot Nothing Then
            EpisodeFile.ListTitle = _DBElement.ListTitle
        End If

        'Show Title
        If _DBElement.TVShow.TitleSpecified Then
            EpisodeFile.ShowTitle = _DBElement.TVShow.Title
        End If

        'VideoSource
        If _DBElement.TVEpisode.VideoSourceSpecified Then
            EpisodeFile.VideoSource = _DBElement.TVEpisode.VideoSource
        End If

        If _DBElement.TVEpisode.FileInfoSpecified Then
            'Resolution
            If _DBElement.TVEpisode.FileInfo.StreamDetails.VideoSpecified Then
                Dim tVid As MediaContainers.Video = NFO.GetBestVideo(_DBElement.TVEpisode.FileInfo)
                Dim tRes As String = NFO.GetResFromDimensions(tVid)
                EpisodeFile.Resolution = String.Format("{0}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes))
            End If

            If _DBElement.TVEpisode.FileInfo.StreamDetails.AudioSpecified Then
                Dim tAud As MediaContainers.Audio = NFO.GetBestAudio(_DBElement.TVEpisode.FileInfo, False)

                'Audio Channels
                If tAud.ChannelsSpecified Then
                    EpisodeFile.AudioChannels = tAud.Channels
                End If

                'AudioCodec
                If tAud.CodecSpecified Then
                    EpisodeFile.AudioCodec = tAud.Codec
                End If
            End If

            'MultiViewCount
            If _DBElement.TVEpisode.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).MultiViewCountSpecified AndAlso CDbl(_DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) > 1 Then
                    EpisodeFile.MultiViewCount = "3d"
                End If
            End If

            'MultiViewLayout
            If _DBElement.TVEpisode.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).MultiViewLayoutSpecified Then
                    EpisodeFile.MultiViewLayout = _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).MultiViewLayout
                End If
            End If

            'StereoMode
            If _DBElement.TVEpisode.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).StereoModeSpecified Then
                    EpisodeFile.StereoMode = _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).StereoMode
                    EpisodeFile.ShortStereoMode = _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).ShortStereoMode
                End If
            End If

            'Video Codec
            If _DBElement.TVEpisode.FileInfo.StreamDetails.VideoSpecified Then
                If _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).CodecSpecified Then
                    EpisodeFile.VideoCodec = _DBElement.TVEpisode.FileInfo.StreamDetails.Video.Item(0).Codec
                End If
            End If
        End If

        'Path
        EpisodeFile.BasePath = _DBElement.Source.Path
        EpisodeFile.OldFullPath = FileUtils.Common.GetMainPath(_DBElement.Filename).FullName
        EpisodeFile.ShowPath = _DBElement.ShowPath.Replace(_DBElement.Source.Path, String.Empty)
        EpisodeFile.ShowPath = If(EpisodeFile.ShowPath.StartsWith(Path.DirectorySeparatorChar), EpisodeFile.ShowPath.Substring(1), EpisodeFile.ShowPath)
        If FileUtils.Common.isVideoTS(_DBElement.Filename) Then
            EpisodeFile.Parent = FileUtils.Common.GetMainPath(_DBElement.Filename).Name
            If EpisodeFile.BasePath = FileUtils.Common.GetMainPath(_DBElement.Filename).FullName Then
                EpisodeFile.OldPath = String.Empty
                EpisodeFile.BasePath = Directory.GetParent(EpisodeFile.BasePath).FullName
            Else
                EpisodeFile.OldPath = Directory.GetParent(FileUtils.Common.GetMainPath(_DBElement.Filename).FullName).FullName.Replace(_DBElement.Source.Path, String.Empty)
            End If
        ElseIf FileUtils.Common.isBDRip(_DBElement.Filename) Then
            EpisodeFile.Parent = FileUtils.Common.GetMainPath(_DBElement.Filename).Name
            If EpisodeFile.BasePath = Directory.GetParent(FileUtils.Common.GetMainPath(_DBElement.Filename).FullName).FullName Then
                EpisodeFile.OldPath = String.Empty
                EpisodeFile.BasePath = Directory.GetParent(EpisodeFile.BasePath).FullName
            Else
                EpisodeFile.OldPath = Directory.GetParent(FileUtils.Common.GetMainPath(_DBElement.Filename).FullName).FullName.Replace(_DBElement.Source.Path, String.Empty)
            End If
        Else
            EpisodeFile.Parent = Directory.GetParent(_DBElement.Filename).FullName.Replace(Path.Combine(_DBElement.Source.Path, EpisodeFile.ShowPath), String.Empty).Trim
            EpisodeFile.Path = Directory.GetParent(_DBElement.Filename).FullName.Replace(_DBElement.Source.Path, String.Empty)
        End If
        EpisodeFile.IsBDMV = FileUtils.Common.isBDRip(_DBElement.Filename)
        EpisodeFile.IsVideoTS = FileUtils.Common.isVideoTS(_DBElement.Filename)

        EpisodeFile.Parent = If(EpisodeFile.Parent.StartsWith(Path.DirectorySeparatorChar), EpisodeFile.Parent.Substring(1), EpisodeFile.Parent)
        EpisodeFile.Path = If(EpisodeFile.Path.StartsWith(Path.DirectorySeparatorChar), EpisodeFile.Path.Substring(1), EpisodeFile.Path)

        'File
        EpisodeFile.Extension = Path.GetExtension(_DBElement.Filename)
        EpisodeFile.OldFullFileName = _DBElement.Filename
        If Not EpisodeFile.IsVideoTS AndAlso Not EpisodeFile.IsBDMV Then
            If Path.GetFileName(_DBElement.Filename.ToLower) = "video_ts.ifo" Then
                EpisodeFile.OldFileName = "VIDEO_TS"
            Else
                EpisodeFile.OldFileName = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(_DBElement.Filename))
                Dim stackMark As String = Path.GetFileNameWithoutExtension(_DBElement.Filename).Replace(EpisodeFile.OldFileName, String.Empty).ToLower
                If Not stackMark = String.Empty AndAlso _DBElement.TVEpisode.Title.ToLower.EndsWith(stackMark) Then
                    EpisodeFile.OldFileName = Path.GetFileNameWithoutExtension(_DBElement.Filename)
                End If
            End If
        ElseIf EpisodeFile.IsBDMV Then
            EpisodeFile.OldFileName = String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")
        Else
            EpisodeFile.OldFileName = "VIDEO_TS"
        End If

        Return EpisodeFile
    End Function

    Public Shared Function GetInfo_TVShow(ByVal _DBElement As Database.DBElement) As FileRename
        Dim ShowFile As New FileRename

        'ID
        ShowFile.ID = _DBElement.ShowID

        'Genres
        If _DBElement.TVShow.GenresSpecified Then
            ShowFile.Genre = String.Join(" / ", _DBElement.TVShow.Genres.ToArray)
        End If

        'IsLock
        ShowFile.IsLock = _DBElement.IsLock

        'IsSingle
        ShowFile.IsSingle = _DBElement.Source.IsSingle

        'ListTitle
        If _DBElement.ListTitle IsNot Nothing Then
            ShowFile.ListTitle = _DBElement.ListTitle
        End If

        'MPAA
        If _DBElement.TVShow.MPAASpecified Then
            ShowFile.MPAA = SelectMPAA(_DBElement.TVShow.MPAA)
        End If

        'OriginalTitle
        If _DBElement.TVShow.OriginalTitleSpecified Then
            ShowFile.OriginalTitle = _DBElement.TVShow.OriginalTitle
        End If

        'Rating
        If _DBElement.TVShow.RatingSpecified Then
            ShowFile.Rating = _DBElement.TVShow.Rating
        End If

        'Title / ShowTitle
        If _DBElement.TVShow.TitleSpecified Then
            ShowFile.Title = _DBElement.TVShow.Title
            ShowFile.ShowTitle = _DBElement.TVShow.Title
        Else
            ShowFile.Title = _DBElement.ListTitle
            ShowFile.ShowTitle = _DBElement.ListTitle
        End If

        'TVDB
        If _DBElement.TVShow.TVDBSpecified Then
            ShowFile.TVDBID = _DBElement.TVShow.TVDB
        End If

        'Year
        If _DBElement.TVShow.PremieredSpecified Then
            Dim tmpDate As Date
            If Date.TryParse(_DBElement.TVShow.Premiered, tmpDate) Then
                ShowFile.Year = tmpDate.Year.ToString
            End If
        End If

        'Path
        ShowFile.BasePath = _DBElement.Source.Path
        ShowFile.OldFullPath = _DBElement.ShowPath
        ShowFile.ShowPath = _DBElement.ShowPath.Replace(_DBElement.Source.Path, String.Empty)
        ShowFile.ShowPath = If(ShowFile.ShowPath.StartsWith(Path.DirectorySeparatorChar), ShowFile.ShowPath.Substring(1), ShowFile.ShowPath)
        If ShowFile.BasePath = Directory.GetParent(_DBElement.ShowPath).FullName Then
            ShowFile.OldPath = String.Empty
        Else
            ShowFile.OldPath = Directory.GetParent(Directory.GetParent(_DBElement.ShowPath).FullName).FullName.Replace(_DBElement.Source.Path, String.Empty)
        End If

        ShowFile.Path = Path.Combine(ShowFile.OldPath, Path.GetFileName(_DBElement.ShowPath))
        ShowFile.Path = If(ShowFile.Path.StartsWith(Path.DirectorySeparatorChar), ShowFile.Path.Substring(1), ShowFile.Path)

        Return ShowFile
    End Function

    Public Function GetTable_Movies() As DataTable
        Dim dtMovies As New DataTable

        dtMovies.Columns.Add(Master.eLang.GetString(21, "Title"), GetType(String))
        dtMovies.Columns.Add(Master.eLang.GetString(410, "Path"), GetType(String))
        dtMovies.Columns.Add(Master.eLang.GetString(15, "File Name"), GetType(String))
        dtMovies.Columns.Add(Master.eLang.GetString(141, "New Path"), GetType(String))
        dtMovies.Columns.Add(Master.eLang.GetString(142, "New File Name"), GetType(String))
        dtMovies.Columns.Add("IsLocked", GetType(Boolean))
        dtMovies.Columns.Add("DirExist", GetType(Boolean))
        dtMovies.Columns.Add("FileExist", GetType(Boolean))
        dtMovies.Columns.Add("IsSingle", GetType(Boolean))
        dtMovies.Columns.Add("IsRenamed", GetType(Boolean))

        For Each dtRow As FileRename In _movies
            dtMovies.Rows.Add(dtRow.Title, dtRow.Path, dtRow.OldFileName, dtRow.NewPath,
                              dtRow.NewFileName, dtRow.IsLock, dtRow.DirExist,
                              dtRow.FileExist, dtRow.IsSingle, dtRow.DoRename)
        Next

        Return dtMovies
    End Function

    Public Function GetTable_TVEpisodes() As DataTable
        Dim dtEpisodes As New DataTable

        dtEpisodes.Columns.Add(Master.eLang.GetString(21, "Title"), GetType(String))
        dtEpisodes.Columns.Add(Master.eLang.GetString(410, "Path"), GetType(String))
        dtEpisodes.Columns.Add(Master.eLang.GetString(15, "File Name"), GetType(String))
        dtEpisodes.Columns.Add(Master.eLang.GetString(141, "New Path"), GetType(String))
        dtEpisodes.Columns.Add(Master.eLang.GetString(142, "New File Name"), GetType(String))
        dtEpisodes.Columns.Add("IsLocked", GetType(Boolean))
        dtEpisodes.Columns.Add("DirExist", GetType(Boolean))
        dtEpisodes.Columns.Add("FileExist", GetType(Boolean))
        dtEpisodes.Columns.Add("IsSingle", GetType(Boolean))
        dtEpisodes.Columns.Add("IsRenamed", GetType(Boolean))

        For Each dtRow As FileRename In _episodes
            dtEpisodes.Rows.Add(dtRow.Title, dtRow.Path, dtRow.OldFileName, dtRow.NewPath,
                              dtRow.NewFileName, dtRow.IsLock, dtRow.DirExist,
                              dtRow.FileExist, dtRow.IsSingle, dtRow.DoRename)
        Next

        Return dtEpisodes
    End Function

    Public Shared Function HaveBase(ByVal fPattern As String) As Boolean
        If fPattern.Contains("$B") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Sub Process_Movie(ByRef MovieFile As FileRename, ByVal folderPattern As String, ByVal filePattern As String)
        Try
            If HaveBase(folderPattern) Then
                MovieFile.NewPath = ProccessPattern(MovieFile, If(MovieFile.IsSingle, folderPattern, "$D"), True).Trim
            Else
                MovieFile.NewPath = Path.Combine(MovieFile.OldPath, ProccessPattern(MovieFile, If(MovieFile.IsSingle, folderPattern, "$D"), True).Trim)
            End If

            If Not MovieFile.IsVideoTS AndAlso Not MovieFile.IsBDMV Then
                If MovieFile.OldFileName.ToLower = "video_ts" Then
                    MovieFile.NewFileName = MovieFile.OldFileName
                Else
                    MovieFile.NewFileName = ProccessPattern(MovieFile, filePattern, False).Trim
                End If
            ElseIf MovieFile.IsBDMV Then
                MovieFile.NewFileName = MovieFile.OldFileName
            Else
                MovieFile.NewFileName = MovieFile.OldFileName
            End If

            ' removes all leading DirectorySeparatorChar (otherwise, Path.Combine later does not work)
            While MovieFile.NewPath.StartsWith(Path.DirectorySeparatorChar)
                MovieFile.NewPath = MovieFile.NewPath.Remove(0, 1)
            End While

            ' removes all dots at the end of the foldername (dots are not allowed)
            While MovieFile.NewPath.Last = "."
                MovieFile.NewPath = MovieFile.NewPath.Remove(MovieFile.NewPath.Length - 1)
            End While

            ' removes all dots at the end of the filename (for accord with foldername)
            While MovieFile.NewFileName.Last = "."
                MovieFile.NewFileName = MovieFile.NewFileName.Remove(MovieFile.NewFileName.Length - 1)
            End While

            Dim newFullFileName As String = Path.Combine(MovieFile.BasePath, Path.Combine(MovieFile.NewPath, String.Concat(MovieFile.NewFileName, MovieFile.Extension)))
            Dim newFullDirPath As String = Path.Combine(MovieFile.BasePath, MovieFile.NewPath)
            Dim newDirInfo As New DirectoryInfo(newFullDirPath)
            MovieFile.FileExist = File.Exists(newFullFileName) AndAlso Not (newFullFileName.ToLower = MovieFile.OldFullFileName.ToLower)
            MovieFile.DirExist = newDirInfo.Exists AndAlso Not If(newFullDirPath.ToLower = MovieFile.OldFullPath.ToLower, True, newDirInfo.GetFileSystemInfos.Count = 0) OrElse Not MovieFile.IsSingle
            MovieFile.DoRename = Not MovieFile.NewPath = MovieFile.Path OrElse Not MovieFile.NewFileName = MovieFile.OldFileName
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            MovieFile.DoRename = False
        End Try
    End Sub

    Public Shared Sub Process_TVEpisode(ByRef EpisodeFile As FileRename, ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String)
        Try
            Dim pSeason As String = ProccessPattern(EpisodeFile, folderPatternSeasons, True).Trim
            EpisodeFile.NewPath = Path.Combine(EpisodeFile.ShowPath, pSeason)

            If Not EpisodeFile.IsVideoTS AndAlso Not EpisodeFile.IsBDMV Then
                If EpisodeFile.OldFileName.ToLower = "video_ts" Then
                    EpisodeFile.NewFileName = EpisodeFile.OldFileName
                Else
                    EpisodeFile.NewFileName = ProccessPattern(EpisodeFile, filePatternEpisodes, False, EpisodeFile.IsMultiEpisode).Trim
                End If
            ElseIf EpisodeFile.IsBDMV Then
                EpisodeFile.NewFileName = EpisodeFile.OldFileName
            Else
                EpisodeFile.NewFileName = EpisodeFile.OldFileName
            End If

            ' removes all leading DirectorySeparatorChar (otherwise, Path.Combine later does not work)
            While EpisodeFile.NewPath.StartsWith(Path.DirectorySeparatorChar)
                EpisodeFile.NewPath = EpisodeFile.NewPath.Remove(0, 1)
            End While

            ' removes all dots at the end of the foldername (dots are not allowed)
            While EpisodeFile.NewPath.Last = "."
                EpisodeFile.NewPath = EpisodeFile.NewPath.Remove(EpisodeFile.NewPath.Length - 1)
            End While

            ' removes all dots at the end of the filename (for accord with foldername)
            While EpisodeFile.NewFileName.Last = "."
                EpisodeFile.NewFileName = EpisodeFile.NewFileName.Remove(EpisodeFile.NewFileName.Length - 1)
            End While

            Dim newFullFileName As String = Path.Combine(EpisodeFile.BasePath, Path.Combine(EpisodeFile.NewPath, String.Concat(EpisodeFile.NewFileName, EpisodeFile.Extension)))
            EpisodeFile.FileExist = File.Exists(newFullFileName) AndAlso Not (newFullFileName.ToLower = EpisodeFile.OldFullFileName.ToLower)
            EpisodeFile.DoRename = Not EpisodeFile.NewPath = EpisodeFile.Path OrElse Not EpisodeFile.NewFileName = EpisodeFile.OldFileName
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            EpisodeFile.DoRename = False
        End Try
    End Sub

    Public Shared Sub Process_TVShow(ByRef ShowFile As FileRename, ByVal folderPatternShows As String)
        Try
            If HaveBase(folderPatternShows) Then
                ShowFile.NewPath = ProccessPattern(ShowFile, folderPatternShows, True).Trim
            Else
                ShowFile.NewPath = Path.Combine(ShowFile.OldPath, ProccessPattern(ShowFile, folderPatternShows, True).Trim)
            End If

            ' removes all leading DirectorySeparatorChar (otherwise, Path.Combine later does not work)
            While ShowFile.NewPath.StartsWith(Path.DirectorySeparatorChar)
                ShowFile.NewPath = ShowFile.NewPath.Remove(0, 1)
            End While

            ' removes all dots at the end of the foldername (dots are not allowed)
            While ShowFile.NewPath.Last = "."
                ShowFile.NewPath = ShowFile.NewPath.Remove(ShowFile.NewPath.Length - 1)
            End While

            Dim newFullDirPath As String = Path.Combine(ShowFile.BasePath, ShowFile.NewPath)
            Dim newDirInfo As New DirectoryInfo(newFullDirPath)
            ShowFile.DirExist = newDirInfo.Exists AndAlso Not If(newFullDirPath.ToLower = ShowFile.OldFullPath.ToLower, True, newDirInfo.GetFileSystemInfos.Count = 0)
            ShowFile.DoRename = Not ShowFile.NewPath = ShowFile.Path
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            ShowFile.DoRename = False
        End Try
    End Sub

    Public Sub ProccessFiles_Movies(ByVal folderPattern As String, ByVal filePattern As String, Optional ByVal folderPatternIsNotSingle As String = "$D")
        Try
            For Each f As FileRename In _movies
                Process_Movie(f, If(f.IsSingle, folderPattern, folderPatternIsNotSingle), filePattern)
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub ProccessFiles_TVEpisodes(ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String)
        Try
            For Each f As FileRename In _episodes
                Process_TVEpisode(f, folderPatternSeasons, filePatternEpisodes)
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Function ProccessPattern(ByVal f As FileRename, ByVal opattern As String, ByVal isPath As Boolean, Optional isMultiEpisode As Boolean = False) As String
        Try
            If Not String.IsNullOrEmpty(opattern) Then
                Dim pattern As String = opattern
                'Dim strSource As String = f.FileSource  ' APIXML.GetFileSource(Path.Combine(f.Path.ToLower, f.FileName.ToLower))

                'pattern = "$T{($S.$S)}"
                Dim joinIndex As Integer
                Dim nextC = pattern.IndexOf("$")
                Dim nextIB = pattern.IndexOf("{")
                Dim nextEB = pattern.IndexOf("}")
                Dim strCond As String
                Dim strBase As String
                Dim strNoFlags As String
                Dim strJoin As String
                While Not nextC = -1
                    If nextC > nextIB AndAlso nextC < nextEB AndAlso Not nextC = -1 AndAlso Not nextIB = -1 AndAlso Not nextEB = -1 Then
                        strCond = pattern.Substring(nextIB, nextEB - nextIB + 1)
                        strNoFlags = strCond
                        strBase = strCond
                        strCond = ApplyPattern(strCond, "1", If(Not String.IsNullOrEmpty(f.SortTitle), f.SortTitle.Substring(0, 1), String.Empty))
                        strCond = ApplyPattern(strCond, "2", f.Aired)
                        strCond = ApplyPattern(strCond, "3", f.ShortStereoMode)
                        strCond = ApplyPattern(strCond, "4", f.StereoMode)
                        strCond = ApplyPattern(strCond, "A", f.AudioChannels)
                        strCond = ApplyPattern(strCond, "B", String.Empty) 'This is not needed here, Only to HaveBase
                        strCond = ApplyPattern(strCond, "C", f.Director)
                        strCond = ApplyPattern(strCond, "D", f.Parent)
                        strCond = ApplyPattern(strCond, "E", f.SortTitle)
                        strCond = ApplyPattern(strCond, "F", f.OldFileName.Replace("\", String.Empty))
                        '                                G   Genres
                        strCond = ApplyPattern(strCond, "H", f.VideoCodec)
                        strCond = ApplyPattern(strCond, "I", f.IMDB)
                        strCond = ApplyPattern(strCond, "J", f.AudioCodec)
                        '                                K   Season
                        strCond = ApplyPattern(strCond, "L", f.ListTitle)
                        strCond = ApplyPattern(strCond, "M", f.MPAA)
                        strCond = ApplyPattern(strCond, "N", f.Collection)
                        '                                O   OriginalTitle
                        '                                OO  OriginalTitle if different from the title
                        strCond = ApplyPattern(strCond, "P", If(Not String.IsNullOrEmpty(f.Rating), String.Format("{0:0.0}", Double.Parse(f.Rating, Globalization.CultureInfo.InvariantCulture)), String.Empty))
                        '                                Q   Episode
                        strCond = ApplyPattern(strCond, "R", f.Resolution)
                        strCond = ApplyPattern(strCond, "S", f.VideoSource)
                        strCond = ApplyPattern(strCond, "T", f.Title)
                        '                                U   Countries
                        strCond = ApplyPattern(strCond, "V", f.MultiViewCount)
                        '                                W   SeasonEpisode
                        '                                X   
                        strCond = ApplyPattern(strCond, "Y", f.Year)
                        strCond = ApplyPattern(strCond, "Z", f.ShowTitle)

                        'Genres
                        joinIndex = strCond.IndexOf("$G")
                        If Not joinIndex = -1 Then
                            If strCond.Length > joinIndex + 2 Then
                                strJoin = strCond.Substring(joinIndex + 2, 1)
                                If Not ". -".IndexOf(strJoin) = -1 Then
                                    strCond = ApplyPattern(strCond, String.Concat("G", strJoin), f.Genre.Replace(" / ", strJoin))
                                Else
                                    strCond = ApplyPattern(strCond, "G", f.Genre.Replace(" / ", " "))
                                End If
                            Else
                                strCond = ApplyPattern(strCond, "G", f.Genre.Replace(" / ", " "))
                            End If
                        End If

                        'OriginalTitle
                        joinIndex = strCond.IndexOf("$O")
                        If Not joinIndex = -1 Then
                            If strCond.Length > joinIndex + 2 Then
                                strJoin = strCond.Substring(joinIndex + 2, 1)
                                If strJoin = "O" Then
                                    strCond = ApplyPattern(strCond, "OO", If(Not f.OriginalTitle = f.Title, f.OriginalTitle, String.Empty))
                                Else
                                    strCond = ApplyPattern(strCond, "O", f.OriginalTitle)
                                End If
                            Else
                                strCond = ApplyPattern(strCond, "O", f.OriginalTitle)
                            End If
                        End If

                        'Countries
                        joinIndex = strCond.IndexOf("$U")
                        If Not joinIndex = -1 Then
                            If strCond.Length > joinIndex + 2 Then
                                strJoin = strCond.Substring(joinIndex + 2, 1)
                                If Not ". -".IndexOf(strJoin) = -1 Then
                                    strCond = ApplyPattern(strCond, String.Concat("U", strJoin), f.Country.Replace(" / ", strJoin))
                                Else
                                    strCond = ApplyPattern(strCond, "U", f.Country.Replace(" / ", " "))
                                End If
                            Else
                                strCond = ApplyPattern(strCond, "U", f.Country.Replace(" / ", " "))
                            End If
                        End If

                        strNoFlags = Regex.Replace(strNoFlags, "\$((?:OO|[12ABCDEFHIJKLMNOPQRSTVWY]|G[. -]|U[. -]?))", String.Empty) '"(?i)\$([DFTYRAS])"  "\$((?i:[DFTYRAS]))"
                        If strCond.Trim = strNoFlags.Trim Then
                            strCond = String.Empty
                        Else
                            strCond = strCond.Substring(1, strCond.Length - 2)
                        End If
                        pattern = pattern.Replace(strBase, strCond)
                        nextC = pattern.IndexOf("$")
                    Else
                        nextC = pattern.IndexOf("$", nextC + 1)
                    End If
                    nextIB = pattern.IndexOf("{")
                    nextEB = pattern.IndexOf("}")
                End While

                pattern = ApplyPattern(pattern, "1", If(Not String.IsNullOrEmpty(f.SortTitle), f.SortTitle.Substring(0, 1), String.Empty))
                pattern = ApplyPattern(pattern, "2", f.Aired)
                pattern = ApplyPattern(pattern, "3", f.ShortStereoMode)
                pattern = ApplyPattern(pattern, "4", f.StereoMode)
                pattern = ApplyPattern(pattern, "A", f.AudioChannels)
                pattern = ApplyPattern(pattern, "B", String.Empty) 'This is not need here, Only to HaveBase
                pattern = ApplyPattern(pattern, "C", f.Director)
                pattern = ApplyPattern(pattern, "D", f.Parent) '.Replace("\", String.Empty))
                pattern = ApplyPattern(pattern, "E", f.SortTitle)
                pattern = ApplyPattern(pattern, "F", f.OldFileName.Replace("\", String.Empty))
                '                                G   Genres
                pattern = ApplyPattern(pattern, "H", f.VideoCodec)
                pattern = ApplyPattern(pattern, "I", f.IMDB)
                pattern = ApplyPattern(pattern, "J", f.AudioCodec)
                '                                K   Season
                pattern = ApplyPattern(pattern, "L", f.ListTitle)
                pattern = ApplyPattern(pattern, "M", f.MPAA)
                pattern = ApplyPattern(pattern, "N", f.Collection)
                '                                O   OriginalTitle
                '                                OO  OriginalTitle if different from the title
                pattern = ApplyPattern(pattern, "P", If(Not String.IsNullOrEmpty(f.Rating), String.Format("{0:0.0}", Double.Parse(f.Rating, Globalization.CultureInfo.InvariantCulture)), String.Empty))
                '                                Q   Episode
                pattern = ApplyPattern(pattern, "R", f.Resolution)
                pattern = ApplyPattern(pattern, "S", f.VideoSource)
                pattern = ApplyPattern(pattern, "T", f.Title)
                '                                U   Countries
                pattern = ApplyPattern(pattern, "V", f.MultiViewCount)
                '                                W   SeasonEpisode
                '                                X   
                pattern = ApplyPattern(pattern, "Y", f.Year)
                pattern = ApplyPattern(pattern, "Z", f.ShowTitle)

                'season and episode
                nextC = pattern.IndexOf("$W")
                If Not nextC = -1 Then
                    If pattern.Length > nextC + 2 Then
                        Dim ePattern As String = String.Empty
                        Dim ePrefix As String = String.Empty
                        Dim ePadding As Integer = 1
                        Dim eFormat As String = "{0:0}"
                        Dim eSeparator As String = String.Empty
                        Dim fPattern As String = String.Empty
                        Dim sPattern As String = String.Empty
                        Dim sPrefix As String = String.Empty
                        Dim sSeparator As String = String.Empty
                        Dim seString As String = String.Empty
                        Dim sPadding As Integer = 1
                        Dim sFormat As String = "{0:0}"

                        strBase = pattern.Substring(nextC)
                        nextIB = strBase.IndexOf("?")
                        If nextIB > -1 Then
                            nextEB = strBase.IndexOf("?", nextIB + 1)
                            If nextEB > -1 Then
                                sPattern = strBase.Substring(2, nextIB - 2)
                                If Not String.IsNullOrEmpty(sPattern) AndAlso Integer.TryParse(sPattern.Substring(0, 1), 0) Then
                                    sPadding = CInt(sPattern.Substring(0, 1))
                                    sPattern = sPattern.Substring(1)
                                    If Not sPadding > 0 Then
                                        sPadding = 1
                                    End If
                                End If
                                ePattern = strBase.Substring(nextIB + 1, nextEB - nextIB - 1)
                                If Not String.IsNullOrEmpty(ePattern) AndAlso Integer.TryParse(ePattern.Substring(0, 1), 0) Then
                                    ePadding = CInt(ePattern.Substring(0, 1))
                                    ePattern = ePattern.Substring(1)
                                    If Not ePadding > 0 Then
                                        ePadding = 1
                                    End If
                                End If
                                fPattern = strBase.Substring(0, nextEB + 1)
                            End If
                        End If

                        If sPattern.StartsWith(".") OrElse sPattern.StartsWith("_") Then
                            sSeparator = sPattern.Substring(0, 1)
                            sPrefix = sPattern.Remove(0, 1)
                        Else
                            sPrefix = sPattern
                        End If

                        For i As Integer = 1 To sPadding - 1
                            sFormat = sFormat.Insert(sFormat.Length - 1, "0")
                        Next

                        If ePattern.StartsWith(".") OrElse ePattern.StartsWith("_") OrElse ePattern.StartsWith("x") Then
                            eSeparator = ePattern.Substring(0, 1)
                            ePrefix = ePattern.Remove(0, 1)
                        Else
                            ePrefix = ePattern
                        End If

                        For i As Integer = 1 To ePadding - 1
                            eFormat = eFormat.Insert(eFormat.Length - 1, "0")
                        Next

                        For Each season As SeasonsEpisodes In f.SeasonsEpisodes
                            seString = String.Concat(seString, sSeparator, sPrefix, String.Format(sFormat, season.Season))
                            For Each episode In season.Episodes
                                If Not episode.SubEpisode = -1 Then
                                    seString = String.Concat(seString, eSeparator, ePrefix, String.Concat(String.Format(eFormat, episode.Episode), ".", episode.SubEpisode))
                                Else
                                    seString = String.Concat(seString, eSeparator, ePrefix, String.Format(eFormat, episode.Episode))
                                End If
                            Next
                        Next

                        If Not String.IsNullOrEmpty(sSeparator) AndAlso seString.StartsWith(sSeparator) Then seString = seString.Remove(0, 1)

                        pattern = pattern.Replace(fPattern, seString)
                    End If
                End If

                'season
                nextC = pattern.IndexOf("$K")
                If Not nextC = -1 Then
                    If pattern.Length > nextC + 1 Then
                        Dim sPattern As String = String.Empty
                        Dim sPrefix As String = String.Empty
                        Dim sSeparator As String = String.Empty
                        Dim fPattern As String = String.Empty
                        Dim sString As String = String.Empty
                        Dim sPadding As Integer = 1
                        Dim sFormat As String = "{0:0}"

                        strBase = pattern.Substring(nextC)
                        nextIB = strBase.IndexOf("?")
                        If nextIB > -1 Then
                            sPattern = strBase.Substring(2, nextIB - 2)
                            If Not String.IsNullOrEmpty(sPattern) AndAlso Integer.TryParse(sPattern.Substring(0, 1), 0) Then
                                sPadding = CInt(sPattern.Substring(0, 1))
                                sPattern = sPattern.Substring(1)
                                If Not sPadding > 0 Then
                                    sPadding = 1
                                End If
                            End If
                            fPattern = strBase.Substring(0, nextIB + 1)
                        End If

                        If sPattern.StartsWith(".") OrElse sPattern.StartsWith("_") OrElse sPattern.StartsWith("x") Then
                            sSeparator = sPattern.Substring(0, 1)
                            sPrefix = sPattern.Remove(0, 1)
                        Else
                            sPrefix = sPattern
                        End If

                        For i As Integer = 1 To sPadding - 1
                            sFormat = sFormat.Insert(sFormat.Length - 1, "0")
                        Next

                        For Each season As SeasonsEpisodes In f.SeasonsEpisodes
                            sString = String.Concat(sString, sSeparator, sPrefix, String.Format(sFormat, season.Season))
                        Next

                        If Not String.IsNullOrEmpty(sSeparator) AndAlso sString.StartsWith(sSeparator) Then sString = sString.Remove(0, 1)

                        pattern = pattern.Replace(fPattern, sString)
                    End If
                End If

                'episode
                nextC = pattern.IndexOf("$Q")
                If Not nextC = -1 Then
                    If pattern.Length > nextC + 2 Then
                        Dim ePattern As String = String.Empty
                        Dim ePrefix As String = String.Empty
                        Dim eSeparator As String = String.Empty
                        Dim fPattern As String = String.Empty
                        Dim eString As String = String.Empty
                        Dim ePadding As Integer = 1
                        Dim eFormat As String = "{0:0}"

                        strBase = pattern.Substring(nextC)
                        nextIB = strBase.IndexOf("?")
                        If nextIB > -1 Then
                            ePattern = strBase.Substring(2, nextIB - 2)
                            If Not String.IsNullOrEmpty(ePattern) AndAlso Integer.TryParse(ePattern.Substring(0, 1), 0) Then
                                ePadding = CInt(ePattern.Substring(0, 1))
                                ePattern = ePattern.Substring(1)
                                If Not ePadding > 0 Then
                                    ePadding = 1
                                End If
                            End If
                            fPattern = strBase.Substring(0, nextIB + 1)
                        End If

                        If ePattern.StartsWith(".") OrElse ePattern.StartsWith("_") OrElse ePattern.StartsWith("x") Then
                            eSeparator = ePattern.Substring(0, 1)
                            ePrefix = ePattern.Remove(0, 1)
                        Else
                            ePrefix = ePattern
                        End If

                        For i As Integer = 1 To ePadding - 1
                            eFormat = eFormat.Insert(eFormat.Length - 1, "0")
                        Next

                        For Each season As SeasonsEpisodes In f.SeasonsEpisodes
                            For Each episode In season.Episodes
                                If Not episode.SubEpisode = -1 Then
                                    eString = String.Concat(eString, eSeparator, ePrefix, String.Concat(String.Format(eFormat, episode.Episode), ".", episode.SubEpisode))
                                Else
                                    eString = String.Concat(eString, eSeparator, ePrefix, String.Format(eFormat, episode.Episode))
                                End If
                            Next
                        Next

                        If Not String.IsNullOrEmpty(eSeparator) AndAlso eString.StartsWith(eSeparator) Then eString = eString.Remove(0, 1)

                        pattern = pattern.Replace(fPattern, eString)
                    End If
                End If

                'OriginalTitle
                nextC = pattern.IndexOf("$O")
                If Not nextC = -1 Then
                    If pattern.Length > nextC + 2 Then
                        strCond = pattern.Substring(nextC + 2, 1)
                        If strCond = "O" Then
                            pattern = ApplyPattern(pattern, "OO", If(Not f.OriginalTitle = f.Title, f.OriginalTitle, String.Empty))
                        Else
                            pattern = ApplyPattern(pattern, "O", f.OriginalTitle)
                        End If
                    Else
                        pattern = ApplyPattern(pattern, "O", f.OriginalTitle)
                    End If
                End If

                'Genres
                nextC = pattern.IndexOf("$G")
                If Not nextC = -1 Then
                    If pattern.Length > nextC + 2 Then
                        strCond = pattern.Substring(nextC + 2, 1)
                        If Not ". -".IndexOf(strCond) = -1 Then
                            pattern = ApplyPattern(pattern, String.Concat("G", strCond), f.Genre.Replace(" / ", strCond))
                        Else
                            pattern = ApplyPattern(pattern, "G", f.Genre.Replace(" / ", " "))
                        End If
                    Else
                        pattern = ApplyPattern(pattern, "G", f.Genre.Replace(" / ", " "))
                    End If
                End If

                'Countries
                nextC = pattern.IndexOf("$U")
                If Not nextC = -1 Then
                    If pattern.Length > nextC + 2 Then
                        strCond = pattern.Substring(nextC + 2, 1)
                        If Not ". -".IndexOf(strCond) = -1 Then
                            pattern = ApplyPattern(pattern, String.Concat("U", strCond), f.Country.Replace(" / ", strCond))
                        Else
                            pattern = ApplyPattern(pattern, "U", f.Country.Replace(" / ", " "))
                        End If
                    Else
                        pattern = ApplyPattern(pattern, "U", f.Country.Replace(" / ", " "))
                    End If
                End If

                'Replace all spaces with character
                nextC = pattern.IndexOf("$X")
                If Not nextC = -1 AndAlso pattern.Length > nextC + 2 Then
                    strCond = pattern.Substring(nextC + 2, 1)
                    pattern = pattern.Replace(String.Concat("$X", strCond), String.Empty)
                    pattern = pattern.Replace(" ", strCond)
                End If

                'Replace character(s) with another character(s)
                nextC = pattern.IndexOf("$?")
                Dim strmore As String = String.Empty
                While nextC > -1
                    'If nextC > -1 Then
                    strBase = pattern.Substring(nextC + 2)
                    pattern = pattern.Substring(0, nextC)
                    If Not strBase = String.Empty Then
                        nextIB = strBase.IndexOf("?")
                        If nextIB > -1 Then
                            nextEB = strBase.Substring(nextIB + 1).IndexOf("?")
                            If nextEB > -1 Then
                                strCond = strBase.Substring(nextIB + 1, nextEB)
                                strmore = strBase.Substring(nextIB + nextEB + 2)
                                strBase = strBase.Substring(0, nextIB)
                                If Not strBase = String.Empty Then pattern = pattern.Replace(strBase, strCond)
                            End If
                        End If
                    End If
                    'End If
                    pattern = String.Concat(pattern, strmore)
                    nextC = pattern.IndexOf("$?")
                End While

                'Lowercase all letters
                nextC = pattern.IndexOf("$;")
                If Not nextC = -1 Then
                    pattern = pattern.ToLower
                    pattern = pattern.Replace("$;", String.Empty)
                End If

                'Uppercase first letter in each word
                nextC = pattern.IndexOf("$!")
                If Not nextC = -1 Then
                    pattern = Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(pattern)
                    pattern = pattern.Replace("$!", String.Empty)
                End If

                'Cleaning
                If isPath Then
                    pattern = StringUtils.CleanPath(pattern)
                Else
                    pattern = StringUtils.CleanFileName(pattern)
                End If

                ' removes all dots at the end of the name (dots are not allowed)
                If Not String.IsNullOrEmpty(pattern) Then
                    While pattern.Last = "."
                        pattern = pattern.Remove(pattern.Length - 1)
                    End While
                End If

                Return pattern.Trim
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return String.Empty
        End Try
    End Function

    Public Shared Sub RenameSingle_Movie(ByRef _tmpMovie As Database.DBElement, ByVal folderPattern As String, ByVal filePattern As String, ByVal BatchMode As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Dim MovieFile As New FileRename

        MovieFile = GetInfo_Movie(_tmpMovie)
        Process_Movie(MovieFile, folderPattern, filePattern)

        If MovieFile.DoRename Then
            DoRenameSingle_Movie(MovieFile, _tmpMovie, BatchMode, ShowError, toDB)
        Else
            If toDB Then
                Master.DB.Save_Movie(_tmpMovie, BatchMode, False, False, False)
            End If
        End If
    End Sub

    Public Shared Sub RenameSingle_TVEpisode(ByRef _tmpEpisode As Database.DBElement, ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String, ByVal BatchMode As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Dim EpisodeFile As New FileRename

        EpisodeFile = GetInfo_TVEpisode(_tmpEpisode)
        Process_TVEpisode(EpisodeFile, folderPatternSeasons, filePatternEpisodes)

        If EpisodeFile.DoRename Then
            DoRenameSingle_TVEpisode(EpisodeFile, _tmpEpisode, BatchMode, ShowError, toDB)
        Else
            If toDB Then
                Master.DB.Save_TVEpisode(_tmpEpisode, BatchMode, False, False, False, True)
            End If
        End If
    End Sub

    Public Shared Sub RenameSingle_TVShow(ByRef _tmpShow As Database.DBElement, ByVal folderPatternShows As String, ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String, ByVal BatchMode As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Dim ShowFile As New FileRename

        ShowFile = GetInfo_TVShow(_tmpShow)
        Process_TVShow(ShowFile, folderPatternShows)

        'rename episodes (does not change the tvshow folder)
        If Not String.IsNullOrEmpty(folderPatternSeasons) AndAlso Not String.IsNullOrEmpty(filePatternEpisodes) Then
            For Each tEpisode As Database.DBElement In _tmpShow.Episodes.Where(Function(f) f.IsOnline)
                Dim EpisodeFile As New FileRename
                EpisodeFile = GetInfo_TVEpisode(tEpisode)
                Process_TVEpisode(EpisodeFile, folderPatternSeasons, filePatternEpisodes)
                If EpisodeFile.DoRename Then
                    DoRenameSingle_TVEpisode(EpisodeFile, tEpisode, BatchMode, ShowError, False)
                End If
            Next
        End If

        If ShowFile.DoRename AndAlso Not ShowFile.IsSingle Then
            'rename tvshow
            DoRenameSingle_TVShow(ShowFile, _tmpShow, BatchMode, ShowError, toDB)
        Else
            If toDB Then
                Master.DB.Save_TVShow(_tmpShow, BatchMode, False, False, True)
            End If
            If ShowFile.DoRename AndAlso ShowFile.IsSingle Then
                If ShowError Then
                    MessageBox.Show(Master.eLang.GetString(1086, "The directory can not be renamed because the directory is specified as the source."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                logger.Error(String.Format("[{0}] [{1}] [Partially Done] {2}",
                                           Reflection.MethodBase.GetCurrentMethod.ReflectedType,
                                           Reflection.MethodBase.GetCurrentMethod.Name,
                                           "Can't rename a directory that has been set as source"))
            End If
        End If
    End Sub

    Public Shared Function SelectMPAA(ByVal strMPAA As String) As String
        If Not String.IsNullOrEmpty(strMPAA) Then
            If strMPAA.ToLower.StartsWith("rated g") Then
                Return "G"
            ElseIf strMPAA.ToLower.StartsWith("rated pg-13") Then
                Return "PG-13"
            ElseIf strMPAA.ToLower.StartsWith("rated pg") Then
                Return "PG"
            ElseIf strMPAA.ToLower.StartsWith("rated r") Then
                Return "R"
            ElseIf strMPAA.ToLower.StartsWith("rated nc-17") Then
                Return "NC-17"
            ElseIf strMPAA.Contains(":") Then 'might be a certification
                Dim strSplit As String = strMPAA.Split(Convert.ToChar(":")).Last
                Return Regex.Replace(strSplit, "rated", String.Empty, RegexOptions.IgnoreCase).Trim
            Else
                Return strMPAA
            End If
        Else
            Return String.Empty
        End If
    End Function

    Public Sub SetIsLocked_Movies(ByVal path As String, ByVal filename As String, ByVal lock As Boolean)
        For Each f As FileRename In _movies
            If (f.Path = path AndAlso f.OldFileName = filename) OrElse filename = String.Empty Then f.IsLock = lock
        Next
    End Sub

    Public Sub SetIsLocked_TVEpisodes(ByVal path As String, ByVal filename As String, ByVal lock As Boolean)
        For Each f As FileRename In _episodes
            If (f.Path = path AndAlso f.OldFileName = filename) OrElse filename = String.Empty Then f.IsLock = lock
        Next
    End Sub

    Private Shared Sub UpdatePaths_Movie(ByRef _DBM As Database.DBElement, ByVal oldPath As String, ByVal newPath As String, ByVal oldFile As String, ByVal newFile As String)
        If _DBM.ExtrafanartsPathSpecified Then _DBM.ExtrafanartsPath = Path.Combine(Directory.GetParent(_DBM.ExtrafanartsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ExtrafanartsPath).Replace(oldFile, newFile))
        If _DBM.ExtrathumbsPathSpecified Then _DBM.ExtrathumbsPath = Path.Combine(Directory.GetParent(_DBM.ExtrathumbsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ExtrathumbsPath).Replace(oldFile, newFile))
        If _DBM.FilenameSpecified Then _DBM.Filename = Path.Combine(Directory.GetParent(_DBM.Filename).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.Filename).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.Banner.LocalFilePathSpecified Then _DBM.ImagesContainer.Banner.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.Banner.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.Banner.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.ClearArt.LocalFilePathSpecified Then _DBM.ImagesContainer.ClearArt.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.ClearArt.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.ClearArt.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.ClearLogo.LocalFilePathSpecified Then _DBM.ImagesContainer.ClearLogo.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.ClearLogo.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.ClearLogo.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.DiscArt.LocalFilePathSpecified Then _DBM.ImagesContainer.DiscArt.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.DiscArt.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.DiscArt.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.Fanart.LocalFilePathSpecified Then _DBM.ImagesContainer.Fanart.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.Fanart.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.Fanart.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.Landscape.LocalFilePathSpecified Then _DBM.ImagesContainer.Landscape.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.Landscape.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.Landscape.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.Poster.LocalFilePathSpecified Then _DBM.ImagesContainer.Poster.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.ImagesContainer.Poster.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ImagesContainer.Poster.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.NfoPathSpecified Then _DBM.NfoPath = Path.Combine(Directory.GetParent(_DBM.NfoPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.NfoPath).Replace(oldFile, newFile))
        If _DBM.ThemePathSpecified Then _DBM.ThemePath = Path.Combine(Directory.GetParent(_DBM.ThemePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ThemePath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.Trailer.LocalFilePath) Then _DBM.Trailer.LocalFilePath = Path.Combine(Directory.GetParent(_DBM.Trailer.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.Trailer.LocalFilePath).Replace(oldFile, newFile))
        If _DBM.ImagesContainer.Extrafanarts.Count > 0 Then
            For Each eImg In _DBM.ImagesContainer.Extrafanarts.Where(Function(f) f.LocalFilePathSpecified)
                eImg.LocalFilePath = Path.Combine(Directory.GetParent(eImg.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(eImg.LocalFilePath).Replace(oldFile, newFile))
            Next
        End If
        If _DBM.ImagesContainer.Extrathumbs.Count > 0 Then
            For Each eImg In _DBM.ImagesContainer.Extrathumbs.Where(Function(f) f.LocalFilePathSpecified)
                eImg.LocalFilePath = Path.Combine(Directory.GetParent(eImg.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(eImg.LocalFilePath).Replace(oldFile, newFile))
            Next
        End If
        If _DBM.SubtitlesSpecified Then
            For Each subtitle In _DBM.Subtitles.Where(Function(f) f.SubsPathSpecified)
                subtitle.SubsPath = Path.Combine(Directory.GetParent(subtitle.SubsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(subtitle.SubsPath).Replace(oldFile, newFile))
            Next
        End If
    End Sub

    Private Shared Sub UpdatePaths_TVEpisode(ByRef _DBE As Database.DBElement, ByVal oldPath As String, ByVal newPath As String, ByVal oldFile As String, ByVal newFile As String)
        If _DBE.FilenameSpecified Then _DBE.Filename = Path.Combine(Directory.GetParent(_DBE.Filename).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.Filename).Replace(oldFile, newFile))
        If _DBE.ImagesContainer.Fanart.LocalFilePathSpecified Then _DBE.ImagesContainer.Fanart.LocalFilePath = Path.Combine(Directory.GetParent(_DBE.ImagesContainer.Fanart.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.ImagesContainer.Fanart.LocalFilePath).Replace(oldFile, newFile))
        If _DBE.ImagesContainer.Poster.LocalFilePathSpecified Then _DBE.ImagesContainer.Poster.LocalFilePath = Path.Combine(Directory.GetParent(_DBE.ImagesContainer.Poster.LocalFilePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.ImagesContainer.Poster.LocalFilePath).Replace(oldFile, newFile))
        If _DBE.NfoPathSpecified Then _DBE.NfoPath = Path.Combine(Directory.GetParent(_DBE.NfoPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.NfoPath).Replace(oldFile, newFile))
        If _DBE.SubtitlesSpecified Then
            For Each subtitle In _DBE.Subtitles.Where(Function(f) f.SubsPathSpecified)
                subtitle.SubsPath = Path.Combine(Directory.GetParent(subtitle.SubsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(subtitle.SubsPath).Replace(oldFile, newFile))
            Next
        End If
    End Sub

    Private Shared Sub UpdatePaths_TVShow(ByRef _DBE As Database.DBElement, ByVal oldPath As String, ByVal newPath As String)
        If _DBE.ExtrafanartsPathSpecified Then _DBE.ExtrafanartsPath = _DBE.ExtrafanartsPath.Replace(oldPath, newPath)
        If _DBE.FilenameSpecified Then _DBE.Filename = _DBE.Filename.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.Banner.LocalFilePathSpecified Then _DBE.ImagesContainer.Banner.LocalFilePath = _DBE.ImagesContainer.Banner.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.CharacterArt.LocalFilePathSpecified Then _DBE.ImagesContainer.CharacterArt.LocalFilePath = _DBE.ImagesContainer.CharacterArt.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.ClearArt.LocalFilePathSpecified Then _DBE.ImagesContainer.ClearArt.LocalFilePath = _DBE.ImagesContainer.ClearArt.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.ClearLogo.LocalFilePathSpecified Then _DBE.ImagesContainer.ClearLogo.LocalFilePath = _DBE.ImagesContainer.ClearLogo.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.Fanart.LocalFilePathSpecified Then _DBE.ImagesContainer.Fanart.LocalFilePath = _DBE.ImagesContainer.Fanart.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.Landscape.LocalFilePathSpecified Then _DBE.ImagesContainer.Landscape.LocalFilePath = _DBE.ImagesContainer.Landscape.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.ImagesContainer.Poster.LocalFilePathSpecified Then _DBE.ImagesContainer.Poster.LocalFilePath = _DBE.ImagesContainer.Poster.LocalFilePath.Replace(oldPath, newPath)
        If _DBE.NfoPathSpecified Then _DBE.NfoPath = _DBE.NfoPath.Replace(oldPath, newPath)
        If _DBE.ShowPathSpecified Then _DBE.ShowPath = _DBE.ShowPath.Replace(oldPath, newPath)
        If _DBE.ThemePathSpecified Then _DBE.ThemePath = _DBE.ThemePath.Replace(oldPath, newPath)
        If _DBE.SubtitlesSpecified Then
            For Each subtitle In _DBE.Subtitles.Where(Function(f) f.SubsPathSpecified)
                subtitle.SubsPath = subtitle.SubsPath.Replace(oldPath, newPath)
            Next
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Class FileRename

#Region "Fields"

        Private _aired As String
        Private _audiochannels As String
        Private _audiocodec As String
        Private _basepath As String
        Private _collection As String
        Private _country As String
        Private _direxist As Boolean
        Private _director As String
        Private _dorename As Boolean
        Private _extension As String
        Private _fileexist As Boolean
        Private _genre As String
        Private _id As Long
        Private _imdb As String
        Private _ismultiepisode As Boolean
        Private _issingle As Boolean
        Private _isbdmv As Boolean
        Private _islock As Boolean
        Private _isvideots As Boolean
        Private _listtitle As String
        Private _mpaa As String
        Private _multiviewcount As String
        Private _multiviewlayout As String
        Private _newfilename As String
        Private _newpath As String
        Private _oldfilename As String
        Private _oldfullfilename As String
        Private _oldfullpath As String
        Private _oldpath As String
        Private _originaltitle As String
        Private _parent As String
        Private _path As String
        Private _rating As String
        Private _resolution As String
        Private _seasonsepisodes As New List(Of SeasonsEpisodes)
        Private _shortstereomode As String
        Private _showpath As String
        Private _showtitle As String
        Private _sorttitle As String
        Private _status As String
        Private _stereomode As String
        Private _title As String
        Private _tvdbid As String
        Private _videocodec As String
        Private _videosource As String
        Private _year As String

#End Region 'Fields

#Region "Properties"

        Public Property Aired() As String
            Get
                Return _aired
            End Get
            Set(ByVal value As String)
                _aired = value
            End Set
        End Property

        Public Property AudioChannels() As String
            Get
                Return _audiochannels
            End Get
            Set(ByVal value As String)
                _audiochannels = value
            End Set
        End Property

        Public Property AudioCodec() As String
            Get
                Return _audiocodec
            End Get
            Set(ByVal value As String)
                _audiocodec = value
            End Set
        End Property

        Public Property BasePath() As String
            Get
                Return _basepath
            End Get
            Set(ByVal value As String)
                _basepath = value
            End Set
        End Property

        Public Property Collection() As String
            Get
                Return _collection
            End Get
            Set(ByVal value As String)
                _collection = value
            End Set
        End Property

        Public Property DirExist() As Boolean
            Get
                Return _direxist
            End Get
            Set(ByVal value As Boolean)
                _direxist = value
            End Set
        End Property

        Public Property DoRename() As Boolean
            Get
                Return _dorename
            End Get
            Set(ByVal value As Boolean)
                _dorename = value
            End Set
        End Property

        Public Property Extension() As String
            Get
                Return _extension
            End Get
            Set(ByVal value As String)
                _extension = value.Trim
            End Set
        End Property

        Public Property FileExist() As Boolean
            Get
                Return _fileexist
            End Get
            Set(ByVal value As Boolean)
                _fileexist = value
            End Set
        End Property

        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal value As Long)
                _id = value
            End Set
        End Property

        Public Property IsBDMV() As Boolean
            Get
                Return _isbdmv
            End Get
            Set(ByVal value As Boolean)
                _isbdmv = value
            End Set
        End Property

        Public Property IsLock() As Boolean
            Get
                Return _islock
            End Get
            Set(ByVal value As Boolean)
                _islock = value
            End Set
        End Property

        Public Property IsMultiEpisode() As Boolean
            Get
                Return _ismultiepisode
            End Get
            Set(ByVal value As Boolean)
                _ismultiepisode = value
            End Set
        End Property

        Public Property IsSingle() As Boolean
            Get
                Return _issingle
            End Get
            Set(ByVal value As Boolean)
                _issingle = value
            End Set
        End Property

        Public Property IsVideoTS() As Boolean
            Get
                Return _isvideots
            End Get
            Set(ByVal value As Boolean)
                _isvideots = value
            End Set
        End Property

        Public Property ListTitle() As String
            Get
                Return _listtitle
            End Get
            Set(ByVal value As String)
                _listtitle = value.Trim
            End Set
        End Property

        Public Property MPAA() As String
            Get
                Return _mpaa
            End Get
            Set(ByVal value As String)
                _mpaa = value
            End Set
        End Property

        Public Property MultiViewCount() As String
            Get
                Return _multiviewcount
            End Get
            Set(ByVal value As String)
                _multiviewcount = value
            End Set
        End Property

        Public Property MultiViewLayout() As String
            Get
                Return _multiviewlayout
            End Get
            Set(ByVal value As String)
                _multiviewlayout = value
            End Set
        End Property

        Public Property NewFileName() As String
            Get
                Return _newfilename
            End Get
            Set(ByVal value As String)
                _newfilename = value.Trim
            End Set
        End Property

        Public Property NewPath() As String
            Get
                Return _newpath
            End Get
            Set(ByVal value As String)
                _newpath = value.Trim
            End Set
        End Property

        Public Property OldFileName() As String
            Get
                Return _oldfilename
            End Get
            Set(ByVal value As String)
                _oldfilename = value.Trim
            End Set
        End Property

        Public Property OldFullFileName() As String
            Get
                Return _oldfullfilename
            End Get
            Set(ByVal value As String)
                _oldfullfilename = value.Trim
            End Set
        End Property

        Public Property OldFullPath() As String
            Get
                Return _oldfullpath
            End Get
            Set(ByVal value As String)
                _oldfullpath = value.Trim
            End Set
        End Property

        Public Property OldPath() As String
            Get
                Return _oldpath
            End Get
            Set(ByVal value As String)
                _oldpath = value.Trim
            End Set
        End Property

        Public Property OriginalTitle() As String
            Get
                Return _originaltitle
            End Get
            Set(ByVal value As String)
                _originaltitle = value.Trim
            End Set
        End Property

        Public Property Parent() As String
            Get
                Return _parent
            End Get
            Set(ByVal value As String)
                _parent = value.Trim
            End Set
        End Property

        Public Property Path() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value.Trim
            End Set
        End Property

        Public Property Rating() As String
            Get
                Return _rating
            End Get
            Set(ByVal value As String)
                _rating = value
            End Set
        End Property

        Public Property Resolution() As String
            Get
                Return _resolution
            End Get
            Set(ByVal value As String)
                _resolution = value
            End Set
        End Property

        Public Property Country() As String
            Get
                Return _country
            End Get
            Set(ByVal value As String)
                _country = value.Trim
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value.Trim
            End Set
        End Property

        Public Property TVDBID() As String
            Get
                Return _tvdbid
            End Get
            Set(ByVal value As String)
                _tvdbid = value.Trim
            End Set
        End Property

        Public Property SeasonsEpisodes() As List(Of SeasonsEpisodes)
            Get
                Return _seasonsepisodes
            End Get
            Set(ByVal value As List(Of SeasonsEpisodes))
                _seasonsepisodes = value
            End Set
        End Property

        Public Property ShortStereoMode() As String
            Get
                Return _shortstereomode
            End Get
            Set(ByVal value As String)
                _shortstereomode = value.Trim
            End Set
        End Property

        Public Property ShowPath() As String
            Get
                Return _showpath
            End Get
            Set(ByVal value As String)
                _showpath = value.Trim
            End Set
        End Property

        Public Property ShowTitle() As String
            Get
                Return _showtitle
            End Get
            Set(ByVal value As String)
                _showtitle = value.Trim
            End Set
        End Property

        Public Property SortTitle() As String
            Get
                Return _sorttitle
            End Get
            Set(ByVal value As String)
                _sorttitle = value.Trim
            End Set
        End Property

        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value.Trim
            End Set
        End Property

        Public Property StereoMode() As String
            Get
                Return _stereomode
            End Get
            Set(ByVal value As String)
                _stereomode = value.Trim
            End Set
        End Property

        Public Property VideoCodec() As String
            Get
                Return _videocodec
            End Get
            Set(ByVal value As String)
                _videocodec = value
            End Set
        End Property

        Public Property Year() As String
            Get
                Return _year
            End Get
            Set(ByVal value As String)
                _year = value
            End Set
        End Property

        Public Property IMDB() As String
            Get
                Return _imdb
            End Get
            Set(ByVal value As String)
                _imdb = value.Trim
            End Set
        End Property

        Public Property Genre() As String
            Get
                Return _genre
            End Get
            Set(ByVal value As String)
                _genre = value.Trim
            End Set
        End Property

        Public Property Director() As String
            Get
                Return _director
            End Get
            Set(ByVal value As String)
                _director = value.Trim
            End Set
        End Property

        Public Property VideoSource() As String
            Get
                Return _videosource
            End Get
            Set(ByVal value As String)
                _videosource = value.Trim
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            Clear()
        End Sub

        Public Sub Clear()
            _aired = String.Empty
            _audiochannels = String.Empty
            _audiocodec = String.Empty
            _basepath = String.Empty
            _collection = String.Empty
            _country = String.Empty
            _direxist = False
            _director = String.Empty
            _dorename = False
            _extension = String.Empty
            _fileexist = False
            _videosource = String.Empty
            _genre = String.Empty
            _id = -1
            _imdb = String.Empty
            _ismultiepisode = False
            _issingle = False
            _isbdmv = False
            _islock = False
            _isvideots = False
            _listtitle = String.Empty
            _mpaa = String.Empty
            _multiviewcount = String.Empty
            _multiviewlayout = String.Empty
            _newfilename = String.Empty
            _newpath = String.Empty
            _oldfilename = String.Empty
            _oldfullfilename = String.Empty
            _oldfullpath = String.Empty
            _oldpath = String.Empty
            _originaltitle = String.Empty
            _parent = String.Empty
            _path = String.Empty
            _rating = String.Empty
            _resolution = String.Empty
            _seasonsepisodes.Clear()
            _showpath = String.Empty
            _showtitle = String.Empty
            _sorttitle = String.Empty
            _status = String.Empty
            _stereomode = String.Empty
            _title = String.Empty
            _tvdbid = String.Empty
            _videocodec = String.Empty
            _year = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Class SeasonsEpisodes

#Region "Fields"

        Private _season As Integer
        Private _episodes As List(Of Episode)

#End Region 'Fields

#Region "Properties"

        Public Property Season() As Integer
            Get
                Return _season
            End Get
            Set(ByVal value As Integer)
                _season = value
            End Set
        End Property

        Public Property Episodes() As List(Of Episode)
            Get
                Return _episodes
            End Get
            Set(ByVal value As List(Of Episode))
                _episodes = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            _season = -1
            _episodes = New List(Of Episode)
        End Sub

        Public Sub Clear()
            _season = -1
            _episodes.Clear()
        End Sub

#End Region 'Methods

    End Class

    Class Episode
        Implements IComparable(Of Episode)

#Region "Fields"

        Private _id As Integer
        Private _episode As Integer
        Private _subepisode As Integer
        Private _title As String

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

        Public Property Episode() As Integer
            Get
                Return _episode
            End Get
            Set(ByVal value As Integer)
                _episode = value
            End Set
        End Property

        Public Property SubEpisode() As Integer
            Get
                Return _subepisode
            End Get
            Set(ByVal value As Integer)
                _subepisode = value
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

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            _id = -1
            _episode = -1
            _subepisode = -1
            _title = String.Empty
        End Sub

        Public Sub Clear()
            _id = -1
            _episode = -1
            _subepisode = -1
            _title = String.Empty
        End Sub

        Public Function CompareTo(ByVal obj As Episode) As Integer Implements System.IComparable(Of Episode).CompareTo
            Return Episode.CompareTo(obj.Episode)
        End Function

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class