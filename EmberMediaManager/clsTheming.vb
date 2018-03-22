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

Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class Theming

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private rProcs(3) As Regex
    Private _availablecontrols As New List(Of Controls)
    Private _eptheme As New Theme
    Private _movietheme As New Theme
    Private _moviesettheme As New Theme
    Private _showtheme As New Theme

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Const AvailCalcs As String = "*/+-"

        For i As Integer = 0 To 3
            rProcs(i) = New Regex("(\d+(?:[.,]\d+)?) *(\#) *([+-]?\d+(?:[.,]\d+)?)".Replace("#", AvailCalcs.Substring(i, 1)))
        Next

        BuildControlList()

        ParseThemes(_movietheme, "movie", Master.eSettings.GeneralMovieTheme)
        ParseThemes(_moviesettheme, "movieset", Master.eSettings.GeneralMovieSetTheme)
        ParseThemes(_showtheme, "tvshow", Master.eSettings.GeneralTVShowTheme)
        ParseThemes(_eptheme, "tvep", Master.eSettings.GeneralTVEpisodeTheme)
    End Sub

#End Region 'Constructors

#Region "Enumerations"

    Public Enum ThemeType As Integer
        Movie = 0
        Show = 1
        Episode = 2
        MovieSet = 3
    End Enum

#End Region 'Enumerations

