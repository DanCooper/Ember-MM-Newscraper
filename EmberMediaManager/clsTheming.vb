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
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("theme")>
Public Class XMLTheme

#Region "Properties"

    <XmlElement("imagepanel")>
    Public Property ImagePanel As New ImagePanelSettings
    <XmlElement("infopanel")>
    Public Property InfoPanel As New InfoPanelSettings
    <XmlElement("medialist")>
    Public Property MediaList As New MediaListSettings
    <XmlElement("toppanel")>
    Public Property TopPanel As New TopPanelSettings

#End Region 'Properties 

#Region "Nested Types"

    Public Class ControlSettings

#Region "Properties"

        <XmlAttribute("name")>
        Public Property Name As String

        <XmlIgnore()>
        Public ReadOnly Property Anchor As AnchorStyles
            Get
                Return DirectCast(AnchorFromXML, AnchorStyles)
            End Get
        End Property

        <XmlElement("anchor")>
        Public Property AnchorFromXML As Integer

        <XmlIgnore()>
        Public Property BackColor As Color

        <XmlElement("backcolor")>
        Public Property BackColorFromXML As String
            Get
                Return BackColor.ToString
            End Get
            Set(value As String)
                BackColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlIgnore()>
        Public ReadOnly Property Font As Font
            Get
                If String.IsNullOrEmpty(FontName) OrElse
                   FontSize = -1 OrElse
                   FontStyleFromXML = -1 Then
                    Return New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                Else
                    Return New Font(FontName, FontSize, DirectCast(FontStyleFromXML, FontStyle))
                End If
            End Get
        End Property

        <XmlElement("font")>
        Public Property FontName As String = String.Empty

        <XmlElement("fontsize")>
        Public Property FontSize As Integer = -1

        <XmlElement("fontstyle")>
        Public Property FontStyleFromXML As Integer = -1

        <XmlIgnore()>
        Public Property ForeColor As Color

        <XmlElement("forecolor")>
        Public Property ForeColorFromXML As String
            Get
                Return ForeColor.ToString
            End Get
            Set(value As String)
                ForeColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlElement("height")>
        Public Property Height As String

        <XmlElement("left")>
        Public Property Left As String

        <XmlElement("top")>
        Public Property Top As String

        <XmlElement("width")>
        Public Property Width As String

#End Region 'Properties

    End Class

    Public Class ImagePanelSettings

#Region "Properties"

        <XmlIgnore()>
        Public Property BackColor As Color

        <XmlElement("backcolor")>
        Public Property BackColorFromXML As String
            Get
                Return BackColor.ToString
            End Get
            Set(value As String)
                BackColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlElement("genre")>
        Public Property Genre As New ControlSettings

        <XmlElement("imagename")>
        Public Property ImageName As New ControlSettings
        <XmlElement("imagesize")>
        Public Property ImageSize As New ControlSettings

        <XmlElement("mpaa")>
        Public Property MPAA As New ControlSettings

        <XmlElement("movie")>
        Public Property Movie As New ImagePanelContentSettings

        <XmlElement("movieset")>
        Public Property Movieset As New ImagePanelContentSettings

        <XmlElement("tvepisode")>
        Public Property TVEpisode As New ImagePanelContentSettings

        <XmlElement("tvseason")>
        Public Property TVSeason As New ImagePanelContentSettings

        <XmlElement("tvshow")>
        Public Property TVShow As New ImagePanelContentSettings


#End Region 'Properties

    End Class

    Public Class ImagePanelContentSettings

#Region "Properties"

        <XmlElement("banner")>
        Public Property Banner As New ImageSettings
        <XmlElement("characterart")>
        Public Property CharacterArt As New ImageSettings
        <XmlElement("clearart")>
        Public Property ClearArt As New ImageSettings
        <XmlElement("clearlogo")>
        Public Property ClearLogo As New ImageSettings
        <XmlElement("discart")>
        Public Property DiscArt As New ImageSettings
        <XmlElement("fanartsmall")>
        Public Property FanartSmall As New ImageSettings
        <XmlElement("keyart")>
        Public Property Keyart As New ImageSettings
        <XmlElement("landscape")>
        Public Property Landscape As New ImageSettings
        <XmlElement("poster")>
        Public Property Poster As New ImageSettings

#End Region 'Properties

    End Class

    Public Class InfoPanelSettings

#Region "Properties"

        <XmlElement("movie")>
        Public Property Movie As New InfoPanelContentSettings

        <XmlElement("movieset")>
        Public Property Movieset As New InfoPanelContentSettings

        <XmlElement("tvepisode")>
        Public Property TVEpisode As New InfoPanelContentSettings

        <XmlElement("tvseason")>
        Public Property TVSeason As New InfoPanelContentSettings

        <XmlElement("tvshow")>
        Public Property TVShow As New InfoPanelContentSettings

