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

Public Class dlgDeleteConfirm

#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _PropogatingDown As Boolean = False
    Private _PropogatingUp As Boolean = False
    Private _ContentType As Enums.ContentType

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal lstIDs As List(Of Long), ByVal tContentType As Enums.ContentType) As DialogResult
        _ContentType = tContentType
        Populate_FileList(lstIDs)
        Return MyBase.ShowDialog
    End Function

    Private Sub AddFileNode(ByVal tParentNode As TreeNode, ByVal tFileInfo As FileInfo, ByVal bIsVideoFile As Boolean)
        Dim NewNode As TreeNode = tParentNode.Nodes.Add(tFileInfo.FullName, tFileInfo.Name)
        NewNode.Tag = tFileInfo.FullName
        NewNode.ImageKey = If(bIsVideoFile, "VIDEO", "FILE")
        NewNode.SelectedImageKey = If(bIsVideoFile, "VIDEO", "FILE")
    End Sub

    Private Sub AddFolderNode(ByVal tParentNode As TreeNode, ByVal tDirectoryInfo As DirectoryInfo)
        Dim NewNode As TreeNode = tParentNode.Nodes.Add(tDirectoryInfo.FullName, tDirectoryInfo.Name)
        NewNode.Tag = tDirectoryInfo.FullName
        NewNode.ImageKey = "FOLDER"
        NewNode.SelectedImageKey = "FOLDER"

        If Master.DB.LoadAll_Sources_Movie.FirstOrDefault(Function(f) f.Path = tDirectoryInfo.FullName) Is Nothing Then
            'populate all the sub-folders in the folder
            For Each nDirectoryInfo As DirectoryInfo In tDirectoryInfo.GetDirectories
                AddFolderNode(NewNode, nDirectoryInfo)
            Next
        End If

        'populate all the files in the folder
        For Each nFileItem As FileInfo In tDirectoryInfo.GetFiles()
            AddFileNode(NewNode, nFileItem, False)
        Next
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If DeleteSelectedItems() Then
            DialogResult = DialogResult.OK
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub btnToggleAllFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggleAllFiles.Click
        Dim Checked As Boolean?
        With tvFiles
            If .Nodes.Count = 0 Then Return
            For Each node As TreeNode In .Nodes
                If Not Checked.HasValue Then
                    'this is the first node of this type, set toggle status based on this
                    Checked = Not node.Checked
                End If
                node.Checked = Checked.Value
            Next
        End With
    End Sub

    Private Function DeleteSelectedItems() As Boolean
        Dim result As Boolean = True
        Dim tPair As New KeyValuePair(Of Long, Long)

        If tvFiles.Nodes.Count = 0 Then Return False

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction() 'Only on Batch Mode
            For Each nMainNode As TreeNode In tvFiles.Nodes
                If nMainNode.Nodes.Count > 0 Then
                    Dim nDeleteResults = DeleteSelectedSubItems(nMainNode.Nodes)
                    If (nDeleteResults.bHasRemoved OrElse nMainNode.Checked) AndAlso Not _ContentType = Enums.ContentType.TVSeason Then
                        Select Case _ContentType
                            Case Enums.ContentType.Movie
                                Master.DB.Delete_Movie(Convert.ToInt64(nMainNode.Tag), True)
                            Case Enums.ContentType.Movieset
                                Master.DB.Delete_MovieSet(Convert.ToInt64(nMainNode.Tag), True)
                            Case Enums.ContentType.TVEpisode
                                Master.DB.Delete_TVEpisode(Convert.ToInt64(nMainNode.Tag), False, False, True)
                            Case Enums.ContentType.TVShow
                                Master.DB.Delete_TVShow(Convert.ToInt64(nMainNode.Tag), True)
                        End Select
                    ElseIf nDeleteResults.bNeedsReload Then
                        'TODO: Reload
                        Select Case _ContentType
                            Case Enums.ContentType.Movie
                            Case Enums.ContentType.Movieset
                            Case Enums.ContentType.TVEpisode
                            Case Enums.ContentType.TVSeason
                            Case Enums.ContentType.TVShow
                        End Select
                    End If
                End If
            Next
            SQLtransaction.Commit()
        End Using

        Return result
    End Function

    Private Function DeleteSelectedSubItems(ByVal tSubNodeCollection As TreeNodeCollection) As DeleteResults
        Dim nResults As New DeleteResults
        For Each nSubNode As TreeNode In tSubNodeCollection
            If nSubNode.Checked Then
                Select Case nSubNode.ImageKey
                    Case "FOLDER"
                        Dim nDirectoryInfo As New DirectoryInfo(nSubNode.Tag.ToString)
                        If nDirectoryInfo.Exists Then
                            nDirectoryInfo.Delete(True)
                            nResults.bNeedsReload = True
                        End If

                    Case "FILE"
                        Dim nFileInfo As New FileInfo(nSubNode.Tag.ToString)
                        If nFileInfo.Exists Then
                            nFileInfo.Delete()
                            nResults.bNeedsReload = True
                        End If

                    Case "VIDEO"
                        Dim nFileInfo As New FileInfo(nSubNode.Tag.ToString)
                        If nFileInfo.Exists Then
                            nFileInfo.Delete()
                            nResults.bHasRemoved = True
                        End If
                End Select
            ElseIf nSubNode.Nodes.Count > 0 Then
                Dim nSubResults = DeleteSelectedSubItems(nSubNode.Nodes)
                If nSubResults.bNeedsReload Then nResults.bNeedsReload = True
                If nSubResults.bHasRemoved Then nResults.bHasRemoved = True
            End If
        Next
        Return nResults
    End Function

    Private Sub dlgDeleteConfirm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
    End Sub

    Private Sub Populate_FileList(ByVal lstIDs As List(Of Long))
        Dim bHadError As Boolean = False
        Dim lstItems As New List(Of FileSystemInfo)
        Dim ItemParentNode As New TreeNode

        With tvFiles
            Select Case _ContentType

                'MOVIE
                Case Enums.ContentType.Movie
                    For Each lngMovieID As Long In lstIDs
                        bHadError = False

                        Dim nMovie As Database.DBElement = Master.DB.Load_Movie(lngMovieID)

                        ItemParentNode = .Nodes.Add(nMovie.ID.ToString, nMovie.Movie.Title)
                        ItemParentNode.ImageKey = "DBE"
                        ItemParentNode.SelectedImageKey = "DBE"
                        ItemParentNode.Tag = nMovie.ID

                        'get all associated files
                        lstItems = FileUtils.Common.GetAllItemsOfDBElement(nMovie)

                        For Each nItem As FileSystemInfo In lstItems
                            If Not ItemParentNode.Nodes.ContainsKey(nItem.FullName) Then
                                If TypeOf nItem Is DirectoryInfo Then
                                    Try
                                        AddFolderNode(ItemParentNode, DirectCast(nItem, DirectoryInfo))
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                Else
                                    Try
                                        AddFileNode(ItemParentNode, DirectCast(nItem, FileInfo), nMovie.Filename = nItem.FullName)
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                End If
                            End If
                        Next

                        If bHadError Then .Nodes.Remove(ItemParentNode)
                    Next

                    'MovieSet
                Case Enums.ContentType.Movieset
                    For Each lngMovieID As Long In lstIDs
                        bHadError = False

                        Dim nMovieSet As Database.DBElement = Master.DB.Load_Movieset(lngMovieID)

                        ItemParentNode = .Nodes.Add(nMovieSet.ID.ToString, nMovieSet.MovieSet.Title)
                        ItemParentNode.ImageKey = "DBE"
                        ItemParentNode.SelectedImageKey = "DBE"
                        ItemParentNode.Tag = nMovieSet.ID

                        'get all associated files
                        lstItems = FileUtils.Common.GetAllItemsOfDBElement(nMovieSet)

                        For Each nItem As FileSystemInfo In lstItems
                            If Not ItemParentNode.Nodes.ContainsKey(nItem.FullName) Then
                                If TypeOf nItem Is DirectoryInfo Then
                                    Try
                                        AddFolderNode(ItemParentNode, DirectCast(nItem, DirectoryInfo))
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                Else
                                    Try
                                        AddFileNode(ItemParentNode, DirectCast(nItem, FileInfo), False)
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                End If
                            End If
                        Next

                        If bHadError Then .Nodes.Remove(ItemParentNode)
                    Next


                    'TVEpisode
                Case Enums.ContentType.TVEpisode
                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        For Each lngTVEpisodeID As Long In lstIDs
                            bHadError = False

                            Dim nTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(lngTVEpisodeID, True)

                            ItemParentNode = .Nodes.Add(nTVEpisode.ID.ToString, String.Format("{0} - {1}", nTVEpisode.TVShow.Title, nTVEpisode.TVEpisode.Title))
                            ItemParentNode.ImageKey = "DBE"
                            ItemParentNode.SelectedImageKey = "DBE"
                            ItemParentNode.Tag = lngTVEpisodeID

                            'get all associated files
                            lstItems = FileUtils.Common.GetAllItemsOfDBElement(nTVEpisode)

                            For Each nItem As FileSystemInfo In lstItems
                                If Not ItemParentNode.Nodes.ContainsKey(nItem.FullName) Then
                                    If TypeOf nItem Is DirectoryInfo Then
                                        Try
                                            AddFolderNode(ItemParentNode, DirectCast(nItem, DirectoryInfo))
                                        Catch
                                            bHadError = True
                                            Exit For
                                        End Try
                                    Else
                                        Try
                                            AddFileNode(ItemParentNode, DirectCast(nItem, FileInfo), nTVEpisode.Filename = nItem.FullName)
                                        Catch
                                            bHadError = True
                                            Exit For
                                        End Try
                                    End If
                                End If
                            Next
                        Next
                    End Using

                    'TVSeason
                Case Enums.ContentType.TVSeason
                    For Each lngTVSeasonID As Long In lstIDs
                        bHadError = False

                        Dim nTVSeason As Database.DBElement = Master.DB.Load_TVSeason(lngTVSeasonID, True, True)

                        ItemParentNode = .Nodes.Add(nTVSeason.ID.ToString, String.Format("{0} - {1}",
                                                                                         nTVSeason.TVShow.Title,
                                                                                         StringUtils.FormatSeasonTitle(nTVSeason.TVSeason.Season)))
                        ItemParentNode.ImageKey = "DBE"
                        ItemParentNode.SelectedImageKey = "DBE"
                        ItemParentNode.Tag = nTVSeason.ID

                        'get all associated files
                        lstItems = FileUtils.Common.GetAllItemsOfDBElement(nTVSeason)

                        For Each fileItem As FileSystemInfo In lstItems
                            If Not ItemParentNode.Nodes.ContainsKey(fileItem.FullName) Then
                                If TypeOf fileItem Is DirectoryInfo Then
                                    Try
                                        AddFolderNode(ItemParentNode, DirectCast(fileItem, DirectoryInfo))
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                Else
                                    Try
                                        AddFileNode(ItemParentNode, DirectCast(fileItem, FileInfo), False)
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                End If
                            End If
                        Next

                        'SQLDelCommand.CommandText = String.Concat("SELECT idEpisode, idFile FROM episode WHERE idShow = ", lngTVSeasonID.Value, " AND Season = ", lngTVSeasonID.Key, ";")
                        'Using SQLDelReader As SQLite.SQLiteDataReader = SQLDelCommand.ExecuteReader
                        '    While SQLDelReader.Read
                        '        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        '            SQLCommand.CommandText = String.Concat("SELECT strFilename FROM files WHERE idFile = ", SQLDelReader("idFile"), ";")
                        '            Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                        '                If SQLReader.HasRows Then
                        '                    SQLReader.Read()
                        '                    If Functions.IsSeasonDirectory(Directory.GetParent(SQLReader("strFilename").ToString).FullName) Then
                        '                        Try
                        '                            AddFolderNode(ItemParentNode, New DirectoryInfo(Directory.GetParent(SQLReader("strFilename").ToString).FullName))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).Parent.FullName, String.Format("season{0}.tbn", lngTVSeasonID.Key.ToString.PadLeft(2, Convert.ToChar("0"))))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).Parent.FullName, String.Format("season{0}.tbn", lngTVSeasonID.Key.ToString))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).Parent.FullName, String.Format("season{0}.jpg", lngTVSeasonID.Key.ToString.PadLeft(2, Convert.ToChar("0"))))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).Parent.FullName, String.Format("season{0}.jpg", lngTVSeasonID.Key.ToString))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                        Catch
                        '                            .Nodes.Remove(ItemParentNode)
                        '                        End Try
                        '                        Exit While
                        '                    Else
                        '                        Try
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).FullName, Path.GetFileNameWithoutExtension(SQLReader("strFilename").ToString))
                        '                            AddFileNode(ItemParentNode, New FileInfo(SQLReader("strFilename").ToString))
                        '                            If File.Exists(String.Concat(ePath, ".nfo")) Then AddFileNode(ItemParentNode, New FileInfo(String.Concat(ePath, ".nfo")))
                        '                            If File.Exists(String.Concat(ePath, ".tbn")) Then AddFileNode(ItemParentNode, New FileInfo(String.Concat(ePath, ".tbn")))
                        '                            If File.Exists(String.Concat(ePath, ".jpg")) Then AddFileNode(ItemParentNode, New FileInfo(String.Concat(ePath, ".jpg")))
                        '                            If File.Exists(String.Concat(ePath, "-fanart.jpg")) Then AddFileNode(ItemParentNode, New FileInfo(String.Concat(ePath, "-fanart.jpg")))
                        '                            If File.Exists(String.Concat(ePath, ".fanart.jpg")) Then AddFileNode(ItemParentNode, New FileInfo(String.Concat(ePath, ".fanart.jpg")))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).FullName, String.Format("season{0}.tbn", lngTVSeasonID.Key.ToString.PadLeft(2, Convert.ToChar("0"))))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).FullName, String.Format("season{0}.tbn", lngTVSeasonID.Key.ToString))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).FullName, String.Format("season{0}.jpg", lngTVSeasonID.Key.ToString.PadLeft(2, Convert.ToChar("0"))))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                            ePath = Path.Combine(Directory.GetParent(SQLReader("strFilename").ToString).FullName, String.Format("season{0}.jpg", lngTVSeasonID.Key.ToString))
                        '                            If File.Exists(ePath) Then AddFileNode(ItemParentNode, New FileInfo(ePath))
                        '                        Catch
                        '                            .Nodes.Remove(ItemParentNode)
                        '                            Exit While
                        '                        End Try
                        '                    End If
                        '                End If
                        '            End Using
                        '        End Using
                        '    End While
                        'End Using
                    Next

                    'TVShow
                Case Enums.ContentType.TVShow
                    For Each lngTVShowID As Long In lstIDs
                        bHadError = False

                        Dim nTVShow As Database.DBElement = Master.DB.Load_TVShow(lngTVShowID, False, False)

                        ItemParentNode = .Nodes.Add(nTVShow.ID.ToString, nTVShow.TVShow.Title)
                        ItemParentNode.ImageKey = "DBE"
                        ItemParentNode.SelectedImageKey = "DBE"
                        ItemParentNode.Tag = nTVShow.ID

                        'get all associated files
                        lstItems = FileUtils.Common.GetAllItemsOfDBElement(nTVShow)

                        For Each nItem As FileSystemInfo In lstItems
                            If Not ItemParentNode.Nodes.ContainsKey(nItem.FullName) Then
                                If TypeOf nItem Is DirectoryInfo Then
                                    Try
                                        AddFolderNode(ItemParentNode, DirectCast(nItem, DirectoryInfo))
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                Else
                                    Try
                                        AddFileNode(ItemParentNode, DirectCast(nItem, FileInfo), False)
                                    Catch
                                        bHadError = True
                                        Exit For
                                    End Try
                                End If
                            End If
                        Next

                        'Try
                        '    AddFolderNode(ItemParentNode, New DirectoryInfo(nTVShow.ShowPath))
                        'Catch
                        '    .Nodes.Remove(ItemParentNode)
                        'End Try
                    Next
            End Select

            'check all the nodes
            For Each node As TreeNode In .Nodes
                node.Checked = True
                node.Expand()
            Next
        End With
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(714, "Confirm Items To Be Deleted")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnToggleAllFiles.Text = Master.eLang.GetString(715, "Toggle All Files")
        tsslSelectedNode.Text = String.Empty
    End Sub

    Private Sub tvFiles_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvFiles.AfterCheck
        If e.Node.Parent Is Nothing Then
            'this is a movie node
            If _PropogatingUp Then Return

            'check/uncheck all children
            _PropogatingDown = True
            For Each node As TreeNode In e.Node.Nodes
                node.Checked = e.Node.Checked
            Next
            _PropogatingDown = False
        Else
            'this is a file/folder node
            If e.Node.Checked Then
                If Not _PropogatingUp Then
                    _PropogatingDown = True
                    For Each node As TreeNode In e.Node.Nodes
                        node.Checked = True
                    Next
                    _PropogatingDown = False
                End If

                'if all children are checked then check root node
                For Each node As TreeNode In e.Node.Parent.Nodes
                    If Not node.Checked Then Return
                Next
                _PropogatingUp = True
                e.Node.Parent.Checked = True
                _PropogatingUp = False
            Else
                If Not _PropogatingUp Then
                    'uncheck any children
                    _PropogatingDown = True
                    For Each node As TreeNode In e.Node.Nodes
                        node.Checked = False
                    Next
                    _PropogatingDown = False
                End If

                'make sure parent is no longer checked
                _PropogatingUp = True
                e.Node.Parent.Checked = False
                _PropogatingUp = False
            End If
        End If
    End Sub

    Private Sub tvFiles_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvFiles.AfterSelect
        Select Case e.Node.ImageKey
            Case "DBE"
                tsslSelectedNode.Text = e.Node.Text
            Case "FILE", "FOLDER", "VIDEO"
                tsslSelectedNode.Text = e.Node.Tag.ToString
        End Select
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure DeleteResults
        Dim bNeedsReload As Boolean
        Dim bHasRemoved As Boolean
    End Structure

#End Region 'Nested Types

End Class