#Region "Methods"

    Public Sub ApplyTheme(ByVal tType As ThemeType)
        Dim xTheme As New Theme
        Dim xControl As New Control
        Select Case tType
            Case ThemeType.Movie
                xTheme = _movietheme
            Case ThemeType.MovieSet
                xTheme = _moviesettheme
            Case ThemeType.Show
                xTheme = _showtheme
            Case ThemeType.Episode
                xTheme = _eptheme
        End Select

        'Top Panel
        frmMain.lblOriginalTitle.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblRating.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblRuntime.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblStudio.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblTagline.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblTitle.ForeColor = xTheme.TopPanelForeColor
        frmMain.pbAudioCodec.BackColor = xTheme.TopPanelBackColor
        frmMain.pbAudioChannels.BackColor = xTheme.TopPanelBackColor
        frmMain.pbVideoResolution.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar1.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar2.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar3.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar4.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar5.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStudio.BackColor = xTheme.TopPanelBackColor
        frmMain.pbVideoSource.BackColor = xTheme.TopPanelBackColor
        frmMain.pnlInfoIcons.BackColor = xTheme.TopPanelBackColor
        frmMain.pnlInfoPanel.BackColor = xTheme.InfoPanelBackColor
        frmMain.pnlRating.BackColor = xTheme.TopPanelBackColor
        frmMain.pnlTop.BackColor = xTheme.TopPanelBackColor

        'MPAA
        frmMain.pnlMPAA.BackColor = xTheme.MPAABackColor
        frmMain.pbMPAA.BackColor = xTheme.MPAABackColor

        'Banner-Style
        frmMain.pbBanner.BackColor = xTheme.BannerBackColor
        frmMain.pnlBanner.BackColor = xTheme.BannerBackColor
        frmMain.pnlBannerMain.BackColor = xTheme.BannerBackColor
        frmMain.pnlBannerBottom.BackColor = xTheme.BannerBottomBackColor
        frmMain.pnlBannerTop.BackColor = xTheme.BannerTopBackColor

        'CharacterArt-Style
        frmMain.pbCharacterArt.BackColor = xTheme.CharacterArtBackColor
        frmMain.pnlCharacterArt.BackColor = xTheme.CharacterArtBackColor
        frmMain.pnlCharacterArtMain.BackColor = xTheme.CharacterArtBackColor
        frmMain.pnlCharacterArtBottom.BackColor = xTheme.CharacterArtBottomBackColor
        frmMain.pnlCharacterArtTop.BackColor = xTheme.CharacterArtTopBackColor

        'ClearArt-Style
        frmMain.pbClearArt.BackColor = xTheme.ClearArtBackColor
        frmMain.pnlClearArt.BackColor = xTheme.ClearArtBackColor
        frmMain.pnlClearArtMain.BackColor = xTheme.ClearArtBackColor
        frmMain.pnlClearArtBottom.BackColor = xTheme.ClearArtBottomBackColor
        frmMain.pnlClearArtTop.BackColor = xTheme.ClearArtTopBackColor

        'ClearLogo-Style
        frmMain.pbClearLogo.BackColor = xTheme.ClearlogoBackColor
        frmMain.pnlClearLogo.BackColor = xTheme.ClearlogoBackColor
        frmMain.pnlClearLogoMain.BackColor = xTheme.ClearlogoBackColor
        frmMain.pnlClearLogoBottom.BackColor = xTheme.ClearLogoBottomBackColor
        frmMain.pnlClearLogoTop.BackColor = xTheme.ClearLogoTopBackColor

        'DiscArt-Style
        frmMain.pbDiscArt.BackColor = xTheme.DiscartBackColor
        frmMain.pnlDiscArt.BackColor = xTheme.DiscartBackColor
        frmMain.pnlDiscArtMain.BackColor = xTheme.DiscartBackColor
        frmMain.pnlDiscArtBottom.BackColor = xTheme.DiscartBottomBackColor
        frmMain.pnlDiscArtTop.BackColor = xTheme.DiscartTopBackColor

        'Fanart-Style
        frmMain.pbFanart.BackColor = xTheme.FanartBackColor
        frmMain.scMain.Panel2.BackColor = xTheme.FanartBackColor

        'FanartSmall-Style
        frmMain.pbFanartSmall.BackColor = xTheme.FanartSmallBackColor
        frmMain.pnlFanartSmall.BackColor = xTheme.FanartSmallBackColor
        frmMain.pnlFanartSmallMain.BackColor = xTheme.FanartSmallBackColor
        frmMain.pnlFanartSmallBottom.BackColor = xTheme.FanartSmallBottomBackColor
        frmMain.pnlFanartSmallTop.BackColor = xTheme.FanartSmallTopBackColor

        'Landscape-Style
        frmMain.pbLandscape.BackColor = xTheme.LandscapeBackColor
        frmMain.pnlLandscape.BackColor = xTheme.LandscapeBackColor
        frmMain.pnlLandscapeMain.BackColor = xTheme.LandscapeBackColor
        frmMain.pnlLandscapeBottom.BackColor = xTheme.LandscapeBottomBackColor
        frmMain.pnlLandscapeTop.BackColor = xTheme.LandscapeTopBackColor

        'Poster-Style
        frmMain.pbPoster.BackColor = xTheme.PosterBackColor
        frmMain.pnlPoster.BackColor = xTheme.PosterBackColor
        frmMain.pnlPosterMain.BackColor = xTheme.PosterBackColor
        frmMain.pnlPosterBottom.BackColor = xTheme.PosterBottomBackColor
        frmMain.pnlPosterTop.BackColor = xTheme.PosterTopBackColor

        'MediaListColors
        frmMain.MediaListColor_Default = xTheme.MediaListColor_Default
        frmMain.MediaListColor_Locked = xTheme.MediaListColor_Locked
        frmMain.MediaListColor_Marked = xTheme.MediaListColor_Marked
        frmMain.MediaListColor_Missing = xTheme.MediaListColor_Missing
        frmMain.MediaListColor_New = xTheme.MediaListColor_New
        frmMain.MediaListColor_OutOfTolerance = xTheme.MediaListColor_OutOfTolerance

        'Image sizes
        frmMain.BannerMaxWidth = xTheme.BannerMaxWidth
        frmMain.BannerMaxHeight = xTheme.BannerMaxHeight
        frmMain.BannerPosLeft = xTheme.BannerPosLeft
        frmMain.BannerPosTop = xTheme.BannerPosTop
        frmMain.CharacterArtMaxWidth = xTheme.CharacterArtMaxWidth
        frmMain.CharacterArtMaxHeight = xTheme.CharacterArtMaxHeight
        frmMain.CharacterArtPosLeft = xTheme.CharacterArtPosLeft
        frmMain.CharacterArtPosTop = xTheme.CharacterArtPosTop
        frmMain.ClearArtMaxWidth = xTheme.ClearArtMaxWidth
        frmMain.ClearArtMaxHeight = xTheme.ClearArtMaxHeight
        frmMain.ClearArtPosLeft = xTheme.ClearArtPosLeft
        frmMain.ClearArtPosTop = xTheme.ClearArtPosTop
        frmMain.ClearLogoMaxWidth = xTheme.ClearLogoMaxWidth
        frmMain.ClearLogoMaxHeight = xTheme.ClearLogoMaxHeight
        frmMain.ClearLogoPosLeft = xTheme.ClearLogoPosLeft
        frmMain.ClearLogoPosTop = xTheme.ClearLogoPosTop
        frmMain.DiscArtMaxWidth = xTheme.DiscArtMaxWidth
        frmMain.DiscArtMaxHeight = xTheme.DiscArtMaxHeight
        frmMain.DiscArtPosLeft = xTheme.DiscArtPosLeft
        frmMain.DiscArtPosTop = xTheme.DiscArtPosTop
        frmMain.FanartSmallMaxWidth = xTheme.FanartSmallMaxWidth
        frmMain.FanartSmallMaxHeight = xTheme.FanartSmallMaxHeight
        frmMain.FanartSmallPosLeft = xTheme.FanartSmallPosLeft
        frmMain.FanartSmallPosTop = xTheme.FanartSmallPosTop
        frmMain.GenrePanelColor = xTheme.GenreBackColor
        frmMain.IPMid = xTheme.IPMid
        frmMain.IPUp = xTheme.IPUp
        frmMain.LandscapeMaxWidth = xTheme.LandscapeMaxWidth
        frmMain.LandscapeMaxHeight = xTheme.LandscapeMaxHeight
        frmMain.LandscapePosLeft = xTheme.LandscapePosLeft
        frmMain.LandscapePosTop = xTheme.LandscapePosTop
        frmMain.PosterMaxWidth = xTheme.PosterMaxWidth
        frmMain.PosterMaxHeight = xTheme.PosterMaxHeight
        frmMain.PosterPosLeft = xTheme.PosterPosLeft
        frmMain.PosterPosTop = xTheme.PosterPosTop

        For Each xCon As Controls In xTheme.Controls
            Select Case xCon.Control
                Case "pnlInfoPanel"
                    xControl = frmMain.pnlInfoPanel
                Case "pbTop250", "lblTop250"
                    xControl = frmMain.pnlTop250.Controls(xCon.Control)
                Case "lblActorsHeader", "lstActors", "pbActors", "pbActLoad"
                    xControl = frmMain.pnlActors.Controls(xCon.Control)
                Case "lblMoviesInSetHeader", "lvMoviesInSet"
                    xControl = frmMain.pnlMoviesInSet.Controls(xCon.Control)
                Case Else
                    xControl = frmMain.pnlInfoPanel.Controls(xCon.Control)
            End Select

            If Not xCon.Control = "pnlInfoPanel" Then xControl.Visible = xCon.Visible
            If xCon.Visible Then
                If Not xCon.Control = "pnlInfoPanel" AndAlso Not String.IsNullOrEmpty(xCon.Width) Then xControl.Width = EvaluateFormula(xCon.Width)
                If Not xCon.Control = "pnlInfoPanel" AndAlso Not String.IsNullOrEmpty(xCon.Height) Then xControl.Height = EvaluateFormula(xCon.Height)
                If Not xCon.Control = "pnlInfoPanel" AndAlso Not String.IsNullOrEmpty(xCon.Left) Then xControl.Left = EvaluateFormula(xCon.Left)
                If Not xCon.Control = "pnlInfoPanel" AndAlso Not String.IsNullOrEmpty(xCon.Top) Then xControl.Top = EvaluateFormula(xCon.Top)
                If Not xCon.Control = "btnUp" AndAlso Not xCon.Control = "btnMid" AndAlso Not xCon.Control = "btnDown" AndAlso Not xCon.Control = "btnMetaDataRefresh" Then xControl.BackColor = xCon.BackColor
                xControl.ForeColor = xCon.ForeColor
                xControl.Font = xCon.Font
                If Not xCon.Control = "pnlInfoPanel" Then xControl.Anchor = xCon.Anchor
            End If
        Next
    End Sub

    Public Sub BuildControlList()
        Try
            _availablecontrols.Clear()
            Const PossibleControls As String = "pnlInfoPanel,lblInfoPanelHeader,btnUp,btnMid,btnDown,lblDirectorsHeader,lblDirectors,lblReleaseDateHeader,lblReleaseDate,pnlTop250,pbTop250,lblTop250,lblOutlineHeader,txtOutline,lblIMDBHeader,txtIMDBID,lblTMDBHeader,txtTMDBID,lblCertificationsHeader,txtCertifications,lblFilePathHeader,txtFilePath,btnFilePlay,lblTrailerPathHeader,txtTrailerPath,btnTrailerPlay,pnlActors,lblActorsHeader,lstActors,pbActors,pbActLoad,lblPlotHeader,txtPlot,lblMetaDataHeader,btnMetaDataRefresh,txtMetaData,pbMILoading,pnlMoviesInSet,lblMoviesInSetHeader,lvMoviesInSet"
            For Each sCon As String In PossibleControls.Split(Convert.ToChar(","))
                _availablecontrols.Add(New Controls With {.Control = sCon})
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function EvaluateFormula(ByVal sFormula As String) As Integer
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
                    For Each rMatch As Regex In rProcs
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

            Return Convert.ToInt32(sFormula)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & sFormula)
        End Try

        Return 0
    End Function

    Private Function ParseMediaListColors(ByVal node As IEnumerable(Of XElement)) As Theme.MediaListColors
        Dim nMediaListColors As New Theme.MediaListColors
        'BackColor
        If Not String.IsNullOrEmpty(node.<backcolor>.Value) Then
            If Integer.TryParse(node.<backcolor>.Value, 0) Then
                nMediaListColors.BackColor = Color.FromArgb(Convert.ToInt32(node.<backcolor>.Value))
            ElseIf Color.FromName(node.<backcolor>.Value).IsKnownColor OrElse node.<backcolor>.Value.StartsWith("#") Then
                nMediaListColors.BackColor = ColorTranslator.FromHtml(node.<backcolor>.Value)
            Else
                logger.Error(String.Concat("No valid color value: ", node.<backcolor>.Value))
                Return Nothing
            End If
        End If
        'ForeColor
        If Not String.IsNullOrEmpty(node.<forecolor>.Value) Then
            If Integer.TryParse(node.<forecolor>.Value, 0) Then
                nMediaListColors.ForeColor = Color.FromArgb(Convert.ToInt32(node.<forecolor>.Value))
            ElseIf Color.FromName(node.<forecolor>.Value).IsKnownColor OrElse node.<forecolor>.Value.StartsWith("#") Then
                nMediaListColors.ForeColor = ColorTranslator.FromHtml(node.<forecolor>.Value)
            Else
                logger.Error(String.Concat("No valid color value: ", node.<forecolor>.Value))
                Return Nothing
            End If
        End If
        'SelectionBackColor
        If Not String.IsNullOrEmpty(node.<selectionbackcolor>.Value) Then
            If Integer.TryParse(node.<selectionbackcolor>.Value, 0) Then
                nMediaListColors.SelectionBackColor = Color.FromArgb(Convert.ToInt32(node.<selectionbackcolor>.Value))
            ElseIf Color.FromName(node.<selectionbackcolor>.Value).IsKnownColor OrElse node.<selectionbackcolor>.Value.StartsWith("#") Then
                nMediaListColors.SelectionBackColor = ColorTranslator.FromHtml(node.<selectionbackcolor>.Value)
            Else
                logger.Error(String.Concat("No valid color value: ", node.<selectionbackcolor>.Value))
                Return Nothing
            End If
        End If
        'SelectionForeColor
        If Not String.IsNullOrEmpty(node.<selectionforecolor>.Value) Then
            If Integer.TryParse(node.<selectionforecolor>.Value, 0) Then
                nMediaListColors.SelectionForeColor = Color.FromArgb(Convert.ToInt32(node.<selectionforecolor>.Value))
            ElseIf Color.FromName(node.<selectionforecolor>.Value).IsKnownColor OrElse node.<selectionforecolor>.Value.StartsWith("#") Then
                nMediaListColors.SelectionForeColor = ColorTranslator.FromHtml(node.<selectionforecolor>.Value)
            Else
                logger.Error(String.Concat("No valid color value: ", node.<selectionforecolor>.Value))
                Return Nothing
            End If
        End If
        Return nMediaListColors
    End Function

    Public Sub ParseThemes(ByRef tTheme As Theme, ByVal tType As String, ByVal sTheme As String)
        Dim ThemeXML As New XDocument
        Dim cControl As Controls
        Dim cName As String = String.Empty
        Dim cFont As String = "Microsoft Sans Serif"
        Dim cFontSize As Integer = 8
        Dim cFontStyle As FontStyle = FontStyle.Bold

        Dim tPath As String = String.Concat(Functions.AppPath, "Themes", Path.DirectorySeparatorChar, String.Format("{0}-{1}.xml", tType, sTheme))
        If File.Exists(tPath) Then
            ThemeXML = XDocument.Load(tPath)
        Else
            Select Case tType
                Case "movie"
                    ThemeXML = XDocument.Parse(My.Resources.movie_Default)
                Case "movieset"
                    ThemeXML = XDocument.Parse(My.Resources.movieset_Default)
                Case "tvshow"
                    ThemeXML = XDocument.Parse(My.Resources.tvshow_Default)
                Case "tvep"
                    ThemeXML = XDocument.Parse(My.Resources.tvep_Default)
            End Select
        End If

        'top panel
        Try
            Dim xTop = From xTheme In ThemeXML...<theme>...<toppanel>

            If xTop.Count > 0 Then
                If Not String.IsNullOrEmpty(xTop.<backcolor>.Value) Then
                    If Integer.TryParse(xTop.<backcolor>.Value, 0) Then
                        tTheme.TopPanelBackColor = Color.FromArgb(Convert.ToInt32(xTop.<backcolor>.Value))
                    ElseIf Color.FromName(xTop.<backcolor>.Value).IsKnownColor OrElse xTop.<backcolor>.Value.StartsWith("#") Then
                        tTheme.TopPanelBackColor = ColorTranslator.FromHtml(xTop.<backcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xTop.<backcolor>.Value))
                    End If
                End If

                If Not String.IsNullOrEmpty(xTop.<forecolor>.Value) Then
                    If Integer.TryParse(xTop.<forecolor>.Value, 0) Then
                        tTheme.TopPanelForeColor = Color.FromArgb(Convert.ToInt32(xTop.<forecolor>.Value))
                    ElseIf Color.FromName(xTop.<forecolor>.Value).IsKnownColor OrElse xTop.<forecolor>.Value.StartsWith("#") Then
                        tTheme.TopPanelForeColor = ColorTranslator.FromHtml(xTop.<forecolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xTop.<forecolor>.Value))
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        'media list colors
        Try
            'Default
            Dim xMediaListColor_Default = From xTheme In ThemeXML...<theme>...<medialistcolors>...<default>
            If xMediaListColor_Default.Count > 0 Then
                Dim nMediaListColors = ParseMediaListColors(xMediaListColor_Default)
                If nMediaListColors IsNot Nothing Then
                    tTheme.MediaListColor_Default = nMediaListColors
                End If
            End If
            'Locked
            Dim xMediaListColor_Locked = From xTheme In ThemeXML...<theme>...<medialistcolors>...<locked>
            If xMediaListColor_Locked.Count > 0 Then
                Dim nMediaListColors = ParseMediaListColors(xMediaListColor_Locked)
                If nMediaListColors IsNot Nothing Then
                    tTheme.MediaListColor_Locked = nMediaListColors
                End If
            End If
            'Marked
            Dim xMediaListColor_Marked = From xTheme In ThemeXML...<theme>...<medialistcolors>...<marked>
            If xMediaListColor_Marked.Count > 0 Then
                Dim nMediaListColors = ParseMediaListColors(xMediaListColor_Marked)
                If nMediaListColors IsNot Nothing Then
                    tTheme.MediaListColor_Marked = nMediaListColors
                End If
            End If
            'Missing
            Dim xMediaListColor_Missing = From xTheme In ThemeXML...<theme>...<medialistcolors>...<missing>
            If xMediaListColor_Missing.Count > 0 Then
                Dim nMediaListColors = ParseMediaListColors(xMediaListColor_Missing)
                If nMediaListColors IsNot Nothing Then
                    tTheme.MediaListColor_Missing = nMediaListColors
                End If
            End If
            'New
            Dim xMediaListColor_New = From xTheme In ThemeXML...<theme>...<medialistcolors>...<new>
            If xMediaListColor_New.Count > 0 Then
                Dim nMediaListColors = ParseMediaListColors(xMediaListColor_New)
                If nMediaListColors IsNot Nothing Then
                    tTheme.MediaListColor_New = nMediaListColors
                End If
            End If
            'OutOfTolerance
            Dim xMediaListColor_OutOfTolerance = From xTheme In ThemeXML...<theme>...<medialistcolors>...<outoftolerance>
            If xMediaListColor_OutOfTolerance.Count > 0 Then
                Dim nMediaListColors = ParseMediaListColors(xMediaListColor_OutOfTolerance)
                If nMediaListColors IsNot Nothing Then
                    tTheme.MediaListColor_OutOfTolerance = nMediaListColors
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        'images
        Try
            Dim xImages = From xTheme In ThemeXML...<theme>...<images>
            If xImages.Count > 0 Then

                'Banner
                If Not String.IsNullOrEmpty(xImages.<bannerbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<bannerbackcolor>.Value, 0) Then
                        tTheme.BannerBackColor = Color.FromArgb(Convert.ToInt32(xImages.<bannerbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<bannerbackcolor>.Value).IsKnownColor OrElse xImages.<bannerbackcolor>.Value.StartsWith("#") Then
                        tTheme.BannerBackColor = ColorTranslator.FromHtml(xImages.<bannerbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<bannerbackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannerbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<bannerbottombackcolor>.Value, 0) Then
                        tTheme.BannerBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<bannerbottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<bannerbottombackcolor>.Value).IsKnownColor OrElse xImages.<bannerbottombackcolor>.Value.StartsWith("#") Then
                        tTheme.BannerBottomBackColor = ColorTranslator.FromHtml(xImages.<bannerbottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<bannerbottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannermaxheight>.Value) Then
                    If Integer.TryParse(xImages.<bannermaxheight>.Value, 0) Then
                        tTheme.BannerMaxHeight = Convert.ToInt32(xImages.<bannermaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannermaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<bannermaxwidth>.Value, 0) Then
                        tTheme.BannerMaxWidth = Convert.ToInt32(xImages.<bannermaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannerposleft>.Value) Then
                    If Integer.TryParse(xImages.<bannerposleft>.Value, 0) Then
                        tTheme.BannerPosLeft = Convert.ToInt32(xImages.<bannerposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannerpostop>.Value) Then
                    If Integer.TryParse(xImages.<bannerpostop>.Value, 0) Then
                        tTheme.BannerPosTop = Convert.ToInt32(xImages.<bannerpostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannertopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<bannertopbackcolor>.Value, 0) Then
                        tTheme.BannerTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<bannertopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<bannertopbackcolor>.Value).IsKnownColor OrElse xImages.<bannertopbackcolor>.Value.StartsWith("#") Then
                        tTheme.BannerTopBackColor = ColorTranslator.FromHtml(xImages.<bannertopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<bannertopbackcolor>.Value))
                    End If
                End If

                'Characterart
                If Not String.IsNullOrEmpty(xImages.<characterartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<characterartbackcolor>.Value, 0) Then
                        tTheme.CharacterArtBackColor = Color.FromArgb(Convert.ToInt32(xImages.<characterartbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<characterartbackcolor>.Value).IsKnownColor OrElse xImages.<characterartbackcolor>.Value.StartsWith("#") Then
                        tTheme.CharacterArtBackColor = ColorTranslator.FromHtml(xImages.<characterartbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<characterartbackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<characterartbottombackcolor>.Value, 0) Then
                        tTheme.CharacterArtBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<characterartbottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<characterartbottombackcolor>.Value).IsKnownColor OrElse xImages.<characterartbottombackcolor>.Value.StartsWith("#") Then
                        tTheme.CharacterArtBottomBackColor = ColorTranslator.FromHtml(xImages.<characterartbottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<characterartbottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterartmaxheight>.Value) Then
                    If Integer.TryParse(xImages.<characterartmaxheight>.Value, 0) Then
                        tTheme.CharacterArtMaxHeight = Convert.ToInt32(xImages.<characterartmaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterartmaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<characterartmaxwidth>.Value, 0) Then
                        tTheme.CharacterArtMaxWidth = Convert.ToInt32(xImages.<characterartmaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterartposleft>.Value) Then
                    If Integer.TryParse(xImages.<characterartposleft>.Value, 0) Then
                        tTheme.CharacterArtPosLeft = Convert.ToInt32(xImages.<characterartposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterartpostop>.Value) Then
                    If Integer.TryParse(xImages.<characterartpostop>.Value, 0) Then
                        tTheme.CharacterArtPosTop = Convert.ToInt32(xImages.<characterartpostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterarttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<characterarttopbackcolor>.Value, 0) Then
                        tTheme.CharacterArtTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<characterarttopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<characterarttopbackcolor>.Value).IsKnownColor OrElse xImages.<characterarttopbackcolor>.Value.StartsWith("#") Then
                        tTheme.CharacterArtTopBackColor = ColorTranslator.FromHtml(xImages.<characterarttopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<characterarttopbackcolor>.Value))
                    End If
                End If

                'Clearart
                If Not String.IsNullOrEmpty(xImages.<clearartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearartbackcolor>.Value, 0) Then
                        tTheme.ClearArtBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearartbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<clearartbackcolor>.Value).IsKnownColor OrElse xImages.<clearartbackcolor>.Value.StartsWith("#") Then
                        tTheme.ClearArtBackColor = ColorTranslator.FromHtml(xImages.<clearartbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<clearartbackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearartbottombackcolor>.Value, 0) Then
                        tTheme.ClearArtBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearartbottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<clearartbottombackcolor>.Value).IsKnownColor OrElse xImages.<clearartbottombackcolor>.Value.StartsWith("#") Then
                        tTheme.ClearArtBottomBackColor = ColorTranslator.FromHtml(xImages.<clearartbottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<clearartbottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearartmaxheight>.Value) Then
                    If Integer.TryParse(xImages.<clearartmaxheight>.Value, 0) Then
                        tTheme.ClearArtMaxHeight = Convert.ToInt32(xImages.<clearartmaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearartmaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<clearartmaxwidth>.Value, 0) Then
                        tTheme.ClearArtMaxWidth = Convert.ToInt32(xImages.<clearartmaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearartposleft>.Value) Then
                    If Integer.TryParse(xImages.<clearartposleft>.Value, 0) Then
                        tTheme.ClearArtPosLeft = Convert.ToInt32(xImages.<clearartposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearartpostop>.Value) Then
                    If Integer.TryParse(xImages.<clearartpostop>.Value, 0) Then
                        tTheme.ClearArtPosTop = Convert.ToInt32(xImages.<clearartpostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<cleararttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<cleararttopbackcolor>.Value, 0) Then
                        tTheme.ClearArtTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<cleararttopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<cleararttopbackcolor>.Value).IsKnownColor OrElse xImages.<cleararttopbackcolor>.Value.StartsWith("#") Then
                        tTheme.ClearArtTopBackColor = ColorTranslator.FromHtml(xImages.<cleararttopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<cleararttopbackcolor>.Value))
                    End If
                End If

                'Clearlogo
                If Not String.IsNullOrEmpty(xImages.<clearlogobackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearlogobackcolor>.Value, 0) Then
                        tTheme.ClearlogoBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearlogobackcolor>.Value))
                    ElseIf Color.FromName(xImages.<clearlogobackcolor>.Value).IsKnownColor OrElse xImages.<clearlogobackcolor>.Value.StartsWith("#") Then
                        tTheme.ClearlogoBackColor = ColorTranslator.FromHtml(xImages.<clearlogobackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<clearlogobackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogobottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearlogobottombackcolor>.Value, 0) Then
                        tTheme.ClearLogoBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearlogobottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<clearlogobottombackcolor>.Value).IsKnownColor OrElse xImages.<clearlogobottombackcolor>.Value.StartsWith("#") Then
                        tTheme.ClearLogoBottomBackColor = ColorTranslator.FromHtml(xImages.<clearlogobottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<clearlogobottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogomaxheight>.Value) Then
                    If Integer.TryParse(xImages.<clearlogomaxheight>.Value, 0) Then
                        tTheme.ClearLogoMaxHeight = Convert.ToInt32(xImages.<clearlogomaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogomaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<clearlogomaxwidth>.Value, 0) Then
                        tTheme.ClearLogoMaxWidth = Convert.ToInt32(xImages.<clearlogomaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogoposleft>.Value) Then
                    If Integer.TryParse(xImages.<clearlogoposleft>.Value, 0) Then
                        tTheme.ClearLogoPosLeft = Convert.ToInt32(xImages.<clearlogoposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogopostop>.Value) Then
                    If Integer.TryParse(xImages.<clearlogopostop>.Value, 0) Then
                        tTheme.ClearLogoPosTop = Convert.ToInt32(xImages.<clearlogopostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogotopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearlogotopbackcolor>.Value, 0) Then
                        tTheme.ClearLogoTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearlogotopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<clearlogotopbackcolor>.Value).IsKnownColor OrElse xImages.<clearlogotopbackcolor>.Value.StartsWith("#") Then
                        tTheme.ClearLogoTopBackColor = ColorTranslator.FromHtml(xImages.<clearlogotopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<clearlogotopbackcolor>.Value))
                    End If
                End If

                'Discart
                If Not String.IsNullOrEmpty(xImages.<discartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<discartbackcolor>.Value, 0) Then
                        tTheme.DiscartBackColor = Color.FromArgb(Convert.ToInt32(xImages.<discartbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<discartbackcolor>.Value).IsKnownColor OrElse xImages.<discartbackcolor>.Value.StartsWith("#") Then
                        tTheme.DiscartBackColor = ColorTranslator.FromHtml(xImages.<discartbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<discartbackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<discartbottombackcolor>.Value, 0) Then
                        tTheme.DiscartBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<discartbottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<discartbottombackcolor>.Value).IsKnownColor OrElse xImages.<discartbottombackcolor>.Value.StartsWith("#") Then
                        tTheme.DiscartBottomBackColor = ColorTranslator.FromHtml(xImages.<discartbottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<discartbottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discartmaxheight>.Value) Then
                    If Integer.TryParse(xImages.<discartmaxheight>.Value, 0) Then
                        tTheme.DiscArtMaxHeight = Convert.ToInt32(xImages.<discartmaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discartmaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<discartmaxwidth>.Value, 0) Then
                        tTheme.DiscArtMaxWidth = Convert.ToInt32(xImages.<discartmaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discartposleft>.Value) Then
                    If Integer.TryParse(xImages.<discartposleft>.Value, 0) Then
                        tTheme.DiscArtPosLeft = Convert.ToInt32(xImages.<discartposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discartpostop>.Value) Then
                    If Integer.TryParse(xImages.<discartpostop>.Value, 0) Then
                        tTheme.DiscArtPosTop = Convert.ToInt32(xImages.<discartpostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discarttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<discarttopbackcolor>.Value, 0) Then
                        tTheme.DiscartTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<discarttopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<discarttopbackcolor>.Value).IsKnownColor OrElse xImages.<discarttopbackcolor>.Value.StartsWith("#") Then
                        tTheme.DiscartTopBackColor = ColorTranslator.FromHtml(xImages.<discarttopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<discarttopbackcolor>.Value))
                    End If
                End If

                'Fanart Main
                If Not String.IsNullOrEmpty(xImages.<fanartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartbackcolor>.Value, 0) Then
                        tTheme.FanartBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<fanartbackcolor>.Value).IsKnownColor OrElse xImages.<fanartbackcolor>.Value.StartsWith("#") Then
                        tTheme.FanartBackColor = ColorTranslator.FromHtml(xImages.<fanartbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<fanartbackcolor>.Value))
                    End If
                End If

                'Fanart Small
                If Not String.IsNullOrEmpty(xImages.<fanartsmallbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmallbackcolor>.Value, 0) Then
                        tTheme.FanartSmallBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartsmallbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<fanartsmallbackcolor>.Value).IsKnownColor OrElse xImages.<fanartsmallbackcolor>.Value.StartsWith("#") Then
                        tTheme.FanartSmallBackColor = ColorTranslator.FromHtml(xImages.<fanartsmallbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<fanartsmallbackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartsmallbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmallbottombackcolor>.Value, 0) Then
                        tTheme.FanartSmallBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartsmallbottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<fanartsmallbottombackcolor>.Value).IsKnownColor OrElse xImages.<fanartsmallbottombackcolor>.Value.StartsWith("#") Then
                        tTheme.FanartSmallBottomBackColor = ColorTranslator.FromHtml(xImages.<fanartsmallbottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<fanartsmallbottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartsmallmaxheight>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmallmaxheight>.Value, 0) Then
                        tTheme.FanartSmallMaxHeight = Convert.ToInt32(xImages.<fanartsmallmaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartsmallmaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmallmaxwidth>.Value, 0) Then
                        tTheme.FanartSmallMaxWidth = Convert.ToInt32(xImages.<fanartsmallmaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartsmallposleft>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmallposleft>.Value, 0) Then
                        tTheme.FanartSmallPosLeft = Convert.ToInt32(xImages.<fanartsmallposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartsmallpostop>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmallpostop>.Value, 0) Then
                        tTheme.FanartSmallPosTop = Convert.ToInt32(xImages.<fanartsmallpostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartsmalltopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartsmalltopbackcolor>.Value, 0) Then
                        tTheme.FanartSmallTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartsmalltopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<fanartsmalltopbackcolor>.Value).IsKnownColor OrElse xImages.<fanartsmalltopbackcolor>.Value.StartsWith("#") Then
                        tTheme.FanartSmallTopBackColor = ColorTranslator.FromHtml(xImages.<fanartsmalltopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<fanartsmalltopbackcolor>.Value))
                    End If
                End If

                'Landscape
                If Not String.IsNullOrEmpty(xImages.<landscapebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<landscapebackcolor>.Value, 0) Then
                        tTheme.LandscapeBackColor = Color.FromArgb(Convert.ToInt32(xImages.<landscapebackcolor>.Value))
                    ElseIf Color.FromName(xImages.<landscapebackcolor>.Value).IsKnownColor OrElse xImages.<landscapebackcolor>.Value.StartsWith("#") Then
                        tTheme.LandscapeBackColor = ColorTranslator.FromHtml(xImages.<landscapebackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<landscapebackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapebottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<landscapebottombackcolor>.Value, 0) Then
                        tTheme.LandscapeBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<landscapebottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<landscapebottombackcolor>.Value).IsKnownColor OrElse xImages.<landscapebottombackcolor>.Value.StartsWith("#") Then
                        tTheme.LandscapeBottomBackColor = ColorTranslator.FromHtml(xImages.<landscapebottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<landscapebottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapemaxheight>.Value) Then
                    If Integer.TryParse(xImages.<landscapemaxheight>.Value, 0) Then
                        tTheme.LandscapeMaxHeight = Convert.ToInt32(xImages.<landscapemaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapemaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<landscapemaxwidth>.Value, 0) Then
                        tTheme.LandscapeMaxWidth = Convert.ToInt32(xImages.<landscapemaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapeposleft>.Value) Then
                    If Integer.TryParse(xImages.<landscapeposleft>.Value, 0) Then
                        tTheme.LandscapePosLeft = Convert.ToInt32(xImages.<landscapeposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapepostop>.Value) Then
                    If Integer.TryParse(xImages.<landscapepostop>.Value, 0) Then
                        tTheme.LandscapePosTop = Convert.ToInt32(xImages.<landscapepostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapetopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<landscapetopbackcolor>.Value, 0) Then
                        tTheme.LandscapeTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<landscapetopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<landscapetopbackcolor>.Value).IsKnownColor OrElse xImages.<landscapetopbackcolor>.Value.StartsWith("#") Then
                        tTheme.LandscapeTopBackColor = ColorTranslator.FromHtml(xImages.<landscapetopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<landscapetopbackcolor>.Value))
                    End If
                End If

                'Poster
                If Not String.IsNullOrEmpty(xImages.<posterbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<posterbackcolor>.Value, 0) Then
                        tTheme.PosterBackColor = Color.FromArgb(Convert.ToInt32(xImages.<posterbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<posterbackcolor>.Value).IsKnownColor OrElse xImages.<posterbackcolor>.Value.StartsWith("#") Then
                        tTheme.PosterBackColor = ColorTranslator.FromHtml(xImages.<posterbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<posterbackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<posterbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<posterbottombackcolor>.Value, 0) Then
                        tTheme.PosterBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<posterbottombackcolor>.Value))
                    ElseIf Color.FromName(xImages.<posterbottombackcolor>.Value).IsKnownColor OrElse xImages.<posterbottombackcolor>.Value.StartsWith("#") Then
                        tTheme.PosterBottomBackColor = ColorTranslator.FromHtml(xImages.<posterbottombackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<posterbottombackcolor>.Value))
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<postermaxheight>.Value) Then
                    If Integer.TryParse(xImages.<postermaxheight>.Value, 0) Then
                        tTheme.PosterMaxHeight = Convert.ToInt32(xImages.<postermaxheight>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<postermaxwidth>.Value) Then
                    If Integer.TryParse(xImages.<postermaxwidth>.Value, 0) Then
                        tTheme.PosterMaxWidth = Convert.ToInt32(xImages.<postermaxwidth>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<posterposleft>.Value) Then
                    If Integer.TryParse(xImages.<posterposleft>.Value, 0) Then
                        tTheme.PosterPosLeft = Convert.ToInt32(xImages.<posterposleft>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<posterpostop>.Value) Then
                    If Integer.TryParse(xImages.<posterpostop>.Value, 0) Then
                        tTheme.PosterPosTop = Convert.ToInt32(xImages.<posterpostop>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<postertopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<postertopbackcolor>.Value, 0) Then
                        tTheme.PosterTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<postertopbackcolor>.Value))
                    ElseIf Color.FromName(xImages.<postertopbackcolor>.Value).IsKnownColor OrElse xImages.<postertopbackcolor>.Value.StartsWith("#") Then
                        tTheme.PosterTopBackColor = ColorTranslator.FromHtml(xImages.<postertopbackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<postertopbackcolor>.Value))
                    End If
                End If

                'Genre
                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    ElseIf Color.FromName(xImages.<genrebackcolor>.Value).IsKnownColor OrElse xImages.<genrebackcolor>.Value.StartsWith("#") Then
                        tTheme.GenreBackColor = ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<genrebackcolor>.Value))
                    End If
                End If

                'MPAA
                If Not String.IsNullOrEmpty(xImages.<mpaabackcolor>.Value) Then
                    If Integer.TryParse(xImages.<mpaabackcolor>.Value, 0) Then
                        tTheme.MPAABackColor = Color.FromArgb(Convert.ToInt32(xImages.<mpaabackcolor>.Value))
                    ElseIf Color.FromName(xImages.<mpaabackcolor>.Value).IsKnownColor OrElse xImages.<mpaabackcolor>.Value.StartsWith("#") Then
                        tTheme.MPAABackColor = ColorTranslator.FromHtml(xImages.<mpaabackcolor>.Value)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xImages.<mpaabackcolor>.Value))
                    End If
                End If

            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Try
            'info panel
            Dim xIPMain = From xTheme In ThemeXML...<theme>...<infopanel> Select xTheme.<backcolor>.Value, xTheme.<ipup>.Value, xTheme.<ipmid>.Value
            If xIPMain.Count > 0 Then
                If Not String.IsNullOrEmpty(xIPMain(0).backcolor) Then
                    If Integer.TryParse(xIPMain(0).backcolor, 0) Then
                        tTheme.InfoPanelBackColor = Color.FromArgb(Convert.ToInt32(xIPMain(0).backcolor))
                    ElseIf Color.FromName(xIPMain(0).backcolor).IsKnownColor OrElse xIPMain(0).backcolor.StartsWith("#") Then
                        tTheme.InfoPanelBackColor = ColorTranslator.FromHtml(xIPMain(0).backcolor)
                    Else
                        logger.Error(String.Concat("No valid color value: ", xIPMain(0).backcolor))
                    End If
                End If
                If Not String.IsNullOrEmpty(xIPMain(0).ipup) Then tTheme.IPUp = Convert.ToInt32(xIPMain(0).ipup)
                If Not String.IsNullOrEmpty(xIPMain(0).ipmid) Then tTheme.IPMid = Convert.ToInt32(xIPMain(0).ipmid)
            End If

            tTheme.Controls.Clear()
            For Each xIP As XElement In ThemeXML...<theme>...<infopanel>...<object>
                If Not String.IsNullOrEmpty(xIP.@name) Then
                    cName = xIP.@name
                    Dim xControl = From xCons As Controls In _availablecontrols Where xCons.Control = cName
                    If xControl.Count > 0 Then
                        cControl = New Controls
                        cControl.Control = cName
                        If Not String.IsNullOrEmpty(xIP.<width>.Value) Then cControl.Width = xIP.<width>.Value
                        If Not String.IsNullOrEmpty(xIP.<height>.Value) Then cControl.Height = xIP.<height>.Value
                        If Not String.IsNullOrEmpty(xIP.<left>.Value) Then cControl.Left = xIP.<left>.Value
                        If Not String.IsNullOrEmpty(xIP.<top>.Value) Then cControl.Top = xIP.<top>.Value
                        If Not String.IsNullOrEmpty(xIP.<backcolor>.Value) Then
                            If Integer.TryParse(xIP.<backcolor>.Value, 0) Then
                                cControl.BackColor = Color.FromArgb(Convert.ToInt32(xIP.<backcolor>.Value))
                            ElseIf Color.FromName(xIP.<backcolor>.Value).IsKnownColor OrElse xIP.<backcolor>.Value.StartsWith("#") Then
                                cControl.BackColor = ColorTranslator.FromHtml(xIP.<backcolor>.Value)
                            Else
                                logger.Error(String.Concat("No valid color value: ", xIP.<backcolor>.Value))
                            End If
                        End If
                        If Not String.IsNullOrEmpty(xIP.<forecolor>.Value) Then
                            If Integer.TryParse(xIP.<forecolor>.Value, 0) Then
                                cControl.ForeColor = Color.FromArgb(Convert.ToInt32(xIP.<forecolor>.Value))
                            ElseIf Color.FromName(xIP.<forecolor>.Value).IsKnownColor OrElse xIP.<forecolor>.Value.StartsWith("#") Then
                                cControl.ForeColor = ColorTranslator.FromHtml(xIP.<forecolor>.Value)
                            Else
                                logger.Error(String.Concat("No valid color value: ", xIP.<forecolor>.Value))
                            End If
                        End If
                        If Not String.IsNullOrEmpty(xIP.<anchor>.Value) Then cControl.Anchor = DirectCast(Convert.ToInt32(xIP.<anchor>.Value), AnchorStyles)
                        If Not String.IsNullOrEmpty(xIP.<visible>.Value) Then cControl.Visible = Convert.ToBoolean(xIP.<visible>.Value)

                        cFont = "Microsoft Sans Serif"
                        cFontSize = 8
                        cFontStyle = FontStyle.Regular

                        If Not String.IsNullOrEmpty(xIP.<font>.Value) Then cFont = xIP.<font>.Value
                        If Not String.IsNullOrEmpty(xIP.<fontsize>.Value) Then cFontSize = Convert.ToInt32(xIP.<fontsize>.Value)
                        If Not String.IsNullOrEmpty(xIP.<fontstyle>.Value) Then cFontStyle = DirectCast(Convert.ToInt32(xIP.<fontstyle>.Value), FontStyle)
                        cControl.Font = New Font(cFont, cFontSize, cFontStyle)
                        tTheme.Controls.Add(cControl)
                    End If
                End If

            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub ReplaceControlVars(ByRef sFormula As String)
        Dim xControl As New Control
        Dim cName As String

        Try

            For Each xCon As Match In Regex.Matches(sFormula, "(?<control>[a-z]+)\.(?<value>[a-z]+)", RegexOptions.IgnoreCase)
                cName = xCon.Groups("control").Value
                Dim aCon = From bCon As Controls In _availablecontrols Where bCon.Control.ToLower = cName.ToLower

                If aCon.Count > 0 Then
                    Select Case aCon(0).Control
                        Case "pnlInfoPanel"
                            xControl = frmMain.pnlInfoPanel
                        Case "pbTop250", "lblTop250"
                            xControl = frmMain.pnlTop250.Controls(aCon(0).Control)
                        Case "lblActorsHeader", "lstActors", "pbActors", "pbActLoad"
                            xControl = frmMain.pnlActors.Controls(aCon(0).Control)
                        Case "lblMoviesInSetHeader", "lvMoviesInSet"
                            xControl = frmMain.pnlMoviesInSet.Controls(aCon(0).Control)
                        Case Else
                            xControl = frmMain.pnlInfoPanel.Controls(aCon(0).Control)
                    End Select

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
                End If
            Next

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class Controls