#End Region 'Properties

    End Class

    Public Class InfoPanelContentSettings

#Region "Properties"

        <XmlIgnore()>
        Public Property BackColor As Color

        <XmlElement("backcolor")>
        Public Property BackColorFromXML As String
            Get
                Return BackColor.ToString
            End Get
            Set(value As String)
                BackColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlElement("control")>
        Public Property Controls As New List(Of ControlSettings)

        <XmlElement("globalheadersettings")>
        Public Property GlobalHeaderSettings As ControlSettings

        <XmlElement("globalvaluesettings")>
        Public Property GlobalValueSettings As ControlSettings

        <XmlElement("posmid")>
        Public Property PosMid As Integer

        <XmlElement("posup")>
        Public Property PosUp As Integer

#End Region 'Properties

    End Class

    Public Class ImageSettings

#Region "Properties"

        <XmlElement("left")>
        Public Property Left As Integer

        Public ReadOnly Property Location As Point
            Get
                Return New Point(Left, Top)
            End Get
        End Property

        <XmlElement("maxheight")>
        Public Property MaxHeight As Integer

        <XmlElement("maxwidth")>
        Public Property MaxWidth As Integer

        <XmlElement("top")>
        Public Property Top As Integer

#End Region 'Properties

    End Class

    Public Class MediaListSettings

#Region "Properties"

        <XmlIgnore()>
        Public Property BackColor As Color

        <XmlElement("backcolor")>
        Public Property BackColorFromXml As String
            Get
                Return BackColor.ToString
            End Get
            Set(value As String)
                BackColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlIgnore()>
        Public Property GridColor As Color

        <XmlElement("gridcolor")>
        Public Property GridColorFromXml As String
            Get
                Return GridColor.ToString
            End Get
            Set(value As String)
                GridColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlElement("default")>
        Public Property [Default] As New MediaListCellSettings With {
            .BackColor = Color.White,
            .ForeColor = Color.Black,
            .SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight),
            .SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        }
        <XmlElement("locked")>
        Public Property Locked As New MediaListCellSettings With {
            .BackColor = Color.LightSteelBlue,
            .ForeColor = Color.Black,
            .SelectionBackColor = Color.DarkTurquoise,
            .SelectionForeColor = Color.Black
        }
        <XmlElement("new")>
        Public Property [New] As New MediaListCellSettings With {
            .BackColor = Color.White,
            .ForeColor = Color.Green,
            .SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight),
            .SelectionForeColor = Color.Green
        }
        <XmlElement("marked")>
        Public Property Marked As New MediaListCellSettings With {
            .BackColor = Color.White,
            .ForeColor = Color.Crimson,
            .SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight),
            .SelectionForeColor = Color.Crimson
        }
        <XmlElement("missing")>
        Public Property Missing As New MediaListCellSettings With {
            .BackColor = Color.White,
            .ForeColor = Color.Gray,
            .SelectionBackColor = Color.DarkGray,
            .SelectionForeColor = Color.LightGray
        }
        <XmlElement("outoftolerance")>
        Public Property OutOfTolerance As New MediaListCellSettings With {
            .BackColor = Color.MistyRose,
            .ForeColor = Color.Black,
            .SelectionBackColor = Color.DarkMagenta,
            .SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        }

#End Region 'Properties

    End Class

    Public Class MediaListCellSettings

#Region "Properties"

        <XmlIgnore()>
        Public Property BackColor As Color

        <XmlElement("backcolor")>
        Public Property BackColorFromXML As String
            Get
                Return BackColor.ToString
            End Get
            Set(value As String)
                BackColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlIgnore()>
        Public Property ForeColor As Color

        <XmlElement("forecolor")>
        Public Property ForeColorFromXML As String
            Get
                Return ForeColor.ToString
            End Get
            Set(value As String)
                ForeColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlIgnore()>
        Public Property SelectionBackColor As Color

        <XmlElement("selectionbackcolor")>
        Public Property SelectionBackColorFromXML As String
            Get
                Return SelectionBackColor.ToString
            End Get
            Set(value As String)
                SelectionBackColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

        <XmlIgnore()>
        Public Property SelectionForeColor As Color

        <XmlElement("selectionforecolor")>
        Public Property SelectionForeColorFromXML As String
            Get
                Return SelectionForeColor.ToString
            End Get
            Set(value As String)
                SelectionForeColor = Functions.ConvertStringToColor(value)
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class TopPanelSettings

