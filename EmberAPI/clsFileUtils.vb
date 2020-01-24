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
Imports System.Windows.Forms

Namespace FileUtils

    Public Class CleanUp

#Region "Fields"

        Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

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
                            Dim nScanner As New Scanner
                            nScanner.GetFolderContents_Movie(tmpMovie, True)
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

    Public Class ClipboardHandler

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Shared Function GetImagesFromClipboard() As List(Of MediaContainers.Image)
            Dim lstImages As New List(Of MediaContainers.Image)
            Try
                Select Case True
                    Case Clipboard.ContainsImage
                        Dim nImage As New MediaContainers.Image
                        nImage.ImageOriginal.UpdateMSfromImg(Clipboard.GetImage)
                        lstImages.Add(nImage)
                    Case Clipboard.ContainsText
                        Dim nImage As New MediaContainers.Image
                        Dim nURI As Uri = Nothing
                        If Uri.TryCreate(Clipboard.GetText, UriKind.Absolute, nURI) Then
                            Select Case True
                                Case nURI.IsFile
                                    nImage.ImageOriginal.LoadFromFile(nURI.LocalPath, True)
                                    lstImages.Add(nImage)
                                Case nURI.Scheme = "http", nURI.Scheme = "https"
                                    nImage.ImageOriginal.LoadFromWeb(nURI.AbsoluteUri, True)
                                    lstImages.Add(nImage)
                            End Select
                        End If
                    Case Clipboard.ContainsFileDropList
                        For Each nPath In Clipboard.GetFileDropList
                            Dim nImage As New MediaContainers.Image
                            nImage.ImageOriginal.LoadFromFile(nPath, True)
                            lstImages.Add(nImage)
                        Next
                End Select
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
            Return lstImages
        End Function

