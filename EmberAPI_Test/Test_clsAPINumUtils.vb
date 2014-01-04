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
Imports UnitTests

Namespace EmberTests


    <TestClass()> Public Class Test_clsAPINumUtils
        
        <UnitTest>
        <TestMethod()>
        Public Sub NumUtils_ConvertToSingle_NothingParameter()
            'Arrange

            'Act
            Dim result As Single = NumUtils.ConvertToSingle(Nothing)
            'Assert
            Assert.IsTrue(result = 0, "Nothing parameter")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub NumUtils_ConvertToSingle()
            'Arrange
            Dim source As New Dictionary(Of String, Single)
            With source
                .Add(String.Empty, 0.0F)
                .Add("0", 0.0F)
                .Add("1", 1.0F)
                .Add("2", 2.0F)
                .Add("-1", -1.0F)
                .Add("-2", -2.0F)
                .Add(Single.MaxValue.ToString("G9"), Single.MaxValue)  'Must specify G9 or else 2 significant digits are tossed
                .Add(Single.MinValue.ToString("G9"), Single.MinValue)  'Must specify G9 or else 2 significant digits are tossed
                'The following two tests are untestable since Single's precision is not adequate to add small numbers (like 1 or 10)
                '.Add((Convert.ToDouble(Single.MaxValue) + 1).ToString("G9"), 0F) 'Need to use Double since Single would fail
                '.Add((Convert.ToDouble(Single.MinValue) - 1).ToString("G9"), 0F)
                .Add("999999999999999999999999999", 1.0E+27F)
                .Add("-999999999999999999999999999", -1.0E+27F)
                .Add("999999999999999999999999999.99999", 1.0E+27F)
                .Add("-999999999999999999999999999.99999", -1.0E+27F)
                .Add("5.", 5.0F)
                .Add("5.5", 5.5F)
                .Add("5,", 5.0F)
                .Add("5,5", 5.5F)
                .Add("5,5,", 0.0F)
                .Add("5,5,5", 0.0F)
                .Add("5,.5,.5", 0.0F)
                .Add(".5", 0.5)
                .Add(",5", 0.5)
                .Add("Test", 0.0F) '
                .Add("0-4", 0.0F)
            End With

            For Each pair As KeyValuePair(Of String, Single) In source
                'Act
                Dim returnValue As Single = NumUtils.ConvertToSingle(pair.Key)

                'Assert
                'NOTE that floating-point values are NOT exact representations. Therefore, something
                'like Assert.AreEqual(pair.Value, returnValue, "Value: <{0}>", pair.Key) will NOT necessarily be true.
                'Must test with a "close enough" factor - http://msdn.microsoft.com/en-us/library/vstudio/ae382yt8.aspx

                Dim closeEnough As Single = 0.001
                Dim absoluteDifference As Single = Math.Abs(pair.Value - returnValue)
                Dim practicallyEqual As Boolean = (absoluteDifference < closeEnough)
                Assert.IsTrue(practicallyEqual, "Expected: <{0}>, Received: <{1}>, Value: <{2}>", pair.Value, returnValue, pair.Key)
            Next
        End Sub

    End Class
End Namespace
