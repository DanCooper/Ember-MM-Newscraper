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

Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports EmberAPI
Imports EmberAPI.Interfaces
Imports EmberScraperModule
Imports UnitTests
Imports EmberAPI.Enums
Imports EmberAPI.Structures
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml

Namespace EmberTests
    ''' <summary>
    ''' As an implementation of <c>EmberMovieScraperModule_Data</c>, at a minimum these tests should test the following interface items:
    ''' <code>
    '''    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef sStudio As List(Of String)) As ModuleResult
    '''    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions) As ModuleResult
    ''' </code>
    ''' </summary>
    ''' <remarks></remarks>
    <TestClass()>
    Public Class Test_IMDB_Data
        '******************************************************************************************
        '******************************************************************************************
        ' COPY ENTIRE CLASS, UPDATE THESE ENTRIES, AND GENERATE NEW TEST DATA TO TEST NEW PROVIDERS
        '******************************************************************************************
        Dim providerType As Type = GetType(IMDB_Data)
        Dim providerName As String = "IMDB_Data"
        '******************************************************************************************
        '******************************************************************************************
        '******************************************************************************************
        '******************************************************************************************


        Private scrapeOptions_All As ScrapeOptions = New ScrapeOptions With
                                       {
                                            .bCast = True,
                                            .bCert = True,
                                            .bDirector = True,
                                            .bFullCast = True,
                                            .bFullCrew = True,
                                            .bGenre = True,
                                            .bMPAA = True,
                                            .bMusicBy = True,
                                            .bOtherCrew = True,
                                            .bOutline = True,
                                            .bPlot = True,
                                            .bProducers = True,
                                            .bRating = True,
                                            .bLanguageV = True,
                                            .bLanguageA = True,
                                            .buseMPAAForFSK = True,
                                            .bRelease = True,
                                            .bRuntime = True,
                                            .bStudio = True,
                                            .bTagline = True,
                                            .bTitle = True,
                                            .bTop250 = True,
                                            .bCountry = True,
                                            .bTrailer = True,
                                            .bVotes = True,
                                            .bWriters = True,
                                            .bYear = True
                                        }
        Private Shared TestMoviePath As String = Path.Combine(Functions.AppPath, "TestMovieData")


        ''' <summary>
        ''' Common setup for all tests in this class. This will run before EVERY test case in this class
        ''' </summary>
        ''' <remarks></remarks>
        <ClassInitialize>
        Public Shared Sub ClassInit(ByVal context As TestContext)
            Directory.CreateDirectory(TestMoviePath)
        End Sub
        ''' <summary>
        ''' This method instantiates and returns the Data provider being tested.
        ''' To test different Data providers, simply copy the entire class,
        ''' update the provider type and name above, generate the baseline test data, 
        ''' and things should be good to go!
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetProvider() As EmberMovieScraperModule_Data
            Dim provider = Activator.CreateInstance(providerType)
            provider.Init(providerName)
            Return provider
        End Function

        ''' <summary>
        ''' Takes the supplied Movie and serializes the XML to the supplied filename.
        ''' Note movies are stored in <c>TestMoviePath</c>
        ''' </summary>
        ''' <param name="movie">Movie object to manipulate</param>
        ''' <param name="fileName">Destination to store the serialized Movie</param>
        ''' <remarks></remarks>
        Private Sub StoreMovieInfo(fileName As String, movie As MediaContainers.Movie)
            Dim stringMovie As String = ConvertToString(movie)

            Dim moviePath As String = Path.Combine(TestMoviePath, fileName)
            Dim returnValue As String = String.Empty
            Try
                Using myWriter As StreamWriter = New StreamWriter(moviePath)
                    myWriter.Write(stringMovie)
                    myWriter.Close()
                End Using
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Assert.Fail("Error in unit test, not necessarily in application")
            End Try

        End Sub
        ''' <summary>
        ''' Reads the stored movie information for the given movie name
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetStoredMovie(fileName As String) As String
            Dim moviePath As String = Path.Combine(TestMoviePath, fileName)
            Dim returnValue As String = String.Empty
            Try
                Using myReader As StreamReader = New StreamReader(moviePath, Encoding.UTF8, True)
                    returnValue = myReader.ReadToEnd()
                    myReader.Close()
                End Using
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Assert.Fail("Error in unit test, not necessarily in application")
            End Try
            Return returnValue
        End Function
        ''' <summary>
        ''' Takes the supplied Movie, serializes it, and returns the resulting String
        ''' </summary>
        ''' <param name="movie">Movie object to manipulate</param>
        ''' <returns>XML string representing the Movie</returns>
        ''' <remarks></remarks>
        Private Function ConvertToString(movie As MediaContainers.Movie) As String
            Dim result As String = String.Empty
            Try
                Dim utf8 As UTF8Encoding = New UTF8Encoding(False, True)    ' We need this to remove the Byte Order Mark at the beginning of the string, which would cause problems with comparisons
                Dim xmlSettings As XmlWriterSettings = New XmlWriterSettings With
                                                        {
                                                            .Encoding = utf8,
                                                            .NewLineHandling = NewLineHandling.Replace,
                                                            .Indent = True
                                                        }
                'Note: Serializer -> XmlWriter(with opts) -> MemoryStream -> String
                Using myWriter As MemoryStream = New MemoryStream()
                    Using xmlWriter As XmlWriter = xmlWriter.Create(myWriter, xmlSettings)  ' We need this step to ensure encoding is added to XML tag
                        Dim mySerializer As XmlSerializer = New XmlSerializer(movie.GetType())
                        mySerializer.Serialize(xmlWriter, movie)
                    End Using
                    'At this point, myWriter contains the Movie, encoded in XML
                    result = utf8.GetString(myWriter.ToArray())
                    myWriter.Close()
                End Using
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Assert.Fail("Error in unit test, not necessarily in application")
            End Try

            Return result
        End Function
        ''' <summary>
        ''' Utility method to get the name of the CALLING method
        ''' </summary>
        ''' <returns>Simple <c>String</c> representing the calling method's name</returns>
        ''' <remarks></remarks>
        Private Function GetMethodName() As String
            Dim frame As StackFrame = New StackFrame(1)
            Dim method As Reflection.MethodBase = frame.GetMethod()
            Return method.Name
        End Function
        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_GetMovieStudio_NothingDBMovie()
            'Arrange
            Dim movie As Structures.DBMovie = Nothing
            Dim studio As List(Of String) = New List(Of String)

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.GetMovieStudio(movie, studio)

            'Assert
            Dim resultOK As Boolean = _
                result.Cancelled AndAlso _
                Not result.breakChain
            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
        End Sub
        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_GetMovieStudio_NothingMovie()
            'Arrange
            Dim movie As Structures.DBMovie = New Structures.DBMovie()
            'Note that no movie.Movie is not defined
            Dim studio As List(Of String) = New List(Of String)

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.GetMovieStudio(movie, studio)

            'Assert
            Dim resultOK As Boolean = _
                result.Cancelled AndAlso _
                Not result.breakChain
            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
        End Sub
        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_GetMovieStudio_EmptyMovieID()
            'Arrange
            Dim movie As Structures.DBMovie = New Structures.DBMovie()
            movie.Movie = New MediaContainers.Movie()
            ' In this test, the Movie object is created, but no ID is assigned
            '            movie.Movie.IMDBID = 91949    'This is ShortCircuit from http://www.imdb.com/title/tt0091949/
            Dim studio As List(Of String) = New List(Of String)

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.GetMovieStudio(movie, studio)

            'Assert
            Dim resultOK As Boolean = _
                result.Cancelled AndAlso _
                Not result.breakChain
            Dim dataOK As Boolean = studio.Count() = 0

            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
            Assert.IsTrue(dataOK, "Studios returned were not as expected")
        End Sub

        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_GetMovieStudio_NothingStudio()
            'Arrange
            Dim movie As Structures.DBMovie = New Structures.DBMovie()
            movie.Movie = New MediaContainers.Movie()
            movie.Movie.IMDBID = 91949    'This is ShortCircuit from http://www.imdb.com/title/tt0091949/
            Dim studio As List(Of String) = Nothing

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.GetMovieStudio(movie, studio)

            'Assert
            Dim resultOK As Boolean = _
                Not result.Cancelled AndAlso _
                Not result.breakChain
            Dim dataOK As Boolean = _
                studio.Count() = 3 AndAlso _
                studio.Contains("David Foster Productions") AndAlso _
                studio.Contains("Producers Sales Organization (PSO)") AndAlso _
                studio.Contains("TriStar Pictures")

            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
            Assert.IsTrue(dataOK, "Studios returned were not as expected")
        End Sub

        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_GetMovieStudio_FullStudio()
            'Arrange
            Dim movie As Structures.DBMovie = New Structures.DBMovie()
            movie.Movie = New MediaContainers.Movie()
            movie.Movie.IMDBID = 91949    'This is ShortCircuit from http://www.imdb.com/title/tt0091949/
            Dim studio As List(Of String) = New List(Of String)
            With studio
                .Add("Test 1")
                .Add("Test 2")
                .Add("Test 3")
                .Add("Test 4")
                .Add("Test 5")
                .Add("Test 6")
                .Add("Test 7")
                .Add("Test 8")
            End With

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.GetMovieStudio(movie, studio)

            'Assert
            Dim resultOK As Boolean = _
                Not result.Cancelled AndAlso _
                Not result.breakChain
            Dim dataOK As Boolean = _
                studio.Count() = 3 AndAlso _
                studio.Contains("David Foster Productions") AndAlso _
                studio.Contains("Producers Sales Organization (PSO)") AndAlso _
                studio.Contains("TriStar Pictures")

            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
            Assert.IsTrue(dataOK, "Studios returned were not as expected")
        End Sub
        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_GetMovieStudio_MovieDoesNotExist()
            'TODO 2013/12/19 Dekker500 - This test should be changed. Wouldn't you expect a non-exsistant movie to generate a "Cancelled"?
            'Arrange
            Dim movie As Structures.DBMovie = New Structures.DBMovie()
            movie.Movie = New MediaContainers.Movie()
            movie.Movie.IMDBID = 9194884    'This movie does not exist at IMDB
            Dim studio As List(Of String) = New List(Of String)

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.GetMovieStudio(movie, studio)

            'Assert
            Dim resultOK As Boolean = _
                Not result.Cancelled AndAlso _
                Not result.breakChain
            Dim dataOK As Boolean = _
                studio.Count() = 0

            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
            Assert.IsTrue(dataOK, "Studios returned were not as expected")
        End Sub
        <UnitTest()>
        <TestMethod()>
        Public Sub Test_IMDB_Data_Scraper_HappyDay()
            'Arrange
            Dim movie As Structures.DBMovie = New Structures.DBMovie()
            movie.Movie = New MediaContainers.Movie()
            movie.Movie.IMDBID = 91949    'This is ShortCircuit from http://www.imdb.com/title/tt0091949/
            Dim scrapeType As ScrapeType = Enums.ScrapeType.SingleScrape
            Dim options As ScrapeOptions = scrapeOptions_All
            options.bTrailer = False

            'These simulate the "allowed" scrape items
            Functions.SetScraperMod(MovieModType.All, True)
            Functions.SetScraperMod(MovieModType.Trailer, False, False)

            Dim provider As EmberMovieScraperModule_Data = GetProvider()

            'Act
            Dim result As Interfaces.ModuleResult = provider.Scraper(movie, scrapeType, options)

            Dim methodName = GetMethodName()

            'VERY IMPORTANT: Following line MUST be commented out when performing test, and only uncommented when generating new data!
            'StoreMovieInfo(methodName, movie.Movie) 'Only use when generating new data!!!

            If Not File.Exists(Path.Combine(TestMoviePath, methodName)) Then
                Assert.Inconclusive("Seed file was never created")
            End If

            Dim currentMovie = ConvertToString(movie.Movie)
            Dim storedMovie = GetStoredMovie(methodName)

            'Assert
            Dim resultOK As Boolean = _
                Not result.Cancelled AndAlso _
                Not result.breakChain
            Dim dataOK As Boolean = currentMovie.Equals(storedMovie)

            Assert.IsTrue(resultOK, "Result was Cancelled:<{0}> and breakChain:<{1}>", result.Cancelled, result.breakChain)
            Assert.IsTrue(dataOK, "Studios returned were not as expected")
        End Sub

    End Class
End Namespace
