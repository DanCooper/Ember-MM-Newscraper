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
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

<Assembly: InternalsVisibleTo("EmberAPI_Test")> 

Public Class ImageUtils

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Public Const DefaultPaddingARGB As Integer = -16777216  'This is FF000000, which is completely opaque black

#End Region 'Fields

#Region "Methods"
    ''' <summary>
    ''' Adds an overlay on the provided image to indicate that it is a "DUPLICATE"
    ''' </summary>
    ''' <param name="oImage">Source <c>Images</c></param>
    ''' <returns><c>Image</c> with "DUPLICATE" overlay, or just the source <paramref name="oImage"/> if an error was encountered.</returns>
    ''' <remarks></remarks>
    Public Shared Function AddDuplicateStamp(ByVal oImage As Image) As Image
        If oImage Is Nothing Then Return oImage

        Using sImage As New Bitmap(oImage)
            'now overlay "DUPLICATE" image
            Using grOverlay As Graphics = Graphics.FromImage(sImage)
                Dim oWidth As Integer = If(sImage.Width >= Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Duplicate.png")).Width, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Duplicate.png")).Width, sImage.Width)
                Dim oheight As Integer = If(sImage.Height >= Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Duplicate.png")).Height, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Duplicate.png")).Height, sImage.Height)
                grOverlay.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                grOverlay.DrawImage(Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Duplicate.png")), 0, 0, oWidth, oheight)
            End Using
            Dim nImage As New Images
            nImage.UpdateMSfromImg(sImage)
            Return nImage.Image
        End Using
    End Function
    ''' <summary>
    ''' Adds an overlay on the provided image to indicate that it is "missing"
    ''' </summary>
    ''' <param name="oImage">Source <c>Images</c></param>
    ''' <returns><c>Images</c> with "missing" overlay, or just the source <paramref name="oImage"/> if an error was encountered.</returns>
    ''' <remarks></remarks>
    Public Shared Function AddMissingStamp(ByVal oImage As Images) As Images
        If oImage Is Nothing OrElse oImage.Image Is Nothing Then Return oImage

        Using nImage As New Bitmap(GrayScale(oImage).Image)
            'now overlay "missing" image
            Using grOverlay As Graphics = Graphics.FromImage(nImage)
                Dim oWidth As Integer = If(nImage.Width >= Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Missing.png")).Width, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Missing.png")).Width, nImage.Width)
                Dim oheight As Integer = If(nImage.Height >= Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Missing.png")).Height, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Missing.png")).Height, nImage.Height)
                grOverlay.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                grOverlay.DrawImage(Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Missing.png")), 0, 0, oWidth, oheight)
            End Using
            oImage.UpdateMSfromImg(nImage)
        End Using
        Return oImage
    End Function
    ''' <summary>
    ''' Converts the source <paramref name="oImage"/> to grayscale
    ''' </summary>
    ''' <param name="oImage">Source <c>Images</c></param>
    ''' <returns><c>Images</c> that has been converted to grayscale, or just the source <paramref name="oImage"/> if an error was encountered.</returns>
    ''' <remarks></remarks>
    Public Shared Function GrayScale(ByVal oImage As Images) As Images
        If oImage Is Nothing OrElse oImage.Image Is Nothing Then Return oImage

        Dim nImage As New Bitmap(oImage.Image)

        'first let's convert the background to grayscale
        Using g As Graphics = Graphics.FromImage(nImage)
            Dim cm As Imaging.ColorMatrix = New Imaging.ColorMatrix(New Single()() _
               {New Single() {0.5, 0.5, 0.5, 0, 0},
                New Single() {0.5, 0.5, 0.5, 0, 0},
                New Single() {0.5, 0.5, 0.5, 0, 0},
                New Single() {0, 0, 0, 1, 0},
                New Single() {0, 0, 0, 0, 1}})

            Dim ia As Imaging.ImageAttributes = New Imaging.ImageAttributes()
            ia.SetColorMatrix(cm)
            g.DrawImage(nImage, New Rectangle(0, 0, nImage.Width, nImage.Height), 0, 0, nImage.Width, nImage.Height, GraphicsUnit.Pixel, ia)
        End Using

        oImage.UpdateMSfromImg(nImage)

        Return oImage
    End Function
    ''' <summary>
    ''' Resize the supplied <paramref name="_image"/>, preserving proportions.
    ''' </summary>
    ''' <param name="_image">Source <c>Image</c> to manipulate</param>
    ''' <param name="maxWidth">Maximum width of resized image</param>
    ''' <param name="maxHeight">Maximum height of resized image</param>
    ''' <param name="usePadding">If <c>True</c>, background of bounding rectangle not covered by the 
    ''' resized image will be drawn with <paramref name="PaddingARGB"/> color</param>
    ''' <param name="PaddingARGB">Optional color to use as padding.</param>
    ''' <remarks>Image will retain the same proportions, however will resize such that it can fit 
    ''' within the <paramref name="maxWidth"/> and <paramref name="maxHeight"/>. If <paramref name="usePadding"/> is specified, 
    ''' the background of the rectangle specified by <paramref name="maxWidth"/> and <paramref name="maxHeight"/> will be filled
    ''' by <paramref name="PaddingARGB"/></remarks>
    Public Shared Sub ResizeImage(ByRef _image As Image, ByVal maxWidth As Integer, ByVal maxHeight As Integer, Optional ByVal usePadding As Boolean = False, Optional ByVal PaddingARGB As Integer = DefaultPaddingARGB)
        If (_image Is Nothing) OrElse
            (maxWidth <= 0) OrElse
            (maxHeight <= 0) Then
            Return
        End If

        Try
            Dim sPropPerc As Single = 1.0 'no default scaling

            If _image.Width > _image.Height Then
                sPropPerc = CSng(maxWidth / _image.Width)
            Else
                sPropPerc = CSng(maxHeight / _image.Height)
            End If

            ' Get the source bitmap.
            Using bmSource As New Bitmap(_image)
                ' Make a bitmap for the result.
                Dim bmDest As New Bitmap(
                Convert.ToInt32(bmSource.Width * sPropPerc),
                Convert.ToInt32(bmSource.Height * sPropPerc))
                ' Make a Graphics object for the result Bitmap.
                Using grDest As Graphics = Graphics.FromImage(bmDest)
                    grDest.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    ' Copy the source image into the destination bitmap.
                    grDest.DrawImage(bmSource, New Rectangle(0, 0,
                    bmDest.Width, bmDest.Height), New Rectangle(0, 0,
                    bmSource.Width, bmSource.Height), GraphicsUnit.Pixel)
                End Using

                If usePadding Then
                    Dim bgBMP As Bitmap = New Bitmap(maxWidth, maxHeight)
                    Using grOverlay As Graphics = Graphics.FromImage(bgBMP)
                        grOverlay.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                        grOverlay.FillRectangle(New SolidBrush(Color.FromArgb(PaddingARGB)), New RectangleF(0, 0, maxWidth, maxHeight))
                        Dim iLeft As Integer = Convert.ToInt32(If(bmDest.Width = maxWidth, 0, (maxWidth - bmDest.Width) / 2))
                        Dim iTop As Integer = Convert.ToInt32(If(bmDest.Height = maxHeight, 0, (maxHeight - bmDest.Height) / 2))
                        grOverlay.DrawImage(bmDest, iLeft, iTop, bmDest.Width, bmDest.Height)
                    End Using
                    bmDest = bgBMP
                End If

                _image = bmDest

            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Copies the image from <paramref name="pbSource"/> into <paramref name="pbDestination"/>,
    ''' resizing it (if necessary) so that it is no larger than the supplied width and height.
    ''' It only shrinks, but does not grow the image.
    ''' </summary>
    ''' <param name="pbDestination">Destination picture box</param>
    ''' <param name="pbSource">Source picture box</param>
    ''' <param name="maxHeight">Maximum height for <paramref name="pbDestination"/></param>
    ''' <param name="maxWidth">Maximum width for <paramref name="pbDestination"/></param>
    ''' <remarks>Why not use "Zoom" for fanart background? - To keep the image at the top. Zoom centers vertically.</remarks>
    Public Shared Sub ResizePB(ByRef pbDestination As PictureBox,
                               ByRef pbSource As PictureBox,
                               ByVal maxHeight As Integer,
                               ByVal maxWidth As Integer,
                               Optional DoClip As Boolean = False)
        If pbSource Is Nothing OrElse pbSource.Image Is Nothing Then Return

        If pbSource.Image IsNot Nothing Then
            Try
                Dim sPropPerc As Single = 1.0 'no default scaling
                pbDestination.Size = New Size(maxWidth, maxHeight)

                If Not pbDestination.Image Is Nothing Then pbDestination.Image.Dispose()

                If Not DoClip Then
                    pbDestination.SizeMode = PictureBoxSizeMode.Normal
                    ' Height
                    If pbSource.Image.Height > pbDestination.Height Then
                        ' Reduce height first
                        sPropPerc = CSng(pbDestination.Height / pbSource.Image.Height)
                    End If
                    ' Width 
                    If (pbSource.Image.Width * sPropPerc) > pbDestination.Width Then
                        ' Scaled width exceeds Box's width, recalculate scale_factor
                        sPropPerc = CSng(pbDestination.Width / pbSource.Image.Width)
                    End If
                Else
                    pbDestination.SizeMode = PictureBoxSizeMode.CenterImage
                    Dim dblImageAspectRatio As Double = pbSource.Image.Height / pbSource.Image.Width
                    Dim dblDestAspectRation As Double = pbDestination.Height / pbDestination.Width
                    If dblDestAspectRation < dblImageAspectRatio Then
                        'Image has to be clipped in height and use the full width of destination PB
                        sPropPerc = CSng(pbDestination.Width / pbSource.Image.Width)
                    Else
                        'Image has to be clipped in width and use the full height of destination PB
                        sPropPerc = CSng(pbDestination.Height / pbSource.Image.Height)
                    End If
                End If

                ' Get the source bitmap.
                Using bmSource As New Bitmap(pbSource.Image)
                    ' Make a bitmap for the result.
                    Dim bmDest As New Bitmap(
                            Convert.ToInt32(bmSource.Width * sPropPerc),
                            Convert.ToInt32(bmSource.Height * sPropPerc))
                    ' Make a Graphics object for the result Bitmap.
                    Using grDest As Graphics = Graphics.FromImage(bmDest)
                        ' Copy the source image into the destination bitmap.
                        grDest.DrawImage(bmSource, 0, 0,
                                         bmDest.Width + 1,
                                         bmDest.Height + 1)
                        ' Display the result.
                        pbDestination.Image = bmDest

                        If Not DoClip Then
                            'tweak pb after resizing pic
                            pbDestination.Width = pbDestination.Image.Width
                            pbDestination.Height = pbDestination.Image.Height
                        End If

                        'Clean up
                        bmDest = Nothing
                    End Using
                End Using

            Catch ex As Exception
                pbDestination.Left = 0
                pbDestination.Size = New Size(maxWidth, maxHeight)
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            pbDestination.Left = 0
            pbDestination.Size = New Size(maxWidth, maxHeight)
        End If
    End Sub
    ''' <summary>
    ''' Apply a glass overlay on the supplied <c>PictureBox</c>
    ''' </summary>
    ''' <param name="pbUnderlay"><c>PictureBox</c> representing the source image</param>
    ''' <remarks></remarks>
    Public Shared Sub SetGlassOverlay(ByRef pbUnderlay As PictureBox)
        If (pbUnderlay Is Nothing) OrElse (pbUnderlay.Image Is Nothing) Then Return

        Try
            Dim bmOverlay As New Bitmap(pbUnderlay.Image)
            Using grOverlay As Graphics = Graphics.FromImage(bmOverlay)
                Dim bmHeight As Integer = Convert.ToInt32(pbUnderlay.Image.Height * 0.65)

                grOverlay.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

                grOverlay.DrawImage(Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Overlay.png")), 0, 0, pbUnderlay.Image.Width, bmHeight)
                pbUnderlay.Image = bmOverlay

                bmOverlay = New Bitmap(pbUnderlay.Image)
            End Using
            Using grOverlay = Graphics.FromImage(bmOverlay)

                grOverlay.DrawImage(Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "Overlay2.png")), 0, 0, pbUnderlay.Image.Width, pbUnderlay.Image.Height)
                pbUnderlay.Image = bmOverlay

            End Using
            bmOverlay = Nothing
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Adds the supplied <paramref name="imgOverlay"/> on top of the supplied <paramref name="imgUnderlay"/>
    ''' </summary>
    ''' <param name="imgUnderlay">Base <c>Image</c></param>
    ''' <param name="iWidth">Desired width for the final image</param>
    ''' <param name="iHeight">Desired height for the final image</param>
    ''' <param name="imgOverlay"><c>Image</c> to overlay on top of <paramref name="imgUnderlay"/></param>
    ''' <param name="Location">Relative location to place the overlay. 1 = upper left, 2 = upper right, 3 = lower left, 4 = lower right. Invalid values are treated as 1 (upper left)</param>
    ''' <returns>Returns the combined image that includes the overlay</returns>
    ''' <remarks>Note that setting <paramref name="iHeight"/> and/or <paramref name="iWidth"/> to a value
    ''' smaller than <paramref name="imgUnderlay"/> will cause the overlay that extends over the edge will be clipped.
    ''' Also note that when the underlay is resized, proportions are preserved, and any exposed background will be
    ''' filled with transparency.</remarks>
    Public Shared Function SetOverlay(ByRef imgUnderlay As Image, ByVal iWidth As Integer, ByVal iHeight As Integer, ByVal imgOverlay As Image, ByVal Location As Integer) As Image
        If imgUnderlay Is Nothing OrElse imgOverlay Is Nothing Then Return imgUnderlay
        'If the overlay is sized to be invisible, nothing left to do!
        If iWidth = 0 OrElse iHeight = 0 Then Return imgUnderlay
        If Location < 1 OrElse Location > 4 Then Location = 1 'Just go with the flow...

        Try
            'Resize the overlay to the requested size
            ResizeImage(imgUnderlay, iWidth, iHeight, True, Color.Transparent.ToArgb)
            Dim bmOverlay As New Bitmap(imgUnderlay)
            Using grOverlay As Graphics = Graphics.FromImage(bmOverlay)
                Dim iLeft As Integer = 0
                Dim iTop As Integer = 0

                Select Case Location
                    Case 2
                        iLeft = bmOverlay.Width - imgOverlay.Width
                        iTop = 0
                    Case 3
                        iLeft = 0
                        iTop = bmOverlay.Height - imgOverlay.Height
                    Case 4
                        iLeft = bmOverlay.Width - imgOverlay.Width
                        iTop = bmOverlay.Height - imgOverlay.Height
                    Case Else
                        iLeft = 0
                        iTop = 0
                End Select

                grOverlay.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

                grOverlay.DrawImage(imgOverlay, iLeft, iTop, imgOverlay.Width, imgOverlay.Height)
                imgUnderlay = bmOverlay
            End Using
            bmOverlay = Nothing
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return imgUnderlay
    End Function
    ''' <summary>
    ''' Adds the supplied <paramref name="genreString"/> to the given <paramref name="image"/>
    ''' </summary>
    ''' <param name="image">Source <c>Image</c> to manipulate</param>
    ''' <param name="genreString"><c>String</c> to superimpose</param>
    ''' <remarks>If an error is encountered, the source image is returned.</remarks>
    Public Shared Function AddGenreString(ByRef image As Image, genreString As String) As Bitmap
        If (image Is Nothing) OrElse (image.Size.IsEmpty) Then
            logger.Error("Invalid image parameter", New StackTrace().ToString())
            Return Nothing
        End If
        Dim bmGenre As New Bitmap(image)
        Try
            Using grGenre As Graphics = Graphics.FromImage(bmGenre),
                drawFont1 As New Font("Microsoft Sans Serif", 14, FontStyle.Bold, GraphicsUnit.Pixel)

                Dim drawBrush As New SolidBrush(Color.White)
                Dim drawWidth As Single = grGenre.MeasureString(genreString, drawFont1).Width
                Dim drawSize As Integer = Convert.ToInt32((14 * (bmGenre.Width / drawWidth)) - 0.5)
                Using drawFont2 = New Font("Microsoft Sans Serif", If(drawSize > 14, 14, drawSize), FontStyle.Bold, GraphicsUnit.Pixel)
                    Dim drawHeight As Single = grGenre.MeasureString(genreString, drawFont2).Height
                    Dim iLeft As Integer = Convert.ToInt32((bmGenre.Width - grGenre.MeasureString(genreString, drawFont2).Width) / 2)
                    grGenre.DrawString(genreString, drawFont2, drawBrush, iLeft, (bmGenre.Height - drawHeight))
                End Using
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return bmGenre
    End Function

#End Region 'Methods
    ''' <summary>
    ''' Contains methods for image comparison and recognition. 
    ''' </summary>
    ''' <remarks>
    ''' 2015/09/22 Cocotus - First implementation
    ''' Idea: Use this class/algorithm to avoid duplicate images in library (i.e. same image as fanart.jpg and in extrafanart-folder) 
    ''' Methods are based and implemented using information provided by Dr. Neal Krawetz
    ''' on his blog: http://www.hackerfactor.com/blog/index.php?/archives/432-Looks-Like-It.html.
    ''' A big fragment of the code below is result of merging the provided samples/examples at that blog (phyton language) and converted to VB.NET
    ''' </remarks>
    Public Class ImageComparison

        Public Enum Algorithm
            AverageHash = 0
            PHash = 1
        End Enum
        ''' <summary>
        ''' Check similarity between two given images
        ''' </summary>
        ''' <param name="image_1">The first image</param>
        ''' <param name="image_2">The second image</param>
        ''' <returns>0: identical images, 0-5: most likely identical, >10: different images</returns>
        ''' <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' </remarks>
        Public Shared Function GetSimilarity(image_1 As Image, image_2 As Image, Algorithm As Algorithm) As Integer
            Dim imagehash_1 As String
            Dim imagehash_2 As String
            If Algorithm = ImageComparison.Algorithm.AverageHash Then
                imagehash_1 = AverageHash.GetAverageHash(image_1)
                imagehash_2 = AverageHash.GetAverageHash(image_2)
            Else
                imagehash_1 = PHash.GetPHash(image_1)
                imagehash_2 = PHash.GetPHash(image_2)
            End If

            Return CalculateHammingDistance(imagehash_1, imagehash_2)
        End Function
        ''' <summary>
        ''' Check similarity between two given images
        ''' </summary>
        ''' <param name="image_1">The first image</param>
        ''' <param name="imagepath_2">Path to the second image</param>
        ''' <returns>0: identical images, 0-5: most likely identical, >10: different images</returns>
        ''' <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' </remarks>
        Public Shared Function GetSimilarity(image_1 As Image, imagepath_2 As String, Algorithm As Algorithm) As Integer
            Dim image_2 As New Bitmap(imagepath_2)
            Dim imagehash_1 As String
            Dim imagehash_2 As String
            If Algorithm = ImageComparison.Algorithm.AverageHash Then
                imagehash_1 = AverageHash.GetAverageHash(image_1)
                imagehash_2 = AverageHash.GetAverageHash(image_2)
            Else
                imagehash_1 = PHash.GetPHash(image_1)
                imagehash_2 = PHash.GetPHash(image_2)
            End If

            Return CalculateHammingDistance(imagehash_1, imagehash_2)
        End Function
        ''' <summary>
        ''' Check similarity between two given images
        ''' </summary>
        ''' <param name="imagepath_1">Path to the first image</param>
        ''' <param name="imagepath_2">Path to the second image</param>
        ''' <returns>0: identical images, 0-5: most likely identical, >10: different images</returns>
        ''' <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' </remarks>
        Public Shared Function GetSimilarity(imagepath_1 As String, imagepath_2 As String, Algorithm As Algorithm) As Integer
            Dim image_1 As New Bitmap(imagepath_1)
            Dim image_2 As New Bitmap(imagepath_2)
            Dim imagehash_1 As String
            Dim imagehash_2 As String
            If Algorithm = ImageComparison.Algorithm.AverageHash Then
                imagehash_1 = AverageHash.GetAverageHash(image_1)
                imagehash_2 = AverageHash.GetAverageHash(image_2)
            Else
                imagehash_1 = PHash.GetPHash(image_1)
                imagehash_2 = PHash.GetPHash(image_2)
            End If

            Return CalculateHammingDistance(imagehash_1, imagehash_2)
        End Function
        ''' <summary>
        ''' Check similarity between two given hashes (of images)
        ''' </summary>
        ''' <param name="hashCode_1">The first hash.</param>
        ''' <param name="hashCode_2">The second hash.</param>
        ''' <returns>true=Images are identical, false=different images</returns>
        ''' <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' May need some tweaking to decide at what value to return true
        ''' </remarks>
        Private Shared Function IsIdentical(hashCode_1 As String, hashCode_2 As String, Optional ByVal MatchTolerance As Integer = 5) As Boolean
            'assumption: smaller HammingDistance means, higher possibility of images being identical -> this a more robust setting
            'A distance of zero indicates that it is likely a very similar picture (or a variation of the same picture). A distance of 5 means a few things may be different, but they are probably still close enough to be similar. But a distance of 10 or more? That's probably a very different picture.
            'MatchTolerance:
            '0 = identical images
            '<5 = likely identical images
            '>10 = completely different images

            'for poster since they look often the same (different language on poster but same backgroudn) -> Use MatchFactor = 1
            'for fanart we use higher value because fanarts are mostly not similar -> Use MatchFactor = 5

            'calculate similarity between the two hashes. 
            'The lower the value, the closer the hashes are to being identical
            Dim HammingDistance = CalculateHammingDistance(hashCode_1, hashCode_2)

            If HammingDistance <= MatchTolerance Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' Computes the HammingDistance between the hashcode of two images
        ''' </summary>
        ''' <param name="hashCode_1">Hash of Image 1</param>  
        ''' <param name="hashCode_2">Hash of Image 2</param>  
        ''' <returns>Hamming Distance value -> 0: identical images, 0-5: most likely identical, >10: different images</returns>  
        '''  <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' Used in AverageHash and PHash algorithm
        ''' </remarks>
        Private Shared Function CalculateHammingDistance(ByVal hashCode_1 As String, ByVal hashCode_2 As String) As Integer
            Dim same As Integer = 0
            Dim len As Integer = hashCode_1.Length
            Dim i As Integer = 0
            Do While (i < len)
                If (hashCode_1(i) <> hashCode_2(i)) Then
                    same = (same + 1)
                End If
                i = (i + 1)
            Loop
            Return same
        End Function
        ''' <summary>
        ''' Reduce size of image
        ''' </summary>
        ''' <param name="img">The image that should be shrinked</param>
        ''' <param name="width">new width of image</param>
        ''' <param name="height">new height of image</param>
        ''' <returns>resized image</returns>
        ''' <remarks>
        ''' 2015/09/26 Cocotus - First implementation
        ''' Used in AverageHash and PHash algorithm:
        ''' 1.Step: Reduce size of image to 8x8 (AverageHasg) or 32x32 (PHash)
        ''' </remarks>
        Private Shared Function Shrink(ByVal img As Image, width As Integer, height As Integer) As Bitmap
            Dim bmp As New Bitmap(width, height, Imaging.PixelFormat.Format32bppRgb)
            Dim tmpcanvas As Graphics = Graphics.FromImage(bmp)
            tmpcanvas.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            tmpcanvas.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
            tmpcanvas.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            tmpcanvas.DrawImage(img, 0, 0, width, height)
            tmpcanvas.Dispose()
            Return bmp
        End Function

        ''' <summary>
        ''' Reduce color of image and convert to grayscale
        ''' </summary>
        ''' <param name="img">The image that should be shrinked</param>
        ''' <param name="width">new width of image</param>
        ''' <param name="height">new height of image</param>
        ''' <returns>resized image</returns>
        ''' <remarks>
        ''' 2015/09/26 Cocotus - First implementation
        ''' Used in AverageHash and PHash algorithm:
        ''' 1.Step: Reduce size of image to 8x8 (AverageHasg) or 32x32 (PHash)
        ''' </remarks>
        Private Shared Function ReduceColors(ByVal img As Bitmap, width As Integer, height As Integer) As Integer()
            '2.Step: Reduce color. The tiny 8x8 picture is converted to a grayscale
            Dim pixels As Integer() = New Integer(((width * height)) - 1) {}
            Dim i As Integer = 0
            Do While (i < width)
                Dim j As Integer = 0
                Do While (j < height)
                    Dim color As Color = img.GetPixel(i, j)
                    pixels(((i * height) + j)) = RGBToGray(color.ToArgb)
                    j = (j + 1)
                Loop
                i = (i + 1)
            Loop
            Return pixels
        End Function
        ''' <summary>  
        ''' Convert image to grayscale
        ''' </summary>  
        ''' <param name="pixels"></param>  
        ''' <returns>pixel color value</returns>  
        ''' <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' Conversion helper used in AverageHash/PHash calculation
        ''' </remarks>
        Private Shared Function RGBToGray(ByVal pixels As Integer) As Integer
            Dim _red As Integer = ((pixels + 16) _
                        And 255)
            Dim _green As Integer = ((pixels + 8) _
                        And 255)
            Dim _blue As Integer = (pixels And 255)
            Return CType(((0.3 * _red) _
                        + ((0.59 * _green) + (0.11 * _blue))), Integer)
        End Function

#Region "AverageHash Algorithm"

        ''' <summary>
        ''' The algorithm used in this class is called "AverageHash"
        ''' </summary>
        ''' <remarks>
        ''' 2015/09/22 Cocotus - First implementation
        ''' </remarks>
        Partial Class AverageHash

            ''' <summary>
            ''' Computes the average hash of an image according to the algorithm provided by Dr. Neal Krawetz
            ''' on his blog: http://www.hackerfactor.com/blog/index.php?/archives/432-Looks-Like-It.html.
            ''' </summary>
            ''' <param name="imgtoHash">The image to calculate the hash value for</param>
            ''' <returns>The hash of the image as string</returns>
            ''' <remarks>
            ''' 2015/09/22 Cocotus - First implementation
            ''' Average Hash algorithm implemented as described in above blog.
            ''' 1.Step: Reduce size of image to 8x8 (shrink)
            ''' 2.Step: Reduce color. The tiny 8x8 picture is converted to a grayscale
            ''' 3.Step: Average the colors. Compute the mean value of the 64 colors.
            ''' 4.Step: Compute the bits. This is the fun part. Each bit is simply set based on whether the color value is above or below the mean.
            ''' 5.Step: Construct the hash
            ''' </remarks>
            Public Shared Function GetAverageHash(imgtoHash As Image) As String

                Dim width As Integer = 8
                Dim height As Integer = 8

                '1.Step:Reduce size of image (shrink)
                Dim shrinkedbmp = Shrink(imgtoHash, width, height)

                '2.Step: Reduce color
                Dim cleanedpixels = ReduceColors(shrinkedbmp, width, height)

                '3.Step: Average the colors. Compute the mean value of the 64 colors.
                Dim m As Single = 0
                Dim i As Integer = 0
                For i = 0 To cleanedpixels.Length - 1
                    m += cleanedpixels(i)
                Next
                m = m / cleanedpixels.Length
                Dim avgPixel As Integer = CInt(m)

                '4.Step: Compute the bits. This is the fun part. Each bit is simply set based on whether the color value is above or below the mean.
                ' Return 1-bits when the tone is equal to or above the average,
                ' and 0-bits when it's below the average.
                Dim comps() As Integer = New Integer(((width * height)) - 1) {}
                i = 0
                Do While (i < comps.Length)
                    If (cleanedpixels(i) >= avgPixel) Then
                        comps(i) = 1
                    Else
                        comps(i) = 0
                    End If
                    i = (i + 1)
                Loop

                '5.Step: Construct the hash
                Dim hashCode As System.Text.StringBuilder = New System.Text.StringBuilder
                i = 0
                Do While (i < comps.Length)
                    Dim result As Integer = ((comps(i) * CType(Math.Pow(2, 3), Integer)) _
                                + ((comps((i + 1)) * CType(Math.Pow(2, 2), Integer)) _
                                + ((comps((i + 2)) * CType(Math.Pow(2, 1), Integer)) _
                                + comps((i + 2)))))
                    hashCode.Append(BinaryToHex(result))
                    i = (i + 4)
                Loop

                shrinkedbmp.Dispose()
                'the final hashcode/identifier of the image
                Return hashCode.ToString
            End Function

            ''' <summary>
            ''' Conversion helper used in AverageHash calculation
            ''' </summary>
            ''' <remarks>
            ''' 2015/09/22 Cocotus - First implementation
            ''' </remarks>
            Private Shared Function BinaryToHex(ByVal binary As Integer) As Char
                Dim ch As Char = Microsoft.VisualBasic.ChrW(32)
                Select Case (binary)
                    Case 0
                        ch = Microsoft.VisualBasic.ChrW(48)
                    Case 1
                        ch = Microsoft.VisualBasic.ChrW(49)
                    Case 2
                        ch = Microsoft.VisualBasic.ChrW(50)
                    Case 3
                        ch = Microsoft.VisualBasic.ChrW(51)
                    Case 4
                        ch = Microsoft.VisualBasic.ChrW(52)
                    Case 5
                        ch = Microsoft.VisualBasic.ChrW(53)
                    Case 6
                        ch = Microsoft.VisualBasic.ChrW(54)
                    Case 7
                        ch = Microsoft.VisualBasic.ChrW(55)
                    Case 8
                        ch = Microsoft.VisualBasic.ChrW(56)
                    Case 9
                        ch = Microsoft.VisualBasic.ChrW(57)
                    Case 10
                        ch = Microsoft.VisualBasic.ChrW(97)
                    Case 11
                        ch = Microsoft.VisualBasic.ChrW(98)
                    Case 12
                        ch = Microsoft.VisualBasic.ChrW(99)
                    Case 13
                        ch = Microsoft.VisualBasic.ChrW(100)
                    Case 14
                        ch = Microsoft.VisualBasic.ChrW(101)
                    Case 15
                        ch = Microsoft.VisualBasic.ChrW(102)
                    Case Else
                        ch = Microsoft.VisualBasic.ChrW(32)
                End Select
                Return ch
            End Function
        End Class

