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

Imports EmberAPI
Imports NLog
Imports System.IO
Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("Settings")>
Public Class MSettings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    Public Property MainOptions As MainOptions = New MainOptions

    Public Property Movie As MovieBase = New MovieBase

    Public Property Movieset As MoviesetBase = New MoviesetBase

    Public Property TVEpisode As TVEpisodeBase = New TVEpisodeBase

    Public Property TVSeason As TVSeasonBase = New TVSeasonBase

    Public Property TVShow As TVShowBase = New TVShowBase

#End Region 'Properties

#Region "Methods"

    Public Sub Load()
        Dim configpath As String = Path.Combine(Master.SettingsPath, "ManagerSettings.xml")

        Try
            If File.Exists(configpath) Then
                Dim xmlSer As New XmlSerializer(GetType(MSettings))
                Using xmlSR As StreamReader = New StreamReader(configpath)
                    Manager.mSettings = DirectCast(xmlSer.Deserialize(xmlSR), MSettings)
                End Using
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub Save()
        Try
            Dim xmlSerial As New XmlSerializer(GetType(MSettings))
            Dim xmlWriter As New StreamWriter(Path.Combine(Master.SettingsPath, "ManagerSettings.xml"))
            xmlSerial.Serialize(xmlWriter, Manager.mSettings)
            xmlWriter.Close()
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function GetDefaultsForList_MediaListSorting_Movie() As List(Of GuiBase.ListSorting)
        Return New List(Of GuiBase.ListSorting) From {
            New GuiBase.ListSorting With {.DisplayIndex = 0, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), .LabelID = 21, .LabelText = "Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 1, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle), .LabelID = 302, .LabelText = "Original Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 2, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Year), .LabelID = 278, .LabelText = "Year"},
            New GuiBase.ListSorting With {.DisplayIndex = 3, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.MPAA), .LabelID = 401, .LabelText = "MPAA"},
            New GuiBase.ListSorting With {.DisplayIndex = 4, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.UserRating), .LabelID = 1467, .LabelText = "User Rating"},
            New GuiBase.ListSorting With {.DisplayIndex = 5, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Top250), .LabelID = 0, .LabelText = "Top250"},
            New GuiBase.ListSorting With {.DisplayIndex = 6, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"},
            New GuiBase.ListSorting With {.DisplayIndex = 7, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"},
            New GuiBase.ListSorting With {.DisplayIndex = 8, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), .LabelID = 1096, .LabelText = "ClearArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 9, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), .LabelID = 1097, .LabelText = "ClearLogo"},
            New GuiBase.ListSorting With {.DisplayIndex = 10, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath), .LabelID = 1098, .LabelText = "DiscArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 11, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath), .LabelID = 992, .LabelText = "Extrafanarts"},
            New GuiBase.ListSorting With {.DisplayIndex = 12, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath), .LabelID = 153, .LabelText = "Extrathumbs"},
            New GuiBase.ListSorting With {.DisplayIndex = 13, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"},
            New GuiBase.ListSorting With {.DisplayIndex = 14, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), .LabelID = 296, .LabelText = "KeyArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 15, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"},
            New GuiBase.ListSorting With {.DisplayIndex = 16, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"},
            New GuiBase.ListSorting With {.DisplayIndex = 17, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles), .LabelID = 152, .LabelText = "Subtitles"},
            New GuiBase.ListSorting With {.DisplayIndex = 18, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ThemePath), .LabelID = 1118, .LabelText = "Theme"},
            New GuiBase.ListSorting With {.DisplayIndex = 19, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath), .LabelID = 151, .LabelText = "Trailer"},
            New GuiBase.ListSorting With {.DisplayIndex = 20, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset), .LabelID = 1295, .LabelText = "Part of a MovieSet"},
            New GuiBase.ListSorting With {.DisplayIndex = 21, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed), .LabelID = 981, .LabelText = "Watched"}
        }
    End Function

    Public Function GetDefaultsForList_MediaListSorting_Movieset() As List(Of GuiBase.ListSorting)
        Return New List(Of GuiBase.ListSorting) From {
            New GuiBase.ListSorting With {.DisplayIndex = 0, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), .LabelID = 21, .LabelText = "Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 1, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"},
            New GuiBase.ListSorting With {.DisplayIndex = 2, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"},
            New GuiBase.ListSorting With {.DisplayIndex = 3, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), .LabelID = 1096, .LabelText = "ClearArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 4, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), .LabelID = 1097, .LabelText = "ClearLogo"},
            New GuiBase.ListSorting With {.DisplayIndex = 5, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath), .LabelID = 1098, .LabelText = "DiscArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 6, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"},
            New GuiBase.ListSorting With {.DisplayIndex = 7, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), .LabelID = 296, .LabelText = "KeyArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 8, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"},
            New GuiBase.ListSorting With {.DisplayIndex = 9, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"}
        }
    End Function

    Public Function GetDefaultsForList_MediaListSorting_TVEpisode() As List(Of GuiBase.ListSorting)
        Return New List(Of GuiBase.ListSorting) From {
            New GuiBase.ListSorting With {.DisplayIndex = 0, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Title), .LabelID = 21, .LabelText = "Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 1, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle), .LabelID = 302, .LabelText = "Original Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 2, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.UserRating), .LabelID = 1467, .LabelText = "User Rating"},
            New GuiBase.ListSorting With {.DisplayIndex = 3, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"},
            New GuiBase.ListSorting With {.DisplayIndex = 4, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"},
            New GuiBase.ListSorting With {.DisplayIndex = 5, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"},
            New GuiBase.ListSorting With {.DisplayIndex = 6, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles), .LabelID = 152, .LabelText = "Subtitles"},
            New GuiBase.ListSorting With {.DisplayIndex = 7, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed), .LabelID = 981, .LabelText = "Watched"}
        }
    End Function

    Public Function GetDefaultsForList_MediaListSorting_TVSeason() As List(Of GuiBase.ListSorting)
        Return New List(Of GuiBase.ListSorting) From {
            New GuiBase.ListSorting With {.DisplayIndex = 0, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Title), .LabelID = 650, .LabelText = "Season"},
            New GuiBase.ListSorting With {.DisplayIndex = 1, .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount), .LabelID = 682, .LabelText = "Episodes"},
            New GuiBase.ListSorting With {.DisplayIndex = 2, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"},
            New GuiBase.ListSorting With {.DisplayIndex = 3, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"},
            New GuiBase.ListSorting With {.DisplayIndex = 4, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"},
            New GuiBase.ListSorting With {.DisplayIndex = 5, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"},
            New GuiBase.ListSorting With {.DisplayIndex = 6, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasWatched), .LabelID = 981, .LabelText = "Watched"}
        }
    End Function

    Public Function GetDefaultsForList_MediaListSorting_TVShow() As List(Of GuiBase.ListSorting)
        Return New List(Of GuiBase.ListSorting) From {
            New GuiBase.ListSorting With {.DisplayIndex = 0, .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), .LabelID = 21, .LabelText = "Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 1, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle), .LabelID = 302, .LabelText = "Original Title"},
            New GuiBase.ListSorting With {.DisplayIndex = 2, .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Status), .LabelID = 215, .LabelText = "Status"},
            New GuiBase.ListSorting With {.DisplayIndex = 3, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.MPAA), .LabelID = 401, .LabelText = "MPAA"},
            New GuiBase.ListSorting With {.DisplayIndex = 4, .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.UserRating), .LabelID = 1467, .LabelText = "User Rating"},
            New GuiBase.ListSorting With {.DisplayIndex = 5, .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader, .Show = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount), .LabelID = 682, .LabelText = "Episodes"},
            New GuiBase.ListSorting With {.DisplayIndex = 6, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"},
            New GuiBase.ListSorting With {.DisplayIndex = 7, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"},
            New GuiBase.ListSorting With {.DisplayIndex = 8, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath), .LabelID = 1140, .LabelText = "CharacterArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 9, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), .LabelID = 1096, .LabelText = "ClearArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 10, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), .LabelID = 1097, .LabelText = "ClearLogo"},
            New GuiBase.ListSorting With {.DisplayIndex = 11, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath), .LabelID = 992, .LabelText = "Extrafanarts"},
            New GuiBase.ListSorting With {.DisplayIndex = 12, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"},
            New GuiBase.ListSorting With {.DisplayIndex = 13, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), .LabelID = 296, .LabelText = "KeyArt"},
            New GuiBase.ListSorting With {.DisplayIndex = 14, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"},
            New GuiBase.ListSorting With {.DisplayIndex = 15, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"},
            New GuiBase.ListSorting With {.DisplayIndex = 16, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ThemePath), .LabelID = 1118, .LabelText = "Theme"},
            New GuiBase.ListSorting With {.DisplayIndex = 17, .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet, .Show = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasWatched), .LabelID = 981, .LabelText = "Watched"}
        }
    End Function

    Public Sub SetDefaultsForLists(ByVal Type As Enums.DefaultType, ByVal Force As Boolean)

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ListSorting_Movie) AndAlso (Force OrElse Not Manager.mSettings.Movie.GuiSettings.MediaListSortingSecified) Then
            Manager.mSettings.Movie.GuiSettings.MediaListSorting = GetDefaultsForList_MediaListSorting_Movie()
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ListSorting_Movieset) AndAlso (Force OrElse Not Manager.mSettings.Movieset.GuiSettings.MediaListSortingSecified) Then
            Manager.mSettings.Movieset.GuiSettings.MediaListSorting = GetDefaultsForList_MediaListSorting_Movieset()
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ListSorting_TVEpisode) AndAlso (Force OrElse Not Manager.mSettings.TVEpisode.GuiSettings.MediaListSortingSecified) Then
            Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting = GetDefaultsForList_MediaListSorting_TVEpisode()
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ListSorting_TVSeason) AndAlso (Force OrElse Not Manager.mSettings.TVSeason.GuiSettings.MediaListSortingSecified) Then
            Manager.mSettings.TVSeason.GuiSettings.MediaListSorting = GetDefaultsForList_MediaListSorting_TVSeason()
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ListSorting_TVShow) AndAlso (Force OrElse Not Manager.mSettings.TVShow.GuiSettings.MediaListSortingSecified) Then
            Manager.mSettings.TVShow.GuiSettings.MediaListSorting = GetDefaultsForList_MediaListSorting_TVShow()
        End If

    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class MainOptions

