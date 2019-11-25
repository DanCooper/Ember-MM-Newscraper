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

Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports NLog

Public Class AddonsManager

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared AssemblyList As New List(Of AssemblyListItem)
    Public Shared VersionList As New List(Of VersionItem)

    Public GenericAddons As New List(Of GenericAddon)
    Public ScraperAddons_Data_Movie As New List(Of ScraperAddon_Data_Movie)
    Public ScraperAddons_Data_Movieset As New List(Of ScraperAddon_Data_Movieset)
    Public ScraperAddons_Data_TV As New List(Of ScraperAddon_Data_TV)
    Public ScraperAddons_Image_Movie As New List(Of ScraperAddon_Image_Movie)
    Public ScraperAddons_Image_Movieset As New List(Of ScraperAddon_Image_MovieSet)
    Public ScraperAddons_Image_TV As New List(Of ScraperAddon_Image_TV)
    Public ScraperAddons_Theme_Movie As New List(Of ScraperAddon_Theme_Movie)
    Public ScraperAddons_Theme_TV As New List(Of ScraperAddon_Theme_TV)
    Public ScraperAddons_Trailer_Movie As New List(Of ScraperAddon_Trailer_Movie)
    Public RuntimeObjects As New EmberRuntimeObjects

    'Singleton Instace for module manager .. allways use this one
    Private Shared Singleton As AddonsManager = Nothing

    Private _AddonsLocation As String = Path.Combine(Functions.AppPath, "Addons")

    Friend WithEvents bwLoadAddons As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

    Public Shared ReadOnly Property Instance() As AddonsManager
        Get
            If Singleton Is Nothing Then
                Singleton = New AddonsManager()
            End If
            Return Singleton
        End Get
    End Property

    Public ReadOnly Property AllAddonsLoaded() As Boolean
        Get
            Return Not bwLoadAddons.IsBusy
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
    Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Event ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)

#End Region 'Events

