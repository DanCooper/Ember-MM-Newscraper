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

Imports System.Globalization

Public Class NumUtils

    #Region "Methods"

    ''' <summary>
    ''' Convert a numerical string to single (internationally friendly method)
    ''' </summary>
    ''' <param name="sNumber">Number (as string) to convert</param>
    ''' <returns>Number as single, or 0 if any error is encountered parsing <paramref name="sNumber"/>.</returns>
    ''' <remarks>Many countries use the "," symbol to indicate a decimal (5,32 meaning 5.32).
    ''' This method first converts any existing commas into decimals before attempting to parse 
    ''' the source string. Note that this may cause an otherwise acceptable number (1,000.00) to fail
    ''' to parse.
    ''' 
    ''' 2013/11/25 Dekker500 - Refactored because original could not pass unit tests. Also converted to
    '''                        TryParse instead of just Parse because of efficiencies in avoiding Try/Catch block
    '''</remarks>
    Public Shared Function ConvertToSingle(ByVal sNumber As String) As Single
        If String.IsNullOrEmpty(sNumber) Then Return 0.0F
        sNumber = sNumber.Replace(",", ".")    ' This is to deal with those regions that use commas in place of a dot for the decimal indicator
        Dim result As Single = 0.0F
        Dim success As Boolean = Single.TryParse(sNumber, result)
        If success Then Return result
        Return 0.0F

    End Function

    #End Region 'Methods

End Class