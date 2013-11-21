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

Namespace EmberTests


    <TestClass()> Public Class Test_clsTrailers
        Private trailers As Trailers

        ''' <summary>
        ''' Common setup for all tests in this class. This will run before EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()
            trailers = New Trailers()
        End Sub

        ''' <summary>
        ''' Common teardown for all tests in this class. This will run after EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestCleanup>
        Public Sub TestCleanup()
            If trailers IsNot Nothing Then
                trailers.Cancel()
                trailers = Nothing
            End If
        End Sub

        <UnitTest>
        <TestMethod()>
        Public Sub Trailers_Cancel()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

        <IntegrationTest>
        <TestMethod()>
        Public Sub Trailers_DeleteTrailers()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

        <IntegrationTest>
        <TestMethod()>
        Public Sub Trailers_DownloadProgressUpdated()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

        <IntegrationTest>
        <TestMethod()>
        Public Sub Trailers_PreferredTrailer()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

        <IntegrationTest>
        <TestMethod()>
        Public Sub Trailers_DownloadTrailer()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

        <IntegrationTest>
        <TestMethod()>
        Public Sub Trailers_IsAllowedToDownload()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

    End Class
End Namespace
