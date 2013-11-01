Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.IO

Imports EmberAPI

Namespace EmberTests

    <TestClass()>
    Public Class Test_clsAPIFileUtils_Common

        <TestMethod()>
        Public Sub GetDirectory()
            'Arrange
            'The sourceDirectories should contain pairs of Strings - source strings to pass to the method, and expected return values.
            Dim sourceDirectories As New Dictionary(Of String, String) From
                {
                    {String.Empty, String.Empty},
                    {"c:\dir1\dir 2", "dir 2"},
                    {"c:\dir1\dir2\", "dir2"},
                    {"c:\dir1\dir 2\filename.ext", "filename.ext"},
                    {"c:\", "c"},
                    {"c:", "c"},
                    {"fjkienmz.de.e", "fjkienmz.de.e"},
                    {"\dir1\dir2\", "dir2"}
                }

            For Each pair As KeyValuePair(Of String, String) In sourceDirectories
                'Act
                Dim resultDirectory As String = FileUtils.Common.GetDirectory(pair.Key)

                'Assert
                Assert.AreEqual(pair.Value, resultDirectory, "Data tested was: '" & pair.Key & "' : '" & pair.Value & "'")
            Next

        End Sub

        <TestMethod()>
        Public Sub GetLongestFromRip()
            'This method is impractical to test properly as it requires multiple files over 1GB. Perhaps in the future.
            Assert.Inconclusive("Test not implemented")
        End Sub
        <TestMethod()>
        Public Sub isBDRip()
            'Arrange
            'The sourceDirectories should contain a String and Boolean - String is a path, and bool should be True if path should be interpreted as a blu-ray
            Dim sourceDirectories As New Dictionary(Of String, Boolean) From
                {
                    {String.Empty, False},
                    {"c:\dir1\dir 2", False},
                    {"c:\dir1\dir 2\filename.ext", False},
                    {"c:\", False},
                    {"c:", False},
                    {"fjkienmz.de.e", False},
                    {"\dir1\dir2\", False},
                    {"c:\dir1\dir2\movie title (2013)\BDMV\stream", True},
                    {"C:\DIR 1\DIR 2\movie title (2015)\BDMV", False},
                    {"C:\DiR 1\Second directo.ry\movie title Part3\BDMV\stream\0040.m2ts", True},
                    {"C:\DiR 1\Second directo.ry\movie title Part3\video_TS\0040.m2ts", False},
                    {"c:\stream\BDmv\0020.ext", False},
                    {"x:\movie.name\bdmv\stream", True},
                    {"x:\BDMV\stream", True},
                    {"x:\bdmv\stREAM\", True},
                    {"x:\bdmv\stREAM\//\/", True}
                }

            For Each pair As KeyValuePair(Of String, Boolean) In sourceDirectories
                'Act
                Dim result As Boolean = FileUtils.Common.isBDRip(pair.Key)

                'Assert
                Assert.AreEqual(pair.Value, result, "Data tested was: '" & pair.Key & "' : '" & pair.Value & "'")
            Next
        End Sub
        <TestMethod()>
        Public Sub isVideoTS()
            'Arrange
            'The sourceDirectories should contain a String and Boolean - String is a path, and bool should be True if path should be interpreted as a blu-ray
            Dim sourceDirectories As New Dictionary(Of String, Boolean) From
                {
                    {String.Empty, False},
                    {"c:\dir1\dir 2", False},
                    {"c:\dir1\dir 2\filename.ext", False},
                    {"c:\", False},
                    {"c:", False},
                    {"fjkienmz.de.e", False},
                    {"\dir1\dir2\", False},
                    {"c:\dir1\dir2\movie title 1 (2013)\video_ts", True},
                    {"c:\dir1\dir2\movie title 2 (2013)\VIDEO_TS", True},
                    {"c:\dir1\dir2\movie title 3 (2013)\vidEO_tS", True},
                    {"C:\DIR 1\DIR 2\movie title 4 (2015)\BDMV", False},
                    {"C:\DiR 1\Second directo.ry\movie title Part5\BDMV\stream\0040.m2ts", False},
                    {"C:\DiR 1\Second directo.ry\movie title Part6\video_TS\0040.vob", True},
                    {"x:\movie.name\VideoTS", False},
                    {"x:\movie.name\Video_TS", True},
                    {"x:\Video_TS", True},
                    {"x:\video_TS\", True},
                    {"c:\Video_TS\0020.ext", True}
                }

            For Each pair As KeyValuePair(Of String, Boolean) In sourceDirectories
                'Act
                Dim result As Boolean = FileUtils.Common.isVideoTS(pair.Key)

                'Assert
                Assert.AreEqual(pair.Value, result, "Data tested was: '" & pair.Key & "' : '" & pair.Value & "'")
            Next
        End Sub
        <TestMethod()>
        Public Sub MoveFileWithStream()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        <TestMethod()>
        Public Sub RemoveExtFromPath()
            'Arrange
            'The sourceDirectories should contain pairs of Strings - source strings to pass to the method, and expected return values.
            'NOTE: the test that starts with a relative path "\dir1\dir4" resolves to the current working directory's drive (c:\ in my case).
            'If your test system is different, expect a different result!
            Dim sourcePaths As New Dictionary(Of String, String) From
                {
                    {String.Empty, String.Empty},
                    {"c:\dir1\dir 1", "c:\dir1\dir 1"},
                    {"c:\dir1\dir 2\filename.ext", "c:\dir1\dir 2\filename"},
                    {"c:\dir1\dir 3\filename.ext.another", "c:\dir1\dir 3\filename.ext"},
                    {"\dir1\dir4\", "\dir1\dir4"},
                    {"fjkienmz.de.e", "fjkienmz.de"},
                    {"c:\", "c:\"},
                    {"c:", String.Empty},
                    {"!)(&^%$#", "!)(&^%$#"}
                }

            For Each pair As KeyValuePair(Of String, String) In sourcePaths
                'Act
                Dim result As String = FileUtils.Common.RemoveExtFromPath(pair.Key)

                'Assert
                'Ignore case during compare
                Assert.AreEqual(pair.Value, result, True, "Data tested was: '" & pair.Key & "' : '" & pair.Value & "'")
            Next
        End Sub
        <TestMethod()>
        Public Sub MakeValidFilename()
            'Arrange
            'The fileNames should contain pairs of Strings - source strings to pass to the method, and expected return values.
            Dim fileNames As New Dictionary(Of String, String) From
                {
                    {String.Empty, String.Empty},
                    {":/\|<>?*  ", " -\ "},
                    {"  *?><|\/:", " \ -"},
                    {"c:\", "c -\"},
                    {"c:\dir 1\perfectly legal filename (2013)\bdmv\strEAM\0021.m2ts", "c -\dir 1\perfectly legal filename (2013)\bdmv\strEAM\0021.m2ts"},
                    {"::?*", " - -"}
                }

            For Each pair As KeyValuePair(Of String, String) In fileNames
                'Act
                Dim result As String = FileUtils.Common.MakeValidFilename(pair.Key)

                'Assert
                Assert.AreEqual(pair.Value, result, "Data tested was: '" & pair.Key & "' : '" & pair.Value & "'")
            Next

        End Sub

    End Class

    <TestClass()>
    Public Class Test_clsAPIFileUtils_Delete
        ''' <summary>
        ''' Common setup for all tests in this class
        ''' 
        ''' Create a TempLibrary in the Temporary directory
        ''' Create a Blu-Ray file structure
        ''' Create a DVD file structure
        ''' Create a series of MKV/AVI files in separate directories
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()

        End Sub

        ''' <summary>
        ''' Common teardown for all tests in this class
        ''' 
        ''' Remove all the files created in the setup (if they still exist)
        ''' </summary>
        ''' <remarks></remarks>
        <TestCleanup>
        Public Sub TestCleanup()

        End Sub

        <TestMethod()>
        Public Sub DeleteDirectory()
            Assert.Inconclusive("Test not implemented")
        End Sub

        <TestMethod()>
        Public Sub GetItemsToDelete()
            Assert.Inconclusive("Test not implemented")
        End Sub
    End Class

    <TestClass()>
    Public Class Test_clsAPIFileUtils_FileSorter
        Dim testMediaDirectory As DirectoryInfo

        ''' <summary>
        ''' Create temp directory with multiple media files and some trailer/nfo/poster/fanart
        ''' </summary>
        ''' <remarks></remarks>
        <TestInitialize>
        Public Sub TestSetup()
            '' NOTE: I am not catching exceptions here. If something fails, the developer should resolve the file-level problem

            'Dim mediaNames = New List(Of String) From {{"Video alpha.avi"}, {"Movie beta.mkv"}, {"Gamma quadrant.flv"}, {"Roger roger.qt"}}
            'Dim mediaVariations = New List(Of String) From {{"-trailer.avi"}, {"-fanart.png"}, {".fanart.png"}, {"trailer5.avi"}}

            'Dim systemTempDirectory = New DirectoryInfo(Path.GetTempPath())
            'If systemTempDirectory.Exists() Then
            '    testMediaDirectory = systemTempDirectory.CreateSubdirectory(Path.GetRandomFileName())
            '    Debug.WriteLine("Temp directory is: " & testMediaDirectory.FullName)

            '    Dim rootPath = testMediaDirectory.FullName()
            '    'Dim tempFile As FileInfo
            '    'Dim tempFile As FileStream

            '    For Each name In mediaNames
            '        Using tempFile = File.Create(Path.Combine(rootPath, name))
            '            Debug.WriteLine("  Creating: " & tempFile.Name)
            '        End Using
            '        'tempFile = New FileInfo(Path.Combine(rootPath, name))
            '        'tempFile.Create()
            '        'tempFile.
            '        For Each variation In mediaVariations
            '            Dim filePath = Path.Combine(rootPath, String.Concat(Path.GetFileNameWithoutExtension(name), variation))
            '            Using tempFile = File.Create(filePath)
            '                'tempFile = New FileInfo(Path.Combine(rootPath, String.Concat(Path.GetFileNameWithoutExtension(name), variation)))
            '                'tempFile.Create()
            '                Debug.WriteLine("  Creating: " & tempFile.Name)
            '            End Using

            '        Next
            '        Debug.WriteLine(String.Empty)
            '    Next
            'Else
            '    Throw New DirectoryNotFoundException("System temp directory not found!")
            'End If
        End Sub

        ''' <summary>
        ''' Remove any files remaining in that temp directory
        ''' </summary>
        ''' <remarks></remarks>
        <TestCleanup>
        Public Sub TestCleanup()
            ''NOTE that there is no exception checking - Tester should resolve permission issues if any arise

            'If testMediaDirectory IsNot Nothing Then
            '    'Delete the directory and its sub-folders
            '    testMediaDirectory.Delete(True)
            '    Debug.WriteLine("  DELETING: " & testMediaDirectory.FullName)
            'End If


        End Sub
        <TestMethod()>
        Public Sub SortFiles()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub

    End Class



End Namespace