#End Region

#Region "PHash Algorithm"

        ''' <summary>
        ''' The algorithm used in this class is called "PHash"
        ''' </summary>
        ''' <remarks>
        ''' 2015/09/26 Cocotus - First implementation
        ''' Its more robust but more complicated then the fast AverageHash algorithm, maybe use to compare posters?
        ''' </remarks>
        Private Class PHash

            ''' <summary>
            ''' Computes the PHash of an image according to the algorithm provided by Dr. Neal Krawetz
            ''' on his blog: http://www.hackerfactor.com/blog/index.php?/archives/432-Looks-Like-It.html.
            ''' </summary>
            ''' <param name="imgtoHash">The path to the image to calculate the hash value for</param>
            ''' <returns>The hash of the image as string</returns>
            ''' <remarks>
            ''' 2015/09/26 Cocotus - First implementation
            ''' PHash algorithm implemented as described in above blog.
            ''' 1.Step: Reduce size of image to 32x32 (shrink)
            ''' 2.Step: Reduce color. The tiny picture is converted to a grayscale
            ''' 3.Step: Compute the DCT
            ''' 4.Step: Compute the average value
            ''' 5.Step: Further reduce the DCT
            ''' 6.Step: Construct the hash
            ''' </remarks>
            Public Shared Function GetPHash(imgtoHash As Image) As String
                Dim width As Integer = 32
                Dim height As Integer = 32
                Dim hashCode = ""

                '1.Step:Reduce size of image (shrink)
                Dim shrinkedbmp = Shrink(imgtoHash, width, height)

                '2.Step: Reduce color
                Dim cleanedpixels = ReduceColors(shrinkedbmp, width, height)

                '3.Step: Compute the DCT
                Dim dctpixels = DCT.GetDCT(cleanedpixels, width)

                '4.Step: Calculate average pixel value of 8 x 8 area of image
                width = 8
                height = 8
                Dim sum As Integer = 0
                For i As Integer = 0 To height - 1
                    For j As Integer = 0 To width - 1
                        sum = sum + dctpixels(i * width + j)
                    Next
                Next
                Dim avr = CInt(sum / (width * height))

                '5/6.Step: Further reduce the DCT, Construct the hash
                Dim sb As New System.Text.StringBuilder()
                For i As Integer = 0 To height - 1
                    For j As Integer = 0 To width - 1
                        If dctpixels(i * width + j) >= avr Then
                            sb.Append("1")
                        Else
                            sb.Append("0")
                        End If
                    Next
                Next
                Dim result As Int64 = 0
                If sb(0) = "0"c Then
                    result = Convert.ToInt64(sb.ToString(), 2)
                Else
                    Dim ba = "1000000000000000000000000000000000000000000000000000000000000000"
                    result = Convert.ToInt64(ba, 2) Xor Convert.ToInt64(sb.ToString().Substring(1), 2)
                End If
                sb = New System.Text.StringBuilder(result.ToString("x"))
                If sb.Length < 16 Then
                    Dim n As Integer = 16 - sb.Length
                    For i As Integer = 0 To n - 1
                        sb.Insert(0, "0")
                    Next
                End If
                'the final imagehashcode
                hashCode = sb.ToString
                shrinkedbmp.Dispose()
                Return hashCode
            End Function

        End Class


        'DiscreteCosineTransformation algorithm, helper class for PHash
        Partial Private Class DCT
            Public Shared Function GetDCT(pix As Integer(), n As Integer) As Integer()
                Dim iMatrix As Double()() = DoubleArray(n, n)
                For i As Integer = 0 To n - 1
                    For j As Integer = 0 To n - 1
                        iMatrix(i)(j) = CDbl(pix(i * n + j))
                    Next
                Next
                Dim quotient As Double()() = Coefficient(n)
                Dim quotientT As Double()() = TransposingMatrix(quotient, n)
                Dim temp As Double()() = DoubleArray(n, n)
                temp = MatrixMultiply(quotient, iMatrix, n)
                iMatrix = MatrixMultiply(temp, quotientT, n)

                Dim newpix As Integer() = New Integer(n * n - 1) {}
                For i As Integer = 0 To n - 1
                    For j As Integer = 0 To n - 1
                        newpix(i * n + j) = CInt(iMatrix(i)(j))
                    Next
                Next
                Return newpix
            End Function
            Private Shared Function DoubleArray(m As Integer, n As Integer) As Double()()
                Dim Array As Double()()
                If m > -1 Then
                    Array = New Double(m - 1)() {}
                    If n > -1 Then
                        For i As Integer = 0 To m - 1
                            Array(i) = New Double(n - 1) {}
                        Next
                    End If
                Else
                    Array = Nothing
                End If

                Return Array
            End Function
            Private Shared Function Coefficient(n As Integer) As Double()()
                Dim coeff As Double()() = DoubleArray(n, n)
                Dim sqrt As Double = 1.0 / Math.Sqrt(n)
                For i As Integer = 0 To n - 1
                    coeff(0)(i) = sqrt
                Next
                For i As Integer = 1 To n - 1
                    For j As Integer = 0 To n - 1
                        coeff(i)(j) = Math.Sqrt(2.0 / n) * Math.Cos(i * Math.PI * (j + 0.5) / CDbl(n))
                    Next
                Next
                Return coeff
            End Function
            Private Shared Function TransposingMatrix(matrix As Double()(), n As Integer) As Double()()
                Dim nMatrix As Double()() = DoubleArray(n, n)
                For i As Integer = 0 To n - 1
                    For j As Integer = 0 To n - 1
                        nMatrix(i)(j) = matrix(j)(i)
                    Next
                Next
                Return nMatrix
            End Function
            Private Shared Function MatrixMultiply(A As Double()(), B As Double()(), n As Integer) As Double()()
                Dim nMatrix As Double()() = DoubleArray(n, n)
                Dim t As Double = 0.0
                For i As Integer = 0 To n - 1
                    For j As Integer = 0 To n - 1
                        t = 0
                        For k As Integer = 0 To n - 1
                            t += A(i)(k) * B(k)(j)
                        Next
                        nMatrix(i)(j) = t
                    Next
                Next
                Return nMatrix
            End Function
        End Class

#End Region

    End Class

End Class