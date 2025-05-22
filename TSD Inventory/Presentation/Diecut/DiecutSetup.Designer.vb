<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiecutSetup
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tDiecutNumber = New System.Windows.Forms.TextBox()
        Me.cmbRack = New System.Windows.Forms.ComboBox()
        Me.GroupInputDiecut = New System.Windows.Forms.GroupBox()
        Me.cmdAddLoc = New System.Windows.Forms.Button()
        Me.cmbOrder = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DiecutGrid = New System.Windows.Forms.DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.diecutnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rack = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rack_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.GroupInputDiecut.SuspendLayout()
        CType(Me.DiecutGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(47, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Diecut #:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Rack Location:"
        '
        'tDiecutNumber
        '
        Me.tDiecutNumber.Location = New System.Drawing.Point(112, 12)
        Me.tDiecutNumber.Name = "tDiecutNumber"
        Me.tDiecutNumber.Size = New System.Drawing.Size(169, 22)
        Me.tDiecutNumber.TabIndex = 0
        '
        'cmbRack
        '
        Me.cmbRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRack.FormattingEnabled = True
        Me.cmbRack.Location = New System.Drawing.Point(112, 42)
        Me.cmbRack.Name = "cmbRack"
        Me.cmbRack.Size = New System.Drawing.Size(170, 24)
        Me.cmbRack.TabIndex = 1
        '
        'GroupInputDiecut
        '
        Me.GroupInputDiecut.BackColor = System.Drawing.Color.Transparent
        Me.GroupInputDiecut.Controls.Add(Me.cmdAddLoc)
        Me.GroupInputDiecut.Controls.Add(Me.cmbOrder)
        Me.GroupInputDiecut.Controls.Add(Me.cmbRack)
        Me.GroupInputDiecut.Controls.Add(Me.tDiecutNumber)
        Me.GroupInputDiecut.Controls.Add(Me.Label3)
        Me.GroupInputDiecut.Controls.Add(Me.Label2)
        Me.GroupInputDiecut.Controls.Add(Me.Label1)
        Me.GroupInputDiecut.Enabled = False
        Me.GroupInputDiecut.Location = New System.Drawing.Point(15, 14)
        Me.GroupInputDiecut.Name = "GroupInputDiecut"
        Me.GroupInputDiecut.Size = New System.Drawing.Size(381, 105)
        Me.GroupInputDiecut.TabIndex = 1
        Me.GroupInputDiecut.TabStop = False
        '
        'cmdAddLoc
        '
        Me.cmdAddLoc.Location = New System.Drawing.Point(288, 43)
        Me.cmdAddLoc.Name = "cmdAddLoc"
        Me.cmdAddLoc.Size = New System.Drawing.Size(44, 23)
        Me.cmdAddLoc.TabIndex = 3
        Me.cmdAddLoc.Text = "+"
        Me.cmdAddLoc.UseVisualStyleBackColor = True
        '
        'cmbOrder
        '
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.FormattingEnabled = True
        Me.cmbOrder.Items.AddRange(New Object() {"ASCENDING", "DESCENDING"})
        Me.cmbOrder.Location = New System.Drawing.Point(112, 72)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.Size = New System.Drawing.Size(170, 24)
        Me.cmbOrder.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Rack Order:"
        '
        'DiecutGrid
        '
        Me.DiecutGrid.AllowUserToAddRows = False
        Me.DiecutGrid.AllowUserToDeleteRows = False
        Me.DiecutGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DiecutGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.diecutnum, Me.rack, Me.rack_id})
        Me.DiecutGrid.Location = New System.Drawing.Point(15, 126)
        Me.DiecutGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.DiecutGrid.MultiSelect = False
        Me.DiecutGrid.Name = "DiecutGrid"
        Me.DiecutGrid.ReadOnly = True
        Me.DiecutGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DiecutGrid.Size = New System.Drawing.Size(380, 260)
        Me.DiecutGrid.TabIndex = 0
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "ID"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 50
        '
        'diecutnum
        '
        Me.diecutnum.DataPropertyName = "diecutnum"
        Me.diecutnum.HeaderText = "Diecut Number"
        Me.diecutnum.Name = "diecutnum"
        Me.diecutnum.ReadOnly = True
        '
        'rack
        '
        Me.rack.DataPropertyName = "rack"
        Me.rack.HeaderText = "Rack Location"
        Me.rack.Name = "rack"
        Me.rack.ReadOnly = True
        '
        'rack_id
        '
        Me.rack_id.DataPropertyName = "rack_id"
        Me.rack_id.HeaderText = "Rack ID"
        Me.rack_id.Name = "rack_id"
        Me.rack_id.ReadOnly = True
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(208, 393)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(87, 31)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(301, 393)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(87, 31)
        Me.cmdAdd.TabIndex = 3
        Me.cmdAdd.Text = "ADD"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Location = New System.Drawing.Point(115, 393)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(87, 31)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.Text = "EDIT"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(22, 393)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(87, 31)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "CANCEL"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'DiecutSetup
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(410, 457)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.GroupInputDiecut)
        Me.Controls.Add(Me.DiecutGrid)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "DiecutSetup"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Diecut Setup"
        Me.GroupInputDiecut.ResumeLayout(False)
        Me.GroupInputDiecut.PerformLayout()
        CType(Me.DiecutGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tDiecutNumber As System.Windows.Forms.TextBox
    Friend WithEvents cmbRack As System.Windows.Forms.ComboBox
    Friend WithEvents GroupInputDiecut As System.Windows.Forms.GroupBox
    Friend WithEvents DiecutGrid As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents diecutnum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rack_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdAddLoc As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbOrder As System.Windows.Forms.ComboBox
End Class