#Region "Properties"

    Public Property GuiSettings As MainGuiSettings = New MainGuiSettings

#End Region 'Properties

End Class

<Serializable()>
Public Class MovieBase

#Region "Properties"

    Public Property GuiSettings As GuiBase = New GuiBase

#End Region 'Properties

End Class

<Serializable()>
Public Class MoviesetBase

#Region "Properties"

    Public Property GuiSettings As GuiBase = New GuiBase

#End Region 'Properties

End Class

<Serializable()>
Public Class TVEpisodeBase

#Region "Properties"

    Public Property GuiSettings As GuiBase = New GuiBase

#End Region 'Properties

End Class

<Serializable()>
Public Class TVSeasonBase

#Region "Properties"

    Public Property GuiSettings As GuiBase = New GuiBase

#End Region 'Properties

End Class

<Serializable()>
Public Class TVShowBase

#Region "Properties"

    Public Property GuiSettings As GuiBase = New GuiBase

#End Region 'Properties

End Class


<Serializable()>
Public Class GuiBase

#Region "Properties"

    Public Property ClickScrapeEnabled As Boolean = False

    Public Property ClickScrapeShowResults As Boolean = False

    Public Property CustomScrapeButtonEnabled As Boolean = False

    Public Property CustomScrapeButtonModifierType As Enums.ModifierType = Enums.ModifierType.All

    Public Property CustomScrapeButtonScrapeType As Enums.ScrapeType = Enums.ScrapeType.Skip

    Public Property CustomScrapeButtonSelectionType As Enums.SelectionType = Enums.SelectionType.[New]

    Public Property DisplayMissingElements As Boolean = False

    Public Property MediaListSorting As List(Of ListSorting) = New List(Of ListSorting)

    <XmlIgnore>
    Public ReadOnly Property MediaListSortingSecified As Boolean
        Get
            Return MediaListSorting.Count > 0
        End Get
    End Property

    Public Property PreferredAudioLanguage As String = String.Empty

    <XmlIgnore>
    Public ReadOnly Property PreferredAudioLanguageSpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(PreferredAudioLanguage)
        End Get
    End Property

