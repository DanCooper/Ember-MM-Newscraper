Public Class PatternImplementations

    Public Property Arguments() As List(Of String)
    Public Property FileRename() As FileFolderRenamer.FileRename

    Private Function Argument(ByVal index As Integer, ByVal def As String) As String
        If Arguments.Count >= index + 1 Then
            Return Arguments.Item(index)
        Else
            Return def
        End If
    End Function

    Public Function _Pattern_AINFO() As String
        Dim order = Argument(0, "l-n-c")
        Dim strSplit = Argument(1, ".")
        Dim channels = Argument(2, "ch")
        Dim maxCount = Integer.Parse(Argument(3, "0"))
        Dim unknownLanguages = Argument(4, "und")

        Dim result = String.Empty
        Dim count = 0
        For Each item In FileRename.FullAudioInfo
            Dim language = item("l")

            If unknownLanguages = "SKIP" AndAlso String.IsNullOrEmpty(language) Then
                Continue For
            End If

            If String.IsNullOrEmpty(language) Then
                language = unknownLanguages
            End If

            If count > 0 Then
                result += strSplit
            End If

            result += order.
                Replace("l", language).
                Replace("n", item("n")).
                Replace("c", item("c") + channels)

            count += 1
            If count >= maxCount Then
                Exit For
            End If
        Next

        Return result.Trim()
    End Function

End Class
