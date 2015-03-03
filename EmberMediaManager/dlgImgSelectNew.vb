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
    Private DLType As Enums.ImageType_Movie
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
    Private _results As New MediaContainers.Image
    Private selIndex As Integer = -1

    Private tIsMovie As Boolean
    Private tMovie As New Structures.DBMovie
    Private tMovieSet As New Structures.DBMovieSet
    Private tmpImage As New MediaContainers.Image
    Private tmpImageEF As New MediaContainers.Image
    Private tmpImageET As New MediaContainers.Image

    Private _ImageList As New List(Of MediaContainers.Image)
    Private _efList As New List(Of String)
    Private _etList As New List(Of String)

    Private aDes As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Public Property Results As MediaContainers.Image
        Get
            Return _results
        End Get
        Set(value As MediaContainers.Image)
            _results = value
        End Set
    End Property

    Public Property efList As List(Of String)
        Get
            Return _efList
        End Get
        Set(value As List(Of String))
            _efList = value
        End Set
    End Property

    Public Property etList As List(Of String)
        Get
            Return _etList
        End Get
        Set(value As List(Of String))
            _etList = value
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

#End Region 'Methods

End Class