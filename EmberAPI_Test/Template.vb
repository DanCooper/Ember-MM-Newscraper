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
