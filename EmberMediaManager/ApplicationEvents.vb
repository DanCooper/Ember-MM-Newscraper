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

        Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private frmEmber As frmMain

#End Region 'Fields

#Region "Methods"

        ''' <summary>
        ''' Process/load information before beginning the main application.
        ''' </summary>
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            logger.Info("====Ember Media Manager starting up====")
            logger.Info(String.Format("===={0}", Master.Version))

            Net.ServicePointManager.SecurityProtocol = Net.ServicePointManager.SecurityProtocol Or Net.SecurityProtocolType.Tls11 Or Net.SecurityProtocolType.Tls12

            Master.fLoading = New frmSplash
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
            If Master.isUserInteractive AndAlso Not Master.appArgs.CommandLine.Contains("-nowindow") Then
                '# Show UI
                Master.fLoading.Show()
            End If

            Master.fLoading.SetVersionMesg(Master.Version)

            Application.DoEvents()

            Functions.TestMediaInfoDLL()

            If Master.appArgs.CommandLine.Count > 0 Then
                Master.isCL = True
                Master.fLoading.SetProgressBarSize(10)
            End If
            ' Run InstallTask to see if any pending file needs to install
            ' Do this before loading modules/themes/etc
            Dim configpath As String = Path.Combine(Functions.AppPath, "InstallTasks.xml")
            If File.Exists(configpath) Then
                FileUtils.Common.InstallNewFiles(configpath)
            End If

            Master.fLoading.SetLoadingMesg("Select Profile")

            Master.eProfiles.LoadSettings()
            If Not Directory.Exists(Path.Combine(Functions.AppPath, "Profiles")) Then
                Directory.CreateDirectory(Path.Combine(Functions.AppPath, "Profiles"))
            End If

            If Not Directory.Exists(Path.Combine(Functions.AppPath, String.Concat("Profiles\", "Default"))) Then
                Directory.CreateDirectory(Path.Combine(Functions.AppPath, String.Concat("Profiles\", "Default")))
            End If

            If Master.isCL Then
                If Master.appArgs.CommandLine.Contains("-profile") Then
                    Dim bProfileHasLoaded As Boolean = False
                    For i As Integer = 0 To Master.appArgs.CommandLine.Count - 1
                        Select Case Master.appArgs.CommandLine(i).ToLower
                            Case "-profile"
                                If Not bProfileHasLoaded Then
                                    If Master.appArgs.CommandLine.Count - 1 > i AndAlso Not Master.appArgs.CommandLine(i + 1).StartsWith("-") Then
                                        If Directory.Exists(Path.Combine(Functions.AppPath, String.Concat("Profiles\", Master.appArgs.CommandLine(i + 1).Replace("""", String.Empty)))) Then
                                            Master.SettingsPath = Path.Combine(Functions.AppPath, String.Concat("Profiles\", Master.appArgs.CommandLine(i + 1).Replace("""", String.Empty)))
                                            bProfileHasLoaded = True
                                            i += 1
                                            If Master.appArgs.CommandLine.Count = 2 Then
                                                'switch back to GUI mode
                                                Master.isCL = False
                                            End If
                                        Else
                                            logger.Warn(String.Format("[CommandLine] [Abort] Profile ""{0}"" not found. Abort to prevent library corruption.", Master.appArgs.CommandLine(i + 1).Replace("""", String.Empty)))
                                            logger.Info("====Ember Media Manager exiting====")
                                            Environment.Exit(0)
                                        End If
                                    Else
                                        logger.Warn("[CommandLine] [Abort] Missing profile name for command ""-profile"". Abort to prevent library corruption.")
                                        logger.Info("====Ember Media Manager exiting====")
                                        Environment.Exit(0)
                                    End If
                                Else
                                    logger.Warn(String.Format("[CommandLine] [Abort] More than one profile has been specified. Abort to prevent library corruption.", Master.appArgs.CommandLine(i + 1).Replace("""", String.Empty)))
                                    logger.Info("====Ember Media Manager exiting====")
                                    Environment.Exit(0)
                                End If
                        End Select
                    Next
                Else
                    logger.Info("[CommandLine] Using profile ""Default"".")
                    Master.SettingsPath = Path.Combine(Functions.AppPath, "Profiles\Default")
                End If
            ElseIf Master.eProfiles.DefaultProfileSpecified AndAlso
                Directory.Exists(Master.eProfiles.DefaultProfileFullPath) AndAlso
                Master.eProfiles.Autoload Then
                Master.SettingsPath = Master.eProfiles.DefaultProfileFullPath
            Else
                'show Profile Select dialog
                Using dProfileSelect As New dlgProfileSelect
                    If dProfileSelect.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrEmpty(dProfileSelect.SelectedProfileFullPath) Then
                        Master.SettingsPath = dProfileSelect.SelectedProfileFullPath
                    Else
                        logger.Info("====Ember Media Manager exiting====")
                        Environment.Exit(0)
                    End If
                End Using
            End If

            Master.fLoading.SetLoadingMesg("Loading settings...")
            Master.eSettings.Load()
            Master.eAdvancedSettings.Load()
            Manager.mSettings.Load()

            Master.fLoading.SetLoadingMesg("Caching XMLs...")
            APIXML.CacheAll()

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1164, "Loading Main Form. Please wait..."))
            frmEmber = New frmMain
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
            logger.Error(e.Exception, e.Exception.Source)
            MessageBox.Show(e.Exception.Message, "Ember Media Manager", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Log.WriteException(e.Exception, TraceEventType.Critical, "Unhandled Exception.")
        End Sub

#End Region 'Methods

    End Class

End Namespace