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

Public Class Master

#Region "Fields"

    Public Shared fLoading As New frmSplash

    Public Shared isUserInteractive As Boolean = True
    Public Shared isCL As Boolean
    Public Shared appArgs As ApplicationServices.StartupEventArgs

    Public Shared AppPos As New Drawing.Rectangle
    Public Shared CanScanDiscImage As Boolean
    Public Shared DB As New Database
    Public Shared DefaultOptions_Movie As New Structures.ScrapeOptions
    Public Shared DefaultOptions_MovieSet As New Structures.ScrapeOptions
    Public Shared DefaultOptions_TV As New Structures.ScrapeOptions
    Public Shared ExcludeDirs As New List(Of String)
    Public Shared MovieSources As New List(Of Database.DBSource)
    Public Shared SettingsPath As String = Path.Combine(Functions.AppPath, "Profiles\Default")
    Public Shared SourcesList As New List(Of String)
    Public Shared TVShowSources As New List(Of Database.DBSource)
    Public Shared TempPath As String = Path.Combine(Functions.AppPath, "Temp")
    Public Shared eLang As New Localization
    Public Shared eSettings As New Settings
    Public Shared is32Bit As Boolean
    Public Shared isWindows As Boolean = Functions.CheckIfWindows

    Public Shared strVersionOverwrite As String = "1.4.8.0 Alpha 23.3"

#End Region 'Fields

End Class