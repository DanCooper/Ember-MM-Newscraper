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
            SQLNewcommand.CommandText = String.Concat("SELECT Path FROM Sources;")
            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                While SQLReader.Read
                    MovieFolders.Add(If(SQLReader("Path").ToString.EndsWith(Path.DirectorySeparatorChar), SQLReader("Path").ToString, String.Concat(SQLReader("Path").ToString, Path.DirectorySeparatorChar)))
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
            SQLNewcommand.CommandText = String.Concat("SELECT Path FROM TVSources;")
            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                While SQLReader.Read
                    TVShowFolders.Add(If(SQLReader("Path").ToString.EndsWith(Path.DirectorySeparatorChar), SQLReader("Path").ToString, String.Concat(SQLReader("Path").ToString, Path.DirectorySeparatorChar)))
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

    Public Sub AddEpisode(ByVal _episode As FileRename)
        _episodes.Add(_episode)
    End Sub

    Public Sub AddMovie(ByVal _movie As FileRename)
        _movies.Add(_movie)
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

    Public Sub DoRename_Episodes(Optional ByVal sfunction As ShowProgress = Nothing)
        Dim _tvDB As Structures.DBTV = Nothing
        Dim iProg As Integer = 0
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each f As FileFolderRenamer.FileRename In _episodes.Where(Function(s) s.IsRenamed AndAlso Not s.FileExist AndAlso Not s.IsLocked)
                    iProg += 1
                    If Not f.ID = -1 Then
                        _tvDB = Master.DB.LoadTVEpFromDB(f.ID, True)
                        DoRenameSingle_Episode(f, _tvDB, True, False, False, True, sfunction, iProg)
                    End If
                Next
                SQLtransaction.Commit()
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub DoRename_Movies(Optional ByVal sfunction As ShowProgress = Nothing)
        Dim _movieDB As Structures.DBMovie = Nothing
        Dim iProg As Integer = 0
        Try
            For Each f As FileFolderRenamer.FileRename In _movies.Where(Function(s) s.IsRenamed AndAlso Not s.FileExist AndAlso Not s.IsLocked)
                iProg += 1
                If Not f.ID = -1 Then
                    _movieDB = Master.DB.LoadMovieFromDB(f.ID)
                    DoRenameSingle_Movie(f, _movieDB, True, False, False, True, sfunction, iProg)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub DoRenameSingle_Episode(ByVal _frename As FileRename, ByRef _tv As Structures.DBTV, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean, Optional ByVal sfunction As ShowProgress = Nothing, Optional ByVal iProg As Integer = 0)
        Try
            If Not _frename.IsLocked AndAlso Not _frename.FileExist Then
                Dim getError As Boolean = False
                Dim srcDir As String = Path.Combine(_frename.BasePath, _frename.Path)
                Dim destDir As String = Path.Combine(_frename.BasePath, _frename.NewPath)
                Dim srcFilenamePath As String = Path.Combine(_frename.BasePath, _frename.Path, _frename.FileName)
                Dim dstFilenamePath As String = Path.Combine(_frename.BasePath, _frename.NewPath, _frename.NewFileName)

                'Rename/Create Directory
                If Not srcDir = destDir Then
                    Try
                        If Not sfunction Is Nothing Then
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
                If Not _frename.IsVideo_TS AndAlso Not _frename.IsBDMV Then
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
                                dstFile = Path.Combine(destDir, lFile.Name.Replace(_frename.FileName.Trim, _frename.NewFileName.Trim))
                                If Not srcFile = dstFile Then
                                    Try
                                        If Not sfunction Is Nothing Then
                                            If Not sfunction(_frename.NewFileName, iProg) Then Return
                                        End If

                                        If srcFile.ToLower = dstFile.ToLower Then
                                            File.Move(srcFile, String.Concat(dstFile, ".$emm$"))
                                            File.Move(String.Concat(dstFile, ".$emm$"), dstFile)
                                        Else
                                            If lFile.Name.StartsWith(_frename.FileName, StringComparison.OrdinalIgnoreCase) Then
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
                    UpdatePaths_Episode(_tv, srcDir, destDir, _frename.FileName, _frename.NewFileName)

                    If toDB Then
                        Master.DB.SaveTVEpToDB(_tv, False, BatchMode, toNfo)
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

                        If fileCount = 0 AndAlso dirCount = 0 OrElse _
                            fileCount = 0 AndAlso dirCount = 1 AndAlso di.GetDirectories.First.Name = ".actors" Then
                            di.Delete(True)
                        End If
                    End If
                End If
            Else
                If ShowError Then
                    MessageBox.Show("Error", Master.eLang.GetString(171, "Unable to Rename File"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub DoRenameSingle_Movie(ByVal _frename As FileRename, ByRef _movie As Structures.DBMovie, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean, Optional ByVal sfunction As ShowProgress = Nothing, Optional ByVal iProg As Integer = 0)
        Try
            If Not _movie.IsLock AndAlso Not _frename.FileExist Then
                Dim getError As Boolean = False
                Dim srcDir As String = Path.Combine(_frename.BasePath, _frename.Path)
                Dim destDir As String = Path.Combine(_frename.BasePath, _frename.NewPath)
                Dim srcFilenamePath As String = Path.Combine(_frename.BasePath, _frename.Path, _frename.FileName)
                Dim dstFilenamePath As String = Path.Combine(_frename.BasePath, _frename.NewPath, _frename.NewFileName)

                'Rename Directory
                If Not srcDir = destDir Then
                    Try
                        If Not sfunction Is Nothing Then
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
                If Not getError AndAlso Not _frename.IsVideo_TS AndAlso Not _frename.IsBDMV Then
                    If (Not _frename.NewFileName = _frename.FileName) OrElse (_frename.Path = String.Empty AndAlso Not _frename.NewPath = String.Empty) OrElse Not _movie.IsSingle Then
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
                                dstFile = Path.Combine(destDir, lFile.Name.Replace(_frename.FileName.Trim, _frename.NewFileName.Trim))
                                If Not srcFile = dstFile Then
                                    Try
                                        If Not sfunction Is Nothing Then
                                            If Not sfunction(_frename.NewFileName, iProg) Then Return
                                        End If

                                        If srcFile.ToLower = dstFile.ToLower Then
                                            File.Move(srcFile, String.Concat(dstFile, ".$emm$"))
                                            File.Move(String.Concat(dstFile, ".$emm$"), dstFile)
                                        Else
                                            If lFile.Name.StartsWith(_frename.FileName, StringComparison.OrdinalIgnoreCase) Then
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
                    UpdatePaths_Movie(_movie, srcDir, destDir, _frename.FileName, _frename.NewFileName)

                    If toDB Then
                        Master.DB.SaveMovieToDB(_movie, False, BatchMode, toNfo)
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
                If ShowError Then
                    MessageBox.Show("Error", Master.eLang.GetString(171, "Unable to Rename File"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub DoRenameSingle_Show(ByVal _frename As FileRename, ByRef _tv As Structures.DBTV, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Try
            If Not _tv.IsLockEp AndAlso Not _frename.DirExist Then
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
                    UpdatePaths_Show(_tv, srcDir, destDir)

                    If toDB Then
                        Master.DB.SaveTVShowToDB(_tv, False, BatchMode, toNfo)
                        Master.DB.SaveTVSeasonToDB(_tv, BatchMode)
                    End If

                    Try
                        'first step: get a list of all seasons
                        Dim aSeasonsList As New List(Of Integer)
                        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLNewcommand.CommandText = String.Concat("SELECT Season FROM TVSeason WHERE TVShowID = ", _tv.ShowID, ";")
                            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                                While SQLReader.Read
                                    If Not aSeasonsList.Contains(Convert.ToInt32(SQLReader("Season"))) Then aSeasonsList.Add(Convert.ToInt32(SQLReader("Season")))
                                End While
                            End Using
                            aSeasonsList.Sort()
                        End Using

                        For Each aSeason In aSeasonsList
                            Dim tmpTV As New Structures.DBTV
                            tmpTV = Master.DB.LoadTVSeasonFromDB(_tv.ShowID, aSeason, False)
                            UpdatePaths_Show(tmpTV, srcDir, destDir)
                            Master.DB.SaveTVSeasonToDB(tmpTV, BatchMode)
                        Next

                        'second step: get all list of all episode paths
                        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLNewcommand.CommandText = String.Concat("SELECT ID, TVEpPath FROM TVEpPaths WHERE TVEpPath LIKE '", srcDir, "%';")
                            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                                While SQLReader.Read
                                    Dim oldPath As String = SQLReader("TVEpPath").ToString
                                    Using SQLCommandPath As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                        SQLCommandPath.CommandText = String.Concat("UPDATE TVEpPaths SET TVEpPath = (?) WHERE ID =", SQLReader("ID").ToString, ";")
                                        Dim parTVEpPath As SQLite.SQLiteParameter = SQLCommandPath.Parameters.Add("parTVEpPath", DbType.String, 0, "TVEpPath")
                                        parTVEpPath.Value = oldPath.Replace(srcDir, destDir)
                                        SQLCommandPath.ExecuteNonQuery()
                                    End Using
                                End While
                            End Using
                        End Using

                        'last step: get a list of all episodes
                        Dim aEpisodesList As New List(Of Integer)
                        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLNewcommand.CommandText = String.Concat("SELECT idEpisode FROM episode WHERE idShow = ", _tv.ShowID, ";")
                            Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                                While SQLReader.Read
                                    If Not aEpisodesList.Contains(Convert.ToInt32(SQLReader("idEpisode"))) Then aEpisodesList.Add(Convert.ToInt32(SQLReader("idEpisode")))
                                End While
                            End Using
                            aEpisodesList.Sort()
                        End Using

                        For Each aEpisode In aEpisodesList
                            Dim tmpTV As New Structures.DBTV
                            tmpTV = Master.DB.LoadTVEpFromDB(aEpisode, False)
                            UpdatePaths_Show(tmpTV, srcDir, destDir)
                            Master.DB.SaveTVEpToDB(tmpTV, False, BatchMode)
                        Next

                    Catch ex As Exception
                        logger.Error(New StackFrame().GetMethod().Name, ex)
                    End Try

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
                    MessageBox.Show("Error", Master.eLang.GetString(171, "Unable to Rename File"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Function GetCount_Episodes() As Integer
        Return _episodes.Count
    End Function

    Public Function GetCount_Movies() As Integer
        Return _movies.Count
    End Function

    Public Function GetCountLocked_Episodes() As Integer
        Dim c As Integer = c
        For Each f As FileRename In _episodes
            If f.IsLocked Then c += 1
        Next
        Return c
    End Function

    Public Function GetCountLocked_Movies() As Integer
        Dim c As Integer = c
        For Each f As FileRename In _movies
            If f.IsLocked Then c += 1
        Next
        Return c
    End Function

    Public Function GetEpisodes() As DataTable
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
            dtEpisodes.Rows.Add(dtRow.Title, dtRow.Path, dtRow.FileName, dtRow.NewPath, _
                              dtRow.NewFileName, dtRow.IsLocked, dtRow.DirExist, _
                              dtRow.FileExist, dtRow.IsSingle, dtRow.IsRenamed)
        Next

        Return dtEpisodes
    End Function

    Public Function GetMovies() As DataTable
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
            dtMovies.Rows.Add(dtRow.Title, dtRow.Path, dtRow.FileName, dtRow.NewPath, _
                              dtRow.NewFileName, dtRow.IsLocked, dtRow.DirExist, _
                              dtRow.FileExist, dtRow.IsSingle, dtRow.IsRenamed)
        Next

        Return dtMovies
    End Function

    Public Function GetEpisodesCount() As Integer
        Dim Renamed = From rList In _episodes Where rList.IsRenamed = True
        Return Renamed.Count
    End Function

    Public Function GetMoviesCount() As Integer
        Dim Renamed = From rList In _movies Where rList.IsRenamed = True
        Return Renamed.Count
    End Function

    Public Shared Sub Process_Episode(ByRef EpisodeFile As FileRename, ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String)
        Try
            Dim pSeason As String = ProccessPattern(EpisodeFile, folderPatternSeasons, True).Trim
            Dim nPath As String = Path.Combine(EpisodeFile.ShowPath, pSeason)

            If Not EpisodeFile.IsVideo_TS AndAlso Not EpisodeFile.IsBDMV Then
                If EpisodeFile.FileName.ToLower = "video_ts" Then
                    EpisodeFile.NewFileName = EpisodeFile.FileName
                Else
                    EpisodeFile.NewFileName = ProccessPattern(EpisodeFile, filePatternEpisodes, False, EpisodeFile.IsMultiEpisode).Trim
                End If
            ElseIf EpisodeFile.IsBDMV Then
                EpisodeFile.NewFileName = EpisodeFile.FileName
            Else
                EpisodeFile.NewFileName = EpisodeFile.FileName
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

            EpisodeFile.FileExist = File.Exists(Path.Combine(EpisodeFile.BasePath, Path.Combine(EpisodeFile.NewPath, String.Concat(EpisodeFile.NewFileName, EpisodeFile.Extension)))) AndAlso Not (EpisodeFile.FileName.ToLower = EpisodeFile.NewFileName.ToLower)
            EpisodeFile.DirExist = Directory.Exists(Path.Combine(EpisodeFile.BasePath, EpisodeFile.NewPath)) AndAlso Not (EpisodeFile.Path.ToLower = EpisodeFile.NewPath.ToLower)
            EpisodeFile.IsRenamed = Not EpisodeFile.NewPath = EpisodeFile.Path OrElse Not EpisodeFile.NewFileName = EpisodeFile.FileName
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub Process_Movie(ByRef MovieFile As FileRename, ByVal folderPattern As String, ByVal filePattern As String)
        Try
            If HaveBase(folderPattern) Then
                MovieFile.NewPath = ProccessPattern(MovieFile, If(MovieFile.IsSingle, folderPattern, "$D"), True).Trim
            Else
                MovieFile.NewPath = Path.Combine(MovieFile.OldPath, ProccessPattern(MovieFile, If(MovieFile.IsSingle, folderPattern, "$D"), True).Trim)
            End If

            If Not MovieFile.IsVideo_TS AndAlso Not MovieFile.IsBDMV Then
                If MovieFile.FileName.ToLower = "video_ts" Then
                    MovieFile.NewFileName = MovieFile.FileName
                Else
                    MovieFile.NewFileName = ProccessPattern(MovieFile, filePattern, False).Trim
                End If
            ElseIf MovieFile.IsBDMV Then
                MovieFile.NewFileName = MovieFile.FileName
            Else
                MovieFile.NewFileName = MovieFile.FileName
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

            MovieFile.FileExist = File.Exists(Path.Combine(MovieFile.BasePath, Path.Combine(MovieFile.NewPath, String.Concat(MovieFile.NewFileName, MovieFile.Extension)))) AndAlso Not (MovieFile.FileName.ToLower = MovieFile.NewFileName.ToLower)
            MovieFile.DirExist = Directory.Exists(Path.Combine(MovieFile.BasePath, MovieFile.NewPath)) AndAlso Not (MovieFile.Path.ToLower = MovieFile.NewPath.ToLower)
            MovieFile.IsRenamed = Not MovieFile.NewPath = MovieFile.Path OrElse Not MovieFile.NewFileName = MovieFile.FileName
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub Process_Show(ByRef ShowFile As FileRename, ByVal folderPatternShows As String)
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

            ShowFile.DirExist = Directory.Exists(Path.Combine(ShowFile.BasePath, ShowFile.NewPath)) AndAlso Not (ShowFile.Path.ToLower = ShowFile.NewPath.ToLower)
            ShowFile.IsRenamed = Not ShowFile.NewPath = ShowFile.Path
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Function GetInfo_Episode(ByVal _tmpTVEpisode As Structures.DBTV) As FileFolderRenamer.FileRename
        Dim EpisodeFile As New FileFolderRenamer.FileRename

        Try
            'get list of all episodes for multi-episode files
            Dim aSeasonsEpisodes As New List(Of SeasonsEpisodes)

            If Not _tmpTVEpisode.FilenameID = -1 Then

                'first step: get a list of all seasons
                Dim aSeasonsList As New List(Of Integer)
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT Season FROM episode WHERE TVEpPathID = ", _tmpTVEpisode.FilenameID, ";")
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
                        SQLNewcommand.CommandText = String.Concat("SELECT idEpisode, Episode, Title FROM episode WHERE TVEpPathID = ", _tmpTVEpisode.FilenameID, " AND Season = ", aSeason, ";")
                        Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                            While SQLReader.Read
                                Dim aEpisode As New Episode
                                aEpisode.ID = Convert.ToInt32(SQLReader("idEpisode"))
                                aEpisode.Episode = Convert.ToInt32(SQLReader("Episode"))
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

            EpisodeFile.ID = CInt(_tmpTVEpisode.EpID)

            'Aired
            If _tmpTVEpisode.TVEp.AiredSpecified Then
                EpisodeFile.Aired = _tmpTVEpisode.TVEp.Aired
            End If

            'Lock
            EpisodeFile.IsLocked = _tmpTVEpisode.IsLockEp

            'Show ListTitle
            If _tmpTVEpisode.ListTitle IsNot Nothing Then
                EpisodeFile.ListTitle = _tmpTVEpisode.ListTitle
            End If

            'Rating
            If Not EpisodeFile.IsMultiEpisode Then
                If _tmpTVEpisode.TVEp.Rating IsNot Nothing Then
                    EpisodeFile.Rating = _tmpTVEpisode.TVEp.Rating
                End If
            Else
                EpisodeFile.Rating = String.Empty
            End If

            'Episode Title
            If Not EpisodeFile.IsMultiEpisode Then
                If _tmpTVEpisode.TVEp.Title IsNot Nothing Then
                    EpisodeFile.Title = _tmpTVEpisode.TVEp.Title
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

            'Show Title
            If _tmpTVEpisode.TVShow.Title IsNot Nothing Then
                EpisodeFile.ShowTitle = _tmpTVEpisode.TVShow.Title
            End If

            'VideoSource
            If _tmpTVEpisode.TVEp.VideoSource IsNot Nothing Then
                EpisodeFile.VideoSource = _tmpTVEpisode.TVEp.VideoSource
            End If

            If _tmpTVEpisode.TVEp.FileInfo IsNot Nothing Then
                Try
                    'Resolution
                    If _tmpTVEpisode.TVEp.FileInfo.StreamDetails.Video.Count > 0 Then
                        Dim tVid As MediaInfo.Video = NFO.GetBestVideo(_tmpTVEpisode.TVEp.FileInfo)
                        Dim tRes As String = NFO.GetResFromDimensions(tVid)
                        EpisodeFile.Resolution = String.Format("{0}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes))
                    End If

                    If _tmpTVEpisode.TVEp.FileInfo.StreamDetails.Audio.Count > 0 Then
                        Dim tAud As MediaInfo.Audio = NFO.GetBestAudio(_tmpTVEpisode.TVEp.FileInfo, False)

                        'Audio Channels
                        If tAud.ChannelsSpecified Then
                            EpisodeFile.AudioChannels = String.Format("{0}ch", tAud.Channels)
                        End If

                        'AudioCodec
                        If tAud.CodecSpecified Then
                            EpisodeFile.AudioCodec = tAud.Codec
                        End If
                    End If

                    'MultiViewCount
                    If _tmpTVEpisode.TVEp.FileInfo.StreamDetails.Video.Count > 0 Then
                        If Not String.IsNullOrEmpty(_tmpTVEpisode.TVEp.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) AndAlso CDbl(_tmpTVEpisode.TVEp.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) > 1 Then
                            EpisodeFile.MultiViewCount = "3D"
                        End If
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End If

            Dim eFolders As New List(Of String)
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT Path FROM TVSources;")
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        eFolders.Add(SQLReader("Path").ToString)
                    End While
                End Using
            End Using
            eFolders.Sort()

            For Each i As String In eFolders
                If _tmpTVEpisode.Filename.StartsWith(i, StringComparison.OrdinalIgnoreCase) Then
                    EpisodeFile.BasePath = i
                    EpisodeFile.ShowPath = _tmpTVEpisode.ShowPath.Replace(i, String.Empty)
                    EpisodeFile.ShowPath = If(EpisodeFile.ShowPath.StartsWith(Path.DirectorySeparatorChar), EpisodeFile.ShowPath.Substring(1), EpisodeFile.ShowPath)
                    If FileUtils.Common.isVideoTS(_tmpTVEpisode.Filename) Then
                        EpisodeFile.Parent = Directory.GetParent(Directory.GetParent(_tmpTVEpisode.Filename).FullName).Name
                        If EpisodeFile.BasePath = Directory.GetParent(Directory.GetParent(_tmpTVEpisode.Filename).FullName).FullName Then
                            EpisodeFile.OldPath = String.Empty
                            EpisodeFile.BasePath = Directory.GetParent(EpisodeFile.BasePath).FullName
                        Else
                            EpisodeFile.OldPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpTVEpisode.Filename).FullName).FullName).FullName.Replace(i, String.Empty)
                        End If
                        EpisodeFile.IsVideo_TS = True
                    ElseIf FileUtils.Common.isBDRip(_tmpTVEpisode.Filename) Then
                        EpisodeFile.Parent = Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpTVEpisode.Filename).FullName).FullName).Name
                        If EpisodeFile.BasePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpTVEpisode.Filename).FullName).FullName).FullName Then
                            EpisodeFile.OldPath = String.Empty
                            EpisodeFile.BasePath = Directory.GetParent(EpisodeFile.BasePath).FullName
                        Else
                            EpisodeFile.OldPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpTVEpisode.Filename).FullName).FullName).FullName).FullName.Replace(i, String.Empty)
                        End If
                        EpisodeFile.IsBDMV = True
                    Else
                        EpisodeFile.Parent = Directory.GetParent(_tmpTVEpisode.Filename).FullName.Replace(Path.Combine(i, EpisodeFile.ShowPath), String.Empty).Trim
                        EpisodeFile.Path = Directory.GetParent(_tmpTVEpisode.Filename).FullName.Replace(i, String.Empty)
                    End If
                End If
            Next

            EpisodeFile.Parent = If(EpisodeFile.Parent.StartsWith(Path.DirectorySeparatorChar), EpisodeFile.Parent.Substring(1), EpisodeFile.Parent)
            EpisodeFile.Path = If(EpisodeFile.Path.StartsWith(Path.DirectorySeparatorChar), EpisodeFile.Path.Substring(1), EpisodeFile.Path)

            If Not EpisodeFile.IsVideo_TS AndAlso Not EpisodeFile.IsBDMV Then
                If Path.GetFileName(_tmpTVEpisode.Filename.ToLower) = "video_ts.ifo" Then
                    EpisodeFile.FileName = "VIDEO_TS"
                Else
                    EpisodeFile.FileName = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(_tmpTVEpisode.Filename))
                    Dim stackMark As String = Path.GetFileNameWithoutExtension(_tmpTVEpisode.Filename).Replace(EpisodeFile.FileName, String.Empty).ToLower
                    If Not stackMark = String.Empty AndAlso _tmpTVEpisode.TVEp.Title.ToLower.EndsWith(stackMark) Then
                        EpisodeFile.FileName = Path.GetFileNameWithoutExtension(_tmpTVEpisode.Filename)
                    End If
                End If
            ElseIf EpisodeFile.IsBDMV Then
                EpisodeFile.FileName = String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")
            Else
                EpisodeFile.FileName = "VIDEO_TS"
            End If

            EpisodeFile.Extension = Path.GetExtension(_tmpTVEpisode.Filename)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return EpisodeFile
    End Function

    Public Shared Function GetInfo_Movie(ByVal _tmpMovie As Structures.DBMovie) As FileFolderRenamer.FileRename
        Dim MovieFile As New FileFolderRenamer.FileRename

        Try
            MovieFile.ID = CInt(_tmpMovie.ID)

            'Title
            If String.IsNullOrEmpty(_tmpMovie.Movie.Title) Then
                MovieFile.Title = _tmpMovie.ListTitle
            Else
                MovieFile.Title = _tmpMovie.Movie.Title
            End If

            'SortTitle
            If String.IsNullOrEmpty(_tmpMovie.Movie.SortTitle) Then
                MovieFile.SortTitle = _tmpMovie.ListTitle
            Else
                MovieFile.SortTitle = _tmpMovie.Movie.SortTitle
            End If

            'MovieSets
            If _tmpMovie.Movie.Sets IsNot Nothing AndAlso _tmpMovie.Movie.Sets.Count > 0 Then
                MovieFile.Collection = _tmpMovie.Movie.Sets.Item(0).Title
            End If

            'Director
            If _tmpMovie.Movie.Director IsNot Nothing Then
                MovieFile.Director = _tmpMovie.Movie.Director
            End If

            'VideoSource
            If _tmpMovie.Movie.VideoSource IsNot Nothing Then
                MovieFile.VideoSource = _tmpMovie.Movie.VideoSource
            End If

            'Genres
            If _tmpMovie.Movie.Genre IsNot Nothing Then
                MovieFile.Genre = _tmpMovie.Movie.Genre
            End If

            'IMDBID
            If _tmpMovie.Movie.IMDBID IsNot Nothing Then
                MovieFile.IMDBID = _tmpMovie.Movie.IMDBID
            End If

            'IsLock
            MovieFile.IsLocked = _tmpMovie.IsLock

            'IsSingle
            MovieFile.IsSingle = _tmpMovie.IsSingle

            'ListTitle
            If _tmpMovie.ListTitle IsNot Nothing Then
                MovieFile.ListTitle = _tmpMovie.ListTitle
            End If

            'MPAA
            If _tmpMovie.Movie.MPAA IsNot Nothing Then
                MovieFile.MPAA = FileFolderRenamer.SelectMPAA(_tmpMovie.Movie.MPAA)
            End If

            'OriginalTitle
            If _tmpMovie.Movie.OriginalTitle IsNot Nothing Then
                MovieFile.OriginalTitle = If(_tmpMovie.Movie.OriginalTitle <> _tmpMovie.Movie.Title, _tmpMovie.Movie.OriginalTitle, String.Empty)
            End If

            'Rating
            If _tmpMovie.Movie.Rating IsNot Nothing Then
                MovieFile.Rating = _tmpMovie.Movie.Rating
            End If

            'Year
            If _tmpMovie.Movie.Year IsNot Nothing Then
                MovieFile.Year = _tmpMovie.Movie.Year
            End If

            If _tmpMovie.Movie.FileInfo IsNot Nothing Then
                Try
                    If _tmpMovie.Movie.FileInfo.StreamDetails.Video.Count > 0 Then
                        Dim tVid As MediaInfo.Video = NFO.GetBestVideo(_tmpMovie.Movie.FileInfo)
                        Dim tRes As String = NFO.GetResFromDimensions(tVid)
                        MovieFile.Resolution = String.Format("{0}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes))
                    End If

                    If _tmpMovie.Movie.FileInfo.StreamDetails.Audio.Count > 0 Then
                        Dim tAud As MediaInfo.Audio = NFO.GetBestAudio(_tmpMovie.Movie.FileInfo, False)

                        If tAud.ChannelsSpecified Then
                            MovieFile.AudioChannels = tAud.Channels
                        End If

                        If tAud.CodecSpecified Then
                            MovieFile.AudioCodec = tAud.Codec
                        End If
                    End If

                    If _tmpMovie.Movie.FileInfo.StreamDetails.Video.Count > 0 Then
                        If Not String.IsNullOrEmpty(_tmpMovie.Movie.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) AndAlso CDbl(_tmpMovie.Movie.FileInfo.StreamDetails.Video.Item(0).MultiViewCount) > 1 Then
                            MovieFile.MultiViewCount = "3D"
                        End If
                    End If

                    If _tmpMovie.Movie.FileInfo.StreamDetails.Video.Count > 0 Then
                        If _tmpMovie.Movie.FileInfo.StreamDetails.Video.Item(0).CodecSpecified Then
                            MovieFile.VideoCodec = _tmpMovie.Movie.FileInfo.StreamDetails.Video.Item(0).Codec
                        End If
                    End If

                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End If

            Dim mFolders As New List(Of String)
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT Path FROM Sources;")
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        mFolders.Add(SQLReader("Path").ToString)
                    End While
                End Using
            End Using
            mFolders.Sort()

            Dim tPath As String = String.Empty
            For Each i As String In mFolders
                If _tmpMovie.Filename.StartsWith(i, StringComparison.OrdinalIgnoreCase) Then
                    MovieFile.BasePath = i
                    If FileUtils.Common.isVideoTS(_tmpMovie.Filename) Then
                        MovieFile.Parent = Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).Name
                        If MovieFile.BasePath = Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).FullName Then
                            MovieFile.OldPath = String.Empty
                            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
                        Else
                            MovieFile.OldPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).FullName).FullName.Replace(i, String.Empty)
                        End If
                        MovieFile.IsVideo_TS = True
                    ElseIf FileUtils.Common.isBDRip(_tmpMovie.Filename) Then
                        MovieFile.Parent = Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).FullName).Name
                        If MovieFile.BasePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).FullName).FullName Then
                            MovieFile.OldPath = String.Empty
                            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
                        Else
                            MovieFile.OldPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).FullName).FullName).FullName.Replace(i, String.Empty)
                        End If
                        MovieFile.IsBDMV = True
                    Else
                        MovieFile.Parent = Directory.GetParent(_tmpMovie.Filename).Name
                        If MovieFile.BasePath = Directory.GetParent(_tmpMovie.Filename).FullName Then
                            MovieFile.OldPath = String.Empty
                            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
                        Else
                            MovieFile.OldPath = Directory.GetParent(Directory.GetParent(_tmpMovie.Filename).FullName).FullName.Replace(i, String.Empty)
                        End If
                    End If
                End If
            Next

            MovieFile.Path = Path.Combine(MovieFile.OldPath, MovieFile.Parent)
            MovieFile.Path = If(MovieFile.Path.StartsWith(Path.DirectorySeparatorChar), MovieFile.Path.Substring(1), MovieFile.Path)

            If Not MovieFile.IsVideo_TS AndAlso Not MovieFile.IsBDMV Then
                If Path.GetFileName(_tmpMovie.Filename.ToLower) = "video_ts.ifo" Then
                    MovieFile.FileName = "VIDEO_TS"
                Else
                    MovieFile.FileName = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(_tmpMovie.Filename))
                    Dim stackMark As String = Path.GetFileNameWithoutExtension(_tmpMovie.Filename).Replace(MovieFile.FileName, String.Empty).ToLower
                    If Not stackMark = String.Empty AndAlso _tmpMovie.Movie.Title.ToLower.EndsWith(stackMark) Then
                        MovieFile.FileName = Path.GetFileNameWithoutExtension(_tmpMovie.Filename)
                    End If
                End If
            ElseIf MovieFile.IsBDMV Then
                MovieFile.FileName = String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")
            Else
                MovieFile.FileName = "VIDEO_TS"
            End If

            MovieFile.Extension = Path.GetExtension(_tmpMovie.Filename)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return MovieFile
    End Function

    Public Shared Function GetInfo_Show(ByVal _tmpTVShow As Structures.DBTV) As FileFolderRenamer.FileRename
        Dim ShowFile As New FileFolderRenamer.FileRename

        Try
            ShowFile.ID = CInt(_tmpTVShow.ShowID)
            ShowFile.IsLocked = _tmpTVShow.IsLockShow

            If String.IsNullOrEmpty(_tmpTVShow.TVShow.Title) Then
                ShowFile.Title = _tmpTVShow.ListTitle
            Else
                ShowFile.Title = _tmpTVShow.TVShow.Title
            End If
            If _tmpTVShow.TVShow.Title IsNot Nothing Then
                ShowFile.ShowTitle = _tmpTVShow.TVShow.Title
            End If
            If _tmpTVShow.TVShow.Genre IsNot Nothing Then
                ShowFile.Genre = _tmpTVShow.TVShow.Genre
            End If
            If _tmpTVShow.TVShow.TVDBID IsNot Nothing Then
                ShowFile.TVDBID = _tmpTVShow.TVShow.TVDBID
            End If
            If _tmpTVShow.ListTitle IsNot Nothing Then
                ShowFile.ListTitle = _tmpTVShow.ListTitle
            End If
            If _tmpTVShow.TVShow.MPAA IsNot Nothing Then
                ShowFile.MPAA = FileFolderRenamer.SelectMPAA(_tmpTVShow.TVShow.MPAA)
            End If
            If _tmpTVShow.TVShow.Rating IsNot Nothing Then
                ShowFile.Rating = _tmpTVShow.TVShow.Rating
            End If

            Dim mFolders As New List(Of String)
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT Path FROM TVSources;")
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        mFolders.Add(SQLReader("Path").ToString)
                    End While
                End Using
            End Using
            mFolders.Sort()

            Dim tPath As String = String.Empty
            For Each i As String In mFolders
                If _tmpTVShow.ShowPath.StartsWith(i, StringComparison.OrdinalIgnoreCase) Then
                    ShowFile.BasePath = i
                    ShowFile.ShowPath = _tmpTVShow.ShowPath.Replace(i, String.Empty)
                    ShowFile.ShowPath = If(ShowFile.ShowPath.StartsWith(Path.DirectorySeparatorChar), ShowFile.ShowPath.Substring(1), ShowFile.ShowPath)
                    If ShowFile.BasePath = Directory.GetParent(_tmpTVShow.ShowPath).FullName Then
                        ShowFile.OldPath = String.Empty
                    Else
                        ShowFile.OldPath = Directory.GetParent(Directory.GetParent(_tmpTVShow.ShowPath).FullName).FullName.Replace(i, String.Empty)
                    End If
                End If
            Next

            ShowFile.Path = Path.Combine(ShowFile.OldPath, Path.GetFileName(_tmpTVShow.ShowPath))
            ShowFile.Path = If(ShowFile.Path.StartsWith(Path.DirectorySeparatorChar), ShowFile.Path.Substring(1), ShowFile.Path)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return ShowFile
    End Function

    Public Shared Function HaveBase(ByVal fPattern As String) As Boolean
        If fPattern.Contains("$B") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub ProccessFiles_Episodes(ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String)
        Try
            For Each f As FileRename In _episodes
                Process_Episode(f, folderPatternSeasons, filePatternEpisodes)
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub ProccessFiles_Movies(ByVal folderPattern As String, ByVal filePattern As String, Optional ByVal folderPatternIsNotSingle As String = "$D")
        Try
            For Each f As FileRename In _movies
                Process_Movie(f, If(f.IsSingle, folderPattern, folderPatternIsNotSingle), filePattern)
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
                        strCond = ApplyPattern(strCond, "A", f.AudioChannels)
                        strCond = ApplyPattern(strCond, "B", String.Empty) 'This is not need here, Only to HaveBase
                        strCond = ApplyPattern(strCond, "C", f.Director)
                        strCond = ApplyPattern(strCond, "D", f.Parent) '.Replace("\", String.Empty))
                        strCond = ApplyPattern(strCond, "E", f.SortTitle)
                        strCond = ApplyPattern(strCond, "F", f.FileName.Replace("\", String.Empty))
                        '                                G   Genres
                        strCond = ApplyPattern(strCond, "H", f.VideoCodec)
                        strCond = ApplyPattern(strCond, "I", If(Not String.IsNullOrEmpty(f.IMDBID), String.Concat("tt", f.IMDBID), String.Empty))
                        strCond = ApplyPattern(strCond, "J", f.AudioCodec)
                        '                                K   Season
                        strCond = ApplyPattern(strCond, "L", f.ListTitle)
                        strCond = ApplyPattern(strCond, "M", f.MPAA)
                        strCond = ApplyPattern(strCond, "N", f.Collection)
                        strCond = ApplyPattern(strCond, "O", f.OriginalTitle)
                        strCond = ApplyPattern(strCond, "P", If(Not String.IsNullOrEmpty(f.Rating), String.Format("{0:0.0}", CDbl(f.Rating)), String.Empty))
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
                        strNoFlags = Regex.Replace(strNoFlags, "\$((?:[12ABCDEFHIJKLMNOPQRSTVWY]|G[. -]|U[. -]?))", String.Empty) '"(?i)\$([DFTYRAS])"  "\$((?i:[DFTYRAS]))"
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
                pattern = ApplyPattern(pattern, "A", f.AudioChannels)
                pattern = ApplyPattern(pattern, "B", String.Empty) 'This is not need here, Only to HaveBase
                pattern = ApplyPattern(pattern, "C", f.Director)
                pattern = ApplyPattern(pattern, "D", f.Parent) '.Replace("\", String.Empty))
                pattern = ApplyPattern(pattern, "E", f.SortTitle)
                pattern = ApplyPattern(pattern, "F", f.FileName.Replace("\", String.Empty))
                '                                G   Genres
                pattern = ApplyPattern(pattern, "H", f.VideoCodec)
                pattern = ApplyPattern(pattern, "I", If(Not String.IsNullOrEmpty(f.IMDBID), String.Concat("tt", f.IMDBID), String.Empty))
                pattern = ApplyPattern(pattern, "J", f.AudioCodec)
                '                                K   Season
                pattern = ApplyPattern(pattern, "L", f.ListTitle)
                pattern = ApplyPattern(pattern, "M", f.MPAA)
                pattern = ApplyPattern(pattern, "N", f.Collection)
                pattern = ApplyPattern(pattern, "O", f.OriginalTitle)
                pattern = ApplyPattern(pattern, "P", If(Not String.IsNullOrEmpty(f.Rating), String.Format("{0:0.0}", CDbl(f.Rating)), String.Empty))
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
                                seString = String.Concat(seString, eSeparator, ePrefix, String.Format(eFormat, episode.Episode))
                            Next
                        Next

                        If Not String.IsNullOrEmpty(sSeparator) AndAlso seString.StartsWith(sSeparator) Then seString = seString.Remove(0, 1)

                        pattern = pattern.Replace(fPattern, seString)
                    End If
                End If

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
                                eString = String.Concat(eString, eSeparator, ePrefix, String.Format(eFormat, episode.Episode))
                            Next
                        Next

                        If Not String.IsNullOrEmpty(eSeparator) AndAlso eString.StartsWith(eSeparator) Then eString = eString.Remove(0, 1)

                        pattern = pattern.Replace(fPattern, eString)
                    End If
                End If

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

                nextC = pattern.IndexOf("$X")
                If Not nextC = -1 AndAlso pattern.Length > nextC + 2 Then
                    strCond = pattern.Substring(nextC + 2, 1)
                    pattern = pattern.Replace(String.Concat("$X", strCond), "")
                    pattern = pattern.Replace(" ", strCond)
                End If

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
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return String.Empty
        End Try
    End Function


    Public Sub RenameAfterUpdateDB_TV(ByVal tvSource As String, ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Try
            Dim EpisodeFile As New FileFolderRenamer.FileRename
            Dim _currShow As New Structures.DBTV

            ' Load new episodes using path from DB
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim _tmpPath As String = String.Empty
                Dim iProg As Integer = 0
                If String.IsNullOrEmpty(tvSource) Then
                    SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idEpisode) AS mcount FROM episode WHERE Missing = 0 AND New = 1;")
                Else
                    SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idEpisode) AS mcount FROM episode WHERE Missing = 0 AND New = 1 AND Source = '", tvSource, "';")
                End If
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLcount.HasRows AndAlso SQLcount.Read() Then
                        'Me.bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
                    End If
                End Using
                If String.IsNullOrEmpty(tvSource) Then
                    SQLNewcommand.CommandText = String.Concat("SELECT NfoPath, idEpisode FROM episode WHERE Missing = 0 AND New = 1 ORDER BY idShow ASC, Season ASC, Episode ASC;")
                Else
                    SQLNewcommand.CommandText = String.Concat("SELECT NfoPath, idEpisode FROM episode WHERE Missing = 0 AND New = 1 AND Source = '", tvSource, "' ORDER BY idShow ASC, Season ASC, Episode ASC;")
                End If
                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            Try
                                If Not DBNull.Value.Equals(SQLreader("NfoPath")) AndAlso Not DBNull.Value.Equals(SQLreader("idEpisode")) Then
                                    _tmpPath = SQLreader("NfoPath").ToString
                                    If Not String.IsNullOrEmpty(_tmpPath) Then

                                        EpisodeFile = New FileFolderRenamer.FileRename
                                        _currShow = Master.DB.LoadTVEpFromDB(Convert.ToInt32(SQLreader("idEpisode")), True)

                                        If Not _currShow.EpID = -1 AndAlso Not _currShow.ShowID = -1 AndAlso Not String.IsNullOrEmpty(_currShow.Filename) Then
                                            'Me.bwLoadInfo.ReportProgress(iProg, String.Concat(_currShow.TVShow.Title, ": ", _currShow.TVEp.Title))
                                            EpisodeFile = FileFolderRenamer.GetInfo_Episode(_currShow)
                                            AddEpisode(EpisodeFile)
                                        End If
                                    End If
                                End If
                            Catch ex As Exception
                                logger.Error(New StackFrame().GetMethod().Name, ex)
                            End Try
                            iProg += 1
                        End While
                    End If
                End Using
            End Using

            ProccessFiles_Episodes(folderPatternSeasons, filePatternEpisodes)
            DoRename_Episodes()

            'set the status back to "New"
            Using SQLcommand_update_episode As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                For Each episode In _episodes
                    SQLcommand_update_episode.CommandText = String.Format("UPDATE episode SET 'New'=1 WHERE idEpisode={0}", episode.ID)
                    SQLcommand_update_episode.ExecuteNonQuery()
                Next
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub RenameSingle_Episode(ByRef _tmpEpisode As Structures.DBTV, ByVal folderPatternSeasons As String, ByVal filePatternEpisodes As String, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Dim EpisodeFile As New FileRename

        EpisodeFile = GetInfo_Episode(_tmpEpisode)
        Process_Episode(EpisodeFile, folderPatternSeasons, filePatternEpisodes)

        If EpisodeFile.IsRenamed Then
            DoRenameSingle_Episode(EpisodeFile, _tmpEpisode, BatchMode, toNfo, ShowError, toDB)
        Else
            If toDB Then
                Master.DB.SaveTVEpToDB(_tmpEpisode, False, True, False)
            End If
        End If
    End Sub

    Public Shared Sub RenameSingle_Movie(ByRef _tmpMovie As Structures.DBMovie, ByVal folderPattern As String, ByVal filePattern As String, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Dim MovieFile As New FileRename

        MovieFile = GetInfo_Movie(_tmpMovie)
        Process_Movie(MovieFile, folderPattern, filePattern)

        If MovieFile.IsRenamed Then
            DoRenameSingle_Movie(MovieFile, _tmpMovie, BatchMode, toNfo, ShowError, toDB)
        Else
            If toDB Then
                Master.DB.SaveMovieToDB(_tmpMovie, False, True, False)
            End If
        End If
    End Sub

    Public Shared Sub RenameSingle_Show(ByRef _tmpShow As Structures.DBTV, ByVal folderPatternShows As String, ByVal BatchMode As Boolean, ByVal toNfo As Boolean, ByVal ShowError As Boolean, ByVal toDB As Boolean)
        Dim ShowFile As New FileRename

        ShowFile = GetInfo_Show(_tmpShow)
        Process_Show(ShowFile, folderPatternShows)

        If ShowFile.IsRenamed Then
            DoRenameSingle_Show(ShowFile, _tmpShow, BatchMode, toNfo, ShowError, toDB)
        Else
            If toDB Then
                Master.DB.SaveTVShowToDB(_tmpShow, BatchMode, False)
            End If
        End If
    End Sub

    Public Shared Function SelectMPAA(ByVal tMPAA As String) As String
        If Not String.IsNullOrEmpty(tMPAA) Then
            Try
                Dim strMPAA As String = tMPAA
                If strMPAA.ToLower.StartsWith("rated g") Then
                    Return "0"
                ElseIf strMPAA.ToLower.StartsWith("rated pg-13") Then
                    Return "13"
                ElseIf strMPAA.ToLower.StartsWith("rated pg") Then
                    Return "7"
                ElseIf strMPAA.ToLower.StartsWith("rated r") Then
                    Return "17"
                ElseIf strMPAA.ToLower.StartsWith("rated nc-17") Then
                    Return "17"
                ElseIf strMPAA.Contains(":") Then 'might be a certification
                    Dim tReturn As String = strMPAA.Split(Convert.ToChar(":")).Last
                    'just in case
                    For Each fnC As Char In Path.GetInvalidFileNameChars
                        tReturn = tReturn.Replace(fnC, String.Empty)
                    Next
                    For Each fC As Char In Path.GetInvalidPathChars
                        tReturn = tReturn.Replace(fC, String.Empty)
                    Next
                    Return tReturn
                Else
                    Return tMPAA
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Else
            Return String.Empty
        End If
        Return String.Empty
    End Function

    Public Sub SetIsLocked_Episodes(ByVal path As String, ByVal filename As String, ByVal lock As Boolean)
        For Each f As FileRename In _episodes
            If (f.Path = path AndAlso f.FileName = filename) OrElse filename = String.Empty Then f.IsLocked = lock
        Next
    End Sub

    Public Sub SetIsLocked_Movies(ByVal path As String, ByVal filename As String, ByVal lock As Boolean)
        For Each f As FileRename In _movies
            If (f.Path = path AndAlso f.FileName = filename) OrElse filename = String.Empty Then f.IsLocked = lock
        Next
    End Sub

    Private Shared Sub UpdatePaths_Episode(ByRef _DBE As Structures.DBTV, ByVal oldPath As String, ByVal newPath As String, ByVal oldFile As String, ByVal newFile As String)
        If Not String.IsNullOrEmpty(_DBE.EpFanartPath) Then _DBE.EpFanartPath = Path.Combine(Directory.GetParent(_DBE.EpFanartPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.EpFanartPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBE.Filename) Then _DBE.Filename = Path.Combine(Directory.GetParent(_DBE.Filename).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.Filename).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBE.EpNfoPath) Then _DBE.EpNfoPath = Path.Combine(Directory.GetParent(_DBE.EpNfoPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.EpNfoPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBE.EpPosterPath) Then _DBE.EpPosterPath = Path.Combine(Directory.GetParent(_DBE.EpPosterPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBE.EpPosterPath).Replace(oldFile, newFile))
        If _DBE.EpSubtitles.Count > 0 Then
            For Each subtitle In _DBE.EpSubtitles
                subtitle.SubsPath = Path.Combine(Directory.GetParent(subtitle.SubsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(subtitle.SubsPath).Replace(oldFile, newFile))
            Next
        End If
    End Sub

    Private Shared Sub UpdatePaths_Movie(ByRef _DBM As Structures.DBMovie, ByVal oldPath As String, ByVal newPath As String, ByVal oldFile As String, ByVal newFile As String)
        If Not String.IsNullOrEmpty(_DBM.BannerPath) Then _DBM.BannerPath = Path.Combine(Directory.GetParent(_DBM.BannerPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.BannerPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.ClearArtPath) Then _DBM.ClearArtPath = Path.Combine(Directory.GetParent(_DBM.ClearArtPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ClearArtPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.ClearLogoPath) Then _DBM.ClearLogoPath = Path.Combine(Directory.GetParent(_DBM.ClearLogoPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ClearLogoPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.DiscArtPath) Then _DBM.DiscArtPath = Path.Combine(Directory.GetParent(_DBM.DiscArtPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.DiscArtPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.EFanartsPath) Then _DBM.EFanartsPath = Path.Combine(Directory.GetParent(_DBM.EFanartsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.EFanartsPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.EThumbsPath) Then _DBM.EThumbsPath = Path.Combine(Directory.GetParent(_DBM.EThumbsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.EThumbsPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.FanartPath) Then _DBM.FanartPath = Path.Combine(Directory.GetParent(_DBM.FanartPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.FanartPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.Filename) Then _DBM.Filename = Path.Combine(Directory.GetParent(_DBM.Filename).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.Filename).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.LandscapePath) Then _DBM.LandscapePath = Path.Combine(Directory.GetParent(_DBM.LandscapePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.LandscapePath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.NfoPath) Then _DBM.NfoPath = Path.Combine(Directory.GetParent(_DBM.NfoPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.NfoPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.PosterPath) Then _DBM.PosterPath = Path.Combine(Directory.GetParent(_DBM.PosterPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.PosterPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.SubPath) Then _DBM.SubPath = Path.Combine(Directory.GetParent(_DBM.SubPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.SubPath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.ThemePath) Then _DBM.ThemePath = Path.Combine(Directory.GetParent(_DBM.ThemePath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.ThemePath).Replace(oldFile, newFile))
        If Not String.IsNullOrEmpty(_DBM.TrailerPath) Then _DBM.TrailerPath = Path.Combine(Directory.GetParent(_DBM.TrailerPath).FullName.Replace(oldPath, newPath), Path.GetFileName(_DBM.TrailerPath).Replace(oldFile, newFile))
        If _DBM.Subtitles.Count > 0 Then
            For Each subtitle In _DBM.Subtitles
                subtitle.SubsPath = Path.Combine(Directory.GetParent(subtitle.SubsPath).FullName.Replace(oldPath, newPath), Path.GetFileName(subtitle.SubsPath).Replace(oldFile, newFile))
            Next
        End If
    End Sub

    Private Shared Sub UpdatePaths_Show(ByRef _DBE As Structures.DBTV, ByVal oldPath As String, ByVal newPath As String)
        If _DBE.EpNfoPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.EpNfoPath) Then _DBE.EpNfoPath = _DBE.EpNfoPath.Replace(oldPath, newPath)
        If _DBE.EpFanartPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.EpFanartPath) Then _DBE.EpFanartPath = _DBE.EpFanartPath.Replace(oldPath, newPath)
        If _DBE.EpPosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.EpPosterPath) Then _DBE.EpPosterPath = _DBE.EpPosterPath.Replace(oldPath, newPath)
        If _DBE.SeasonBannerPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.SeasonBannerPath) Then _DBE.SeasonBannerPath = _DBE.SeasonBannerPath.Replace(oldPath, newPath)
        If _DBE.SeasonFanartPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.SeasonFanartPath) Then _DBE.SeasonFanartPath = _DBE.SeasonFanartPath.Replace(oldPath, newPath)
        If _DBE.SeasonLandscapePath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.SeasonLandscapePath) Then _DBE.SeasonLandscapePath = _DBE.SeasonLandscapePath.Replace(oldPath, newPath)
        If _DBE.SeasonPosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.SeasonPosterPath) Then _DBE.SeasonPosterPath = _DBE.SeasonPosterPath.Replace(oldPath, newPath)
        If _DBE.ShowBannerPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowBannerPath) Then _DBE.ShowBannerPath = _DBE.ShowBannerPath.Replace(oldPath, newPath)
        If _DBE.ShowCharacterArtPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowCharacterArtPath) Then _DBE.ShowCharacterArtPath = _DBE.ShowCharacterArtPath.Replace(oldPath, newPath)
        If _DBE.ShowClearArtPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowClearArtPath) Then _DBE.ShowClearArtPath = _DBE.ShowClearArtPath.Replace(oldPath, newPath)
        If _DBE.ShowClearLogoPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowClearLogoPath) Then _DBE.ShowClearLogoPath = _DBE.ShowClearLogoPath.Replace(oldPath, newPath)
        If _DBE.ShowEFanartsPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowEFanartsPath) Then _DBE.ShowEFanartsPath = _DBE.ShowEFanartsPath.Replace(oldPath, newPath)
        If _DBE.ShowFanartPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowFanartPath) Then _DBE.ShowFanartPath = _DBE.ShowFanartPath.Replace(oldPath, newPath)
        If _DBE.ShowLandscapePath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowLandscapePath) Then _DBE.ShowLandscapePath = _DBE.ShowLandscapePath.Replace(oldPath, newPath)
        If _DBE.ShowNfoPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowNfoPath) Then _DBE.ShowNfoPath = _DBE.ShowNfoPath.Replace(oldPath, newPath)
        If _DBE.ShowPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowPath) Then _DBE.ShowPath = _DBE.ShowPath.Replace(oldPath, newPath)
        If _DBE.ShowPosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowPosterPath) Then _DBE.ShowPosterPath = _DBE.ShowPosterPath.Replace(oldPath, newPath)
        If _DBE.ShowThemePath IsNot Nothing AndAlso Not String.IsNullOrEmpty(_DBE.ShowThemePath) Then _DBE.ShowThemePath = _DBE.ShowThemePath.Replace(oldPath, newPath)
        If _DBE.EpSubtitles IsNot Nothing AndAlso _DBE.EpSubtitles.Count > 0 Then
            For Each subtitle In _DBE.EpSubtitles
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
        Private _basePath As String
        Private _collection As String
        Private _country As String
        Private _dirExist As Boolean
        Private _director As String
        Private _extension As String
        Private _fileExist As Boolean
        Private _fileName As String
        Private _genre As String
        Private _id As Integer
        Private _imdbid As String
        Private _ismultiepisode As Boolean
        Private _isRenamed As Boolean
        Private _isSingle As Boolean
        Private _isbdmv As Boolean
        Private _islocked As Boolean
        Private _isvideo_ts As Boolean
        Private _listtitle As String
        Private _mpaa As String
        Private _multiviewcount As String
        Private _multiviewlayout As String
        Private _newFileName As String
        Private _newPath As String
        Private _oldpath As String
        Private _originalTitle As String
        Private _parent As String
        Private _path As String
        Private _rating As String
        Private _resolution As String
        Private _seasonsepisodes As New List(Of SeasonsEpisodes)
        Private _showpath As String
        Private _showtitle As String
        Private _sorttitle As String
        Private _status As String
        Private _title As String
        Private _tvdbid As String
        Private _videocodec As String
        Private _videosource As String
        Private _year As String