#End Region 'Methods

    End Class

    Public Class Common

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Shared Function CheckOnlineStatus(ByRef dbElement As Database.DBElement, ByVal showMessage As Boolean) As Boolean
            Select Case dbElement.ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.TVEpisode
                    If dbElement.FileItemSpecified Then
                        While Not File.Exists(dbElement.FileItem.FirstPathFromStack)
                            If showMessage Then
                                If MessageBox.Show(String.Concat(
                                                   Master.eLang.GetString(587, "This file is no longer available"), ".",
                                                   Environment.NewLine,
                                                   Master.eLang.GetString(630, "Reconnect the source and press Retry"), ".",
                                                   Environment.NewLine,
                                                   Environment.NewLine,
                                                   dbElement.FileItem.FirstPathFromStack),
                                                   String.Empty,
                                                   MessageBoxButtons.RetryCancel,
                                                   MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then Return False
                            Else
                                Return False
                            End If
                        End While
                        dbElement.IsOnline = True
                        Return True
                    Else
                        Return False
                    End If

                Case Enums.ContentType.Movieset
                    For Each nMovie In dbElement.MoviesInSet
                        If Not CheckOnlineStatus(nMovie.DBMovie, showMessage) Then
                            Return False
                        End If
                        Return True
                    Next
                    'Empty MovieSet
                    Return True

                Case Enums.ContentType.TVShow, Enums.ContentType.TVSeason
                    While Not Directory.Exists(dbElement.ShowPath)
                        If showMessage Then
                            If MessageBox.Show(String.Concat(
                                               Master.eLang.GetString(719, "This path is no longer available"), ".",
                                               Environment.NewLine,
                                               Master.eLang.GetString(630, "Reconnect the source and press Retry"), ".",
                                               Environment.NewLine,
                                               Environment.NewLine,
                                               dbElement.ShowPath),
                                               String.Empty,
                                               MessageBoxButtons.RetryCancel,
                                               MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then Return False
                        Else
                            Return False
                        End If
                    End While
                    dbElement.IsOnline = True
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Public Shared Function CheckOnlineStatus(ByRef dbSource As Database.DBSource, ByVal showMessage As Boolean) As Boolean
            While Not Directory.Exists(dbSource.Path)
                If showMessage Then
                    If MessageBox.Show(String.Concat(
                                       Master.eLang.GetString(719, "This path is no longer available"), ".",
                                       Environment.NewLine,
                                       Master.eLang.GetString(630, "Reconnect the source and press Retry"), ".",
                                       Environment.NewLine,
                                       Environment.NewLine,
                                       dbSource.Path),
                                       String.Empty,
                                       MessageBoxButtons.RetryCancel,
                                       MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Cancel Then Return False
                Else
                    Return False
                End If
            End While
            Return True
        End Function

        Public Shared Sub CopyFanartToBackdropsPath(ByVal strSourceFilename As String, ByVal ContentType As Enums.ContentType)
            If Not String.IsNullOrEmpty(strSourceFilename) AndAlso File.Exists(strSourceFilename) Then
                Dim strDestinationFilename As String = String.Empty
                Select Case ContentType
                    Case Enums.ContentType.Movie
                        If String.IsNullOrEmpty(Master.eSettings.MovieBackdropsPath) Then Return

                        'TODO: fix
                        'If isVideoTS(strSourceFilename) Then
                        '    strDestinationFilename = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.Combine(GetMainPath(strSourceFilename).FullName, GetMainPath(strSourceFilename).Name), "-fanart.jpg"))
                        'ElseIf isBDRip(strSourceFilename) Then
                        '    strDestinationFilename = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(GetMainPath(strSourceFilename).Name, "-fanart.jpg"))
                        'Else
                        '    strDestinationFilename = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(GetMainPath(strSourceFilename).Name, "-fanart.jpg"))
                        'End If
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

        Public Shared Sub DirectoryCopy(ByVal strSourceDir As String,
                                        ByVal strDestinationDir As String,
                                        ByVal withSubDirs As Boolean,
                                        ByVal overwriteFiles As Boolean)

            If Not Directory.Exists(strSourceDir) Then
                logger.Error(String.Concat("Source directory does not exist: ", strSourceDir))
                Exit Sub
            End If

            ' Get the subdirectories for the specified directory.
            Dim dir As DirectoryInfo = New DirectoryInfo(strSourceDir)
            Dim subdirs As DirectoryInfo() = dir.GetDirectories()

            ' If the destination directory doesn't exist, create it.
            If Not Directory.Exists(strDestinationDir) Then
                Directory.CreateDirectory(strDestinationDir)
            End If

            ' Get the files in the directory and copy them to the new location.
            Dim files As FileInfo() = dir.GetFiles()
            For Each cFile In files
                Dim temppath As String = Path.Combine(strDestinationDir, cFile.Name)
                If Not File.Exists(temppath) OrElse overwriteFiles Then
                    cFile.CopyTo(temppath, overwriteFiles)
                End If
            Next cFile

            ' If copying subdirectories, copy them and their contents to new location.
            If withSubDirs Then
                For Each subdir In subdirs
                    Dim temppath As String = Path.Combine(strDestinationDir, subdir.Name)
                    DirectoryCopy(subdir.FullName, temppath, withSubDirs, overwriteFiles)
                Next subdir
            End If
        End Sub

        Public Shared Sub DirectoryMove(ByVal strSourceDir As String,
                                        ByVal strDestinationDir As String,
                                        ByVal withSubDirs As Boolean,
                                        ByVal overwriteFiles As Boolean)

            If Not Directory.Exists(strSourceDir) Then
                logger.Error(String.Concat("Source directory does not exist: ", strSourceDir))
                Exit Sub
            End If

            If Path.GetPathRoot(strSourceDir).ToLower = Path.GetPathRoot(strDestinationDir).ToLower AndAlso withSubDirs Then
                If Not Directory.Exists(Directory.GetParent(strDestinationDir).FullName) Then Directory.CreateDirectory(Directory.GetParent(strDestinationDir).FullName)
                Directory.Move(strSourceDir, strDestinationDir)
            Else
                ' Get the subdirectories for the specified directory.
                Dim dir As DirectoryInfo = New DirectoryInfo(strSourceDir)
                Dim subdirs As DirectoryInfo() = dir.GetDirectories()

                ' If the destination directory doesn't exist, create it.
                If Not Directory.Exists(strDestinationDir) Then
                    Directory.CreateDirectory(strDestinationDir)
                End If

                ' Get the files in the directory and move them to the new location.
                Dim files As FileInfo() = dir.GetFiles()
                For Each cFile In files
                    Dim temppath As String = Path.Combine(strDestinationDir, cFile.Name)
                    If Not File.Exists(temppath) OrElse overwriteFiles Then
                        If File.Exists(temppath) Then File.Delete(temppath)
                        cFile.MoveTo(temppath)
                    End If
                Next

                ' If copying subdirectories, copy them and their contents to new location.
                If withSubDirs Then
                    For Each subdir In subdirs
                        Dim temppath As String = Path.Combine(strDestinationDir, subdir.Name)
                        DirectoryMove(subdir.FullName, temppath, withSubDirs, overwriteFiles)
                    Next subdir
                End If

                If dir.GetFiles.Count = 0 AndAlso dir.GetDirectories.Count = 0 Then
                    Directory.Delete(dir.FullName)
                End If
            End If

        End Sub

        Public Shared Function GetAllItemsOfDBElement(ByVal tDBElement As Database.DBElement) As List(Of FileSystemInfo)
            Dim lstItems As New List(Of FileSystemInfo)

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.TVEpisode
                    If tDBElement.FileItemSpecified Then
                        If tDBElement.IsSingle OrElse tDBElement.FileItem.bIsBDMV OrElse tDBElement.FileItem.bIsVideoTS Then
                            lstItems.Add(tDBElement.FileItem.MainPath)
                        Else
                            Select Case tDBElement.ContentType
                                Case Enums.ContentType.Movie
                                    Dim lstFiles As New List(Of String)
                                    lstFiles.AddRange(tDBElement.FileItem.PathList)
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainBanner))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainClearArt))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainClearLogo))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainDiscArt))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainFanart))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainKeyArt))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainLandscape))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainNFO))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainPoster))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainSubtitle))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainTheme))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainTrailer))
                                    lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                                    lstFiles.Sort()
                                    For Each nFile In lstFiles
                                        Dim nFileinfo As New FileInfo(nFile)
                                        If nFileinfo.Exists Then lstItems.Add(nFileinfo)
                                    Next

                                Case Enums.ContentType.TVEpisode
                                    Dim lstFiles As New List(Of String)
                                    lstFiles.AddRange(tDBElement.FileItem.PathList)
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.EpisodeFanart))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.EpisodeNFO))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.EpisodePoster))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.EpisodeSubtitle))
                                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.EpisodeWatchedFile))
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
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainBanner))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainClearArt))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainClearLogo))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainDiscArt))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainFanart))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainKeyArt))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainLandscape))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainNFO))
                    lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainPoster))
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
                    If tDBElement.MainDetails.Season_IsAllSeasons Then
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.AllSeasonsBanner))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.AllSeasonsFanart))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.AllSeasonsLandscape))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.AllSeasonsPoster))
                        lstFiles = lstFiles.Distinct().ToList()  'remove double entries
                        lstFiles.Sort()
                    Else
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.SeasonBanner))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.SeasonFanart))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.SeasonLandscape))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.SeasonNFO))
                        lstFiles.AddRange(FileNames.GetFileNames(tDBElement, Enums.ModifierType.SeasonPoster))
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
        ''' Given a path, determine whether it is a Blu-Ray or DVD folder. Find the respective media files within, and return the
        ''' largest one that is over 1 GB, or String.Empty otherwise.
        ''' </summary>
        ''' <param name="sPath">Path to Blu-Ray or DVD files.</param> 
        ''' <returns>Path/filename to the largest media file for the detected video type</returns>
        ''' <remarks>
        '''  2016/01/26  Cocotus - Remove 1GB limit from query
        ''' </remarks>
        Public Shared Function GetLongestFromRip(ByVal fileItem As FileItem) As String
            Dim lFileList As New List(Of FileInfo)
            Select Case True
                Case fileItem.bIsBDMV
                    lFileList.AddRange(New DirectoryInfo(fileItem.MainPath.FullName).GetFiles("*.m2ts", SearchOption.AllDirectories))
                Case fileItem.bIsVideoTS
                    lFileList.AddRange(New DirectoryInfo(fileItem.MainPath.FullName).GetFiles("*.vob"))
            End Select
            'Return filename/path of the largest file
            Return lFileList.OrderByDescending(Function(s) s.Length).Select(Function(s) s.FullName).FirstOrDefault
        End Function

        Public Shared Function GetOpenFileDialogFilter_Theme() As String
            Dim lstValidExtensions As New List(Of String)

            For Each nExtension In Master.eSettings.Options.FileSystem.ValidThemeExtensions
                lstValidExtensions.Add(String.Concat("*", nExtension))
            Next

            Return String.Concat(Master.eLang.GetString(1285, "Themes"), "|", String.Join(";", lstValidExtensions.ToArray))
        End Function

        Public Shared Function GetOpenFileDialogFilter_Video(ByVal strDescription As String) As String
            Dim lstValidExtensions As New List(Of String)

            For Each nExtension In Master.eSettings.Options.FileSystem.ValidVideoExtensions
                lstValidExtensions.Add(String.Concat("*", nExtension))
            Next

            Return String.Concat(strDescription, "|", String.Join(";", lstValidExtensions.ToArray))
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
                        For Each sMatch As Match In Regex.Matches(sDir.Name, pattern, RegexOptions.IgnoreCase)
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

        Public Shared Function GetTotalLengt(ByRef tFileItem As FileItem) As Long
            If tFileItem IsNot Nothing AndAlso Not tFileItem.bIsDirectory AndAlso tFileItem.bIsOnline Then
                Try
                    Dim lngTotalLength As Long = 0
                    If tFileItem.bIsBDMV OrElse tFileItem.bIsVideoTS Then 'TODO: cleanup result from trailers and other files
                        For Each nFile In Directory.GetFiles(tFileItem.MainPath.FullName, "*.*", SearchOption.AllDirectories)
                            lngTotalLength += New FileInfo(nFile).Length
                        Next
                        Return lngTotalLength
                    Else
                        For Each nFile In tFileItem.PathList
                            lngTotalLength += New FileInfo(nFile).Length
                        Next
                        Return lngTotalLength
                    End If
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
            Return -1
        End Function

        Public Shared Function GetVideosource(ByVal fileItem As FileItem, ByVal isEpisode As Boolean) As String

            'by FileItem
            If fileItem.bIsBDMV Then Return "bluray"
            If fileItem.bIsVideoTS Then Return "dvd"
            If Path.GetFileName(fileItem.FirstPathFromStack).ToLower = "video_ts.ifo" Then Return "dvd"

            'by file name/path and regex mapping
            If Master.eSettings.Options.VideoSourceMapping.ByRegexEnabled Then
                Dim strPath As String = String.Empty
                If isEpisode Then
                    strPath = Path.GetFileName(fileItem.FirstPathFromStack).ToLower
                Else
                    strPath = If(Master.eSettings.Movie.SourceSettings.VideoSourceFromFolder, String.Concat(Directory.GetParent(fileItem.FirstPathFromStack).Name.ToLower, Path.DirectorySeparatorChar, Path.GetFileName(fileItem.FirstPathFromStack).ToLower), Path.GetFileName(fileItem.FirstPathFromStack).ToLower)
                End If
                If Master.eSettings.Options.VideoSourceMapping.ByRegexSpecified Then
                    For Each m In Master.eSettings.Options.VideoSourceMapping.ByRegex
                        If Regex.IsMatch(strPath, m.Regexp) Then
                            Return m.Videosource
                        End If
                    Next
                End If
            End If

            'by file extension
            If Master.eSettings.Options.VideoSourceMapping.ByExtensionEnabled Then
                Dim strExtension = Path.GetExtension(fileItem.FirstPathFromStack)
                Dim nVideosource = Master.eSettings.Options.VideoSourceMapping.ByExtension.FirstOrDefault(Function(f) f.Extension = strExtension)
                If nVideosource IsNot Nothing Then Return nVideosource.VideoSource
            End If

            Return String.Empty
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
        ''' <summary>
        ''' Check if we should scan the directory.
        ''' </summary>
        ''' <param name="directory">DirectoryInfo to check</param>
        ''' <returns>True if directory is valid, false if not.</returns>
        Public Shared Function IsValidDir(ByVal directory As DirectoryInfo, ByVal contentType As Enums.ContentType) As Boolean
            Try
                For Each s As String In Master.DB.GetExcludedPaths
                    If directory.FullName.ToLower = s.ToLower Then
                        logger.Info(String.Format("[FileUtils] [IsValidDir] [ExcludeDirs] Path ""{0}"" has been skipped (path is in ""exclude directory"" list)", directory.FullName))
                        Return False
                    End If
                Next
                If (contentType = Enums.ContentType.Movie AndAlso directory.Name.ToLower = "extras") OrElse
                    If(directory.FullName.IndexOf("\") >= 0, directory.FullName.Remove(0, directory.FullName.IndexOf("\")).Contains(":"), False) Then
                    Return False
                End If
                For Each s As String In AdvancedSettings.GetSetting("NotValidDirIs", ".actors|certificate|extrafanart|extrathumbs|audio_ts|recycler|subs|subtitles|.trashes").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                    If directory.Name.ToLower = s.ToLower Then
                        logger.Info(String.Format("[FileUtils] [IsValidDir] [NotValidDirIs] Path ""{0}"" has been skipped (path name is ""{1}"")", directory.FullName, s))
                        Return False
                    End If
                Next
                For Each s As String In AdvancedSettings.GetSetting("NotValidDirContains", "-trailer|[trailer|temporary files|(noscan)|$recycle.bin|lost+found|system volume information|sample").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                    If directory.Name.ToLower.Contains(s.ToLower) Then
                        logger.Info(String.Format("[FileUtils] [IsValidDir] [NotValidDirContains] Path ""{0}"" has been skipped (path contains ""{1}"")", directory.FullName, s))
                        Return False
                    End If
                Next

            Catch ex As Exception
                logger.Error(String.Format("[FileUtils] [IsValidDir] Path ""{0}"" has been skipped ({1})", directory.FullName, ex.Message))
                Return False
            End Try
            Return True
        End Function

        Public Shared Function IsValidFile(ByVal file As FileInfo, ByVal contentType As Enums.ContentType) As Boolean
            Try
                Dim intSkipLessThan As Integer = 0
                Select Case contentType
                    Case Enums.ContentType.Movie
                        intSkipLessThan = Master.eSettings.Movie.SourceSettings.SkipLessThan
                    Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                        intSkipLessThan = Master.eSettings.TVEpisode.SourceSettings.SkipLessThan
                End Select

                For Each s As String In Master.DB.GetExcludedPaths
                    If file.FullName.ToLower.StartsWith(s.ToLower) Then
                        logger.Info(String.Format("[FileUtils] [IsValidDir] [ExcludeDirs] File ""{0}"" has been skipped (path is in ""exclude directory"" list)", file.FullName))
                        Return False
                    End If
                Next
                If Not Master.eSettings.Options.FileSystem.ValidVideoExtensions.Contains(file.Extension.ToLower) Then
                    logger.Trace(String.Format("[FileUtils] [IsValidFile] [NotValidFileContains] File ""{0}"" has been skipped (valid video file extension list does not contain ""{1}"")", file.FullName, file.Extension))
                    Return False
                End If
                For Each s As String In AdvancedSettings.GetSetting("NotValidFileContains", "trailer|sample").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                    If file.Name.ToLower.Contains(s.ToLower) Then
                        logger.Info(String.Format("[FileUtils] [IsValidFile] [NotValidFileContains] File ""{0}"" has been skipped (path contains ""{1}"")", file.FullName, s))
                        Return False
                    End If
                Next
                If intSkipLessThan > 0 AndAlso intSkipLessThan * 1048576 > file.Length Then
                    logger.Info(String.Format("[FileUtils] [IsValidFile] [NotValidFileContains] File ""{0}"" has been skipped (file size ({1}) is lower than the ""skip less than"" user value of ""{2}"" ", file.FullName, file.Length, intSkipLessThan))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(String.Format("[FileUtils] [IsValidFile] File ""{0}"" has been skipped ({1})", file.FullName, ex.Message))
                Return False
            End Try
            Return True
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

        Public Shared Function ReturnSettingsFile(Dir As String, Name As String) As String
            'Load from central Dir folder if it exists!
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
        ''' <param name="Path">Full path of directory to delete</param>
        ''' <remarks>This method deletes the supplied path by recursively deleting its child directories, 
        ''' then deleting its file contents before deleting itself.</remarks>
        Public Shared Sub DeleteDirectory(ByVal Path As String)
            If String.IsNullOrEmpty(Path) Then Return
            Try
                Dim currDirectory As New DirectoryInfo(Path)
                If currDirectory.Exists Then
                    Try
                        For Each inDir In currDirectory.GetDirectories
                            DeleteDirectory(inDir.FullName)
                        Next
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try

                    Try
                        For Each inFile In currDirectory.GetFiles
                            DeleteDirectory(inFile.FullName)
                        Next
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                    currDirectory.Delete(True)
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Methods

    End Class

    Public Class DragAndDrop

