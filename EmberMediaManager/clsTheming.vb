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
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

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
        frmMain.pnlTop.BackColor = xTheme.TopPanelBackColor
        frmMain.pnlInfoIcons.BackColor = xTheme.TopPanelBackColor
        frmMain.pnlRating.BackColor = xTheme.TopPanelBackColor
        frmMain.pbVideo.BackColor = xTheme.TopPanelBackColor
        frmMain.pbResolution.BackColor = xTheme.TopPanelBackColor
        frmMain.pbAudio.BackColor = xTheme.TopPanelBackColor
        frmMain.pbChannels.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStudio.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar1.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar2.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar3.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar4.BackColor = xTheme.TopPanelBackColor
        frmMain.pbStar5.BackColor = xTheme.TopPanelBackColor

        frmMain.lblTitle.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblRating.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblRuntime.ForeColor = xTheme.TopPanelForeColor
        frmMain.lblTagline.ForeColor = xTheme.TopPanelForeColor


   
        frmMain.pnlMPAA.BackColor = xTheme.MPAABackColor
        frmMain.pbMPAA.BackColor = xTheme.MPAABackColor

        'Poster-Style
        frmMain.pnlPoster.BackColor = xTheme.PosterBackColor
        frmMain.pbPoster.BackColor = xTheme.PosterBackColor
        frmMain.pnlPosterMain.BackColor = xTheme.PosterBackColor
        frmMain.pnlPosterBottom.BackColor = xTheme.PosterBottomBackColor
        frmMain.pnlPosterTop.BackColor = xTheme.PosterTopBackColor
        'Fanart-Style
        frmMain.scMain.Panel2.BackColor = xTheme.FanartBigBackColor
        frmMain.pbFanart.BackColor = xTheme.FanartBigBackColor
        frmMain.pnlFanartSmall.BackColor = xTheme.FanartBackColor
        frmMain.pbFanartSmall.BackColor = xTheme.FanartBackColor
        frmMain.pnlFanartSmallMain.BackColor = xTheme.FanartBackColor
        frmMain.pnlFanartSmallBottom.BackColor = xTheme.FanartBottomBackColor
        frmMain.pnlFanartSmallTop.BackColor = xTheme.FanartTopBackColor
        'Banner-Style
        frmMain.pnlBanner.BackColor = xTheme.BannerBackColor
        frmMain.pbBanner.BackColor = xTheme.BannerBackColor
        frmMain.pnlBannerMain.BackColor = xTheme.BannerBackColor
        frmMain.pnlBannerBottom.BackColor = xTheme.BannerBottomBackColor
        frmMain.pnlBannerTop.BackColor = xTheme.BannerTopBackColor
        'CharacterArt-Style
        frmMain.pnlCharacterArt.BackColor = xTheme.CharacterArtBackColor
        frmMain.pbCharacterArt.BackColor = xTheme.CharacterArtBackColor
        frmMain.pnlCharacterArtMain.BackColor = xTheme.CharacterArtBackColor
        frmMain.pnlCharacterArtBottom.BackColor = xTheme.CharacterArtBottomBackColor
        frmMain.pnlCharacterArtTop.BackColor = xTheme.CharacterArtTopBackColor
        'ClearArt-Style
        frmMain.pnlClearArt.BackColor = xTheme.ClearArtBackColor
        frmMain.pbClearArt.BackColor = xTheme.ClearArtBackColor
        frmMain.pnlClearArtMain.BackColor = xTheme.ClearArtBackColor
        frmMain.pnlClearArtBottom.BackColor = xTheme.ClearArtBottomBackColor
        frmMain.pnlClearArtTop.BackColor = xTheme.ClearArtTopBackColor
        'ClearLogo-Style
        frmMain.pnlClearLogo.BackColor = xTheme.ClearlogoBackColor
        frmMain.pbClearLogo.BackColor = xTheme.ClearlogoBackColor
        frmMain.pnlClearLogoMain.BackColor = xTheme.ClearlogoBackColor
        frmMain.pnlClearLogoBottom.BackColor = xTheme.ClearlogoBottomBackColor
        frmMain.pnlClearLogoTop.BackColor = xTheme.ClearlogoTopBackColor
        'DiscArt-Style
        frmMain.pnlDiscArt.BackColor = xTheme.DiscartBackColor
        frmMain.pbDiscArt.BackColor = xTheme.DiscartBackColor
        frmMain.pnlDiscArtMain.BackColor = xTheme.DiscartBackColor
        frmMain.pnlDiscArtBottom.BackColor = xTheme.DiscartBottomBackColor
        frmMain.pnlDiscArtTop.BackColor = xTheme.DiscartTopBackColor
        'Landscape-Style
        frmMain.pnlLandscape.BackColor = xTheme.LandscapeBackColor
        frmMain.pbLandscape.BackColor = xTheme.LandscapeBackColor
        frmMain.pnlLandscapeMain.BackColor = xTheme.LandscapeBackColor
        frmMain.pnlLandscapeBottom.BackColor = xTheme.LandscapeBottomBackColor
        frmMain.pnlLandscapeTop.BackColor = xTheme.LandscapeTopBackColor


        frmMain.GenrePanelColor = xTheme.GenreBackColor
        frmMain.PosterMaxWidth = xTheme.PosterMaxWidth
        frmMain.PosterMaxHeight = xTheme.PosterMaxHeight
        frmMain.FanartSmallMaxWidth = xTheme.FanartSmallMaxWidth
        frmMain.FanartSmallMaxHeight = xTheme.FanartSmallMaxHeight
        frmMain.IPUp = xTheme.IPUp
        frmMain.IPMid = xTheme.IPMid

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
            Const PossibleControls As String = "pnlInfoPanel,lblInfoPanelHeader,btnUp,btnMid,btnDown,lblDirectorHeader,lblDirector,lblReleaseDateHeader,lblReleaseDate,pnlTop250,pbTop250,lblTop250,lblOutlineHeader,txtOutline,lblIMDBHeader,txtIMDBID,lblCertsHeader,txtCerts,lblFilePathHeader,txtFilePath,btnPlay,pnlActors,lblActorsHeader,lstActors,pbActors,pbActLoad,lblPlotHeader,txtPlot,lblMetaDataHeader,btnMetaDataRefresh,txtMetaData,pbMILoading,pnlMoviesInSet,lblMoviesInSetHeader,lvMoviesInSet"
            For Each sCon As String In PossibleControls.Split(Convert.ToChar(","))
                _availablecontrols.Add(New Controls With {.Control = sCon})
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
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
            logger.Error(New StackFrame().GetMethod().Name & sFormula, ex)
        End Try

        Return 0
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
                    If Integer.TryParse(xTop.<backcolor>.Value, 0) = True Then
                        tTheme.TopPanelBackColor = Color.FromArgb(Convert.ToInt32(xTop.<backcolor>.Value))
                    Else
                        tTheme.TopPanelBackColor = System.Drawing.ColorTranslator.FromHtml(xTop.<backcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xTop.<forecolor>.Value) Then
                    If Integer.TryParse(xTop.<forecolor>.Value, 0) = True Then
                        tTheme.TopPanelForeColor = Color.FromArgb(Convert.ToInt32(xTop.<forecolor>.Value))
                    Else
                        tTheme.TopPanelForeColor = System.Drawing.ColorTranslator.FromHtml(xTop.<forecolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xTop.<forecolor>.Value) Then
                    If Integer.TryParse(xTop.<forecolor>.Value, 0) = True Then
                        tTheme.TopPanelForeColor = Color.FromArgb(Convert.ToInt32(xTop.<forecolor>.Value))
                    Else
                        tTheme.TopPanelForeColor = System.Drawing.ColorTranslator.FromHtml(xTop.<forecolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xTop.<forecolor>.Value) Then
                    If Integer.TryParse(xTop.<forecolor>.Value, 0) = True Then
                        tTheme.TopPanelForeColor = Color.FromArgb(Convert.ToInt32(xTop.<forecolor>.Value))
                    Else
                        tTheme.TopPanelForeColor = System.Drawing.ColorTranslator.FromHtml(xTop.<forecolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xTop.<forecolor>.Value) Then
                    If Integer.TryParse(xTop.<forecolor>.Value, 0) = True Then
                        tTheme.TopPanelForeColor = Color.FromArgb(Convert.ToInt32(xTop.<forecolor>.Value))
                    Else
                        tTheme.TopPanelForeColor = System.Drawing.ColorTranslator.FromHtml(xTop.<forecolor>.Value)
                    End If
                End If

            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        'images
        Try
            Dim xImages = From xTheme In ThemeXML...<theme>...<images>
            If xImages.Count > 0 Then

                'Fanart
                If Not String.IsNullOrEmpty(xImages.<fanartbigbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartbigbackcolor>.Value, 0) = True Then
                        tTheme.FanartBigBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartbigbackcolor>.Value))
                    Else
                        tTheme.FanartBigBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<fanartbigbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartbackcolor>.Value, 0) = True Then
                        tTheme.FanartBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartbackcolor>.Value))
                    Else
                        tTheme.FanartBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<fanartbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanarttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanarttopbackcolor>.Value, 0) = True Then
                        tTheme.FanartTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanarttopbackcolor>.Value))
                    Else
                        tTheme.FanartTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<fanarttopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<fanartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<fanartbottombackcolor>.Value, 0) = True Then
                        tTheme.FanartBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<fanartbottombackcolor>.Value))
                    Else
                        tTheme.FanartBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<fanartbottombackcolor>.Value)
                    End If
                End If

                'Poster
                If Not String.IsNullOrEmpty(xImages.<posterbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<posterbackcolor>.Value, 0) = True Then
                        tTheme.PosterBackColor = Color.FromArgb(Convert.ToInt32(xImages.<posterbackcolor>.Value))
                    Else
                        tTheme.PosterBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<posterbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<postertopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<postertopbackcolor>.Value, 0) = True Then
                        tTheme.PosterTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<postertopbackcolor>.Value))
                    Else
                        tTheme.PosterTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<postertopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<posterbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<posterbottombackcolor>.Value, 0) = True Then
                        tTheme.PosterBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<posterbottombackcolor>.Value))
                    Else
                        tTheme.PosterBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<posterbottombackcolor>.Value)
                    End If
                End If

                'Banner
                If Not String.IsNullOrEmpty(xImages.<bannerbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<bannerbackcolor>.Value, 0) = True Then
                        tTheme.BannerBackColor = Color.FromArgb(Convert.ToInt32(xImages.<bannerbackcolor>.Value))
                    Else
                        tTheme.BannerBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<bannerbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannertopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<bannertopbackcolor>.Value, 0) = True Then
                        tTheme.BannerTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<bannertopbackcolor>.Value))
                    Else
                        tTheme.BannerTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<bannertopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<bannerbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<bannerbottombackcolor>.Value, 0) = True Then
                        tTheme.BannerBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<bannerbottombackcolor>.Value))
                    Else
                        tTheme.BannerBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<bannerbottombackcolor>.Value)
                    End If
                End If

                'Clearart
                If Not String.IsNullOrEmpty(xImages.<clearartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearartbackcolor>.Value, 0) = True Then
                        tTheme.ClearArtBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearartbackcolor>.Value))
                    Else
                        tTheme.ClearArtBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<clearartbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<cleararttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<cleararttopbackcolor>.Value, 0) = True Then
                        tTheme.ClearArtTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<cleararttopbackcolor>.Value))
                    Else
                        tTheme.ClearArtTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<cleararttopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearartbottombackcolor>.Value, 0) = True Then
                        tTheme.ClearArtBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearartbottombackcolor>.Value))
                    Else
                        tTheme.ClearArtBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<clearartbottombackcolor>.Value)
                    End If
                End If

                'Clearlogo
                If Not String.IsNullOrEmpty(xImages.<clearlogobackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearlogobackcolor>.Value, 0) = True Then
                        tTheme.ClearlogoBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearlogobackcolor>.Value))
                    Else
                        tTheme.ClearlogoBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<clearlogobackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogotopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearlogotopbackcolor>.Value, 0) = True Then
                        tTheme.ClearlogoTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearlogotopbackcolor>.Value))
                    Else
                        tTheme.ClearlogoTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<clearlogotopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<clearlogobottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<clearlogobottombackcolor>.Value, 0) = True Then
                        tTheme.ClearlogoBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<clearlogobottombackcolor>.Value))
                    Else
                        tTheme.ClearlogoBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<clearlogobottombackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<mpaabackcolor>.Value) Then
                    If Integer.TryParse(xImages.<mpaabackcolor>.Value, 0) = True Then
                        tTheme.MPAABackColor = Color.FromArgb(Convert.ToInt32(xImages.<mpaabackcolor>.Value))
                    Else
                        tTheme.MPAABackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<mpaabackcolor>.Value)
                    End If
                End If

                'Discart
                If Not String.IsNullOrEmpty(xImages.<discartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<discartbackcolor>.Value, 0) = True Then
                        tTheme.DiscartBackColor = Color.FromArgb(Convert.ToInt32(xImages.<discartbackcolor>.Value))
                    Else
                        tTheme.DiscartBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<discartbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discarttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<discarttopbackcolor>.Value, 0) = True Then
                        tTheme.DiscartTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<discarttopbackcolor>.Value))
                    Else
                        tTheme.DiscartTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<discarttopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<discartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<discartbottombackcolor>.Value, 0) = True Then
                        tTheme.DiscartBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<discartbottombackcolor>.Value))
                    Else
                        tTheme.DiscartBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<discartbottombackcolor>.Value)
                    End If
                End If

                'Landscape
                If Not String.IsNullOrEmpty(xImages.<landscapebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<landscapebackcolor>.Value, 0) = True Then
                        tTheme.LandscapeBackColor = Color.FromArgb(Convert.ToInt32(xImages.<landscapebackcolor>.Value))
                    Else
                        tTheme.LandscapeBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<landscapebackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapetopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<landscapetopbackcolor>.Value, 0) = True Then
                        tTheme.LandscapeTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<landscapetopbackcolor>.Value))
                    Else
                        tTheme.LandscapeTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<landscapetopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<landscapebottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<landscapebottombackcolor>.Value, 0) = True Then
                        tTheme.LandscapeBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<landscapebottombackcolor>.Value))
                    Else
                        tTheme.LandscapeBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<landscapebottombackcolor>.Value)
                    End If
                End If

                'Characterart
                If Not String.IsNullOrEmpty(xImages.<characterartbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<characterartbackcolor>.Value, 0) = True Then
                        tTheme.CharacterArtBackColor = Color.FromArgb(Convert.ToInt32(xImages.<characterartbackcolor>.Value))
                    Else
                        tTheme.CharacterArtBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<characterartbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterarttopbackcolor>.Value) Then
                    If Integer.TryParse(xImages.<characterarttopbackcolor>.Value, 0) = True Then
                        tTheme.CharacterArtTopBackColor = Color.FromArgb(Convert.ToInt32(xImages.<characterarttopbackcolor>.Value))
                    Else
                        tTheme.CharacterArtTopBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<characterarttopbackcolor>.Value)
                    End If
                End If
                If Not String.IsNullOrEmpty(xImages.<characterartbottombackcolor>.Value) Then
                    If Integer.TryParse(xImages.<characterartbottombackcolor>.Value, 0) = True Then
                        tTheme.CharacterArtBottomBackColor = Color.FromArgb(Convert.ToInt32(xImages.<characterartbottombackcolor>.Value))
                    Else
                        tTheme.CharacterArtBottomBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<characterartbottombackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<genrebackcolor>.Value) Then
                    If Integer.TryParse(xImages.<genrebackcolor>.Value, 0) = True Then
                        tTheme.GenreBackColor = Color.FromArgb(Convert.ToInt32(xImages.<genrebackcolor>.Value))
                    Else
                        tTheme.GenreBackColor = System.Drawing.ColorTranslator.FromHtml(xImages.<genrebackcolor>.Value)
                    End If
                End If


                If Not String.IsNullOrEmpty(xImages.<postermaxheight>.Value) Then
                    If Integer.TryParse(xImages.<postermaxheight>.Value, 0) = True Then
                        tTheme.PosterMaxHeight = Convert.ToInt32(xImages.<postermaxheight>.Value)
                    End If
                End If

                If Not String.IsNullOrEmpty(xImages.<postermaxwidth>.Value) Then tTheme.PosterMaxWidth = Convert.ToInt32(xImages.<postermaxwidth>.Value)
                If Not String.IsNullOrEmpty(xImages.<fanartsmallmaxheight>.Value) Then tTheme.FanartSmallMaxHeight = Convert.ToInt32(xImages.<fanartsmallmaxheight>.Value)
                If Not String.IsNullOrEmpty(xImages.<fanartsmallmaxwidth>.Value) Then tTheme.FanartSmallMaxWidth = Convert.ToInt32(xImages.<fanartsmallmaxwidth>.Value)
     
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try

        Try
            'info panel
            Dim xIPMain = From xTheme In ThemeXML...<theme>...<infopanel> Select xTheme.<backcolor>.Value, xTheme.<ipup>.Value, xTheme.<ipmid>.Value
            If xIPMain.Count > 0 Then
                If Not String.IsNullOrEmpty(xIPMain(0).backcolor) Then
                    If Integer.TryParse(xIPMain(0).backcolor, 0) = True Then
                        tTheme.InfoPanelBackColor = Color.FromArgb(Convert.ToInt32(xIPMain(0).backcolor))
                    Else
                        tTheme.InfoPanelBackColor = System.Drawing.ColorTranslator.FromHtml(xIPMain(0).backcolor)
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
                            If Integer.TryParse(xIP.<backcolor>.Value, 0) = True Then
                                cControl.BackColor = Color.FromArgb(Convert.ToInt32(xIP.<backcolor>.Value))
                            Else
                                cControl.BackColor = System.Drawing.ColorTranslator.FromHtml(xIP.<backcolor>.Value)
                            End If
                        End If
                        If Not String.IsNullOrEmpty(xIP.<forecolor>.Value) Then
                            If Integer.TryParse(xIP.<forecolor>.Value, 0) = True Then
                                cControl.ForeColor = Color.FromArgb(Convert.ToInt32(xIP.<forecolor>.Value))
                            Else
                                cControl.ForeColor = System.Drawing.ColorTranslator.FromHtml(xIP.<forecolor>.Value)
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
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
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class Controls

