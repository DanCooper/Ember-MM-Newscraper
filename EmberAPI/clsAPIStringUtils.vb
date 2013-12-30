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
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Net

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")> 

Public Class StringUtils

#Region "Properties"
    Private Shared _internalGenreList As Dictionary(Of String, String) = Nothing
    ''' <summary>
    ''' <c>Dictionary</c> of Ember-specific Genres. 
    ''' The Key is a comparison-friendly <c>String</c> (such as "filmnoir")
    ''' and the Value is the full-text <c>String</c> (such as "Film Noir")
    ''' </summary>
    ''' <value>Dictionary of Ember-specific Genres</value>
    ''' <returns><c>Dictionary</c> of Ember's internally-defined genres</returns>
    ''' <remarks>This singleton removes the need to re-generate this list every time foreign genre lists
    ''' are validated against Ember's internal genre list.</remarks>
    Friend Shared ReadOnly Property InternalGenreList As Dictionary(Of String, String)
        Get
            If String.IsNullOrEmpty(Master.eSettings.GenreFilter) Then
                'NOTE: This is just a dummy. Next call we will try to define it again, in case the user has changed the settings
                Return New Dictionary(Of String, String)
            End If
            If _internalGenreList Is Nothing Then
                If Not String.IsNullOrEmpty(Master.eSettings.GenreFilter) Then
                    'Dim fGenres() As String = APIXML.GetGenreListString     'List of Ember's configured Genres
                    'convert EMBER Genres to comparable strings, by removing underscores, dashes, and spaces
                    _internalGenreList = GenerateNeutralList(APIXML.GetGenreListString)
                End If
            End If
            Return _internalGenreList
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Determines whether the supplied <c>Char</c> is alphanumeric or not. Special characters are optionally allowed as well.
    ''' </summary>
    ''' <param name="KeyChar">The <c>Char</c> to test</param>
    ''' <param name="AllowSpecial"><c>Boolean</c> flag to indicate whether special characters should be allowed. 
    ''' These include control characters, whitespace, comma, dash, period</param>
    ''' <returns><c>True</c> if <paramref name="KeyChar"/> is an alphanumeric (or special if <paramref name="AllowSpecial"/> is <c>True</c>)</returns>
    ''' <remarks>Alphanumeric is described by Unicode http://www.fileformat.info/info/unicode/category/index.htm
    ''' Specifically: 
    ''' <list>
    '''   <item>UppercaseLetter</item>
    '''   <item>LowercaseLetter</item>
    '''   <item>TitlecaseLetter</item>
    '''   <item>ModifierLetter</item>
    '''   <item>OtherLetter</item>
    '''   <item>DecimalDigitNumber</item>
    ''' </list>
    ''' 
    ''' 2013/11/12 Dekker500 - Refactored. Reversed output to match result expected from method name. Also updated references (all from frmMain) to match.
    ''' </remarks>
    Public Shared Function AlphaNumericOnly(ByVal KeyChar As Char, Optional ByVal AllowSpecial As Boolean = False) As Boolean
        If Char.IsLetterOrDigit(KeyChar) OrElse (AllowSpecial AndAlso (Char.IsControl(KeyChar) OrElse _
        Char.IsWhiteSpace(KeyChar) OrElse Asc(KeyChar) = 44 OrElse Asc(KeyChar) = 45 OrElse Asc(KeyChar) = 46 OrElse Asc(KeyChar) = 58)) Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Transforms the supplied <c>String</c> into one that simplifies comparison 
    ''' by removing leading/trailing white space, underscores, dashes, and spaces, 
    ''' and converting to lowercase.
    ''' </summary>
    ''' <param name="source">Source <c>String</c></param>
    ''' <returns><c>String</c> that has been transformed</returns>
    ''' <remarks>Though neutralizing the string can be taken to a much greater extreme 
    ''' (removing all special characters like @, #, etc), the intended purpose of this
    ''' method is to clean the genre names returned by IMDB, such as those listed 
    ''' here: http://www.imdb.com/search/title
    ''' </remarks>
    Friend Shared Function GenerateNeutralString(source As String) As String
        Return source.Trim.Replace("_", String.Empty).Replace("-", String.Empty).Replace(" ", String.Empty).ToLower
    End Function
    Friend Shared Function GenerateNeutralList(source As String()) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        For Each blub In source
            result.Add(GenerateNeutralString(blub), blub)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Converts the supplied /-separated string of genres and returns an equivalent
    ''' /-separated string of genres, but using Ember's internal genre list
    ''' </summary>
    ''' <param name="aGenres"><c>String</c> of genres, separated by a forward-slash (/)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' This method will take genres from the parameter (such as IMDB's Film-Noir) and return Ember's 
    ''' internal representation (Film Noir). It does this by striping special characters from both the 
    ''' supplied list of Genres, and Ember's internal list of Genres before performing the comparison.
    ''' 
    ''' 2013/11/13 Dekker500 - Extracted code to simplify strings into GenerateNeutralString to avoid duplicate code and its maintenance evils
    ''' </remarks>
    Public Shared Function GenreFilter(aGenres As String) As String

        If Not String.IsNullOrEmpty(Master.eSettings.GenreFilter) Then
            Dim sGenres() As String = Strings.Split(aGenres, "/")   'List of supplied Genres from the parameter
            'Dim fGenres() As String = APIXML.GetGenreListString     'List of Ember's configured Genres
            Dim rGenres As New List(Of String)  'List of Ember Genres to return

            'Cocotus, 20130930,http://bugs.embermediamanager.org/thebuggenie/embermediamanager/issues/24 More robust Genre matching, should be working for IMDB genres like Game-Show, Talk-Show, Film-Noir 
            'convert genres scraped from IMDB to comparable strings by removing underscores, dashes, and spaces
            Dim comparelistsGenres = GenerateNeutralList(sGenres)

            'now look for matches with Ember's internal genre list...
            Dim toAdd As String = String.Empty
            For Each candidate In comparelistsGenres
                If InternalGenreList.TryGetValue(candidate.Key, toAdd) Then
                    rGenres.Add(toAdd)
                Else
                    Master.eLog.Error(GetType(StringUtils), String.Format("Unhandled genre encountered: {0}", candidate.Value), New StackTrace(True).ToString(), "Error")
                End If
            Next

            If rGenres.Count > 0 Then
                Dim tGenres = Strings.Join(rGenres.ToArray, "/").Trim
                Return tGenres
            Else
                Return String.Empty
            End If
        Else
            Return aGenres
        End If

    End Function
    ''' <summary>
    ''' When given a valid path to a video/media file, return the path but without stacking markers.
    ''' </summary>
    ''' <param name="sPath"><c>String</c> path to clean</param>
    ''' <param name="Asterisk">If <c>True</c>, any stacking markers will be replaced with an asterix (*). If <c>False</c>, stacking markers will be replace by a space ( )</param>
    ''' <returns>The <c>String</c> path with the stacking markers removed, and replaced with a (space) or an asterix (*)</returns>
    ''' <remarks>Stacking markers are found using a regex similar to this:
    '''          "[\s_\-\.]+\(?(cd|dvd|p(?:ar)?t|dis[ck])+[_\-\.]?[0-9]+\)?"
    ''' Markers are identified with a prefix, an optional separator, followed by a number.
    ''' Therefore the following examples would all be replaced:
    ''' <list>
    '''   <item>cd1</item>
    '''   <item>cd-1</item>
    '''   <item>dvd_2</item>
    '''   <item>p3</item>
    '''   <item>pt4</item>
    '''   <item>part.5</item>
    '''   <item>disc-6</item>
    '''   <item>disk_73</item>
    ''' </list>
    ''' Note that text after the stacking marker are left untouched.
    ''' </remarks>
    Public Shared Function CleanStackingMarkers(ByVal sPath As String, Optional ByVal Asterisk As Boolean = False) As String
        'Don't do anything if DisableMultiPartMedia is True
        If AdvancedSettings.GetBooleanSetting("DisableMultiPartMedia", False) Then Return sPath
        If String.IsNullOrEmpty(sPath) Then Return String.Empty
        Dim pattern As String = AdvancedSettings.GetSetting("DeleteStackMarkers", "[\s_\-\.]+\(?(cd|dvd|p(?:ar)?t|dis[ck])+[_\-\.]?[0-9]+\)?")
        Dim replacement = If(Asterisk, "*", " ")
        Dim sReturn As String = Regex.Replace(sPath, pattern, replacement, RegexOptions.IgnoreCase)
        If Not sReturn.Trim = sPath.Trim Then
            'Replace any double white space by a single white space
            sReturn = Regex.Replace(sReturn, "\s\s(\s+)?", " ").Trim
        End If
        Return sReturn.Trim
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sURL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CleanURL(ByVal sURL As String) As String
        'TODO 2013/11/12 Dekker500 - Consider removing this method as it is not being referenced
        If sURL.ToLower.Contains("imgobject.com") Then
            Dim tURL As String = String.Empty
            Dim i As Integer = sURL.IndexOf("/posters/")
            If i >= 0 Then tURL = sURL.Substring(i + 9)
            i = sURL.IndexOf("/backdrops/")
            If i >= 0 Then tURL = sURL.Substring(i + 11)
            'tURL = sURL.Replace("http://images.themoviedb.org/posters/", String.Empty)
            'tURL = tURL.Replace("http://images.themoviedb.org/backdrops/", String.Empty)
            '$$ to sort first
            sURL = String.Concat("$$[imgobject.com]", tURL)
        Else
            sURL = TruncateURL(sURL, 40, True)
        End If
        Return sURL.Replace(":", "$c$").Replace("/", "$s$")
    End Function
    ''' <summary>
    ''' Determine the Levenshtein Distance between the two supplied strings.
    ''' In essence, it indicates how closely matched two strings are.
    ''' </summary>
    ''' <param name="s">First <c>String</c> to compare</param>
    ''' <param name="t">Second <c>String</c> to compare</param>
    ''' <returns>The number of single-character changes it would require to transform 
    ''' the first string into the second string</returns>
    ''' <remarks>For a description of Levenshtein Distance, please refer to: http://en.wikipedia.org/wiki/Levenshtein_distance
    ''' In brief, it generates a number representing the number of single-character changes it would require
    ''' to transform the source string to the transformed string. e.g. kitten -> sitting = 3
    ''' Note that the comparison is not case sensitive.
    '''  
    ''' 2013/11/14 Dekker500 
    '''      - Fixed bug by forcing Levenshtein to ignore case. It appears that case differences would SOMETIMES (but not always) appear as a difference</remarks>
    '''      - Fixed bug where empty/Nothing parameters would cause an exception
    Public Shared Function ComputeLevenshtein(ByVal s As String, ByVal t As String) As Integer
        'Ensure source parameters are sane
        If s Is Nothing Then
            Return 100
        End If
        If t Is Nothing Then
            Return s.Length()
        End If

        s = s.ToLower()
        t = t.ToLower()

        Dim n As Integer = s.Length
        Dim m As Integer = t.Length
        Dim d As Integer(,) = New Integer(n, m) {}

        Dim i As Integer = 0
        While i <= n
            d(i, 0) = System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While

        Dim j As Integer = 0
        While j <= m
            d(0, j) = System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
        End While

        Dim cost As Integer = 0
        For k As Integer = 1 To n
            For l As Integer = 1 To m
                cost = If((t(l - 1) = s(k - 1)), 0, 1)

                d(k, l) = Math.Min(Math.Min(d(k - 1, l) + 1, d(k, l - 1) + 1), d(k - 1, l - 1) + cost)
            Next
        Next
        Return d(n, m) - 1
    End Function
    ''' <summary>
    ''' Decode the supplied Base64 <c>String</c> from a standard ASCII <c>String</c> (a.k.a. uudecode)
    ''' </summary>
    ''' <param name="encText">Encoded Base64 <c>String</c></param>
    ''' <returns>Standard text <c>String</c>, or Empty string if argument was Nothing</returns>
    ''' <remarks>http://en.wikipedia.org/wiki/Base64
    ''' Note that the decode function is tolerant of whitespace embedded within
    ''' the string to be decoded. This is because Base64 only considers the 64 text characters as relevant,
    ''' and ignores all else.</remarks>
    Public Shared Function Decode(ByVal encText As String) As String
        If String.IsNullOrEmpty(encText) Then Return String.Empty
        Try
            Dim dByte() As Byte
            dByte = System.Convert.FromBase64String(encText)
            Dim decText As String
            decText = System.Text.Encoding.ASCII.GetString(dByte)
            Return decText
        Catch
        End Try
        Return String.Empty
    End Function
    ''' <summary>
    ''' Encode the supplied <c>String</c> into a Base64 <c>String</c> (a.k.a. uudencode)
    ''' </summary>
    ''' <param name="decText">Text <c>String</c> to be decoded</param>
    ''' <returns>Base64 <c>String</c>, or Empty string if argument was Nothing</returns>
    ''' <remarks>http://en.wikipedia.org/wiki/Base64</remarks>
    Public Shared Function Encode(ByVal decText As String) As String
        If String.IsNullOrEmpty(decText) Then Return String.Empty
        Dim eByte() As Byte
        ReDim eByte(decText.Length)
        eByte = System.Text.Encoding.ASCII.GetBytes(decText)
        Dim encText As String
        encText = System.Convert.ToBase64String(eByte)
        Return encText
    End Function
    ''' <summary>
    ''' Cleans up a <c>String</c> name by applying the given list of regex filters.
    ''' </summary>
    ''' <param name="name">The <c>String</c> to modify</param>
    ''' <param name="filters">The <c>List</c> of regex expressions to apply. Note that matches are replaced by <c>String.Empty</c>, with the exception to expressions containing [->] which replace values on the left by values on the right (such as ".[->]-" which would replace periods with dashes).</param>
    ''' <returns><c>String</c> name that has had the given regex entries applied. 
    ''' <c>String.Empty</c> is returned if the name is empty or Nothing.
    ''' The value of <paramref name="name"/> is returned if no filter is passed</returns>
    ''' <remarks></remarks>
    Friend Shared Function ApplyFilters(ByVal name As String, ByRef filters As List(Of String)) As String
        If String.IsNullOrEmpty(name) Then Return String.Empty
        If filters Is Nothing OrElse filters.Count = 0 Then Return name

        Dim strSplit() As String
        Try
            'run through each of the custom filters
            For Each Str As String In filters
                If Str.IndexOf("[->]") > 0 Then
                    strSplit = Strings.Split(Str, "[->]")
                    name = Strings.Replace(name, Regex.Match(name, strSplit.First).ToString, strSplit.Last)
                Else
                    name = Strings.Replace(name, Regex.Match(name, Str).ToString, String.Empty)
                End If
                'everything was already filtered out, return an empty string
                If String.IsNullOrEmpty(name) Then Return String.Empty
            Next
            Return name.Trim
        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Name: " & name & " generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
        End Try
        Return String.Empty
    End Function
    ''' <summary>
    ''' Cleans up a name by stripping it down to the basic title with no additional decorations.
    ''' </summary>
    ''' <param name="movieName">The <c>String</c> movie name to clean</param>
    ''' <param name="doExtras">If <c>True</c>, consider optional cleanups such as changing to Title Case, 
    ''' and re-positioning starting [The/A/An] to the end.</param>
    ''' <param name="remPunct">If <c>True</c> remove any non-word character [^a-zA-Z0-9_]
    ''' and duplicate whitespaces, replacing them all with a simple space </param>
    ''' <returns>The filtered name as a <c>String</c></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterName(ByVal movieName As String, Optional ByVal doExtras As Boolean = True, Optional ByVal remPunct As Boolean = False) As String
        If String.IsNullOrEmpty(movieName) Then Return String.Empty

        movieName = ApplyFilters(movieName, Master.eSettings.FilterCustom)

        movieName = CleanStackingMarkers(movieName.Trim)

        'Convert String To Proper Case
        If Master.eSettings.ProperCase AndAlso doExtras Then
            movieName = ProperCase(movieName)
        End If

        If doExtras Then movieName = FilterTokens(movieName.Trim)
        If remPunct Then movieName = RemovePunctuation(movieName.Trim)

        Return movieName.Trim
    End Function
    ''' <summary>
    ''' Scan the <c>String</c> title provided, and if it starts with one of the pre-defined
    ''' sort tokens (<c>Master.eSettings.SortTokens"</c>) then remove it from the front
    ''' of the string and move it to the end after a comma.
    ''' </summary>
    ''' <param name="sTitle"><c>String</c> to clean up</param>
    ''' <returns><c>String</c> with any defined sort tokens moved to the end</returns>
    ''' <remarks>This function will take a string such as "The Movie" and return "Movie, The".
    ''' The default tokens are:
    '''  <list>
    '''    <item>a</item>
    '''    <item>an</item>
    '''    <item>the</item>
    ''' </list>
    ''' Once the first token is found and moved, no further search is made for other tokens.</remarks>
    Public Shared Function FilterTokens(ByVal sTitle As String) As String
        If String.IsNullOrEmpty(sTitle) Then Return String.Empty
        Dim newTitle As String = sTitle
        If Master.eSettings.SortTokens.Count > 0 Then
            Dim tokenContents As String
            Dim onlyTokenFromTitle As RegularExpressions.Match
            Dim titleWithoutToken As String
            For Each sToken As String In Master.eSettings.SortTokens
                Try
                    If Regex.IsMatch(sTitle, String.Concat("^", sToken), RegexOptions.IgnoreCase) Then
                        tokenContents = Regex.Replace(sToken, "\[(.*?)\]", String.Empty)
                        onlyTokenFromTitle = Regex.Match(sTitle, String.Concat("^", tokenContents), RegexOptions.IgnoreCase)
                        titleWithoutToken = Regex.Replace(sTitle, String.Concat("^", sToken), String.Empty, RegexOptions.IgnoreCase).Trim
                        newTitle = String.Format("{0}, {1}", titleWithoutToken, onlyTokenFromTitle.Value).Trim

                        'newTitle = String.Format("{0}, {1}", Regex.Replace(sTitle, String.Concat("^", sToken), String.Empty, RegexOptions.IgnoreCase).Trim, Regex.Match(sTitle, String.Concat("^", Regex.Replace(sToken, "\[(.*?)\]", String.Empty)), RegexOptions.IgnoreCase)).Trim
                        Exit For
                    End If
                Catch ex As Exception
                    Master.eLog.Error(GetType(StringUtils), "Title: " & sTitle & " generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
                End Try
            Next
        End If
        Return newTitle.Trim
    End Function
    ''' <summary>
    ''' Cleans up a name by stripping it down to the basic title with no additional decorations.
    ''' </summary>
    ''' <param name="TVEpName">The <c>String</c> TV Episode name to clean</param>
    ''' <param name="TVShowName">The <c>String</c> EV Show name to clean</param>
    ''' <param name="doExtras">If <c>True</c>, consider optional cleanups such as changing to Title Case</param>
    ''' <param name="remPunct">If <c>True</c> remove any non-word character [^a-zA-Z0-9_]
    ''' and duplicate whitespaces, replacing them all with a simple space </param>
    ''' <returns>The filtered name as a <c>String</c></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterTVEpName(ByVal TVEpName As String, ByVal TVShowName As String, Optional ByVal doExtras As Boolean = True, Optional ByVal remPunct As Boolean = False) As String
        Try

            If String.IsNullOrEmpty(TVEpName) Then Return String.Empty
            TVEpName = TVEpName.Trim
            TVEpName = ApplyFilters(TVEpName, Master.eSettings.EpFilterCustom)
            TVEpName = CleanStackingMarkers(TVEpName)

            'remove the show name from the episode name
            If Not String.IsNullOrEmpty(TVShowName) Then TVEpName = Strings.Replace(TVEpName, TVShowName.Trim, String.Empty, 1, -1, CompareMethod.Text)

            'Convert String To Proper Case
            If Master.eSettings.EpProperCase AndAlso doExtras Then
                TVEpName = ProperCase(TVEpName.Trim)
            End If

            'TODO Dekker500 Why are we not using this next line (FilterTokens)?
            'If doExtras Then TVEpName = FilterTokens(TVEpName.Trim)
            If remPunct Then TVEpName = RemovePunctuation(TVEpName.Trim)


        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), ex.Message, ex.StackTrace, "Error")
        End Try
        Return TVEpName.Trim
    End Function
    ''' <summary>
    ''' Cleans up a name by stripping it down to the basic title with no additional decorations.
    ''' </summary>
    ''' <param name="TVShowName">The <c>String</c> TV Show name to clean</param>
    ''' <param name="doExtras">If <c>True</c>, consider optional cleanups such as changing to Title Case</param>
    ''' <param name="remPunct">If <c>True</c> remove any non-word character [^a-zA-Z0-9_]
    ''' and duplicate whitespaces, replacing them all with a simple space </param>
    ''' <returns>The filtered name as a <c>String</c></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterTVShowName(ByVal TVShowName As String, Optional ByVal doExtras As Boolean = True, Optional ByVal remPunct As Boolean = False) As String
        '//
        ' Clean all the crap out of the name
        '\\
        If String.IsNullOrEmpty(TVShowName) Then Return String.Empty
        TVShowName = TVShowName.Trim
        TVShowName = ApplyFilters(TVShowName, Master.eSettings.ShowFilterCustom)
        'TVShowName = CleanStackingMarkers(TVShowName)

        'Convert String To Proper Case
        If Master.eSettings.ShowProperCase AndAlso doExtras Then
            TVShowName = ProperCase(TVShowName)
        End If

        'TODO Dekker500 Why are we not using this next line (FilterTokens)?
        'If doExtras Then TVEpName = FilterTokens(TVEpName.Trim)
        If remPunct Then TVShowName = RemovePunctuation(CleanStackingMarkers(TVShowName.Trim))

        Return TVShowName.Trim
    End Function
    ''' <summary>
    ''' Removes the four-digit year from the given <c>String</c>
    ''' </summary>
    ''' <param name="sString"><c>String</c> from which to strip the year</param>
    ''' <returns>Source <c>String</c> but without the year</returns>
    ''' <remarks>The year can only be 4 digits. More or less digits and the string won't be modified.
    ''' Opening or closing bracket is optional.
    ''' Date must be preceeded by space, underscore, period, or dash
    ''' NOTE: Closing bracket is required, otherwise 5-digit (or longer) numbers would get truncated</remarks>
    Public Shared Function FilterYear(ByVal sString As String) As String
        If String.IsNullOrEmpty(sString) Then Return String.Empty
        Return Regex.Replace(sString, "([ _.-]\(?\d{4}\))?", String.Empty).Trim
    End Function
    ''' <summary>
    ''' For a given <c>Integer</c> season number, determine the appropriate season text
    ''' </summary>
    ''' <param name="sSeason"><c>Integer</c> season value. Valid values are 0 or higher. Negatives evaluate to Unknonw</param>
    ''' <returns><c>String</c> title appropriate for the season</returns>
    ''' <remarks>For <paramref name="sSeason"/> greater than 0, evaluates to (regional equivalent of) "Season XX" where XX is a 0-padded number.
    ''' For 0, returns equivalent of "Season Specials".
    ''' For less than 0, returns equivalent of "Unknown"</remarks>
    Public Shared Function FormatSeasonText(ByVal sSeason As Integer) As String
        If sSeason > 0 Then
            Return String.Concat(Master.eLang.GetString(650, "Season"), " ", sSeason.ToString.PadLeft(2, Convert.ToChar("0")))
        ElseIf sSeason = 0 Then
            Return Master.eLang.GetString(655, "Season Specials")
        Else
            Return Master.eLang.GetString(138, "Unknown")
        End If
    End Function
    ''' <summary>
    ''' Converts a string to an HTML-encoded string.
    ''' </summary>
    ''' <param name="stext"></param>
    ''' <returns>An encoded <c>String</c></returns>
    ''' <remarks>If characters such as blanks and punctuation are passed in an HTTP stream, 
    ''' they might be misinterpreted at the receiving end. HTML encoding converts characters 
    ''' that are not allowed in HTML into character-entity equivalents; 
    ''' HTML decoding reverses the encoding. For example, when embedded in a block of text, 
    ''' the characters for "less-than" and "greater-than" are encoded as &lt; and &gt; for HTTP transmission.
    ''' NOTE that this implementation is non-standard in that it can handle entries with characters above 0xFF</remarks>
    Public Shared Function HtmlEncode(ByVal stext As String) As String
        If String.IsNullOrEmpty(stext) Then Return String.Empty
        Try
            Dim chars = Web.HttpUtility.HtmlEncode(stext).ToCharArray()

            'Now do some extra magic to handle characters above 0xFF
            Dim result = New StringBuilder(stext.Length + Convert.ToInt16(stext.Length * 0.1))

            For Each c As Char In chars
                Dim value As Integer = Convert.ToInt32(c)
                If (value > 127) Then
                    result.AppendFormat("&#{0};", value)
                Else
                    result.Append(c)
                End If
            Next
            Return result.ToString()
        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Input <" & stext & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
        End Try

        'If we get here, something went wrong.
        Return String.Empty
    End Function
    ''' <summary>
    ''' Determine whether the given string represents a file that needs to be treated as if it is stacked (single media in multiple files)
    ''' If the system setting "DisableMultiPartMedia" is False, then always return False
    ''' </summary>
    ''' <param name="sName"><c>String</c> to evaluate</param>
    ''' <param name="VTS">If <c>True</c> then DVD file structure stacking is also considered. Default is <c>False</c></param>
    ''' <returns><c>True</c> if the string represents a stacked file, or <c>False</c> otherwise</returns>
    ''' <remarks>A stacked file is one that appears to be a part of a series of files that belong together.
    ''' Examples would be "filename.cd1.1080p.avi", "movie.part1.mkv" or "film.disc.1.iso".
    ''' A special case of stacking is the DVD file structure, which has segments in a format such as:
    ''' "VTS_01_0.VOB", "VTS_03_2.VOB", etc.
    ''' </remarks>
    Public Shared Function IsStacked(ByVal sName As String, Optional ByVal VTS As Boolean = False) As Boolean
        If String.IsNullOrEmpty(sName) Then Return False
        If AdvancedSettings.GetBooleanSetting("DisableMultiPartMedia", False) Then Return False
        Dim sCheckStackMarkers As String = AdvancedSettings.GetSetting("CheckStackMarkers", "[\s_\-\.]+\(?(cd|dvd|p(?:ar)?t|dis[ck])+[_\-\.]?[0-9]+\)?")
        Try
            Dim bReturn As Boolean = Regex.IsMatch(sName, sCheckStackMarkers, RegexOptions.IgnoreCase)
            If VTS And Not bReturn Then
                bReturn = Regex.IsMatch(sName, "^vts_[0-9]+_[0-9]+", RegexOptions.IgnoreCase)
            End If
            Return bReturn
        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Input <" & sName & "><" & VTS & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
        End Try

        'If we get here, something went wrong.
        Return False
    End Function
    ''' <summary>
    ''' Determine whether the format of the supplied URL is valid. No actual Internet query is made to see if
    ''' the URL is actually responsive.
    ''' </summary>
    ''' <param name="sToCheck"><c>String</c> URL to check</param>
    ''' <returns><c>True</c> if the URL is properly formatted, <c>False</c> otherwise</returns>
    ''' <remarks>This is not a thoroughly exhaustive check, but is instead a suitably-acceptable sanity check
    ''' for user-supplied URLs.
    ''' </remarks>
    Public Shared Function isValidURL(ByVal sToCheck As String) As Boolean
        If String.IsNullOrEmpty(sToCheck) Then Return False

        Dim validatedUri As Uri = Nothing
        Dim parsedOK = Uri.TryCreate(sToCheck, UriKind.Absolute, validatedUri)
        If parsedOK Then
            If validatedUri.Scheme = Uri.UriSchemeHttp OrElse validatedUri.Scheme = Uri.UriSchemeHttps Then
                Return True
            End If
        End If
        Return False

        'Oldest Return Regex.IsMatch(sToCheck, "^((ht|f)tps?\:\/\/|~\/|\/)?(\w+:\w+@)?(([-\w]+\.)+(com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|travel|[a-z]{2}))\/", RegexOptions.IgnoreCase)
        'Old Return Regex.IsMatch(sToCheck, "[a-zA-Z]{3,}://[a-zA-Z0-9\.]+/*[a-zA-Z0-9/\\%_.]*\?*[a-zA-Z0-9/\\%_.=&amp;]*")
        'Alternate method using regex.
        'Static expression As String = "^(http(?:s)?\:\/\/[a-zA-Z0-9\-]+(?:\.[a-zA-Z0-9\-]+)*\.[a-zA-Z]{2,6}(?:\/?|(?:\/[\w\-]+)*)(?:\/?|\/\w+\.[a-zA-Z]{2,4}(?:\?[\w]+\=[\w\-]+)?)?(?:\&[\w]+\=[\w\-]+)*)$"
        'Try
        '    Return Regex.IsMatch(sToCheck, expression, RegexOptions.IgnoreCase)
        'Catch ex As Exception
        '    Master.eLog.Error(GetType(StringUtils),"Input <" & sToCheck & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
        'End Try

        ''If we get here, something went wrong.
        'Return False
    End Function
    ''' <summary>
    ''' Determines whether the supplied character is valid for a numeric-only field such as a text-box.
    ''' </summary>
    ''' <param name="KeyChar"><c>Char</c> to evaluate</param>
    ''' <param name="isIP"></param>
    ''' <returns><c>False</c> if <paramref name="KeyChar"/> is something that should be kept (and processed by the
    ''' underlying control) or <c>True</c> if it should be ignored.</returns>
    ''' <remarks>Intended to be used when determining whether a textbox or equivalent numeric-only field should
    ''' handle a keypress or not, this method returns <c>True</c> when the key should be ignored and not processed,
    ''' and <c>False</c> when it should be allowed to pass and be processed.
    ''' </remarks>
    Public Shared Function NumericOnly(ByVal KeyChar As Char, Optional ByVal isIP As Boolean = False) As Boolean
        'TODO Dekker500 - This method is horribly named. It should be something like "IsInvalidNumericChar". Also, why are we allowing control chars, whitespace, or period?
        If Char.IsNumber(KeyChar) OrElse Char.IsControl(KeyChar) OrElse Char.IsWhiteSpace(KeyChar) OrElse (isIP AndAlso Asc(KeyChar) = 46) Then
            Return False
        Else
            Return True
        End If
    End Function
    ''' <summary>
    ''' Converts the supplied <c>String</c> to title-case, and converts certain keywords to uppercase
    ''' </summary>
    ''' <param name="sString"><c>String</c> to modify</param>
    ''' <returns>Converted <c>String</c>. It is always Trimmed</returns>
    ''' <remarks>Converts <paramref name="sString"/> to title-case (first char of each word is upper-case) and certain keywords are uppercase.
    ''' Note that if a problem is encountered processing the string, the source string is returned.</remarks>
    Public Shared Function ProperCase(ByVal sString As String) As String
        If String.IsNullOrEmpty(sString) Then Return String.Empty
        Dim sReturn As String = String.Empty

        Try
            sReturn = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sString)
            Dim toUpper As String = AdvancedSettings.GetSetting("ToProperCase", "\b(hd|cd|dvd|bc|b\.c\.|ad|a\.d\.|sw|nw|se|sw|ii|iii|iv|vi|vii|viii|ix|x)\b")

            Dim mcUp As MatchCollection = Regex.Matches(sReturn, toUpper, RegexOptions.IgnoreCase)
            For Each M As Match In mcUp
                sReturn = sReturn.Replace(M.Value, Strings.StrConv(M.Value, VbStrConv.Uppercase))
            Next

        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Source of <" & sString & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
            'Return the source string and move along
            sReturn = sString.Trim
        End Try

        Return sReturn.Trim
    End Function
    ''' <summary>
    ''' Remove any non-word characters, and repace all whitespace with a simple single space
    ''' </summary>
    ''' <param name="sString">Source <c>String</c> to work on</param>
    ''' <returns><c>String</c> that has been stripped of punctuation as described</returns>
    ''' <remarks>
    ''' The cleaning is done in this sequence:
    '''   <list>
    '''      <item>Remove non-word characters [^a-zA-Z0-9_] and replace with a space,</item>
    '''      <item>Convert any double-whitespace [\f\n\r\t\v\u00A0\u2028\u2029\space] to a space</item>
    '''   </list>
    ''' NOTE: By its nature, it does not handle unusual UNICODE characters!
    ''' </remarks>
    Public Shared Function RemovePunctuation(ByVal sString As String) As String
        If String.IsNullOrEmpty(sString) Then Return String.Empty
        Dim sReturn As String = String.Empty

        Try
            sReturn = Regex.Replace(sString, "\W", " ")
            'TODO Dekker500 - This used to be "sReturn.ToLower", but didn't make sense why it did... Investigate up the chain! (What does the case have to do with punctuation anyway???)
            sReturn = Regex.Replace(sReturn, "\s\s(\s+)?", " ")
        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Source of <" & sString & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
            'Return the source string and move along
            sReturn = sString
        End Try
        Return sReturn.Trim
    End Function
    ''' <summary>
    ''' Converts a string indicating a size into an actual <c>Size</c> object
    ''' </summary>
    ''' <param name="sString"><c>String</c> to parse for the size (WIDTHxHeight format)</param>
    ''' <returns>A valid <c>Size</c> object. Will have 0 width and 0 height if an error was encountered,
    ''' otherwise will have width and height as indicated by the supplied string</returns>
    ''' <remarks>A sample source string is "4x3" which is converted to Width of 4, Height of 3,
    ''' or "16x9" which is converted to 16 width and 9 height.
    '''  
    ''' 2013/11/21 Dekker500 - Modified so it changes input strin ToLowerInvariant before parsing and splitting to overcome inconsistant behaviour with upper-case "x" in source string
    ''' </remarks>
    Public Shared Function StringToSize(ByVal sString As String) As Size
        'TODO Dekker500 - This can be made more robust by trimming whitespace within the string, so we could accept "16 x 9" 
        If String.IsNullOrEmpty(sString) Then Return New Size(0, 0)

        Try
            Dim source As String = sString.ToLowerInvariant
            If Regex.IsMatch(source, "^[0-9]+x[0-9]+$", RegexOptions.IgnoreCase) Then
                Dim SplitSize() As String = Strings.Split(source, "x")
                Return New Size(Convert.ToInt32(SplitSize(0)), Convert.ToInt32(SplitSize(1)))
            End If
        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Source of <" & sString & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
        End Try
        'If you get here, something went wrong
        Return New Size(0, 0)

    End Function
    ''' <summary>
    ''' Shorten a URL String to the given maximum length
    ''' </summary>
    ''' <param name="sString">URL <c>String</c> to be shortened</param>
    ''' <param name="MaxLength">Maximum Integer length of the <c>String</c></param>
    ''' <param name="EndOnly">If <c>True</c>, only return the right-hand portion of the source string, up to <paramref name="MaxLength"/> in length</param>
    ''' <returns><c>String</c> url containing no more than <paramref name="MaxLength"/> characters</returns>
    ''' <remarks>If <paramref name="EndOnly"/>, then the end of the string is returned (up to <paramref name="MaxLength"/> in length.
    ''' Otherwise, extract the last significant portion of the URL (the portion after the last "/"). Return that portion,
    ''' along with as much of the start of the string as possible, joined by three periods.
    ''' Therefore, if EndOnly was TRUE, and MaxLength was 10:
    '''   TruncateURL("http://a.b.com/page1/page2", 15, True) would give ".../page1/page2"
    '''   TruncateURL("http://a.b.com/page1/page2", 15, False) would give "http:/.../page2"
    '''   TruncateURL("http://a.b.com/page1/this_long_title_page2", 15, True) would give "..._title_page2"
    ''' 
    ''' Note that if the source string is null or empty, String.Empty is returned.
    ''' Note that if MaxLength is less than or equal to 0, String.Empty is returned.
    ''' 
    ''' 2013/11/21 Dekker500 - Correct issue where if string did not actually need to be shortened (when maxLen > sString.Length), it was still shortened inappropriately
    ''' </remarks>
    Public Shared Function TruncateURL(ByVal sString As String, ByVal MaxLength As Integer, Optional ByVal EndOnly As Boolean = False) As String
        If String.IsNullOrEmpty(sString) OrElse (MaxLength <= 0) Then Return String.Empty
        If MaxLength >= sString.Length Then Return sString 'Nothing to do, since it is short enough

        Try
            Dim sEnd As String = String.Empty
            If EndOnly Then
                Return Strings.Right(sString, MaxLength)
            Else
                sEnd = Strings.Right(sString, sString.Length - sString.LastIndexOf("/"))
                If ((MaxLength - sEnd.Length) - 3) > 0 Then
                    Return String.Format("{0}...{1}", Strings.Left(sString, (MaxLength - sEnd.Length) - 3), sEnd)
                Else
                    If sEnd.Length >= MaxLength Then
                        Return String.Format("...{0}", Strings.Right(sEnd, MaxLength - 3))
                    Else
                        Return sEnd
                    End If
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(GetType(StringUtils), "Source of <" & sString & "> generated the following message: " & vbCrLf & ex.Message, ex.StackTrace, "Error")
        End Try

        'If you get here, something went wrong
        Return String.Empty
    End Function
    ''' <summary>
    ''' Transform the codified US movie certification value to MPAA certification
    ''' </summary>
    ''' <param name="sCert"><c>String</c> USA certification</param>
    ''' <returns><c>String</c>MPAA certification, or String.Empty if <paramref name="sCert"/> was not recognized</returns>
    ''' <remarks>Converts entries such as "usa:g" into "Rated G"</remarks>
    Public Shared Function USACertToMPAA(ByVal sCert As String) As String
        If String.IsNullOrEmpty(sCert) Then Return String.Empty

        Select Case sCert.ToLower
            Case "usa:g"
                Return "Rated G"
            Case "usa:pg"
                Return "Rated PG"
            Case "usa:pg-13"
                Return "Rated PG-13"
            Case "usa:r"
                Return "Rated R"
            Case "usa:nc-17"
                Return "Rated NC-17"
        End Select
        Return String.Empty
    End Function
    ''' <summary>
    ''' Removes invalid token from the given filename string
    ''' </summary>
    ''' <param name="fName"><c>String</c> filename to clean</param>
    ''' <returns>Cleaned <c>String</c></returns>
    ''' <remarks>Removes all invalid filename characters such as (:/|<>?*)
    ''' 
    ''' 2013/11/21 Dekker500 - Re-factored to remove the full set of invalid characters. Why was "\" considered acceptable before?
    ''' </remarks>
    Public Shared Function CleanFileName(ByVal fName As String) As String
        If String.IsNullOrEmpty(fName) Then Return String.Empty

        'Do specific replaces first
        fName = fName.Replace(":", " -")

        'Everthing else gets removed
        Dim invalidFileChars() As Char = Path.GetInvalidFileNameChars()
        For Each someChar In invalidFileChars
            fName = fName.Replace(someChar, " "c)
        Next

        'fName = fName.Replace("/", String.Empty)
        ''pattern = pattern.Replace("\", String.Empty)
        'fName = fName.Replace("|", String.Empty)
        'fName = fName.Replace("<", String.Empty)
        'fName = fName.Replace(">", String.Empty)
        'fName = fName.Replace("?", String.Empty)
        'fName = fName.Replace("*", String.Empty)
        'fName = fName.Replace(" ", " ")
        Return fName
    End Function
    ''' <summary>
    ''' Shortens the given <paramref name="fOutline"/> such that it is not longer than <paramref name="fLimit"/>.
    ''' </summary>
    ''' <param name="fOutline"><c>String</c> to shorten</param>
    ''' <param name="fLimit"><c>Integer</c> length that must not be exceeded</param>
    ''' <returns><c>Sting</c> that contains as much of the source <paramref name="fOutline"/> as possible</returns>
    ''' <remarks>The shortening is done by finding the last period "." and trimming from there.
    ''' 
    ''' 2013/11/22 Dekker500 - Major rewrite, since original did not pass many Unit Tests
    ''' </remarks>
    Public Shared Function ShortenOutline(ByVal fOutline As String, ByVal fLimit As Integer) As String
        If String.IsNullOrEmpty(fOutline) OrElse fLimit < 0 Then Return String.Empty

        If fLimit >= fOutline.Length Then Return fOutline 'Supplied string is within limits, so just return it
        If fLimit <= 3 Then
            'fLimit is ridiculously small. Fudge it and just return the appropriate number of dots
            Return "...".Substring(0, fLimit)
        End If

        'If we get this far, fOutline is longer than we want it, so it needs to be shortened.
        Dim lastPeriod As Integer = fOutline.LastIndexOf("."c)

        If lastPeriod < 0 OrElse lastPeriod > fLimit - 3 Then
            'No period was found, or was too close to the max length
            'Cheat and trim the last 3 chars, replacing with "..."
            Return String.Concat(fOutline.Substring(0, fLimit - 3), "...")
        End If

        'If we get this far, we found a period that was not at the extreme end of the string
        Return String.Concat(fOutline.Substring(0, lastPeriod + 1), "..") 'Note only 2 periods required, since one is already there
    End Function

    ''' <summary>
    ''' Cleans the given <paramref name="strText"/> such that it does no longer contain any bracket-sections and unwanted spaces.
    ''' </summary>
    ''' <param name="strText"><c>String</c> to remove brackets from</param>
    ''' <returns><c>Sting</c> that contains no brackets and unwanted whitespaces</returns>
    ''' <remarks>This one is used for cleaning scraped plots/outline, often actor names are provided in brackets which some might not like
    ''' 
    ''' 2013/12/21 Cocotus - First implementation, it seem's there no method like this in Ember?
    ''' </remarks>
    Public Shared Function RemoveBrackets(ByVal strText As String) As String
        'First 2 Regex to get rid of brackets, including string between brackets
        strText = Regex.Replace(strText, "\[.+?\]", "")
        strText = Regex.Replace(strText, "\(.+?\)", "")
        'After removing brackets unwanted spaces may be left - get rid of them
        strText = strText.Replace("  ", " ")
        strText = strText.Replace(" , ", ", ")
        strText = strText.Replace(" .", ".")
        Return strText.Trim()
    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class Wildcard

#Region "Methods"

        Public Shared Function IsMatch(ByVal ExpressionToMatch As String, ByVal FilterExpression As String, Optional ByVal IgnoreCase As Boolean = True) As Boolean
            If FilterExpression.Contains("*") _
                OrElse FilterExpression.Contains("?") _
                OrElse FilterExpression.Contains("#") Then

                If IgnoreCase Then
                    Return (ExpressionToMatch.ToLower Like FilterExpression.ToLower)
                Else
                    Return (ExpressionToMatch Like FilterExpression)
                End If

            Else
                If IgnoreCase Then
                    Return ExpressionToMatch.ToLower.Contains(FilterExpression.ToLower)
                Else
                    Return ExpressionToMatch.Contains(FilterExpression)
                End If
            End If
        End Function

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class