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
Imports UnitTests

Namespace EmberTests

    <TestClass()>
    Public Class Test_clsYouTube_Scraper

        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_GetVideoLinks_URLWithValues()
            'Arrange
            Dim scraper = New YouTube.Scraper()
            'This is for "The Hobbit: An Unexpected Journey - Official Trailer 2 [HD]"
            Dim url As String = "http://www.youtube.com/watch?v=SDnYMbYB-nU"
            'Act
            scraper.GetVideoLinks(url)
            Dim result As Integer = scraper.VideoLinks.Count()
            'Assert
            Assert.IsTrue(result > 0, "Method returned {0} results. Expected 0 results from {1}", result, url)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_GetVideoLinks_URLWithNoValues()

            'TODO Need to find one! Need a valid YouTube video URL with no download links.
            'Used to be some, but they all seem to return valid/proper results now.

            Assert.Inconclusive("Test not implemented")

            ''Arrange
            'Dim scraper = New YouTube.Scraper()
            ''This is for "Sharpay's Fabulous Adventure Official Trailer"
            'Dim url As String = "http://www.youtube.com/watch?v=DrDMaovvJzo"
            ''Act
            'scraper.GetVideoLinks(url)
            'Dim result As Integer = scraper.VideoLinks.Count()
            ''Assert
            'Assert.IsTrue(result = 0, "Method returned {0} results. Expected 0 results from {1}", result, url)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_GetVideoLinks_InvalidURL1()
            'Arrange
            Dim scraper = New YouTube.Scraper()
            Dim url As String = "http://a.ks890nf07&"
            'Act
            scraper.GetVideoLinks(url)
            Dim result As Integer = scraper.VideoLinks.Count()
            'Assert
            Assert.IsTrue(result = 0, "Method returned {0} results. Expected 0 results from {1}", result, url)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_GetVideoLinks_InvalidURL2()
            'Arrange
            Dim scraper = New YouTube.Scraper()
            Dim url As String = "a.ks890nf07&"
            'Act
            scraper.GetVideoLinks(url)
            Dim result As Integer = scraper.VideoLinks.Count()
            'Assert
            Assert.IsTrue(result = 0, "Method returned {0} results. Expected 0 results from {1}", result, url)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_GetVideoLinks_NothingParameter()
            'Arrange
            Dim scraper = New YouTube.Scraper()
            'Act
            scraper.GetVideoLinks(Nothing)
            Dim result As Integer = scraper.VideoLinks.Count()
            'Assert
            Assert.IsTrue(result = 0, "Method returned {0} results. Expected 0 results from {1}", result, "Nothing")
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_SearchOnYouTube_HasValues()
            'Arrange
            'Note that this name should be modified occasionally (annually?) to uniquely match a popular movie.
            Dim name As String = "thor dark world"
            'Act
            Dim trailers = YouTube.Scraper.SearchOnYouTube(name)
            'Assert
            Assert.IsTrue(trailers.Count > 0, "Expected some trailers (at least 1), got {0}", trailers.Count)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_SearchOnYouTube_HasNoValues()
            'Arrange
            Dim name As String = "JD*)(JGF0"
            'Act
            Dim trailers = YouTube.Scraper.SearchOnYouTube(name)
            'Assert
            Assert.IsTrue(trailers.Count = 0, "Expected no trailers, got {0}", trailers.Count)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_SearchOnYouTube_NothingParameter()
            'Arrange
            Dim name As String = Nothing
            'Act
            Dim trailers = YouTube.Scraper.SearchOnYouTube(name)
            'Assert
            Assert.IsTrue(trailers.Count = 0, "Expected no trailers, got {0}", trailers.Count)
        End Sub
        <IntegrationTest>
        <TestMethod()>
        Public Sub YouTube_Scraper_SearchOnYouTube_EmptyParameter()
            'Arrange
            Dim name As String = String.Empty
            'Act
            Dim trailers = YouTube.Scraper.SearchOnYouTube(name)
            'Assert
            Assert.IsTrue(trailers.Count = 0, "Expected no trailers, got {0}", trailers.Count)
        End Sub


    End Class


    <TestClass()>
    Public Class Test_clsYouTube_VideoLinkItemCollection

        <UnitTest>
        <TestMethod()>
        Public Sub YouTube_VideoLinkItemCollection_Add_DoesntYetExist()
            'Arrange
            Dim videoLinkItemCollection As New YouTube.YouTubeLinkItemCollection()
            Dim itemToAdd = New YouTube.VideoLinkItem()
            itemToAdd.Description = "First item to add"
            itemToAdd.URL = "Random URL"

            'Act
            videoLinkItemCollection.Add(itemToAdd)
            Dim result = videoLinkItemCollection.VideoLinks.Count()

            'Assert
            Assert.IsTrue(result = 1, "Expected 1, actual count was {0}, when adding Nothing", result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub YouTube_VideoLinkItemCollection_Add_AlreadyExists1()
            'Arrange
            Dim videoLinkItemCollection As New YouTube.YouTubeLinkItemCollection()
            Dim firstItemToAdd = New YouTube.VideoLinkItem()
            firstItemToAdd.Description = "First item to add"
            firstItemToAdd.URL = "Random URL"
            firstItemToAdd.FormatQuality = Enums.TrailerVideoQuality.OTHERS

            Dim secondItemToAdd = New YouTube.VideoLinkItem()
            secondItemToAdd.Description = "Second item to add"
            secondItemToAdd.URL = "Random URL 2"
            secondItemToAdd.FormatQuality = Enums.TrailerVideoQuality.SQ360p

            Dim duplicateItemToAdd = New YouTube.VideoLinkItem()
            duplicateItemToAdd.Description = "Duplicate item to add"
            duplicateItemToAdd.URL = "Random URL 2"
            duplicateItemToAdd.FormatQuality = Enums.TrailerVideoQuality.OTHERS

            'Act
            videoLinkItemCollection.Add(firstItemToAdd)
            videoLinkItemCollection.Add(secondItemToAdd)
            videoLinkItemCollection.Add(duplicateItemToAdd)
            Dim result = videoLinkItemCollection.Count()

            'Assert
            Assert.IsTrue(result = 2, "Expected 2, actual count was {0}, when adding Nothing", result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub YouTube_VideoLinkItemCollection_Add_AlreadyExists2()
            'Arrange
            Dim videoLinkItemCollection As New YouTube.YouTubeLinkItemCollection()
            Dim firstItemToAdd = New YouTube.VideoLinkItem()
            firstItemToAdd.Description = "First item to add"
            firstItemToAdd.URL = "Random URL"
            firstItemToAdd.FormatQuality = Enums.TrailerVideoQuality.HD1080p

            Dim secondItemToAdd = New YouTube.VideoLinkItem()
            secondItemToAdd.Description = "Second item to add"
            secondItemToAdd.URL = "Random URL 2"
            secondItemToAdd.FormatQuality = Enums.TrailerVideoQuality.HD720p

            Dim duplicateItemToAdd = New YouTube.VideoLinkItem()
            duplicateItemToAdd.Description = "Duplicate item to add"
            duplicateItemToAdd.URL = "Random URL 2"
            duplicateItemToAdd.FormatQuality = Enums.TrailerVideoQuality.HD1080p

            'Act
            videoLinkItemCollection.Add(firstItemToAdd)
            videoLinkItemCollection.Add(secondItemToAdd)
            videoLinkItemCollection.Add(duplicateItemToAdd)
            Dim result = videoLinkItemCollection.Count()

            'Assert
            Assert.IsTrue(result = 2, "Expected 2, actual count was {0}, when adding Nothing", result)
        End Sub
        <UnitTest>
        <TestMethod()>
        Public Sub YouTube_VideoLinkItemCollection_Add_NothingArgument()
            'Arrange
            Dim videoLinkItemCollection As New YouTube.YouTubeLinkItemCollection()
            Dim videolink As YouTube.VideoLinkItem

            'Act
            YouTube.YouTubeLinkItemCollection.Add(videolink)
            Dim result = videoLinkItemCollection.Count()
            'Assert
            Assert.IsTrue(result = 0, "Expected 0, actual count was {0}, when adding Nothing", result)

        End Sub


    End Class


End Namespace
