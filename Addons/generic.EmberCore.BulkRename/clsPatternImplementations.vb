﻿Public Class PatternImplementations

    ' ### IMPORTANT NOTICE ###
    ' There must not be any function in this class that
    ' follows the naming pattern "_Pattern_XXXX" but does
    ' not return a string as they are dynamically invoked.

#Region "Internal code"

    Public Property Arguments As List(Of String)
    Public Property FileRename As FileFolderRenamer.FileRename

    Private Function Argument(index As Integer, def As String) As String
        If Arguments.Count >= index + 1 Then
            Return Arguments.Item(index)
        Else
            Return def
        End If
    End Function

#End Region ' Internal code

#Region "Patterns"
    ' The functions in this section are dynamically invoked.
    ' Their names must start with '_Pattern_' and be followed 
    ' with the name of the pattern (uppercase by definition)

    Public Function _Pattern_AINFO() As String
        Dim order = Argument(0, "l-n-c")
        Dim separator = Argument(1, ".")
        Dim chSuffix = Argument(2, "ch")
        Dim maxCount = Integer.Parse(Argument(3, "0"))
        Dim unknownLanguages = Argument(4, "und")

        Dim result = String.Empty
        Dim count = 0
        For Each item In FileRename.FullAudioInfo
            Dim language = item.Language
            If unknownLanguages = "SKIP" AndAlso String.IsNullOrEmpty(language) Then
                Continue For
            End If

            If String.IsNullOrEmpty(language) Then
                language = unknownLanguages
            End If

            If count > 0 Then
                result += separator
            End If

            order = order.
                Replace("l", "_REPLACE_LANGUAGE_").
                Replace("n", "_REPLACE_CHANNELS_").
                Replace("c", "_REPLACE_CODEC_")

            result += order.
                Replace("_REPLACE_LANGUAGE_", language).
                Replace("_REPLACE_CHANNELS_", item.Channels + chSuffix).
                Replace("_REPLACE_CODEC_", item.Codec)

            count += 1
            If maxCount > 0 And count >= maxCount Then
                Exit For
            End If
        Next

        Return result.Trim()
    End Function

    Public Function _Pattern_AIRED() As String
        Return FileRename.Aired
    End Function

    Public Function _Pattern_STEREO() As String
        Return FileRename.StereoMode
    End Function

    Public Function _Pattern_SHORTSTEREO() As String
        Return FileRename.ShortStereoMode
    End Function

    Public Function _Pattern_DIRECTOR() As String
        Return FileRename.Director
    End Function

    Public Function _Pattern_PARENT() As String
        Return FileRename.Parent
    End Function

    Public Function _Pattern_TITLESORT() As String
        Return FileRename.SortTitle
    End Function

    Public Function _Pattern_VCODEC() As String
        Return FileRename.VideoCodec
    End Function

    Public Function _Pattern_IMDB() As String
        Return FileRename.IMDB
    End Function

    Public Function _Pattern_ACHANNELS() As String
        Return FileRename.AudioChannels
    End Function

    Public Function _Pattern_ACODEC() As String
        Return FileRename.AudioCodec
    End Function

    Public Function _Pattern_RES() As String
        Return FileRename.Resolution
    End Function

    Public Function _Pattern_MPAA() As String
        Return FileRename.MPAA
    End Function

    Public Function _Pattern_TITLE() As String
        Return FileRename.Title
    End Function

    Public Function _Pattern_SRC() As String
        Return FileRename.VideoSource
    End Function

    Public Function _Pattern_YEAR() As String
        Return FileRename.Year
    End Function

    Public Function _Pattern_COL() As String
        Return FileRename.Collection
    End Function

    Public Function _Pattern_LITLE() As String
        Return FileRename.ListTitle
    End Function

    Public Function _Pattern_STITLE() As String
        Return FileRename.ShowTitle
    End Function

    Public Function _Pattern_FNAME() As String
        Return FileRename.OldFileName.Replace("\", String.Empty)
    End Function

    Public Function _Pattern_OTITLE() As String
        Return FileRename.OriginalTitle
    End Function

    Public Function _Pattern_OOTITLE() As String
        Return If(Not FileRename.OriginalTitle = FileRename.Title, FileRename.OriginalTitle, String.Empty)
    End Function

    Public Function _Pattern_GENRE() As String
        Dim separator = Argument(0, " ")
        Return FileRename.Genre.Replace(" / ", separator)
    End Function

    Public Function _Pattern_COUNTRY() As String
        Dim separator = Argument(0, " ")
        Return FileRename.Country.Replace(" / ", separator)
    End Function

    Public Function _Pattern_RATING() As String
        If String.IsNullOrEmpty(FileRename.Rating) Then
            Return String.Empty
        End If
        Dim rating = Double.Parse(FileRename.Rating, Globalization.CultureInfo.InvariantCulture)
        Return $"{rating:0.0}"
    End Function

    Public Function _Pattern_VIEW() As String
        Return FileRename.MultiViewCount
    End Function

    Public Function _Pattern_BASEPATH() As String
        Return String.Empty ' Contents is not used. The pattern works as a switch in FileFolderRenamer.HaveBase
    End Function

    Public Function _Pattern_EPISODE() As String
        Dim prefix = Argument(0, String.Empty)
        Dim padding = Integer.Parse(Argument(1, "0"))
        Dim separator = Argument(2, " ")

        Dim eFormat = "{0:0}"
        For i = 1 To padding - 1
            eFormat = eFormat.Insert(eFormat.Length - 1, "0")
        Next

        Dim result = ""
        For Each season In FileRename.SeasonsEpisodes
            For Each episode In season.Episodes
                If Not String.IsNullOrEmpty(result) Then
                    result += separator
                End If
                result += prefix + String.Format(eFormat, episode.Episode)
                If Not episode.SubEpisode = -1 Then
                    result += "." + episode.SubEpisode.ToString()
                End If
            Next
        Next
        Return result
    End Function

    Public Function _Pattern_SEASON() As String
        Dim prefix = Argument(0, String.Empty)
        Dim padding = Integer.Parse(Argument(1, "0"))
        Dim separator = Argument(2, " ")

        Dim sFormat = "{0:0}"
        For i = 1 To padding - 1
            sFormat = sFormat.Insert(sFormat.Length - 1, "0")
        Next

        Dim result = ""
        For Each season In FileRename.SeasonsEpisodes
            If Not String.IsNullOrEmpty(result) Then
                result += separator
            End If
            result += prefix + String.Format(sFormat, season.Season)
        Next

        Return result
    End Function

    Public Function _Pattern_SEASONEPISODE() As String
        Dim sPrefix = Argument(0, String.Empty)
        Dim ePrefix = Argument(1, String.Empty)
        Dim sPadding = Integer.Parse(Argument(2, "0"))
        Dim ePadding = Integer.Parse(Argument(3, "0"))
        Dim sSeparator = Argument(4, " ")
        Dim eSeparator = Argument(5, sSeparator)

        Dim sFormat = "{0:0}"
        For i = 1 To sPadding - 1
            sFormat = sFormat.Insert(sFormat.Length - 1, "0")
        Next
        Dim eFormat = "{0:0}"
        For i = 1 To ePadding - 1
            eFormat = sFormat.Insert(sFormat.Length - 1, "0")
        Next

        Dim result = ""
        For Each season In FileRename.SeasonsEpisodes
            If Not String.IsNullOrEmpty(result) Then
                result += sSeparator
            End If
            result += result + sPrefix + String.Format(sFormat, season.Season)

            Dim sTemp = ""
            For Each episode In season.Episodes
                If Not String.IsNullOrEmpty(sTemp) Then
                    sTemp += eSeparator
                End If
                sTemp += ePrefix + String.Format(eFormat, episode.Episode)
                If Not episode.SubEpisode = -1 Then
                    sTemp += ePrefix + "." + episode.SubEpisode.ToString()
                End If
            Next
            result += sTemp
        Next

        Return result
    End Function

    Public Function _Pattern_ALLACODEC() As String
        Dim separator = Argument(0, " ")
        Dim maxCount = Integer.Parse(Argument(1, "0"))
        Dim count = 0
        Dim result = ""
        For Each item In FileRename.FullAudioInfo
            If Not String.IsNullOrEmpty(result) Then
                result += separator
            End If
            result += item.Codec

            count += 1
            If maxCount > 0 And count >= maxCount Then
                Exit For
            End If
        Next
        Return result
    End Function

    Public Function _Pattern_ALLACHANNEL() As String
        Dim separator = Argument(0, " ")
        Dim suffix = Argument(1, "ch")
        Dim maxCount = Integer.Parse(Argument(2, "0"))
        Dim count = 0
        Dim result = ""
        For Each item In FileRename.FullAudioInfo
            If Not String.IsNullOrEmpty(result) Then
                result += separator
            End If
            result += item.Channels + suffix

            count += 1
            If maxCount > 0 And count >= maxCount Then
                Exit For
            End If
        Next
        Return result
    End Function

    Public Function _Pattern_ALLALANG() As String
        Dim separator = Argument(0, " ")
        Dim unknown = Argument(1, "SKIP")
        Dim maxCount = Integer.Parse(Argument(2, "0"))
        Dim count = 0
        Dim result = ""
        For Each item In FileRename.FullAudioInfo
            If unknown = "SKIP" AndAlso String.IsNullOrEmpty(item.Language) Then
                Continue For
            End If

            If Not String.IsNullOrEmpty(result) Then
                result += separator
            End If
            result += item.Language

            count += 1
            If maxCount > 0 And count >= maxCount Then
                Exit For
            End If
        Next
        Return result
    End Function

    Public Function _Pattern_TVDBID() As String
        Return FileRename.TVDBID
    End Function

#End Region ' Patterns

End Class
