<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetCombination
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.gridPaperCombination = New System.Windows.Forms.DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.liner1_val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.medium1_val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.liner2_val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.medium2_val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.liner3_val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.liner1_color_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.gridPaperCombination, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(200, 391)
        Me.cmdOK.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(121, 39)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "&OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'gridPaperCombination
        '
        Me.gridPaperCombination.AllowUserToAddRows = False
        Me.gridPaperCombination.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        Me.gridPaperCombination.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.gridPaperCombination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridPaperCombination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridPaperCombination.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.liner1_val, Me.medium1_val, Me.liner2_val, Me.medium2_val, Me.liner3_val, Me.liner1_color_id})
        Me.gridPaperCombination.Location = New System.Drawing.Point(7, 45)
        Me.gridPaperCombination.Margin = New System.Windows.Forms.Padding(5)
        Me.gridPaperCombination.MultiSelect = False
        Me.gridPaperCombination.Name = "gridPaperCombination"
        Me.gridPaperCombination.ReadOnly = True
        Me.gridPaperCombination.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridPaperCombination.Size = New System.Drawing.Size(506, 336)
        Me.gridPaperCombination.TabIndex = 3
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "ID"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 50
        '
        'liner1_val
        '
        Me.liner1_val.DataPropertyName = "liner1_val"
        Me.liner1_val.HeaderText = "Liner 1"
        Me.liner1_val.Name = "liner1_val"
        Me.liner1_val.ReadOnly = True
        Me.liner1_val.Width = 90
        '
        'medium1_val
        '
        Me.medium1_val.DataPropertyName = "medium1_val"
        Me.medium1_val.HeaderText = "Medium 1"
        Me.medium1_val.Name = "medium1_val"
        Me.medium1_val.ReadOnly = True
        Me.medium1_val.Width = 90
        '
        'liner2_val
        '
        Me.liner2_val.DataPropertyName = "liner2_val"
        Me.liner2_val.HeaderText = "Liner 2"
        Me.liner2_val.Name = "liner2_val"
        Me.liner2_val.ReadOnly = True
        Me.liner2_val.Width = 90
        '
        'medium2_val
        '
        Me.medium2_val.DataPropertyName = "medium2_val"
        Me.medium2_val.HeaderText = "Medium 2"
        Me.medium2_val.Name = "medium2_val"
        Me.medium2_val.ReadOnly = True
        Me.medium2_val.Width = 90
        '
        'liner3_val
        '
        Me.liner3_val.DataPropertyName = "liner3_val"
        Me.liner3_val.HeaderText = "Liner 3"
        Me.liner3_val.Name = "liner3_val"
        Me.liner3_val.ReadOnly = True
        Me.liner3_val.Width = 90
        '
        'liner1_color_id
        '
        Me.liner1_color_id.DataPropertyName = "liner1_color_id"
        Me.liner1_color_id.HeaderText = "Color"
        Me.liner1_color_id.Name = "liner1_color_id"
        Me.liner1_color_id.ReadOnly = True
        Me.liner1_color_id.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(306, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Note: For White Kraft datagrid background is white."
        '
        'GetCombination
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 444)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.gridPaperCombination)
        Me.Controls.Add(Me.cmdOK)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GetCombination"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Get Paper Combination"
        CType(Me.gridPaperCombination, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents gridPaperCombination As System.Windows.Forms.DataGridView
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents liner1_val As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents medium1_val As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents liner2_val As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents medium2_val As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents liner3_val As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents liner1_color_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
