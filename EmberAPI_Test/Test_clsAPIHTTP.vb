Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace EmberTests

    <TestClass()> Public Class Test_clsAPIHTTP
        Dim HTTP As EmberAPI.HTTP = Nothing
        ''' <summary>
        ''' Common setup for all tests in this class. This will run before EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()
            HTTP = New EmberAPI.HTTP()
        End Sub

        ''' <summary>
        ''' Common teardown for all tests in this class. This will run after EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestCleanup>
        Public Sub TestCleanup()
            If HTTP IsNot Nothing Then
                HTTP.Dispose()
            End If
        End Sub

        <TestMethod()> Public Sub IsValidURL()
            'Arrange
            'The sourceValues should contain a String and Boolean - String is a URL, and bool to indicate whether it is indeed valid
            Dim sourceValues As New Dictionary(Of String, Boolean) From
                {
                    {String.Empty, False},
                    {"http://google.com", True},
                    {"google.ca", False},
                    {"http://google", False},
                    {"http://google.ca/", True},
                    {"http://i54.tinypic.com/27ybwqt.png", True},
                    {"http://i54.tinypic.com/27ybwqt.png/fdsa", True}
                }

            For Each pair As KeyValuePair(Of String, Boolean) In sourceValues
                'Act
                Dim result As Boolean = HTTP.IsValidURL(pair.Key)
                'Assert
                Assert.AreEqual(pair.Value, result, "Data tested was: '{0}' and was expecting '{1}', but received '{2}'", pair.Key, pair.Value, result)
            Next
        End Sub
        <TestMethod()> Public Sub IsValidURL_Nothing_parameter()
            'Arrange
            'Act
            Dim result As Boolean = HTTP.IsValidURL(Nothing)
            'Assert
            Assert.IsFalse(result, "Data tested was: 'Nothing' and was expecting 'False', but received '{0}'", result)
        End Sub

    End Class
End Namespace
