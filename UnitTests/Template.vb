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

'See http://msdn.microsoft.com/en-us/library/dd264975.aspx for ideas

Namespace EmberTests

    <TestClass()>
    Public Class Template

        ''' <summary>
        ''' Common setup for all tests in this class. This will run before EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()

        End Sub

        ''' <summary>
        ''' Common teardown for all tests in this class. This will run after EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestCleanup>
        Public Sub TestCleanup()

        End Sub
        ''' <summary>
        ''' Unit test for single method. Multiple unit tests are expected for each method. 
        ''' Only test one thing at one time, and make sure a failure lets you see what actually failed!
        ''' There is no such thing as too many unit tests!!! 
        ''' Test with Nothing, test 0, test Integer.MinValue, Integer.MaxValue, the maximum acceptable value, and one too many, etc.
        ''' Use your imagination, but be thorough. Name your tests using the following template {method name}_{condition under test}
        ''' Don't worry about long names. They are infinitely better than cryptic or short names.
        ''' 
        ''' Document with &lt;UnitTest&gt; or &lt;IntegrationTest&gt; before the &lt;TestMethod&gt;
        ''' Unit tests run quickly in isolation. Integration tests rely on the file system, database, or other external system.
        ''' </summary>
        ''' <remarks></remarks>
        <TestMethod()>
        Public Sub Template()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

    End Class

End Namespace