#Region "Methods"

        Public Shared Function CheckDroppedImage(ByVal e As DragEventArgs) As Boolean
            Dim strFile() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If strFile IsNot Nothing Then
                Dim fi As New FileInfo(strFile(0))
                Return fi.Extension = ".bmp" OrElse
                    fi.Extension = ".gif" OrElse
                    fi.Extension = ".jpeg" OrElse
                    fi.Extension = ".jpg" OrElse
                    fi.Extension = ".png"
            Else
                Dim tPictureBox As PictureBox = CType(e.Data.GetData(GetType(PictureBox)), PictureBox)
                If tPictureBox IsNot Nothing AndAlso
                    tPictureBox.Tag IsNot Nothing AndAlso
                    TypeOf tPictureBox.Tag Is MediaContainers.Image Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Shared Function GetDroppedImage(ByVal e As DragEventArgs) As MediaContainers.Image
            Dim tPictureBox As PictureBox = CType(e.Data.GetData(GetType(PictureBox)), PictureBox)
            If tPictureBox IsNot Nothing AndAlso
                    tPictureBox.Tag IsNot Nothing AndAlso
                    TypeOf tPictureBox.Tag Is MediaContainers.Image Then
                Return CType(tPictureBox.Tag, MediaContainers.Image)
            End If
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

    Public Class FileNames

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Shared Function GetFileNames(ByVal DBElement As Database.DBElement, ByVal ModType As Enums.ModifierType, Optional Forced As Boolean = False) As List(Of String)
            Select Case DBElement.ContentType
                Case Enums.ContentType.Movie
                    Return Movie(DBElement, ModType, Forced)
                Case Enums.ContentType.Movieset
                    Return Movieset(DBElement, ModType, Forced)
                Case Enums.ContentType.TVEpisode
                    Return TVEpisode(DBElement, ModType)
                Case Enums.ContentType.TVSeason
                    If DBElement.MainDetails.Season_IsAllSeasons Then
                        Return TVShow(DBElement, ModType)
                    Else
                        Return TVSeason(DBElement, ModType)
                    End If
                Case Enums.ContentType.TVShow
                    Return TVShow(DBElement, ModType)
            End Select
            Return New List(Of String)
        End Function
        ''' <summary>
        ''' Creates a list of filenames to save or read movie content
        ''' </summary>
        ''' <param name="mType"></param>
        ''' <param name="bForced">Enable ALL known file naming schemas. Should only be used to search files and not to save files!</param>
        ''' <returns><c>List(Of String)</c> all filenames with full path</returns>
        ''' <remarks></remarks>
        Private Shared Function Movie(ByVal dbElement As Database.DBElement, ByVal mType As Enums.ModifierType, Optional ByVal bForced As Boolean = False) As List(Of String)
            Dim FilenameList As New List(Of String)

            If Not dbElement.FileItemSpecified Then Return FilenameList

            Dim strFilePath As String = dbElement.FileItem.FirstPathFromStack
            Dim strFileParentPath As String = Directory.GetParent(dbElement.FileItem.FirstPathFromStack).FullName
            Dim strMainPath As String = dbElement.FileItem.MainPath.FullName
            Dim strMainPathName As String = dbElement.FileItem.MainPath.Name

            Dim strFileNameWoExt As String = Path.GetFileNameWithoutExtension(strFilePath)
            Dim strFilePathWoExt As String = Path.Combine(strFileParentPath, strFileNameWoExt)
            Dim strStackedFileNameWoExt As String = Path.GetFileNameWithoutExtension(dbElement.FileItem.StackedFileName)
            Dim strStackedFilePathWoExt As String = Path.Combine(Directory.GetParent(strFilePath).FullName, strStackedFileNameWoExt)

            Dim bIsSingle As Boolean = dbElement.IsSingle
            Dim bIsBDMV As Boolean = dbElement.FileItem.bIsBDMV
            Dim bIsVideoTS As Boolean = dbElement.FileItem.bIsVideoTS
            Dim bIsVideoTSFile As Boolean = strFileNameWoExt.ToLower = "video_ts"

            'TODO: fix NMJ file names

            Select Case mType
                Case Enums.ModifierType.MainActorThumbs
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieActorThumbsEden) Then FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>.tbn"))
                            If .MovieUseExpert AndAlso .MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertVTS) Then
                                If .MovieUseBaseDirectoryExpertVTS Then
                                    FilenameList.Add(String.Concat(Path.Combine(strMainPath, ".actors"), "\<placeholder>", .MovieActorThumbsExtExpertVTS))
                                Else
                                    FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>", .MovieActorThumbsExtExpertVTS))
                                End If
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieActorThumbsEden) Then FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>.tbn"))
                            If .MovieUseExpert AndAlso .MovieActorThumbsExpertBDMV AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertBDMV) Then
                                If .MovieUseBaseDirectoryExpertBDMV Then
                                    FilenameList.Add(String.Concat(Path.Combine(strMainPath, ".actors"), "\<placeholder>", .MovieActorThumbsExtExpertBDMV))
                                Else
                                    FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>", .MovieActorThumbsExtExpertBDMV))
                                End If
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, ".actors"), "\<placeholder>.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieActorThumbsEden) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, ".actors"), "\<placeholder>.tbn"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS AndAlso .MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertVTS) Then
                                FilenameList.Add(String.Concat(Path.Combine(strMainPath, ".actors"), "\<placeholder>", .MovieActorThumbsExtExpertVTS))
                            ElseIf .MovieUseExpert AndAlso .MovieActorThumbsExpertSingle AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertSingle) Then
                                FilenameList.Add(String.Concat(Path.Combine(strMainPath, ".actors"), "\<placeholder>", .MovieActorThumbsExtExpertSingle))
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieActorThumbsFrodo) Then FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>.jpg"))
                            If .MovieUseExpert AndAlso .MovieActorThumbsExpertMulti AndAlso Not String.IsNullOrEmpty(.MovieActorThumbsExtExpertMulti) Then
                                FilenameList.Add(String.Concat(strFileParentPath, "\.actors", "\<placeholder>", .MovieActorThumbsExtExpertMulti))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainBanner
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieBannerExtended) Then FilenameList.Add(Path.Combine(strMainPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieBannerAD) Then FilenameList.Add(Path.Combine(strMainPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName, Directory.GetParent(strFileParentPath).Name), ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertVTS) Then
                                For Each a In .MovieBannerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieBannerExtended) Then FilenameList.Add(Path.Combine(strMainPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieBannerAD) Then FilenameList.Add(Path.Combine(strMainPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).Name), ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName, Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).Name), ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertBDMV) Then
                                For Each a In .MovieBannerExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso bIsVideoTSFile AndAlso .MovieBannerExtended) Then FilenameList.Add(Path.Combine(strFileParentPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not bIsVideoTSFile AndAlso .MovieBannerExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-banner.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieBannerAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".banner.jpg"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieBannerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieBannerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieBannerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieBannerExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-banner.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieBannerNMJ) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".banner.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieBannerYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertMulti) Then
                                For Each a In .MovieBannerExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainClearArt
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearArtExtended) Then FilenameList.Add(Path.Combine(strMainPath, "clearart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearArtAD) Then FilenameList.Add(Path.Combine(strMainPath, "clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertVTS) Then
                                For Each a In .MovieClearArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearArtExtended) Then FilenameList.Add(Path.Combine(strMainPath, "clearart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearArtAD) Then FilenameList.Add(Path.Combine(strMainPath, "clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertBDMV) Then
                                For Each a In .MovieClearArtExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso bIsVideoTSFile AndAlso .MovieClearArtExtended) Then FilenameList.Add(Path.Combine(strFileParentPath, "clearart.png"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not bIsVideoTSFile AndAlso .MovieClearArtExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-clearart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearArtAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "clearart.png"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieClearArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieClearArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieClearArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearArtExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertMulti) Then
                                For Each a In .MovieClearArtExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainClearLogo
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearLogoExtended) Then FilenameList.Add(Path.Combine(strMainPath, "clearlogo.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearLogoAD) Then FilenameList.Add(Path.Combine(strMainPath, "logo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertVTS) Then
                                For Each a In .MovieClearLogoExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearLogoExtended) Then FilenameList.Add(Path.Combine(strMainPath, "clearlogo.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearLogoAD) Then FilenameList.Add(Path.Combine(strMainPath, "logo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertBDMV) Then
                                For Each a In .MovieClearLogoExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso bIsVideoTSFile AndAlso .MovieClearLogoExtended) Then FilenameList.Add(Path.Combine(strFileParentPath, "clearlogo.png"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not bIsVideoTSFile AndAlso .MovieClearLogoExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-clearlogo.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieClearLogoAD) Then FilenameList.Add(Path.Combine(strMainPath, "logo.png"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieClearLogoExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieClearLogoExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieClearLogoExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieClearLogoExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-clearlogo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertMulti) Then
                                For Each a In .MovieClearLogoExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainDiscArt
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieDiscArtExtended) Then FilenameList.Add(Path.Combine(strMainPath, "discart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieDiscArtAD) Then FilenameList.Add(Path.Combine(strMainPath, "disc.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertVTS) Then
                                For Each a In .MovieDiscArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieDiscArtExtended) Then FilenameList.Add(Path.Combine(strMainPath, "discart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieDiscArtAD) Then FilenameList.Add(Path.Combine(strMainPath, "disc.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertBDMV) Then
                                For Each a In .MovieDiscArtExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso bIsVideoTSFile AndAlso .MovieDiscArtExtended) Then FilenameList.Add(Path.Combine(strFileParentPath, "discart.png"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not bIsVideoTSFile AndAlso .MovieDiscArtExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-discart.png"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieDiscArtAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "disc.png"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieDiscArtExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieDiscArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieDiscArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieDiscArtExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-discart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertMulti) Then
                                For Each a In .MovieDiscArtExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainExtrafanarts
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrafanartsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrafanartsEden) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            If .MovieUseExpert AndAlso .MovieExtrafanartsExpertVTS Then
                                If .MovieUseBaseDirectoryExpertVTS Then
                                    FilenameList.Add(Path.Combine(strMainPath, "extrafanart"))
                                Else
                                    FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                                End If
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrafanartsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrafanartsEden) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            If .MovieUseExpert AndAlso .MovieExtrafanartsExpertBDMV Then
                                If .MovieUseBaseDirectoryExpertBDMV Then
                                    FilenameList.Add(Path.Combine(strMainPath, "extrafanart"))
                                Else
                                    FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                                End If
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrafanartsFrodo) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrafanartsEden) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS AndAlso .MovieExtrafanartsExpertVTS Then
                                FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            ElseIf .MovieUseExpert AndAlso .MovieExtrafanartsExpertSingle Then
                                FilenameList.Add(Path.Combine(strFileParentPath, "extrafanart"))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainExtrathumbs
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrathumbsFrodo) AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrathumbsEden) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            If .MovieUseExpert AndAlso .MovieExtrathumbsExpertVTS Then
                                If .MovieUseBaseDirectoryExpertVTS Then
                                    FilenameList.Add(Path.Combine(strMainPath, "extrathumbs"))
                                Else
                                    FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                                End If
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrathumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrathumbsEden) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            If .MovieUseExpert AndAlso .MovieExtrathumbsExpertBDMV Then
                                If .MovieUseBaseDirectoryExpertBDMV Then
                                    FilenameList.Add(Path.Combine(strMainPath, "extrathumbs"))
                                Else
                                    FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                                End If
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieExtrathumbsFrodo) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieExtrathumbsEden) Then FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS AndAlso .MovieExtrathumbsExpertVTS Then
                                FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            ElseIf .MovieUseExpert AndAlso .MovieExtrathumbsExpertSingle Then
                                FilenameList.Add(Path.Combine(strFileParentPath, "extrathumbs"))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainFanart
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(Path.Combine(strMainPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieFanartBoxee) Then FilenameList.Add(Path.Combine(strMainPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertVTS) Then
                                For Each a In .MovieFanartExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(Path.Combine(strMainPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(Path.Combine(Directory.GetParent(strFileParentPath).FullName, "index-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).Name), ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName, Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).Name), ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertBDMV) Then
                                For Each a In .MovieFanartExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso bIsVideoTSFile AndAlso .MovieFanartFrodo) Then FilenameList.Add(Path.Combine(strFileParentPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseFrodo AndAlso Not bIsVideoTSFile AndAlso .MovieFanartFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieFanartBoxee) Then FilenameList.Add(Path.Combine(strFileParentPath, "fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".fanart.jpg"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieFanartExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieFanartExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieFanartExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-fanart.jpg"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".fanart.jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertMulti) Then
                                For Each a In .MovieFanartExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainKeyArt
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieKeyArtExtended) Then FilenameList.Add(Path.Combine(strMainPath, "keyart.jpg"))
                            'If bForced OrElse (.MovieUseAD AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strMainPath, "landscape.jpg"))
                            'If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertVTS) Then
                            '    For Each a In .MovieLandscapeExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            '        If .MovieUseBaseDirectoryExpertVTS Then
                            '            FilenameList.Add(Path.Combine(Directory.GetParent(strFileParentPath).FullName, a.Replace("<filename>", strFileName)))
                            '        Else
                            '            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '        End If
                            '    Next
                            'End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieKeyArtExtended) Then FilenameList.Add(Path.Combine(strMainPath, "keyart.jpg"))
                            'If bForced OrElse (.MovieUseAD AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strMainPath, "landscape.jpg"))
                            'If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertBDMV) Then
                            '    For Each a In .MovieLandscapeExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            '        If .MovieUseBaseDirectoryExpertBDMV Then
                            '            FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName, a.Replace("<filename>", strFileName)))
                            '        Else
                            '            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '        End If
                            '    Next
                            'End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso bIsVideoTSFile AndAlso .MovieKeyArtExtended) Then FilenameList.Add(Path.Combine(strFileParentPath, "keyart.jpg"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not bIsVideoTSFile AndAlso .MovieKeyArtExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-keyart.jpg"))
                            'If bForced OrElse (.MovieUseAD AndAlso bIsVideoTSFile AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "landscape.jpg"))
                            'If bForced OrElse (.MovieUseAD AndAlso Not bIsVideoTSFile AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "landscape.jpg"))
                            'If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                            '    For Each a In .MovieLandscapeExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            '        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '    Next
                            'ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertSingle) Then
                            '    If .MovieStackExpertSingle Then
                            '        For Each a In .MovieLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            '            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFilename)))

                            '            If .MovieUnstackExpertSingle Then
                            '                FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '            End If
                            '        Next
                            '    Else
                            '        For Each a In .MovieLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            '            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '        Next
                            '    End If
                            'End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieKeyArtExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-keyart.jpg"))
                            'If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertMulti) Then
                            '    For Each a In .MovieLandscapeExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                            '        If .MovieStackExpertMulti Then
                            '            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFilename)))

                            '            If .MovieUnstackExpertMulti Then
                            '                FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '            End If
                            '        Else
                            '            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileName)))
                            '        End If
                            '    Next
                            'End If
                        End If
                    End With

                Case Enums.ModifierType.MainLandscape
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieLandscapeExtended) Then FilenameList.Add(Path.Combine(strMainPath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strMainPath, "landscape.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertVTS) Then
                                For Each a In .MovieLandscapeExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieLandscapeExtended) Then FilenameList.Add(Path.Combine(strMainPath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strMainPath, "landscape.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertBDMV) Then
                                For Each a In .MovieLandscapeExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseExtended AndAlso bIsVideoTSFile AndAlso .MovieLandscapeExtended) Then FilenameList.Add(Path.Combine(strFileParentPath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseExtended AndAlso Not bIsVideoTSFile AndAlso .MovieLandscapeExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso bIsVideoTSFile AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "landscape.jpg"))
                            If bForced OrElse (.MovieUseAD AndAlso Not bIsVideoTSFile AndAlso .MovieLandscapeAD) Then FilenameList.Add(Path.Combine(strFileParentPath, "landscape.jpg"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieLandscapeExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseExtended AndAlso .MovieLandscapeExtended) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-landscape.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertMulti) Then
                                For Each a In .MovieLandscapeExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainNFO
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(strMainPath, "movie.nfo"))
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieNFOBoxee) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(Directory.GetParent(strFileParentPath).FullName, Path.DirectorySeparatorChar, Directory.GetParent(strFileParentPath).Name, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertVTS) Then
                                For Each a In .MovieNFOExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(Path.Combine(strMainPath, "movie.nfo"))
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(Path.Combine(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName).FullName, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).Name, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertBDMV) Then
                                For Each a In .MovieNFOExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso bIsVideoTSFile) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not bIsVideoTSFile) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieNFOBoxee AndAlso bIsVideoTSFile) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MovieNFOBoxee AndAlso Not bIsVideoTSFile) Then FilenameList.Add(Path.Combine(strFileParentPath, "movie.nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                If Not String.IsNullOrEmpty(.MovieNFOExpertVTS) Then
                                    For Each a In .MovieNFOExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieNFOExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieNFOExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieNFOFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieNFOEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieNFONMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieNFOYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertMulti) Then
                                For Each a In .MovieNFOExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainPoster
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MoviePosterFrodo) Then FilenameList.Add(Path.Combine(strMainPath, "poster.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MoviePosterBoxee) Then FilenameList.Add(Path.Combine(strMainPath, "folder.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MoviePosterEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MoviePosterNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName, Directory.GetParent(strFileParentPath).Name), ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MoviePosterYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertVTS) Then
                                For Each a In .MoviePosterExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MoviePosterFrodo) Then FilenameList.Add(Path.Combine(strMainPath, "poster.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MoviePosterEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MoviePosterNMJ) Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(strFileParentPath).FullName).Name), ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MoviePosterYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertBDMV) Then
                                For Each a In .MoviePosterExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso bIsVideoTSFile AndAlso .MoviePosterFrodo) Then FilenameList.Add(Path.Combine(strFileParentPath, "poster.jpg"))
                            If bForced OrElse (.MovieUseFrodo AndAlso Not bIsVideoTSFile AndAlso .MoviePosterFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-poster.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MoviePosterBoxee) Then FilenameList.Add(Path.Combine(strFileParentPath, "folder.jpg"))
                            If bForced OrElse (.MovieUseEden AndAlso .MoviePosterEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MoviePosterNMJ) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MoviePosterYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".jpg"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MoviePosterExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MoviePosterExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MoviePosterExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieFanartFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-poster.jpg"))
                            If bForced OrElse (.MovieUseBoxee AndAlso .MoviePosterBoxee) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".tbn"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieFanartEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".tbn"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieFanartNMJ) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, ".jpg"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieFanartYAMJ) Then FilenameList.Add(String.Concat(String.Concat(strFilePathWoExt, ".jpg")))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertMulti) Then
                                For Each a In .MoviePosterExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainSubtitle
                    With Master.eSettings
                        If bIsVideoTS Then
                            FilenameList.Add(Path.Combine(strMainPath, "subs"))
                        ElseIf bIsBDMV Then
                            FilenameList.Add(Path.Combine(strMainPath, "subs"))
                        ElseIf bIsSingle Then
                            FilenameList.Add(Path.Combine(strFileParentPath, "subs"))
                            FilenameList.Add(strFileParentPath)
                        End If
                    End With

                Case Enums.ModifierType.MainTheme
                    With Master.eSettings
                        If bIsVideoTS Then
                            If .MovieThemeTvTunesEnable Then
                                If .MovieThemeTvTunesMoviePath Then FilenameList.Add(Path.Combine(strMainPath, "theme"))
                                If .MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.MovieThemeTvTunesCustomPath, "theme"))
                                If .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir) Then FilenameList.Add(Path.Combine(strMainPath, .MovieThemeTvTunesSubDir, "theme"))
                            End If
                        ElseIf bIsBDMV Then
                            If .MovieThemeTvTunesEnable Then
                                If .MovieThemeTvTunesMoviePath Then FilenameList.Add(Path.Combine(strMainPath, "theme"))
                                If .MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.MovieThemeTvTunesCustomPath, "theme"))
                                If .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir) Then FilenameList.Add(Path.Combine(strMainPath, .MovieThemeTvTunesSubDir, "theme"))
                            End If
                        Else
                            If .MovieThemeTvTunesEnable Then
                                If .MovieThemeTvTunesMoviePath Then FilenameList.Add(Path.Combine(strFileParentPath, "theme"))
                                If .MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.MovieThemeTvTunesCustomPath, "theme"))
                                If .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir) Then FilenameList.Add(Path.Combine(strMainPath, .MovieThemeTvTunesSubDir, "theme"))
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainTrailer
                    With Master.eSettings
                        If bIsVideoTS Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertVTS) Then
                                For Each a In .MovieTrailerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsBDMV Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo AndAlso Not .MovieXBMCProtectVTSBDMV) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(Path.Combine(strMainPath, "movie-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertBDMV) Then
                                For Each a In .MovieTrailerExpertBDMV.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(strMainPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        ElseIf bIsSingle Then
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".[trailer]"))
                            If .MovieUseExpert AndAlso bIsVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieTrailerExpertVTS.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieTrailerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieTrailerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    Next
                                End If
                            End If
                        Else
                            If bForced OrElse (.MovieUseFrodo AndAlso .MovieTrailerFrodo) Then FilenameList.Add(String.Concat(strStackedFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseEden AndAlso .MovieTrailerEden) Then FilenameList.Add(String.Concat(strFilePathWoExt, "-trailer"))
                            If bForced OrElse (.MovieUseNMJ AndAlso .MovieTrailerNMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".[trailer]"))
                            If bForced OrElse (.MovieUseYAMJ AndAlso .MovieTrailerYAMJ) Then FilenameList.Add(String.Concat(strFilePathWoExt, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertMulti) Then
                                For Each a In .MovieTrailerExpertMulti.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieStackExpertMulti Then
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strStackedFileNameWoExt)))

                                        If .MovieUnstackExpertMulti Then
                                            FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                        End If
                                    Else
                                        FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.MainWatchedFile
                    With Master.eSettings
                        If bIsVideoTS Then
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".watched"))
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso Not String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(.MovieYAMJWatchedFolder, strMainPathName), ".watched"))
                        ElseIf bIsBDMV Then
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(strMainPath, strMainPathName), ".watched"))
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso Not String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(.MovieYAMJWatchedFolder, strMainPathName), ".watched"))
                        Else
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(strFilePath, ".watched"))
                            If .MovieUseYAMJ AndAlso .MovieYAMJWatchedFile AndAlso Not String.IsNullOrEmpty(.MovieYAMJWatchedFolder) Then FilenameList.Add(String.Concat(Path.Combine(.MovieYAMJWatchedFolder, strMainPathName), ".watched"))
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
        Private Shared Function Movieset(ByVal dbElement As Database.DBElement, ByVal mType As Enums.ModifierType, Optional ByVal bForceOldTitle As Boolean = False) As List(Of String)
            Dim FilenameList As New List(Of String)

            If String.IsNullOrEmpty(dbElement.MainDetails.Title) Then Return FilenameList

            Dim strSetTitle As String = If(bForceOldTitle, dbElement.MainDetails.Title_Old, dbElement.MainDetails.Title)
            For Each cInvalid As Char In Path.GetInvalidFileNameChars
                strSetTitle = strSetTitle.Replace(cInvalid, "-")
            Next

            Select Case mType
                Case Enums.ModifierType.MainBanner
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetBannerExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-banner.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetBannerMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(strSetTitle, "-banner.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetBannerExpertSingle) Then
                            For Each a In .MovieSetBannerExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearArt
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetClearArtExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-clearart.png")))
                        If .MovieSetUseMSAA AndAlso .MovieSetClearArtMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(strSetTitle, "-clearart.png")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetClearArtExpertSingle) Then
                            For Each a In .MovieSetClearArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearLogo
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetClearLogoExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-clearlogo.png")))
                        If .MovieSetUseMSAA AndAlso .MovieSetClearLogoMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(strSetTitle, "-logo.png")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetClearLogoExpertSingle) Then
                            For Each a In .MovieSetClearLogoExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainDiscArt
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetDiscArtExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-discart.png")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetDiscArtExpertSingle) Then
                            For Each a In .MovieSetDiscArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainFanart
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetFanartExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-fanart.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetFanartMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(strSetTitle, "-fanart.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetFanartExpertSingle) Then
                            For Each a In .MovieSetFanartExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainKeyArt
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetKeyArtExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-keyart.jpg")))
                        'If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetKeyArtExpertSingle) Then
                        '    For Each a In .MovieSetKeyArtExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                        '        FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", fSetTitle)))
                        '    Next
                        'End If
                    End With

                Case Enums.ModifierType.MainLandscape
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetLandscapeExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-landscape.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetLandscapeMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(strSetTitle, "-landscape.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetLandscapeExpertSingle) Then
                            For Each a In .MovieSetLandscapeExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainNFO
                    With Master.eSettings
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetNFOExpertSingle) Then
                            For Each a In .MovieSetNFOExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainPoster
                    With Master.eSettings
                        If .MovieSetUseExtended AndAlso .MovieSetPosterExtended AndAlso Not String.IsNullOrEmpty(.MovieSetPathExtended) Then FilenameList.Add(Path.Combine(.MovieSetPathExtended, String.Concat(strSetTitle, "-poster.jpg")))
                        If .MovieSetUseMSAA AndAlso .MovieSetPosterMSAA AndAlso Not String.IsNullOrEmpty(.MovieSetPathMSAA) Then FilenameList.Add(Path.Combine(.MovieSetPathMSAA, String.Concat(strSetTitle, "-poster.jpg")))
                        If .MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(.MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(.MovieSetPosterExpertSingle) Then
                            For Each a In .MovieSetPosterExpertSingle.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(.MovieSetPathExpertSingle, a.Replace("<settitle>", strSetTitle)))
                            Next
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Private Shared Function TVEpisode(ByVal dbElement As Database.DBElement, ByVal mType As Enums.ModifierType) As List(Of String)
            Dim FilenameList As New List(Of String)

            If Not dbElement.FileItemSpecified Then Return FilenameList

            Dim strFileNameWoExt As String = Path.GetFileNameWithoutExtension(dbElement.FileItem.FirstPathFromStack)
            Dim strFileParentPath As String = Directory.GetParent(dbElement.FileItem.FirstPathFromStack).FullName
            Dim strFilePathWoExt As String = Path.Combine(strFileParentPath, strFileNameWoExt)
            Dim strMainPath As String = dbElement.FileItem.MainPath.FullName

            Dim bIsBDMV As Boolean = dbElement.FileItem.bIsBDMV
            Dim bIsVideoTS As Boolean = dbElement.FileItem.bIsVideoTS
            Dim bIsVideoTSFile As Boolean = strFileNameWoExt.ToLower = "video_ts"

            Select Case mType
                Case Enums.ModifierType.EpisodeActorThumbs
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVEpisodeActorThumbsFrodo Then FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>.jpg"))
                        If .TVUseExpert AndAlso .TVEpisodeActorThumbsExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodeActorThumbsExtExpert) Then
                            FilenameList.Add(String.Concat(Path.Combine(strFileParentPath, ".actors"), "\<placeholder>", .TVEpisodeActorThumbsExtExpert))
                        End If
                    End With

                Case Enums.ModifierType.EpisodeFanart
                    With Master.eSettings
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodeFanartExpert) Then
                            For Each a In .TVEpisodeFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.EpisodeNFO
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVEpisodeNFOFrodo Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                        If .TVUseBoxee AndAlso .TVEpisodeNFOBoxee Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                        If .TVUseEden Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                        If .TVUseYAMJ AndAlso .TVEpisodeNFOYAMJ Then FilenameList.Add(String.Concat(strFilePathWoExt, ".nfo"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodeNFOExpert) Then
                            For Each a In .TVEpisodeNFOExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.EpisodePoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVEpisodePosterFrodo Then FilenameList.Add(String.Concat(strFilePathWoExt, "-thumb.jpg"))
                        If .TVUseBoxee AndAlso .TVEpisodePosterBoxee Then FilenameList.Add(String.Concat(strFilePathWoExt, ".tbn"))
                        If .TVUseYAMJ AndAlso .TVEpisodePosterYAMJ Then FilenameList.Add(String.Concat(strFilePathWoExt, ".videoimage.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVEpisodePosterExpert) Then
                            For Each a In .TVEpisodePosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strFileParentPath, a.Replace("<filename>", strFileNameWoExt)))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.EpisodeSubtitle
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(strFileParentPath, "subs"))
                        FilenameList.Add(strFileParentPath)
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Private Shared Function TVSeason(ByVal dbElement As Database.DBElement, ByVal mType As Enums.ModifierType) As List(Of String)
            Dim FilenameList As New List(Of String)
            Dim bInside As Boolean = False

            If String.IsNullOrEmpty(dbElement.ShowPath) Then Return FilenameList

            Dim strEpisodePath As String = String.Empty
            Dim strSeasonPath As String = Common.GetSeasonDirectoryFromShowPath(dbElement.ShowPath, dbElement.MainDetails.Season)
            Dim strShowPath As String = dbElement.ShowPath
            Dim strSeasonNumber As String = dbElement.MainDetails.Season.ToString.PadLeft(2, Convert.ToChar("0"))
            Dim strSeasonFolder As String = Path.GetFileName(strSeasonPath)

            'checks if there are separate season folders
            If Not String.IsNullOrEmpty(strSeasonPath) AndAlso Not strSeasonPath = strShowPath Then
                bInside = True
            End If

            'get first episode of season (YAMJ need that for episodes without separate season folders)
            Try
                If Master.eSettings.TVUseYAMJ AndAlso Not bInside Then
                    Dim nFilter As New SmartFilter.Filter(Enums.ContentType.TVEpisode)
                    nFilter.Rules.Add(New SmartFilter.Rule With {
                                          .Field = Database.ColumnName.idShow,
                                          .[Operator] = SmartFilter.Operators.Is,
                                          .Value = dbElement.ShowID})
                    nFilter.Rules.Add(New SmartFilter.Rule With {
                                          .Field = Database.ColumnName.SeasonNumber,
                                          .[Operator] = SmartFilter.Operators.Is,
                                          .Value = dbElement.MainDetails.Season})
                    nFilter.OrderBy.Add(New SmartFilter.Order With {
                                            .SortedBy = Database.ColumnName.EpisodeNumber})
                    Dim dtEpisodes = Master.DB.GetTVSeasons(nFilter)
                    'Master.DB.FillDataTable(dtEpisodes, String.Format("SELECT * FROM {0} INNER JOIN {1} ON ({1}.{2} = {0}.{2}) WHERE {3} = {4} AND {5} = {6} ORDER BY {7};",
                    '                                                  Database.Helpers.GetTableName(Database.TableName.episode),
                    '                                                  Database.Helpers.GetTableName(Database.TableName.file),
                    '                                                  Database.Helpers.GetMainIdName(Database.TableName.episode),
                    '                                                  Database.Helpers.GetMainIdName(Database.TableName.tvshow),
                    '                                                  dbElement.ShowID,
                    '                                                  Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber),
                    '                                                  dbElement.TVSeason.Season,
                    '                                                  Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber))
                    '                                                  )
                    If dtEpisodes.Rows.Count > 0 Then
                        strEpisodePath = dtEpisodes.Rows(0).Item(Database.Helpers.GetColumnName(Database.ColumnName.Path)).ToString
                        If Not String.IsNullOrEmpty(strEpisodePath) Then
                            strEpisodePath = Path.Combine(Directory.GetParent(strEpisodePath).FullName, Path.GetFileNameWithoutExtension(strEpisodePath))
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Select Case mType
                Case Enums.ModifierType.SeasonBanner
                    With Master.eSettings
                        If dbElement.MainDetails.Season = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(strShowPath, "season-specials-banner.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, String.Concat(strSeasonFolder, ".banner.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(strEpisodePath) Then FilenameList.Add(String.Concat(strEpisodePath, ".banner.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonBannerExpert) Then
                                For Each a In .TVSeasonBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonBannerFrodo Then FilenameList.Add(Path.Combine(strShowPath, String.Format("season{0}-banner.jpg", strSeasonNumber)))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, String.Concat(strSeasonFolder, ".banner.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(strEpisodePath) Then FilenameList.Add(String.Concat(strEpisodePath, ".banner.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonBannerExpert) Then
                                For Each a In .TVSeasonBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.SeasonFanart
                    With Master.eSettings
                        If dbElement.MainDetails.Season = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(strShowPath, "season-specials-fanart.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, String.Concat(strSeasonFolder, ".fanart.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(strEpisodePath) Then FilenameList.Add(String.Concat(strEpisodePath, ".fanart.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonFanartExpert) Then
                                For Each a In .TVSeasonFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(strShowPath, String.Format("season{0}-fanart.jpg", strSeasonNumber)))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, String.Concat(strSeasonFolder, ".fanart.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(strEpisodePath) Then FilenameList.Add(String.Concat(strEpisodePath, ".fanart.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonFanartExpert) Then
                                For Each a In .TVSeasonFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.SeasonLandscape
                    With Master.eSettings
                        If dbElement.MainDetails.Season = 0 Then 'season specials
                            If .TVUseExtended AndAlso .TVSeasonLandscapeExtended Then FilenameList.Add(Path.Combine(strShowPath, "season-specials-landscape.jpg"))
                            If .TVUseAD AndAlso .TVSeasonLandscapeAD Then FilenameList.Add(Path.Combine(strShowPath, "season-specials-landscape.jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonLandscapeExpert) Then
                                For Each a In .TVSeasonLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseExtended AndAlso .TVSeasonLandscapeExtended Then FilenameList.Add(Path.Combine(strShowPath, String.Format("season{0}-landscape.jpg", strSeasonNumber)))
                            If .TVUseAD AndAlso .TVSeasonLandscapeAD Then FilenameList.Add(Path.Combine(strShowPath, String.Format("season{0}-landscape.jpg", strSeasonNumber)))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonLandscapeExpert) Then
                                For Each a In .TVSeasonLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With

                Case Enums.ModifierType.SeasonPoster
                    With Master.eSettings
                        If dbElement.MainDetails.Season = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(strShowPath, "season-specials-poster.jpg"))
                            If .TVUseBoxee AndAlso .TVSeasonPosterBoxee AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, "poster.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, String.Concat(strSeasonFolder, ".jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(strEpisodePath) Then FilenameList.Add(String.Concat(strEpisodePath, ".jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonPosterExpert) Then
                                For Each a In .TVSeasonPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(strShowPath, String.Format("season{0}-poster.jpg", strSeasonNumber)))
                            If .TVUseBoxee AndAlso .TVSeasonPosterBoxee AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, "poster.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(strSeasonPath, String.Concat(strSeasonFolder, ".jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso Not bInside AndAlso Not String.IsNullOrEmpty(strEpisodePath) Then FilenameList.Add(String.Concat(strEpisodePath, ".jpg"))
                            If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVSeasonPosterExpert) Then
                                For Each a In .TVSeasonPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                    If Not (a.Contains("<seasonpath>") AndAlso Not bInside) Then
                                        Dim sPath As String = a.Replace("<seasonpath>", strSeasonPath)
                                        sPath = String.Format(sPath, strSeasonNumber, dbElement.MainDetails.Season, strSeasonNumber) 'Season# padding: {0} = 01; {1} = 1; {2} = 01
                                        FilenameList.Add(Path.Combine(strShowPath, sPath))
                                    End If
                                Next
                            End If
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Private Shared Function TVShow(ByVal dbElement As Database.DBElement, ByVal mType As Enums.ModifierType) As List(Of String)
            Dim FilenameList As New List(Of String)

            If String.IsNullOrEmpty(dbElement.ShowPath) Then Return FilenameList

            Dim strShowPath As String = dbElement.ShowPath
            Dim strShowPathName As String = Path.GetDirectoryName(strShowPath)

            Select Case mType
                Case Enums.ModifierType.MainActorThumbs
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowActorThumbsFrodo Then FilenameList.Add(String.Concat(Path.Combine(strShowPath, ".actors"), "\<placeholder>.jpg"))
                        If .TVUseExpert AndAlso .TVShowActorThumbsExpert AndAlso Not String.IsNullOrEmpty(.TVShowActorThumbsExtExpert) Then
                            FilenameList.Add(String.Concat(Path.Combine(strShowPath, ".actors"), "\<placeholder>", .TVShowActorThumbsExtExpert))
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsBanner
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonBannerFrodo Then FilenameList.Add(Path.Combine(strShowPath, "season-all-banner.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsBannerExpert) Then
                            For Each a In .TVAllSeasonsBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsFanart
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(strShowPath, "season-all-fanart.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsFanartExpert) Then
                            For Each a In .TVAllSeasonsFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsLandscape
                    With Master.eSettings
                        If .TVUseAD AndAlso .TVSeasonLandscapeAD Then FilenameList.Add(Path.Combine(strShowPath, "season-all-landscape.jpg"))
                        If .TVUseExtended AndAlso .TVSeasonLandscapeExtended Then FilenameList.Add(Path.Combine(strShowPath, "season-all-landscape.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsLandscapeExpert) Then
                            For Each a In .TVAllSeasonsLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.AllSeasonsPoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(strShowPath, "season-all-poster.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVAllSeasonsPosterExpert) Then
                            For Each a In .TVAllSeasonsPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainBanner
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowBannerFrodo Then FilenameList.Add(Path.Combine(strShowPath, "banner.jpg"))
                        If .TVUseBoxee AndAlso .TVShowBannerBoxee Then FilenameList.Add(Path.Combine(strShowPath, "banner.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowBannerYAMJ Then FilenameList.Add(Path.Combine(strShowPath, String.Concat(strShowPathName, ".banner.jpg")))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowBannerExpert) Then
                            For Each a In .TVShowBannerExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainCharacterArt
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowCharacterArtExtended Then FilenameList.Add(Path.Combine(strShowPath, "characterart.png"))
                        If .TVUseAD AndAlso .TVShowCharacterArtAD Then FilenameList.Add(Path.Combine(strShowPath, "character.png"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowCharacterArtExpert) Then
                            For Each a In .TVShowCharacterArtExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearArt
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowClearArtExtended Then FilenameList.Add(Path.Combine(strShowPath, "clearart.png"))
                        If .TVUseAD AndAlso .TVShowClearArtAD Then FilenameList.Add(Path.Combine(strShowPath, "clearart.png"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowClearArtExpert) Then
                            For Each a In .TVShowClearArtExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainClearLogo
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowClearLogoExtended Then FilenameList.Add(Path.Combine(strShowPath, "clearlogo.png"))
                        If .TVUseAD AndAlso .TVShowClearLogoAD Then FilenameList.Add(Path.Combine(strShowPath, "logo.png"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowClearLogoExpert) Then
                            For Each a In .TVShowClearLogoExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainExtrafanarts
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowExtrafanartsFrodo Then FilenameList.Add(Path.Combine(strShowPath, "extrafanart"))
                        If .TVUseEden AndAlso .TVShowExtrafanartsFrodo Then FilenameList.Add(Path.Combine(strShowPath, "extrafanart"))
                        If .TVUseExpert AndAlso .TVShowExtrafanartsExpert Then FilenameList.Add(Path.Combine(strShowPath, "extrafanart"))
                    End With

                Case Enums.ModifierType.MainFanart
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowFanartFrodo Then FilenameList.Add(Path.Combine(strShowPath, "fanart.jpg"))
                        If .TVUseBoxee AndAlso .TVShowFanartBoxee Then FilenameList.Add(Path.Combine(strShowPath, "fanart.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowFanartYAMJ Then FilenameList.Add(Path.Combine(strShowPath, String.Concat(strShowPathName, ".fanart.jpg")))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowFanartExpert) Then
                            For Each a In .TVShowFanartExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainKeyArt
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowKeyArtExtended Then FilenameList.Add(Path.Combine(strShowPath, "keyart.jpg"))
                        'If .TVUseAD AndAlso .TVShowLandscapeAD Then FilenameList.Add(Path.Combine(fShowPath, "landscape.jpg"))
                        'If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowLandscapeExpert) Then
                        '    For Each a In .TVShowLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                        '        FilenameList.Add(Path.Combine(fShowPath, a))
                        '    Next
                        'End If
                    End With

                Case Enums.ModifierType.MainLandscape
                    With Master.eSettings
                        If .TVUseExtended AndAlso .TVShowLandscapeExtended Then FilenameList.Add(Path.Combine(strShowPath, "landscape.jpg"))
                        If .TVUseAD AndAlso .TVShowLandscapeAD Then FilenameList.Add(Path.Combine(strShowPath, "landscape.jpg"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowLandscapeExpert) Then
                            For Each a In .TVShowLandscapeExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainNFO
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowNFOFrodo Then FilenameList.Add(Path.Combine(strShowPath, "tvshow.nfo"))
                        If .TVUseBoxee AndAlso .TVShowNFOBoxee Then FilenameList.Add(Path.Combine(strShowPath, "tvshow.nfo"))
                        If .TVUseYAMJ AndAlso .TVShowNFOYAMJ Then FilenameList.Add(Path.Combine(strShowPath, "tvshow.nfo"))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowNFOExpert) Then
                            For Each a In .TVShowNFOExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainPoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowPosterFrodo Then FilenameList.Add(Path.Combine(strShowPath, "poster.jpg"))
                        If .TVUseBoxee AndAlso .TVShowPosterBoxee Then FilenameList.Add(Path.Combine(strShowPath, "folder.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowPosterYAMJ Then FilenameList.Add(Path.Combine(strShowPath, String.Concat(strShowPathName, ".jpg")))
                        If .TVUseExpert AndAlso Not String.IsNullOrEmpty(.TVShowPosterExpert) Then
                            For Each a In .TVShowPosterExpert.Split(New String() {","c}, StringSplitOptions.RemoveEmptyEntries)
                                FilenameList.Add(Path.Combine(strShowPath, a))
                            Next
                        End If
                    End With

                Case Enums.ModifierType.MainTheme
                    With Master.eSettings
                        If .TVShowThemeTvTunesEnable Then
                            If .TVShowThemeTvTunesShowPath Then FilenameList.Add(Path.Combine(strShowPath, "theme"))
                            If .TVShowThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.TVShowThemeTvTunesCustomPath) Then FilenameList.Add(Path.Combine(.TVShowThemeTvTunesCustomPath, "theme"))
                            If .TVShowThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.TVShowThemeTvTunesSubDir) Then FilenameList.Add(Path.Combine(strShowPath, .TVShowThemeTvTunesSubDir, "theme"))
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

        Dim logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Events"
        ''' <summary>
        ''' Event that is raised when SortFiles desires the progress indicator to be updated
        ''' </summary>
        ''' <param name="iPercent">Percentage complete</param>
        ''' <param name="sStatus">Message to be displayed alongside the progress indicator</param>
        ''' <remarks></remarks>
        Public Event ProgressUpdated(ByVal iPercent As Integer, ByVal sStatus As String)

#End Region 'Events

#Region "Methods"
        ''' <summary>
        ''' Reorganize the media files in the given folder into subfolders.
        ''' </summary>
        ''' <param name="strSourcePath">Path to be sorted</param>
        ''' <remarks>Occasionally a directory will contain multiple media files (and meta-files) and 
        ''' this method will walk through the files in that directory and move each to its own unique subdirectory.
        ''' This will move all files with the same core name, without extension or fanart/trailer endings.</remarks>
        Public Sub SortFiles(ByVal strSourcePath As String)
            'TODO Need to test what happens if sPath points to an existing FILE (and not just a directory)
            Dim iCount As Integer = 0

            Try
                If Directory.Exists(strSourcePath) Then
                    'Get information about files in the directory
                    Dim di As New DirectoryInfo(strSourcePath)
                    Dim lFi As New List(Of FileInfo)
                    Dim lMediaList As IOrderedEnumerable(Of FileInfo)

                    'Create a List of files in the directory
                    Try
                        lFi.AddRange(di.GetFiles())
                    Catch
                    End Try

                    'Create a list of all media files with a valid extension in the directory
                    lMediaList = lFi.Where(Function(f) Master.eSettings.Options.FileSystem.ValidVideoExtensions.Contains(f.Extension.ToLower) AndAlso
                             Not Regex.IsMatch(f.Name, String.Concat("[^\w\s]\s?(", AdvancedSettings.GetSetting("NotValidFileContains", "trailer|sample"), ")"), RegexOptions.IgnoreCase) AndAlso ((Not Master.eSettings.Movie.SourceSettings.SkipLessThanSpecified OrElse f.Length >= Master.eSettings.Movie.SourceSettings.SkipLessThan * 1048576))).OrderBy(Function(f) f.FullName)

                    'For each valid file in the directory...
                    For Each sFile As FileInfo In lMediaList
                        Dim nMovie As New Database.DBElement(Enums.ContentType.Movie) With {.FileItem = New FileItem(sFile.FullName), .IsSingle = False}
                        RaiseEvent ProgressUpdated((iCount \ lMediaList.Count), String.Concat(Master.eLang.GetString(219, "Moving "), sFile.Name))

                        'create a new directory for the movie
                        Dim strNewPath As String = Path.Combine(strSourcePath, Path.GetFileNameWithoutExtension(Common.RemoveStackingMarkers(nMovie.FileItem.StackedFileName)))
                        If Not Directory.Exists(strNewPath) Then
                            Directory.CreateDirectory(strNewPath)
                        End If

                        'move movie to the new directory
                        sFile.MoveTo(Path.Combine(strNewPath, Path.GetFileName(nMovie.FileItem.FirstPathFromStack)))

                        'search for files that belong to this movie
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainBanner, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainCharacterArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainClearArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainClearLogo, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainDiscArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainFanart, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainKeyArt, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainLandscape, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainNFO, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainPoster, True)
                            If File.Exists(a) Then File.Move(a, Path.Combine(strNewPath, Path.GetFileName(a)))
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainTheme, True)
                            For Each t As String In Master.eSettings.Options.FileSystem.ValidThemeExtensions
                                If File.Exists(String.Concat(a, t)) Then File.Move(String.Concat(a, t), Path.Combine(strNewPath, Path.GetFileName(String.Concat(a, t))))
                            Next
                        Next
                        For Each a In FileNames.GetFileNames(nMovie, Enums.ModifierType.MainTrailer, True)
                            For Each t As String In Master.eSettings.Options.FileSystem.ValidVideoExtensions
                                If File.Exists(String.Concat(a, t)) Then File.Move(String.Concat(a, t), Path.Combine(strNewPath, Path.GetFileName(String.Concat(a, t))))
                            Next
                        Next
                        'TODO: search  more files like subtitles
                        iCount += 1
                    Next

                    RaiseEvent ProgressUpdated((iCount \ lMediaList.Count), Master.eLang.GetString(362, "Done "))
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Methods

    End Module

    Public Class Stacking

