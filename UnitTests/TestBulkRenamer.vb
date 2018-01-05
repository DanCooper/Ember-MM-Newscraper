Imports generic.EmberCore.BulkRename
Imports EmberAPI

<TestClass()> Public Class TestBulkRenamer

    Private _fDummyMultiEpisode As New FileFolderRenamer.FileRename
    Private _fDummyMultiSeason As New FileFolderRenamer.FileRename
    Private _fDummySingleEpisode As New FileFolderRenamer.FileRename
    Private _fDummySingleMovie As New FileFolderRenamer.FileRename

    Private Sub CreateDummies()
        _fDummyMultiEpisode.Clear()
        _fDummyMultiEpisode.AudioChannels = "2"
        _fDummyMultiEpisode.AudioCodec = "mp3"
        _fDummyMultiEpisode.BasePath = ""
        _fDummyMultiEpisode.Collection = ""
        _fDummyMultiEpisode.Country = ""
        _fDummyMultiEpisode.Director = ""
        _fDummyMultiEpisode.DirExist = False
        _fDummyMultiEpisode.FileExist = False
        _fDummyMultiEpisode.FullAudioInfo = New List(Of MediaContainers.Audio) From {
            New MediaContainers.Audio With {.Language = "eng", .Channels = "6", .Codec = "dts"},
            New MediaContainers.Audio With {.Language = "deu", .Channels = "2", .Codec = "ac3"},
            New MediaContainers.Audio With {.Language = "", .Channels = "1", .Codec = "mp3"}
        }
        _fDummyMultiEpisode.OldFileName = "OldFileName"
        _fDummyMultiEpisode.Genre = "Comedy / Lovestory"
        _fDummyMultiEpisode.ID = -1
        _fDummyMultiEpisode.IMDB = ""
        _fDummyMultiEpisode.IsBDMV = False
        _fDummyMultiEpisode.IsLock = False
        _fDummyMultiEpisode.IsMultiEpisode = True
        _fDummyMultiEpisode.DoRename = False
        _fDummyMultiEpisode.IsSingle = True
        _fDummyMultiEpisode.IsVideoTS = False
        _fDummyMultiEpisode.ListTitle = "Big Bang Theory, The"
        _fDummyMultiEpisode.MPAA = "TV-14"
        _fDummyMultiEpisode.MultiViewCount = "3d"
        _fDummyMultiEpisode.MultiViewLayout = "Side by Side (left eye first)"
        _fDummyMultiEpisode.NewFileName = ""
        _fDummyMultiEpisode.NewPath = ""
        _fDummyMultiEpisode.OldPath = ""
        _fDummyMultiEpisode.OriginalTitle = ""
        _fDummyMultiEpisode.Parent = "OldDirectoryName"
        _fDummyMultiEpisode.Path = ""
        _fDummyMultiEpisode.Rating = "7.3"
        _fDummyMultiEpisode.Resolution = "720p"
        _fDummyMultiEpisode.ShortStereoMode = "sbs"
        _fDummyMultiEpisode.ShowTitle = "The Big Bang Theory"
        _fDummyMultiEpisode.SortTitle = "Big Bang Theory"
        _fDummyMultiEpisode.StereoMode = "left_right"
        _fDummyMultiEpisode.Title = "Pilot"
        _fDummyMultiEpisode.TVDBID = "58056"
        _fDummyMultiEpisode.VideoCodec = "xvid"
        _fDummyMultiEpisode.VideoSource = "dvd"
        _fDummyMultiEpisode.Year = "2007"
        Dim dMEpisode1 As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dMEpisode2 As New FileFolderRenamer.Episode With {.ID = 2, .Episode = 2, .Title = "The Big Bran Hypothesis"}
        Dim dMEpisodeList As New List(Of FileFolderRenamer.Episode)
        dMEpisodeList.Add(dMEpisode1)
        dMEpisodeList.Add(dMEpisode2)
        _fDummyMultiEpisode.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dMEpisodeList})

        _fDummyMultiSeason.Clear()
        _fDummyMultiSeason.AudioChannels = "2"
        _fDummyMultiSeason.AudioCodec = "mp3"
        _fDummyMultiSeason.BasePath = ""
        _fDummyMultiSeason.Collection = ""
        _fDummyMultiSeason.Country = ""
        _fDummyMultiSeason.Director = ""
        _fDummyMultiSeason.DirExist = False
        _fDummyMultiSeason.FileExist = False
        _fDummyMultiSeason.FullAudioInfo = New List(Of MediaContainers.Audio) From {
            New MediaContainers.Audio With {.Language = "eng", .Channels = "6", .Codec = "dts"},
            New MediaContainers.Audio With {.Language = "deu", .Channels = "2", .Codec = "ac3"},
            New MediaContainers.Audio With {.Language = "", .Channels = "1", .Codec = "mp3"}
        }
        _fDummyMultiSeason.OldFileName = "OldFileName"
        _fDummyMultiSeason.Genre = "Comedy / Lovestory"
        _fDummyMultiSeason.ID = -1
        _fDummyMultiSeason.IMDB = ""
        _fDummyMultiSeason.IsBDMV = False
        _fDummyMultiSeason.IsLock = False
        _fDummyMultiSeason.IsMultiEpisode = True
        _fDummyMultiSeason.DoRename = False
        _fDummyMultiSeason.IsSingle = True
        _fDummyMultiSeason.IsVideoTS = False
        _fDummyMultiSeason.ListTitle = "Big Bang Theory, The"
        _fDummyMultiSeason.MPAA = "TV-14"
        _fDummyMultiSeason.MultiViewCount = "3d"
        _fDummyMultiSeason.MultiViewLayout = "Side by Side (left eye first)"
        _fDummyMultiSeason.NewFileName = ""
        _fDummyMultiSeason.NewPath = ""
        _fDummyMultiSeason.OldPath = ""
        _fDummyMultiSeason.OriginalTitle = ""
        _fDummyMultiSeason.Parent = "OldDirectoryName"
        _fDummyMultiSeason.Path = ""
        _fDummyMultiSeason.Rating = "7.3"
        _fDummyMultiSeason.Resolution = "720p"
        _fDummyMultiSeason.ShortStereoMode = "sbs"
        _fDummyMultiSeason.ShowTitle = "The Big Bang Theory"
        _fDummyMultiSeason.SortTitle = "Big Bang Theory"
        _fDummyMultiSeason.StereoMode = "left_right"
        _fDummyMultiSeason.Title = "Pilot"
        _fDummyMultiSeason.TVDBID = "58056"
        _fDummyMultiSeason.VideoCodec = "xvid"
        _fDummyMultiSeason.VideoSource = "dvd"
        _fDummyMultiSeason.Year = "2007"
        Dim dMSEpisode1 As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dMSEpisode2 As New FileFolderRenamer.Episode With {.ID = 2, .Episode = 2, .Title = "The Big Bran Hypothesis"}
        Dim dMSEpisode3 As New FileFolderRenamer.Episode With {.ID = 3, .Episode = 1, .Title = "The Bad Fish Paradigm"}
        Dim dMSEpisode4 As New FileFolderRenamer.Episode With {.ID = 4, .Episode = 2, .Title = "The Codpiece Topology"}
        Dim dMSEpisodeList1 As New List(Of FileFolderRenamer.Episode)
        Dim dMSEpisodeList2 As New List(Of FileFolderRenamer.Episode)
        dMSEpisodeList1.Add(dMSEpisode1)
        dMSEpisodeList1.Add(dMSEpisode2)
        dMSEpisodeList2.Add(dMSEpisode3)
        dMSEpisodeList2.Add(dMSEpisode4)
        _fDummyMultiSeason.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dMSEpisodeList1})
        _fDummyMultiSeason.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 2, .Episodes = dMSEpisodeList2})

        _fDummySingleEpisode.Clear()
        _fDummySingleEpisode.AudioChannels = "2"
        _fDummySingleEpisode.AudioCodec = "mp3"
        _fDummySingleEpisode.BasePath = ""
        _fDummySingleEpisode.Collection = ""
        _fDummySingleEpisode.Country = ""
        _fDummySingleEpisode.Director = ""
        _fDummySingleEpisode.DirExist = False
        _fDummySingleEpisode.FileExist = False
        _fDummySingleEpisode.FullAudioInfo = New List(Of MediaContainers.Audio) From {
            New MediaContainers.Audio With {.Language = "eng", .Channels = "6", .Codec = "dts"},
            New MediaContainers.Audio With {.Language = "deu", .Channels = "2", .Codec = "ac3"},
            New MediaContainers.Audio With {.Language = "", .Channels = "1", .Codec = "mp3"}
            }
        _fDummySingleEpisode.OldFileName = "OldFileName"
        _fDummySingleEpisode.Genre = "Comedy / Lovestory"
        _fDummySingleEpisode.ID = -1
        _fDummySingleEpisode.IMDB = ""
        _fDummySingleEpisode.IsBDMV = False
        _fDummySingleEpisode.IsLock = False
        _fDummySingleEpisode.IsMultiEpisode = False
        _fDummySingleEpisode.DoRename = False
        _fDummySingleEpisode.IsSingle = True
        _fDummySingleEpisode.IsVideoTS = False
        _fDummySingleEpisode.ListTitle = "Big Bang Theory, The"
        _fDummySingleEpisode.MPAA = "TV-14"
        _fDummySingleEpisode.MultiViewCount = "3d"
        _fDummySingleEpisode.MultiViewLayout = "Side by Side (left eye first)"
        _fDummySingleEpisode.NewFileName = ""
        _fDummySingleEpisode.NewPath = ""
        _fDummySingleEpisode.OldPath = ""
        _fDummySingleEpisode.OriginalTitle = ""
        _fDummySingleEpisode.Parent = "OldDirectoryName"
        _fDummySingleEpisode.Path = ""
        _fDummySingleEpisode.Rating = "7.3"
        _fDummySingleEpisode.Resolution = "720p"
        _fDummySingleEpisode.ShortStereoMode = "sbs"
        _fDummySingleEpisode.ShowTitle = "The Big Bang Theory"
        _fDummySingleEpisode.SortTitle = "Big Bang Theory"
        _fDummySingleEpisode.StereoMode = "left_right"
        _fDummySingleEpisode.Title = "Pilot"
        _fDummySingleEpisode.TVDBID = "58056"
        _fDummySingleEpisode.VideoCodec = "xvid"
        _fDummySingleEpisode.VideoSource = "dvd"
        _fDummySingleEpisode.Year = "2007"
        Dim dSEpisode As New FileFolderRenamer.Episode With {.ID = 1, .Episode = 1, .Title = "Pilot"}
        Dim dSEpisodeList As New List(Of FileFolderRenamer.Episode)
        dSEpisodeList.Add(dSEpisode)
        _fDummySingleEpisode.SeasonsEpisodes.Add(New FileFolderRenamer.SeasonsEpisodes With {.Season = 1, .Episodes = dSEpisodeList})

        _fDummySingleMovie.Clear()
        _fDummySingleMovie.AudioChannels = "6"
        _fDummySingleMovie.AudioCodec = "dts"
        _fDummySingleMovie.BasePath = "D:\Movies"
        _fDummySingleMovie.Collection = "The Avengers Collection"
        _fDummySingleMovie.Country = "United States of America / Japan"
        _fDummySingleMovie.Director = "Joss Whedon"
        _fDummySingleMovie.DirExist = False
        _fDummySingleMovie.FileExist = False
        _fDummySingleMovie.FullAudioInfo = New List(Of MediaContainers.Audio) From {
            New MediaContainers.Audio With {.Language = "eng", .Channels = "6", .Codec = "dts"},
            New MediaContainers.Audio With {.Language = "deu", .Channels = "2", .Codec = "ac3"},
            New MediaContainers.Audio With {.Language = "", .Channels = "1", .Codec = "mp3"}
         }
        _fDummySingleMovie.OldFileName = "OldFileName"
        _fDummySingleMovie.Genre = "Action / Sci-Fi"
        _fDummySingleMovie.ID = -1
        _fDummySingleMovie.IMDB = "tt0848228"
        _fDummySingleMovie.IsBDMV = False
        _fDummySingleMovie.IsLock = False
        _fDummySingleMovie.IsMultiEpisode = False
        _fDummySingleMovie.DoRename = False
        _fDummySingleMovie.IsSingle = True
        _fDummySingleMovie.IsVideoTS = False
        _fDummySingleMovie.ListTitle = "Avengers, The"
        _fDummySingleMovie.MPAA = "13"
        _fDummySingleMovie.MultiViewCount = "3d"
        _fDummySingleMovie.MultiViewLayout = "Side by Side (left eye first)"
        _fDummySingleMovie.NewFileName = ""
        _fDummySingleMovie.NewPath = ""
        _fDummySingleMovie.OldPath = ""
        _fDummySingleMovie.OriginalTitle = "Marvel's The Avengers"
        _fDummySingleMovie.Parent = "OldDirectoryName"
        _fDummySingleMovie.Path = ""
        _fDummySingleMovie.Rating = "7.3"
        _fDummySingleMovie.Resolution = "1080p"
        _fDummySingleMovie.ShortStereoMode = "sbs"
        _fDummySingleMovie.ShowTitle = ""
        _fDummySingleMovie.SortTitle = "Avengers"
        _fDummySingleMovie.StereoMode = "left_right"
        _fDummySingleMovie.Title = "The Avengers"
        _fDummySingleMovie.TVDBID = ""
        _fDummySingleMovie.VideoCodec = "h264"
        _fDummySingleMovie.VideoSource = "bluray"
        _fDummySingleMovie.Year = "2012"
    End Sub

    <TestMethod()> Public Sub MovieTests()
        CreateDummies()

        Dim tests = New Dictionary(Of String, String) From {
                {"$TITLE$", "The Avengers"},
                {"$GENRE%,$", "Action,Sci-Fi"},
                {"$TITLE${ ($YEAR$)}", "The Avengers (2012)"},
                {"$TITLE${ ($YEAR$)}{.$TVDBID$}{.$IMDB$}", "The Avengers (2012).tt0848228"},
                {"$TITLE${ ($YEAR$)}{.$TVDBID$.$IMDB$}", "The Avengers (2012)..tt0848228"},
                {"$AINFO%l-c-n;,;ch;0;und$", "eng-dts-6ch,deu-ac3-2ch,und-mp3-1ch"},
                {"$AINFO%l,c,n;\;;ch;2;und$", "eng,dts,6ch;deu,ac3,2ch"},
                {"$AINFO%l\$c\$n;\;;ch;2;und$", "eng$dts$6ch;deu$ac3$2ch"},
                {"$AINFO%l,c,n;.;ch;0;SKIP$", "eng,dts,6ch.deu,ac3,2ch"},
                {"$AINFO%l,c,n;.;CH;1;und$", "eng,dts,6CH"},
                {"$AINFO%l|c|n;.;Kanäle;1;und$", "engdts6Kanäle"}
            }

        For Each key In tests.Keys
            Assert.AreEqual(tests(key), FileFolderRenamer.ProcessPattern(_fDummySingleMovie, key, False, False))
        Next
    End Sub

    <TestMethod()> Public Sub EpisodeTests()
        CreateDummies()

        Dim tests = New Dictionary(Of String, String) From {
                {"$TITLE$", "Pilot"},
                {"$GENRE%,$", "Comedy,Lovestory"},
                {"$TITLE${ ($YEAR$)}", "Pilot (2007)"},
                {"$TITLE${ ($YEAR$)}{.$TVDBID$}{.$IMDB$}", "Pilot (2007).58056"},
                {"$TITLE${ ($YEAR$)}{.$TVDBID$.$IMDB$}", "Pilot (2007).58056"},
                {"$AINFO%l-c-n;,;ch;0;und$", "eng-dts-6ch,deu-ac3-2ch,und-mp3-1ch"},
                {"$EPISODE$", "1"},
                {"$EPISODE%E;2$", "E01"}
            }

        For Each key In tests.Keys
            Assert.AreEqual(tests(key), FileFolderRenamer.ProcessPattern(_fDummySingleEpisode, key, False, False))
        Next
    End Sub

End Class