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
Imports System.Text.RegularExpressions

<Serializable()>
Public Class FileItem

#Region "Fields"

    Private _directoryinfo As DirectoryInfo
    Private _fileinfo As FileInfo
    Private _id As Long
    Private _originalFileName As String
    Private _path As String

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal path As String)
        Clear()
        If path.StartsWith("stack://") Then
            _fileinfo = New FileInfo(FileUtils.Stacking.GetFirstPathFromStack(path))
        ElseIf Not String.IsNullOrEmpty(path) Then
            _fileinfo = New FileInfo(path)
        End If
        _path = path
    End Sub

    Public Sub New(ByVal directoryInfo As DirectoryInfo)
        Clear()
        _directoryinfo = directoryInfo
        _path = directoryInfo.FullName
    End Sub

    Public Sub New(ByVal fileInfo As FileInfo)
        Clear()
        _fileinfo = fileInfo
        _originalFileName = fileInfo.FullName
        _path = fileInfo.FullName
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public ReadOnly Property bIsArchive() As Boolean
        Get
            Return IsArchive()
        End Get
    End Property

    Public ReadOnly Property bIsBDMV() As Boolean
        Get
            Return IsBDMV()
        End Get
    End Property

    Public ReadOnly Property bIsDirectory() As Boolean
        Get
            Return _directoryinfo IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property bIsDiscImage() As Boolean
        Get
            Return IsDiscImage()
        End Get
    End Property

    Public ReadOnly Property bIsDiscStub() As Boolean
        Get
            Return IsDiscStub()
        End Get
    End Property

    Public ReadOnly Property bIsOnline() As Boolean
        Get
            Return IsOnline()
        End Get
    End Property

    Public ReadOnly Property bIsStacked() As Boolean
        Get
            Return IsStacked()
        End Get
    End Property

    Public ReadOnly Property bIsVideoTS() As Boolean
        Get
            Return IsVideoTS()
        End Get
    End Property

    Public ReadOnly Property DirectoryInfo() As DirectoryInfo
        Get
            Return _directoryinfo
        End Get
    End Property

    Public ReadOnly Property Extension As String
        Get
            Return _fileinfo.Extension
        End Get
    End Property

    Public Property ID() As Long
        Get
            Return _id
        End Get
        Set(value As Long)
            _id = value
        End Set
    End Property

    Public ReadOnly Property IDSpecified() As Boolean
        Get
            Return Not _id = -1
        End Get
    End Property


    Public ReadOnly Property MainPath() As DirectoryInfo
        Get
            Return GetMainPath()
        End Get
    End Property

    Public ReadOnly Property FileInfo() As FileInfo
        Get
            Return _fileinfo
        End Get
    End Property

    Public ReadOnly Property FullPath() As String
        Get
            Return _path
        End Get
    End Property

    Public ReadOnly Property FullPathSpecified() As Boolean
        Get
            Return Not String.IsNullOrEmpty(_path)
        End Get
    End Property

    Public ReadOnly Property FirstPathFromStack() As String
        Get
            Return GetFirstPathFromStack()
        End Get
    End Property

    Public Property OriginalFileName As String
        Get
            Return _originalFileName
        End Get
        Set(value As String)
            _originalFileName = value
        End Set
    End Property

    Public ReadOnly Property OriginalFileNameSpecified() As Boolean
        Get
            Return Not String.IsNullOrEmpty(OriginalFileName)
        End Get
    End Property

    Public ReadOnly Property PathList() As List(Of String)
        Get
            Return GetPathList()
        End Get
    End Property

    ''' <summary>
    ''' Return the stacked file name and extension of a stacked file
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property StackedFileName() As String
        Get
            Return Path.GetFileName(FileUtils.Stacking.GetStackedPath(FullPath))
        End Get
    End Property
    ''' <summary>
    ''' Total size, in bytes, of the current FileItem
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TotalSize() As Long
        Get
            Return FileUtils.Common.GetTotalLengt(Me)
        End Get
    End Property

    Public ReadOnly Property TotalSizeSpecified() As Boolean
        Get
            Return Not TotalSize = 0
        End Get
    End Property

    Public ReadOnly Property TotalSizeAsReadableString() As String
        Get
            Select Case TotalSize
                Case 0 To 1023
                    Return TotalSize & " Bytes"
                Case 1024 To 1048575
                    Return String.Concat((TotalSize / 1024).ToString("###0.00"), " KB")
                Case 1048576 To 1043741824
                    Return String.Concat((TotalSize / 1024 ^ 2).ToString("###0.00"), " MB")
                Case Is > 1043741824
                    Return String.Concat((TotalSize / 1024 ^ 3).ToString("###0.00"), " GB")
                Case Else
                    Return "unknown"
            End Select
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Clear()
        _directoryinfo = Nothing
        _fileinfo = Nothing
        _id = -1
        _path = String.Empty
        _originalFileName = String.Empty
    End Sub

    Private Function GetFirstPathFromStack() As String
        If bIsStacked Then
            Return FileUtils.Stacking.GetFirstPathFromStack(_path)
        Else
            Return _path
        End If
    End Function

    Private Function GetPathList() As List(Of String)
        Return FileUtils.Stacking.GetPathList(_path)
    End Function
    ''' <summary>
    ''' Returned the main folder that contains the file
    ''' </summary>
    ''' <returns></returns>
    Private Function GetMainPath() As DirectoryInfo
        If Not bIsDirectory Then
            If IsBDMV() Then
                Return Directory.GetParent(Directory.GetParent(_fileinfo.FullName).FullName)
            ElseIf IsVideoTS() AndAlso Directory.GetParent(_fileinfo.FullName).Name = "VIDEO_TS" Then
                Return Directory.GetParent(Directory.GetParent(_fileinfo.FullName).FullName)
            ElseIf IsVideoTS() Then
                Return Directory.GetParent(_fileinfo.FullName)
            Else
                Return Directory.GetParent(_fileinfo.FullName)
            End If
        Else
            Return _directoryinfo
        End If
    End Function

    Private Function IsArchive() As Boolean
        If Not bIsDirectory Then
            Dim strExtensions() As String = {".rar", ".zip"}
            Return strExtensions.Contains(_fileinfo.Extension.ToLower)
        End If
        Return False
    End Function

    Private Function IsBDMV() As Boolean
        If Not bIsDirectory Then
            Return _fileinfo.Name.ToLower = "index.bdmv" OrElse _fileinfo.Name.ToLower = "moviembject.bdmv"
        End If
        Return False
    End Function

    Private Function IsDiscImage() As Boolean
        If Not bIsDirectory Then
            Dim strExtensions() As String = {".bin", ".img", ".iso", ".nrg"}
            Return strExtensions.Contains(_fileinfo.Extension.ToLower)
        End If
        Return False
    End Function

    Private Function IsOnline() As Boolean
        If Not bIsDirectory Then
            If IsStacked() Then
                For Each nFile In PathList
                    If Not File.Exists(nFile) Then Return False
                Next
                Return True
            Else
                Return File.Exists(_path)
            End If
        Else
            If Directory.Exists(GetFirstPathFromStack) Then Return True
        End If
        Return False
    End Function

    Private Function IsDiscStub() As Boolean
        If Not bIsDirectory Then
            Dim strExtensions() As String = {".disc"}
            Return strExtensions.Contains(_fileinfo.Extension.ToLower)
        End If
        Return False
    End Function

    Private Function IsStacked() As Boolean
        Return _path.StartsWith("stack://")
    End Function

    Private Function IsVideoTS() As Boolean
        If Not bIsDirectory Then
            If _fileinfo.Name.ToLower = "video_ts.ifo" Then Return True
            If _fileinfo.Name.ToLower.StartsWith("vts_") AndAlso
                _fileinfo.Name.ToLower.EndsWith("_0.ifo") AndAlso
                _fileinfo.Name.ToLower.Length = 12 Then Return True
        End If
        Return False
    End Function

