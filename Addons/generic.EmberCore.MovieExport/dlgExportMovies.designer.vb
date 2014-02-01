<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgExportMovies
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cbTemplate As System.Windows.Forms.ComboBox
    Friend  WithEvents Close_Button As System.Windows.Forms.Button
    Friend  WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lblFilterSelected As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents pbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlBG As System.Windows.Forms.Panel
    Friend WithEvents pnlBottomMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents Save_Button As System.Windows.Forms.Button
    Friend WithEvents wbMovieList As System.Windows.Forms.WebBrowser

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

#End Region 'Fields

#Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgExportMovies))
        Me.Save_Button = New System.Windows.Forms.Button()
        Me.Close_Button = New System.Windows.Forms.Button()
        Me.pnlBottomMain = New System.Windows.Forms.Panel()
        Me.btn_BuildHTML = New System.Windows.Forms.Button()
        Me.lblFilterSelected = New System.Windows.Forms.Label()
        Me.cbo_SelectedFilter = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbTemplate = New System.Windows.Forms.ComboBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.pnlBG = New System.Windows.Forms.Panel()
        Me.wbMovieList = New System.Windows.Forms.WebBrowser()
        Me.pnlBottomMain.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        Me.pnlBG.SuspendLayout()
        Me.SuspendLayout()
        '
        'Save_Button
        '
        Me.Save_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Save_Button.Enabled = False
        Me.Save_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Save_Button.Location = New System.Drawing.Point(779, 9)
        Me.Save_Button.Name = "Save_Button"
        Me.Save_Button.Size = New System.Drawing.Size(119, 31)
        Me.Save_Button.TabIndex = 6
        Me.Save_Button.Text = "Save"
        '
        'Close_Button
        '
        Me.Close_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Close_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Close_Button.Location = New System.Drawing.Point(904, 9)
        Me.Close_Button.Name = "Close_Button"
        Me.Close_Button.Size = New System.Drawing.Size(119, 31)
        Me.Close_Button.TabIndex = 1
        Me.Close_Button.Text = "Close"
        '
        'pnlBottomMain
        '
        Me.pnlBottomMain.Controls.Add(Me.Close_Button)
        Me.pnlBottomMain.Controls.Add(Me.btn_BuildHTML)
        Me.pnlBottomMain.Controls.Add(Me.Save_Button)
        Me.pnlBottomMain.Controls.Add(Me.lblFilterSelected)
        Me.pnlBottomMain.Controls.Add(Me.cbo_SelectedFilter)
        Me.pnlBottomMain.Controls.Add(Me.Label2)
        Me.pnlBottomMain.Controls.Add(Me.cbTemplate)
        Me.pnlBottomMain.Location = New System.Drawing.Point(0, 502)
        Me.pnlBottomMain.Name = "pnlBottomMain"
        Me.pnlBottomMain.Size = New System.Drawing.Size(1035, 48)
        Me.pnlBottomMain.TabIndex = 0
        '
        'btn_BuildHTML
        '
        Me.btn_BuildHTML.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_BuildHTML.Enabled = False
        Me.btn_BuildHTML.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_BuildHTML.Location = New System.Drawing.Point(604, 9)
        Me.btn_BuildHTML.Name = "btn_BuildHTML"
        Me.btn_BuildHTML.Size = New System.Drawing.Size(169, 31)
        Me.btn_BuildHTML.TabIndex = 5
        Me.btn_BuildHTML.Text = "Generate Template"
        '
        'lblFilterSelected
        '
        Me.lblFilterSelected.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterSelected.Location = New System.Drawing.Point(288, 6)
        Me.lblFilterSelected.Name = "lblFilterSelected"
        Me.lblFilterSelected.Size = New System.Drawing.Size(43, 16)
        Me.lblFilterSelected.TabIndex = 0
        Me.lblFilterSelected.Text = "Filter"
        Me.lblFilterSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbo_SelectedFilter
        '
        Me.cbo_SelectedFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_SelectedFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_SelectedFilter.FormattingEnabled = True
        Me.cbo_SelectedFilter.Items.AddRange(New Object() {"-", "Filter 1", "Filter 2", "Filter 3"})
        Me.cbo_SelectedFilter.Location = New System.Drawing.Point(337, 9)
        Me.cbo_SelectedFilter.Name = "cbo_SelectedFilter"
        Me.cbo_SelectedFilter.Size = New System.Drawing.Size(138, 21)
        Me.cbo_SelectedFilter.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Template"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTemplate
        '
        Me.cbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTemplate.FormattingEnabled = True
        Me.cbTemplate.Location = New System.Drawing.Point(71, 9)
        Me.cbTemplate.Name = "cbTemplate"
        Me.cbTemplate.Size = New System.Drawing.Size(195, 21)
        Me.cbTemplate.TabIndex = 2
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "asc.png")
        Me.ImageList1.Images.SetKeyName(1, "desc.png")
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.LightGray
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Controls.Add(Me.pbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Location = New System.Drawing.Point(242, 12)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 1
        Me.pnlCancel.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'pbCompile
        '
        Me.pbCompile.Location = New System.Drawing.Point(8, 36)
        Me.pbCompile.Name = "pbCompile"
        Me.pbCompile.Size = New System.Drawing.Size(388, 18)
        Me.pbCompile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbCompile.TabIndex = 3
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 11)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(395, 20)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Compiling Movie List..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.lblCanceling.Text = "Canceling Compilation..."
        Me.lblCanceling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCanceling.Visible = False
        '
        'pnlBG
        '
        Me.pnlBG.AutoScroll = True
        Me.pnlBG.Controls.Add(Me.pnlCancel)
        Me.pnlBG.Controls.Add(Me.wbMovieList)
        Me.pnlBG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBG.Location = New System.Drawing.Point(0, 0)
        Me.pnlBG.Name = "pnlBG"
        Me.pnlBG.Size = New System.Drawing.Size(1035, 550)
        Me.pnlBG.TabIndex = 4
        '
        'wbMovieList
        '
        Me.wbMovieList.Location = New System.Drawing.Point(0, 0)
        Me.wbMovieList.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbMovieList.Name = "wbMovieList"
        Me.wbMovieList.Size = New System.Drawing.Size(1034, 500)
        Me.wbMovieList.TabIndex = 0
        Me.wbMovieList.Visible = False
        '
        'dlgExportMovies
        '
        Me.AcceptButton = Me.Save_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.CancelButton = Me.Close_Button
        Me.ClientSize = New System.Drawing.Size(1035, 550)
        Me.Controls.Add(Me.pnlBottomMain)
        Me.Controls.Add(Me.pnlBG)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgExportMovies"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export Movies"
        Me.pnlBottomMain.ResumeLayout(False)
        Me.pnlBottomMain.PerformLayout()
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlBG.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbo_SelectedFilter As System.Windows.Forms.ComboBox
    Friend WithEvents btn_BuildHTML As System.Windows.Forms.Button

#End Region 'Methods

End Class