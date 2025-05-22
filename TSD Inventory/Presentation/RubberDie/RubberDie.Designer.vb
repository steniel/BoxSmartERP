<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RubberDieCreate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RubberDieCreate))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.GroupInputRubberdie = New System.Windows.Forms.GroupBox()
        Me.labelPrintNum = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CustID = New System.Windows.Forms.Label()
        Me.dDateRepair = New System.Windows.Forms.DateTimePicker()
        Me.dDateMounted = New System.Windows.Forms.DateTimePicker()
        Me.dDateCreated = New System.Windows.Forms.DateTimePicker()
        Me.tMaxLife = New System.Windows.Forms.TextBox()
        Me.tBoxCount = New System.Windows.Forms.TextBox()
        Me.tdescription = New System.Windows.Forms.TextBox()
        Me.cmdAddLoc = New System.Windows.Forms.Button()
        Me.cmbOrder = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.cmbRack = New System.Windows.Forms.ComboBox()
        Me.tRubberdieNumber = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RubberdieGrid = New System.Windows.Forms.DataGridView()
        Me.rubber_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.organization_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.die_number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rubberdie_string_num = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.boxes_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ageing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.date_created = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.date_mounted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.date_repair = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rack = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rubberdie_racks_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.MountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RepairToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RepairHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MountHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MovementHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupSearch = New System.Windows.Forms.GroupBox()
        Me.cmbNumRecords = New System.Windows.Forms.ComboBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmdSearchDB = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.gridColumn = New System.Windows.Forms.ComboBox()
        Me.cSearchThis = New System.Windows.Forms.TextBox()
        Me.GroupInputRubberdie.SuspendLayout()
        CType(Me.RubberdieGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.Color.White
        Me.cmdCancel.BackgroundImage = CType(resources.GetObject("cmdCancel.BackgroundImage"), System.Drawing.Image)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(9, 551)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(100, 40)
        Me.cmdCancel.TabIndex = 56
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.BackColor = System.Drawing.Color.White
        Me.cmdAdd.BackgroundImage = CType(resources.GetObject("cmdAdd.BackgroundImage"), System.Drawing.Image)
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAdd.Location = New System.Drawing.Point(245, 551)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(100, 40)
        Me.cmdAdd.TabIndex = 50
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.Color.White
        Me.cmdSave.BackgroundImage = CType(resources.GetObject("cmdSave.BackgroundImage"), System.Drawing.Image)
        Me.cmdSave.Enabled = False
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(129, 551)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(100, 40)
        Me.cmdSave.TabIndex = 52
        Me.cmdSave.Text = "Save"
        Me.cmdSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'GroupInputRubberdie
        '
        Me.GroupInputRubberdie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupInputRubberdie.BackColor = System.Drawing.Color.Transparent
        Me.GroupInputRubberdie.Controls.Add(Me.labelPrintNum)
        Me.GroupInputRubberdie.Controls.Add(Me.Label10)
        Me.GroupInputRubberdie.Controls.Add(Me.CustID)
        Me.GroupInputRubberdie.Controls.Add(Me.dDateRepair)
        Me.GroupInputRubberdie.Controls.Add(Me.dDateMounted)
        Me.GroupInputRubberdie.Controls.Add(Me.dDateCreated)
        Me.GroupInputRubberdie.Controls.Add(Me.tMaxLife)
        Me.GroupInputRubberdie.Controls.Add(Me.tBoxCount)
        Me.GroupInputRubberdie.Controls.Add(Me.tdescription)
        Me.GroupInputRubberdie.Controls.Add(Me.cmdAddLoc)
        Me.GroupInputRubberdie.Controls.Add(Me.cmbOrder)
        Me.GroupInputRubberdie.Controls.Add(Me.cmbStatus)
        Me.GroupInputRubberdie.Controls.Add(Me.cmbCustomer)
        Me.GroupInputRubberdie.Controls.Add(Me.cmbRack)
        Me.GroupInputRubberdie.Controls.Add(Me.tRubberdieNumber)
        Me.GroupInputRubberdie.Controls.Add(Me.Label8)
        Me.GroupInputRubberdie.Controls.Add(Me.Label9)
        Me.GroupInputRubberdie.Controls.Add(Me.Label7)
        Me.GroupInputRubberdie.Controls.Add(Me.Label6)
        Me.GroupInputRubberdie.Controls.Add(Me.Label4)
        Me.GroupInputRubberdie.Controls.Add(Me.Label3)
        Me.GroupInputRubberdie.Controls.Add(Me.Label2)
        Me.GroupInputRubberdie.Controls.Add(Me.Label5)
        Me.GroupInputRubberdie.Controls.Add(Me.Label1)
        Me.GroupInputRubberdie.Enabled = False
        Me.GroupInputRubberdie.Location = New System.Drawing.Point(14, 213)
        Me.GroupInputRubberdie.Name = "GroupInputRubberdie"
        Me.GroupInputRubberdie.Size = New System.Drawing.Size(326, 332)
        Me.GroupInputRubberdie.TabIndex = 1
        Me.GroupInputRubberdie.TabStop = False
        Me.GroupInputRubberdie.Text = "Details"
        '
        'labelPrintNum
        '
        Me.labelPrintNum.AutoSize = True
        Me.labelPrintNum.Location = New System.Drawing.Point(62, 8)
        Me.labelPrintNum.Name = "labelPrintNum"
        Me.labelPrintNum.Size = New System.Drawing.Size(56, 16)
        Me.labelPrintNum.TabIndex = 30
        Me.labelPrintNum.Text = "Label16"
        Me.labelPrintNum.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(26, 286)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Date Repair:"
        '
        'CustID
        '
        Me.CustID.AutoSize = True
        Me.CustID.BackColor = System.Drawing.Color.Transparent
        Me.CustID.Location = New System.Drawing.Point(11, 286)
        Me.CustID.Name = "CustID"
        Me.CustID.Size = New System.Drawing.Size(56, 16)
        Me.CustID.TabIndex = 29
        Me.CustID.Text = "Label11"
        Me.CustID.Visible = False
        '
        'dDateRepair
        '
        Me.dDateRepair.CalendarForeColor = System.Drawing.Color.Black
        Me.dDateRepair.CalendarMonthBackground = System.Drawing.Color.White
        Me.dDateRepair.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dDateRepair.Location = New System.Drawing.Point(118, 283)
        Me.dDateRepair.Name = "dDateRepair"
        Me.dDateRepair.Size = New System.Drawing.Size(195, 22)
        Me.dDateRepair.TabIndex = 28
        '
        'dDateMounted
        '
        Me.dDateMounted.CalendarForeColor = System.Drawing.Color.Black
        Me.dDateMounted.CalendarMonthBackground = System.Drawing.Color.White
        Me.dDateMounted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dDateMounted.Location = New System.Drawing.Point(118, 255)
        Me.dDateMounted.Name = "dDateMounted"
        Me.dDateMounted.Size = New System.Drawing.Size(195, 22)
        Me.dDateMounted.TabIndex = 26
        '
        'dDateCreated
        '
        Me.dDateCreated.CalendarForeColor = System.Drawing.Color.Black
        Me.dDateCreated.CalendarMonthBackground = System.Drawing.Color.White
        Me.dDateCreated.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dDateCreated.Location = New System.Drawing.Point(118, 227)
        Me.dDateCreated.Name = "dDateCreated"
        Me.dDateCreated.Size = New System.Drawing.Size(195, 22)
        Me.dDateCreated.TabIndex = 24
        '
        'tMaxLife
        '
        Me.tMaxLife.BackColor = System.Drawing.Color.White
        Me.tMaxLife.ForeColor = System.Drawing.Color.Black
        Me.tMaxLife.Location = New System.Drawing.Point(118, 113)
        Me.tMaxLife.Name = "tMaxLife"
        Me.tMaxLife.Size = New System.Drawing.Size(195, 22)
        Me.tMaxLife.TabIndex = 16
        '
        'tBoxCount
        '
        Me.tBoxCount.BackColor = System.Drawing.Color.White
        Me.tBoxCount.ForeColor = System.Drawing.Color.Black
        Me.tBoxCount.Location = New System.Drawing.Point(118, 141)
        Me.tBoxCount.Name = "tBoxCount"
        Me.tBoxCount.Size = New System.Drawing.Size(195, 22)
        Me.tBoxCount.TabIndex = 18
        Me.tBoxCount.Text = "0"
        '
        'tdescription
        '
        Me.tdescription.BackColor = System.Drawing.Color.White
        Me.tdescription.ForeColor = System.Drawing.Color.Black
        Me.tdescription.Location = New System.Drawing.Point(118, 57)
        Me.tdescription.Name = "tdescription"
        Me.tdescription.Size = New System.Drawing.Size(195, 22)
        Me.tdescription.TabIndex = 12
        '
        'cmdAddLoc
        '
        Me.cmdAddLoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdAddLoc.Location = New System.Drawing.Point(270, 169)
        Me.cmdAddLoc.Name = "cmdAddLoc"
        Me.cmdAddLoc.Size = New System.Drawing.Size(43, 24)
        Me.cmdAddLoc.TabIndex = 21
        Me.cmdAddLoc.Text = "+"
        Me.cmdAddLoc.UseVisualStyleBackColor = True
        '
        'cmbOrder
        '
        Me.cmbOrder.BackColor = System.Drawing.Color.White
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.ForeColor = System.Drawing.Color.Black
        Me.cmbOrder.FormattingEnabled = True
        Me.cmbOrder.Items.AddRange(New Object() {"ASC", "DESC"})
        Me.cmbOrder.Location = New System.Drawing.Point(214, 169)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.Size = New System.Drawing.Size(50, 24)
        Me.cmbOrder.TabIndex = 24
        Me.cmbOrder.TabStop = False
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.Color.White
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(118, 198)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(195, 24)
        Me.cmbStatus.TabIndex = 22
        '
        'cmbCustomer
        '
        Me.cmbCustomer.BackColor = System.Drawing.Color.White
        Me.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCustomer.ForeColor = System.Drawing.Color.Black
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(118, 27)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(195, 24)
        Me.cmbCustomer.TabIndex = 10
        '
        'cmbRack
        '
        Me.cmbRack.BackColor = System.Drawing.Color.White
        Me.cmbRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRack.ForeColor = System.Drawing.Color.Black
        Me.cmbRack.FormattingEnabled = True
        Me.cmbRack.Location = New System.Drawing.Point(118, 169)
        Me.cmbRack.Name = "cmbRack"
        Me.cmbRack.Size = New System.Drawing.Size(90, 24)
        Me.cmbRack.TabIndex = 20
        '
        'tRubberdieNumber
        '
        Me.tRubberdieNumber.BackColor = System.Drawing.Color.White
        Me.tRubberdieNumber.ForeColor = System.Drawing.Color.Black
        Me.tRubberdieNumber.Location = New System.Drawing.Point(118, 85)
        Me.tRubberdieNumber.Name = "tRubberdieNumber"
        Me.tRubberdieNumber.Size = New System.Drawing.Size(195, 22)
        Me.tRubberdieNumber.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(15, 258)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 16)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Date Mounted:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(19, 230)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Date Created:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(20, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Ageing(days):"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(28, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Boxes count:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(44, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Customer:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(62, 202)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Status:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(13, 173)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Rack Location:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(33, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Description:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(22, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Rubber Die #:"
        '
        'RubberdieGrid
        '
        Me.RubberdieGrid.AllowUserToAddRows = False
        Me.RubberdieGrid.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.RubberdieGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.RubberdieGrid.BackgroundColor = System.Drawing.Color.Silver
        Me.RubberdieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RubberdieGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.rubber_id, Me.organization_name, Me.description, Me.status, Me.die_number, Me.rubberdie_string_num, Me.boxes_count, Me.ageing, Me.date_created, Me.date_mounted, Me.date_repair, Me.rack, Me.rubberdie_racks_id})
        Me.RubberdieGrid.ContextMenuStrip = Me.ContextMenuStrip1
        Me.RubberdieGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RubberdieGrid.GridColor = System.Drawing.Color.Black
        Me.RubberdieGrid.Location = New System.Drawing.Point(0, 0)
        Me.RubberdieGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.RubberdieGrid.MultiSelect = False
        Me.RubberdieGrid.Name = "RubberdieGrid"
        Me.RubberdieGrid.ReadOnly = True
        Me.RubberdieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.RubberdieGrid.Size = New System.Drawing.Size(888, 603)
        Me.RubberdieGrid.TabIndex = 2
        '
        'rubber_id
        '
        Me.rubber_id.DataPropertyName = "rubber_id"
        Me.rubber_id.HeaderText = "ID"
        Me.rubber_id.Name = "rubber_id"
        Me.rubber_id.ReadOnly = True
        Me.rubber_id.Visible = False
        Me.rubber_id.Width = 50
        '
        'organization_name
        '
        Me.organization_name.DataPropertyName = "organization_name"
        Me.organization_name.HeaderText = "Customer"
        Me.organization_name.Name = "organization_name"
        Me.organization_name.ReadOnly = True
        Me.organization_name.Width = 150
        '
        'description
        '
        Me.description.DataPropertyName = "description"
        Me.description.HeaderText = "Description"
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.Width = 200
        '
        'status
        '
        Me.status.DataPropertyName = "status"
        Me.status.HeaderText = "Status"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        '
        'die_number
        '
        Me.die_number.DataPropertyName = "die_number"
        Me.die_number.HeaderText = "Die #"
        Me.die_number.Name = "die_number"
        Me.die_number.ReadOnly = True
        Me.die_number.Width = 50
        '
        'rubberdie_string_num
        '
        Me.rubberdie_string_num.DataPropertyName = "rubberdie_string_num"
        Me.rubberdie_string_num.HeaderText = "Printcard No."
        Me.rubberdie_string_num.Name = "rubberdie_string_num"
        Me.rubberdie_string_num.ReadOnly = True
        '
        'boxes_count
        '
        Me.boxes_count.DataPropertyName = "boxes_count"
        Me.boxes_count.HeaderText = "Boxes Run"
        Me.boxes_count.Name = "boxes_count"
        Me.boxes_count.ReadOnly = True
        Me.boxes_count.Width = 50
        '
        'ageing
        '
        Me.ageing.DataPropertyName = "ageing"
        Me.ageing.HeaderText = "Ageing(Days)"
        Me.ageing.Name = "ageing"
        Me.ageing.ReadOnly = True
        Me.ageing.Width = 50
        '
        'date_created
        '
        Me.date_created.DataPropertyName = "date_created"
        Me.date_created.HeaderText = "Date Created"
        Me.date_created.Name = "date_created"
        Me.date_created.ReadOnly = True
        '
        'date_mounted
        '
        Me.date_mounted.DataPropertyName = "date_mounted"
        Me.date_mounted.HeaderText = "Mount Date"
        Me.date_mounted.Name = "date_mounted"
        Me.date_mounted.ReadOnly = True
        '
        'date_repair
        '
        Me.date_repair.DataPropertyName = "date_repair"
        Me.date_repair.HeaderText = "Date Repair"
        Me.date_repair.Name = "date_repair"
        Me.date_repair.ReadOnly = True
        '
        'rack
        '
        Me.rack.DataPropertyName = "rack"
        Me.rack.HeaderText = "Rack Location"
        Me.rack.Name = "rack"
        Me.rack.ReadOnly = True
        Me.rack.Width = 50
        '
        'rubberdie_racks_id
        '
        Me.rubberdie_racks_id.DataPropertyName = "rubberdie_racks_id"
        Me.rubberdie_racks_id.HeaderText = "Rack ID"
        Me.rubberdie_racks_id.Name = "rubberdie_racks_id"
        Me.rubberdie_racks_id.ReadOnly = True
        Me.rubberdie_racks_id.Visible = False
        Me.rubberdie_racks_id.Width = 50
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackgroundImage = CType(resources.GetObject("ContextMenuStrip1.BackgroundImage"), System.Drawing.Image)
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SearchToolStripMenuItem, Me.EditToolStrip, Me.ToolStripMenuItem2, Me.RepairHistoryToolStripMenuItem, Me.MountHistoryToolStripMenuItem, Me.MovementHistoryToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.ToolStripMenuItem1, Me.CancelToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(192, 170)
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Image = CType(resources.GetObject("SearchToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SearchToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.SearchToolStripMenuItem.Text = "Search"
        '
        'EditToolStrip
        '
        Me.EditToolStrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MountToolStripMenuItem, Me.RepairToolStripMenuItem, Me.MoveToolStripMenuItem, Me.StatusToolStripMenuItem})
        Me.EditToolStrip.Name = "EditToolStrip"
        Me.EditToolStrip.Size = New System.Drawing.Size(191, 22)
        Me.EditToolStrip.Text = "Edit"
        '
        'MountToolStripMenuItem
        '
        Me.MountToolStripMenuItem.Name = "MountToolStripMenuItem"
        Me.MountToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.MountToolStripMenuItem.Text = "Mount"
        '
        'RepairToolStripMenuItem
        '
        Me.RepairToolStripMenuItem.Name = "RepairToolStripMenuItem"
        Me.RepairToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.RepairToolStripMenuItem.Text = "Repair"
        '
        'MoveToolStripMenuItem
        '
        Me.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem"
        Me.MoveToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.MoveToolStripMenuItem.Text = "Move"
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
        Me.StatusToolStripMenuItem.Text = "Status"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(188, 6)
        '
        'RepairHistoryToolStripMenuItem
        '
        Me.RepairHistoryToolStripMenuItem.Name = "RepairHistoryToolStripMenuItem"
        Me.RepairHistoryToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.RepairHistoryToolStripMenuItem.Text = "Repair History"
        '
        'MountHistoryToolStripMenuItem
        '
        Me.MountHistoryToolStripMenuItem.Name = "MountHistoryToolStripMenuItem"
        Me.MountHistoryToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.MountHistoryToolStripMenuItem.Text = "Mount History"
        '
        'MovementHistoryToolStripMenuItem
        '
        Me.MovementHistoryToolStripMenuItem.Name = "MovementHistoryToolStripMenuItem"
        Me.MovementHistoryToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.MovementHistoryToolStripMenuItem.Text = "Movement History"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(188, 6)
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.Image = CType(resources.GetObject("CancelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.CancelToolStripMenuItem.Text = "Prope&rties"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSave)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupInputRubberdie)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdAdd)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdCancel)
        Me.SplitContainer1.Panel1MinSize = 355
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Panel2.Controls.Add(Me.RubberdieGrid)
        Me.SplitContainer1.Panel2MinSize = 600
        Me.SplitContainer1.Size = New System.Drawing.Size(1247, 603)
        Me.SplitContainer1.SplitterDistance = 355
        Me.SplitContainer1.TabIndex = 57
        '
        'GroupSearch
        '
        Me.GroupSearch.Controls.Add(Me.cmbNumRecords)
        Me.GroupSearch.Controls.Add(Me.txtYear)
        Me.GroupSearch.Controls.Add(Me.cmbMonth)
        Me.GroupSearch.Controls.Add(Me.Label11)
        Me.GroupSearch.Controls.Add(Me.Label12)
        Me.GroupSearch.Controls.Add(Me.Label13)
        Me.GroupSearch.Controls.Add(Me.cmdSearchDB)
        Me.GroupSearch.Controls.Add(Me.Label14)
        Me.GroupSearch.Controls.Add(Me.cmdSearch)
        Me.GroupSearch.Controls.Add(Me.Label15)
        Me.GroupSearch.Controls.Add(Me.gridColumn)
        Me.GroupSearch.Controls.Add(Me.cSearchThis)
        Me.GroupSearch.Enabled = False
        Me.GroupSearch.Location = New System.Drawing.Point(14, 0)
        Me.GroupSearch.Name = "GroupSearch"
        Me.GroupSearch.Size = New System.Drawing.Size(326, 207)
        Me.GroupSearch.TabIndex = 41
        Me.GroupSearch.TabStop = False
        Me.GroupSearch.Text = "Search"
        '
        'cmbNumRecords
        '
        Me.cmbNumRecords.FormattingEnabled = True
        Me.cmbNumRecords.Items.AddRange(New Object() {"10", "20", "30", "40", "50", "100", "150", "200", "250", "300", "350", "400", "450", "500", "600", "700", "800", "900", "1000", "1200", "1300", "1400", "1500", "1600", "1700", "1800", "1900", "2000"})
        Me.cmbNumRecords.Location = New System.Drawing.Point(114, 108)
        Me.cmbNumRecords.Name = "cmbNumRecords"
        Me.cmbNumRecords.Size = New System.Drawing.Size(204, 24)
        Me.cmbNumRecords.TabIndex = 48
        Me.cmbNumRecords.TabStop = False
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(114, 80)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(205, 22)
        Me.txtYear.TabIndex = 47
        Me.txtYear.TabStop = False
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", "All Year Round"})
        Me.cmbMonth.Location = New System.Drawing.Point(114, 50)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(204, 24)
        Me.cmbMonth.TabIndex = 46
        Me.cmbMonth.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(60, 83)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 16)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Year:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(53, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 16)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "Month:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(20, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 16)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Limit results:"
        '
        'cmdSearchDB
        '
        Me.cmdSearchDB.BackColor = System.Drawing.Color.White
        Me.cmdSearchDB.BackgroundImage = CType(resources.GetObject("cmdSearchDB.BackgroundImage"), System.Drawing.Image)
        Me.cmdSearchDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSearchDB.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDB.Image = CType(resources.GetObject("cmdSearchDB.Image"), System.Drawing.Image)
        Me.cmdSearchDB.Location = New System.Drawing.Point(263, 166)
        Me.cmdSearchDB.Name = "cmdSearchDB"
        Me.cmdSearchDB.Size = New System.Drawing.Size(55, 33)
        Me.cmdSearchDB.TabIndex = 42
        Me.cmdSearchDB.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(28, 22)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 16)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "Search by:"
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.Color.White
        Me.cmdSearch.BackgroundImage = CType(resources.GetObject("cmdSearch.BackgroundImage"), System.Drawing.Image)
        Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(114, 166)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(55, 33)
        Me.cmdSearch.TabIndex = 41
        Me.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(5, 141)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(95, 16)
        Me.Label15.TabIndex = 39
        Me.Label15.Text = "Text to search:"
        '
        'gridColumn
        '
        Me.gridColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.gridColumn.FormattingEnabled = True
        Me.gridColumn.Location = New System.Drawing.Point(114, 19)
        Me.gridColumn.Margin = New System.Windows.Forms.Padding(4)
        Me.gridColumn.Name = "gridColumn"
        Me.gridColumn.Size = New System.Drawing.Size(204, 24)
        Me.gridColumn.TabIndex = 37
        Me.gridColumn.TabStop = False
        '
        'cSearchThis
        '
        Me.cSearchThis.Location = New System.Drawing.Point(114, 138)
        Me.cSearchThis.Name = "cSearchThis"
        Me.cSearchThis.Size = New System.Drawing.Size(205, 22)
        Me.cSearchThis.TabIndex = 38
        '
        'RubberDieCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1247, 603)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "RubberDieCreate"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Rubberdie Management"
        Me.GroupInputRubberdie.ResumeLayout(False)
        Me.GroupInputRubberdie.PerformLayout()
        CType(Me.RubberdieGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupSearch.ResumeLayout(False)
        Me.GroupSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupInputRubberdie As System.Windows.Forms.GroupBox
    Friend WithEvents cmdAddLoc As System.Windows.Forms.Button
    Friend WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRack As System.Windows.Forms.ComboBox
    Friend WithEvents tRubberdieNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RubberdieGrid As System.Windows.Forms.DataGridView
    Friend WithEvents tdescription As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tMaxLife As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dDateCreated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tBoxCount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dDateMounted As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dDateRepair As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CustID As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RepairHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MountHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CancelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MovementHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStrip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RepairToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupSearch As System.Windows.Forms.GroupBox
    Friend WithEvents cmbNumRecords As System.Windows.Forms.ComboBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmdSearchDB As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents gridColumn As System.Windows.Forms.ComboBox
    Friend WithEvents cSearchThis As System.Windows.Forms.TextBox
    Friend WithEvents labelPrintNum As System.Windows.Forms.Label
    Friend WithEvents rubber_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents organization_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents die_number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rubberdie_string_num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents boxes_count As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ageing As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents date_created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents date_mounted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents date_repair As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rubberdie_racks_id As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