#Region "Fields"

        Dim _anchor As AnchorStyles
        Dim _backcolor As Color
        Dim _control As String
        Dim _font As Font
        Dim _forecolor As Color
        Dim _height As String
        Dim _left As String
        Dim _top As String
        Dim _visible As Boolean
        Dim _width As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Anchor() As AnchorStyles
            Get
                Return _anchor
            End Get
            Set(ByVal value As AnchorStyles)
                _anchor = value
            End Set
        End Property

        Public Property BackColor() As Color
            Get
                Return _backcolor
            End Get
            Set(ByVal value As Color)
                _backcolor = value
            End Set
        End Property

        Public Property Control() As String
            Get
                Return _control
            End Get
            Set(ByVal value As String)
                _control = value
            End Set
        End Property

        Public Property ForeColor() As Color
            Get
                Return _forecolor
            End Get
            Set(ByVal value As Color)
                _forecolor = value
            End Set
        End Property

        Public Property Height() As String
            Get
                Return _height
            End Get
            Set(ByVal value As String)
                _height = value
            End Set
        End Property

        Public Property Left() As String
            Get
                Return _left
            End Get
            Set(ByVal value As String)
                _left = value
            End Set
        End Property

        Public Property Top() As String
            Get
                Return _top
            End Get
            Set(ByVal value As String)
                _top = value
            End Set
        End Property

        Public Property Visible() As Boolean
            Get
                Return _visible
            End Get
            Set(ByVal value As Boolean)
                _visible = value
            End Set
        End Property

        Public Property Width() As String
            Get
                Return _width
            End Get
            Set(ByVal value As String)
                _width = value
            End Set
        End Property

        Public Property [Font]() As Font
            Get
                Return _font
            End Get
            Set(ByVal value As Font)
                _font = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _control = String.Empty
            _width = String.Empty
            _height = String.Empty
            _left = String.Empty
            _top = String.Empty
            _backcolor = Color.Gainsboro
            _forecolor = Color.Black
            _anchor = AnchorStyles.None
            _visible = True
            _font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        End Sub

