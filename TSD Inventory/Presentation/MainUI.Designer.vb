<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainUI))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewPrintcardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SavePrintcardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BrowsePrintcardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegisterColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomerSettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiecutManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RubberDieManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PaperCombinationSettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CascadeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileVerticalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileHorizontalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tbCreateNewPrintcard = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.tbDocumentSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btAddCustomer = New System.Windows.Forms.ToolStripButton()
        Me.bDocumentEdit = New System.Windows.Forms.ToolStripButton()
        Me.tbCancelEdit = New System.Windows.Forms.ToolStripButton()
        Me.StatusMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.MenuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.WindowsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(970, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewPrintcardToolStripMenuItem, Me.SavePrintcardToolStripMenuItem, Me.BrowsePrintcardToolStripMenuItem, Me.RegisterColorToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'NewPrintcardToolStripMenuItem
        '
        Me.NewPrintcardToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.NewPrintcardToolStripMenuItem.Name = "NewPrintcardToolStripMenuItem"
        Me.NewPrintcardToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewPrintcardToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.NewPrintcardToolStripMenuItem.Text = "&New Printcard"
        '
        'SavePrintcardToolStripMenuItem
        '
        Me.SavePrintcardToolStripMenuItem.Enabled = False
        Me.SavePrintcardToolStripMenuItem.Image = CType(resources.GetObject("SavePrintcardToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SavePrintcardToolStripMenuItem.Name = "SavePrintcardToolStripMenuItem"
        Me.SavePrintcardToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SavePrintcardToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.SavePrintcardToolStripMenuItem.Text = "Save Printcard"
        '
        'BrowsePrintcardToolStripMenuItem
        '
        Me.BrowsePrintcardToolStripMenuItem.Image = CType(resources.GetObject("BrowsePrintcardToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BrowsePrintcardToolStripMenuItem.Name = "BrowsePrintcardToolStripMenuItem"
        Me.BrowsePrintcardToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.BrowsePrintcardToolStripMenuItem.Text = "&Browse Printcard"
        '
        'RegisterColorToolStripMenuItem
        '
        Me.RegisterColorToolStripMenuItem.Enabled = False
        Me.RegisterColorToolStripMenuItem.Name = "RegisterColorToolStripMenuItem"
        Me.RegisterColorToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.RegisterColorToolStripMenuItem.Text = "New &Color Registration"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(192, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CustomerSettingToolStripMenuItem, Me.DiecutManagementToolStripMenuItem, Me.RubberDieManagementToolStripMenuItem, Me.PaperCombinationSettingToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'CustomerSettingToolStripMenuItem
        '
        Me.CustomerSettingToolStripMenuItem.Name = "CustomerSettingToolStripMenuItem"
        Me.CustomerSettingToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.CustomerSettingToolStripMenuItem.Text = "Customer Setting"
        '
        'DiecutManagementToolStripMenuItem
        '
        Me.DiecutManagementToolStripMenuItem.Name = "DiecutManagementToolStripMenuItem"
        Me.DiecutManagementToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DiecutManagementToolStripMenuItem.Text = "Diecut Management"
        '
        'RubberDieManagementToolStripMenuItem
        '
        Me.RubberDieManagementToolStripMenuItem.Name = "RubberDieManagementToolStripMenuItem"
        Me.RubberDieManagementToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RubberDieManagementToolStripMenuItem.Text = "Rubber Die Management"
        '
        'PaperCombinationSettingToolStripMenuItem
        '
        Me.PaperCombinationSettingToolStripMenuItem.Name = "PaperCombinationSettingToolStripMenuItem"
        Me.PaperCombinationSettingToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.PaperCombinationSettingToolStripMenuItem.Text = "Paper Combination Setting"
        '
        'WindowsToolStripMenuItem
        '
        Me.WindowsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CascadeToolStripMenuItem, Me.TileVerticalToolStripMenuItem, Me.TileHorizontalToolStripMenuItem, Me.CloseAllToolStripMenuItem})
        Me.WindowsToolStripMenuItem.Name = "WindowsToolStripMenuItem"
        Me.WindowsToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.WindowsToolStripMenuItem.Text = "&Windows"
        '
        'CascadeToolStripMenuItem
        '
        Me.CascadeToolStripMenuItem.Name = "CascadeToolStripMenuItem"
        Me.CascadeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CascadeToolStripMenuItem.Text = "&Cascade"
        '
        'TileVerticalToolStripMenuItem
        '
        Me.TileVerticalToolStripMenuItem.Name = "TileVerticalToolStripMenuItem"
        Me.TileVerticalToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.TileVerticalToolStripMenuItem.Text = "&Tile Vertical"
        '
        'TileHorizontalToolStripMenuItem
        '
        Me.TileHorizontalToolStripMenuItem.Name = "TileHorizontalToolStripMenuItem"
        Me.TileHorizontalToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.TileHorizontalToolStripMenuItem.Text = "Tile &Horizontal"
        '
        'CloseAllToolStripMenuItem
        '
        Me.CloseAllToolStripMenuItem.Name = "CloseAllToolStripMenuItem"
        Me.CloseAllToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CloseAllToolStripMenuItem.Text = "Close &All"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbCreateNewPrintcard, Me.ToolStripButton2, Me.tbDocumentSave, Me.ToolStripSeparator1, Me.btAddCustomer, Me.bDocumentEdit, Me.tbCancelEdit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(970, 39)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90
        '
        'tbCreateNewPrintcard
        '
        Me.tbCreateNewPrintcard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbCreateNewPrintcard.Image = CType(resources.GetObject("tbCreateNewPrintcard.Image"), System.Drawing.Image)
        Me.tbCreateNewPrintcard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tbCreateNewPrintcard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbCreateNewPrintcard.Name = "tbCreateNewPrintcard"
        Me.tbCreateNewPrintcard.Size = New System.Drawing.Size(36, 36)
        Me.tbCreateNewPrintcard.Text = "New Printcard"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton2.Text = "Browse  Printcard"
        '
        'tbDocumentSave
        '
        Me.tbDocumentSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbDocumentSave.Enabled = False
        Me.tbDocumentSave.Image = CType(resources.GetObject("tbDocumentSave.Image"), System.Drawing.Image)
        Me.tbDocumentSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tbDocumentSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbDocumentSave.Name = "tbDocumentSave"
        Me.tbDocumentSave.Size = New System.Drawing.Size(36, 36)
        Me.tbDocumentSave.Text = "Save Document"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'btAddCustomer
        '
        Me.btAddCustomer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btAddCustomer.Enabled = False
        Me.btAddCustomer.Image = CType(resources.GetObject("btAddCustomer.Image"), System.Drawing.Image)
        Me.btAddCustomer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btAddCustomer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btAddCustomer.Name = "btAddCustomer"
        Me.btAddCustomer.Size = New System.Drawing.Size(36, 36)
        Me.btAddCustomer.Text = "Add Customer"
        '
        'bDocumentEdit
        '
        Me.bDocumentEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bDocumentEdit.Enabled = False
        Me.bDocumentEdit.Image = CType(resources.GetObject("bDocumentEdit.Image"), System.Drawing.Image)
        Me.bDocumentEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.bDocumentEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bDocumentEdit.Name = "bDocumentEdit"
        Me.bDocumentEdit.Size = New System.Drawing.Size(36, 36)
        Me.bDocumentEdit.Text = "Edit Customer"
        '
        'tbCancelEdit
        '
        Me.tbCancelEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbCancelEdit.Enabled = False
        Me.tbCancelEdit.Image = CType(resources.GetObject("tbCancelEdit.Image"), System.Drawing.Image)
        Me.tbCancelEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tbCancelEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbCancelEdit.Name = "tbCancelEdit"
        Me.tbCancelEdit.Size = New System.Drawing.Size(36, 36)
        Me.tbCancelEdit.Text = "Cancel Task"
        '
        'StatusMessage
        '
        Me.StatusMessage.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusMessage.Name = "StatusMessage"
        Me.StatusMessage.Size = New System.Drawing.Size(111, 19)
        Me.StatusMessage.Text = "StatusMessage"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.White
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusMessage})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 514)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(970, 24)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'MainUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(970, 538)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainUI"
        Me.Text = "SMPC - TSD Inventory System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewPrintcardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RegisterColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tbDocumentSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbCreateNewPrintcard As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CascadeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TileVerticalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TileHorizontalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CustomerSettingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiecutManagementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PaperCombinationSettingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowsePrintcardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SavePrintcardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bDocumentEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbCancelEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btAddCustomer As System.Windows.Forms.ToolStripButton
    Friend WithEvents RubberDieManagementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusMessage As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip

End Class
