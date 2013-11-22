Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports EmberAPI

Namespace EmberTests

    <TestClass()> Public Class Test_clsAPIStringUtils_StringUtils

        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_AlphaNumericOnly_VariousUnicode_NoSpecial()
            'Arrange
            Dim source As New Dictionary(Of Char, Boolean) 'From
            With source
                .Add("A"c, True)    'Letter, Uppercase http://www.fileformat.info/info/unicode/category/Lu/index.htm
                .Add("Z"c, True)    'Letter, Uppercase http://www.fileformat.info/info/unicode/category/Lu/index.htm
                .Add("a"c, True)    'Letter, Lowercase http://www.fileformat.info/info/unicode/category/Ll/index.htm
                .Add("z"c, True)    'Letter, Lowercase http://www.fileformat.info/info/unicode/category/Ll/index.htm
                .Add("1"c, True)    'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add("9"c, True)    'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add("0"c, True)    'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add(ChrW(&H1C5), True) 'Letter, Uppercase http://www.fileformat.info/info/unicode/category/Lu/index.htm
                .Add(ChrW(&H2E0), True) 'Letter, Modifier http://www.fileformat.info/info/unicode/category/Lm/index.htm
                .Add(ChrW(&H5DA), True) 'Letter, Other http://www.fileformat.info/info/unicode/category/Lo/index.htm
                .Add(ChrW(&H6F5), True) 'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add(ChrW(0), False)    'Other, Control http://www.fileformat.info/info/unicode/category/Cc/index.htm
                .Add("["c, False)   'Punctuation, Open http://www.fileformat.info/info/unicode/category/Ps/index.htm
                .Add(" "c, False)   'Separator, Space http://www.fileformat.info/info/unicode/category/Zs/index.htm
                .Add(ChrW(&HA0), False)  'Separator, Space http://www.fileformat.info/info/unicode/category/Zs/index.htm
                .Add(ChrW(&H2028), False)   'Separator, Line http://www.fileformat.info/info/unicode/category/Zl/index.htm
                .Add("+"c, False)   'Symbol, Math http://www.fileformat.info/info/unicode/category/Sm/index.htm
                .Add("$"c, False)   'Special Asc 36
                .Add("%"c, False)   'Special Asc 37
                .Add("&"c, False)   'Special Asc 38
                .Add(","c, False)   'Special Asc 44
                .Add("-"c, False)   'Special Asc 45
                .Add("."c, False)   'Special Asc 46
                .Add(":"c, False)   'Special Asc 58
                .Add(")"c, False)   'Special Asc 41
            End With

            For Each pair As KeyValuePair(Of Char, Boolean) In source
                'Act
                Dim returnValue As Boolean = StringUtils.AlphaNumericOnly(pair.Key, False)

                'Assert
                Assert.AreEqual(pair.Value, returnValue, "Expected {0}, but received {1} for value: U+{2:X4} ({3})", pair.Value, returnValue, Convert.ToUInt16(pair.Key), pair.Key)
            Next
        End Sub
        <UnitTest>