#End Region 'Properties

#Region "Nested Types"

    Public Class ListSorting

#Region "Properties"
        ''' <summary>
        ''' Column AutoSizeMode
        ''' </summary>
        ''' <returns></returns>
        Public Property AutoSizeMode As DataGridViewAutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet
        ''' <summary>
        ''' Column name in database (need to be exactly like column name in DB)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Column As String = String.Empty
        ''' <summary>
        ''' Position of the column in the media list
        ''' </summary>
        ''' <returns></returns>
        Public Property DisplayIndex As Integer = -1
        ''' <summary>
        ''' Hide or show column in the media list
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Show As Boolean = False
        ''' <summary>
        ''' ID of string in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelID As UInteger = 0
        ''' <summary>
        ''' Default text for the LabelID in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelText As String = String.Empty

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class


<Serializable()>
Public Class MainGuiSettings

    Public Property DisplayAudioChannelsFlag As Boolean = True
    Public Property DisplayAudioSourceFlag As Boolean = True
    Public Property DisplayBanner As Boolean = True
    Public Property DisplayCharacterart As Boolean = True
    Public Property DisplayClearart As Boolean = True
    Public Property DisplayClearlogo As Boolean = True
    Public Property DisplayDiscart As Boolean = True
    Public Property DisplayFanartAsBackground As Boolean = True
    Public Property DisplayFanart As Boolean = True
    Public Property DisplayGenreFlags As Boolean = True
    Public Property DisplayGenreText As Boolean = True
    Public Property DisplayImgageDimensions As Boolean = True
    Public Property DisplayImgageGlassOverlay As Boolean = False
    Public Property DisplayImgageNames As Boolean = True
    Public Property DisplayKeyart As Boolean = True
    Public Property DisplayLandscape As Boolean = True
    Public Property DisplayLanguageFlags As Boolean = True
    Public Property DisplayPoster As Boolean = True
    Public Property DisplayStudioFlag As Boolean = True
    Public Property DisplayStudioName As Boolean = False
    Public Property DisplayVideoCodecFlag As Boolean = True
    Public Property DisplayVideoResolutionFlag As Boolean = True
    Public Property DisplayVideoSourceFlag As Boolean = True
    Public Property DoubleClickScrapeEnabled As Boolean = False
    Public Property FilterPanelIsRaised_Movie As Boolean = False
    Public Property FilterPanelIsRaised_Movieset As Boolean = False
    Public Property FilterPanelIsRaised_TVShow As Boolean = False
    Public Property InfoPanelState_Movie As Integer = 2
    Public Property InfoPanelState_Movieset As Integer = 2
    Public Property InfoPanelState_TVEpisode As Integer = 2
    Public Property InfoPanelState_TVSeason As Integer = 2
    Public Property InfoPanelState_TVShow As Integer = 2
    Public Property MainFilterSortColumn_Movie As Integer = 3
    Public Property MainFilterSortColumn_Movieset As Integer = 1
    Public Property MainFilterSortColumn_TVEpisode As Integer = 1
    Public Property MainFilterSortColumn_TVSeason As Integer = 1
    Public Property MainFilterSortColumn_TVShow As Integer = 1
    Public Property MainFilterSortOrder_Movie As Integer = 0
    Public Property MainFilterSortOrder_Movieset As Integer = 0
    Public Property MainFilterSortOrder_TVEpisode As Integer = 0
    Public Property MainFilterSortOrder_TVSeason As Integer = 0
    Public Property MainFilterSortOrder_TVShow As Integer = 0
    Public Property SplitterDistance_MediaList As Integer = 550
    Public Property SplitterDistance_TVSeason As Integer = 200
    Public Property SplitterDistance_TVShow As Integer = 200
    Public Property Theme() As String = "FullHD-Default"
    Public Property WindowLoc() As Point = New Point(10, 10)
    Public Property WindowSize() As Size = New Size(1024, 768)
    Public Property WindowState() As FormWindowState = FormWindowState.Maximized

End Class