#End Region 'Methods

    End Class

    Public Class Theme

#Region "Fields"

        Dim _controls As List(Of Controls)


        Dim _genrebackcolor As Color
        Dim _infopanelbackcolor As Color
        Dim _ipmid As Integer
        Dim _ipup As Integer
        Dim _mpaabackcolor As Color
        Dim _postermaxheight As Integer
        Dim _postermaxwidth As Integer
        Dim _fanartsmallmaxheight As Integer
        Dim _fanartsmallmaxwidth As Integer
        Dim _toppanelbackcolor As Color
        Dim _toppanelforecolor As Color

        Dim _posterbackcolor As Color
        Dim _postertopbackcolor As Color
        Dim _posterbottombackcolor As Color

        Dim _fanartbigbackcolor As Color
        Dim _fanartbackcolor As Color
        Dim _fanarttopbackcolor As Color
        Dim _fanartbottombackcolor As Color

        Dim _bannerbackcolor As Color
        Dim _bannertopbackcolor As Color
        Dim _bannerbottombackcolor As Color

        Dim _clearartbackcolor As Color
        Dim _cleararttopbackcolor As Color
        Dim _clearartbottombackcolor As Color

        Dim _landscapebackcolor As Color
        Dim _landscapetopbackcolor As Color
        Dim _landscapebottombackcolor As Color

        Dim _discartbackcolor As Color
        Dim _discarttopbackcolor As Color
        Dim _discartbottombackcolor As Color

        Dim _characterartbackcolor As Color
        Dim _characterarttopbackcolor As Color
        Dim _characterartbottombackcolor As Color

        Dim _clearlogobackcolor As Color
        Dim _clearlogotopbackcolor As Color
        Dim _clearlogobottombackcolor As Color

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Controls() As List(Of Controls)
            Get
                Return _controls
            End Get
            Set(ByVal value As List(Of Controls))
                _controls = value
            End Set
        End Property



        Public Property GenreBackColor() As Color
            Get
                Return _genrebackcolor
            End Get
            Set(ByVal value As Color)
                _genrebackcolor = value
            End Set
        End Property

        Public Property InfoPanelBackColor() As Color
            Get
                Return _infopanelbackcolor
            End Get
            Set(ByVal value As Color)
                _infopanelbackcolor = value
            End Set
        End Property

        Public Property IPMid() As Integer
            Get
                Return _ipmid
            End Get
            Set(ByVal value As Integer)
                _ipmid = value
            End Set
        End Property

        Public Property IPUp() As Integer
            Get
                Return _ipup
            End Get
            Set(ByVal value As Integer)
                _ipup = value
            End Set
        End Property

        Public Property MPAABackColor() As Color
            Get
                Return _mpaabackcolor
            End Get
            Set(ByVal value As Color)
                _mpaabackcolor = value
            End Set
        End Property

       

        Public Property PosterMaxHeight() As Integer
            Get
                Return _postermaxheight
            End Get
            Set(ByVal value As Integer)
                _postermaxheight = value
            End Set
        End Property

        Public Property PosterMaxWidth() As Integer
            Get
                Return _postermaxwidth
            End Get
            Set(ByVal value As Integer)
                _postermaxwidth = value
            End Set
        End Property

        Public Property FanartSmallMaxHeight() As Integer
            Get
                Return _fanartsmallmaxheight
            End Get
            Set(ByVal value As Integer)
                _fanartsmallmaxheight = value
            End Set
        End Property

        Public Property FanartSmallMaxWidth() As Integer
            Get
                Return _fanartsmallmaxwidth
            End Get
            Set(ByVal value As Integer)
                _fanartsmallmaxwidth = value
            End Set
        End Property

        Public Property TopPanelBackColor() As Color
            Get
                Return _toppanelbackcolor
            End Get
            Set(ByVal value As Color)
                _toppanelbackcolor = value
            End Set
        End Property

        Public Property TopPanelForeColor() As Color
            Get
                Return _toppanelforecolor
            End Get
            Set(ByVal value As Color)
                _toppanelforecolor = value
            End Set
        End Property

        'Poster-Styling
        Public Property PosterBackColor() As Color
            Get
                Return _posterbackcolor
            End Get
            Set(ByVal value As Color)
                _posterbackcolor = value
            End Set
        End Property

        Public Property PosterTopBackColor() As Color
            Get
                Return _Postertopbackcolor
            End Get
            Set(ByVal value As Color)
                _Postertopbackcolor = value
            End Set
        End Property

        Public Property PosterBottomBackColor() As Color
            Get
                Return _Posterbottombackcolor
            End Get
            Set(ByVal value As Color)
                _Posterbottombackcolor = value
            End Set
        End Property

        'Fanart-Styling

        Public Property FanartBigBackColor() As Color
            Get
                Return _fanartbigbackcolor
            End Get
            Set(ByVal value As Color)
                _fanartbigbackcolor = value
            End Set
        End Property

        Public Property FanartBackColor() As Color
            Get
                Return _fanartbackcolor
            End Get
            Set(ByVal value As Color)
                _fanartbackcolor = value
            End Set
        End Property

        Public Property FanartTopBackColor() As Color
            Get
                Return _Fanarttopbackcolor
            End Get
            Set(ByVal value As Color)
                _Fanarttopbackcolor = value
            End Set
        End Property

        Public Property FanartBottomBackColor() As Color
            Get
                Return _Fanartbottombackcolor
            End Get
            Set(ByVal value As Color)
                _Fanartbottombackcolor = value
            End Set
        End Property

        'Banner-Styling
        Public Property BannerBackColor() As Color
            Get
                Return _bannerbackcolor
            End Get
            Set(ByVal value As Color)
                _bannerbackcolor = value
            End Set
        End Property

        Public Property BannerTopBackColor() As Color
            Get
                Return _bannertopbackcolor
            End Get
            Set(ByVal value As Color)
                _bannertopbackcolor = value
            End Set
        End Property

        Public Property BannerBottomBackColor() As Color
            Get
                Return _bannerbottombackcolor
            End Get
            Set(ByVal value As Color)
                _bannerbottombackcolor = value
            End Set
        End Property

        'Clearlogo-Styling
        Public Property ClearlogoBackColor() As Color
            Get
                Return _clearlogobackcolor
            End Get
            Set(ByVal value As Color)
                _clearlogobackcolor = value
            End Set
        End Property

        Public Property ClearlogoTopBackColor() As Color
            Get
                Return _clearlogotopbackcolor
            End Get
            Set(ByVal value As Color)
                _clearlogotopbackcolor = value
            End Set
        End Property

        Public Property ClearlogoBottomBackColor() As Color
            Get
                Return _clearlogobottombackcolor
            End Get
            Set(ByVal value As Color)
                _clearlogobottombackcolor = value
            End Set
        End Property

        'ClearArt-Styling
        Public Property ClearArtBackColor() As Color
            Get
                Return _clearartbackcolor
            End Get
            Set(ByVal value As Color)
                _clearartbackcolor = value
            End Set
        End Property

        Public Property ClearArtTopBackColor() As Color
            Get
                Return _cleararttopbackcolor
            End Get
            Set(ByVal value As Color)
                _cleararttopbackcolor = value
            End Set
        End Property

        Public Property ClearArtBottomBackColor() As Color
            Get
                Return _clearartbottombackcolor
            End Get
            Set(ByVal value As Color)
                _clearartbottombackcolor = value
            End Set
        End Property

        'Discart-Styling
        Public Property DiscartBackColor() As Color
            Get
                Return _discartbackcolor
            End Get
            Set(ByVal value As Color)
                _discartbackcolor = value
            End Set
        End Property

        Public Property DiscartTopBackColor() As Color
            Get
                Return _discarttopbackcolor
            End Get
            Set(ByVal value As Color)
                _discarttopbackcolor = value
            End Set
        End Property

        Public Property DiscartBottomBackColor() As Color
            Get
                Return _discartbottombackcolor
            End Get
            Set(ByVal value As Color)
                _discartbottombackcolor = value
            End Set
        End Property

        'CharacterArt-Styling
        Public Property CharacterArtBackColor() As Color
            Get
                Return _characterartbackcolor
            End Get
            Set(ByVal value As Color)
                _characterartbackcolor = value
            End Set
        End Property

        Public Property CharacterArtTopBackColor() As Color
            Get
                Return _characterarttopbackcolor
            End Get
            Set(ByVal value As Color)
                _characterarttopbackcolor = value
            End Set
        End Property

        Public Property CharacterArtBottomBackColor() As Color
            Get
                Return _characterartbottombackcolor
            End Get
            Set(ByVal value As Color)
                _characterartbottombackcolor = value
            End Set
        End Property

        'Landscape-Styling
        Public Property LandscapeBackColor() As Color
            Get
                Return _landscapebackcolor
            End Get
            Set(ByVal value As Color)
                _landscapebackcolor = value
            End Set
        End Property

        Public Property LandscapeTopBackColor() As Color
            Get
                Return _landscapetopbackcolor
            End Get
            Set(ByVal value As Color)
                _landscapetopbackcolor = value
            End Set
        End Property

        Public Property LandscapeBottomBackColor() As Color
            Get
                Return _landscapebottombackcolor
            End Get
            Set(ByVal value As Color)
                _landscapebottombackcolor = value
            End Set
        End Property


