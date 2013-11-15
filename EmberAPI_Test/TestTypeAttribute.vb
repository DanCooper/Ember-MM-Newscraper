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

Namespace EmberTests

    ''' <summary>
    ''' Used to indicate that the test is a Unit Test
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UnitTestAttribute
        Inherits TestCategoryBaseAttribute
        Public Overrides ReadOnly Property TestCategories() As IList(Of String)
            Get
                Return New List(Of String)() From {"UnitTest"}
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Used to indicate that the test is an integration test, and interacts
    ''' with external systems such as the file system, or database, or web site.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IntegrationTestAttribute
        Inherits TestCategoryBaseAttribute
        Public Overrides ReadOnly Property TestCategories() As IList(Of String)
            Get
                Return New List(Of String)() From {"IntegrationTest"}
            End Get
        End Property
    End Class

End Namespace