#End Region 'Methods

End Class

Public Class FileItemList

#Region "Fields"

    Private _bismultipath As Boolean
    Private _fileitemlist As New List(Of FileItem)

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

    Public Sub New(ByVal Path As String, ByVal tContentType As Enums.ContentType)
        Clear()

        Dim intSkipLessThan As Integer = 0
        Select Case tContentType
            Case Enums.ContentType.Movie
                intSkipLessThan = Master.eSettings.Movie.SourceSettings.SkipLessThan
            Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                intSkipLessThan = Master.eSettings.TVEpisode.SourceSettings.SkipLessThan
        End Select

        Dim diPath As DirectoryInfo = New DirectoryInfo(Path)
        'get all paths
        Dim lstDirectories = diPath.GetDirectories.Where(Function(f) FileUtils.Common.IsValidDir(f, tContentType))
        For Each nDirectoryInfo In lstDirectories
            _fileitemlist.Add(New FileItem(nDirectoryInfo))
        Next

        'get all files
        Dim lstFiles = diPath.GetFiles.Where(Function(f) FileUtils.Common.IsValidFile(f, tContentType)).OrderBy(Function(f) f.FullName)
        For Each nFileInfo In lstFiles
            _fileitemlist.Add(New FileItem(nFileInfo))
        Next
        Stack()
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public ReadOnly Property FileItems() As List(Of FileItem)
        Get
            Return _fileitemlist
        End Get
    End Property


    Public Property bMultiPath() As Boolean
        Get
            Return _bismultipath
        End Get
        Set(value As Boolean)
            _bismultipath = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub AddItem(strPath As String)
        _fileitemlist.Add(New FileItem(strPath))
    End Sub

    Public Sub AddItem(tFileInfo As FileInfo)
        _fileitemlist.Add(New FileItem(tFileInfo))
    End Sub

    Public Sub AddItem(tFileItem As FileItem)
        _fileitemlist.Add(tFileItem)
    End Sub

    Public Sub AddItems(tFileItems As IEnumerable(Of FileInfo))
        For i As Integer = 0 To tFileItems.Count - 1
            _fileitemlist.Add(New FileItem(tFileItems(i)))
        Next
    End Sub

    Public Sub Clear()
        _bismultipath = False
        _fileitemlist.Clear()
    End Sub

    Public Function GetItem(iItem As Integer) As FileItem
        If iItem > -1 AndAlso iItem < _fileitemlist.Count Then
            Return _fileitemlist(iItem)
        Else
            Return Nothing
        End If
    End Function

    Public Sub RemoveItem(iFileItem As Integer)
        _fileitemlist.RemoveAt(iFileItem)
    End Sub

    Public Sub RemoveItem(tFileItem As FileItem)
        _fileitemlist.Remove(tFileItem)
    End Sub

    Private Sub Stack()
        StackFolders()
        StackFiles()
    End Sub

    Private Sub StackFolders()
        Dim strFolderStackingPattern As String = AdvancedSettings.AdvancedSettings.GetStringSetting("FolderStacking", "((cd|dvd|dis[ck])[0-9]+)$")

        'stack folders
        Dim i As Integer = 0
        While i < _fileitemlist.Count
            If _fileitemlist(i).bIsDirectory Then
                Dim tFileItem1 As FileItem = GetItem(i)
                'folder stacking is disabled, does not work in Kodi
                'we only check for VIDEO_TS and BDMV structure

                If tFileItem1 IsNot Nothing Then
                    'check if VIDEO_TS
                    Dim strPath As String = Path.Combine(tFileItem1.FirstPathFromStack, "VIDEO_TS.IFO")
                    If File.Exists(strPath) Then
                        'VIDEO_TS structure found, change folder to file
                        _fileitemlist(i) = New FileItem(strPath)
                    Else
                        strPath = Path.Combine(tFileItem1.FirstPathFromStack, "VIDEO_TS")
                        strPath = Path.Combine(strPath, "VIDEO_TS.IFO")
                        If File.Exists(strPath) Then
                            'VIDEO_TS structure found, change folder to file
                            _fileitemlist(i) = New FileItem(strPath)
                        Else
                            'check if BDMV
                            strPath = Path.Combine(tFileItem1.FirstPathFromStack, "index.bdmv")
                            If File.Exists(strPath) Then
                                'BDMV structure found, change folder to file
                                _fileitemlist(i) = New FileItem(strPath)
                            Else
                                strPath = Path.Combine(tFileItem1.FirstPathFromStack, "BDMV")
                                strPath = Path.Combine(strPath, "index.bdmv")
                                If File.Exists(strPath) Then
                                    'BDMV structure found, change folder to file
                                    _fileitemlist(i) = New FileItem(strPath)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            i += 1
        End While
    End Sub

    Private Sub StackFiles()
        Dim strFileStackingPattern As String = AdvancedSettings.AdvancedSettings.GetStringSetting("FileStacking", "(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck])[ _.-]*[0-9]+)(.*?)(\.[^.]+)$")

        'now stack the files, some of which may be from the previous stack iteration
        Dim i As Integer = 0
        While i < _fileitemlist.Count
            If Not _fileitemlist(i).bIsDirectory AndAlso Not _fileitemlist(i).bIsBDMV AndAlso Not _fileitemlist(i).bIsVideoTS Then
                Dim iStack As New List(Of Integer)
                Dim lngSize As Long = 0
                Dim strStackName As String = String.Empty
                Dim tFileItem1 As FileItem = GetItem(i)

                If tFileItem1 IsNot Nothing Then
                    Dim rResultFileItem1 As Match = Regex.Match(tFileItem1.FileInfo.Name, strFileStackingPattern, RegexOptions.IgnoreCase)
                    If rResultFileItem1.Success Then
                        Dim strTitle1 As String = rResultFileItem1.Groups(1).Value
                        Dim strVolume1 As String = rResultFileItem1.Groups(2).Value
                        Dim strIgnore1 As String = rResultFileItem1.Groups(3).Value
                        Dim strExtension1 As String = rResultFileItem1.Groups(4).Value

                        Dim j As Integer = i + 1
                        While j < _fileitemlist.Count
                            Dim tFileItem2 As FileItem = GetItem(j)

                            Dim rResultFileItem2 As Match = Regex.Match(tFileItem2.FileInfo.Name, strFileStackingPattern, RegexOptions.IgnoreCase)
                            If rResultFileItem2.Success Then
                                Dim strTitle2 As String = rResultFileItem2.Groups(1).Value
                                Dim strVolume2 As String = rResultFileItem2.Groups(2).Value
                                Dim strIgnore2 As String = rResultFileItem2.Groups(3).Value
                                Dim strExtension2 As String = rResultFileItem2.Groups(4).Value

                                If strTitle1.ToLower = strTitle2.ToLower Then
                                    If Not strVolume1.ToLower = strVolume2.ToLower Then
                                        If strIgnore1.ToLower = strIgnore2.ToLower AndAlso strExtension1.ToLower = strExtension2.ToLower Then
                                            If iStack.Count = 0 Then
                                                strStackName = String.Concat(strTitle1, strIgnore1, strExtension1)
                                                iStack.Add(i)
                                                lngSize = tFileItem1.FileInfo.Length
                                            End If
                                            iStack.Add(j)
                                            lngSize = tFileItem2.FileInfo.Length
                                        Else 'Sequel
                                            Exit While
                                        End If
                                    Else 'False positive, try again with offset
                                        Exit While
                                    End If
                                Else 'Title mismatch
                                    Exit While
                                End If
                            End If
                            j += 1
                        End While
                    End If
                End If

                If iStack.Count > 1 Then
                    'have a stack, remove the items And add the stacked item
                    'dont actually stack a multipart rar set, just remove all items but the first
                    Dim strStackedPath As String = FileUtils.Stacking.ConstructStackedPath(Me, iStack)
                    _fileitemlist(i) = New FileItem(strStackedPath)

                    'clean up list
                    Dim k As Integer = 1
                    While k < iStack.Count
                        RemoveItem(i + 1)
                        k += 1
                    End While
                End If
            End If
            i += 1
        End While
    End Sub

#End Region 'Methods

End Class