#End Region 'Fields

#Region "Properties"

        Public Property Aired() As String
            Get
                Return Me._aired
            End Get
            Set(ByVal value As String)
                Me._aired = value
            End Set
        End Property

        Public Property AudioChannels() As String
            Get
                Return Me._audiochannels
            End Get
            Set(ByVal value As String)
                Me._audiochannels = value
            End Set
        End Property

        Public Property AudioCodec() As String
            Get
                Return Me._audiocodec
            End Get
            Set(ByVal value As String)
                Me._audiocodec = value
            End Set
        End Property

        Public Property BasePath() As String
            Get
                Return Me._basePath
            End Get
            Set(ByVal value As String)
                _basePath = value
            End Set
        End Property

        Public Property Collection() As String
            Get
                Return Me._collection
            End Get
            Set(ByVal value As String)
                _collection = value
            End Set
        End Property

        Public Property DirExist() As Boolean
            Get
                Return Me._dirExist
            End Get
            Set(ByVal value As Boolean)
                Me._dirExist = value
            End Set
        End Property

        Public Property Extension() As String
            Get
                Return Me._extension
            End Get
            Set(ByVal value As String)
                Me._extension = value.Trim
            End Set
        End Property

        Public Property FileExist() As Boolean
            Get
                Return Me._fileExist
            End Get
            Set(ByVal value As Boolean)
                Me._fileExist = value
            End Set
        End Property

        Public Property FileName() As String
            Get
                Return Me._fileName
            End Get
            Set(ByVal value As String)
                Me._fileName = value.Trim
            End Set
        End Property

        Public Property ID() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property IsBDMV() As Boolean
            Get
                Return Me._isbdmv
            End Get
            Set(ByVal value As Boolean)
                _isbdmv = value
            End Set
        End Property

        Public Property IsLocked() As Boolean
            Get
                Return Me._islocked
            End Get
            Set(ByVal value As Boolean)
                Me._islocked = value
            End Set
        End Property

        Public Property IsMultiEpisode() As Boolean
            Get
                Return Me._ismultiepisode
            End Get
            Set(ByVal value As Boolean)
                Me._ismultiepisode = value
            End Set
        End Property

        Public Property IsRenamed() As Boolean
            Get
                Return Me._isRenamed
            End Get
            Set(ByVal value As Boolean)
                Me._isRenamed = value
            End Set
        End Property

        Public Property IsSingle() As Boolean
            Get
                Return Me._isSingle
            End Get
            Set(ByVal value As Boolean)
                Me._isSingle = value
            End Set
        End Property

        Public Property IsVideo_TS() As Boolean
            Get
                Return Me._isvideo_ts
            End Get
            Set(ByVal value As Boolean)
                _isvideo_ts = value
            End Set
        End Property

        Public Property ListTitle() As String
            Get
                Return Me._listtitle
            End Get
            Set(ByVal value As String)
                Me._listtitle = value.Trim
            End Set
        End Property

        Public Property MPAA() As String
            Get
                Return Me._mpaa
            End Get
            Set(ByVal value As String)
                Me._mpaa = value
            End Set
        End Property

        Public Property MultiViewCount() As String
            Get
                Return Me._multiviewcount
            End Get
            Set(ByVal value As String)
                Me._multiviewcount = value
            End Set
        End Property

        Public Property MultiViewLayout() As String
            Get
                Return Me._multiviewlayout
            End Get
            Set(ByVal value As String)
                Me._multiviewlayout = value
            End Set
        End Property

        Public Property NewFileName() As String
            Get
                Return Me._newFileName
            End Get
            Set(ByVal value As String)
                Me._newFileName = value.Trim
            End Set
        End Property

        Public Property NewPath() As String
            Get
                Return Me._newPath
            End Get
            Set(ByVal value As String)
                Me._newPath = value.Trim
            End Set
        End Property

        Public Property OldPath() As String
            Get
                Return Me._oldpath
            End Get
            Set(ByVal value As String)
                Me._oldpath = value.Trim
            End Set
        End Property

        Public Property OriginalTitle() As String
            Get
                Return Me._originalTitle
            End Get
            Set(ByVal value As String)
                Me._originalTitle = value.Trim
            End Set
        End Property

        Public Property Parent() As String
            Get
                Return Me._parent
            End Get
            Set(ByVal value As String)
                Me._parent = value.Trim
            End Set
        End Property

        Public Property Path() As String
            Get
                Return Me._path
            End Get
            Set(ByVal value As String)
                Me._path = value.Trim
            End Set
        End Property

        Public Property Rating() As String
            Get
                Return Me._rating
            End Get
            Set(ByVal value As String)
                Me._rating = value
            End Set
        End Property

        Public Property Resolution() As String
            Get
                Return Me._resolution
            End Get
            Set(ByVal value As String)
                Me._resolution = value
            End Set
        End Property

        Public Property Country() As String
            Get
                Return Me._country
            End Get
            Set(ByVal value As String)
                Me._country = value.Trim
            End Set
        End Property

        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value.Trim
            End Set
        End Property

        Public Property TVDBID() As String
            Get
                Return Me._tvdbid
            End Get
            Set(ByVal value As String)
                Me._tvdbid = value.Trim
            End Set
        End Property

        Public Property SeasonsEpisodes() As List(Of SeasonsEpisodes)
            Get
                Return Me._seasonsepisodes
            End Get
            Set(ByVal value As List(Of SeasonsEpisodes))
                Me._seasonsepisodes = value
            End Set
        End Property

        Public Property ShowPath() As String
            Get
                Return Me._showpath
            End Get
            Set(ByVal value As String)
                Me._showpath = value.Trim
            End Set
        End Property

        Public Property ShowTitle() As String
            Get
                Return Me._showtitle
            End Get
            Set(ByVal value As String)
                Me._showtitle = value.Trim
            End Set
        End Property

        Public Property SortTitle() As String
            Get
                Return Me._sorttitle
            End Get
            Set(ByVal value As String)
                Me._sorttitle = value.Trim
            End Set
        End Property

        Public Property Status() As String
            Get
                Return Me._status
            End Get
            Set(ByVal value As String)
                Me._status = value.Trim
            End Set
        End Property

        Public Property VideoCodec() As String
            Get
                Return Me._videocodec
            End Get
            Set(ByVal value As String)
                Me._videocodec = value
            End Set
        End Property

        Public Property Year() As String
            Get
                Return Me._year
            End Get
            Set(ByVal value As String)
                Me._year = value
            End Set
        End Property

        Public Property IMDBID() As String
            Get
                Return Me._imdbid
            End Get
            Set(ByVal value As String)
                Me._imdbid = value.Trim
            End Set
        End Property

        Public Property Genre() As String
            Get
                Return Me._genre
            End Get
            Set(ByVal value As String)
                Me._genre = value.Trim
            End Set
        End Property

        Public Property Director() As String
            Get
                Return Me._director
            End Get
            Set(ByVal value As String)
                Me._director = value.Trim
            End Set
        End Property

        Public Property VideoSource() As String
            Get
                Return Me._videosource
            End Get
            Set(ByVal value As String)
                Me._videosource = value.Trim
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
            _basePath = String.Empty
            _collection = String.Empty
            _country = String.Empty
            _dirExist = False
            _director = String.Empty
            _extension = String.Empty
            _fileExist = False
            _fileName = String.Empty
            _videosource = String.Empty
            _genre = String.Empty
            _id = -1
            _imdbid = String.Empty
            _ismultiepisode = False
            _isRenamed = False
            _isSingle = False
            _isbdmv = False
            _islocked = False
            _isvideo_ts = False
            _listtitle = String.Empty
            _mpaa = String.Empty
            _multiviewcount = String.Empty
            _multiviewlayout = String.Empty
            _newFileName = String.Empty
            _newPath = String.Empty
            _oldpath = String.Empty
            _originalTitle = String.Empty
            _parent = String.Empty
            _path = String.Empty
            _rating = String.Empty
            _resolution = String.Empty
            _seasonsepisodes.Clear()
            _showpath = String.Empty
            _showtitle = String.Empty
            _sorttitle = String.Empty
            _status = String.Empty
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
                Return Me._season
            End Get
            Set(ByVal value As Integer)
                Me._season = value
            End Set
        End Property

        Public Property Episodes() As List(Of Episode)
            Get
                Return Me._episodes
            End Get
            Set(ByVal value As List(Of Episode))
                Me._episodes = value
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
        Private _title As String

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

        Public Property Episode() As Integer
            Get
                Return Me._episode
            End Get
            Set(ByVal value As Integer)
                Me._episode = value
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

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            _id = -1
            _episode = -1
            _title = String.Empty
        End Sub

        Public Sub Clear()
            _id = -1
            _episode = -1
            _title = String.Empty
        End Sub

        Public Function CompareTo(ByVal obj As Episode) As Integer Implements System.IComparable(Of Episode).CompareTo
            Dim c1 As Integer = Me.Episode.CompareTo(obj.Episode)
            If c1 <> 0 Then Return c1
        End Function

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class