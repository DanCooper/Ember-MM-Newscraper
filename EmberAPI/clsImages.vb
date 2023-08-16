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
    ''' <param name="fullName"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Private Shared Sub Delete(ByVal fullName As String)
        If Not String.IsNullOrEmpty(fullName) Then
            Try
                File.Delete(fullName)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete all movie images with specified image type
    ''' </summary>
    ''' <param name="dbElement"></param>
    ''' <param name="imageType"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal dbElement As Database.DBElement,
                             ByVal imageType As Enums.ModifierType,
                             Optional ByVal forceFileCleanupOrOldTitle As Boolean = False)

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movieset
                If Not dbElement.MainDetails.TitleSpecified Then Return
            Case Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                If Not dbElement.ShowPathSpecified Then Return
            Case Else
                If Not dbElement.FilenameSpecified Then Return
        End Select

        Try
            For Each a In FileUtils.FileNames.GetFileNames(dbElement, imageType, forceFileCleanupOrOldTitle)
                Select Case imageType
                    Case Enums.ModifierType.MainActorThumbs, Enums.ModifierType.EpisodeActorThumbs
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
    Public Shared Function DuplicateImages_Find(ByRef ImageList As List(Of MediaContainers.Image), ByVal ContentType As Enums.ContentType, Optional ByVal CurrentImageList As List(Of MediaContainers.Image) = Nothing, Optional ByVal MatchTolerance As Integer = 5, Optional ByVal Limit As Integer = 0, Optional ByVal RemoveDuplicatesFromList As Boolean = True) As Boolean
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
                    currentimagesimilarity = DuplicateImages_Search(tmpImage, CurrentImageList, MatchTolerance)
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
    Public Shared Function DuplicateImages_Search(ByRef referenceImage As Images, ByVal searchImageList As List(Of MediaContainers.Image), ByVal MatchTolerance As Integer) As Integer

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
    ''' <param name="imageList"></param>
    ''' <param name="imgResult"></param>
    ''' <param name="contentType"></param>
    ''' <param name="imageType"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredExtraImages(ByRef dbElement As Database.DBElement,
                                                    ByRef imageList As List(Of MediaContainers.Image),
                                                    ByRef imgResultList As List(Of MediaContainers.Image),
                                                    ByVal imageType As Enums.ModifierType,
                                                    ByVal limit As Integer,
                                                    Optional ByVal currentImageFileNames As List(Of String) = Nothing) As Boolean
        'reset state
        imgResultList = New List(Of MediaContainers.Image)

        If currentImageFileNames Is Nothing Then currentImageFileNames = New List(Of String)

        Dim ImageSettings = Settings.Helpers.GetImageSpecificationItem(dbElement.ContentType, imageType)

        If ImageSettings.VideoExtractionPreferred Then
            imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(dbElement, limit, 30000)
            limit -= imgResultList.Count
        End If

        If imgResultList.Count = 0 AndAlso ImageSettings.PreferredSize = Enums.ImageSize.Any Then
            For Each img As MediaContainers.Image In imageList.Where(Function(f) Not currentImageFileNames.Contains(Path.GetFileName(f.URLOriginal)))
                If limit <= 0 Then Exit For
                imgResultList.Add(img)
                limit -= 1
            Next
        End If

        If imgResultList.Count < limit AndAlso ImageSettings.PreferredSizeOnly Then
            For Each img As MediaContainers.Image In imageList.Where(Function(f) f.ImageSize = ImageSettings.PreferredSize AndAlso
                                                                         Not currentImageFileNames.Contains(Path.GetFileName(f.URLOriginal)))
                If limit <= 0 Then Exit For
                imgResultList.Add(img)
                limit -= 1
            Next
        End If

        If imgResultList.Count < limit AndAlso Not ImageSettings.PreferredSizeOnly AndAlso Not ImageSettings.PreferredSize = Enums.ImageSize.Any Then
            For Each img As MediaContainers.Image In imageList.Where(Function(f) Not currentImageFileNames.Contains(Path.GetFileName(f.URLOriginal)))
                If limit <= 0 Then Exit For
                imgResultList.Add(img)
                limit -= 1
            Next
        End If

        If Not limit <= 0 AndAlso ImageSettings.VideoExtraction Then
            imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(dbElement, limit, 30000)
        End If

        If Not imgResultList.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Select the single most preferred image
    ''' </summary>
    ''' <param name="imageList"></param>
    ''' <param name="imgResult"></param>
    ''' <param name="contentType"></param>
    ''' <param name="imageType"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredImage(ByRef imageList As List(Of MediaContainers.Image),
                                              ByRef imgResult As MediaContainers.Image,
                                              ByVal contentType As Enums.ContentType,
                                              ByVal imageType As Enums.ModifierType) As Boolean
        'reset state
        imgResult = Nothing

        Dim nImageSettings As ImageItemBase = Settings.Helpers.GetImageSpecificationItem(contentType, imageType)
        If nImageSettings Is Nothing Then Return False

        If nImageSettings.PreferredSize = Enums.ImageSize.Any Then
            imgResult = imageList.FirstOrDefault
        End If

        If imgResult Is Nothing Then
            imgResult = imageList.Find(Function(f) f.ImageSize = nImageSettings.PreferredSize)
        End If

        If imgResult Is Nothing AndAlso Not nImageSettings.PreferredSizeOnly AndAlso Not nImageSettings.PreferredSize = Enums.ImageSize.Any Then
            imgResult = imageList.FirstOrDefault
        End If

        If imgResult IsNot Nothing Then
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

        Dim nImageSettings As ImageItemBase = Settings.Helpers.GetImageSpecificationItem(ContentType, ImageType)
        If nImageSettings Is Nothing Then Return False

        If nImageSettings.PreferredSize = Enums.ImageSize.Any Then
            ImgResult = ImageList.Find(Function(f) f.Season = Season)
        End If

        If ImgResult Is Nothing Then
            ImgResult = ImageList.Find(Function(f) f.ImageSize = nImageSettings.PreferredSize AndAlso f.Season = Season)
        End If

        If ImgResult Is Nothing AndAlso Not nImageSettings.PreferredSizeOnly AndAlso Not nImageSettings.PreferredSize = Enums.ImageSize.Any Then
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
    ''' <param name="imageList"></param>
    ''' <param name="imgResult"></param>
    ''' <param name="contentType"></param>
    ''' <param name="imageType"></param>
    ''' <param name="season"></param>
    ''' <param name="episode"></param>
    ''' <param name="showImageList"></param>
    ''' <returns></returns>
    Private Shared Function GetPreferredImage(ByRef dbElement As Database.DBElement,
                                              ByRef imageList As List(Of MediaContainers.Image),
                                              ByRef imgResult As MediaContainers.Image,
                                              ByVal imageType As Enums.ModifierType,
                                              ByVal season As Integer,
                                              ByVal episode As Integer,
                                              Optional ByRef showImageList As List(Of MediaContainers.Image) = Nothing) As Boolean
        'reset state
        imgResult = Nothing

        Dim nImageSettings As ImageItemBase = Settings.Helpers.GetImageSpecificationItem(dbElement.ContentType, imageType)
        If nImageSettings Is Nothing Then Return False

        If imageType = Enums.ModifierType.EpisodePoster AndAlso nImageSettings.VideoExtractionPreferred Then
            Dim imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(dbElement, 1, 30000)
            If Not imgResultList.Count = 0 Then
                imgResult = imgResultList(0)
            End If
        End If

        If imgResult Is Nothing AndAlso nImageSettings.PreferredSize = Enums.ImageSize.Any Then
            imgResult = imageList.Find(Function(f) f.Episode = episode AndAlso f.Season = season)
        End If

        If imgResult Is Nothing Then
            imgResult = imageList.Find(Function(f) f.ImageSize = nImageSettings.PreferredSize AndAlso f.Episode = episode AndAlso f.Season = season)
        End If

        If imgResult Is Nothing AndAlso Not nImageSettings.PreferredSizeOnly AndAlso Not nImageSettings.PreferredSize = Enums.ImageSize.Any Then
            imgResult = imageList.Find(Function(f) f.Episode = episode AndAlso f.Season = season)
        End If

        If imgResult Is Nothing AndAlso showImageList IsNot Nothing Then
            GetPreferredImage(showImageList, imgResult, dbElement.ContentType, imageType)
        End If

        If imgResult Is Nothing AndAlso imageType = Enums.ModifierType.EpisodePoster AndAlso nImageSettings.VideoExtraction Then
            Dim imgResultList = FFmpeg.FFmpeg.GenerateThumbnailsWithoutBars(dbElement, 1, 30000)
            If Not imgResultList.Count = 0 Then
                imgResult = imgResultList(0)
            End If
        End If

        If imgResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredImagesContainer(ByVal dbElement As Database.DBElement,
                                                       ByVal searchResultsContainer As MediaContainers.SearchResultsContainer,
                                                       ByVal scrapeModifiers As Structures.ScrapeModifiers) As MediaContainers.PreferredImagesContainer

        Dim nPreferredImagesContainer As New MediaContainers.PreferredImagesContainer
        Dim tContentType As Enums.ContentType = dbElement.ContentType

        'set all old images as default for the new preferred "ImagesContainer"
        'remark: don't copy the whole "ImagesContainer", otherwise the original "DBElement.ImagesContainer" will be overwritten
        'even if the preferred images has been discarded
        nPreferredImagesContainer.ImagesContainer.Banner = dbElement.ImagesContainer.Banner
        nPreferredImagesContainer.ImagesContainer.CharacterArt = dbElement.ImagesContainer.CharacterArt
        nPreferredImagesContainer.ImagesContainer.ClearArt = dbElement.ImagesContainer.ClearArt
        nPreferredImagesContainer.ImagesContainer.ClearLogo = dbElement.ImagesContainer.ClearLogo
        nPreferredImagesContainer.ImagesContainer.DiscArt = dbElement.ImagesContainer.DiscArt
        nPreferredImagesContainer.ImagesContainer.Extrafanarts.AddRange(dbElement.ImagesContainer.Extrafanarts)
        nPreferredImagesContainer.ImagesContainer.Extrathumbs.AddRange(dbElement.ImagesContainer.Extrathumbs)
        nPreferredImagesContainer.ImagesContainer.Fanart = dbElement.ImagesContainer.Fanart
        nPreferredImagesContainer.ImagesContainer.Keyart = dbElement.ImagesContainer.Keyart
        nPreferredImagesContainer.ImagesContainer.Landscape = dbElement.ImagesContainer.Landscape
        nPreferredImagesContainer.ImagesContainer.Poster = dbElement.ImagesContainer.Poster

        For Each tImageType In GetImageTypeByScrapeModifiers(tContentType, scrapeModifiers, dbElement.MainDetails)
            Dim prefImage As MediaContainers.Image = Nothing
            Dim currImage = dbElement.ImagesContainer.GetImageByType(tImageType)
            Dim nImageSettings = Settings.Helpers.GetImageSpecificationItem(tContentType, tImageType)
            If nImageSettings IsNot Nothing Then
                If currImage Is Nothing OrElse Not currImage.LocalFilePathSpecified OrElse Not nImageSettings.KeepExisting Then
                    Select Case tContentType
                        Case Enums.ContentType.TVEpisode
                            Select Case tImageType
                                Case Enums.ModifierType.EpisodeFanart
                                    GetPreferredImage(dbElement, searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tImageType, dbElement.MainDetails.Season, dbElement.MainDetails.Episode,
                                                      searchResultsContainer.GetImagesByType(Enums.ModifierType.MainFanart))
                                Case Enums.ModifierType.EpisodePoster
                                    GetPreferredImage(dbElement, searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tImageType, dbElement.MainDetails.Season, dbElement.MainDetails.Episode)
                            End Select
                        Case Enums.ContentType.TVSeason
                            Select Case tImageType
                                Case Enums.ModifierType.AllSeasonsBanner
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, dbElement.MainDetails.Season,
                                                      searchResultsContainer.GetImagesByType(Enums.ModifierType.MainBanner))
                                Case Enums.ModifierType.AllSeasonsFanart
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, dbElement.MainDetails.Season,
                                                      searchResultsContainer.GetImagesByType(Enums.ModifierType.MainFanart))
                                Case Enums.ModifierType.AllSeasonsLandscape
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, dbElement.MainDetails.Season,
                                                      searchResultsContainer.GetImagesByType(Enums.ModifierType.MainLandscape))
                                Case Enums.ModifierType.AllSeasonsPoster
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, dbElement.MainDetails.Season,
                                                      searchResultsContainer.GetImagesByType(Enums.ModifierType.MainPoster))
                                Case Enums.ModifierType.SeasonBanner, Enums.ModifierType.SeasonLandscape, Enums.ModifierType.SeasonPoster
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, dbElement.MainDetails.Season)
                                Case Enums.ModifierType.SeasonFanart
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage,
                                                      tContentType, tImageType, dbElement.MainDetails.Season,
                                                      searchResultsContainer.GetImagesByType(Enums.ModifierType.MainFanart))
                            End Select
                        Case Else
                            Select Case tImageType
                                Case Enums.ModifierType.MainBanner,
                                     Enums.ModifierType.MainCharacterArt,
                                     Enums.ModifierType.MainClearArt,
                                     Enums.ModifierType.MainClearLogo,
                                     Enums.ModifierType.MainDiscArt,
                                     Enums.ModifierType.MainFanart,
                                     Enums.ModifierType.MainKeyart,
                                     Enums.ModifierType.MainLandscape,
                                     Enums.ModifierType.MainPoster
                                    GetPreferredImage(searchResultsContainer.GetImagesByType(tImageType), prefImage, tContentType, tImageType)
                                Case Enums.ModifierType.MainExtrafanarts
                                    If Not nImageSettings.KeepExisting OrElse Not dbElement.ImagesContainer.Extrafanarts.Count >= nImageSettings.Limit OrElse nImageSettings.Limit = 0 Then
                                        Dim lstCurrentFileNames As New List(Of String)
                                        Dim iDifference As Integer = nImageSettings.Limit - dbElement.ImagesContainer.Extrafanarts.Count
                                        If nImageSettings.KeepExisting Then
                                            'Extrafanarts most time are saved with the same file name that the server use.
                                            'Use the file names of existing images to prevent to get the same images again
                                            'while we try fill the limit with new images.
                                            lstCurrentFileNames.AddRange(dbElement.ImagesContainer.GetImagesByType(tImageType).
                                                                         Cast(Of MediaContainers.Image)().
                                                                         Select(Function(f) Path.GetFileName(f.LocalFilePath)).ToList)
                                        End If
                                        Dim prefImageList As New List(Of MediaContainers.Image)
                                        GetPreferredExtraImages(dbElement, searchResultsContainer.GetImagesByType(tImageType), prefImageList,
                                                                tImageType, If(Not nImageSettings.KeepExisting, nImageSettings.Limit, iDifference), lstCurrentFileNames)
                                        If nImageSettings.KeepExisting Then
                                            nPreferredImagesContainer.ImagesContainer.Extrafanarts.AddRange(prefImageList)
                                        Else
                                            nPreferredImagesContainer.ImagesContainer.Extrafanarts = prefImageList
                                        End If
                                    End If
                                Case Enums.ModifierType.MainExtrathumbs
                                    If Not nImageSettings.KeepExisting OrElse Not dbElement.ImagesContainer.Extrathumbs.Count >= nImageSettings.Limit OrElse nImageSettings.Limit = 0 Then
                                        Dim iDifference As Integer = nImageSettings.Limit - dbElement.ImagesContainer.Extrathumbs.Count
                                        Dim prefImageList As New List(Of MediaContainers.Image)
                                        GetPreferredExtraImages(dbElement, searchResultsContainer.GetImagesByType(tImageType), prefImageList, tImageType, If(Not nImageSettings.KeepExisting, nImageSettings.Limit, iDifference))
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
        For Each tSeason As Database.DBElement In dbElement.Seasons
            Dim nContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Season = tSeason.MainDetails.Season}
            Dim nSeasonResultsContainer = GetPreferredImagesContainer(tSeason, searchResultsContainer, scrapeModifiers)
            If nSeasonResultsContainer.ImagesContainer.BannerSpecified Then nContainer.Banner = nSeasonResultsContainer.ImagesContainer.Banner
            If nSeasonResultsContainer.ImagesContainer.FanartSpecified Then nContainer.Fanart = nSeasonResultsContainer.ImagesContainer.Fanart
            If nSeasonResultsContainer.ImagesContainer.LandscapeSpecified Then nContainer.Landscape = nSeasonResultsContainer.ImagesContainer.Landscape
            If nSeasonResultsContainer.ImagesContainer.PosterSpecified Then nContainer.Poster = nSeasonResultsContainer.ImagesContainer.Poster
            nPreferredImagesContainer.Seasons.Add(nContainer)
        Next

        'Episodes while tv show scraping
        For Each tEpisode As Database.DBElement In dbElement.Episodes.Where(Function(f) f.FilenameSpecified)
            Dim nContainer As New MediaContainers.EpisodeOrSeasonImagesContainer With {.Episode = tEpisode.MainDetails.Episode, .Season = tEpisode.MainDetails.Season}
            Dim nSeasonResultsContainer = GetPreferredImagesContainer(tEpisode, searchResultsContainer, scrapeModifiers)
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
    ''' <param name="fullName">Path to the image file</param>
    ''' <param name="loadBitmap">Create bitmap from memorystream</param>
    ''' <remarks></remarks>
    Public Sub LoadFromFile(ByVal fullName As String, Optional loadBitmap As Boolean = False)
        If Not String.IsNullOrEmpty(fullName) Then
            Dim fiImage As FileInfo = Nothing
            Try
                fiImage = New FileInfo(fullName)
            Catch ex As Exception
                logger.Error(String.Format("[APIImages] [LoadFromFile]: (0) ""(1)""", ex.Message, fullName))
                Return
            End Try

            If Not fiImage.Exists Then
                logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" not found", fullName))
                _ms = New MemoryStream
                _image = Nothing
            ElseIf fiImage.Length > 0 Then
                _ms = New MemoryStream()
                _image = Nothing
                Using fsImage As FileStream = File.OpenRead(fullName)
                    Dim memStream As New MemoryStream
                    memStream.SetLength(fsImage.Length)
                    fsImage.Read(memStream.GetBuffer, 0, CInt(Fix(fsImage.Length)))
                    _ms.Write(memStream.GetBuffer, 0, CInt(Fix(fsImage.Length)))
                    _ms.Flush()
                    If _ms.Length > 0 Then
                        If loadBitmap Then
                            _image = New Bitmap(_ms)
                        End If
                    Else
                        logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" is empty", fullName))
                        _ms = New MemoryStream
                        _image = Nothing
                    End If
                End Using
            Else
                logger.Error(String.Format("[APIImages] [LoadFromFile]: File ""{0}"" is empty", fullName))
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
        End If
        Return False
    End Function
    ''' <summary>
    ''' Loads this Image from the supplied URL
    ''' </summary>
    ''' <param name="webUrl">URL to the image file</param>
    ''' <param name="loadBitmap">Create bitmap from memorystream</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal webUrl As String, Optional loadBitmap As Boolean = False)
        If String.IsNullOrEmpty(webUrl) Then Return

        Try
            Dim sHTTP As New HTTP
            sHTTP.StartDownloadImage(webUrl)
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
                If loadBitmap Then
                    _image = New Bitmap(sHTTP.Image) '(Me._ms)
                End If

                ' if is not a JPG or PNG we have to convert the memory stream to JPG format
                If Not (sHTTP.isJPG OrElse sHTTP.isPNG) Then
                    UpdateMSfromImg(New Bitmap(_image))
                End If
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Stores the Image to the supplied <paramref name="fullName"/>
    ''' </summary>
    ''' <param name="fullName">Location to store the image</param>
    ''' <remarks></remarks>
    Public Sub SaveToFile(ByVal fullName As String)
        If _ms.Length > 0 Then
            Dim retSave() As Byte
            Try
                retSave = _ms.ToArray

                'make sure directory exists
                Directory.CreateDirectory(Directory.GetParent(fullName).FullName)
                If fullName.Length <= 260 Then
                    Using fs As New FileStream(fullName, FileMode.Create, FileAccess.Write)
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
    ''' <param name="dbElement"><c>Database.DBElement</c> representing the Movie being referred to</param>
    ''' <param name="imageType"></param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal dbElement As Database.DBElement, ByVal imageType As Enums.ModifierType) As String
        Dim strReturn As String = String.Empty

        Dim bNeedResize As Boolean
        Dim nImageSettings As ImageItemBase = Settings.Helpers.GetImageSpecificationItem(dbElement.ContentType, imageType)
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

            For Each a In FileUtils.FileNames.GetFileNames(dbElement, imageType)
                SaveToFile(a)
                strReturn = a
            Next

            If dbElement.ContentType = Enums.ContentType.Movie AndAlso imageType = Enums.ModifierType.MainFanart AndAlso Not String.IsNullOrEmpty(strReturn) AndAlso
                Not String.IsNullOrEmpty(Master.eSettings.MovieBackdropsPath) AndAlso Master.eSettings.MovieBackdropsAuto Then
                FileUtils.Common.CopyFanartToBackdropsPath(strReturn, dbElement.ContentType)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Shared Sub Save_Actorthumbs(ByVal dbElement As Database.DBElement)
        'First, (Down)Load all actor thumbs from LocalFilePath or URL
        For Each Actor As MediaContainers.Person In dbElement.MainDetails.Actors
            Actor.Thumb.LoadAndCache(dbElement.ContentType, True)
        Next

        'Second, remove the old ones
        'TODO: methode to delete episode actrothumbs only if no other episode use it
        If Not dbElement.ContentType = Enums.ContentType.TVEpisode Then Delete(dbElement, Enums.ModifierType.MainActorThumbs, False)

        'Thirdly, save all actor thumbs
        For Each Actor As MediaContainers.Person In dbElement.MainDetails.Actors
            If Actor.Thumb.LoadAndCache(dbElement.ContentType, True) Then
                Actor.Thumb.LocalFilePath = Actor.Thumb.ImageOriginal.Save_Actorthumb(dbElement, Actor)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Save the image as an actor thumbnail
    ''' </summary>
    ''' <param name="dbElement"><c>Database.DBElement</c> representing the element being referred to</param>
    ''' <param name="actor"><c>MediaContainers.Person</c> representing the actor</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_Actorthumb(ByVal dbElement As Database.DBElement, ByVal actor As MediaContainers.Person) As String
        Dim ActorthumbFullName As String = String.Empty
        Dim ImageType As Enums.ModifierType

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie, Enums.ContentType.TVShow
                ImageType = Enums.ModifierType.MainActorThumbs
            Case Enums.ContentType.TVEpisode
                ImageType = Enums.ModifierType.EpisodeActorThumbs
        End Select

        For Each FileName In FileUtils.FileNames.GetFileNames(dbElement, ImageType)
            ActorthumbFullName = FileName.Replace("<placeholder>", actor.Name.Replace(" ", "_"))
            SaveToFile(ActorthumbFullName)
        Next

        Clear() 'Dispose to save memory
        Return ActorthumbFullName
    End Function
    ''' <summary>
    ''' Save all Extrafanarts
    ''' </summary>
    ''' <param name="dbElement"><c>Database.DBElement</c> representing the movie or tv show being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Shared Function Save_Extrafanarts(ByVal dbElement As Database.DBElement) As String
        Dim ExtrafanartsFullName As String = String.Empty

        'First, (Down)Load all Extrafanarts from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In dbElement.ImagesContainer.Extrafanarts
            eImg.LoadAndCache(dbElement.ContentType, True)
        Next

        'Second, remove the old ones
        Delete(dbElement, Enums.ModifierType.MainExtrafanarts, False)

        'Thirdly, save all Extrafanarts
        For Each eImg As MediaContainers.Image In dbElement.ImagesContainer.Extrafanarts
            If eImg.LoadAndCache(dbElement.ContentType, True) Then
                ExtrafanartsFullName = eImg.ImageOriginal.Save_Extrafanart(dbElement, If(Not String.IsNullOrEmpty(eImg.URLOriginal), Path.GetFileName(eImg.URLOriginal), Path.GetFileName(eImg.LocalFilePath)))
            End If
        Next

        'If efPath is empty (i.e. expert setting enabled but expert extrafanart scraping disabled) it will cause Ember to crash, therefore do check first
        If Not String.IsNullOrEmpty(ExtrafanartsFullName) Then
            Return Directory.GetParent(ExtrafanartsFullName).FullName
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrafanart
    ''' </summary>
    ''' <param name="dbElement"><c>Structures.DBMovie</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_Extrafanart(ByVal dbElement As Database.DBElement, ByVal name As String) As String
        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                If String.IsNullOrEmpty(dbElement.Filename) Then Return String.Empty
            Case Enums.ContentType.TVShow
                If String.IsNullOrEmpty(dbElement.ShowPath) Then Return String.Empty
        End Select

        Dim ExtrafanartFullName As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        Dim imageSettings = Settings.Helpers.GetImageSpecificationItem(dbElement.ContentType, Enums.ModifierType.MainExtrafanarts)

        Dim ResizeNeeded As Boolean = False
        If imageSettings.Resize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            ResizeNeeded = _image.Width > imageSettings.MaxWidth OrElse _image.Height > imageSettings.MaxHeight
        End If

        Try
            If ResizeNeeded Then
                ImageUtils.ResizeImage(_image, imageSettings.MaxWidth, imageSettings.MaxHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainExtrafanarts)
                If Not String.IsNullOrEmpty(a) Then
                    If Not Directory.Exists(a) Then
                        Directory.CreateDirectory(a)
                    End If
                    If String.IsNullOrEmpty(name) Then
                        iMod = Functions.GetExtrafanartsModifier(a)
                        iVal = iMod + 1
                        name = Path.Combine(a, String.Concat("extrafanart", iVal, ".jpg"))
                    End If
                    ExtrafanartFullName = Path.Combine(a, name)
                    SaveToFile(ExtrafanartFullName)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return ExtrafanartFullName
    End Function
    ''' <summary>
    ''' Save all Extrathumbs
    ''' </summary>
    ''' <param name="dbElement"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Shared Function Save_Extrathumbs(ByVal dbElement As Database.DBElement) As String
        Dim ExtrathumbsFullName As String = String.Empty

        'First, (Down)Load all Extrathumbs from LocalFilePath or URL
        For Each eImg As MediaContainers.Image In dbElement.ImagesContainer.Extrathumbs
            eImg.LoadAndCache(dbElement.ContentType, True)
        Next

        'Secound, remove the old ones
        Delete(dbElement, Enums.ModifierType.MainExtrathumbs, False)

        'Thirdly, save all Extrathumbs
        For Each eImg As MediaContainers.Image In dbElement.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
            If eImg.LoadAndCache(dbElement.ContentType, True) Then
                ExtrathumbsFullName = eImg.ImageOriginal.Save_Extrathumb(dbElement)
            End If
        Next

        'If etPath is empty (i.e. expert setting enabled but expert extrathumb scraping disabled) it will cause Ember to crash, therefore do check first
        If Not String.IsNullOrEmpty(ExtrathumbsFullName) Then
            Return Directory.GetParent(ExtrathumbsFullName).FullName
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Save the image as a movie's extrathumb
    ''' </summary>
    ''' <param name="dbElement"><c>Database.DBElement</c> representing the movie being referred to</param>
    ''' <returns><c>String</c> path to the saved image</returns>
    ''' <remarks></remarks>
    Public Function Save_Extrathumb(ByVal dbElement As Database.DBElement) As String
        Dim etPath As String = String.Empty
        Dim iMod As Integer = 0
        Dim iVal As Integer = 1

        If String.IsNullOrEmpty(dbElement.Filename) Then Return etPath

        Dim doResize As Boolean = False
        If Master.eSettings.Movie.ImageSettings.Extrathumbs.Resize Then
            If _image Is Nothing Then LoadFromMemoryStream()
            doResize = _image.Width > Master.eSettings.Movie.ImageSettings.Extrathumbs.MaxWidth OrElse _image.Height > Master.eSettings.Movie.ImageSettings.Extrathumbs.MaxHeight
        End If

        Try
            If doResize Then
                ImageUtils.ResizeImage(_image, Master.eSettings.Movie.ImageSettings.Extrathumbs.MaxWidth, Master.eSettings.Movie.ImageSettings.Extrathumbs.MaxHeight)
                'need to align _immage and _ms
                UpdateMSfromImg(_image)
            End If

            For Each a In FileUtils.FileNames.Movie(dbElement, Enums.ModifierType.MainExtrathumbs)
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

    Public Shared Sub SetPreferredImages(ByRef dbElement As Database.DBElement,
                                         ByVal preferredImagesContainer As MediaContainers.PreferredImagesContainer)

        If preferredImagesContainer IsNot Nothing Then
            dbElement.ImagesContainer = preferredImagesContainer.ImagesContainer
            For Each tSeason As Database.DBElement In dbElement.Seasons
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = preferredImagesContainer.Seasons.FirstOrDefault(Function(f) f.Season = tSeason.MainDetails.Season)
                If prefImages IsNot Nothing Then
                    tSeason.ImagesContainer.Banner = prefImages.Banner
                    tSeason.ImagesContainer.Fanart = prefImages.Fanart
                    tSeason.ImagesContainer.Landscape = prefImages.Landscape
                    tSeason.ImagesContainer.Poster = prefImages.Poster
                End If
            Next
            For Each tEpisode As Database.DBElement In dbElement.Episodes
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = preferredImagesContainer.Episodes.FirstOrDefault(Function(f) f.Episode = tEpisode.MainDetails.Episode AndAlso f.Season = tEpisode.MainDetails.Season)
                If prefImages IsNot Nothing Then
                    tEpisode.ImagesContainer.Fanart = prefImages.Fanart
                    tEpisode.ImagesContainer.Poster = prefImages.Poster
                End If
            Next
        End If
    End Sub

    Public Shared Sub SetPreferredImages(ByRef dbElement As Database.DBElement,
                                         ByVal searchResultsContainer As MediaContainers.SearchResultsContainer,
                                         ByVal scrapeModifiers As Structures.ScrapeModifiers)

        Dim PreferredImagesContainer As MediaContainers.PreferredImagesContainer = GetPreferredImagesContainer(dbElement, searchResultsContainer, scrapeModifiers)

        If PreferredImagesContainer IsNot Nothing Then
            'Main Images
            dbElement.ImagesContainer = PreferredImagesContainer.ImagesContainer

            'Season Images while tvshow scraping
            For Each tSeason As Database.DBElement In dbElement.Seasons
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = PreferredImagesContainer.Seasons.FirstOrDefault(Function(f) f.Season = tSeason.MainDetails.Season)
                If prefImages IsNot Nothing Then
                    tSeason.ImagesContainer.Banner = prefImages.Banner
                    tSeason.ImagesContainer.Fanart = prefImages.Fanart
                    tSeason.ImagesContainer.Landscape = prefImages.Landscape
                    tSeason.ImagesContainer.Poster = prefImages.Poster
                End If
            Next

            'Episode Images while tvshow scraping
            For Each tEpisode As Database.DBElement In dbElement.Episodes
                Dim prefImages As MediaContainers.EpisodeOrSeasonImagesContainer = PreferredImagesContainer.Episodes.FirstOrDefault(Function(f) f.Episode = tEpisode.MainDetails.Episode AndAlso
                                                                                                                                        f.Season = tEpisode.MainDetails.Season)
                If prefImages IsNot Nothing Then
                    tEpisode.ImagesContainer.Fanart = prefImages.Fanart
                    tEpisode.ImagesContainer.Poster = prefImages.Poster
                End If
            Next
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Shared Function GetImageTypeByScrapeModifiers(ByVal contentType As Enums.ContentType,
                                                          ByVal options As Structures.ScrapeModifiers,
                                                          ByVal seasonDetails As MediaContainers.MainDetails) As List(Of Enums.ModifierType)
        Dim lstModTypes As New List(Of Enums.ModifierType)
        With options
            Select Case contentType
                Case Enums.ContentType.Movie, Enums.ContentType.Movieset, Enums.ContentType.TVShow
                    If .Banner Then lstModTypes.Add(Enums.ModifierType.MainBanner)
                    If .Characterart Then lstModTypes.Add(Enums.ModifierType.MainCharacterArt)
                    If .Clearart Then lstModTypes.Add(Enums.ModifierType.MainClearArt)
                    If .Clearlogo Then lstModTypes.Add(Enums.ModifierType.MainClearLogo)
                    If .Discart Then lstModTypes.Add(Enums.ModifierType.MainDiscArt)
                    If .Extrafanarts Then lstModTypes.Add(Enums.ModifierType.MainExtrafanarts)
                    If .Extrathumbs Then lstModTypes.Add(Enums.ModifierType.MainExtrathumbs)
                    If .Fanart Then lstModTypes.Add(Enums.ModifierType.MainFanart)
                    If .Keyart Then lstModTypes.Add(Enums.ModifierType.MainKeyart)
                    If .Landscape Then lstModTypes.Add(Enums.ModifierType.MainLandscape)
                    If .Poster Then lstModTypes.Add(Enums.ModifierType.MainPoster)
                Case Enums.ContentType.TVEpisode
                    If .Episodes.Fanart Then lstModTypes.Add(Enums.ModifierType.EpisodeFanart)
                    If .Episodes.Poster Then lstModTypes.Add(Enums.ModifierType.EpisodePoster)
                Case Enums.ContentType.TVSeason
                    If seasonDetails IsNot Nothing Then
                        If seasonDetails.Season_IsAllSeasons Then
                            If .AllSeasonsBanner Then lstModTypes.Add(Enums.ModifierType.AllSeasonsBanner)
                            If .AllSeasonsFanart Then lstModTypes.Add(Enums.ModifierType.AllSeasonsFanart)
                            If .AllSeasonsLandscape Then lstModTypes.Add(Enums.ModifierType.AllSeasonsLandscape)
                            If .AllSeasonsPoster Then lstModTypes.Add(Enums.ModifierType.AllSeasonsPoster)
                        Else
                            If .Seasons.Banner Then lstModTypes.Add(Enums.ModifierType.SeasonBanner)
                            If .Seasons.Fanart Then lstModTypes.Add(Enums.ModifierType.SeasonFanart)
                            If .Seasons.Landscape Then lstModTypes.Add(Enums.ModifierType.SeasonLandscape)
                            If .Seasons.Poster Then lstModTypes.Add(Enums.ModifierType.SeasonPoster)
                        End If
                    End If
            End Select
        End With
        Return lstModTypes
    End Function

#End Region 'Nested Types

End Class