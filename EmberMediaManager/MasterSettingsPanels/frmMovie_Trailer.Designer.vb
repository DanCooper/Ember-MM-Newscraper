﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_Trailer
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTrailers = New System.Windows.Forms.GroupBox()
        Me.tblTrailers = New System.Windows.Forms.TableLayoutPanel()
        Me.txtDefaultSearchParameter = New System.Windows.Forms.TextBox()
        Me.lblDefaultSearchParameter = New System.Windows.Forms.Label()
        Me.chkKeepExisting = New System.Windows.Forms.CheckBox()
        Me.cbMinimumQuality = New System.Windows.Forms.ComboBox()
        Me.lblMinimumQuality = New System.Windows.Forms.Label()
        Me.lblPreferredQuality = New System.Windows.Forms.Label()
        Me.cbPreferredQuality = New System.Windows.Forms.ComboBox()
        Me.sgvScraper = New Ember_Media_Manager.ScraperGridView()
        Me.lblScraper = New System.Windows.Forms.Label()
        Me.lblKeepExisting = New System.Windows.Forms.Label()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbTrailers.SuspendLayout()
        Me.tblTrailers.SuspendLayout()
        CType(Me.sgvScraper, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(470, 303)
        Me.pnlSettings.TabIndex = 22
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbTrailers, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(470, 303)
        Me.tblSettings.TabIndex = 2
        '
        'gbTrailers
        '
        Me.gbTrailers.AutoSize = True
        Me.gbTrailers.Controls.Add(Me.tblTrailers)
        Me.gbTrailers.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbTrailers.Location = New System.Drawing.Point(3, 3)
        Me.gbTrailers.Name = "gbTrailers"
        Me.gbTrailers.Size = New System.Drawing.Size(339, 146)
        Me.gbTrailers.TabIndex = 1
        Me.gbTrailers.TabStop = False
        Me.gbTrailers.Text = "Trailers"
        '
        'tblTrailers
        '
        Me.tblTrailers.AutoSize = True
        Me.tblTrailers.ColumnCount = 2
        Me.tblTrailers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTrailers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTrailers.Controls.Add(Me.lblPreferredQuality, 0, 1)
        Me.tblTrailers.Controls.Add(Me.cbPreferredQuality, 1, 1)
        Me.tblTrailers.Controls.Add(Me.lblMinimumQuality, 0, 2)
        Me.tblTrailers.Controls.Add(Me.cbMinimumQuality, 1, 2)
        Me.tblTrailers.Controls.Add(Me.lblDefaultSearchParameter, 0, 3)
        Me.tblTrailers.Controls.Add(Me.txtDefaultSearchParameter, 1, 3)
        Me.tblTrailers.Controls.Add(Me.sgvScraper, 1, 4)
        Me.tblTrailers.Controls.Add(Me.lblScraper, 0, 4)
        Me.tblTrailers.Controls.Add(Me.chkKeepExisting, 1, 0)
        Me.tblTrailers.Controls.Add(Me.lblKeepExisting, 0, 0)
        Me.tblTrailers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTrailers.Location = New System.Drawing.Point(3, 18)
        Me.tblTrailers.Name = "tblTrailers"
        Me.tblTrailers.RowCount = 5
        Me.tblTrailers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTrailers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTrailers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTrailers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTrailers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTrailers.Size = New System.Drawing.Size(333, 125)
        Me.tblTrailers.TabIndex = 2
        '
        'txtDefaultSearchParameter
        '
        Me.txtDefaultSearchParameter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtDefaultSearchParameter.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDefaultSearchParameter.Location = New System.Drawing.Point(148, 77)
        Me.txtDefaultSearchParameter.Name = "txtDefaultSearchParameter"
        Me.txtDefaultSearchParameter.Size = New System.Drawing.Size(182, 22)
        Me.txtDefaultSearchParameter.TabIndex = 10
        '
        'lblDefaultSearchParameter
        '
        Me.lblDefaultSearchParameter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDefaultSearchParameter.AutoSize = True
        Me.lblDefaultSearchParameter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultSearchParameter.Location = New System.Drawing.Point(3, 81)
        Me.lblDefaultSearchParameter.Name = "lblDefaultSearchParameter"
        Me.lblDefaultSearchParameter.Size = New System.Drawing.Size(139, 13)
        Me.lblDefaultSearchParameter.TabIndex = 11
        Me.lblDefaultSearchParameter.Text = "Default Search Parameter:"
        '
        'chkKeepExisting
        '
        Me.chkKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkKeepExisting.AutoSize = True
        Me.chkKeepExisting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkKeepExisting.Location = New System.Drawing.Point(148, 3)
        Me.chkKeepExisting.Name = "chkKeepExisting"
        Me.chkKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkKeepExisting.TabIndex = 4
        Me.chkKeepExisting.UseVisualStyleBackColor = True
        '
        'cbMinimumQuality
        '
        Me.cbMinimumQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMinimumQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMinimumQuality.FormattingEnabled = True
        Me.cbMinimumQuality.Location = New System.Drawing.Point(148, 50)
        Me.cbMinimumQuality.Name = "cbMinimumQuality"
        Me.cbMinimumQuality.Size = New System.Drawing.Size(182, 21)
        Me.cbMinimumQuality.TabIndex = 9
        '
        'lblMinimumQuality
        '
        Me.lblMinimumQuality.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMinimumQuality.AutoSize = True
        Me.lblMinimumQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinimumQuality.Location = New System.Drawing.Point(3, 54)
        Me.lblMinimumQuality.Name = "lblMinimumQuality"
        Me.lblMinimumQuality.Size = New System.Drawing.Size(97, 13)
        Me.lblMinimumQuality.TabIndex = 8
        Me.lblMinimumQuality.Text = "Minimum Quality:"
        '
        'lblPreferredQuality
        '
        Me.lblPreferredQuality.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPreferredQuality.AutoSize = True
        Me.lblPreferredQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreferredQuality.Location = New System.Drawing.Point(3, 27)
        Me.lblPreferredQuality.Name = "lblPreferredQuality"
        Me.lblPreferredQuality.Size = New System.Drawing.Size(96, 13)
        Me.lblPreferredQuality.TabIndex = 6
        Me.lblPreferredQuality.Text = "Preferred Quality:"
        '
        'cbPreferredQuality
        '
        Me.cbPreferredQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPreferredQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbPreferredQuality.FormattingEnabled = True
        Me.cbPreferredQuality.Location = New System.Drawing.Point(148, 23)
        Me.cbPreferredQuality.Name = "cbPreferredQuality"
        Me.cbPreferredQuality.Size = New System.Drawing.Size(182, 21)
        Me.cbPreferredQuality.TabIndex = 7
        '
        'sgvScraper
        '
        Me.sgvScraper.AllowUserToAddRows = False
        Me.sgvScraper.AllowUserToDeleteRows = False
        Me.sgvScraper.AllowUserToOrderColumns = True
        Me.sgvScraper.AllowUserToResizeColumns = False
        Me.sgvScraper.AllowUserToResizeRows = False
        Me.sgvScraper.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.sgvScraper.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.sgvScraper.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.sgvScraper.ColumnHeadersVisible = False
        Me.sgvScraper.Location = New System.Drawing.Point(145, 102)
        Me.sgvScraper.Margin = New System.Windows.Forms.Padding(0)
        Me.sgvScraper.MultiSelect = False
        Me.sgvScraper.Name = "sgvScraper"
        Me.sgvScraper.RowHeadersVisible = False
        Me.sgvScraper.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.sgvScraper.Size = New System.Drawing.Size(100, 23)
        Me.sgvScraper.TabIndex = 12
        '
        'lblScraper
        '
        Me.lblScraper.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblScraper.AutoSize = True
        Me.lblScraper.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraper.Location = New System.Drawing.Point(3, 107)
        Me.lblScraper.Name = "lblScraper"
        Me.lblScraper.Size = New System.Drawing.Size(48, 13)
        Me.lblScraper.TabIndex = 11
        Me.lblScraper.Text = "Scraper:"
        '
        'lblKeepExisting
        '
        Me.lblKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblKeepExisting.AutoSize = True
        Me.lblKeepExisting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKeepExisting.Location = New System.Drawing.Point(3, 3)
        Me.lblKeepExisting.Name = "lblKeepExisting"
        Me.lblKeepExisting.Size = New System.Drawing.Size(78, 13)
        Me.lblKeepExisting.TabIndex = 6
        Me.lblKeepExisting.Text = "Keep existing:"
        '
        'frmMovie_Trailer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(470, 303)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovie_Trailer"
        Me.Text = "frmMovie_Trailer"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbTrailers.ResumeLayout(False)
        Me.gbTrailers.PerformLayout()
        Me.tblTrailers.ResumeLayout(False)
        Me.tblTrailers.PerformLayout()
        CType(Me.sgvScraper, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbTrailers As GroupBox
    Friend WithEvents tblTrailers As TableLayoutPanel
    Friend WithEvents txtDefaultSearchParameter As TextBox
    Friend WithEvents lblDefaultSearchParameter As Label
    Friend WithEvents chkKeepExisting As CheckBox
    Friend WithEvents cbMinimumQuality As ComboBox
    Friend WithEvents lblMinimumQuality As Label
    Friend WithEvents lblPreferredQuality As Label
    Friend WithEvents cbPreferredQuality As ComboBox
    Friend WithEvents sgvScraper As ScraperGridView
    Friend WithEvents lblScraper As Label
    Friend WithEvents lblKeepExisting As Label
End Class
