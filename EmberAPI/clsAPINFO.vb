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

Imports NLog
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

Public Class NFO

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Public Shared Function Clean(ByVal details As MediaContainers.MainDetails) As MediaContainers.MainDetails
        If details IsNot Nothing Then
            details.Aired = NumUtils.DateToISO8601Date(details.Aired)
            details.Outline = details.Outline.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            details.Plot = details.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            details.Premiered = NumUtils.DateToISO8601Date(details.Premiered)
            details.Votes = NumUtils.CleanVotes(details.Votes)
            If details.FileInfoSpecified Then
                If details.FileInfo.StreamDetails.AudioSpecified Then
                    For Each aStream In details.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                    Next
                End If
                If details.FileInfo.StreamDetails.SubtitleSpecified Then
                    For Each sStream In details.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                    Next
                End If
            End If
            If details.SetsSpecified Then
                For i = details.Sets.Count - 1 To 0 Step -1
                    If Not details.Sets(i).TitleSpecified Then
                        details.Sets.RemoveAt(i)
                    End If
                Next
            End If

            'changes a LongLanguage to Alpha2 code
            If details.LanguageSpecified Then
                Dim Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = details.Language)
                If Language IsNot Nothing Then
                    details.Language = Language.Abbreviation
                Else
                    'check if it's a valid Alpha2 code or remove the information the use the source default language
                    Dim ShortLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = details.Language)
                    If ShortLanguage Is Nothing Then
                        details.Language = String.Empty
                    End If
                End If
            End If

            'Boxee support
            If Master.eSettings.TVUseBoxee Then
                If details.BoxeeTvDbSpecified AndAlso Not details.UniqueIDs.TVDbIdSpecified Then
                    details.UniqueIDs.TVDbId = details.BoxeeTvDb
                    'mNFO.BlankBoxeeId()
                End If
            End If

            Return details
        Else
            Return details
        End If
    End Function
    ''' <summary>
    ''' Delete all movie NFOs
    ''' </summary>
    ''' <param name="dbElement"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal dbElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
        If Not dbElement.FileItemSpecified Then Return
        Try
            For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO, ForceFileCleanup)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Function GetIMDBFromNonConf(ByVal nfoPath As String, ByVal isSingle As Boolean) As NonConf
        Dim tNonConf As New NonConf
        Dim dirPath As String = Directory.GetParent(nfoPath).FullName
        Dim lstFiles As New List(Of String)

        If isSingle Then
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, "*.nfo"))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, "*.info"))
            Catch
            End Try
        Else
            Dim fName As String = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(nfoPath)).ToLower
            Dim oName As String = Path.GetFileNameWithoutExtension(nfoPath)
            fName = If(fName.EndsWith("*"), fName, String.Concat(fName, "*"))
            oName = If(oName.EndsWith("*"), oName, String.Concat(oName, "*"))

            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".nfo")))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".nfo")))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".info")))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".info")))
            Catch
            End Try
        End If

        For Each sFile As String In lstFiles
            Using srInfo As New StreamReader(sFile)
                Dim sInfo As String = srInfo.ReadToEnd
                Dim strIMDBID As String = Regex.Match(sInfo, "tt\d\d\d\d\d\d\d*", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString

                If Not String.IsNullOrEmpty(strIMDBID) Then
                    tNonConf.IMDBID = strIMDBID
                    'now lets try to see if the rest of the file is a proper nfo
                    If sInfo.ToLower.Contains("</movie>") Then
                        tNonConf.Text = APIXML.XMLToLowerCase(sInfo.Substring(0, sInfo.ToLower.IndexOf("</movie>") + 8))
                    End If
                    Exit For
                Else
                    strIMDBID = Regex.Match(nfoPath, "tt\d\d\d\d\d\d\d*", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString
                    If Not String.IsNullOrEmpty(strIMDBID) Then
                        tNonConf.IMDBID = strIMDBID
                    End If
                End If
            End Using
        Next
        Return tNonConf
    End Function

    Public Shared Function GetNfoPath_MovieSet(ByVal dbElement As Database.DBElement) As String
        For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
            If File.Exists(a) Then
                Return a
            End If
        Next
        Return String.Empty
    End Function

    Public Shared Function IsConformingNFO(ByVal nfoPath As String) As Boolean
        Dim xmlSer As XmlSerializer = Nothing
        Try
            Dim fInfo As New FileInfo(nfoPath)
            If (fInfo.Extension = ".nfo" OrElse fInfo.Extension = ".info") AndAlso fInfo.Exists Then
                Using srNFO As StreamReader = New StreamReader(nfoPath)
                    xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                    xmlSer.Deserialize(srNFO)
                End Using
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Shared Function IsConformingNFO_TVEpisode(ByVal nfoPath As String) As Boolean
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.MainDetails))
        Dim nDetails As New MediaContainers.MainDetails

        Try
            If (Path.GetExtension(nfoPath) = ".nfo" OrElse Path.GetExtension(nfoPath) = ".info") AndAlso File.Exists(nfoPath) Then
                Using xmlSR As StreamReader = New StreamReader(nfoPath)
                    Dim xmlStr As String = xmlSR.ReadToEnd
                    Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                    If rMatches.Count = 1 Then
                        Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                            nDetails = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                            xmlSer = Nothing
                            nDetails = Nothing
                            Return True
                        End Using
                    ElseIf rMatches.Count > 1 Then
                        'read them all... if one fails, the entire nfo is non conforming
                        For Each xmlReg As Match In rMatches
                            Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                nDetails = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                nDetails = Nothing
                            End Using
                        Next
                        xmlSer = Nothing
                        Return True
                    Else
                        xmlSer = Nothing
                        If nDetails IsNot Nothing Then
                            nDetails = Nothing
                        End If
                        Return False
                    End If
                End Using
            Else
                xmlSer = Nothing
                nDetails = Nothing
                Return False
            End If
        Catch
            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
            If nDetails IsNot Nothing Then
                nDetails = Nothing
            End If
            Return False
        End Try
    End Function

    Public Shared Function LoadFromNFO_Movie(ByVal nfoPath As String, ByVal isSingle As Boolean) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMov As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(nfoPath) Then
            Try
                If File.Exists(nfoPath) AndAlso Path.GetExtension(nfoPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(nfoPath)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                        xmlMov = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MainDetails)
                        xmlMov = Clean(xmlMov)
                    End Using
                Else
                    If Not String.IsNullOrEmpty(nfoPath) Then
                        Dim sReturn As New NonConf
                        sReturn = GetIMDBFromNonConf(nfoPath, isSingle)
                        xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                        Try
                            If Not String.IsNullOrEmpty(sReturn.Text) Then
                                Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                    xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                                    xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.MainDetails)
                                    xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                                    xmlMov = Clean(xmlMov)
                                End Using
                            End If
                        Catch
                        End Try
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                xmlMov = New MediaContainers.MainDetails
                If Not String.IsNullOrEmpty(nfoPath) Then

                    'go ahead and rename it now, will still be picked up in getimdbfromnonconf
                    If Not Master.eSettings.Movie.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_Movie(nfoPath, True)
                    End If

                    Dim sReturn As New NonConf
                    sReturn = GetIMDBFromNonConf(nfoPath, isSingle)
                    xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                    Try
                        If Not String.IsNullOrEmpty(sReturn.Text) Then
                            Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                                xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.MainDetails)
                                xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                                xmlMov = Clean(xmlMov)
                            End Using
                        End If
                    Catch
                    End Try
                End If
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMov
    End Function

    Public Shared Function LoadFromNFO_MovieSet(ByVal nfoPath As String) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMovSet As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(nfoPath) Then
            Try
                If File.Exists(nfoPath) AndAlso Path.GetExtension(nfoPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(nfoPath)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                        xmlMovSet = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MainDetails)
                        xmlMovSet.Plot = xmlMovSet.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                    End Using
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                xmlMovSet = New MediaContainers.MainDetails
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMovSet
    End Function

    Public Shared Function LoadFromNFO_TVEpisode(ByVal nfoPath As String, ByVal seasonNumber As Integer, ByVal episodeNumber As Integer) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.MainDetails))
        Dim xmlEp As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(nfoPath) AndAlso seasonNumber >= -1 Then
            Try
                If File.Exists(nfoPath) AndAlso Path.GetExtension(nfoPath).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(nfoPath)
                        Dim xmlStr As String = xmlSR.ReadToEnd
                        Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                        If rMatches.Count = 1 Then
                            'only one episodedetail... assume it's the proper one
                            Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                                xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                xmlEp = Clean(xmlEp)
                                xmlSer = Nothing
                                If xmlEp.FileInfoSpecified Then
                                    If xmlEp.FileInfo.StreamDetails.AudioSpecified Then
                                        For Each aStream In xmlEp.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                                        Next
                                    End If
                                    If xmlEp.FileInfo.StreamDetails.SubtitleSpecified Then
                                        For Each sStream In xmlEp.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                                        Next
                                    End If
                                End If
                                Return xmlEp
                            End Using
                        ElseIf rMatches.Count > 1 Then
                            For Each xmlReg As Match In rMatches
                                Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                    xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                    xmlEp = Clean(xmlEp)
                                    If xmlEp.Episode = episodeNumber AndAlso xmlEp.Season = seasonNumber Then
                                        xmlSer = Nothing
                                        Return xmlEp
                                    End If
                                End Using
                            Next
                        End If
                    End Using

                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.TVEpisode.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_TVEpisode(nfoPath, True)
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.TVEpisode.SourceSettings.OverWriteNfo Then
                    RenameNonConfNFO_TVEpisode(nfoPath, True)
                End If
            End Try
        End If

        Return New MediaContainers.MainDetails
    End Function

    Public Shared Function LoadFromNFO_TVEpisode(ByVal nfoPath As String, ByVal seasonNumber As Integer, ByVal airedDate As String) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.MainDetails))
        Dim xmlEp As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(nfoPath) AndAlso seasonNumber >= -1 Then
            Try
                If File.Exists(nfoPath) AndAlso Path.GetExtension(nfoPath).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(nfoPath)
                        Dim xmlStr As String = xmlSR.ReadToEnd
                        Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                        If rMatches.Count = 1 Then
                            'only one episodedetail... assume it's the proper one
                            Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                                xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                xmlEp = Clean(xmlEp)
                                xmlSer = Nothing
                                If xmlEp.FileInfoSpecified Then
                                    If xmlEp.FileInfo.StreamDetails.AudioSpecified Then
                                        For Each aStream In xmlEp.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                                        Next
                                    End If
                                    If xmlEp.FileInfo.StreamDetails.SubtitleSpecified Then
                                        For Each sStream In xmlEp.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                                        Next
                                    End If
                                End If
                                Return xmlEp
                            End Using
                        ElseIf rMatches.Count > 1 Then
                            For Each xmlReg As Match In rMatches
                                Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                    xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                    xmlEp = Clean(xmlEp)
                                    If xmlEp.Aired = airedDate AndAlso xmlEp.Season = seasonNumber Then
                                        xmlSer = Nothing
                                        Return xmlEp
                                    End If
                                End Using
                            Next
                        End If
                    End Using

                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.TVEpisode.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_TVEpisode(nfoPath, True)
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.TVEpisode.SourceSettings.OverWriteNfo Then
                    RenameNonConfNFO_TVEpisode(nfoPath, True)
                End If
            End Try
        End If

        Return New MediaContainers.MainDetails
    End Function

    Public Shared Function LoadFromNFO_TVShow(ByVal nfoPath As String) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlShow As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(nfoPath) Then
            Try
                If File.Exists(nfoPath) AndAlso Path.GetExtension(nfoPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(nfoPath)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                        xmlShow = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MainDetails)
                        xmlShow = Clean(xmlShow)
                    End Using
                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.TVShow.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_TVShow(nfoPath)
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.TVShow.SourceSettings.OverWriteNfo Then
                    RenameNonConfNFO_TVShow(nfoPath)
                End If
            End Try

            Try
                Dim params As New List(Of Object)(New Object() {xmlShow})
                Dim doContinue As Boolean = True
                AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFORead_TVShow, params, doContinue, False)

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlShow
    End Function

    Private Shared Sub RenameNonConfNFO_Movie(ByVal nfoPath As String, ByVal isChecked As Boolean)
        'test if current nfo is non-conforming... rename per setting
        Try
            If isChecked OrElse Not IsConformingNFO(nfoPath) Then
                If isChecked OrElse File.Exists(nfoPath) Then
                    RenameToInfo(nfoPath)
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNFO_TVEpisode(ByVal nfoPath As String, ByVal isChecked As Boolean)
        'test if current nfo is non-conforming... rename per setting

        Try
            If File.Exists(nfoPath) AndAlso Not IsConformingNFO_TVEpisode(nfoPath) Then
                RenameToInfo(nfoPath)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNFO_TVShow(ByVal nfoPath As String)
        'test if current nfo is non-conforming... rename per setting

        Try
            If File.Exists(nfoPath) AndAlso Not IsConformingNFO(nfoPath) Then
                RenameToInfo(nfoPath)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameToInfo(ByVal nfoPath As String)
        Try
            Dim fInfo As New FileInfo(nfoPath)
            Dim i As Integer = 1
            Dim strFullNameNoExt As String = Regex.Replace(fInfo.FullName, fInfo.Extension, String.Empty)
            Dim strNewFileName As String = String.Format("{0}.info", strFullNameNoExt)
            'in case there is already a .info file
            If File.Exists(strNewFileName) Then
                Do
                    strNewFileName = String.Format("{0}({1}).info", strFullNameNoExt, i)
                    i += 1
                Loop While File.Exists(strNewFileName)
            End If
            File.Move(nfoPath, strFullNameNoExt)
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_Movie(ByRef dbElement As Database.DBElement, ByVal forceFileCleanup As Boolean)
        Try
            Try
                Dim params As New List(Of Object)(New Object() {dbElement})
                Dim doContinue As Boolean = True
                Select Case dbElement.ContentType
                    Case Enums.ContentType.Movie
                        AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_Movie, params, doContinue, False)
                    Case Enums.ContentType.Movieset
                        AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_Movieset, params, doContinue, False)
                    Case Enums.ContentType.TVEpisode
                        AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_TVEpisode, params, doContinue, False)
                    Case Enums.ContentType.TVSeason
                        AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_TVSeason, params, doContinue, False)
                    Case Enums.ContentType.TVShow
                        AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_TVShow, params, doContinue, False)
                End Select
                If Not doContinue Then Return
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If dbElement.FileItemSpecified Then
                'cleanup old NFOs if needed
                If forceFileCleanup Then Delete(dbElement, forceFileCleanup)

                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tMovie As MediaContainers.MainDetails = CType(dbElement.MainDetails.CloneDeep, MediaContainers.MainDetails)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.MainDetails))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'YAMJ support
                If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieNFOYAMJ Then
                    If tMovie.UniqueIDs.TMDbIdSpecified Then
                        tMovie.UniqueIDs.TMDbId = String.Empty
                    End If
                End If

                'digit grouping symbol for Votes count
                If Master.eSettings.Options.Global.DigitGrpSymbolVotesEnabled Then
                    If tMovie.VotesSpecified Then
                        Dim vote As String = Double.Parse(tMovie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        If vote IsNot Nothing Then tMovie.Votes = vote
                    End If
                End If

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
                    If Not Master.eSettings.Movie.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_Movie(a, False)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then
                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If
                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, tMovie)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_Movieset(ByRef dbElement As Database.DBElement)
        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {moviesetToSave})
            '    Dim doContinue As Boolean = True
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieSetNFOSave, params, doContinue, False)
            '    If Not doContinue Then Return
            'Catch ex As Exception
            'End Try

            If Not String.IsNullOrEmpty(dbElement.MainDetails.Title) Then
                If dbElement.MainDetails.Title_HasChanged Then Delete(dbElement, True)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.MainDetails))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
                    'If Not Master.eSettings.GeneralOverwriteNfo Then
                    '    RenameNonConfNfo(a, False)
                    'End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then
                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If
                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, dbElement.MainDetails)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_TVEpisode(ByRef dbElement As Database.DBElement)
        Try
            If dbElement.FileItemSpecified Then
                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tTVEpisode As MediaContainers.MainDetails = CType(dbElement.MainDetails.CloneDeep, MediaContainers.MainDetails)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.MainDetails))

                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True
                Dim EpList As New List(Of MediaContainers.MainDetails)
                Dim sBuilder As New StringBuilder

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.EpisodeNFO)
                    If Not Master.eSettings.TVEpisode.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_TVEpisode(a, False)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then

                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If

                        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            'TODO: move that query to clsAPIDatabase
                            SQLCommand.CommandText = "SELECT idEpisode FROM episode WHERE idEpisode <> (?) AND idFile IN (SELECT idFile FROM file WHERE path = (?)) ORDER BY Episode"
                            Dim parID As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parID", DbType.Int64, 0, "idEpisode")
                            Dim parPath As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parPath", DbType.String, 0, "path")

                            parID.Value = dbElement.ID
                            parPath.Value = dbElement.FileItem.FullPath

                            Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                                While SQLreader.Read
                                    EpList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), False).MainDetails)
                                End While
                            End Using

                            EpList.Add(tTVEpisode)

                            Dim NS As New XmlSerializerNamespaces
                            NS.Add(String.Empty, String.Empty)

                            For Each tvEp As MediaContainers.MainDetails In EpList.OrderBy(Function(s) s.Season).OrderBy(Function(e) e.Episode)

                                'digit grouping symbol for Votes count
                                If Master.eSettings.Options.Global.DigitGrpSymbolVotesEnabled Then
                                    If tvEp.VotesSpecified Then
                                        Dim vote As String = Double.Parse(tvEp.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                                        If vote IsNot Nothing Then tvEp.Votes = vote
                                    End If
                                End If

                                'removing <displayepisode> and <displayseason> if disabled
                                If Not Master.eSettings.TVScraperUseDisplaySeasonEpisode Then
                                    tvEp.DisplayEpisode = -1
                                    tvEp.DisplaySeason = -1
                                End If

                                Using xmlSW As New Utf8StringWriter
                                    xmlSer.Serialize(xmlSW, tvEp, NS)
                                    If sBuilder.Length > 0 Then
                                        sBuilder.Append(Environment.NewLine)
                                        xmlSW.GetStringBuilder.Remove(0, xmlSW.GetStringBuilder.ToString.IndexOf(Environment.NewLine) + 1)
                                    End If
                                    sBuilder.Append(xmlSW.ToString)
                                End Using
                            Next

                            dbElement.NfoPath = a

                            If sBuilder.Length > 0 Then
                                Using fSW As New StreamWriter(a)
                                    fSW.Write(sBuilder.ToString)
                                End Using
                            End If
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_TVShow(ByRef dbElement As Database.DBElement)
        Try
            Dim params As New List(Of Object)(New Object() {dbElement})
            Dim doContinue As Boolean = True
            AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_TVShow, params, doContinue, False)
            If Not doContinue Then Return
        Catch ex As Exception
        End Try

        Try
            If dbElement.ShowPathSpecified Then
                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tTVShow As MediaContainers.MainDetails = CType(dbElement.MainDetails.CloneDeep, MediaContainers.MainDetails)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.MainDetails))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'Boxee support
                If Master.eSettings.TVUseBoxee Then
                    If tTVShow.UniqueIDs.TVDbIdSpecified Then
                        tTVShow.BoxeeTvDb = tTVShow.UniqueIDs.TVDbId
                        'tTVShow.BlankId()
                    End If
                End If

                'digit grouping symbol for Votes count
                If Master.eSettings.Options.Global.DigitGrpSymbolVotesEnabled Then
                    If tTVShow.VotesSpecified Then
                        Dim vote As String = Double.Parse(tTVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        If vote IsNot Nothing Then tTVShow.Votes = vote
                    End If
                End If

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
                    If Not Master.eSettings.TVShow.SourceSettings.OverWriteNfo Then
                        RenameNonConfNFO_TVShow(a)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then

                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If

                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, tTVShow)
                        End Using

                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class NonConf

#Region "Properties"

        Public Property IMDBID() As String = String.Empty

        Public Property Text() As String = String.Empty

#End Region 'Properties

    End Class

    Public NotInheritable Class Utf8StringWriter
        Inherits StringWriter
        Public Overloads Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return Encoding.UTF8
            End Get
        End Property
    End Class

#End Region 'Nested Types

End Class