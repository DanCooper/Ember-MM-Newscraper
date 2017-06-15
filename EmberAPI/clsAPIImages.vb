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

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _image As Image
    Private _ms As MemoryStream

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public ReadOnly Property HasMemoryStream() As Boolean
        Get
            Return _ms.Length > 0
        End Get
    End Property

    Public ReadOnly Property [Image]() As Image
        Get
            Return _image
        End Get
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
    Public Sub UpdateMSfromImg(nImage As Image, Optional iQuality As Integer = 100)
        Try
            Dim ICI As ImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg)

            Dim EncPars As EncoderParameters = New EncoderParameters(2)
            EncPars.Param(0) = New EncoderParameter(Encoder.RenderMethod, EncoderValue.RenderNonProgressive)
            EncPars.Param(1) = New EncoderParameter(Encoder.Quality, iQuality)

            'Write the supplied image into the MemoryStream
            If Not _ms Is Nothing Then _ms.Dispose()
            _ms = New MemoryStream()
            nImage.Save(_ms, ICI, EncPars)
            _ms.Flush()

            'Replace the existing image with the new image
            If Not _image Is Nothing Then _image.Dispose()
            _image = New Bitmap(_ms)

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Reset this instance to a pristine condition
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        _image = Nothing
        _ms = New MemoryStream()
    End Sub
    ''' <summary>
    ''' Delete the given arbitrary file
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Private Shared Sub Delete(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) Then
            Try
                File.Delete(sPath)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Param: <" & sPath & ">")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete all movie images with specified image type
    ''' </summary>
    ''' <param name="DBMovie"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_Movie(ByVal DBMovie As Database.DBElement, ByVal ImageType As Enums.ModifierType, ByVal ForceFileCleanup As Boolean)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie, ImageType, ForceFileCleanup)
                Select Case ImageType
                    Case Enums.ModifierType.MainActorThumbs
                        Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                        If Directory.Exists(tmpPath) Then
                            FileUtils.Delete.DeleteDirectory(tmpPath)
                        End If
                    Case Enums.ModifierType.MainExtrafanarts, Enums.ModifierType.MainExtrathumbs
                        If Directory.Exists(a) Then
                            FileUtils.Delete.DeleteDirectory(a)
                        End If
                    Case Else
                        If File.Exists(a) Then
                            Delete(a)
                        End If
                End Select
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBMovie.Filename & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all movieset images with specified image type
    ''' </summary>
    ''' <param name="DBMovieSet"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_MovieSet(ByVal DBMovieSet As Database.DBElement, ByVal ImageType As Enums.ModifierType, Optional ByVal bForceOldTitle As Boolean = False)
        If String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.MovieSet(DBMovieSet, ImageType, bForceOldTitle)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBMovieSet.MovieSet.Title & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all tv show AllSeasons images with specified image type
    ''' </summary>
    ''' <param name="DBTVShow"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVAllSeasons(ByVal DBTVShow As Database.DBElement, ByVal ImageType As Enums.ModifierType)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow, ImageType)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBTVShow.ShowPath & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all tv episode images with specified image type
    ''' </summary>
    ''' <param name="DBTVEpisode"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVEpisode(ByVal DBTVEpisode As Database.DBElement, ByVal ImageType As Enums.ModifierType)
        If String.IsNullOrEmpty(DBTVEpisode.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVEpisode(DBTVEpisode, ImageType)
                Select Case ImageType
                    Case Enums.ModifierType.EpisodeActorThumbs
                        Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                        If Directory.Exists(tmpPath) Then
                            FileUtils.Delete.DeleteDirectory(tmpPath)
                        End If
                    Case Else
                        If File.Exists(a) Then
                            Delete(a)
                        End If
                End Select
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBTVEpisode.ShowPath & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all tv season images with specified image type
    ''' </summary>
    ''' <param name="DBTVSeason"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVSeason(ByVal DBTVSeason As Database.DBElement, ByVal ImageType As Enums.ModifierType)
        If String.IsNullOrEmpty(DBTVSeason.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVSeason(DBTVSeason, ImageType)
                If File.Exists(a) Then
                    Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBTVSeason.ShowPath & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all tv show images with specified image type
    ''' </summary>
    ''' <param name="DBTVShow"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVShow(ByVal DBTVShow As Database.DBElement, ByVal ImageType As Enums.ModifierType)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow, ImageType)
                Select Case ImageType
                    Case Enums.ModifierType.MainActorThumbs
                        Dim tmpPath As String = Directory.GetParent(a.Replace("<placeholder>", "dummy")).FullName
                        If Directory.Exists(tmpPath) Then
                            FileUtils.Delete.DeleteDirectory(tmpPath)
                        End If
                    Case Enums.ModifierType.MainExtrafanarts
                        If Directory.Exists(a) Then
                            FileUtils.Delete.DeleteDirectory(a)
                        End If
                    Case Else
                        If File.Exists(a) Then
                            Delete(a)
                        End If
                End Select
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBTVShow.ShowPath & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Loads this Image from the contents of the supplied file
    ''' </summary>
    ''' <param name="strPath">Path to the image file</param>
    ''' <param name="bLoadBitmap">Create bitmap from memorystream</param>
    ''' <remarks></remarks>
    Public Sub LoadFromFile(ByVal strPath As String, Optional bLoadBitmap As Boolean = False)
        If Not String.IsNullOrEmpty(strPath) Then
            Dim fiImage = New FileInfo(strPath)

            If Not fiImage.Exists Then
                logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" not found", strPath))
                _ms = New MemoryStream
                _image = Nothing
            ElseIf fiImage.Length > 0 Then
                _ms = New MemoryStream()
                _image = Nothing
                Using fsImage As FileStream = File.OpenRead(strPath)
                    Dim memStream As New MemoryStream
                    memStream.SetLength(fsImage.Length)
                    fsImage.Read(memStream.GetBuffer, 0, CInt(Fix(fsImage.Length)))
                    _ms.Write(memStream.GetBuffer, 0, CInt(Fix(fsImage.Length)))
                    _ms.Flush()
                    If _ms.Length > 0 Then
                        If bLoadBitmap Then
                            _image = New Bitmap(_ms)
                        End If
                    Else
                        logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" is empty", strPath))
                        _ms = New MemoryStream
                        _image = Nothing
                    End If
                End Using
            Else
                logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" is empty", strPath))
                _ms = New MemoryStream
                _image = Nothing
            End If
        Else
            logger.Error("[APIImages] [LoadFromFile]: Path is empty")
            _ms = New MemoryStream
            _image = Nothing
        End If
    End Sub

    Public Function LoadFromMemoryStream() As Boolean
        If HasMemoryStream Then
            _image = New Bitmap(_ms)
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Loads this Image from the supplied URL
    ''' </summary>
    ''' <param name="sURL">URL to the image file</param>
    ''' <param name="LoadBitmap">Create bitmap from memorystream</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal sURL As String, Optional LoadBitmap As Boolean = False)
        If String.IsNullOrEmpty(sURL) Then Return

        Try
            Dim sHTTP As New HTTP
            sHTTP.StartDownloadImage(sURL)
            While sHTTP.IsDownloading
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If sHTTP.Image IsNot Nothing Then
                If _ms IsNot Nothing Then
                    _ms.Dispose()
                End If
                _ms = New MemoryStream()

                Dim retSave() As Byte
                retSave = sHTTP.ms.ToArray
                _ms.Write(retSave, 0, retSave.Length)

                'I do not copy from the _ms as it could not be a JPG
                '_image = New Bitmap(sHTTP.Image)
                If LoadBitmap Then
                    _image = New Bitmap(sHTTP.Image) '(Me._ms)
                End If

                ' if is not a JPG or PNG we have to convert the memory stream to JPG format
                If Not (sHTTP.isJPG OrElse sHTTP.isPNG) Then
                    UpdateMSfromImg(New Bitmap(_image))
                End If
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & sURL & ">")
        End Try
    End Sub

    Public Sub ResizeExtraFanart(ByVal fromPath As String, ByVal toPath As String)
        LoadFromFile(fromPath)
        SaveToFile(toPath)
    End Sub

    Public Sub ResizeExtraThumb(ByVal fromPath As String, ByVal toPath As String)
        LoadFromFile(fromPath)
        SaveToFile(toPath)
    End Sub
    ''' <summary>
    ''' Stores the Image to the supplied <paramref name="sPath"/>
    ''' </summary>
    ''' <param name="sPath">Location to store the image</param>
    ''' <remarks></remarks>
    Public Sub SaveToFile(ByVal sPath As String)
        If _ms.Length > 0 Then
            Dim retSave() As Byte
            Try
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
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            Throw New ArgumentOutOfRangeException("Looks like MemoryStream is empty")
        End If
    End Sub
    ''' <summary>
    ''' Save the image as a Movie image
    ''' </summary>
    ''' <param name="tDBElement"><c>Database.DBElement</c> representing the Movie being referred to</param>
    ''' <param name="tImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_Movie(ByVal tDBElement As Database.DBElement, ByVal tImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = False
        Dim bResizeEnabled As Boolean = False
        Dim intHeight As Integer = -1
        Dim intQuality As Integer = 100
        Dim intWidth As Integer = -1

        Select Case tImageType
            Case Enums.ModifierType.MainBanner
                bResizeEnabled = Master.eSettings.MovieBannerResize
                intHeight = Master.eSettings.MovieBannerHeight
                intQuality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , Enums.ContentType.Movie))
                intWidth = Master.eSettings.MovieBannerWidth
            Case Enums.ModifierType.MainExtrafanarts
                bResizeEnabled = Master.eSettings.MovieExtrafanartsResize
                intHeight = Master.eSettings.MovieExtrafanartsHeight
                intQuality = CInt(AdvancedSettings.GetSetting("ExtrafanartsQuality", "100", , Enums.ContentType.Movie))
                intWidth = Master.eSettings.MovieExtrafanartsWidth
            Case Enums.ModifierType.MainExtrathumbs
                bResizeEnabled = Master.eSettings.MovieExtrathumbsResize
                intHeight = Master.eSettings.MovieExtrathumbsHeight
                intQuality = CInt(AdvancedSettings.GetSetting("ExtrathumbsQuality", "100", , Enums.ContentType.Movie))
                intWidth = Master.eSettings.MovieExtrathumbsWidth
            Case Enums.ModifierType.MainFanart
                bResizeEnabled = Master.eSettings.MovieFanartResize
                intHeight = Master.eSettings.MovieFanartHeight
                intQuality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , Enums.ContentType.Movie))
                intWidth = Master.eSettings.MovieFanartWidth
            Case Enums.ModifierType.MainPoster
                bResizeEnabled = Master.eSettings.MoviePosterResize
                intHeight = Master.eSettings.MoviePosterHeight
                intQuality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , Enums.ContentType.Movie))
                intWidth = Master.eSettings.MoviePosterWidth
        End Select

        If bResizeEnabled Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                doResize = _image.Width > intWidth OrElse _image.Height > intHeight
            End If
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, intWidth, intHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image, intQuality)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, tImageType)
                SaveToFile(a)
                strReturn = a
            Next

            If tImageType = Enums.ModifierType.MainFanart AndAlso Not String.IsNullOrEmpty(strReturn) AndAlso
                Not String.IsNullOrEmpty(Master.eSettings.MovieBackdropsPath) AndAlso Master.eSettings.MovieBackdropsAuto Then
                FileUtils.Common.CopyFanartToBackdropsPath(strReturn, tDBElement.ContentType)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a MovieSet image
    ''' </summary>
    ''' <param name="tDBElement"><c>Database.DBElement</c> representing the MovieSet being referred to</param>
    ''' <param name="tImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_MovieSet(ByVal tDBElement As Database.DBElement, ByVal tImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = False
        Dim bResizeEnabled As Boolean = False
        Dim intHeight As Integer = -1
        Dim intQuality As Integer = 100
        Dim intWidth As Integer = -1

        Select Case tImageType
            Case Enums.ModifierType.MainBanner
                bResizeEnabled = Master.eSettings.MovieSetBannerResize
                intHeight = Master.eSettings.MovieSetBannerHeight
                intQuality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , Enums.ContentType.MovieSet))
                intWidth = Master.eSettings.MovieSetBannerWidth
            Case Enums.ModifierType.MainFanart
                bResizeEnabled = Master.eSettings.MovieSetFanartResize
                intHeight = Master.eSettings.MovieSetFanartHeight
                intQuality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , Enums.ContentType.MovieSet))
                intWidth = Master.eSettings.MovieSetFanartWidth
            Case Enums.ModifierType.MainPoster
                bResizeEnabled = Master.eSettings.MovieSetPosterResize
                intHeight = Master.eSettings.MovieSetPosterHeight
                intQuality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , Enums.ContentType.MovieSet))
                intWidth = Master.eSettings.MovieSetPosterWidth
        End Select

        If bResizeEnabled Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                doResize = _image.Width > intWidth OrElse _image.Height > intHeight
            End If
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, intWidth, intHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image, intQuality)
            End If

            For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, tImageType)
                SaveToFile(a)
                strReturn = a
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TVAllSeasons image
    ''' </summary>
    ''' <param name="tDBElement"><c>Database.DBElement</c> representing the TVShow being referred to</param>
    ''' <param name="tImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_TVAllSeasons(ByVal tDBElement As Database.DBElement, ByVal tImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = False
        Dim bResizeEnabled As Boolean = False
        Dim intHeight As Integer = -1
        Dim intQuality As Integer = 100
        Dim intWidth As Integer = -1

        Select Case tImageType
            Case Enums.ModifierType.AllSeasonsBanner
                bResizeEnabled = Master.eSettings.TVAllSeasonsBannerResize
                intHeight = Master.eSettings.TVAllSeasonsBannerHeight
                intQuality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , Enums.ContentType.TVSeason))
                intWidth = Master.eSettings.TVAllSeasonsBannerWidth
            Case Enums.ModifierType.SeasonFanart
                bResizeEnabled = Master.eSettings.TVAllSeasonsFanartResize
                intHeight = Master.eSettings.TVAllSeasonsFanartHeight
                intQuality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , Enums.ContentType.TVSeason))
                intWidth = Master.eSettings.TVAllSeasonsFanartWidth
            Case Enums.ModifierType.SeasonPoster
                bResizeEnabled = Master.eSettings.TVAllSeasonsPosterResize
                intHeight = Master.eSettings.TVAllSeasonsPosterHeight
                intQuality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , Enums.ContentType.TVSeason))
                intWidth = Master.eSettings.TVAllSeasonsPosterWidth
        End Select

        If bResizeEnabled Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                doResize = _image.Width > intWidth OrElse _image.Height > intHeight
            End If
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, intWidth, intHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image, intQuality)
            End If

            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, tImageType)
                SaveToFile(a)
                strReturn = a
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TVEpisode image
    ''' </summary>
    ''' <param name="tDBElement"><c>Database.DBElement</c> representing the TVEpisode being referred to</param>
    ''' <param name="tImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_TVEpisode(ByVal tDBElement As Database.DBElement, ByVal tImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = False
        Dim bResizeEnabled As Boolean = False
        Dim intHeight As Integer = -1
        Dim intQuality As Integer = 100
        Dim intWidth As Integer = -1

        Select Case tImageType
            Case Enums.ModifierType.EpisodeFanart
                bResizeEnabled = Master.eSettings.TVEpisodeFanartResize
                intHeight = Master.eSettings.TVEpisodeFanartHeight
                intQuality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , Enums.ContentType.TVEpisode))
                intWidth = Master.eSettings.TVEpisodeFanartWidth
            Case Enums.ModifierType.EpisodePoster
                bResizeEnabled = Master.eSettings.TVEpisodePosterResize
                intHeight = Master.eSettings.TVEpisodePosterHeight
                intQuality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , Enums.ContentType.TVEpisode))
                intWidth = Master.eSettings.TVEpisodePosterWidth
        End Select

        If bResizeEnabled Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                doResize = _image.Width > intWidth OrElse _image.Height > intHeight
            End If
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, intWidth, intHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image, intQuality)
            End If

            For Each a In FileUtils.GetFilenameList.TVEpisode(tDBElement, tImageType)
                SaveToFile(a)
                strReturn = a
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TVSeason image
    ''' </summary>
    ''' <param name="tDBElement"><c>Database.DBElement</c> representing the TVSeason being referred to</param>
    ''' <param name="tImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_TVSeason(ByVal tDBElement As Database.DBElement, ByVal tImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = False
        Dim bResizeEnabled As Boolean = False
        Dim intHeight As Integer = -1
        Dim intQuality As Integer = 100
        Dim intWidth As Integer = -1

        Select Case tImageType
            Case Enums.ModifierType.SeasonBanner
                bResizeEnabled = Master.eSettings.TVSeasonBannerResize
                intHeight = Master.eSettings.TVSeasonBannerHeight
                intQuality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , Enums.ContentType.TVSeason))
                intWidth = Master.eSettings.TVSeasonBannerWidth
            Case Enums.ModifierType.SeasonFanart
                bResizeEnabled = Master.eSettings.TVSeasonFanartResize
                intHeight = Master.eSettings.TVSeasonFanartHeight
                intQuality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , Enums.ContentType.TVSeason))
                intWidth = Master.eSettings.TVSeasonFanartWidth
            Case Enums.ModifierType.SeasonPoster
                bResizeEnabled = Master.eSettings.TVSeasonPosterResize
                intHeight = Master.eSettings.TVSeasonPosterHeight
                intQuality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , Enums.ContentType.TVSeason))
                intWidth = Master.eSettings.TVSeasonPosterWidth
        End Select

        If bResizeEnabled Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                doResize = _image.Width > intWidth OrElse _image.Height > intHeight
            End If
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, intWidth, intHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image, intQuality)
            End If

            For Each a In FileUtils.GetFilenameList.TVSeason(tDBElement, tImageType)
                SaveToFile(a)
                strReturn = a
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function
    ''' <summary>
    ''' Save the image as a TVShow image
    ''' </summary>
    ''' <param name="tDBElement"><c>Database.DBElement</c> representing the TVShow being referred to</param>
    ''' <param name="tImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_TVShow(ByVal tDBElement As Database.DBElement, ByVal tImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim doResize As Boolean = False
        Dim bResizeEnabled As Boolean = False
        Dim intHeight As Integer = -1
        Dim intQuality As Integer = 100
        Dim intWidth As Integer = -1

        Select Case tImageType
            Case Enums.ModifierType.MainBanner
                bResizeEnabled = Master.eSettings.TVShowBannerResize
                intHeight = Master.eSettings.TVShowBannerHeight
                intQuality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , Enums.ContentType.TVShow))
                intWidth = Master.eSettings.TVShowBannerWidth
            Case Enums.ModifierType.MainExtrafanarts
                bResizeEnabled = Master.eSettings.TVShowExtrafanartsResize
                intHeight = Master.eSettings.TVShowExtrafanartsHeight
                intQuality = CInt(AdvancedSettings.GetSetting("ExtrafanartsQuality", "100", , Enums.ContentType.TVShow))
                intWidth = Master.eSettings.TVShowExtrafanartsWidth
            Case Enums.ModifierType.MainFanart
                bResizeEnabled = Master.eSettings.TVShowFanartResize
                intHeight = Master.eSettings.TVShowFanartHeight
                intQuality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , Enums.ContentType.TVShow))
                intWidth = Master.eSettings.TVShowFanartWidth
            Case Enums.ModifierType.MainPoster
                bResizeEnabled = Master.eSettings.TVShowPosterResize
                intHeight = Master.eSettings.TVShowPosterHeight
                intQuality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , Enums.ContentType.TVShow))
                intWidth = Master.eSettings.TVShowPosterWidth
        End Select

        If bResizeEnabled Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                doResize = _image.Width > intWidth OrElse _image.Height > intHeight
            End If
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, intWidth, intHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image, intQuality)
            End If

            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, tImageType)
                SaveToFile(a)
                strReturn = a
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Shared Sub SaveMovieActorThumbs(ByVal mMovie As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each tActor As MediaContainers.Person In mMovie.Movie.Actors
            tActor.Thumb.LoadAndCache(mMovie.ContentType, True)
        Next

        'Second, remove the old ones
        Delete_Movie(mMovie, Enums.ModifierType.MainActorThumbs, False)

        'Thirdly, save all actor thumbs
        For Each tActor As MediaContainers.Person In mMovie.Movie.Actors
            If tActor.Thumb.LoadAndCache(mMovie.ContentType, True) Then
                tActor.Thumb.LocalFilePath = tActor.Thumb.ImageOriginal.SaveAsMovieActorThumb(mMovie, tActor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="aMovie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieActorThumb(ByVal aMovie As Database.DBElement, ByVal actor As MediaContainers.Person) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.Movie(aMovie, Enums.ModifierType.MainActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            SaveToFile(tPath)
        Next

        Clear() 'Dispose to save memory
        Return tPath
    End Function
    ''' <summary>
    ''' Save all movie Extrafanarts
    ''' </summary>
    ''' <param name="mMovie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Shared Function SaveMovieExtrafanarts(ByVal mMovie As Database.DBElement) As String
        Dim efPath As String = String.Empty

        'First, (Down)Load all Extrafanarts from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In mMovie.ImagesContainer.Extrafanarts
            eImg.LoadAndCache(mMovie.ContentType, True)
        Next

        'Second, remove the old ones
        Delete_Movie(mMovie, Enums.ModifierType.MainExtrafanarts, False)

        'Thirdly, save all Extrafanarts
        For Each eImg As MediaContainers.Image In mMovie.ImagesContainer.Extrafanarts
            If eImg.LoadAndCache(mMovie.ContentType, True) Then
                efPath = eImg.ImageOriginal.SaveAsMovieExtrafanart(mMovie, If(Not String.IsNullOrEmpty(eImg.URLOriginal), Path.GetFileName(eImg.URLOriginal), Path.GetFileName(eImg.LocalFilePath)))
            End If
        Next

        'If efPath is empty (i.e. expert setting enabled but expert extrafanart scraping disabled) it will cause Ember to crash, therefore do check first
        If Not String.IsNullOrEmpty(efPath) Then
            Return Directory.GetParent(efPath).FullName
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrafanart
    ''' </summary>
    ''' <param name="mMovie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieExtrafanart(ByVal mMovie As Database.DBElement, ByVal sName As String) As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mMovie.Filename) Then Return efPath

        Dim doResize As Boolean = False
        If Master.eSettings.MovieExtrafanartsResize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            doResize = _image.Width > Master.eSettings.MovieExtrafanartsWidth OrElse _image.Height > Master.eSettings.MovieExtrafanartsHeight
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieExtrafanartsWidth, Master.eSettings.MovieExtrafanartsHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie, Enums.ModifierType.MainExtrafanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    If String.IsNullOrEmpty(sName) Then
                        iMod = Functions.GetExtrafanartsModifier(a)
                        iVal = iMod + 1
                        sName = Path.Combine(a, String.Concat("extrafanart", iVal, ".jpg"))
                    End If
                    efPath = Path.Combine(a, sName)
                    SaveToFile(efPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return efPath
    End Function
    ''' <summary>
    ''' Save all movie Extrathumbs
    ''' </summary>
    ''' <param name="mMovie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Shared Function SaveMovieExtrathumbs(ByVal mMovie As Database.DBElement) As String
        Dim etPath As String = String.Empty

        'First, (Down)Load all Extrathumbs from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In mMovie.ImagesContainer.Extrathumbs
            eImg.LoadAndCache(mMovie.ContentType, True)
        Next

        'Secound, remove the old ones
        Delete_Movie(mMovie, Enums.ModifierType.MainExtrathumbs, False)

        'Thirdly, save all Extrathumbs
        For Each eImg As MediaContainers.Image In mMovie.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
            If eImg.LoadAndCache(mMovie.ContentType, True) Then
                etPath = eImg.ImageOriginal.SaveAsMovieExtrathumb(mMovie)
            End If
        Next

        'If etPath is empty (i.e. expert setting enabled but expert extrathumb scraping disabled) it will cause Ember to crash, therefore do check first
        If Not String.IsNullOrEmpty(etPath) Then
            Return Directory.GetParent(etPath).FullName
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrathumb
    ''' </summary>
    ''' <param name="mMovie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieExtrathumb(ByVal mMovie As Database.DBElement) As String
        Dim etPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mMovie.Filename) Then Return etPath

        Dim doResize As Boolean = False
        If Master.eSettings.MovieExtrathumbsResize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            doResize = _image.Width > Master.eSettings.MovieExtrathumbsWidth OrElse _image.Height > Master.eSettings.MovieExtrathumbsHeight
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieExtrathumbsWidth, Master.eSettings.MovieExtrathumbsHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.Movie(mMovie, Enums.ModifierType.MainExtrathumbs)
                If Not String.IsNullOrEmpty(a) Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    iMod = Functions.GetExtrathumbsModifier(a)
                    iVal = iMod + 1
                    etPath = Path.Combine(a, String.Concat("thumb", iVal, ".jpg"))
                    SaveToFile(etPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return etPath
    End Function

    Public Shared Sub SaveTVEpisodeActorThumbs(ByVal mEpisode As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each tActor As MediaContainers.Person In mEpisode.TVEpisode.Actors
            tActor.Thumb.LoadAndCache(mEpisode.ContentType, True)
        Next

        'Secound, remove the old ones
        'Images.Delete_TVEpisode(mEpisode, Enums.ModifierType.EpisodeActorThumbs) 'TODO: find a way to only remove actor thumbs that not needed in other episodes with same actor thumbs path

        'Thirdly, save all actor thumbs
        For Each tActor As MediaContainers.Person In mEpisode.TVEpisode.Actors
            If tActor.Thumb.LoadAndCache(mEpisode.ContentType, True) Then
                tActor.Thumb.LocalFilePath = tActor.Thumb.ImageOriginal.SaveAsTVEpisodeActorThumb(mEpisode, tActor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="mEpisode"><c>Database.DBElement</c> representing the episode being referred to</param>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVEpisodeActorThumb(ByVal mEpisode As Database.DBElement, ByVal actor As MediaContainers.Person) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.TVEpisode(mEpisode, Enums.ModifierType.EpisodeActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            SaveToFile(tPath)
        Next

        Clear() 'Dispose to save memory
        Return tPath
    End Function

    Public Shared Sub SaveTVShowActorThumbs(ByVal mShow As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each tActor As MediaContainers.Person In mShow.TVShow.Actors
            tActor.Thumb.LoadAndCache(mShow.ContentType, True)
        Next

        'Secound, remove the old ones
        Images.Delete_TVShow(mShow, Enums.ModifierType.MainActorThumbs)

        'Thirdly, save all actor thumbs
        For Each tActor As MediaContainers.Person In mShow.TVShow.Actors
            If tActor.Thumb.LoadAndCache(mShow.ContentType, True) Then
                tActor.Thumb.LocalFilePath = tActor.Thumb.ImageOriginal.SaveAsTVShowActorThumb(mShow, tActor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="mShow"><c>Database.DBElement</c> representing the show being referred to</param>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowActorThumb(ByVal mShow As Database.DBElement, ByVal actor As MediaContainers.Person) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.GetFilenameList.TVShow(mShow, Enums.ModifierType.MainActorThumbs)
            tPath = a.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            SaveToFile(tPath)
        Next

        Clear() 'Dispose to save memory
        Return tPath
    End Function

    Public Shared Function SaveTVShowExtrafanarts(ByVal mShow As Database.DBElement) As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        'First, (Down)Load all Extrafanarts from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In mShow.ImagesContainer.Extrafanarts
            eImg.LoadAndCache(mShow.ContentType, True)
        Next

        'Secound, remove the old ones
        Images.Delete_TVShow(mShow, Enums.ModifierType.MainExtrafanarts)

        'Thirdly, save all Extrafanarts
        For Each eImg As MediaContainers.Image In mShow.ImagesContainer.Extrafanarts
            If eImg.LoadAndCache(mShow.ContentType, True) Then
                efPath = eImg.ImageOriginal.SaveAsTVShowExtrafanart(mShow, If(Not String.IsNullOrEmpty(eImg.URLOriginal), Path.GetFileName(eImg.URLOriginal), Path.GetFileName(eImg.LocalFilePath)))
            End If
        Next

        'If efPath is empty (i.e. expert setting enabled but expert extrafanart scraping disabled) it will cause Ember to crash, therefore do check first
        If Not String.IsNullOrEmpty(efPath) Then
            Return Directory.GetParent(efPath).FullName
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Save the image as a tv show's extrafanart
    ''' </summary>
    ''' <param name="mShow"><c>Database.DBElement</c> representing the TV Show being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowExtrafanart(ByVal mShow As Database.DBElement, ByVal sName As String) As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(mShow.ShowPath) Then Return efPath

        Dim doResize As Boolean = False
        If Master.eSettings.TVShowExtrafanartsResize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            doResize = _image.Width > Master.eSettings.TVShowExtrafanartsWidth OrElse _image.Height > Master.eSettings.TVShowExtrafanartsHeight
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVShowExtrafanartsWidth, Master.eSettings.TVShowExtrafanartsHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.GetFilenameList.TVShow(mShow, Enums.ModifierType.MainExtrafanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    If String.IsNullOrEmpty(sName) Then
                        iMod = Functions.GetExtrafanartsModifier(a)
                        iVal = iMod + 1
                        sName = Path.Combine(a, String.Concat("extrafanart", iVal, ".jpg"))
                    End If
                    efPath = Path.Combine(a, sName)
                    SaveToFile(efPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return efPath
    End Function

    Public Shared Function GetPreferredImagesContainer(ByVal DBElement As Database.DBElement,
                                            ByVal SearchResultsContainer As MediaContainers.SearchResultsContainer,
                                            ByVal ScrapeModifiers As Structures.ScrapeModifiers,
                                            Optional ByVal IsAutoScraper As Boolean = False) As MediaContainers.PreferredImagesContainer

        Dim nPreferredImagesContainer As New MediaContainers.PreferredImagesContainer

        Dim DoEpisodeFanart As Boolean
        Dim DoEpisodePoster As Boolean
        Dim DoMainBanner As Boolean
        Dim DoMainCharacterArt As Boolean
        Dim DoMainClearArt As Boolean
        Dim DoMainClearLogo As Boolean
        Dim DoMainDiscArt As Boolean
        Dim DoMainExtrafanarts As Boolean
        Dim DoMainExtrathumbs As Boolean
        Dim DoMainFanart As Boolean
        Dim DoMainLandscape As Boolean
        Dim DoMainPoster As Boolean
        Dim DoSeasonBanner As Boolean
        Dim DoSeasonFanart As Boolean
        Dim DoSeasonLandscape As Boolean
        Dim DoSeasonPoster As Boolean

        Dim tContentType As Enums.ContentType = DBElement.ContentType

        Select Case tContentType
            Case Enums.ContentType.Movie
                DoMainBanner = ScrapeModifiers.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled
                DoMainClearArt = ScrapeModifiers.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled
                DoMainClearLogo = ScrapeModifiers.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
                DoMainDiscArt = ScrapeModifiers.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled
                DoMainExtrafanarts = ScrapeModifiers.MainExtrafanarts AndAlso Master.eSettings.MovieExtrafanartsAnyEnabled
                DoMainExtrathumbs = ScrapeModifiers.MainExtrathumbs AndAlso Master.eSettings.MovieExtrathumbsAnyEnabled
                DoMainFanart = ScrapeModifiers.MainFanart AndAlso Master.eSettings.MovieFanartAnyEnabled
                DoMainLandscape = ScrapeModifiers.MainLandscape AndAlso Master.eSettings.MovieLandscapeAnyEnabled
                DoMainPoster = ScrapeModifiers.MainPoster AndAlso Master.eSettings.MoviePosterAnyEnabled
            Case Enums.ContentType.MovieSet
                DoMainBanner = ScrapeModifiers.MainBanner AndAlso Master.eSettings.MovieSetBannerAnyEnabled
                DoMainClearArt = ScrapeModifiers.MainClearArt AndAlso Master.eSettings.MovieSetClearArtAnyEnabled
                DoMainClearLogo = ScrapeModifiers.MainClearLogo AndAlso Master.eSettings.MovieSetClearLogoAnyEnabled
                DoMainDiscArt = ScrapeModifiers.MainDiscArt AndAlso Master.eSettings.MovieSetDiscArtAnyEnabled
                DoMainFanart = ScrapeModifiers.MainFanart AndAlso Master.eSettings.MovieSetFanartAnyEnabled
                DoMainLandscape = ScrapeModifiers.MainLandscape AndAlso Master.eSettings.MovieSetLandscapeAnyEnabled
                DoMainPoster = ScrapeModifiers.MainPoster AndAlso Master.eSettings.MovieSetPosterAnyEnabled
            Case Enums.ContentType.TVEpisode
                DoMainFanart = ScrapeModifiers.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                DoMainPoster = ScrapeModifiers.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
            Case Enums.ContentType.TVSeason
                DoMainBanner = (ScrapeModifiers.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled) OrElse (ScrapeModifiers.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled)
                DoMainFanart = (ScrapeModifiers.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled) OrElse (ScrapeModifiers.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled)
                DoMainLandscape = (ScrapeModifiers.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled) OrElse (ScrapeModifiers.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled)
                DoMainPoster = (ScrapeModifiers.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled) OrElse (ScrapeModifiers.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled)
            Case Enums.ContentType.TVShow
                DoEpisodeFanart = ScrapeModifiers.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                DoEpisodePoster = ScrapeModifiers.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
                DoMainBanner = ScrapeModifiers.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                DoMainCharacterArt = ScrapeModifiers.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                DoMainClearArt = ScrapeModifiers.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                DoMainClearLogo = ScrapeModifiers.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                DoMainExtrafanarts = ScrapeModifiers.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
                DoMainFanart = ScrapeModifiers.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                DoMainLandscape = ScrapeModifiers.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                DoMainPoster = ScrapeModifiers.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
                DoSeasonBanner = ScrapeModifiers.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                DoSeasonFanart = ScrapeModifiers.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                DoSeasonLandscape = ScrapeModifiers.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                DoSeasonPoster = ScrapeModifiers.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
        End Select

        'Main Banner
        If DoMainBanner Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.MovieBannerKeepExisting Then
                        GetPreferredMovieBanner(SearchResultsContainer.MainBanners, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.MovieSetBannerKeepExisting Then
                        GetPreferredMovieSetBanner(SearchResultsContainer.MainBanners, defImg)
                    End If
                Case Enums.ContentType.TVSeason
                    If DBElement.TVSeason.Season = 999 Then
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsBannerKeepExisting Then
                            GetPreferredTVAllSeasonsBanner(SearchResultsContainer.SeasonBanners, SearchResultsContainer.MainBanners, defImg)
                        End If
                    Else
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVSeasonBannerKeepExisting Then
                            GetPreferredTVSeasonBanner(SearchResultsContainer.SeasonBanners, defImg, DBElement.TVSeason.Season)
                        End If
                    End If
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVShowBannerKeepExisting Then
                        GetPreferredTVShowBanner(SearchResultsContainer.MainBanners, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.Banner = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.Banner = DBElement.ImagesContainer.Banner
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.Banner = DBElement.ImagesContainer.Banner
        End If

        'Main CharacterArt
        If DoMainCharacterArt Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.CharacterArt.LocalFilePath) OrElse Not Master.eSettings.TVShowCharacterArtKeepExisting Then
                        GetPreferredTVShowCharacterArt(SearchResultsContainer.MainCharacterArts, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.CharacterArt = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.CharacterArt = DBElement.ImagesContainer.CharacterArt
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.CharacterArt = DBElement.ImagesContainer.CharacterArt
        End If

        'Main ClearArt
        If DoMainClearArt Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) OrElse Not Master.eSettings.MovieClearArtKeepExisting Then
                        GetPreferredMovieClearArt(SearchResultsContainer.MainClearArts, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) OrElse Not Master.eSettings.MovieSetClearArtKeepExisting Then
                        GetPreferredMovieSetClearArt(SearchResultsContainer.MainClearArts, defImg)
                    End If
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) OrElse Not Master.eSettings.TVShowClearArtKeepExisting Then
                        GetPreferredTVShowClearArt(SearchResultsContainer.MainClearArts, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.ClearArt = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.ClearArt = DBElement.ImagesContainer.ClearArt
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.ClearArt = DBElement.ImagesContainer.ClearArt
        End If

        'Main ClearLogo
        If DoMainClearLogo Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) OrElse Not Master.eSettings.MovieClearLogoKeepExisting Then
                        GetPreferredMovieClearLogo(SearchResultsContainer.MainClearLogos, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) OrElse Not Master.eSettings.MovieSetClearLogoKeepExisting Then
                        GetPreferredMovieSetClearLogo(SearchResultsContainer.MainClearLogos, defImg)
                    End If
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) OrElse Not Master.eSettings.TVShowClearLogoKeepExisting Then
                        GetPreferredTVShowClearLogo(SearchResultsContainer.MainClearLogos, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.ClearLogo = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.ClearLogo = DBElement.ImagesContainer.ClearLogo
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.ClearLogo = DBElement.ImagesContainer.ClearLogo
        End If

        'Main DiscArt
        If DoMainDiscArt Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.DiscArt.LocalFilePath) OrElse Not Master.eSettings.MovieDiscArtKeepExisting Then
                        GetPreferredMovieDiscArt(SearchResultsContainer.MainDiscArts, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.DiscArt.LocalFilePath) OrElse Not Master.eSettings.MovieSetDiscArtKeepExisting Then
                        GetPreferredMovieSetDiscArt(SearchResultsContainer.MainDiscArts, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.DiscArt = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.DiscArt = DBElement.ImagesContainer.DiscArt
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.DiscArt = DBElement.ImagesContainer.DiscArt
        End If

        'Main Fanart
        If DoMainFanart Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.MovieFanartKeepExisting Then
                        GetPreferredMovieFanart(SearchResultsContainer.MainFanarts, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.MovieSetFanartKeepExisting Then
                        GetPreferredMovieSetFanart(SearchResultsContainer.MainFanarts, defImg)
                    End If
                Case Enums.ContentType.TVEpisode
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVEpisodeFanartKeepExisting Then
                        GetPreferredTVEpisodeFanart(SearchResultsContainer.EpisodeFanarts, SearchResultsContainer.MainFanarts, defImg, DBElement.TVEpisode.Season, DBElement.TVEpisode.Episode)
                    End If
                Case Enums.ContentType.TVSeason
                    If DBElement.TVSeason.Season = 999 Then
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsFanartKeepExisting Then
                            GetPreferredTVAllSeasonsFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg)
                        End If
                    Else
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVSeasonFanartKeepExisting Then
                            GetPreferredTVSeasonFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg, DBElement.TVSeason.Season)
                        End If
                    End If
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVShowFanartKeepExisting Then
                        GetPreferredTVShowFanart(SearchResultsContainer.MainFanarts, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.Fanart = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.Fanart = DBElement.ImagesContainer.Fanart
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.Fanart = DBElement.ImagesContainer.Fanart
        End If

        'Main Extrafanarts
        If DoMainExtrafanarts Then
            Dim bKeepExisting As Boolean = False
            Dim iLimit As Integer = 0
            Dim iDifference As Integer = 0

            Select Case tContentType
                Case Enums.ContentType.Movie
                    bKeepExisting = Master.eSettings.MovieExtrafanartsKeepExisting
                    iLimit = Master.eSettings.MovieExtrafanartsLimit
                Case Enums.ContentType.TVShow
                    bKeepExisting = Master.eSettings.TVShowExtrafanartsKeepExisting
                    iLimit = Master.eSettings.TVShowExtrafanartsLimit
            End Select

            If Not bKeepExisting OrElse Not DBElement.ImagesContainer.Extrafanarts.Count >= iLimit OrElse iLimit = 0 Then
                iDifference = iLimit - DBElement.ImagesContainer.Extrafanarts.Count
                Dim defImgList As New List(Of MediaContainers.Image)
                Select Case tContentType
                    Case Enums.ContentType.Movie
                        GetPreferredMovieExtrafanarts(SearchResultsContainer.MainFanarts, defImgList, If(Not bKeepExisting, iLimit, iDifference), nPreferredImagesContainer.ImagesContainer.Fanart, IsAutoScraper:=IsAutoScraper)
                    Case Enums.ContentType.TVShow
                        ' If option RemoveDuplicateImages is activated, compare all scraped extrafanarts of the show against all current fanart (including season fanart)
                        ' for this put all fanart of a show in temporary container
                        Dim tmpCurrentShowImagesContainer As New MediaContainers.PreferredImagesContainer
                        'show fanart
                        tmpCurrentShowImagesContainer.ImagesContainer.Extrafanarts.Add(nPreferredImagesContainer.ImagesContainer.Fanart)
                        'season fanart
                        For Each season In DBElement.Seasons
                            tmpCurrentShowImagesContainer.ImagesContainer.Extrafanarts.Add(season.ImagesContainer.Fanart)
                        Next
                        GetPreferredTVShowExtrafanarts(SearchResultsContainer.MainFanarts, defImgList, If(Not bKeepExisting, iLimit, iDifference), tmpCurrentShowImagesContainer.ImagesContainer.Extrafanarts, IsAutoScraper:=IsAutoScraper)
                End Select

                If Not bKeepExisting Then
                    nPreferredImagesContainer.ImagesContainer.Extrafanarts = defImgList
                Else
                    nPreferredImagesContainer.ImagesContainer.Extrafanarts.AddRange(DBElement.ImagesContainer.Extrafanarts)
                    nPreferredImagesContainer.ImagesContainer.Extrafanarts.AddRange(defImgList)
                End If
            Else
                nPreferredImagesContainer.ImagesContainer.Extrafanarts = DBElement.ImagesContainer.Extrafanarts
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.Extrafanarts = DBElement.ImagesContainer.Extrafanarts
        End If

        'Main Extrathumbs
        If DoMainExtrathumbs Then
            Dim bKeepExisting As Boolean = False
            Dim iLimit As Integer = 0
            Dim iDifference As Integer = 0

            Select Case tContentType
                Case Enums.ContentType.Movie
                    bKeepExisting = Master.eSettings.MovieExtrathumbsKeepExisting
                    iLimit = Master.eSettings.MovieExtrathumbsLimit
            End Select

            If Not bKeepExisting OrElse Not DBElement.ImagesContainer.Extrathumbs.Count >= iLimit OrElse iLimit = 0 Then
                iDifference = iLimit - DBElement.ImagesContainer.Extrathumbs.Count
                Dim defImgList As New List(Of MediaContainers.Image)

                Select Case tContentType
                    Case Enums.ContentType.Movie
                        GetPreferredMovieExtrathumbs(SearchResultsContainer.MainFanarts, defImgList, If(Not bKeepExisting, iLimit, iDifference), nPreferredImagesContainer.ImagesContainer.Fanart, DBElement, IsAutoScraper:=IsAutoScraper)
                End Select

                If Not bKeepExisting Then
                    nPreferredImagesContainer.ImagesContainer.Extrathumbs = defImgList
                Else
                    nPreferredImagesContainer.ImagesContainer.Extrathumbs.AddRange(DBElement.ImagesContainer.Extrathumbs)
                    nPreferredImagesContainer.ImagesContainer.Extrathumbs.AddRange(defImgList)
                End If
            Else
                nPreferredImagesContainer.ImagesContainer.Extrathumbs = DBElement.ImagesContainer.Extrathumbs
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.Extrathumbs = DBElement.ImagesContainer.Extrathumbs
        End If

        'Main Landscape
        If DoMainLandscape Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.MovieLandscapeKeepExisting Then
                        GetPreferredMovieLandscape(SearchResultsContainer.MainLandscapes, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.MovieSetLandscapeKeepExisting Then
                        GetPreferredMovieSetLandscape(SearchResultsContainer.MainLandscapes, defImg)
                    End If
                Case Enums.ContentType.TVSeason
                    If DBElement.TVSeason.Season = 999 Then
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsLandscapeKeepExisting Then
                            GetPreferredTVAllSeasonsLandscape(SearchResultsContainer.SeasonLandscapes, SearchResultsContainer.MainLandscapes, defImg)
                        End If
                    Else
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVSeasonLandscapeKeepExisting Then
                            GetPreferredTVSeasonLandscape(SearchResultsContainer.SeasonLandscapes, defImg, DBElement.TVSeason.Season)
                        End If
                    End If
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVShowLandscapeKeepExisting Then
                        GetPreferredTVShowLandscape(SearchResultsContainer.MainLandscapes, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.Landscape = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.Landscape = DBElement.ImagesContainer.Landscape
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.Landscape = DBElement.ImagesContainer.Landscape
        End If

        'Main Poster
        If DoMainPoster Then
            Dim defImg As MediaContainers.Image = Nothing

            Select Case tContentType
                Case Enums.ContentType.Movie
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.MoviePosterKeepExisting Then
                        GetPreferredMoviePoster(SearchResultsContainer.MainPosters, defImg)
                    End If
                Case Enums.ContentType.MovieSet
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.MovieSetPosterKeepExisting Then
                        GetPreferredMovieSetPoster(SearchResultsContainer.MainPosters, defImg)
                    End If
                Case Enums.ContentType.TVEpisode
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVEpisodePosterKeepExisting Then
                        GetPreferredTVEpisodePoster(SearchResultsContainer.EpisodePosters, defImg, DBElement.TVEpisode.Season, DBElement.TVEpisode.Episode)
                    End If
                Case Enums.ContentType.TVSeason
                    If DBElement.TVSeason.Season = 999 Then
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsPosterKeepExisting Then
                            GetPreferredTVAllSeasonsPoster(SearchResultsContainer.SeasonPosters, SearchResultsContainer.MainPosters, defImg)
                        End If
                    Else
                        If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVSeasonPosterKeepExisting Then
                            GetPreferredTVSeasonPoster(SearchResultsContainer.SeasonPosters, defImg, DBElement.TVSeason.Season)
                        End If
                    End If
                Case Enums.ContentType.TVShow
                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVShowPosterKeepExisting Then
                        GetPreferredTVShowPoster(SearchResultsContainer.MainPosters, defImg)
                    End If
            End Select

            If defImg IsNot Nothing Then
                nPreferredImagesContainer.ImagesContainer.Poster = defImg
            Else
                nPreferredImagesContainer.ImagesContainer.Poster = DBElement.ImagesContainer.Poster
            End If
        Else
            nPreferredImagesContainer.ImagesContainer.Poster = DBElement.ImagesContainer.Poster
        End If

        'Seasons while tv show scraping
        For Each sSeason As Database.DBElement In DBElement.Seasons
            Dim sContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Season = sSeason.TVSeason.Season}

            'Season Banner
            If DoSeasonBanner Then
                Dim defImg As MediaContainers.Image = Nothing
                If sSeason.TVSeason.Season = 999 Then
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsBannerKeepExisting Then
                        GetPreferredTVAllSeasonsBanner(SearchResultsContainer.SeasonBanners, SearchResultsContainer.MainBanners, defImg)
                    End If
                Else
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVSeasonBannerKeepExisting Then
                        GetPreferredTVSeasonBanner(SearchResultsContainer.SeasonBanners, defImg, sContainer.Season)
                    End If
                End If

                If defImg IsNot Nothing Then
                    sSeason.ImagesContainer.Banner = defImg
                    sContainer.Banner = defImg
                Else
                    sContainer.Banner = sSeason.ImagesContainer.Banner
                End If
            Else
                sContainer.Banner = sSeason.ImagesContainer.Banner
            End If

            'Season Fanart
            If DoSeasonFanart Then
                Dim defImg As MediaContainers.Image = Nothing
                If sSeason.TVSeason.Season = 999 Then
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsFanartKeepExisting Then
                        GetPreferredTVAllSeasonsFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg)
                    End If
                Else
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVSeasonFanartKeepExisting Then
                        GetPreferredTVSeasonFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg, sContainer.Season)
                    End If
                End If

                If defImg IsNot Nothing Then
                    sSeason.ImagesContainer.Fanart = defImg
                    sContainer.Fanart = defImg
                Else
                    sContainer.Fanart = sSeason.ImagesContainer.Fanart
                End If
            Else
                sContainer.Fanart = sSeason.ImagesContainer.Fanart
            End If

            'Season Landscape
            If DoSeasonLandscape Then
                Dim defImg As MediaContainers.Image = Nothing
                If sSeason.TVSeason.Season = 999 Then
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsLandscapeKeepExisting Then
                        GetPreferredTVAllSeasonsLandscape(SearchResultsContainer.SeasonLandscapes, SearchResultsContainer.MainLandscapes, defImg)
                    End If
                Else
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVSeasonLandscapeKeepExisting Then
                        GetPreferredTVSeasonLandscape(SearchResultsContainer.SeasonLandscapes, defImg, sContainer.Season)
                    End If
                End If

                If defImg IsNot Nothing Then
                    sSeason.ImagesContainer.Landscape = defImg
                    sContainer.Landscape = defImg
                Else
                    sContainer.Landscape = sSeason.ImagesContainer.Landscape
                End If
            Else
                sContainer.Landscape = sSeason.ImagesContainer.Landscape
            End If

            'Season Poster
            If DoSeasonPoster Then
                Dim defImg As MediaContainers.Image = Nothing
                If sSeason.TVSeason.Season = 999 Then
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsPosterKeepExisting Then
                        GetPreferredTVAllSeasonsPoster(SearchResultsContainer.SeasonPosters, SearchResultsContainer.MainPosters, defImg)
                    End If
                Else
                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVSeasonPosterKeepExisting Then
                        GetPreferredTVSeasonPoster(SearchResultsContainer.SeasonPosters, defImg, sContainer.Season)
                    End If
                End If

                If defImg IsNot Nothing Then
                    sSeason.ImagesContainer.Poster = defImg
                    sContainer.Poster = defImg
                Else
                    sContainer.Poster = sSeason.ImagesContainer.Poster
                End If
            Else
                sContainer.Poster = sSeason.ImagesContainer.Poster
            End If

            nPreferredImagesContainer.Seasons.Add(sContainer)
        Next

        'Episodes while tv show scraping
        For Each tEpisode As Database.DBElement In DBElement.Episodes
            Dim sContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Episode = tEpisode.TVEpisode.Episode, .Season = tEpisode.TVEpisode.Season}

            'Episode Fanart
            If DoEpisodeFanart Then
                Dim defImg As MediaContainers.Image = Nothing
                If String.IsNullOrEmpty(tEpisode.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVEpisodeFanartKeepExisting Then
                    GetPreferredTVEpisodeFanart(SearchResultsContainer.EpisodeFanarts, SearchResultsContainer.MainFanarts, defImg, sContainer.Season, sContainer.Episode)
                End If

                If defImg IsNot Nothing Then
                    tEpisode.ImagesContainer.Fanart = defImg
                    sContainer.Fanart = defImg
                Else
                    sContainer.Fanart = tEpisode.ImagesContainer.Fanart
                End If
            Else
                sContainer.Fanart = tEpisode.ImagesContainer.Fanart
            End If

            'Episode Poster
            If DoEpisodePoster Then
                Dim defImg As MediaContainers.Image = Nothing
                If String.IsNullOrEmpty(tEpisode.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVEpisodePosterKeepExisting Then
                    GetPreferredTVEpisodePoster(SearchResultsContainer.EpisodePosters, defImg, sContainer.Season, sContainer.Episode)
                End If

                If defImg IsNot Nothing Then
                    tEpisode.ImagesContainer.Poster = defImg
                    sContainer.Poster = defImg
                Else
                    sContainer.Poster = tEpisode.ImagesContainer.Poster
                End If
            Else
                sContainer.Poster = tEpisode.ImagesContainer.Poster
            End If

            nPreferredImagesContainer.Episodes.Add(sContainer)
        Next

        Return nPreferredImagesContainer
    End Function

    Public Shared Sub SetPreferredImages(ByRef DBElement As Database.DBElement,
                                         ByVal PreferredImagesContainer As MediaContainers.PreferredImagesContainer)

        If PreferredImagesContainer IsNot Nothing Then
            DBElement.ImagesContainer = PreferredImagesContainer.ImagesContainer
            For Each tSeason As Database.DBElement In DBElement.Seasons
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = PreferredImagesContainer.Seasons.FirstOrDefault(Function(f) f.Season = tSeason.TVSeason.Season)
                If prefImages IsNot Nothing Then
                    tSeason.ImagesContainer.Banner = prefImages.Banner
                    tSeason.ImagesContainer.Fanart = prefImages.Fanart
                    tSeason.ImagesContainer.Landscape = prefImages.Landscape
                    tSeason.ImagesContainer.Poster = prefImages.Poster
                End If
            Next
        End If
    End Sub

    Public Shared Sub SetPreferredImages(ByRef DBElement As Database.DBElement,
                                         ByVal SearchResultsContainer As MediaContainers.SearchResultsContainer,
                                         ByVal ScrapeModifiers As Structures.ScrapeModifiers,
                                         Optional ByVal IsAutoScraper As Boolean = False)

        Dim PreferredImagesContainer As MediaContainers.PreferredImagesContainer = GetPreferredImagesContainer(DBElement, SearchResultsContainer, ScrapeModifiers, IsAutoScraper)

        If PreferredImagesContainer IsNot Nothing Then

            'Main Images
            DBElement.ImagesContainer = PreferredImagesContainer.ImagesContainer

            'Season Images while tvshow scraping
            For Each tSeason As Database.DBElement In DBElement.Seasons
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = PreferredImagesContainer.Seasons.FirstOrDefault(Function(f) f.Season = tSeason.TVSeason.Season)
                If prefImages IsNot Nothing Then
                    tSeason.ImagesContainer.Banner = prefImages.Banner
                    tSeason.ImagesContainer.Fanart = prefImages.Fanart
                    tSeason.ImagesContainer.Landscape = prefImages.Landscape
                    tSeason.ImagesContainer.Poster = prefImages.Poster
                End If
            Next

            'Episode Images while tvshow scraping
            For Each tEpisode As Database.DBElement In DBElement.Episodes
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = PreferredImagesContainer.Episodes.FirstOrDefault(Function(f) f.Episode = tEpisode.TVEpisode.Episode AndAlso f.Season = tEpisode.TVEpisode.Season)
                If prefImages IsNot Nothing Then
                    tEpisode.ImagesContainer.Fanart = prefImages.Fanart
                    tEpisode.ImagesContainer.Poster = prefImages.Poster
                End If
            Next
        End If
    End Sub

    'Public Shared Sub SetDefaultImages(ByRef DBElement As Database.DBElement,
    '                                   ByRef DefaultImagesContainer As MediaContainers.ImagesContainer,
    '                                   ByRef SearchResultsContainer As MediaContainers.SearchResultsContainer,
    '                                   ByRef ScrapeModifiers As Structures.ScrapeModifiers,
    '                                   Optional ByRef DefaultSeasonImagesContainer As List(Of MediaContainers.EpisodeOrSeasonImagesContainer) = Nothing,
    '                                   Optional ByRef DefaultEpisodeImagesContainer As List(Of MediaContainers.EpisodeOrSeasonImagesContainer) = Nothing,
    '                                   Optional ByVal IsAutoScraper As Boolean = True)

    '    Dim DoEpisodeFanart As Boolean = False
    '    Dim DoEpisodePoster As Boolean = False
    '    Dim DoMainBanner As Boolean = False
    '    Dim DoMainCharacterArt As Boolean = False
    '    Dim DoMainClearArt As Boolean = False
    '    Dim DoMainClearLogo As Boolean = False
    '    Dim DoMainDiscArt As Boolean = False
    '    Dim DoMainExtrafanarts As Boolean = False
    '    Dim DoMainExtrathumbs As Boolean = False
    '    Dim DoMainFanart As Boolean = False
    '    Dim DoMainLandscape As Boolean = False
    '    Dim DoMainPoster As Boolean = False
    '    Dim DoSeasonBanner As Boolean = False
    '    Dim DoSeasonFanart As Boolean = False
    '    Dim DoSeasonLandscape As Boolean = False
    '    Dim DoSeasonPoster As Boolean = False

    '    Dim tContentType As Enums.ContentType = Enums.ContentType.None

    '    Select Case DBElement.ContentType
    '        Case Enums.ContentType.TVShow
    '            If ScrapeModifier.withEpisodes OrElse ScrapeModifier.withSeasons Then
    '                tContentType = Enums.ContentType.TV
    '            Else
    '                tContentType = Enums.ContentType.TVShow
    '            End If
    '        Case Else
    '            tContentType = DBElement.ContentType
    '    End Select

    '    Select Case tContentType
    '        Case Enums.ContentType.Movie
    '            DoMainBanner = ScrapeModifier.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled
    '            DoMainClearArt = ScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled
    '            DoMainClearLogo = ScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
    '            DoMainDiscArt = ScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled
    '            DoMainExtrafanarts = ScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.MovieExtrafanartsAnyEnabled
    '            DoMainExtrathumbs = ScrapeModifier.MainExtrathumbs AndAlso Master.eSettings.MovieExtrathumbsAnyEnabled
    '            DoMainFanart = ScrapeModifier.MainFanart AndAlso Master.eSettings.MovieFanartAnyEnabled
    '            DoMainLandscape = ScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieLandscapeAnyEnabled
    '            DoMainPoster = ScrapeModifier.MainPoster AndAlso Master.eSettings.MoviePosterAnyEnabled
    '        Case Enums.ContentType.MovieSet
    '            DoMainBanner = ScrapeModifier.MainBanner AndAlso Master.eSettings.MovieSetBannerAnyEnabled
    '            DoMainClearArt = ScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieSetClearArtAnyEnabled
    '            DoMainClearLogo = ScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieSetClearLogoAnyEnabled
    '            DoMainDiscArt = ScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieSetDiscArtAnyEnabled
    '            DoMainFanart = ScrapeModifier.MainFanart AndAlso Master.eSettings.MovieSetFanartAnyEnabled
    '            DoMainLandscape = ScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieSetLandscapeAnyEnabled
    '            DoMainPoster = ScrapeModifier.MainPoster AndAlso Master.eSettings.MovieSetPosterAnyEnabled
    '        Case Enums.ContentType.TV
    '            DoEpisodeFanart = ScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
    '            DoEpisodePoster = ScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
    '            DoMainBanner = ScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
    '            DoMainCharacterArt = ScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
    '            DoMainClearArt = ScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
    '            DoMainClearLogo = ScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
    '            DoMainExtrafanarts = ScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
    '            DoMainFanart = ScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
    '            DoMainLandscape = ScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
    '            DoMainPoster = ScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
    '            DoSeasonBanner = ScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
    '            DoSeasonFanart = ScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
    '            DoSeasonLandscape = ScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
    '            DoSeasonPoster = ScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
    '        Case Enums.ContentType.TVShow
    '            DoMainBanner = ScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
    '            DoMainCharacterArt = ScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
    '            DoMainClearArt = ScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
    '            DoMainClearLogo = ScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
    '            DoMainExtrafanarts = ScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
    '            DoMainFanart = ScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
    '            DoMainLandscape = ScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
    '            DoMainPoster = ScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
    '        Case Enums.ContentType.TVEpisode
    '            DoEpisodeFanart = ScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
    '            DoEpisodePoster = ScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
    '        Case Enums.ContentType.TVSeason
    '            DoSeasonBanner = (ScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled) OrElse (ScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled)
    '            DoSeasonFanart = (ScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled) OrElse (ScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled)
    '            DoSeasonLandscape = (ScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled) OrElse (ScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled)
    '            DoSeasonPoster = (ScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled) OrElse (ScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled)
    '    End Select

    '    'Remove duplicate fanart from image scraperresults
    '    If DoMainExtrafanarts OrElse DoMainExtrathumbs OrElse DoMainFanart Then
    '        'If process is autoscraper, then make sure that extraimages is not the same as main image of movie (i.e. fanart.jpg of movie should not be part of extrafanart)
    '        If IsAutoScraper AndAlso Master.eSettings.GeneralImageFilter AndAlso Master.eSettings.GeneralImageFilterAutoscraper Then
    '            RemoveDuplicateImages(SearchResultsContainer.MainFanarts, tContentType, DBElement.ImagesContainer.Fanart, MatchTolerance:=Master.eSettings.GeneralImageFilterFanartMatchTolerance)
    '        ElseIf Not IsAutoScraper AndAlso Master.eSettings.GeneralImageFilter AndAlso Master.eSettings.GeneralImageFilterImagedialog Then
    '            'only remove duplicates in the scraped imagelist, do not consider main image of movie (else current image of movie would not be selectable in image preview window!)
    '            RemoveDuplicateImages(SearchResultsContainer.MainFanarts, tContentType, MatchTolerance:=Master.eSettings.GeneralImageFilterFanartMatchTolerance)
    '        End If
    '    End If

    '    'Remove duplicate posters from image scraperresults
    '    If DoMainPoster Then
    '        'If process is autoscraper, then make sure that extraimages is not the same as main image of movie (i.e. fanart.jpg of movie should not be part of extrafanart)
    '        If IsAutoScraper AndAlso Master.eSettings.GeneralImageFilter AndAlso Master.eSettings.GeneralImageFilterAutoscraper Then
    '            RemoveDuplicateImages(SearchResultsContainer.MainPosters, tContentType, DBElement.ImagesContainer.Poster, MatchTolerance:=Master.eSettings.GeneralImageFilterPosterMatchTolerance)
    '        ElseIf Not IsAutoScraper AndAlso Master.eSettings.GeneralImageFilter AndAlso Master.eSettings.GeneralImageFilterImagedialog Then
    '            'only remove duplicates in the scraped imagelist, do not consider main image of movie (else current image of movie would not be selectable in image preview window!)
    '            RemoveDuplicateImages(SearchResultsContainer.MainPosters, tContentType, MatchTolerance:=Master.eSettings.GeneralImageFilterPosterMatchTolerance)
    '        End If
    '    End If

    '    'Main Banner
    '    If DoMainBanner OrElse DoSeasonBanner Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.MovieBannerKeepExisting Then
    '                    Images.GetPreferredMovieBanner(SearchResultsContainer.MainBanners, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.MovieSetBannerKeepExisting Then
    '                    Images.GetPreferredMovieSetBanner(SearchResultsContainer.MainBanners, defImg)
    '                End If
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVShowBannerKeepExisting Then
    '                    Images.GetPreferredTVShowBanner(SearchResultsContainer.MainBanners, defImg)
    '                End If
    '            Case Enums.ContentType.TVSeason
    '                If DBElement.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsBannerKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsBanner(SearchResultsContainer.SeasonBanners, SearchResultsContainer.MainBanners, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVSeasonBannerKeepExisting Then
    '                        Images.GetPreferredTVSeasonBanner(SearchResultsContainer.SeasonBanners, defImg, DBElement.TVSeason.Season)
    '                    End If
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.Banner = defImg
    '            DefaultImagesContainer.Banner = defImg
    '        Else
    '            DefaultImagesContainer.Banner = DBElement.ImagesContainer.Banner
    '        End If
    '    Else
    '        DefaultImagesContainer.Banner = DBElement.ImagesContainer.Banner
    '    End If

    '    'Main CharacterArt
    '    If DoMainCharacterArt Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.CharacterArt.LocalFilePath) OrElse Not Master.eSettings.TVShowCharacterArtKeepExisting Then
    '                    Images.GetPreferredTVShowCharacterArt(SearchResultsContainer.MainCharacterArts, defImg)
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.CharacterArt = defImg
    '            DefaultImagesContainer.CharacterArt = defImg
    '        Else
    '            DefaultImagesContainer.CharacterArt = DBElement.ImagesContainer.CharacterArt
    '        End If
    '    Else
    '        DefaultImagesContainer.CharacterArt = DBElement.ImagesContainer.CharacterArt
    '    End If

    '    'Main ClearArt
    '    If DoMainClearArt Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) OrElse Not Master.eSettings.MovieClearArtKeepExisting Then
    '                    Images.GetPreferredMovieClearArt(SearchResultsContainer.MainClearArts, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) OrElse Not Master.eSettings.MovieSetClearArtKeepExisting Then
    '                    Images.GetPreferredMovieSetClearArt(SearchResultsContainer.MainClearArts, defImg)
    '                End If
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearArt.LocalFilePath) OrElse Not Master.eSettings.TVShowClearArtKeepExisting Then
    '                    Images.GetPreferredTVShowClearArt(SearchResultsContainer.MainClearArts, defImg)
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.ClearArt = defImg
    '            DefaultImagesContainer.ClearArt = defImg
    '        Else
    '            DefaultImagesContainer.ClearArt = DBElement.ImagesContainer.ClearArt
    '        End If
    '    Else
    '        DefaultImagesContainer.ClearArt = DBElement.ImagesContainer.ClearArt
    '    End If

    '    'Main ClearLogo
    '    If DoMainClearLogo Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) OrElse Not Master.eSettings.MovieClearLogoKeepExisting Then
    '                    Images.GetPreferredMovieClearLogo(SearchResultsContainer.MainClearLogos, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) OrElse Not Master.eSettings.MovieSetClearLogoKeepExisting Then
    '                    Images.GetPreferredMovieSetClearLogo(SearchResultsContainer.MainClearLogos, defImg)
    '                End If
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.ClearLogo.LocalFilePath) OrElse Not Master.eSettings.TVShowClearLogoKeepExisting Then
    '                    Images.GetPreferredTVShowClearLogo(SearchResultsContainer.MainClearLogos, defImg)
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.ClearLogo = defImg
    '            DefaultImagesContainer.ClearLogo = defImg
    '        Else
    '            DefaultImagesContainer.ClearLogo = DBElement.ImagesContainer.ClearLogo
    '        End If
    '    Else
    '        DefaultImagesContainer.ClearLogo = DBElement.ImagesContainer.ClearLogo
    '    End If

    '    'Main DiscArt
    '    If DoMainDiscArt Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.DiscArt.LocalFilePath) OrElse Not Master.eSettings.MovieDiscArtKeepExisting Then
    '                    Images.GetPreferredMovieDiscArt(SearchResultsContainer.MainDiscArts, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.DiscArt.LocalFilePath) OrElse Not Master.eSettings.MovieSetDiscArtKeepExisting Then
    '                    Images.GetPreferredMovieSetDiscArt(SearchResultsContainer.MainDiscArts, defImg)
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.DiscArt = defImg
    '            DefaultImagesContainer.DiscArt = defImg
    '        Else
    '            DefaultImagesContainer.DiscArt = DBElement.ImagesContainer.DiscArt
    '        End If
    '    Else
    '        DefaultImagesContainer.DiscArt = DBElement.ImagesContainer.DiscArt
    '    End If

    '    'Main Extrafanarts
    '    If DoMainExtrafanarts Then
    '        Dim bKeepExisting As Boolean = False
    '        Dim iLimit As Integer = 0
    '        Dim iDifference As Integer = 0

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                bKeepExisting = Master.eSettings.MovieExtrafanartsKeepExisting
    '                iLimit = Master.eSettings.MovieExtrafanartsLimit
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                bKeepExisting = Master.eSettings.TVShowExtrafanartsKeepExisting
    '                iLimit = Master.eSettings.TVShowExtrafanartsLimit
    '        End Select

    '        If Not bKeepExisting OrElse Not DBElement.ImagesContainer.Extrafanarts.Count >= iLimit OrElse iLimit = 0 Then
    '            iDifference = iLimit - DBElement.ImagesContainer.Extrafanarts.Count
    '            Dim defImgList As New List(Of MediaContainers.Image)

    '            Select Case tContentType
    '                Case Enums.ContentType.Movie
    '                    Images.GetPreferredMovieExtrafanarts(SearchResultsContainer.MainFanarts, defImgList, If(Not bKeepExisting, iLimit, iDifference))
    '                Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                    Images.GetPreferredTVShowExtrafanarts(SearchResultsContainer.MainFanarts, defImgList, If(Not bKeepExisting, iLimit, iDifference))
    '            End Select

    '            If Not bKeepExisting Then
    '                DBElement.ImagesContainer.Extrafanarts = defImgList
    '                DefaultImagesContainer.Extrafanarts = DBElement.ImagesContainer.Extrafanarts
    '            Else
    '                DBElement.ImagesContainer.Extrafanarts.AddRange(defImgList)
    '                DefaultImagesContainer.Extrafanarts.AddRange(DBElement.ImagesContainer.Extrafanarts)
    '            End If
    '        Else
    '            DefaultImagesContainer.Extrafanarts = DBElement.ImagesContainer.Extrafanarts
    '        End If
    '    Else
    '        DefaultImagesContainer.Extrafanarts = DBElement.ImagesContainer.Extrafanarts
    '    End If

    '    'Main Extrathumbs
    '    If DoMainExtrathumbs Then
    '        Dim bKeepExisting As Boolean = False
    '        Dim iLimit As Integer = 0
    '        Dim iDifference As Integer = 0

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                bKeepExisting = Master.eSettings.MovieExtrathumbsKeepExisting
    '                iLimit = Master.eSettings.MovieExtrathumbsLimit
    '        End Select

    '        If Not bKeepExisting OrElse Not DBElement.ImagesContainer.Extrathumbs.Count >= iLimit OrElse iLimit = 0 Then
    '            iDifference = iLimit - DBElement.ImagesContainer.Extrathumbs.Count
    '            Dim defImgList As New List(Of MediaContainers.Image)

    '            Select Case tContentType
    '                Case Enums.ContentType.Movie
    '                    Images.GetPreferredMovieExtrathumbs(SearchResultsContainer.MainFanarts, defImgList, If(Not bKeepExisting, iLimit, iDifference))
    '            End Select

    '            If Not bKeepExisting Then
    '                DBElement.ImagesContainer.Extrathumbs = (defImgList)
    '                DefaultImagesContainer.Extrathumbs = (DBElement.ImagesContainer.Extrathumbs)
    '            Else
    '                DBElement.ImagesContainer.Extrathumbs.AddRange(defImgList)
    '                DefaultImagesContainer.Extrathumbs.AddRange(DBElement.ImagesContainer.Extrathumbs)
    '            End If
    '        Else
    '            DefaultImagesContainer.Extrathumbs = DBElement.ImagesContainer.Extrathumbs
    '        End If
    '    Else
    '        DefaultImagesContainer.Extrathumbs = DBElement.ImagesContainer.Extrathumbs
    '    End If

    '    'Main Fanart
    '    If DoMainFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.MovieFanartKeepExisting Then
    '                    Images.GetPreferredMovieFanart(SearchResultsContainer.MainFanarts, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.MovieSetFanartKeepExisting Then
    '                    Images.GetPreferredMovieSetFanart(SearchResultsContainer.MainFanarts, defImg)
    '                End If
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVShowFanartKeepExisting Then
    '                    Images.GetPreferredTVShowFanart(SearchResultsContainer.MainFanarts, defImg)
    '                End If
    '            Case Enums.ContentType.TVEpisode
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVEpisodeFanartKeepExisting Then
    '                    Images.GetPreferredTVEpisodeFanart(SearchResultsContainer.EpisodeFanarts, SearchResultsContainer.MainFanarts, defImg, DBElement.TVEpisode.Season, DBElement.TVEpisode.Episode)
    '                End If
    '            Case Enums.ContentType.TVSeason
    '                If DBElement.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsFanartKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVSeasonFanartKeepExisting Then
    '                        Images.GetPreferredTVSeasonFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg, DBElement.TVSeason.Season)
    '                    End If
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.Fanart = defImg
    '            DefaultImagesContainer.Fanart = defImg
    '        Else
    '            DefaultImagesContainer.Fanart = DBElement.ImagesContainer.Fanart
    '        End If
    '    Else
    '        DefaultImagesContainer.Fanart = DBElement.ImagesContainer.Fanart
    '    End If

    '    'Main Landscape
    '    If DoMainLandscape OrElse DoSeasonLandscape Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.MovieLandscapeKeepExisting Then
    '                    Images.GetPreferredMovieLandscape(SearchResultsContainer.MainLandscapes, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.MovieSetLandscapeKeepExisting Then
    '                    Images.GetPreferredMovieSetLandscape(SearchResultsContainer.MainLandscapes, defImg)
    '                End If
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVShowLandscapeKeepExisting Then
    '                    Images.GetPreferredTVShowLandscape(SearchResultsContainer.MainLandscapes, defImg)
    '                End If
    '            Case Enums.ContentType.TVSeason
    '                If DBElement.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsLandscapeKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsLandscape(SearchResultsContainer.SeasonLandscapes, SearchResultsContainer.MainLandscapes, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVSeasonLandscapeKeepExisting Then
    '                        Images.GetPreferredTVSeasonLandscape(SearchResultsContainer.SeasonLandscapes, defImg, DBElement.TVSeason.Season)
    '                    End If
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.Landscape = defImg
    '            DefaultImagesContainer.Landscape = defImg
    '        Else
    '            DefaultImagesContainer.Landscape = DBElement.ImagesContainer.Landscape
    '        End If
    '    Else
    '        DefaultImagesContainer.Landscape = DBElement.ImagesContainer.Landscape
    '    End If

    '    'Main Poster
    '    If DoMainPoster OrElse DoEpisodePoster OrElse DoSeasonPoster Then
    '        Dim defImg As MediaContainers.Image = Nothing

    '        Select Case tContentType
    '            Case Enums.ContentType.Movie
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.MoviePosterKeepExisting Then
    '                    Images.GetPreferredMoviePoster(SearchResultsContainer.MainPosters, defImg)
    '                End If
    '            Case Enums.ContentType.MovieSet
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.MovieSetPosterKeepExisting Then
    '                    Images.GetPreferredMovieSetPoster(SearchResultsContainer.MainPosters, defImg)
    '                End If
    '            Case Enums.ContentType.TV, Enums.ContentType.TVShow
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVShowPosterKeepExisting Then
    '                    Images.GetPreferredTVShowPoster(SearchResultsContainer.MainPosters, defImg)
    '                End If
    '            Case Enums.ContentType.TVEpisode
    '                If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVEpisodePosterKeepExisting Then
    '                    Images.GetPreferredTVEpisodePoster(SearchResultsContainer.EpisodePosters, defImg, DBElement.TVEpisode.Season, DBElement.TVEpisode.Episode)
    '                End If
    '            Case Enums.ContentType.TVSeason
    '                If DBElement.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsPosterKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsPoster(SearchResultsContainer.SeasonPosters, SearchResultsContainer.MainPosters, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(DBElement.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVSeasonPosterKeepExisting Then
    '                        Images.GetPreferredTVSeasonPoster(SearchResultsContainer.SeasonPosters, defImg, DBElement.TVSeason.Season)
    '                    End If
    '                End If
    '        End Select

    '        If defImg IsNot Nothing Then
    '            DBElement.ImagesContainer.Poster = defImg
    '            DefaultImagesContainer.Poster = defImg
    '        Else
    '            DefaultImagesContainer.Poster = DBElement.ImagesContainer.Poster
    '        End If
    '    Else
    '        DefaultImagesContainer.Poster = DBElement.ImagesContainer.Poster
    '    End If

    '    'Episodes while tv show scraping
    '    If DefaultEpisodeImagesContainer IsNot Nothing Then
    '        For Each sEpisode As Database.DBElement In DBElement.Episodes
    '            Dim sContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Episode = sEpisode.TVEpisode.Episode, .Season = sEpisode.TVEpisode.Season}

    '            'Fanart
    '            If DoEpisodeFanart Then
    '                Dim defImg As MediaContainers.Image = Nothing
    '                If String.IsNullOrEmpty(sEpisode.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVEpisodeFanartKeepExisting Then
    '                    Images.GetPreferredTVEpisodeFanart(SearchResultsContainer.EpisodeFanarts, SearchResultsContainer.MainFanarts, defImg, sContainer.Season, sContainer.Episode)
    '                End If

    '                If defImg IsNot Nothing Then
    '                    sEpisode.ImagesContainer.Fanart = defImg
    '                    sContainer.Fanart = defImg
    '                Else
    '                    sContainer.Fanart = sEpisode.ImagesContainer.Fanart
    '                End If
    '            Else
    '                sContainer.Fanart = sEpisode.ImagesContainer.Fanart
    '            End If

    '            'Poster
    '            If DoEpisodePoster Then
    '                Dim defImg As MediaContainers.Image = Nothing
    '                If String.IsNullOrEmpty(sEpisode.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVEpisodePosterKeepExisting Then
    '                    Images.GetPreferredTVEpisodePoster(SearchResultsContainer.EpisodePosters, defImg, sContainer.Season, sContainer.Episode)
    '                End If

    '                If defImg IsNot Nothing Then
    '                    sEpisode.ImagesContainer.Poster = defImg
    '                    sContainer.Poster = defImg
    '                Else
    '                    sContainer.Poster = sEpisode.ImagesContainer.Poster
    '                End If
    '            Else
    '                sContainer.Poster = sEpisode.ImagesContainer.Poster
    '            End If

    '            DefaultEpisodeImagesContainer.Add(sContainer)
    '        Next
    '    End If

    '    'Seasons while tv show scraping
    '    If DefaultSeasonImagesContainer IsNot Nothing Then
    '        For Each sSeason As Database.DBElement In DBElement.Seasons
    '            Dim sContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Season = sSeason.TVSeason.Season}

    '            'Banner
    '            If DoSeasonBanner Then
    '                Dim defImg As MediaContainers.Image = Nothing
    '                If sSeason.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsBannerKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsBanner(SearchResultsContainer.SeasonBanners, SearchResultsContainer.MainBanners, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Banner.LocalFilePath) OrElse Not Master.eSettings.TVSeasonBannerKeepExisting Then
    '                        Images.GetPreferredTVSeasonBanner(SearchResultsContainer.SeasonBanners, defImg, sContainer.Season)
    '                    End If
    '                End If

    '                If defImg IsNot Nothing Then
    '                    sSeason.ImagesContainer.Banner = defImg
    '                    sContainer.Banner = defImg
    '                Else
    '                    sContainer.Banner = sSeason.ImagesContainer.Banner
    '                End If
    '            Else
    '                sContainer.Banner = sSeason.ImagesContainer.Banner
    '            End If

    '            'Fanart
    '            If DoSeasonFanart Then
    '                Dim defImg As MediaContainers.Image = Nothing
    '                If sSeason.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsFanartKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Fanart.LocalFilePath) OrElse Not Master.eSettings.TVSeasonFanartKeepExisting Then
    '                        Images.GetPreferredTVSeasonFanart(SearchResultsContainer.SeasonFanarts, SearchResultsContainer.MainFanarts, defImg, sContainer.Season)
    '                    End If
    '                End If

    '                If defImg IsNot Nothing Then
    '                    sSeason.ImagesContainer.Fanart = defImg
    '                    sContainer.Fanart = defImg
    '                Else
    '                    sContainer.Fanart = sSeason.ImagesContainer.Fanart
    '                End If
    '            Else
    '                sContainer.Fanart = sSeason.ImagesContainer.Fanart
    '            End If

    '            'Landscape
    '            If DoSeasonLandscape Then
    '                Dim defImg As MediaContainers.Image = Nothing
    '                If sSeason.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsLandscapeKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsLandscape(SearchResultsContainer.SeasonLandscapes, SearchResultsContainer.MainLandscapes, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Landscape.LocalFilePath) OrElse Not Master.eSettings.TVSeasonLandscapeKeepExisting Then
    '                        Images.GetPreferredTVSeasonLandscape(SearchResultsContainer.SeasonLandscapes, defImg, sContainer.Season)
    '                    End If
    '                End If

    '                If defImg IsNot Nothing Then
    '                    sSeason.ImagesContainer.Landscape = defImg
    '                    sContainer.Landscape = defImg
    '                Else
    '                    sContainer.Landscape = sSeason.ImagesContainer.Landscape
    '                End If
    '            Else
    '                sContainer.Landscape = sSeason.ImagesContainer.Landscape
    '            End If

    '            'Poster
    '            If DoSeasonPoster Then
    '                Dim defImg As MediaContainers.Image = Nothing
    '                If sSeason.TVSeason.Season = 999 Then
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVAllSeasonsPosterKeepExisting Then
    '                        Images.GetPreferredTVAllSeasonsPoster(SearchResultsContainer.SeasonPosters, SearchResultsContainer.MainPosters, defImg)
    '                    End If
    '                Else
    '                    If String.IsNullOrEmpty(sSeason.ImagesContainer.Poster.LocalFilePath) OrElse Not Master.eSettings.TVSeasonPosterKeepExisting Then
    '                        Images.GetPreferredTVSeasonPoster(SearchResultsContainer.SeasonPosters, defImg, sContainer.Season)
    '                    End If
    '                End If

    '                If defImg IsNot Nothing Then
    '                    sSeason.ImagesContainer.Poster = defImg
    '                    sContainer.Poster = defImg
    '                Else
    '                    sContainer.Poster = sSeason.ImagesContainer.Poster
    '                End If
    '            Else
    '                sContainer.Poster = sSeason.ImagesContainer.Poster
    '            End If

    '            DefaultSeasonImagesContainer.Add(sContainer)
    '        Next
    '    End If
    'End Sub
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

    Public Shared Function GetPreferredMovieClearArt(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieSetClearArt(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieClearLogo(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieSetClearLogo(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieDiscArt(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Fetch a list of preferred extraFanart
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extraFanart</param>
    ''' <returns><c>List</c> of image URLs that fit the preferred thumb size</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPreferredMovieExtrafanarts(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResultList As List(Of MediaContainers.Image), ByVal iLimit As Integer, ByVal CurrentMovieFanart As MediaContainers.Image, Optional ByVal IsAutoScraper As Boolean = False) As Boolean
        If ImageList.Count = 0 Then Return False

        Dim DoCalculateDuplicaImages As Boolean = False

        If Master.eSettings.GeneralImageFilter = True AndAlso Master.eSettings.GeneralImageFilterFanart = True AndAlso ((IsAutoScraper = True AndAlso Master.eSettings.GeneralImageFilterAutoscraper = True) OrElse (IsAutoScraper = False AndAlso Master.eSettings.GeneralImageFilterImagedialog = True)) Then
            DoCalculateDuplicaImages = True
        End If

        If Master.eSettings.MovieExtrafanartsPrefSize = Enums.MovieFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because method RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If (imgResultList Is Nothing OrElse imgResultList.Count < iLimit) AndAlso Master.eSettings.MovieExtrafanartsPrefSizeOnly Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) f.MovieFanartSize = Master.eSettings.MovieExtrafanartsPrefSize)
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If (imgResultList Is Nothing OrElse imgResultList.Count < iLimit) AndAlso Not Master.eSettings.MovieExtrafanartsPrefSizeOnly AndAlso Not Master.eSettings.MovieExtrafanartsPrefSize = Enums.MovieFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URLOriginal))
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If DoCalculateDuplicaImages = True Then
            'make sure that extraimages is not the same as main image of movie (i.e. fanart.jpg of movie should not be part of extrafanart)
            Dim lsttmpCurrentMovieFanarts As New List(Of MediaContainers.Image)
            lsttmpCurrentMovieFanarts.Add(CurrentMovieFanart)
            FindDuplicateImages(imgResultList, Enums.ContentType.Movie, CurrentImageList:=lsttmpCurrentMovieFanarts, MatchTolerance:=Master.eSettings.GeneralImageFilterFanartMatchTolerance, Limit:=iLimit)
        End If

        'If Master.eSettings.MovieFanartPrefSize = Enums.MovieFanartSize.Any Then
        '    imgResult = ImageList.First
        'End If

        'If imgResult Is Nothing Then
        '    imgResult = ImageList.Find(Function(f) f.MovieFanartSize = Master.eSettings.MovieFanartPrefSize)
        'End If

        'If imgResult Is Nothing AndAlso Not Master.eSettings.MovieFanartPrefSizeOnly AndAlso Not Master.eSettings.MovieFanartPrefSize = Enums.MovieFanartSize.Any Then
        '    imgResult = ImageList.First
        'End If

        If imgResultList IsNot Nothing AndAlso imgResultList.Count > 0 Then
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
    Public Shared Function GetPreferredMovieExtrathumbs(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResultList As List(Of MediaContainers.Image), ByVal iLimit As Integer, ByVal CurrentMovieFanart As MediaContainers.Image, ByVal DBElement As Database.DBElement, Optional ByVal IsAutoScraper As Boolean = False) As Boolean

        'Generate thumbnails?
        If (Master.eSettings.MovieExtrathumbsCreatorAutoThumbs OrElse (Master.eSettings.MovieExtrathumbsCreatorUseETasFA AndAlso imgResultList.Count = 0)) AndAlso iLimit > 0 Then
            imgResultList.Clear()
            imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(DBElement, ThumbCount:=iLimit, NoSpoilers:=Master.eSettings.MovieExtrathumbsCreatorNoSpoilers, Timeout:=30000)
            'dont't care about the following extrathumb PrefQuality setting(s) of scrapers since it doesn't make sense for frame extraction (we only respect the Keep and Limit setting and Resize options for extracted frames)
            'if it necessary to mix scraped fanarts with extracted frames then special handling is needed in this function but for now don't care about this
            Return True
        End If

        If ImageList.Count = 0 Then Return False
        Dim DoCalculateDuplicaImages As Boolean = False

        If Master.eSettings.GeneralImageFilter = True AndAlso Master.eSettings.GeneralImageFilterFanart = True AndAlso ((IsAutoScraper = True AndAlso Master.eSettings.GeneralImageFilterAutoscraper = True) OrElse (IsAutoScraper = False AndAlso Master.eSettings.GeneralImageFilterImagedialog = True)) Then
            DoCalculateDuplicaImages = True
        End If

        If Master.eSettings.MovieExtrathumbsPrefSize = Enums.MovieFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If (imgResultList Is Nothing OrElse imgResultList.Count < iLimit) AndAlso Master.eSettings.MovieExtrathumbsPrefSizeOnly Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) f.MovieFanartSize = Master.eSettings.MovieExtrathumbsPrefSize)
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If (imgResultList Is Nothing OrElse imgResultList.Count < iLimit) AndAlso Not Master.eSettings.MovieExtrathumbsPrefSizeOnly AndAlso Not Master.eSettings.MovieExtrathumbsPrefSize = Enums.MovieFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URLOriginal))
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        'Use Imagefilter?
        If DoCalculateDuplicaImages = True Then
            'make sure that extraimages is not the same as main image of movie (i.e. fanart.jpg of movie should not be part of extrafanart)
            Dim lsttmpCurrentMovieFanarts As New List(Of MediaContainers.Image)
            lsttmpCurrentMovieFanarts.Add(CurrentMovieFanart)
            FindDuplicateImages(imgResultList, Enums.ContentType.Movie, CurrentImageList:=lsttmpCurrentMovieFanarts, MatchTolerance:=Master.eSettings.GeneralImageFilterFanartMatchTolerance, Limit:=iLimit)
        End If

        If imgResultList IsNot Nothing AndAlso imgResultList.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieSetDiscArt(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieLandscape(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If ImageList.Count = 0 Then Return False

        imgResult = ImageList.First

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieSetLandscape(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
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

    Public Shared Function GetPreferredTVAllSeasonsBanner(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVAllSeasonsBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVBannerSize = Master.eSettings.TVAllSeasonsBannerPrefSize AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVAllSeasonsBannerPrefSizeOnly AndAlso Not Master.eSettings.TVAllSeasonsBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVAllSeasonsBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVBannerSize = Master.eSettings.TVAllSeasonsBannerPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVAllSeasonsBannerPrefSizeOnly AndAlso Not Master.eSettings.TVAllSeasonsBannerPrefSize = Enums.TVBannerSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVAllSeasonsFanart(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False


        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVAllSeasonsFanartPrefSize = Enums.TVPosterSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVAllSeasonsFanartPrefSize AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVAllSeasonsFanartPrefSizeOnly AndAlso Not Master.eSettings.TVAllSeasonsFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVAllSeasonsFanartPrefSize = Enums.TVPosterSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVFanartSize = Master.eSettings.TVAllSeasonsFanartPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVAllSeasonsFanartPrefSizeOnly AndAlso Not Master.eSettings.TVAllSeasonsFanartPrefSize = Enums.TVFanartSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVAllSeasonsLandscape(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
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

    Public Shared Function GetPreferredTVAllSeasonsPoster(ByRef SeasonImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image) As Boolean
        If SeasonImageList.Count = 0 AndAlso ShowImageList.Count = 0 Then Return False

        If Not SeasonImageList.Count = 0 Then
            If Master.eSettings.TVAllSeasonsPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If

            If imgResult Is Nothing Then
                imgResult = SeasonImageList.Find(Function(f) f.TVPosterSize = Master.eSettings.TVAllSeasonsPosterPrefSize AndAlso f.Season = 999)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVAllSeasonsPosterPrefSizeOnly AndAlso Not Master.eSettings.TVAllSeasonsPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = SeasonImageList.Find(Function(f) f.Season = 999)
            End If
        End If

        If Not ShowImageList.Count = 0 Then
            If imgResult Is Nothing AndAlso Master.eSettings.TVAllSeasonsPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = ShowImageList.First
            End If

            If imgResult Is Nothing Then
                imgResult = ShowImageList.Find(Function(f) f.TVPosterSize = Master.eSettings.TVAllSeasonsPosterPrefSize)
            End If

            If imgResult Is Nothing AndAlso Not Master.eSettings.TVAllSeasonsPosterPrefSizeOnly AndAlso Not Master.eSettings.TVAllSeasonsPosterPrefSize = Enums.TVPosterSize.Any Then
                imgResult = ShowImageList.First
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVEpisodeFanart(ByRef EpisodeImageList As List(Of MediaContainers.Image), ByRef ShowImageList As List(Of MediaContainers.Image), ByRef imgResult As MediaContainers.Image, ByVal iSeason As Integer, ByVal iEpisode As Integer) As Boolean
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

        If Master.eSettings.TVEpisodePosterPrefSize = Enums.TVEpisodePosterSize.Any Then
            imgResult = ImageList.Find(Function(f) f.Episode = iEpisode AndAlso f.Season = iSeason)
        End If

        If imgResult Is Nothing Then
            imgResult = ImageList.Find(Function(f) f.TVEpisodePosterSize = Master.eSettings.TVEpisodePosterPrefSize AndAlso f.Episode = iEpisode AndAlso f.Season = iSeason)
        End If

        If imgResult Is Nothing AndAlso Not Master.eSettings.TVEpisodePosterPrefSizeOnly AndAlso Not Master.eSettings.TVEpisodePosterPrefSize = Enums.TVEpisodePosterSize.Any Then
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

    Public Shared Function GetPreferredTVShowExtrafanarts(ByRef ImageList As List(Of MediaContainers.Image), ByRef imgResultList As List(Of MediaContainers.Image), ByVal iLimit As Integer, ByVal lsttmpCurrentTVSowFanarts As List(Of MediaContainers.Image), Optional ByVal IsAutoScraper As Boolean = False) As Boolean

        If ImageList.Count = 0 Then Return False

        Dim DoCalculateDuplicaImages As Boolean = False

        If Master.eSettings.GeneralImageFilter = True AndAlso Master.eSettings.GeneralImageFilterFanart = True AndAlso ((IsAutoScraper = True AndAlso Master.eSettings.GeneralImageFilterAutoscraper = True) OrElse (IsAutoScraper = False AndAlso Master.eSettings.GeneralImageFilterImagedialog = True)) Then
            DoCalculateDuplicaImages = True
        End If

        If Master.eSettings.TVShowExtrafanartsPrefSize = Enums.TVFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because method RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If (imgResultList Is Nothing OrElse imgResultList.Count < iLimit) AndAlso Master.eSettings.TVShowExtrafanartsPrefSizeOnly Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) f.TVFanartSize = Master.eSettings.TVShowExtrafanartsPrefSize)
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If (imgResultList Is Nothing OrElse imgResultList.Count < iLimit) AndAlso Not Master.eSettings.TVShowExtrafanartsPrefSizeOnly AndAlso Not Master.eSettings.TVShowExtrafanartsPrefSize = Enums.TVFanartSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not String.IsNullOrEmpty(f.URLOriginal))
                imgResultList.Add(img)
                'different handling if ImageFilter is activated: Dont't limit/cut images because RemoveDuplicateImages will make sure that only "iLimit" and unique images will be returned
                If DoCalculateDuplicaImages = False Then
                    iLimit -= 1
                    If iLimit = 0 Then Exit For
                End If
            Next
        End If

        If DoCalculateDuplicaImages = True Then
            'make sure that extraimages is not the same as main image of movie (i.e. fanart.jpg of movie should not be part of extrafanart)
            FindDuplicateImages(imgResultList, Enums.ContentType.Movie, CurrentImageList:=lsttmpCurrentTVSowFanarts, MatchTolerance:=Master.eSettings.GeneralImageFilterFanartMatchTolerance, Limit:=iLimit)
        End If

        If imgResultList IsNot Nothing AndAlso imgResultList.Count > 0 Then
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

    ''' <summary>
    ''' Find duplicate images in a given list of images
    ''' </summary>
    ''' <param name="ImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extraFanart</param>
    ''' <param name="CurrentImage">Optional: Current image of video file (i.e. fanart.jpg). This will ensure, that the list of results doesn't contain this image</param>
    ''' <param name="MatchTolerance">Optional: 0: 100% identical images, 0-5: most likely identical, >10: different images</param>
    ''' <param name="Limit">Optional: only return a specific number of unique images, 0: check all images for duplicates and return all avalaible images for the video (unique+duplicates)</param>
    ''' <param name="RemoveDuplicatesFromList">Optional: false: Limit parameter is not considered, do not remove images from the list, only set IsDuplicate property of duplicate images to true, false= remove duplicate images from list, so the returned list only contains unique (limit) images</param>
    ''' <returns>true: no errors, false: no images to compare</returns>
    ''' <remarks>
    ''' Find duplicate images in a given list of images and either mark them in list by setting IsDuplicate property or remove them from the list - behaviour depends on limit parameter
    ''' 2016/02/10 Cocotus - Supports now comparing against list of currentimages (like fanarts of tvshows (seasons)) - for that created new function SearchDuplicateImageinList
    ''' 2016/01/10 Cocotus - Optimized behaviour to support Limit parameter (will result in faster processing)
    ''' Basic usage: 
    ''' If RemoveDuplicatesFromList = false -> ALL images of movie/episode will be downloaded and checked for duplicates. Whole Imagelist will be returned (may also contain duplicate images). Duplicated images will have Image-property "IsDuplicate"=true set
    ''' If RemoveDuplicatesFromList = true -> "Limit"-images of movie/episode will be returned, meaning Imagelist.Count will never be bigger than Limit. Also the returned list will only contain unique images!
    ''' 2015/09/23 Cocotus - First implementation
    ''' Used to avoid duplicate images
    ''' </remarks>
    Public Shared Function FindDuplicateImages(ByRef ImageList As List(Of MediaContainers.Image), ByVal ContentType As Enums.ContentType, Optional ByVal CurrentImageList As List(Of MediaContainers.Image) = Nothing, Optional ByVal MatchTolerance As Integer = 5, Optional ByVal Limit As Integer = 0, Optional ByVal RemoveDuplicatesFromList As Boolean = True) As Boolean
        'if there are no images to compare, then leave immediately
        If ImageList.Count = 0 Then Return False

        'list which will be filled with "limit" unique images (i.e. if Limit=10 -> lstUniqueImages will have 10 unique images (at best)). This list won't be filled if RemoveDuplicates = false
        Dim lstLimitImages As New List(Of MediaContainers.Image)
        'used for storing calculated similarity value between two images(temp-parameter, won't be returned)
        Dim currentimagesimilarity As Integer = 0
        'a list of calculated similarity values between image pairs(temp-parameter, won't be returned)
        Dim lstCalculatedSimilarity As New List(Of Tuple(Of Integer, Integer))

        'check if we need to adjust limit parameter (i.e. if there aren't enough images)
        Select Case Limit
            Case 0
                Limit = ImageList.Count
            Case Is > ImageList.Count
                Limit = ImageList.Count
        End Select

        'If RemoveDuplicatesFromList = False then Limit parameter is not considered -> always go through whole list
        If RemoveDuplicatesFromList = False Then
            Limit = ImageList.Count
            'since the following algorithm marks duplicates from beginning of list and keeps the images instance which is lower in list, we reverse the list to place images in preferred language at the end of the list
            ImageList.Reverse()
        End If

        'current compared image in imagelist
        Dim tmpImage As Images = Nothing
        Dim lsttmpSimilarityresults As New List(Of Integer)

        'loop through every image scrapedlist
        For i = 0 To ImageList.Count - 1
            'only repeat until Limit-images are found
            If lstLimitImages.Count < Limit Then
                'To compare images for similarity we need to load them
                'Checking for similarity means we need to load images to compare the content! -> Need to download the scraped image
                'If the images aren't available in cache or stored local, download them
                If ImageList(i).LoadAndCache(ContentType, False, True) Then
                    If ImageList(i).ImageThumb IsNot Nothing AndAlso ImageList(i).ImageThumb.Image IsNot Nothing Then
                        tmpImage = ImageList(i).ImageThumb
                    ElseIf ImageList(i).ImageOriginal IsNot Nothing AndAlso ImageList(i).ImageOriginal.Image IsNot Nothing Then
                        tmpImage = ImageList(i).ImageOriginal
                    End If

                    '1. Step (Optional): Check if tmpimage is identical to (current) image(s) (i.e. fanart) of movie/shows!
                    currentimagesimilarity = SearchDuplicateImageinList(tmpImage, CurrentImageList, MatchTolerance)
                    If MatchTolerance >= currentimagesimilarity Then
                        logger.Trace("[FindDuplicateImages] Duplicate images found: Image1: " & ImageList.Item(i).URLOriginal & " Image2: Current image!")
                        If RemoveDuplicatesFromList = True Then
                            'investigate next image, start with next item in loop
                            Continue For
                        End If
                    End If
                    'if all images will be analyzed then store index of image in imagelist with calculacted Similarityvalue
                    If RemoveDuplicatesFromList = False Then
                        Dim newSimilarityvalue = Tuple.Create(i, currentimagesimilarity)
                        lstCalculatedSimilarity.Add(newSimilarityvalue)
                    End If

                    '2. Step: Calculate similarity for each image combination in imagelist (lstLimit or whole Imagelist depending on RemoveDuplicates parameter) - basically we compare each image to find out which images are identical to each other
                    If RemoveDuplicatesFromList = True Then
                        Dim IsUniqueImage As Boolean = True
                        Dim referenceimage As Images = Nothing
                        If lstLimitImages.Count > 0 Then
                            'check for invalid images (one of images is nothing?!)
                            If tmpImage Is Nothing OrElse tmpImage.Image Is Nothing Then
                                currentimagesimilarity = 99
                                'First image is nothing -> no need to compare anything!
                                logger.Warn("[FindDuplicateImages] Image 1 is nothing. Can't compare images! Image at Index: " & i)
                            Else
                                For j = 0 To lstLimitImages.Count - 1
                                    'To compare images for similarity we need to load them
                                    'Checking for similarity means we need to load images to compare the content! -> Need to download the scraped image
                                    'If the images aren't available in cache or stored local, download them
                                    referenceimage = Nothing
                                    If lstLimitImages(j).LoadAndCache(ContentType, False, True) Then
                                        If lstLimitImages(j).ImageThumb IsNot Nothing AndAlso lstLimitImages(j).ImageThumb.Image IsNot Nothing Then
                                            referenceimage = lstLimitImages(j).ImageThumb
                                        ElseIf lstLimitImages(j).ImageOriginal IsNot Nothing AndAlso lstLimitImages(j).ImageOriginal.Image IsNot Nothing Then
                                            referenceimage = lstLimitImages(j).ImageOriginal
                                        End If
                                    End If
                                    If referenceimage Is Nothing OrElse referenceimage.Image Is Nothing Then
                                        currentimagesimilarity = 99
                                        'Second (loaded) image is nothing -> no need to compare anything!
                                        logger.Warn("[FindDuplicateImages] Image 2 is nothing. Can't compare images! Image at Index: " & j)
                                    Else
                                        currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(tmpImage.Image, referenceimage.Image, ImageUtils.ImageComparison.Algorithm.AverageHash)
                                        'Combine with pHash?!
                                        'If currentimagesimilarity > MatchTolerance Then
                                        '    currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(referenceitem.Image, referenceimage.Item(j).Image, ImageUtils.ImageComparison.Algorithm.PHash)
                                        'End If
                                    End If
                                    If MatchTolerance >= currentimagesimilarity Then
                                        logger.Trace("[FindDuplicateImages] Duplicate images found: Image1: " & ImageList.Item(i).URLOriginal & " Image2: " & lstLimitImages(j).URLOriginal)
                                        IsUniqueImage = False
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        'finally add image to Limitimagelist (this list will contain only unique images) 
                        If IsUniqueImage = True Then
                            lstLimitImages.Add(ImageList(i))
                        End If
                    Else
                        For j = i + 1 To ImageList.Count - 1
                            'To compare images for similarity we need to load them
                            'Checking for similarity means we need to load images to compare the content! -> Need to download the scraped image
                            'If the images aren't available in cache or stored local, download them
                            Dim referenceimage As Images = Nothing
                            If ImageList(j).LoadAndCache(ContentType, False, True) Then
                                If ImageList(j).ImageThumb IsNot Nothing AndAlso ImageList(j).ImageThumb.Image IsNot Nothing Then
                                    referenceimage = ImageList(j).ImageThumb
                                ElseIf ImageList(j).ImageOriginal IsNot Nothing AndAlso ImageList(j).ImageOriginal.Image IsNot Nothing Then
                                    referenceimage = ImageList(j).ImageOriginal
                                End If
                            End If

                            If referenceimage Is Nothing OrElse referenceimage.Image Is Nothing Then
                                currentimagesimilarity = 99
                                'Second (loaded) image is nothing -> no need to compare anything!
                                logger.Warn("[FindDuplicateImages] Image 2 is nothing. Can't compare images! Image at Index: " & j)
                            Else
                                currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(tmpImage.Image, referenceimage.Image, ImageUtils.ImageComparison.Algorithm.AverageHash)
                                'Combine with pHash?!
                                'If currentimagesimilarity > MatchTolerance Then
                                '    currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(referenceitem.Image, referenceimage.Item(j).Image, ImageUtils.ImageComparison.Algorithm.PHash)
                                'End If
                            End If
                            If MatchTolerance >= currentimagesimilarity Then
                                logger.Trace("[FindDuplicateImages] Duplicate images found: Image1: " & ImageList.Item(i).URLOriginal & " Image2: " & ImageList.Item(j).URLOriginal)
                            End If
                            'stores index of image in imagelist with calculacted Similarityvalue
                            Dim newSimilarityvalue = Tuple.Create(i, currentimagesimilarity)
                            lstCalculatedSimilarity.Add(newSimilarityvalue)
                        Next
                    End If
                End If
            Else
                logger.Trace("[FindDuplicateImages] List of unique images contains " & lstLimitImages.Count & " images")
                Exit For
            End If
        Next

        '3. Step:
        'if RemoveDuplicatesFromList = false: mark duplicate image in imagelist at index calculated above
        'if RemoveDuplicatesFromList = true: just return lstUniqueImages instead of imagelist
        If RemoveDuplicatesFromList = True Then
            ImageList.Clear()
            ImageList.AddRange(lstLimitImages)
        Else
            'Sort Similaritylist by similarityvalue
            lstCalculatedSimilarity.Sort(Function(x, y) y.Item2.CompareTo(x.Item2))
            lstCalculatedSimilarity.Reverse()
            'logging used for debugging in tests
            'For Each calculatedimage In lstSimilarImages
            '    If calculatedimage.Item1 <= MatchTolerance Then
            '        logger.Trace("[RemoveDuplicateImages] Ignore image with MatchTolerance: " & calculatedimage.Item2 & " at Index: " & calculatedimage.Item1 & " Name: " & ImageList.Item(calculatedimage.Item1).URLOriginal)
            '    Else
            '        logger.Trace("[RemoveDuplicateImages] Keep image with MatchTolerance: " & calculatedimage.Item2 & " at Index: " & calculatedimage.Item1 & " Name: " & ImageList.Item(calculatedimage.Item1).URLOriginal)
            '    End If
            'Next
            logger.Trace("[FindDuplicateImages] Ignore all images with MatchTolerance less/equal then : " & MatchTolerance & "...")
            For i = ImageList.Count - 1 To 0 Step -1
                If lstCalculatedSimilarity.Any(Function(c) c.Item1 = i AndAlso c.Item2 <= MatchTolerance) Then
                    If i > 0 Then
                        logger.Trace("[FindDuplicateImages] Duplicate images found: Image1: " & ImageList.Item(i).URLOriginal & " Image2: " & ImageList.Item(i - 1).URLOriginal)
                    End If
                    'don't remove duplicate images directly, instead "mark" them as duplicate and handle filtering in following methods...
                    'ImageList.RemoveAt(i)
                    ImageList.Item(i).IsDuplicate = True
                End If
            Next
        End If

        If RemoveDuplicatesFromList = False Then
            'finished processing, reverse imagelist to put preferred languages back to top like its used to be
            ImageList.Reverse()
        End If

        Return True
    End Function

    ''' <summary>
    ''' Check if an image is in a given list of images
    ''' </summary>
    ''' <param name="referenceImage">Current image of video file (i.e. fanart.jpg) which should be checked</param>
    ''' <param name="searchImageList">Source <c>List</c> of <c>MediaContainers.Image</c> holding available extraFanart</param>
    ''' <param name="MatchTolerance">0: 100% identical images, 0-5: most likely identical, >10: different images</param>
    ''' <returns>similarity value of found duplicate image (result of image comparison)</returns>
    ''' <remarks>
    ''' 2016/02/10 Cocotus - First implementation
    ''' Refactored the massive FindDuplicateImages method...
    ''' </remarks>
    Public Shared Function SearchDuplicateImageinList(ByRef referenceImage As Images, ByVal searchImageList As List(Of MediaContainers.Image), ByVal MatchTolerance As Integer) As Integer

        Dim currentimagesimilarity As Integer = 99
        If searchImageList Is Nothing Then
            Return 99
        End If
        For Each searchImage In searchImageList
            If searchImage IsNot Nothing Then
                'invalid comparison (one of images is nothing?!)
                If referenceImage Is Nothing OrElse referenceImage.Image Is Nothing Then
                    currentimagesimilarity = 99
                    'image is Nothing -> no need to compare anything!
                    logger.Warn("[SearchDuplicateImageinList] Image is nothing. Can't compare images!")
                Else
                    If searchImage.LocalFilePathSpecified Then
                        currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(referenceImage.Image, searchImage.LocalFilePath, ImageUtils.ImageComparison.Algorithm.AverageHash)
                    ElseIf searchImage.ImageThumb IsNot Nothing Then
                        If searchImage.ImageThumb.Image IsNot Nothing Then
                            currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(referenceImage.Image, searchImage.ImageThumb.Image, ImageUtils.ImageComparison.Algorithm.AverageHash)
                        Else
                            currentimagesimilarity = 99
                            'image is Nothing -> no need to compare anything!
                            logger.Warn("[SearchDuplicateImageinList] searchImage is nothing. Can't compare images!")
                        End If
                    ElseIf searchImage.ImageOriginal IsNot Nothing Then
                        If searchImage.ImageOriginal.Image IsNot Nothing Then
                            currentimagesimilarity = ImageUtils.ImageComparison.GetSimilarity(referenceImage.Image, searchImage.ImageOriginal.Image, ImageUtils.ImageComparison.Algorithm.AverageHash)
                        Else
                            currentimagesimilarity = 99
                            'image is Nothing -> no need to compare anything!
                            logger.Warn("[SearchDuplicateImageinList] searchImage is nothing. Can't compare images!")
                        End If
                    Else
                        currentimagesimilarity = 99
                        'image is Nothing -> no need to compare anything!
                        logger.Warn("[SearchDuplicateImageinList] searchImage is nothing. Can't compare images!")
                    End If
                End If
                'check for duplicate
                If MatchTolerance >= currentimagesimilarity Then
                    logger.Trace("[SearchDuplicateImageinList] Duplicate images found: Image: " & searchImage.URLOriginal)
                    Return currentimagesimilarity
                End If
            End If
        Next
        Return currentimagesimilarity
    End Function
#End Region 'Methods

End Class