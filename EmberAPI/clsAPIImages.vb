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

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Methods"
    ''' <summary>
    ''' Replaces the internally-held image with the supplied image
    ''' </summary>
    ''' <param name="Image">The new <c>Image</c> to retain</param>
    ''' <remarks>
    ''' 2013/11/25 Dekker500 - Disposed old image before replacing
    ''' </remarks>
    Public Sub UpdateMSfromImg(Image As Image, Optional Quality As Integer = 100)
        Try
            Dim ICI As ImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg)

            Dim EncPars As EncoderParameters = New EncoderParameters(2)
            EncPars.Param(0) = New EncoderParameter(Encoder.RenderMethod, EncoderValue.RenderNonProgressive)
            EncPars.Param(1) = New EncoderParameter(Encoder.Quality, Quality)

            'Write the supplied image into the MemoryStream
            If Not _ms Is Nothing Then _ms.Dispose()
            _ms = New MemoryStream()
            Image.Save(_ms, ICI, EncPars)
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
    ''' <param name="Path"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Private Shared Sub Delete(ByVal Path As String)
        If Not String.IsNullOrEmpty(Path) Then
            Try
                File.Delete(Path)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Param: <" & Path & ">")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete all movie images with specified image type
    ''' </summary>
    ''' <param name="DBMovie"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_Movie(ByVal DBMovie As Database.DBElement,
                                   ByVal ImageType As Enums.ModifierType,
                                   ByVal ForceFileCleanup As Boolean)
        If Not DBMovie.FileItemSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBMovie, ImageType, ForceFileCleanup)
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBMovie.FileItem.FirstPathFromStack & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all movieset images with specified image type
    ''' </summary>
    ''' <param name="DBMovieSet"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_MovieSet(ByVal DBMovieSet As Database.DBElement,
                                      ByVal ImageType As Enums.ModifierType,
                                      Optional ByVal ForceOldTitle As Boolean = False)
        If Not DBMovieSet.MovieSet.TitleSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBMovieSet, ImageType, ForceOldTitle)
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
        If Not DBTVShow.ShowPathSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBTVShow, ImageType)
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
        If Not DBTVEpisode.FileItemSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBTVEpisode, ImageType)
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & DBTVEpisode.FileItem.FirstPathFromStack & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all tv season images with specified image type
    ''' </summary>
    ''' <param name="DBTVSeason"></param>
    ''' <param name="ImageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVSeason(ByVal DBTVSeason As Database.DBElement, ByVal ImageType As Enums.ModifierType)
        If Not DBTVSeason.ShowPathSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBTVSeason, ImageType)
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
        If Not DBTVShow.ShowPathSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBTVShow, ImageType)
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
    Public Shared Function DuplicateImages_Find(ByRef ImageList As List(Of MediaContainers.Image),
                                                ByVal ContentType As Enums.ContentType,
                                                Optional ByVal CurrentImageList As List(Of MediaContainers.Image) = Nothing,
                                                Optional ByVal MatchTolerance As Integer = 5,
                                                Optional ByVal Limit As Integer = 0,
                                                Optional ByVal RemoveDuplicatesFromList As Boolean = True) As Boolean
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
        If Not RemoveDuplicatesFromList Then
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
                    currentimagesimilarity = DuplicateImages_Search(tmpImage, CurrentImageList, MatchTolerance)
                    If MatchTolerance >= currentimagesimilarity Then
                        logger.Trace("[FindDuplicateImages] Duplicate images found: Image1: " & ImageList.Item(i).URLOriginal & " Image2: Current image!")
                        If RemoveDuplicatesFromList Then
                            'investigate next image, start with next item in loop
                            Continue For
                        End If
                    End If
                    'if all images will be analyzed then store index of image in imagelist with calculacted Similarityvalue
                    If Not RemoveDuplicatesFromList Then
                        Dim newSimilarityvalue = Tuple.Create(i, currentimagesimilarity)
                        lstCalculatedSimilarity.Add(newSimilarityvalue)
                    End If

                    '2. Step: Calculate similarity for each image combination in imagelist (lstLimit or whole Imagelist depending on RemoveDuplicates parameter) - basically we compare each image to find out which images are identical to each other
                    If RemoveDuplicatesFromList Then
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
                        If IsUniqueImage Then
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
        If RemoveDuplicatesFromList Then
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

        If Not RemoveDuplicatesFromList Then
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
    Public Shared Function DuplicateImages_Search(ByRef referenceImage As Images,
                                                  ByVal searchImageList As List(Of MediaContainers.Image),
                                                  ByVal MatchTolerance As Integer) As Integer

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
    ''' Select the single most preferred image
    ''' </summary>
    ''' <param name="ImageList"></param>
    ''' <param name="imgResult"></param>
    ''' <param name="contentType"></param>
    ''' <param name="ImageType"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredExtraImages(ByRef DBElement As Database.DBElement,
                                                    ByRef ImageList As List(Of MediaContainers.Image),
                                                    ByRef ImgResultList As List(Of MediaContainers.Image),
                                                    ByVal ImageType As Enums.ModifierType,
                                                    ByVal Limit As Integer,
                                                    Optional ByVal CurrentImageFileNames As List(Of String) = Nothing) As Boolean
        'reset state
        ImgResultList = New List(Of MediaContainers.Image)

        If CurrentImageFileNames Is Nothing Then CurrentImageFileNames = New List(Of String)

        Dim bPrefSizeOnly As Boolean
        Dim bVideoExtraction As Boolean
        Dim bVideoExtractionPref As Boolean
        Dim ePrefSize As Enums.ImageSize = Enums.ImageSize.Any

        Select Case DBElement.ContentType
            Case Enums.ContentType.Movie
                Select Case ImageType
                    Case Enums.ModifierType.MainExtrafanarts
                        bPrefSizeOnly = Master.eSettings.MovieExtrafanartsPrefSizeOnly
                        ePrefSize = Master.eSettings.MovieExtrafanartsPrefSize
                    Case Enums.ModifierType.MainExtrathumbs
                        bPrefSizeOnly = Master.eSettings.MovieExtrathumbsPrefSizeOnly
                        bVideoExtraction = Master.eSettings.MovieExtrathumbsVideoExtraction
                        bVideoExtractionPref = Master.eSettings.MovieExtrathumbsVideoExtractionPref
                        ePrefSize = Master.eSettings.MovieExtrathumbsPrefSize
                End Select
            Case Enums.ContentType.TVShow
                Select Case ImageType
                    Case Enums.ModifierType.MainExtrafanarts
                        bPrefSizeOnly = Master.eSettings.TVShowExtrafanartsPrefSizeOnly
                        ePrefSize = Master.eSettings.TVShowExtrafanartsPrefSize
                End Select
        End Select

        If bVideoExtractionPref Then
            ImgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(DBElement, Limit, 30000)
            Limit -= ImgResultList.Count
        End If

        If ImgResultList.Count = 0 AndAlso ePrefSize = Enums.ImageSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not CurrentImageFileNames.Contains(Path.GetFileName(f.URLOriginal)))
                If Limit <= 0 Then Exit For
                ImgResultList.Add(img)
                Limit -= 1
            Next
        End If

        If ImgResultList.Count < Limit AndAlso bPrefSizeOnly Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) f.ImageSize = ePrefSize AndAlso
                                                                         Not CurrentImageFileNames.Contains(Path.GetFileName(f.URLOriginal)))
                If Limit <= 0 Then Exit For
                ImgResultList.Add(img)
                Limit -= 1
            Next
        End If

        If ImgResultList.Count < Limit AndAlso Not bPrefSizeOnly AndAlso Not ePrefSize = Enums.ImageSize.Any Then
            For Each img As MediaContainers.Image In ImageList.Where(Function(f) Not CurrentImageFileNames.Contains(Path.GetFileName(f.URLOriginal)))
                If Limit <= 0 Then Exit For
                ImgResultList.Add(img)
                Limit -= 1
            Next
        End If

        If Not Limit <= 0 AndAlso bVideoExtraction Then
            ImgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(DBElement, Limit, 30000)
        End If

        If Not ImgResultList.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred image
    ''' </summary>
    ''' <param name="ImageList"></param>
    ''' <param name="ImgResult"></param>
    ''' <param name="ContentType"></param>
    ''' <param name="ImageType"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredImage(ByRef ImageList As List(Of MediaContainers.Image),
                                              ByRef ImgResult As MediaContainers.Image,
                                              ByVal ContentType As Enums.ContentType,
                                              ByVal ImageType As Enums.ModifierType) As Boolean
        'reset state
        ImgResult = Nothing

        Dim nImageSettings As Settings.Helpers.ImageSettingSpecifications = Settings.Helpers.GetImageSettings(ContentType, ImageType)
        If nImageSettings Is Nothing Then Return False

        If nImageSettings.PrefSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.FirstOrDefault
        End If

        If ImgResult Is Nothing Then
            ImgResult = ImageList.Find(Function(f) f.ImageSize = nImageSettings.PrefSize)
        End If

        If ImgResult Is Nothing AndAlso Not nImageSettings.PrefSizeOnly AndAlso Not nImageSettings.PrefSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.FirstOrDefault
        End If

        If ImgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred image depending on season number
    ''' </summary>
    ''' <param name="ImageList"></param>
    ''' <param name="ImgResult"></param>
    ''' <param name="ContentType"></param>
    ''' <param name="ImageType"></param>
    ''' <param name="Season"></param>
    ''' <param name="ShowImageList"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredImage(ByRef ImageList As List(Of MediaContainers.Image),
                                              ByRef ImgResult As MediaContainers.Image,
                                              ByVal ContentType As Enums.ContentType,
                                              ByVal ImageType As Enums.ModifierType,
                                              ByVal Season As Integer,
                                              Optional ByRef ShowImageList As List(Of MediaContainers.Image) = Nothing) As Boolean
        'reset state
        ImgResult = Nothing

        Dim nImageSettings As Settings.Helpers.ImageSettingSpecifications = Settings.Helpers.GetImageSettings(ContentType, ImageType)
        If nImageSettings Is Nothing Then Return False

        If nImageSettings.PrefSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.Find(Function(f) f.Season = Season)
        End If

        If ImgResult Is Nothing Then
            ImgResult = ImageList.Find(Function(f) f.ImageSize = nImageSettings.PrefSize AndAlso f.Season = Season)
        End If

        If ImgResult Is Nothing AndAlso Not nImageSettings.PrefSizeOnly AndAlso Not nImageSettings.PrefSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.Find(Function(f) f.Season = Season)
        End If

        If ImgResult Is Nothing AndAlso ShowImageList IsNot Nothing Then
            GetPreferredImage(ShowImageList, ImgResult, ContentType, ImageType)
        End If

        If ImgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred image depending on season and episode number
    ''' </summary>
    ''' <param name="ImageList"></param>
    ''' <param name="ImgResult"></param>
    ''' <param name="contentType"></param>
    ''' <param name="ImageType"></param>
    ''' <param name="Season"></param>
    ''' <param name="Episode"></param>
    ''' <param name="ShowImageList"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredImage(ByRef DBElement As Database.DBElement,
                                              ByRef ImageList As List(Of MediaContainers.Image),
                                              ByRef ImgResult As MediaContainers.Image,
                                              ByVal ImageType As Enums.ModifierType,
                                              ByVal Season As Integer,
                                              ByVal Episode As Integer,
                                              Optional ByRef ShowImageList As List(Of MediaContainers.Image) = Nothing) As Boolean
        'reset state
        ImgResult = Nothing

        Dim nImageSettings As Settings.Helpers.ImageSettingSpecifications = Settings.Helpers.GetImageSettings(DBElement.ContentType, ImageType)
        If nImageSettings Is Nothing Then Return False

        If ImageType = Enums.ModifierType.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterVideoExtractionPref Then
            Dim imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(DBElement, 1, 30000)
            If Not imgResultList.Count = 0 Then
                ImgResult = imgResultList(0)
            End If
        End If

        If ImgResult Is Nothing AndAlso nImageSettings.PrefSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.Find(Function(f) f.Episode = Episode AndAlso f.Season = Season)
        End If

        If ImgResult Is Nothing Then
            ImgResult = ImageList.Find(Function(f) f.ImageSize = nImageSettings.PrefSize AndAlso f.Episode = Episode AndAlso f.Season = Season)
        End If

        If ImgResult Is Nothing AndAlso Not nImageSettings.PrefSizeOnly AndAlso Not nImageSettings.PrefSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.Find(Function(f) f.Episode = Episode AndAlso f.Season = Season)
        End If

        If ImgResult Is Nothing AndAlso ShowImageList IsNot Nothing Then
            GetPreferredImage(ShowImageList, ImgResult, DBElement.ContentType, ImageType)
        End If

        If ImgResult Is Nothing AndAlso ImageType = Enums.ModifierType.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterVideoExtraction Then
            Dim imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(DBElement, 1, 30000)
            If Not imgResultList.Count = 0 Then
                ImgResult = imgResultList(0)
            End If
        End If

        If ImgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredImagesContainer(ByVal DBElement As Database.DBElement,
                                                       ByVal SearchResultsContainer As MediaContainers.SearchResultsContainer,
                                                       ByVal ScrapeModifiers As Structures.ScrapeModifiers) As MediaContainers.PreferredImagesContainer

        Dim nPreferredImagesContainer As New MediaContainers.PreferredImagesContainer
        Dim tContentType As Enums.ContentType = DBElement.ContentType

        'set all old images as default for the new preferred "ImagesContainer"
        'remark: don't copy the whole "ImagesContainer", otherwise the original "DBElement.ImagesContainer" will be overwritten
        'even if the preferred images has been discarded
        nPreferredImagesContainer.ImagesContainer.Banner = DBElement.ImagesContainer.Banner
        nPreferredImagesContainer.ImagesContainer.CharacterArt = DBElement.ImagesContainer.CharacterArt
        nPreferredImagesContainer.ImagesContainer.ClearArt = DBElement.ImagesContainer.ClearArt
        nPreferredImagesContainer.ImagesContainer.ClearLogo = DBElement.ImagesContainer.ClearLogo
        nPreferredImagesContainer.ImagesContainer.DiscArt = DBElement.ImagesContainer.DiscArt
        nPreferredImagesContainer.ImagesContainer.Extrafanarts.AddRange(DBElement.ImagesContainer.Extrafanarts)
        nPreferredImagesContainer.ImagesContainer.Extrathumbs.AddRange(DBElement.ImagesContainer.Extrathumbs)
        nPreferredImagesContainer.ImagesContainer.Fanart = DBElement.ImagesContainer.Fanart
        nPreferredImagesContainer.ImagesContainer.KeyArt = DBElement.ImagesContainer.KeyArt
        nPreferredImagesContainer.ImagesContainer.Landscape = DBElement.ImagesContainer.Landscape
        nPreferredImagesContainer.ImagesContainer.Poster = DBElement.ImagesContainer.Poster

        For Each tImageType In GetImageTypeByScrapeModifiers(tContentType, ScrapeModifiers, DBElement.TVSeason)
            Dim prefImage As MediaContainers.Image = Nothing
            Dim currImage = DBElement.ImagesContainer.GetImageByType(tImageType)
            Dim nImageSettings = Settings.Helpers.GetImageSettings(tContentType, tImageType)
            If nImageSettings IsNot Nothing Then
                If currImage Is Nothing OrElse Not currImage.LocalFilePathSpecified OrElse Not nImageSettings.KeepExisting Then
                    Select Case tContentType
                        Case Enums.ContentType.TVEpisode
                            Select Case tImageType
                                Case Enums.ModifierType.EpisodeFanart
                                    GetPreferredImage(DBElement, SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tImageType, DBElement.TVEpisode.Season, DBElement.TVEpisode.Episode,
                                                      SearchResultsContainer.GetImagesByType(Enums.ModifierType.MainFanart))
                                Case Enums.ModifierType.EpisodePoster
                                    GetPreferredImage(DBElement, SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tImageType, DBElement.TVEpisode.Season, DBElement.TVEpisode.Episode)
                            End Select
                        Case Enums.ContentType.TVSeason
                            Select Case tImageType
                                Case Enums.ModifierType.AllSeasonsBanner
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, DBElement.TVSeason.Season,
                                                      SearchResultsContainer.GetImagesByType(Enums.ModifierType.MainBanner))
                                Case Enums.ModifierType.AllSeasonsFanart
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, DBElement.TVSeason.Season,
                                                      SearchResultsContainer.GetImagesByType(Enums.ModifierType.MainFanart))
                                Case Enums.ModifierType.AllSeasonsLandscape
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, DBElement.TVSeason.Season,
                                                      SearchResultsContainer.GetImagesByType(Enums.ModifierType.MainLandscape))
                                Case Enums.ModifierType.AllSeasonsPoster
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, DBElement.TVSeason.Season,
                                                      SearchResultsContainer.GetImagesByType(Enums.ModifierType.MainPoster))
                                Case Enums.ModifierType.SeasonBanner, Enums.ModifierType.SeasonLandscape, Enums.ModifierType.SeasonPoster
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, DBElement.TVSeason.Season)
                                Case Enums.ModifierType.SeasonFanart
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, DBElement.TVSeason.Season,
                                                      SearchResultsContainer.GetImagesByType(Enums.ModifierType.MainFanart))
                            End Select
                        Case Else
                            Select Case tImageType
                                Case Enums.ModifierType.MainBanner,
                                     Enums.ModifierType.MainCharacterArt,
                                     Enums.ModifierType.MainClearArt,
                                     Enums.ModifierType.MainClearLogo,
                                     Enums.ModifierType.MainDiscArt,
                                     Enums.ModifierType.MainFanart,
                                     Enums.ModifierType.MainKeyArt,
                                     Enums.ModifierType.MainLandscape,
                                     Enums.ModifierType.MainPoster
                                    GetPreferredImage(SearchResultsContainer.GetImagesByType(tImageType), prefImage, tContentType, tImageType)
                                Case Enums.ModifierType.MainExtrafanarts
                                    If Not nImageSettings.KeepExisting OrElse Not DBElement.ImagesContainer.Extrafanarts.Count >= nImageSettings.Limit OrElse nImageSettings.Limit = 0 Then
                                        Dim lstCurrentFileNames As New List(Of String)
                                        Dim iDifference As Integer = nImageSettings.Limit - DBElement.ImagesContainer.Extrafanarts.Count
                                        If nImageSettings.KeepExisting Then
                                            'Extrafanarts most time are saved with the same file name that the server use.
                                            'Use the file names of existing images to prevent to get the same images again
                                            'while we try fill the limit with new images.
                                            lstCurrentFileNames.AddRange(DBElement.ImagesContainer.GetImagesByType(tImageType).
                                                                         Cast(Of MediaContainers.Image)().
                                                                         Select(Function(f) Path.GetFileName(f.LocalFilePath)).ToList)
                                        End If
                                        Dim prefImageList As New List(Of MediaContainers.Image)
                                        GetPreferredExtraImages(DBElement, SearchResultsContainer.GetImagesByType(tImageType), prefImageList,
                                                                tImageType, If(Not nImageSettings.KeepExisting, nImageSettings.Limit, iDifference), lstCurrentFileNames)
                                        If nImageSettings.KeepExisting Then
                                            nPreferredImagesContainer.ImagesContainer.Extrafanarts.AddRange(prefImageList)
                                        Else
                                            nPreferredImagesContainer.ImagesContainer.Extrafanarts = prefImageList
                                        End If
                                    End If
                                Case Enums.ModifierType.MainExtrathumbs
                                    If Not nImageSettings.KeepExisting OrElse Not DBElement.ImagesContainer.Extrathumbs.Count >= nImageSettings.Limit OrElse nImageSettings.Limit = 0 Then
                                        Dim iDifference As Integer = nImageSettings.Limit - DBElement.ImagesContainer.Extrathumbs.Count
                                        Dim prefImageList As New List(Of MediaContainers.Image)
                                        GetPreferredExtraImages(DBElement, SearchResultsContainer.GetImagesByType(tImageType), prefImageList, tImageType, If(Not nImageSettings.KeepExisting, nImageSettings.Limit, iDifference))
                                        If nImageSettings.KeepExisting Then
                                            nPreferredImagesContainer.ImagesContainer.Extrathumbs.AddRange(prefImageList)
                                        Else
                                            nPreferredImagesContainer.ImagesContainer.Extrathumbs = prefImageList
                                        End If
                                    End If
                            End Select
                    End Select
                    If prefImage IsNot Nothing Then
                        nPreferredImagesContainer.ImagesContainer.SetImageByType(prefImage, tImageType)
                    End If
                End If
            End If
        Next

        'Seasons while tv show scraping
        For Each tSeason As Database.DBElement In DBElement.Seasons
            Dim nContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Season = tSeason.TVSeason.Season}
            Dim nSeasonResultsContainer = GetPreferredImagesContainer(tSeason, SearchResultsContainer, ScrapeModifiers)
            If nSeasonResultsContainer.ImagesContainer.BannerSpecified Then nContainer.Banner = nSeasonResultsContainer.ImagesContainer.Banner
            If nSeasonResultsContainer.ImagesContainer.FanartSpecified Then nContainer.Fanart = nSeasonResultsContainer.ImagesContainer.Fanart
            If nSeasonResultsContainer.ImagesContainer.LandscapeSpecified Then nContainer.Landscape = nSeasonResultsContainer.ImagesContainer.Landscape
            If nSeasonResultsContainer.ImagesContainer.PosterSpecified Then nContainer.Poster = nSeasonResultsContainer.ImagesContainer.Poster
            nPreferredImagesContainer.Seasons.Add(nContainer)
        Next

        'Episodes while tv show scraping
        For Each tEpisode As Database.DBElement In DBElement.Episodes.Where(Function(f) f.FileItemSpecified)
            Dim nContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Episode = tEpisode.TVEpisode.Episode, .Season = tEpisode.TVEpisode.Season}
            Dim nSeasonResultsContainer = GetPreferredImagesContainer(tEpisode, SearchResultsContainer, ScrapeModifiers)
            If nSeasonResultsContainer.ImagesContainer.BannerSpecified Then nContainer.Banner = nSeasonResultsContainer.ImagesContainer.Banner
            If nSeasonResultsContainer.ImagesContainer.FanartSpecified Then nContainer.Fanart = nSeasonResultsContainer.ImagesContainer.Fanart
            If nSeasonResultsContainer.ImagesContainer.LandscapeSpecified Then nContainer.Landscape = nSeasonResultsContainer.ImagesContainer.Landscape
            If nSeasonResultsContainer.ImagesContainer.PosterSpecified Then nContainer.Poster = nSeasonResultsContainer.ImagesContainer.Poster
            nPreferredImagesContainer.Episodes.Add(nContainer)
        Next

        Return nPreferredImagesContainer
    End Function
    ''' <summary>
    ''' Loads this Image from the contents of the supplied file
    ''' </summary>
    ''' <param name="Path">Path to the image file</param>
    ''' <param name="LoadBitmap">Create bitmap from memorystream</param>
    ''' <remarks></remarks>
    Public Sub LoadFromFile(ByVal Path As String, Optional LoadBitmap As Boolean = False)
        If Not String.IsNullOrEmpty(Path) Then
            Dim fiImage As FileInfo = Nothing
            Try
                fiImage = New FileInfo(Path)
            Catch ex As Exception
                logger.Error(String.Format("[APIImages] [LoadFromFile]: (0) ""(1)""", ex.Message, Path))
                Return
            End Try

            If Not fiImage.Exists Then
                logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" not found", Path))
                _ms = New MemoryStream
                _image = Nothing
            ElseIf fiImage.Length > 0 Then
                _ms = New MemoryStream()
                _image = Nothing
                Using fsImage As FileStream = File.OpenRead(Path)
                    Dim memStream As New MemoryStream
                    memStream.SetLength(fsImage.Length)
                    fsImage.Read(memStream.GetBuffer, 0, CInt(Fix(fsImage.Length)))
                    _ms.Write(memStream.GetBuffer, 0, CInt(Fix(fsImage.Length)))
                    _ms.Flush()
                    If _ms.Length > 0 Then
                        If LoadBitmap Then
                            _image = New Bitmap(_ms)
                        End If
                    Else
                        logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" is empty", Path))
                        _ms = New MemoryStream
                        _image = Nothing
                    End If
                End Using
            Else
                logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" is empty", Path))
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
    ''' <param name="URL">URL to the image file</param>
    ''' <param name="LoadBitmap">Create bitmap from memorystream</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal URL As String, Optional LoadBitmap As Boolean = False)
        If String.IsNullOrEmpty(URL) Then Return

        Try
            Dim sHTTP As New HTTP
            sHTTP.StartDownloadImage(URL)
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "<" & URL & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Stores the Image to the supplied <paramref name="PathToSave"/>
    ''' </summary>
    ''' <param name="PathToSave">Location to store the image</param>
    ''' <remarks></remarks>
    Public Sub SaveToFile(ByVal PathToSave As String)
        If _ms.Length > 0 Then
            Dim retSave() As Byte
            Try
                retSave = _ms.ToArray

                'make sure directory exists
                Directory.CreateDirectory(Directory.GetParent(PathToSave).FullName)
                Using fs As New FileStream(PathToSave, FileMode.Create, FileAccess.Write)
                    fs.Write(retSave, 0, retSave.Length)
                    fs.Flush()
                    fs.Close()
                End Using
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
    ''' <param name="DBElement"><c>Database.DBElement</c> representing the Movie being referred to</param>
    ''' <param name="ImageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal DBElement As Database.DBElement, ByVal ImageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim bNeedResize As Boolean
        Dim nImageSettings As Settings.Helpers.ImageSettingSpecifications = Settings.Helpers.GetImageSettings(DBElement.ContentType, ImageType)
        If nImageSettings Is Nothing Then Return String.Empty

        If nImageSettings.Resize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            If _image IsNot Nothing Then
                bNeedResize = _image.Width > nImageSettings.MaxWidth OrElse _image.Height > nImageSettings.MaxHeight
            End If
        End If

        Try
            If bNeedResize Then
                ImageUtils.ResizeImage(_image, nImageSettings.MaxWidth, nImageSettings.MaxHeight)
                'need to align _image and _ms
                UpdateMSfromImg(_image, nImageSettings.Quality)
            End If

            For Each a In FileUtils.FileNames.GetFileNames(DBElement, ImageType)
                SaveToFile(a)
                strReturn = a
            Next

            If DBElement.ContentType = Enums.ContentType.Movie AndAlso ImageType = Enums.ModifierType.MainFanart AndAlso Not String.IsNullOrEmpty(strReturn) AndAlso
                Not String.IsNullOrEmpty(Master.eSettings.MovieBackdropsPath) AndAlso Master.eSettings.MovieBackdropsAuto Then
                FileUtils.Common.CopyFanartToBackdropsPath(strReturn, DBElement.ContentType)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Shared Sub SaveMovieActorThumbs(ByVal Movie As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each tActor As MediaContainers.Person In Movie.Movie.Actors
            tActor.Thumb.LoadAndCache(Movie.ContentType, True)
        Next

        'Second, remove the old ones
        Delete_Movie(Movie, Enums.ModifierType.MainActorThumbs, False)

        'Thirdly, save all actor thumbs
        For Each tActor As MediaContainers.Person In Movie.Movie.Actors
            If tActor.Thumb.LoadAndCache(Movie.ContentType, True) Then
                tActor.Thumb.LocalFilePath = tActor.Thumb.ImageOriginal.SaveAsMovieActorThumb(Movie, tActor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="Movie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <param name="Actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieActorThumb(ByVal Movie As Database.DBElement, ByVal Actor As MediaContainers.Person) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.FileNames.GetFileNames(Movie, Enums.ModifierType.MainActorThumbs)
            tPath = a.Replace("<placeholder>", Actor.Name.Replace(" ", "_"))
            SaveToFile(tPath)
        Next

        Clear() 'Dispose to save memory
        Return tPath
    End Function
    ''' <summary>
    ''' Save all movie Extrafanarts
    ''' </summary>
    ''' <param name="Movie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Shared Function SaveMovieExtrafanarts(ByVal Movie As Database.DBElement) As String
        Dim efPath As String = String.Empty

        'First, (Down)Load all Extrafanarts from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In Movie.ImagesContainer.Extrafanarts
            eImg.LoadAndCache(Movie.ContentType, True)
        Next

        'Second, remove the old ones
        Delete_Movie(Movie, Enums.ModifierType.MainExtrafanarts, False)

        'Thirdly, save all Extrafanarts
        For Each eImg As MediaContainers.Image In Movie.ImagesContainer.Extrafanarts
            If eImg.LoadAndCache(Movie.ContentType, True) Then
                efPath = eImg.ImageOriginal.SaveAsMovieExtrafanart(Movie, If(Not String.IsNullOrEmpty(eImg.URLOriginal), Path.GetFileName(eImg.URLOriginal), Path.GetFileName(eImg.LocalFilePath)))
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
    ''' <param name="Movie"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieExtrafanart(ByVal Movie As Database.DBElement, ByVal Name As String) As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If Not Movie.FileItemSpecified Then Return String.Empty

        Dim doResize As Boolean
        If Master.eSettings.MovieExtrafanartsResize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            doResize = _image.Width > Master.eSettings.MovieExtrafanartsWidth OrElse _image.Height > Master.eSettings.MovieExtrafanartsHeight
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.MovieExtrafanartsWidth, Master.eSettings.MovieExtrafanartsHeight)
                'need to align _image and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.FileNames.GetFileNames(Movie, Enums.ModifierType.MainExtrafanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    If String.IsNullOrEmpty(Name) Then
                        iMod = Functions.GetExtrafanartsModifier(a)
                        iVal = iMod + 1
                        Name = Path.Combine(a, String.Concat("extrafanart", iVal, ".jpg"))
                    End If
                    efPath = Path.Combine(a, Name)
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
    ''' <param name="Movie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Shared Function SaveMovieExtrathumbs(ByVal Movie As Database.DBElement) As String
        Dim etPath As String = String.Empty

        'First, (Down)Load all Extrathumbs from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In Movie.ImagesContainer.Extrathumbs
            eImg.LoadAndCache(Movie.ContentType, True)
        Next

        'Secound, remove the old ones
        Delete_Movie(Movie, Enums.ModifierType.MainExtrathumbs, False)

        'Thirdly, save all Extrathumbs
        For Each eImg As MediaContainers.Image In Movie.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
            If eImg.LoadAndCache(Movie.ContentType, True) Then
                etPath = eImg.ImageOriginal.SaveAsMovieExtrathumb(Movie)
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
    ''' <param name="Movie"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsMovieExtrathumb(ByVal Movie As Database.DBElement) As String
        Dim etPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If Not Movie.FileItemSpecified Then Return String.Empty

        Dim doResize As Boolean
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

            For Each a In FileUtils.FileNames.GetFileNames(Movie, Enums.ModifierType.MainExtrathumbs)
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

    Public Shared Sub SaveTVEpisodeActorThumbs(ByVal Episode As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each tActor As MediaContainers.Person In Episode.TVEpisode.Actors
            tActor.Thumb.LoadAndCache(Episode.ContentType, True)
        Next

        'Secound, remove the old ones
        'Images.Delete_TVEpisode(mEpisode, Enums.ModifierType.EpisodeActorThumbs) 'TODO: find a way to only remove actor thumbs that not needed in other episodes with same actor thumbs path

        'Thirdly, save all actor thumbs
        For Each tActor As MediaContainers.Person In Episode.TVEpisode.Actors
            If tActor.Thumb.LoadAndCache(Episode.ContentType, True) Then
                tActor.Thumb.LocalFilePath = tActor.Thumb.ImageOriginal.SaveAsTVEpisodeActorThumb(Episode, tActor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="Episode"><c>Database.DBElement</c> representing the episode being referred to</param>
    ''' <param name="Actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVEpisodeActorThumb(ByVal Episode As Database.DBElement, ByVal Actor As MediaContainers.Person) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.FileNames.GetFileNames(Episode, Enums.ModifierType.EpisodeActorThumbs)
            tPath = a.Replace("<placeholder>", Actor.Name.Replace(" ", "_"))
            SaveToFile(tPath)
        Next

        Clear() 'Dispose to save memory
        Return tPath
    End Function

    Public Shared Sub SaveTVShowActorThumbs(ByVal Show As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each tActor As MediaContainers.Person In Show.TVShow.Actors
            tActor.Thumb.LoadAndCache(Show.ContentType, True)
        Next

        'Secound, remove the old ones
        Images.Delete_TVShow(Show, Enums.ModifierType.MainActorThumbs)

        'Thirdly, save all actor thumbs
        For Each tActor As MediaContainers.Person In Show.TVShow.Actors
            If tActor.Thumb.LoadAndCache(Show.ContentType, True) Then
                tActor.Thumb.LocalFilePath = tActor.Thumb.ImageOriginal.SaveAsTVShowActorThumb(Show, tActor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="Show"><c>Database.DBElement</c> representing the show being referred to</param>
    ''' <param name="Actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowActorThumb(ByVal Show As Database.DBElement, ByVal Actor As MediaContainers.Person) As String
        Dim tPath As String = String.Empty

        For Each a In FileUtils.FileNames.GetFileNames(Show, Enums.ModifierType.MainActorThumbs)
            tPath = a.Replace("<placeholder>", Actor.Name.Replace(" ", "_"))
            SaveToFile(tPath)
        Next

        Clear() 'Dispose to save memory
        Return tPath
    End Function

    Public Shared Function SaveTVShowExtrafanarts(ByVal Show As Database.DBElement) As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        'First, (Down)Load all Extrafanarts from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In Show.ImagesContainer.Extrafanarts
            eImg.LoadAndCache(Show.ContentType, True)
        Next

        'Secound, remove the old ones
        Images.Delete_TVShow(Show, Enums.ModifierType.MainExtrafanarts)

        'Thirdly, save all Extrafanarts
        For Each eImg As MediaContainers.Image In Show.ImagesContainer.Extrafanarts
            If eImg.LoadAndCache(Show.ContentType, True) Then
                efPath = eImg.ImageOriginal.SaveAsTVShowExtrafanart(Show, If(Not String.IsNullOrEmpty(eImg.URLOriginal), Path.GetFileName(eImg.URLOriginal), Path.GetFileName(eImg.LocalFilePath)))
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
    ''' <param name="Show"><c>Database.DBElement</c> representing the TV Show being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function SaveAsTVShowExtrafanart(ByVal Show As Database.DBElement, ByVal Name As String) As String
        Dim efPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(Show.ShowPath) Then Return efPath

        Dim doResize As Boolean
        If Master.eSettings.TVShowExtrafanartsResize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            doResize = _image.Width > Master.eSettings.TVShowExtrafanartsWidth OrElse _image.Height > Master.eSettings.TVShowExtrafanartsHeight
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.TVShowExtrafanartsWidth, Master.eSettings.TVShowExtrafanartsHeight)
                'need to align _image and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.FileNames.GetFileNames(Show, Enums.ModifierType.MainExtrafanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    If String.IsNullOrEmpty(Name) Then
                        iMod = Functions.GetExtrafanartsModifier(a)
                        iVal = iMod + 1
                        Name = Path.Combine(a, String.Concat("extrafanart", iVal, ".jpg"))
                    End If
                    efPath = Path.Combine(a, Name)
                    SaveToFile(efPath)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return efPath
    End Function

    Public Shared Sub SetPreferredImages(ByRef DBElement As Database.DBElement,
                                         ByVal PreferredImagesContainer As MediaContainers.PreferredImagesContainer)

        If PreferredImagesContainer IsNot Nothing Then
            DBElement.ImagesContainer = PreferredImagesContainer.ImagesContainer
            'Season Images
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
                                         ByVal ScrapeModifiers As Structures.ScrapeModifiers)

        Dim PreferredImagesContainer As MediaContainers.PreferredImagesContainer = GetPreferredImagesContainer(DBElement, SearchResultsContainer, ScrapeModifiers)

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

#End Region 'Methods

#Region "Nested Types"

    Private Shared Function GetImageTypeByScrapeModifiers(ByVal ContentType As Enums.ContentType,
                                                          ByVal Options As Structures.ScrapeModifiers,
                                                          ByVal TVSeason As MediaContainers.SeasonDetails) As List(Of Enums.ModifierType)
        Dim lstModTypes As New List(Of Enums.ModifierType)
        With Options
            Select Case ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.Movieset, Enums.ContentType.TVShow
                    If .MainBanner Then lstModTypes.Add(Enums.ModifierType.MainBanner)
                    If .MainCharacterArt Then lstModTypes.Add(Enums.ModifierType.MainCharacterArt)
                    If .MainClearArt Then lstModTypes.Add(Enums.ModifierType.MainClearArt)
                    If .MainClearLogo Then lstModTypes.Add(Enums.ModifierType.MainClearLogo)
                    If .MainDiscArt Then lstModTypes.Add(Enums.ModifierType.MainDiscArt)
                    If .MainExtrafanarts Then lstModTypes.Add(Enums.ModifierType.MainExtrafanarts)
                    If .MainExtrathumbs Then lstModTypes.Add(Enums.ModifierType.MainExtrathumbs)
                    If .MainFanart Then lstModTypes.Add(Enums.ModifierType.MainFanart)
                    If .MainKeyArt Then lstModTypes.Add(Enums.ModifierType.MainKeyArt)
                    If .MainLandscape Then lstModTypes.Add(Enums.ModifierType.MainLandscape)
                    If .MainPoster Then lstModTypes.Add(Enums.ModifierType.MainPoster)
                Case Enums.ContentType.TVEpisode
                    If .EpisodeFanart Then lstModTypes.Add(Enums.ModifierType.EpisodeFanart)
                    If .EpisodePoster Then lstModTypes.Add(Enums.ModifierType.EpisodePoster)
                Case Enums.ContentType.TVSeason
                    If TVSeason IsNot Nothing Then
                        If TVSeason.IsAllSeasons Then
                            If .AllSeasonsBanner Then lstModTypes.Add(Enums.ModifierType.AllSeasonsBanner)
                            If .AllSeasonsFanart Then lstModTypes.Add(Enums.ModifierType.AllSeasonsFanart)
                            If .AllSeasonsLandscape Then lstModTypes.Add(Enums.ModifierType.AllSeasonsLandscape)
                            If .AllSeasonsPoster Then lstModTypes.Add(Enums.ModifierType.AllSeasonsPoster)
                        Else
                            If .SeasonBanner Then lstModTypes.Add(Enums.ModifierType.SeasonBanner)
                            If .SeasonFanart Then lstModTypes.Add(Enums.ModifierType.SeasonFanart)
                            If .SeasonLandscape Then lstModTypes.Add(Enums.ModifierType.SeasonLandscape)
                            If .SeasonPoster Then lstModTypes.Add(Enums.ModifierType.SeasonPoster)
                        End If
                    End If
            End Select
        End With
        Return lstModTypes
    End Function

#End Region

End Class