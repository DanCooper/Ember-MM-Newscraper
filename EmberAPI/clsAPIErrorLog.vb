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

Imports System.IO
Imports NLog

Public Class ErrorLogger


#Region "Events"

    Public Event ErrorOccurred()

#End Region 'Events

#Region "Methods"

    ''' <summary>
    ''' Write the error to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks></remarks>
    <Obsolete("WriteToErrorLog has been deprecated. Please use Trace/Debug/Info/Warn/Error/Fatal instead")>
    Public Sub WriteToErrorLog(ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        'Delegate to the new logging routines
        [Error](GetType(EmberAPI.Master), msg, stkTrace, title, Notify)


        'Master.logger.Debug("{0} {1}", msg, stkTrace)
        'Dim masterLogger = loggers(GetType(EmberAPI.Master))
        'Dim masterLogger = LogManager.GetLogger(GetType(EmberAPI.Master).FullName()) 'Using GetType (instead of a literal String) to catch design-type changes

        'masterLogger.Debug("{0} {1}", msg, stkTrace)

        'If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        'RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Write a message at the TRACE level to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="type"><c>Type</c> of the calling class. Recommend Me.GetType() or GetType(classname).</param>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks>Often used for fine-grained informational events, seldom of any use beyond the developer</remarks>
    Public Sub Trace(ByVal type As Type, ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        LogManager.GetLogger(type.FullName).Trace("{0} {1}", msg, stkTrace)
        'Don't raise user-level notifications for trace-level messages
        'If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        'RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Write a message at the DEBUG level to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="type"><c>Type</c> of the calling class. Recommend Me.GetType() or GetType(classname).</param>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks>Often used for fine-grained informational events, often of use to developers. 
    ''' Usually contains diagnostic related information</remarks>
    Public Sub Debug(ByVal type As Type, ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        LogManager.GetLogger(type.FullName).Debug("{0} {1}", msg, stkTrace)
        'Don't raise user-level notifications for trace-level messages
        'If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        'RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Write a message at the INFO level to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="type"><c>Type</c> of the calling class. Recommend Me.GetType() or GetType(classname).</param>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks>Often used to highlight the progress of the application at a coarse-grained level.
    ''' Things such as startup/shutdown may be logged at this level</remarks>
    Public Sub Info(ByVal type As Type, ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        LogManager.GetLogger(type.FullName).Info("{0} {1}", msg, stkTrace)
        'Don't raise user-level notifications for trace-level messages
        'If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        'RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Write a message at the WARN level to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="type"><c>Type</c> of the calling class. Recommend Me.GetType() or GetType(classname).</param>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks>Often used to record potentially harmful situations, but which can be recovered from.
    ''' Things such as switching to alternate methods, retrying an operation, missing secondary data, etc.</remarks>
    Public Sub Warn(ByVal type As Type, ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        LogManager.GetLogger(type.FullName).Warn("{0} {1}", msg, stkTrace)
        If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Write a message at the ERROR level to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="type"><c>Type</c> of the calling class. Recommend Me.GetType() or GetType(classname).</param>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks>Often used to record any error which is fatal to the *operation* but not the service or application.
    ''' These errors will usually force the user to intervene in some manner.</remarks>
    Public Sub [Error](ByVal type As Type, ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        LogManager.GetLogger(type.FullName).Error("{0} {1}", msg, stkTrace)
        If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Write a message at the FATAL level to our log file, if enabled in settings.
    ''' </summary>
    ''' <param name="type"><c>Type</c> of the calling class. Recommend Me.GetType() or GetType(classname).</param>
    ''' <param name="msg">Error summary</param>
    ''' <param name="stkTrace">Full stack trace</param>
    ''' <param name="title">Error title</param>
    ''' <param name="Notify"></param>
    ''' <remarks>Often used to record any error that is forcing a shutdown of the application. </remarks>
    Public Sub Fatal(ByVal type As Type, ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        LogManager.GetLogger(type.FullName).Fatal("{0} {1}", msg, stkTrace)
        If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        RaiseEvent ErrorOccurred()
    End Sub

    ''' <summary>
    ''' Get the primary log filename with path
    ''' </summary>
    ''' <returns>Primary log filename including path, or <c>String.Empty</c> if none is currently defined</returns>
    ''' <remarks></remarks>
    Public Shared Function GetPrimaryLog() As String
        Try

            Dim target As Targets.Target = LogManager.Configuration.ConfiguredNamedTargets(0)
            Dim asyncWrapper As Targets.Wrappers.AsyncTargetWrapper = CType(target, Targets.Wrappers.AsyncTargetWrapper)
            Dim retryingWrapper As Targets.Wrappers.RetryingTargetWrapper = CType(asyncWrapper.WrappedTarget, Targets.Wrappers.RetryingTargetWrapper)
            Dim fileTarget As Targets.FileTarget = CType(retryingWrapper.WrappedTarget, Targets.FileTarget)
            Dim fileName As String = fileTarget.FileName.Render(New LogEventInfo(NLog.LogLevel.Debug, String.Empty, String.Empty))
            Return fileName
        Catch ex As Exception
            Master.eLog.Error(GetType(ErrorLogger), "Could not determine log file name." & vbNewLine & ex.Message, ex.StackTrace, "Error")
        End Try
        Return String.Empty
    End Function
#End Region 'Methods

End Class

