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
Imports NLog


<Serializable()> _
Public Class Images
    Implements IDisposable
    '2013/11/28 Dekker500 - This class needs some serious love. Clear candidate for polymorphism. Images should be root, with posters, fanart, etc as children. None of this if/else stuff to confuse the issue. I will re-visit later

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

	Private _ms As MemoryStream
    Private Ret As Byte()
    <NonSerialized()> _
    Private sHTTP As HTTP
	Private _image As Image
    Private _isedit As Boolean

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
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
            If Not _ms Is Nothing Then _ms.Dispose()
            _ms = New MemoryStream()
            nImage.Save(_ms, ICI, EncPars)
            _ms.Flush()

            'Replace the existing image with the new image
            If Not _image Is Nothing Then _image.Dispose()
            _image = New Bitmap(_ms)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
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
    '		logger.Error(GetType(Images),ex.Message, ex.StackTrace, "Error")
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
    '        logger.Error(GetType(Images),ex.Message, ex.StackTrace, "Error")
    '        Return Enums.PosterSize.Small
    '    End Try        
    'End Function

    ''' <summary>
    ''' Reset this instance to a pristine condition
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        'Out with the old...
        If _ms IsNot Nothing Or _image IsNot Nothing Then
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
    Public Shared Sub Delete(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) Then
            Try
                File.Delete(sPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Param: <" & sPath & ">", ex)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete the AllSeasons banner for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVASBanner(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.ModifierType.AllSeasonsBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the AllSeasons posters for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVASFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.ModifierType.AllSeasonsFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the AllSeasons landscape for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVASLandscape(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.ModifierType.AllSeasonsLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the AllSeasons posters for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVASPoster(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.ModifierType.AllSeasonsPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the episode actorthumbs for the given episode/DBTV
    ''' </summary>
    ''' <param name="DBTVEpisode">Episode database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVEpisodeActorThumbs(ByVal DBTVEpisode As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVEpisode.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVEpisode(DBTVEpisode.Filename, Enums.ModifierType.EpisodeActorThumbs)
                Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                If Directory.Exists(tmpPath) Then
                    FileUtils.Delete.DeleteDirectory(tmpPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVEpisode.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the episode fanart for the given episode/DBTV
    ''' </summary>
    ''' <param name="DBTVEpisode">Episode database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVEpisodeFanart(ByVal DBTVEpisode As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVEpisode.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVEpisode(DBTVEpisode.Filename, Enums.ModifierType.EpisodeFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVEpisode.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the episode poster for the given episode/DBTV
    ''' </summary>
    ''' <param name="DBTVEpisode">Episode database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVEpisodePoster(ByVal DBTVEpisode As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVEpisode.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVEpisode(DBTVEpisode.Filename, Enums.ModifierType.EpisodePoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVEpisode.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie actorthumbs
    ''' </summary>
    ''' <param name="DBMovie">Movie database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieActorThumbs(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainActorThumbs)
                Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                If Directory.Exists(tmpPath) Then
                    FileUtils.Delete.DeleteDirectory(tmpPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's banner
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieBanner(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's ClearArt
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieClearArt(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainClearArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's ClearLogo
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieClearLogo(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainClearLogo)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's DiscArt
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieDiscArt(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainDiscArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie Extrafanarts
    ''' </summary>
    ''' <param name="DBMovie">Movie database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieEFanarts(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainEFanarts)
                If Directory.Exists(a) Then
                    FileUtils.Delete.DeleteDirectory(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie Extrathumbs
    ''' </summary>
    ''' <param name="DBMovie">Movie database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieEThumbs(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainEThumbs)
                If Directory.Exists(a) Then
                    FileUtils.Delete.DeleteDirectory(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's fanart
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieFanart(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's landscape
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieLandscape(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's posters
    ''' </summary>
    ''' <param name="DBMovie">Structures.DBMovie representing the movie to be worked on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMoviePoster(ByVal DBMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie.Filename, DBMovie.IsSingle, Enums.ModifierType.MainPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's banner
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetBanner(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's ClearArt
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetClearArt(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainClearArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's ClearLogo
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetClearLogo(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainClearLogo)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's DiscArt
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetDiscArt(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainDiscArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's fanart
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetFanart(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's landscape
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetLandscape(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's poster
    ''' </summary>
    ''' <param name="DBMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieSetPoster(ByVal DBMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet.MovieSet.Title, Enums.ModifierType.MainPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season banner
    ''' </summary>
    ''' <param name="DBTVSeason"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVSeasonBanner(ByVal DBTVSeason As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVSeason.ShowPath) Then Return

        Try
            Dim Season As Integer = DBTVSeason.TVSeason.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(DBTVSeason.ShowPath, DBTVSeason.TVSeason.Season)
            Dim ShowPath As String = DBTVSeason.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", DBTVSeason.ShowID, " AND Season = ", DBTVSeason.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVSeason.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season fanart
    ''' </summary>
    ''' <param name="DBTVSeason"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVSeasonFanart(ByVal DBTVSeason As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVSeason.ShowPath) Then Return

        Try
            Dim Season As Integer = DBTVSeason.TVSeason.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(DBTVSeason.ShowPath, DBTVSeason.TVSeason.Season)
            Dim ShowPath As String = DBTVSeason.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", DBTVSeason.ShowID, " AND Season = ", DBTVSeason.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVSeason.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season landscape
    ''' </summary>
    ''' <param name="DBTVSeason"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVSeasonLandscape(ByVal DBTVSeason As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVSeason.ShowPath) Then Return

        Try
            Dim Season As Integer = DBTVSeason.TVSeason.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(DBTVSeason.ShowPath, DBTVSeason.TVSeason.Season)
            Dim ShowPath As String = DBTVSeason.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", DBTVSeason.ShowID, " AND Season = ", DBTVSeason.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVSeason.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season posters
    ''' </summary>
    ''' <param name="DBTVSeason"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVSeasonPoster(ByVal DBTVSeason As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVSeason.ShowPath) Then Return

        Try
            Dim Season As Integer = DBTVSeason.TVSeason.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(DBTVSeason.ShowPath, DBTVSeason.TVSeason.Season)
            Dim ShowPath As String = DBTVSeason.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", DBTVSeason.ShowID, " AND Season = ", DBTVSeason.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVSeason.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the tv show actorthumbs for the given episode/DBTV
    ''' </summary>
    ''' <param name="DBTVShow">TV Show database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowActorThumbs(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainActorThumbs)
                Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                If Directory.Exists(tmpPath) Then
                    FileUtils.Delete.DeleteDirectory(tmpPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's banner
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowBanner(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's CharacterArt
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowCharacterArt(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainCharacterArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's ClearArt
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowClearArt(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainClearArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's ClearLogo
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowClearLogo(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainClearLogo)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the tv show Extrafanarts
    ''' </summary>
    ''' <param name="DBTVShow">TV Show database record</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowEFanarts(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainEFanarts)
                If Directory.Exists(a) Then
                    FileUtils.Delete.DeleteDirectory(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's Fanart
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowFanart(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's landscape
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowLandscape(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's posters
    ''' </summary>
    ''' <param name="DBTVShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowPoster(ByVal DBTVShow As Structures.DBTV)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow.ShowPath, Enums.ModifierType.MainPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Loads this Image from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the image file</param>
    ''' <remarks></remarks>
    Public Sub FromFile(ByVal sPath As String)
        If Me._ms IsNot Nothing Then
            Me._ms.Dispose()
        End If
        If Me._image IsNot Nothing Then
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
                logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sPath & ">", ex)
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
            While sHTTP.IsDownloading
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If sHTTP.Image IsNot Nothing Then
                If Me._ms IsNot Nothing Then
                    Me._ms.Dispose()
                End If
                Me._ms = New MemoryStream()

                Dim retSave() As Byte
                retSave = sHTTP.ms.ToArray
                Me._ms.Write(retSave, 0, retSave.Length)

                'I do not copy from the _ms as it could not be a JPG
                _image = New Bitmap(sHTTP.Image)

                ' if is not a JPG or PNG we have to convert the memory stream to JPG format
                If Not (sHTTP.isJPG OrElse sHTTP.isPNG) Then
                    UpdateMSfromImg(New Bitmap(_image))
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sURL & ">", ex)
        End Try
    End Sub

    Public Function IsAllowedToDownload(ByVal mMovie As Structures.DBMovie, ByVal fType As Enums.ImageType_Movie, Optional ByVal isChange As Boolean = False) As Boolean
        With Master.eSettings
            Select Case fType
                Case Enums.ImageType_Movie.Banner
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.BannerPath) OrElse .MovieBannerOverwrite) AndAlso .MovieBannerAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.ClearArt
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.ClearArtPath) OrElse .MovieClearArtOverwrite) AndAlso .MovieClearArtAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.ClearLogo
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.ClearLogoPath) OrElse .MovieClearLogoOverwrite) AndAlso .MovieClearLogoAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.DiscArt
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.DiscArtPath) OrElse .MovieDiscArtOverwrite) AndAlso .MovieDiscArtAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.EFanarts
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.EFanartsPath) OrElse .MovieEFanartsOverwrite) AndAlso .MovieEFanartsAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.EThumbs
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.EThumbsPath) OrElse .MovieEThumbsOverwrite) AndAlso .MovieEThumbsAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.Fanart
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.FanartPath) OrElse .MovieFanartOverwrite) AndAlso .MovieFanartAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.Landscape
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.LandscapePath) OrElse .MovieLandscapeOverwrite) AndAlso .MovieLandscapeAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.Poster
                    If isChange OrElse (String.IsNullOrEmpty(mMovie.PosterPath) OrElse .MoviePosterOverwrite) AndAlso .MoviePosterAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
        End With
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mMovieSet"></param>
    ''' <param name="fType"></param>
    ''' <param name="isChange"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsAllowedToDownload(ByVal mMovieSet As Structures.DBMovieSet, ByVal fType As Enums.ImageType_Movie, Optional ByVal isChange As Boolean = False) As Boolean
        With Master.eSettings
            Select Case fType
                Case Enums.ImageType_Movie.Banner
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.BannerPath) OrElse .MovieSetBannerOverwrite) AndAlso .MovieSetBannerAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.ClearArt
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.ClearArtPath) OrElse .MovieSetClearArtOverwrite) AndAlso .MovieSetClearArtAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.ClearLogo
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.ClearLogoPath) OrElse .MovieSetClearLogoOverwrite) AndAlso .MovieSetClearLogoAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.DiscArt
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.DiscArtPath) OrElse .MovieSetDiscArtOverwrite) AndAlso .MovieSetDiscArtAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.Fanart
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.FanartPath) OrElse .MovieSetFanartOverwrite) AndAlso .MovieSetFanartAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.Landscape
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.LandscapePath) OrElse .MovieSetLandscapeOverwrite) AndAlso .MovieSetLandscapeAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ImageType_Movie.Poster
                    If (isChange OrElse (String.IsNullOrEmpty(mMovieSet.PosterPath) OrElse .MovieSetPosterOverwrite) AndAlso .MovieSetPosterAnyEnabled) Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
        End With
    End Function

    Public Function IsAllowedToDownload(ByVal mTV As Structures.DBTV, ByVal fType As Enums.ModifierType, Optional ByVal isChange As Boolean = False) As Boolean
        With Master.eSettings
            Select Case fType
                Case Enums.ModifierType.MainBanner
                    If isChange OrElse (String.IsNullOrEmpty(mTV.BannerPath) OrElse .TVShowBannerOverwrite) AndAlso .MovieBannerAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainCharacterArt
                    If isChange OrElse (String.IsNullOrEmpty(mTV.CharacterArtPath) OrElse .TVShowCharacterArtOverwrite) AndAlso .TVShowCharacterArtAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainClearArt
                    If isChange OrElse (String.IsNullOrEmpty(mTV.ClearArtPath) OrElse .TVShowClearArtOverwrite) AndAlso .TVShowClearArtAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainClearLogo
                    If isChange OrElse (String.IsNullOrEmpty(mTV.ClearLogoPath) OrElse .TVShowClearLogoOverwrite) AndAlso .TVShowClearLogoAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainEFanarts
                    If isChange OrElse (String.IsNullOrEmpty(mTV.EFanartsPath) OrElse .TVShowEFanartsOverwrite) AndAlso .TVShowEFanartsAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainFanart
                    If isChange OrElse (String.IsNullOrEmpty(mTV.FanartPath) OrElse .TVShowFanartOverwrite) AndAlso .TVShowFanartAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainLandscape
                    If isChange OrElse (String.IsNullOrEmpty(mTV.LandscapePath) OrElse .TVShowLandscapeOverwrite) AndAlso .TVShowLandscapeAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
                Case Enums.ModifierType.MainPoster
                    If isChange OrElse (String.IsNullOrEmpty(mTV.PosterPath) OrElse .TVShowPosterOverwrite) AndAlso .TVShowPosterAnyEnabled Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
        End With
    End Function

    Public Sub ResizeExtraFanart(ByVal fromPath As String, ByVal toPath As String)
        Me.FromFile(fromPath)
        Me.Save(toPath)
    End Sub

    Public Sub ResizeExtraThumb(ByVal fromPath As String, ByVal toPath As String)
        Me.FromFile(fromPath)
        Me.Save(toPath)
    End Sub
    ''' <summary>
    ''' Stores the Image to the supplied <paramref name="sPath"/>
    ''' </summary>
    ''' <param name="sPath">Location to store the image</param>
    ''' <param name="iQuality"><c>Integer</c> value representing <c>Encoder.Quality</c>. 0 is lowest quality, 100 is highest</param>
    ''' <param name="sUrl">URL of desired image</param>
    ''' <param name="doResize"></param>
    ''' <remarks></remarks>
    Public Sub Save(ByVal sPath As String, Optional ByVal sUrl As String = "")
        '2013/11/26 Dekker500 - This method is a swiss army knife. Completely different behaviour based on what parameter is supplied. Break it down a bit for a more logical flow (if I set a path and URL and quality but no resize, it'll happily ignore everything but the path)
        Dim retSave() As Byte
        Try
            If String.IsNullOrEmpty(sUrl) Then
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

            If _image Is Nothing Then Exit Sub

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
                        Dim EncPars As EncoderParameters = New EncoderParameters(1)

                        EncPars.Param(0) = New EncoderParameter(Encoder.RenderMethod, EncoderValue.RenderNonProgressive)

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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <param name="fpath"><c>String</c> representing the movie path</param>
    ''' <param name="aMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieActorThumb(ByVal actor As MediaContainers.Person, ByVal fpath As String, ByVal aMovie As Structures.DBMovie) As String
        'TODO 2013/11/26 Dekker500 - This should be re-factored to remove the fPath argument. All callers pass the same string derived from the provided DBMovie, so why do it twice?
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.Movie(aMovie.Filename, aMovie.IsSingle, Enums.ModifierType.MainActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.MovieActorThumbsOverwrite) Then
                Save(tPath)
            End If
        Next

        Return tPath
    End Function
    ''' <summary>
    ''' Save the image as a movie banner
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieBanner(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MovieBannerResize AndAlso (_image.Width > Master.eSettings.MovieBannerWidth OrElse _image.Height > Master.eSettings.MovieBannerHeight)

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnBannerSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieBannerWidth, Master.eSettings.MovieBannerHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieBannerOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movie ClearArt
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieClearArt(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnClearArtSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainClearArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieClearArtOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movie ClearLogo
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieClearLogo(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnClearLogoSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainClearLogo)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieClearLogoOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movie landscape
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieDiscArt(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnDiscArtSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainDiscArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieDiscArtOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrafanart
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sName"><c>String</c> name of the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieExtrafanart(ByVal mMovie As Structures.DBMovie, ByVal sName As String, Optional sURL As String = "") As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mMovie.Filename) Then Return efPath

        Dim doResize As Boolean = Master.eSettings.MovieEFanartsResize AndAlso (_image.Width > Master.eSettings.MovieEFanartsWidth OrElse _image.Height > Master.eSettings.MovieEFanartsHeight)

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieEFanartsWidth, Master.eSettings.MovieEFanartsHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainEFanarts)
                If Not a = String.Empty Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    efPath = Path.Combine(a, sName)
                    Save(efPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return efPath
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrathumb
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieExtrathumb(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim etPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mMovie.Filename) Then Return etPath

        Dim doResize As Boolean = Master.eSettings.MovieEThumbsResize AndAlso (_image.Width > Master.eSettings.MovieEThumbsWidth OrElse _image.Height > Master.eSettings.MovieEThumbsHeight)

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieEThumbsWidth, Master.eSettings.MovieEThumbsHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainEThumbs)
                If Not a = String.Empty Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    iMod = Functions.GetExtraModifier(a)
                    iVal = iMod + 1
                    etPath = Path.Combine(a, String.Concat("thumb", iVal, ".jpg"))
                    Save(etPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return etPath
    End Function
    ''' <summary>
    ''' Save the image as a movie fanart
    ''' </summary>
    ''' <param name="mMovie"><c></c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieFanart(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MovieFanartResize AndAlso (_image.Width > Master.eSettings.MovieFanartWidth OrElse _image.Height > Master.eSettings.MovieFanartHeight)

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnFanartSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieFanartWidth, Master.eSettings.MovieFanartHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieFanartOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

            If Master.eSettings.MovieBackdropsAuto AndAlso Directory.Exists(Master.eSettings.MovieBackdropsPath) Then
                Save(String.Concat(Master.eSettings.MovieBackdropsPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), sURL)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movie landscape
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieLandscape(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnLandscapeSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieLandscapeOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsMoviePoster(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MoviePosterResize AndAlso (_image.Width > Master.eSettings.MoviePosterWidth OrElse _image.Height > Master.eSettings.MoviePosterHeight)

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnPosterSave_Movie, params, _image, False)
            Catch ex As Exception
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MoviePosterWidth, Master.eSettings.MoviePosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModifierType.MainPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MoviePosterOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset banner
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetBanner(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MovieSetBannerResize AndAlso (_image.Width > Master.eSettings.MovieSetBannerWidth OrElse _image.Height > Master.eSettings.MovieSetBannerHeight)

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieSetBannerWidth, Master.eSettings.MovieSetBannerHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetBannerOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset ClearArt
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetClearArt(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainClearArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetClearArtOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset ClearLogo
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetClearLogo(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainClearLogo)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetClearLogoOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset DiscArt
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetDiscArt(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainDiscArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetDiscArtOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset Fanart
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetFanart(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MovieSetFanartResize AndAlso (_image.Width > Master.eSettings.MovieSetFanartWidth OrElse _image.Height > Master.eSettings.MovieSetFanartHeight)

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieSetFanartWidth, Master.eSettings.MovieSetFanartHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetFanartOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset Landscape
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetLandscape(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetLandscapeOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a movieset Poster
    ''' </summary>
    ''' <param name="mMovieSet"><c>Structures.DBMovieSet</c> representing the movieset being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieSetPoster(ByVal mMovieSet As Structures.DBMovieSet, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MovieSetPosterResize AndAlso (_image.Width > Master.eSettings.MovieSetPosterWidth OrElse _image.Height > Master.eSettings.MovieSetPosterHeight)

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieSetPosterWidth, Master.eSettings.MovieSetPosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.MovieSet.Title, Enums.ModifierType.MainPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetPosterOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Saves the image as the AllSeason banner
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVASBanner(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVASBannerResize AndAlso (_image.Width > Master.eSettings.TVASBannerWidth OrElse _image.Height > Master.eSettings.TVASBannerHeight)

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVASBannerWidth, Master.eSettings.TVASBannerHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.AllSeasonsBanner, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASBannerOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASBannerOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return strReturn
    End Function
    ''' <summary>
    ''' Saves the image as the AllSeason fanart
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVASFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVASFanartResize AndAlso (_image.Width > Master.eSettings.TVASFanartWidth OrElse _image.Height > Master.eSettings.TVASFanartHeight)

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVASFanartWidth, Master.eSettings.TVASFanartHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.AllSeasonsFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASFanartOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASFanartOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return strReturn
    End Function
    ''' <summary>
    ''' Saves the image as the AllSeason landscape
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVASLandscape(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.AllSeasonsLandscape, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASLandscapeOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASLandscapeOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return strReturn
    End Function
    ''' <summary>
    ''' Saves the image as the AllSeason poster
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVASPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVASPosterResize AndAlso (_image.Width > Master.eSettings.TVASPosterWidth OrElse _image.Height > Master.eSettings.TVASPosterHeight)

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVASPosterWidth, Master.eSettings.TVASPosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.AllSeasonsPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASPosterOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASPosterOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the episode being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVEpisodeActorThumb(ByVal actor As MediaContainers.Person, ByVal mShow As Structures.DBTV) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.TVEpisode(mShow.Filename, Enums.ModifierType.EpisodeActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.TVEpisodeActorThumbsOverwrite) Then
                Save(tPath)
            End If
        Next

        Return tPath
    End Function
    ''' <summary>
    ''' Saves the image as the episode fanart
    ''' </summary>
    ''' <param name="mShow">The <c>Structures.DBTV</c> representing the show being referenced</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVEpisodeFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        If String.IsNullOrEmpty(mShow.Filename) Then Return String.Empty

        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVEpisodeFanartResize AndAlso (_image.Width > Master.eSettings.TVEpisodeFanartWidth OrElse _image.Height > Master.eSettings.TVEpisodeFanartHeight)

        Try
            Dim EpisodePath As String = mShow.Filename

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVEpisodeFanartWidth, Master.eSettings.TVEpisodeFanartHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.EpisodeFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVEpisodeFanartOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.ModifierType.EpisodeFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVEpisodeFanartOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsTVEpisodePoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        If String.IsNullOrEmpty(mShow.Filename) Then Return String.Empty

        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVEpisodePosterResize AndAlso (_image.Width > Master.eSettings.TVEpisodePosterWidth OrElse _image.Height > Master.eSettings.TVEpisodePosterHeight)

        Try
            Dim EpisodePath As String = mShow.Filename

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVEpisodePosterWidth, Master.eSettings.TVEpisodePosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.EpisodePoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVEpisodePosterOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.ModifierType.EpisodePoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVEpisodePosterOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's season banner
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVSeasonBanner(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVSeasonBannerResize AndAlso (_image.Width > Master.eSettings.TVSeasonBannerWidth OrElse _image.Height > Master.eSettings.TVSeasonBannerHeight)

        Try
            Dim Season As Integer = mShow.TVSeason.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVSeason.Season)
            Dim ShowPath As String = mShow.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", mShow.ShowID, " AND Season = ", mShow.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVSeasonBannerWidth, Master.eSettings.TVSeasonBannerHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.SeasonBanner, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonBannerOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonBannerOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as the TV Show's season fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVSeasonFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVSeasonFanartResize AndAlso (_image.Width > Master.eSettings.TVSeasonFanartWidth OrElse _image.Height > Master.eSettings.TVSeasonFanartHeight)

        Try
            Dim Season As Integer = mShow.TVSeason.Season
            Dim SeasonFirstEpisodePath As String = String.Empty
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVSeason.Season)
            Dim ShowPath As String = mShow.ShowPath

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", mShow.ShowID, " AND Season = ", mShow.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVSeasonFanartWidth, Master.eSettings.TVSeasonFanartHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If
            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.SeasonFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonFanartOverwrite) Then

                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonFanartOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's season landscape
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVSeasonLandscape(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Try
            Dim pPath As String = String.Empty
            Dim Season As Integer = mShow.TVSeason.Season
            Dim SeasonFirstEpisodePath As String = String.Empty
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVSeason.Season)
            Dim ShowPath As String = mShow.ShowPath

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", mShow.ShowID, " AND Season = ", mShow.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.SeasonLandscape, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonLandscapeOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonLandscapeOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsTVSeasonPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVSeasonPosterResize AndAlso (_image.Width > Master.eSettings.TVSeasonPosterWidth OrElse _image.Height > Master.eSettings.TVSeasonPosterHeight)

        Try
            Dim Season As Integer = mShow.TVSeason.Season
            Dim SeasonFirstEpisodePath As String = String.Empty
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVSeason.Season)
            Dim ShowPath As String = mShow.ShowPath

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", mShow.ShowID, " AND Season = ", mShow.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVSeasonPosterWidth, Master.eSettings.TVSeasonPosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.SeasonPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonPosterOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonPosterOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the show being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowActorThumb(ByVal actor As MediaContainers.Person, ByVal mShow As Structures.DBTV) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.ModifierType.MainActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.TVShowActorThumbsOverwrite) Then
                Save(tPath)
            End If
        Next

        Return tPath
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's banner
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowBanner(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Dim doResize As Boolean = Master.eSettings.TVShowBannerResize AndAlso (_image.Width > Master.eSettings.TVShowBannerWidth OrElse _image.Height > Master.eSettings.TVShowBannerHeight)

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVShowBannerWidth, Master.eSettings.TVShowBannerHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowBanner, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowBannerOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowBannerOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's CharacterArt
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowCharacterArt(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowCharacterArt, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowCharacterArtOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainCharacterArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowCharacterArtOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's ClearArt
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowClearArt(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowClearArt, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowClearArtOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainClearArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowClearArtOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's ClearLogo
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowClearLogo(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowClearLogo, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowClearLogoOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainClearLogo)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowClearLogoOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a tv show's extrafanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sName"><c>String</c> name of the movie being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowExtrafanart(ByVal mShow As Structures.DBTV, ByVal sName As String, Optional sURL As String = "") As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return efPath

        Dim doResize As Boolean = Master.eSettings.TVShowEFanartsResize AndAlso (_image.Width > Master.eSettings.TVShowEFanartsWidth OrElse _image.Height > Master.eSettings.TVShowEFanartsHeight)

        Try
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVShowEFanartsWidth, Master.eSettings.TVShowEFanartsHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainEFanarts)
                If Not a = String.Empty Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    efPath = Path.Combine(a, sName)
                    Save(efPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return efPath
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Dim doResize As Boolean = Master.eSettings.TVShowFanartResize AndAlso (_image.Width > Master.eSettings.TVShowFanartWidth OrElse _image.Height > Master.eSettings.TVShowFanartHeight)

        Try
            Dim tPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVShowFanartWidth, Master.eSettings.TVShowFanartHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowFanartOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowFanartOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TV Show's landscape
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show being referred to</param>
    ''' <param name="sURL">Optional <c>String</c> URL for the image</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowLandscape(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowLandscape, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowLandscapeOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowLandscapeOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsTVShowPoster(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return strReturn

        Dim doResize As Boolean = Master.eSettings.TVShowPosterResize AndAlso (_image.Width > Master.eSettings.TVShowPosterWidth OrElse _image.Height > Master.eSettings.TVShowPosterHeight)

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVShowPosterWidth, Master.eSettings.TVShowPosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.ImageType_TV.ShowPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowPosterOverwrite) Then
                        Save(s, sURL)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowPosterOverwrite) Then
                    Save(a, sURL)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function

    Public Shared Sub SetDefaultImages_TV(ByRef DBTV As Structures.DBTV, _
                                          ByRef DefaultImagesContainer As MediaContainers.ImagesContainer, _
                                          ByRef DefaultSeasonImagesContainer As List(Of MediaContainers.EpisodeOrSeasonImagesContainer), _
                                          ByRef DefaultEpisodeImagesContainer As List(Of MediaContainers.EpisodeOrSeasonImagesContainer), _
                                          ByRef SearchResultsContainer As MediaContainers.SearchResultsContainer_TV, _
                                          ByRef ScrapeModifier As Structures.ScrapeModifier)

        'Banner Show
        If ScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled Then
            If DBTV.ImagesContainer.Banner.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.Banner = DBTV.ImagesContainer.Banner
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowBanner(SearchResultsContainer.ShowBanners, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.Banner = defImg
                    DefaultImagesContainer.Banner = defImg
                End If
            End If
        End If

        'CharacterArt Show
        If ScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled Then
            If DBTV.ImagesContainer.CharacterArt.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.CharacterArt = DBTV.ImagesContainer.CharacterArt
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowCharacterArt(SearchResultsContainer.ShowCharacterArts, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.CharacterArt = defImg
                    DefaultImagesContainer.CharacterArt = defImg
                End If
            End If
        End If

        'ClearArt Show
        If ScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled Then
            If DBTV.ImagesContainer.ClearArt.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.ClearArt = DBTV.ImagesContainer.ClearArt
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowClearArt(SearchResultsContainer.ShowClearArts, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.ClearArt = defImg
                    DefaultImagesContainer.ClearArt = defImg
                End If
            End If
        End If

        'ClearLogo Show
        If ScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled Then
            If DBTV.ImagesContainer.ClearLogo.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.ClearLogo = DBTV.ImagesContainer.ClearLogo
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowClearLogo(SearchResultsContainer.ShowClearLogos, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.ClearLogo = defImg
                    DefaultImagesContainer.ClearLogo = defImg
                End If
            End If
        End If

        'Fanart Show
        If (ScrapeModifier.EpisodeFanart OrElse ScrapeModifier.MainFanart) Then
            If DBTV.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.Fanart = DBTV.ImagesContainer.Fanart
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowFanart(SearchResultsContainer.ShowFanarts, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.Fanart = defImg
                    DefaultImagesContainer.Fanart = defImg
                End If
            End If
        End If

        'Landscape Show
        If ScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled Then
            If DBTV.ImagesContainer.Landscape.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.Landscape = DBTV.ImagesContainer.Landscape
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowLandscape(SearchResultsContainer.ShowLandscapes, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.Landscape = defImg
                    DefaultImagesContainer.Landscape = defImg
                End If
            End If
        End If

        'Poster Show
        If ScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled Then
            If DBTV.ImagesContainer.Poster.WebImage.Image IsNot Nothing Then
                DefaultImagesContainer.Poster = DBTV.ImagesContainer.Poster
            Else
                Dim defImg As MediaContainers.Image = Nothing
                Images.GetPreferredTVShowPoster(SearchResultsContainer.ShowPosters, defImg)

                If defImg IsNot Nothing Then
                    DBTV.ImagesContainer.Poster = defImg
                    DefaultImagesContainer.Poster = defImg
                End If
            End If
        End If

        'Season / AllSeasons Banner/Fanart/Poster
        If (ScrapeModifier.SeasonBanner OrElse ScrapeModifier.SeasonFanart _
            OrElse ScrapeModifier.SeasonLandscape OrElse ScrapeModifier.SeasonPoster) AndAlso DBTV.Seasons IsNot Nothing Then
            For Each cSeason As Structures.DBTV In DBTV.Seasons.OrderBy(Function(f) f.TVSeason.Season)
                Dim iSeason As Integer = cSeason.TVSeason.Season
                Dim SIC As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Season = iSeason}

                If iSeason = 999 Then
                    'Banner AllSeasons 
                    If ScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVASBannerAnyEnabled Then
                        If cSeason.ImagesContainer.Banner.WebImage.Image IsNot Nothing Then
                            SIC.Banner = cSeason.ImagesContainer.Banner
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVASBanner(SearchResultsContainer.SeasonBanners, SearchResultsContainer.ShowBanners, defImg)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Banner = defImg
                                SIC.Banner = defImg
                            End If
                        End If
                    End If
                Else
                    'Banner Season
                    If ScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled Then
                        If cSeason.ImagesContainer.Banner.WebImage.Image IsNot Nothing Then
                            SIC.Banner = cSeason.ImagesContainer.Banner
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVSeasonBanner(SearchResultsContainer.SeasonBanners, defImg, iSeason)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Banner = defImg
                                SIC.Banner = defImg
                            End If
                        End If
                    End If
                End If

                If iSeason = 999 Then
                    'Fanart AllSeasons
                    If ScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVASFanartAnyEnabled Then
                        If cSeason.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                            SIC.Fanart = cSeason.ImagesContainer.Fanart
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVASFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.ShowFanarts, defImg)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Fanart = defImg
                                SIC.Fanart = defImg
                            End If
                        End If
                    End If
                Else
                    'Fanart Season
                    If ScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled Then
                        If cSeason.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                            SIC.Fanart = cSeason.ImagesContainer.Fanart
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVSeasonFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.ShowFanarts, defImg, iSeason)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Fanart = defImg
                                SIC.Fanart = defImg
                            End If
                        End If
                    End If
                End If

                If iSeason = 999 Then
                    'Landscape AllSeasons
                    If ScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVASLandscapeAnyEnabled Then
                        If cSeason.ImagesContainer.Landscape.WebImage.Image IsNot Nothing Then
                            SIC.Landscape = cSeason.ImagesContainer.Landscape
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVASLandscape(SearchResultsContainer.SeasonLandscapes, SearchResultsContainer.ShowLandscapes, defImg)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Landscape = defImg
                                SIC.Landscape = defImg
                            End If
                        End If
                    End If
                Else
                    ' Landscape Season
                    If ScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled Then
                        If cSeason.ImagesContainer.Landscape.WebImage.Image IsNot Nothing Then
                            SIC.Landscape = cSeason.ImagesContainer.Landscape
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVSeasonLandscape(SearchResultsContainer.SeasonLandscapes, defImg, iSeason)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Landscape = defImg
                                SIC.Landscape = defImg
                            End If
                        End If
                    End If
                End If

                If iSeason = 999 Then
                    'Poster AllSeasons
                    If ScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVASPosterAnyEnabled Then
                        If cSeason.ImagesContainer.Poster.WebImage.Image IsNot Nothing Then
                            SIC.Poster = cSeason.ImagesContainer.Poster
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVASPoster(SearchResultsContainer.SeasonPosters, SearchResultsContainer.ShowPosters, defImg)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Poster = defImg
                                SIC.Poster = defImg
                            End If
                        End If
                    End If
                Else
                    'Poster Season
                    If ScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled Then
                        If cSeason.ImagesContainer.Poster.WebImage.Image IsNot Nothing Then
                            SIC.Poster = cSeason.ImagesContainer.Poster
                        Else
                            Dim defImg As MediaContainers.Image = Nothing
                            Images.GetPreferredTVSeasonPoster(SearchResultsContainer.SeasonPosters, defImg, iSeason)

                            If defImg IsNot Nothing Then
                                cSeason.ImagesContainer.Poster = defImg
                                SIC.Poster = defImg
                            End If
                        End If
                    End If
                End If
                DefaultSeasonImagesContainer.Add(SIC)
            Next
        End If

        'Episode Fanart/Poster
        If (ScrapeModifier.EpisodeFanart OrElse ScrapeModifier.EpisodePoster) AndAlso DBTV.Episodes IsNot Nothing Then
            For Each cEpisode As Structures.DBTV In DBTV.Episodes
                Dim iEpisode As Integer = cEpisode.TVEp.Episode
                Dim iSeason As Integer = cEpisode.TVEp.Season
                Dim EIC As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Episode = iEpisode, .Season = iSeason}

                'Fanart Episode
                If ScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled Then
                    If cEpisode.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                        EIC.Fanart = cEpisode.ImagesContainer.Fanart
                    Else
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVEpisodeFanart(SearchResultsContainer.EpisodeFanarts, SearchResultsContainer.ShowFanarts, defImg, iEpisode, iSeason)

                        If defImg IsNot Nothing Then
                            cEpisode.ImagesContainer.Fanart = defImg
                            EIC.Fanart = defImg
                        End If
                    End If
                End If

                'Poster Episode
                If ScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled Then
                    If cEpisode.ImagesContainer.Fanart.WebImage.Image IsNot Nothing Then
                        EIC.Poster = cEpisode.ImagesContainer.Poster
                    Else
                        Dim defImg As MediaContainers.Image = Nothing
                        Images.GetPreferredTVEpisodePoster(SearchResultsContainer.EpisodePosters, defImg, iSeason, iEpisode)

                        If defImg IsNot Nothing Then
                            cEpisode.ImagesContainer.Poster = defImg
                            EIC.Poster = defImg
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' Determines the <c>ImageCodecInfo</c> from a given <c>ImageFormat</c>
    ''' </summary>
    ''' <param name="Format"><c>ImageFormat</c> to query</param>
    ''' <returns><c>ImageCodecInfo</c> that matches the image format supplied in the parameter</returns>
    ''' <remarks></remarks>
    Private Shared Function GetEncoderInfo(ByVal Format As ImageFormat) As ImageCodecInfo
        Dim Encoders() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()

        For i As Integer = 0 To Encoders.Count - 1
            If Encoders(i).FormatID = Format.Guid Then
                Return Encoders(i)
            End If
        Next

        Return Nothing
    End Function
    ''' <summary>
    ''' Select the single most preferred Banner image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available banners</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieBanner(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False
        imgResult = Nothing

        If Master.eSettings.MovieBannerPrefSize = Enums.MovieBannerSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.MovieBannerSize = Master.eSettings.MovieBannerPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.MovieBannerPrefSizeOnly AndAlso Not Master.eSettings.MovieBannerPrefSize = Enums.MovieBannerSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Banner image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available banners</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieSetBanner(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False
        imgResult = Nothing

        If Master.eSettings.MovieSetBannerPrefSize = Enums.MovieBannerSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.MovieBannerSize = Master.eSettings.MovieSetBannerPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.MovieSetBannerPrefSizeOnly AndAlso Not Master.eSettings.MovieSetBannerPrefSize = Enums.MovieBannerSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Poster image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available posters</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMoviePoster(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False
        imgResult = Nothing

        If Master.eSettings.MoviePosterPrefSize = Enums.MoviePosterSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.MoviePosterSize = Master.eSettings.MoviePosterPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.MoviePosterPrefSizeOnly AndAlso Not Master.eSettings.MoviePosterPrefSize = Enums.MoviePosterSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Poster image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available posters</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieSetPoster(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False
        imgResult = Nothing

        If Master.eSettings.MovieSetPosterPrefSize = Enums.MoviePosterSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.MoviePosterSize = Master.eSettings.MovieSetPosterPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.MovieSetPosterPrefSizeOnly AndAlso Not Master.eSettings.MovieSetPosterPrefSize = Enums.MoviePosterSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Fanart image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available fanart</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieFanart(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False
        imgResult = Nothing

        If Master.eSettings.MovieFanartPrefSize = Enums.MovieFanartSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.MovieFanartSize = Master.eSettings.MovieFanartPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.MovieFanartPrefSizeOnly AndAlso Not Master.eSettings.MovieFanartPrefSize = Enums.MovieFanartSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Fanart image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available fanart</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieSetFanart(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False
        imgResult = Nothing

        If Master.eSettings.MovieSetFanartPrefSize = Enums.MovieFanartSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.MovieFanartSize = Master.eSettings.MovieSetFanartPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.MovieSetFanartPrefSizeOnly AndAlso Not Master.eSettings.MovieSetFanartPrefSize = Enums.MovieFanartSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Fetch a list of preferred extrathumbs
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extrathumbs</param>
    ''' <returns><c>List</c> of image URLs that fit the preferred thumb size</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieEThumbs(ByRef ImageList As List(Of MediaContainers.Image)) As List(Of String)
        Dim imgList As New List(Of String)
        If ImageList.Count = 0 Then Return imgList

        If Master.eSettings.MovieEThumbsPrefSize = Enums.MovieFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URL))
                imgList.Add(img.URL)
            Next
        ElseIf Master.eSettings.MovieEThumbsPrefSize = Enums.MovieFanartSize.Thumb Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.ThumbURL))
                imgList.Add(img.ThumbURL)
            Next
        Else
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URL) AndAlso f.MovieFanartSize = Master.eSettings.MovieEThumbsPrefSize)
                imgList.Add(img.URL)
            Next
            If Not Master.eSettings.MovieEThumbsPrefSizeOnly Then
                For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URL) AndAlso Not f.MovieFanartSize = Master.eSettings.MovieEThumbsPrefSize)
                    imgList.Add(img.URL)
                Next
            End If
        End If

        Return imgList
    End Function
    ''' <summary>
    ''' Fetch a list of preferred extraFanart
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extraFanart</param>
    ''' <returns><c>List</c> of image URLs that fit the preferred thumb size</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieEFanarts(ByRef ImageList As List(Of MediaContainers.Image)) As List(Of String)
        Dim imgList As New List(Of String)
        If ImageList.Count = 0 Then Return imgList

        If Master.eSettings.MovieEFanartsPrefSize = Enums.MovieFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URL))
                imgList.Add(img.URL)
            Next
        ElseIf Master.eSettings.MovieEFanartsPrefSize = Enums.MovieFanartSize.Thumb Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.ThumbURL))
                imgList.Add(img.ThumbURL)
            Next
        Else
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URL) AndAlso f.MovieFanartSize = Master.eSettings.MovieEFanartsPrefSize)
                imgList.Add(img.URL)
            Next
            If Not Master.eSettings.MovieEFanartsPrefSizeOnly Then
                For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URL) AndAlso Not f.MovieFanartSize = Master.eSettings.MovieEFanartsPrefSize)
                    imgList.Add(img.URL)
                Next
            End If
        End If

        Return imgList
    End Function

    Public Shared Function GetPreferredTVASBanner(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVASBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVBannerSize = Master.eSettings.TVASBannerPrefSize AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVASBannerPrefSizeOnly AndAlso Not Master.eSettings.TVASBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVASBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVBannerSize = Master.eSettings.TVASBannerPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVASBannerPrefSizeOnly AndAlso Not Master.eSettings.TVASBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVASFanart(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False


        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVASFanartPrefSize = Enums.TVPosterSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVASFanartPrefSize AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVASFanartPrefSizeOnly AndAlso Not Master.eSettings.TVASFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVASFanartPrefSize = Enums.TVPosterSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVASFanartPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVASFanartPrefSizeOnly AndAlso Not Master.eSettings.TVASFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVASLandscape(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        imgResult = SeasonImageList.Find(Function(f) f.Season = 999)

        If imgResult Is Nothing Then
            imgResult = ShowImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVASPoster(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVASPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVPosterSize = Master.eSettings.TVASPosterPrefSize AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVASPosterPrefSizeOnly AndAlso Not Master.eSettings.TVASPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVASPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVPosterSize = Master.eSettings.TVASPosterPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVASPosterPrefSizeOnly AndAlso Not Master.eSettings.TVASPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVEpisodeFanart(ByRef EpisodeImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iEpisode As Integer, ByVal iSeason As Integer) As Boolean
        If EpisodeImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        If Not EpisodeImageList.Count = 0 Then
            If Master.eSettings.TVEpisodeFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = EpisodeImageList.Find(Function(f) f.Episode = iEpisode AndAlso f.Season = iSeason)
            End If

            If imgResult Is Nothing Then
                imgResult = EpisodeImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVEpisodeFanartPrefSize AndAlso f.Episode = iEpisode AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVEpisodeFanartPrefSizeOnly AndAlso Not Master.eSettings.TVEpisodeFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = EpisodeImageList.Find(Function(f) f.Episode = iEpisode AndAlso f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVEpisodeFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVEpisodeFanartPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVEpisodeFanartPrefSizeOnly AndAlso Not Master.eSettings.TVEpisodeFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVEpisodePoster(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iSeason As Integer, ByVal iEpisode As Integer) As Boolean
        If ImageList.Count = 0 Then Return False

        If Master.eSettings.TVEpisodePosterPrefSize = Enums.TVPosterSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Episode = iEpisode AndAlso f.Season = iSeason)
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVPosterSize = Master.eSettings.TVEpisodePosterPrefSize AndAlso f.Episode = iEpisode AndAlso f.Season = iSeason)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVEpisodePosterPrefSizeOnly AndAlso Not Master.eSettings.TVEpisodePosterPrefSize = Enums.TVPosterSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Episode = iEpisode AndAlso f.Season = iSeason)
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Banner image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available banners</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredTVSeasonBanner(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iSeason As Integer) As Boolean
        If ImageList.Count = 0 Then Return False

        If Master.eSettings.TVSeasonBannerPrefSize = Enums.TVBannerSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Season = iSeason)
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVBannerSize = Master.eSettings.TVSeasonBannerPrefSize AndAlso f.Season = iSeason)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVSeasonBannerPrefSizeOnly AndAlso Not Master.eSettings.TVSeasonBannerPrefSize = Enums.TVBannerSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Season = iSeason)
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVSeasonFanart(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iSeason As Integer) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVSeasonFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = iSeason)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVSeasonFanartPrefSize AndAlso f.Season = iSeason)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVSeasonFanartPrefSizeOnly AndAlso Not Master.eSettings.TVSeasonFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = iSeason)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVSeasonFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVSeasonFanartPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVSeasonFanartPrefSizeOnly AndAlso Not Master.eSettings.TVSeasonFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVSeasonLandscape(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iSeason As Integer) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.Find(Function(f) f.Season = iSeason)

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVSeasonPoster(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iSeason As Integer) As Boolean
        If ImageList.Count = 0 Then Return False

        If Master.eSettings.TVSeasonPosterPrefSize = Enums.TVSeasonPosterSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Season = iSeason)
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVSeasonPosterSize = Master.eSettings.TVSeasonPosterPrefSize AndAlso f.Season = iSeason)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVSeasonPosterPrefSizeOnly AndAlso Not Master.eSettings.TVSeasonPosterPrefSize = Enums.TVSeasonPosterSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Season = iSeason)
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Banner image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available banners</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredTVShowBanner(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        If Master.eSettings.TVShowBannerPrefSize = Enums.TVBannerSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVBannerSize = Master.eSettings.TVShowBannerPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVShowBannerPrefSizeOnly AndAlso Not Master.eSettings.TVShowBannerPrefSize = Enums.TVBannerSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVShowCharacterArt(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVShowClearArt(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVShowClearLogo(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Fanart image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available banners</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredTVShowFanart(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        If Master.eSettings.TVShowFanartPrefSize = Enums.TVFanartSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVShowFanartPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVShowFanartPrefSizeOnly AndAlso Not Master.eSettings.TVShowFanartPrefSize = Enums.TVFanartSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVShowLandscape(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred Poster image
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available banners</param>
    ''' <param name="imgResult">Single <c>MediaContainers.Image</c>, if preferred image was found</param>
    ''' <returns><c>True</c> if a preferred image was found, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredTVShowPoster(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        If Master.eSettings.TVShowPosterPrefSize = Enums.TVPosterSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVPosterSize = Master.eSettings.TVShowPosterPrefSize)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVShowPosterPrefSizeOnly AndAlso Not Master.eSettings.TVShowPosterPrefSize = Enums.TVPosterSize.Any Then
            imgResult = ImageList.First
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
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

#End Region 'IDisposable Support

End Class