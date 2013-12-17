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

Namespace FileUtils

    Public Class Common

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
                Master.eLog.Error(GetType(Common), ex.Message, ex.StackTrace, "Error")
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
                Master.eLog.Error(GetType(Common), "Source: <" & sPath & ">" & ex.Message, ex.StackTrace, "Error")
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
#End Region	'Methods

    End Class

    Public Class Delete

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
                Master.eLog.Error(GetType(Delete), ex.Message, ex.StackTrace, "Error")
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

				If Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isVideoTS(MovieDir.FullName) Then
					dPath = String.Concat(Path.Combine(MovieDir.Parent.FullName, MovieDir.Parent.Name), ".ext")
				ElseIf Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isBDRip(MovieDir.FullName) Then
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

				If isCleaner AndAlso Master.eSettings.ExpertCleaner Then

					For Each sFile As FileInfo In ioFi
						If Not Master.eSettings.CleanWhitelistExts.Contains(sFile.Extension.ToLower) AndAlso ((Master.eSettings.CleanWhitelistVideo AndAlso Not Master.eSettings.ValidExts.Contains(sFile.Extension.ToLower)) OrElse Not Master.eSettings.CleanWhitelistVideo) Then
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
									tPath = Path.Combine(Master.eSettings.BDPath, String.Concat(Directory.GetParent(Directory.GetParent(fPath).FullName).Name, "-fanart.jpg"))
								Else
									tPath = Path.Combine(Master.eSettings.BDPath, Path.GetFileName(fPath))
								End If
							ElseIf Common.isBDRip(fPath) Then
								If Path.GetFileName(fPath).ToLower = "fanart.jpg" Then
									tPath = Path.Combine(Master.eSettings.BDPath, String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(fPath).FullName).FullName).Name, "-fanart.jpg"))
								Else
									tPath = Path.Combine(Master.eSettings.BDPath, Path.GetFileName(fPath))
								End If
							Else
								If Path.GetFileName(fPath).ToLower = "fanart.jpg" Then
									tPath = Path.Combine(Master.eSettings.BDPath, String.Concat(Path.GetFileNameWithoutExtension(mMovie.Filename), "-fanart.jpg"))
								Else
									tPath = Path.Combine(Master.eSettings.BDPath, Path.GetFileName(fPath))
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

						If Master.eSettings.CleanExtraThumbs Then
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
                Master.eLog.Error(GetType(Delete), ex.Message, ex.StackTrace, "Error")
			End Try
			Return ItemsToDelete
		End Function

#End Region	'Methods

	End Class

    ''' <summary>
    ''' This module is a convenience library for sorting files into respective subdirectories.
    ''' This module does NOT need to be instantiated!
    ''' </summary>
    ''' <remarks></remarks>
    Public Module FileSorter

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
                Master.eLog.Error(GetType(FileSorter), ex.Message, ex.StackTrace, "Error")
            End Try
        End Sub

#End Region 'Methods

    End Module

End Namespace

