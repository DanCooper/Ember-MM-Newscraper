﻿' ################################################################################
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
Imports NLog

Public Class NumUtils

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region

#Region "Methods"

    Private Shared cultureUS As CultureInfo = New CultureInfo("en-US", False)
    Private Shared cultureDE As CultureInfo = New CultureInfo("de-DE", False)
    Private Shared style As NumberStyles = NumberStyles.Float

    ''' <summary>
    ''' Convert a numerical string to single (internationally friendly method)
    ''' </summary>
    ''' <param name="sNumber">Number (as string) to convert</param>
    ''' <returns>Number as single, or 0 if any error is encountered parsing <paramref name="sNumber"/>.</returns>
    ''' <remarks>Many countries use the "," symbol to indicate a decimal (5,32 meaning 5.32).
    ''' This method first attempts to parse numbers using the US culture norms (comma separator, dot decimal),
    ''' then tries the GB culture norm (dot separator, comma decimal).
    ''' Note that because of cultural ambiguities, thousands separators are NOT allowed, or they may
    ''' be mis-interpreted by the parser.
    ''' 
    ''' 2013/11/25 Dekker500 - Refactored because original could not pass unit tests. Also converted to
    '''                        TryParse instead of just Parse because of efficiencies in avoiding Try/Catch block
    ''' 2014/01/06 Dekker500 - Bug discovered - User's culture affected TryParse's success. Now we compare with 
    '''                        the two major formats
    '''                            US culture norms (comma separator, dot decimal),
    '''                            GB culture norm (dot separator, comma decimal)
    '''</remarks>
    Public Shared Function ConvertToSingle(ByVal sNumber As String) As Single
        If String.IsNullOrEmpty(sNumber) Then Return 0.0F

        Dim result As Single = 0.0F
        Dim success As Boolean
        success = Single.TryParse(sNumber, style, cultureUS, result)
        If success Then Return result
        success = Single.TryParse(sNumber, style, cultureDE, result)
        If success Then Return result

        'If we got here, something went wrong
        Dim trace = New StackTrace()
        logger.Error( "Failed to convert <{0}>", sNumber)
        Return 0.0F

    End Function

#End Region 'Methods

End Class