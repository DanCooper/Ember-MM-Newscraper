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

Public Class frmInfoPanel

    Public ReadOnly Property InfoPanel As Panel
        Get
            Return pnlInfoPanel
        End Get
    End Property



    Public Sub New()
        InitializeComponent()
        SetUp()
    End Sub

    Sub SetUp()
        lvFlags.Groups.Clear()
        lvFlags.Items.Clear()

        'create ListViewGroups
        Dim lvgAudio As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Audio")}
        Dim lvgDates As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Dates")}
        Dim lvgFiles As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Files & Folders")}
        Dim lvgInformation As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Additional Information")}
        Dim lvgModifiers As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Modifiers")}
        Dim lvgVideo As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Video")}
        Dim lvgTitles As New ListViewGroup With {.Header = Master.eLang.GetString(9999999, "Titles")}

        lvFlags.Groups.AddRange(New ListViewGroup() {
                                lvgTitles,
                                lvgDates,
                                lvgFiles,
                                lvgInformation,
                                lvgAudio,
                                lvgVideo,
                                lvgModifiers
                                })

        'Titles
        Dim lviTitle0 = New ListViewItem(New String() {
                                         "$TITLE$",
                                         Master.eLang.GetString(9999999, "Title"),
                                         "movies / episodes / tv shows"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle1 = New ListViewItem(New String() {
                                         "$TITLE%1$",
                                         Master.eLang.GetString(9999999, "List Title"),
                                         "movies / tv shows"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle2 = New ListViewItem(New String() {
                                         "$TITLE%2$",
                                         Master.eLang.GetString(9999999, "Sort Title"),
                                         "movies / tv shows"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle3 = New ListViewItem(New String() {
                                         "$TITLE%3$",
                                         Master.eLang.GetString(9999999, "Original Title"),
                                         "movies / tv shows"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle4 = New ListViewItem(New String() {
                                         "$TITLE%4$",
                                         Master.eLang.GetString(9999999, "Original Title if different from Title"),
                                         "movies / tv shows"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle5 = New ListViewItem(New String() {
                                         "$TITLE%5$",
                                         Master.eLang.GetString(9999999, "Collection Title"),
                                         "movies"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle6 = New ListViewItem(New String() {
                                         "$TITLE%6$",
                                         Master.eLang.GetString(9999999, "Collection List Title"),
                                         "movies"
                                         }) With {.Group = lvgTitles}
        Dim lviTitle7 = New ListViewItem(New String() {
                                         "$TITLE%7$",
                                         Master.eLang.GetString(9999999, "TV Show Title"),
                                         "episodes"
                                         }) With {.Group = lvgTitles}

        'Dates
        Dim lviAired0 = New ListViewItem(New String() {
                                         "$AIRED$",
                                         Master.eLang.GetString(9999999, "Aired"),
                                         "episodes"
                                         }) With {.Group = lvgDates}
        Dim lviYear0 = New ListViewItem(New String() {
                                         "$YEAR$",
                                         Master.eLang.GetString(9999999, "Year"),
                                         "movies"
                                         }) With {.Group = lvgDates}

        'Information
        Dim lviCountry0 = New ListViewItem(New String() {
                                         "$COUNTRY%<sep>;<limit>$",
                                         Master.eLang.GetString(9999999, "Country"),
                                         "movies"
                                         }) With {.Group = lvgInformation}
        Dim lviDirector0 = New ListViewItem(New String() {
                                         "$DIRECTOR%<sep>;<limit>$",
                                         Master.eLang.GetString(9999999, "Director"),
                                         "movies"
                                         }) With {.Group = lvgInformation}
        Dim lviGenre0 = New ListViewItem(New String() {
                                         "$GENRE%<sep>;<limit>$",
                                         Master.eLang.GetString(9999999, "Genre"),
                                         "movies / tv shows"
                                         }) With {.Group = lvgInformation}


        lvFlags.Items.AddRange(New ListViewItem() {
                               lviTitle0,
                               lviTitle1,
                               lviTitle2,
                               lviTitle3,
                               lviTitle4,
                               lviTitle5,
                               lviTitle6,
                               lviTitle7,
                               lviAired0,
                               lviYear0,
                               lviCountry0,
                               lviDirector0
                               })

        For Each flag In lvFlags.Items

        Next
    End Sub

End Class