#Region "Methods"

        Public Shared Function ConstructStackedPath(tItemList As FileItemList, iStack As List(Of Integer)) As String
            Dim strStackedPath As String = "stack://"
            Dim strFilename As String = tItemList.GetItem(iStack(0)).FileInfo.FullName
            'double escape any occurence of commas
            strFilename = strFilename.Replace(",", ",,")
            strStackedPath += strFilename

            Dim i As Integer = 1
            While i < iStack.Count
                strStackedPath += " , "
                strFilename = tItemList.GetItem(iStack(i)).FileInfo.FullName

                'double escape any occurence of commas
                strFilename = strFilename.Replace(",", ",,")
                strStackedPath += strFilename
                i += 1
            End While

            Return strStackedPath
        End Function

        Public Shared Function ConstructStackedPath(tPaths As List(Of String)) As String
            If tPaths.Count = 0 Then Return String.Empty
            If Not tPaths.Count > 1 Then Return tPaths(0)

            Dim strStackedPath As String = "stack://"
            Dim strFilename As String = tPaths(0)
            'double escape any occurence of commas
            strFilename = strFilename.Replace(",", ",,")
            strStackedPath += strFilename

            Dim i As Integer = 1
            While i < tPaths.Count
                strStackedPath += " , "
                strFilename = tPaths(i)

                'double escape any occurence of commas
                strFilename = strFilename.Replace(",", ",,")
                strStackedPath += strFilename
                i += 1
            End While

            Return strStackedPath
        End Function
        ''' <summary>
        ''' Return the full path of the first file from the stacked list or the full path of a non-stacked file
        ''' </summary>
        ''' <param name="strFilename"></param>
        ''' <returns></returns>
        Public Shared Function GetFirstPathFromStack(strFilename As String) As String
            Dim strFirstFilename As String = String.Empty
            If strFilename.Contains(" , ") Then
                strFirstFilename = strFilename.Substring(0, strFilename.IndexOf(" , "))
            Else 'single filed stacks - should really not happen
                strFirstFilename = strFilename
            End If

            'remove "stack://" from the folder
            strFirstFilename = strFirstFilename.Replace("stack://", String.Empty)
            strFirstFilename = strFirstFilename.Replace(",,", ",")

            Return strFirstFilename
        End Function

        Public Shared Function GetPathList(strFilename As String) As List(Of String)
            Dim lstPaths As New List(Of String)
            'format Is:
            'stack://file1 , file2 , file3 , file4
            'filenames with commas are double escaped (ie replaced with ,,), thus the " , " separator used.

            'remove stack:// from the beginning
            strFilename = strFilename.Replace("stack://", String.Empty)

            lstPaths = Regex.Split(strFilename, " , ").ToList
            For Each nPath In lstPaths
                nPath = nPath.Replace(",,", ",")
            Next

            Return lstPaths
        End Function
        ''' <summary>
        ''' Returns the stacked full file path (e.g. "C:\Avatar\Avatar.mkv" instead of "C:\Avatar\Avatar-cd1.mkv")
        ''' </summary>
        ''' <param name="strPath"></param>
        ''' <returns></returns>
        Public Shared Function GetStackedPath(strPath As String) As String
            If String.IsNullOrEmpty(strPath) Then Return String.Empty

            If strPath.StartsWith("stack://") Then
                Dim strFirstStackedFile As String = GetFirstPathFromStack(strPath)
                Dim strFileStackingPattern As String = AdvancedSettings.GetSetting("FileStacking", "(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck])[ _.-]*[0-9]+)(.*?)(\.[^.]+)$")
                Dim rResultFileItem1 As Match = Regex.Match(strFirstStackedFile, strFileStackingPattern, RegexOptions.IgnoreCase)
                If rResultFileItem1.Success Then
                    Dim strTitle1 As String = rResultFileItem1.Groups(1).Value
                    Dim strVolume1 As String = rResultFileItem1.Groups(2).Value
                    Dim strIgnore1 As String = rResultFileItem1.Groups(3).Value
                    Dim strExtension1 As String = rResultFileItem1.Groups(4).Value

                    Return String.Concat(strTitle1, strIgnore1, strExtension1)
                End If
            End If
            Return strPath
        End Function