#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _toppanelbackcolor = Color.Gainsboro
            _toppanelforecolor = Color.Black
            _posterbackcolor = Color.Gainsboro
            _posterbottombackcolor = Color.Gainsboro
            _postertopbackcolor = Color.Gainsboro
            _fanartbigbackcolor = Color.Gainsboro
            _fanartbackcolor = Color.Gainsboro
            _fanartbottombackcolor = Color.Gainsboro
            _fanarttopbackcolor = Color.Gainsboro
            _bannerbackcolor = Color.Gainsboro
            _bannerbottombackcolor = Color.Gainsboro
            _bannertopbackcolor = Color.Gainsboro
            _characterartbackcolor = Color.Gainsboro
            _characterartbottombackcolor = Color.Gainsboro
            _characterarttopbackcolor = Color.Gainsboro
            _clearartbackcolor = Color.Gainsboro
            _clearartbottombackcolor = Color.Gainsboro
            _cleararttopbackcolor = Color.Gainsboro
            _clearlogobackcolor = Color.Gainsboro
            _clearlogobottombackcolor = Color.Gainsboro
            _clearlogotopbackcolor = Color.Gainsboro
            _discartbackcolor = Color.Gainsboro
            _discartbottombackcolor = Color.Gainsboro
            _discarttopbackcolor = Color.Gainsboro
            _landscapebackcolor = Color.Gainsboro
            _landscapebottombackcolor = Color.Gainsboro
            _landscapetopbackcolor = Color.Gainsboro

            _postermaxwidth = 160
            _postermaxheight = 160
            _fanartsmallmaxwidth = 285
            _fanartsmallmaxheight = 160
            _mpaabackcolor = Color.Gainsboro
            _genrebackcolor = Color.Gainsboro
            _infopanelbackcolor = Color.Gainsboro
            _ipup = 500
            _ipmid = 280
            _controls = New List(Of Controls)
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class