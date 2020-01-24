<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgTrakttvManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTrakttvManager))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.pnlSaving = New System.Windows.Forms.Panel()
        Me.lblSaving = New System.Windows.Forms.Label()
        Me.prbSaving = New System.Windows.Forms.ProgressBar()
        Me.prbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tbTrakt = New System.Windows.Forms.TabControl()
        Me.tbptraktPlaycount = New System.Windows.Forms.TabPage()
        Me.pnltraktPlaycount = New System.Windows.Forms.Panel()
        Me.gbPlaycount = New System.Windows.Forms.GroupBox()
        Me.btnPlaycountSyncRating = New System.Windows.Forms.Button()
        Me.btnPlaycountSyncDeleteItem = New System.Windows.Forms.Button()
        Me.gbtraktPlaycountSync = New System.Windows.Forms.GroupBox()
        Me.btnPlaycountSyncWatched_Movies = New System.Windows.Forms.Button()
        Me.btnPlaycountSyncWatched_TVShows = New System.Windows.Forms.Button()
        Me.btnPlaycountGetList_Movies = New System.Windows.Forms.Button()
        Me.dgvPlaycount = New System.Windows.Forms.DataGridView()
        Me.colPlaycountTraktID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlaycountTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlaycountPlayed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlaycountLastWatched = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlaycountProgress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlaycountRating = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblPlaycountDone = New System.Windows.Forms.Label()
        Me.prgPlaycount = New System.Windows.Forms.ProgressBar()
        Me.lblPlaycountMessage = New System.Windows.Forms.Label()
        Me.btnSaveWatchedStateToEmber = New System.Windows.Forms.Button()
        Me.btnPlaycountGetList_TVShows = New System.Windows.Forms.Button()
        Me.tbptraktWatchlist = New System.Windows.Forms.TabPage()
        Me.pnltraktWatchlist = New System.Windows.Forms.Panel()
        Me.gbtraktWatchlist = New System.Windows.Forms.GroupBox()
        Me.gbtraktWatchlistExpert = New System.Windows.Forms.GroupBox()
        Me.btntraktWatchlistClean = New System.Windows.Forms.Button()
        Me.btntraktWatchlistSendEmberUnwatched = New System.Windows.Forms.Button()
        Me.btntraktWatchlistGetMovies = New System.Windows.Forms.Button()
        Me.dgvtraktWatchlist = New System.Windows.Forms.DataGridView()
        Me.coltraktWatchlistTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktWatchlistYear = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktWatchlistListedAt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktWatchlistIMDB = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.lbltraktWatchliststate = New System.Windows.Forms.Label()
        Me.prgtraktWatchlist = New System.Windows.Forms.ProgressBar()
        Me.lbltraktWatchlisthelp = New System.Windows.Forms.Label()
        Me.btntraktWatchlistSyncLibrary = New System.Windows.Forms.Button()
        Me.btntraktWatchlistGetSeries = New System.Windows.Forms.Button()
        Me.tbptraktListsSync = New System.Windows.Forms.TabPage()
        Me.pnltraktLists = New System.Windows.Forms.Panel()
        Me.lbltraktListsLink = New System.Windows.Forms.LinkLabel()
        Me.gbtraktListsSYNC = New System.Windows.Forms.GroupBox()
        Me.lbltraktListsNoticeSync = New System.Windows.Forms.Label()
        Me.btntraktListsSaveToDatabase = New System.Windows.Forms.Button()
        Me.btntraktListsSyncTrakt = New System.Windows.Forms.Button()
        Me.gbtraktListsGET = New System.Windows.Forms.GroupBox()
        Me.btntraktListsGetPersonal = New System.Windows.Forms.Button()
        Me.lbltraktListsstate = New System.Windows.Forms.Label()
        Me.gbtraktLists = New System.Windows.Forms.GroupBox()
        Me.gbtraktListsDetails = New System.Windows.Forms.GroupBox()
        Me.btntraktListsDetailsUpdate = New System.Windows.Forms.Button()
        Me.chktraktListsDetailsNumbers = New System.Windows.Forms.CheckBox()
        Me.chkltraktListsDetailsComments = New System.Windows.Forms.CheckBox()
        Me.lbltraktListsDetailsDescription = New System.Windows.Forms.Label()
        Me.lbltraktListsDetailsName = New System.Windows.Forms.Label()
        Me.lbltraktListsDetailsPrivacy = New System.Windows.Forms.Label()
        Me.cbotraktListsDetailsPrivacy = New System.Windows.Forms.ComboBox()
        Me.txttraktListsDetailsName = New System.Windows.Forms.TextBox()
        Me.txttraktListsDetailsDescription = New System.Windows.Forms.TextBox()
        Me.btntraktListsGetDatabase = New System.Windows.Forms.Button()
        Me.lbDBLists = New System.Windows.Forms.ListBox()
        Me.txttraktListsEditList = New System.Windows.Forms.TextBox()
        Me.btntraktListsRemoveList = New System.Windows.Forms.Button()
        Me.btntraktListsEditList = New System.Windows.Forms.Button()
        Me.btntraktListsNewList = New System.Windows.Forms.Button()
        Me.lbtraktLists = New System.Windows.Forms.ListBox()
        Me.prgtraktLists = New System.Windows.Forms.ProgressBar()
        Me.gbtraktListsMovies = New System.Windows.Forms.GroupBox()
        Me.dgvMovies = New System.Windows.Forms.DataGridView()
        Me.btntraktListsAddMovie = New System.Windows.Forms.Button()
        Me.gbtraktListsMoviesInLists = New System.Windows.Forms.GroupBox()
        Me.lbltraktListsCurrentList = New System.Windows.Forms.Label()
        Me.btntraktListsRemove = New System.Windows.Forms.Button()
        Me.lbtraktListsMoviesinLists = New System.Windows.Forms.ListBox()
        Me.tbptraktComments = New System.Windows.Forms.TabPage()
        Me.gbtraktComments = New System.Windows.Forms.GroupBox()
        Me.lbltraktCommentsNotice = New System.Windows.Forms.Label()
        Me.gbtraktCommentsList = New System.Windows.Forms.GroupBox()
        Me.dgvtraktComments = New System.Windows.Forms.DataGridView()
        Me.coltraktCommentsMovie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktCommentsDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktCommentsReplies = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktCommentsLikes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktCommentsURL = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.coltraktCommentsImdb = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chktraktCommentsOnlyNoComments = New System.Windows.Forms.CheckBox()
        Me.chktraktCommentsOnlyComments = New System.Windows.Forms.CheckBox()
        Me.gbtraktCommentsDetails = New System.Windows.Forms.GroupBox()
        Me.btntraktCommentsDetailsSend = New System.Windows.Forms.Button()
        Me.btntraktCommentsDetailsUpdate = New System.Windows.Forms.Button()
        Me.lbltraktCommentsDetailsDate2 = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsType2 = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsReplies2 = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsLikes2 = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsRating2 = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsRating = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsLikes = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsReplies = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsType = New System.Windows.Forms.Label()
        Me.btntraktCommentsDetailsDelete = New System.Windows.Forms.Button()
        Me.chktraktCommentsDetailsSpoiler = New System.Windows.Forms.CheckBox()
        Me.lbltraktCommentsDetailsDescription = New System.Windows.Forms.Label()
        Me.lbltraktCommentsDetailsDate = New System.Windows.Forms.Label()
        Me.txttraktCommentsDetailsComment = New System.Windows.Forms.TextBox()
        Me.gbtraktCommentsGET = New System.Windows.Forms.GroupBox()
        Me.btntraktCommentsGet = New System.Windows.Forms.Button()
        Me.tbptraktListViewer = New System.Windows.Forms.TabPage()
        Me.pnltraktListsComparer = New System.Windows.Forms.Panel()
        Me.gbtraktListsViewer = New System.Windows.Forms.GroupBox()
        Me.btntraktListsSendToKodi = New System.Windows.Forms.Button()
        Me.btntraktListsSavePlaylist = New System.Windows.Forms.Button()
        Me.cbotraktListsFavorites = New System.Windows.Forms.ComboBox()
        Me.gbtraktListsViewerStep2 = New System.Windows.Forms.GroupBox()
        Me.txttraktListURL = New System.Windows.Forms.TextBox()
        Me.btntraktListLoad = New System.Windows.Forms.Button()
        Me.lbltraktListURL = New System.Windows.Forms.Label()
        Me.btntraktListSaveFavorite = New System.Windows.Forms.Button()
        Me.btntraktListRemoveFavorite = New System.Windows.Forms.Button()
        Me.lbltraktListsFavorites = New System.Windows.Forms.Label()
        Me.gbtraktListsViewerStep1 = New System.Windows.Forms.GroupBox()
        Me.lbltraktListsScraped = New System.Windows.Forms.Label()
        Me.cbotraktListsScraped = New System.Windows.Forms.ComboBox()
        Me.btntraktListsGetFollowers = New System.Windows.Forms.Button()
        Me.btntraktListsGetFriends = New System.Windows.Forms.Button()
        Me.btntraktListsGetPopular = New System.Windows.Forms.Button()
        Me.lbltraktListDescriptionText = New System.Windows.Forms.Label()
        Me.lbltraktListDescription = New System.Windows.Forms.Label()
        Me.lbltraktListsCount = New System.Windows.Forms.Label()
        Me.chktraktListsCompare = New System.Windows.Forms.CheckBox()
        Me.btntraktListsSaveList = New System.Windows.Forms.Button()
        Me.btntraktListsSaveListCompare = New System.Windows.Forms.Button()
        Me.dgvtraktList = New System.Windows.Forms.DataGridView()
        Me.coltraktListTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktListYear = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktListRating = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktListGenres = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coltraktListIMDB = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.coltraktListTrailer = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.tbptraktCleaning = New System.Windows.Forms.TabPage()
        Me.gbtraktCleaning = New System.Windows.Forms.GroupBox()
        Me.gbtraktCleaningHistoryTimespan = New System.Windows.Forms.GroupBox()
        Me.lbltraktCleaningHistoryTimespanDesc = New System.Windows.Forms.Label()
        Me.cbotraktCleaningHistoryTimespan = New System.Windows.Forms.ComboBox()
        Me.lbltraktCleaningHistoryTimespan = New System.Windows.Forms.Label()
        Me.btntraktCleaningHistoryTimespan = New System.Windows.Forms.Button()
        Me.gbtraktCleaningHistoryTimestamp = New System.Windows.Forms.GroupBox()
        Me.lbltraktCleaningHistoryTimestamp = New System.Windows.Forms.Label()
        Me.txttraktCleaningHistoryTimestamp = New System.Windows.Forms.TextBox()
        Me.lbltraktCleaningHistoryTimestampDesc = New System.Windows.Forms.Label()
        Me.btntraktCleaningHistoryTimestamp = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTop.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        Me.pnlSaving.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.tbTrakt.SuspendLayout()
        Me.tbptraktPlaycount.SuspendLayout()
        Me.pnltraktPlaycount.SuspendLayout()
        Me.gbPlaycount.SuspendLayout()
        Me.gbtraktPlaycountSync.SuspendLayout()
        CType(Me.dgvPlaycount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbptraktWatchlist.SuspendLayout()
        Me.pnltraktWatchlist.SuspendLayout()
        Me.gbtraktWatchlist.SuspendLayout()
        Me.gbtraktWatchlistExpert.SuspendLayout()
        CType(Me.dgvtraktWatchlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbptraktListsSync.SuspendLayout()
        Me.pnltraktLists.SuspendLayout()
        Me.gbtraktListsSYNC.SuspendLayout()
        Me.gbtraktListsGET.SuspendLayout()
        Me.gbtraktLists.SuspendLayout()
        Me.gbtraktListsDetails.SuspendLayout()
        Me.gbtraktListsMovies.SuspendLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbtraktListsMoviesInLists.SuspendLayout()
        Me.tbptraktComments.SuspendLayout()
        Me.gbtraktComments.SuspendLayout()
        Me.gbtraktCommentsList.SuspendLayout()
        CType(Me.dgvtraktComments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbtraktCommentsDetails.SuspendLayout()
        Me.gbtraktCommentsGET.SuspendLayout()
        Me.tbptraktListViewer.SuspendLayout()
        Me.pnltraktListsComparer.SuspendLayout()
        Me.gbtraktListsViewer.SuspendLayout()
        Me.gbtraktListsViewerStep2.SuspendLayout()
        Me.gbtraktListsViewerStep1.SuspendLayout()
        CType(Me.dgvtraktList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbptraktCleaning.SuspendLayout()
        Me.gbtraktCleaning.SuspendLayout()
        Me.gbtraktCleaningHistoryTimespan.SuspendLayout()
        Me.gbtraktCleaningHistoryTimestamp.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(84, 32)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Close"
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTopDetails)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pnlCancel)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1107, 64)
        Me.pnlTop.TabIndex = 1
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(272, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Sync lists and playcount with your trakt.tv account."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(62, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(210, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Trakt.tv Manager"
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.White
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.pnlSaving)
        Me.pnlCancel.Controls.Add(Me.prbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(365, 3)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 40
        Me.pnlCancel.Visible = False
        '
        'pnlSaving
        '
        Me.pnlSaving.BackColor = System.Drawing.Color.White
        Me.pnlSaving.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSaving.Controls.Add(Me.lblSaving)
        Me.pnlSaving.Controls.Add(Me.prbSaving)
        Me.pnlSaving.Location = New System.Drawing.Point(77, 12)
        Me.pnlSaving.Name = "pnlSaving"
        Me.pnlSaving.Size = New System.Drawing.Size(252, 51)
        Me.pnlSaving.TabIndex = 5
        Me.pnlSaving.Visible = False
        '
        'lblSaving
        '
        Me.lblSaving.AutoSize = True
        Me.lblSaving.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSaving.Location = New System.Drawing.Point(2, 7)
        Me.lblSaving.Name = "lblSaving"
        Me.lblSaving.Size = New System.Drawing.Size(51, 13)
        Me.lblSaving.TabIndex = 0
        Me.lblSaving.Text = "Saving..."
        '
        'prbSaving
        '
        Me.prbSaving.Location = New System.Drawing.Point(4, 26)
        Me.prbSaving.MarqueeAnimationSpeed = 25
        Me.prbSaving.Name = "prbSaving"
        Me.prbSaving.Size = New System.Drawing.Size(242, 16)
        Me.prbSaving.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbSaving.TabIndex = 1
        '
        'prbCompile
        '
        Me.prbCompile.Location = New System.Drawing.Point(8, 36)
        Me.prbCompile.Name = "prbCompile"
        Me.prbCompile.Size = New System.Drawing.Size(388, 18)
        Me.prbCompile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prbCompile.TabIndex = 3
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 11)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(203, 20)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Loading Movies..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCompiling.Visible = False
        '
        'lblFile
        '
        Me.lblFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFile.Location = New System.Drawing.Point(3, 57)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(395, 13)
        Me.lblFile.TabIndex = 4
        Me.lblFile.Text = "File ..."
        '
        'lblCanceling
        '
        Me.lblCanceling.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanceling.Location = New System.Drawing.Point(110, 12)
        Me.lblCanceling.Name = "lblCanceling"
        Me.lblCanceling.Size = New System.Drawing.Size(186, 20)
        Me.lblCanceling.TabIndex = 1
        Me.lblCanceling.Text = "Canceling Load..."
        Me.lblCanceling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCanceling.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(298, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = Global.generic.EmberCore.Trakt.My.Resources.Resources.logo
        Me.pbTopLogo.Location = New System.Drawing.Point(12, 7)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.tbTrakt)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 64)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1107, 520)
        Me.pnlMain.TabIndex = 16
        '
        'tbTrakt
        '
        Me.tbTrakt.Controls.Add(Me.tbptraktPlaycount)
        Me.tbTrakt.Controls.Add(Me.tbptraktWatchlist)
        Me.tbTrakt.Controls.Add(Me.tbptraktListsSync)
        Me.tbTrakt.Controls.Add(Me.tbptraktComments)
        Me.tbTrakt.Controls.Add(Me.tbptraktListViewer)
        Me.tbTrakt.Controls.Add(Me.tbptraktCleaning)
        Me.tbTrakt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbTrakt.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTrakt.Location = New System.Drawing.Point(0, 0)
        Me.tbTrakt.Name = "tbTrakt"
        Me.tbTrakt.SelectedIndex = 0
        Me.tbTrakt.Size = New System.Drawing.Size(1107, 520)
        Me.tbTrakt.TabIndex = 1
        '
        'tbptraktPlaycount
        '
        Me.tbptraktPlaycount.Controls.Add(Me.pnltraktPlaycount)
        Me.tbptraktPlaycount.Location = New System.Drawing.Point(4, 27)
        Me.tbptraktPlaycount.Name = "tbptraktPlaycount"
        Me.tbptraktPlaycount.Padding = New System.Windows.Forms.Padding(3)
        Me.tbptraktPlaycount.Size = New System.Drawing.Size(1099, 489)
        Me.tbptraktPlaycount.TabIndex = 0
        Me.tbptraktPlaycount.Text = "Sync Playcount"
        Me.tbptraktPlaycount.UseVisualStyleBackColor = True
        '
        'pnltraktPlaycount
        '
        Me.pnltraktPlaycount.Controls.Add(Me.gbPlaycount)
        Me.pnltraktPlaycount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltraktPlaycount.Location = New System.Drawing.Point(3, 3)
        Me.pnltraktPlaycount.Name = "pnltraktPlaycount"
        Me.pnltraktPlaycount.Size = New System.Drawing.Size(1093, 483)
        Me.pnltraktPlaycount.TabIndex = 41
        '
        'gbPlaycount
        '
        Me.gbPlaycount.Controls.Add(Me.btnPlaycountSyncRating)
        Me.gbPlaycount.Controls.Add(Me.btnPlaycountSyncDeleteItem)
        Me.gbPlaycount.Controls.Add(Me.gbtraktPlaycountSync)
        Me.gbPlaycount.Controls.Add(Me.btnPlaycountGetList_Movies)
        Me.gbPlaycount.Controls.Add(Me.dgvPlaycount)
        Me.gbPlaycount.Controls.Add(Me.lblPlaycountDone)
        Me.gbPlaycount.Controls.Add(Me.prgPlaycount)
        Me.gbPlaycount.Controls.Add(Me.lblPlaycountMessage)
        Me.gbPlaycount.Controls.Add(Me.btnSaveWatchedStateToEmber)
        Me.gbPlaycount.Controls.Add(Me.btnPlaycountGetList_TVShows)
        Me.gbPlaycount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbPlaycount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbPlaycount.Location = New System.Drawing.Point(0, 0)
        Me.gbPlaycount.Name = "gbPlaycount"
        Me.gbPlaycount.Size = New System.Drawing.Size(1093, 483)
        Me.gbPlaycount.TabIndex = 41
        Me.gbPlaycount.TabStop = False
        Me.gbPlaycount.Text = "Sync Playcount"
        '
        'btnPlaycountSyncRating
        '
        Me.btnPlaycountSyncRating.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnPlaycountSyncRating.Enabled = False
        Me.btnPlaycountSyncRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlaycountSyncRating.Location = New System.Drawing.Point(889, 45)
        Me.btnPlaycountSyncRating.Name = "btnPlaycountSyncRating"
        Me.btnPlaycountSyncRating.Size = New System.Drawing.Size(159, 44)
        Me.btnPlaycountSyncRating.TabIndex = 42
        Me.btnPlaycountSyncRating.Text = "Submit rating to trakt.tv"
        Me.btnPlaycountSyncRating.UseVisualStyleBackColor = False
        '
        'btnPlaycountSyncDeleteItem
        '
        Me.btnPlaycountSyncDeleteItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnPlaycountSyncDeleteItem.Enabled = False
        Me.btnPlaycountSyncDeleteItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlaycountSyncDeleteItem.Location = New System.Drawing.Point(889, 104)
        Me.btnPlaycountSyncDeleteItem.Name = "btnPlaycountSyncDeleteItem"
        Me.btnPlaycountSyncDeleteItem.Size = New System.Drawing.Size(159, 44)
        Me.btnPlaycountSyncDeleteItem.TabIndex = 43
        Me.btnPlaycountSyncDeleteItem.Text = "Delete selected item(s) from trakt.tv history"
        Me.btnPlaycountSyncDeleteItem.UseVisualStyleBackColor = False
        '
        'gbtraktPlaycountSync
        '
        Me.gbtraktPlaycountSync.Controls.Add(Me.btnPlaycountSyncWatched_Movies)
        Me.gbtraktPlaycountSync.Controls.Add(Me.btnPlaycountSyncWatched_TVShows)
        Me.gbtraktPlaycountSync.Location = New System.Drawing.Point(6, 302)
        Me.gbtraktPlaycountSync.Name = "gbtraktPlaycountSync"
        Me.gbtraktPlaycountSync.Size = New System.Drawing.Size(223, 175)
        Me.gbtraktPlaycountSync.TabIndex = 45
        Me.gbtraktPlaycountSync.TabStop = False
        Me.gbtraktPlaycountSync.Text = "Ember  -> trakt.tv"
        '
        'btnPlaycountSyncWatched_Movies
        '
        Me.btnPlaycountSyncWatched_Movies.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnPlaycountSyncWatched_Movies.Enabled = False
        Me.btnPlaycountSyncWatched_Movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlaycountSyncWatched_Movies.Location = New System.Drawing.Point(30, 21)
        Me.btnPlaycountSyncWatched_Movies.Name = "btnPlaycountSyncWatched_Movies"
        Me.btnPlaycountSyncWatched_Movies.Size = New System.Drawing.Size(159, 66)
        Me.btnPlaycountSyncWatched_Movies.TabIndex = 43
        Me.btnPlaycountSyncWatched_Movies.Text = "Submit watched movies to trakt.tv history"
        Me.btnPlaycountSyncWatched_Movies.UseVisualStyleBackColor = False
        '
        'btnPlaycountSyncWatched_TVShows
        '
        Me.btnPlaycountSyncWatched_TVShows.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnPlaycountSyncWatched_TVShows.Enabled = False
        Me.btnPlaycountSyncWatched_TVShows.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlaycountSyncWatched_TVShows.Location = New System.Drawing.Point(30, 103)
        Me.btnPlaycountSyncWatched_TVShows.Name = "btnPlaycountSyncWatched_TVShows"
        Me.btnPlaycountSyncWatched_TVShows.Size = New System.Drawing.Size(159, 66)
        Me.btnPlaycountSyncWatched_TVShows.TabIndex = 44
        Me.btnPlaycountSyncWatched_TVShows.Text = "Submit watched episodes to trakt.tv history"
        Me.btnPlaycountSyncWatched_TVShows.UseVisualStyleBackColor = False
        '
        'btnPlaycountGetList_Movies
        '
        Me.btnPlaycountGetList_Movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlaycountGetList_Movies.Location = New System.Drawing.Point(6, 23)
        Me.btnPlaycountGetList_Movies.Name = "btnPlaycountGetList_Movies"
        Me.btnPlaycountGetList_Movies.Size = New System.Drawing.Size(105, 66)
        Me.btnPlaycountGetList_Movies.TabIndex = 4
        Me.btnPlaycountGetList_Movies.Text = "Get watched movies"
        Me.btnPlaycountGetList_Movies.UseVisualStyleBackColor = True
        '
        'dgvPlaycount
        '
        Me.dgvPlaycount.AllowUserToAddRows = False
        Me.dgvPlaycount.AllowUserToDeleteRows = False
        Me.dgvPlaycount.AllowUserToResizeColumns = False
        Me.dgvPlaycount.AllowUserToResizeRows = False
        Me.dgvPlaycount.BackgroundColor = System.Drawing.Color.White
        Me.dgvPlaycount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPlaycount.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPlaycountTraktID, Me.colPlaycountTitle, Me.colPlaycountPlayed, Me.colPlaycountLastWatched, Me.colPlaycountProgress, Me.colPlaycountRating})
        Me.dgvPlaycount.Location = New System.Drawing.Point(258, 23)
        Me.dgvPlaycount.Name = "dgvPlaycount"
        Me.dgvPlaycount.RowHeadersVisible = False
        Me.dgvPlaycount.RowHeadersWidth = 175
        Me.dgvPlaycount.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvPlaycount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvPlaycount.ShowCellErrors = False
        Me.dgvPlaycount.ShowCellToolTips = False
        Me.dgvPlaycount.ShowRowErrors = False
        Me.dgvPlaycount.Size = New System.Drawing.Size(612, 454)
        Me.dgvPlaycount.TabIndex = 32
        '
        'colPlaycountTraktID
        '
        Me.colPlaycountTraktID.Frozen = True
        Me.colPlaycountTraktID.HeaderText = "TraktID"
        Me.colPlaycountTraktID.Name = "colPlaycountTraktID"
        Me.colPlaycountTraktID.ReadOnly = True
        Me.colPlaycountTraktID.Visible = False
        '
        'colPlaycountTitle
        '
        Me.colPlaycountTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPlaycountTitle.HeaderText = "Title"
        Me.colPlaycountTitle.Name = "colPlaycountTitle"
        Me.colPlaycountTitle.ReadOnly = True
        '
        'colPlaycountPlayed
        '
        Me.colPlaycountPlayed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colPlaycountPlayed.DefaultCellStyle = DataGridViewCellStyle1
        Me.colPlaycountPlayed.HeaderText = "Played"
        Me.colPlaycountPlayed.Name = "colPlaycountPlayed"
        Me.colPlaycountPlayed.ReadOnly = True
        Me.colPlaycountPlayed.Width = 67
        '
        'colPlaycountLastWatched
        '
        Me.colPlaycountLastWatched.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colPlaycountLastWatched.HeaderText = "Last watched"
        Me.colPlaycountLastWatched.Name = "colPlaycountLastWatched"
        Me.colPlaycountLastWatched.ReadOnly = True
        '
        'colPlaycountProgress
        '
        Me.colPlaycountProgress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colPlaycountProgress.DefaultCellStyle = DataGridViewCellStyle2
        Me.colPlaycountProgress.HeaderText = "Progress"
        Me.colPlaycountProgress.Name = "colPlaycountProgress"
        Me.colPlaycountProgress.ReadOnly = True
        Me.colPlaycountProgress.Width = 77
        '
        'colPlaycountRating
        '
        Me.colPlaycountRating.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colPlaycountRating.DefaultCellStyle = DataGridViewCellStyle3
        Me.colPlaycountRating.HeaderText = "Rating"
        Me.colPlaycountRating.Name = "colPlaycountRating"
        Me.colPlaycountRating.Width = 66
        '
        'lblPlaycountDone
        '
        Me.lblPlaycountDone.AutoSize = True
        Me.lblPlaycountDone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlaycountDone.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblPlaycountDone.Location = New System.Drawing.Point(184, 171)
        Me.lblPlaycountDone.Name = "lblPlaycountDone"
        Me.lblPlaycountDone.Size = New System.Drawing.Size(45, 15)
        Me.lblPlaycountDone.TabIndex = 35
        Me.lblPlaycountDone.Text = "Done!"
        Me.lblPlaycountDone.Visible = False
        '
        'prgPlaycount
        '
        Me.prgPlaycount.Location = New System.Drawing.Point(6, 145)
        Me.prgPlaycount.Name = "prgPlaycount"
        Me.prgPlaycount.Size = New System.Drawing.Size(223, 23)
        Me.prgPlaycount.TabIndex = 34
        '
        'lblPlaycountMessage
        '
        Me.lblPlaycountMessage.AutoSize = True
        Me.lblPlaycountMessage.Location = New System.Drawing.Point(8, 171)
        Me.lblPlaycountMessage.Name = "lblPlaycountMessage"
        Me.lblPlaycountMessage.Size = New System.Drawing.Size(11, 13)
        Me.lblPlaycountMessage.TabIndex = 40
        Me.lblPlaycountMessage.Text = "-"
        '
        'btnSaveWatchedStateToEmber
        '
        Me.btnSaveWatchedStateToEmber.Enabled = False
        Me.btnSaveWatchedStateToEmber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveWatchedStateToEmber.Location = New System.Drawing.Point(6, 95)
        Me.btnSaveWatchedStateToEmber.Name = "btnSaveWatchedStateToEmber"
        Me.btnSaveWatchedStateToEmber.Size = New System.Drawing.Size(223, 44)
        Me.btnSaveWatchedStateToEmber.TabIndex = 33
        Me.btnSaveWatchedStateToEmber.Text = "Save playcount to database/Nfo"
        Me.btnSaveWatchedStateToEmber.UseVisualStyleBackColor = True
        '
        'btnPlaycountGetList_TVShows
        '
        Me.btnPlaycountGetList_TVShows.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlaycountGetList_TVShows.Location = New System.Drawing.Point(124, 23)
        Me.btnPlaycountGetList_TVShows.Name = "btnPlaycountGetList_TVShows"
        Me.btnPlaycountGetList_TVShows.Size = New System.Drawing.Size(105, 66)
        Me.btnPlaycountGetList_TVShows.TabIndex = 39
        Me.btnPlaycountGetList_TVShows.Text = "Get watched episodes"
        Me.btnPlaycountGetList_TVShows.UseVisualStyleBackColor = True
        '
        'tbptraktWatchlist
        '
        Me.tbptraktWatchlist.Controls.Add(Me.pnltraktWatchlist)
        Me.tbptraktWatchlist.Location = New System.Drawing.Point(4, 27)
        Me.tbptraktWatchlist.Name = "tbptraktWatchlist"
        Me.tbptraktWatchlist.Padding = New System.Windows.Forms.Padding(3)
        Me.tbptraktWatchlist.Size = New System.Drawing.Size(1099, 489)
        Me.tbptraktWatchlist.TabIndex = 3
        Me.tbptraktWatchlist.Text = "Watchlist"
        Me.tbptraktWatchlist.UseVisualStyleBackColor = True
        '
        'pnltraktWatchlist
        '
        Me.pnltraktWatchlist.Controls.Add(Me.gbtraktWatchlist)
        Me.pnltraktWatchlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltraktWatchlist.Location = New System.Drawing.Point(3, 3)
        Me.pnltraktWatchlist.Name = "pnltraktWatchlist"
        Me.pnltraktWatchlist.Size = New System.Drawing.Size(1093, 483)
        Me.pnltraktWatchlist.TabIndex = 42
        '
        'gbtraktWatchlist
        '
        Me.gbtraktWatchlist.Controls.Add(Me.gbtraktWatchlistExpert)
        Me.gbtraktWatchlist.Controls.Add(Me.btntraktWatchlistGetMovies)
        Me.gbtraktWatchlist.Controls.Add(Me.dgvtraktWatchlist)
        Me.gbtraktWatchlist.Controls.Add(Me.lbltraktWatchliststate)
        Me.gbtraktWatchlist.Controls.Add(Me.prgtraktWatchlist)
        Me.gbtraktWatchlist.Controls.Add(Me.lbltraktWatchlisthelp)
        Me.gbtraktWatchlist.Controls.Add(Me.btntraktWatchlistSyncLibrary)
        Me.gbtraktWatchlist.Controls.Add(Me.btntraktWatchlistGetSeries)
        Me.gbtraktWatchlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbtraktWatchlist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktWatchlist.Location = New System.Drawing.Point(0, 0)
        Me.gbtraktWatchlist.Name = "gbtraktWatchlist"
        Me.gbtraktWatchlist.Size = New System.Drawing.Size(1093, 483)
        Me.gbtraktWatchlist.TabIndex = 41
        Me.gbtraktWatchlist.TabStop = False
        Me.gbtraktWatchlist.Text = "Sync Watchlist"
        '
        'gbtraktWatchlistExpert
        '
        Me.gbtraktWatchlistExpert.Controls.Add(Me.btntraktWatchlistClean)
        Me.gbtraktWatchlistExpert.Controls.Add(Me.btntraktWatchlistSendEmberUnwatched)
        Me.gbtraktWatchlistExpert.Location = New System.Drawing.Point(6, 290)
        Me.gbtraktWatchlistExpert.Name = "gbtraktWatchlistExpert"
        Me.gbtraktWatchlistExpert.Size = New System.Drawing.Size(241, 133)
        Me.gbtraktWatchlistExpert.TabIndex = 43
        Me.gbtraktWatchlistExpert.TabStop = False
        Me.gbtraktWatchlistExpert.Text = "Advanced Options"
        '
        'btntraktWatchlistClean
        '
        Me.btntraktWatchlistClean.Enabled = False
        Me.btntraktWatchlistClean.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktWatchlistClean.Location = New System.Drawing.Point(6, 21)
        Me.btntraktWatchlistClean.Name = "btntraktWatchlistClean"
        Me.btntraktWatchlistClean.Size = New System.Drawing.Size(223, 44)
        Me.btntraktWatchlistClean.TabIndex = 42
        Me.btntraktWatchlistClean.Text = "Clear trakt.tv watchlist"
        Me.btntraktWatchlistClean.UseVisualStyleBackColor = True
        '
        'btntraktWatchlistSendEmberUnwatched
        '
        Me.btntraktWatchlistSendEmberUnwatched.Enabled = False
        Me.btntraktWatchlistSendEmberUnwatched.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktWatchlistSendEmberUnwatched.Location = New System.Drawing.Point(5, 83)
        Me.btntraktWatchlistSendEmberUnwatched.Name = "btntraktWatchlistSendEmberUnwatched"
        Me.btntraktWatchlistSendEmberUnwatched.Size = New System.Drawing.Size(223, 44)
        Me.btntraktWatchlistSendEmberUnwatched.TabIndex = 41
        Me.btntraktWatchlistSendEmberUnwatched.Text = "Send unwatched movies to trakt.tv watchlist"
        Me.btntraktWatchlistSendEmberUnwatched.UseVisualStyleBackColor = True
        '
        'btntraktWatchlistGetMovies
        '
        Me.btntraktWatchlistGetMovies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktWatchlistGetMovies.Location = New System.Drawing.Point(6, 23)
        Me.btntraktWatchlistGetMovies.Name = "btntraktWatchlistGetMovies"
        Me.btntraktWatchlistGetMovies.Size = New System.Drawing.Size(105, 66)
        Me.btntraktWatchlistGetMovies.TabIndex = 4
        Me.btntraktWatchlistGetMovies.Text = "Get movies from trakt.tv watchlist"
        Me.btntraktWatchlistGetMovies.UseVisualStyleBackColor = True
        '
        'dgvtraktWatchlist
        '
        Me.dgvtraktWatchlist.AllowUserToAddRows = False
        Me.dgvtraktWatchlist.AllowUserToDeleteRows = False
        Me.dgvtraktWatchlist.AllowUserToResizeColumns = False
        Me.dgvtraktWatchlist.AllowUserToResizeRows = False
        Me.dgvtraktWatchlist.BackgroundColor = System.Drawing.Color.White
        Me.dgvtraktWatchlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvtraktWatchlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.coltraktWatchlistTitle, Me.coltraktWatchlistYear, Me.coltraktWatchlistListedAt, Me.coltraktWatchlistIMDB})
        Me.dgvtraktWatchlist.Location = New System.Drawing.Point(258, 23)
        Me.dgvtraktWatchlist.MultiSelect = False
        Me.dgvtraktWatchlist.Name = "dgvtraktWatchlist"
        Me.dgvtraktWatchlist.RowHeadersVisible = False
        Me.dgvtraktWatchlist.RowHeadersWidth = 175
        Me.dgvtraktWatchlist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvtraktWatchlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvtraktWatchlist.ShowCellErrors = False
        Me.dgvtraktWatchlist.ShowCellToolTips = False
        Me.dgvtraktWatchlist.ShowRowErrors = False
        Me.dgvtraktWatchlist.Size = New System.Drawing.Size(746, 454)
        Me.dgvtraktWatchlist.TabIndex = 32
        '
        'coltraktWatchlistTitle
        '
        Me.coltraktWatchlistTitle.Frozen = True
        Me.coltraktWatchlistTitle.HeaderText = "Title"
        Me.coltraktWatchlistTitle.Name = "coltraktWatchlistTitle"
        Me.coltraktWatchlistTitle.ReadOnly = True
        Me.coltraktWatchlistTitle.Width = 250
        '
        'coltraktWatchlistYear
        '
        Me.coltraktWatchlistYear.Frozen = True
        Me.coltraktWatchlistYear.HeaderText = "Year"
        Me.coltraktWatchlistYear.Name = "coltraktWatchlistYear"
        Me.coltraktWatchlistYear.ReadOnly = True
        '
        'coltraktWatchlistListedAt
        '
        Me.coltraktWatchlistListedAt.Frozen = True
        Me.coltraktWatchlistListedAt.HeaderText = "Listed At"
        Me.coltraktWatchlistListedAt.Name = "coltraktWatchlistListedAt"
        Me.coltraktWatchlistListedAt.ReadOnly = True
        Me.coltraktWatchlistListedAt.Width = 130
        '
        'coltraktWatchlistIMDB
        '
        Me.coltraktWatchlistIMDB.Frozen = True
        Me.coltraktWatchlistIMDB.HeaderText = "IMDB"
        Me.coltraktWatchlistIMDB.Name = "coltraktWatchlistIMDB"
        Me.coltraktWatchlistIMDB.ReadOnly = True
        Me.coltraktWatchlistIMDB.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.coltraktWatchlistIMDB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.coltraktWatchlistIMDB.Width = 264
        '
        'lbltraktWatchliststate
        '
        Me.lbltraktWatchliststate.AutoSize = True
        Me.lbltraktWatchliststate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktWatchliststate.ForeColor = System.Drawing.Color.SteelBlue
        Me.lbltraktWatchliststate.Location = New System.Drawing.Point(184, 171)
        Me.lbltraktWatchliststate.Name = "lbltraktWatchliststate"
        Me.lbltraktWatchliststate.Size = New System.Drawing.Size(45, 15)
        Me.lbltraktWatchliststate.TabIndex = 35
        Me.lbltraktWatchliststate.Text = "Done!"
        Me.lbltraktWatchliststate.Visible = False
        '
        'prgtraktWatchlist
        '
        Me.prgtraktWatchlist.Location = New System.Drawing.Point(6, 145)
        Me.prgtraktWatchlist.Name = "prgtraktWatchlist"
        Me.prgtraktWatchlist.Size = New System.Drawing.Size(223, 23)
        Me.prgtraktWatchlist.TabIndex = 34
        '
        'lbltraktWatchlisthelp
        '
        Me.lbltraktWatchlisthelp.AutoSize = True
        Me.lbltraktWatchlisthelp.Location = New System.Drawing.Point(8, 171)
        Me.lbltraktWatchlisthelp.Name = "lbltraktWatchlisthelp"
        Me.lbltraktWatchlisthelp.Size = New System.Drawing.Size(11, 13)
        Me.lbltraktWatchlisthelp.TabIndex = 40
        Me.lbltraktWatchlisthelp.Text = "-"
        '
        'btntraktWatchlistSyncLibrary
        '
        Me.btntraktWatchlistSyncLibrary.Enabled = False
        Me.btntraktWatchlistSyncLibrary.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktWatchlistSyncLibrary.Location = New System.Drawing.Point(6, 95)
        Me.btntraktWatchlistSyncLibrary.Name = "btntraktWatchlistSyncLibrary"
        Me.btntraktWatchlistSyncLibrary.Size = New System.Drawing.Size(223, 44)
        Me.btntraktWatchlistSyncLibrary.TabIndex = 33
        Me.btntraktWatchlistSyncLibrary.Text = "Remove watched movies from trakt.tv watchlist"
        Me.btntraktWatchlistSyncLibrary.UseVisualStyleBackColor = True
        '
        'btntraktWatchlistGetSeries
        '
        Me.btntraktWatchlistGetSeries.Enabled = False
        Me.btntraktWatchlistGetSeries.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktWatchlistGetSeries.Location = New System.Drawing.Point(124, 23)
        Me.btntraktWatchlistGetSeries.Name = "btntraktWatchlistGetSeries"
        Me.btntraktWatchlistGetSeries.Size = New System.Drawing.Size(105, 66)
        Me.btntraktWatchlistGetSeries.TabIndex = 39
        Me.btntraktWatchlistGetSeries.Text = "Get episodes from trakt.tv watchlist"
        Me.btntraktWatchlistGetSeries.UseVisualStyleBackColor = True
        '
        'tbptraktListsSync
        '
        Me.tbptraktListsSync.Controls.Add(Me.pnltraktLists)
        Me.tbptraktListsSync.Location = New System.Drawing.Point(4, 27)
        Me.tbptraktListsSync.Name = "tbptraktListsSync"
        Me.tbptraktListsSync.Padding = New System.Windows.Forms.Padding(3)
        Me.tbptraktListsSync.Size = New System.Drawing.Size(1099, 489)
        Me.tbptraktListsSync.TabIndex = 1
        Me.tbptraktListsSync.Text = "Sync Lists/Tags"
        Me.tbptraktListsSync.UseVisualStyleBackColor = True
        '
        'pnltraktLists
        '
        Me.pnltraktLists.Controls.Add(Me.lbltraktListsLink)
        Me.pnltraktLists.Controls.Add(Me.gbtraktListsSYNC)
        Me.pnltraktLists.Controls.Add(Me.gbtraktListsGET)
        Me.pnltraktLists.Controls.Add(Me.lbltraktListsstate)
        Me.pnltraktLists.Controls.Add(Me.gbtraktLists)
        Me.pnltraktLists.Controls.Add(Me.prgtraktLists)
        Me.pnltraktLists.Controls.Add(Me.gbtraktListsMovies)
        Me.pnltraktLists.Controls.Add(Me.gbtraktListsMoviesInLists)
        Me.pnltraktLists.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltraktLists.Location = New System.Drawing.Point(3, 3)
        Me.pnltraktLists.Name = "pnltraktLists"
        Me.pnltraktLists.Size = New System.Drawing.Size(1093, 483)
        Me.pnltraktLists.TabIndex = 1
        '
        'lbltraktListsLink
        '
        Me.lbltraktListsLink.AutoSize = True
        Me.lbltraktListsLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListsLink.Location = New System.Drawing.Point(20, 21)
        Me.lbltraktListsLink.Name = "lbltraktListsLink"
        Me.lbltraktListsLink.Size = New System.Drawing.Size(118, 13)
        Me.lbltraktListsLink.TabIndex = 44
        Me.lbltraktListsLink.TabStop = True
        Me.lbltraktListsLink.Text = "Your trakt.tv dashboard"
        '
        'gbtraktListsSYNC
        '
        Me.gbtraktListsSYNC.Controls.Add(Me.lbltraktListsNoticeSync)
        Me.gbtraktListsSYNC.Controls.Add(Me.btntraktListsSaveToDatabase)
        Me.gbtraktListsSYNC.Controls.Add(Me.btntraktListsSyncTrakt)
        Me.gbtraktListsSYNC.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktListsSYNC.Location = New System.Drawing.Point(17, 141)
        Me.gbtraktListsSYNC.Name = "gbtraktListsSYNC"
        Me.gbtraktListsSYNC.Size = New System.Drawing.Size(148, 235)
        Me.gbtraktListsSYNC.TabIndex = 43
        Me.gbtraktListsSYNC.TabStop = False
        Me.gbtraktListsSYNC.Text = "Save personal lists"
        '
        'lbltraktListsNoticeSync
        '
        Me.lbltraktListsNoticeSync.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListsNoticeSync.Location = New System.Drawing.Point(6, 18)
        Me.lbltraktListsNoticeSync.Name = "lbltraktListsNoticeSync"
        Me.lbltraktListsNoticeSync.Size = New System.Drawing.Size(133, 90)
        Me.lbltraktListsNoticeSync.TabIndex = 49
        Me.lbltraktListsNoticeSync.Text = "Edited existing list(s) will be saved with prefix ""NEWLIST_"". Please change name " &
    "of list in dashboard!"
        '
        'btntraktListsSaveToDatabase
        '
        Me.btntraktListsSaveToDatabase.Enabled = False
        Me.btntraktListsSaveToDatabase.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsSaveToDatabase.Location = New System.Drawing.Point(6, 183)
        Me.btntraktListsSaveToDatabase.Name = "btntraktListsSaveToDatabase"
        Me.btntraktListsSaveToDatabase.Size = New System.Drawing.Size(133, 46)
        Me.btntraktListsSaveToDatabase.TabIndex = 41
        Me.btntraktListsSaveToDatabase.Text = "Save tag to database/Nfo"
        Me.btntraktListsSaveToDatabase.UseVisualStyleBackColor = True
        '
        'btntraktListsSyncTrakt
        '
        Me.btntraktListsSyncTrakt.Enabled = False
        Me.btntraktListsSyncTrakt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsSyncTrakt.Location = New System.Drawing.Point(6, 111)
        Me.btntraktListsSyncTrakt.Name = "btntraktListsSyncTrakt"
        Me.btntraktListsSyncTrakt.Size = New System.Drawing.Size(133, 46)
        Me.btntraktListsSyncTrakt.TabIndex = 37
        Me.btntraktListsSyncTrakt.Text = "Sync to trakt.tv"
        Me.btntraktListsSyncTrakt.UseVisualStyleBackColor = True
        '
        'gbtraktListsGET
        '
        Me.gbtraktListsGET.Controls.Add(Me.btntraktListsGetPersonal)
        Me.gbtraktListsGET.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktListsGET.Location = New System.Drawing.Point(17, 51)
        Me.gbtraktListsGET.Name = "gbtraktListsGET"
        Me.gbtraktListsGET.Size = New System.Drawing.Size(148, 84)
        Me.gbtraktListsGET.TabIndex = 42
        Me.gbtraktListsGET.TabStop = False
        Me.gbtraktListsGET.Text = "Load personal lists"
        '
        'btntraktListsGetPersonal
        '
        Me.btntraktListsGetPersonal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsGetPersonal.Location = New System.Drawing.Point(6, 23)
        Me.btntraktListsGetPersonal.Name = "btntraktListsGetPersonal"
        Me.btntraktListsGetPersonal.Size = New System.Drawing.Size(133, 46)
        Me.btntraktListsGetPersonal.TabIndex = 36
        Me.btntraktListsGetPersonal.Text = "Load trakt.tv lists"
        Me.btntraktListsGetPersonal.UseVisualStyleBackColor = True
        '
        'lbltraktListsstate
        '
        Me.lbltraktListsstate.AutoSize = True
        Me.lbltraktListsstate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListsstate.ForeColor = System.Drawing.Color.SteelBlue
        Me.lbltraktListsstate.Location = New System.Drawing.Point(120, 412)
        Me.lbltraktListsstate.Name = "lbltraktListsstate"
        Me.lbltraktListsstate.Size = New System.Drawing.Size(45, 15)
        Me.lbltraktListsstate.TabIndex = 39
        Me.lbltraktListsstate.Text = "Done!"
        Me.lbltraktListsstate.Visible = False
        '
        'gbtraktLists
        '
        Me.gbtraktLists.Controls.Add(Me.gbtraktListsDetails)
        Me.gbtraktLists.Controls.Add(Me.btntraktListsGetDatabase)
        Me.gbtraktLists.Controls.Add(Me.lbDBLists)
        Me.gbtraktLists.Controls.Add(Me.txttraktListsEditList)
        Me.gbtraktLists.Controls.Add(Me.btntraktListsRemoveList)
        Me.gbtraktLists.Controls.Add(Me.btntraktListsEditList)
        Me.gbtraktLists.Controls.Add(Me.btntraktListsNewList)
        Me.gbtraktLists.Controls.Add(Me.lbtraktLists)
        Me.gbtraktLists.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbtraktLists.Location = New System.Drawing.Point(171, 12)
        Me.gbtraktLists.Name = "gbtraktLists"
        Me.gbtraktLists.Size = New System.Drawing.Size(404, 456)
        Me.gbtraktLists.TabIndex = 5
        Me.gbtraktLists.TabStop = False
        Me.gbtraktLists.Text = "Personal trakt.tv Lists"
        '
        'gbtraktListsDetails
        '
        Me.gbtraktListsDetails.Controls.Add(Me.btntraktListsDetailsUpdate)
        Me.gbtraktListsDetails.Controls.Add(Me.chktraktListsDetailsNumbers)
        Me.gbtraktListsDetails.Controls.Add(Me.chkltraktListsDetailsComments)
        Me.gbtraktListsDetails.Controls.Add(Me.lbltraktListsDetailsDescription)
        Me.gbtraktListsDetails.Controls.Add(Me.lbltraktListsDetailsName)
        Me.gbtraktListsDetails.Controls.Add(Me.lbltraktListsDetailsPrivacy)
        Me.gbtraktListsDetails.Controls.Add(Me.cbotraktListsDetailsPrivacy)
        Me.gbtraktListsDetails.Controls.Add(Me.txttraktListsDetailsName)
        Me.gbtraktListsDetails.Controls.Add(Me.txttraktListsDetailsDescription)
        Me.gbtraktListsDetails.Location = New System.Drawing.Point(217, 20)
        Me.gbtraktListsDetails.Name = "gbtraktListsDetails"
        Me.gbtraktListsDetails.Size = New System.Drawing.Size(181, 428)
        Me.gbtraktListsDetails.TabIndex = 48
        Me.gbtraktListsDetails.TabStop = False
        Me.gbtraktListsDetails.Text = "trakt.tv ListDetails"
        '
        'btntraktListsDetailsUpdate
        '
        Me.btntraktListsDetailsUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsDetailsUpdate.Location = New System.Drawing.Point(3, 352)
        Me.btntraktListsDetailsUpdate.Name = "btntraktListsDetailsUpdate"
        Me.btntraktListsDetailsUpdate.Size = New System.Drawing.Size(175, 35)
        Me.btntraktListsDetailsUpdate.TabIndex = 49
        Me.btntraktListsDetailsUpdate.Text = "Update list"
        Me.btntraktListsDetailsUpdate.UseVisualStyleBackColor = True
        '
        'chktraktListsDetailsNumbers
        '
        Me.chktraktListsDetailsNumbers.AutoSize = True
        Me.chktraktListsDetailsNumbers.Location = New System.Drawing.Point(9, 331)
        Me.chktraktListsDetailsNumbers.Name = "chktraktListsDetailsNumbers"
        Me.chktraktListsDetailsNumbers.Size = New System.Drawing.Size(106, 17)
        Me.chktraktListsDetailsNumbers.TabIndex = 53
        Me.chktraktListsDetailsNumbers.Text = "Show Numbers"
        Me.chktraktListsDetailsNumbers.UseVisualStyleBackColor = True
        '
        'chkltraktListsDetailsComments
        '
        Me.chkltraktListsDetailsComments.AutoSize = True
        Me.chkltraktListsDetailsComments.Location = New System.Drawing.Point(9, 308)
        Me.chkltraktListsDetailsComments.Name = "chkltraktListsDetailsComments"
        Me.chkltraktListsDetailsComments.Size = New System.Drawing.Size(115, 17)
        Me.chkltraktListsDetailsComments.TabIndex = 52
        Me.chkltraktListsDetailsComments.Text = "Allow Comments"
        Me.chkltraktListsDetailsComments.UseVisualStyleBackColor = True
        '
        'lbltraktListsDetailsDescription
        '
        Me.lbltraktListsDetailsDescription.AutoSize = True
        Me.lbltraktListsDetailsDescription.Location = New System.Drawing.Point(6, 60)
        Me.lbltraktListsDetailsDescription.Name = "lbltraktListsDetailsDescription"
        Me.lbltraktListsDetailsDescription.Size = New System.Drawing.Size(66, 13)
        Me.lbltraktListsDetailsDescription.TabIndex = 48
        Me.lbltraktListsDetailsDescription.Text = "Description"
        '
        'lbltraktListsDetailsName
        '
        Me.lbltraktListsDetailsName.AutoSize = True
        Me.lbltraktListsDetailsName.Location = New System.Drawing.Point(6, 19)
        Me.lbltraktListsDetailsName.Name = "lbltraktListsDetailsName"
        Me.lbltraktListsDetailsName.Size = New System.Drawing.Size(38, 13)
        Me.lbltraktListsDetailsName.TabIndex = 44
        Me.lbltraktListsDetailsName.Text = "Name"
        '
        'lbltraktListsDetailsPrivacy
        '
        Me.lbltraktListsDetailsPrivacy.AutoSize = True
        Me.lbltraktListsDetailsPrivacy.Location = New System.Drawing.Point(6, 265)
        Me.lbltraktListsDetailsPrivacy.Name = "lbltraktListsDetailsPrivacy"
        Me.lbltraktListsDetailsPrivacy.Size = New System.Drawing.Size(74, 13)
        Me.lbltraktListsDetailsPrivacy.TabIndex = 45
        Me.lbltraktListsDetailsPrivacy.Text = "Privacy Level"
        '
        'cbotraktListsDetailsPrivacy
        '
        Me.cbotraktListsDetailsPrivacy.FormattingEnabled = True
        Me.cbotraktListsDetailsPrivacy.Location = New System.Drawing.Point(9, 281)
        Me.cbotraktListsDetailsPrivacy.Name = "cbotraktListsDetailsPrivacy"
        Me.cbotraktListsDetailsPrivacy.Size = New System.Drawing.Size(169, 21)
        Me.cbotraktListsDetailsPrivacy.TabIndex = 46
        '
        'txttraktListsDetailsName
        '
        Me.txttraktListsDetailsName.Location = New System.Drawing.Point(3, 35)
        Me.txttraktListsDetailsName.Multiline = True
        Me.txttraktListsDetailsName.Name = "txttraktListsDetailsName"
        Me.txttraktListsDetailsName.ReadOnly = True
        Me.txttraktListsDetailsName.Size = New System.Drawing.Size(172, 22)
        Me.txttraktListsDetailsName.TabIndex = 47
        '
        'txttraktListsDetailsDescription
        '
        Me.txttraktListsDetailsDescription.AcceptsReturn = True
        Me.txttraktListsDetailsDescription.AcceptsTab = True
        Me.txttraktListsDetailsDescription.Location = New System.Drawing.Point(3, 76)
        Me.txttraktListsDetailsDescription.Multiline = True
        Me.txttraktListsDetailsDescription.Name = "txttraktListsDetailsDescription"
        Me.txttraktListsDetailsDescription.Size = New System.Drawing.Size(175, 175)
        Me.txttraktListsDetailsDescription.TabIndex = 43
        '
        'btntraktListsGetDatabase
        '
        Me.btntraktListsGetDatabase.Enabled = False
        Me.btntraktListsGetDatabase.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsGetDatabase.Location = New System.Drawing.Point(6, 411)
        Me.btntraktListsGetDatabase.Name = "btntraktListsGetDatabase"
        Me.btntraktListsGetDatabase.Size = New System.Drawing.Size(205, 37)
        Me.btntraktListsGetDatabase.TabIndex = 42
        Me.btntraktListsGetDatabase.Text = "Add list to trakt.tv"
        Me.btntraktListsGetDatabase.UseVisualStyleBackColor = True
        '
        'lbDBLists
        '
        Me.lbDBLists.Enabled = False
        Me.lbDBLists.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbDBLists.FormattingEnabled = True
        Me.lbDBLists.Location = New System.Drawing.Point(6, 230)
        Me.lbDBLists.Name = "lbDBLists"
        Me.lbDBLists.Size = New System.Drawing.Size(205, 173)
        Me.lbDBLists.Sorted = True
        Me.lbDBLists.TabIndex = 41
        '
        'txttraktListsEditList
        '
        Me.txttraktListsEditList.Enabled = False
        Me.txttraktListsEditList.Location = New System.Drawing.Point(6, 160)
        Me.txttraktListsEditList.Name = "txttraktListsEditList"
        Me.txttraktListsEditList.Size = New System.Drawing.Size(178, 22)
        Me.txttraktListsEditList.TabIndex = 39
        '
        'btntraktListsRemoveList
        '
        Me.btntraktListsRemoveList.Enabled = False
        Me.btntraktListsRemoveList.Image = CType(resources.GetObject("btntraktListsRemoveList.Image"), System.Drawing.Image)
        Me.btntraktListsRemoveList.Location = New System.Drawing.Point(188, 187)
        Me.btntraktListsRemoveList.Name = "btntraktListsRemoveList"
        Me.btntraktListsRemoveList.Size = New System.Drawing.Size(23, 23)
        Me.btntraktListsRemoveList.TabIndex = 3
        Me.btntraktListsRemoveList.UseVisualStyleBackColor = True
        '
        'btntraktListsEditList
        '
        Me.btntraktListsEditList.Enabled = False
        Me.btntraktListsEditList.Image = CType(resources.GetObject("btntraktListsEditList.Image"), System.Drawing.Image)
        Me.btntraktListsEditList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btntraktListsEditList.Location = New System.Drawing.Point(188, 160)
        Me.btntraktListsEditList.Name = "btntraktListsEditList"
        Me.btntraktListsEditList.Size = New System.Drawing.Size(23, 23)
        Me.btntraktListsEditList.TabIndex = 2
        Me.btntraktListsEditList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btntraktListsEditList.UseVisualStyleBackColor = True
        '
        'btntraktListsNewList
        '
        Me.btntraktListsNewList.Enabled = False
        Me.btntraktListsNewList.Image = CType(resources.GetObject("btntraktListsNewList.Image"), System.Drawing.Image)
        Me.btntraktListsNewList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btntraktListsNewList.Location = New System.Drawing.Point(6, 187)
        Me.btntraktListsNewList.Name = "btntraktListsNewList"
        Me.btntraktListsNewList.Size = New System.Drawing.Size(23, 23)
        Me.btntraktListsNewList.TabIndex = 1
        Me.btntraktListsNewList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btntraktListsNewList.UseVisualStyleBackColor = True
        '
        'lbtraktLists
        '
        Me.lbtraktLists.Enabled = False
        Me.lbtraktLists.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbtraktLists.FormattingEnabled = True
        Me.lbtraktLists.Location = New System.Drawing.Point(6, 20)
        Me.lbtraktLists.Name = "lbtraktLists"
        Me.lbtraktLists.Size = New System.Drawing.Size(205, 134)
        Me.lbtraktLists.Sorted = True
        Me.lbtraktLists.TabIndex = 0
        '
        'prgtraktLists
        '
        Me.prgtraktLists.Location = New System.Drawing.Point(17, 386)
        Me.prgtraktLists.Name = "prgtraktLists"
        Me.prgtraktLists.Size = New System.Drawing.Size(148, 23)
        Me.prgtraktLists.TabIndex = 38
        '
        'gbtraktListsMovies
        '
        Me.gbtraktListsMovies.Controls.Add(Me.dgvMovies)
        Me.gbtraktListsMovies.Controls.Add(Me.btntraktListsAddMovie)
        Me.gbtraktListsMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbtraktListsMovies.Location = New System.Drawing.Point(821, 12)
        Me.gbtraktListsMovies.Name = "gbtraktListsMovies"
        Me.gbtraktListsMovies.Size = New System.Drawing.Size(259, 456)
        Me.gbtraktListsMovies.TabIndex = 7
        Me.gbtraktListsMovies.TabStop = False
        Me.gbtraktListsMovies.Text = "available Movies"
        '
        'dgvMovies
        '
        Me.dgvMovies.AllowUserToAddRows = False
        Me.dgvMovies.AllowUserToDeleteRows = False
        Me.dgvMovies.AllowUserToResizeColumns = False
        Me.dgvMovies.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvMovies.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvMovies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMovies.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovies.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovies.Enabled = False
        Me.dgvMovies.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvMovies.Location = New System.Drawing.Point(8, 21)
        Me.dgvMovies.Name = "dgvMovies"
        Me.dgvMovies.ReadOnly = True
        Me.dgvMovies.RowHeadersVisible = False
        Me.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovies.ShowCellErrors = False
        Me.dgvMovies.ShowRowErrors = False
        Me.dgvMovies.Size = New System.Drawing.Size(243, 394)
        Me.dgvMovies.StandardTab = True
        Me.dgvMovies.TabIndex = 51
        '
        'btntraktListsAddMovie
        '
        Me.btntraktListsAddMovie.Enabled = False
        Me.btntraktListsAddMovie.Image = CType(resources.GetObject("btntraktListsAddMovie.Image"), System.Drawing.Image)
        Me.btntraktListsAddMovie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btntraktListsAddMovie.Location = New System.Drawing.Point(8, 425)
        Me.btntraktListsAddMovie.Name = "btntraktListsAddMovie"
        Me.btntraktListsAddMovie.Size = New System.Drawing.Size(23, 23)
        Me.btntraktListsAddMovie.TabIndex = 1
        Me.btntraktListsAddMovie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btntraktListsAddMovie.UseVisualStyleBackColor = True
        '
        'gbtraktListsMoviesInLists
        '
        Me.gbtraktListsMoviesInLists.Controls.Add(Me.lbltraktListsCurrentList)
        Me.gbtraktListsMoviesInLists.Controls.Add(Me.btntraktListsRemove)
        Me.gbtraktListsMoviesInLists.Controls.Add(Me.lbtraktListsMoviesinLists)
        Me.gbtraktListsMoviesInLists.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbtraktListsMoviesInLists.Location = New System.Drawing.Point(581, 12)
        Me.gbtraktListsMoviesInLists.Name = "gbtraktListsMoviesInLists"
        Me.gbtraktListsMoviesInLists.Size = New System.Drawing.Size(234, 456)
        Me.gbtraktListsMoviesInLists.TabIndex = 6
        Me.gbtraktListsMoviesInLists.TabStop = False
        Me.gbtraktListsMoviesInLists.Text = "Movies In List"
        '
        'lbltraktListsCurrentList
        '
        Me.lbltraktListsCurrentList.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbltraktListsCurrentList.Location = New System.Drawing.Point(6, 20)
        Me.lbltraktListsCurrentList.Name = "lbltraktListsCurrentList"
        Me.lbltraktListsCurrentList.Size = New System.Drawing.Size(102, 23)
        Me.lbltraktListsCurrentList.TabIndex = 0
        Me.lbltraktListsCurrentList.Text = "None Selected"
        Me.lbltraktListsCurrentList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btntraktListsRemove
        '
        Me.btntraktListsRemove.Enabled = False
        Me.btntraktListsRemove.Image = CType(resources.GetObject("btntraktListsRemove.Image"), System.Drawing.Image)
        Me.btntraktListsRemove.Location = New System.Drawing.Point(205, 425)
        Me.btntraktListsRemove.Name = "btntraktListsRemove"
        Me.btntraktListsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btntraktListsRemove.TabIndex = 4
        Me.btntraktListsRemove.UseVisualStyleBackColor = True
        '
        'lbtraktListsMoviesinLists
        '
        Me.lbtraktListsMoviesinLists.Enabled = False
        Me.lbtraktListsMoviesinLists.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbtraktListsMoviesinLists.FormattingEnabled = True
        Me.lbtraktListsMoviesinLists.HorizontalScrollbar = True
        Me.lbtraktListsMoviesinLists.Location = New System.Drawing.Point(6, 46)
        Me.lbtraktListsMoviesinLists.Name = "lbtraktListsMoviesinLists"
        Me.lbtraktListsMoviesinLists.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbtraktListsMoviesinLists.Size = New System.Drawing.Size(222, 368)
        Me.lbtraktListsMoviesinLists.TabIndex = 1
        '
        'tbptraktComments
        '
        Me.tbptraktComments.Controls.Add(Me.gbtraktComments)
        Me.tbptraktComments.Location = New System.Drawing.Point(4, 27)
        Me.tbptraktComments.Name = "tbptraktComments"
        Me.tbptraktComments.Padding = New System.Windows.Forms.Padding(3)
        Me.tbptraktComments.Size = New System.Drawing.Size(1099, 489)
        Me.tbptraktComments.TabIndex = 4
        Me.tbptraktComments.Text = "Comments"
        Me.tbptraktComments.UseVisualStyleBackColor = True
        '
        'gbtraktComments
        '
        Me.gbtraktComments.Controls.Add(Me.lbltraktCommentsNotice)
        Me.gbtraktComments.Controls.Add(Me.gbtraktCommentsList)
        Me.gbtraktComments.Controls.Add(Me.gbtraktCommentsGET)
        Me.gbtraktComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbtraktComments.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktComments.Location = New System.Drawing.Point(3, 3)
        Me.gbtraktComments.Name = "gbtraktComments"
        Me.gbtraktComments.Size = New System.Drawing.Size(1093, 483)
        Me.gbtraktComments.TabIndex = 42
        Me.gbtraktComments.TabStop = False
        Me.gbtraktComments.Text = "Sync Comments"
        '
        'lbltraktCommentsNotice
        '
        Me.lbltraktCommentsNotice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktCommentsNotice.ForeColor = System.Drawing.Color.Maroon
        Me.lbltraktCommentsNotice.Location = New System.Drawing.Point(8, 119)
        Me.lbltraktCommentsNotice.Name = "lbltraktCommentsNotice"
        Me.lbltraktCommentsNotice.Size = New System.Drawing.Size(151, 303)
        Me.lbltraktCommentsNotice.TabIndex = 50
        Me.lbltraktCommentsNotice.Text = "Comments must be at least 5 words" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Comments 200 words or longer will be automat" &
    "ically marked as a review" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Correctly indicate if the comment contains spoilers" &
    "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Only write comments in English"
        '
        'gbtraktCommentsList
        '
        Me.gbtraktCommentsList.Controls.Add(Me.dgvtraktComments)
        Me.gbtraktCommentsList.Controls.Add(Me.chktraktCommentsOnlyNoComments)
        Me.gbtraktCommentsList.Controls.Add(Me.chktraktCommentsOnlyComments)
        Me.gbtraktCommentsList.Controls.Add(Me.gbtraktCommentsDetails)
        Me.gbtraktCommentsList.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbtraktCommentsList.Location = New System.Drawing.Point(165, 23)
        Me.gbtraktCommentsList.Name = "gbtraktCommentsList"
        Me.gbtraktCommentsList.Size = New System.Drawing.Size(925, 454)
        Me.gbtraktCommentsList.TabIndex = 46
        Me.gbtraktCommentsList.TabStop = False
        Me.gbtraktCommentsList.Text = "Comments"
        '
        'dgvtraktComments
        '
        Me.dgvtraktComments.AllowUserToAddRows = False
        Me.dgvtraktComments.AllowUserToDeleteRows = False
        Me.dgvtraktComments.AllowUserToResizeColumns = False
        Me.dgvtraktComments.AllowUserToResizeRows = False
        Me.dgvtraktComments.BackgroundColor = System.Drawing.Color.White
        Me.dgvtraktComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvtraktComments.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.coltraktCommentsMovie, Me.coltraktCommentsDate, Me.coltraktCommentsReplies, Me.coltraktCommentsLikes, Me.coltraktCommentsURL, Me.coltraktCommentsImdb})
        Me.dgvtraktComments.Location = New System.Drawing.Point(9, 20)
        Me.dgvtraktComments.MultiSelect = False
        Me.dgvtraktComments.Name = "dgvtraktComments"
        Me.dgvtraktComments.RowHeadersVisible = False
        Me.dgvtraktComments.RowHeadersWidth = 175
        Me.dgvtraktComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvtraktComments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvtraktComments.ShowCellErrors = False
        Me.dgvtraktComments.ShowCellToolTips = False
        Me.dgvtraktComments.ShowRowErrors = False
        Me.dgvtraktComments.Size = New System.Drawing.Size(460, 405)
        Me.dgvtraktComments.TabIndex = 71
        '
        'coltraktCommentsMovie
        '
        Me.coltraktCommentsMovie.Frozen = True
        Me.coltraktCommentsMovie.HeaderText = "Movie"
        Me.coltraktCommentsMovie.Name = "coltraktCommentsMovie"
        Me.coltraktCommentsMovie.ReadOnly = True
        Me.coltraktCommentsMovie.Width = 182
        '
        'coltraktCommentsDate
        '
        Me.coltraktCommentsDate.Frozen = True
        Me.coltraktCommentsDate.HeaderText = "Date"
        Me.coltraktCommentsDate.Name = "coltraktCommentsDate"
        Me.coltraktCommentsDate.ReadOnly = True
        Me.coltraktCommentsDate.Width = 116
        '
        'coltraktCommentsReplies
        '
        Me.coltraktCommentsReplies.Frozen = True
        Me.coltraktCommentsReplies.HeaderText = "Replies"
        Me.coltraktCommentsReplies.Name = "coltraktCommentsReplies"
        Me.coltraktCommentsReplies.ReadOnly = True
        Me.coltraktCommentsReplies.Width = 68
        '
        'coltraktCommentsLikes
        '
        Me.coltraktCommentsLikes.Frozen = True
        Me.coltraktCommentsLikes.HeaderText = "Likes"
        Me.coltraktCommentsLikes.Name = "coltraktCommentsLikes"
        Me.coltraktCommentsLikes.Width = 42
        '
        'coltraktCommentsURL
        '
        Me.coltraktCommentsURL.Frozen = True
        Me.coltraktCommentsURL.HeaderText = "URL"
        Me.coltraktCommentsURL.Name = "coltraktCommentsURL"
        Me.coltraktCommentsURL.ReadOnly = True
        Me.coltraktCommentsURL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.coltraktCommentsURL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.coltraktCommentsURL.Width = 50
        '
        'coltraktCommentsImdb
        '
        Me.coltraktCommentsImdb.Frozen = True
        Me.coltraktCommentsImdb.HeaderText = "Imdb"
        Me.coltraktCommentsImdb.MinimumWidth = 2
        Me.coltraktCommentsImdb.Name = "coltraktCommentsImdb"
        Me.coltraktCommentsImdb.Width = 2
        '
        'chktraktCommentsOnlyNoComments
        '
        Me.chktraktCommentsOnlyNoComments.AutoSize = True
        Me.chktraktCommentsOnlyNoComments.Location = New System.Drawing.Point(9, 431)
        Me.chktraktCommentsOnlyNoComments.Name = "chktraktCommentsOnlyNoComments"
        Me.chktraktCommentsOnlyNoComments.Size = New System.Drawing.Size(190, 17)
        Me.chktraktCommentsOnlyNoComments.TabIndex = 70
        Me.chktraktCommentsOnlyNoComments.Text = "only movies without comments"
        Me.chktraktCommentsOnlyNoComments.UseVisualStyleBackColor = True
        Me.chktraktCommentsOnlyNoComments.Visible = False
        '
        'chktraktCommentsOnlyComments
        '
        Me.chktraktCommentsOnlyComments.AutoSize = True
        Me.chktraktCommentsOnlyComments.Location = New System.Drawing.Point(201, 431)
        Me.chktraktCommentsOnlyComments.Name = "chktraktCommentsOnlyComments"
        Me.chktraktCommentsOnlyComments.Size = New System.Drawing.Size(172, 17)
        Me.chktraktCommentsOnlyComments.TabIndex = 69
        Me.chktraktCommentsOnlyComments.Text = "only movies with comments"
        Me.chktraktCommentsOnlyComments.UseVisualStyleBackColor = True
        Me.chktraktCommentsOnlyComments.Visible = False
        '
        'gbtraktCommentsDetails
        '
        Me.gbtraktCommentsDetails.Controls.Add(Me.btntraktCommentsDetailsSend)
        Me.gbtraktCommentsDetails.Controls.Add(Me.btntraktCommentsDetailsUpdate)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsDate2)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsType2)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsReplies2)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsLikes2)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsRating2)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsRating)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsLikes)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsReplies)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsType)
        Me.gbtraktCommentsDetails.Controls.Add(Me.btntraktCommentsDetailsDelete)
        Me.gbtraktCommentsDetails.Controls.Add(Me.chktraktCommentsDetailsSpoiler)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsDescription)
        Me.gbtraktCommentsDetails.Controls.Add(Me.lbltraktCommentsDetailsDate)
        Me.gbtraktCommentsDetails.Controls.Add(Me.txttraktCommentsDetailsComment)
        Me.gbtraktCommentsDetails.Location = New System.Drawing.Point(475, 20)
        Me.gbtraktCommentsDetails.Name = "gbtraktCommentsDetails"
        Me.gbtraktCommentsDetails.Size = New System.Drawing.Size(444, 405)
        Me.gbtraktCommentsDetails.TabIndex = 48
        Me.gbtraktCommentsDetails.TabStop = False
        Me.gbtraktCommentsDetails.Text = "Details"
        '
        'btntraktCommentsDetailsSend
        '
        Me.btntraktCommentsDetailsSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktCommentsDetailsSend.Location = New System.Drawing.Point(6, 355)
        Me.btntraktCommentsDetailsSend.Name = "btntraktCommentsDetailsSend"
        Me.btntraktCommentsDetailsSend.Size = New System.Drawing.Size(118, 44)
        Me.btntraktCommentsDetailsSend.TabIndex = 68
        Me.btntraktCommentsDetailsSend.Text = "Post comment"
        Me.btntraktCommentsDetailsSend.UseVisualStyleBackColor = True
        '
        'btntraktCommentsDetailsUpdate
        '
        Me.btntraktCommentsDetailsUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktCommentsDetailsUpdate.Location = New System.Drawing.Point(284, 355)
        Me.btntraktCommentsDetailsUpdate.Name = "btntraktCommentsDetailsUpdate"
        Me.btntraktCommentsDetailsUpdate.Size = New System.Drawing.Size(148, 44)
        Me.btntraktCommentsDetailsUpdate.TabIndex = 67
        Me.btntraktCommentsDetailsUpdate.Text = "Update comment"
        Me.btntraktCommentsDetailsUpdate.UseVisualStyleBackColor = True
        '
        'lbltraktCommentsDetailsDate2
        '
        Me.lbltraktCommentsDetailsDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lbltraktCommentsDetailsDate2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltraktCommentsDetailsDate2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbltraktCommentsDetailsDate2.Location = New System.Drawing.Point(6, 33)
        Me.lbltraktCommentsDetailsDate2.Name = "lbltraktCommentsDetailsDate2"
        Me.lbltraktCommentsDetailsDate2.Size = New System.Drawing.Size(83, 20)
        Me.lbltraktCommentsDetailsDate2.TabIndex = 66
        '
        'lbltraktCommentsDetailsType2
        '
        Me.lbltraktCommentsDetailsType2.BackColor = System.Drawing.SystemColors.Control
        Me.lbltraktCommentsDetailsType2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltraktCommentsDetailsType2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbltraktCommentsDetailsType2.Location = New System.Drawing.Point(95, 33)
        Me.lbltraktCommentsDetailsType2.Name = "lbltraktCommentsDetailsType2"
        Me.lbltraktCommentsDetailsType2.Size = New System.Drawing.Size(77, 20)
        Me.lbltraktCommentsDetailsType2.TabIndex = 65
        '
        'lbltraktCommentsDetailsReplies2
        '
        Me.lbltraktCommentsDetailsReplies2.BackColor = System.Drawing.SystemColors.Control
        Me.lbltraktCommentsDetailsReplies2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltraktCommentsDetailsReplies2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbltraktCommentsDetailsReplies2.Location = New System.Drawing.Point(178, 33)
        Me.lbltraktCommentsDetailsReplies2.Name = "lbltraktCommentsDetailsReplies2"
        Me.lbltraktCommentsDetailsReplies2.Size = New System.Drawing.Size(52, 20)
        Me.lbltraktCommentsDetailsReplies2.TabIndex = 64
        '
        'lbltraktCommentsDetailsLikes2
        '
        Me.lbltraktCommentsDetailsLikes2.BackColor = System.Drawing.SystemColors.Control
        Me.lbltraktCommentsDetailsLikes2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltraktCommentsDetailsLikes2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbltraktCommentsDetailsLikes2.Location = New System.Drawing.Point(249, 33)
        Me.lbltraktCommentsDetailsLikes2.Name = "lbltraktCommentsDetailsLikes2"
        Me.lbltraktCommentsDetailsLikes2.Size = New System.Drawing.Size(52, 20)
        Me.lbltraktCommentsDetailsLikes2.TabIndex = 63
        '
        'lbltraktCommentsDetailsRating2
        '
        Me.lbltraktCommentsDetailsRating2.BackColor = System.Drawing.SystemColors.Control
        Me.lbltraktCommentsDetailsRating2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbltraktCommentsDetailsRating2.Location = New System.Drawing.Point(371, 33)
        Me.lbltraktCommentsDetailsRating2.Name = "lbltraktCommentsDetailsRating2"
        Me.lbltraktCommentsDetailsRating2.Size = New System.Drawing.Size(61, 37)
        Me.lbltraktCommentsDetailsRating2.TabIndex = 62
        Me.lbltraktCommentsDetailsRating2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbltraktCommentsDetailsRating
        '
        Me.lbltraktCommentsDetailsRating.AutoSize = True
        Me.lbltraktCommentsDetailsRating.Location = New System.Drawing.Point(371, 15)
        Me.lbltraktCommentsDetailsRating.Name = "lbltraktCommentsDetailsRating"
        Me.lbltraktCommentsDetailsRating.Size = New System.Drawing.Size(61, 13)
        Me.lbltraktCommentsDetailsRating.TabIndex = 60
        Me.lbltraktCommentsDetailsRating.Text = "My Rating"
        '
        'lbltraktCommentsDetailsLikes
        '
        Me.lbltraktCommentsDetailsLikes.AutoSize = True
        Me.lbltraktCommentsDetailsLikes.Location = New System.Drawing.Point(246, 15)
        Me.lbltraktCommentsDetailsLikes.Name = "lbltraktCommentsDetailsLikes"
        Me.lbltraktCommentsDetailsLikes.Size = New System.Drawing.Size(33, 13)
        Me.lbltraktCommentsDetailsLikes.TabIndex = 59
        Me.lbltraktCommentsDetailsLikes.Text = "Likes"
        '
        'lbltraktCommentsDetailsReplies
        '
        Me.lbltraktCommentsDetailsReplies.AutoSize = True
        Me.lbltraktCommentsDetailsReplies.Location = New System.Drawing.Point(175, 15)
        Me.lbltraktCommentsDetailsReplies.Name = "lbltraktCommentsDetailsReplies"
        Me.lbltraktCommentsDetailsReplies.Size = New System.Drawing.Size(44, 13)
        Me.lbltraktCommentsDetailsReplies.TabIndex = 57
        Me.lbltraktCommentsDetailsReplies.Text = "Replies"
        '
        'lbltraktCommentsDetailsType
        '
        Me.lbltraktCommentsDetailsType.AutoSize = True
        Me.lbltraktCommentsDetailsType.Location = New System.Drawing.Point(92, 15)
        Me.lbltraktCommentsDetailsType.Name = "lbltraktCommentsDetailsType"
        Me.lbltraktCommentsDetailsType.Size = New System.Drawing.Size(32, 13)
        Me.lbltraktCommentsDetailsType.TabIndex = 56
        Me.lbltraktCommentsDetailsType.Text = "Type"
        '
        'btntraktCommentsDetailsDelete
        '
        Me.btntraktCommentsDetailsDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktCommentsDetailsDelete.Location = New System.Drawing.Point(130, 355)
        Me.btntraktCommentsDetailsDelete.Name = "btntraktCommentsDetailsDelete"
        Me.btntraktCommentsDetailsDelete.Size = New System.Drawing.Size(148, 44)
        Me.btntraktCommentsDetailsDelete.TabIndex = 49
        Me.btntraktCommentsDetailsDelete.Text = "Delete comment"
        Me.btntraktCommentsDetailsDelete.UseVisualStyleBackColor = True
        '
        'chktraktCommentsDetailsSpoiler
        '
        Me.chktraktCommentsDetailsSpoiler.AutoSize = True
        Me.chktraktCommentsDetailsSpoiler.Location = New System.Drawing.Point(303, 36)
        Me.chktraktCommentsDetailsSpoiler.Name = "chktraktCommentsDetailsSpoiler"
        Me.chktraktCommentsDetailsSpoiler.Size = New System.Drawing.Size(62, 17)
        Me.chktraktCommentsDetailsSpoiler.TabIndex = 53
        Me.chktraktCommentsDetailsSpoiler.Text = "Spoiler"
        Me.chktraktCommentsDetailsSpoiler.UseVisualStyleBackColor = True
        '
        'lbltraktCommentsDetailsDescription
        '
        Me.lbltraktCommentsDetailsDescription.AutoSize = True
        Me.lbltraktCommentsDetailsDescription.Location = New System.Drawing.Point(3, 57)
        Me.lbltraktCommentsDetailsDescription.Name = "lbltraktCommentsDetailsDescription"
        Me.lbltraktCommentsDetailsDescription.Size = New System.Drawing.Size(58, 13)
        Me.lbltraktCommentsDetailsDescription.TabIndex = 48
        Me.lbltraktCommentsDetailsDescription.Text = "Comment"
        '
        'lbltraktCommentsDetailsDate
        '
        Me.lbltraktCommentsDetailsDate.AutoSize = True
        Me.lbltraktCommentsDetailsDate.Location = New System.Drawing.Point(3, 16)
        Me.lbltraktCommentsDetailsDate.Name = "lbltraktCommentsDetailsDate"
        Me.lbltraktCommentsDetailsDate.Size = New System.Drawing.Size(31, 13)
        Me.lbltraktCommentsDetailsDate.TabIndex = 44
        Me.lbltraktCommentsDetailsDate.Text = "Date"
        '
        'txttraktCommentsDetailsComment
        '
        Me.txttraktCommentsDetailsComment.AcceptsReturn = True
        Me.txttraktCommentsDetailsComment.AcceptsTab = True
        Me.txttraktCommentsDetailsComment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txttraktCommentsDetailsComment.Location = New System.Drawing.Point(6, 73)
        Me.txttraktCommentsDetailsComment.Multiline = True
        Me.txttraktCommentsDetailsComment.Name = "txttraktCommentsDetailsComment"
        Me.txttraktCommentsDetailsComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txttraktCommentsDetailsComment.Size = New System.Drawing.Size(426, 276)
        Me.txttraktCommentsDetailsComment.TabIndex = 43
        '
        'gbtraktCommentsGET
        '
        Me.gbtraktCommentsGET.Controls.Add(Me.btntraktCommentsGet)
        Me.gbtraktCommentsGET.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktCommentsGET.Location = New System.Drawing.Point(11, 23)
        Me.gbtraktCommentsGET.Name = "gbtraktCommentsGET"
        Me.gbtraktCommentsGET.Size = New System.Drawing.Size(148, 84)
        Me.gbtraktCommentsGET.TabIndex = 44
        Me.gbtraktCommentsGET.TabStop = False
        Me.gbtraktCommentsGET.Text = "Load comments"
        '
        'btntraktCommentsGet
        '
        Me.btntraktCommentsGet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktCommentsGet.Location = New System.Drawing.Point(6, 23)
        Me.btntraktCommentsGet.Name = "btntraktCommentsGet"
        Me.btntraktCommentsGet.Size = New System.Drawing.Size(133, 46)
        Me.btntraktCommentsGet.TabIndex = 36
        Me.btntraktCommentsGet.Text = "Load your movie comments"
        Me.btntraktCommentsGet.UseVisualStyleBackColor = True
        '
        'tbptraktListViewer
        '
        Me.tbptraktListViewer.Controls.Add(Me.pnltraktListsComparer)
        Me.tbptraktListViewer.Location = New System.Drawing.Point(4, 27)
        Me.tbptraktListViewer.Name = "tbptraktListViewer"
        Me.tbptraktListViewer.Padding = New System.Windows.Forms.Padding(3)
        Me.tbptraktListViewer.Size = New System.Drawing.Size(1099, 489)
        Me.tbptraktListViewer.TabIndex = 2
        Me.tbptraktListViewer.Text = "List viewer"
        Me.tbptraktListViewer.UseVisualStyleBackColor = True
        '
        'pnltraktListsComparer
        '
        Me.pnltraktListsComparer.Controls.Add(Me.gbtraktListsViewer)
        Me.pnltraktListsComparer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltraktListsComparer.Location = New System.Drawing.Point(3, 3)
        Me.pnltraktListsComparer.Name = "pnltraktListsComparer"
        Me.pnltraktListsComparer.Size = New System.Drawing.Size(1093, 483)
        Me.pnltraktListsComparer.TabIndex = 0
        '
        'gbtraktListsViewer
        '
        Me.gbtraktListsViewer.Controls.Add(Me.btntraktListsSendToKodi)
        Me.gbtraktListsViewer.Controls.Add(Me.btntraktListsSavePlaylist)
        Me.gbtraktListsViewer.Controls.Add(Me.cbotraktListsFavorites)
        Me.gbtraktListsViewer.Controls.Add(Me.gbtraktListsViewerStep2)
        Me.gbtraktListsViewer.Controls.Add(Me.lbltraktListsFavorites)
        Me.gbtraktListsViewer.Controls.Add(Me.gbtraktListsViewerStep1)
        Me.gbtraktListsViewer.Controls.Add(Me.lbltraktListDescriptionText)
        Me.gbtraktListsViewer.Controls.Add(Me.lbltraktListDescription)
        Me.gbtraktListsViewer.Controls.Add(Me.lbltraktListsCount)
        Me.gbtraktListsViewer.Controls.Add(Me.chktraktListsCompare)
        Me.gbtraktListsViewer.Controls.Add(Me.btntraktListsSaveList)
        Me.gbtraktListsViewer.Controls.Add(Me.btntraktListsSaveListCompare)
        Me.gbtraktListsViewer.Controls.Add(Me.dgvtraktList)
        Me.gbtraktListsViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbtraktListsViewer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktListsViewer.Location = New System.Drawing.Point(0, 0)
        Me.gbtraktListsViewer.Name = "gbtraktListsViewer"
        Me.gbtraktListsViewer.Size = New System.Drawing.Size(1093, 483)
        Me.gbtraktListsViewer.TabIndex = 43
        Me.gbtraktListsViewer.TabStop = False
        Me.gbtraktListsViewer.Text = "List viewer"
        '
        'btntraktListsSendToKodi
        '
        Me.btntraktListsSendToKodi.Enabled = False
        Me.btntraktListsSendToKodi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsSendToKodi.Location = New System.Drawing.Point(791, 408)
        Me.btntraktListsSendToKodi.Name = "btntraktListsSendToKodi"
        Me.btntraktListsSendToKodi.Size = New System.Drawing.Size(133, 46)
        Me.btntraktListsSendToKodi.TabIndex = 61
        Me.btntraktListsSendToKodi.Text = "Send list to Kodi"
        Me.btntraktListsSendToKodi.UseVisualStyleBackColor = True
        '
        'btntraktListsSavePlaylist
        '
        Me.btntraktListsSavePlaylist.Enabled = False
        Me.btntraktListsSavePlaylist.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsSavePlaylist.Location = New System.Drawing.Point(792, 356)
        Me.btntraktListsSavePlaylist.Name = "btntraktListsSavePlaylist"
        Me.btntraktListsSavePlaylist.Size = New System.Drawing.Size(133, 46)
        Me.btntraktListsSavePlaylist.TabIndex = 60
        Me.btntraktListsSavePlaylist.Text = "Export to Kodi playlist"
        Me.btntraktListsSavePlaylist.UseVisualStyleBackColor = True
        '
        'cbotraktListsFavorites
        '
        Me.cbotraktListsFavorites.FormattingEnabled = True
        Me.cbotraktListsFavorites.Location = New System.Drawing.Point(733, 16)
        Me.cbotraktListsFavorites.Name = "cbotraktListsFavorites"
        Me.cbotraktListsFavorites.Size = New System.Drawing.Size(351, 21)
        Me.cbotraktListsFavorites.TabIndex = 59
        '
        'gbtraktListsViewerStep2
        '
        Me.gbtraktListsViewerStep2.Controls.Add(Me.txttraktListURL)
        Me.gbtraktListsViewerStep2.Controls.Add(Me.btntraktListLoad)
        Me.gbtraktListsViewerStep2.Controls.Add(Me.lbltraktListURL)
        Me.gbtraktListsViewerStep2.Controls.Add(Me.btntraktListSaveFavorite)
        Me.gbtraktListsViewerStep2.Controls.Add(Me.btntraktListRemoveFavorite)
        Me.gbtraktListsViewerStep2.Location = New System.Drawing.Point(644, 154)
        Me.gbtraktListsViewerStep2.Name = "gbtraktListsViewerStep2"
        Me.gbtraktListsViewerStep2.Size = New System.Drawing.Size(440, 89)
        Me.gbtraktListsViewerStep2.TabIndex = 59
        Me.gbtraktListsViewerStep2.TabStop = False
        Me.gbtraktListsViewerStep2.Text = "Step 2: Load selected list or type URL"
        '
        'txttraktListURL
        '
        Me.txttraktListURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttraktListURL.Location = New System.Drawing.Point(51, 25)
        Me.txttraktListURL.Name = "txttraktListURL"
        Me.txttraktListURL.Size = New System.Drawing.Size(280, 20)
        Me.txttraktListURL.TabIndex = 44
        '
        'btntraktListLoad
        '
        Me.btntraktListLoad.Enabled = False
        Me.btntraktListLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListLoad.Location = New System.Drawing.Point(337, 25)
        Me.btntraktListLoad.Name = "btntraktListLoad"
        Me.btntraktListLoad.Size = New System.Drawing.Size(97, 52)
        Me.btntraktListLoad.TabIndex = 36
        Me.btntraktListLoad.Text = "Load list"
        Me.btntraktListLoad.UseVisualStyleBackColor = True
        '
        'lbltraktListURL
        '
        Me.lbltraktListURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListURL.Location = New System.Drawing.Point(6, 28)
        Me.lbltraktListURL.Name = "lbltraktListURL"
        Me.lbltraktListURL.Size = New System.Drawing.Size(39, 17)
        Me.lbltraktListURL.TabIndex = 45
        Me.lbltraktListURL.Text = "Url:"
        Me.lbltraktListURL.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'btntraktListSaveFavorite
        '
        Me.btntraktListSaveFavorite.Enabled = False
        Me.btntraktListSaveFavorite.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListSaveFavorite.Location = New System.Drawing.Point(51, 50)
        Me.btntraktListSaveFavorite.Name = "btntraktListSaveFavorite"
        Me.btntraktListSaveFavorite.Size = New System.Drawing.Size(137, 27)
        Me.btntraktListSaveFavorite.TabIndex = 58
        Me.btntraktListSaveFavorite.Text = "Save list to favorites"
        Me.btntraktListSaveFavorite.UseVisualStyleBackColor = True
        '
        'btntraktListRemoveFavorite
        '
        Me.btntraktListRemoveFavorite.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListRemoveFavorite.Location = New System.Drawing.Point(194, 50)
        Me.btntraktListRemoveFavorite.Name = "btntraktListRemoveFavorite"
        Me.btntraktListRemoveFavorite.Size = New System.Drawing.Size(137, 27)
        Me.btntraktListRemoveFavorite.TabIndex = 56
        Me.btntraktListRemoveFavorite.Text = "Remove list from favorites"
        Me.btntraktListRemoveFavorite.UseVisualStyleBackColor = True
        '
        'lbltraktListsFavorites
        '
        Me.lbltraktListsFavorites.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListsFavorites.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lbltraktListsFavorites.Location = New System.Drawing.Point(644, 15)
        Me.lbltraktListsFavorites.Name = "lbltraktListsFavorites"
        Me.lbltraktListsFavorites.Size = New System.Drawing.Size(90, 28)
        Me.lbltraktListsFavorites.TabIndex = 58
        Me.lbltraktListsFavorites.Text = "Favorite lists:"
        Me.lbltraktListsFavorites.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbtraktListsViewerStep1
        '
        Me.gbtraktListsViewerStep1.Controls.Add(Me.lbltraktListsScraped)
        Me.gbtraktListsViewerStep1.Controls.Add(Me.cbotraktListsScraped)
        Me.gbtraktListsViewerStep1.Controls.Add(Me.btntraktListsGetFollowers)
        Me.gbtraktListsViewerStep1.Controls.Add(Me.btntraktListsGetFriends)
        Me.gbtraktListsViewerStep1.Controls.Add(Me.btntraktListsGetPopular)
        Me.gbtraktListsViewerStep1.Location = New System.Drawing.Point(644, 46)
        Me.gbtraktListsViewerStep1.Name = "gbtraktListsViewerStep1"
        Me.gbtraktListsViewerStep1.Size = New System.Drawing.Size(440, 104)
        Me.gbtraktListsViewerStep1.TabIndex = 57
        Me.gbtraktListsViewerStep1.TabStop = False
        Me.gbtraktListsViewerStep1.Text = "Step 1 (Optional): Load specific lists from trakt.tv"
        '
        'lbltraktListsScraped
        '
        Me.lbltraktListsScraped.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListsScraped.Location = New System.Drawing.Point(8, 74)
        Me.lbltraktListsScraped.Name = "lbltraktListsScraped"
        Me.lbltraktListsScraped.Size = New System.Drawing.Size(133, 21)
        Me.lbltraktListsScraped.TabIndex = 55
        Me.lbltraktListsScraped.Text = "Scraped lists:"
        Me.lbltraktListsScraped.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'cbotraktListsScraped
        '
        Me.cbotraktListsScraped.Enabled = False
        Me.cbotraktListsScraped.FormattingEnabled = True
        Me.cbotraktListsScraped.Location = New System.Drawing.Point(148, 74)
        Me.cbotraktListsScraped.Name = "cbotraktListsScraped"
        Me.cbotraktListsScraped.Size = New System.Drawing.Size(274, 21)
        Me.cbotraktListsScraped.TabIndex = 55
        '
        'btntraktListsGetFollowers
        '
        Me.btntraktListsGetFollowers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsGetFollowers.Location = New System.Drawing.Point(9, 21)
        Me.btntraktListsGetFollowers.Name = "btntraktListsGetFollowers"
        Me.btntraktListsGetFollowers.Size = New System.Drawing.Size(133, 47)
        Me.btntraktListsGetFollowers.TabIndex = 57
        Me.btntraktListsGetFollowers.Text = "Scrape lists of favorite users"
        Me.btntraktListsGetFollowers.UseVisualStyleBackColor = True
        '
        'btntraktListsGetFriends
        '
        Me.btntraktListsGetFriends.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsGetFriends.Location = New System.Drawing.Point(147, 21)
        Me.btntraktListsGetFriends.Name = "btntraktListsGetFriends"
        Me.btntraktListsGetFriends.Size = New System.Drawing.Size(134, 47)
        Me.btntraktListsGetFriends.TabIndex = 55
        Me.btntraktListsGetFriends.Text = "Scrape lists of friends"
        Me.btntraktListsGetFriends.UseVisualStyleBackColor = True
        '
        'btntraktListsGetPopular
        '
        Me.btntraktListsGetPopular.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsGetPopular.Location = New System.Drawing.Point(287, 21)
        Me.btntraktListsGetPopular.Name = "btntraktListsGetPopular"
        Me.btntraktListsGetPopular.Size = New System.Drawing.Size(134, 47)
        Me.btntraktListsGetPopular.TabIndex = 54
        Me.btntraktListsGetPopular.Text = "Scrape popular lists"
        Me.btntraktListsGetPopular.UseVisualStyleBackColor = True
        '
        'lbltraktListDescriptionText
        '
        Me.lbltraktListDescriptionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltraktListDescriptionText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListDescriptionText.ForeColor = System.Drawing.Color.Blue
        Me.lbltraktListDescriptionText.Location = New System.Drawing.Point(644, 264)
        Me.lbltraktListDescriptionText.Name = "lbltraktListDescriptionText"
        Me.lbltraktListDescriptionText.Size = New System.Drawing.Size(440, 58)
        Me.lbltraktListDescriptionText.TabIndex = 55
        Me.lbltraktListDescriptionText.Text = "-"
        '
        'lbltraktListDescription
        '
        Me.lbltraktListDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListDescription.Location = New System.Drawing.Point(641, 246)
        Me.lbltraktListDescription.Name = "lbltraktListDescription"
        Me.lbltraktListDescription.Size = New System.Drawing.Size(75, 18)
        Me.lbltraktListDescription.TabIndex = 56
        Me.lbltraktListDescription.Text = "Description"
        Me.lbltraktListDescription.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lbltraktListsCount
        '
        Me.lbltraktListsCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktListsCount.Location = New System.Drawing.Point(641, 457)
        Me.lbltraktListsCount.Name = "lbltraktListsCount"
        Me.lbltraktListsCount.Size = New System.Drawing.Size(130, 20)
        Me.lbltraktListsCount.TabIndex = 52
        Me.lbltraktListsCount.Text = "Search results"
        Me.lbltraktListsCount.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lbltraktListsCount.Visible = False
        '
        'chktraktListsCompare
        '
        Me.chktraktListsCompare.Enabled = False
        Me.chktraktListsCompare.Location = New System.Drawing.Point(655, 325)
        Me.chktraktListsCompare.Name = "chktraktListsCompare"
        Me.chktraktListsCompare.Size = New System.Drawing.Size(175, 25)
        Me.chktraktListsCompare.TabIndex = 51
        Me.chktraktListsCompare.Text = "only show unknown movies"
        Me.chktraktListsCompare.UseVisualStyleBackColor = True
        '
        'btntraktListsSaveList
        '
        Me.btntraktListsSaveList.Enabled = False
        Me.btntraktListsSaveList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsSaveList.Location = New System.Drawing.Point(653, 408)
        Me.btntraktListsSaveList.Name = "btntraktListsSaveList"
        Me.btntraktListsSaveList.Size = New System.Drawing.Size(132, 46)
        Me.btntraktListsSaveList.TabIndex = 48
        Me.btntraktListsSaveList.Text = "Export complete list"
        Me.btntraktListsSaveList.UseVisualStyleBackColor = True
        '
        'btntraktListsSaveListCompare
        '
        Me.btntraktListsSaveListCompare.Enabled = False
        Me.btntraktListsSaveListCompare.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktListsSaveListCompare.Location = New System.Drawing.Point(653, 356)
        Me.btntraktListsSaveListCompare.Name = "btntraktListsSaveListCompare"
        Me.btntraktListsSaveListCompare.Size = New System.Drawing.Size(133, 46)
        Me.btntraktListsSaveListCompare.TabIndex = 47
        Me.btntraktListsSaveListCompare.Text = "Export unknown movies"
        Me.btntraktListsSaveListCompare.UseVisualStyleBackColor = True
        '
        'dgvtraktList
        '
        Me.dgvtraktList.AllowUserToAddRows = False
        Me.dgvtraktList.AllowUserToDeleteRows = False
        Me.dgvtraktList.AllowUserToResizeColumns = False
        Me.dgvtraktList.AllowUserToResizeRows = False
        Me.dgvtraktList.BackgroundColor = System.Drawing.Color.White
        Me.dgvtraktList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvtraktList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.coltraktListTitle, Me.coltraktListYear, Me.coltraktListRating, Me.coltraktListGenres, Me.coltraktListIMDB, Me.coltraktListTrailer})
        Me.dgvtraktList.Location = New System.Drawing.Point(6, 16)
        Me.dgvtraktList.MultiSelect = False
        Me.dgvtraktList.Name = "dgvtraktList"
        Me.dgvtraktList.RowHeadersVisible = False
        Me.dgvtraktList.RowHeadersWidth = 175
        Me.dgvtraktList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvtraktList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvtraktList.ShowCellErrors = False
        Me.dgvtraktList.ShowCellToolTips = False
        Me.dgvtraktList.ShowRowErrors = False
        Me.dgvtraktList.Size = New System.Drawing.Size(626, 461)
        Me.dgvtraktList.TabIndex = 46
        '
        'coltraktListTitle
        '
        Me.coltraktListTitle.Frozen = True
        Me.coltraktListTitle.HeaderText = "Title"
        Me.coltraktListTitle.Name = "coltraktListTitle"
        Me.coltraktListTitle.ReadOnly = True
        Me.coltraktListTitle.Width = 160
        '
        'coltraktListYear
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.coltraktListYear.DefaultCellStyle = DataGridViewCellStyle5
        Me.coltraktListYear.Frozen = True
        Me.coltraktListYear.HeaderText = "Year"
        Me.coltraktListYear.Name = "coltraktListYear"
        Me.coltraktListYear.ReadOnly = True
        Me.coltraktListYear.Width = 45
        '
        'coltraktListRating
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.NullValue = Nothing
        Me.coltraktListRating.DefaultCellStyle = DataGridViewCellStyle6
        Me.coltraktListRating.HeaderText = "Rating"
        Me.coltraktListRating.Name = "coltraktListRating"
        Me.coltraktListRating.ReadOnly = True
        Me.coltraktListRating.Width = 68
        '
        'coltraktListGenres
        '
        Me.coltraktListGenres.HeaderText = "Genres"
        Me.coltraktListGenres.Name = "coltraktListGenres"
        Me.coltraktListGenres.ReadOnly = True
        Me.coltraktListGenres.Width = 180
        '
        'coltraktListIMDB
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.coltraktListIMDB.DefaultCellStyle = DataGridViewCellStyle7
        Me.coltraktListIMDB.HeaderText = "IMDB"
        Me.coltraktListIMDB.Name = "coltraktListIMDB"
        Me.coltraktListIMDB.ReadOnly = True
        Me.coltraktListIMDB.Text = "IMDB"
        Me.coltraktListIMDB.Width = 70
        '
        'coltraktListTrailer
        '
        Me.coltraktListTrailer.HeaderText = "Trailer"
        Me.coltraktListTrailer.Name = "coltraktListTrailer"
        Me.coltraktListTrailer.ReadOnly = True
        Me.coltraktListTrailer.Text = "Link"
        '
        'tbptraktCleaning
        '
        Me.tbptraktCleaning.Controls.Add(Me.gbtraktCleaning)
        Me.tbptraktCleaning.Location = New System.Drawing.Point(4, 27)
        Me.tbptraktCleaning.Name = "tbptraktCleaning"
        Me.tbptraktCleaning.Padding = New System.Windows.Forms.Padding(3)
        Me.tbptraktCleaning.Size = New System.Drawing.Size(1099, 489)
        Me.tbptraktCleaning.TabIndex = 5
        Me.tbptraktCleaning.Text = "Cleaning"
        Me.tbptraktCleaning.UseVisualStyleBackColor = True
        '
        'gbtraktCleaning
        '
        Me.gbtraktCleaning.Controls.Add(Me.gbtraktCleaningHistoryTimespan)
        Me.gbtraktCleaning.Controls.Add(Me.gbtraktCleaningHistoryTimestamp)
        Me.gbtraktCleaning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbtraktCleaning.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktCleaning.Location = New System.Drawing.Point(3, 3)
        Me.gbtraktCleaning.Name = "gbtraktCleaning"
        Me.gbtraktCleaning.Size = New System.Drawing.Size(1093, 483)
        Me.gbtraktCleaning.TabIndex = 42
        Me.gbtraktCleaning.TabStop = False
        Me.gbtraktCleaning.Text = "Cleaning"
        '
        'gbtraktCleaningHistoryTimespan
        '
        Me.gbtraktCleaningHistoryTimespan.Controls.Add(Me.lbltraktCleaningHistoryTimespanDesc)
        Me.gbtraktCleaningHistoryTimespan.Controls.Add(Me.cbotraktCleaningHistoryTimespan)
        Me.gbtraktCleaningHistoryTimespan.Controls.Add(Me.lbltraktCleaningHistoryTimespan)
        Me.gbtraktCleaningHistoryTimespan.Controls.Add(Me.btntraktCleaningHistoryTimespan)
        Me.gbtraktCleaningHistoryTimespan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktCleaningHistoryTimespan.Location = New System.Drawing.Point(355, 21)
        Me.gbtraktCleaningHistoryTimespan.Name = "gbtraktCleaningHistoryTimespan"
        Me.gbtraktCleaningHistoryTimespan.Size = New System.Drawing.Size(329, 164)
        Me.gbtraktCleaningHistoryTimespan.TabIndex = 55
        Me.gbtraktCleaningHistoryTimespan.TabStop = False
        Me.gbtraktCleaningHistoryTimespan.Text = "Delete history for a specific timespan"
        '
        'lbltraktCleaningHistoryTimespanDesc
        '
        Me.lbltraktCleaningHistoryTimespanDesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktCleaningHistoryTimespanDesc.Location = New System.Drawing.Point(6, 18)
        Me.lbltraktCleaningHistoryTimespanDesc.Name = "lbltraktCleaningHistoryTimespanDesc"
        Me.lbltraktCleaningHistoryTimespanDesc.Size = New System.Drawing.Size(315, 53)
        Me.lbltraktCleaningHistoryTimespanDesc.TabIndex = 49
        Me.lbltraktCleaningHistoryTimespanDesc.Text = "This will remove all plays in your watched movie history which were registered in" &
    " a specific timespan (i.e. 3 plays for one movie within 5 minutes, will delete 2" &
    " movies and keep first)"
        '
        'cbotraktCleaningHistoryTimespan
        '
        Me.cbotraktCleaningHistoryTimespan.FormattingEnabled = True
        Me.cbotraktCleaningHistoryTimespan.Items.AddRange(New Object() {"", "2", "5", "10", "30", "60", "120", "180"})
        Me.cbotraktCleaningHistoryTimespan.Location = New System.Drawing.Point(118, 73)
        Me.cbotraktCleaningHistoryTimespan.Name = "cbotraktCleaningHistoryTimespan"
        Me.cbotraktCleaningHistoryTimespan.Size = New System.Drawing.Size(54, 21)
        Me.cbotraktCleaningHistoryTimespan.TabIndex = 53
        '
        'lbltraktCleaningHistoryTimespan
        '
        Me.lbltraktCleaningHistoryTimespan.AutoSize = True
        Me.lbltraktCleaningHistoryTimespan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktCleaningHistoryTimespan.Location = New System.Drawing.Point(10, 76)
        Me.lbltraktCleaningHistoryTimespan.Name = "lbltraktCleaningHistoryTimespan"
        Me.lbltraktCleaningHistoryTimespan.Size = New System.Drawing.Size(98, 13)
        Me.lbltraktCleaningHistoryTimespan.TabIndex = 52
        Me.lbltraktCleaningHistoryTimespan.Text = "Timespan [minutes]"
        Me.lbltraktCleaningHistoryTimespan.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'btntraktCleaningHistoryTimespan
        '
        Me.btntraktCleaningHistoryTimespan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktCleaningHistoryTimespan.Location = New System.Drawing.Point(6, 102)
        Me.btntraktCleaningHistoryTimespan.Name = "btntraktCleaningHistoryTimespan"
        Me.btntraktCleaningHistoryTimespan.Size = New System.Drawing.Size(312, 46)
        Me.btntraktCleaningHistoryTimespan.TabIndex = 37
        Me.btntraktCleaningHistoryTimespan.Text = "Start cleaning movie history"
        Me.btntraktCleaningHistoryTimespan.UseVisualStyleBackColor = True
        '
        'gbtraktCleaningHistoryTimestamp
        '
        Me.gbtraktCleaningHistoryTimestamp.Controls.Add(Me.lbltraktCleaningHistoryTimestamp)
        Me.gbtraktCleaningHistoryTimestamp.Controls.Add(Me.txttraktCleaningHistoryTimestamp)
        Me.gbtraktCleaningHistoryTimestamp.Controls.Add(Me.lbltraktCleaningHistoryTimestampDesc)
        Me.gbtraktCleaningHistoryTimestamp.Controls.Add(Me.btntraktCleaningHistoryTimestamp)
        Me.gbtraktCleaningHistoryTimestamp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbtraktCleaningHistoryTimestamp.Location = New System.Drawing.Point(6, 21)
        Me.gbtraktCleaningHistoryTimestamp.Name = "gbtraktCleaningHistoryTimestamp"
        Me.gbtraktCleaningHistoryTimestamp.Size = New System.Drawing.Size(329, 164)
        Me.gbtraktCleaningHistoryTimestamp.TabIndex = 44
        Me.gbtraktCleaningHistoryTimestamp.TabStop = False
        Me.gbtraktCleaningHistoryTimestamp.Text = "Delete history from a specific date"
        '
        'lbltraktCleaningHistoryTimestamp
        '
        Me.lbltraktCleaningHistoryTimestamp.AutoSize = True
        Me.lbltraktCleaningHistoryTimestamp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktCleaningHistoryTimestamp.Location = New System.Drawing.Point(6, 77)
        Me.lbltraktCleaningHistoryTimestamp.Name = "lbltraktCleaningHistoryTimestamp"
        Me.lbltraktCleaningHistoryTimestamp.Size = New System.Drawing.Size(111, 13)
        Me.lbltraktCleaningHistoryTimestamp.TabIndex = 54
        Me.lbltraktCleaningHistoryTimestamp.Text = "Timestamp [hh:mm:ss]"
        Me.lbltraktCleaningHistoryTimestamp.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'txttraktCleaningHistoryTimestamp
        '
        Me.txttraktCleaningHistoryTimestamp.Location = New System.Drawing.Point(123, 74)
        Me.txttraktCleaningHistoryTimestamp.Name = "txttraktCleaningHistoryTimestamp"
        Me.txttraktCleaningHistoryTimestamp.Size = New System.Drawing.Size(75, 22)
        Me.txttraktCleaningHistoryTimestamp.TabIndex = 50
        Me.txttraktCleaningHistoryTimestamp.Text = "00:00:00"
        '
        'lbltraktCleaningHistoryTimestampDesc
        '
        Me.lbltraktCleaningHistoryTimestampDesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltraktCleaningHistoryTimestampDesc.Location = New System.Drawing.Point(6, 18)
        Me.lbltraktCleaningHistoryTimestampDesc.Name = "lbltraktCleaningHistoryTimestampDesc"
        Me.lbltraktCleaningHistoryTimestampDesc.Size = New System.Drawing.Size(315, 53)
        Me.lbltraktCleaningHistoryTimestampDesc.TabIndex = 49
        Me.lbltraktCleaningHistoryTimestampDesc.Text = "This will remove all plays in your watched movie history which were played on a s" &
    "pecific time (i.e. ""0:00:00"")"
        '
        'btntraktCleaningHistoryTimestamp
        '
        Me.btntraktCleaningHistoryTimestamp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntraktCleaningHistoryTimestamp.Location = New System.Drawing.Point(6, 102)
        Me.btntraktCleaningHistoryTimestamp.Name = "btntraktCleaningHistoryTimestamp"
        Me.btntraktCleaningHistoryTimestamp.Size = New System.Drawing.Size(312, 46)
        Me.btntraktCleaningHistoryTimestamp.TabIndex = 37
        Me.btntraktCleaningHistoryTimestamp.Text = "Start cleaning movie history"
        Me.btntraktCleaningHistoryTimestamp.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 584)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1107, 38)
        Me.pnlBottom.TabIndex = 17
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 2
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.OK_Button, 0, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.Size = New System.Drawing.Size(1107, 38)
        Me.tblBottom.TabIndex = 0
        '
        'dlgTrakttvManager
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1107, 622)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgTrakttvManager"
        Me.Text = "Trakt.tv Manager"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlSaving.ResumeLayout(False)
        Me.pnlSaving.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.tbTrakt.ResumeLayout(False)
        Me.tbptraktPlaycount.ResumeLayout(False)
        Me.pnltraktPlaycount.ResumeLayout(False)
        Me.gbPlaycount.ResumeLayout(False)
        Me.gbPlaycount.PerformLayout()
        Me.gbtraktPlaycountSync.ResumeLayout(False)
        CType(Me.dgvPlaycount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbptraktWatchlist.ResumeLayout(False)
        Me.pnltraktWatchlist.ResumeLayout(False)
        Me.gbtraktWatchlist.ResumeLayout(False)
        Me.gbtraktWatchlist.PerformLayout()
        Me.gbtraktWatchlistExpert.ResumeLayout(False)
        CType(Me.dgvtraktWatchlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbptraktListsSync.ResumeLayout(False)
        Me.pnltraktLists.ResumeLayout(False)
        Me.pnltraktLists.PerformLayout()
        Me.gbtraktListsSYNC.ResumeLayout(False)
        Me.gbtraktListsGET.ResumeLayout(False)
        Me.gbtraktLists.ResumeLayout(False)
        Me.gbtraktLists.PerformLayout()
        Me.gbtraktListsDetails.ResumeLayout(False)
        Me.gbtraktListsDetails.PerformLayout()
        Me.gbtraktListsMovies.ResumeLayout(False)
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbtraktListsMoviesInLists.ResumeLayout(False)
        Me.tbptraktComments.ResumeLayout(False)
        Me.gbtraktComments.ResumeLayout(False)
        Me.gbtraktCommentsList.ResumeLayout(False)
        Me.gbtraktCommentsList.PerformLayout()
        CType(Me.dgvtraktComments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbtraktCommentsDetails.ResumeLayout(False)
        Me.gbtraktCommentsDetails.PerformLayout()
        Me.gbtraktCommentsGET.ResumeLayout(False)
        Me.tbptraktListViewer.ResumeLayout(False)
        Me.pnltraktListsComparer.ResumeLayout(False)
        Me.gbtraktListsViewer.ResumeLayout(False)
        Me.gbtraktListsViewerStep2.ResumeLayout(False)
        Me.gbtraktListsViewerStep2.PerformLayout()
        Me.gbtraktListsViewerStep1.ResumeLayout(False)
        CType(Me.dgvtraktList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbptraktCleaning.ResumeLayout(False)
        Me.gbtraktCleaning.ResumeLayout(False)
        Me.gbtraktCleaningHistoryTimespan.ResumeLayout(False)
        Me.gbtraktCleaningHistoryTimespan.PerformLayout()
        Me.gbtraktCleaningHistoryTimestamp.ResumeLayout(False)
        Me.gbtraktCleaningHistoryTimestamp.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents tbTrakt As System.Windows.Forms.TabControl
    Friend WithEvents tbptraktPlaycount As System.Windows.Forms.TabPage
    Friend WithEvents pnltraktPlaycount As System.Windows.Forms.Panel
    Friend WithEvents btnPlaycountGetList_Movies As System.Windows.Forms.Button
    Friend WithEvents dgvPlaycount As System.Windows.Forms.DataGridView
    Friend WithEvents lblPlaycountMessage As System.Windows.Forms.Label
    Friend WithEvents btnPlaycountGetList_TVShows As System.Windows.Forms.Button
    Friend WithEvents btnSaveWatchedStateToEmber As System.Windows.Forms.Button
    Friend WithEvents prgPlaycount As System.Windows.Forms.ProgressBar
    Friend WithEvents lblPlaycountDone As System.Windows.Forms.Label
    Friend WithEvents tbptraktListsSync As System.Windows.Forms.TabPage
    Friend WithEvents pnltraktLists As System.Windows.Forms.Panel
    Friend WithEvents gbtraktListsSYNC As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktListsSaveToDatabase As System.Windows.Forms.Button
    Friend WithEvents btntraktListsSyncTrakt As System.Windows.Forms.Button
    Friend WithEvents gbtraktListsGET As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktListsGetPersonal As System.Windows.Forms.Button
    Friend WithEvents lbltraktListsstate As System.Windows.Forms.Label
    Friend WithEvents gbtraktLists As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktListsGetDatabase As System.Windows.Forms.Button
    Friend WithEvents lbDBLists As System.Windows.Forms.ListBox
    Friend WithEvents txttraktListsEditList As System.Windows.Forms.TextBox
    Friend WithEvents btntraktListsRemoveList As System.Windows.Forms.Button
    Friend WithEvents btntraktListsEditList As System.Windows.Forms.Button
    Friend WithEvents btntraktListsNewList As System.Windows.Forms.Button
    Friend WithEvents lbtraktLists As System.Windows.Forms.ListBox
    Friend WithEvents prgtraktLists As System.Windows.Forms.ProgressBar
    Friend WithEvents gbtraktListsMovies As System.Windows.Forms.GroupBox
    Friend WithEvents dgvMovies As System.Windows.Forms.DataGridView
    Friend WithEvents btntraktListsAddMovie As System.Windows.Forms.Button
    Friend WithEvents gbtraktListsMoviesInLists As System.Windows.Forms.GroupBox
    Friend WithEvents lbltraktListsCurrentList As System.Windows.Forms.Label
    Friend WithEvents btntraktListsRemove As System.Windows.Forms.Button
    Friend WithEvents lbtraktListsMoviesinLists As System.Windows.Forms.ListBox
    Friend WithEvents tbptraktListViewer As System.Windows.Forms.TabPage
    Friend WithEvents pnltraktListsComparer As System.Windows.Forms.Panel
    Friend WithEvents gbtraktListsViewer As System.Windows.Forms.GroupBox
    Friend WithEvents lbltraktListDescription As System.Windows.Forms.Label
    Friend WithEvents lbltraktListDescriptionText As System.Windows.Forms.Label
    Friend WithEvents btntraktListsGetPopular As System.Windows.Forms.Button
    Friend WithEvents lbltraktListsCount As System.Windows.Forms.Label
    Friend WithEvents chktraktListsCompare As System.Windows.Forms.CheckBox
    Friend WithEvents btntraktListsSaveList As System.Windows.Forms.Button
    Friend WithEvents btntraktListsSaveListCompare As System.Windows.Forms.Button
    Friend WithEvents dgvtraktList As System.Windows.Forms.DataGridView
    Friend WithEvents btntraktListLoad As System.Windows.Forms.Button
    Friend WithEvents lbltraktListURL As System.Windows.Forms.Label
    Friend WithEvents txttraktListURL As System.Windows.Forms.TextBox
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents pnlSaving As System.Windows.Forms.Panel
    Friend WithEvents lblSaving As System.Windows.Forms.Label
    Friend WithEvents prbSaving As System.Windows.Forms.ProgressBar
    Friend WithEvents prbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbPlaycount As System.Windows.Forms.GroupBox
    Friend WithEvents gbtraktListsDetails As System.Windows.Forms.GroupBox
    Friend WithEvents txttraktListsDetailsName As System.Windows.Forms.TextBox
    Friend WithEvents lbltraktListsDetailsPrivacy As System.Windows.Forms.Label
    Friend WithEvents lbltraktListsDetailsName As System.Windows.Forms.Label
    Friend WithEvents txttraktListsDetailsDescription As System.Windows.Forms.TextBox
    Friend WithEvents lbltraktListsDetailsDescription As System.Windows.Forms.Label
    Friend WithEvents chktraktListsDetailsNumbers As System.Windows.Forms.CheckBox
    Friend WithEvents chkltraktListsDetailsComments As System.Windows.Forms.CheckBox
    Friend WithEvents cbotraktListsDetailsPrivacy As System.Windows.Forms.ComboBox
    Friend WithEvents btntraktListsDetailsUpdate As System.Windows.Forms.Button
    Friend WithEvents tbptraktWatchlist As System.Windows.Forms.TabPage
    Friend WithEvents pnltraktWatchlist As System.Windows.Forms.Panel
    Friend WithEvents gbtraktWatchlist As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktWatchlistGetMovies As System.Windows.Forms.Button
    Friend WithEvents dgvtraktWatchlist As System.Windows.Forms.DataGridView
    Friend WithEvents lbltraktWatchliststate As System.Windows.Forms.Label
    Friend WithEvents prgtraktWatchlist As System.Windows.Forms.ProgressBar
    Friend WithEvents lbltraktWatchlisthelp As System.Windows.Forms.Label
    Friend WithEvents btntraktWatchlistSyncLibrary As System.Windows.Forms.Button
    Friend WithEvents btntraktWatchlistGetSeries As System.Windows.Forms.Button
    Friend WithEvents btntraktWatchlistSendEmberUnwatched As System.Windows.Forms.Button
    Friend WithEvents gbtraktWatchlistExpert As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktWatchlistClean As System.Windows.Forms.Button
    Friend WithEvents btntraktListSaveFavorite As System.Windows.Forms.Button
    Friend WithEvents gbtraktListsViewerStep1 As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktListsGetFriends As System.Windows.Forms.Button
    Friend WithEvents gbtraktListsViewerStep2 As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktListRemoveFavorite As System.Windows.Forms.Button
    Friend WithEvents btntraktListsGetFollowers As System.Windows.Forms.Button
    Friend WithEvents lbltraktListsScraped As System.Windows.Forms.Label
    Friend WithEvents cbotraktListsScraped As System.Windows.Forms.ComboBox
    Friend WithEvents cbotraktListsFavorites As System.Windows.Forms.ComboBox
    Friend WithEvents lbltraktListsFavorites As System.Windows.Forms.Label
    Friend WithEvents lbltraktListsLink As System.Windows.Forms.LinkLabel
    Friend WithEvents lbltraktListsNoticeSync As System.Windows.Forms.Label
    Friend WithEvents btnPlaycountSyncRating As System.Windows.Forms.Button
    Friend WithEvents coltraktWatchlistTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktWatchlistYear As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktWatchlistListedAt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktWatchlistIMDB As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents coltraktListTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktListYear As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktListRating As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktListGenres As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktListIMDB As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents coltraktListTrailer As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnPlaycountSyncWatched_Movies As System.Windows.Forms.Button
    Friend WithEvents btnPlaycountSyncWatched_TVShows As System.Windows.Forms.Button
    Friend WithEvents gbtraktPlaycountSync As System.Windows.Forms.GroupBox
    Friend WithEvents btnPlaycountSyncDeleteItem As System.Windows.Forms.Button
    Friend WithEvents tbptraktComments As System.Windows.Forms.TabPage
    Friend WithEvents gbtraktComments As System.Windows.Forms.GroupBox
    Friend WithEvents gbtraktCommentsGET As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktCommentsGet As System.Windows.Forms.Button
    Friend WithEvents gbtraktCommentsList As System.Windows.Forms.GroupBox
    Friend WithEvents gbtraktCommentsDetails As System.Windows.Forms.GroupBox
    Friend WithEvents btntraktCommentsDetailsDelete As System.Windows.Forms.Button
    Friend WithEvents chktraktCommentsDetailsSpoiler As System.Windows.Forms.CheckBox
    Friend WithEvents lbltraktCommentsDetailsDescription As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsDate As System.Windows.Forms.Label
    Friend WithEvents txttraktCommentsDetailsComment As System.Windows.Forms.TextBox
    Friend WithEvents lbltraktCommentsNotice As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsType As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsLikes As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsReplies As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsRating As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsRating2 As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsType2 As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsReplies2 As System.Windows.Forms.Label
    Friend WithEvents lbltraktCommentsDetailsDate2 As System.Windows.Forms.Label
    Friend WithEvents btntraktCommentsDetailsSend As System.Windows.Forms.Button
    Friend WithEvents btntraktCommentsDetailsUpdate As System.Windows.Forms.Button
    Friend WithEvents chktraktCommentsOnlyNoComments As System.Windows.Forms.CheckBox
    Friend WithEvents chktraktCommentsOnlyComments As System.Windows.Forms.CheckBox
    Friend WithEvents dgvtraktComments As System.Windows.Forms.DataGridView
    Friend WithEvents lbltraktCommentsDetailsLikes2 As System.Windows.Forms.Label
    Friend WithEvents coltraktCommentsMovie As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktCommentsDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktCommentsReplies As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktCommentsLikes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coltraktCommentsURL As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents coltraktCommentsImdb As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btntraktListsSavePlaylist As System.Windows.Forms.Button
    Friend WithEvents btntraktListsSendToKodi As System.Windows.Forms.Button
    Friend WithEvents tbptraktCleaning As System.Windows.Forms.TabPage
    Friend WithEvents gbtraktCleaning As System.Windows.Forms.GroupBox
    Friend WithEvents gbtraktCleaningHistoryTimestamp As System.Windows.Forms.GroupBox
    Friend WithEvents lbltraktCleaningHistoryTimestampDesc As System.Windows.Forms.Label
    Friend WithEvents btntraktCleaningHistoryTimestamp As System.Windows.Forms.Button
    Friend WithEvents lbltraktCleaningHistoryTimespan As System.Windows.Forms.Label
    Friend WithEvents txttraktCleaningHistoryTimestamp As System.Windows.Forms.TextBox
    Friend WithEvents cbotraktCleaningHistoryTimespan As System.Windows.Forms.ComboBox
    Friend WithEvents gbtraktCleaningHistoryTimespan As System.Windows.Forms.GroupBox
    Friend WithEvents lbltraktCleaningHistoryTimespanDesc As System.Windows.Forms.Label
    Friend WithEvents btntraktCleaningHistoryTimespan As System.Windows.Forms.Button
    Friend WithEvents lbltraktCleaningHistoryTimestamp As System.Windows.Forms.Label
    Friend WithEvents colPlaycountTraktID As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlaycountTitle As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlaycountPlayed As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlaycountLastWatched As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlaycountProgress As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlaycountRating As Windows.Forms.DataGridViewTextBoxColumn
End Class
