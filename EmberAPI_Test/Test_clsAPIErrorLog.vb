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

Imports NLog
Imports EmberAPI

Namespace EmberTests

    <TestClass()>
    Public Class Test_clsAPIErrorLog

        Dim logger As ErrorLogger
        Dim exception As Exception
        Shared debugTarget As NLog.Targets.DebugTarget

        <ClassInitialize>
        Public Shared Sub ClassInit(ByVal context As TestContext)
            debugTarget = New NLog.Targets.DebugTarget()
            debugTarget.Name = "UnitTest"
            debugTarget.Layout = "${longdate}|${callsite}|${threadid}|${uppercase:${level}}|${message}"
            NLog.LogManager.Configuration.AddTarget("UnitTest", debugTarget)
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(debugTarget, LogLevel.Trace)
            NLog.LogManager.ReconfigExistingLoggers()
        End Sub

        <ClassCleanup>
        Public Shared Sub ClassCleanup()
            NLog.LogManager.Configuration.RemoveTarget(debugTarget.Name())
            debugTarget.Dispose()
            debugTarget = Nothing
        End Sub

        ''' <summary>
        ''' Common setup for all tests in this class. This will run before EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()
            Me.logger = New ErrorLogger()

            Try
                Throw New OverflowException("Log Test")
            Catch ex As Exception
                Me.exception = ex
            End Try
        End Sub

        ''' <summary>
        ''' Common teardown for all tests in this class. This will run after EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <TestCleanup>
        Public Sub TestCleanup()
            Me.logger = Nothing
            Me.exception = Nothing
        End Sub


        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_HappyDay()
            'Arrange
            Dim title As String = "My Title"
            Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(myMessage, Me.exception.StackTrace, title)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", myMessage, exception.StackTrace) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingMessage()
            'Arrange
            Dim title As String = "My Title"
            'Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(Nothing, Me.exception.StackTrace, title)
            ' Sample result: "2013-11-08 14:13:54.5405|EmberAPI.ErrorLogger.WriteToErrorLog|7|DEBUG|Message    at EmberAPI_Test.EmberTests.Test_clsAPIErrorLog.TestSetup() in C:\Users\Michael\Documents\Visual Studio 2012\Projects\Ember-MM-Newscraper\EmberAPI_Test\Test_clsAPIErrorLog.vb:line 62"

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            ' "${longdate}|${callsite}|${threadid}|${uppercase:${level}}|${message}"
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", Nothing, exception.StackTrace) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingTitle()
            ' Note that it appears the Title does not impact the written log.

            'Arrange
            'Dim title As String = "My Title"
            Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(myMessage, Me.exception.StackTrace, Nothing)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", myMessage, exception.StackTrace) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingStackTrace()
            'Arrange
            Dim title As String = "My Title"
            Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(myMessage, Nothing, title)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", myMessage, Nothing) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingMessageAndStackTrace()
            'Arrange
            Dim title As String = "My Title"
            'Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(Nothing, Nothing, title)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", Nothing, Nothing) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingMessageAndTitle()
            'Note that it appears that Title does not affect file output

            'Arrange
            'Dim title As String = "My Title"
            'Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(Nothing, Me.exception.StackTrace, Nothing)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", Nothing, exception.StackTrace) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingStackTraceAndTitle()
            'Note that it appears that title has no effect on the log file output

            'Arrange
            'Dim title As String = "My Title"
            Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(myMessage, Nothing, Nothing)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", myMessage, Nothing) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim myMessage As String = "My Message"
            'Act
            logger.WriteToErrorLog(myMessage, Me.exception.StackTrace, title, False)

            'Assert
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split("|")
            If output.Length < 5 Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", 4, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger.WriteToErrorLog"
            Dim expectedLevel = "DEBUG"
            Dim expectedMessage = String.Format("{0} {1}", myMessage, exception.StackTrace) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel, expectedLevel, True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)

        End Sub

    End Class
End Namespace