#End Region 'Methods

    End Class

    Public Class VirtualCloneDrive

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private fiImage As FileInfo
        Private fiVirtualCloneDriveBin As FileInfo = New FileInfo(Master.eSettings.Options.FileSystem.VirtualDriveBinPath)

#End Region 'Fields

#Region "Properties"

        Public Property IsReady As Boolean = False
        Public Property Path As String = String.Empty

#End Region 'Properties

#Region "Constructors"

        Public Sub New(ByVal imagepath As String)
            fiImage = New FileInfo(imagepath)
            LoadDiscImage()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Private Sub LoadDiscImage()
            Dim strDriveLetter As String = Master.eSettings.Options.FileSystem.VirtualDriveDriveLetter
            If Not String.IsNullOrEmpty(strDriveLetter) AndAlso fiVirtualCloneDriveBin.Exists AndAlso fiImage.Exists Then
                'Unmount e.g. ""C:\Program Files\Elaborate Bytes\VirtualCloneDrive\VCDMount.exe" /u"
                Functions.Run_Process(fiVirtualCloneDriveBin.FullName, "/u", False, True)
                'Mount ISO on virtual drive, e.g. ""C:\Program Files (x86)\Elaborate Bytes\VirtualCloneDrive\vcdmount.exe" "U:\isotest\test2iso.ISO""
                Functions.Run_Process(fiVirtualCloneDriveBin.FullName, String.Format("""{0}""", fiImage.FullName), False, True)

                Dim diVirtualDrive = New DriveInfo(String.Concat(strDriveLetter, ":\"))

                Dim iLimit As Integer
                While Not diVirtualDrive.IsReady
                    If iLimit = 10000 Then
                        logger.Warn("[FileUtils] [VirtualDrive] [LoadDiscImage] wait until the virtual drive is ready timeout")
                        Exit While
                    Else
                        logger.Trace("[FileUtils] [VirtualDrive] [LoadDiscImage] wait until the virtual drive is ready ...")
                        Threading.Thread.Sleep(500)
                        iLimit += 500
                    End If
                End While
                If diVirtualDrive.IsReady Then
                    logger.Trace("[FileUtils] [VirtualDrive] [LoadDiscImage] virtual drive is ready")
                    IsReady = True
                    Path = String.Concat(strDriveLetter, ":\")
                End If
            End If
        End Sub

        Public Sub UnmountDiscImage()
            Functions.Run_Process(fiVirtualCloneDriveBin.FullName, "/u", False, True)
            IsReady = False
            Path = String.Empty
        End Sub

#End Region 'Methods

    End Class

End Namespace