<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AssignRubberdie
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AssignRubberdie))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.CustomerAssignRubberdie = New System.Windows.Forms.DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.organization_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.box_description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printcardno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.boardsize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.insidedimension = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CustomerAssignRubberdie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(529, 310)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(195, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.White
        Me.OK_Button.BackgroundImage = CType(resources.GetObject("OK_Button.BackgroundImage"), System.Drawing.Image)
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.OK_Button.Location = New System.Drawing.Point(4, 4)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(89, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.BackColor = System.Drawing.Color.White
        Me.Cancel_Button.BackgroundImage = CType(resources.GetObject("Cancel_Button.BackgroundImage"), System.Drawing.Image)
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Cancel_Button.Location = New System.Drawing.Point(101, 4)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'CustomerAssignRubberdie
        '
        Me.CustomerAssignRubberdie.AllowUserToAddRows = False
        Me.CustomerAssignRubberdie.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.CustomerAssignRubberdie.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.CustomerAssignRubberdie.BackgroundColor = System.Drawing.Color.White
        Me.CustomerAssignRubberdie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomerAssignRubberdie.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.organization_name, Me.box_description, Me.printcardno, Me.boardsize, Me.insidedimension, Me.printnum})
        Me.CustomerAssignRubberdie.Location = New System.Drawing.Point(12, 12)
        Me.CustomerAssignRubberdie.MultiSelect = False
        Me.CustomerAssignRubberdie.Name = "CustomerAssignRubberdie"
        Me.CustomerAssignRubberdie.ReadOnly = True
        Me.CustomerAssignRubberdie.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CustomerAssignRubberdie.Size = New System.Drawing.Size(712, 287)
        Me.CustomerAssignRubberdie.TabIndex = 1
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "Printcard ID"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
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
        Me.box_description.HeaderText = "Description"
        Me.box_description.Name = "box_description"
        Me.box_description.ReadOnly = True
        Me.box_description.Width = 200
        '
        'printcardno
        '
        Me.printcardno.DataPropertyName = "printcardno"
        Me.printcardno.HeaderText = "Printcard Number"
        Me.printcardno.Name = "printcardno"
        Me.printcardno.ReadOnly = True
        '
        'boardsize
        '
        Me.boardsize.DataPropertyName = "boardsize"
        Me.boardsize.HeaderText = "Board Size"
        Me.boardsize.Name = "boardsize"
        Me.boardsize.ReadOnly = True
        '
        'insidedimension
        '
        Me.insidedimension.DataPropertyName = "insidedimension"
        Me.insidedimension.HeaderText = "I.D."
        Me.insidedimension.Name = "insidedimension"
        Me.insidedimension.ReadOnly = True
        '
        'printnum
        '
        Me.printnum.DataPropertyName = "printnum"
        Me.printnum.HeaderText = "Printcard Num(INT)"
        Me.printnum.Name = "printnum"
        Me.printnum.ReadOnly = True
        Me.printnum.Visible = False
        '
        'AssignRubberdie
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(736, 360)
        Me.Controls.Add(Me.CustomerAssignRubberdie)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AssignRubberdie"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Assign Rubberdie"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.CustomerAssignRubberdie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents CustomerAssignRubberdie As System.Windows.Forms.DataGridView
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents organization_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents box_description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents printcardno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents boardsize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents insidedimension As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents printnum As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
