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

Imports EmberAPI
Imports System.Drawing

Namespace EmberTests


    <TestClass()> Public Class Test_clsAPIImages

        '<UnitTest>
        '<TestMethod()>
        'Public Sub Images_GetFanartDims_NothingParameter()
        '    'Arrange

        '    'Act
        '    Dim result As Enums.FanartSize = Images.GetFanartDims(Nothing)
        '    'Assert
        '    Assert.AreEqual(result, Enums.FanartSize.Small, "Nothing parameter")
        'End Sub
        '<UnitTest>
        '<TestMethod()>
        'Public Sub Images_GetFanartDims()
        '    'Arrange
        '    Dim source As New Dictionary(Of Image, Enums.FanartSize)
        '    With source
        '        'Bitmap already prevents 0 and negative size Bitmaps. Can not test them here.
        '        .Add(New Bitmap(2000, 2000), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(2000, 1001), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(2000, 1000), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(2000, 751), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(2000, 750), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(2000, 401), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(2000, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(2000, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(1001, 2000), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(1001, 1001), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(1001, 1000), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(1001, 751), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(1001, 750), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(1001, 401), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(1001, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(1001, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(1000, 2000), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(1000, 1001), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(1000, 1000), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(1000, 751), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(1000, 750), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(1000, 401), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(1000, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(1000, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(751, 2000), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(751, 1001), Enums.FanartSize.Lrg)
        '        .Add(New Bitmap(751, 1000), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(751, 751), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(751, 750), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(751, 401), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(751, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(751, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(750, 2000), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(750, 1001), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(750, 1000), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(750, 751), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(750, 750), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(750, 401), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(750, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(750, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(401, 2000), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(401, 1001), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(401, 1000), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(401, 751), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(401, 750), Enums.FanartSize.Mid)
        '        .Add(New Bitmap(401, 401), Enums.FanartSize.Small)
        '        .Add(New Bitmap(401, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(401, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(400, 2000), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 1001), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 1000), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 751), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 750), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 401), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(400, 50), Enums.FanartSize.Small)

        '        .Add(New Bitmap(50, 2000), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 1001), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 1000), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 751), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 750), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 401), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 400), Enums.FanartSize.Small)
        '        .Add(New Bitmap(50, 50), Enums.FanartSize.Small)
        '    End With

        '    For Each pair As KeyValuePair(Of Image, Enums.FanartSize) In source
        '        'Act
        '        Dim returnValue As Enums.FanartSize = Images.GetFanartDims(pair.Key)

        '        'Assert
        '        Assert.AreEqual(pair.Value, returnValue, "Expected: <{0}>, Received: <{1}>, Value: <{2}x{3}>", pair.Value, returnValue, pair.Key.Width, pair.Key.Height)
        '    Next

        '    'CLEANUP!
        '    'Have to do some manual iterating to dispose of the bitmaps, or risk memory leaks
        '    Dim enumerator As System.Collections.IEnumerator = source.GetEnumerator()
        '    While enumerator.MoveNext()
        '        'get the pair of Dictionary
        '        Dim pair As KeyValuePair(Of Image, Enums.FanartSize) = enumerator.Current
        '        'dispose it
        '        pair.Key.Dispose()
        '    End While
        'End Sub

    End Class
End Namespace
