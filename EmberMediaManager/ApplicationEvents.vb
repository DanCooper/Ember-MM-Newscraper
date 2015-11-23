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

Imports EmberAPI
Imports NLog
Imports System.IO

Namespace My

    Partial Friend Class MyApplication

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        Private frmEmber As frmMain

#End Region 'Fields

#Region "Methods"

        ''' <summary>
        ''' Process/load information before beginning the main application.
        ''' </summary>
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            'Try
            logger.Info("====Ember Media Manager starting up====")
            logger.Info(String.Format("====Version {0}.{1}.{2}.{3}====", Application.Info.Version.Major, Application.Info.Version.Minor, Application.Info.Version.Build, Application.Info.Version.Revision))
            Master.fLoading = New EmberAPI.frmSplash
            Master.is32Bit = (IntPtr.Size = 4)
            Master.appArgs = e

            ' #############################################
            ' ###  Inserted by: redglory on 14.07.2013  ###
            ' #############################################
            ' #  Check If Ember Media Manager is called   #
            ' #  from a service process or from a Web     # 
            ' #  application                              #
            ' #                                           #
            ' #  UserInteractive property (True/False)    #
            ' #############################################
            Master.isUserInteractive = Environment.UserInteractive
            If Master.isUserInteractive Then
                '# Show UI
                Master.fLoading.Show()
            End If
            Application.DoEvents()

            Functions.TestMediaInfoDLL()

            If e.CommandLine.Count > 0 Then
                Master.isCL = True
                Master.fLoading.SetProgressBarSize(10)
            End If
            ' Run InstallTask to see if any pending file needs to install
            ' Do this before loading modules/themes/etc
            Dim configpath As String = Path.Combine(Functions.AppPath, "InstallTasks.xml")
            If File.Exists(configpath) Then
                FileUtils.Common.InstallNewFiles(configpath)
            End If

            'cocotus Check if new "Settings" folder exists - if not then create it!
            If Not Directory.Exists(String.Concat(Functions.AppPath, "Settings")) Then
                Directory.CreateDirectory(String.Concat(Functions.AppPath, "Settings"))
            End If
            'cocotus end

            Master.eSettings.Load()

            ' Force initialization of languages for main
            Master.eLang.LoadAllLanguage(Master.eSettings.GeneralLanguage)

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(484, "Loading settings..."))

            Dim aBit As String = "x64"
            If Master.is32Bit Then
                aBit = "x86"
            End If
            Master.fLoading.SetVersionMesg(Master.eLang.GetString(865, "Version {0}.{1}.{2}.{3} {4}"), aBit)

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(862, "Loading translations..."))
            APIXML.CacheXMLs()

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1164, "Loading Main Form. Please wait..."))
            frmEmber = New frmMain

            'Catch ex As Exception
            '    logger.Error(New StackFrame().GetMethod().Name, ex)
            'End Try
        End Sub

        ''' <summary>
        ''' Check if Ember is already running, but only for GUI instances
        ''' </summary>
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            If e.CommandLine.Count = 0 Then
                e.BringToForeground = True
            ElseIf e.CommandLine.Count > 0 Then
                Dim Args() As String = e.CommandLine.ToArray
                frmMain.fCommandLine.RunCommandLine(Args)
            End If
        End Sub

        ''' <summary>
        ''' Basic wrapper for unhandled exceptions
        ''' </summary>
        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            logger.Error(e.Exception.Source, e.Exception)
            MessageBox.Show(e.Exception.Message, "Ember Media Manager", MessageBoxButtons.OK, MessageBoxIcon.Error)
            My.Application.Log.WriteException(e.Exception, TraceEventType.Critical, "Unhandled Exception.")
        End Sub

#End Region 'Methods

    End Class

End Namespace

