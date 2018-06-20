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
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports NLog

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")>

Public Class StringUtils

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

#End Region 'Properties

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
        If Char.IsLetterOrDigit(KeyChar) OrElse (AllowSpecial AndAlso (Char.IsControl(KeyChar) OrElse
        Char.IsWhiteSpace(KeyChar) OrElse Convert.ToInt32(KeyChar) = 44 OrElse Convert.ToInt32(KeyChar) = 45 OrElse Convert.ToInt32(KeyChar) = 46 OrElse Convert.ToInt32(KeyChar) = 58)) Then
            Return True
        Else
            Return False
        End If
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
    Public Shared Function ApplyFilters(ByVal name As String, ByRef filters As List(Of String)) As String
        If String.IsNullOrEmpty(name) Then Return String.Empty
        If filters Is Nothing OrElse filters.Count = 0 Then Return name
        Dim newName As String = name

        Dim strSplit() As String
        Try
            'run through each of the custom filters
            For Each Str As String In filters
                If Str.IndexOf("[->]") > 0 Then
                    strSplit = Str.Split(New String() {"[->]"}, StringSplitOptions.None)
                    newName = Regex.Replace(newName, strSplit.First, strSplit.Last)
                Else
                    newName = Regex.Replace(newName, Str, String.Empty)
                End If
                'everything was already filtered out, return an empty string
                If String.IsNullOrEmpty(newName) Then Return String.Empty
            Next
            Return newName.Trim
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Name: " & name & " generated an error message")
        End Try
        Return name.Trim
    End Function

    Public Shared Function BuildGenericTitle_TVEpisode(ByVal tDBElement As Database.DBElement) As String
        If tDBElement Is Nothing OrElse Not tDBElement.ContentType = Enums.ContentType.TVEpisode Then Return String.Empty
        Return String.Format("{0} S{1}E{2}{3}",
                             tDBElement.TVShow.Title,
                             tDBElement.TVEpisode.Season.ToString.PadLeft(2, Convert.ToChar("0")),
                             tDBElement.TVEpisode.Episode.ToString.PadLeft(2, Convert.ToChar("0")),
                             If(tDBElement.TVEpisode.SubEpisodeSpecified, String.Concat(".", tDBElement.TVEpisode.SubEpisode), String.Empty))
    End Function

    Public Shared Function GenreFilter(ByRef aGenres As List(Of String), Optional ByVal addNewGenres As Boolean = True) As Boolean
        Dim nGernes As New List(Of String)

        If Not aGenres.Count = 0 Then
            For Each tGenre As String In aGenres
                Dim gMappings As genreMapping = APIXML.GenreXML.Mappings.FirstOrDefault(Function(f) f.SearchString = tGenre)
                If gMappings IsNot Nothing Then
                    nGernes.AddRange(gMappings.MappedTo)
                ElseIf addNewGenres Then
                    'check if the tGenre is already existing in Gernes list
                    Dim gProperty As genreProperty = APIXML.GenreXML.Genres.FirstOrDefault(Function(f) f.Name = tGenre)
                    If gProperty Is Nothing Then
                        APIXML.GenreXML.Genres.Add(New genreProperty With {.Name = tGenre})
                    End If
                    'add a new mapping if tGenre is not in the Mappings list
                    APIXML.GenreXML.Mappings.Add(New genreMapping With {.MappedTo = New List(Of String) From {tGenre}, .SearchString = tGenre})
                    nGernes.Add(tGenre)
                End If
            Next
        End If

        'Cleanup for comparing
        nGernes = nGernes.Distinct().ToList()
        aGenres.Sort()
        nGernes.Sort()

        'Comparing (check if something has been changed)
        Dim bNoChanges = aGenres.SequenceEqual(nGernes)

        'Set new Genre list
        aGenres = nGernes

        'Return if the list has been changed or not
        Return Not bNoChanges
    End Function
    ''' <summary>
    ''' Removes invalid token from the given filename string
    ''' </summary>
    ''' <param name="fName"><c>String</c> filename to clean</param>
    ''' <returns>Cleaned <c>String</c></returns>
    ''' <remarks>Removes all invalid filename characters)
    ''' </remarks>
    Public Shared Function CleanFileName(ByVal fName As String) As String
        If String.IsNullOrEmpty(fName) Then Return String.Empty

        'Do specific replaces first
        fName = fName.Replace(":", " -")
        fName = fName.Replace("/", "-")
        fName = fName.Replace("?", String.Empty)
        fName = fName.Replace("*", String.Empty)

        'Everthing else gets removed
        Dim invalidFileChars() As Char = Path.GetInvalidFileNameChars()
        For Each someChar In invalidFileChars
            fName = fName.Replace(someChar, String.Empty)
        Next

        Return fName
    End Function
    ''' <summary>
    ''' Removes invalid token from the given path string
    ''' </summary>
    ''' <param name="fName"><c>String</c> path to clean</param>
    ''' <returns>Cleaned <c>String</c></returns>
    ''' <remarks>Removes all invalid path characters)
    ''' </remarks>
    Public Shared Function CleanPath(ByVal fName As String) As String
        If String.IsNullOrEmpty(fName) Then Return String.Empty

        'Do specific replaces first
        fName = fName.Replace(":", " -")
        fName = fName.Replace("/", "-")
        fName = fName.Replace("?", String.Empty)
        fName = fName.Replace("*", String.Empty)

        'Everthing else gets removed
        Dim invalidPathChars() As Char = Path.GetInvalidPathChars()
        For Each someChar In invalidPathChars
            fName = fName.Replace(someChar, String.Empty)
        Next

        Return fName
    End Function
    ''' <summary>
    ''' Removes all URLs and HTML tags
    ''' </summary>
    ''' <param name="strPlotOutline"></param>
    ''' <remarks></remarks>
    Public Shared Function CleanPlotOutline(ByVal strPlotOutline As String) As String
        Dim strResult As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(strPlotOutline) Then
                Dim cleanPattern As String = "<a.*?>(?<TEXT>.*?)<\/a>"
                Dim cResult As MatchCollection = Regex.Matches(strPlotOutline, cleanPattern, RegexOptions.Singleline)
                For ctr As Integer = 0 To cResult.Count - 1
                    strPlotOutline = strPlotOutline.Replace(cResult.Item(ctr).Value, cResult.Item(ctr).Groups(1).Value)
                Next
                strPlotOutline = strPlotOutline.Replace("<b>", String.Empty)
                strPlotOutline = strPlotOutline.Replace("</b>", String.Empty)
                strPlotOutline = strPlotOutline.Replace("<br />", String.Empty)
                strPlotOutline = strPlotOutline.Replace("<p>", String.Empty)
                strPlotOutline = strPlotOutline.Replace("</p>", String.Empty)
                strPlotOutline = strPlotOutline.Replace("<strong>", String.Empty)
                strPlotOutline = strPlotOutline.Replace("</strong>", String.Empty)
                strPlotOutline = strPlotOutline.Replace(Convert.ToChar(10), " ")    'vbLf
                strPlotOutline = strPlotOutline.Replace(Convert.ToChar(13), " ")    'vbCrLf
                strPlotOutline = strPlotOutline.Replace(Environment.NewLine, " ")
                strPlotOutline = strPlotOutline.Replace("  ", " ")
                strResult = strPlotOutline.Trim()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return strResult
    End Function

    Public Shared Function ConvertFromKodiTrailerFormatToYouTubeURL(ByVal strURL As String) As String
        If String.IsNullOrEmpty(strURL) Then Return String.Empty
        Return strURL.Replace("plugin://plugin.video.youtube/?action=play_video&videoid=", "http://www.youtube.com/watch?v=")
    End Function

    Public Shared Function ConvertFromYouTubeURLToKodiTrailerFormat(ByVal strURL As String) As String
        If String.IsNullOrEmpty(strURL) Then Return String.Empty
        Return String.Concat("plugin://plugin.video.youtube/?action=play_video&videoid=", YouTube.UrlUtils.GetVideoID(strURL))
    End Function
    ''' <summary>
    ''' Converts the supplied <c>String</c> to title-case, and converts certain keywords to uppercase
    ''' </summary>
    ''' <param name="sString"><c>String</c> to modify</param>
    ''' <returns>Converted <c>String</c>. It is always Trimmed</returns>
    ''' <remarks>Converts <paramref name="sString"/> to title-case (first char of each word is upper-case) and certain keywords are uppercase.
    ''' Note that if a problem is encountered processing the string, the source string is returned.</remarks>
    Public Shared Function ConvertToProperCase(ByVal sString As String) As String
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Source of <" & sString & "> generated an error")
            'Return the source string and move along
            sReturn = sString.Trim
        End Try

        Return sReturn.Trim
    End Function

    Public Shared Function ConvertToValidFilterString(ByVal strInput As String) As String
        Return strInput.Replace("["c, "[[]").Replace("'"c, "''").Replace("%"c, "[%]")
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
    ''' Convert String to SHA1
    ''' </summary>
    ''' <param name="inputstring">Input string to encrypt to SHA1</param>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' Used for POST-Requests to trakt.tv (encrypt password)
    ''' </remarks>
    ''' 
    Public Shared Function EncryptToSHA1(ByVal inputstring As String) As String
        Dim strToHash As String = inputstring
        Dim Result As String = ""
        Dim OSha1 As New _
        System.Security.Cryptography.SHA1CryptoServiceProvider

        'Step 1
        Dim bytesToHash() As Byte _
         = System.Text.Encoding.ASCII.GetBytes(strToHash)

        'Step 2
        bytesToHash = OSha1.ComputeHash(bytesToHash)

        'Step 3
        For Each item As Byte In bytesToHash
            Result += item.ToString("x2")
        Next
        Return Result
    End Function
    ''' <summary>
    ''' Cleans up a movie path by stripping it down to the basic title with no additional decorations.
    ''' </summary>
    ''' <param name="strPath"><c>String</c> full file path (including file extension) to get title from</param>
    ''' <returns>The filtered title as a <c>String</c></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterTitleFromPath_Movie(ByVal fileItem As FileItem, ByVal IsSingle As Boolean, ByVal UseForderName As Boolean) As String
        If fileItem Is Nothing Then Return String.Empty

        'get raw title from path
        Dim strRawString As String = String.Empty
        If fileItem.bIsBDMV OrElse fileItem.bIsVideoTS Then
            strRawString = fileItem.MainPath.Name
        Else
            strRawString = If(IsSingle AndAlso UseForderName, fileItem.MainPath.Name, Path.GetFileNameWithoutExtension(fileItem.StackedFilename))
        End If

        'filter raw title by filter list
        Dim strTitle As String = ApplyFilters(strRawString, Master.eSettings.MovieFilterCustom)

        'Convert String To Proper Case
        If Master.eSettings.MovieProperCase Then
            strTitle = ConvertToProperCase(strTitle)
        End If

        'everything was filtered out... just set to file or directory name
        If String.IsNullOrEmpty(strTitle) Then
            Return strRawString.Trim
        Else
            Return strTitle.Trim
        End If
    End Function
    ''' <summary>
    ''' Cleans up a tv episode path by stripping it down to the basic title with no additional decorations.
    ''' </summary>
    ''' <param name="strPath"><c>String</c> full file path (including file extension) to get title from</param>
    ''' <param name="strTVShowName">The <c>String</c> TV Show name to remove it from TV Episode title</param>
    ''' <returns>The filtered title as a <c>String</c></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterTitleFromPath_TVEpisode(ByVal strPath As String, ByVal strTVShowName As String) As String
        If String.IsNullOrEmpty(strPath) Then Return String.Empty

        'removing stack markers
        strPath = FileUtils.Common.RemoveStackingMarkers(strPath)

        'get raw title from path
        Dim strRawTitle As String = Path.GetFileNameWithoutExtension(strPath)

        'filter raw title by filter list
        Dim strTitle As String = ApplyFilters(strRawTitle, Master.eSettings.TVEpisodeFilterCustom)

        'remove the tv show name from the episode title
        If Not String.IsNullOrEmpty(strTVShowName) Then strTitle = strTitle.Replace(strTVShowName.Trim, String.Empty).Trim

        'Convert String To Proper Case
        If Master.eSettings.TVEpisodeProperCase Then
            strTitle = ConvertToProperCase(strTitle)
        End If

        'everything was filtered out... just set to file name
        If String.IsNullOrEmpty(strTitle) Then
            Return strRawTitle.Trim
        Else
            Return strTitle.Trim
        End If
    End Function
    ''' <summary>
    ''' Cleans up a tv show path by stripping it down to the basic title with no additional decorations.
    ''' </summary>
    ''' <param name="strPath"><c>String</c> full tv show main path to get title from</param>
    ''' <returns>The filtered name as a <c>String</c></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterTitleFromPath_TVShow(ByVal strPath As String) As String
        If String.IsNullOrEmpty(strPath) Then Return String.Empty

        'get raw title from path
        Dim strRawTitle As String = Path.GetDirectoryName(strPath)

        'filter raw title by filter list
        Dim strTitle As String = ApplyFilters(strRawTitle, Master.eSettings.TVShowFilterCustom)

        'Convert String To Proper Case
        If Master.eSettings.TVShowProperCase Then
            strTitle = ConvertToProperCase(strTitle)
        End If

        'everything was filtered out... just set to directory name
        If String.IsNullOrEmpty(strTitle) Then
            Return strRawTitle.Trim
        Else
            Return strTitle.Trim
        End If
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
    ''' Get the four-digit year from the given FileItem
    ''' </summary>
    ''' <param name="fileItem"></param>
    ''' <param name="IsSingle"></param>
    ''' <param name="UseForderName"></param>
    ''' <returns></returns>
    Public Shared Function FilterYearFromPath_Movie(ByVal fileItem As FileItem, ByVal IsSingle As Boolean, ByVal UseForderName As Boolean) As Integer
        If fileItem Is Nothing Then Return 0

        'get raw string to get year from
        Dim strRawString As String = String.Empty
        If fileItem.bIsBDMV OrElse fileItem.bIsVideoTS Then
            strRawString = fileItem.MainPath.Name
        Else
            strRawString = If(IsSingle AndAlso UseForderName, fileItem.MainPath.Name, fileItem.FileInfo.Name)
        End If

        Dim iYear As Integer
        Integer.TryParse(Regex.Match(strRawString, "((19|20)\d{2})", RegexOptions.RightToLeft).Value.Trim, iYear)

        Return iYear
    End Function

    Public Shared Function FormatDuration(ByVal tDur As String, ByVal contentType As Enums.ContentType) As String
        Dim strMask As String = String.Empty
        Select Case contentType
            Case Enums.ContentType.Movie
                strMask = Master.eSettings.MovieScraperDurationRuntimeFormat
            Case Enums.ContentType.TVEpisode
                strMask = Master.eSettings.TVScraperDurationRuntimeFormat
            Case Else
                Return String.Empty
        End Select
        Dim rmDuration As Match = Regex.Match(tDur, "(([0-9]+)h)?\s?(([0-9]+)min)?\s?(([0-9]+)s)?")
        Dim iHours As Integer = If(Not String.IsNullOrEmpty(rmDuration.Groups(2).Value), (Convert.ToInt32(rmDuration.Groups(2).Value)), 0)
        Dim iMinutes As Integer = If(Not String.IsNullOrEmpty(rmDuration.Groups(4).Value), (Convert.ToInt32(rmDuration.Groups(4).Value)), 0)
        Dim iSeconds As Integer = If(Not String.IsNullOrEmpty(rmDuration.Groups(6).Value), (Convert.ToInt32(rmDuration.Groups(6).Value)), 0)

        'new handling: only seconds as tdur
        If Integer.TryParse(tDur, 0) Then
            Dim ts As New TimeSpan(0, 0, Convert.ToInt32(tDur))
            iHours = ts.Hours
            iMinutes = ts.Minutes
            iSeconds = ts.Seconds
        End If

        If strMask.Contains("<h>") Then
            If strMask.Contains("<m>") OrElse strMask.Contains("<0m>") Then
                If strMask.Contains("<s>") OrElse strMask.Contains("<0s>") Then
                    Return strMask.Replace("<h>", iHours.ToString).Replace("<m>", iMinutes.ToString).Replace("<0m>", iMinutes.ToString("00")).Replace("<s>", iSeconds.ToString).Replace("<0s>", iSeconds.ToString("00"))
                Else
                    Return strMask.Replace("<h>", iHours.ToString).Replace("<m>", iMinutes.ToString).Replace("<0m>", iMinutes.ToString("00"))
                End If
            Else
                Dim tHDec As String = If(iMinutes > 0, Convert.ToSingle(1 / (60 / iMinutes)).ToString(".00"), String.Empty)
                Return strMask.Replace("<h>", String.Concat(iHours, tHDec))
            End If
        ElseIf strMask.Contains("<m>") Then
            If strMask.Contains("<s>") OrElse strMask.Contains("<0s>") Then
                Return strMask.Replace("<m>", ((iHours * 60) + iMinutes).ToString).Replace("<s>", iSeconds.ToString).Replace("<0s>", iSeconds.ToString("00"))
            Else
                Return strMask.Replace("<m>", ((iHours * 60) + iMinutes).ToString)
            End If
        ElseIf strMask.Contains("<s>") Then
            Return strMask.Replace("<s>", ((iHours * 60 * 60) + iMinutes * 60 + iSeconds).ToString)
        Else
            Return strMask
        End If
    End Function
    ''' <summary>
    ''' For a given <c>Integer</c> season number, determine the appropriate season text
    ''' </summary>
    ''' <param name="iSeason"><c>Integer</c> season value. Valid values are -1 or higher.</param>
    ''' <returns><c>String</c> title appropriate for the season</returns>
    ''' <remarks>For <paramref name="iSeason"/> greater than 0, evaluates to (regional equivalent of) "Season XX" where XX is a 0-padded number.
    ''' For 0, returns equivalent of "Season Specials".
    ''' For -1, returns equivalent of "* All Seasons".
    ''' For less than -1, returns equivalent of "Unknown"</remarks>
    Public Shared Function FormatSeasonText(ByVal iSeason As Integer) As String
        If iSeason > 0 Then
            Return String.Concat(Master.eLang.GetString(650, "Season"), " ", iSeason.ToString.PadLeft(2, Convert.ToChar("0")))
        ElseIf iSeason = 0 Then
            Return Master.eLang.GetString(655, "Season Specials")
        ElseIf iSeason = -1 Then
            Return Master.eLang.GetString(1256, "* All Seasons")
        Else
            Return Master.eLang.GetString(138, "Unknown")
        End If
    End Function

    Public Shared Function GetIMDBIDFromString(ByVal text As String, Optional ByVal searchrighttoleft As Boolean = False) As String
        If String.IsNullOrEmpty(text) Then Return String.Empty
        Return Regex.Match(text, "tt\d{6}\d*", If(searchrighttoleft, RegexOptions.RightToLeft, RegexOptions.None)).Value.Trim
    End Function

    Public Shared Function GetURL_IMDb(ByVal dbelement As Database.DBElement) As String
        Select Case dbelement.ContentType
            Case Enums.ContentType.Movie
                If dbelement.Movie.UniqueIDs.IMDbIdSpecified Then
                    Return String.Concat("https://www.imdb.com/title/", dbelement.Movie.UniqueIDs.IMDbId)
                End If
            Case Enums.ContentType.TVEpisode
                If dbelement.TVEpisode.UniqueIDs.IMDbIdSpecified Then
                    Return String.Concat("https://www.imdb.com/title/", dbelement.TVEpisode.UniqueIDs.IMDbId)
                End If
            Case Enums.ContentType.TVSeason
                If dbelement.TVShow.UniqueIDs.IMDbIdSpecified AndAlso dbelement.TVSeason.SeasonSpecified Then
                    Return String.Format("https://www.imdb.com/title/{0}/episodes?season={1}", dbelement.TVShow.UniqueIDs.IMDbId, dbelement.TVSeason.Season)
                End If
            Case Enums.ContentType.TVShow
                If dbelement.TVShow.UniqueIDs.IMDbIdSpecified Then
                    Return String.Concat("https://www.imdb.com/title/", dbelement.TVShow.UniqueIDs.IMDbId)
                End If
        End Select
        Return String.Empty
    End Function

    Public Shared Function GetURL_TMDb(ByVal dbelement As Database.DBElement) As String
        Select Case dbelement.ContentType
            Case Enums.ContentType.Movie
                If dbelement.Movie.UniqueIDs.TMDbIdSpecified Then
                    Return String.Concat("https://www.themoviedb.org/movie/", dbelement.Movie.UniqueIDs.TMDbId)
                End If
            Case Enums.ContentType.MovieSet
                If dbelement.MovieSet.UniqueIDs.TMDbIdSpecified Then
                    Return String.Concat("https://www.themoviedb.org/collection/", dbelement.MovieSet.UniqueIDs.TMDbId)
                End If
            Case Enums.ContentType.TVEpisode
                If dbelement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso dbelement.TVEpisode.SeasonSpecified AndAlso dbelement.TVEpisode.EpisodeSpecified Then
                    Return String.Format("https://www.themoviedb.org/tv/{0}/season/{1}/episode/{2}", dbelement.TVShow.UniqueIDs.TMDbId, dbelement.TVEpisode.Season, dbelement.TVEpisode.Episode)
                End If
            Case Enums.ContentType.TVSeason
                If dbelement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso dbelement.TVSeason.SeasonSpecified Then
                    Return String.Format("https://www.themoviedb.org/tv/{0}/season/{1}", dbelement.TVShow.UniqueIDs.TMDbId, dbelement.TVSeason.Season)
                End If
            Case Enums.ContentType.TVShow
                If dbelement.TVShow.UniqueIDs.TMDbIdSpecified Then
                    Return String.Concat("https://www.themoviedb.org/tv/", dbelement.TVShow.UniqueIDs.TMDbId)
                End If
        End Select
        Return String.Empty
    End Function

    Public Shared Function GetURL_TVDb(ByVal dbelement As Database.DBElement) As String
        Select Case dbelement.ContentType
            Case Enums.ContentType.TVEpisode
                If dbelement.TVShow.UniqueIDs.TVDbIdSpecified AndAlso dbelement.TVEpisode.UniqueIDs.TVDbIdSpecified Then
                    Return String.Format("https://thetvdb.com/?tab=episode&seriesid={0}&id={1}", dbelement.TVShow.UniqueIDs.TVDbId, dbelement.TVEpisode.UniqueIDs.TVDbId)
                End If
            Case Enums.ContentType.TVSeason
                If dbelement.TVShow.UniqueIDs.TVDbIdSpecified AndAlso dbelement.TVSeason.UniqueIDs.TVDbIdSpecified Then
                    Return String.Format("https://thetvdb.com/?tab=season&seriesid={0}&seasonid={1}", dbelement.TVShow.UniqueIDs.TVDbId, dbelement.TVSeason.UniqueIDs.TVDbId)
                End If
            Case Enums.ContentType.TVShow
                If dbelement.TVShow.UniqueIDs.TVDbIdSpecified Then
                    Return String.Concat("https://thetvdb.com/?tab=series&id=", dbelement.TVShow.UniqueIDs.TVDbId)
                End If
        End Select
        Return String.Empty
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Input <" & stext & "> generated an error message")
        End Try

        'If we get here, something went wrong.
        Return String.Empty
    End Function
    ''' <summary>
    ''' Determine whether the language of the supplied string is english. 
    ''' </summary>
    ''' <param name="sToCheck"><c>String</c> to check</param>
    ''' <returns><c>True</c> if the string is english, <c>False</c> otherwise (foreign language)</returns>
    ''' <remarks>This is not a thoroughly exhaustive check, but it does the job for now and worked in my tests
    ''' </remarks>
    Public Shared Function isEnglishText(ByVal sToCheck As String) As Boolean
        If sToCheck.ToLower.Contains("the ") OrElse sToCheck.ToLower.Contains("this ") OrElse sToCheck.ToLower.Contains("that ") OrElse sToCheck.ToLower.Contains(" by ") OrElse sToCheck.ToLower.Contains(" of ") OrElse sToCheck.ToLower.Contains(" and ") Then
            Return True
        End If
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
        If Uri.TryCreate(sToCheck, UriKind.Absolute, validatedUri) Then
            If validatedUri.Scheme = Uri.UriSchemeHttp OrElse validatedUri.Scheme = Uri.UriSchemeHttps Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Shared Function ListTitle_Movie(ByVal MovieTitle As String, ByVal MovieYear As String) As String
        Dim ListTitle As String = MovieTitle
        If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(MovieYear) Then
            ListTitle = String.Format("{0} ({1})", SortTokens_Movie(MovieTitle.Trim), MovieYear.Trim)
        Else
            ListTitle = SortTokens_Movie(MovieTitle.Trim)
        End If
        Return ListTitle
    End Function

    Public Shared Function ListTitle_TVShow(ByVal TVShowTitle As String, ByVal MovieYear As String) As String
        Dim ListTitle As String = TVShowTitle
        If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(MovieYear) Then
            ListTitle = String.Format("{0} ({1})", SortTokens_Movie(TVShowTitle.Trim), MovieYear.Trim)
        Else
            ListTitle = SortTokens_Movie(TVShowTitle.Trim)
        End If
        Return ListTitle
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
        If Char.IsNumber(KeyChar) OrElse Char.IsControl(KeyChar) OrElse (isIP AndAlso Convert.ToInt32(KeyChar) = 46) Then
            Return False
        Else
            Return True
        End If
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Source of <" & sString & "> generated an error")
            'Return the source string and move along
            sReturn = sString
        End Try
        Return sReturn.Trim
    End Function
    ''' <summary>
    ''' Shortens the given <paramref name="fOutline"/> such that it is not longer than <paramref name="fLimit"/>.
    ''' </summary>
    ''' <param name="fOutline"><c>String</c> to shorten</param>
    ''' <param name="fLimit"><c>Integer</c> length that must not be exceeded</param>
    ''' <returns><c>Sting</c> that contains as much of the source <paramref name="fOutline"/> as possible</returns>
    ''' <remarks>The shortening is done by finding the last period "." and trimming from there.</remarks>
    Public Shared Function ShortenOutline(ByVal fOutline As String, ByVal fLimit As Integer) As String
        If String.IsNullOrEmpty(fOutline) OrElse fLimit < 0 Then Return String.Empty

        If fLimit >= fOutline.Length Then Return fOutline 'Supplied string is within limits, so just return it
        If fLimit = 0 Then Return fOutline 'Limit of 0 means full plot for outline
        If fLimit <= 3 Then
            'fLimit is ridiculously small. Fudge it and just return the appropriate number of dots
            Return "...".Substring(0, fLimit)
        End If

        Dim nOutline = fOutline

        nOutline = nOutline.Substring(0, fLimit - 2)
        If nOutline.EndsWith(".") AndAlso Not (nOutline.ToLower.EndsWith("dr.") OrElse nOutline.ToLower.EndsWith("mr.") OrElse nOutline.ToLower.EndsWith("ms.")) Then
            Return String.Concat(nOutline, "..")
        ElseIf nOutline.EndsWith(".") Then
            nOutline = nOutline.Substring(0, nOutline.Length - 1)
        End If

        'If we get this far, nOutline is longer than we want it, so it needs to be shortened.
        Dim lastPeriod As Integer = nOutline.LastIndexOf("."c)

        If lastPeriod < 0 Then
            'No period was found, or was too close to the max length
            'Cheat and trim the last char, replacing with "..."
            Return String.Concat(nOutline.Substring(0, nOutline.Length - 1), "...")
        End If

        'If we get this far, we found a period that was not at the extreme end of the string
        nOutline = nOutline.Substring(0, lastPeriod + 1)

        While nOutline.ToLower.EndsWith("dr.") OrElse nOutline.ToLower.EndsWith("mr.") OrElse nOutline.ToLower.EndsWith("ms.")
            nOutline = nOutline.Substring(0, nOutline.Length - 1)
            lastPeriod = nOutline.LastIndexOf("."c)
            If lastPeriod < 0 Then
                'No period was found, or was too close to the max length
                'Cheat and trim the last char, replacing with "..."
                Return String.Concat(fOutline.Substring(0, fLimit - 3), "...")
            End If
            nOutline = nOutline.Substring(0, lastPeriod + 1)
        End While

        Return String.Concat(nOutline.Substring(0, lastPeriod + 1), "..") 'Note only 2 periods required, since one is already there
    End Function

    Private Shared Function SortTokens(ByVal strTitle As String, ByVal lstTokenList As List(Of String)) As String
        For Each strToken As String In lstTokenList
            Try
                Dim mToken As Match = Regex.Match(strTitle, String.Concat("(^", strToken, ")(.*)"), RegexOptions.IgnoreCase)
                If mToken.Success AndAlso mToken.Groups.Count = 3 Then
                    Return String.Format("{0}, {1}", mToken.Groups(2).Value.Trim, mToken.Groups(1).Value.Trim)
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Title: " & strTitle & " generated an error message")
            End Try
        Next
        Return strTitle
    End Function
    ''' <summary>
    ''' Scan the <c>String</c> title provided, and if it starts with one of the pre-defined
    ''' sort tokens (<c>Master.eSettings.MovieSortTokens"</c>) then remove it from the front
    ''' of the string and move it to the end after a comma.
    ''' </summary>
    ''' <param name="strTitle"><c>String</c> to clean up</param>
    ''' <returns><c>String</c> with any defined sort tokens moved to the end</returns>
    ''' <remarks>This function will take a string such as "The Movie" and return "Movie, The".
    ''' The default tokens are:
    '''  <list>
    '''    <item>a</item>
    '''    <item>an</item>
    '''    <item>the</item>
    ''' </list>
    ''' Once the first token is found and moved, no further search is made for other tokens.</remarks>
    Public Shared Function SortTokens_Movie(ByVal strTitle As String) As String
        Return SortTokens(strTitle, Master.eSettings.MovieSortTokens)
    End Function
    ''' <summary>
    ''' Scan the <c>String</c> title provided, and if it starts with one of the pre-defined
    ''' sort tokens (<c>Master.eSettings.MovieSetSortTokens"</c>) then remove it from the front
    ''' of the string and move it to the end after a comma.
    ''' </summary>
    ''' <param name="strTitle"><c>String</c> to clean up</param>
    ''' <returns><c>String</c> with any defined sort tokens moved to the end</returns>
    ''' <remarks>This function will take a string such as "The MovieSet" and return "MovieSet, The".
    ''' The default tokens are:
    '''  <list>
    '''    <item>a</item>
    '''    <item>an</item>
    '''    <item>the</item>
    ''' </list>
    ''' Once the first token is found and moved, no further search is made for other tokens.</remarks>
    Public Shared Function SortTokens_MovieSet(ByVal strTitle As String) As String
        Return SortTokens(strTitle, Master.eSettings.MovieSetSortTokens)
    End Function
    ''' <summary>
    ''' Scan the <c>String</c> title provided, and if it starts with one of the pre-defined
    ''' sort tokens (<c>Master.eSettings.SortTokens"</c>) then remove it from the front
    ''' of the string and move it to the end after a comma.
    ''' </summary>
    ''' <param name="strTitle"><c>String</c> to clean up</param>
    ''' <returns><c>String</c> with any defined sort tokens moved to the end</returns>
    ''' <remarks>This function will take a string such as "The Show" and return "Show, The".
    ''' The default tokens are:
    '''  <list>
    '''    <item>a</item>
    '''    <item>an</item>
    '''    <item>the</item>
    ''' </list>
    ''' Once the first token is found and moved, no further search is made for other tokens.</remarks>
    Public Shared Function SortTokens_TV(ByVal strTitle As String) As String
        Return SortTokens(strTitle, Master.eSettings.TVSortTokens)
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
                Dim SplitSize() As String = source.Split("x"c)
                Return New Size(Convert.ToInt32(SplitSize(0)), Convert.ToInt32(SplitSize(1)))
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Source of <" & sString & "> generated an error")
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
                Return sString.Substring(sString.Length - MaxLength)
            Else
                sEnd = sString.Substring(sString.LastIndexOf("/"))
                If ((MaxLength - sEnd.Length) - 3) > 0 Then
                    Return String.Format("{0}...{1}", sString.Substring(0, (MaxLength - sEnd.Length) - 3), sEnd)
                Else
                    If sEnd.Length >= MaxLength Then
                        Return String.Format("...{0}", sEnd.Substring(sEnd.Length - MaxLength + 3))
                    Else
                        Return sEnd
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Source of <" & sString & "> generated an error")
        End Try

        'If you get here, something went wrong
        Return String.Empty
    End Function
    ''' <summary>
    ''' Transform the codified US movie certification value to MPAA certification
    ''' </summary>
    ''' <param name="strCert"><c>String</c> USA certification</param>
    ''' <returns><c>String</c>MPAA certification, or String.Empty if <paramref name="strCert"/> was not recognized</returns>
    ''' <remarks>Converts entries such as "usa:g" into "Rated G"</remarks>
    Public Shared Function USACertToMPAA(ByVal strCert As String) As String
        Select Case strCert.ToLower
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
            Case "usa:approved"
                Return "Approved"
            Case "usa:passed"
                Return "Passed"
            Case Else
                Return strCert
        End Select
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