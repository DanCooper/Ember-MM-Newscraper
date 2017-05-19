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
Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports System.Drawing
Imports NLog

Public Class ModulesManager

#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared AssemblyList As New List(Of AssemblyListItem)
    Public Shared VersionList As New List(Of VersionItem)

    Public externalGenericModules As New List(Of _externalGenericModuleClass)
    Public externalScrapersModules_Data_Movie As New List(Of _externalScraperModuleClass_Data_Movie)
    Public externalScrapersModules_Data_MovieSet As New List(Of _externalScraperModuleClass_Data_MovieSet)
    Public externalScrapersModules_Data_TV As New List(Of _externalScraperModuleClass_Data_TV)
    Public externalScrapersModules_Image_Movie As New List(Of _externalScraperModuleClass_Image_Movie)
    Public externalScrapersModules_Image_MovieSet As New List(Of _externalScraperModuleClass_Image_MovieSet)
    Public externalScrapersModules_Image_TV As New List(Of _externalScraperModuleClass_Image_TV)
    Public externalScrapersModules_Theme_Movie As New List(Of _externalScraperModuleClass_Theme_Movie)
    Public externalScrapersModules_Theme_TV As New List(Of _externalScraperModuleClass_Theme_TV)
    Public externalScrapersModules_Trailer_Movie As New List(Of _externalScraperModuleClass_Trailer_Movie)
    Public RuntimeObjects As New EmberRuntimeObjects

    'Singleton Instace for module manager .. allways use this one
    Private Shared Singleton As ModulesManager = Nothing

    Private moduleLocation As String = Path.Combine(Functions.AppPath, "Modules")

    Friend WithEvents bwLoadModules As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
    Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Event ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)

#End Region 'Events

#Region "Properties"

    Public Shared ReadOnly Property Instance() As ModulesManager
        Get
            If (Singleton Is Nothing) Then
                Singleton = New ModulesManager()
            End If
            Return Singleton
        End Get
    End Property

    Public ReadOnly Property ModulesLoaded() As Boolean
        Get
            Return Not bwLoadModules.IsBusy
        End Get
    End Property
#End Region 'Properties

