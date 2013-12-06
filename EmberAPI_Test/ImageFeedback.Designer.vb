<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageFeedback
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.btnYes = New System.Windows.Forms.Button()
        Me.Instructions = New System.Windows.Forms.TextBox()
        Me.BottomPanel = New System.Windows.Forms.Panel()
        Me.ButtonFlowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBackground = New System.Windows.Forms.Panel()
        Me.ShownPicture = New System.Windows.Forms.PictureBox()
        Me.SquareFiller = New System.Windows.Forms.Panel()
        Me.VerticalRuler = New EmberAPI_Test.PixelRuler()
        Me.HorizontalRuler = New EmberAPI_Test.PixelRuler()
        Me.BottomPanel.SuspendLayout()
        Me.ButtonFlowLayoutPanel.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PictureBackground.SuspendLayout()
        CType(Me.ShownPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNo
        '
        Me.btnNo.DialogResult = System.Windows.Forms.DialogResult.No
        Me.btnNo.Location = New System.Drawing.Point(266, 3)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(75, 23)
        Me.btnNo.TabIndex = 1
        Me.btnNo.Text = "&No"
        Me.btnNo.UseVisualStyleBackColor = True
        '
        'btnYes
        '
        Me.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnYes.Location = New System.Drawing.Point(185, 3)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(75, 23)
        Me.btnYes.TabIndex = 2
        Me.btnYes.Text = "&Yes"
        Me.btnYes.UseVisualStyleBackColor = True
        '
        'Instructions
        '
        Me.Instructions.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Instructions.Dock = System.Windows.Forms.DockStyle.Top
        Me.Instructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Instructions.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Instructions.Location = New System.Drawing.Point(0, 0)
        Me.Instructions.Multiline = True
        Me.Instructions.Name = "Instructions"
        Me.Instructions.ReadOnly = True
        Me.Instructions.Size = New System.Drawing.Size(344, 76)
        Me.Instructions.TabIndex = 3
        Me.Instructions.TabStop = False
        '
        'BottomPanel
        '
        Me.BottomPanel.AutoSize = True
        Me.BottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BottomPanel.Controls.Add(Me.ButtonFlowLayoutPanel)
        Me.BottomPanel.Controls.Add(Me.Instructions)
        Me.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottomPanel.Location = New System.Drawing.Point(0, 179)
        Me.BottomPanel.Name = "BottomPanel"
        Me.BottomPanel.Size = New System.Drawing.Size(344, 105)
        Me.BottomPanel.TabIndex = 4
        '
        'ButtonFlowLayoutPanel
        '
        Me.ButtonFlowLayoutPanel.AutoSize = True
        Me.ButtonFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ButtonFlowLayoutPanel.Controls.Add(Me.btnNo)
        Me.ButtonFlowLayoutPanel.Controls.Add(Me.btnYes)
        Me.ButtonFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.ButtonFlowLayoutPanel.Location = New System.Drawing.Point(0, 76)
        Me.ButtonFlowLayoutPanel.Name = "ButtonFlowLayoutPanel"
        Me.ButtonFlowLayoutPanel.Size = New System.Drawing.Size(344, 29)
        Me.ButtonFlowLayoutPanel.TabIndex = 5
        '
        'TopPanel
        '
        Me.TopPanel.AutoSize = True
        Me.TopPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TopPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(344, 179)
        Me.TopPanel.TabIndex = 5
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBackground, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SquareFiller, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VerticalRuler, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.HorizontalRuler, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(344, 179)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'PictureBackground
        '
        Me.PictureBackground.AutoSize = True
        Me.PictureBackground.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PictureBackground.Controls.Add(Me.ShownPicture)
        Me.PictureBackground.Location = New System.Drawing.Point(17, 17)
        Me.PictureBackground.Margin = New System.Windows.Forms.Padding(0, 0, 1, 1)
        Me.PictureBackground.Name = "PictureBackground"
        Me.PictureBackground.Size = New System.Drawing.Size(221, 126)
        Me.PictureBackground.TabIndex = 6
        '
        'ShownPicture
        '
        Me.ShownPicture.BackColor = System.Drawing.Color.Black
        Me.ShownPicture.Location = New System.Drawing.Point(0, 0)
        Me.ShownPicture.Margin = New System.Windows.Forms.Padding(0)
        Me.ShownPicture.Name = "ShownPicture"
        Me.ShownPicture.Size = New System.Drawing.Size(221, 126)
        Me.ShownPicture.TabIndex = 3
        Me.ShownPicture.TabStop = False
        '
        'SquareFiller
        '
        Me.SquareFiller.Location = New System.Drawing.Point(1, 1)
        Me.SquareFiller.Margin = New System.Windows.Forms.Padding(0)
        Me.SquareFiller.Name = "SquareFiller"
        Me.SquareFiller.Size = New System.Drawing.Size(15, 15)
        Me.SquareFiller.TabIndex = 1
        '
        'VerticalRuler
        '
        Me.VerticalRuler.AutoSize = True
        Me.VerticalRuler.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.VerticalRuler.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.VerticalRuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VerticalRuler.Location = New System.Drawing.Point(1, 17)
        Me.VerticalRuler.Margin = New System.Windows.Forms.Padding(0)
        Me.VerticalRuler.Name = "VerticalRuler"
        Me.VerticalRuler.Size = New System.Drawing.Size(15, 161)
        Me.VerticalRuler.TabIndex = 2
        '
        'HorizontalRuler
        '
        Me.HorizontalRuler.AutoSize = True
        Me.HorizontalRuler.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.HorizontalRuler.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.HorizontalRuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HorizontalRuler.Location = New System.Drawing.Point(17, 1)
        Me.HorizontalRuler.Margin = New System.Windows.Forms.Padding(0)
        Me.HorizontalRuler.Name = "HorizontalRuler"
        Me.HorizontalRuler.Size = New System.Drawing.Size(326, 15)
        Me.HorizontalRuler.TabIndex = 1
        '
        'ImageFeedback
        '
        Me.AcceptButton = Me.btnNo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(344, 284)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.BottomPanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "ImageFeedback"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Image Feedback"
        Me.TopMost = True
        Me.BottomPanel.ResumeLayout(False)
        Me.BottomPanel.PerformLayout()
        Me.ButtonFlowLayoutPanel.ResumeLayout(False)
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.PictureBackground.ResumeLayout(False)
        CType(Me.ShownPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents btnYes As System.Windows.Forms.Button
    Friend WithEvents Instructions As System.Windows.Forms.TextBox
    Friend WithEvents BottomPanel As System.Windows.Forms.Panel
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents HorizontalRuler As EmberAPI_Test.PixelRuler
    Friend WithEvents VerticalRuler As EmberAPI_Test.PixelRuler
    Friend WithEvents SquareFiller As System.Windows.Forms.Panel
    Friend WithEvents ButtonFlowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ShownPicture As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBackground As System.Windows.Forms.Panel
End Class
