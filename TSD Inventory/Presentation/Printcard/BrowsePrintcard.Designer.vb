<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrowsePrintcard
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BrowsePrintcard))
        Me.PrintcardGrid = New System.Windows.Forms.DataGridView()
        Me.printcardid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fileid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.organization_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.box_description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.insidedimension = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.boardsize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printcardno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rubber_location = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.diecut_number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.racklocation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.testid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.test_type_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.date_created = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.filetype = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.color1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.color2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.color3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.color4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.boardtypeid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fluteid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jointid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.combinationid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dimensionid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.scaleid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customer_file_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customerfile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customerfiletype = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printcopyno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.filename = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.deleted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.notes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printcard_status_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customer_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.scale_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.boxcategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewCustomerFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewPrintcardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewPlateUsageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateCopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditPrintcardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetPriceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeletePrintcardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EmailToCustomerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdCreateCopy = New System.Windows.Forms.Button()
        Me.cSearchThis = New System.Windows.Forms.TextBox()
        Me.cmdViewPrintcard = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.cmbNumRecords = New System.Windows.Forms.ComboBox()
        Me.gridColumn = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.lCountRows = New System.Windows.Forms.Label()
        Me.lCurrentSel = New System.Windows.Forms.Label()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmdSearchDB = New System.Windows.Forms.Button()
        Me.cmdViewCustomerFile = New System.Windows.Forms.Button()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.cmdEditPrintcard = New System.Windows.Forms.Button()
        Me.PanelFunction1 = New System.Windows.Forms.Panel()
        Me.cmdCreateOrder = New System.Windows.Forms.Button()
        Me.cmdSetPrice = New System.Windows.Forms.Button()
        Me.PanelUtilities = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkDisplayHint = New System.Windows.Forms.CheckBox()
        Me.chkSeachByDate = New System.Windows.Forms.CheckBox()
        CType(Me.PrintcardGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.PanelFunction1.SuspendLayout()
        Me.PanelUtilities.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintcardGrid
        '
        Me.PrintcardGrid.AllowUserToAddRows = False
        Me.PrintcardGrid.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.PrintcardGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.PowderBlue
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PrintcardGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.PrintcardGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PrintcardGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.printcardid, Me.fileid, Me.id, Me.organization_name, Me.box_description, Me.insidedimension, Me.boardsize, Me.printcardno, Me.status, Me.rubber_location, Me.diecut_number, Me.racklocation, Me.testid, Me.test_type_code, Me.date_created, Me.filetype, Me.color1, Me.color2, Me.color3, Me.color4, Me.boardtypeid, Me.fluteid, Me.jointid, Me.combinationid, Me.dimensionid, Me.scaleid, Me.customer_file_id, Me.customerfile, Me.customerfiletype, Me.printcopyno, Me.filename, Me.deleted, Me.notes, Me.printcard_status_id, Me.customer_id, Me.scale_id, Me.boxcategory})
        Me.PrintcardGrid.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PrintcardGrid.Location = New System.Drawing.Point(15, 57)
        Me.PrintcardGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.PrintcardGrid.MultiSelect = False
        Me.PrintcardGrid.Name = "PrintcardGrid"
        Me.PrintcardGrid.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.PowderBlue
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PrintcardGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.PrintcardGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PrintcardGrid.Size = New System.Drawing.Size(1207, 481)
        Me.PrintcardGrid.TabIndex = 0
        '
        'printcardid
        '
        Me.printcardid.DataPropertyName = "printcardid"
        Me.printcardid.HeaderText = "ID"
        Me.printcardid.Name = "printcardid"
        Me.printcardid.ReadOnly = True
        Me.printcardid.Width = 50
        '
        'fileid
        '
        Me.fileid.DataPropertyName = "fileid"
        Me.fileid.HeaderText = "File ID"
        Me.fileid.Name = "fileid"
        Me.fileid.ReadOnly = True
        Me.fileid.Visible = False
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "ID"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        Me.id.Width = 50
        '
        'organization_name
        '
        Me.organization_name.DataPropertyName = "organization_name"
        Me.organization_name.HeaderText = "Customer"
        Me.organization_name.Name = "organization_name"
        Me.organization_name.ReadOnly = True
        Me.organization_name.Width = 200
        '
        'box_description
        '
        Me.box_description.DataPropertyName = "box_description"
        Me.box_description.HeaderText = "Box Description"
        Me.box_description.Name = "box_description"
        Me.box_description.ReadOnly = True
        Me.box_description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.box_description.Width = 250
        '
        'insidedimension
        '
        Me.insidedimension.DataPropertyName = "insidedimension"
        Me.insidedimension.HeaderText = "Inside Dimension"
        Me.insidedimension.Name = "insidedimension"
        Me.insidedimension.ReadOnly = True
        '
        'boardsize
        '
        Me.boardsize.DataPropertyName = "boardsize"
        Me.boardsize.HeaderText = "Board Size"
        Me.boardsize.Name = "boardsize"
        Me.boardsize.ReadOnly = True
        Me.boardsize.Width = 150
        '
        'printcardno
        '
        Me.printcardno.DataPropertyName = "printcardno"
        Me.printcardno.HeaderText = "Printcard No."
        Me.printcardno.Name = "printcardno"
        Me.printcardno.ReadOnly = True
        Me.printcardno.Width = 150
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        '
        'rubber_location
        '
        Me.rubber_location.DataPropertyName = "rubber_location"
        Me.rubber_location.HeaderText = "Rubberdie Rack"
        Me.rubber_location.Name = "rubber_location"
        Me.rubber_location.ReadOnly = True
        Me.rubber_location.Width = 75
        '
        'diecut_number
        '
        Me.diecut_number.DataPropertyName = "diecut_number"
        Me.diecut_number.HeaderText = "Diecut"
        Me.diecut_number.Name = "diecut_number"
        Me.diecut_number.ReadOnly = True
        Me.diecut_number.Width = 50
        '
        'racklocation
        '
        Me.racklocation.DataPropertyName = "racklocation"
        Me.racklocation.HeaderText = "Diecut Rack"
        Me.racklocation.Name = "racklocation"
        Me.racklocation.ReadOnly = True
        Me.racklocation.Width = 50
        '
        'testid
        '
        Me.testid.DataPropertyName = "testid"
        Me.testid.HeaderText = "Test"
        Me.testid.Name = "testid"
        Me.testid.ReadOnly = True
        Me.testid.Width = 50
        '
        'test_type_code
        '
        Me.test_type_code.DataPropertyName = "test_type_code"
        Me.test_type_code.HeaderText = "Test Type"
        Me.test_type_code.Name = "test_type_code"
        Me.test_type_code.ReadOnly = True
        Me.test_type_code.Width = 60
        '
        'date_created
        '
        Me.date_created.DataPropertyName = "date_created"
        Me.date_created.HeaderText = "Date Created"
        Me.date_created.Name = "date_created"
        Me.date_created.ReadOnly = True
        '
        'filetype
        '
        Me.filetype.DataPropertyName = "filetype"
        Me.filetype.HeaderText = "Filetype"
        Me.filetype.Name = "filetype"
        Me.filetype.ReadOnly = True
        Me.filetype.Visible = False
        Me.filetype.Width = 120
        '
        'color1
        '
        Me.color1.DataPropertyName = "color1"
        Me.color1.HeaderText = "Color 1"
        Me.color1.Name = "color1"
        Me.color1.ReadOnly = True
        Me.color1.Visible = False
        '
        'color2
        '
        Me.color2.DataPropertyName = "color2"
        Me.color2.HeaderText = "Color 2"
        Me.color2.Name = "color2"
        Me.color2.ReadOnly = True
        Me.color2.Visible = False
        '
        'color3
        '
        Me.color3.DataPropertyName = "color3"
        Me.color3.HeaderText = "Color 3"
        Me.color3.Name = "color3"
        Me.color3.ReadOnly = True
        Me.color3.Visible = False
        '
        'color4
        '
        Me.color4.DataPropertyName = "color4"
        Me.color4.HeaderText = "Color 4"
        Me.color4.Name = "color4"
        Me.color4.ReadOnly = True
        Me.color4.Visible = False
        '
        'boardtypeid
        '
        Me.boardtypeid.DataPropertyName = "boardtypeid"
        Me.boardtypeid.HeaderText = "Board Type"
        Me.boardtypeid.Name = "boardtypeid"
        Me.boardtypeid.ReadOnly = True
        '
        'fluteid
        '
        Me.fluteid.DataPropertyName = "fluteid"
        Me.fluteid.HeaderText = "Flute ID"
        Me.fluteid.Name = "fluteid"
        Me.fluteid.ReadOnly = True
        Me.fluteid.Visible = False
        '
        'jointid
        '
        Me.jointid.DataPropertyName = "jointid"
        Me.jointid.HeaderText = "Joint"
        Me.jointid.Name = "jointid"
        Me.jointid.ReadOnly = True
        Me.jointid.Visible = False
        '
        'combinationid
        '
        Me.combinationid.DataPropertyName = "combinationid"
        Me.combinationid.HeaderText = "Combination"
        Me.combinationid.Name = "combinationid"
        Me.combinationid.ReadOnly = True
        Me.combinationid.Visible = False
        '
        'dimensionid
        '
        Me.dimensionid.DataPropertyName = "dimensionid"
        Me.dimensionid.HeaderText = "Dimension"
        Me.dimensionid.Name = "dimensionid"
        Me.dimensionid.ReadOnly = True
        Me.dimensionid.Visible = False
        '
        'scaleid
        '
        Me.scaleid.DataPropertyName = "scaleid"
        Me.scaleid.HeaderText = "Scale"
        Me.scaleid.Name = "scaleid"
        Me.scaleid.ReadOnly = True
        Me.scaleid.Visible = False
        '
        'customer_file_id
        '
        Me.customer_file_id.DataPropertyName = "customer_file_id"
        Me.customer_file_id.HeaderText = "Customer File ID"
        Me.customer_file_id.Name = "customer_file_id"
        Me.customer_file_id.ReadOnly = True
        Me.customer_file_id.Visible = False
        '
        'customerfile
        '
        Me.customerfile.DataPropertyName = "customerfile"
        Me.customerfile.HeaderText = "Customer File"
        Me.customerfile.Name = "customerfile"
        Me.customerfile.ReadOnly = True
        Me.customerfile.Visible = False
        '
        'customerfiletype
        '
        Me.customerfiletype.DataPropertyName = "customerfiletype"
        Me.customerfiletype.HeaderText = "CustomerFileType"
        Me.customerfiletype.Name = "customerfiletype"
        Me.customerfiletype.ReadOnly = True
        Me.customerfiletype.Visible = False
        '
        'printcopyno
        '
        Me.printcopyno.DataPropertyName = "printcopyno"
        Me.printcopyno.HeaderText = "Printcard #"
        Me.printcopyno.Name = "printcopyno"
        Me.printcopyno.ReadOnly = True
        Me.printcopyno.Visible = False
        '
        'filename
        '
        Me.filename.DataPropertyName = "filename"
        Me.filename.HeaderText = "Filename"
        Me.filename.Name = "filename"
        Me.filename.ReadOnly = True
        Me.filename.Width = 200
        '
        'deleted
        '
        Me.deleted.DataPropertyName = "deleted"
        Me.deleted.HeaderText = "deleted"
        Me.deleted.Name = "deleted"
        Me.deleted.ReadOnly = True
        Me.deleted.Visible = False
        '
        'notes
        '
        Me.notes.DataPropertyName = "notes"
        Me.notes.HeaderText = "Notes"
        Me.notes.Name = "notes"
        Me.notes.ReadOnly = True
        '
        'printcard_status_id
        '
        Me.printcard_status_id.DataPropertyName = "printcard_status_id"
        Me.printcard_status_id.HeaderText = "printcard_status_id"
        Me.printcard_status_id.Name = "printcard_status_id"
        Me.printcard_status_id.ReadOnly = True
        Me.printcard_status_id.Visible = False
        '
        'customer_id
        '
        Me.customer_id.DataPropertyName = "customer_id"
        Me.customer_id.HeaderText = "customer_id"
        Me.customer_id.Name = "customer_id"
        Me.customer_id.ReadOnly = True
        Me.customer_id.Visible = False
        '
        'scale_id
        '
        Me.scale_id.HeaderText = "Scale"
        Me.scale_id.Name = "scale_id"
        Me.scale_id.ReadOnly = True
        Me.scale_id.Visible = False
        '
        'boxcategory
        '
        Me.boxcategory.DataPropertyName = "boxcategory"
        Me.boxcategory.HeaderText = "Category"
        Me.boxcategory.Name = "boxcategory"
        Me.boxcategory.ReadOnly = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.BackgroundImage = CType(resources.GetObject("ContextMenuStrip1.BackgroundImage"), System.Drawing.Image)
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportToExcelToolStripMenuItem, Me.ViewCustomerFileToolStripMenuItem, Me.ViewPrintcardToolStripMenuItem, Me.ViewPlateUsageToolStripMenuItem, Me.CreateCopyToolStripMenuItem, Me.EditPrintcardToolStripMenuItem, Me.SetPriceToolStripMenuItem, Me.DeletePrintcardToolStripMenuItem, Me.ToolStripMenuItem3, Me.EmailToCustomerToolStripMenuItem, Me.ToolStripMenuItem2, Me.RefreshToolStripMenuItem, Me.PropertiesToolStripMenuItem, Me.ToolStripMenuItem4})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(213, 264)
        '
        'ExportToExcelToolStripMenuItem
        '
        Me.ExportToExcelToolStripMenuItem.Image = CType(resources.GetObject("ExportToExcelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExportToExcelToolStripMenuItem.Name = "ExportToExcelToolStripMenuItem"
        Me.ExportToExcelToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ExportToExcelToolStripMenuItem.Text = "Export to Excel"
        '
        'ViewCustomerFileToolStripMenuItem
        '
        Me.ViewCustomerFileToolStripMenuItem.Name = "ViewCustomerFileToolStripMenuItem"
        Me.ViewCustomerFileToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ViewCustomerFileToolStripMenuItem.Text = "View Customer File"
        '
        'ViewPrintcardToolStripMenuItem
        '
        Me.ViewPrintcardToolStripMenuItem.Name = "ViewPrintcardToolStripMenuItem"
        Me.ViewPrintcardToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ViewPrintcardToolStripMenuItem.Text = "View Printcard"
        '
        'ViewPlateUsageToolStripMenuItem
        '
        Me.ViewPlateUsageToolStripMenuItem.Name = "ViewPlateUsageToolStripMenuItem"
        Me.ViewPlateUsageToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.ViewPlateUsageToolStripMenuItem.Text = "View Plate Usage"
        '
        'CreateCopyToolStripMenuItem
        '
        Me.CreateCopyToolStripMenuItem.Name = "CreateCopyToolStripMenuItem"
        Me.CreateCopyToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.CreateCopyToolStripMenuItem.Text = "Create Copy"
        '
        'EditPrintcardToolStripMenuItem
        '
        Me.EditPrintcardToolStripMenuItem.Name = "EditPrintcardToolStripMenuItem"
        Me.EditPrintcardToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.EditPrintcardToolStripMenuItem.Text = "Edit Printcard"
        '
        'SetPriceToolStripMenuItem
        '
        Me.SetPriceToolStripMenuItem.Name = "SetPriceToolStripMenuItem"
        Me.SetPriceToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.SetPriceToolStripMenuItem.Text = "Set Price"
        '
        'DeletePrintcardToolStripMenuItem
        '
        Me.DeletePrintcardToolStripMenuItem.Name = "DeletePrintcardToolStripMenuItem"
        Me.DeletePrintcardToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.DeletePrintcardToolStripMenuItem.Text = "Delete Printcard"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(209, 6)
        '
        'EmailToCustomerToolStripMenuItem
        '
        Me.EmailToCustomerToolStripMenuItem.Name = "EmailToCustomerToolStripMenuItem"
        Me.EmailToCustomerToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.EmailToCustomerToolStripMenuItem.Text = "Email to Customer ->"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(209, 6)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Image = CType(resources.GetObject("PropertiesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(212, 22)
        Me.PropertiesToolStripMenuItem.Text = "P&roperties"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(209, 6)
        '
        'cmdCreateCopy
        '
        Me.cmdCreateCopy.BackColor = System.Drawing.Color.White
        Me.cmdCreateCopy.BackgroundImage = CType(resources.GetObject("cmdCreateCopy.BackgroundImage"), System.Drawing.Image)
        Me.cmdCreateCopy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdCreateCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdCreateCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCreateCopy.Image = CType(resources.GetObject("cmdCreateCopy.Image"), System.Drawing.Image)
        Me.cmdCreateCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCreateCopy.Location = New System.Drawing.Point(378, 7)
        Me.cmdCreateCopy.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdCreateCopy.Name = "cmdCreateCopy"
        Me.cmdCreateCopy.Size = New System.Drawing.Size(113, 48)
        Me.cmdCreateCopy.TabIndex = 30
        Me.cmdCreateCopy.Text = "Create Copy"
        Me.cmdCreateCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdCreateCopy.UseVisualStyleBackColor = False
        '
        'cSearchThis
        '
        Me.cSearchThis.BackColor = System.Drawing.Color.White
        Me.cSearchThis.Location = New System.Drawing.Point(320, 7)
        Me.cSearchThis.Name = "cSearchThis"
        Me.cSearchThis.Size = New System.Drawing.Size(250, 22)
        Me.cSearchThis.TabIndex = 10
        '
        'cmdViewPrintcard
        '
        Me.cmdViewPrintcard.BackColor = System.Drawing.Color.White
        Me.cmdViewPrintcard.BackgroundImage = CType(resources.GetObject("cmdViewPrintcard.BackgroundImage"), System.Drawing.Image)
        Me.cmdViewPrintcard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdViewPrintcard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdViewPrintcard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdViewPrintcard.Image = CType(resources.GetObject("cmdViewPrintcard.Image"), System.Drawing.Image)
        Me.cmdViewPrintcard.Location = New System.Drawing.Point(257, 7)
        Me.cmdViewPrintcard.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdViewPrintcard.Name = "cmdViewPrintcard"
        Me.cmdViewPrintcard.Size = New System.Drawing.Size(113, 48)
        Me.cmdViewPrintcard.TabIndex = 25
        Me.cmdViewPrintcard.Text = "View Printcard"
        Me.cmdViewPrintcard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdViewPrintcard.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(756, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 16)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Limit results:"
        '
        'cmbMonth
        '
        Me.cmbMonth.BackColor = System.Drawing.Color.White
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.Enabled = False
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", "All Year Round"})
        Me.cmbMonth.Location = New System.Drawing.Point(965, 7)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(135, 24)
        Me.cmbMonth.TabIndex = 32
        Me.cmbMonth.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(912, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 16)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Month:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(1110, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 16)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Year:"
        '
        'txtYear
        '
        Me.txtYear.BackColor = System.Drawing.Color.White
        Me.txtYear.Enabled = False
        Me.txtYear.Location = New System.Drawing.Point(1156, 8)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(66, 22)
        Me.txtYear.TabIndex = 33
        Me.txtYear.TabStop = False
        '
        'cmbNumRecords
        '
        Me.cmbNumRecords.BackColor = System.Drawing.Color.White
        Me.cmbNumRecords.FormattingEnabled = True
        Me.cmbNumRecords.Items.AddRange(New Object() {"10", "20", "30", "40", "50", "100", "150", "200", "250", "300", "350", "400", "450", "500", "600", "700", "800", "900", "1000", "1200", "1300", "1400", "1500", "1600", "1700", "1800", "1900", "2000"})
        Me.cmbNumRecords.Location = New System.Drawing.Point(842, 7)
        Me.cmbNumRecords.Name = "cmbNumRecords"
        Me.cmbNumRecords.Size = New System.Drawing.Size(64, 24)
        Me.cmbNumRecords.TabIndex = 34
        Me.cmbNumRecords.TabStop = False
        '
        'gridColumn
        '
        Me.gridColumn.BackColor = System.Drawing.Color.White
        Me.gridColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.gridColumn.FormattingEnabled = True
        Me.gridColumn.Location = New System.Drawing.Point(91, 6)
        Me.gridColumn.Name = "gridColumn"
        Me.gridColumn.Size = New System.Drawing.Size(122, 24)
        Me.gridColumn.TabIndex = 35
        Me.gridColumn.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(13, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Search by:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(219, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 16)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Text to search:"
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.Color.White
        Me.cmdSearch.BackgroundImage = CType(resources.GetObject("cmdSearch.BackgroundImage"), System.Drawing.Image)
        Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(576, 1)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(55, 33)
        Me.cmdSearch.TabIndex = 36
        Me.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'lCountRows
        '
        Me.lCountRows.AutoSize = True
        Me.lCountRows.BackColor = System.Drawing.Color.Transparent
        Me.lCountRows.Location = New System.Drawing.Point(293, 549)
        Me.lCountRows.Name = "lCountRows"
        Me.lCountRows.Size = New System.Drawing.Size(49, 16)
        Me.lCountRows.TabIndex = 37
        Me.lCountRows.Text = "Label1"
        '
        'lCurrentSel
        '
        Me.lCurrentSel.AutoSize = True
        Me.lCurrentSel.BackColor = System.Drawing.Color.Transparent
        Me.lCurrentSel.Location = New System.Drawing.Point(293, 574)
        Me.lCurrentSel.Name = "lCurrentSel"
        Me.lCurrentSel.Size = New System.Drawing.Size(49, 16)
        Me.lCurrentSel.TabIndex = 37
        Me.lCurrentSel.Text = "Label1"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.White
        Me.cmdRefresh.BackgroundImage = CType(resources.GetObject("cmdRefresh.BackgroundImage"), System.Drawing.Image)
        Me.cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
        Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRefresh.Location = New System.Drawing.Point(11, 5)
        Me.cmdRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(113, 48)
        Me.cmdRefresh.TabIndex = 45
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 360000
        '
        'cmdSearchDB
        '
        Me.cmdSearchDB.BackColor = System.Drawing.Color.White
        Me.cmdSearchDB.BackgroundImage = CType(resources.GetObject("cmdSearchDB.BackgroundImage"), System.Drawing.Image)
        Me.cmdSearchDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSearchDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDB.Image = CType(resources.GetObject("cmdSearchDB.Image"), System.Drawing.Image)
        Me.cmdSearchDB.Location = New System.Drawing.Point(637, 1)
        Me.cmdSearchDB.Name = "cmdSearchDB"
        Me.cmdSearchDB.Size = New System.Drawing.Size(61, 33)
        Me.cmdSearchDB.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.cmdSearchDB, "Search Database")
        Me.cmdSearchDB.UseVisualStyleBackColor = False
        '
        'cmdViewCustomerFile
        '
        Me.cmdViewCustomerFile.BackColor = System.Drawing.Color.White
        Me.cmdViewCustomerFile.BackgroundImage = CType(resources.GetObject("cmdViewCustomerFile.BackgroundImage"), System.Drawing.Image)
        Me.cmdViewCustomerFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdViewCustomerFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdViewCustomerFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdViewCustomerFile.Image = CType(resources.GetObject("cmdViewCustomerFile.Image"), System.Drawing.Image)
        Me.cmdViewCustomerFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdViewCustomerFile.Location = New System.Drawing.Point(136, 7)
        Me.cmdViewCustomerFile.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdViewCustomerFile.Name = "cmdViewCustomerFile"
        Me.cmdViewCustomerFile.Size = New System.Drawing.Size(113, 48)
        Me.cmdViewCustomerFile.TabIndex = 20
        Me.cmdViewCustomerFile.Text = "Customer File"
        Me.cmdViewCustomerFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdViewCustomerFile.UseVisualStyleBackColor = False
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.Color.White
        Me.cmdExport.BackgroundImage = CType(resources.GetObject("cmdExport.BackgroundImage"), System.Drawing.Image)
        Me.cmdExport.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExport.Image = CType(resources.GetObject("cmdExport.Image"), System.Drawing.Image)
        Me.cmdExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdExport.Location = New System.Drawing.Point(132, 5)
        Me.cmdExport.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(113, 48)
        Me.cmdExport.TabIndex = 50
        Me.cmdExport.Text = "Export to Excel"
        Me.cmdExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'cmdEditPrintcard
        '
        Me.cmdEditPrintcard.BackColor = System.Drawing.Color.White
        Me.cmdEditPrintcard.BackgroundImage = CType(resources.GetObject("cmdEditPrintcard.BackgroundImage"), System.Drawing.Image)
        Me.cmdEditPrintcard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdEditPrintcard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdEditPrintcard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditPrintcard.Image = CType(resources.GetObject("cmdEditPrintcard.Image"), System.Drawing.Image)
        Me.cmdEditPrintcard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEditPrintcard.Location = New System.Drawing.Point(499, 7)
        Me.cmdEditPrintcard.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdEditPrintcard.Name = "cmdEditPrintcard"
        Me.cmdEditPrintcard.Size = New System.Drawing.Size(113, 48)
        Me.cmdEditPrintcard.TabIndex = 35
        Me.cmdEditPrintcard.Text = "Edit Printcard"
        Me.cmdEditPrintcard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdEditPrintcard.UseVisualStyleBackColor = False
        '
        'PanelFunction1
        '
        Me.PanelFunction1.BackColor = System.Drawing.Color.Transparent
        Me.PanelFunction1.Controls.Add(Me.cmdCreateOrder)
        Me.PanelFunction1.Controls.Add(Me.cmdCreateCopy)
        Me.PanelFunction1.Controls.Add(Me.cmdViewPrintcard)
        Me.PanelFunction1.Controls.Add(Me.cmdViewCustomerFile)
        Me.PanelFunction1.Controls.Add(Me.cmdSetPrice)
        Me.PanelFunction1.Controls.Add(Me.cmdEditPrintcard)
        Me.PanelFunction1.Location = New System.Drawing.Point(474, 540)
        Me.PanelFunction1.Name = "PanelFunction1"
        Me.PanelFunction1.Size = New System.Drawing.Size(748, 62)
        Me.PanelFunction1.TabIndex = 56
        '
        'cmdCreateOrder
        '
        Me.cmdCreateOrder.BackColor = System.Drawing.Color.White
        Me.cmdCreateOrder.BackgroundImage = CType(resources.GetObject("cmdCreateOrder.BackgroundImage"), System.Drawing.Image)
        Me.cmdCreateOrder.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCreateOrder.Enabled = False
        Me.cmdCreateOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdCreateOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCreateOrder.Image = CType(resources.GetObject("cmdCreateOrder.Image"), System.Drawing.Image)
        Me.cmdCreateOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCreateOrder.Location = New System.Drawing.Point(15, 7)
        Me.cmdCreateOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdCreateOrder.Name = "cmdCreateOrder"
        Me.cmdCreateOrder.Size = New System.Drawing.Size(113, 48)
        Me.cmdCreateOrder.TabIndex = 18
        Me.cmdCreateOrder.Text = "Sales Order"
        Me.cmdCreateOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdCreateOrder.UseVisualStyleBackColor = False
        '
        'cmdSetPrice
        '
        Me.cmdSetPrice.BackColor = System.Drawing.Color.White
        Me.cmdSetPrice.BackgroundImage = CType(resources.GetObject("cmdSetPrice.BackgroundImage"), System.Drawing.Image)
        Me.cmdSetPrice.Enabled = False
        Me.cmdSetPrice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki
        Me.cmdSetPrice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGoldenrod
        Me.cmdSetPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSetPrice.Image = CType(resources.GetObject("cmdSetPrice.Image"), System.Drawing.Image)
        Me.cmdSetPrice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSetPrice.Location = New System.Drawing.Point(620, 7)
        Me.cmdSetPrice.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSetPrice.Name = "cmdSetPrice"
        Me.cmdSetPrice.Size = New System.Drawing.Size(113, 48)
        Me.cmdSetPrice.TabIndex = 38
        Me.cmdSetPrice.Text = "Set Price"
        Me.cmdSetPrice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdSetPrice.UseVisualStyleBackColor = False
        '
        'PanelUtilities
        '
        Me.PanelUtilities.BackColor = System.Drawing.Color.Transparent
        Me.PanelUtilities.Controls.Add(Me.cmdExport)
        Me.PanelUtilities.Controls.Add(Me.cmdRefresh)
        Me.PanelUtilities.Location = New System.Drawing.Point(15, 542)
        Me.PanelUtilities.Name = "PanelUtilities"
        Me.PanelUtilities.Size = New System.Drawing.Size(256, 58)
        Me.PanelUtilities.TabIndex = 57
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(297, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(297, 15)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "(Enter * and press Search DB to search all printcards)"
        '
        'chkDisplayHint
        '
        Me.chkDisplayHint.AutoSize = True
        Me.chkDisplayHint.Location = New System.Drawing.Point(16, 33)
        Me.chkDisplayHint.Name = "chkDisplayHint"
        Me.chkDisplayHint.Size = New System.Drawing.Size(106, 20)
        Me.chkDisplayHint.TabIndex = 59
        Me.chkDisplayHint.Text = "Display Hints"
        Me.chkDisplayHint.UseVisualStyleBackColor = True
        '
        'chkSeachByDate
        '
        Me.chkSeachByDate.AutoSize = True
        Me.chkSeachByDate.Location = New System.Drawing.Point(962, 33)
        Me.chkSeachByDate.Name = "chkSeachByDate"
        Me.chkSeachByDate.Size = New System.Drawing.Size(167, 20)
        Me.chkSeachByDate.TabIndex = 59
        Me.chkSeachByDate.Text = "Include Search By Date"
        Me.chkSeachByDate.UseVisualStyleBackColor = True
        '
        'BrowsePrintcard
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(1235, 610)
        Me.Controls.Add(Me.chkDisplayHint)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.chkSeachByDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PanelUtilities)
        Me.Controls.Add(Me.lCurrentSel)
        Me.Controls.Add(Me.gridColumn)
        Me.Controls.Add(Me.PanelFunction1)
        Me.Controls.Add(Me.lCountRows)
        Me.Controls.Add(Me.cmdSearchDB)
        Me.Controls.Add(Me.cmbNumRecords)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.cmbMonth)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cSearchThis)
        Me.Controls.Add(Me.PrintcardGrid)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "BrowsePrintcard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Browse Printcard"
        CType(Me.PrintcardGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.PanelFunction1.ResumeLayout(False)
        Me.PanelUtilities.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PrintcardGrid As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCreateCopy As System.Windows.Forms.Button
    Friend WithEvents cSearchThis As System.Windows.Forms.TextBox
    Friend WithEvents cmdViewPrintcard As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents cmbNumRecords As System.Windows.Forms.ComboBox
    Friend WithEvents gridColumn As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents lCountRows As System.Windows.Forms.Label
    Friend WithEvents lCurrentSel As System.Windows.Forms.Label
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmdSearchDB As System.Windows.Forms.Button
    Friend WithEvents cmdViewCustomerFile As System.Windows.Forms.Button
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents cmdEditPrintcard As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExportToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewCustomerFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewPrintcardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateCopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditPrintcardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeletePrintcardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmailToCustomerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewPlateUsageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelFunction1 As System.Windows.Forms.Panel
    Friend WithEvents PanelUtilities As System.Windows.Forms.Panel
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdCreateOrder As System.Windows.Forms.Button
    Friend WithEvents cmdSetPrice As System.Windows.Forms.Button
    Friend WithEvents SetPriceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkDisplayHint As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeachByDate As System.Windows.Forms.CheckBox
    Friend WithEvents printcardid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fileid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents organization_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents box_description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents insidedimension As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents boardsize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents printcardno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rubber_location As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents diecut_number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents racklocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents testid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents test_type_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents date_created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents filetype As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents color4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents boardtypeid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fluteid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents jointid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents combinationid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dimensionid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents scaleid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_file_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customerfile As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customerfiletype As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents printcopyno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents filename As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents deleted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents notes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents printcard_status_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents scale_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents boxcategory As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
