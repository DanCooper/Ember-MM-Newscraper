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
Imports System.Windows.Forms
Imports NLog

Namespace FileUtils

    Public Class CleanUp

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Delegates"

        Public Delegate Function ReportProgress(ByVal iProgress As Integer, ByVal strMessage As String) As Boolean

#End Region 'Delegates

#Region "Methods"

        Public Shared Function DoCleanUp(Optional ByVal sfunction As ReportProgress = Nothing) As Boolean
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLCommand.CommandText = String.Format("SELECT idMovie FROM movie ORDER BY Title ASC;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader()
                        While SQLReader.Read()
                            Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt32(SQLReader("idMovie")))
                            Dim fScanner As New Scanner
                            fScanner.GetFolderContents_Movie(tmpMovie, True)
                            Master.DB.Save_Movie(tmpMovie, True, True, True, True, True)
                        End While
                    End Using
                End Using
                SQLtransaction.Commit()
            End Using

            Return True
        End Function

#End Region 'Methods

    End Class

    Public Class Common

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Shared Sub CopyFanartToBackdropsPath(ByVal strSourceFilename As String, ByVal ContentType As Enums.ContentType)
            If Not String.IsNullOrEmpty(strSourceFilename) AndAlso File.Exists(strSourceFilename) Then
                Dim strDestinationFilename As String = String.Empty
                Select Case ContentType
                    Case Enums.ContentType.Movie
                        If String.IsNullOrEmpty(Master.eSettings.MovieBackdropsPath) Then Return

                        If isVideoTS(strSourceFilename) Then
                            strDestinationFilename = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.Combine(GetMainPath(strSourceFilename).FullName, GetMainPath(strSourceFilename).Name), "-fanart.jpg"))
                        ElseIf isBDRip(strSourceFilename) Then
                            strDestinationFilename = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(GetMainPath(strSourceFilename).Name, "-fanart.jpg"))
                        Else
                            strDestinationFilename = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(GetMainPath(strSourceFilename).Name, "-fanart.jpg"))
                        End If
                End Select

                If Not String.IsNullOrEmpty(strDestinationFilename) Then
                    Try
                        If Not Directory.Exists(Directory.GetParent(strDestinationFilename).FullName) Then
                            Directory.CreateDirectory(Directory.GetParent(strDestinationFilename).FullName)
                        End If
                        File.Copy(strSourceFilename, strDestinationFilename, True)
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                End If
            End If
        End Sub

        Public Shared Sub DirectoryCopy(
                                       ByVal sourcePath As String,
                                       ByVal destinationPath As String,
                                       ByVal withSubDirs As Boolean,
                                       ByVal overwriteFiles As Boolean)

            If Not Directory.Exists(sourcePath) Then
                logger.Error(String.Concat("Source directory does not exist: ", sourcePath))
                Exit Sub
            End If

            ' Get the subdirectories for the specified directory.
            Dim dir As DirectoryInfo = New DirectoryInfo(sourcePath)
            Dim subdirs As DirectoryInfo() = dir.GetDirectories()

            ' If the destination directory doesn't exist, create it.
            If Not Directory.Exists(destinationPath) Then
                Directory.CreateDirectory(destinationPath)
            End If

            ' Get the files in the directory and copy them to the new location.
            Dim files As FileInfo() = dir.GetFiles()
            For Each cFile In files
                Dim temppath As String = Path.Combine(destinationPath, cFile.Name)
                If Not File.Exists(temppath) OrElse overwriteFiles Then
                    cFile.CopyTo(temppath, overwriteFiles)
                End If
            Next cFile

            ' If copying subdirectories, copy them and their contents to new location.
            If withSubDirs Then
                For Each subdir In subdirs
                    Dim temppath As String = Path.Combine(destinationPath, subdir.Name)
                    DirectoryCopy(subdir.FullName, temppath, withSubDirs, overwriteFiles)
                Next subdir
            End If
        End Sub

        Public Shared Sub DirectoryMove(
                                       ByVal sourcePath As String,
                                       ByVal destinationPath As String,
                                       ByVal withSubDirs As Boolean,
                                       ByVal overwriteFiles As Boolean)

            If Not Directory.Exists(sourcePath) Then
                logger.Error(String.Concat("Source directory does not exist: ", sourcePath))
                Exit Sub
            End If

            If Path.GetPathRoot(sourcePath).ToLower = Path.GetPathRoot(destinationPath).ToLower AndAlso withSubDirs Then
                If Not Directory.Exists(Directory.GetParent(destinationPath).FullName) Then Directory.CreateDirectory(Directory.GetParent(destinationPath).FullName)
                Directory.Move(sourcePath, destinationPath)
            Else
                ' Get the subdirectories for the specified directory.
                Dim dir As DirectoryInfo = New DirectoryInfo(sourcePath)
                Dim subdirs As DirectoryInfo() = dir.GetDirectories()

                ' If the destination directory doesn't exist, create it.
                If Not Directory.Exists(destinationPath) Then
                    Directory.CreateDirectory(destinationPath)
                End If

                ' Get the files in the directory and move them to the new location.
                Dim files As FileInfo() = dir.GetFiles()
                For Each cFile In files
                    Dim temppath As String = Path.Combine(destinationPath, cFile.Name)
                    If Not File.Exists(temppath) OrElse overwriteFiles Then
                        If File.Exists(temppath) Then File.Delete(temppath)
                        cFile.MoveTo(temppath)
                    End If
                Next

                ' If copying subdirectories, copy them and their contents to new location.
                If withSubDirs Then
                    For Each subdir In subdirs
                        Dim temppath As String = Path.Combine(destinationPath, subdir.Name)
                        DirectoryMove(subdir.FullName, temppath, withSubDirs, overwriteFiles)
                    Next subdir
                End If

                If dir.GetFiles.Count = 0 AndAlso dir.GetDirectories.Count = 0 Then
                    Directory.Delete(dir.FullName)
                End If
            End If
        End Sub

        Public Shared Sub FileCopy(
                                  ByVal sourcePath As String,
                                  ByVal destinationPath As String,
                                  ByVal overwriteFiles As Boolean)

            Dim fiSource As New FileInfo(sourcePath)
            Dim fiDestination As New FileInfo(destinationPath)
            If fiSource.Exists Then
                If Not fiDestination.Exists OrElse overwriteFiles Then
                    fiSource.CopyTo(fiDestination.FullName, overwriteFiles)
                End If
            Else
                logger.Error(String.Concat("Source file does not exist: ", fiSource.FullName))
                Exit Sub
            End If
        End Sub

        Public Shared Sub FileMove(
                                  ByVal sourcePath As String,
                                  ByVal destinationPath As String,
                                  ByVal overwriteFiles As Boolean)

            Dim fiSource As New FileInfo(sourcePath)
            Dim fiDestination As New FileInfo(destinationPath)
            If fiSource.Exists Then
                If Not fiDestination.Exists OrElse overwriteFiles Then
                    fiSource.MoveTo(fiDestination.FullName)
                End If
            Else
                logger.Error(String.Concat("Source file does not exist: ", fiSource.FullName))
                Exit Sub
            End If
        End Sub

        Public Shared Function GetAllItemsOfDBElement(ByVal tDBElement As Database.DBElement) As List(Of FileSystemInfo)
            Dim lstItems As New List(Of FileSystemInfo)

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.TVEpisode
                    If tDBElement.FilenameSpecified Then
                        If tDBElement.IsSingle OrElse isBDRip(tDBElement.Filename) OrElse isVideoTS(tDBElement.Filename) Then
                            lstItems.Add(GetMainPath(tDBElement.Filename))
                        Else
                            Select Case tDBElement.ContentType
                                Case Enums.ContentType.Movie
                                    lstItems.Add(New FileInfo(tDBElement.Filename))
                                    Dim lstFiles As New List(Of String)
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainBanner))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainClearArt))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainClearLogo))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainDiscArt))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainFanart))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainLandscape))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainNFO))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainPoster))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainSubtitle))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTheme))
                                    lstFiles.AddRange(GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTrailer))
                                    lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                                    lstFiles.Sort()
                                    For Each nFile In lstFiles
                                        Dim nFileinfo As New FileInfo(nFile)
                                        If nFileinfo.Exists Then lstItems.Add(nFileinfo)
                                    Next

                                Case Enums.ContentType.TVEpisode
                                    lstItems.Add(New FileInfo(tDBElement.Filename))
                                    Dim lstFiles As New List(Of String)
                                    lstFiles.AddRange(GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeFanart))
                                    lstFiles.AddRange(GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeNFO))
                                    lstFiles.AddRange(GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodePoster))
                                    lstFiles.AddRange(GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeSubtitle))
                                    lstFiles.AddRange(GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeWatchedFile))
                                    lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                                    lstFiles.Sort()
                                    For Each nFile In lstFiles
                                        Dim nFileinfo As New FileInfo(nFile)
                                        If nFileinfo.Exists Then lstItems.Add(nFileinfo)
                                    Next
                            End Select
                        End If
                    End If


                Case Enums.ContentType.Movieset
                    Dim lstFiles As New List(Of String)
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainBanner))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainClearArt))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainClearLogo))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainDiscArt))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainFanart))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainLandscape))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainNFO))
                    lstFiles.AddRange(GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainPoster))
                    lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                    lstFiles.Sort()
                    For Each nFile In lstFiles
                        Dim nFileinfo As New FileInfo(nFile)
                        If nFileinfo.Exists Then lstItems.Add(nFileinfo)
                    Next

                Case Enums.ContentType.TVSeason
                    For Each nEpisode In tDBElement.Episodes
                        lstItems.AddRange(GetAllItemsOfDBElement(nEpisode))
                    Next
                    Dim lstFiles As New List(Of String)
                    If tDBElement.TVSeason.IsAllSeasons Then
                        lstFiles.AddRange(GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsBanner))
                        lstFiles.AddRange(GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsFanart))
                        lstFiles.AddRange(GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsLandscape))
                        lstFiles.AddRange(GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsPoster))
                        lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                        lstFiles.Sort()
                    Else
                        lstFiles.AddRange(GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonBanner))
                        lstFiles.AddRange(GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonFanart))
                        lstFiles.AddRange(GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonLandscape))
                        lstFiles.AddRange(GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonNFO))
                        lstFiles.AddRange(GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonPoster))
                        lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                        lstFiles.Sort()
                    End If
                    For Each nFile In lstFiles
                        Dim nFileinfo As New FileInfo(nFile)
                        If nFileinfo.Exists Then lstItems.Add(nFileinfo)
                    Next

                Case Enums.ContentType.TVShow
                    lstItems.Add(New DirectoryInfo(tDBElement.ShowPath))
            End Select

            Return lstItems
        End Function
        ''' <summary>
        ''' Determine the lowest-level directory from the supplied path string. 
        ''' </summary>
        ''' <param name="sPath">The path string to parse</param>
        ''' <returns>String containing a directory name, or String.Empty if no valid directory name was found</returns>
        ''' <remarks>Passing a path to a filename will treat that filename as a path. </remarks>
        Public Shared Function GetDirectory(ByVal sPath As String) As String
            'TODO Need to evaluate more actual EMM uses of this method. I'm not confident in my understanding of what it is actually trying to accomplish. It seems overly complex for such a simple role
            'Why not .split on DirectorySeparatorChar and use the last non-Empty string?
            Try
                If String.IsNullOrEmpty(sPath) Then Return String.Empty
                If sPath.EndsWith(Path.DirectorySeparatorChar) Then sPath = sPath.Substring(0, sPath.Length - 1)
                If Not String.IsNullOrEmpty(Path.GetDirectoryName(sPath)) AndAlso sPath.StartsWith(Path.GetDirectoryName(sPath)) Then sPath = sPath.Replace(Path.GetDirectoryName(sPath), String.Empty).Trim
                If sPath.StartsWith(Path.DirectorySeparatorChar) Then sPath = sPath.Substring(1)
                'it could be just a drive letter at this point. Check ending chars again
                If sPath.EndsWith(Path.DirectorySeparatorChar) Then sPath = sPath.Substring(0, sPath.Length - 1)
                If sPath.EndsWith(":") Then sPath = sPath.Substring(0, sPath.Length - 1)
                Return sPath
            Catch ex As Exception
                Return String.Empty
            End Try
        End Function
        ''' <summary>
        ''' Given a path, determine whether it is a Blu-Ray or DVD folder. Find the respective media files within, and return the
        ''' largest one that is over 1 GB, or String.Empty otherwise.
        ''' </summary>
        ''' <param name="sPath">Path to Blu-Ray or DVD files.</param>
        ''' <param name="ForceBDMV">Assume path holds Blu-Ray files if <c>True</c></param>
        ''' <returns>Path/filename to the largest media file for the detected video type</returns>
        ''' <remarks>
        '''  2016/01/26  Cocotus - Remove 1GB limit from query
        ''' </remarks>
        Public Shared Function GetLongestFromRip(ByVal sPath As String, Optional ByVal ForceBDMV As Boolean = False) As String
            'TODO Needs error handling for when largest file is under 1GB. No default is set. Also, should error if path is not DVD or BR. Also, if ForceBDMV, complain if no files found
            Dim lFileList As New List(Of FileInfo)
            Select Case True
                Case isBDRip(sPath) OrElse ForceBDMV
                    lFileList.AddRange(New DirectoryInfo(Directory.GetParent(sPath).FullName).GetFiles("*.m2ts"))
                Case isVideoTS(sPath)
                    lFileList.AddRange(New DirectoryInfo(Directory.GetParent(sPath).FullName).GetFiles("*.vob"))
            End Select
            'Return filename/path of the largest file
            Return lFileList.OrderByDescending(Function(s) s.Length).Select(Function(s) s.FullName).FirstOrDefault
        End Function
        ''' <summary>
        ''' Returned the main folder that contains the file
        ''' </summary>
        ''' <param name="strFilenameFullPath">File name with full path</param>
        ''' <returns></returns>
        Public Shared Function GetMainPath(ByVal strFilenameFullPath As String) As DirectoryInfo
            If isBDRip(strFilenameFullPath) Then
                Return Directory.GetParent(Directory.GetParent(Directory.GetParent(strFilenameFullPath).FullName).FullName)
            ElseIf isVideoTS(strFilenameFullPath) Then
                Return Directory.GetParent(Directory.GetParent(strFilenameFullPath).FullName)
            Else
                Return Directory.GetParent(strFilenameFullPath)
            End If
        End Function

        Public Shared Function GetOpenFileDialogFilter_Theme() As String
            Dim lstValidExtensions As New List(Of String)

            For Each nExtension In Master.eSettings.FileSystemValidThemeExts
                lstValidExtensions.Add(String.Concat("*", nExtension))
            Next

            Return String.Concat(Master.eLang.GetString(1285, "Themes"), "|", String.Join(";", lstValidExtensions.ToArray))
        End Function

        Public Shared Function GetOpenFileDialogFilter_Video(ByVal strDescription As String) As String
            Dim lstValidExtensions As New List(Of String)

            For Each nExtension In Master.eSettings.FileSystemValidExts
                lstValidExtensions.Add(String.Concat("*", nExtension))
            Next

            Return String.Concat(strDescription, "|", String.Join(";", lstValidExtensions.ToArray))
        End Function

        ''' <summary>
        ''' Determine whether the path provided contains a Blu-Ray image
        ''' </summary>
        ''' <param name="strPath">Path to be evaluated</param>
        ''' <returns><c>True</c> if the supplied path is determined to be a Blu-Ray path. <c>False</c> otherwise</returns>
        ''' <remarks>Two tests are performed. If the supplied path has an extension (such as if a .m2ts file was provided), check 
        ''' that the parent directory is "stream" and its parent is "bdmv"</remarks>
        Public Shared Function isBDRip(ByVal strPath As String) As Boolean
            'TODO Kludge. Consider FileSystemInfo.Attributes to detect if path is a file or directory, and proceed from there
            If String.IsNullOrEmpty(strPath) Then Return False
            If strPath.EndsWith(Path.DirectorySeparatorChar) OrElse strPath.EndsWith(Path.AltDirectorySeparatorChar) Then
                'The current/parent directory comparisons can't handle paths ending with a directory separator. Therefore, strip them out
                Return isBDRip(strPath.Substring(0, strPath.Length - 1))
            End If
            If Path.HasExtension(strPath) Then
                Return Directory.GetParent(strPath).Name.ToLower = "stream" AndAlso Directory.GetParent(Directory.GetParent(strPath).FullName).Name.ToLower = "bdmv"
            Else
                Return GetDirectory(strPath).ToLower = "stream" AndAlso Directory.GetParent(strPath).Name.ToLower = "bdmv"
            End If
        End Function
        ''' <summary>
        ''' Determine whether the given string represents a file that needs to be treated as if it is stacked (single media in multiple files)
        ''' If the system setting "DisableMultiPartMedia" is False, then always return False
        ''' </summary>
        ''' <param name="strPath"><c>String</c> to evaluate</param>
        ''' <returns><c>True</c> if the string represents a stacked file, or <c>False</c> otherwise</returns>
        ''' <remarks></remarks>
        Public Shared Function isStacked(ByVal strPath As String) As Boolean
            If Not strPath = RemoveStackingMarkers(strPath) Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' Deermine whether the path provided contains a DVD image
        ''' </summary>
        ''' <param name="strPath">Path to be evaluated</param>
        ''' <returns><c>True</c> if the supplied path is determined to be a Blu-Ray path. <c>False</c> otherwise</returns>
        ''' <remarks>If the path is a file, check if parent is video_ts. Otherwise, it should be a directory, so see if it is video_ts</remarks>
        Public Shared Function isVideoTS(ByVal strPath As String) As Boolean
            'TODO Kludge. Consider FileSystemInfo.Attributes to detect if path is a file or directory, and proceed from there
            If String.IsNullOrEmpty(strPath) Then Return False
            If Path.HasExtension(strPath) Then
                Return Directory.GetParent(strPath).Name.ToLower = "video_ts"
            Else
                Return GetDirectory(strPath).ToLower = "video_ts"
            End If
        End Function
        ''' <summary>
        ''' Get the entire path and filename of a file, but without the extension
        ''' </summary>
        ''' <param name="strPath">Full path to file.</param>
        ''' <returns>Path and filename of a file, without the extension</returns>
        ''' <remarks>No validation is made on whether the path/file actually exists.</remarks>
        Public Shared Function RemoveExtFromPath(ByVal strPath As String) As String
            'TODO Dekker500 This method needs serious work. Invalid paths are not consistently handled. Need analysis on how to handle these properly
            If String.IsNullOrEmpty(strPath) Then Return String.Empty
            Try
                'If the path has no directories (only the root), short-circuit the routine and just return
                If strPath.Equals(Directory.GetDirectoryRoot(strPath)) Then Return strPath
                Return Path.Combine(Path.GetDirectoryName(strPath), Path.GetFileNameWithoutExtension(strPath))
                'Return Path.Combine(Directory.GetParent(sPath).FullName, Path.GetFileNameWithoutExtension(sPath))
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Source: <" & strPath & ">")
                Return String.Empty
            End Try
        End Function
        ''' <summary>
        ''' Takes the supplied filename and replaces any invalid characters with suitable substitutions.
        ''' </summary>
        ''' <param name="filename">String intended to represent a filename, without any path.</param>
        ''' <returns>A String that has had any invalid characters substituted with acceptable alternatives.</returns>
        ''' <remarks>No validation is done as to whether the filename actually exists</remarks>
        Public Shared Function MakeValidFilename(ByVal filename As String) As String
            'TODO Should look into Path.GetInvalidFileNameChars and Path.GetInvalidPathChars
            filename = filename.Replace(":", " -")
            filename = filename.Replace("/", String.Empty)
            'pattern = pattern.Replace("\", String.Empty)
            filename = filename.Replace("|", String.Empty)
            filename = filename.Replace("<", String.Empty)
            filename = filename.Replace(">", String.Empty)
            filename = filename.Replace("?", String.Empty)
            filename = filename.Replace("*", String.Empty)
            filename = filename.Replace("  ", " ")
            Return filename
        End Function

        Public Shared Function ReturnSettingsFile(Dir As String, Name As String) As String
            'Cocotus, Load from central Dir folder if it exists!
            Dim configpath As String = String.Concat(Functions.AppPath, Dir, Path.DirectorySeparatorChar, Name)

            'AdvancedSettings.xml is still at old place (root) -> move to new place if there's no AdvancedSettings.xml !
            If Not File.Exists(configpath) AndAlso File.Exists(Path.Combine(Functions.AppPath, Name)) AndAlso Directory.Exists(String.Concat(Functions.AppPath, Dir, Path.DirectorySeparatorChar)) Then
                File.Move(Path.Combine(Functions.AppPath, Name), String.Concat(Functions.AppPath, Dir, Path.DirectorySeparatorChar, Name))
                'New Settings folder doesn't exist -> do it the old way...
            ElseIf Not Directory.Exists(String.Concat(Functions.AppPath, Dir, Path.DirectorySeparatorChar)) Then
                configpath = Path.Combine(Functions.AppPath, Name)
            End If

            Return configpath
        End Function

        Public Shared Sub InstallNewFiles(ByVal fname As String)
            Dim _cmds As Containers.InstallCommands = Containers.InstallCommands.Load(fname)
            For Each _cmd As Containers.CommandsNoTransactionCommand In _cmds.noTransaction
                Try
                    Select Case _cmd.type
                        Case "FILE.Move"
                            Dim s() As String = _cmd.execute.Split("|"c)
                            If s.Count >= 2 Then
                                If File.Exists(s(1)) Then File.Delete(s(1))
                                If Not Directory.Exists(Path.GetDirectoryName(s(1))) Then
                                    Directory.CreateDirectory(Path.GetDirectoryName(s(1)))
                                End If
                                File.Move(s(0), s(1))
                            End If
                        Case "FILE.Delete"
                            If File.Exists(_cmd.execute) Then File.Delete(_cmd.execute)
                    End Select
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            Next
            Dim destPath As String = Path.Combine(Functions.AppPath, "InstalledTasks_" & Format(DateTime.Now, "YYYYMMDD") & Format(DateTime.Now, "HHMMSS") & ".xml")
            Try
                File.Move(fname, destPath)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Shared Function CheckOnlineStatus_Movie(ByRef dbMovie As Database.DBElement, ByVal showMessage As Boolean) As Boolean
            While Not File.Exists(dbMovie.Filename)
                If showMessage Then
                    If MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine,
                                                     Master.eLang.GetString(630, "Reconnect the source and press Retry"), ".",
                                                     Environment.NewLine, Environment.NewLine,
                                                     dbMovie.Filename), String.Empty,
                                                 MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then Return False
                Else
                    Return False
                End If
            End While
            dbMovie.IsOnline = True
            Return True
        End Function

        Public Shared Function CheckOnlineStatus_TVEpisode(ByRef dbTV As Database.DBElement, ByVal showMessage As Boolean) As Boolean
            While Not File.Exists(dbTV.Filename)
                If showMessage Then
                    If MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine,
                                                     Master.eLang.GetString(630, "Reconnect the source and press Retry"), ".",
                                                     Environment.NewLine, Environment.NewLine,
                                                     dbTV.Filename), String.Empty,
                                                 MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then Return False
                Else
                    Return False
                End If
            End While
            dbTV.IsOnline = True
            Return True
        End Function

        Public Shared Function CheckOnlineStatus_TVShow(ByRef dbTV As Database.DBElement, ByVal showMessage As Boolean) As Boolean
            While Not Directory.Exists(dbTV.ShowPath)
                If showMessage Then
                    If MessageBox.Show(String.Concat(Master.eLang.GetString(719, "This path is no longer available"), ".", Environment.NewLine,
                                                     Master.eLang.GetString(630, "Reconnect the source and press Retry"), ".",
                                                     Environment.NewLine, Environment.NewLine,
                                                     dbTV.ShowPath), String.Empty,
                                                 MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then Return False
                Else
                    Return False
                End If
            End While
            dbTV.IsOnline = True
            Return True
        End Function

        ''' <summary>
        ''' Gets total size in bytes for given subtree specified by directory path
        ''' </summary>
        ''' <param name="spathDirectory">Path represented the location of the directory</param>
        ''' <returns>Total size in bytes for the given folder</returns>
        ''' <remarks></remarks>
        Public Shared Function GetFolderSize(ByVal spathDirectory As String) As Long
            Dim size As Long = 0
            If System.IO.Directory.Exists(spathDirectory) Then
                ' Take a snapshot of the file system.
                Dim dir As New DirectoryInfo(spathDirectory)
                ' This method assumes that the application has discovery permissions
                ' for all folders under the specified path.
                Dim fileList As IEnumerable(Of FileInfo) = dir.GetFiles("*.*", SearchOption.AllDirectories)

                For Each fi As FileInfo In fileList
                    size += GetFileLength(fi)
                Next
            Else
                logger.Debug("Can't calculate foldersize! Not a valid directory: ", spathDirectory)
            End If
            'Return the size of the smallest file
            Return size
        End Function

        ''' <summary>
        ''' Get filename of largest file for given subtree specified by directory path
        ''' </summary>
        ''' <param name="spathDirectory">Path represented the location of the directory</param>
        ''' <returns>Filename of largest file for the given folder</returns>
        ''' <remarks></remarks>
        Public Shared Function GetLargestFilePathFromDir(spathDirectory As String) As String
            ' Take a snapshot of the file system.
            Dim dir As New DirectoryInfo(spathDirectory)
            ' This method assumes that the application has discovery permissions
            ' for all folders under the specified path.
            Dim fileList As IEnumerable(Of FileInfo) = dir.GetFiles("*.*", SearchOption.AllDirectories)
            'Return the size of the largest file
            Dim maxSize As Long = (From file In fileList Let len = GetFileLength(file) Select len).Max()
            logger.Debug("The length of the largest file under {0} is {1}", spathDirectory, maxSize)
            ' Return the FileInfo object for the largest file
            ' by sorting and selecting from beginning of list
            Dim longestFile As FileInfo = (From file In fileList Let len = GetFileLength(file) Where len > 0 Order By len Descending Select file).First()
            logger.Debug("The largest file under {0} is {1} with a length of {2} bytes", spathDirectory, longestFile.FullName, longestFile.Length)
            Return longestFile.FullName
        End Function

        ''' <summary>
        ''' Get filename of smallest file for given subtree specified by directory path
        ''' </summary>
        ''' <param name="spathDirectory">Path represented the location of the directory</param>
        ''' <returns>Filename of smallest file for the given folder</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSmallestFilePathFromDir(spathDirectory As String) As String
            ' Take a snapshot of the file system.
            Dim dir As New DirectoryInfo(spathDirectory)
            ' This method assumes that the application has discovery permissions
            ' for all folders under the specified path.
            Dim fileList As IEnumerable(Of FileInfo) = dir.GetFiles("*.*", SearchOption.AllDirectories)
            'Return the FileInfo of the smallest file
            Dim smallestFile As FileInfo = (From file In fileList Let len = GetFileLength(file) Where len > 0 Order By len Ascending Select file).First()
            logger.Debug("The smallest file under {0} is {1} with a length of {2} bytes", spathDirectory, smallestFile.FullName, smallestFile.Length)
            Return smallestFile.FullName
        End Function

        ''' <summary>
        ''' Helper: Get filesize of specific file, handling exception
        ''' </summary>
        ''' <param name="fi">FileInfo of specific file</param>
        ''' <returns>FileSize of the given file</returns>
        ''' <remarks>
        ''' ' This method is used to swallow the possible exception
        ''' ' that can be raised when accessing the FileInfo.Length property.
        ''' ' In this particular case, it is safe to swallow the exception.
        ''' </remarks>
        Private Shared Function GetFileLength(fi As FileInfo) As Long
            Dim retval As Long
            Try
                retval = fi.Length
            Catch ex As FileNotFoundException
                ' If a file is no longer present,
                ' just add zero bytes to the total.
                logger.Error(String.Concat("Specific file is no longer present!", ex))
                retval = 0
            End Try
            Return retval
        End Function
        ''' <summary>
        ''' When given a valid path to a video/media file, return the path but without stacking markers.
        ''' </summary>
        ''' <param name="strPath"><c>String</c> full file path (including file extension) to clean</param>
        ''' <returns>The <c>String</c> path with the stacking markers removed</returns>
        ''' <remarks>The following are default stacking extensions that can be added to file names These are for video file names that are in the same folder:
        ''' # can be 1 through 9. No spaces between the extension and number. 
        ''' <list>
        '''   <item>part#​</item>
        '''   <item>cd#​</item>
        '''   <item>dvd#</item>
        '''   <item>pt#</item>
        '''   <item>disk#</item>
        '''   <item>disc#</item>
        ''' </list>
        ''' Note that text after the stacking marker are left untouched.
        ''' </remarks>
        Public Shared Function RemoveStackingMarkers(ByVal strPath As String) As String
            'Don't do anything if DisableMultiPartMedia is True or strPath is String.Empty
            If AdvancedSettings.GetBooleanSetting("DisableMultiPartMedia", False) OrElse String.IsNullOrEmpty(strPath) Then Return strPath

            Dim FilePattern As String = AdvancedSettings.GetSetting("FileStacking", "(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck])[0-9]+)(.*?)(\.[^.]+)$")
            Dim FolderPattern As String = AdvancedSettings.GetSetting("FolderStacking", "((cd|dvd|dis[ck])[0-9]+)$")

            Dim FileStacking = Regex.Match(strPath, FilePattern, RegexOptions.IgnoreCase)
            If FileStacking.Success Then
                Dim strPathCleaned As String = strPath.Replace(FileStacking.Groups(2).Value, String.Empty)
                Return strPathCleaned
            Else
                Return strPath
            End If
        End Function

#End Region 'Methods

    End Class

    Public Class Delete

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Shared Sub Cache_All()
            If MessageBox.Show(Master.eLang.GetString(104, "Are you sure?"), Master.eLang.GetString(565, "Clear Cache"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Directory.Exists(Master.TempPath) Then
                    Try
                        Dim dInfo As New DirectoryInfo(Master.TempPath)
                        Dim dList As IEnumerable(Of DirectoryInfo)
                        Dim fList As New List(Of FileInfo)
                        dList = dInfo.GetDirectories
                        fList.AddRange(dInfo.GetFiles())
                        For Each inDir As DirectoryInfo In dList
                            Directory.Delete(inDir.FullName, True)
                        Next
                        For Each fFile As FileInfo In fList
                            File.Delete(fFile.FullName)
                        Next
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                End If
            End If
        End Sub

        Public Shared Sub Cache_Show(ByVal TVDBIDs As List(Of String), ByVal cData As Boolean, ByVal cImages As Boolean)
            If TVDBIDs IsNot Nothing AndAlso TVDBIDs.Count > 0 Then
                If MessageBox.Show(Master.eLang.GetString(104, "Are you sure?"), Master.eLang.GetString(565, "Clear Cache"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    For Each id As String In TVDBIDs
                        Try
                            Dim basePath As String = Path.Combine(Master.TempPath, String.Concat("Shows", Path.DirectorySeparatorChar, id))
                            If Directory.Exists(basePath) Then
                                If cData AndAlso cImages Then
                                    Directory.Delete(basePath, True)
                                ElseIf cData Then
                                    Dim dInfo As New DirectoryInfo(basePath)
                                    Dim fList As New List(Of FileInfo)
                                    fList.AddRange(dInfo.GetFiles("*.zip"))
                                    For Each fFile As FileInfo In fList
                                        File.Delete(fFile.FullName)
                                    Next
                                ElseIf cImages Then
                                    Dim dInfo As New DirectoryInfo(basePath)
                                    Dim dList As IEnumerable(Of DirectoryInfo)
                                    dList = dInfo.GetDirectories
                                    For Each inDir As DirectoryInfo In dList
                                        Directory.Delete(inDir.FullName, True)
                                    Next
                                End If
                            End If
                        Catch ex As Exception
                            logger.Error(ex, New StackFrame().GetMethod().Name)
                        End Try
                    Next
                End If
            End If
        End Sub
        ''' <summary>
        ''' Safer method of deleting a diretory and all it's contents
        ''' </summary>
        ''' <param name="sPath">Full path of directory to delete</param>
        ''' <remarks>This method deletes the supplied path by recursively deleting its child directories, 
        ''' then deleting its file contents before deleting itself.</remarks>
        Public Shared Sub DeleteDirectory(ByVal sPath As String)
            'TODO The calls to Directory.Exists may return a false negative if the user does not have read access. If this happens
            'TODO during a recursive call, orphan folders may be left behind, causing the final Delete to fail. Should give better
            'TODO error messages so the log can be easier to interpret.
            Try
                If String.IsNullOrEmpty(sPath) Then Return

                If Directory.Exists(sPath) Then

                    Dim Dirs As New List(Of String)

                    Try
                        Dirs.AddRange(Directory.GetDirectories(sPath))
                    Catch
                    End Try

                    For Each inDir As String In Dirs
                        DeleteDirectory(inDir)
                    Next

                    Dim fFiles As New List(Of String)

                    Try
                        fFiles.AddRange(Directory.GetFiles(sPath))
                    Catch
                    End Try

                    For Each fFile As String In fFiles
                        Try
                            File.Delete(fFile)
                        Catch
                        End Try
                    Next

                    Directory.Delete(sPath, True)
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Methods

    End Class

    Public Class DragAndDrop

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Shared Function CheckDroppedImage(ByVal e As DragEventArgs) As Boolean
            Dim strFile() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If strFile IsNot Nothing Then
                Dim fi As New FileInfo(strFile(0))
                If fi.Extension = ".gif" Or fi.Extension = ".bmp" Or fi.Extension = ".jpg" Or fi.Extension = ".jpeg" Or fi.Extension = ".png" Then
                    Return True
                End If
            End If

            Return False
        End Function

        Public Shared Function GetDoppedImage(ByVal e As DragEventArgs) As MediaContainers.Image
            Dim tImage As New MediaContainers.Image
            If e.Data.GetDataPresent("HTML FORMAT") Then
                Dim clipboardHtml As String = CStr(e.Data.GetData("HTML Format"))
                Dim htmlFragment As String = getHtmlFragment(clipboardHtml)
                Dim imageSrc As String = parseImageSrc(htmlFragment)
                Dim baseURL As String = parseBaseURL(clipboardHtml)

                If (imageSrc.ToLower().IndexOf("http://") = 0) Or (imageSrc.ToLower().IndexOf("https://") = 0) Then
                    tImage.ImageOriginal.LoadFromWeb(imageSrc, True)
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Return tImage
                    End If
                Else
                    tImage.ImageOriginal.LoadFromWeb(baseURL + imageSrc.Substring(1), True)
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Return tImage
                    End If
                End If
            ElseIf e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                Dim localImage() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
                tImage.ImageOriginal.LoadFromFile(localImage(0).ToString, True)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Return tImage
                End If
            End If

            Return tImage
        End Function
        Public Shared Function getHtmlFragment(ByVal html As String) As String
            Dim fragStartPos As Integer
            Dim fragEndPos As Integer

            fragStartPos = Integer.Parse(Regex.Match(html, "^StartFragment:(\d+)", RegexOptions.Multiline).Groups(1).Value)
            fragEndPos = Integer.Parse(Regex.Match(html, "^EndFragment:(\d+)", RegexOptions.Multiline).Groups(1).Value)

            Return html.Substring(fragStartPos, fragEndPos - fragStartPos)
        End Function

        Public Shared Function parseBaseURL(ByVal html As String) As String
            Return Regex.Match(html, "http(s)?://.*?/", RegexOptions.IgnoreCase).Groups(0).Value
        End Function

        Public Shared Function parseImageSrc(ByVal html As String) As String
            Return Regex.Match(html, "<img.*?src=[""""'](.*?)[""""'].*>", RegexOptions.IgnoreCase Or RegexOptions.Singleline).Groups(1).Value
        End Function

#End Region 'Methods

    End Class

    Public Class GetFilenameList

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"
        ''' <summary>
        ''' Creates a list of filenames to save or read movie content
        ''' </summary>
        ''' <param name="mType"></param>
        ''' <param name="bForced">Enable ALL known file naming schemas. Should only be used to search files and not to save files!</param>
        ''' <returns><c>List(Of String)</c> all filenames with full path</returns>
        ''' <remarks></remarks>
        Public Shared Function Movie(ByVal DBElement As Database.DBElement, ByVal mType As Enums.ModifierType, Optional ByVal bForced As Boolean = False) As List(Of String)
            Dim FilenameList As New List(Of String)

            If String.IsNullOrEmpty(DBElement.Filename) Then Return FilenameList

            Dim basePath As String = String.Empty
            Dim fPath As String = DBElement.Filename
            Dim fileName As String = Path.GetFileNameWithoutExtension(fPath)
            Dim fileNameStack As String = Path.GetFileNameWithoutExtension(Common.RemoveStackingMarkers(fPath))
            Dim filePath As String = Path.Combine(Directory.GetParent(fPath).FullName, fileName)
            Dim filePathStack As String = Path.Combine(Directory.GetParent(fPath).FullName, fileNameStack)
            Dim fileParPath As String = Directory.GetParent(filePath).FullName
            Dim isSingle As Boolean = DBElement.IsSingle

            Dim isVideoTS As Boolean = Common.isVideoTS(fPath)
            Dim isBDRip As Boolean = Common.isBDRip(fPath)
            Dim isVideoTSFile As Boolean = fileName.ToLower = "video_ts"

            If isVideoTS OrElse isBDRip Then basePath = Common.GetMainPath(fPath).FullName

            Select Case mType
                Case Enums.ModifierType.MainActorThumbs
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieActorThumbsEden) Then FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>.tbn"))
                            If .MovieUseExpert AndAlso .MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertVTS) Then
                                If .MovieUseBaseDirectoryExpertVTS Then
                                    FilenameList.Add(String.Concat(basePath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertVTS))
                                Else
                                    FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertVTS))
                                End If
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "\.actors", "\<placeholder>.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieActorThumbsEden) Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "\.actors", "\<placeholder>.tbn"))
                            If .MovieUseExpert AndAlso .MovieActorThumbsExpertBDMV AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertBDMV) Then
                                If .MovieUseBaseDirectoryExpertBDMV Then
                                    FilenameList.Add(String.Concat(basePath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertBDMV))
                                Else
                                    FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertBDMV))
                                End If
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo) Then FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieActorThumbsEden) Then FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>.tbn"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS AndAlso .MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertVTS) Then
                                FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertVTS))
                            ElseIf .MovieUseExpert AndAlso .MovieActorThumbsExpertSingle AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertSingle) Then
                                FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertSingle))
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo) Then FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>.jpg"))
                            If .MovieUseExpert AndAlso .MovieActorThumbsExpertMulti AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertMulti) Then
                                FilenameList.Add(String.Concat(fileParPath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertMulti))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainBanner
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieBannerExtended) Then FilenameList.Add(Path.Combine(basePath, "banner.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieBannerAD) Then FilenameList.Add(Path.Combine(basePath, "banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertVTS) Then
                                For Each a In .MovieBannerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieBannerExtended) Then FilenameList.Add(Path.Combine(basePath, "banner.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieBannerAD) Then FilenameList.Add(Path.Combine(basePath, "banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertBDMV) Then
                                For Each a In .MovieBannerExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso isVideoTSFile AndAlso .MovieBannerExtended) Then FilenameList.Add(Path.Combine(fileParPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not isVideoTSFile AndAlso .MovieBannerExtended) Then FilenameList.Add(String.Concat(filePathStack, "-banner.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieBannerAD) Then FilenameList.Add(Path.Combine(fileParPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(filePathStack, ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(filePath, ".banner.jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieBannerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieBannerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieBannerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieBannerExtended) Then FilenameList.Add(String.Concat(filePathStack, "-banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(filePathStack, ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(filePath, ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertMulti) Then
                                For Each a In .MovieBannerExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainClearArt
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearArtExtended) Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearArtAD) Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertVTS) Then
                                For Each a In .MovieClearArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearArtExtended) Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearArtAD) Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertBDMV) Then
                                For Each a In .MovieClearArtExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso isVideoTSFile AndAlso .MovieClearArtExtended) Then FilenameList.Add(Path.Combine(fileParPath, "clearart.png"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not isVideoTSFile AndAlso .MovieClearArtExtended) Then FilenameList.Add(String.Concat(filePathStack, "-clearart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearArtAD) Then FilenameList.Add(Path.Combine(fileParPath, "clearart.png"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieClearArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieClearArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieClearArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearArtExtended) Then FilenameList.Add(String.Concat(filePathStack, "-clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertMulti) Then
                                For Each a In .MovieClearArtExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainClearLogo
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearLogoExtended) Then FilenameList.Add(Path.Combine(basePath, "clearlogo.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearLogoAD) Then FilenameList.Add(Path.Combine(basePath, "logo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertVTS) Then
                                For Each a In .MovieClearLogoExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearLogoExtended) Then FilenameList.Add(Path.Combine(basePath, "clearlogo.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearLogoAD) Then FilenameList.Add(Path.Combine(basePath, "logo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertBDMV) Then
                                For Each a In .MovieClearLogoExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso isVideoTSFile AndAlso .MovieClearLogoExtended) Then FilenameList.Add(Path.Combine(fileParPath, "clearlogo.png"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not isVideoTSFile AndAlso .MovieClearLogoExtended) Then FilenameList.Add(String.Concat(filePathStack, "-clearlogo.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearLogoAD) Then FilenameList.Add(Path.Combine(fileParPath, "logo.png"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieClearLogoExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieClearLogoExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieClearLogoExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearLogoExtended) Then FilenameList.Add(String.Concat(filePathStack, "-clearlogo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertMulti) Then
                                For Each a In .MovieClearLogoExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainDiscArt
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieDiscArtExtended) Then FilenameList.Add(Path.Combine(basePath, "discart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieDiscArtAD) Then FilenameList.Add(Path.Combine(basePath, "disc.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertVTS) Then
                                For Each a In .MovieDiscArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieDiscArtExtended) Then FilenameList.Add(Path.Combine(basePath, "discart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieDiscArtAD) Then FilenameList.Add(Path.Combine(basePath, "disc.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertBDMV) Then
                                For Each a In .MovieDiscArtExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso isVideoTSFile AndAlso .MovieDiscArtExtended) Then FilenameList.Add(Path.Combine(fileParPath, "discart.png"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not isVideoTSFile AndAlso .MovieDiscArtExtended) Then FilenameList.Add(String.Concat(filePathStack, "-discart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieDiscArtAD) Then FilenameList.Add(Path.Combine(fileParPath, "disc.png"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieDiscArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieDiscArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieDiscArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieDiscArtExtended) Then FilenameList.Add(String.Concat(filePathStack, "-discart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertMulti) Then
                                For Each a In .MovieDiscArtExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainExtrafanarts
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrafanartsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrafanart"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrafanartsEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrafanart"))
                            If .MovieUseExpert AndAlso .MovieExtrafanartsExpertVTS Then
                                If .MovieUseBaseDirectoryExpertVTS Then
                                    FilenameList.Add(Path.Combine(basePath, "extrafanart"))
                                Else
                                    FilenameList.Add(Path.Combine(fileParPath, "extrafanart"))
                                End If
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrafanartsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrafanart"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrafanartsEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrafanart"))
                            If .MovieUseExpert AndAlso .MovieExtrafanartsExpertBDMV Then
                                If .MovieUseBaseDirectoryExpertBDMV Then
                                    FilenameList.Add(Path.Combine(basePath, "extrafanart"))
                                Else
                                    FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrafanart"))
                                End If
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrafanartsFrodo) Then FilenameList.Add(Path.Combine(fileParPath, "extrafanart"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrafanartsEden) Then FilenameList.Add(Path.Combine(fileParPath, "extrafanart"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS AndAlso .MovieExtrafanartsExpertVTS Then
                                FilenameList.Add(Path.Combine(fileParPath, "extrafanart"))
                            ElseIf .MovieUseExpert AndAlso .MovieExtrafanartsExpertSingle Then
                                FilenameList.Add(Path.Combine(fileParPath, "extrafanart"))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainExtrathumbs
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrathumbsFrodo) AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrathumbs"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrathumbsEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrathumbs"))
                            If .MovieUseExpert AndAlso .MovieExtrathumbsExpertVTS Then
                                If .MovieUseBaseDirectoryExpertVTS Then
                                    FilenameList.Add(Path.Combine(basePath, "extrathumbs"))
                                Else
                                    FilenameList.Add(Path.Combine(fileParPath, "extrathumbs"))
                                End If
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrathumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrathumbs"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrathumbsEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrathumbs"))
                            If .MovieUseExpert AndAlso .MovieExtrathumbsExpertBDMV Then
                                If .MovieUseBaseDirectoryExpertBDMV Then
                                    FilenameList.Add(Path.Combine(basePath, "extrathumbs"))
                                Else
                                    FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrathumbs"))
                                End If
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrathumbsFrodo) Then FilenameList.Add(Path.Combine(fileParPath, "extrathumbs"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrathumbsEden) Then FilenameList.Add(Path.Combine(fileParPath, "extrathumbs"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS AndAlso .MovieExtrathumbsExpertVTS Then
                                FilenameList.Add(Path.Combine(fileParPath, "extrathumbs"))
                            ElseIf .MovieUseExpert AndAlso .MovieExtrathumbsExpertSingle Then
                                FilenameList.Add(Path.Combine(fileParPath, "extrathumbs"))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainFanart
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(Path.Combine(basePath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieFanartBoxee) Then FilenameList.Add(Path.Combine(basePath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertVTS) Then
                                For Each a In .MovieFanartExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(Path.Combine(basePath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertBDMV) Then
                                For Each a In .MovieFanartExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso isVideoTSFile AndAlso .MovieFanartFrodo) Then FilenameList.Add(Path.Combine(fileParPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseFrodo AndAlso Not isVideoTSFile AndAlso .MovieFanartFrodo) Then FilenameList.Add(String.Concat(filePathStack, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieFanartBoxee) Then FilenameList.Add(Path.Combine(fileParPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(filePathStack, ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(filePath, ".fanart.jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieFanartExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieFanartExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieFanartExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(String.Concat(filePathStack, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(filePathStack, ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(filePath, ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertMulti) Then
                                For Each a In .MovieFanartExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainLandscape
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieLandscapeExtended) Then FilenameList.Add(Path.Combine(basePath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(basePath, "landscape.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertVTS) Then
                                For Each a In .MovieLandscapeExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieLandscapeExtended) Then FilenameList.Add(Path.Combine(basePath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(basePath, "landscape.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertBDMV) Then
                                For Each a In .MovieLandscapeExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso isVideoTSFile AndAlso .MovieLandscapeExtended) Then FilenameList.Add(Path.Combine(fileParPath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not isVideoTSFile AndAlso .MovieLandscapeExtended) Then FilenameList.Add(String.Concat(filePathStack, "-landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso isVideoTSFile AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(fileParPath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso Not isVideoTSFile AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(fileParPath, "landscape.jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieLandscapeExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieLandscapeExtended) Then FilenameList.Add(String.Concat(filePathStack, "-landscape.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertMulti) Then
                                For Each a In .MovieLandscapeExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainNFO
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(basePath, "movie.nfo"))
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieNFOBoxee) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertVTS) Then
                                For Each a In .MovieNFOExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(basePath, "movie.nfo"))
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index.nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index.nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertBDMV) Then
                                For Each a In .MovieNFOExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso isVideoTSFile) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not isVideoTSFile) Then FilenameList.Add(String.Concat(filePathStack, ".nfo"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieNFOBoxee AndAlso isVideoTSFile) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieNFOBoxee AndAlso Not isVideoTSFile) Then FilenameList.Add(Path.Combine(fileParPath, "movie.nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                If Not String.IsNullOrEmpty(.MovieNFOExpertVTS) Then
                                    For Each a In .MovieNFOExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieNFOExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieNFOExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo) Then FilenameList.Add(String.Concat(filePathStack, ".nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertMulti) Then
                                For Each a In .MovieNFOExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainPoster
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MoviePosterFrodo) Then FilenameList.Add(Path.Combine(basePath, "poster.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MoviePosterBoxee) Then FilenameList.Add(Path.Combine(basePath, "folder.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MoviePosterEden) Then FilenameList.Add(String.Concat(filePath, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MoviePosterNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MoviePosterYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertVTS) Then
                                For Each a In .MoviePosterExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MoviePosterFrodo) Then FilenameList.Add(Path.Combine(basePath, "poster.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MoviePosterEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index.tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MoviePosterNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MoviePosterYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertBDMV) Then
                                For Each a In .MoviePosterExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso isVideoTSFile AndAlso .MoviePosterFrodo) Then FilenameList.Add(Path.Combine(fileParPath, "poster.jpg"))
                            If bForced OrElse (.MovieUseFrodo AndAlso Not isVideoTSFile AndAlso .MoviePosterFrodo) Then FilenameList.Add(String.Concat(filePathStack, "-poster.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MoviePosterBoxee) Then FilenameList.Add(Path.Combine(fileParPath, "folder.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MoviePosterEden) Then FilenameList.Add(String.Concat(filePath, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MoviePosterNMJ) Then FilenameList.Add(String.Concat(filePathStack, ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MoviePosterYAMJ) Then FilenameList.Add(String.Concat(filePath, ".jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MoviePosterExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MoviePosterExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MoviePosterExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(String.Concat(filePathStack, "-poster.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MoviePosterBoxee) Then FilenameList.Add(Path.Combine(filePath, ".tbn"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(filePath, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(filePathStack, ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(String.Concat(filePath, ".jpg")))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertMulti) Then
                                For Each a In .MoviePosterExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainSubtitle
                    With Master.eSettings
                        If isVideoTS Then
                            FilenameList.Add(Path.Combine(basePath, "subs"))
                        ElseIf isBDRip Then
                            FilenameList.Add(Path.Combine(basePath, "subs"))
                        ElseIf isSingle Then
                            FilenameList.Add(Path.Combine(fileParPath, "subs"))
                            FilenameList.Add(fileParPath)
                        End If
                    End With

                Case Enums.ModifierType.MainTheme
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieThemeTvTunesEnable Then
                                If .MovieThemeTvTunesMoviePath Then FilenameList.Add(Path.Combine(basePath, "theme"))
                                If .MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.MovieThemeTvTunesCustomPath, "theme"))
                                If .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, .MovieThemeTvTunesSubDir, Path.DirectorySeparatorChar, "theme"))
                            End If
                        ElseIf isBDRip Then
                            If .MovieThemeTvTunesEnable Then
                                If .MovieThemeTvTunesMoviePath Then FilenameList.Add(Path.Combine(basePath, "theme"))
                                If .MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.MovieThemeTvTunesCustomPath, "theme"))
                                If .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, .MovieThemeTvTunesSubDir, Path.DirectorySeparatorChar, "theme"))
                            End If
                        Else
                            If .MovieThemeTvTunesEnable Then
                                If .MovieThemeTvTunesMoviePath Then FilenameList.Add(Path.Combine(fileParPath, "theme"))
                                If .MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.MovieThemeTvTunesCustomPath, "theme"))
                                If .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir) Then FilenameList.Add(String.Concat(fileParPath, Path.DirectorySeparatorChar, .MovieThemeTvTunesSubDir, Path.DirectorySeparatorChar, "theme"))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainTrailer
                    With Master.eSettings
                        If isVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertVTS) Then
                                For Each a In .MovieTrailerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, "index-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, "index-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertBDMV) Then
                                For Each a In .MovieTrailerExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo) Then FilenameList.Add(String.Concat(filePathStack, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieTrailerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieTrailerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieTrailerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo) Then FilenameList.Add(String.Concat(filePathStack, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertMulti) Then
                                For Each a In .MovieTrailerExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainWatchedFile
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(basePath, Directory.GetParent(fileParPath).Name), ".watched"))
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso Not String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(.MovieYAMJWatchedFolder, Directory.GetParent(fileParPath).Name), ".watched"))
                        ElseIf isBDRip Then
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(basePath, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".watched"))
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso Not String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(.MovieYAMJWatchedFolder, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".watched"))
                        Else
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(fPath, ".watched"))
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso Not String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(.MovieYAMJWatchedFolder, String.Concat(Path.GetFileName(fPath), ".watched"))))
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function
        ''' <summary>
        ''' Creates a list of filenames to save or read movieset content
        ''' </summary>
        ''' <param name="mType"></param>
        ''' <returns><c>List(Of String)</c> all filenames with full path</returns>
        ''' <remarks></remarks>
        Public Shared Function MovieSet(ByVal DBElement As Database.DBElement, ByVal mType As Enums.ModifierType, Optional ByVal bForceOldTitle As Boolean = False) As List(Of String)
            Dim FilenameList As New List(Of String)

            If String.IsNullOrEmpty(DBElement.MovieSet.Title) Then Return FilenameList

            Dim fSetTitle As String = If(bForceOldTitle, DBElement.MovieSet.OldTitle, DBElement.MovieSet.Title)
            For Each Invalid As Char In Path.GetInvalidFileNameChars
                fSetTitle = fSetTitle.Replace(Invalid, "-")
            Next

            Select Case mType
                Case Enums.ModifierType.MainBanner
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetBannerExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-banner.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetBannerMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(fSetTitle, "-banner.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetBannerExpertSingle) Then
                            For Each a In .MovieSetBannerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearArt
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetClearArtExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-clearart.png")))
                        If .MovieSetUseMSAA AndAlso .MovieSetClearArtMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(fSetTitle, "-clearart.png")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetClearArtExpertSingle) Then
                            For Each a In .MovieSetClearArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearLogo
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetClearLogoExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-clearlogo.png")))
                        If .MovieSetUseMSAA AndAlso .MovieSetClearLogoMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(fSetTitle, "-logo.png")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetClearLogoExpertSingle) Then
                            For Each a In .MovieSetClearLogoExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainDiscArt
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetDiscArtExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-discart.png")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetDiscArtExpertSingle) Then
                            For Each a In .MovieSetDiscArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainFanart
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetFanartExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-fanart.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetFanartMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(fSetTitle, "-fanart.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetFanartExpertSingle) Then
                            For Each a In .MovieSetFanartExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainLandscape
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetLandscapeExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-landscape.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetLandscapeMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(fSetTitle, "-landscape.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetLandscapeExpertSingle) Then
                            For Each a In .MovieSetLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainNFO
                    With Master.eSettings
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetNFOExpertSingle) Then
                            For Each a In .MovieSetNFOExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainPoster
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetPosterExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(fSetTitle, "-poster.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetPosterMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(fSetTitle, "-poster.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetPosterExpertSingle) Then
                            For Each a In .MovieSetPosterExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                            Next
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Public Shared Function TVEpisode(ByVal DBElement As Database.DBElement, ByVal mType As Enums.ModifierType) As List(Of String)
            Dim FilenameList As New List(Of String)

            If String.IsNullOrEmpty(DBElement.Filename) Then Return FilenameList

            Dim fEpisodeFileName As String = Path.GetFileNameWithoutExtension(DBElement.Filename)
            Dim fEpisodePath As String = Common.RemoveExtFromPath(DBElement.Filename)
            Dim fEpisodeParentPath As String = Directory.GetParent(DBElement.Filename).FullName
            Dim sSeason As String = DBElement.TVEpisode.Season.ToString.PadLeft(2, Convert.ToChar("0"))

            Select Case mType
                Case Enums.ModifierType.EpisodeActorThumbs
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVEpisodeActorThumbsFrodo Then FilenameList.Add(String.Concat(fEpisodeParentPath, "\.actors", "\<placeholder>.jpg"))
                        If .TVUseExpert AndAlso .TVEpisodeActorThumbsExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodeActorThumbsExtExpert) Then
                            FilenameList.Add(String.Concat(fEpisodeParentPath, "\.actors", "\<placeholder>", .TVEpisodeActorThumbsExtExpert))
                        End If
                    End With

                Case Enums.ModifierType.EpisodeFanart
                    With Master.eSettings
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodeFanartExpert) Then
                            For Each a In .TVEpisodeFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fEpisodeParentPath, a.Replace("<filename>", fEpisodeFileName)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.EpisodeNFO
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVEpisodeNFOFrodo Then FilenameList.Add(String.Concat(fEpisodePath, ".nfo"))
                        If .TVUseBoxee AndAlso .TVEpisodeNFOBoxee Then FilenameList.Add(String.Concat(fEpisodePath, ".nfo"))
                        If .TVUseEden Then FilenameList.Add(String.Concat(fEpisodePath, ".nfo"))
                        If .TVUseYAMJ AndAlso .TVEpisodeNFOYAMJ Then FilenameList.Add(String.Concat(fEpisodePath, ".nfo"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodeNFOExpert) Then
                            For Each a In .TVEpisodeNFOExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fEpisodeParentPath, a.Replace("<filename>", fEpisodeFileName)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.EpisodePoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVEpisodePosterFrodo Then FilenameList.Add(String.Concat(fEpisodePath, "-thumb.jpg"))
                        If .TVUseBoxee AndAlso .TVEpisodePosterBoxee Then FilenameList.Add(String.Concat(fEpisodePath, ".tbn"))
                        If .TVUseYAMJ AndAlso .TVEpisodePosterYAMJ Then FilenameList.Add(String.Concat(fEpisodePath, ".videoimage.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodePosterExpert) Then
                            For Each a In .TVEpisodePosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fEpisodeParentPath, a.Replace("<filename>", fEpisodeFileName)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.EpisodeSubtitle
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fEpisodeParentPath, "subs"))
                        FilenameList.Add(fEpisodeParentPath)
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Public Shared Function TVSeason(ByVal DBElement As Database.DBElement, ByVal mType As Enums.ModifierType) As List(Of String)
            Dim FilenameList As New List(Of String)
            Dim bInside As Boolean = False

            If String.IsNullOrEmpty(DBElement.ShowPath) Then Return FilenameList

            Dim fEpisodePath As String = String.Empty
            Dim fSeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(DBElement.ShowPath, DBElement.TVSeason.Season)
            Dim fShowPath As String = DBElement.ShowPath
            Dim sSeason As String = DBElement.TVSeason.Season.ToString.PadLeft(2, Convert.ToChar("0"))
            Dim fSeasonFolder As String = Path.GetFileName(fSeasonPath)

            'checks if there are separate season folders
            If Not String.IsNullOrEmpty(fSeasonPath) AndAlso Not fSeasonPath = fShowPath Then
                bInside = True
            End If

            'get first episode of season (YAMJ need that for episodes without separate season folders)
            Try
                If Master.eSettings.TVUseYAMJ AndAlso Not bInside Then
                    Dim dtEpisodes As New DataTable
                    Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN files ON (files.idFile = episode.idFile) WHERE idShow = ", DBElement.ShowID, " AND Season = ", DBElement.TVSeason.Season, " ORDER BY Episode;"))
                    If dtEpisodes.Rows.Count > 0 Then
                        fEpisodePath = dtEpisodes.Rows(0).Item("strFilename").ToString
                        If Not String.IsNullOrEmpty(fEpisodePath) Then
                            fEpisodePath = Path.Combine(Directory.GetParent(fEpisodePath).FullName, Path.GetFileNameWithoutExtension(fEpisodePath))
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Select Case mType
                Case Enums.ModifierType.SeasonBanner
                    With Master.eSettings
                        If DBElement.TVSeason.Season = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-banner.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".banner.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(fEpisodePath) Then FilenameList.Add(String.Concat(fEpisodePath, ".banner.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonBannerExpert) Then
                                For Each a In .TVSeasonBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonBannerFrodo Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-banner.jpg", sSeason)))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".banner.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(fEpisodePath) Then FilenameList.Add(String.Concat(fEpisodePath, ".banner.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonBannerExpert) Then
                                For Each a In .TVSeasonBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.SeasonFanart
                    With Master.eSettings
                        If DBElement.TVSeason.Season = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-fanart.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".fanart.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(fEpisodePath) Then FilenameList.Add(String.Concat(fEpisodePath, ".fanart.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonFanartExpert) Then
                                For Each a In .TVSeasonFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-fanart.jpg", sSeason)))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".fanart.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(fEpisodePath) Then FilenameList.Add(String.Concat(fEpisodePath, ".fanart.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonFanartExpert) Then
                                For Each a In .TVSeasonFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.SeasonLandscape
                    With Master.eSettings
                        If DBElement.TVSeason.Season = 0 Then 'season specials
                            If .TVUseExtended AndAlso .TVSeasonLandscapeExtended Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-landscape.jpg"))
                            If .TVUseAD AndAlso .TVSeasonLandscapeAD Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-landscape.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonLandscapeExpert) Then
                                For Each a In .TVSeasonLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseExtended AndAlso .TVSeasonLandscapeExtended Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-landscape.jpg", sSeason)))
                            If .TVUseAD AndAlso .TVSeasonLandscapeAD Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-landscape.jpg", sSeason)))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonLandscapeExpert) Then
                                For Each a In .TVSeasonLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.SeasonPoster
                    With Master.eSettings
                        If DBElement.TVSeason.Season = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-poster.jpg"))
                            If .TVUseBoxee AndAlso .TVSeasonPosterBoxee AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, "poster.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(fEpisodePath) Then FilenameList.Add(String.Concat(fEpisodePath, ".jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonPosterExpert) Then
                                For Each a In .TVSeasonPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-poster.jpg", sSeason)))
                            If .TVUseBoxee AndAlso .TVSeasonPosterBoxee AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, "poster.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(fEpisodePath) Then FilenameList.Add(String.Concat(fEpisodePath, ".jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonPosterExpert) Then
                                For Each a In .TVSeasonPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", fSeasonPath)
                                        sPath = String.Format(sPath, sSeason, DBElement.TVSeason.Season, sSeason) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(fShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Public Shared Function TVShow(ByVal DBElement As Database.DBElement, ByVal mType As Enums.ModifierType) As List(Of String)
            Dim FilenameList As New List(Of String)

            If String.IsNullOrEmpty(DBElement.ShowPath) Then Return FilenameList

            Dim fShowPath As String = DBElement.ShowPath
            Dim fShowFolder As String = Path.GetFileName(fShowPath)

            Select Case mType
                Case Enums.ModifierType.MainActorThumbs
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowActorThumbsFrodo Then FilenameList.Add(String.Concat(fShowPath, "\.actors", "\<placeholder>.jpg"))
                        If .TVUseExpert AndAlso .TVShowActorThumbsExpert AndAlso Not String.IsNullOrEmpty(.TVShowActorThumbsExtExpert) Then
                            FilenameList.Add(String.Concat(fShowPath, "\.actors", "\<placeholder>", .TVShowActorThumbsExtExpert))
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsBanner
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonBannerFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-all-banner.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsBannerExpert) Then
                            For Each a In .TVAllSeasonsBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsFanart
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-all-fanart.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsFanartExpert) Then
                            For Each a In .TVAllSeasonsFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsLandscape
                    With Master.eSettings
                        If .TVUseAD AndAlso .TVSeasonLandscapeAD Then FilenameList.Add(Path.Combine(fShowPath, "season-all-landscape.jpg"))
                        If .TVUseExtended AndAlso .TVSeasonLandscapeExtended Then FilenameList.Add(Path.Combine(fShowPath, "season-all-landscape.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsLandscapeExpert) Then
                            For Each a In .TVAllSeasonsLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsPoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-all-poster.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsPosterExpert) Then
                            For Each a In .TVAllSeasonsPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainBanner
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowBannerFrodo Then FilenameList.Add(Path.Combine(fShowPath, "banner.jpg"))
                        If .TVUseBoxee AndAlso .TVShowBannerBoxee Then FilenameList.Add(Path.Combine(fShowPath, "banner.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowBannerYAMJ Then FilenameList.Add(Path.Combine(fShowPath, String.Concat(fShowFolder, ".banner.jpg")))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowBannerExpert) Then
                            For Each a In .TVShowBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainCharacterArt
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowCharacterArtExtended Then FilenameList.Add(Path.Combine(fShowPath, "characterart.png"))
                        If .TVUseAD AndAlso .TVShowCharacterArtAD Then FilenameList.Add(Path.Combine(fShowPath, "character.png"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowCharacterArtExpert) Then
                            For Each a In .TVShowCharacterArtExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearArt
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowClearArtExtended Then FilenameList.Add(Path.Combine(fShowPath, "clearart.png"))
                        If .TVUseAD AndAlso .TVShowClearArtAD Then FilenameList.Add(Path.Combine(fShowPath, "clearart.png"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowClearArtExpert) Then
                            For Each a In .TVShowClearArtExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearLogo
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowClearLogoExtended Then FilenameList.Add(Path.Combine(fShowPath, "clearlogo.png"))
                        If .TVUseAD AndAlso .TVShowClearLogoAD Then FilenameList.Add(Path.Combine(fShowPath, "logo.png"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowClearLogoExpert) Then
                            For Each a In .TVShowClearLogoExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainExtrafanarts
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowExtrafanartsFrodo Then FilenameList.Add(Path.Combine(fShowPath, "extrafanart"))
                        If .TVUseEden AndAlso .TVShowExtrafanartsFrodo Then FilenameList.Add(Path.Combine(fShowPath, "extrafanart"))
                        If .TVUseExpert AndAlso .TVShowExtrafanartsExpert Then FilenameList.Add(Path.Combine(fShowPath, "extrafanart"))
                    End With

                Case Enums.ModifierType.MainFanart
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "fanart.jpg"))
                        If .TVUseBoxee AndAlso .TVShowFanartBoxee Then FilenameList.Add(Path.Combine(fShowPath, "fanart.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowFanartYAMJ Then FilenameList.Add(Path.Combine(fShowPath, String.Concat(fShowFolder, ".fanart.jpg")))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowFanartExpert) Then
                            For Each a In .TVShowFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainLandscape
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowLandscapeExtended Then FilenameList.Add(Path.Combine(fShowPath, "landscape.jpg"))
                        If .TVUseAD AndAlso .TVShowLandscapeAD Then FilenameList.Add(Path.Combine(fShowPath, "landscape.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowLandscapeExpert) Then
                            For Each a In .TVShowLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainNFO
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowNFOFrodo Then FilenameList.Add(Path.Combine(fShowPath, "tvshow.nfo"))
                        If .TVUseBoxee AndAlso .TVShowNFOBoxee Then FilenameList.Add(Path.Combine(fShowPath, "tvshow.nfo"))
                        If .TVUseYAMJ AndAlso .TVShowNFOYAMJ Then FilenameList.Add(Path.Combine(fShowPath, "tvshow.nfo"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowNFOExpert) Then
                            For Each a In .TVShowNFOExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainPoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, "poster.jpg"))
                        If .TVUseBoxee AndAlso .TVShowPosterBoxee Then FilenameList.Add(Path.Combine(fShowPath, "folder.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowPosterYAMJ Then FilenameList.Add(Path.Combine(fShowPath, String.Concat(fShowFolder, ".jpg")))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowPosterExpert) Then
                            For Each a In .TVShowPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(fShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainTheme
                    With Master.eSettings
                        If .TVShowThemeTvTunesEnable Then
                            If .TVShowThemeTvTunesShowPath Then FilenameList.Add(Path.Combine(fShowPath, "theme"))
                            If .TVShowThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.TVShowThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.TVShowThemeTvTunesCustomPath, "theme"))
                            If .TVShowThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.TVShowThemeTvTunesSubDir) Then FilenameList.Add(String.Concat(fShowPath, Path.DirectorySeparatorChar, .TVShowThemeTvTunesSubDir, Path.DirectorySeparatorChar, "theme"))
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

#End Region 'Methods

    End Class
    ''' <summary>
    ''' This module is a convenience library for sorting files into respective subdirectories.
    ''' This module does NOT need to be instantiated!
    ''' </summary>
    ''' <remarks></remarks>
    Public Module FileSorter

#Region "Fields"

        Dim _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Events"
        ''' <summary>
        ''' Event that is raised when SortFiles desires the progress indicator to be updated
        ''' </summary>
        ''' <param name="percent">Percentage complete</param>
        ''' <param name="status">Message to be displayed alongside the progress indicator</param>
        ''' <remarks></remarks>
        Public Event ProgressUpdated(ByVal percent As Integer, ByVal status As String)

#End Region 'Events

#Region "Methods"
        ''' <summary>
        ''' Reorganize the media files in the given folder into subfolders.
        ''' </summary>
        ''' <param name="sourcePath">Path to be sorted</param>
        ''' <remarks>Occasionally a directory will contain multiple media files (and meta-files) and 
        ''' this method will walk through the files in that directory and move each to its own unique subdirectory.
        ''' This will move all files with the same core name, without extension or fanart/trailer endings.</remarks>
        Public Sub SortFiles(ByVal sourcePath As String)
            'TODO Need to test what happens if sPath points to an existing FILE (and not just a directory)
            Dim iCount As Integer = 0

            Try
                If Directory.Exists(sourcePath) Then
                    'Get information about files in the directory
                    Dim di As New DirectoryInfo(sourcePath)
                    Dim lFi As New List(Of FileInfo)
                    Dim lMediaList As IOrderedEnumerable(Of FileInfo)

                    'Create a List of files in the directory
                    Try
                        lFi.AddRange(di.GetFiles())
                    Catch
                    End Try

                    'Create a list of all media files with a valid extension in the directory
                    lMediaList = lFi.Where(Function(f) Master.eSettings.FileSystemValidExts.Contains(f.Extension.ToLower) AndAlso
                             Not Regex.IsMatch(f.Name, String.Concat("[^\w\s]\s?(", AdvancedSettings.GetSetting("NotValidFileContains", "trailer|sample"), ")"), RegexOptions.IgnoreCase) AndAlso ((Master.eSettings.MovieSkipStackedSizeCheck AndAlso
                            Common.isStacked(f.FullName)) OrElse (Not Convert.ToInt32(Master.eSettings.MovieSkipLessThan) > 0 OrElse f.Length >= Master.eSettings.MovieSkipLessThan * 1048576))).OrderByDescending(Function(f) Path.GetFileNameWithoutExtension(f.FullName))

                    'For each valid file in the directory...
                    For Each sFile As FileInfo In lMediaList
                        Dim nMovie As New Database.DBElement(Enums.ContentType.Movie) With {.Filename = sFile.FullName, .IsSingle = False}
                        RaiseEvent ProgressUpdated((iCount \ lMediaList.Count * 100), String.Concat(Master.eLang.GetString(219, "Moving "), sFile.Name))

                        'create a new directory for the movie
                        Dim strNewPath As String = Path.Combine(sourcePath, Path.GetFileNameWithoutExtension(Common.RemoveStackingMarkers(nMovie.Filename)))
                        If Not Directory.Exists(strNewPath) Then
                            Directory.CreateDirectory(strNewPath)
                        End If

                        'move movie to the new directory
                        sFile.MoveTo(Path.Combine(strNewPath, Path.GetFileName(nMovie.Filename)))

                        'search for files that belong to this movie
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainBanner, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainCharacterArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainClearArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainClearLogo, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainDiscArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainFanart, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainLandscape, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainNFO, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainPoster, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainTheme, True)
                            For Each t As String In Master.eSettings.FileSystemValidThemeExts
                                If File.Exists(String.Concat(a, t)) Then File.Move(String.Concat(a, t), Path.Combine(strNewPath, Path.GetFileName(String.Concat(a, t))))
                            Next
                        Next
                        For Each a In GetFilenameList.Movie(nMovie, Enums.ModifierType.MainTrailer, True)
                            For Each t As String In Master.eSettings.FileSystemValidExts
                                If File.Exists(String.Concat(a, t)) Then File.Move(String.Concat(a, t), Path.Combine(strNewPath, Path.GetFileName(String.Concat(a, t))))
                            Next
                        Next

                        'search for files that starts with the same name like the file name
                        For Each aFile In lFi.Where(Function(f) Not lMediaList.Contains(f) AndAlso File.Exists(f.FullName) AndAlso f.FullName.ToLower.StartsWith(Common.RemoveExtFromPath(nMovie.Filename).ToLower))
                            aFile.MoveTo(Path.Combine(strNewPath, Path.GetFileName(aFile.Name)))
                        Next
                        iCount += 1
                    Next
                    RaiseEvent ProgressUpdated((100), Master.eLang.GetString(362, "Done"))
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Methods

    End Module

End Namespace