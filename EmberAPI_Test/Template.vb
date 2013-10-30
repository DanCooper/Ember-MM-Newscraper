Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'See http://msdn.microsoft.com/en-us/library/dd264975.aspx for ideas

<TestClass()>
Public Class Template

    ''' <summary>
    ''' Common setup for all tests in this class
    ''' </summary>
    ''' <remarks></remarks>
    <TestInitialize>
    Public Sub TestSetup()

    End Sub

    ''' <summary>
    ''' Common teardown for all tests in this class
    ''' </summary>
    ''' <remarks></remarks>
    <TestCleanup>
    Public Sub TestCleanup()

    End Sub
    ''' <summary>
    ''' Unit test for single method. Multiple unit tests are expected for each method. 
    ''' Only test one thing at one time, and make sure a failure lets you see what actually failed!
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