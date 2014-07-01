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
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports NLog

<Assembly: InternalsVisibleTo("EmberAPI_Test")> 

Public Class ImageUtils

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Const DefaultPaddingARGB As Integer = -16777216  'This is FF000000, which is completely opaque black
#End Region

#Region "Methods"
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
                Dim oWidth As Integer = If(nImage.Width >= Global.EmberAPI.My.Resources.missing.Width, Global.EmberAPI.My.Resources.missing.Width, nImage.Width)
                Dim oheight As Integer = If(nImage.Height >= Global.EmberAPI.My.Resources.missing.Height, Global.EmberAPI.My.Resources.missing.Height, nImage.Height)
                grOverlay.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                grOverlay.DrawImage(Global.EmberAPI.My.Resources.missing, 0, 0, oWidth, oheight)
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
               {New Single() {0.5, 0.5, 0.5, 0, 0}, _
                New Single() {0.5, 0.5, 0.5, 0, 0}, _
                New Single() {0.5, 0.5, 0.5, 0, 0}, _
                New Single() {0, 0, 0, 1, 0}, _
                New Single() {0, 0, 0, 0, 1}})

            Dim ia As Imaging.ImageAttributes = New Imaging.ImageAttributes()
            ia.SetColorMatrix(cm)
            g.DrawImage(nImage, New Rectangle(0, 0, nImage.Width, nImage.Height), 0, 0, nImage.Width, nImage.Height, GraphicsUnit.Pixel, ia)
        End Using

        oImage.UpdateMSfromImg(nImage)

        Return oImage
    End Function
    ''' <summary>
    ''' Draw a gradiated ellipse on the supplied <paramref name="graphics"/>, defined by <paramref name="bounds"/>,
    ''' with a line color of <paramref name="color2"/> and a center/fill of <paramref name="color1"/>.
    ''' </summary>
    ''' <param name="graphics"><c>Graphics</c> surface to draw on</param>
    ''' <param name="bounds"><c>Rectangle</c> that defines the boundaries of the ellipse</param>
    ''' <param name="centerColor">The center <c>Color</c></param>
    ''' <param name="outerColor">The outer edge <c>Color</c></param>
    ''' <remarks></remarks>
    Public Shared Sub DrawGradEllipse(ByRef graphics As Graphics, ByVal bounds As Rectangle, ByVal centerColor As Color, ByVal outerColor As Color)
        'Some quick-and-dirty sanity checking
        If graphics Is Nothing OrElse (bounds.Width = 0 And bounds.Height = 0) Then Return

        Try
            Using gPath As New Drawing2D.GraphicsPath
                gPath.AddEllipse(bounds.X, bounds.Y, bounds.Width, bounds.Height)
                Using pgBrush = New Drawing2D.PathGradientBrush(gPath)
                    pgBrush.CenterColor = centerColor
                    pgBrush.SurroundColors = New Color() {outerColor, outerColor, outerColor, outerColor}
                    graphics.FillEllipse(pgBrush, bounds.X, bounds.Y, bounds.Width, bounds.Height)
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
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
        If (_image Is Nothing) OrElse _
            (maxWidth <= 0) OrElse _
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
                Dim bmDest As New Bitmap( _
                Convert.ToInt32(bmSource.Width * sPropPerc), _
                Convert.ToInt32(bmSource.Height * sPropPerc))
                ' Make a Graphics object for the result Bitmap.
                Using grDest As Graphics = Graphics.FromImage(bmDest)
                    grDest.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    ' Copy the source image into the destination bitmap.
                    grDest.DrawImage(bmSource, New Rectangle(0, 0, _
                    bmDest.Width, bmDest.Height), New Rectangle(0, 0, _
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Copies the image from <paramref name="pbSource"/> into <paramref name="pbDestination"/>,
    ''' resizing it (if necessary) so that it is no larger than the supplied width and height.
    ''' It only shrinks, but does not grow the image.
    '''
    ''' </summary>
    ''' <param name="pbDestination">Destination picture box</param>
    ''' <param name="pbSource">Source picture box</param>
    ''' <param name="maxHeight">Maximum height for <paramref name="pbResize"/></param>
    ''' <param name="maxWidth">Maximum width for <paramref name="pbResize"/></param>
    ''' <remarks>Why not use "Zoom" for fanart background? - To keep the image at the top. Zoom centers vertically.</remarks>
    Public Shared Sub ResizePB(ByRef pbDestination As PictureBox, ByRef pbSource As PictureBox, ByVal maxHeight As Integer, ByVal maxWidth As Integer)
        If pbSource Is Nothing OrElse pbSource.Image Is Nothing Then Return

        If Not IsNothing(pbSource.Image) Then
            Try
                pbDestination.SizeMode = PictureBoxSizeMode.Normal
                Dim sPropPerc As Single = 1.0 'no default scaling

                pbDestination.Size = New Size(maxWidth, maxHeight)

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

                ' Get the source bitmap.
                Using bmSource As New Bitmap(pbSource.Image)
                    ' Make a bitmap for the result.
                    Dim bmDest As New Bitmap( _
                            Convert.ToInt32(bmSource.Width * sPropPerc), _
                            Convert.ToInt32(bmSource.Height * sPropPerc))
                    ' Make a Graphics object for the result Bitmap.
                    Using grDest As Graphics = Graphics.FromImage(bmDest)
                        ' Copy the source image into the destination bitmap.
                        grDest.DrawImage(bmSource, 0, 0, _
                                         bmDest.Width + 1, _
                                         bmDest.Height + 1)
                        ' Display the result.
                        If Not pbDestination.Image Is Nothing Then pbDestination.Image.Dispose()
                        pbDestination.Image = bmDest

                        'tweak pb after resizing pic
                        pbDestination.Width = pbDestination.Image.Width
                        pbDestination.Height = pbDestination.Image.Height

                        'Clean up
                        bmDest = Nothing
                    End Using
                End Using

            Catch ex As Exception
                pbDestination.Left = 0
                pbDestination.Size = New Size(maxWidth, maxHeight)
                logger.Error(New StackFrame().GetMethod().Name, ex)
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

                grOverlay.DrawImage(My.Resources.overlay, 0, 0, pbUnderlay.Image.Width, bmHeight)
                pbUnderlay.Image = bmOverlay

                bmOverlay = New Bitmap(pbUnderlay.Image)
            End Using
            Using grOverlay = Graphics.FromImage(bmOverlay)

                grOverlay.DrawImage(My.Resources.overlay2, 0, 0, pbUnderlay.Image.Width, pbUnderlay.Image.Height)
                pbUnderlay.Image = bmOverlay

            End Using
            bmOverlay = Nothing
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return imgUnderlay
    End Function

    'cocotus, 2013/02 Export HTML expanded: configurable resizable images
    ' New Image methods added here (resizing/compressing)

    ''' <summary>
    ''' Compress JPEG <c>Image</c> and save result as local image
    ''' </summary>
    ''' <param name="Image">Image which should be compressed/encoded</param>
    ''' <param name="OutPutFile">Savepath of recoded image</param>
    ''' <param name="Qualitiy">Quality Setting 0-100</param>
    Public Shared Sub JPEGCompression(ByVal Image As Image, ByVal OutPutFile As String, ByVal Qualitiy As Integer)
        If String.IsNullOrEmpty(OutPutFile) Then Return

        Dim ImageCodecs() As Imaging.ImageCodecInfo
        Dim ImageParameters As Imaging.EncoderParameters

        ImageCodecs = Imaging.ImageCodecInfo.GetImageEncoders()
        ImageParameters = New Imaging.EncoderParameters(1)
        ImageParameters.Param(0) = New Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Qualitiy)
        Image.Save(OutPutFile, ImageCodecs(1), ImageParameters)
    End Sub
    ''' <summary>
    ''' Resize the supplied <c>Image</c>
    ''' </summary>
    ''' <param name="poImage"><c>Image</c> which should be resized</param>
    ''' <param name="poSize"><c>Size</c> of image</param>
    Public Shared Function ResizeImage(ByVal poImage As Image, ByVal poSize As Size) As Image
        'TODO 2013/12/16 Dekker500 - This method fails unit tests. This STRETCHES the images, not a true resize. Don't think this is what is actually desired

        If (poImage Is Nothing) _
            OrElse (poSize.IsEmpty) _
            OrElse (poSize.Width <= 0 OrElse poSize.Height <= 0) Then
            Return poImage
        End If
        Dim ResizedImage As Image
        Using Original As Image = DirectCast(poImage.Clone(), Image)
            ResizedImage = New Bitmap(poSize.Width, poSize.Height, Original.PixelFormat)
            Using oGraphic As Graphics = Graphics.FromImage(ResizedImage)
                oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                Dim oRectangle As Rectangle = New Rectangle(0, 0, poSize.Width, poSize.Height)
                oGraphic.DrawImage(Original, oRectangle)
            End Using
        End Using

        Return ResizedImage
    End Function

    'cocotus end

    ''' <summary>
    ''' Adds the supplied <paramref name="genreString"/> to the given <paramref name="image"/>
    ''' </summary>
    ''' <param name="image">Source <c>Image</c> to manipulate</param>
    ''' <param name="genreString"><c>String</c> to superimpose</param>
    ''' <remarks>If an error is encountered, the source image is returned.</remarks>
    Public Shared Function AddGenreString(ByRef image As System.Drawing.Image, genreString As String) As Bitmap
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return bmGenre

    End Function

#End Region 'Methods

End Class