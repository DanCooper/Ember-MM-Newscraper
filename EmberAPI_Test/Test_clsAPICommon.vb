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
Imports System.IO
Imports UnitTests

Namespace EmberTests

    <TestClass()> Public Class Test_clsAPICommon_Functions
        <UnitTest>
        <TestMethod()> Public Sub Functions_CheckIfWindows()
            'Arrange
            Dim expectedResult As Boolean
            expectedResult = Environment.OSVersion.ToString.ToLower.IndexOf("windows") > 0

            'Act
            Dim actualResult = Functions.CheckIfWindows()

            'Assert
            Assert.AreEqual(expectedResult, actualResult)

        End Sub 'CheckIfWindows

        <UnitTest>
        <TestMethod()> Public Sub Functions_ConvertFromUnixTimestamp_Valid()
            'Arrange
            Dim sourceTimestamps As New Dictionary(Of Double, DateTime) From
                {
                    {0.0R, New DateTime(1970, 1, 1, 0, 0, 0)},
                    {-62135596800.0R, New DateTime(1, 1, 1, 0, 0, 0)},
                    {253402300799.0R, New DateTime(9999, 12, 31, 23, 59, 59)},
                    {1383233480.0R, New DateTime(2013, 10, 31, 15, 31, 20)},
                    {-1383233480.0R, New DateTime(1926, 3, 3, 8, 28, 40)}
                }
            '??
            '        {Double.NaN, New DateTime(0)},
            '                                {Double.NegativeInfinity, New DateTime(0)},
            '                   {Double.PositiveInfinity, New DateTime(0)},

            'System.ArgumentOutOfRangeException()
            '{Double.MinValue, New DateTime(0)},
            '{Double.MaxValue, New DateTime(0)},

            For Each pair As KeyValuePair(Of Double, DateTime) In sourceTimestamps
                'Act
                Dim actualResults As DateTime = Functions.ConvertFromUnixTimestamp(pair.Key)
                Dim expectedResults As DateTime = pair.Value
                'Assert
                Assert.AreEqual(expectedResults, actualResults, "Data tested was: '" & pair.Key & "' : '" & pair.Value & "'")
            Next
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_ConvertFromUnixTimestamp_NotValid()
            'Arrange
            Dim sourceTimestamps As New Dictionary(Of Double, Type) From
                {
                    {Double.NaN, GetType(ArgumentException)},
                    {Double.NegativeInfinity, GetType(ArgumentOutOfRangeException)},
                    {Double.PositiveInfinity, GetType(ArgumentOutOfRangeException)},
                    {Double.MinValue, GetType(ArgumentOutOfRangeException)},
                    {Double.MaxValue, GetType(ArgumentOutOfRangeException)},
                    {-62135596801.0R, GetType(ArgumentOutOfRangeException)},
                    {253402300800.0R, GetType(ArgumentOutOfRangeException)}
                }

            For Each pair As KeyValuePair(Of Double, Type) In sourceTimestamps
                Dim exception As Exception = Nothing
                'Act
                Try
                    Dim returnValue As DateTime = Functions.ConvertFromUnixTimestamp(pair.Key)

                Catch ex As Exception
                    exception = ex
                End Try
                'Assert
                Dim expectedResults As Type = pair.Value

                Assert.AreEqual(expectedResults, exception.GetType, "Data tested was: '" & pair.Key & "' : '" & pair.Value.ToString() & "'")
            Next
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_ConvertToUnixTimestamp()
            'Arrange
            'Oct 31, 2013, 10:31:20.0
            Dim valueToConvert As DateTime = New DateTime(2013, 10, 31, 15, 31, 20)
            Dim expectedResult As Double = 1383233480.0
            'Act
            Dim actualResults As Double = Functions.ConvertToUnixTimestamp(valueToConvert)

            'Assert
            Assert.AreEqual(expectedResult, actualResults)
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_DGVDoubleBuffer()
            'Arrange
            Dim sourceDGV As System.Windows.Forms.DataGridView = New System.Windows.Forms.DataGridView()
            Dim conType As Type = sourceDGV.GetType
            Dim pi As System.Reflection.PropertyInfo = conType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
            Dim currentValue As Boolean = pi.GetValue(sourceDGV, Nothing)
            'Act
            Functions.DGVDoubleBuffer(sourceDGV)
            Dim resultValue As Boolean = pi.GetValue(sourceDGV, Nothing)

            'Assert
            Assert.IsTrue(resultValue, "Value before method call was " & currentValue & ", and after the call it was " & resultValue)
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_EmberAPIVersion()
            'Arrange

            'Act
            Dim version As String = Functions.EmberAPIVersion()
            Dim subVersions As String() = version.Split(".")

            Dim numParts As Integer = subVersions.Length()

            'Assert
            Assert.IsTrue(numParts = 4, "Version that was returned had {0} sections, and was expecting 4 sections", numParts)
        End Sub
        <IntegrationTest>
        <TestMethod()> Public Sub Functions_GetExtraModifier()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve system integration with the OS
        End Sub

        <IntegrationTest>
        <TestMethod()> Public Sub Functions_GetSeasonDirectoryFromShowPath()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub
        <UnitTest>
        <TestMethod()> Public Sub Functions_HasModifier()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub
        <UnitTest>
        <TestMethod()> Public Sub Functions_IsSeasonDirectory()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_ListToStringWithSeparator_Happy_Day()
            'Arrange
            Dim sourceList = New List(Of String) From {{"First"}, {"Second"}, {"Third"}, {"Fourth"}, {"Last"}}
            Dim separator = ";"
            'Act
            Dim result As String = Functions.ListToStringWithSeparator(sourceList, separator)
            Dim expected = "First;Second;Third;Fourth;Last"
            'Assert
            Assert.AreEqual(expected, result, False, "Supplied List: <{0}> Separator: <{1}> and received <{2}>", sourceList, separator, result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_ListToStringWithSeparator_Nothing_List()
            'Arrange
            Dim sourceList As List(Of String) = Nothing
            Dim separator = ","
            'Act
            Dim result As String = Functions.ListToStringWithSeparator(sourceList, separator)
            Dim expected = String.Empty
            'Assert
            Assert.AreEqual(expected, result, False, "Supplied List: <{0}> Separator: <{1}> and received <{2}>", sourceList, separator, result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_ListToStringWithSeparator_Nothing_value_in_list()
            'Arrange
            Dim sourceList = New List(Of String) From {{"First"}, {Nothing}, {"Third"}, {"Fourth"}, {"Last"}}
            Dim separator = ","
            'Act
            Dim result As String = Functions.ListToStringWithSeparator(sourceList, separator)
            Dim expected = "First,Third,Fourth,Last"
            'Assert
            Assert.AreEqual(expected, result, False, "Supplied List: <{0}> Separator: <{1}> and received <{2}>", sourceList, separator, result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_ListToStringWithSeparator_Nothing_Separator()
            'Arrange
            Dim sourceList = New List(Of String) From {{"First"}, {"Second"}, {"Third"}, {"Fourth"}, {"Last"}}
            Dim separator As String = Nothing
            'Act
            Dim result As String = Functions.ListToStringWithSeparator(sourceList, separator)
            Dim expected = "First,Second,Third,Fourth,Last"
            'Assert
            Assert.AreEqual(expected, result, False, "Supplied List: <{0}> Separator: <{1}> and received <{2}>", sourceList, separator, result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_ListToStringWithSeparator_Extra_Blanks_In_List()
            'Arrange
            Dim sourceList = New List(Of String) From {{"First"}, {"Second "}, {"Third"}, {" Fourth"}, {"Last"}}
            Dim separator = ","
            'Act
            Dim result As String = Functions.ListToStringWithSeparator(sourceList, separator)
            Dim expected = "First,Second ,Third, Fourth,Last"
            'Assert
            Assert.AreEqual(expected, result, False, "Supplied List: <{0}> Separator: <{1}> and received <{2}>", sourceList, separator, result)
        End Sub
        <UnitTest>
        <TestMethod()> Public Sub Functions_PNLDoubleBuffer()
            'Arrange
            Dim sourcePnl As System.Windows.Forms.Panel = New System.Windows.Forms.Panel()
            Dim conType As Type = sourcePnl.GetType
            Dim pi As System.Reflection.PropertyInfo = conType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
            Dim currentValue As Boolean = pi.GetValue(sourcePnl, Nothing)
            'Act
            Functions.PNLDoubleBuffer(sourcePnl)
            Dim resultValue As Boolean = pi.GetValue(sourcePnl, Nothing)

            'Assert
            Assert.IsTrue(resultValue, "Value before method call was " & currentValue & ", and after the call it was " & resultValue)
        End Sub
        ''' <summary>
        ''' Internal structure for Quantize test
        ''' </summary>
        ''' <remarks></remarks>
        Private Structure IntegerTriplet
            Dim first As Integer
            Dim second As Integer
            Dim third As Integer

            Sub New(p1 As Integer, p2 As Integer, p3 As Integer)
                first = p1
                second = p2
                third = p3
            End Sub

        End Structure
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Quantize_HappyDay()
            'Arrange
            'Triplets are to be interpreted as Number, Multiple, ExpectedResult
            Dim sourceValues As New List(Of IntegerTriplet) From
                {
                    New IntegerTriplet(0, 1, 0),
                    New IntegerTriplet(10, 1, 10),
                    New IntegerTriplet(9, 3, 9),
                    New IntegerTriplet(10, 3, 9),
                    New IntegerTriplet(11, 3, 12),
                    New IntegerTriplet(12, 3, 12)
                }

            For Each triplet As IntegerTriplet In sourceValues
                Dim exception As Exception = Nothing
                'Act
                Dim result As Integer = Functions.Quantize(triplet.first, triplet.second)
                Assert.AreEqual(result, triplet.third, "Attempted to Quantize({0},{1}) and received {2}", triplet.first, triplet.second, result)
                'Assert
                Dim expectedResults As Integer = triplet.third
            Next
        End Sub
        <UnitTest>
        <TestMethod()>
        <ExpectedException(GetType(ArgumentOutOfRangeException))>
        Public Sub Functions_Quantize_NullMultiple()
            'Arrange
            'Act
            Dim result As Integer = Functions.Quantize(30, Nothing)
            'Assert
        End Sub
        <UnitTest>
        <TestMethod()>
        <ExpectedException(GetType(ArgumentOutOfRangeException))>
        Public Sub Functions_Quantize_NegativeMultiple()
            'Arrange
            'Act
            Dim result As Integer = Functions.Quantize(-30, Nothing)
            'Assert
        End Sub
        <UnitTest>
        <TestMethod()> Public Sub Functions_ReadStreamToEnd()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub
        <UnitTest>
        <TestMethod()> Public Sub Functions_ScrapeModifierAndAlso()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_ScrapeOptionsAndAlso()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_TVScrapeOptionsAndAlso()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub

        <UnitTest>
        <TestMethod()> Public Sub Functions_SetScraperMod()
            'The difficulty with this method is that the real test would
            'be to check if all the fields have been cleared/set. Problem is,
            'if a new field is added, existing tests would not detect it!
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub

        <IntegrationTest>
        <TestMethod()> Public Sub Functions_TestMediaInfoDLL()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
            'This test would involve some setup...
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_NothingDestination_Disallow()
            'Arrange
            Dim destination As String = Nothing

            'Act
            Dim result As Boolean = Functions.Launch(destination)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_NothingDestination_DisallowExplicit()
            'Arrange
            Dim destination As String = Nothing

            'Act
            Dim result As Boolean = Functions.Launch(destination, False)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_NothingDestination_AllowExplicit()
            'Arrange
            Dim destination As String = Nothing

            'Act
            Dim result As Boolean = Functions.Launch(destination, True)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_EmptyDestination_Disallow()
            'Arrange
            Dim destination As String = String.Empty

            'Act
            Dim result As Boolean = Functions.Launch(destination)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_EmptyDestination_DisallowExplicit()
            'Arrange
            Dim destination As String = String.Empty

            'Act
            Dim result As Boolean = Functions.Launch(destination, False)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_EmptyDestination_AllowExplicit()
            'Arrange
            Dim destination As String = String.Empty

            'Act
            Dim result As Boolean = Functions.Launch(destination, True)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_LocalFile_Disallow()
            'Arrange

            Dim destination As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images")
            destination = Path.Combine(destination, "OfflineOverlay.png")

            'Act
            Dim result As Boolean = Functions.Launch(destination)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_LocalFile_DisallowExplicit()
            'Arrange

            Dim destination As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images")
            destination = Path.Combine(destination, "OfflineOverlay.png")

            'Act
            Dim result As Boolean = Functions.Launch(destination, False)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub Functions_Launch_LocalFile_AllowExplicit()
            'Arrange

            Dim destination As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images")
            destination = Path.Combine(destination, "OfflineOverlay.png")

            'Act
            Dim result As Boolean = Functions.Launch(destination, True)

            'Assert
            Assert.IsTrue(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_URL_Disallow()
            'Arrange

            Dim destination As String = "http://embermediamanager.org/"

            'Act
            Dim result As Boolean = Functions.Launch(destination)

            'Assert
            Assert.IsTrue(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_URL_DisallowExplicit()
            'Arrange

            Dim destination As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images")
            destination = Path.Combine(destination, "OfflineOverlay.png")

            'Act
            Dim result As Boolean = Functions.Launch(destination, False)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <InteractiveTest>
        <TestMethod()>
        Public Sub Functions_Launch_URL_AllowExplicit()
            'Arrange

            Dim destination As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images")
            destination = Path.Combine(destination, "OfflineOverlay.png")

            'Act
            Dim result As Boolean = Functions.Launch(destination, True)

            'Assert
            Assert.IsTrue(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_URL_Malformed1()
            'Arrange

            Dim destination As String = "http://embermed iamanager.org/"

            'Act
            Dim result As Boolean = Functions.Launch(destination)

            'Assert
            Assert.IsFalse(result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_URL_Malformed2()
            'Arrange

            Dim destination As String = "embermediamanager.org"

            'Act
            Dim result As Boolean = Functions.Launch(destination)

            'Assert
            Assert.IsFalse(result)
        End Sub
        ''' <summary>
        ''' This method is to replicate an error in the original frmMain.btnPlay_Click, where it had a line as follows:
        ''' Process.Start(String.Concat("""", Me.txtFilePath.Text, """"))
        ''' But with the new Launch implementation, the extra quotes are not required any more
        ''' </summary>
        ''' <remarks></remarks>
        <UnitTest>
        <TestMethod()>
        Public Sub Functions_Launch_ExtraQuotes_FileAllowed()
            'Arrange
            Dim tempDestination As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images")
            tempDestination = Path.Combine(tempDestination, "OfflineOverlay.png")

            'This should produce an error because it will not like the explicit quotes around the string
            Dim destination As String = String.Format("""{0}""", tempDestination)

            'Act
            Dim result As Boolean = Functions.Launch(destination, True)

            'Assert
            Assert.IsFalse(result)
        End Sub

    End Class

End Namespace
