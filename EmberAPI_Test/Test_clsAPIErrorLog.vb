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
    ''' <summary>
    ''' This test class is a bit of an oddity.
    ''' It uses the EMM logging facility, but it creates its own custom target.
    ''' No messages are written to the EMM configured targets, because that configuration
    ''' file is not read. Instead, they are written to the "debugTarget" 
    ''' defined in the class initializer below. 
    ''' </summary>
    ''' <remarks></remarks>
    <TestClass()>
    Public Class Test_clsAPIErrorLog

        Dim logger As ErrorLogger
        Dim exception As Exception
        Shared debugTarget As NLog.Targets.DebugTarget
        Dim defaultExpectedMessageParts As Integer = 5
        Shared defaultMessageSeparator As Char = "|"c

        <ClassInitialize>
        Public Shared Sub ClassInit(ByVal context As TestContext)
            debugTarget = New NLog.Targets.DebugTarget()
            debugTarget.Name = "UnitTest"
            '            debugTarget.Layout = "${longdate}|${callsite}|${threadid}|${uppercase:${level}}|${message}"
            debugTarget.Layout = String.Join(defaultMessageSeparator, _
                                             {"${longdate}", _
                                              "${callsite}", _
                                              "${threadid}", _
                                              "${uppercase:${level}}", _
                                              "${message}"})
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
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingMessage()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = Nothing
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingTitle()
            ' Note that it appears the Title does not impact the written log.

            'Arrange
            Dim title As String = Nothing
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingStackTrace()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Nothing
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingMessageAndStackTrace()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = Nothing
            Dim stackTrace As String = Nothing
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingMessageAndTitle()
            'Note that it appears that Title does not affect file output

            'Arrange
            Dim title As String = Nothing
            Dim message As String = Nothing
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)

            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_NothingStackTraceAndTitle()
            'Note that it appears that title has no effect on the log file output
            'Arrange
            Dim title As String = Nothing
            Dim message As String = "My Message"
            Dim stackTrace As String = Nothing
            'Act
            logger.WriteToErrorLog(message, stackTrace, title)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())

        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_WriteToErrorLog_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.WriteToErrorLog(message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_Trace_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.Trace(Me.GetType(), message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Trace.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_Debug_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.Debug(Me.GetType(), message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Debug.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_Info_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.Info(Me.GetType(), message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Info.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_Warn_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.Warn(Me.GetType(), message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Warn.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_Error_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.Error(Me.GetType(), message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Error.ToString())
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub ErrorLogger_Fatal_HappyDayNoNotify()
            'Arrange
            Dim title As String = "My Title"
            Dim message As String = "My Message"
            Dim stackTrace As String = Me.exception.StackTrace
            'Act
            logger.Fatal(Me.GetType(), message, stackTrace, title, False)
            'Assert
            CheckLastMessage(message, stackTrace, title, NLog.LogLevel.Fatal.ToString())
        End Sub

        Public Sub CheckLastMessage(ByVal message As String, ByVal stackTrace As String, ByVal title As String, ByVal expectedLevel As String)
            If String.IsNullOrEmpty(debugTarget.LastMessage) Then
                Assert.Fail("Message did not get written to the log")
            End If

            Dim output As String() = debugTarget.LastMessage.Split(defaultMessageSeparator)
            If output.Length < defaultExpectedMessageParts Then
                Assert.Fail("Message had incorrect number of parts. Expected {0}, got {1}", defaultExpectedMessageParts, output.Length)
            End If
            Dim outputCallSite = output(1)
            Dim outputThreadID = output(2)
            Dim outputLevel = output(3)
            Dim outputMessage = output(4)

            Dim expectedCallSite = "EmberAPI.ErrorLogger." & expectedLevel
            Dim expectedMessage = String.Format("{0} {1}", message, stackTrace) 'This should match clsAPIErrorLog's WriteErrorLog routine

            Assert.AreEqual(outputCallSite, expectedCallSite, True, "CallSite - expecting: {0}, got: {1}", expectedCallSite, outputCallSite)
            Assert.AreEqual(outputLevel.ToUpper(), expectedLevel.ToUpper(), True, "OutputLevel - expecting: {0}, got: {1}", expectedLevel, outputLevel)
            Assert.AreEqual(outputMessage, expectedMessage, True, "Message - expecting: {0}, got: {1}", expectedMessage, outputMessage)
        End Sub
    End Class
End Namespace
