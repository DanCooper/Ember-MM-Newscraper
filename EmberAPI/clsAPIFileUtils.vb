'################################################################################
'#                             EMBER MEDIA MANAGER                              #
'################################################################################
'################################################################################
'# This file is part of Ember Media Manager.                                    #
'#                                                                              #
'# Ember Media Manager is free software: you can redistribute it and/or modify  #
'# it under the terms of the GNU General Public License as published by         #
'# the Free Software Foundation, either version 3 of the License, or            #
'# (at your option) any later version.                                          #
'#                                                                              #
'# Ember Media Manager is distributed in the hope that it will be useful,       #
'# but WITHOUT ANY WARRANTY; without even the implied warranty of               #
'# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
'# GNU General Public License for more details.                                 #
'#                                                                              #
'# You should have received a copy of the GNU General Public License            #
'# along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
'################################################################################

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports NLog

Namespace FileUtils

    Public Class Common

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Methods"

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
        ''' <remarks></remarks>
        Public Shared Function GetLongestFromRip(ByVal sPath As String, Optional ByVal ForceBDMV As Boolean = False) As String
            'TODO Needs error handling for when largest file is under 1GB. No default is set. Also, should error if path is not DVD or BR. Also, if ForceBDMV, complain if no files found
            Dim lFileList As New List(Of FileInfo)
            Select Case True
                Case isBDRip(sPath) OrElse ForceBDMV
                    lFileList.AddRange(New DirectoryInfo(Directory.GetParent(sPath).FullName).GetFiles("*.m2ts"))
                Case isVideoTS(sPath)
                    lFileList.AddRange(New DirectoryInfo(Directory.GetParent(sPath).FullName).GetFiles("*.vob"))
            End Select
            'Return filename/path of the largest file that is over 1 GB in size.
            Return lFileList.Where(Function(s) s.Length > 1073741824).OrderByDescending(Function(s) s.Length).Select(Function(s) s.FullName).FirstOrDefault
        End Function
        ''' <summary>
        ''' Determine whether the path provided contains a Blu-Ray image
        ''' </summary>
        ''' <param name="sPath">Path to be evaluated</param>
        ''' <returns><c>True</c> if the supplied path is determined to be a Blu-Ray path. <c>False</c> otherwise</returns>
        ''' <remarks>Two tests are performed. If the supplied path has an extension (such as if a .m2ts file was provided), check 
        ''' that the parent directory is "stream" and its parent is "bdmv"</remarks>
        Public Shared Function isBDRip(ByVal sPath As String) As Boolean
            'TODO Kludge. Consider FileSystemInfo.Attributes to detect if path is a file or directory, and proceed from there
            If String.IsNullOrEmpty(sPath) Then Return False
            If sPath.EndsWith(Path.DirectorySeparatorChar) OrElse sPath.EndsWith(Path.AltDirectorySeparatorChar) Then
                'The current/parent directory comparisons can't handle paths ending with a directory separator. Therefore, strip them out
                Return isBDRip(sPath.Substring(0, sPath.Length - 1))
            End If
            If Path.HasExtension(sPath) Then
                Return Directory.GetParent(sPath).Name.ToLower = "stream" AndAlso Directory.GetParent(Directory.GetParent(sPath).FullName).Name.ToLower = "bdmv"
            Else
                Return GetDirectory(sPath).ToLower = "stream" AndAlso Directory.GetParent(sPath).Name.ToLower = "bdmv"
            End If
        End Function
        ''' <summary>
        ''' Deermine whether the path provided contains a DVD image
        ''' </summary>
        ''' <param name="sPath">Path to be evaluated</param>
        ''' <returns><c>True</c> if the supplied path is determined to be a Blu-Ray path. <c>False</c> otherwise</returns>
        ''' <remarks>If the path is a file, check if parent is video_ts. Otherwise, it should be a directory, so see if it is video_ts</remarks>
        Public Shared Function isVideoTS(ByVal sPath As String) As Boolean
            'TODO Kludge. Consider FileSystemInfo.Attributes to detect if path is a file or directory, and proceed from there
            If String.IsNullOrEmpty(sPath) Then Return False
            If Path.HasExtension(sPath) Then
                Return Directory.GetParent(sPath).Name.ToLower = "video_ts"
            Else
                Return GetDirectory(sPath).ToLower = "video_ts"
            End If
        End Function
        ''' <summary>
        ''' Copy a file from one location to another using a stream/buffer
        ''' </summary>
        ''' <param name="sPathFrom">Old path of file to move.</param>
        ''' <param name="sPathTo">New path of file to move.</param>
        Public Shared Sub MoveFileWithStream(ByVal sPathFrom As String, ByVal sPathTo As String)
            'TODO Inefficient. Why not use system-provided FileInfo.MoveTo method. Instantaneous if on same system volume as a bonus.
            'TODO Should do validation checking on input parameters. Should handle Empty or invalid files. Should perhaps handle directory (and content) moves intelligently
            'TODO Badly named. Should instead be CopyFileWithStream, and use FileInfo.CopyTo method. 
            Try
                Using SourceStream As FileStream = New FileStream(String.Concat("", sPathFrom, ""), FileMode.Open, FileAccess.Read)
                    Using DestinationStream As FileStream = New FileStream(String.Concat("", sPathTo, ""), FileMode.Create, FileAccess.Write)
                        Dim StreamBuffer(Convert.ToInt32(SourceStream.Length - 1)) As Byte

                        SourceStream.Read(StreamBuffer, 0, StreamBuffer.Length)
                        DestinationStream.Write(StreamBuffer, 0, StreamBuffer.Length)

                        StreamBuffer = Nothing
                    End Using
                End Using
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name,ex)
            End Try
        End Sub
        ''' <summary>
        ''' Get the entire path and filename of a file, but without the extension
        ''' </summary>
        ''' <param name="sPath">Full path to file.</param>
        ''' <returns>Path and filename of a file, without the extension</returns>
        ''' <remarks>No validation is made on whether the path/file actually exists.</remarks>
        Public Shared Function RemoveExtFromPath(ByVal sPath As String) As String
            'TODO Dekker500 This method needs serious work. Invalid paths are not consistently handled. Need analysis on how to handle these properly
            If String.IsNullOrEmpty(sPath) Then Return String.Empty
            Try
                'If the path has no directories (only the root), short-circuit the routine and just return
                If sPath.Equals(Directory.GetDirectoryRoot(sPath)) Then Return sPath
                Return Path.Combine(Path.GetDirectoryName(sPath), Path.GetFileNameWithoutExtension(sPath))
                'Return Path.Combine(Directory.GetParent(sPath).FullName, Path.GetFileNameWithoutExtension(sPath))
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "Source: <" & sPath & ">", ex)
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
#End Region 'Methods

    End Class

    Public Class Delete
#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Methods"

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
                logger.ErrorException(New StackFrame().GetMethod().Name,ex)
            End Try
        End Sub

        ''' <summary>
        ''' Gather a list of all files to be deleted for display in a confirmation dialog.
        ''' </summary>
        ''' <param name="isCleaner">Is the function being called from the cleaner?</param>
        ''' <param name="mMovie">DBMovie object to get paths from</param>        
        ''' <returns><c>True</c> if files were found that are to be deleted, <c>False</c> if not.</returns>
        ''' <remarks>Not used for cleaner, needs to be modified to reflect.</remarks>
        Public Function GetItemsToDelete(ByVal isCleaner As Boolean, ByVal mMovie As Structures.DBMovie) As List(Of IO.FileSystemInfo)
            Dim dPath As String = String.Empty
            Dim ItemsToDelete As New List(Of FileSystemInfo)
            Dim fScanner As New Scanner

            Try
                Dim MovieFile As New FileInfo(mMovie.Filename)
                Dim MovieDir As DirectoryInfo = MovieFile.Directory
                'TODO: check VIDEO_TS parent
                If FileUtils.Common.isVideoTS(MovieDir.FullName) Then
                    dPath = String.Concat(Path.Combine(MovieDir.Parent.FullName, MovieDir.Parent.Name), ".ext")
                ElseIf FileUtils.Common.isBDRip(MovieDir.FullName) Then
                    dPath = String.Concat(Path.Combine(MovieDir.Parent.Parent.FullName, MovieDir.Parent.Parent.Name), ".ext")
                Else
                    dPath = mMovie.Filename
                End If

                Dim sOrName As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(dPath))
                Dim sPathShort As String = Directory.GetParent(dPath).FullName
                Dim sPathNoExt As String = Common.RemoveExtFromPath(dPath)

                Dim dirInfo As New DirectoryInfo(sPathShort)
                Dim ioFi As New List(Of FileSystemInfo)

                Try
                    ioFi.AddRange(dirInfo.GetFiles())
                Catch
                End Try

                If isCleaner AndAlso Master.eSettings.FileSystemExpertCleaner Then

                    For Each sFile As FileInfo In ioFi
                        If Not Master.eSettings.FileSystemCleanerWhitelistExts.Contains(sFile.Extension.ToLower) AndAlso ((Master.eSettings.FileSystemCleanerWhitelist AndAlso Not Master.eSettings.FileSystemValidExts.Contains(sFile.Extension.ToLower)) OrElse Not Master.eSettings.FileSystemCleanerWhitelist) Then
                            sFile.Delete()
                        End If
                    Next

                Else

                    If Not isCleaner Then
                        Dim fPath As String = mMovie.FanartPath
                        Dim tPath As String = String.Empty
                        If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                            If Common.isVideoTS(fPath) Then
                                If Path.GetFileName(fPath).ToLower = "fanart.jpg" Then
                                    tPath = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).Name, "-fanart.jpg"))
                                Else
                                    tPath = Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(fPath))
                                End If
                            ElseIf Common.isBDRip(fPath) Then 'TODO: this looks wrong: MovieBackdropsPath ???
                                If Path.GetFileName(fPath).ToLower = "fanart.jpg" Then
                                    tPath = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName).Name, "-fanart.jpg"))
                                Else
                                    tPath = Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(fPath))
                                End If
                            Else
                                If Path.GetFileName(fPath).ToLower = "fanart.jpg" Then
                                    tPath = Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.GetFileNameWithoutExtension(mMovie.Filename), "-fanart.jpg"))
                                Else
                                    tPath = Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(fPath))
                                End If
                            End If
                        End If
                        If Not String.IsNullOrEmpty(tPath) Then
                            If IO.File.Exists(tPath) Then
                                ItemsToDelete.Add(New IO.FileInfo(tPath))
                            End If
                        End If
                    End If

                    If Not isCleaner AndAlso mMovie.isSingle AndAlso Not Master.SourcesList.Contains(MovieDir.Parent.ToString) Then
                        If Common.isVideoTS(MovieDir.FullName) Then
                            ItemsToDelete.Add(MovieDir.Parent)
                        ElseIf Common.isBDRip(MovieDir.FullName) Then
                            ItemsToDelete.Add(MovieDir.Parent.Parent)
                        Else
                            'check if there are other folders with movies in them
                            If Not fScanner.SubDirsHaveMovies(MovieDir) Then
                                'no movies in sub dirs... delete the whole thing
                                ItemsToDelete.Add(MovieDir)
                            Else
                                'just delete the movie file itself
                                ItemsToDelete.Add(New IO.FileInfo(mMovie.Filename))
                            End If
                        End If
                    Else
                        For Each lFI As FileInfo In ioFi
                            If isCleaner Then
                                If (Master.eSettings.CleanFolderJPG AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "folder.jpg")) _
                                    OrElse (Master.eSettings.CleanFanartJPG AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "fanart.jpg")) _
                                    OrElse (Master.eSettings.CleanMovieTBN AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "movie.tbn")) _
                                    OrElse (Master.eSettings.CleanMovieNFO AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "movie.nfo")) _
                                    OrElse (Master.eSettings.CleanPosterTBN AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "poster.tbn")) _
                                    OrElse (Master.eSettings.CleanPosterJPG AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "poster.jpg")) _
                                    OrElse (Master.eSettings.CleanMovieJPG AndAlso lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "movie.jpg")) Then
                                    File.Delete(lFI.FullName)
                                    Continue For
                                End If
                            End If

                            If (Master.eSettings.CleanMovieTBNB AndAlso isCleaner) OrElse (Not isCleaner) Then
                                If lFI.FullName.ToLower = String.Concat(sPathNoExt.ToLower, ".tbn") _
                                OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "video_ts.tbn") _
                                OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "index.tbn") _
                                OrElse lFI.FullName.ToLower = String.Concat(Path.Combine(sPathShort.ToLower, sOrName.ToLower), ".tbn") Then
                                    If isCleaner Then
                                        File.Delete(lFI.FullName)
                                    Else
                                        ItemsToDelete.Add(lFI)
                                    End If
                                    Continue For
                                End If
                            End If

                            If (Master.eSettings.CleanMovieFanartJPG AndAlso isCleaner) OrElse (Not isCleaner) Then
                                If lFI.FullName.ToLower = String.Concat(sPathNoExt.ToLower, "-fanart.jpg") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "video_ts-fanart.jpg") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "index-fanart.jpg") _
                                    OrElse lFI.FullName.ToLower = String.Concat(Path.Combine(sPathShort.ToLower, sOrName.ToLower), "-fanart.jpg") Then
                                    If isCleaner Then
                                        File.Delete(lFI.FullName)
                                    Else
                                        ItemsToDelete.Add(lFI)
                                    End If
                                    Continue For
                                End If
                            End If

                            If (Master.eSettings.CleanMovieNFOB AndAlso isCleaner) OrElse (Not isCleaner) Then
                                If lFI.FullName.ToLower = String.Concat(sPathNoExt.ToLower, ".nfo") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "video_ts.nfo") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "index.nfo") _
                                    OrElse lFI.FullName.ToLower = String.Concat(Path.Combine(sPathShort.ToLower, sOrName.ToLower), ".nfo") Then
                                    If isCleaner Then
                                        File.Delete(lFI.FullName)
                                    Else
                                        ItemsToDelete.Add(lFI)
                                    End If
                                    Continue For
                                End If
                            End If

                            If (Master.eSettings.CleanDotFanartJPG AndAlso isCleaner) OrElse (Not isCleaner) Then
                                If lFI.FullName.ToLower = String.Concat(sPathNoExt.ToLower, ".fanart.jpg") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "video_ts.fanart.jpg") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "index.fanart.jpg") _
                                    OrElse lFI.FullName.ToLower = String.Concat(Path.Combine(sPathShort.ToLower, sOrName.ToLower), ".fanart.jpg") Then
                                    If isCleaner Then
                                        File.Delete(lFI.FullName)
                                    Else
                                        ItemsToDelete.Add(lFI)
                                    End If
                                    Continue For
                                End If
                            End If

                            If (Master.eSettings.CleanMovieNameJPG AndAlso isCleaner) OrElse (Not isCleaner) Then
                                If lFI.FullName.ToLower = String.Concat(sPathNoExt.ToLower, ".jpg") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "video_ts.jpg") _
                                    OrElse lFI.FullName.ToLower = Path.Combine(sPathShort.ToLower, "index.jpg") _
                                    OrElse lFI.FullName.ToLower = String.Concat(Path.Combine(sPathShort.ToLower, sOrName.ToLower), ".jpg") Then
                                    If isCleaner Then
                                        File.Delete(lFI.FullName)
                                    Else
                                        ItemsToDelete.Add(lFI)
                                    End If
                                    Continue For
                                End If
                            End If
                        Next

                        If Not isCleaner Then

                            ioFi.Clear()
                            Try
                                If mMovie.isSingle Then ioFi.AddRange(dirInfo.GetFiles(String.Concat(sOrName, "*.*")))
                            Catch
                            End Try

                            Try
                                ioFi.AddRange(dirInfo.GetFiles(String.Concat(Path.GetFileNameWithoutExtension(mMovie.Filename), ".*")))
                            Catch
                            End Try

                            ItemsToDelete.AddRange(ioFi)

                        End If

                        If Master.eSettings.CleanExtrathumbs Then
                            If Directory.Exists(Path.Combine(sPathShort, "extrathumbs")) Then
                                If isCleaner Then
                                    DeleteDirectory(Path.Combine(sPathShort, "extrathumbs"))
                                Else
                                    Dim dir As New DirectoryInfo(Path.Combine(sPathShort, "extrathumbs"))
                                    If dir.Exists Then
                                        ItemsToDelete.Add(dir)
                                    End If
                                End If
                            End If
                        End If

                    End If
                End If

                ioFi = Nothing
                dirInfo = Nothing
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name,ex)
            End Try
            Return ItemsToDelete
        End Function

