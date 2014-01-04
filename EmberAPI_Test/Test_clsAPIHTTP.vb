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

    <TestClass()>
    Public Class Test_clsAPIHTTP
        Implements IDisposable


        Dim HTTP As HTTP = Nothing
        ''' <summary>
        ''' Common setup for all tests in this class. This will run before EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()
            HTTP = New HTTP()
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
        <IntegrationTest>
        <TestMethod()>
        Public Sub HTTP_DownloadData()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub HTTP_PostDownloadData()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub HTTP_DownloadFile()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub HTTP_DownloadImage()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub HTTP_DownloadZip()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub HTTP_PrepareProxy()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub HTTP_IsValidURL()
            'Arrange
            'The sourceValues should contain a String and Boolean - String is a URL, and bool to indicate whether it is indeed valid
            Dim sourceValues As New Dictionary(Of String, Boolean) From
                {
                    {String.Empty, False},
                    {"http://google.com", True},
                    {"google.ca", False},
                    {"http://google", False},
                    {"http://google.ca/", True},
                    {"http://www.google.ca/garbage", False},
                    {"http://i54.tinypic.com/27ybwqt.png", True},
                    {"http://i54.tinypic.com/27ybwqt.png/fdsa", True},
                    {"http://www.youtube.com/watch?v=SDnYMbYB-nU", True}
                }

            For Each pair As KeyValuePair(Of String, Boolean) In sourceValues
                'Act
                Dim result As Boolean = HTTP.IsValidURL(pair.Key)
                'Assert
                Assert.AreEqual(pair.Value, result, "Data tested was: '{0}' and was expecting '{1}', but received '{2}'", pair.Key, pair.Value, result)
            Next
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub HTTP_IsValidURL_Nothing_parameter()
            'Arrange
            'Act
            Dim result As Boolean = HTTP.IsValidURL(Nothing)
            'Assert
            Assert.IsFalse(result, "Data tested was: 'Nothing' and was expecting 'False', but received '{0}'", result)
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' dispose managed state (managed objects).
                    If HTTP IsNot Nothing Then HTTP.Dispose()
                End If

                ' free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' set large fields to null.
                HTTP = Nothing
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
