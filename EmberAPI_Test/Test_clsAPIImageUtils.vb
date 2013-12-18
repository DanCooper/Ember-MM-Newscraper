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
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows.Forms

Imports EmberAPI
Imports System.Drawing

Namespace EmberTests


    <TestClass()> Public Class Test_clsAPIImageUtils

        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_AddMissingStamp_NothingParameter()
            'Arrange

            'Act
            Dim result As Images = ImageUtils.AddMissingStamp(Nothing)
            'Assert
            Assert.IsTrue(result Is Nothing, "Nothing parameter")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_AddMissingStamp()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using img As Images = New EmberAPI.Images()
                img.UpdateMSfromImg(My.Resources.TestPattern)

                'Act
                Using result As EmberAPI.Images = ImageUtils.AddMissingStamp(img)
                    'result.Image.Save("AddMissingStamp.png", System.Drawing.Imaging.ImageFormat.Png)
                    Using dialog As ImageFeedback = New ImageFeedback()
                        dialog.LoadInfo(result.Image, "Is there a Missing stamp across the top left?")
                        userResponse = dialog.ShowDialog()
                    End Using
                End Using
            End Using
            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_Grayscale_NothingParameter()
            'Arrange

            'Act
            Dim result As Images = ImageUtils.GrayScale(Nothing)
            'Assert
            Assert.IsTrue(result Is Nothing, "Nothing parameter")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_GrayScale()
            'Arrange
            Dim userResponse As System.Windows.Forms.DialogResult = DialogResult.None
            Using img As Images = New EmberAPI.Images()
                img.UpdateMSfromImg(My.Resources.TestPattern)

                'Act
                Using result As EmberAPI.Images = ImageUtils.GrayScale(img)
                    'result.Image.Save("Grayscale.png", System.Drawing.Imaging.ImageFormat.Png)
                    Using dialog As ImageFeedback = New ImageFeedback()
                        dialog.LoadInfo(result.Image, "Is this image in grayscale?")
                        userResponse = dialog.ShowDialog()
                    End Using
                End Using

            End Using
            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")

        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_DrawGradEllipse_NothingParameter()
            'Arrange
            Dim userResponse As System.Windows.Forms.DialogResult = DialogResult.None
            Using img = My.Resources.TestPattern_Med, _
                g = Graphics.FromImage(img), _
                font = New Font("Arial", 8, FontStyle.Bold)

                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

                Dim message = String.Format("{0} x {1}", img.Width, img.Height)

                Dim messageSize = g.MeasureString(message, font).ToSize()
                Dim paddingSize = New Size(15, 2)

                Dim x = (img.Width - messageSize.Width) \ 2 - paddingSize.Width
                Dim y = (img.Height - messageSize.Height) \ 2 - paddingSize.Height
                Dim width = messageSize.Width + paddingSize.Width * 2
                Dim height = messageSize.Height + paddingSize.Height * 2
                Dim rect = New Rectangle(x, y, width, height)

                Dim centerColor = Color.FromArgb(250, 120, 120, 120)
                Dim outerColor = Color.FromArgb(100, 255, 255, 255)
                'Dim rect = New Rectangle(Convert.ToInt32((img.Width - messageSize.Width) / 2 - 15), img.Height - 25, messageSize.Width + 30, 25)

                'Act
                ImageUtils.DrawGradEllipse(Nothing, rect, centerColor, outerColor)

                g.DrawString(message, font, New SolidBrush(Color.White), (img.Width - messageSize.Width) \ 2, (img.Height - messageSize.Height) \ 2)
                'img.Save("DrawGradEllipse.png", System.Drawing.Imaging.ImageFormat.Png)
                Using dialog As ImageFeedback = New ImageFeedback()
                    dialog.LoadInfo(img, "Do you see the image size (300x236) in the center of the image, with NO surrounding ellipse?")
                    userResponse = dialog.ShowDialog()
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_DrawGradEllipse()
            'Arrange
            Dim userResponse As System.Windows.Forms.DialogResult = DialogResult.None
            Using img = My.Resources.TestPattern_Med, _
                g = Graphics.FromImage(img), _
                font = New Font("Arial", 8, FontStyle.Bold)

                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

                Dim message = String.Format("{0} x {1}", img.Width, img.Height)

                Dim messageSize = g.MeasureString(message, font).ToSize()
                Dim paddingSize = New Size(15, 2)

                Dim x = (img.Width - messageSize.Width) \ 2 - paddingSize.Width
                Dim y = (img.Height - messageSize.Height) \ 2 - paddingSize.Height
                Dim width = messageSize.Width + paddingSize.Width * 2
                Dim height = messageSize.Height + paddingSize.Height * 2
                Dim rect = New Rectangle(x, y, width, height)

                Dim centerColor = Color.FromArgb(250, 120, 120, 120)
                Dim outerColor = Color.FromArgb(100, 255, 255, 255)
                'Dim rect = New Rectangle(Convert.ToInt32((img.Width - messageSize.Width) / 2 - 15), img.Height - 25, messageSize.Width + 30, 25)

                'Act
                ImageUtils.DrawGradEllipse(g, rect, centerColor, outerColor)

                g.DrawString(message, font, New SolidBrush(Color.White), (img.Width - messageSize.Width) \ 2, (img.Height - messageSize.Height) \ 2)
                'img.Save("DrawGradEllipse.png", System.Drawing.Imaging.ImageFormat.Png)
                Using dialog As ImageFeedback = New ImageFeedback()
                    dialog.LoadInfo(img, "Do you see the image size (300x236) in the center of the image, surrounded by a faint white ellipse with a gray center?")
                    userResponse = dialog.ShowDialog()
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_NothingImage()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = Nothing

            Dim maxWidth = 200
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)
            success = image Is Nothing

            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_ZeroWidth()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern     ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 0
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)

            'Assert
            Dim widthOK = image.Width = My.Resources.TestPattern.Width
            Dim heightOK = image.Height = My.Resources.TestPattern.Height
            success = widthOK And heightOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_ZeroHeight()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern     ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 200
            Dim maxHeight = 0

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)

            'Assert
            Dim widthOK = image.Width = My.Resources.TestPattern.Width
            Dim heightOK = image.Height = My.Resources.TestPattern.Height
            success = widthOK And heightOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_NegativeWidth()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern  ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = -200
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)

            'Assert
            Dim widthOK = image.Width = My.Resources.TestPattern.Width
            Dim heightOK = image.Height = My.Resources.TestPattern.Height
            success = widthOK And heightOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_NegativeHeight()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern     ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 200
            Dim maxHeight = -200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)

            'Assert
            Dim widthOK = image.Width = My.Resources.TestPattern.Width
            Dim heightOK = image.Height = My.Resources.TestPattern.Height
            success = widthOK And heightOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Shrink()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern     ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 200
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with no padding?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
       <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Grow()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern_Med     ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 800
            Dim maxHeight = 800

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight)

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with no padding?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using
            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Shrink_WithPadding()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern  ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 200
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True)

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with Black padding on the top and bottom?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing
            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
       <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Grow_WithPadding()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern_Med   ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 800
            Dim maxHeight = 800

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True)

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with Black padding on top and bottom?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Shrink_WithGreenPaddingTopBottom()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern  ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 200
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True, Color.Green.ToArgb())

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with Green padding on top and bottom?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK

            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Shrink_WithGreenPaddingSides()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern  ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 400
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True, Color.Green.ToArgb())

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with bottom portion missing?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_ShrinkVert_WithGreenPaddingSides()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern_Vert  ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 400
            Dim maxHeight = 200

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True, Color.Green.ToArgb())

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with Green padding on the sides?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
         <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Grow_WithGreenPaddingTopBottom()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern_Med  ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 800
            Dim maxHeight = 800

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True, Color.Green.ToArgb())

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with Green padding on top and bottom?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_Grow_WithGreenPaddingSides()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern_Med     ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 800
            Dim maxHeight = 400

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True, Color.Green.ToArgb())

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with bottom portion missing?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_width_height_padding_GrowVert_WithGreenPaddingSides()
            'Arrange
            Dim success As Boolean = False
            Dim image As Image = My.Resources.TestPattern_Med_Vert    ' Cannot use "using" because of ByVal in ResizeImage

            Dim maxWidth = 800
            Dim maxHeight = 400

            'Act
            ImageUtils.ResizeImage(image, maxWidth, maxHeight, True, Color.Green.ToArgb())

            'Assert
            If image Is Nothing Then Assert.Fail()
            Dim widthOK = image.Width <= maxWidth
            Dim heightOK = image.Height <= maxHeight
            Dim scaleOK = (image.Width = maxWidth) OrElse (image.Height = maxHeight)

            Dim visualOK = False
            Using dialog = New ImageFeedback()
                dialog.LoadInfo(image, "Is the image close-cropped, with Green padding on the sides?")

                Dim userResponse = dialog.ShowDialog()
                visualOK = userResponse = Windows.Forms.DialogResult.Yes    'Yes means it looked good
            End Using

            success = widthOK And heightOK And scaleOK And visualOK
            image.Dispose()
            image = Nothing

            Assert.IsTrue(success)
        End Sub
        <UnitTest>
         <TestMethod()>
        Public Sub ImageUtils_ResizePB_NothingSourceImage()
            'Arrange
            Dim success As Boolean = False
            Dim destImage As PictureBox = New PictureBox()
            Using sourceImage As PictureBox = New PictureBox()

                Dim boxWidth = 600
                Dim boxHeight = 400

                'Act
                ImageUtils.ResizePB(destImage, sourceImage, boxHeight, boxWidth)
                success = destImage.Image Is Nothing
            End Using
            destImage.Dispose()
            destImage = Nothing
            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizePB_NothingSource()
            'Arrange
            Dim success As Boolean = False
            Dim destImage As PictureBox = New PictureBox()

            Dim boxWidth = 600
            Dim boxHeight = 400

            'Act
            ImageUtils.ResizePB(destImage, Nothing, boxHeight, boxWidth)
            success = destImage.Image Is Nothing
            destImage.Dispose()
            destImage = Nothing
            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizePB_Wide()
            'Arrange
            Dim success As Boolean = False
            Dim destImage As PictureBox = New PictureBox()
            Using sourceImage As PictureBox = New PictureBox()

                sourceImage.Image = My.Resources.TestPattern_Med
                Dim boxWidth = 600
                Dim boxHeight = 400

                'Act
                ImageUtils.ResizePB(destImage, sourceImage, boxHeight, boxWidth)

                Dim widthOK As Boolean = destImage.Width <= IIf(sourceImage.Image.Width >= boxWidth, boxWidth, sourceImage.Image.Width)
                Dim heightOK As Boolean = destImage.Height <= IIf(sourceImage.Image.Height >= boxHeight, boxHeight, sourceImage.Image.Height)
                success = widthOK And heightOK
            End Using
            destImage.Dispose()
            destImage = Nothing

            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizePB_Tall()
            'Arrange
            Dim success As Boolean = False
            Dim destImage As PictureBox = New PictureBox()
            Using sourceImage As PictureBox = New PictureBox()

                sourceImage.Image = My.Resources.TestPattern_Med
                Dim boxWidth = 400
                Dim boxHeight = 600

                'Act
                ImageUtils.ResizePB(destImage, sourceImage, boxHeight, boxWidth)

                'Assert
                Dim widthOK As Boolean = destImage.Width <= IIf(sourceImage.Image.Width >= boxWidth, boxWidth, sourceImage.Image.Width)
                Dim heightOK As Boolean = destImage.Height <= IIf(sourceImage.Image.Height >= boxHeight, boxHeight, sourceImage.Image.Height)
                success = widthOK And heightOK
            End Using
            destImage.Dispose()
            destImage = Nothing

            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizePB_Tall_Shrink()
            'Arrange
            Dim success As Boolean = False
            Dim destImage As PictureBox = New PictureBox()
            Using sourceImage As PictureBox = New PictureBox()

                sourceImage.Image = My.Resources.TestPattern_Med
                Dim boxWidth = 200
                Dim boxHeight = 400

                'Act
                ImageUtils.ResizePB(destImage, sourceImage, boxHeight, boxWidth)

                'Assert
                Dim widthOK As Boolean = destImage.Width <= IIf(sourceImage.Image.Width >= boxWidth, boxWidth, sourceImage.Image.Width)
                Dim heightOK As Boolean = destImage.Height <= IIf(sourceImage.Image.Height >= boxHeight, boxHeight, sourceImage.Image.Height)
                success = widthOK And heightOK
            End Using
            destImage.Dispose()
            destImage = Nothing

            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizePB_Wide_Shrink()
            'Arrange
            Dim success As Boolean = False
            Dim destImage As PictureBox = New PictureBox()
            Using  sourceImage As PictureBox = New PictureBox()

                sourceImage.Image = My.Resources.TestPattern_Med
                Dim boxWidth = 400
                Dim boxHeight = 200

                'Act
                ImageUtils.ResizePB(destImage, sourceImage, boxHeight, boxWidth)

                'Assert
                Dim widthOK As Boolean = destImage.Width <= IIf(sourceImage.Image.Width >= boxWidth, boxWidth, sourceImage.Image.Width)
                Dim heightOK As Boolean = destImage.Height <= IIf(sourceImage.Image.Height >= boxHeight, boxHeight, sourceImage.Image.Height)
                success = widthOK And heightOK
            End Using
            destImage.Dispose()
            destImage = Nothing

            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_SetGlassOverlay_NothingParameter()
            'Arrange
            Dim source As PictureBox = Nothing
            'Act
            ImageUtils.SetGlassOverlay(source)

            'Assert
            Assert.IsNull(source, "Expected Nothing, got something else")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_SetGlassOverlay_NothingImage()
            'Arrange
            Dim source As PictureBox = New PictureBox()
            source.Image = Nothing
            'Act
            ImageUtils.SetGlassOverlay(source)

            'Assert
            Assert.IsNull(source.Image, "Expected Nothing, got something else")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_SetGlassOverlay_NormalImage_Horizontal()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using source As PictureBox = New PictureBox()
                source.Image = My.Resources.TestPattern_Med

                'Act
                ImageUtils.SetGlassOverlay(source)

                Using dialog As ImageFeedback = New ImageFeedback()
                    dialog.LoadInfo(source.Image, "Does the image have a Glass overlay?")
                    userResponse = dialog.ShowDialog()
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_SetGlassOverlay_NormalImage_Vertical()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using source As PictureBox = New PictureBox()
                source.Image = My.Resources.TestPattern_Med_Vert

                'Act
                ImageUtils.SetGlassOverlay(source)

                Using dialog As ImageFeedback = New ImageFeedback()
                    dialog.LoadInfo(source.Image, "Does the image have a Glass overlay?")
                    userResponse = dialog.ShowDialog()
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_SetOverlay_NothingImage()
            'Arrange
            Dim overlay As Image = EmberAPI.My.Resources.haslanguage
            'Act
            Dim result As Image = ImageUtils.SetOverlay(Nothing, 500, 500, overlay, 1)

            'Assert
            Assert.IsNull(result, "Expected Nothing, got something else")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_SetOverlay_TopLeft()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using img As Image = My.Resources.TestPattern, _
                overlay As Image = EmberAPI.My.Resources.haslanguage

                'Act
                Using result As Image = ImageUtils.SetOverlay(img, img.Width \ 2, img.Height \ 2, overlay, 1)
                    'result.Save("TestPatternWithLanguage_Top_Left.png", System.Drawing.Imaging.ImageFormat.Png)
                    Using dialog As ImageFeedback = New ImageFeedback()
                        dialog.LoadInfo(result, "Is there a message bubble in the top-left corner?")
                        userResponse = dialog.ShowDialog()
                    End Using
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_SetOverlay_TopRight()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using img As Image = My.Resources.TestPattern, _
                overlay As Image = EmberAPI.My.Resources.haslanguage

                'Act
                Using result As Image = ImageUtils.SetOverlay(img, img.Width \ 2, img.Height \ 2, overlay, 2)
                    'result.Save("TestPatternWithLanguage_Top_Right.png", System.Drawing.Imaging.ImageFormat.Png)
                    Using dialog As ImageFeedback = New ImageFeedback()
                        dialog.LoadInfo(result, "Is there a message bubble in the top-right corner?")
                        userResponse = dialog.ShowDialog()
                    End Using
                End Using
            End Using
            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_SetOverlay_BottomLeft()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using img As Image = My.Resources.TestPattern, _
                overlay As Image = EmberAPI.My.Resources.haslanguage

                'Act
                Using result As Image = ImageUtils.SetOverlay(img, img.Width \ 2, img.Height \ 2, overlay, 3)
                    'result.Save("TestPatternWithLanguage_Bottom_Left.png", System.Drawing.Imaging.ImageFormat.Png)
                    Using dialog As ImageFeedback = New ImageFeedback()
                        dialog.LoadInfo(result, "Is there a message bubble in the lower-left corner?")
                        userResponse = dialog.ShowDialog()
                    End Using
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_SetOverlay_BottomRight()
            'Arrange
            Dim userResponse As Windows.Forms.DialogResult = Nothing
            Using img As Image = My.Resources.TestPattern, _
                overlay As Image = EmberAPI.My.Resources.haslanguage
                'Act
                Using result As Image = ImageUtils.SetOverlay(img, img.Width \ 2, img.Height \ 2, overlay, 4)
                    'result.Save("TestPatternWithLanguage_Bottom_Right.png", System.Drawing.Imaging.ImageFormat.Png)
                    Using dialog As ImageFeedback = New ImageFeedback()
                        dialog.LoadInfo(result, "Is there a message bubble in the lower-right corner?")
                        userResponse = dialog.ShowDialog()
                    End Using
                End Using
            End Using

            'Assert
            Assert.IsTrue(userResponse = Windows.Forms.DialogResult.Yes, "User disagreed")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_JPEGCompression()
            'Arrange
            'Act
            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_image_size_NothingImage()
            'Arrange
            Dim success As Boolean = False

            Using source As Image = Nothing
                Dim size As Size = New Size(100, 400)
                'Act
                Using result As Image = ImageUtils.ResizeImage(source, size)
                    success = result Is Nothing
                End Using

            End Using

            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_image_size_NothingSize()
            'Arrange
            Dim success As Boolean = False
            Using source As Image = My.Resources.TestPattern
                Dim size As Size = Nothing
                'Act
                Using result As Image = ImageUtils.ResizeImage(source, size)

                    If result Is Nothing Then Assert.Fail()
                    Dim widthOK = result.Width = source.Width
                    Dim heightOK = result.Height = source.Height
                    success = widthOK And heightOK
                End Using
            End Using
            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_image_size_ZeroSize()
            'Arrange
            Dim success As Boolean = False
            Using source As Image = My.Resources.TestPattern
                Dim size As Size = New Size(0, 0)
                'Act
                Using result As Image = ImageUtils.ResizeImage(source, size)

                    If result Is Nothing Then Assert.Fail()
                    Dim widthOK = result.Width = source.Width
                    Dim heightOK = result.Height = source.Height
                    success = widthOK And heightOK
                End Using
            End Using
            'Assert
            Assert.IsTrue(success)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_image_size_NegativeSize()
            'Arrange
            Dim success As Boolean = False
            Using source As Image = My.Resources.TestPattern
                Dim size As Size = New Size(-200, -200)
                'Act
                Using result As Image = ImageUtils.ResizeImage(source, size)

                    If result Is Nothing Then Assert.Fail()
                    Dim widthOK = result.Width = source.Width
                    Dim heightOK = result.Height = source.Height
                    success = widthOK And heightOK
                End Using
            End Using
            'Assert
            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_image_sizeTall()
            'Arrange
            Dim success As Boolean = False
            Using source As Image = My.Resources.TestPattern
                Dim size As Size = New Size(100, 400)
                'Act
                Using result As Image = ImageUtils.ResizeImage(source, size)

                    'Assert
                    If result Is Nothing Then Assert.Fail()
                    Dim widthOK = result.Width <= size.Width
                    Dim heightOK = result.Height <= size.Height
                    Dim scaleOK = (result.Width = size.Width) OrElse (result.Height = size.Height)

                    Dim visualOK = False
                    Using dialog = New ImageFeedback()
                        dialog.LoadInfo(result, "Is this image squished or stretched?")

                        Dim userResponse = dialog.ShowDialog()
                        visualOK = userResponse = Windows.Forms.DialogResult.No    'No means it is not stretched
                    End Using
                    success = widthOK And heightOK And scaleOK And visualOK
                End Using
            End Using

            Assert.IsTrue(success)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub ImageUtils_ResizeImage_image_sizeWide()
            'Arrange
            Dim success As Boolean = False
            Using source As Image = My.Resources.TestPattern
                Dim size As Size = New Size(400, 100)
                'Act
                Using result As Image = ImageUtils.ResizeImage(source, size)

                    'Assert
                    If result Is Nothing Then Assert.Fail()
                    Dim widthOK = result.Width <= size.Width
                    Dim heightOK = result.Height <= size.Height
                    Dim scaleOK = (result.Width = size.Width) OrElse (result.Height = size.Height)

                    Dim visualOK = False
                    Using dialog = New ImageFeedback()
                        dialog.LoadInfo(result, "Is this image squished or stretched?")

                        Dim userResponse = dialog.ShowDialog()
                        visualOK = userResponse = Windows.Forms.DialogResult.No    'No means it is not stretched
                    End Using
                    success = widthOK And heightOK And scaleOK And visualOK
                End Using
            End Using

            Assert.IsTrue(success)
        End Sub

        <UnitTest>
        <TestMethod()>
        Public Sub ImageUtils_AddGenreString()
            'Arrange
            Dim success As Boolean = False
            Using img As Image = My.Resources.Genre
                Dim genreString As String = "Fantasy"
                'Act
                Using result As Image = ImageUtils.AddGenreString(img, genreString)

                    'Assert
                    If result Is Nothing Then Assert.Fail()
                    Dim sizeOK = result.Size = img.Size

                    Dim visualOK = False
                    Using dialog = New ImageFeedback()
                        dialog.LoadInfo(result, "Does this image have " & genreString & " written on it?")

                        Dim userResponse = dialog.ShowDialog()
                        visualOK = userResponse = Windows.Forms.DialogResult.No    'No means it is not stretched
                    End Using
                    success = sizeOK And visualOK
                End Using
            End Using
        End Sub

    End Class
End Namespace
