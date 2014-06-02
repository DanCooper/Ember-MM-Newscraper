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
            If Not _ms Is Nothing Then _ms.Dispose()
            _ms = New MemoryStream()
            nImage.Save(_ms, ICI, EncPars)
            _ms.Flush()

            'Replace the existing image with the new image
            If Not _image Is Nothing Then _image.Dispose()
            _image = New Bitmap(_ms)

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
    '		logger.ErrorException(GetType(Images),ex.Message, ex.StackTrace, "Error")
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
    '        logger.ErrorException(GetType(Images),ex.Message, ex.StackTrace, "Error")
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
                logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "Param: <" & sPath & ">", ex)
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
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.AllSeasonsBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
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
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.AllSeasonsFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
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
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.AllSeasonsLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the AllSeasons posters for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVASPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.AllSeasonsPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the episode fan art for the given show/DBTV
    ''' </summary>
    ''' <param name="mShow">Show database record from which the ShowPath is extracted. It is parsed from the actual show's path</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVEpisodeFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVEpisode(mShow.Filename, Enums.TVModType.EpisodeFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the show's episode posters
    ''' </summary>
    ''' <param name="mShow">Show database record from which the path is extracted. It is parsed from the individual episode path</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVEpisodePosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVEpisode(mShow.Filename, Enums.TVModType.EpisodePoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's banner
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieBanner(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Banner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's ClearArt
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieClearArt(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.ClearArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's ClearLogo
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieClearLogo(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.ClearLogo)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's DiscArt
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieDiscArt(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.DiscArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's fanart
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieFanart(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Fanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's landscape
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieLandscape(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Landscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movie's posters
    ''' </summary>
    ''' <param name="mMovie">Structures.DBMovie representing the movie to be worked on</param>
    ''' <remarks></remarks>
    Public Sub DeleteMoviePoster(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Poster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's banner
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetBanner(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Banner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's ClearArt
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetClearArt(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.ClearArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's ClearLogo
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetClearLogo(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.ClearLogo)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's DiscArt
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetDiscArt(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.DiscArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's fanart
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetFanart(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Fanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's landscape
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetLandscape(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Landscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the movieset's poster
    ''' </summary>
    ''' <param name="mMovieSet"><c>DBMovieSet</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieSetPoster(ByVal mMovieSet As Structures.DBMovieSet)
        If String.IsNullOrEmpty(mMovieSet.SetName) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Poster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mMovieSet.SetName & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season banner
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVSeasonBanner(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVSeasonFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season landscape
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVSeasonLandscape(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's season posters
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVSeasonPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's banner
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowBanner(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowBanner)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's CharacterArt
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowCharacterArt(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowCharacterArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's ClearArt
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowClearArt(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowClearArt)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's ClearLogo
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowClearLogo(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowClearLogo)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's Fanart
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowFanart(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowFanart)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's landscape
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowLandscape(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowLandscape)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the TV Show's posters
    ''' </summary>
    ''' <param name="mShow"><c>Structures.DBTV</c> representing the TV Show to work on</param>
    ''' <remarks></remarks>
    Public Sub DeleteTVShowPosters(ByVal mShow As Structures.DBTV)
        If String.IsNullOrEmpty(mShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(mShow.ShowPath, Enums.TVModType.ShowPoster)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & mShow.ShowPath & ">", ex)
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
                logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & sPath & ">", ex)
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

                ' if is not a JPG or PNG we have to convert the memory stream to JPG format
                If Not (sHTTP.isJPG OrElse sHTTP.isPNG) Then
                    UpdateMSfromImg(New Bitmap(_image))
                End If
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "<" & sURL & ">", ex)
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
    Public Function IsAllowedToDownload(ByVal mMovie As Structures.DBMovie, ByVal fType As Enums.MovieImageType, Optional ByVal isChange As Boolean = False) As Boolean
        '2013/11/26 Dekker500 - Need to figure out exactly what this method is doing so it can be documented

        Try
            With Master.eSettings
                Select Case fType
                    Case Enums.MovieImageType.Banner
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.BannerPath) OrElse .MovieBannerOverwrite) AndAlso _
                            (.MovieBannerEden OrElse .MovieBannerFrodo OrElse .MovieBannerNMJ OrElse .MovieBannerYAMJ OrElse _
                             (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieBannerExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MovieBannerExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieBannerExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MovieBannerExpertVTS))))) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.ClearArt
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.ClearArtPath) OrElse .MovieClearArtOverwrite) AndAlso _
                            (.MovieClearArtEden OrElse .MovieClearArtFrodo OrElse _
                             (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieClearArtExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MovieClearArtExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieClearArtExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MovieClearArtExpertVTS))))) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.ClearLogo
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.ClearLogoPath) OrElse .MovieClearLogoOverwrite) AndAlso _
                            (.MovieClearLogoEden OrElse .MovieClearLogoFrodo OrElse _
                             (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieClearLogoExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MovieClearLogoExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieClearLogoExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MovieClearLogoExpertVTS))))) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.DiscArt
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.DiscArtPath) OrElse .MovieDiscArtOverwrite) AndAlso _
                            (.MovieDiscArtEden OrElse .MovieDiscArtFrodo OrElse _
                             (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieDiscArtExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MovieDiscArtExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieDiscArtExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MovieDiscArtExpertVTS))))) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.EFanarts
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.EFanartsPath) OrElse .MovieEFanartsOverwrite) AndAlso _
                            (.MovieExtrafanartsEden OrElse .MovieExtrafanartsFrodo)) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.EThumbs
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.EThumbsPath) OrElse .MovieEThumbsOverwrite) AndAlso _
                            (.MovieExtrathumbsEden OrElse .MovieExtrathumbsFrodo)) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.Fanart
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.FanartPath) OrElse .MovieFanartOverwrite)) AndAlso _
                        (.MovieFanartBoxee OrElse .MovieFanartFrodo OrElse .MovieFanartEden OrElse .MovieFanartYAMJ OrElse _
                         .MovieFanartNMJ OrElse (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieFanartExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MovieFanartExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieFanartExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MovieFanartExpertVTS)))) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.Landscape
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.LandscapePath) OrElse .MovieLandscapeOverwrite) AndAlso _
                            (.MovieLandscapeEden OrElse .MovieLandscapeFrodo OrElse _
                             (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieLandscapeExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MovieLandscapeExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieLandscapeExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MovieLandscapeExpertVTS))))) Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Enums.MovieImageType.Poster
                        If (isChange OrElse (String.IsNullOrEmpty(mMovie.PosterPath) OrElse .MoviePosterOverwrite)) AndAlso _
                        (.MoviePosterBoxee OrElse .MoviePosterFrodo OrElse .MoviePosterEden OrElse .MoviePosterNMJ OrElse _
                         .MoviePosterYAMJ OrElse (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MoviePosterExpertBDMV) OrElse _
                         Not String.IsNullOrEmpty(.MoviePosterExpertMulti) OrElse Not String.IsNullOrEmpty(.MoviePosterExpertSingle) OrElse _
                         Not String.IsNullOrEmpty(.MoviePosterExpertVTS)))) Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            End With
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
    End Function

    Public Sub ResizeExtraFanart(ByVal fromPath As String, ByVal toPath As String)
        'Debug.Print("---------- ResizeExtraThumb ----------")
        Me.FromFile(fromPath)
        'If Not Master.eSettings.ETNative Then
        '	Dim iWidth As Integer = Master.eSettings.ETWidth
        '	Dim iHeight As Integer = Master.eSettings.ETHeight
        '	ImageUtils.ResizeImage(_image, iWidth, iHeight, Master.eSettings.ETPadding, Color.Black.ToArgb)
        'End If
        Me.Save(toPath)
    End Sub

    Public Sub ResizeExtraThumb(ByVal fromPath As String, ByVal toPath As String)
        'Debug.Print("---------- ResizeExtraThumb ----------")
        Me.FromFile(fromPath)
        'If Not Master.eSettings.ETNative Then
        '	Dim iWidth As Integer = Master.eSettings.ETWidth
        '	Dim iHeight As Integer = Master.eSettings.ETHeight
        '	ImageUtils.ResizeImage(_image, iWidth, iHeight, Master.eSettings.ETPadding, Color.Black.ToArgb)
        'End If
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
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
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
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.AllSeasonsBanner, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASBannerOverwrite) Then
                        Save(s, Master.eSettings.TVASBannerQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASBannerOverwrite) Then
                    Save(a, Master.eSettings.TVASBannerQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.AllSeasonsFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASFanartOverwrite) Then
                        Save(s, Master.eSettings.TVASFanartQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASFanartOverwrite) Then
                    Save(a, Master.eSettings.TVASFanartQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.AllSeasonsLandscape, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASLandscapeOverwrite) Then
                        Save(s, 0, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASLandscapeOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.AllSeasonsPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVASPosterOverwrite) Then
                        Save(s, Master.eSettings.TVASPosterQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVASPosterOverwrite) Then
                    Save(a, Master.eSettings.TVASPosterQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsTVEpisodeFanart(ByVal mShow As Structures.DBTV, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVEpisodeFanartResize AndAlso (_image.Width > Master.eSettings.TVEpisodeFanartWidth OrElse _image.Height > Master.eSettings.TVEpisodeFanartHeight)

        Try
            Dim EpisodePath As String = mShow.Filename

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVEpisodeFanartWidth, Master.eSettings.TVEpisodeFanartHeight)
            End If
            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.EpisodeFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVEpisodeFanartOverwrite) Then
                        Save(s, Master.eSettings.TVEpisodeFanartQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.TVModType.EpisodeFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVEpisodeFanartOverwrite) Then
                    Save(a, Master.eSettings.TVEpisodeFanartQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.TVEpisodePosterResize AndAlso (_image.Width > Master.eSettings.TVEpisodePosterWidth OrElse _image.Height > Master.eSettings.TVEpisodePosterHeight)

        Try
            Dim EpisodePath As String = mShow.Filename

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVEpisodePosterWidth, Master.eSettings.TVEpisodePosterHeight)
            End If
            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.EpisodePoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVEpisodePosterOverwrite) Then
                        Save(s, Master.eSettings.TVEpisodePosterQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.TVModType.EpisodePoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVEpisodePosterOverwrite) Then
                    Save(a, Master.eSettings.TVEpisodePosterQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function


    ''' <summary>
    ''' Save the image as collection poster
    ''' </summary>
    ''' <param name="fPath"><c></c> name of artwork folder</param>
    ''' <param name="setname"><c></c> name of collection</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieCollectionPoster(ByVal fPath As String, ByVal setname As String) As String
        Dim strReturn As String = String.Empty
        Try
            Dim doResize As Boolean = Master.eSettings.MoviePosterResize AndAlso (_image.Width > Master.eSettings.MoviePosterWidth OrElse _image.Height > Master.eSettings.MoviePosterHeight)

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MoviePosterWidth, Master.eSettings.MoviePosterHeight)
            End If

            'TODO At the moment only single artwork folder is supported
            If Directory.Exists(fPath) AndAlso setname <> "" Then
                fPath = Path.Combine(fPath, setname & "-poster.jpg")
                Save(fPath, Master.eSettings.MoviePosterQual, "", doResize)
                strReturn = fPath
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return strReturn
        End Try

        Return strReturn
    End Function

    ''' <summary>
    ''' Save the image as collection fanart
    ''' </summary>
    ''' <param name="fPath"><c></c> name of artwork folder</param>
    ''' <param name="setname"><c></c> name of collection</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieCollectionFanart(ByVal fPath As String, ByVal setname As String) As String
        Dim strReturn As String = String.Empty
        Try
            Dim doResize As Boolean = Master.eSettings.MovieFanartResize AndAlso (_image.Width > Master.eSettings.MovieFanartWidth OrElse _image.Height > Master.eSettings.MovieFanartHeight)

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieFanartWidth, Master.eSettings.MovieFanartHeight)
            End If

            'TODO At the moment only single artwork folder is supported
            If Directory.Exists(fPath) AndAlso setname <> "" Then
                fPath = Path.Combine(fPath, setname & "-fanart.jpg")
                Save(fPath, Master.eSettings.MovieFanartQual, "", doResize)
                strReturn = fPath
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return strReturn
        End Try

        Return strReturn
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
        Dim doResize As Boolean = False

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Banner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieBannerOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieClearArtSave, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.ClearArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieClearArtOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieClearLogoSave, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.ClearLogo)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieClearLogoOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieDiscArtSave, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.DiscArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieDiscArtOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsMovieFanart(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = Master.eSettings.MovieFanartResize AndAlso (_image.Width > Master.eSettings.MovieFanartWidth OrElse _image.Height > Master.eSettings.MovieFanartHeight)

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieFanartSave, params, _image, False)
            Catch ex As Exception
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieFanartWidth, Master.eSettings.MovieFanartHeight)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Fanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieFanartOverwrite) Then
                    Save(a, Master.eSettings.MovieFanartQual, sURL, doResize)
                    strReturn = a
                End If
            Next

            If Master.eSettings.MovieBackdropsAuto AndAlso Directory.Exists(Master.eSettings.MovieBackdropsPath) Then
                Save(String.Concat(Master.eSettings.MovieBackdropsPath, Path.DirectorySeparatorChar, StringUtils.CleanFileName(mMovie.Movie.OriginalTitle), "_tt", mMovie.Movie.IMDBID, ".jpg"), Master.eSettings.MovieFanartQual, sURL, doResize)
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieLandscapeSave, params, _image, False)
            Catch ex As Exception
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Landscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieLandscapeOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMoviePosterSave, params, _image, False)
            Catch ex As Exception
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MoviePosterWidth, Master.eSettings.MoviePosterHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.Poster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MoviePosterOverwrite) Then
                    Save(a, Master.eSettings.MoviePosterQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Banner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetBannerOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.ClearArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetClearArtOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.ClearLogo)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetClearLogoOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.DiscArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetDiscArtOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Fanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetFanartOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Landscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetLandscapeOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {mMovie})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieBannerSave, params, _image, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.MovieSet(mMovieSet.SetName, Enums.MovieModType.Poster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.MovieSetPosterOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsMovieActorThumb(ByVal actor As MediaContainers.Person, ByVal fpath As String, ByVal aMovie As Structures.DBMovie) As String
        'TODO 2013/11/26 Dekker500 - This should be re-factored to remove the fPath argument. All callers pass the same string derived from the provided DBMovie, so why do it twice?
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.Movie(aMovie.Filename, aMovie.isSingle, Enums.MovieModType.ActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            If Not File.Exists(tPath) OrElse (IsEdit OrElse Master.eSettings.MovieActorThumbsOverwrite) Then
                Save(tPath, Master.eSettings.MovieActorThumbsQual)
            End If
        Next

        Return tPath
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
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath
            Dim SeasonFirstEpisodePath As String = String.Empty

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVSeasonBannerWidth, Master.eSettings.TVSeasonBannerHeight)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.SeasonBanner, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonBannerOverwrite) Then
                        Save(s, Master.eSettings.TVSeasonBannerQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonBannerOverwrite) Then
                    Save(a, Master.eSettings.TVSeasonBannerQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonFirstEpisodePath As String = String.Empty
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVSeasonFanartWidth, Master.eSettings.TVSeasonFanartHeight)
            End If
            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.SeasonFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonFanartOverwrite) Then

                        Save(s, Master.eSettings.TVSeasonFanartQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonFanartOverwrite) Then
                    Save(a, Master.eSettings.TVSeasonFanartQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
        Dim doResize As Boolean = False

        Try
            Dim pPath As String = String.Empty
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonFirstEpisodePath As String = String.Empty
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.SeasonLandscape, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonLandscapeOverwrite) Then
                        Save(s, 0, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonLandscapeOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            Dim Season As Integer = mShow.TVEp.Season
            Dim SeasonFirstEpisodePath As String = String.Empty
            Dim SeasonPath As String = Functions.GetSeasonDirectoryFromShowPath(mShow.ShowPath, mShow.TVEp.Season)
            Dim ShowPath As String = mShow.ShowPath

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", mShow.ShowID, " AND Season = ", mShow.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVSeasonPosterWidth, Master.eSettings.TVSeasonPosterHeight)
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.SeasonPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVSeasonPosterOverwrite) Then
                        Save(s, Master.eSettings.TVSeasonPosterQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVSeasonPosterOverwrite) Then
                    Save(a, Master.eSettings.TVSeasonPosterQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
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
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowBanner, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowBannerOverwrite) Then
                        Save(s, Master.eSettings.TVShowBannerQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowBanner)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowBannerOverwrite) Then
                    Save(a, Master.eSettings.TVShowBannerQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

        Dim doResize As Boolean = False

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowCharacterArt, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowCharacterArtOverwrite) Then
                        Save(s, 0, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowCharacterArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowCharacterArtOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

        Dim doResize As Boolean = False

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowClearArt, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowClearArtOverwrite) Then
                        Save(s, 0, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowClearArt)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowClearArtOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

        Dim doResize As Boolean = False

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowClearLogo, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowClearLogoOverwrite) Then
                        Save(s, 0, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowClearLogo)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowClearLogoOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

        Try
            Dim ShowPath As String = mShow.ShowPath

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowEFanarts)
                If Not a = String.Empty Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    efPath = Path.Combine(a, sName)
                    Save(efPath)
                    Return efPath
                End If
            Next
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowFanart, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowFanartOverwrite) Then
                        Save(s, Master.eSettings.TVShowFanartQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then
                    Return strReturn
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowFanart)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowFanartOverwrite) Then
                    Save(a, Master.eSettings.TVShowFanartQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

        Dim doResize As Boolean = False

        Try
            Dim pPath As String = String.Empty
            Dim ShowPath As String = mShow.ShowPath

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowLandscape, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowLandscapeOverwrite) Then
                        Save(s, 0, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowLandscape)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowLandscapeOverwrite) Then
                    Save(a, 0, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
            End If

            Try
                Dim params As New List(Of Object)(New Object() {Enums.TVImageType.ShowPoster, mShow, New List(Of String)})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.TVImageNaming, params, doContinue)
                For Each s As String In DirectCast(params(2), List(Of String))
                    If Not File.Exists(s) OrElse (IsEdit OrElse Master.eSettings.TVShowPosterOverwrite) Then
                        Save(s, Master.eSettings.TVShowPosterQual, sURL, doResize)
                        If String.IsNullOrEmpty(strReturn) Then strReturn = s
                    End If
                Next
                If Not doContinue Then Return strReturn
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowPoster)
                If Not File.Exists(a) OrElse (IsEdit OrElse Master.eSettings.TVShowPosterOverwrite) Then
                    Save(a, Master.eSettings.TVShowPosterQual, sURL, doResize)
                    strReturn = a
                End If
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
    Public Function SaveAsMovieExtrathumb(ByVal mMovie As Structures.DBMovie, Optional sURL As String = "") As String
        Dim etPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mMovie.Filename) Then Return etPath

        For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.EThumbs)
            If Not a = String.Empty Then
                If Not Directory.Exists(a) Then
                    Directory.CreateDirectory(a)
                End If
                iMod = Functions.GetExtraModifier(a)
                iVal = iMod + 1
                etPath = Path.Combine(a, String.Concat("thumb", iVal, ".jpg"))
                Save(etPath)
                Return etPath
            End If
        Next

        Return etPath
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

        For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.isSingle, Enums.MovieModType.EFanarts)
            If Not a = String.Empty Then
                If Not Directory.Exists(a) Then
                    Directory.CreateDirectory(a)
                End If
                efPath = Path.Combine(a, sName)
                Save(efPath)
                Return efPath
            End If
        Next

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

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.poster_names Where (aD.index = Master.eSettings.MoviePosterPrefSize)
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

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.backdrop_names Where (aD.index = Master.eSettings.MovieFanartPrefSize)
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

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.backdrop_names Where (aD.index = Master.eSettings.MovieEThumbsPrefSize)
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

        Dim aDesc = From aD As Structures.v3Size In Master.eSize.backdrop_names Where (aD.index = Master.eSettings.MovieEFanartsPrefSize)
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