#Region "Methods"

    Private Sub BuildVersionList()
        VersionList.Clear()
        VersionList.Add(New VersionItem With {.AssemblyFileName = "*EmberAPP", .Name = "Ember Application", .Version = My.Application.Info.Version.ToString()})
        VersionList.Add(New VersionItem With {.AssemblyFileName = "*EmberAPI", .Name = "Ember API", .Version = Functions.EmberAPIVersion()})
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_TV In externalScrapersModules_Data_TV
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_TV In externalScrapersModules_Image_TV
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalTVThemeScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV
            VersionList.Add(New VersionItem With {.Name = _externalTVThemeScraperModule.ProcessorModule.ModuleName,
                    .AssemblyFileName = _externalTVThemeScraperModule.AssemblyFileName,
                    .Version = _externalTVThemeScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie
            VersionList.Add(New VersionItem With {.Name = _externalScraperModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalScraperModule.AssemblyFileName,
              .Version = _externalScraperModule.ProcessorModule.ModuleVersion})
        Next
        For Each _externalModule As _externalGenericModuleClass In externalGenericModules
            VersionList.Add(New VersionItem With {.Name = _externalModule.ProcessorModule.ModuleName,
              .AssemblyFileName = _externalModule.AssemblyFileName,
              .Version = _externalModule.ProcessorModule.ModuleVersion})
        Next
    End Sub

    Private Sub bwLoadModules_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadModules.DoWork
        LoadModules()
    End Sub

    Private Sub bwLoadModules_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadModules.RunWorkerCompleted
        BuildVersionList()
    End Sub

    Public Function GetMovieCollectionID(ByVal sIMDBID As String) As String
        Dim CollectionID As String = String.Empty

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(sIMDBID) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externalScraperModuleClass_Data As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet.Where(Function(e) e.ProcessorModule.ModuleName = "TMDB_Data")
                ret = _externalScraperModuleClass_Data.ProcessorModule.GetCollectionID(sIMDBID, CollectionID)
                If ret.breakChain Then Exit For
            Next
        End If
        Return CollectionID
    End Function

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement) As List(Of String)
        Dim ret As Interfaces.ModuleResult
        Dim sStudio As New List(Of String)
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = _externalScraperModule.ProcessorModule.GetMovieStudio(DBMovie, sStudio)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        sStudio = sStudio.Distinct().ToList() 'remove double entries
        Return sStudio
    End Function

    Public Function GetMovieTMDBID(ByRef sIMDBID As String) As String
        Dim TMDBID As String = String.Empty

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(sIMDBID) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externalScraperModuleClass_Data As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ModuleName = "TMDB_Data")
                ret = _externalScraperModuleClass_Data.ProcessorModule.GetTMDBID(sIMDBID, TMDBID)
                If ret.breakChain Then Exit For
            Next
        End If
        Return TMDBID
    End Function

    Public Sub GetVersions()
        Dim dlgVersions As New dlgVersions
        Dim li As ListViewItem
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each v As VersionItem In VersionList
            li = dlgVersions.lstVersions.Items.Add(v.Name)
            li.SubItems.Add(v.Version)
        Next
        dlgVersions.ShowDialog()
    End Sub

    Public Sub Handler_ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        RaiseEvent ScraperEvent_Movie(eType, Parameter)
    End Sub

    Public Sub Handler_ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        RaiseEvent ScraperEvent_MovieSet(eType, Parameter)
    End Sub

    Public Sub Handler_ScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
        RaiseEvent ScraperEvent_TV(eType, Parameter)
    End Sub

    Public Sub LoadAllModules()
        bwLoadModules.RunWorkerAsync()
    End Sub

    Public Sub LoadModules()
        Dim DataScraperAnyEnabled_Movie As Boolean = False
        Dim DataScraperAnyEnabled_MovieSet As Boolean = False
        Dim DataScraperAnyEnabled_TV As Boolean = False
        Dim DataScraperFound_Movie As Boolean = False
        Dim DataScraperFound_MovieSet As Boolean = False
        Dim DataScraperFound_TV As Boolean = False
        Dim ImageScraperAnyEnabled_Movie As Boolean = False
        Dim ImageScraperAnyEnabled_MovieSet As Boolean = False
        Dim ImageScraperAnyEnabled_TV As Boolean = False
        Dim ImageScraperFound_Movie As Boolean = False
        Dim ImageScraperFound_MovieSet As Boolean = False
        Dim ImageScraperFound_TV As Boolean = False
        Dim ThemeScraperAnyEnabled_Movie As Boolean = False
        Dim ThemeScraperAnyEnabled_TV As Boolean = False
        Dim ThemeScraperFound_Movie As Boolean = False
        Dim ThemeScraperFound_TV As Boolean = False
        Dim TrailerScraperAnyEnabled_Movie As Boolean = False
        Dim TrailerScraperFound_Movie As Boolean = False

        logger.Trace("[ModulesManager] [LoadModules] [Start]")

        If Directory.Exists(moduleLocation) Then
            'add each .dll file to AssemblyList
            For Each file As String In Directory.GetFiles(moduleLocation, "*.dll")
                Dim nAssembly As Reflection.Assembly = Reflection.Assembly.LoadFile(file)
                AssemblyList.Add(New ModulesManager.AssemblyListItem With {.Assembly = nAssembly, .AssemblyName = nAssembly.GetName.Name})
            Next

            For Each tAssemblyItem As AssemblyListItem In AssemblyList
                'Loop through each of the assemeblies type
                For Each fileType As Type In tAssemblyItem.Assembly.GetTypes

                    Dim fType As Type = fileType.GetInterface("GenericModule")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.GenericModule 'Object
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.GenericModule)

                        Dim GenericModule As New _externalGenericModuleClass
                        GenericModule.ProcessorModule = ProcessorModule
                        GenericModule.AssemblyName = tAssemblyItem.AssemblyName
                        GenericModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        GenericModule.Type = ProcessorModule.ModuleType
                        externalGenericModules.Add(GenericModule)

                        GenericModule.ProcessorModule.Init(GenericModule.AssemblyName, GenericModule.AssemblyFileName)

                        Dim bFound As Boolean = False
                        For Each i In Master.eSettings.EmberModules
                            If i.AssemblyName = GenericModule.AssemblyName Then
                                GenericModule.ProcessorModule.Enabled = i.GenericEnabled
                                bFound = True
                            End If
                        Next

                        'Enable all Core Modules by default if no setting was found
                        If Not bFound AndAlso GenericModule.AssemblyFileName.Contains("generic.EmberCore") Then
                            GenericModule.ProcessorModule.Enabled = True
                        End If
                        AddHandler ProcessorModule.GenericEvent, AddressOf GenericRunCallBack
                    End If

                    fType = fileType.GetInterface("ScraperModule_Data_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Data_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Data_Movie)

                        Dim ScraperModule As New _externalScraperModuleClass_Data_Movie
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Data_Movie.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                          f.ContentType = Enums.ContentType.Movie)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            DataScraperAnyEnabled_Movie = DataScraperAnyEnabled_Movie OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            DataScraperFound_Movie = True
                        Next
                        If Not DataScraperFound_Movie Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Image_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Image_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Image_Movie)

                        Dim ScraperModule As New _externalScraperModuleClass_Image_Movie
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Image_Movie.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                      f.ContentType = Enums.ContentType.Movie)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            ImageScraperAnyEnabled_Movie = ImageScraperAnyEnabled_Movie OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            ImageScraperFound_Movie = True
                        Next
                        If Not ImageScraperFound_Movie Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Trailer_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Trailer_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Trailer_Movie)

                        Dim ScraperModule As New _externalScraperModuleClass_Trailer_Movie
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Trailer_Movie.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                  f.ContentType = Enums.ContentType.Movie)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            TrailerScraperAnyEnabled_Movie = TrailerScraperAnyEnabled_Movie OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            TrailerScraperFound_Movie = True
                        Next
                        If Not TrailerScraperFound_Movie Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Theme_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Theme_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Theme_Movie)

                        Dim ScraperModule As New _externalScraperModuleClass_Theme_Movie
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Theme_Movie.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                          f.ContentType = Enums.ContentType.Movie)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            ThemeScraperAnyEnabled_Movie = ThemeScraperAnyEnabled_Movie OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            ThemeScraperFound_Movie = True
                        Next
                        If Not ThemeScraperFound_Movie Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Data_MovieSet")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Data_MovieSet
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Data_MovieSet)

                        Dim ScraperModule As New _externalScraperModuleClass_Data_MovieSet
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Data_MovieSet.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                      f.ContentType = Enums.ContentType.MovieSet)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            DataScraperAnyEnabled_MovieSet = DataScraperAnyEnabled_MovieSet OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            DataScraperFound_MovieSet = True
                        Next
                        If Not DataScraperFound_MovieSet Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Image_MovieSet")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Image_MovieSet
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Image_MovieSet)

                        Dim ScraperModule As New _externalScraperModuleClass_Image_MovieSet
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Image_MovieSet.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                  f.ContentType = Enums.ContentType.MovieSet)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            ImageScraperAnyEnabled_MovieSet = ImageScraperAnyEnabled_MovieSet OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            ImageScraperFound_MovieSet = True
                        Next
                        If Not ImageScraperFound_MovieSet Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Data_TV")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Data_TV
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Data_TV)

                        Dim ScraperModule As New _externalScraperModuleClass_Data_TV
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Data_TV.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                      f.ContentType = Enums.ContentType.TV)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            DataScraperAnyEnabled_TV = DataScraperAnyEnabled_TV OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            DataScraperFound_TV = True
                        Next
                        If Not DataScraperFound_TV Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Image_TV")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Image_TV
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Image_TV)

                        Dim ScraperModule As New _externalScraperModuleClass_Image_TV
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Image_TV.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                                      f.ContentType = Enums.ContentType.TV)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            ImageScraperAnyEnabled_TV = ImageScraperAnyEnabled_TV OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            ImageScraperFound_TV = True
                        Next
                        If Not ImageScraperFound_TV Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If

                    fType = fileType.GetInterface("ScraperModule_Theme_TV")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.ScraperModule_Theme_TV
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.ScraperModule_Theme_TV)

                        Dim ScraperModule As New _externalScraperModuleClass_Theme_TV
                        ScraperModule.ProcessorModule = ProcessorModule
                        ScraperModule.AssemblyName = tAssemblyItem.AssemblyName
                        ScraperModule.AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name
                        externalScrapersModules_Theme_TV.Add(ScraperModule)

                        logger.Trace(String.Concat("[ModulesManager] [LoadModules] Scraper Added: ", ScraperModule.AssemblyName, "_", ScraperModule.ContentType))

                        ScraperModule.ProcessorModule.Init(ScraperModule.AssemblyName)

                        For Each i As _XMLEmberModuleClass In Master.eSettings.EmberModules.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName)
                            ScraperModule.ProcessorModule.ScraperEnabled = i.ModuleEnabled
                            ThemeScraperAnyEnabled_TV = ThemeScraperAnyEnabled_TV OrElse i.ModuleEnabled
                            ScraperModule.ModuleOrder = i.ModuleOrder
                            ThemeScraperFound_TV = True
                        Next
                        If Not ThemeScraperFound_TV Then
                            ScraperModule.ModuleOrder = 999
                        End If
                    End If
                Next
            Next

            'Modules ordering
            Dim c As Integer = 0
            For Each ext As _externalGenericModuleClass In externalGenericModules.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Data_TV In externalScrapersModules_Data_TV.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Image_TV In externalScrapersModules_Image_TV.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next
            c = 0
            For Each ext As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV.OrderBy(Function(f) f.ModuleOrder)
                ext.ModuleOrder = c
                c += 1
            Next

            'Enable default Modules
            If Not DataScraperAnyEnabled_Movie AndAlso Not DataScraperFound_Movie Then
                SetScraperEnable_Data_Movie("scraper.Data.TMDB", True)
            End If
            If Not ImageScraperAnyEnabled_Movie AndAlso Not ImageScraperFound_Movie Then
                SetScraperEnable_Image_Movie("scraper.Image.FanartTV", True)
                SetScraperEnable_Image_Movie("scraper.Image.TMDB", True)
            End If
            If Not ThemeScraperAnyEnabled_Movie AndAlso Not ThemeScraperFound_Movie Then
                SetScraperEnable_Theme_Movie("scraper.Theme.TelevisionTunes", True)
            End If
            If Not TrailerScraperAnyEnabled_Movie AndAlso Not TrailerScraperFound_Movie Then
                SetScraperEnable_Trailer_Movie("scraper.Trailer.TMDB", True)
            End If
            If Not DataScraperAnyEnabled_MovieSet AndAlso Not DataScraperFound_MovieSet Then
                SetScraperEnable_Data_MovieSet("scraper.Data.TMDB", True)
            End If
            If Not ImageScraperAnyEnabled_MovieSet AndAlso Not ImageScraperFound_MovieSet Then
                SetScraperEnable_Image_MovieSet("scraper.Image.FanartTV", True)
                SetScraperEnable_Image_MovieSet("scraper.Image.TMDB", True)
            End If
            If Not DataScraperAnyEnabled_TV AndAlso Not DataScraperFound_TV Then
                SetScraperEnable_Data_TV("scraper.Data.TVDB", True)
            End If
            If Not ImageScraperAnyEnabled_TV AndAlso Not ImageScraperFound_TV Then
                SetScraperEnable_Image_TV("scraper.Image.FanartTV", True)
                SetScraperEnable_Image_TV("scraper.Image.TMDB", True)
                SetScraperEnable_Image_TV("scraper.Image.TVDB", True)
            End If
            If Not ThemeScraperAnyEnabled_TV AndAlso Not ThemeScraperFound_TV Then
                SetScraperEnable_Theme_TV("scraper.TelevisionTunes.Theme", True)
            End If
        End If

        logger.Trace("[ModulesManager] [LoadModules] [Done]")
    End Sub

    Function QueryAnyGenericIsBusy() As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        Dim modules As IEnumerable(Of _externalGenericModuleClass) = externalGenericModules.Where(Function(e) e.ProcessorModule.IsBusy)
        If modules.Count() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Function QueryScraperCapabilities_Image_Movie(ByVal externalScraperModule As _externalScraperModuleClass_Image_Movie, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
        If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
        If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
        If ScrapeModifiers.MainDiscArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainDiscArt) Then Return True
        If ScrapeModifiers.MainExtrafanarts AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainExtrathumbs AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
        If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_Movie(ByVal externalScraperModule As _externalScraperModuleClass_Image_Movie, ByVal ImageType As Enums.ModifierType) As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        Select Case ImageType
            Case Enums.ModifierType.MainExtrafanarts
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
            Case Enums.ModifierType.MainExtrathumbs
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
            Case Else
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)
        End Select

        Return False
    End Function

    Function QueryScraperCapabilities_Image_MovieSet(ByVal externalScraperModule As _externalScraperModuleClass_Image_MovieSet, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
        If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
        If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
        If ScrapeModifiers.MainDiscArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainDiscArt) Then Return True
        If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
        If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_MovieSet(ByVal externalScraperModule As _externalScraperModuleClass_Image_MovieSet, ByVal ImageType As Enums.ModifierType) As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)

        Return False
    End Function

    Function QueryScraperCapabilities_Image_TV(ByVal externalScraperModule As _externalScraperModuleClass_Image_TV, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If ScrapeModifiers.EpisodeFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodeFanart) Then Return True
        If ScrapeModifiers.EpisodePoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodePoster) Then Return True
        If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
        If ScrapeModifiers.MainCharacterArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainCharacterArt) Then Return True
        If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
        If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
        If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
        If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True
        If ScrapeModifiers.SeasonBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonBanner) Then Return True
        If ScrapeModifiers.SeasonFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
        If ScrapeModifiers.SeasonLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonLandscape) Then Return True
        If ScrapeModifiers.SeasonPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonPoster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_TV(ByVal externalScraperModule As _externalScraperModuleClass_Image_TV, ByVal ImageType As Enums.ModifierType) As Boolean
        While Not ModulesLoaded
            Application.DoEvents()
        End While

        Select Case ImageType
            Case Enums.ModifierType.AllSeasonsBanner
                If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) OrElse
                    externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonBanner) Then Return True
            Case Enums.ModifierType.AllSeasonsFanart
                If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) OrElse
                    externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
            Case Enums.ModifierType.AllSeasonsLandscape
                If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) OrElse
                    externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonLandscape) Then Return True
            Case Enums.ModifierType.AllSeasonsPoster
                If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) OrElse
                    externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonPoster) Then Return True
            Case Enums.ModifierType.EpisodeFanart
                If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) OrElse
                    externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodeFanart) Then Return True
            Case Enums.ModifierType.MainExtrafanarts
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
            Case Enums.ModifierType.SeasonFanart
                If externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) OrElse
                    externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
            Case Else
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)
        End Select

        Return False
    End Function
    ''' <summary>
    ''' Calls all the generic modules of the supplied type (if one is defined), passing the supplied _params.
    ''' The module will do its task and return any expected results in the _refparams.
    ''' </summary>
    ''' <param name="mType">The <c>Enums.ModuleEventType</c> of module to execute.</param>
    ''' <param name="_params">Parameters to pass to the module</param>
    ''' <param name="_singleobjekt"><c>Object</c> representing the module's result (if relevant)</param>
    ''' <param name="RunOnlyOne">If <c>True</c>, allow only one module to perform the required task.</param>
    ''' <returns></returns>
    ''' <remarks>Note that if any module returns a result of breakChain, no further modules are processed</remarks>
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), Optional ByVal _singleobjekt As Object = Nothing, Optional ByVal RunOnlyOne As Boolean = False, Optional ByRef DBElement As Database.DBElement = Nothing) As Boolean
        logger.Trace(String.Format("[ModulesManager] [RunGeneric] [Start] <{0}>", mType.ToString))
        Dim ret As Interfaces.ModuleResult

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        Try
            Dim modules As IEnumerable(Of _externalGenericModuleClass) = externalGenericModules.Where(Function(e) e.ProcessorModule.ModuleType.Contains(mType) AndAlso e.ProcessorModule.Enabled)
            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [RunGeneric] No generic modules defined <{0}>", mType.ToString)
            Else
                For Each _externalGenericModule As _externalGenericModuleClass In modules
                    Try
                        logger.Trace("[ModulesManager] [RunGeneric] Run generic module <{0}>", _externalGenericModule.ProcessorModule.ModuleName)
                        ret = _externalGenericModule.ProcessorModule.RunGeneric(mType, _params, _singleobjekt, DBElement)
                    Catch ex As Exception
                        logger.Error("[ModulesManager] [RunGeneric] Run generic module <{0}>", _externalGenericModule.ProcessorModule.ModuleName)
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                    If ret.breakChain OrElse RunOnlyOne Then Exit For
                Next
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return ret.Cancelled
    End Function

    Public Sub SaveSettings()
        Dim tmpForXML As New List(Of _XMLEmberModuleClass)

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        For Each _externalProcessorModule As _externalGenericModuleClass In externalGenericModules
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalProcessorModule.AssemblyName
            t.AssemblyFileName = _externalProcessorModule.AssemblyFileName
            t.GenericEnabled = _externalProcessorModule.ProcessorModule.Enabled
            t.ContentType = _externalProcessorModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In externalScrapersModules_Data_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In externalScrapersModules_Data_MovieSet
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Data_TV In externalScrapersModules_Data_TV
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Image_TV In externalScrapersModules_Image_TV
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalTVScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalTVScraperModule.AssemblyName
            t.AssemblyFileName = _externalTVScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalTVScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalTVScraperModule.ModuleOrder
            t.ContentType = _externalTVScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie
            Dim t As New _XMLEmberModuleClass
            t.AssemblyName = _externalScraperModule.AssemblyName
            t.AssemblyFileName = _externalScraperModule.AssemblyFileName
            t.ModuleEnabled = _externalScraperModule.ProcessorModule.ScraperEnabled
            t.ModuleOrder = _externalScraperModule.ModuleOrder
            t.ContentType = _externalScraperModule.ContentType
            tmpForXML.Add(t)
        Next
        Master.eSettings.EmberModules = tmpForXML
        Master.eSettings.Save()
    End Sub

    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Movie to be scraped</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeData_Movie(ByRef DBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeData_Movie] [Start] {0}", DBElement.Filename))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_Movie) = externalScrapersModules_Data_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Dim ret As Interfaces.ModuleResult_Data_Movie
            Dim ScrapedList As New List(Of MediaContainers.Movie)

            While Not ModulesLoaded
                Application.DoEvents()
            End While

            'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
            If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso ScrapeModifiers.DoSearch Then
                DBElement.ImagesContainer = New MediaContainers.ImagesContainer
                DBElement.Movie = New MediaContainers.Movie

                DBElement.Movie.Title = StringUtils.FilterTitleFromPath_Movie(DBElement.Filename, DBElement.IsSingle, DBElement.Source.UseFolderName)
                DBElement.Movie.VideoSource = DBElement.VideoSource
                DBElement.Movie.Year = StringUtils.FilterYearFromPath_Movie(DBElement.Filename, DBElement.IsSingle, DBElement.Source.UseFolderName)
            End If

            'create a clone of DBMovie
            Dim oDBMovie As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [ScrapeData_Movie] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In modules
                    logger.Trace(String.Format("[ModulesManager] [ScrapeData_Movie] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie

                    ret = _externalScraperModule.ProcessorModule.Scraper_Movie(oDBMovie, ScrapeModifiers, ScrapeType, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers
                        If ret.Result.IMDBSpecified Then
                            oDBMovie.Movie.IMDB = ret.Result.IMDB
                        End If
                        If ret.Result.OriginalTitleSpecified Then
                            oDBMovie.Movie.OriginalTitle = ret.Result.OriginalTitle
                        End If
                        If ret.Result.TitleSpecified Then
                            oDBMovie.Movie.Title = ret.Result.Title
                        End If
                        If ret.Result.TMDBSpecified Then
                            oDBMovie.Movie.TMDB = ret.Result.TMDB
                        End If
                        If ret.Result.YearSpecified Then
                            oDBMovie.Movie.Year = ret.Result.Year
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = NFO.MergeDataScraperResults_Movie(DBElement, ScrapedList, ScrapeType, ScrapeOptions)

                'create cache paths for Actor Thumbs
                DBElement.Movie.CreateCachePaths_ActorsThumbs()
            End If

            If ScrapedList.Count > 0 Then
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_Movie] [Done] {0}", DBElement.Filename))
            Else
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_Movie] [Done] [No Scraper Results] {0}", DBElement.Filename))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeData_Movie] [Abort] [Offline] {0}", DBElement.Filename))
            Return True 'Cancelled
        End If
    End Function
    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">MovieSet to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie set scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeData_MovieSet(ByRef DBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeData_MovieSet] [Start] {0}", DBElement.MovieSet.Title))
        'If DBMovieSet.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_MovieSet(DBMovieSet, showMessage) Then
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_MovieSet) = externalScrapersModules_Data_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult_Data_MovieSet
        Dim ScrapedList As New List(Of MediaContainers.MovieSet)

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
        If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso ScrapeModifiers.DoSearch Then
            Dim tmpTitle As String = DBElement.MovieSet.Title

            DBElement.ImagesContainer = New MediaContainers.ImagesContainer
            DBElement.MovieSet = New MediaContainers.MovieSet

            DBElement.MovieSet.Title = tmpTitle
        End If

        'create a clone of DBMovieSet
        Dim oDBMovieSet As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

        If (modules.Count() <= 0) Then
            logger.Warn("[ModulesManager] [ScrapeData_MovieSet] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In modules
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_MovieSet] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet

                ret = _externalScraperModule.ProcessorModule.Scraper(oDBMovieSet, ScrapeModifiers, ScrapeType, ScrapeOptions)

                If ret.Cancelled Then
                    logger.Trace(String.Format("[ModulesManager] [ScrapeData_MovieSet] [Cancelled] [No Scraper Results] {0}", DBElement.MovieSet.Title))
                    Return ret.Cancelled
                End If

                If ret.Result IsNot Nothing Then
                    ScrapedList.Add(ret.Result)

                    'set new informations for following scrapers
                    If ret.Result.TitleSpecified Then
                        oDBMovieSet.MovieSet.Title = ret.Result.Title
                    End If
                    If ret.Result.TMDBSpecified Then
                        oDBMovieSet.MovieSet.TMDB = ret.Result.TMDB
                    End If
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
                If ret.breakChain Then Exit For
            Next

            'Merge scraperresults considering global datascraper settings
            DBElement = NFO.MergeDataScraperResults_MovieSet(DBElement, ScrapedList, ScrapeType, ScrapeOptions)
        End If

        If ScrapedList.Count > 0 Then
            logger.Trace(String.Format("[ModulesManager] [ScrapeData_MovieSet] [Done] {0}", DBElement.MovieSet.Title))
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeData_MovieSet] [Done] [No Scraper Results] {0}", DBElement.MovieSet.Title))
            Return True 'TODO: need a new trigger
        End If
        Return ret.Cancelled
        'Else
        'Return True 'Cancelled
        'End If
    End Function

    Public Function ScrapeData_TVEpisode(ByRef DBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVEpisode] [Start] {0}", DBElement.Filename))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_TV) = externalScrapersModules_Data_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Dim ret As Interfaces.ModuleResult_Data_TVEpisode
            Dim ScrapedList As New List(Of MediaContainers.EpisodeDetails)

            While Not ModulesLoaded
                Application.DoEvents()
            End While

            'create a clone of DBTV
            Dim oEpisode As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [ScrapeData_TVEpisode] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As _externalScraperModuleClass_Data_TV In modules
                    logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVEpisode] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV

                    ret = _externalScraperModule.ProcessorModule.Scraper_TVEpisode(oEpisode, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers
                        If ret.Result.AiredSpecified Then
                            oEpisode.TVEpisode.Aired = ret.Result.Aired
                        End If
                        If ret.Result.EpisodeSpecified Then
                            oEpisode.TVEpisode.Episode = ret.Result.Episode
                        End If
                        If ret.Result.IMDBSpecified Then
                            oEpisode.TVEpisode.IMDB = ret.Result.IMDB
                        End If
                        If ret.Result.SeasonSpecified Then
                            oEpisode.TVEpisode.Season = ret.Result.Season
                        End If
                        If ret.Result.TitleSpecified Then
                            oEpisode.TVEpisode.Title = ret.Result.Title
                        End If
                        If ret.Result.TMDBSpecified Then
                            oEpisode.TVEpisode.TMDB = ret.Result.TMDB
                        End If
                        If ret.Result.TVDBSpecified Then
                            oEpisode.TVEpisode.TVDB = ret.Result.TVDB
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = NFO.MergeDataScraperResults_TVEpisode_Single(DBElement, ScrapedList, ScrapeOptions)

                'create cache paths for Actor Thumbs
                DBElement.TVEpisode.CreateCachePaths_ActorsThumbs()
            End If

            If ScrapedList.Count > 0 Then
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVEpisode] [Done] {0}", DBElement.Filename))
            Else
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVEpisode] [Done] [No Scraper Results] {0}", DBElement.Filename))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVEpisode] [Abort] [Offline] {0}", DBElement.Filename))
            Return True 'Cancelled
        End If
    End Function

    Public Function ScrapeData_TVSeason(ByRef DBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVSeason] [Start] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_TV) = externalScrapersModules_Data_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Dim ret As Interfaces.ModuleResult_Data_TVSeason
            Dim ScrapedList As New List(Of MediaContainers.SeasonDetails)

            While Not ModulesLoaded
                Application.DoEvents()
            End While

            'create a clone of DBTV
            Dim oSeason As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [ScrapeData_TVSeason] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As _externalScraperModuleClass_Data_TV In modules
                    logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVSeason] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV

                    ret = _externalScraperModule.ProcessorModule.Scraper_TVSeason(oSeason, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers
                        If ret.Result.TMDBSpecified Then
                            oSeason.TVSeason.TMDB = ret.Result.TMDB
                        End If
                        If ret.Result.TVDBSpecified Then
                            oSeason.TVSeason.TVDB = ret.Result.TVDB
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = NFO.MergeDataScraperResults_TVSeason(DBElement, ScrapedList, ScrapeOptions)
            End If

            If ScrapedList.Count > 0 Then
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVSeason] [Done] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
            Else
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVSeason] [Done] [No Scraper Results] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVSeason] [Abort] [Offline] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
            Return True 'Cancelled
        End If
    End Function
    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Show to be scraped</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeData_TVShow(ByRef DBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVShow] [Start] {0}", DBElement.TVShow.Title))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_TV) = externalScrapersModules_Data_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Dim ret As Interfaces.ModuleResult_Data_TVShow
            Dim ScrapedList As New List(Of MediaContainers.TVShow)

            While Not ModulesLoaded
                Application.DoEvents()
            End While

            'clean DBTV if the tv show is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
            If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso ScrapeModifiers.DoSearch Then
                DBElement.ExtrafanartsPath = String.Empty
                DBElement.ImagesContainer = New MediaContainers.ImagesContainer
                DBElement.NfoPath = String.Empty
                DBElement.Seasons.Clear()
                DBElement.Theme = New MediaContainers.Theme
                DBElement.TVShow = New MediaContainers.TVShow

                DBElement.TVShow.Title = StringUtils.FilterTitleFromPath_TVShow(DBElement.ShowPath)

                For Each sEpisode As Database.DBElement In DBElement.Episodes
                    Dim iEpisode As Integer = sEpisode.TVEpisode.Episode
                    Dim iSeason As Integer = sEpisode.TVEpisode.Season
                    Dim strAired As String = sEpisode.TVEpisode.Aired
                    sEpisode.ImagesContainer = New MediaContainers.ImagesContainer
                    sEpisode.NfoPath = String.Empty
                    sEpisode.TVEpisode = New MediaContainers.EpisodeDetails With {.Aired = strAired, .Episode = iEpisode, .Season = iSeason}
                    sEpisode.TVEpisode.VideoSource = sEpisode.VideoSource
                Next
            End If

            'create a clone of DBTV
            Dim oShow As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [ScrapeData_TVShow] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As _externalScraperModuleClass_Data_TV In modules
                    logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVShow] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV

                    ret = _externalScraperModule.ProcessorModule.Scraper_TVShow(oShow, ScrapeModifiers, ScrapeType, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers
                        If ret.Result.IMDBSpecified Then
                            oShow.TVShow.IMDB = ret.Result.IMDB
                        End If
                        If ret.Result.OriginalTitleSpecified Then
                            oShow.TVShow.OriginalTitle = ret.Result.OriginalTitle
                        End If
                        If ret.Result.TitleSpecified Then
                            oShow.TVShow.Title = ret.Result.Title
                        End If
                        If ret.Result.TMDBSpecified Then
                            oShow.TVShow.TMDB = ret.Result.TMDB
                        End If
                        If ret.Result.TVDBSpecified Then
                            oShow.TVShow.TVDB = ret.Result.TVDB
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = NFO.MergeDataScraperResults_TV(DBElement, ScrapedList, ScrapeType, ScrapeOptions, ScrapeModifiers.withEpisodes)

                'create cache paths for Actor Thumbs
                DBElement.TVShow.CreateCachePaths_ActorsThumbs()
                If ScrapeModifiers.withEpisodes Then
                    For Each tEpisode As Database.DBElement In DBElement.Episodes
                        tEpisode.TVEpisode.CreateCachePaths_ActorsThumbs()
                    Next
                End If
            End If

            If ScrapedList.Count > 0 Then
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVShow] [Done] {0}", DBElement.TVShow.Title))
            Else
                logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVShow] [Done] [No Scraper Results] {0}", DBElement.TVShow.Title))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeData_TVShow] [Abort] [Offline] {0}", DBElement.TVShow.Title))
            Return True 'Cancelled
        End If
    End Function
    ''' <summary>
    ''' Request that enabled movie image scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="ImagesContainer">Container of images that the scraper should add to</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeImage_Movie(ByRef DBElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeImage_Movie] [Start] {0}", DBElement.Filename))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_Movie) = externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Dim ret As Interfaces.ModuleResult

            While Not ModulesLoaded
                Application.DoEvents()
            End While

            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [ScrapeImage_Movie] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In modules
                    logger.Trace(String.Format("[ModulesManager] [ScrapeImage_Movie] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                    If QueryScraperCapabilities_Image_Movie(_externalScraperModule, ScrapeModifiers) Then
                        AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        ret = _externalScraperModule.ProcessorModule.Scraper(DBElement, aContainer, ScrapeModifiers)
                        If aContainer IsNot Nothing Then
                            ImagesContainer.MainBanners.AddRange(aContainer.MainBanners)
                            ImagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
                            ImagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
                            ImagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
                            ImagesContainer.MainDiscArts.AddRange(aContainer.MainDiscArts)
                            ImagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
                            ImagesContainer.MainLandscapes.AddRange(aContainer.MainLandscapes)
                            ImagesContainer.MainPosters.AddRange(aContainer.MainPosters)
                        End If
                        RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                        If ret.breakChain Then Exit For
                    End If
                Next

                'sorting
                ImagesContainer.SortAndFilter(DBElement)

                'create cache paths
                ImagesContainer.CreateCachePaths(DBElement)
            End If

            logger.Trace(String.Format("[ModulesManager] [ScrapeImage_Movie] [Done] {0}", DBElement.Filename))
            Return ret.Cancelled
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeImage_Movie] [Abort] [Offline] {0}", DBElement.Filename))
            Return True 'Cancelled
        End If
    End Function
    ''' <summary>
    ''' Request that enabled movieset image scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Movieset to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="ImagesContainer">Container of images that the scraper should add to</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeImage_MovieSet(ByRef DBElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeImage_MovieSet] [Start] {0}", DBElement.MovieSet.Title))
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_MovieSet) = externalScrapersModules_Image_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("[ModulesManager] [ScrapeImage_MovieSet] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In modules
                logger.Trace(String.Format("[ModulesManager] [ScrapeImage_MovieSet] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                If QueryScraperCapabilities_Image_MovieSet(_externalScraperModule, ScrapeModifiers) Then
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
                    Dim aContainer As New MediaContainers.SearchResultsContainer
                    ret = _externalScraperModule.ProcessorModule.Scraper(DBElement, aContainer, ScrapeModifiers)
                    If aContainer IsNot Nothing Then
                        ImagesContainer.MainBanners.AddRange(aContainer.MainBanners)
                        ImagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
                        ImagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
                        ImagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
                        ImagesContainer.MainDiscArts.AddRange(aContainer.MainDiscArts)
                        ImagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
                        ImagesContainer.MainLandscapes.AddRange(aContainer.MainLandscapes)
                        ImagesContainer.MainPosters.AddRange(aContainer.MainPosters)
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
                    If ret.breakChain Then Exit For
                End If
            Next

            'sorting
            ImagesContainer.SortAndFilter(DBElement)

            'create cache paths
            ImagesContainer.CreateCachePaths(DBElement)
        End If

        logger.Trace(String.Format("[ModulesManager] [ScrapeImage_MovieSet] [Done] {0}", DBElement.MovieSet.Title))
        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled tv image scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">TV Show to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="ScrapeModifiers">What kind of image is being scraped (poster, fanart, etc)</param>
    ''' <param name="ImagesContainer">Container of images that the scraper should add to</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeImage_TV(ByRef DBElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers, ByVal showMessage As Boolean) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeImage_TV] [Start] {0}", DBElement.TVShow.Title))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_TV) = externalScrapersModules_Image_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Dim ret As Interfaces.ModuleResult

            While Not ModulesLoaded
                Application.DoEvents()
            End While

            'workaround to get MainFanarts for AllSeasonsFanarts, EpisodeFanarts and SeasonFanarts,
            'also get MainBanners, MainLandscapes and MainPosters for AllSeasonsBanners, AllSeasonsLandscapes and AllSeasonsPosters
            If ScrapeModifiers.AllSeasonsBanner Then
                ScrapeModifiers.MainBanner = True
                ScrapeModifiers.SeasonBanner = True
            End If
            If ScrapeModifiers.AllSeasonsFanart Then
                ScrapeModifiers.MainFanart = True
                ScrapeModifiers.SeasonFanart = True
            End If
            If ScrapeModifiers.AllSeasonsLandscape Then
                ScrapeModifiers.MainLandscape = True
                ScrapeModifiers.SeasonLandscape = True
            End If
            If ScrapeModifiers.AllSeasonsPoster Then
                ScrapeModifiers.MainPoster = True
                ScrapeModifiers.SeasonPoster = True
            End If
            If ScrapeModifiers.EpisodeFanart Then
                ScrapeModifiers.MainFanart = True
            End If
            If ScrapeModifiers.MainExtrafanarts Then
                ScrapeModifiers.MainFanart = True
            End If
            If ScrapeModifiers.MainExtrathumbs Then
                ScrapeModifiers.MainFanart = True
            End If
            If ScrapeModifiers.SeasonFanart Then
                ScrapeModifiers.MainFanart = True
            End If

            If (modules.Count() <= 0) Then
                logger.Warn("[ModulesManager] [ScrapeImage_TV] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As _externalScraperModuleClass_Image_TV In modules
                    logger.Trace(String.Format("[ModulesManager] [ScrapeImage_TV] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                    If QueryScraperCapabilities_Image_TV(_externalScraperModule, ScrapeModifiers) Then
                        AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        ret = _externalScraperModule.ProcessorModule.Scraper(DBElement, aContainer, ScrapeModifiers)
                        If aContainer IsNot Nothing Then
                            ImagesContainer.EpisodeFanarts.AddRange(aContainer.EpisodeFanarts)
                            ImagesContainer.EpisodePosters.AddRange(aContainer.EpisodePosters)
                            ImagesContainer.SeasonBanners.AddRange(aContainer.SeasonBanners)
                            ImagesContainer.SeasonFanarts.AddRange(aContainer.SeasonFanarts)
                            ImagesContainer.SeasonLandscapes.AddRange(aContainer.SeasonLandscapes)
                            ImagesContainer.SeasonPosters.AddRange(aContainer.SeasonPosters)
                            ImagesContainer.MainBanners.AddRange(aContainer.MainBanners)
                            ImagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
                            ImagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
                            ImagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
                            ImagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
                            ImagesContainer.MainLandscapes.AddRange(aContainer.MainLandscapes)
                            ImagesContainer.MainPosters.AddRange(aContainer.MainPosters)
                        End If
                        RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                        If ret.breakChain Then Exit For
                    End If
                Next

                'sorting
                ImagesContainer.SortAndFilter(DBElement)

                'create cache paths
                ImagesContainer.CreateCachePaths(DBElement)
            End If

            logger.Trace(String.Format("[ModulesManager] [ScrapeImage_TV] [Done] {0}", DBElement.TVShow.Title))
            Return ret.Cancelled
        Else
            logger.Trace(String.Format("[ModulesManager] [ScrapeImage_Movie] [Abort] [Offline] {0}", DBElement.Filename))
            Return True 'Cancelled
        End If
    End Function
    ''' <summary>
    ''' Request that enabled movie theme scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="Type">NOT ACTUALLY USED!</param>
    ''' <param name="ThemeList">List of Theme objects that the scraper will append to.</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks></remarks>
    Public Function ScrapeTheme_Movie(ByRef DBElement As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.Theme)) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeTheme_Movie] [Start] {0}", DBElement.Filename))
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_Movie) = externalScrapersModules_Theme_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("[ModulesManager] [ScrapeTheme_Movie] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In modules
                logger.Trace(String.Format("[ModulesManager] [ScrapeTheme_Movie] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                Dim aList As New List(Of MediaContainers.Theme)
                ret = _externalScraperModule.ProcessorModule.Scraper(DBElement, Type, aList)
                If aList IsNot Nothing Then
                    ThemeList.AddRange(aList)
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                If ret.breakChain Then Exit For
            Next
        End If
        logger.Trace(String.Format("[ModulesManager] [ScrapeTheme_Movie] [Done] {0}", DBElement.Filename))
        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled tvshow theme scrapers perform their functions on the supplied tv show
    ''' </summary>
    ''' <param name="DBElement">TV Show to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="Type">NOT ACTUALLY USED!</param>
    ''' <param name="ThemeList">List of Theme objects that the scraper will append to.</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks></remarks>
    Public Function ScrapeTheme_TVShow(ByRef DBElement As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.Theme)) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeTheme_TVShow] [Start] {0}", DBElement.TVShow.Title))
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_TV) = externalScrapersModules_Theme_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("[ModulesManager] [ScrapeTheme_TVShow] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_TV In modules
                logger.Trace(String.Format("[ModulesManager] [ScrapeTheme_TVShow] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                Dim aList As New List(Of MediaContainers.Theme)
                ret = _externalScraperModule.ProcessorModule.Scraper(DBElement, Type, aList)
                If aList IsNot Nothing Then
                    ThemeList.AddRange(aList)
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                If ret.breakChain Then Exit For
            Next
        End If
        logger.Trace(String.Format("[ModulesManager] [ScrapeTheme_TVShow] [Done] {0}", DBElement.TVShow.Title))
        Return ret.Cancelled
    End Function
    ''' <summary>
    ''' Request that enabled movie trailer scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Movie to be scraped. Scraper will directly manipulate this structure</param>
    ''' <param name="Type">NOT ACTUALLY USED!</param>
    ''' <param name="TrailerList">List of Trailer objects that the scraper will append to. Note that only the URL is returned, 
    ''' not the full content of the trailer</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks></remarks>
    Public Function ScrapeTrailer_Movie(ByRef DBElement As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.Trailer)) As Boolean
        logger.Trace(String.Format("[ModulesManager] [ScrapeTrailer_Movie] [Start] {0}", DBElement.Filename))
        Dim modules As IEnumerable(Of _externalScraperModuleClass_Trailer_Movie) = externalScrapersModules_Trailer_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
        Dim ret As Interfaces.ModuleResult

        While Not ModulesLoaded
            Application.DoEvents()
        End While

        If (modules.Count() <= 0) Then
            logger.Warn("[ModulesManager] [ScrapeTrailer_Movie] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In modules
                logger.Trace(String.Format("[ModulesManager] [ScrapeTrailer_Movie] [Using] {0}", _externalScraperModule.ProcessorModule.ModuleName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                Dim aList As New List(Of MediaContainers.Trailer)
                ret = _externalScraperModule.ProcessorModule.Scraper(DBElement, Type, aList)
                If aList IsNot Nothing Then
                    TrailerList.AddRange(aList)
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                If ret.breakChain Then Exit For
            Next
        End If
        logger.Trace(String.Format("[ModulesManager] [ScrapeTrailer_Movie] [Done] {0}", DBElement.Filename))
        Return ret.Cancelled
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_Movie(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In externalScrapersModules_Image_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = QueryScraperCapabilities_Image_Movie(_externalScraperModule, ImageType)
                If ret Then Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_MovieSet(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In externalScrapersModules_Image_MovieSet.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = QueryScraperCapabilities_Image_MovieSet(_externalScraperModule, ImageType)
                If ret Then Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_TV(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Image_TV In externalScrapersModules_Image_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = QueryScraperCapabilities_Image_TV(_externalScraperModule, ImageType)
                If ret Then Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function ScraperWithCapabilityAnyEnabled_Theme_Movie(ByVal cap As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In externalScrapersModules_Theme_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = True 'if a theme scraper is enabled we can exit.
                Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function ScraperWithCapabilityAnyEnabled_Theme_TV(ByVal cap As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Theme_TV In externalScrapersModules_Theme_TV.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = True 'if a theme scraper is enabled we can exit.
                Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    Function ScraperWithCapabilityAnyEnabled_Trailer_Movie(ByVal cap As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not ModulesLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In externalScrapersModules_Trailer_Movie.Where(Function(e) e.ProcessorModule.ScraperEnabled).OrderBy(Function(e) e.ModuleOrder)
            Try
                ret = True 'if a trailer scraper is enabled we can exit.
                Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="ModuleAssembly"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="ModuleAssembly"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>
    Public Sub SetModuleEnable_Generic(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetModuleEnable_Generic] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalGenericModuleClass) = externalGenericModules.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetModuleEnable_Generic] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalProcessorModule As _externalGenericModuleClass In modules
                Try
                    _externalProcessorModule.ProcessorModule.Enabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Data_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_Movie) = externalScrapersModules_Data_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Data_Movie]  modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_MovieSet(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Data_MovieSet] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_MovieSet) = externalScrapersModules_Data_MovieSet.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Data_MovieSet] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_MovieSet In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_TV(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Data_TV] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Data_TV) = externalScrapersModules_Data_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Data_TV] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Data_TV In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Image_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_Movie) = externalScrapersModules_Image_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Image_Movie] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_MovieSet(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Image_MovieSet] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_MovieSet) = externalScrapersModules_Image_MovieSet.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Image_MovieSet] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_MovieSet In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_TV(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Image_TV] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Image_TV) = externalScrapersModules_Image_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Image_TV] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Image_TV In externalScrapersModules_Image_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            Next
        End If
    End Sub

    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="ModuleAssembly"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="ModuleAssembly"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>

    Public Sub SetScraperEnable_Theme_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Theme_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_Movie) = externalScrapersModules_Theme_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Theme_Movie] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Theme_TV(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Theme_TV] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Theme_TV) = externalScrapersModules_Theme_TV.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Theme_TV] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Theme_TV In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub
    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="ModuleAssembly"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="ModuleAssembly"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>

    Public Sub SetScraperEnable_Trailer_Movie(ByVal ModuleAssembly As String, ByVal value As Boolean)
        If (String.IsNullOrEmpty(ModuleAssembly)) Then
            logger.Error("[ModulesManager] [SetScraperEnable_Trailer_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of _externalScraperModuleClass_Trailer_Movie) = externalScrapersModules_Trailer_Movie.Where(Function(p) p.AssemblyName = ModuleAssembly)
        If (modules.Count < 0) Then
            logger.Warn("[ModulesManager] [SetScraperEnable_Trailer_Movie] No modules of type <{0}> were found", ModuleAssembly)
        Else
            For Each _externalScraperModule As _externalScraperModuleClass_Trailer_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.ScraperEnabled = value
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & ModuleAssembly & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Private Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        RaiseEvent GenericEvent(mType, _params)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AssemblyListItem

#Region "Fields"

        Public Assembly As System.Reflection.Assembly
        Public AssemblyName As String

#End Region 'Fields

    End Structure

    Structure VersionItem

#Region "Fields"

        Public AssemblyFileName As String
        Public Name As String
        Public NeedUpdate As Boolean
        Public Version As String

#End Region 'Fields

    End Structure

    Class EmberRuntimeObjects

#Region "Fields"

        Private _ContextMenuMovieList As ContextMenuStrip
        Private _ContextMenuMovieSetList As ContextMenuStrip
        Private _ContextMenuTVEpisodeList As ContextMenuStrip
        Private _ContextMenuTVSeasonList As ContextMenuStrip
        Private _ContextMenuTVShowList As ContextMenuStrip
        Private _FilterMovies As String
        Private _FilterMoviesSearch As String
        Private _FilterMoviesType As String
        Private _FilterTVShows As String
        Private _FilterTVShowsSearch As String
        Private _FilterTVShowsType As String
        Private _ListMovieSets As String
        Private _ListMovies As String
        Private _ListTVShows As String
        Private _LoadMedia As LoadMedia
        Private _MainMenu As MenuStrip
        Private _MainTabControl As TabControl
        Private _MainToolStrip As ToolStrip
        Private _MediaListMovieSets As DataGridView
        Private _MediaListMovies As DataGridView
        Private _MediaListTVEpisodes As DataGridView
        Private _MediaListTVSeasons As DataGridView
        Private _MediaListTVShows As DataGridView
        Private _MediaTabSelected As Structures.MainTabType
        Private _OpenImageViewer As OpenImageViewer
        Private _TrayMenu As ContextMenuStrip


#End Region 'Fields

#Region "Delegates"

        Delegate Sub LoadMedia(ByVal Scan As Structures.ScanOrClean, ByVal SourceID As Long)

        'all runtime object including Function (delegate) that need to be exposed to Modules
        Delegate Sub OpenImageViewer(ByVal _Image As Image)

#End Region 'Delegates

#Region "Properties"

        Public Property ListMovies() As String
            Get
                Return If(_ListMovies IsNot Nothing, _ListMovies, "movielist")
            End Get
            Set(ByVal value As String)
                _ListMovies = value
            End Set
        End Property

        Public Property ListMovieSets() As String
            Get
                Return If(_ListMovieSets IsNot Nothing, _ListMovieSets, "setslist")
            End Get
            Set(ByVal value As String)
                _ListMovieSets = value
            End Set
        End Property

        Public Property ListTVShows() As String
            Get
                Return If(_ListTVShows IsNot Nothing, _ListTVShows, "tvshowlist")
            End Get
            Set(ByVal value As String)
                _ListTVShows = value
            End Set
        End Property

        Public Property FilterMovies() As String
            Get
                Return _FilterMovies
            End Get
            Set(ByVal value As String)
                _FilterMovies = value
            End Set
        End Property

        Public Property FilterMoviesSearch() As String
            Get
                Return _FilterMoviesSearch
            End Get
            Set(ByVal value As String)
                _FilterMoviesSearch = value
            End Set
        End Property

        Public Property FilterMoviesType() As String
            Get
                Return _FilterMoviesType
            End Get
            Set(ByVal value As String)
                _FilterMoviesType = value
            End Set
        End Property
        Public Property FilterTVShows() As String
            Get
                Return _FilterTVShows
            End Get
            Set(ByVal value As String)
                _FilterTVShows = value
            End Set
        End Property

        Public Property FilterTVShowsSearch() As String
            Get
                Return _FilterTVShowsSearch
            End Get
            Set(ByVal value As String)
                _FilterTVShowsSearch = value
            End Set
        End Property

        Public Property FilterTVShowsType() As String
            Get
                Return _FilterTVShowsType
            End Get
            Set(ByVal value As String)
                _FilterTVShowsType = value
            End Set
        End Property

        Public Property MediaTabSelected() As Structures.MainTabType
            Get
                Return _MediaTabSelected
            End Get
            Set(ByVal value As Structures.MainTabType)
                _MediaTabSelected = value
            End Set
        End Property

        Public Property MainToolStrip() As ToolStrip
            Get
                Return _MainToolStrip
            End Get
            Set(ByVal value As ToolStrip)
                _MainToolStrip = value
            End Set
        End Property

        Public Property MediaListMovies() As DataGridView
            Get
                Return _MediaListMovies
            End Get
            Set(ByVal value As DataGridView)
                _MediaListMovies = value
            End Set
        End Property

        Public Property MediaListMovieSets() As DataGridView
            Get
                Return _MediaListMovieSets
            End Get
            Set(ByVal value As DataGridView)
                _MediaListMovieSets = value
            End Set
        End Property

        Public Property MediaListTVEpisodes() As DataGridView
            Get
                Return _MediaListTVEpisodes
            End Get
            Set(ByVal value As DataGridView)
                _MediaListTVEpisodes = value
            End Set
        End Property

        Public Property MediaListTVSeasons() As DataGridView
            Get
                Return _MediaListTVSeasons
            End Get
            Set(ByVal value As DataGridView)
                _MediaListTVSeasons = value
            End Set
        End Property

        Public Property MediaListTVShows() As DataGridView
            Get
                Return _MediaListTVShows
            End Get
            Set(ByVal value As DataGridView)
                _MediaListTVShows = value
            End Set
        End Property

        Public Property ContextMenuMovieList() As ContextMenuStrip
            Get
                Return _ContextMenuMovieList
            End Get
            Set(ByVal value As ContextMenuStrip)
                _ContextMenuMovieList = value
            End Set
        End Property

        Public Property ContextMenuMovieSetList() As ContextMenuStrip
            Get
                Return _ContextMenuMovieSetList
            End Get
            Set(ByVal value As ContextMenuStrip)
                _ContextMenuMovieSetList = value
            End Set
        End Property

        Public Property ContextMenuTVEpisodeList() As ContextMenuStrip
            Get
                Return _ContextMenuTVEpisodeList
            End Get
            Set(ByVal value As ContextMenuStrip)
                _ContextMenuTVEpisodeList = value
            End Set
        End Property

        Public Property ContextMenuTVSeasonList() As ContextMenuStrip
            Get
                Return _ContextMenuTVSeasonList
            End Get
            Set(ByVal value As ContextMenuStrip)
                _ContextMenuTVSeasonList = value
            End Set
        End Property

        Public Property ContextMenuTVShowList() As ContextMenuStrip
            Get
                Return _ContextMenuTVShowList
            End Get
            Set(ByVal value As ContextMenuStrip)
                _ContextMenuTVShowList = value
            End Set
        End Property

        Public Property MainMenu() As MenuStrip
            Get
                Return _MainMenu
            End Get
            Set(ByVal value As MenuStrip)
                _MainMenu = value
            End Set
        End Property

        Public Property TrayMenu() As ContextMenuStrip
            Get
                Return _TrayMenu
            End Get
            Set(ByVal value As ContextMenuStrip)
                _TrayMenu = value
            End Set
        End Property

        Public Property MainTabControl() As TabControl
            Get
                Return _MainTabControl
            End Get
            Set(ByVal value As TabControl)
                _MainTabControl = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub DelegateLoadMedia(ByRef lm As LoadMedia)
            'Setup from EmberAPP
            _LoadMedia = lm
        End Sub

        Public Sub DelegateOpenImageViewer(ByRef IV As OpenImageViewer)
            _OpenImageViewer = IV
        End Sub

        Public Sub InvokeLoadMedia(ByVal Scan As Structures.ScanOrClean, Optional ByVal SourceID As Long = -1)
            'Invoked from Modules
            _LoadMedia.Invoke(Scan, SourceID)
        End Sub

        Public Sub InvokeOpenImageViewer(ByRef _image As Image)
            _OpenImageViewer.Invoke(_image)
        End Sub

#End Region 'Methods

    End Class

    Class _externalGenericModuleClass

#Region "Fields"

        Public AssemblyFileName As String

        'Public Enabled As Boolean
        Public AssemblyName As String
        Public ModuleOrder As Integer 'TODO: not important at this point.. for 1.5
        Public ProcessorModule As Interfaces.GenericModule 'Object
        Public Type As List(Of Enums.ModuleEventType)
        Public ContentType As Enums.ContentType = Enums.ContentType.Generic

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Data_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Data_Movie 'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Data_MovieSet

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Data_MovieSet 'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.MovieSet

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Data_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Data_TV 'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.TV

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Image_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Image_Movie  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Image_MovieSet

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Image_MovieSet  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.MovieSet

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Image_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Image_TV  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.TV

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Theme_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Theme_Movie     'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.Movie

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Theme_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Theme_TV  'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.TV

#End Region 'Fields

    End Class

    Class _externalScraperModuleClass_Trailer_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ProcessorModule As Interfaces.ScraperModule_Trailer_Movie     'Object
        Public ModuleOrder As Integer
        Public ContentType As Enums.ContentType = Enums.ContentType.Movie

#End Region 'Fields

    End Class

    <XmlRoot("EmberModule")> _
    Class _XMLEmberModuleClass

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public ContentType As Enums.ContentType
        Public GenericEnabled As Boolean
        Public PostScraperEnabled As Boolean    'only for TV
        Public PostScraperOrder As Integer      'only for TV
        Public ModuleEnabled As Boolean
        Public ModuleOrder As Integer

#End Region 'Fields

    End Class

#End Region 'Nested Types

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class