<TestMethod()>
        Public Sub StringUtils_AlphaNumericOnly_VariousUnicode_WithSpecial()
            'Arrange
            Dim source As New Dictionary(Of Char, Boolean) 'From
            With source
                .Add("A"c, True)    'Letter, Uppercase http://www.fileformat.info/info/unicode/category/Lu/index.htm
                .Add("Z"c, True)    'Letter, Uppercase http://www.fileformat.info/info/unicode/category/Lu/index.htm
                .Add("a"c, True)    'Letter, Lowercase http://www.fileformat.info/info/unicode/category/Ll/index.htm
                .Add("z"c, True)    'Letter, Lowercase http://www.fileformat.info/info/unicode/category/Ll/index.htm
                .Add("1"c, True)    'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add("9"c, True)    'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add("0"c, True)    'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add(ChrW(&H1C5), True) 'Letter, Uppercase http://www.fileformat.info/info/unicode/category/Lu/index.htm
                .Add(ChrW(&H2E0), True) 'Letter, Modifier http://www.fileformat.info/info/unicode/category/Lm/index.htm
                .Add(ChrW(&H5DA), True) 'Letter, Other http://www.fileformat.info/info/unicode/category/Lo/index.htm
                .Add(ChrW(&H6F5), True) 'Number, Decimal Digit http://www.fileformat.info/info/unicode/category/Nd/index.htm
                .Add(ChrW(0), True) 'Other, Control http://www.fileformat.info/info/unicode/category/Cc/index.htm
                .Add("["c, False)   'Punctuation, Open http://www.fileformat.info/info/unicode/category/Ps/index.htm
                .Add(" "c, True)    'Separator, Space http://www.fileformat.info/info/unicode/category/Zs/index.htm
                .Add(ChrW(&HA0), True)  'Separator, Space http://www.fileformat.info/info/unicode/category/Zs/index.htm
                .Add(ChrW(&H2028), True)    'Separator, Line http://www.fileformat.info/info/unicode/category/Zl/index.htm
                .Add("+"c, False)   'Symbol, Math http://www.fileformat.info/info/unicode/category/Sm/index.htm
                .Add("$"c, False)   'Special Asc 36
                .Add("%"c, False)   'Special Asc 37
                .Add("&"c, False)   'Special Asc 38
                .Add(","c, True)    'Special Asc 44
                .Add("-"c, True)    'Special Asc 45
                .Add("."c, True)    'Special Asc 46
                .Add(":"c, True)    'Special Asc 58
                .Add(")"c, False)   'Special Asc 41
            End With

            For Each pair As KeyValuePair(Of Char, Boolean) In source
                'Act
                Dim returnValue As Boolean = StringUtils.AlphaNumericOnly(pair.Key, True)
                Dim val As Integer = Convert.ToUInt16(pair.Key)
                'Assert
                Assert.AreEqual(pair.Value, returnValue, "Expected {0}, but received {1} for value: U+{2:X4} ({3})", pair.Value, returnValue, Convert.ToUInt16(pair.Key), pair.Key)
            Next
        End Sub

        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_AlphaNumericOnly_NothingChar_NoSpecial()
            'Arrange
            'Act
            Dim returnValue As Boolean = StringUtils.AlphaNumericOnly(Nothing, False)
            'Assert
            Assert.IsFalse(returnValue, "Expected False, but received True, for KeyChar of Nothing")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_AlphaNumericOnly_NothingChar_WithSpecial()
            'Arrange
            'NOTE that this test will PASS because the Nothing will be interpreted as a 0 (default value for Char)
            'and since 0 is a valid Control value (accepted with Specials) this will pass.
            'Act
            Dim returnValue As Boolean = StringUtils.AlphaNumericOnly(Nothing, True)
            'Assert
            Assert.IsTrue(returnValue, "Expected True, but received False, for KeyChar of Nothing")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_GenreFilter_NothingParam()
            'Arrange
            APIXML.CacheXMLs()
            'Act
            Dim returnValue As String = StringUtils.GenreFilter(Nothing)
            'Assert
            Assert.IsTrue(returnValue = String.Empty, "Expected empty string, not Nothing, nor anything longer. ({0})", returnValue)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_GenreFilter_BasicSingleItems()
            'Arrange
            APIXML.CacheXMLs()

            Dim source As New List(Of String)
            With source
                .Add("Action")
                .Add("Adult")
                .Add("Adventure")
                .Add("Animation")
                .Add("Anime")
                .Add("Manga")
                .Add("Biography")
                .Add("Bollywood")
                .Add("Eastern")
                .Add("Children")
                .Add("Christmas")
                .Add("Comedy")
                .Add("Concert")
                .Add("Crime")
                .Add("Disaster")
                .Add("Documentary")
                .Add("Drama")
                .Add("Family")
                .Add("Fantasy")
                .Add("Film Noir")
                .Add("Halloween")
                .Add("Hentai")
                .Add("History")
                .Add("Horror")
                .Add("Splatter")
                .Add("Independent")
                .Add("Interview")
                .Add("Martial Arts")
                .Add("Mini-TV Series")
                .Add("Monster")
                .Add("Music")
                .Add("Musical")
                .Add("Mystery")
                .Add("Nature")
                .Add("Post-Apocalypse")
                .Add("Religion")
                .Add("Romance")
                .Add("Sci-Fi")
                .Add("Science-Fiction")
                .Add("Sport")
                .Add("Stageplay")
                .Add("Stand Up")
                .Add("Superhero")
                .Add("Supernatural")
                .Add("Thriller")
                .Add("War")
                .Add("Western")
                .Add("Wrestling")
                .Add("Short")
            End With

            For Each entry As String In source
                'Act
                Dim returnValue As String = StringUtils.GenreFilter(entry)
                'Assert
                Assert.AreEqual(entry, returnValue, "Expected '{0}', but received '{1}'", entry, returnValue)
            Next
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_GenreFilter_BasicAllItems()
            'Arrange
            APIXML.CacheXMLs()

            'Arrange
            Dim source = "Action/Adult/Adventure/Animation/Anime/Manga/Biography/Bollywood/Eastern/Children/Christmas/Comedy/Concert/Crime/Disaster/Documentary/Drama/Family/Fantasy/Film Noir/Halloween/Hentai/History/Horror/Splatter/Independent/Interview/Martial Arts/Mini-TV Series/Monster/Music/Musical/Mystery/Nature/Post-Apocalypse/Religion/Romance/Sci-Fi/Science-Fiction/Sport/Stageplay/Stand Up/Superhero/Supernatural/Thriller/War/Western/Wrestling/Short"
            'Act
            Dim returnValue As String = StringUtils.GenreFilter(source)
            'Assert
            Assert.AreEqual(source, returnValue, "Expected '{0}', but received '{1}'", source, returnValue)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_GenreFilter_SpecialConditions()
            'Arrange
            APIXML.CacheXMLs()

            Dim source As New Dictionary(Of String, String)
            With source
                .Add("Action/Adventure", "Action/Adventure")
                .Add("action/Adventure", "Action/Adventure")
                .Add("action/AD v-E_N  tu__rE", "Action/Adventure") 'Believe it or not...
                .Add("Adventure/Action", "Adventure/Action")
                .Add("Action/ Adventure", "Action/Adventure")
                .Add("Action /Adventure", "Action/Adventure")
                .Add(" Action/Adventure", "Action/Adventure")
                .Add("Action/Adventure ", "Action/Adventure")
                .Add(" Action / Adventure ", "Action/Adventure")
                .Add("Post Apocalypse", "Post-Apocalypse")
                .Add("Post--Apocalypse", "Post-Apocalypse")
                .Add("Post-Apocalyptic", String.Empty)
                .Add("Sci-Fi/Science Fiction/Music", "Sci-Fi/Science-Fiction/Music")
                .Add("Sci_Fi/Science-Fiction/Music", "Sci-Fi/Science-Fiction/Music")
                .Add("Sci Fi/Science_Fiction/Music", "Sci-Fi/Science-Fiction/Music")
                .Add("Sci Fi/Science%Fiction/Music", "Sci-Fi/Music")    'The invalid char in Science%Fiction will cause it to be filtered out

            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.GenreFilter(entry.Key)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Expected '{0}', but received '{1}', for value '{2}'", entry.Value, returnValue, entry.Key)
            Next
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_CleanStackingMarkers_NoAsterix()
            'Arrange
            'Might need to explicitely configure AdvancedSettings DisableMultiPartMedia to True

            'APIXML.CacheXMLs()

            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps\yodeling.in.the.alps.avi", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps\yodeling.in.the.alps.avi")  'Nothing to do
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - Part 1\yodeling.in.the.alps.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - Part 1\yodeling.in.the.alps.mkv")    'Won't touch the Part 1 because of the space
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - Part 2\yodeling.in.the.alps.pt1.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - Part 2\yodeling.in.the.alps .mkv")   'Will take care of pt1 because it fits profile
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - Part3\yodeling.in.the.alps.pt2.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps \yodeling.in.the.alps .mkv")    'Should work on directory names too
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 4\yodeling.in.the.alps.cd1.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 4\yodeling.in.the.alps .mkv")
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 5\yodeling.in.the.alps.dvd-005.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 5\yodeling.in.the.alps .mkv")
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 6\yodeling.in.the.alps.disc.4.AVC.EER.Nobody.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 6\yodeling.in.the.alps .AVC.EER.Nobody.mkv")    'Should also leave other text alone
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 6\yodeling.in.the.alps.disk.4.AVC.EER.Nobody.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 6\yodeling.in.the.alps .AVC.EER.Nobody.mkv")    'Should also leave other text alone
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 7\yodeling.in.the.alps.part_9.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 7\yodeling.in.the.alps .mkv")
                .Add("c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 8\yodeling.in.the.alps.part`10.mkv", "c:\Users\Some Name\Documents\Files\Movies\Yodeling In The Alps - 8\yodeling.in.the.alps.part`10.mkv")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.CleanStackingMarkers(entry.Key)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_CleanURL()
            'Arrange

            'Act

            'Assert
            Assert.Inconclusive("Test not implemented")
        End Sub
        ''' <summary>
        ''' Internal structure to assist testing with Levenshtein.
        ''' The values are the two strings to be compared,
        ''' and the levenshtein is the expected Levenshtein result.
        ''' </summary>
        ''' <remarks>No validation, as this is all internal</remarks>
        Private Structure LevenshteinData
            Dim value1 As String
            Dim value2 As String
            Dim levenshtein As Integer


            Sub New(val1 As String, val2 As String, lev As Integer)
                value1 = val1
                value2 = val2
                levenshtein = lev
            End Sub
        End Structure
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_ComputeLevenshtein()
            'Arrange
            Dim source As New List(Of LevenshteinData)
            With source
                .Add(New LevenshteinData(Nothing, "airplane", 100))
                .Add(New LevenshteinData("airplane", Nothing, 8))
                .Add(New LevenshteinData(Nothing, Nothing, 100))
                .Add(New LevenshteinData("planes", "airplane", 4))
                .Add(New LevenshteinData("airplane", "planes", 4))
                .Add(New LevenshteinData("automobile", "automobile", 0))
                .Add(New LevenshteinData("airbender", "aIrBenDER", 0))
                .Add(New LevenshteinData("airbender", "Avatar: The Last Airbender", 17))
                .Add(New LevenshteinData("airbender", "The Last Airbender", 9))
                .Add(New LevenshteinData("Airbender", "The Last Airbender: Siege of the North", 29))
            End With

            For Each entry As LevenshteinData In source
                'Act
                Dim returnValue As Integer = StringUtils.ComputeLevenshtein(entry.value1, entry.value2)
                'Assert
                Assert.AreEqual(entry.levenshtein, returnValue, "Source <{0}> and <{1}>", entry.value1, entry.value2)
            Next
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_Decode_NothingParameter()
            'Arrange
            Dim returnValue As String = StringUtils.Decode(Nothing)
            'Assert
            Assert.AreEqual(String.Empty, returnValue, "Source was Nothing")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_Decode()
            'Arrange
            'VERY IMPORTANT NOTE: Source strings are identical to the Encode example,
            'however the value is used as the source string, and the key value is used as the expected result.
            'This is opposite of normal, but simplifies reverse testing!

            'Additional Note: Some random whitespaces in the Value should be inserted to test the decode routine's robustness
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("asdf", "YXNkZg==")
                .Add("asdf1", "YXN kZjE=")
                .Add("asdf12", "YXNkZjEy")
                .Add("asdf123", vbTab & "YXNkZjEyMw  ==")
                .Add("asdf1234", "YXNkZjEyMzQ=")
                .Add("a sdf", "YSBzZGY=")
                .Add("as" & vbTab & "df", "YXMJZGY=")
                .Add("asd" & vbCrLf & "f", "YXNkDQpm")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.Decode(entry.Value)
                'Assert
                Assert.AreEqual(entry.Key, returnValue, "Source <{0}>", entry.Value)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_Encode_NothingParameter()
            'Arrange
            Dim returnValue As String = StringUtils.Encode(Nothing)
            'Assert
            Assert.AreEqual(String.Empty, returnValue, "Source was Nothing")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_Encode()
            'Arrange
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("asdf", "YXNkZg==")
                .Add("asdf1", "YXNkZjE=")
                .Add("asdf12", "YXNkZjEy")
                .Add("asdf123", "YXNkZjEyMw==")
                .Add("asdf1234", "YXNkZjEyMzQ=")
                .Add("a sdf", "YSBzZGY=")
                .Add("as" & vbTab & "df", "YXMJZGY=")
                .Add("asd" & vbCrLf & "f", "YXNkDQpm")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.Encode(entry.Key)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterName_NothingParameter()
            'Arrange
            Dim returnValue As String = StringUtils.FilterName(Nothing)
            'Assert
            Assert.AreEqual(String.Empty, returnValue, "Source was Nothing")
        End Sub

        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterName_DoExtras_LeavePunctuation()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            'Setup the actual test
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("washington.dc", "Washington")
                .Add("1080p", "1080P")
                .Add("1080p.", "1080P")
                .Add("1080", "1080")
                .Add("1080.", "1080")
                .Add("a.1080.", "A")
                .Add("a.1080p.", "A")
                .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
                .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "Other Movie, The")
                .Add("A B.C. Movie", "B C  Movie, A")   'Note double space, because remPunct is not set
                .Add("1999 A.D.", "1999 A D")
                .Add("In 999 A.D.", "In 999 A D")
                .Add("In.1999.A.D.", "In")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.FilterName(entry.Key, True, False)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterName_DoExtras_RemovePunct()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            'Setup the actual test
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("washington.dc", "Washington")
                .Add("1080p", "1080P")
                .Add("1080p.", "1080P")
                .Add("1080", "1080")
                .Add("1080.", "1080")
                .Add("a.1080.", "A")
                .Add("a.1080p.", "A")
                .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
                .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "Other Movie The")
                .Add("A B.C. Movie", "B C Movie A")   'Note double space, because remPunct is not set
                .Add("1999 A.D.", "1999 A D")
                .Add("In 999 A.D.", "In 999 A D")
                .Add("In.1999.A.D.", "In")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.FilterName(entry.Key, True, True)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterName_NoExtras_RemovePunct()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            'Setup the actual test
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("washington.dc", "washington")
                .Add("1080p", "1080p")
                .Add("1080p.", "1080p")
                .Add("1080", "1080")
                .Add("1080.", "1080")
                .Add("a.1080.", "a")
                .Add("a.1080p.", "a")
                .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
                .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "The Other Movie")
                .Add("A B.C. Movie", "A B C Movie")   'Note double space, because remPunct is not set
                .Add("1999 A.D.", "1999 A D")
                .Add("In 999 A.D.", "In 999 A D")
                .Add("In.1999.A.D.", "In")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.FilterName(entry.Key, False, True)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterName_NoExtras_LeavePunct()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            'Setup the actual test
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("washington.dc", "washington")
                .Add("1080p", "1080p")
                .Add("1080p.", "1080p")
                .Add("1080", "1080")
                .Add("1080.", "1080")
                .Add("a.1080.", "a")
                .Add("a.1080p.", "a")
                .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
                .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "The Other Movie")
                .Add("A B.C. Movie", "A B C  Movie")   'Note double space, because remPunct is not set
                .Add("1999 A.D.", "1999 A D")
                .Add("In 999 A.D.", "In 999 A D")
                .Add("In.1999.A.D.", "In")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.FilterName(entry.Key, False, False)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_RemovePunctuation()
            'Arrange
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE 720p BluRay DTS x264 LiZard")
                .Add("The." & vbTab & " Movie.", "The Movie")
                .Add("The." & vbTab & vbTab & vbLf & " Movie.", "The Movie")
                .Add("A" & vbTab & "B", "A B")
                .Add("A B.C. Movie", "A B C Movie")
                .Add("Wow! Nice_-One", "Wow Nice_ One")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.RemovePunctuation(entry.Key)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterTokens_NothingParameter()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            'Act
            Dim result = StringUtils.FilterTokens(Nothing)
            'Assert
            Assert.AreEqual(result, String.Empty, "Unexpected result from Nothing parameter")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterTokens()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            Dim source As New Dictionary(Of String, String)
            With source
                .Add(String.Empty, String.Empty)
                .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard")
                .Add("The. Movie.", "Movie., The")
                .Add("The- Movie", "Movie, The")
                .Add("The - Movie", "- Movie, The")
                .Add("A & B", "& B, A")
                .Add("An Old Movie", "Old Movie, An")
                .Add("The A In That", "A In That, The")
                .Add("Theo And Claude", "Theo And Claude")
            End With

            For Each entry As KeyValuePair(Of String, String) In source
                'Act
                Dim returnValue As String = StringUtils.FilterTokens(entry.Key)
                'Assert
                Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
            Next

        End Sub

        '=========================================

        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterTVEpName_NothingEpName()
            'Arrange
            Dim returnValue As String = StringUtils.FilterTVEpName(Nothing, "Test Show")
            'Assert
            Assert.AreEqual(String.Empty, returnValue, "Episode name was Nothing")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterTVEpName_NothingShowName()
            'Arrange
            Dim episodeName = "Test Episode"
            Dim returnValue As String = StringUtils.FilterTVEpName(episodeName.Clone, Nothing)  'Cloned to prevent original from being modified
            'Assert
            Assert.AreEqual(episodeName, returnValue, "Show name was Nothing")
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterTVEpName_NothingEpAndShowName()
            'Arrange
            Dim returnValue As String = StringUtils.FilterTVEpName(Nothing, Nothing)
            'Assert
            Assert.AreEqual(String.Empty, returnValue, "Episode and Show name was Nothing")
        End Sub
        Private Structure Triplet
            Dim Episode As String
            Dim Show As String
            Dim Expected As String
            Public Sub New(_episode As String, _show As String, _expected As String)
                Episode = _episode
                Show = _show
                Expected = _expected
            End Sub
        End Structure
        <UnitTest>
        <TestMethod()>
        Public Sub StringUtils_FilterTVEpName_DoExtras_LeavePunctuation()
            'Arrange
            'Load the default settings
            Dim settings As Settings = New Settings()
            settings.Load()
            'Setup the actual test
            Dim source As New List(Of Triplet)
            With source
                '.Add(New Triplet("Episode Name", "Show Name", "Episode Name"))
                '.Add(New Triplet("washington.dc", "Show Name", "Washington"))
                '.Add(New Triplet("1080p", "Show Name", "1080P"))
                '.Add(New Triplet("1080p.", "Show Name", "1080P"))
                '.Add(New Triplet("1080", "Show Name", "1080"))
                '.Add(New Triplet("1080.", "Show Name", "1080"))
                '.Add(New Triplet("a.1080.", "Show Name", "A"))
                '.Add(New Triplet("a.1080p.", "Show Name", "A"))
                '.Add(New Triplet("True.Blue.720p.BluRay.DTS.x264-LiZard", "Show Name", "True Blup"))   'TODO Dekker500 - This test is WRONG!  I need to revisit to re-factor the default settings to fix this
                '.Add(New Triplet("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Show Name", "Movie Name Completp"))   'TODO Dekker500 - This test is WRONG!  I need to revisit to re-factor the default settings to fix this
                '.Add(New Triplet("The.Other.Movie.2013.WHERE.BLURAY-LDS", "Show Name", "The Other Movie"))
                '.Add(New Triplet("A B.C. Movie", "Show Name", "A B C  Movie"))   'Note double space, because remPunct is not set
                '.Add(New Triplet("1999 A.D.", "Show Name", "1999 A D"))
                '.Add(New Triplet("In 999 A.D.", "Show Name", "In 999 A D"))
                '.Add(New Triplet("In.1999.A.D.", "Show Name", "In"))
                .Add(New Triplet("Random.Show.S03E04.720p.BluRay.DTS.x264-Scene.mkv", "Random Show", "S03E04.720p.BluRay.DTS.x264-Scene.mkv"))

            End With

            For Each entry As Triplet In source
                'Act
                Dim returnValue As String = StringUtils.FilterTVEpName(entry.Episode, entry.Show, True, False)
                'Assert
                Assert.AreEqual(entry.Expected, returnValue, "Episode <{0}>, Show <{1}>", entry.Episode, entry.Show)
            Next

        End Sub
        '<UnitTest>
        '<TestMethod()>
        'Public Sub StringUtils_FilterTVEpName_DoExtras_RemovePunct()
        '    'Arrange
        '    'Load the default settings
        '    Dim settings As Settings = New Settings()
        '    settings.Load()
        '    'Setup the actual test
        '    Dim source As New Dictionary(Of String, String)
        '    With source
        '        .Add(String.Empty, String.Empty)
        '        .Add("washington.dc", "Washington")
        '        .Add("1080p", "1080P")
        '        .Add("1080p.", "1080P")
        '        .Add("1080", "1080")
        '        .Add("1080.", "1080")
        '        .Add("a.1080.", "A")
        '        .Add("a.1080p.", "A")
        '        .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
        '        .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "Other Movie The")
        '        .Add("A B.C. Movie", "B C Movie A")   'Note double space, because remPunct is not set
        '        .Add("1999 A.D.", "1999 A D")
        '        .Add("In 999 A.D.", "In 999 A D")
        '        .Add("In.1999.A.D.", "In")
        '    End With

        '    For Each entry As KeyValuePair(Of String, String) In source
        '        'Act
        '        Dim returnValue As String = StringUtils.FilterTVEpName(entry.Key, True, True)
        '        'Assert
        '        Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
        '    Next

        'End Sub
        '<UnitTest>
        '<TestMethod()>
        'Public Sub StringUtils_FilterTVEpName_NoExtras_RemovePunct()
        '    'Arrange
        '    'Load the default settings
        '    Dim settings As Settings = New Settings()
        '    settings.Load()
        '    'Setup the actual test
        '    Dim source As New Dictionary(Of String, String)
        '    With source
        '        .Add(String.Empty, String.Empty)
        '        .Add("washington.dc", "washington")
        '        .Add("1080p", "1080p")
        '        .Add("1080p.", "1080p")
        '        .Add("1080", "1080")
        '        .Add("1080.", "1080")
        '        .Add("a.1080.", "a")
        '        .Add("a.1080p.", "a")
        '        .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
        '        .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "The Other Movie")
        '        .Add("A B.C. Movie", "A B C Movie")   'Note double space, because remPunct is not set
        '        .Add("1999 A.D.", "1999 A D")
        '        .Add("In 999 A.D.", "In 999 A D")
        '        .Add("In.1999.A.D.", "In")
        '    End With

        '    For Each entry As KeyValuePair(Of String, String) In source
        '        'Act
        '        Dim returnValue As String = StringUtils.FilterTVEpName(entry.Key, False, True)
        '        'Assert
        '        Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
        '    Next

        'End Sub
        '<UnitTest>
        '<TestMethod()>
        'Public Sub StringUtils_FilterTVEpName_NoExtras_LeavePunct()
        '    'Arrange
        '    'Load the default settings
        '    Dim settings As Settings = New Settings()
        '    settings.Load()
        '    'Setup the actual test
        '    Dim source As New Dictionary(Of String, String)
        '    With source
        '        .Add(String.Empty, String.Empty)
        '        .Add("washington.dc", "washington")
        '        .Add("1080p", "1080p")
        '        .Add("1080p.", "1080p")
        '        .Add("1080", "1080")
        '        .Add("1080.", "1080")
        '        .Add("a.1080.", "a")
        '        .Add("a.1080p.", "a")
        '        .Add("Movie.Name.COMPLETE.720p.BluRay.DTS.x264-LiZard", "Movie Name COMPLETE")
        '        .Add("The.Other.Movie.2013.WHERE.BLURAY-LDS", "The Other Movie")
        '        .Add("A B.C. Movie", "A B C  Movie")   'Note double space, because remPunct is not set
        '        .Add("1999 A.D.", "1999 A D")
        '        .Add("In 999 A.D.", "In 999 A D")
        '        .Add("In.1999.A.D.", "In")
        '    End With

        '    For Each entry As KeyValuePair(Of String, String) In source
        '        'Act
        '        Dim returnValue As String = StringUtils.FilterTVEpName(entry.Key, False, False)
        '        'Assert
        '        Assert.AreEqual(entry.Value, returnValue, "Source <{0}>", entry.Key)
        '    Next

        'End Sub
    End Class
End Namespace