#Region "Properties"

        Public Property Anchor() As AnchorStyles = AnchorStyles.None
        Public Property BackColor() As Color = Color.Gainsboro
        Public Property Control() As String = String.Empty
        Public Property ForeColor() As Color = Color.Black
        Public Property Height() As String = String.Empty
        Public Property Left() As String = String.Empty
        Public Property Top() As String = String.Empty
        Public Property Visible() As Boolean = True
        Public Property Width() As String = String.Empty
        Public Property [Font]() As Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

#End Region 'Properties

    End Class

    Public Class Theme

#Region "Properties"

        Public Property BannerBackColor() As Color = Color.Gainsboro
        Public Property BannerBottomBackColor() As Color = Color.DimGray
        Public Property BannerMaxHeight() As Integer = 160
        Public Property BannerMaxWidth() As Integer = 285
        Public Property BannerPosLeft() As Integer = 124
        Public Property BannerPosTop() As Integer = 327
        Public Property BannerTopBackColor() As Color = Color.DimGray
        Public Property CharacterArtBackColor() As Color = Color.Gainsboro
        Public Property CharacterArtBottomBackColor() As Color = Color.DimGray
        Public Property CharacterArtMaxHeight() As Integer = 160
        Public Property CharacterArtMaxWidth() As Integer = 160
        Public Property CharacterArtPosLeft() As Integer = 1011
        Public Property CharacterArtPosTop() As Integer = 130
        Public Property CharacterArtTopBackColor() As Color = Color.DimGray
        Public Property ClearArtBackColor() As Color = Color.Gainsboro
        Public Property ClearArtBottomBackColor() As Color = Color.DimGray
        Public Property ClearArtMaxHeight() As Integer = 160
        Public Property ClearArtMaxWidth() As Integer = 285
        Public Property ClearArtPosLeft() As Integer = 715
        Public Property ClearArtPosTop() As Integer = 130
        Public Property ClearArtTopBackColor() As Color = Color.DimGray
        Public Property ClearLogoBottomBackColor() As Color = Color.DimGray
        Public Property ClearLogoMaxHeight() As Integer = 160
        Public Property ClearLogoMaxWidth() As Integer = 285
        Public Property ClearLogoPosLeft() As Integer = 419
        Public Property ClearLogoPosTop() As Integer = 327
        Public Property ClearLogoTopBackColor() As Color = Color.DimGray
        Public Property ClearlogoBackColor() As Color = Color.Gainsboro
        Public Property Controls() As List(Of Controls) = New List(Of Controls)
        Public Property DiscArtMaxHeight() As Integer = 160
        Public Property DiscArtMaxWidth() As Integer = 160
        Public Property DiscArtPosLeft() As Integer = 1011
        Public Property DiscArtPosTop() As Integer = 130
        Public Property DiscartBackColor() As Color = Color.Gainsboro
        Public Property DiscartBottomBackColor() As Color = Color.DimGray
        Public Property DiscartTopBackColor() As Color = Color.DimGray
        Public Property FanartBackColor() As Color = Color.Gray
        Public Property FanartSmallBackColor() As Color = Color.Gainsboro
        Public Property FanartSmallBottomBackColor() As Color = Color.DimGray
        Public Property FanartSmallMaxHeight() As Integer = 160
        Public Property FanartSmallMaxWidth() As Integer = 285
        Public Property FanartSmallPosLeft() As Integer = 124
        Public Property FanartSmallPosTop() As Integer = 130
        Public Property FanartSmallTopBackColor() As Color = Color.DimGray
        Public Property GenreBackColor() As Color = Color.Gainsboro
        Public Property IPMid() As Integer = 280
        Public Property IPUp() As Integer = 500
        Public Property InfoPanelBackColor() As Color = Color.Gainsboro
        Public Property LandscapeBackColor() As Color = Color.Gainsboro
        Public Property LandscapeBottomBackColor() As Color = Color.DimGray
        Public Property LandscapeMaxHeight() As Integer = 160
        Public Property LandscapeMaxWidth() As Integer = 285
        Public Property LandscapePosLeft() As Integer = 419
        Public Property LandscapePosTop() As Integer = 130
        Public Property LandscapeTopBackColor() As Color = Color.DimGray
        Public Property MediaListColor_Default As MediaListColors = New MediaListColors With {
            .BackColor = Color.White,
            .ForeColor = Color.Black,
            .SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight),
            .SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        }
        Public Property MediaListColor_Locked As MediaListColors = New MediaListColors With {
            .BackColor = Color.LightSteelBlue,
            .ForeColor = Color.Black,
            .SelectionBackColor = Color.DarkTurquoise,
            .SelectionForeColor = Color.Black
        }
        Public Property MediaListColor_New As MediaListColors = New MediaListColors With {
            .BackColor = Color.White,
            .ForeColor = Color.Green,
            .SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight),
            .SelectionForeColor = Color.Green
        }
        Public Property MediaListColor_Marked As MediaListColors = New MediaListColors With {
            .BackColor = Color.White,
            .ForeColor = Color.Crimson,
            .SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight),
            .SelectionForeColor = Color.Crimson
        }
        Public Property MediaListColor_Missing As MediaListColors = New MediaListColors With {
            .BackColor = Color.White,
            .ForeColor = Color.Gray,
            .SelectionBackColor = Color.DarkGray,
            .SelectionForeColor = Color.LightGray
        }
        Public Property MediaListColor_OutOfTolerance As MediaListColors = New MediaListColors With {
            .BackColor = Color.MistyRose,
            .ForeColor = Color.Black,
            .SelectionBackColor = Color.DarkMagenta,
            .SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        }
        Public Property MPAABackColor() As Color = Color.Gainsboro
        Public Property PosterBackColor() As Color = Color.Gainsboro
        Public Property PosterBottomBackColor() As Color = Color.DimGray
        Public Property PosterMaxHeight() As Integer = 160
        Public Property PosterMaxWidth() As Integer = 160
        Public Property PosterPosLeft() As Integer = 4
        Public Property PosterPosTop() As Integer = 130
        Public Property PosterTopBackColor() As Color = Color.DimGray
        Public Property TopPanelBackColor() As Color = Color.Gainsboro
        Public Property TopPanelForeColor() As Color = Color.Black


#End Region 'Properties

#Region "Nested Types"

        Public Class MediaListColors

#Region "Properties"

            Public Property BackColor As Color
            Public Property ForeColor As Color
            Public Property SelectionBackColor As Color
            Public Property SelectionForeColor As Color

#End Region 'Properties

        End Class

#End Region 'Nested Types

    End Class

#End Region 'Nested Types

End Class