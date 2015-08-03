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
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class dlgImgSelectNew


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwImgDownload As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwImgLoading As New System.ComponentModel.BackgroundWorker

    Public Delegate Sub LoadImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
    Public Delegate Sub Delegate_MeActivate()

    'Private CachePath As String = String.Empty
    Private chkImageET() As CheckBox
    Private chkImageEF() As CheckBox
    Private pnlImageET() As Panel
    Private pnlImageEF() As Panel
    Private DLType As Enums.ModifierType
    Private isWorkerDone As Boolean = False
    'Private ETHashes As New List(Of String)
    Private iCounter As Integer = 0
    Private iLeft As Integer = 5

    Private isEdit As Boolean = False
    'Private isShown As Boolean = False
    Private iTop As Integer = 5
    Private lblImage() As Label

    'Private noImages As Boolean = False
    Private pbImage() As PictureBox
    Private pnlImage() As Panel
    'Private PreDL As Boolean = False
    Private selIndex As Integer = -1

    Private tIsMovie As Boolean
    Private tDBElement As New Database.DBElement
    Private tmpImage As New MediaContainers.Image
    Private tmpImageEF As New MediaContainers.Image
    Private tmpImageET As New MediaContainers.Image

    Private _ImageList As New List(Of MediaContainers.Image)
    Private _efList As New List(Of String)
    Private _etList As New List(Of String)

    Private aDes As String = String.Empty

    Private DefaultImagesContainer As New MediaContainers.ImagesContainer
    Private DefaultEpisodeImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private DefaultSeasonImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private SearchResultsContainer As New MediaContainers.SearchResultsContainer
    Private tmpResultDBElement As New Database.DBElement

    Private tScrapeModifier As New Structures.ScrapeModifier
    Private tContentType As Enums.ContentType

#End Region 'Fields

#Region "Properties"

    Public Property Result As Database.DBElement
        Get
            Return tmpResultDBElement
        End Get
        Set(value As Database.DBElement)
            tmpResultDBElement = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifier As Structures.ScrapeModifier, ByVal ContentType As Enums.ContentType, Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.SearchResultsContainer = ImagesContainer
        Me.tDBElement = DBElement
        Me.tScrapeModifier = ScrapeModifier
        Me.tContentType = ContentType

        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImageSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler pnlImages.MouseWheel, AddressOf MouseWheelEvent
        'AddHandler MyBase.MouseWheel, AddressOf MouseWheelEvent
        'AddHandler tvList.MouseWheel, AddressOf MouseWheelEvent

        Functions.PNLDoubleBuffer(Me.pnlImages)

        Me.SetUp()
    End Sub

    Private Sub dlgImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Me.bwLoadData.WorkerReportsProgress = True
        'Me.bwLoadData.WorkerSupportsCancellation = True
        'Me.bwLoadData.RunWorkerAsync()
    End Sub

    Private Sub SetUp()
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

#End Region 'Methods

End Class