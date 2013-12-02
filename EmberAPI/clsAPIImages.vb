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

Imports System.Drawing.Imaging
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

<Serializable()> _
Public Class Images
    Implements IDisposable
    '2013/11/28 Dekker500 - This class needs some serious love. Clear candidate for polymorphism. Images should be root, with posters, fanart, etc as children. None of this if/else stuff to confuse the issue. I will re-visit later

#Region "Fields"

	Private _ms As MemoryStream
    Private Ret As Byte()
    <NonSerialized()> _
    Private sHTTP As HTTP
	Private _image As Image
    Private _isedit As Boolean

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public Property IsEdit() As Boolean
        Get
            Return _isedit
        End Get
        Set(ByVal value As Boolean)
            _isedit = value
        End Set
    End Property

	Public ReadOnly Property [Image]() As Image
		Get
			Return _image
		End Get
		'Set(ByVal value As Image)
		'    _image = value
		'End Set
	End Property

#End Region 'Properties

#Region "Methods"
    ''' <summary>
    ''' Replaces the internally-held image with the supplied image
    ''' </summary>
    ''' <param name="nImage">The new <c>Image</c> to retain</param>
    ''' <remarks>
    ''' 2013/11/25 Dekker500 - Disposed old image before replacing
    ''' </remarks>
    Public Sub UpdateMSfromImg(nImage As Image)

        Try
            Dim ICI As ImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg)

            Dim EncPars As EncoderParameters = New EncoderParameters(2)
            EncPars.Param(0) = New EncoderParameter(Encoder.RenderMethod, EncoderValue.RenderNonProgressive)
            EncPars.Param(1) = New EncoderParameter(Encoder.Quality, 100)

            'Write the supplied image into the MemoryStream
            _ms.Dispose()
            _ms = New MemoryStream()
            nImage.Save(_ms, ICI, EncPars)
            _ms.Flush()

            'Replace the existing image with the new image
            _image.Dispose()
            _image = New Bitmap(_ms)

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    '   ''' <summary>
    '   ''' Check the size of the image and return a generic name for the size
    '   ''' </summary>
    '   ''' <param name="imgImage"></param>
    '   ''' <returns>A <c>Enums.FanartSize</c> representing the image size</returns>
    '   ''' <remarks></remarks>
    'Public Shared Function GetFanartDims(ByVal imgImage As Image) As Enums.FanartSize
    '       'TODO 2013-11-25 Dekker500 - Consider removing this method as it is no longer being used

    '       If imgImage Is Nothing Then Return Enums.FanartSize.Small 'Should really have an "unknown" size available to gracefully handle exceptions

    '	Dim x As Integer = imgImage.Width
    '	Dim y As Integer = imgImage.Height

    '	Try
    '		If (y > 1000 AndAlso x > 750) OrElse (x > 1000 AndAlso y > 750) Then
    '			Return Enums.FanartSize.Lrg
    '		ElseIf (y > 700 AndAlso x > 400) OrElse (x > 700 AndAlso y > 400) Then
    '			Return Enums.FanartSize.Mid
    '		Else
    '			Return Enums.FanartSize.Small
    '		End If
    '	Catch ex As Exception
    '		Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '		Return Enums.FanartSize.Small
    '	End Try
    'End Function

    ' ''' <summary>
    ' ''' Check the size of the image and return a generic name for the size
    ' ''' </summary>
    ' ''' <param name="imgImage"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Shared Function GetPosterDims(ByVal imgImage As Image) As Enums.PosterSize
    '    '       'TODO 2013-11-25 Dekker500 - Consider removing this method as it is no longer being used

    '    Dim x As Integer = imgImage.Width
    '    Dim y As Integer = imgImage.Height

    '    Try
    '        If (x > y) AndAlso (x > (y * 2)) AndAlso (x > 300) Then
    '            'at least twice as wide than tall... consider it wide (also make sure it's big enough)
    '            Return Enums.PosterSize.Wide
    '        ElseIf (y > 1000 AndAlso x > 750) OrElse (x > 1000 AndAlso y > 750) Then
    '            Return Enums.PosterSize.Xlrg
    '        ElseIf (y > 700 AndAlso x > 500) OrElse (x > 700 AndAlso y > 500) Then
    '            Return Enums.PosterSize.Lrg
    '        ElseIf (y > 250 AndAlso x > 150) OrElse (x > 250 AndAlso y > 150) Then
    '            Return Enums.PosterSize.Mid
    '        Else
    '            Return Enums.PosterSize.Small
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '        Return Enums.PosterSize.Small
    '    End Try        
    'End Function

    ''' <summary>
    ''' Reset this instance to a pristine condition
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        'Out with the old...
        If Not IsNothing(_ms) Or Not IsNothing(_image) Then
            Me.Dispose(True)
            Me.disposedValue = False    'Since this is not a real Dispose call...
        End If

        'In with the new...
        _isedit = False
        _image = Nothing
        _ms = New MemoryStream()
        sHTTP = New HTTP()
    End Sub
    ''' <summary>
    ''' Delete the given arbitrary file
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Public Sub Delete(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) Then
            Try
                File.Delete(sPath)
            Catch ex As Exception
                Master.eLog.WriteToErrorLog("Param: <" & sPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete the season posters for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted</param>
    ''' <remarks></remarks>
    Public Sub DeleteAllSeasonPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Delete(Path.Combine(mShow.ShowPath, "season-all.tbn"))
            Delete(Path.Combine(mShow.ShowPath, "season-all.jpg"))
            Delete(Path.Combine(mShow.ShowPath, "season-all-poster.jpg"))
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Path: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the episode fan art for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted. It is parsed from the actual show's path</param>
    ''' <remarks></remarks>
    Public Sub DeleteEpFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.Filename) Then Return

        Dim tPath As String = FileUtils.Common.RemoveExtFromPath(mShow.Filename)
        If String.IsNullOrEmpty(tPath) Then Return

        Try
            Delete(String.Concat(tPath, "-fanart.jpg"))
            Delete(String.Concat(tPath, ".fanart.jpg"))
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Path: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the show's episode posters
    ''' </summary>
    ''' <param name="mShow">Show database record from which the path is extracted. It is parsed from the individual episode path</param>
    ''' <remarks></remarks>
    Public Sub DeleteEpPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.Filename) Then Return

        Dim tPath As String = FileUtils.Common.RemoveExtFromPath(mShow.Filename)
        If String.IsNullOrEmpty(tPath) Then Return

        Try
            Delete(String.Concat(tPath, ".tbn"))
            Delete(String.Concat(tPath, ".jpg"))
            Delete(String.Concat(tPath, "-thumb.jpg"))
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Path: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's fanart
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteFanart(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            Dim tPath As String = Directory.GetParent(mMovie.Filename).FullName
            Dim params As New List(Of Object)(New Object() {mMovie})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieFanartDelete, params, Nothing, False)

            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "fanart.jpg"))
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, ".fanart.jpg"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(tPath).FullName, Directory.GetParent(tPath).Name), "-fanart.jpg"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(tPath).FullName, Directory.GetParent(tPath).Name), ".fanart.jpg"))
            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                Delete(String.Concat(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Path.DirectorySeparatorChar, "fanart.jpg"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Directory.GetParent(Directory.GetParent(tPath).FullName).Name), "-fanart.jpg"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Directory.GetParent(Directory.GetParent(tPath).FullName).Name), ".fanart.jpg"))
            Else
                If mMovie.isSingle Then
                    Delete(Path.Combine(tPath, "fanart.jpg"))
                End If

                If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                    Delete(Path.Combine(tPath, "video_ts-fanart.jpg"))
                    Delete(Path.Combine(tPath, "video_ts.fanart.jpg"))
                ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                    Delete(Path.Combine(tPath, "index-fanart.jpg"))
                    Delete(Path.Combine(tPath, "index.fanart.jpg"))
                Else
                    Dim fPath As String = Path.Combine(tPath, Path.GetFileNameWithoutExtension(mMovie.Filename))
                    Dim fPathStack As String = Path.Combine(tPath, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(mMovie.Filename)))
                    Delete(String.Concat(fPath, "-fanart.jpg"))
                    Delete(String.Concat(fPath, ".fanart.jpg"))
                    Delete(String.Concat(fPathStack, "-fanart.jpg"))
                End If

            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Movie: <" & mMovie.Filename & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's posters
    ''' </summary>
    ''' <param name="mMovie">Structures.DBMovie representing the movie to be worked on</param>
    ''' <remarks></remarks>
    Public Sub DeletePosters(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            Dim tPath As String = Directory.GetParent(mMovie.Filename).FullName
            Dim params As New List(Of Object)(New Object() {mMovie})

            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMoviePosterDelete, params, Nothing, False)

            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "movie.jpg"))
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "movie.tbn"))
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "folder.jpg"))
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "poster.jpg"))
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "poster.tbn"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(tPath).FullName, Directory.GetParent(tPath).Name), ".jpg"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(tPath).FullName, Directory.GetParent(tPath).Name), ".tbn"))
            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                Delete(String.Concat(Directory.GetParent(tPath).FullName, Path.DirectorySeparatorChar, "poster.jpg"))
                Delete(String.Concat(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Path.DirectorySeparatorChar, "movie.jpg"))
                Delete(String.Concat(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Path.DirectorySeparatorChar, "movie.tbn"))
                Delete(String.Concat(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Path.DirectorySeparatorChar, "folder.jpg"))
                Delete(String.Concat(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Path.DirectorySeparatorChar, "poster.jpg"))
                Delete(String.Concat(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Path.DirectorySeparatorChar, "poster.tbn"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Directory.GetParent(Directory.GetParent(tPath).FullName).Name), ".jpg"))
                Delete(String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(tPath).FullName).FullName, Directory.GetParent(Directory.GetParent(tPath).FullName).Name), ".tbn"))
            Else

                If mMovie.isSingle Then
                    Delete(Path.Combine(tPath, "movie.tbn"))
                    Delete(Path.Combine(tPath, "movie.jpg"))
                    Delete(Path.Combine(tPath, "poster.tbn"))
                    Delete(Path.Combine(tPath, "poster.jpg"))
                    Delete(Path.Combine(tPath, "folder.jpg"))
                End If

                If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                    Delete(Path.Combine(tPath, "video_ts.tbn"))
                    Delete(Path.Combine(tPath, "video_ts.jpg"))
                ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                    Delete(Path.Combine(tPath, "index.tbn"))
                    Delete(Path.Combine(tPath, "index.jpg"))
                Else
                    Dim pPath As String = Path.Combine(tPath, Path.GetFileNameWithoutExtension(mMovie.Filename))
                    Dim pPathStack As String = Path.Combine(tPath, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(mMovie.Filename)))
                    Delete(String.Concat(pPath, ".tbn"))
                    Delete(String.Concat(pPath, ".jpg"))
                    Delete(String.Concat(pPath, "-poster.jpg"))
                    Delete(String.Concat(pPathStack, "-poster.jpg"))
                End If

            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Movie: <" & mMovie.Filename & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteSeasonFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Dim tPath As String = String.Empty

            tPath = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            If Not String.IsNullOrEmpty(tPath) Then
                Delete(Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), ".fanart.jpg")))
                Delete(Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), "-fanart.jpg")))
                Delete(Path.Combine(tPath, "Fanart.jpg"))
            End If
            Delete(Path.Combine(mShow.ShowPath, String.Format("season{0}-fanart.jpg", mShow.TVEp.Season.ToString.PadLeft(2, "0"c))))    'Convert.ToChar("0")
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Show: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub
    ''' <summary>
    ''' Delete the TV Show's season posters
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteSeasonPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Dim tPath As String = String.Empty
            tPath = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            If Not String.IsNullOrEmpty(tPath) Then
                Delete(Path.Combine(tPath, "Poster.tbn"))
                Delete(Path.Combine(tPath, "Poster.jpg"))
                Delete(Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), ".tbn")))
                Delete(Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), ".jpg")))
                Delete(Path.Combine(tPath, "Folder.jpg"))
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Show: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try

        Try
            If mShow.TVEp.Season = 0 Then
                Delete(Path.Combine(mShow.ShowPath, "season-specials.tbn"))
                Delete(Path.Combine(mShow.ShowPath, "season-specials.jpg"))
                Delete(Path.Combine(mShow.ShowPath, "season-specials-poster.jpg"))
            Else
                Delete(Path.Combine(mShow.ShowPath, String.Format("season{0}.tbn", mShow.TVEp.Season)))
                Delete(Path.Combine(mShow.ShowPath, String.Format("season{0}.tbn", mShow.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0")))))
                Delete(Path.Combine(mShow.ShowPath, String.Format("season{0}-poster.jpg", mShow.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0")))))
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Show: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try

    End Sub
    ''' <summary>
    ''' Delete the TV Show's Fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteShowFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Delete(Path.Combine(mShow.ShowPath, "fanart.jpg"))
            Delete(Path.Combine(mShow.ShowPath, String.Concat(FileUtils.Common.GetDirectory(mShow.ShowPath), "-fanart.jpg")))
            Delete(Path.Combine(mShow.ShowPath, String.Concat(FileUtils.Common.GetDirectory(mShow.ShowPath), ".fanart.jpg")))
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Show: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's posters
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteShowPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Delete(Path.Combine(mShow.ShowPath, "folder.jpg"))
            Delete(Path.Combine(mShow.ShowPath, "poster.tbn"))
            Delete(Path.Combine(mShow.ShowPath, "poster.jpg"))
        Catch ex As Exception
            Master.eLog.WriteToErrorLog("Show: <" & mShow.ShowPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Loads this Image from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the image file</param>
    ''' <remarks></remarks>
    Public Sub FromFile(ByVal sPath As String)
        'Debug.Print("FromFile/t{0}", sPath)
        If Not IsNothing(Me._ms) Then
            Me._ms.Dispose()
        End If
        If Not IsNothing(Me._image) Then
            Me._image.Dispose()
        End If
        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Try
                Me._ms = New MemoryStream()
                Using fsImage As New FileStream(sPath, FileMode.Open, FileAccess.Read)
                    Dim StreamBuffer(Convert.ToInt32(fsImage.Length - 1)) As Byte

                    fsImage.Read(StreamBuffer, 0, StreamBuffer.Length)
                    Me._ms.Write(StreamBuffer, 0, StreamBuffer.Length)

                    StreamBuffer = Nothing
                    '_ms.SetLength(fsImage.Length)
                    'fsImage.Read(_ms.GetBuffer(), 0, Convert.ToInt32(fsImage.Length))
                    Me._ms.Flush()
                    _image = New Bitmap(Me._ms)
                End Using
            Catch ex As Exception
                Master.eLog.WriteToErrorLog("Path: <" & sPath & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error: " & sPath)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Loads this Image from the supplied URL
    ''' </summary>
    ''' <param name="sURL">URL to the image file</param>
    ''' <remarks></remarks>
    Public Sub FromWeb(ByVal sURL As String)
        If String.IsNullOrEmpty(sURL) Then Return

        Try
            sHTTP.StartDownloadImage(sURL)
            'Debug.Print("FromWeb/t{0}", sURL)
            While sHTTP.IsDownloading
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If Not IsNothing(sHTTP.Image) Then
                If Not IsNothing(Me._ms) Then
                    Me._ms.Dispose()
                End If
                Me._ms = New MemoryStream()

                Dim retSave() As Byte
                retSave = sHTTP.ms.ToArray
                Me._ms.Write(retSave, 0, retSave.Length)

                'I do not copy from the _ms as it could not be a JPG
                _image = New Bitmap(sHTTP.Image)

                ' if is not a JPG we have to convert the memory stream to JPG format
                If Not sHTTP.isJPG Then
                    UpdateMSfromImg(New Bitmap(_image))
                End If
            End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog("URL: <" & sURL & ">" & vbNewLine & ex.Message, ex.StackTrace, "Error: " & sURL)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mMovie"></param>
    ''' <param name="fType"></param>
    ''' <param name="isChange"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsAllowedToDownload(ByVal mMovie As Structures.DBMovie, ByVal fType As Enums.ImageType, Optional ByVal isChange As Boolean = False) As Boolean
        '2013/11/26 Dekker500 - Need to figure out exactly what this method is doing so it can be documented

        Try
            Select Case fType
                Case Enums.ImageType.Fanart
                    If (isChange OrElse (String.IsNullOrEmpty(mMovie.FanartPath) OrElse Master.eSettings.OverwriteFanart)) AndAlso _
                    (Master.eSettings.MovieNameDotFanartJPG OrElse Master.eSettings.MovieNameFanartJPG OrElse Master.eSettings.FanartJPG _
                     OrElse Master.eSettings.FanartFrodo OrElse Master.eSettings.FanartEden OrElse Master.eSettings.FanartYAMJ OrElse _
                     Master.eSettings.FanartNMJ) Then
                        ' Removed as there is not ONLY the TMDB scraper. Also the GetSetting is bound the calling procedure, is always true 
                        ' AndAlso AdvancedSettings.GetBooleanSetting("UseTMDB", True) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType.EFanarts 'to-do: move Overwrite to SaveAsExtraFanart
                    If (isChange OrElse (String.IsNullOrEmpty(mMovie.EFanartsPath) OrElse Master.eSettings.OverwriteEFanarts) AndAlso (Master.eSettings.ExtrafanartsEden OrElse Master.eSettings.ExtrafanartsFrodo)) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType.EThumbs 'to-do: move Overwrite to SaveAsExtraThumb
                    If (isChange OrElse (String.IsNullOrEmpty(mMovie.EThumbsPath) OrElse Master.eSettings.OverwriteEThumbs) AndAlso (Master.eSettings.ExtrathumbsEden OrElse Master.eSettings.ExtrathumbsFrodo)) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Else
                    If (isChange OrElse (String.IsNullOrEmpty(mMovie.PosterPath) OrElse Master.eSettings.OverwritePoster)) AndAlso _
                    (Master.eSettings.MovieTBN OrElse Master.eSettings.MovieNameTBN OrElse Master.eSettings.MovieJPG OrElse _
                     Master.eSettings.MovieNameJPG OrElse Master.eSettings.MovieNameDashPosterJPG OrElse Master.eSettings.PosterTBN OrElse _
                     Master.eSettings.PosterJPG OrElse Master.eSettings.FolderJPG OrElse Master.eSettings.PosterFrodo OrElse Master.eSettings.PosterEden OrElse _
                     Master.eSettings.PosterYAMJ OrElse Master.eSettings.PosterNMJ) Then
                        ' Removed as there is not ONLY the Native scraper scraper. Also the GetSetting is bound the calling procedure, is always true 
                        ' AndAlso (AdvancedSettings.GetBooleanSetting("UseIMPA", False) OrElse AdvancedSettings.GetBooleanSetting("UseMPDB", False) OrElse AdvancedSettings.GetBooleanSetting("UseTMDB", True)) Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            Return False
        End Try
    End Function

    Public Sub ResizeExtraThumb(ByVal fromPath As String, ByVal toPath As String)
        'Debug.Print("---------- ResizeExtraThumb ----------")
        'Me.FromFile(fromPath)
        'If Not Master.eSettings.ETNative Then
        '	Dim iWidth As Integer = Master.eSettings.ETWidth
        '	Dim iHeight As Integer = Master.eSettings.ETHeight
        '	ImageUtils.ResizeImage(_image, iWidth, iHeight, Master.eSettings.ETPadding, Color.Black.ToArgb)
        'End If
        'Me.Save(toPath)
    End Sub
    ''' <summary>
    ''' Stores the Image to the supplied <paramref name="sPath"/>
    ''' </summary>
    ''' <param name="sPath">Location to store the image</param>
    ''' <param name="iQuality"><c>Integer</c> value representing <c>Encoder.Quality</c>. 0 is lowest quality, 100 is highest</param>
    ''' <param name="sUrl">URL of desired image</param>
    ''' <param name="doResize"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal sPath As String, Optional ByVal iQuality As Long = 0, Optional ByVal sUrl As String = "", Optional ByVal doResize As Boolean = False)
        '2013/11/26 Dekker500 - This method is a swiss army knife. Completely different behaviour based on what parameter is supplied. Break it down a bit for a more logical flow (if I set a path and URL and quality but no resize, it'll happily ignore everything but the path)
        Dim retSave() As Byte
        Try
            If Not doResize Then
                'EmberAPI.FileUtils.Common.MoveFileWithStream(sUrl, sPath)
                retSave = _ms.ToArray

                'make sure directory exists
                Directory.CreateDirectory(Directory.GetParent(sPath).FullName)
                If sPath.Length <= 260 Then
                    Using fs As New FileStream(sPath, FileMode.Create, FileAccess.Write)
                        fs.Write(retSave, 0, retSave.Length)
                        fs.Flush()
                        fs.Close()
                    End Using
                End If
                Return
            End If

            If IsNothing(_image) Then Exit Sub

            Dim doesExist As Boolean = File.Exists(sPath)
            Dim fAtt As New FileAttributes
            Dim fAttWritable As Boolean = True
            If Not String.IsNullOrEmpty(sPath) AndAlso (Not doesExist OrElse (Not CBool(File.GetAttributes(sPath) And FileAttributes.ReadOnly))) Then
                If doesExist Then
                    'get the current attributes to set them back after writing
                    fAtt = File.GetAttributes(sPath)
                    'set attributes to none for writing
                    Try
                        File.SetAttributes(sPath, FileAttributes.Normal)
                    Catch ex As Exception
                        fAttWritable = False
                    End Try
                End If

                If Not sUrl = "" Then

                    Dim webclient As New Net.WebClient
                    'Download image!
                    webclient.DownloadFile(sUrl, sPath)

                Else
                    Using msSave As New MemoryStream
                        Dim ICI As ImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg)
                        Dim EncPars As EncoderParameters = New EncoderParameters(If(iQuality > 0, 2, 1))

                        EncPars.Param(0) = New EncoderParameter(Encoder.RenderMethod, EncoderValue.RenderNonProgressive)

                        If iQuality > 0 Then
                            EncPars.Param(1) = New EncoderParameter(Encoder.Quality, iQuality)
                        End If

                        _image.Save(msSave, ICI, EncPars)

                        retSave = msSave.ToArray

                        'make sure directory exists
                        Directory.CreateDirectory(Directory.GetParent(sPath).FullName)
                        If sPath.Length <= 260 Then
                            Using fs As New FileStream(sPath, FileMode.Create, FileAccess.Write)
                                fs.Write(retSave, 0, retSave.Length)
                                fs.Flush()
                            End Using
                        End If
                        msSave.Flush()
                    End Using
                    'once is saved as teh quality is defined from the user we need to reload the new image to align _ms and _image
                    Me.FromFile(sPath)
                End If

                If doesExist And fAttWritable Then File.SetAttributes(sPath, fAtt)
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Saves the image as the AllSeason poster
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsAllSeasonPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizeAllSPoster AndAlso (_image.Width > Master.eSettings.AllSPosterWidth OrElse _image.Height > Master.eSettings.AllSPosterHeight)

		Try
			Dim pPath As String = String.Empty

			If doResize Then
				ImageUtils.ResizeImage(_image, Master.eSettings.AllSPosterWidth, Master.eSettings.AllSPosterHeight)
			End If

			Try
				Dim params As New List(Of Object)(New Object() {Enums.TVImageType.AllSeasonPoster, mShow, New List(Of String)})
				Dim doContinue As Boolean = True
				ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
				For Each s As String In DirectCast(params(2), List(Of String))
					If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteAllSPoster) Then
						Save(s, Master.eSettings.AllSPosterQuality, sURL, doResize)
						If String.IsNullOrEmpty(strReturn) Then strReturn = s
					End If
				Next
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

			If Master.eSettings.SeasonAllJPG Then
				pPath = Path.Combine(mShow.ShowPath, "season-all.jpg")
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteAllSPoster) Then
					Save(pPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

			If Master.eSettings.SeasonAllTBN Then
				pPath = Path.Combine(mShow.ShowPath, "season-all.tbn")
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteAllSPoster) Then
					Save(pPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

			If Master.eSettings.SeasonAllPosterJPG Then
				pPath = Path.Combine(mShow.ShowPath, "season-all-poster.jpg")
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteAllSPoster) Then
					Save(pPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

		Catch ex As Exception
			Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
		End Try

		Return strReturn
	End Function
    ''' <summary>
    ''' Saves the image as the episode fanart
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsEpFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizeEpFanart AndAlso (_image.Width > Master.eSettings.EpFanartWidth OrElse _image.Height > Master.eSettings.EpFanartHeight)

		Try
			Dim tPath As String = String.Empty

			If doResize Then
				ImageUtils.ResizeImage(_image, Master.eSettings.EpFanartWidth, Master.eSettings.EpFanartHeight)
			End If
			Try
				Dim params As New List(Of Object)(New Object() {Enums.TVImageType.EpisodeFanart, mShow, New List(Of String)})
				Dim doContinue As Boolean = True
				ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
				For Each s As String In DirectCast(params(2), List(Of String))
					If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpFanart) Then
						Save(s, Master.eSettings.AllSPosterQuality, sURL, doResize)
						If String.IsNullOrEmpty(strReturn) Then strReturn = s
					End If
				Next
				If Not doContinue Then
					Return strReturn
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

			If Master.eSettings.EpisodeDotFanart Then
				tPath = String.Concat(FileUtils.Common.RemoveExtFromPath(mShow.Filename), ".fanart.jpg")
				If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpFanart) Then
					Save(tPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = tPath
				End If
			End If

			If Master.eSettings.EpisodeDashFanart Then
				tPath = String.Concat(FileUtils.Common.RemoveExtFromPath(mShow.Filename), "-fanart.jpg")
				If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpFanart) Then
					Save(tPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = tPath
				End If
			End If

		Catch ex As Exception
			Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
		End Try
		Return strReturn
	End Function
    ''' <summary>
    ''' Save the image as an episode poster
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsEpPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizeEpPoster AndAlso (_image.Width > Master.eSettings.EpPosterWidth OrElse _image.Height > Master.eSettings.EpPosterHeight)

		Try
			Dim pPath As String = String.Empty

			If doResize Then
				ImageUtils.ResizeImage(_image, Master.eSettings.EpPosterWidth, Master.eSettings.EpPosterHeight)
			End If
			Try
				Dim params As New List(Of Object)(New Object() {Enums.TVImageType.EpisodePoster, mShow, New List(Of String)})
				Dim doContinue As Boolean = True
				ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
				For Each s As String In DirectCast(params(2), List(Of String))
					If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpPoster) Then
						Save(s, Master.eSettings.AllSPosterQuality, sURL, doResize)
						If String.IsNullOrEmpty(strReturn) Then strReturn = s
					End If
				Next
				If Not doContinue Then
					Return strReturn
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

			If Master.eSettings.EpisodeJPG Then
				pPath = String.Concat(FileUtils.Common.RemoveExtFromPath(mShow.Filename), ".jpg")
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpPoster) Then
					Save(pPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

			If Master.eSettings.EpisodeTBN Then
				pPath = String.Concat(FileUtils.Common.RemoveExtFromPath(mShow.Filename), ".tbn")
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpPoster) Then
					Save(pPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

			If Master.eSettings.EpisodeDashThumbJPG Then
				pPath = String.Concat(FileUtils.Common.RemoveExtFromPath(mShow.Filename), "-thumb.jpg")
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteEpPoster) Then
					Save(pPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

		Catch ex As Exception
			Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
		End Try
		Return strReturn
	End Function
    ''' <summary>
    ''' Save the image as a movie fanart
    ''' </summary>
    ''' <param name="mMovie"><c></c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsFanart(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizeFanart AndAlso (_image.Width > Master.eSettings.FanartWidth OrElse _image.Height > Master.eSettings.FanartHeight)

		Try
            Dim fPath As String = String.Empty 'fanart path
            Dim parPath As String = String.Empty
            Dim fileName As String = Path.GetFileNameWithoutExtension(mMovie.Filename)
            Dim fileNameStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(mMovie.Filename))
            Dim filePath As String = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, fileName)
            Dim filePathStack As String = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, fileNameStack)
            Dim fileParPath As String = Directory.GetParent(filePath).FullName

			Try
				Dim params As New List(Of Object)(New Object() {mMovie})
				ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieFanartSave, params, _image, False)
			Catch ex As Exception
			End Try

			If doResize Then
				ImageUtils.ResizeImage(_image, Master.eSettings.FanartWidth, Master.eSettings.FanartHeight)
			End If

            '*************** XBMC Frodo settings ***************
            If Master.eSettings.UseFrodo Then
                If Master.eSettings.FanartFrodo Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        fPath = Path.Combine(Directory.GetParent(fileParPath).FullName, "fanart.jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        fPath = Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, "fanart.jpg")
                    Else
                        If fileName.ToLower = "video_ts" Then
                            fPath = Path.Combine(fileParPath, "fanart.jpg")
                        Else
                            fPath = String.Concat(filePathStack, "-fanart.jpg")
                        End If
                    End If

                    If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
                        Save(fPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
                        strReturn = fPath
                    End If

                    If Master.eSettings.AutoBD AndAlso Directory.Exists(Master.eSettings.BDPath) Then
                        Save(String.Concat(Master.eSettings.BDPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), Master.eSettings.AllSPosterQuality, sURL, doResize)
                    End If
                End If
            End If

            '*************** XBMC Eden settings ***************
            If Master.eSettings.UseEden Then
                If Master.eSettings.FanartEden Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        fPath = String.Concat(filePath, "-fanart.jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        fPath = Path.Combine(Directory.GetParent(fileParPath).FullName, "index-fanart.jpg")
                    Else
                        fPath = String.Concat(filePath, "-fanart.jpg")
                    End If

                    If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
                        Save(fPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
                        strReturn = fPath
                    End If

                    If Master.eSettings.AutoBD AndAlso Directory.Exists(Master.eSettings.BDPath) Then
                        Save(String.Concat(Master.eSettings.BDPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), Master.eSettings.AllSPosterQuality, sURL, doResize)
                    End If
                End If
            End If

            '****************** YAMJ settings *****************
            If Master.eSettings.UseYAMJ Then
                If Master.eSettings.FanartYAMJ Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        fPath = String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".fanart.jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".fanart.jpg")
                    Else
                        fPath = String.Concat(filePath, ".fanart.jpg")
                    End If

                    If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
                        Save(fPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
                        strReturn = fPath
                    End If

                    If Master.eSettings.AutoBD AndAlso Directory.Exists(Master.eSettings.BDPath) Then
                        Save(String.Concat(Master.eSettings.BDPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), Master.eSettings.AllSPosterQuality, sURL, doResize)
                    End If
                End If
            End If

            '****************** NMJ settings *****************
            If Master.eSettings.UseNMJ Then
                If Master.eSettings.FanartNMJ Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".fanart.jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".fanart.jpg")
                    Else
                        fPath = String.Concat(filePathStack, ".fanart.jpg")
                    End If

                    If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
                        Save(fPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
                        strReturn = fPath
                    End If

                    If Master.eSettings.AutoBD AndAlso Directory.Exists(Master.eSettings.BDPath) Then
                        Save(String.Concat(Master.eSettings.BDPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), Master.eSettings.AllSPosterQuality, sURL, doResize)
                    End If
                End If
            End If

            '***************** Expert settings ****************
            'If Master.eSettings.UseExpert Then
            '    If Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '        If Master.eSettings.FanartJPG Then
            '            fPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "fanart.jpg")
            '        ElseIf Master.eSettings.MovieNameFanartJPG Then
            '            fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).Name), "-fanart.jpg")
            '        ElseIf Master.eSettings.MovieNameFanartJPG Then
            '            fPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "fanart.jpg")
            '        ElseIf Master.eSettings.MovieNameDotFanartJPG Then
            '            fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).Name), ".fanart.jpg")
            '        Else
            '            fPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "fanart.jpg")
            '        End If

            '        If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '            Save(fPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
            '            strReturn = fPath
            '        End If
            '    ElseIf Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isBDRip(mMovie.Filename) Then
            '        If Master.eSettings.FanartJPG Then
            '            fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "fanart.jpg"))
            '        ElseIf Master.eSettings.MovieNameFanartJPG AndAlso Not Master.eSettings.VideoTSParentXBMC Then
            '            fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).Name), "-fanart.jpg")
            '        ElseIf Master.eSettings.MovieNameFanartJPG AndAlso Master.eSettings.VideoTSParentXBMC Then
            '            fPath = String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, Path.DirectorySeparatorChar, "fanart.jpg")
            '        ElseIf Master.eSettings.MovieNameDotFanartJPG Then
            '            fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).Name), ".fanart.jpg")
            '        Else
            '            fPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "fanart.jpg"))
            '        End If

            '        If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '            Save(fPath, Master.eSettings.AllSPosterQuality, sURL, doResize)
            '            strReturn = fPath
            '        End If
            '        If Master.eSettings.AutoBD AndAlso Directory.Exists(Master.eSettings.BDPath) Then
            '            Save(String.Concat(Master.eSettings.BDPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), Master.eSettings.AllSPosterQuality, sURL, doResize)
            '        End If
            '    Else
            '        If Master.eSettings.MovieNameDotFanartJPG AndAlso (Not mMovie.isSingle OrElse Not Master.eSettings.MovieNameMultiOnly) Then
            '            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '                fPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "video_ts.fanart.jpg")
            '            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
            '                fPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "index.fanart.jpg")
            '            Else
            '                fPath = String.Concat(filePath, ".fanart.jpg")
            '            End If
            '            If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteFanart) Then
            '                Save(fPath, Master.eSettings.FanartQuality, sURL, doResize)
            '                strReturn = fPath
            '            End If
            '        End If

            '        If Master.eSettings.MovieNameFanartJPG AndAlso (Not mMovie.isSingle OrElse Not Master.eSettings.MovieNameMultiOnly) Then
            '            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '                fPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "video_ts-fanart.jpg")
            '            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
            '                fPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "index-fanart.jpg")
            '            Else
            '                If Master.eSettings.VideoTSParentXBMC AndAlso fileName.ToLower = "video_ts" Then
            '                    fPath = String.Concat(Directory.GetParent(filePath).FullName, Path.DirectorySeparatorChar, "fanart.jpg")
            '                Else
            '                    fPath = String.Concat(filePathStack, "-fanart.jpg")
            '                End If
            '            End If

            '            If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteFanart) Then
            '                Save(fPath, Master.eSettings.FanartQuality, sURL, doResize)
            '                strReturn = fPath
            '            End If
            '        End If

            '        If Master.eSettings.FanartJPG AndAlso mMovie.isSingle Then
            '            fPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "fanart.jpg")
            '            If Not File.Exists(fPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteFanart) Then
            '                Save(fPath, Master.eSettings.FanartQuality, sURL, doResize)
            '                strReturn = fPath
            '            End If
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

		Return strReturn
	End Function
    ''' <summary>
    ''' Save the image as a movie poster
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsPoster(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizePoster AndAlso (_image.Width > Master.eSettings.PosterWidth OrElse _image.Height > Master.eSettings.PosterHeight)

		Try
            Dim pPath As String = String.Empty 'poster path
            Dim fileName As String = Path.GetFileNameWithoutExtension(mMovie.Filename)
            Dim fileNameStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(mMovie.Filename))
            Dim filePath As String = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, fileName)
            Dim filePathStack As String = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, fileNameStack)
            Dim fileParPath As String = Directory.GetParent(filePath).FullName

			Try
				Dim params As New List(Of Object)(New Object() {mMovie})
				ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMoviePosterSave, params, _image, False)
			Catch ex As Exception
			End Try

			If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.PosterWidth, Master.eSettings.PosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
			End If

            '*************** XBMC Frodo settings ***************
            If Master.eSettings.UseFrodo Then
                If Master.eSettings.PosterFrodo Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        pPath = Path.Combine(Directory.GetParent(fileParPath).FullName, "poster.jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        pPath = Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, "poster.jpg")
                    Else
                        If fileName.ToLower = "video_ts" Then
                            pPath = Path.Combine(fileParPath, "poster.jpg")
                        Else
                            pPath = String.Concat(filePathStack, "-poster.jpg")
                        End If
                    End If

                    If Not pPath = String.Empty And (Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster)) Then
                        Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
                        strReturn = pPath
                    End If
                End If
            End If

            '*************** XBMC Eden settings ***************
            If Master.eSettings.UseEden Then
                If Master.eSettings.PosterEden Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        pPath = String.Concat(filePath, ".tbn")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        pPath = Path.Combine(Directory.GetParent(fileParPath).FullName, "index.tbn")
                    Else
                        pPath = String.Concat(filePath, ".tbn")
                    End If

                    If Not pPath = String.Empty And (Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster)) Then
                        Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
                        strReturn = pPath
                    End If
                End If
            End If

            '****************** YAMJ settings *****************
            If Master.eSettings.UseYAMJ Then
                If Master.eSettings.PosterYAMJ Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        pPath = String.Concat(Path.Combine(Directory.GetParent(fileParPath).FullName, Directory.GetParent(fileParPath).Name), ".jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".jpg")
                    Else
                        pPath = String.Concat(filePath, ".jpg")
                    End If

                    If Not pPath = String.Empty And (Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster)) Then
                        Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
                        strReturn = pPath
                    End If
                End If
            End If

            '****************** NMJ settings ******************
            If Master.eSettings.UseNMJ Then
                If Master.eSettings.PosterNMJ Then
                    If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                        pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName, Directory.GetParent(fileParPath).Name), ".jpg")
                    ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                        pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(fileParPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(fileParPath).FullName).Name), ".jpg")
                    Else
                        pPath = String.Concat(filePathStack, ".jpg")
                    End If

                    If Not pPath = String.Empty And (Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster)) Then
                        Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
                        strReturn = pPath
                    End If
                End If
            End If

            '***************** Expert settings ****************
            'If Master.eSettings.UseExpert Then
            '    If (Master.eSettings.VideoTSParent OrElse Master.eSettings.VideoTSParentXBMC) AndAlso FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '        With Master.eSettings
            '            If .MovieNameJPG Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).Name), ".jpg")
            '            ElseIf .MovieJPG Then
            '                pPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "movie.jpg")
            '            ElseIf .FolderJPG Then
            '                pPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "folder.jpg")
            '            ElseIf .PosterJPG Then
            '                pPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "poster.jpg")
            '            ElseIf .MovieNameTBN Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).Name), ".tbn")
            '            ElseIf .MovieNameDashPosterJPG And Not Master.eSettings.VideoTSParentXBMC Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).Name), "-poster.jpg")
            '            ElseIf .MovieNameDashPosterJPG And Master.eSettings.VideoTSParentXBMC Then
            '                pPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "poster.jpg")
            '            ElseIf .MovieTBN Then
            '                pPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "movie.tbn")
            '            ElseIf .PosterTBN Then
            '                pPath = String.Concat(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, Path.DirectorySeparatorChar, "poster.tbn")
            '            Else
            '                pPath = String.Empty
            '            End If
            '        End With

            '        If Not pPath = String.Empty And (Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster)) Then
            '            Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '            strReturn = pPath
            '        End If
            '    ElseIf (Master.eSettings.VideoTSParent OrElse Master.eSettings.VideoTSParentXBMC) AndAlso FileUtils.Common.isBDRip(mMovie.Filename) Then
            '        With Master.eSettings
            '            If .MovieNameJPG Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).Name), ".jpg")
            '            ElseIf .MovieJPG Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "movie.jpg"))
            '            ElseIf .FolderJPG Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "folder.jpg"))
            '            ElseIf .PosterJPG Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "poster.jpg"))
            '            ElseIf .MovieNameTBN Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).Name), ".tbn")
            '            ElseIf .MovieNameDashPosterJPG And Not Master.eSettings.VideoTSParentXBMC Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).Name), "-poster.jpg")
            '            ElseIf .MovieNameDashPosterJPG And Master.eSettings.VideoTSParentXBMC Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "poster.jpg"))
            '            ElseIf .MovieTBN Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "movie.tbn"))
            '            ElseIf .PosterTBN Then
            '                pPath = String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName).FullName, "poster.tbn"))
            '            Else
            '                pPath = String.Empty
            '            End If
            '        End With

            '        If Not pPath = String.Empty And (Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster)) Then
            '            Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '            strReturn = pPath
            '        End If
            '    Else
            '        If Master.eSettings.FolderJPG AndAlso mMovie.isSingle Then
            '            pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "folder.jpg")
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.PosterJPG AndAlso mMovie.isSingle Then
            '            pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "poster.jpg")
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.PosterTBN AndAlso mMovie.isSingle Then
            '            pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "poster.tbn")
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.MovieNameJPG AndAlso (Not mMovie.isSingle OrElse Not Master.eSettings.MovieNameMultiOnly) Then
            '            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '                pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "video_ts.jpg")
            '            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
            '                pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "index.jpg")
            '            Else
            '                pPath = String.Concat(filePath, ".jpg")
            '            End If
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.MovieNameDashPosterJPG AndAlso (Not mMovie.isSingle OrElse Not Master.eSettings.MovieNameMultiOnly) Then
            '            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '                pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "video_ts-poster.jpg")
            '            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
            '                pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "index-poster.jpg")
            '            Else
            '                If Master.eSettings.VideoTSParentXBMC AndAlso fileName.ToLower = "video_ts" Then
            '                    pPath = String.Concat(Directory.GetParent(pPath).FullName, Path.DirectorySeparatorChar, "poster.jpg")
            '                Else
            '                    pPath = String.Concat(filePathStack, "-poster.jpg")
            '                End If
            '            End If
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.MovieJPG AndAlso mMovie.isSingle Then
            '            pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "movie.jpg")
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.MovieNameTBN AndAlso (Not mMovie.isSingle OrElse Not Master.eSettings.MovieNameMultiOnly) Then
            '            If FileUtils.Common.isVideoTS(mMovie.Filename) Then
            '                pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "video_ts.tbn")
            '            ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
            '                pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "index.tbn")
            '            Else
            '                pPath = String.Concat(pPath, ".tbn")
            '            End If
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If

            '        If Master.eSettings.MovieTBN AndAlso mMovie.isSingle Then
            '            pPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "movie.tbn")
            '            If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
            '                Save(pPath, Master.eSettings.PosterQuality, sURL, doResize)
            '                strReturn = pPath
            '            End If
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
		Return strReturn
	End Function
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <param name="fpath"><c>String</c> representing the movie path</param>
    ''' <param name="aMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsActorThumb(ByVal actor As MediaContainers.Person, ByVal fpath As String, ByVal aMovie As Structures.DBMovie) As String
        'TODO 2013/11/26 Dekker500 - This should be re-factored to remove the fPath argument. All callers pass the same string derived from the provided DBMovie, so why do it twice?
        Dim tPath As String = String.Empty

        If Master.eSettings.UseFrodo Then
            If Master.eSettings.ActorThumbsFrodo AndAlso FileUtils.Common.isBDRip(aMovie.Filename) Then
                tPath = Path.Combine(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".jpg"))
                If Not Directory.Exists(Path.Combine(Directory.GetParent(fpath).FullName, ".actors")) Then Directory.CreateDirectory(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"))
                If Not File.Exists(tPath) Then
                    Save(tPath, Master.eSettings.PosterQuality)
                End If
            Else
                tPath = Path.Combine(Path.Combine(fpath, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".jpg"))
                If Not Directory.Exists(Path.Combine(fpath, ".actors")) Then Directory.CreateDirectory(Path.Combine(fpath, ".actors"))
                If Not File.Exists(tPath) Then
                    Save(tPath, Master.eSettings.PosterQuality)
                End If
            End If
        End If
        If Master.eSettings.UseEden Then
            If Master.eSettings.ActorThumbsEden AndAlso FileUtils.Common.isBDRip(aMovie.Filename) Then
                tPath = Path.Combine(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".tbn"))
                If Not Directory.Exists(Path.Combine(Directory.GetParent(fpath).FullName, ".actors")) Then Directory.CreateDirectory(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"))
                If Not File.Exists(tPath) Then
                    Save(tPath, Master.eSettings.PosterQuality)
                End If
            Else
                tPath = Path.Combine(Path.Combine(fpath, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".tbn"))
                If Not Directory.Exists(Path.Combine(fpath, ".actors")) Then Directory.CreateDirectory(Path.Combine(fpath, ".actors"))
                If Not File.Exists(tPath) Then ' OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
                    Save(tPath, Master.eSettings.PosterQuality)
                End If
            End If
        End If

        'Cocotus 20130905 If FRODO VIDEO_TS Handling is enabled, .actors-folder is now saved in parent directory for DVD folders too (like poster/fanart!) we shouldn't mess with DVD structure!
        'recommended for Frodo by XBMC developer: 'more here http://forum.xbmc.org/showthread.php?tid=166013 (XBMC Developer statement)
        'If Master.eSettings.VideoTSParentXBMC AndAlso (FileUtils.Common.isBDRip(aMovie.Filename) OrElse FileUtils.Common.isVideoTS(aMovie.Filename)) Then
        '    '	If Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(aMovie.Filename) Then
        '    tPath = Path.Combine(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".jpg"))
        '    If Not Directory.Exists(Path.Combine(Directory.GetParent(fpath).FullName, ".actors")) Then Directory.CreateDirectory(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"))
        '    If Not File.Exists(tPath) Then ' OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
        '        Save(tPath, Master.eSettings.PosterQuality)
        '    End If
        'Else
        '    tPath = Path.Combine(Path.Combine(fpath, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".jpg"))
        '    If Not Directory.Exists(Path.Combine(fpath, ".actors")) Then Directory.CreateDirectory(Path.Combine(fpath, ".actors"))
        '    If Not File.Exists(tPath) Then ' OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
        '        Save(tPath, Master.eSettings.PosterQuality)
        '    End If
        'End If
        'End If

        'old
        'If Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(aMovie.Filename) Then
        '	tPath = Path.Combine(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".jpg"))
        '	If Not Directory.Exists(Path.Combine(Directory.GetParent(fpath).FullName, ".actors")) Then Directory.CreateDirectory(Path.Combine(Directory.GetParent(fpath).FullName, ".actors"))
        '	If Not File.Exists(tPath) Then ' OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
        '		Save(tPath, Master.eSettings.PosterQuality)
        '	End If
        'Else
        '	tPath = Path.Combine(Path.Combine(fpath, ".actors"), String.Concat(actor.Name.Replace(" ", "_"), ".jpg"))
        '	If Not Directory.Exists(Path.Combine(fpath, ".actors")) Then Directory.CreateDirectory(Path.Combine(fpath, ".actors"))
        '	If Not File.Exists(tPath) Then ' OrElse (IsEdit OrElse Master.eSettings.OverwritePoster) Then
        '		Save(tPath, Master.eSettings.PosterQuality)
        '	End If
        'End If
        ''End If
        Return tPath
    End Function
    ''' <summary>
    ''' Save the image as the TV Show's season fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsSeasonFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizeSeasonFanart AndAlso (_image.Width > Master.eSettings.SeasonFanartWidth OrElse _image.Height > Master.eSettings.SeasonFanartHeight)

		Try
            Dim pPath As String = String.Empty

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.SeasonFanartWidth, Master.eSettings.SeasonFanartHeight)
            End If
            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.SeasonFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonFanart) Then

                        Save(s, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try

            If Master.eSettings.SeasonFanartJPG OrElse Master.eSettings.SeasonDashFanart OrElse Master.eSettings.SeasonXXDashFanartJPG OrElse Master.eSettings.SeasonDotFanart Then
                Dim tPath As String = String.Empty

                Try
                    tPath = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
                Catch ex As Exception
                End Try

                If Not String.IsNullOrEmpty(tPath) Then

                    If Master.eSettings.SeasonDotFanart Then
                        pPath = Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), ".fanart.jpg"))
                        If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonFanart) Then
                            Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                            strReturn = pPath
                        End If
                    End If

                    If Master.eSettings.SeasonDashFanart Then
                        pPath = Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), "-fanart.jpg"))
                        If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonFanart) Then
                            Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                            strReturn = pPath
                        End If
                    End If

                    If Master.eSettings.SeasonFanartJPG Then
                        pPath = Path.Combine(tPath, "Fanart.jpg")
                        If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonFanart) Then
                            Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                            strReturn = pPath
                        End If
                    End If

                    If Master.eSettings.SeasonXXDashFanartJPG Then
                        If mShow.TVEp.Season = 0 Then
                            pPath = Path.Combine(mShow.ShowPath, "season-specials-fanart.jpg")
                        Else
                            pPath = Path.Combine(mShow.ShowPath, String.Format("season{0}-fanart.jpg", mShow.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0"))))
                        End If
                        If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonFanart) Then
                            Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                            strReturn = pPath
                        End If
                    End If

                Else
                    If Master.eSettings.SeasonXXDashFanartJPG Then
                        If mShow.TVEp.Season = 0 Then
                            pPath = Path.Combine(mShow.ShowPath, "season-specials-fanart.jpg")
                        Else
                            pPath = Path.Combine(mShow.ShowPath, String.Format("season{0}-fanart.jpg", mShow.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0"))))
                        End If
                        If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonFanart) Then
                            Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                            strReturn = pPath
                        End If
                    End If
                End If
            End If

		Catch ex As Exception
			Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
		End Try
		Return strReturn
	End Function
    ''' <summary>
    ''' Save the image as a TV Show's season poster
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsSeasonPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
		Dim strReturn As String = String.Empty
		Dim doResize As Boolean = Master.eSettings.ResizeSeasonPoster AndAlso (_image.Width > Master.eSettings.SeasonPosterWidth OrElse _image.Height > Master.eSettings.SeasonPosterHeight)

		Try
			Dim pPath As String = String.Empty

			If doResize Then
				ImageUtils.ResizeImage(_image, Master.eSettings.SeasonPosterWidth, Master.eSettings.SeasonPosterHeight)
			End If

			Try
				Dim params As New List(Of Object)(New Object() {Enums.TVImageType.SeasonPoster, mShow, New List(Of String)})
				Dim doContinue As Boolean = True
				ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
				For Each s As String In DirectCast(params(2), List(Of String))
					If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
						Save(s, Master.eSettings.SeasonPosterQuality, sURL, doResize)
						If String.IsNullOrEmpty(strReturn) Then strReturn = s
					End If
				Next
				If Not doContinue Then
					Return strReturn
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

			If Master.eSettings.SeasonPosterTBN OrElse Master.eSettings.SeasonPosterJPG OrElse Master.eSettings.SeasonNameTBN OrElse _
			Master.eSettings.SeasonNameJPG OrElse Master.eSettings.FolderJPG Then
				Dim tPath As String = String.Empty
				Try
					tPath = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
				Catch
				End Try

				If Not String.IsNullOrEmpty(tPath) Then
					If Master.eSettings.SeasonPosterTBN Then
						pPath = Path.Combine(tPath, "Poster.tbn")
						If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
							Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
							strReturn = pPath
						End If
					End If

					If Master.eSettings.SeasonPosterJPG Then
						pPath = Path.Combine(tPath, "Poster.jpg")
						If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
							Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
							strReturn = pPath
						End If
					End If

					If Master.eSettings.SeasonNameTBN Then
						pPath = Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), ".tbn"))
						If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
							Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
							strReturn = pPath
						End If
					End If

					If Master.eSettings.SeasonNameJPG Then
						pPath = Path.Combine(tPath, String.Concat(FileUtils.Common.GetDirectory(tPath), ".jpg"))
						If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
							Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
							strReturn = pPath
						End If
					End If

					If Master.eSettings.SeasonFolderJPG Then
						pPath = Path.Combine(tPath, "Folder.jpg")
						If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
							Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
							strReturn = pPath
						End If
					End If
				End If
			End If

			If Master.eSettings.SeasonX Then
				If mShow.TVEp.Season = 0 Then
					pPath = Path.Combine(mShow.ShowPath, "season-specials.tbn")
				Else
					pPath = Path.Combine(mShow.ShowPath, String.Format("season{0}.tbn", mShow.TVEp.Season))
				End If
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
					Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

			If Master.eSettings.SeasonXX Then
				If mShow.TVEp.Season = 0 Then
					pPath = Path.Combine(mShow.ShowPath, "season-specials.tbn")
				Else
					pPath = Path.Combine(mShow.ShowPath, String.Format("season{0}.tbn", mShow.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0"))))
				End If
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
					Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

			If Master.eSettings.SeasonXXDashPosterJPG Then
				If mShow.TVEp.Season = 0 Then
					pPath = Path.Combine(mShow.ShowPath, "season-specials-poster.jpg")
				Else
					pPath = Path.Combine(mShow.ShowPath, String.Format("season{0}-poster.jpg", mShow.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0"))))
				End If
				If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteSeasonPoster) Then
					Save(pPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
					strReturn = pPath
				End If
			End If

		Catch ex As Exception
			Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
		End Try
		Return strReturn
	End Function
    ''' <summary>
    ''' Save the image as a TV Show's fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsShowFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty
        Dim doResize As Boolean = Master.eSettings.ResizeShowFanart AndAlso (_image.Width > Master.eSettings.ShowFanartWidth OrElse _image.Height > Master.eSettings.ShowFanartHeight)

        Try
            Dim tPath As String = String.Empty

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.ShowFanartWidth, Master.eSettings.ShowFanartHeight)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowFanart) Then
                        Save(s, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try

            If Master.eSettings.ShowDotFanart Then
                tPath = Path.Combine(mShow.ShowPath, String.Concat(FileUtils.Common.GetDirectory(mShow.ShowPath), ".fanart.jpg"))
                If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowFanart) Then
                    Save(tPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                    strReturn = tPath
                End If
            End If

            If Master.eSettings.ShowDashFanart Then
                tPath = Path.Combine(mShow.ShowPath, String.Concat(FileUtils.Common.GetDirectory(mShow.ShowPath), "-fanart.jpg"))
                If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowFanart) Then
                    Save(tPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                    strReturn = tPath
                End If
            End If

            If Master.eSettings.ShowFanartJPG Then
                tPath = Path.Combine(mShow.ShowPath, "fanart.jpg")
                If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowFanart) Then
                    Save(tPath, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                    strReturn = tPath
                End If
            End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's poster
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
	Public Function SaveAsShowPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Dim doResize As Boolean = Master.eSettings.ResizeShowPoster AndAlso (_image.Width > Master.eSettings.ShowPosterWidth OrElse _image.Height > Master.eSettings.ShowPosterHeight)

        Try
            Dim pPath As String = String.Empty

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.ShowPosterWidth, Master.eSettings.ShowPosterHeight)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowPoster) Then
                        Save(s, Master.eSettings.SeasonFanartQuality, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try

            If Master.eSettings.ShowPosterJPG Then
                pPath = Path.Combine(mShow.ShowPath, "poster.jpg")
                If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowPoster) Then
                    Save(pPath, Master.eSettings.ShowPosterQuality, sURL, doResize)
                    strReturn = pPath
                End If
            End If

            If Master.eSettings.ShowPosterTBN Then
                pPath = Path.Combine(mShow.ShowPath, "poster.tbn")
                If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowPoster) Then
                    Save(pPath, Master.eSettings.ShowPosterQuality, sURL, doResize)
                    strReturn = pPath
                End If
            End If

            If Master.eSettings.ShowFolderJPG Then
                pPath = Path.Combine(mShow.ShowPath, "folder.jpg")
                If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowPoster) Then
                    Save(pPath, Master.eSettings.ShowPosterQuality, sURL, doResize)
                    strReturn = pPath
                End If
            End If

            If Master.eSettings.ShowJPG Then
                pPath = Path.Combine(mShow.ShowPath, String.Concat(FileUtils.Common.GetDirectory(mShow.ShowPath), ".jpg"))
                If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowPoster) Then
                    Save(pPath, Master.eSettings.ShowPosterQuality, sURL, doResize)
                    strReturn = pPath
                End If
            End If

            If Master.eSettings.ShowTBN Then
                pPath = Path.Combine(mShow.ShowPath, String.Concat(FileUtils.Common.GetDirectory(mShow.ShowPath), ".tbn"))
                If Not File.Exists(pPath) OrElse (IsEdit OrElse Master.eSettings.OverwriteShowPoster) Then
                    Save(pPath, Master.eSettings.ShowPosterQuality, sURL, doResize)
                    strReturn = pPath
                End If
            End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrathumb
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsExtraThumb(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim etPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1
        Dim tPath As String = String.Empty

        If String.IsNullOrEmpty(mMovie.Filename) Then Return etPath

        '*************** XBMC Frodo & Eden settings ***************
        If Master.eSettings.UseFrodo OrElse Master.eSettings.UseEden Then
            If Master.eSettings.ExtrathumbsFrodo OrElse Master.eSettings.ExtrathumbsEden Then
                If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                    tPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "extrathumbs")
                ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                    tPath = Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, "extrathumbs")
                Else
                    tPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "extrathumbs")
                End If

                If Not tPath = String.Empty Then
                    If Not Directory.Exists(tPath) Then
                        Directory.CreateDirectory(tPath)
                    End If
                    iMod = Functions.GetExtraModifier(tPath)
                    iVal = iMod + 1
                    etPath = Path.Combine(tPath, String.Concat("thumb", iVal, ".jpg"))
                    Save(etPath)
                    Return etPath
                End If
            End If
        End If

        Return etPath
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrathumb
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sName"><c>String</c> name of the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsExtraFanart(ByVal mMovie As Structures.DBMovie, ByVal sName As String, Optional sURL As String = "") As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1
        Dim tPath As String = String.Empty

        If String.IsNullOrEmpty(mMovie.Filename) Then Return efPath

        '*************** XBMC Frodo & Eden settings ***************
        If Master.eSettings.UseFrodo OrElse Master.eSettings.UseEden Then
            If Master.eSettings.ExtrafanartsFrodo OrElse Master.eSettings.ExtrafanartsEden Then
                If FileUtils.Common.isVideoTS(mMovie.Filename) Then
                    tPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "extrafanart")
                ElseIf FileUtils.Common.isBDRip(mMovie.Filename) Then
                    tPath = Path.Combine(Directory.GetParent(Directory.GetParent(mMovie.Filename).FullName).FullName, "extrafanart")
                Else
                    tPath = Path.Combine(Directory.GetParent(mMovie.Filename).FullName, "extrafanart")
                End If

                If Not tPath = String.Empty Then
                    If Not Directory.Exists(tPath) Then
                        Directory.CreateDirectory(tPath)
                    End If
                    efPath = Path.Combine(tPath, sName)
                    Save(efPath)
                    Return efPath
                End If
            End If
        End If

        Return efPath
    End Function
    ''' <summary>
    ''' Determines the <c>ImageCodecInfo</c> from a given <c>ImageFormat</c>
    ''' </summary>
    ''' <param name="Format"><c>ImageFormat</c> to query</param>
    ''' <returns><c>ImageCodecInfo</c> that matches the image format supplied in the parameter</returns>
    ''' <remarks></remarks>
    Private Shared Function GetEncoderInfo(ByVal Format As ImageFormat) As ImageCodecInfo
        Dim Encoders() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()

        For i As Integer = 0 To UBound(Encoders)
            If Encoders(i).FormatID = Format.Guid Then
                Return Encoders(i)
            End If
        Next

        Return Nothing
    End Function
    ''' <summary>
    ''' Select the single most preferred Poster image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available posters</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredPoster(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.poster_names Where (aD.index = Master.eSettings.PreferredPosterSize)
        If aDesc.Count = 0 Then Return False

        Dim x = From MI As MediaContainers.Image In ImageList Where (MI.Description = aDesc(0).description)
        If x.Count > 0 Then
            imgResult = x(0)
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' Select the single most preferred Fanart image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available fanart</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredFanart(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.backdrop_names Where (aD.index = Master.eSettings.PreferredFanartSize)
        If aDesc.Count = 0 Then Return False

        Dim x = From MI As MediaContainers.Image In ImageList Where (MI.Description = aDesc(0).description)
        If x.Count > 0 Then
            imgResult = x(0)
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' Fetch a list of preferred extrathumbs
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extrathumbs</param>
    ''' <returns><c>List</c> of image URLs that fit the preferred thumb size</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredEThumbs(ByRef ImageList As List(Of MediaContainers.Image)) As List(Of String)
        Dim imgList As New List(Of String)
        If ImageList.Count = 0 Then Return imgList

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.backdrop_names Where (aD.index = Master.eSettings.PreferredEThumbsSize)
        If aDesc.Count = 0 Then Return imgList

        Dim x = From MI As MediaContainers.Image In ImageList Where (MI.Description = aDesc(0).description)
        If x.Count > 0 Then
            For Each Image As MediaContainers.Image In x.ToArray
                imgList.Add(Image.URL)
            Next
        End If
        Return imgList
    End Function
    ''' <summary>
    ''' Fetch a list of preferred extraFanart
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extraFanart</param>
    ''' <returns><c>List</c> of image URLs that fit the preferred thumb size</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredEFanarts(ByRef ImageList As List(Of MediaContainers.Image)) As List(Of String)
        Dim imgList As New List(Of String)
        If ImageList.Count = 0 Then Return imgList

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.backdrop_names Where (aD.index = Master.eSettings.PreferredEFanartsSize)
        If aDesc.Count = 0 Then Return imgList

        Dim x = From MI As MediaContainers.Image In ImageList Where (MI.Description = aDesc(0).description)
        If x.Count > 0 Then
            For Each Image As MediaContainers.Image In x.ToArray
                imgList.Add(Image.URL)
            Next
        End If
        Return imgList
    End Function
#End Region 'Methods

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
                If _ms IsNot Nothing Then
                    _ms.Flush()
                    _ms.Close()
                    _ms.Dispose()
                End If
                If _image IsNot Nothing Then _image.Dispose()
                If sHTTP IsNot Nothing Then sHTTP.Dispose()
            End If

            ' free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' set large fields to null.
            _ms = Nothing
            _image = Nothing
            sHTTP = Nothing
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class