#Region "Methods"

    Private Sub BuildVersionList()
        VersionList.Clear()
        VersionList.Add(New VersionItem With {
                        .AssemblyFileName = "Ember Application",
                        .Version = My.Application.Info.Version.ToString()
                        })
        VersionList.Add(New VersionItem With {
                        .AssemblyFileName = "*EmberAPI",
                        .Version = Functions.EmberAPIVersion()
                        })
        For Each _externalScraperModule As ScraperAddon_Data_Movie In ScraperAddons_Data_Movie
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Data_Movieset In ScraperAddons_Data_Movieset
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Data_TV In ScraperAddons_Data_TV
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Image_Movie In ScraperAddons_Image_Movie
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Image_MovieSet In ScraperAddons_Image_Movieset
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Image_TV In ScraperAddons_Image_TV
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Theme_Movie In ScraperAddons_Theme_Movie
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalTVThemeScraperModule As ScraperAddon_Theme_TV In ScraperAddons_Theme_TV
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalTVThemeScraperModule.AssemblyFileName,
                            .Version = _externalTVThemeScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalScraperModule As ScraperAddon_Trailer_Movie In ScraperAddons_Trailer_Movie
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalScraperModule.AssemblyFileName,
                            .Version = _externalScraperModule.AssemblyVersion
                            })
        Next
        For Each _externalModule As GenericAddon In GenericAddons
            VersionList.Add(New VersionItem With {
                            .AssemblyFileName = _externalModule.AssemblyFileName,
                            .Version = _externalModule.AssemblyVersion
                            })
        Next
    End Sub

    Private Sub bwLoadAddons_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadAddons.DoWork
        LoadAddons()
    End Sub

    Private Sub bwLoadAddons_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadAddons.RunWorkerCompleted
        BuildVersionList()
    End Sub

    Public Function GetMovieCollectionID(ByVal IMDbID As String) As String
        Dim CollectionID As String = String.Empty

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(IMDbID) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externalScraperModuleClass_Data As ScraperAddon_Data_Movieset In ScraperAddons_Data_Movieset.Where(Function(e) e.AssemblyName = "addon.themoviedb.org") 'TODO: create a proper call via Enums
                ret = _externalScraperModuleClass_Data.ProcessorModule.GetCollectionID(IMDbID, CollectionID)
                If ret.breakChain Then Exit For
            Next
        End If
        Return CollectionID
    End Function

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement) As List(Of String)
        Dim ret As Interfaces.ModuleResult
        Dim sStudio As New List(Of String)
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Data_Movie In ScraperAddons_Data_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Try
                ret = _externalScraperModule.ProcessorModule.GetMovieStudio(DBMovie, sStudio)
            Catch ex As Exception
            End Try
            If ret.breakChain Then Exit For
        Next
        sStudio = sStudio.Distinct().ToList() 'remove double entries
        Return sStudio
    End Function

    Public Function GetMovieTMDbID(ByRef IMDbID As String) As String
        Dim TMDBID As String = String.Empty

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If Not String.IsNullOrEmpty(IMDbID) Then
            Dim ret As Interfaces.ModuleResult
            For Each _externalScraperModuleClass_Data As ScraperAddon_Data_Movie In ScraperAddons_Data_Movie.Where(Function(e) e.AssemblyName = "addon.themoviedb.org") 'TODO: create a proper call via Enums
                ret = _externalScraperModuleClass_Data.ProcessorModule.GetTMDbID(IMDbID, TMDBID)
                If ret.breakChain Then Exit For
            Next
        End If
        Return TMDBID
    End Function

    Public Sub GetVersions()
        Dim dlgVersions As New dlgVersions
        Dim li As ListViewItem
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each v As VersionItem In VersionList
            li = dlgVersions.lstVersions.Items.Add(v.AssemblyFileName)
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

    Public Sub LoadAllAddons()
        bwLoadAddons.RunWorkerAsync()
    End Sub

    Public Sub LoadAddons()
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

        _Logger.Trace("[AddonsManager] [LoadAddons] [Start]")

        If Directory.Exists(_AddonsLocation) Then
            'add each .dll file to AssemblyList
            For Each inDir In Directory.GetDirectories(_AddonsLocation)
                For Each inFile As String In Directory.GetFiles(inDir, "*.dll")
                    Dim nAssembly As Reflection.Assembly = Reflection.Assembly.LoadFile(inFile)
                    AssemblyList.Add(New AssemblyListItem With {
                                     .Assembly = nAssembly,
                                     .AssemblyName = nAssembly.GetName.Name,
                                     .AssemblyVersion = nAssembly.GetName().Version
                                     })
                Next
            Next

            For Each tAssemblyItem As AssemblyListItem In AssemblyList
                'Loop through each of the assemeblies type
                For Each fileType As Type In tAssemblyItem.Assembly.GetTypes

                    Dim fType As Type = fileType.GetInterface("IGenericAddon")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IGenericAddon 'Object
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IGenericAddon)

                        Dim GenericModule As New GenericAddon With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule,
                            .EventType = ProcessorModule.Type
                        }
                        GenericAddons.Add(GenericModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", GenericModule.AssemblyName, "_", GenericModule.InterfaceType))

                        GenericModule.ProcessorModule.Init()

                        Dim bFound As Boolean = False
                        For Each i In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = GenericModule.AssemblyName AndAlso
                                                                        f.InterfaceType = Interfaces.InterfaceType.Generic)
                            GenericModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            bFound = True
                        Next

                        'Enable all Core Modules by default if no setting was found
                        If Not bFound AndAlso GenericModule.AssemblyFileName.Contains("generic.EmberCore") Then
                            GenericModule.ProcessorModule.IsEnabled = True
                        End If
                        AddHandler ProcessorModule.GenericEvent, AddressOf GenericRunCallBack
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Data_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Data_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Data_Movie)

                        Dim ScraperModule As New ScraperAddon_Data_Movie With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Data_Movie.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Data_Movie)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            DataScraperAnyEnabled_Movie = DataScraperAnyEnabled_Movie OrElse i.AddonEnabled
                            DataScraperFound_Movie = True
                        Next
                        If Not DataScraperFound_Movie Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Image_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Image_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Image_Movie)

                        Dim ScraperModule As New ScraperAddon_Image_Movie With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Image_Movie.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Image_Movie)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            ImageScraperAnyEnabled_Movie = ImageScraperAnyEnabled_Movie OrElse i.AddonEnabled
                            ImageScraperFound_Movie = True
                        Next
                        If Not ImageScraperFound_Movie Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Trailer_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Trailer_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Trailer_Movie)

                        Dim ScraperModule As New ScraperAddon_Trailer_Movie With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Trailer_Movie.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Trailer_Movie)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            TrailerScraperAnyEnabled_Movie = TrailerScraperAnyEnabled_Movie OrElse i.AddonEnabled
                            TrailerScraperFound_Movie = True
                        Next
                        If Not TrailerScraperFound_Movie Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Theme_Movie")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Theme_Movie
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Theme_Movie)

                        Dim ScraperModule As New ScraperAddon_Theme_Movie With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Theme_Movie.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Theme_Movie)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            ThemeScraperAnyEnabled_Movie = ThemeScraperAnyEnabled_Movie OrElse i.AddonEnabled
                            ThemeScraperFound_Movie = True
                        Next
                        If Not ThemeScraperFound_Movie Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Data_MovieSet")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Data_MovieSet
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Data_MovieSet)

                        Dim ScraperModule As New ScraperAddon_Data_Movieset With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Data_Movieset.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Data_Movieset)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            DataScraperAnyEnabled_MovieSet = DataScraperAnyEnabled_MovieSet OrElse i.AddonEnabled
                            DataScraperFound_MovieSet = True
                        Next
                        If Not DataScraperFound_MovieSet Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Image_Movieset")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Image_Movieset
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Image_Movieset)

                        Dim ScraperModule As New ScraperAddon_Image_MovieSet With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Image_Movieset.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Image_Movieset)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            ImageScraperAnyEnabled_MovieSet = ImageScraperAnyEnabled_MovieSet OrElse i.AddonEnabled
                            ImageScraperFound_MovieSet = True
                        Next
                        If Not ImageScraperFound_MovieSet Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Data_TV")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Data_TV
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Data_TV)

                        Dim ScraperModule As New ScraperAddon_Data_TV With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Data_TV.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Data_TV)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            DataScraperAnyEnabled_TV = DataScraperAnyEnabled_TV OrElse i.AddonEnabled
                            DataScraperFound_TV = True
                        Next
                        If Not DataScraperFound_TV Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Image_TV")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Image_TV
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Image_TV)

                        Dim ScraperModule As New ScraperAddon_Image_TV With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Image_TV.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Image_TV)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            ImageScraperAnyEnabled_TV = ImageScraperAnyEnabled_TV OrElse i.AddonEnabled
                            ImageScraperFound_TV = True
                        Next
                        If Not ImageScraperFound_TV Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If

                    fType = fileType.GetInterface("IScraperAddon_Theme_TV")
                    If Not fType Is Nothing Then
                        Dim ProcessorModule As Interfaces.IScraperAddon_Theme_TV
                        ProcessorModule = CType(Activator.CreateInstance(fileType), Interfaces.IScraperAddon_Theme_TV)

                        Dim ScraperModule As New ScraperAddon_Theme_TV With {
                            .AssemblyFileName = tAssemblyItem.Assembly.ManifestModule.Name,
                            .AssemblyName = tAssemblyItem.AssemblyName,
                            .AssemblyVersion = tAssemblyItem.AssemblyVersion.ToString,
                            .ProcessorModule = ProcessorModule
                        }
                        ScraperAddons_Theme_TV.Add(ScraperModule)

                        _Logger.Trace(String.Concat("[AddonsManager] [LoadAddons] Addon loaded: ", ScraperModule.AssemblyName, "_", ScraperModule.InterfaceType))

                        ScraperModule.ProcessorModule.Init()

                        For Each i As XMLAddonClass In Master.eSettings.Addons.Where(Function(f) f.AssemblyName = ScraperModule.AssemblyName AndAlso
                                                                                         f.InterfaceType = Interfaces.InterfaceType.Theme_TV)
                            ScraperModule.ProcessorModule.IsEnabled = i.AddonEnabled
                            ScraperModule.ProcessorModule.Order = i.AddonOrder
                            ThemeScraperAnyEnabled_TV = ThemeScraperAnyEnabled_TV OrElse i.AddonEnabled
                            ThemeScraperFound_TV = True
                        Next
                        If Not ThemeScraperFound_TV Then
                            ScraperModule.ProcessorModule.Order = 999
                        End If
                    End If
                Next
            Next

            'Order addons
            Dim c As Integer = 0
            For Each ext As GenericAddon In GenericAddons.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Data_Movie In ScraperAddons_Data_Movie.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Image_Movie In ScraperAddons_Image_Movie.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Theme_Movie In ScraperAddons_Theme_Movie.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Trailer_Movie In ScraperAddons_Trailer_Movie.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Data_Movieset In ScraperAddons_Data_Movieset.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Image_MovieSet In ScraperAddons_Image_Movieset.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Data_TV In ScraperAddons_Data_TV.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Image_TV In ScraperAddons_Image_TV.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next
            c = 0
            For Each ext As ScraperAddon_Theme_TV In ScraperAddons_Theme_TV.OrderBy(Function(f) f.ProcessorModule.Order)
                ext.ProcessorModule.Order = c
                c += 1
            Next

            'Enable default addons
            If Not DataScraperAnyEnabled_Movie AndAlso Not DataScraperFound_Movie Then
                SetScraperEnable_Data_Movie("addon.themoviedb.org", True)
            End If
            If Not ImageScraperAnyEnabled_Movie AndAlso Not ImageScraperFound_Movie Then
                SetScraperEnable_Image_Movie("addon.fanart.tv", True)
                SetScraperEnable_Image_Movie("addon.themoviedb.org", True)
            End If
            If Not ThemeScraperAnyEnabled_Movie AndAlso Not ThemeScraperFound_Movie Then
                SetScraperEnable_Theme_Movie("addon.televisiontunes.com", True)
            End If
            If Not TrailerScraperAnyEnabled_Movie AndAlso Not TrailerScraperFound_Movie Then
                SetScraperEnable_Trailer_Movie("addon.themoviedb.org", True)
            End If
            If Not DataScraperAnyEnabled_MovieSet AndAlso Not DataScraperFound_MovieSet Then
                SetScraperEnable_Data_MovieSet("addon.themoviedb.org", True)
            End If
            If Not ImageScraperAnyEnabled_MovieSet AndAlso Not ImageScraperFound_MovieSet Then
                SetScraperEnable_Image_MovieSet("addon.fanart.tv", True)
                SetScraperEnable_Image_MovieSet("addon.themoviedb.org", True)
            End If
            If Not DataScraperAnyEnabled_TV AndAlso Not DataScraperFound_TV Then
                SetScraperEnable_Data_TV("addon.thetvdb.com", True)
            End If
            If Not ImageScraperAnyEnabled_TV AndAlso Not ImageScraperFound_TV Then
                SetScraperEnable_Image_TV("addon.fanart.tv", True)
                SetScraperEnable_Image_TV("addon.themoviedb.org", True)
                SetScraperEnable_Image_TV("addon.thetvdb.com", True)
            End If
            If Not ThemeScraperAnyEnabled_TV AndAlso Not ThemeScraperFound_TV Then
                SetScraperEnable_Theme_TV("addon.televisiontunes.com", True)
            End If
        End If

        _Logger.Trace("[AddonsManager] [LoadAddons] [Done]")
    End Sub

    Function QueryAnyGenericIsBusy() As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Dim modules As IEnumerable(Of GenericAddon) = GenericAddons.Where(Function(e) e.ProcessorModule.IsBusy)
        If modules.Count() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Function QueryScraperCapabilities_Image_Movie(ByVal externalScraperModule As ScraperAddon_Image_Movie, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
        If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
        If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
        If ScrapeModifiers.MainDiscArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainDiscArt) Then Return True
        If ScrapeModifiers.MainExtrafanarts AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainExtrathumbs AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainKeyArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True
        If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
        If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_Movie(ByVal externalScraperModule As ScraperAddon_Image_Movie, ByVal ImageType As Enums.ModifierType) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Select Case ImageType
            Case Enums.ModifierType.MainExtrafanarts
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
            Case Enums.ModifierType.MainExtrathumbs
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart)
            Case Enums.ModifierType.MainKeyArt
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster)
            Case Else
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)
        End Select

        Return False
    End Function

    Function QueryScraperCapabilities_Image_MovieSet(ByVal externalScraperModule As ScraperAddon_Image_MovieSet, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not AllAddonsLoaded
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

    Function QueryScraperCapabilities_Image_MovieSet(ByVal externalScraperModule As ScraperAddon_Image_MovieSet, ByVal ImageType As Enums.ModifierType) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Select Case ImageType
            Case Enums.ModifierType.MainKeyArt
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster)
            Case Else
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(ImageType)
        End Select

        Return False
    End Function

    Function QueryScraperCapabilities_Image_TV(ByVal externalScraperModule As ScraperAddon_Image_TV, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Boolean
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If ScrapeModifiers.EpisodeFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodeFanart) Then Return True
        If ScrapeModifiers.EpisodePoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.EpisodePoster) Then Return True
        If ScrapeModifiers.MainBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainBanner) Then Return True
        If ScrapeModifiers.MainCharacterArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainCharacterArt) Then Return True
        If ScrapeModifiers.MainClearArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearArt) Then Return True
        If ScrapeModifiers.MainClearLogo AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainClearLogo) Then Return True
        If ScrapeModifiers.MainFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainFanart) Then Return True
        If ScrapeModifiers.MainKeyArt AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True
        If ScrapeModifiers.MainLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainLandscape) Then Return True
        If ScrapeModifiers.MainPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster) Then Return True
        If ScrapeModifiers.SeasonBanner AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonBanner) Then Return True
        If ScrapeModifiers.SeasonFanart AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonFanart) Then Return True
        If ScrapeModifiers.SeasonLandscape AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonLandscape) Then Return True
        If ScrapeModifiers.SeasonPoster AndAlso externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.SeasonPoster) Then Return True

        Return False
    End Function

    Function QueryScraperCapabilities_Image_TV(ByVal externalScraperModule As ScraperAddon_Image_TV, ByVal ImageType As Enums.ModifierType) As Boolean
        While Not AllAddonsLoaded
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
            Case Enums.ModifierType.MainKeyArt
                Return externalScraperModule.ProcessorModule.QueryScraperCapabilities(Enums.ModifierType.MainPoster)
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
        _Logger.Trace(String.Format("[AddonsManager] [RunGeneric] [Start] <{0}>", mType.ToString))
        Dim ret As Interfaces.ModuleResult

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        Try
            Dim modules As IEnumerable(Of GenericAddon) = GenericAddons.Where(Function(e) e.ProcessorModule.Type.Contains(mType) AndAlso e.ProcessorModule.IsEnabled)
            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [RunGeneric] No generic modules defined <{0}>", mType.ToString)
            Else
                For Each _externalGenericModule As GenericAddon In modules
                    Try
                        _Logger.Trace("[AddonsManager] [RunGeneric] Run generic module <{0}>", _externalGenericModule.AssemblyName)
                        ret = _externalGenericModule.ProcessorModule.Run(mType, _params, _singleobjekt, DBElement)
                    Catch ex As Exception
                        _Logger.Error("[AddonsManager] [RunGeneric] Run generic module <{0}>", _externalGenericModule.AssemblyName)
                        _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                    If ret.breakChain OrElse RunOnlyOne Then Exit For
                Next
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return ret.Cancelled
    End Function

    ''' <summary>
    ''' Request that enabled movie scrapers perform their functions on the supplied movie
    ''' </summary>
    ''' <param name="DBElement">Movie to be scraped</param>
    ''' <param name="ScrapeType">What kind of scrape is being requested, such as whether user-validation is desired</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape</param>
    ''' <returns><c>True</c> if one of the scrapers was cancelled</returns>
    ''' <remarks>Note that if no movie scrapers are enabled, a silent warning is generated.</remarks>
    Public Function ScrapeData_Movie(ByRef DBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Start] {0}", DBElement.FileItem.FirstPathFromStack))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of ScraperAddon_Data_Movie) = ScraperAddons_Data_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Dim ret As Interfaces.ModuleResult_Data_Movie
            Dim ScrapedList As New List(Of MediaContainers.Movie)

            While Not AllAddonsLoaded
                Application.DoEvents()
            End While

            'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
            If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso ScrapeModifiers.DoSearch Then
                DBElement.ImagesContainer = New MediaContainers.ImagesContainer
                DBElement.Movie = New MediaContainers.Movie With {
                    .Title = StringUtils.FilterTitleFromPath_Movie(DBElement.FileItem, DBElement.IsSingle, DBElement.Source.UseFolderName),
                    .VideoSource = DBElement.VideoSource,
                    .Year = StringUtils.FilterYearFromPath_Movie(DBElement.FileItem, DBElement.IsSingle, DBElement.Source.UseFolderName)
                }
            End If

            'create a clone of DBMovie
            Dim oDBMovie As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [ScrapeData_Movie] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As ScraperAddon_Data_Movie In modules
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Using] {0}", _externalScraperModule.AssemblyName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie

                    ret = _externalScraperModule.ProcessorModule.Run(oDBMovie, ScrapeModifiers, ScrapeType, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers 
                        If ret.Result.OriginalTitleSpecified Then
                            oDBMovie.Movie.OriginalTitle = ret.Result.OriginalTitle
                        End If
                        If ret.Result.TitleSpecified Then
                            oDBMovie.Movie.Title = ret.Result.Title
                        End If
                        If ret.Result.UniqueIDsSpecified Then
                            oDBMovie.Movie.UniqueIDs.AddRange(ret.Result.UniqueIDs)
                        End If
                        If ret.Result.YearSpecified Then
                            oDBMovie.Movie.Year = ret.Result.Year
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = Info.MergeDataScraperResults_Movie(DBElement, ScrapedList, ScrapeType, ScrapeOptions)

                'create cache paths for Actor Thumbs
                DBElement.Movie.CreateCachePaths_ActorsThumbs()
            End If

            If ScrapedList.Count > 0 Then
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Done] {0}", DBElement.FileItem.FirstPathFromStack))
            Else
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Done] [No Scraper Results] {0}", DBElement.FileItem.FirstPathFromStack))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_Movie] [Abort] [Offline] {0}", DBElement.FileItem.FirstPathFromStack))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Start] {0}", DBElement.Movieset.Title))
        'If DBMovieSet.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_MovieSet(DBMovieSet, showMessage) Then
        Dim modules As IEnumerable(Of ScraperAddon_Data_Movieset) = ScraperAddons_Data_Movieset.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        Dim ret As Interfaces.ModuleResult_Data_MovieSet
        Dim ScrapedList As New List(Of MediaContainers.MovieSet)

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        'clean DBMovie if the movie is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
        If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso ScrapeModifiers.DoSearch Then
            Dim tmpTitle As String = DBElement.Movieset.Title

            DBElement.ImagesContainer = New MediaContainers.ImagesContainer
            DBElement.Movieset = New MediaContainers.MovieSet With {
                .Title = tmpTitle
            }
        End If

        'create a clone of DBMovieSet
        Dim oDBMovieSet As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

        If modules.Count() <= 0 Then
            _Logger.Info("[AddonsManager] [ScrapeData_MovieSet] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As ScraperAddon_Data_Movieset In modules
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Using] {0}", _externalScraperModule.AssemblyName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet

                ret = _externalScraperModule.ProcessorModule.Run(oDBMovieSet, ScrapeModifiers, ScrapeType, ScrapeOptions)

                If ret.Cancelled Then
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Cancelled] [No Scraper Results] {0}", DBElement.Movieset.Title))
                    Return ret.Cancelled
                End If

                If ret.Result IsNot Nothing Then
                    ScrapedList.Add(ret.Result)

                    'set new informations for following scrapers
                    If ret.Result.TitleSpecified Then
                        oDBMovieSet.Movieset.Title = ret.Result.Title
                    End If
                    If ret.Result.UniqueIDsSpecified Then
                        oDBMovieSet.Movieset.UniqueIDs.AddRange(ret.Result.UniqueIDs)
                    End If
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
                If ret.breakChain Then Exit For
            Next

            'Merge scraperresults considering global datascraper settings
            DBElement = Info.MergeDataScraperResults_MovieSet(DBElement, ScrapedList, ScrapeType, ScrapeOptions)
        End If

        If ScrapedList.Count > 0 Then
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Done] {0}", DBElement.Movieset.Title))
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_MovieSet] [Done] [No Scraper Results] {0}", DBElement.Movieset.Title))
            Return True 'TODO: need a new trigger
        End If
        Return ret.Cancelled
        'Else
        'Return True 'Cancelled
        'End If
    End Function

    Public Function ScrapeData_TVEpisode(ByRef DBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Start] {0}", DBElement.FileItem.FirstPathFromStack))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of ScraperAddon_Data_TV) = ScraperAddons_Data_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Dim ret As Interfaces.ModuleResult_Data_TVEpisode
            Dim ScrapedList As New List(Of MediaContainers.EpisodeDetails)

            While Not AllAddonsLoaded
                Application.DoEvents()
            End While

            'create a clone of DBTV
            Dim oEpisode As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [ScrapeData_TVEpisode] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As ScraperAddon_Data_TV In modules
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Using] {0}", _externalScraperModule.AssemblyName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV

                    ret = _externalScraperModule.ProcessorModule.Run_TVEpisode(oEpisode, ScrapeOptions)

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
                        If ret.Result.SeasonSpecified Then
                            oEpisode.TVEpisode.Season = ret.Result.Season
                        End If
                        If ret.Result.TitleSpecified Then
                            oEpisode.TVEpisode.Title = ret.Result.Title
                        End If
                        If ret.Result.UniqueIDsSpecified Then
                            oEpisode.TVEpisode.UniqueIDs.AddRange(ret.Result.UniqueIDs)
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = Info.MergeDataScraperResults_TVEpisode_Single(DBElement, ScrapedList, ScrapeOptions)

                'create cache paths for Actor Thumbs
                DBElement.TVEpisode.CreateCachePaths_ActorsThumbs()
            End If

            If ScrapedList.Count > 0 Then
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Done] {0}", DBElement.FileItem.FirstPathFromStack))
            Else
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Done] [No Scraper Results] {0}", DBElement.FileItem.FirstPathFromStack))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVEpisode] [Abort] [Offline] {0}", DBElement.FileItem.FirstPathFromStack))
            Return True 'Cancelled
        End If
    End Function

    Public Function ScrapeData_TVSeason(ByRef DBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal showMessage As Boolean) As Boolean
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Start] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of ScraperAddon_Data_TV) = ScraperAddons_Data_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Dim ret As Interfaces.ModuleResult_Data_TVSeason
            Dim ScrapedList As New List(Of MediaContainers.SeasonDetails)

            While Not AllAddonsLoaded
                Application.DoEvents()
            End While

            'create a clone of DBTV
            Dim oSeason As Database.DBElement = CType(DBElement.CloneDeep, Database.DBElement)

            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [ScrapeData_TVSeason] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As ScraperAddon_Data_TV In modules
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Using] {0}", _externalScraperModule.AssemblyName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV

                    ret = _externalScraperModule.ProcessorModule.Run_TVSeason(oSeason, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers 
                        If ret.Result.UniqueIDsSpecified Then
                            oSeason.TVSeason.UniqueIDs.AddRange(ret.Result.UniqueIDs)
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = Info.MergeDataScraperResults_TVSeason(DBElement, ScrapedList, ScrapeOptions)
            End If

            If ScrapedList.Count > 0 Then
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Done] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
            Else
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Done] [No Scraper Results] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
            End If
            Return ret.Cancelled
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVSeason] [Abort] [Offline] {0}: Season {1}", DBElement.TVShow.Title, DBElement.TVSeason.Season))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Start] {0}", DBElement.TVShow.Title))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of ScraperAddon_Data_TV) = ScraperAddons_Data_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Dim ret As Interfaces.ModuleResult_Data_TVShow
            Dim ScrapedList As New List(Of MediaContainers.TVShow)

            While Not AllAddonsLoaded
                Application.DoEvents()
            End While

            'clean DBTV if the tv show is to be changed. For this, all existing (incorrect) information must be deleted and the images triggers set to remove.
            If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto) AndAlso ScrapeModifiers.DoSearch Then
                DBElement.ExtrafanartsPath = String.Empty
                DBElement.ImagesContainer = New MediaContainers.ImagesContainer
                DBElement.NfoPath = String.Empty
                DBElement.Seasons.Clear()
                DBElement.Theme = New MediaContainers.Theme
                DBElement.TVShow = New MediaContainers.TVShow With {
                    .Title = StringUtils.FilterTitleFromPath_TVShow(DBElement.ShowPath)
                }

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

            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [ScrapeData_TVShow] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As ScraperAddon_Data_TV In modules
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Using] {0}", _externalScraperModule.AssemblyName))
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV

                    ret = _externalScraperModule.ProcessorModule.Run_TVShow(oShow, ScrapeModifiers, ScrapeType, ScrapeOptions)

                    If ret.Cancelled Then Return ret.Cancelled

                    If ret.Result IsNot Nothing Then
                        ScrapedList.Add(ret.Result)

                        'set new informations for following scrapers 
                        If ret.Result.OriginalTitleSpecified Then
                            oShow.TVShow.OriginalTitle = ret.Result.OriginalTitle
                        End If
                        If ret.Result.TitleSpecified Then
                            oShow.TVShow.Title = ret.Result.Title
                        End If
                        If ret.Result.UniqueIDsSpecified Then
                            oShow.TVShow.UniqueIDs.AddRange(ret.Result.UniqueIDs)
                        End If
                    End If
                    RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                    If ret.breakChain Then Exit For
                Next

                'Merge scraperresults considering global datascraper settings
                DBElement = Info.MergeDataScraperResults_TV(DBElement, ScrapedList, ScrapeType, ScrapeOptions, ScrapeModifiers.withEpisodes)

                'create cache paths for Actor Thumbs
                DBElement.TVShow.CreateCachePaths_ActorsThumbs()
                If ScrapeModifiers.withEpisodes Then
                    For Each tEpisode As Database.DBElement In DBElement.Episodes
                        tEpisode.TVEpisode.CreateCachePaths_ActorsThumbs()
                    Next
                End If
            End If

            If ScrapedList.Count > 0 Then
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Done] {0}", DBElement.TVShow.Title))
            Else
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Done] [No Scraper Results] {0}", DBElement.TVShow.Title))
                Return True 'TODO: need a new trigger
            End If
            Return ret.Cancelled
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeData_TVShow] [Abort] [Offline] {0}", DBElement.TVShow.Title))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Start] {0}", DBElement.FileItem.FirstPathFromStack))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of ScraperAddon_Image_Movie) = ScraperAddons_Image_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Dim ret As Interfaces.ModuleResult

            While Not AllAddonsLoaded
                Application.DoEvents()
            End While

            'workaround to get MainFanarts for Extranfanarts and Extrathumbs,
            'also get MainPosters for MainKeyArts
            If ScrapeModifiers.MainExtrafanarts OrElse ScrapeModifiers.MainExtrathumbs Then
                ScrapeModifiers.MainFanart = True
            End If
            If ScrapeModifiers.MainKeyArt Then
                ScrapeModifiers.MainPoster = True
            End If

            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [ScrapeImage_Movie] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As ScraperAddon_Image_Movie In modules
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Using] {0}", _externalScraperModule.AssemblyName))
                    If QueryScraperCapabilities_Image_Movie(_externalScraperModule, ScrapeModifiers) Then
                        AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        ret = _externalScraperModule.ProcessorModule.Run(DBElement, aContainer, ScrapeModifiers)
                        If aContainer IsNot Nothing Then
                            ImagesContainer.MainBanners.AddRange(aContainer.MainBanners)
                            ImagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
                            ImagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
                            ImagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
                            ImagesContainer.MainDiscArts.AddRange(aContainer.MainDiscArts)
                            ImagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
                            ImagesContainer.MainKeyArts.AddRange(aContainer.MainPosters)
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

            _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Done] {0}", DBElement.FileItem.FirstPathFromStack))
            Return ret.Cancelled
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_Movie] [Abort] [Offline] {0}", DBElement.FileItem.FirstPathFromStack))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_MovieSet] [Start] {0}", DBElement.Movieset.Title))
        Dim modules As IEnumerable(Of ScraperAddon_Image_MovieSet) = ScraperAddons_Image_Movieset.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        Dim ret As Interfaces.ModuleResult

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        'workaround to get MainPosters for MainKeyArts
        If ScrapeModifiers.MainKeyArt Then
            ScrapeModifiers.MainPoster = True
        End If

        If modules.Count() <= 0 Then
            _Logger.Info("[AddonsManager] [ScrapeImage_MovieSet] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As ScraperAddon_Image_MovieSet In modules
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_MovieSet] [Using] {0}", _externalScraperModule.AssemblyName))
                If QueryScraperCapabilities_Image_MovieSet(_externalScraperModule, ScrapeModifiers) Then
                    AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_MovieSet
                    Dim aContainer As New MediaContainers.SearchResultsContainer
                    ret = _externalScraperModule.ProcessorModule.Run(DBElement, aContainer, ScrapeModifiers)
                    If aContainer IsNot Nothing Then
                        ImagesContainer.MainBanners.AddRange(aContainer.MainBanners)
                        ImagesContainer.MainCharacterArts.AddRange(aContainer.MainCharacterArts)
                        ImagesContainer.MainClearArts.AddRange(aContainer.MainClearArts)
                        ImagesContainer.MainClearLogos.AddRange(aContainer.MainClearLogos)
                        ImagesContainer.MainDiscArts.AddRange(aContainer.MainDiscArts)
                        ImagesContainer.MainFanarts.AddRange(aContainer.MainFanarts)
                        ImagesContainer.MainKeyArts.AddRange(aContainer.MainPosters)
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

        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_MovieSet] [Done] {0}", DBElement.Movieset.Title))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Start] {0}", DBElement.TVShow.Title))
        If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, showMessage) Then
            Dim modules As IEnumerable(Of ScraperAddon_Image_TV) = ScraperAddons_Image_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Dim ret As Interfaces.ModuleResult

            While Not AllAddonsLoaded
                Application.DoEvents()
            End While

            'workaround to get MainFanarts for AllSeasonsFanarts, EpisodeFanarts and SeasonFanarts,
            'also get MainBanners, MainLandscapes and MainPosters for AllSeasonsBanners, AllSeasonsLandscapes and AllSeasonsPosters
            'and MainPosters for MainKeyArts
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
            If ScrapeModifiers.MainKeyArt Then
                ScrapeModifiers.MainPoster = True
            End If

            If modules.Count() <= 0 Then
                _Logger.Info("[AddonsManager] [ScrapeImage_TV] [Abort] No scrapers enabled")
            Else
                For Each _externalScraperModule As ScraperAddon_Image_TV In modules
                    _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Using] {0}", _externalScraperModule.AssemblyName))
                    If QueryScraperCapabilities_Image_TV(_externalScraperModule, ScrapeModifiers) Then
                        AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        ret = _externalScraperModule.ProcessorModule.Run(DBElement, aContainer, ScrapeModifiers)
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
                            ImagesContainer.MainKeyArts.AddRange(aContainer.MainPosters)
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

            _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Done] {0}", DBElement.TVShow.Title))
            Return ret.Cancelled
        Else
            _Logger.Trace(String.Format("[AddonsManager] [ScrapeImage_TV] [Abort] [Offline] {0}", DBElement.TVShow.Title))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_Movie] [Start] {0}", DBElement.FileItem.FirstPathFromStack))
        Dim modules As IEnumerable(Of ScraperAddon_Theme_Movie) = ScraperAddons_Theme_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        Dim ret As Interfaces.ModuleResult

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If modules.Count() <= 0 Then
            _Logger.Info("[AddonsManager] [ScrapeTheme_Movie] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As ScraperAddon_Theme_Movie In modules
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_Movie] [Using] {0}", _externalScraperModule.AssemblyName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                Dim aList As New List(Of MediaContainers.Theme)
                ret = _externalScraperModule.ProcessorModule.Run(DBElement, Type, aList)
                If aList IsNot Nothing Then
                    ThemeList.AddRange(aList)
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                If ret.breakChain Then Exit For
            Next
        End If
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_Movie] [Done] {0}", DBElement.FileItem.FirstPathFromStack))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_TVShow] [Start] {0}", DBElement.TVShow.Title))
        Dim modules As IEnumerable(Of ScraperAddon_Theme_TV) = ScraperAddons_Theme_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        Dim ret As Interfaces.ModuleResult

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If modules.Count() <= 0 Then
            _Logger.Info("[AddonsManager] [ScrapeTheme_TVShow] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As ScraperAddon_Theme_TV In modules
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_TVShow] [Using] {0}", _externalScraperModule.AssemblyName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                Dim aList As New List(Of MediaContainers.Theme)
                ret = _externalScraperModule.ProcessorModule.Run(DBElement, Type, aList)
                If aList IsNot Nothing Then
                    ThemeList.AddRange(aList)
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_TV
                If ret.breakChain Then Exit For
            Next
        End If
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeTheme_TVShow] [Done] {0}", DBElement.TVShow.Title))
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
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeTrailer_Movie] [Start] {0}", DBElement.FileItem.FirstPathFromStack))
        Dim modules As IEnumerable(Of ScraperAddon_Trailer_Movie) = ScraperAddons_Trailer_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
        Dim ret As Interfaces.ModuleResult

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        If modules.Count() <= 0 Then
            _Logger.Info("[AddonsManager] [ScrapeTrailer_Movie] [Abort] No scrapers enabled")
        Else
            For Each _externalScraperModule As ScraperAddon_Trailer_Movie In modules
                _Logger.Trace(String.Format("[AddonsManager] [ScrapeTrailer_Movie] [Using] {0}", _externalScraperModule.AssemblyName))
                AddHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                Dim aList As New List(Of MediaContainers.Trailer)
                ret = _externalScraperModule.ProcessorModule.Run(DBElement, Type, aList)
                If aList IsNot Nothing Then
                    TrailerList.AddRange(aList)
                End If
                RemoveHandler _externalScraperModule.ProcessorModule.ScraperEvent, AddressOf Handler_ScraperEvent_Movie
                If ret.breakChain Then Exit For
            Next
        End If
        _Logger.Trace(String.Format("[AddonsManager] [ScrapeTrailer_Movie] [Done] {0}", DBElement.FileItem.FirstPathFromStack))
        Return ret.Cancelled
    End Function

    Function ScraperWithCapabilityAnyEnabled_Image_Movie(ByVal ImageType As Enums.ModifierType) As Boolean
        Dim ret As Boolean = False
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Image_Movie In ScraperAddons_Image_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
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
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Image_MovieSet In ScraperAddons_Image_Movieset.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
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
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Image_TV In ScraperAddons_Image_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
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
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Theme_Movie In ScraperAddons_Theme_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
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
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Theme_TV In ScraperAddons_Theme_TV.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
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
        While Not AllAddonsLoaded
            Application.DoEvents()
        End While
        For Each _externalScraperModule As ScraperAddon_Trailer_Movie In ScraperAddons_Trailer_Movie.Where(Function(e) e.ProcessorModule.IsEnabled).OrderBy(Function(e) e.ProcessorModule.Order)
            Try
                ret = True 'if a trailer scraper is enabled we can exit.
                Exit For
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function

    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="AssemblyName"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="AssemblyName"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>
    Public Sub SetModuleEnable_Generic(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetModuleEnable_Generic] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of GenericAddon) = GenericAddons.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetModuleEnable_Generic] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalProcessorModule As GenericAddon In modules
                Try
                    _externalProcessorModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_Movie(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Data_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Data_Movie) = ScraperAddons_Data_Movie.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Data_Movie]  modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Data_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_MovieSet(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Data_MovieSet] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Data_Movieset) = ScraperAddons_Data_Movieset.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Data_MovieSet] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Data_Movieset In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Data_TV(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Data_TV] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Data_TV) = ScraperAddons_Data_TV.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Data_TV] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Data_TV In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_Movie(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Image_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Image_Movie) = ScraperAddons_Image_Movie.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Image_Movie] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Image_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_MovieSet(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Image_MovieSet] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Image_MovieSet) = ScraperAddons_Image_Movieset.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Image_MovieSet] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Image_MovieSet In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Image_TV(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Image_TV] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Image_TV) = ScraperAddons_Image_TV.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Image_TV] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Image_TV In ScraperAddons_Image_TV.Where(Function(p) p.AssemblyName = AssemblyName)
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            Next
        End If
    End Sub

    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="AssemblyName"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="AssemblyName"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>

    Public Sub SetScraperEnable_Theme_Movie(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Theme_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Theme_Movie) = ScraperAddons_Theme_Movie.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Theme_Movie] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Theme_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub SetScraperEnable_Theme_TV(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Theme_TV] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Theme_TV) = ScraperAddons_Theme_TV.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Theme_TV] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Theme_TV In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub
    ''' <summary>
    ''' Sets the enabled flag of the module identified by <paramref name="AssemblyName"/> to the value of <paramref name="value"/>
    ''' </summary>
    ''' <param name="AssemblyName"><c>String</c> representing the assembly name of the module</param>
    ''' <param name="value"><c>Boolean</c> value to set the enabled flag to</param>
    ''' <remarks></remarks>

    Public Sub SetScraperEnable_Trailer_Movie(ByVal AssemblyName As String, ByVal value As Boolean)
        If String.IsNullOrEmpty(AssemblyName) Then
            _Logger.Error("[AddonsManager] [SetScraperEnable_Trailer_Movie] Invalid ModuleAssembly")
            Return
        End If

        Dim modules As IEnumerable(Of ScraperAddon_Trailer_Movie) = ScraperAddons_Trailer_Movie.Where(Function(p) p.AssemblyName = AssemblyName)
        If modules.Count < 0 Then
            _Logger.Info("[AddonsManager] [SetScraperEnable_Trailer_Movie] No modules of type <{0}> were found", AssemblyName)
        Else
            For Each _externalScraperModule As ScraperAddon_Trailer_Movie In modules
                Try
                    _externalScraperModule.ProcessorModule.IsEnabled = value
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Could not set module <" & AssemblyName & "> to enabled status <" & value & ">")
                End Try
            Next
        End If
    End Sub

    Public Sub Settings_Save()
        Dim tmpForXML As New List(Of XMLAddonClass)

        While Not AllAddonsLoaded
            Application.DoEvents()
        End While

        For Each s As GenericAddon In GenericAddons
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Data_Movie In ScraperAddons_Data_Movie
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Data_Movieset In ScraperAddons_Data_Movieset
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Data_TV In ScraperAddons_Data_TV
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Image_Movie In ScraperAddons_Image_Movie
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Image_MovieSet In ScraperAddons_Image_Movieset
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Image_TV In ScraperAddons_Image_TV
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Theme_Movie In ScraperAddons_Theme_Movie
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Theme_TV In ScraperAddons_Theme_TV
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        For Each s As ScraperAddon_Trailer_Movie In ScraperAddons_Trailer_Movie
            Dim t As New XMLAddonClass With {
                .AddonEnabled = s.ProcessorModule.IsEnabled,
                .AddonOrder = s.ProcessorModule.Order,
                .AssemblyFileName = s.AssemblyFileName,
                .AssemblyName = s.AssemblyName,
                .InterfaceType = s.InterfaceType
            }
            tmpForXML.Add(t)
        Next
        Master.eSettings.Addons = tmpForXML
        Master.eSettings.Save()
    End Sub

    Private Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        RaiseEvent GenericEvent(mType, _params)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AssemblyListItem

#Region "Fields"

        Public Assembly As Reflection.Assembly
        Public AssemblyName As String
        Public AssemblyVersion As Version

#End Region 'Fields

    End Structure

    Structure VersionItem

#Region "Fields"

        Public AssemblyFileName As String
        Public Version As String

#End Region 'Fields

    End Structure

    Class EmberRuntimeObjects

#Region "Fields"

        Private _ListMovieSets As String
        Private _ListMovies As String
        Private _ListTVShows As String
        Private _LoadMedia As LoadMedia
        Private _OpenImageViewer As OpenImageViewer

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
                Return If(_ListMovieSets IsNot Nothing, _ListMovieSets, "moviesetlist")
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

        Public Property ContextMenuMovieList() As ContextMenuStrip
        Public Property ContextMenuMovieSetList() As ContextMenuStrip
        Public Property ContextMenuTVEpisodeList() As ContextMenuStrip
        Public Property ContextMenuTVSeasonList() As ContextMenuStrip
        Public Property ContextMenuTVShowList() As ContextMenuStrip
        Public Property FilterMovies() As String
        Public Property FilterMoviesSearch() As String
        Public Property FilterMoviesType() As String
        Public Property FilterMoviesets() As String
        Public Property FilterTVShows() As String
        Public Property FilterTVShowsSearch() As String
        Public Property FilterTVShowsType() As String
        Public Property MainMenu() As MenuStrip
        Public Property MainToolStrip() As ToolStrip
        Public Property MediaListMovieSets() As DataGridView
        Public Property MediaListMovies() As DataGridView
        Public Property MediaListTVEpisodes() As DataGridView
        Public Property MediaListTVSeasons() As DataGridView
        Public Property MediaListTVShows() As DataGridView
        Public Property MediaTabSelected() As Settings.MainTabSorting
        Public Property TrayMenu() As ContextMenuStrip

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

    Class GenericAddon

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IGenericAddon
        Public EventType As List(Of Enums.ModuleEventType)
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Generic

#End Region 'Fields

    End Class

    Class ScraperAddon_Data_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Data_Movie
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Data_Movie

#End Region 'Fields

    End Class

    Class ScraperAddon_Data_Movieset

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Data_MovieSet
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Data_Movieset

#End Region 'Fields

    End Class

    Class ScraperAddon_Data_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Data_TV
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Data_TV

#End Region 'Fields

    End Class

    Class ScraperAddon_Image_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Image_Movie
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Image_Movie

#End Region 'Fields

    End Class

    Class ScraperAddon_Image_MovieSet

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Image_Movieset
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Image_Movieset

#End Region 'Fields

    End Class

    Class ScraperAddon_Image_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Image_TV
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Image_TV

#End Region 'Fields

    End Class

    Class ScraperAddon_Theme_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Theme_Movie
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Theme_Movie

#End Region 'Fields

    End Class

    Class ScraperAddon_Theme_TV

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Theme_TV
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Theme_TV

#End Region 'Fields

    End Class

    Class ScraperAddon_Trailer_Movie

#Region "Fields"

        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public ProcessorModule As Interfaces.IScraperAddon_Trailer_Movie
        Public InterfaceType As Interfaces.InterfaceType = Interfaces.InterfaceType.Trailer_Movie

#End Region 'Fields

    End Class

    <XmlRoot("Addon")>
    Class XMLAddonClass

#Region "Fields"

        Public AddonEnabled As Boolean
        Public AddonOrder As Integer
        Public AssemblyFileName As String
        Public AssemblyName As String
        Public AssemblyVersion As String
        Public InterfaceType As Interfaces.InterfaceType

#End Region 'Fields

    End Class

#End Region 'Nested Types

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class XMLAddonSettings

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _strAddonSettingsPath As String = String.Empty
    Private _XMLAddonSettings As New XMLAdvancedSettings

#End Region 'Fields

#Region "Properties"

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        _strAddonSettingsPath = Path.Combine(Master.SettingsPath, Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location), "settings.xml")
        Load()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Function GetComplexSetting(ByVal strKey As String) As List(Of AdvancedSettingsComplexSettingsTableItem)
        Dim v = _XMLAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey)
        Return If(v Is Nothing, Nothing, v.Table.Item)
    End Function

    Public Function GetBooleanSetting(ByVal strKey As String, ByVal strDefValue As Boolean, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None) As Boolean
        If tContentType = Enums.ContentType.None Then
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey)
            Return If(v(0) Is Nothing, strDefValue, Convert.ToBoolean(v(0).Value))
        Else
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            Return If(v(0) Is Nothing, strDefValue, Convert.ToBoolean(v(0).Value))
        End If
        Return True
    End Function

    Public Function GetIntegerSetting(ByVal strKey As String, ByVal iDefValue As Integer, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None) As Integer
        If tContentType = Enums.ContentType.None Then
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing OrElse Not Integer.TryParse(v(0).Value, 0), iDefValue, CInt(v(0).Value))
        Else
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing OrElse Not Integer.TryParse(v(0).Value, 0), iDefValue, CInt(v(0).Value))
        End If
    End Function

    Public Function GetStringSetting(ByVal strKey As String, ByVal strDefValue As String, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None) As String
        If tContentType = Enums.ContentType.None Then
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, strDefValue, v(0).Value)
        Else
            Dim v = From e In _XMLAddonSettings.Setting.Where(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, strDefValue, v(0).Value)
        End If
    End Function

    Private Sub Load()
        Try
            If File.Exists(_strAddonSettingsPath) Then
                Dim objStreamReader As New StreamReader(_strAddonSettingsPath)
                Dim xAddonSettings As New XmlSerializer(_XMLAddonSettings.GetType)
                _XMLAddonSettings = CType(xAddonSettings.Deserialize(objStreamReader), XMLAdvancedSettings)
                objStreamReader.Close()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub Save()
        _XMLAddonSettings.Setting.Sort()
        Try
            If Not Directory.Exists(Directory.GetParent(_strAddonSettingsPath).FullName) Then
                Directory.CreateDirectory(Directory.GetParent(_strAddonSettingsPath).FullName)
            End If
            Dim objWriter As New FileStream(_strAddonSettingsPath, FileMode.Create)
            Dim xAddonSettings As New XmlSerializer(_XMLAddonSettings.GetType)
            xAddonSettings.Serialize(objWriter, _XMLAddonSettings)
            objWriter.Close()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub SetComplexSetting(ByVal strKey As String, ByVal tValue As List(Of AdvancedSettingsComplexSettingsTableItem))
        Dim v = _XMLAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey)
        If v Is Nothing Then
            _XMLAddonSettings.ComplexSettings.Add(New AdvancedSettingsComplexSettings With {.Table = New AdvancedSettingsComplexSettingsTable With {.Name = strKey, .Item = tValue}})
        Else
            _XMLAddonSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = strKey).Table.Item = tValue
        End If
    End Sub

    Public Sub SetBooleanSetting(ByVal strKey As String, ByVal bValue As Boolean, Optional ByVal isDefault As Boolean = False, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None)
        If tContentType = Enums.ContentType.None Then
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .DefaultValue = If(isDefault, Convert.ToString(bValue), String.Empty),
                                           .Name = strKey,
                                           .Value = Convert.ToString(bValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey).Value = Convert.ToString(bValue)
            End If
        Else
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .Content = tContentType,
                                           .DefaultValue = If(isDefault, Convert.ToString(bValue), String.Empty),
                                           .Name = strKey,
                                           .Value = Convert.ToString(bValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType).Value = Convert.ToString(bValue)
            End If
        End If
    End Sub

    Public Sub SetIntegerSetting(ByVal strKey As String, ByVal iValue As Integer, Optional ByVal isDefault As Boolean = False, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None)
        If tContentType = Enums.ContentType.None Then
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .DefaultValue = If(isDefault, CStr(iValue), String.Empty),
                                           .Name = strKey,
                                           .Value = CStr(iValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey).Value = CStr(iValue)
            End If
        Else
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .Content = tContentType,
                                           .DefaultValue = If(isDefault, CStr(iValue), String.Empty),
                                           .Name = strKey,
                                           .Value = CStr(iValue)
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType).Value = CStr(iValue)
            End If
        End If
    End Sub

    Public Sub SetStringSetting(ByVal strKey As String, ByVal strValue As String, Optional ByVal isDefault As Boolean = False, Optional ByVal tContentType As Enums.ContentType = Enums.ContentType.None)
        If tContentType = Enums.ContentType.None Then
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .DefaultValue = If(isDefault, strValue, String.Empty),
                                           .Name = strKey,
                                           .Value = strValue
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey).Value = strValue
            End If
        Else
            Dim v = _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType)
            If v Is Nothing Then
                _XMLAddonSettings.Setting.Add(New AdvancedSettingsSetting With {
                                           .Content = tContentType,
                                           .DefaultValue = If(isDefault, strValue, String.Empty),
                                           .Name = strKey,
                                           .Value = strValue
                                           })
            Else
                _XMLAddonSettings.Setting.FirstOrDefault(Function(f) f.Name = strKey AndAlso f.Content = tContentType).Value = strValue
            End If
        End If
    End Sub

#End Region 'Methods

End Class