#Region "Properties"

        <XmlElement("globalsettings")>
        Public Property GlobalSettings As ControlSettings

        <XmlElement("originaltitle")>
        Public Property OriginalTitle As New ControlSettings

        <XmlElement("rating")>
        Public Property Rating As New ControlSettings

        <XmlElement("runtime")>
        Public Property Runtime As New ControlSettings

        <XmlElement("studio")>
        Public Property Studio As New ControlSettings

        <XmlElement("title")>
        Public Property Title As New ControlSettings

        <XmlElement("tagline")>
        Public Property Tagline As New ControlSettings

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class

Public Class Theming

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _availablecontrols As New List(Of XMLTheme.ControlSettings)
    Private _rProcs(3) As Regex
    Private _theme As New XMLTheme

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Const AvailCalcs As String = "*/+-"

        For i As Integer = 0 To 3
            _rProcs(i) = New Regex("(\d+(?:[.,]\d+)?) *(\#) *([+-]?\d+(?:[.,]\d+)?)".Replace("#", AvailCalcs.Substring(i, 1)))
        Next

        BuildControlList()
        ParseTheme()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub ApplyTheme(ByVal contentType As Enums.ContentType)
        'ImagePanel settings
        'Global settings
        With _theme.ImagePanel
            'Background
            frmMain.pbBackground.BackColor = .BackColor
            frmMain.scMain.Panel2.BackColor = .BackColor
            'Banner
            frmMain.pbBanner.BackColor = .BackColor
            frmMain.pnlBanner.BackColor = .BackColor
            frmMain.pnlBannerBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlBannerMain.BackColor = .BackColor
            frmMain.pnlBannerTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblBannerTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblBannerSize, .ImageSize)
            'CharacterArt
            frmMain.pbCharacterArt.BackColor = .BackColor
            frmMain.pnlCharacterArt.BackColor = .BackColor
            frmMain.pnlCharacterArtBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlCharacterArtMain.BackColor = .BackColor
            frmMain.pnlCharacterArtTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblCharacterArtTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblCharacterArtSize, .ImageSize)
            'ClearArt
            frmMain.pbClearArt.BackColor = .BackColor
            frmMain.pnlClearArt.BackColor = .BackColor
            frmMain.pnlClearArtBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlClearArtMain.BackColor = .BackColor
            frmMain.pnlClearArtTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblClearArtTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblClearArtSize, .ImageSize)
            'ClearLogo
            frmMain.pbClearLogo.BackColor = .BackColor
            frmMain.pnlClearLogo.BackColor = .BackColor
            frmMain.pnlClearLogoBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlClearLogoMain.BackColor = .BackColor
            frmMain.pnlClearLogoTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblClearLogoTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblClearLogoSize, .ImageSize)
            'Genre
            frmMain.GenrePanelColor = .Genre.BackColor
            'DiscArt
            frmMain.pbDiscArt.BackColor = .BackColor
            frmMain.pnlDiscArt.BackColor = .BackColor
            frmMain.pnlDiscArtBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlDiscArtMain.BackColor = .BackColor
            frmMain.pnlDiscArtTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblDiscArtTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblDiscArtSize, .ImageSize)
            'FanartSmall
            frmMain.pbFanartSmall.BackColor = .BackColor
            frmMain.pnlFanartSmall.BackColor = .BackColor
            frmMain.pnlFanartSmallBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlFanartSmallMain.BackColor = .BackColor
            frmMain.pnlFanartSmallTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblFanartSmallTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblFanartSmallSize, .ImageSize)
            'Keyart
            frmMain.pbKeyart.BackColor = .BackColor
            frmMain.pnlKeyart.BackColor = .BackColor
            frmMain.pnlKeyartBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlKeyartMain.BackColor = .BackColor
            frmMain.pnlKeyartTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblKeyartTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblKeyartSize, .ImageSize)
            'Landscape
            frmMain.pbLandscape.BackColor = .BackColor
            frmMain.pnlLandscape.BackColor = .BackColor
            frmMain.pnlLandscapeBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlLandscapeMain.BackColor = .BackColor
            frmMain.pnlLandscapeTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblLandscapeTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblLandscapeSize, .ImageSize)
            'MPAA
            frmMain.pnlMPAA.BackColor = .MPAA.BackColor
            frmMain.pbMPAA.BackColor = .MPAA.BackColor
            'Poster
            frmMain.pbPoster.BackColor = .BackColor
            frmMain.pnlPoster.BackColor = .BackColor
            frmMain.pnlPosterBottom.BackColor = .ImageSize.BackColor
            frmMain.pnlPosterMain.BackColor = .BackColor
            frmMain.pnlPosterTop.BackColor = .ImageName.BackColor
            SetControlSettings_ImagePanel(frmMain.lblPosterTitle, .ImageName)
            SetControlSettings_ImagePanel(frmMain.lblPosterSize, .ImageSize)
            'ContentType specific settings
            SetImagePanelSettings(contentType)
        End With

        'MediaList settings
        If Not _theme.MediaList.BackColor.IsEmpty Then
            frmMain.dgvMovies.BackgroundColor = _theme.MediaList.BackColor
            frmMain.dgvMovieSets.BackgroundColor = _theme.MediaList.BackColor
            frmMain.dgvTVEpisodes.BackgroundColor = _theme.MediaList.BackColor
            frmMain.dgvTVSeasons.BackgroundColor = _theme.MediaList.BackColor
            frmMain.dgvTVShows.BackgroundColor = _theme.MediaList.BackColor
        End If
        If Not _theme.MediaList.GridColor.IsEmpty Then
            frmMain.dgvMovies.GridColor = _theme.MediaList.GridColor
            frmMain.dgvMovieSets.GridColor = _theme.MediaList.GridColor
            frmMain.dgvTVEpisodes.GridColor = _theme.MediaList.GridColor
            frmMain.dgvTVSeasons.GridColor = _theme.MediaList.GridColor
        End If
        frmMain.MediaListColors = _theme.MediaList

        'Top Panel
        SetTopPanelSettings()
        SetControlSettings_TopPanel(frmMain.lblOriginalTitle, _theme.TopPanel.OriginalTitle)
        'SetControlSettings_TopPanel(frmMain.lblRuntime, _theme.TopPanel.Runtime)
        SetControlSettings_TopPanel(frmMain.lblStudio, _theme.TopPanel.Studio)
        SetControlSettings_TopPanel(frmMain.lblTagline, _theme.TopPanel.Tagline)
        SetControlSettings_TopPanel(frmMain.lblTitle, _theme.TopPanel.Title)
        frmMain.pbAudioChannels.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pbAudioCodec.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pbStudio.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pbVideoResolution.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pbVideoSource.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pnlInfoIcons.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pnlRating.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.pnlTop.BackColor = _theme.TopPanel.GlobalSettings.BackColor
        frmMain.TopPanelColors = _theme.TopPanel

        'InfoPanel
        SetInfoPanelSettings(contentType)
    End Sub

    Public Sub BuildControlList()
        _availablecontrols.Clear()
        Dim PossibleControls As String() = New String() {
            "btnDown",
            "btnFilePlay",
            "btnMetaDataRefresh",
            "btnMid",
            "btnTrailerPlay",
            "btnUp",
            "lblActorsHeader",
            "lblCertifications",
            "lblCertificationsHeader",
            "lblCollections",
            "lblCollectionsHeader",
            "lblCountries",
            "lblCountriesHeader",
            "lblCredits",
            "lblCreditsHeader",
            "lblDirectors",
            "lblDirectorsHeader",
            "lblGuestStarsHeader",
            "lblFilePathHeader",
            "lblIMDBHeader",
            "lblInfoPanelHeader",
            "lblMetaDataHeader",
            "lblMoviesInSetHeader",
            "lblOutlineHeader",
            "lblPlotHeader",
            "lblPremiered",
            "lblPremieredHeader",
            "lblStatus",
            "lblStatusHeader",
            "lblTags",
            "lblTagsHeader",
            "lblTMDBHeader",
            "lblTrailerPathHeader",
            "lblTVDBHeader",
            "lstActors",
            "lstGuestStars",
            "lvMoviesInSet",
            "pbActors",
            "pbActorsLoad",
            "pbGuestStars",
            "pbGuestStarsLoad",
            "pnlActors",
            "pnlGuestStars",
            "pnlInfoPanel",
            "pnlMoviesInSet",
            "txtFilePath",
            "txtIMDBID",
            "txtMetaData",
            "txtOutline",
            "txtPlot",
            "txtTMDBID",
            "txtTVDBID",
            "txtTrailerPath"}
        For Each sCon As String In PossibleControls
            _availablecontrols.Add(New XMLTheme.ControlSettings With {.Name = sCon})
        Next
    End Sub

    Private Function EvaluateFormula(ByVal sFormula As String) As Integer
        Dim mReg As Match
        Dim rResult As Double = 0.0
        Dim dFirst As Double = 0.0
        Dim dSecond As Double = 0.0

        If Integer.TryParse(sFormula, 0) Then Return Convert.ToInt32(sFormula)

        ReplaceControlVars(sFormula)

        Try
            'check for invalid characters
            If Regex.IsMatch(sFormula, "^[().,1234567890 ^*/+-]+$") Then

                'check for equal number of ()
                If sFormula.Replace("(", String.Empty).Length = sFormula.Replace(")", String.Empty).Length Then

                    'step 2: all code between brackets
                    For Each mReg In Regex.Matches(sFormula, "\((.+)\)")
                        rResult = EvaluateFormula(mReg.Groups(1).ToString())
                        sFormula = sFormula.Replace(mReg.ToString(), rResult.ToString("0.00"))
                    Next

                    'step 2 operators
                    For Each rMatch As Regex In _rProcs
                        Do
                            mReg = rMatch.Match(sFormula)
                            If Not mReg.Success Then Exit Do
                            dFirst = Double.Parse(mReg.Groups(1).ToString())
                            dSecond = Double.Parse(mReg.Groups(3).ToString())

                            Select Case mReg.Groups(2).ToString.Trim
                                Case "*"
                                    rResult = dFirst * dSecond
                                Case "/"
                                    rResult = dFirst / dSecond
                                Case "+"
                                    rResult = dFirst + dSecond
                                Case "-"
                                    rResult = dFirst - dSecond
                            End Select
                            If mReg.ToString().Length = sFormula.Length Then Return Convert.ToInt32(rResult)
                            sFormula = sFormula.Replace(mReg.ToString(), rResult.ToString("0.00"))
                        Loop
                    Next
                End If
            End If

            Dim iResult As Integer = 0
            If Not Integer.TryParse(sFormula, iResult) OrElse iResult = 0 Then
                Dim tasd = sFormula  'Todo: ????
            End If
            Return iResult
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & sFormula)
        End Try

        Return 0
    End Function

    Private Function GetControl(name As String) As Control
        If Not String.IsNullOrEmpty(name) Then
            Select Case name
                Case "pnlInfoPanel"
                    Return frmMain.pnlInfoPanel
                Case "lblActorsHeader", "lstActors", "pbActors", "pbActorsLoad"
                    Return frmMain.pnlActors.Controls(name)
                Case "lblGuestStarsHeader", "lstGuestStars", "pbGuestStars", "pbGuestStarsLoad"
                    Return frmMain.pnlGuestStars.Controls(name)
                Case "lblMoviesInSetHeader", "lvMoviesInSet"
                    Return frmMain.pnlMoviesInSet.Controls(name)
                Case Else
                    Return frmMain.pnlInfoPanel.Controls(name)
            End Select
        End If
        Return Nothing
    End Function

    Private Function Load(ByVal path As String) As XMLTheme
        If File.Exists(path) Then
            Dim xmlSer As New XmlSerializer(GetType(XMLTheme))
            Using xmlSR As StreamReader = New StreamReader(path)
                Return DirectCast(xmlSer.Deserialize(xmlSR), XMLTheme)
            End Using
        End If
        Return Nothing
    End Function

    Public Sub ParseTheme()
        Dim lstFiles As New List(Of FileInfo)
        Dim diDefaults As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Themes"))
        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Themes"))
        If diDefaults.Exists Then lstFiles.AddRange(diDefaults.GetFiles("*.xml"))
        If diCustom.Exists Then lstFiles.AddRange(diCustom.GetFiles("*.xml"))
        Dim fiTheme = lstFiles.FirstOrDefault(Function(f) f.Name = String.Format("{0}.xml", Master.eSettings.GeneralTheme))
        If fiTheme IsNot Nothing AndAlso fiTheme.Exists Then
            _theme = Load(fiTheme.FullName)
        Else
            _theme = Load(String.Concat(Functions.AppPath, "Themes", Path.DirectorySeparatorChar, "FullHD-Default.xml"))
            Master.eSettings.GeneralTheme = "FullHD-Default"
        End If
    End Sub

    Private Sub ReplaceControlVars(ByRef sFormula As String)
        Dim cName As String

        Try
            For Each xCon As Match In Regex.Matches(sFormula, "(?<control>[a-z]+)\.(?<value>[a-z]+)", RegexOptions.IgnoreCase)
                cName = xCon.Groups("control").Value
                Dim aCon = From bCon As XMLTheme.ControlSettings In _availablecontrols Where bCon.Name.ToLower = cName.ToLower

                If aCon.Count > 0 Then
                    Dim xControl = GetControl(aCon(0).Name)
                    If xControl IsNot Nothing Then
                        Select Case xCon.Groups("value").Value.ToLower
                            Case "width"
                                sFormula = sFormula.Replace(xCon.ToString, xControl.Width.ToString)
                            Case "height"
                                sFormula = sFormula.Replace(xCon.ToString, xControl.Height.ToString)
                            Case "top"
                                sFormula = sFormula.Replace(xCon.ToString, xControl.Top.ToString)
                            Case "left"
                                sFormula = sFormula.Replace(xCon.ToString, xControl.Left.ToString)
                        End Select
                    Else
                        _Logger.Error(String.Concat("Unknown control name in Theme: ", aCon(0).Name))
                    End If
                End If
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub SetControlSettings_ImagePanel(ByVal control As Object, ByVal settings As XMLTheme.ControlSettings)
        If TypeOf control Is Label Then
            Dim nControl = DirectCast(control, Label)
            nControl.BackColor = settings.BackColor
            nControl.ForeColor = settings.ForeColor
            nControl.Font = settings.Font
        End If
    End Sub

    Private Sub SetControlSettings_TopPanel(ByVal control As Object, ByVal controlsettings As XMLTheme.ControlSettings)
        If TypeOf control Is Label Then
            Dim nControl = DirectCast(control, Label)
            nControl.BackColor = controlsettings.BackColor
            nControl.ForeColor = controlsettings.ForeColor
            nControl.Font = controlsettings.Font
        End If
    End Sub

    Private Sub SetImagePanelSettings(ByVal contentType As Enums.ContentType)
        Dim settings As New XMLTheme.ImagePanelContentSettings
        Select Case contentType
            Case Enums.ContentType.Movie
                settings = _theme.ImagePanel.Movie
            Case Enums.ContentType.MovieSet
                settings = _theme.ImagePanel.Movieset
            Case Enums.ContentType.TVEpisode
                settings = _theme.ImagePanel.TVEpisode
            Case Enums.ContentType.TVSeason
                settings = _theme.ImagePanel.TVSeason
            Case Enums.ContentType.TVShow
                settings = _theme.ImagePanel.TVShow
            Case Else
                Return
        End Select
        With settings
            'Banner
            frmMain.BannerMaxHeight = .Banner.MaxHeight
            frmMain.BannerMaxWidth = .Banner.MaxWidth
            frmMain.pnlBanner.Location = .Banner.Location
            'CharacterArt
            frmMain.CharacterArtMaxHeight = .CharacterArt.MaxHeight
            frmMain.CharacterArtMaxWidth = .CharacterArt.MaxWidth
            frmMain.pnlCharacterArt.Location = .CharacterArt.Location
            'ClearArt
            frmMain.ClearArtMaxHeight = .ClearArt.MaxHeight
            frmMain.ClearArtMaxWidth = .ClearArt.MaxWidth
            frmMain.pnlClearArt.Location = .ClearArt.Location
            'CleaLogo
            frmMain.ClearLogoMaxHeight = .ClearLogo.MaxHeight
            frmMain.ClearLogoMaxWidth = .ClearLogo.MaxWidth
            frmMain.pnlClearLogo.Location = .ClearLogo.Location
            'DiscArt
            frmMain.DiscArtMaxHeight = .DiscArt.MaxHeight
            frmMain.DiscArtMaxWidth = .DiscArt.MaxWidth
            frmMain.pnlDiscArt.Location = .DiscArt.Location
            'FanartSmall
            frmMain.FanartSmallMaxHeight = .FanartSmall.MaxHeight
            frmMain.FanartSmallMaxWidth = .FanartSmall.MaxWidth
            frmMain.pnlFanartSmall.Location = .FanartSmall.Location
            'Keyart
            frmMain.KeyartMaxHeight = .Keyart.MaxHeight
            frmMain.KeyartMaxWidth = .Keyart.MaxWidth
            frmMain.pnlKeyart.Location = .Keyart.Location
            'Landscape
            frmMain.LandscapeMaxHeight = .Landscape.MaxHeight
            frmMain.LandscapeMaxWidth = .Landscape.MaxWidth
            frmMain.pnlLandscape.Location = .Landscape.Location
            'Poster
            frmMain.PosterMaxHeight = .Poster.MaxHeight
            frmMain.PosterMaxWidth = .Poster.MaxWidth
            frmMain.pnlPoster.Location = .Poster.Location
        End With
    End Sub

    Private Sub SetInfoPanelSettings(ByVal contentType As Enums.ContentType)
        Dim settings As New XMLTheme.InfoPanelContentSettings
        Select Case contentType
            Case Enums.ContentType.Movie
                settings = _theme.InfoPanel.Movie
            Case Enums.ContentType.MovieSet
                settings = _theme.InfoPanel.Movieset
            Case Enums.ContentType.TVEpisode
                settings = _theme.InfoPanel.TVEpisode
            Case Enums.ContentType.TVSeason
                settings = _theme.InfoPanel.TVSeason
            Case Enums.ContentType.TVShow, Enums.ContentType.TV
                settings = _theme.InfoPanel.TVShow
            Case Else
                Return
        End Select

        frmMain.pnlInfoPanel.BackColor = settings.BackColor
        frmMain.InfoPanelMidHeight = settings.PosMid
        frmMain.InfoPanelUpHeight = settings.PosUp

        For Each xCon In _availablecontrols
            Dim nControl = frmMain.pnlInfoPanel.Controls(xCon.Name)
            If nControl IsNot Nothing Then
                nControl.Visible = False
            End If
        Next

        For Each xCon In settings.Controls
            Dim xControl = GetControl(xCon.Name)
            If xControl IsNot Nothing Then
                SetInfoPanelGlobalSettings(xControl, xCon, settings)
            Else
                _Logger.Error(String.Concat("Unknown control name in Theme: ", xCon.Name))
            End If

        Next
    End Sub

    Private Sub SetInfoPanelGlobalSettings(ByRef control As Control, ByVal controlsettings As XMLTheme.ControlSettings, ByVal settings As XMLTheme.InfoPanelContentSettings)
        Select Case True
            Case control.Name = "pnlInfoPanel"
                Dim globalSettings = settings.GlobalHeaderSettings
                If globalSettings IsNot Nothing Then
                    If controlsettings.BackColor.IsEmpty Then controlsettings.BackColor = globalSettings.BackColor
                    If controlsettings.ForeColor.IsEmpty Then controlsettings.ForeColor = globalSettings.ForeColor
                    If String.IsNullOrEmpty(controlsettings.FontName) Then controlsettings.FontName = globalSettings.FontName
                    If controlsettings.FontSize = 0 Then controlsettings.FontSize = globalSettings.FontSize
                    If controlsettings.FontStyleFromXML = -1 Then controlsettings.FontStyleFromXML = globalSettings.FontStyleFromXML
                End If
                control.BackColor = controlsettings.BackColor
                control.ForeColor = controlsettings.ForeColor
                control.Font = controlsettings.Font
            Case control.Name.EndsWith("Header")
                Dim globalSettings = settings.GlobalHeaderSettings
                If globalSettings IsNot Nothing Then
                    If controlsettings.BackColor.IsEmpty Then controlsettings.BackColor = globalSettings.BackColor
                    If controlsettings.ForeColor.IsEmpty Then controlsettings.ForeColor = globalSettings.ForeColor
                    If String.IsNullOrEmpty(controlsettings.FontName) Then controlsettings.FontName = globalSettings.FontName
                    If controlsettings.FontSize = 0 Then controlsettings.FontSize = globalSettings.FontSize
                    If controlsettings.FontStyleFromXML = -1 Then controlsettings.FontStyleFromXML = globalSettings.FontStyleFromXML
                    If String.IsNullOrEmpty(controlsettings.Height) Then controlsettings.Height = globalSettings.Height
                End If
                control.Anchor = controlsettings.Anchor
                control.BackColor = controlsettings.BackColor
                control.ForeColor = controlsettings.ForeColor
                control.Font = controlsettings.Font
                control.Height = EvaluateFormula(controlsettings.Height)
                control.Left = EvaluateFormula(controlsettings.Left)
                control.Top = EvaluateFormula(controlsettings.Top)
                control.Width = EvaluateFormula(controlsettings.Width)
            Case control.Name.StartsWith("lbl"), control.Name.StartsWith("lv"),
                 control.Name.StartsWith("lst"), control.Name.StartsWith("pb"),
                 control.Name.StartsWith("pnl"), control.Name.StartsWith("txt")
                Dim globalSettings = settings.GlobalValueSettings
                If globalSettings IsNot Nothing Then
                    If controlsettings.BackColor.IsEmpty Then controlsettings.BackColor = globalSettings.BackColor
                    If controlsettings.ForeColor.IsEmpty Then controlsettings.ForeColor = globalSettings.ForeColor
                    If String.IsNullOrEmpty(controlsettings.FontName) Then controlsettings.FontName = globalSettings.FontName
                    If controlsettings.FontSize = 0 Then controlsettings.FontSize = globalSettings.FontSize
                    If controlsettings.FontStyleFromXML = -1 Then controlsettings.FontStyleFromXML = globalSettings.FontStyleFromXML
                    If String.IsNullOrEmpty(controlsettings.Height) Then controlsettings.Height = globalSettings.Height
                End If
                control.Anchor = controlsettings.Anchor
                control.BackColor = controlsettings.BackColor
                control.ForeColor = controlsettings.ForeColor
                control.Font = controlsettings.Font
                control.Height = EvaluateFormula(controlsettings.Height)
                control.Left = EvaluateFormula(controlsettings.Left)
                control.Top = EvaluateFormula(controlsettings.Top)
                control.Width = EvaluateFormula(controlsettings.Width)
            Case control.Name.StartsWith("btn")
                control.Anchor = controlsettings.Anchor
                control.ForeColor = controlsettings.ForeColor
                control.Font = controlsettings.Font
                control.Height = EvaluateFormula(controlsettings.Height)
                control.Left = EvaluateFormula(controlsettings.Left)
                control.Top = EvaluateFormula(controlsettings.Top)
                control.Width = EvaluateFormula(controlsettings.Width)
            Case Else
                control.Anchor = controlsettings.Anchor
                control.BackColor = controlsettings.BackColor
                control.ForeColor = controlsettings.ForeColor
                control.Font = controlsettings.Font
                control.Height = EvaluateFormula(controlsettings.Height)
                control.Left = EvaluateFormula(controlsettings.Left)
                control.Top = EvaluateFormula(controlsettings.Top)
                control.Width = EvaluateFormula(controlsettings.Width)
        End Select
        control.Visible = True
    End Sub

    Private Sub SetTopPanelSettings()
        Dim globalSettings = _theme.TopPanel.GlobalSettings
        If globalSettings IsNot Nothing Then
            With _theme.TopPanel.OriginalTitle
                If .BackColor.IsEmpty Then .BackColor = globalSettings.BackColor
                If .ForeColor.IsEmpty Then .ForeColor = globalSettings.ForeColor
                If String.IsNullOrEmpty(.FontName) Then .FontName = globalSettings.FontName
                If .FontSize = 0 Then .FontSize = globalSettings.FontSize
                If .FontStyleFromXML = -1 Then .FontStyleFromXML = globalSettings.FontStyleFromXML
            End With
            With _theme.TopPanel.Rating
                If .BackColor.IsEmpty Then .BackColor = globalSettings.BackColor
                If .ForeColor.IsEmpty Then .ForeColor = globalSettings.ForeColor
                If String.IsNullOrEmpty(.FontName) Then .FontName = globalSettings.FontName
                If .FontSize = 0 Then .FontSize = globalSettings.FontSize
                If .FontStyleFromXML = -1 Then .FontStyleFromXML = globalSettings.FontStyleFromXML
            End With
            With _theme.TopPanel.Runtime
                If .BackColor.IsEmpty Then .BackColor = globalSettings.BackColor
                If .ForeColor.IsEmpty Then .ForeColor = globalSettings.ForeColor
                If String.IsNullOrEmpty(.FontName) Then .FontName = globalSettings.FontName
                If .FontSize = 0 Then .FontSize = globalSettings.FontSize
                If .FontStyleFromXML = -1 Then .FontStyleFromXML = globalSettings.FontStyleFromXML
            End With
            With _theme.TopPanel.Studio
                If .BackColor.IsEmpty Then .BackColor = globalSettings.BackColor
                If .ForeColor.IsEmpty Then .ForeColor = globalSettings.ForeColor
                If String.IsNullOrEmpty(.FontName) Then .FontName = globalSettings.FontName
                If .FontSize = 0 Then .FontSize = globalSettings.FontSize
                If .FontStyleFromXML = -1 Then .FontStyleFromXML = globalSettings.FontStyleFromXML
            End With
            With _theme.TopPanel.Tagline
                If .BackColor.IsEmpty Then .BackColor = globalSettings.BackColor
                If .ForeColor.IsEmpty Then .ForeColor = globalSettings.ForeColor
                If String.IsNullOrEmpty(.FontName) Then .FontName = globalSettings.FontName
                If .FontSize = 0 Then .FontSize = globalSettings.FontSize
                If .FontStyleFromXML = -1 Then .FontStyleFromXML = globalSettings.FontStyleFromXML
            End With
            With _theme.TopPanel.Title
                If .BackColor.IsEmpty Then .BackColor = globalSettings.BackColor
                If .ForeColor.IsEmpty Then .ForeColor = globalSettings.ForeColor
                If String.IsNullOrEmpty(.FontName) Then .FontName = globalSettings.FontName
                If .FontSize = 0 Then .FontSize = globalSettings.FontSize
                If .FontStyleFromXML = -1 Then .FontStyleFromXML = globalSettings.FontStyleFromXML
            End With
        End If
    End Sub

#End Region 'Methods

End Class