#End Region 'Methods

    End Class

    Public Class DragAndDrop
#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Methods"

        Public Shared Function CheckDroppedImage(ByVal e As DragEventArgs) As Boolean
            Dim strFile() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If Not IsNothing(strFile) Then
                Dim fi As New System.IO.FileInfo(strFile(0))
                If fi.Extension = ".gif" Or fi.Extension = ".bmp" Or fi.Extension = ".jpg" Or fi.Extension = ".jpeg" Or fi.Extension = ".png" Then
                    Return True
                End If
            End If

            Return False
        End Function

        Public Shared Function GetDoppedImage(ByVal e As DragEventArgs) As Images
            Dim tImage As New Images
            If e.Data.GetDataPresent("HTML FORMAT") Then
                Dim clipboardHtml As String = CStr(e.Data.GetData("HTML Format"))
                Dim htmlFragment As String = getHtmlFragment(clipboardHtml)
                Dim imageSrc As String = parseImageSrc(htmlFragment)
                Dim baseURL As String = parseBaseURL(clipboardHtml)

                If (imageSrc.ToLower().IndexOf("http://") = 0) Or (imageSrc.ToLower().IndexOf("https://") = 0) Then
                    tImage.FromWeb(imageSrc)
                    If Not IsNothing(tImage.Image) Then
                        Return tImage
                    End If
                Else
                    tImage.FromWeb(baseURL + imageSrc.Substring(1))
                    If Not IsNothing(tImage.Image) Then
                        Return tImage
                    End If
                End If
            ElseIf e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                Dim localImage() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
                tImage.FromFile(localImage(0).ToString)
                If Not IsNothing(tImage.Image) Then
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
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Methods"
        ''' <summary>
        ''' Creates a list of filenames to save or read movie content
        ''' </summary>
        ''' <param name="mPath"><c>String</c> movie filename with full path</param>
        ''' <param name="isSingle"><c>Boolean</c> movie is in a seaparate folder</param>
        ''' <param name="mType"></param>
        ''' <returns><c>List(Of String)</c> all filenames with full path</returns>
        ''' <remarks></remarks>
        Public Shared Function Movie(ByVal mPath As String, ByVal isSingle As Boolean, ByVal mType As Enums.MovieModType) As List(Of String)
            Dim FilenameList As New List(Of String)

            Dim fPath As String = mPath
            Dim fileName As String = Path.GetFileNameWithoutExtension(fPath)
            Dim fileNameStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(fPath))
            Dim filePath As String = Path.Combine(Directory.GetParent(fPath).FullName, fileName)
            Dim filePathStack As String = Path.Combine(Directory.GetParent(fPath).FullName, fileNameStack)
            Dim fileParPath As String = Directory.GetParent(filePath).FullName
            Dim basePath As String = String.Empty

            Dim isVideoTS As Boolean = FileUtils.Common.isVideoTS(fPath)
            Dim isBDRip As Boolean = FileUtils.Common.isBDRip(fPath)
            Dim isVideoTSFile As Boolean = fileName.ToLower = "video_ts"

            If isVideoTS Then basePath = Directory.GetParent(Directory.GetParent(fPath).FullName).FullName
            If isBDRip Then basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName).FullName

            Select Case mType
                Case Enums.MovieModType.NFO
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseBoxee AndAlso .MovieNFOBoxee Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseEden AndAlso .MovieNFOEden Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(basePath, "movie.nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseNMJ AndAlso .MovieNFONMJ Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".nfo"))
                            If .MovieUseYAMJ AndAlso .MovieNFOYAMJ Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertVTS) Then
                                For Each a In .MovieNFOExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseEden AndAlso .MovieNFOEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index.nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(basePath, "movie.nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index.nfo"))
                            If .MovieUseNMJ AndAlso .MovieNFONMJ Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".nfo"))
                            If .MovieUseYAMJ AndAlso .MovieNFOYAMJ Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertBDMV) Then
                                For Each a In .MovieNFOExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseBoxee AndAlso .MovieNFOBoxee AndAlso isVideoTSFile Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseBoxee AndAlso .MovieNFOBoxee AndAlso Not isVideoTSFile Then FilenameList.Add(Path.Combine(fileParPath, "movie.nfo"))
                            If .MovieUseEden AndAlso .MovieNFOEden Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso isVideoTSFile Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo AndAlso Not isVideoTSFile Then FilenameList.Add(String.Concat(filePathStack, ".nfo"))
                            If .MovieUseNMJ AndAlso .MovieNFONMJ Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseYAMJ AndAlso .MovieNFOYAMJ Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                If Not String.IsNullOrEmpty(.MovieNFOExpertVTS) Then
                                    For Each a In .MovieNFOExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieNFOExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieNFOExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If .MovieUseEden AndAlso .MovieNFOEden Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseFrodo AndAlso .MovieNFOFrodo Then FilenameList.Add(String.Concat(filePathStack, ".nfo"))
                            If .MovieUseNMJ AndAlso .MovieNFONMJ Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseYAMJ AndAlso .MovieNFOYAMJ Then FilenameList.Add(String.Concat(filePath, ".nfo"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieNFOExpertMulti) Then
                                For Each a In .MovieNFOExpertMulti.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
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

                Case Enums.MovieModType.Poster
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseBoxee AndAlso .MoviePosterBoxee Then FilenameList.Add(Path.Combine(basePath, "folder.jpg"))
                            If .MovieUseEden AndAlso .MoviePosterEden Then FilenameList.Add(String.Concat(filePath, ".tbn"))
                            If .MovieUseFrodo AndAlso .MoviePosterFrodo Then FilenameList.Add(Path.Combine(basePath, "poster.jpg"))
                            If .MovieUseNMJ AndAlso .MoviePosterNMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".jpg"))
                            If .MovieUseYAMJ AndAlso .MoviePosterYAMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertVTS) Then
                                For Each a In .MoviePosterExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseEden AndAlso .MoviePosterEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index.tbn"))
                            If .MovieUseFrodo AndAlso .MoviePosterFrodo Then FilenameList.Add(Path.Combine(basePath, "poster.jpg"))
                            If .MovieUseNMJ AndAlso .MoviePosterNMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".jpg"))
                            If .MovieUseYAMJ AndAlso .MoviePosterYAMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertBDMV) Then
                                For Each a In .MoviePosterExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseBoxee AndAlso .MoviePosterBoxee Then FilenameList.Add(Path.Combine(fileParPath, "folder.jpg"))
                            If .MovieUseEden AndAlso .MoviePosterEden Then FilenameList.Add(String.Concat(filePath, ".tbn"))
                            If .MovieUseFrodo AndAlso isVideoTSFile AndAlso .MoviePosterFrodo Then FilenameList.Add(Path.Combine(fileParPath, "poster.jpg"))
                            If .MovieUseFrodo AndAlso Not isVideoTSFile AndAlso .MoviePosterFrodo Then FilenameList.Add(String.Concat(filePathStack, "-poster.jpg"))
                            If .MovieUseNMJ AndAlso .MoviePosterNMJ Then FilenameList.Add(String.Concat(filePathStack, ".jpg"))
                            If .MovieUseYAMJ AndAlso .MoviePosterYAMJ Then FilenameList.Add(String.Concat(filePath, ".jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MoviePosterExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MoviePosterExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MoviePosterExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If .MovieUseBoxee AndAlso .MoviePosterBoxee Then FilenameList.Add(Path.Combine(filePath, ".tbn"))
                            If .MovieUseEden AndAlso .MovieFanartEden Then FilenameList.Add(String.Concat(filePath, ".tbn"))
                            If .MovieUseFrodo AndAlso .MovieFanartFrodo Then FilenameList.Add(String.Concat(filePathStack, "-poster.jpg"))
                            If .MovieUseNMJ AndAlso .MovieFanartNMJ Then FilenameList.Add(String.Concat(filePathStack, ".jpg"))
                            If .MovieUseYAMJ AndAlso .MovieFanartYAMJ Then FilenameList.Add(String.Concat(String.Concat(filePath, ".jpg")))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MoviePosterExpertMulti) Then
                                For Each a In .MoviePosterExpertMulti.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
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

                Case Enums.MovieModType.Fanart
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseBoxee AndAlso .MovieFanartBoxee Then FilenameList.Add(Path.Combine(basePath, "fanart.jpg"))
                            If .MovieUseEden AndAlso .MovieFanartEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg"))
                            If .MovieUseFrodo AndAlso .MovieFanartFrodo Then FilenameList.Add(Path.Combine(basePath, "fanart.jpg"))
                            If .MovieUseNMJ AndAlso .MovieFanartNMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".fanart.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieFanartYAMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertVTS) Then
                                For Each a In .MovieFanartExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseEden AndAlso .MovieFanartEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "index-fanart.jpg"))
                            If .MovieUseFrodo AndAlso .MovieFanartFrodo Then FilenameList.Add(Path.Combine(basePath, "fanart.jpg"))
                            If .MovieUseNMJ AndAlso .MovieFanartNMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".fanart.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieFanartYAMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertBDMV) Then
                                For Each a In .MovieFanartExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseBoxee AndAlso .MovieFanartBoxee Then FilenameList.Add(Path.Combine(fileParPath, "fanart.jpg"))
                            If .MovieUseEden AndAlso .MovieFanartEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg"))
                            If .MovieUseFrodo AndAlso isVideoTSFile AndAlso .MovieFanartFrodo Then FilenameList.Add(Path.Combine(fileParPath, "fanart.jpg"))
                            If .MovieUseFrodo AndAlso Not isVideoTSFile AndAlso .MovieFanartFrodo Then FilenameList.Add(String.Concat(filePathStack, "-fanart.jpg"))
                            If .MovieUseNMJ AndAlso .MovieFanartNMJ Then FilenameList.Add(String.Concat(filePathStack, ".fanart.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieFanartYAMJ Then FilenameList.Add(String.Concat(filePath, ".fanart.jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieFanartExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieFanartExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieFanartExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If .MovieUseEden AndAlso .MovieFanartEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg"))
                            If .MovieUseFrodo AndAlso .MovieFanartFrodo Then FilenameList.Add(String.Concat(filePathStack, "-fanart.jpg"))
                            If .MovieUseNMJ AndAlso .MovieFanartNMJ Then FilenameList.Add(String.Concat(filePathStack, ".fanart.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieFanartYAMJ Then FilenameList.Add(String.Concat(filePath, ".fanart.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieFanartExpertMulti) Then
                                For Each a In .MovieFanartExpertMulti.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
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

                Case Enums.MovieModType.Theme
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieXBMCThemeEnable Then
                                If .MovieXBMCThemeMovie Then FilenameList.Add(Path.Combine(basePath, "theme"))
                                If .MovieXBMCThemeCustom AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeCustomPath) Then FilenameList.Add(Path.Combine(.MovieXBMCThemeCustomPath, "theme"))
                                If .MovieXBMCThemeSub AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeSubDir) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, .MovieXBMCThemeSubDir, Path.DirectorySeparatorChar, "theme"))
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieXBMCThemeEnable Then
                                If .MovieXBMCThemeMovie Then FilenameList.Add(Path.Combine(basePath, "theme"))
                                If .MovieXBMCThemeCustom AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeCustomPath) Then FilenameList.Add(Path.Combine(.MovieXBMCThemeCustomPath, "theme"))
                                If .MovieXBMCThemeSub AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeSubDir) Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, .MovieXBMCThemeSubDir, Path.DirectorySeparatorChar, "theme"))
                            End If
                        Else
                            If .MovieUseFrodo AndAlso .MovieXBMCThemeEnable Then
                                If .MovieXBMCThemeMovie Then FilenameList.Add(Path.Combine(fileParPath, "theme"))
                                If .MovieXBMCThemeCustom AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeCustomPath) Then FilenameList.Add(Path.Combine(.MovieXBMCThemeCustomPath, "theme"))
                                If .MovieXBMCThemeSub AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeSubDir) Then FilenameList.Add(String.Concat(fileParPath, Path.DirectorySeparatorChar, .MovieXBMCThemeSubDir, Path.DirectorySeparatorChar, "theme"))
                            End If
                        End If
                    End With

                Case Enums.MovieModType.Trailer
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseEden AndAlso .MovieTrailerEden Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If .MovieUseFrodo AndAlso .MovieTrailerFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If .MovieUseNMJ AndAlso .MovieTrailerNMJ Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".[trailer]"))
                            If .MovieUseYAMJ AndAlso .MovieTrailerYAMJ Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(fileParPath).Name, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertVTS) Then
                                For Each a In .MovieTrailerExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseEden AndAlso .MovieTrailerEden Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, "index-trailer"))
                            If .MovieUseFrodo AndAlso .MovieTrailerFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(String.Concat(Directory.GetParent(fileParPath).FullName, Path.DirectorySeparatorChar, "index-trailer"))
                            If .MovieUseNMJ AndAlso .MovieTrailerNMJ Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".[trailer]"))
                            If .MovieUseYAMJ AndAlso .MovieTrailerYAMJ Then FilenameList.Add(String.Concat(basePath, Path.DirectorySeparatorChar, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertBDMV) Then
                                For Each a In .MovieTrailerExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseEden AndAlso .MovieTrailerEden Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If .MovieUseFrodo AndAlso .MovieTrailerFrodo Then FilenameList.Add(String.Concat(filePathStack, "-trailer"))
                            If .MovieUseNMJ AndAlso .MovieTrailerNMJ Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If .MovieUseYAMJ AndAlso .MovieTrailerYAMJ Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieTrailerExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieTrailerExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieTrailerExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If .MovieUseEden AndAlso .MovieTrailerEden Then FilenameList.Add(String.Concat(filePath, "-trailer"))
                            If .MovieUseFrodo AndAlso .MovieTrailerFrodo Then FilenameList.Add(String.Concat(filePathStack, "-trailer"))
                            If .MovieUseNMJ AndAlso .MovieTrailerNMJ Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If .MovieUseYAMJ AndAlso .MovieTrailerYAMJ Then FilenameList.Add(String.Concat(filePath, ".[trailer]"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieTrailerExpertMulti) Then
                                For Each a In .MovieTrailerExpertMulti.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
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

                Case Enums.MovieModType.Banner
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieBannerFrodo Then FilenameList.Add(Path.Combine(basePath, "banner.jpg"))
                            'If .MovieUseEden AndAlso .MovieBannerEden Then FilenameList.Add(String.Concat(filePath, "banner.jpg")) 'TODO: Check
                            If .MovieUseNMJ AndAlso .MovieBannerNMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".banner.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieBannerYAMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertVTS) Then
                                For Each a In .MovieBannerExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieBannerFrodo Then FilenameList.Add(Path.Combine(basePath, "banner.jpg"))
                            'If .MovieUseEden AndAlso .MovieBannerEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "banner.jpg")) 'TODO: Check
                            If .MovieUseNMJ AndAlso .MovieBannerNMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".banner.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieBannerYAMJ Then FilenameList.Add(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertBDMV) Then
                                For Each a In .MovieBannerExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso isVideoTSFile AndAlso .MovieBannerFrodo Then FilenameList.Add(Path.Combine(fileParPath, "banner.jpg"))
                            If .MovieUseFrodo AndAlso Not isVideoTSFile AndAlso .MovieBannerFrodo Then FilenameList.Add(String.Concat(filePathStack, "-banner.jpg"))
                            'If .MovieUseEden AndAlso .MovieBannerEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg")) 'TODO: Check
                            If .MovieUseNMJ AndAlso .MovieBannerNMJ Then FilenameList.Add(String.Concat(filePathStack, ".banner.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieBannerYAMJ Then FilenameList.Add(String.Concat(filePath, ".banner.jpg"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieBannerExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieBannerExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieBannerExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If .MovieUseFrodo AndAlso .MovieBannerFrodo Then FilenameList.Add(String.Concat(filePathStack, "-banner.jpg"))
                            'If .MovieUseEden AndAlso .MovieFanartEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg")) 'TODO: Check
                            If .MovieUseNMJ AndAlso .MovieBannerNMJ Then FilenameList.Add(String.Concat(filePathStack, ".banner.jpg"))
                            If .MovieUseYAMJ AndAlso .MovieBannerYAMJ Then FilenameList.Add(String.Concat(filePath, ".banner.jpg"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieBannerExpertMulti) Then
                                For Each a In .MovieBannerExpertMulti.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
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

                Case Enums.MovieModType.ClearLogo
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieClearLogoFrodo Then FilenameList.Add(Path.Combine(basePath, "logo.png"))
                            If .MovieUseFrodo AndAlso .MovieClearLogoEden Then FilenameList.Add(Path.Combine(basePath, "logo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertVTS) Then
                                For Each a In .MovieClearLogoExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieClearLogoFrodo Then FilenameList.Add(Path.Combine(basePath, "logo.png"))
                            If .MovieUseFrodo AndAlso .MovieClearLogoEden Then FilenameList.Add(Path.Combine(basePath, "logo.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertBDMV) Then
                                For Each a In .MovieClearLogoExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso .MovieClearLogoFrodo Then FilenameList.Add(Path.Combine(fileParPath, "logo.png"))
                            If .MovieUseFrodo AndAlso .MovieClearLogoEden Then FilenameList.Add(Path.Combine(fileParPath, "logo.png"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieClearLogoExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearLogoExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieClearLogoExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieClearLogoExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                        End If
                    End With

                Case Enums.MovieModType.ClearArt
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieClearArtFrodo Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If .MovieUseFrodo AndAlso .MovieClearArtEden Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertVTS) Then
                                For Each a In .MovieClearArtExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieClearArtFrodo Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If .MovieUseFrodo AndAlso .MovieClearArtEden Then FilenameList.Add(Path.Combine(basePath, "clearart.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertBDMV) Then
                                For Each a In .MovieClearArtExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso .MovieClearArtFrodo Then FilenameList.Add(Path.Combine(fileParPath, "clearart.png"))
                            If .MovieUseFrodo AndAlso .MovieClearArtEden Then FilenameList.Add(Path.Combine(fileParPath, "clearart.png"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieClearArtExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieClearArtExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieClearArtExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieClearArtExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                        End If
                    End With

                Case Enums.MovieModType.DiscArt
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieDiscArtFrodo Then FilenameList.Add(Path.Combine(basePath, "disc.png"))
                            If .MovieUseFrodo AndAlso .MovieDiscArtEden Then FilenameList.Add(Path.Combine(basePath, "disc.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertVTS) Then
                                For Each a In .MovieDiscArtExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieDiscArtFrodo Then FilenameList.Add(Path.Combine(basePath, "disc.png"))
                            If .MovieUseFrodo AndAlso .MovieDiscArtEden Then FilenameList.Add(Path.Combine(basePath, "disc.png"))
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertBDMV) Then
                                For Each a In .MovieDiscArtExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso .MovieDiscArtFrodo Then FilenameList.Add(Path.Combine(fileParPath, "disc.png"))
                            If .MovieUseFrodo AndAlso .MovieDiscArtEden Then FilenameList.Add(Path.Combine(fileParPath, "disc.png"))
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieDiscArtExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieDiscArtExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieDiscArtExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieDiscArtExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                        End If
                    End With

                Case Enums.MovieModType.ActorThumbs
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieActorThumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(String.Concat(fileParPath, "/.actors", "/<placeholder>.jpg"))
                            If .MovieUseEden AndAlso .MovieActorThumbsEden Then FilenameList.Add(String.Concat(fileParPath, "/.actors", "/<placeholder>.tbn"))
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieActorThumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "/.actors", "/<placeholder>.jpg"))
                            If .MovieUseEden AndAlso .MovieActorThumbsEden Then FilenameList.Add(String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "/.actors", "/<placeholder>.tbn"))
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso .MovieActorThumbsFrodo Then FilenameList.Add(String.Concat(fileParPath, "/.actors", "/<placeholder>.jpg"))
                            If .MovieUseEden AndAlso .MovieActorThumbsEden Then FilenameList.Add(String.Concat(fileParPath, "/.actors", "/<placeholder>.tbn"))
                        End If
                    End With

                Case Enums.MovieModType.EThumbs
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieExtrathumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrathumbs"))
                            If .MovieUseEden AndAlso .MovieExtrathumbsEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrathumbs"))
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieExtrathumbsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrathumbs"))
                            If .MovieUseEden AndAlso .MovieExtrathumbsEden Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrathumbs"))
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso .MovieExtrathumbsFrodo Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrathumbs"))
                            If .MovieUseEden AndAlso .MovieExtrathumbsEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrathumbs"))
                        End If
                    End With

                Case Enums.MovieModType.EFanarts
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieExtrafanartsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrafanart"))
                            If .MovieUseEden AndAlso .MovieExtrafanartsEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrafanart"))
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieExtrafanartsFrodo AndAlso Not .MovieXBMCProtectVTSBDMV Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrafanart"))
                            If .MovieUseEden AndAlso .MovieExtrafanartsEden Then FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName, "extrafanart"))
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso .MovieExtrafanartsFrodo Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrafanart"))
                            If .MovieUseEden AndAlso .MovieExtrafanartsEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fPath).FullName, "extrafanart"))
                        End If
                    End With

                Case Enums.MovieModType.Landscape
                    With Master.eSettings
                        If isVideoTS Then
                            If .MovieUseFrodo AndAlso .MovieLandscapeFrodo Then FilenameList.Add(Path.Combine(basePath, "landscape.jpg"))
                            'If .MovieUseEden AndAlso .MovieLandscapeEden Then FilenameList.Add(String.Concat(filePath, "banner.jpg")) 'TODO: Check
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertVTS) Then
                                For Each a In .MovieLandscapeExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertVTS Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isBDRip Then
                            If .MovieUseFrodo AndAlso .MovieLandscapeFrodo Then FilenameList.Add(Path.Combine(basePath, "landscape.jpg"))
                            'If .MovieUseEden AndAlso .MovieLandscapeEden Then FilenameList.Add(Path.Combine(Directory.GetParent(fileParPath).FullName, "banner.jpg")) 'TODO: Check
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertBDMV) Then
                                For Each a In .MovieLandscapeExpertBDMV.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    If .MovieUseBaseDirectoryExpertBDMV Then
                                        FilenameList.Add(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, a.Replace("<filename>", fileName)))
                                    Else
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    End If
                                Next
                            End If
                        ElseIf isSingle Then
                            If .MovieUseFrodo AndAlso isVideoTSFile AndAlso .MovieLandscapeFrodo Then FilenameList.Add(Path.Combine(fileParPath, "landscape.jpg"))
                            If .MovieUseFrodo AndAlso Not isVideoTSFile AndAlso .MovieLandscapeFrodo Then FilenameList.Add(Path.Combine(fileParPath, "landscape.jpg"))
                            'If .MovieUseEden AndAlso .MovieLandscapeEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg")) 'TODO: Check
                            If .MovieUseExpert AndAlso isVideoTSFile AndAlso .MovieRecognizeVTSExpertVTS Then
                                For Each a In .MovieLandscapeExpertVTS.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                    FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                Next
                            ElseIf .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertSingle) Then
                                If .MovieStackExpertSingle Then
                                    For Each a In .MovieLandscapeExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileNameStack)))

                                        If .MovieUnstackExpertSingle Then
                                            FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                        End If
                                    Next
                                Else
                                    For Each a In .MovieLandscapeExpertSingle.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                                        FilenameList.Add(Path.Combine(fileParPath, a.Replace("<filename>", fileName)))
                                    Next
                                End If
                            End If
                        Else
                            If .MovieUseFrodo AndAlso .MovieLandscapeFrodo Then FilenameList.Add(String.Concat(filePathStack, "landscape.jpg"))
                            'If .MovieUseEden AndAlso .MovieLandscapeEden Then FilenameList.Add(String.Concat(filePath, "-fanart.jpg")) 'TODO: Check
                            If .MovieUseExpert AndAlso Not String.IsNullOrEmpty(.MovieLandscapeExpertMulti) Then
                                For Each a In .MovieLandscapeExpertMulti.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
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

                Case Enums.MovieModType.WatchedFile
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
        ''' Creates a list of filenames to save or read movie content
        ''' </summary>
        ''' <param name="mType"></param>
        ''' <returns><c>List(Of String)</c> all filenames with full path</returns>
        ''' <remarks></remarks>
        Public Shared Function MovieSet(ByVal SetName As String, ByVal mType As Enums.MovieModType) As List(Of String)
            Dim FilenameList As New List(Of String)

            Dim fSetName As String = SetName
            Dim fPath As String = Master.eSettings.MovieMoviesetsPath

            Select Case mType
                Case Enums.MovieModType.NFO
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, ".nfo")))
                    End With

                Case Enums.MovieModType.Poster
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-poster.jpg")))
                    End With

                Case Enums.MovieModType.Fanart
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-fanart.jpg")))
                    End With

                Case Enums.MovieModType.Banner
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-banner.jpg")))
                    End With

                Case Enums.MovieModType.ClearLogo
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-logo.png")))
                    End With

                Case Enums.MovieModType.ClearArt
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-clearart.png")))
                    End With

                Case Enums.MovieModType.DiscArt
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-discart.png")))
                    End With

                Case Enums.MovieModType.Landscape
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fPath, String.Concat(fSetName, "-landscape.jpg")))
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Public Shared Function TVEpisode(ByVal EpisodePath As String, ByVal mType As Enums.TVModType, Optional ByVal mSeason As Integer = -1) As List(Of String)
            Dim FilenameList As New List(Of String)

            Dim fEpisodePath As String = FileUtils.Common.RemoveExtFromPath(EpisodePath)
            Dim sSeason As String = mSeason.ToString.PadLeft(2, Convert.ToChar("0"))

            Select Case mType
                Case Enums.TVModType.EpisodeFanart
                    With Master.eSettings
                    End With

                Case Enums.TVModType.EpisodePoster
                    With Master.eSettings
                        If .TVUseBoxee AndAlso .TVEpisodePosterBoxee Then FilenameList.Add(String.Concat(fEpisodePath, ".tbn"))
                        If .TVUseFrodo AndAlso .TVEpisodePosterFrodo Then FilenameList.Add(String.Concat(fEpisodePath, "-thumb.jpg"))
                        If .TVUseYAMJ AndAlso .TVEpisodePosterYAMJ Then FilenameList.Add(String.Concat(fEpisodePath, ".videoimage.jpg"))
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Public Shared Function TVSeason(ByVal ShowPath As String, ByVal SeasonPath As String, ByVal mSeason As Integer, ByVal FirstEpisode As String, ByVal mType As Enums.TVModType) As List(Of String)
            Dim FilenameList As New List(Of String)
            Dim bInside As Boolean = False

            Dim fEpsiodePath As String = Path.Combine(Directory.GetParent(FirstEpisode).FullName, Path.GetFileNameWithoutExtension(FirstEpisode))
            Dim fSeasonPath As String = SeasonPath
            Dim fShowPath As String = ShowPath
            Dim sSeason As String = mSeason.ToString.PadLeft(2, Convert.ToChar("0"))
            Dim fSeasonFolder As String = Path.GetFileName(fSeasonPath)

            'checks if there are separate season folders
            If Not String.IsNullOrEmpty(fSeasonPath) AndAlso Not fSeasonPath = fShowPath Then
                bInside = True
            End If

            Select Case mType
                Case Enums.TVModType.SeasonBanner
                    With Master.eSettings
                        If mSeason = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-banner.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".banner.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso Not bInside Then
                                FilenameList.Add(String.Concat(fEpsiodePath, ".banner.jpg"))
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonBannerFrodo Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-banner.jpg", sSeason)))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".banner.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonBannerYAMJ AndAlso Not bInside Then
                                FilenameList.Add(String.Concat(fEpsiodePath, ".banner.jpg"))
                            End If
                        End If
                    End With

                Case Enums.TVModType.SeasonFanart
                    With Master.eSettings
                        If mSeason = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-fanart.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".fanart.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso Not bInside Then
                                FilenameList.Add(String.Concat(fEpsiodePath, ".fanart.jpg"))
                            End If
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-fanart.jpg", sSeason)))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".fanart.jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonFanartYAMJ AndAlso Not bInside Then
                                FilenameList.Add(String.Concat(fEpsiodePath, ".fanart.jpg"))
                            End If
                        End If
                    End With

                Case Enums.TVModType.SeasonLandscape
                    With Master.eSettings
                        If mSeason = 0 Then 'season specials
                            If .TVUseFrodo AndAlso .TVSeasonLandscapeXBMC Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-landscape.jpg"))
                        Else
                            If .TVUseFrodo AndAlso .TVSeasonLandscapeXBMC Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-landscape.jpg", sSeason)))
                        End If
                    End With

                Case Enums.TVModType.SeasonPoster
                    With Master.eSettings
                        If mSeason = 0 Then 'season specials
                            If .TVUseBoxee AndAlso .TVSeasonPosterBoxee AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, "poster.jpg"))
                            If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-specials-poster.jpg"))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso Not bInside Then
                                FilenameList.Add(String.Concat(fEpsiodePath, ".jpg"))
                            End If
                        Else
                            If .TVUseBoxee AndAlso .TVSeasonPosterBoxee AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, "poster.jpg"))
                            If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, String.Format("season{0}-poster.jpg", sSeason)))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso bInside Then FilenameList.Add(Path.Combine(fSeasonPath, String.Concat(fSeasonFolder, ".jpg")))
                            If .TVUseYAMJ AndAlso .TVSeasonPosterYAMJ AndAlso Not bInside Then
                                FilenameList.Add(String.Concat(fEpsiodePath, ".jpg"))
                            End If
                        End If
                    End With
            End Select

            FilenameList = FilenameList.Distinct().ToList() 'remove double entries
            Return FilenameList
        End Function

        Public Shared Function TVShow(ByVal ShowPath As String, ByVal mType As Enums.TVModType) As List(Of String)
            Dim FilenameList As New List(Of String)

            Dim fShowPath As String = ShowPath
            Dim fShowFolder As String = Path.GetFileName(fShowPath)

            Select Case mType
                Case Enums.TVModType.AllSeasonsBanner
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonBannerFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-all-banner.jpg"))
                    End With

                Case Enums.TVModType.AllSeasonsFanart
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-all-fanart.jpg"))
                    End With

                Case Enums.TVModType.AllSeasonsLandscape
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonLandscapeXBMC Then FilenameList.Add(Path.Combine(fShowPath, "season-all-landscape.jpg"))
                    End With

                Case Enums.TVModType.AllSeasonsPoster
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVSeasonPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, "season-all-poster.jpg"))
                    End With

                Case Enums.TVModType.ShowBanner
                    With Master.eSettings
                        If .TVUseBoxee AndAlso .TVShowBannerBoxee Then FilenameList.Add(Path.Combine(fShowPath, "banner.jpg"))
                        If .TVUseFrodo AndAlso .TVShowBannerFrodo Then FilenameList.Add(Path.Combine(fShowPath, "banner.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowBannerYAMJ Then FilenameList.Add(Path.Combine(fShowPath, String.Concat("Set_", fShowFolder, "_1.banner.jpg")))
                    End With

                Case Enums.TVModType.ShowCharacterArt
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowCharacterArtXBMC Then FilenameList.Add(Path.Combine(fShowPath, "character.png"))
                    End With

                Case Enums.TVModType.ShowClearArt
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowClearArtXBMC Then FilenameList.Add(Path.Combine(fShowPath, "clearart.png"))
                    End With

                Case Enums.TVModType.ShowClearLogo
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowClearLogoXBMC Then FilenameList.Add(Path.Combine(fShowPath, "logo.png"))
                    End With

                Case Enums.TVModType.ShowEFanarts
                    With Master.eSettings
                        If .TVUseEden AndAlso .TVShowExtrafanartsXBMC Then FilenameList.Add(Path.Combine(fShowPath, "extrafanart"))
                        If .TVUseFrodo AndAlso .TVShowExtrafanartsXBMC Then FilenameList.Add(Path.Combine(fShowPath, "extrafanart"))
                    End With

                Case Enums.TVModType.ShowFanart
                    With Master.eSettings
                        If .TVUseBoxee AndAlso .TVShowFanartBoxee Then FilenameList.Add(Path.Combine(fShowPath, "fanart.jpg"))
                        If .TVUseFrodo AndAlso .TVShowFanartFrodo Then FilenameList.Add(Path.Combine(fShowPath, "fanart.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowFanartYAMJ Then FilenameList.Add(Path.Combine(fShowPath, String.Concat("Set_", fShowFolder, "_1.fanart.jpg")))
                    End With

                Case Enums.TVModType.ShowLandscape
                    With Master.eSettings
                        If .TVUseFrodo AndAlso .TVShowLandscapeXBMC Then FilenameList.Add(Path.Combine(ShowPath, "landscape.jpg"))
                    End With

                Case Enums.TVModType.ShowNfo
                    With Master.eSettings
                        FilenameList.Add(Path.Combine(fShowPath, "tvshow.nfo"))
                    End With

                Case Enums.TVModType.ShowPoster
                    With Master.eSettings
                        If .TVUseBoxee AndAlso .TVShowPosterBoxee Then FilenameList.Add(Path.Combine(fShowPath, "folder.jpg"))
                        If .TVUseFrodo AndAlso .TVShowPosterFrodo Then FilenameList.Add(Path.Combine(fShowPath, "poster.jpg"))
                        If .TVUseYAMJ AndAlso .TVShowPosterYAMJ Then FilenameList.Add(Path.Combine(fShowPath, String.Concat("Set_", fShowFolder, "_1.jpg")))
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
        Dim logger As Logger = NLog.LogManager.GetCurrentClassLogger()
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
        ''' <param name="sPath">Path to be sorted</param>
        ''' <remarks>Occasionally a directory will contain multiple media files (and meta-files) and 
        ''' this method will walk through the files in that directory and move each to its own unique subdirectory.
        ''' This will move all files with the same core name, without extension or fanart/trailer endings.</remarks>
        Public Sub SortFiles(ByVal sPath As String)
            'TODO Need to test what happens if sPath points to an existing FILE (and not just a directory)
            Dim tmpAL As New List(Of String)
            Dim tmpPath As String = String.Empty
            Dim tmpName As String = String.Empty
            Dim iCount As Integer = 0
            Try
                If Directory.Exists(sPath) Then
                    'Get information about files in the directory
                    Dim di As New DirectoryInfo(sPath)
                    Dim lFi As New List(Of FileInfo)

                    'Create a List of files in the directory
                    Try
                        lFi.AddRange(di.GetFiles())
                    Catch
                    End Try

                    'For each file in the directory...
                    For Each sFile As FileInfo In lFi
                        RaiseEvent ProgressUpdated((iCount \ lFi.Count), String.Concat(Master.eLang.GetString(219, "Moving "), sFile.Name))
                        tmpName = Path.GetFileNameWithoutExtension(sFile.Name)
                        '...clean fanart and trailer decorations...
                        tmpName = tmpName.Replace(".fanart", String.Empty)
                        tmpName = tmpName.Replace("-fanart", String.Empty)
                        tmpName = tmpName.Replace("-trailer", String.Empty)
                        tmpName = Regex.Replace(tmpName, "\[trailer(\d+)\]", String.Empty)
                        tmpName = StringUtils.CleanStackingMarkers(tmpName)
                        '...determine the best destination path name...
                        tmpPath = Path.Combine(sPath, tmpName)
                        '...create the destination directory if it doesn't already exist
                        If Not Directory.Exists(tmpPath) Then
                            Directory.CreateDirectory(tmpPath)
                        End If
                        '...and move the file into that path
                        File.Move(sFile.FullName, Path.Combine(tmpPath, sFile.Name))
                        iCount += 1
                    Next

                    RaiseEvent ProgressUpdated((iCount \ lFi.Count), Master.eLang.GetString(362, "Done "))
                    lFi = Nothing
                    di = Nothing
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name,ex)
            End Try
        End Sub

#End Region 'Methods

    End Module

End Namespace

