<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateOrderForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateOrderForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ORDER_CustomerName = New System.Windows.Forms.TextBox()
        Me.ORDER_BoxDescription = New System.Windows.Forms.TextBox()
        Me.ORDER_PrintcardNumber = New System.Windows.Forms.TextBox()
        Me.cmdSaveOrder = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ORDER_Quantity = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ORDER_DeliverAddress = New System.Windows.Forms.TextBox()
        Me.TabControlEX1 = New Dotnetrix.Controls.TabControlEX()
        Me.TabPageEX1 = New Dotnetrix.Controls.TabPageEX()
        Me.ORDER_Currency = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ORDER_SalesOrderNumber = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ORDER_TotalCost = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.ORDER_CustomerPONumber = New System.Windows.Forms.TextBox()
        Me.ORDER_VariableCost = New System.Windows.Forms.TextBox()
        Me.ORDER_ItemPrice = New System.Windows.Forms.TextBox()
        Me.TabPageEX2 = New Dotnetrix.Controls.TabPageEX()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabPageEX3 = New Dotnetrix.Controls.TabPageEX()
        Me.TabControlEX1.SuspendLayout()
        Me.TabPageEX1.SuspendLayout()
        Me.TabPageEX2.SuspendLayout()
        Me.TabPageEX3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(91, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(51, 207)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Box Description:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(66, 239)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Plate Number:"
        '
        'ORDER_CustomerName
        '
        Me.ORDER_CustomerName.BackColor = System.Drawing.Color.White
        Me.ORDER_CustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_CustomerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_CustomerName.ForeColor = System.Drawing.Color.Black
        Me.ORDER_CustomerName.Location = New System.Drawing.Point(172, 70)
        Me.ORDER_CustomerName.Name = "ORDER_CustomerName"
        Me.ORDER_CustomerName.ReadOnly = True
        Me.ORDER_CustomerName.Size = New System.Drawing.Size(533, 24)
        Me.ORDER_CustomerName.TabIndex = 1
        Me.ORDER_CustomerName.TabStop = False
        '
        'ORDER_BoxDescription
        '
        Me.ORDER_BoxDescription.BackColor = System.Drawing.Color.White
        Me.ORDER_BoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_BoxDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_BoxDescription.ForeColor = System.Drawing.Color.Black
        Me.ORDER_BoxDescription.Location = New System.Drawing.Point(172, 201)
        Me.ORDER_BoxDescription.Name = "ORDER_BoxDescription"
        Me.ORDER_BoxDescription.ReadOnly = True
        Me.ORDER_BoxDescription.Size = New System.Drawing.Size(533, 24)
        Me.ORDER_BoxDescription.TabIndex = 1
        Me.ORDER_BoxDescription.TabStop = False
        '
        'ORDER_PrintcardNumber
        '
        Me.ORDER_PrintcardNumber.BackColor = System.Drawing.Color.White
        Me.ORDER_PrintcardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_PrintcardNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_PrintcardNumber.ForeColor = System.Drawing.Color.Black
        Me.ORDER_PrintcardNumber.Location = New System.Drawing.Point(172, 233)
        Me.ORDER_PrintcardNumber.Name = "ORDER_PrintcardNumber"
        Me.ORDER_PrintcardNumber.ReadOnly = True
        Me.ORDER_PrintcardNumber.Size = New System.Drawing.Size(533, 24)
        Me.ORDER_PrintcardNumber.TabIndex = 1
        Me.ORDER_PrintcardNumber.TabStop = False
        '
        'cmdSaveOrder
        '
        Me.cmdSaveOrder.BackColor = System.Drawing.Color.White
        Me.cmdSaveOrder.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.cmdSaveOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGreen
        Me.cmdSaveOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen
        Me.cmdSaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveOrder.ForeColor = System.Drawing.Color.Black
        Me.cmdSaveOrder.Location = New System.Drawing.Point(482, 392)
        Me.cmdSaveOrder.Name = "cmdSaveOrder"
        Me.cmdSaveOrder.Size = New System.Drawing.Size(112, 53)
        Me.cmdSaveOrder.TabIndex = 20
        Me.cmdSaveOrder.Text = "Create Order"
        Me.cmdSaveOrder.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.White
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.cmdCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ForeColor = System.Drawing.Color.Black
        Me.cmdCancel.Location = New System.Drawing.Point(624, 392)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(112, 53)
        Me.cmdCancel.TabIndex = 30
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(90, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 18)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Deliver To:"
        '
        'ORDER_Quantity
        '
        Me.ORDER_Quantity.BackColor = System.Drawing.Color.White
        Me.ORDER_Quantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_Quantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_Quantity.ForeColor = System.Drawing.Color.Black
        Me.ORDER_Quantity.Location = New System.Drawing.Point(172, 367)
        Me.ORDER_Quantity.Name = "ORDER_Quantity"
        Me.ORDER_Quantity.Size = New System.Drawing.Size(533, 26)
        Me.ORDER_Quantity.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(102, 375)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 18)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Quantity:"
        '
        'ORDER_DeliverAddress
        '
        Me.ORDER_DeliverAddress.BackColor = System.Drawing.Color.White
        Me.ORDER_DeliverAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_DeliverAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_DeliverAddress.ForeColor = System.Drawing.Color.Black
        Me.ORDER_DeliverAddress.Location = New System.Drawing.Point(172, 102)
        Me.ORDER_DeliverAddress.Multiline = True
        Me.ORDER_DeliverAddress.Name = "ORDER_DeliverAddress"
        Me.ORDER_DeliverAddress.ReadOnly = True
        Me.ORDER_DeliverAddress.Size = New System.Drawing.Size(533, 91)
        Me.ORDER_DeliverAddress.TabIndex = 1
        Me.ORDER_DeliverAddress.TabStop = False
        '
        'TabControlEX1
        '
        Me.TabControlEX1.BackColor = System.Drawing.Color.Transparent
        Me.TabControlEX1.Controls.Add(Me.TabPageEX1)
        Me.TabControlEX1.Controls.Add(Me.TabPageEX2)
        Me.TabControlEX1.Controls.Add(Me.TabPageEX3)
        Me.TabControlEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlEX1.FlatBorderColor = System.Drawing.Color.Black
        Me.TabControlEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlEX1.ForeColor = System.Drawing.Color.Black
        Me.TabControlEX1.HotColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TabControlEX1.HotTrack = True
        Me.TabControlEX1.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.TabControlEX1.ItemSize = New System.Drawing.Size(180, 30)
        Me.TabControlEX1.Location = New System.Drawing.Point(0, 0)
        Me.TabControlEX1.Multiline = True
        Me.TabControlEX1.Name = "TabControlEX1"
        Me.TabControlEX1.SelectedIndex = 0
        Me.TabControlEX1.SelectedTabColor = System.Drawing.Color.Transparent
        Me.TabControlEX1.SelectedTabFontStyle = System.Drawing.FontStyle.Bold
        Me.TabControlEX1.Size = New System.Drawing.Size(784, 525)
        Me.TabControlEX1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControlEX1.TabColor = System.Drawing.Color.Transparent
        Me.TabControlEX1.TabIndex = 31
        '
        'TabPageEX1
        '
        Me.TabPageEX1.Controls.Add(Me.Label3)
        Me.TabPageEX1.Controls.Add(Me.ORDER_Currency)
        Me.TabPageEX1.Controls.Add(Me.Label19)
        Me.TabPageEX1.Controls.Add(Me.ORDER_SalesOrderNumber)
        Me.TabPageEX1.Controls.Add(Me.Label18)
        Me.TabPageEX1.Controls.Add(Me.Label1)
        Me.TabPageEX1.Controls.Add(Me.Label5)
        Me.TabPageEX1.Controls.Add(Me.ORDER_TotalCost)
        Me.TabPageEX1.Controls.Add(Me.ORDER_BoxDescription)
        Me.TabPageEX1.Controls.Add(Me.ORDER_CustomerName)
        Me.TabPageEX1.Controls.Add(Me.ORDER_DeliverAddress)
        Me.TabPageEX1.Controls.Add(Me.ORDER_PrintcardNumber)
        Me.TabPageEX1.Controls.Add(Me.Label2)
        Me.TabPageEX1.Controls.Add(Me.Label20)
        Me.TabPageEX1.Controls.Add(Me.Label21)
        Me.TabPageEX1.Controls.Add(Me.Label4)
        Me.TabPageEX1.Controls.Add(Me.Label22)
        Me.TabPageEX1.Controls.Add(Me.Label6)
        Me.TabPageEX1.Controls.Add(Me.ORDER_CustomerPONumber)
        Me.TabPageEX1.Controls.Add(Me.ORDER_VariableCost)
        Me.TabPageEX1.Controls.Add(Me.ORDER_ItemPrice)
        Me.TabPageEX1.Controls.Add(Me.ORDER_Quantity)
        Me.TabPageEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageEX1.ForeColor = System.Drawing.Color.Black
        Me.TabPageEX1.Location = New System.Drawing.Point(4, 34)
        Me.TabPageEX1.Name = "TabPageEX1"
        Me.TabPageEX1.Size = New System.Drawing.Size(776, 487)
        Me.TabPageEX1.TabIndex = 0
        Me.TabPageEX1.Text = "P.O. Description"
        '
        'ORDER_Currency
        '
        Me.ORDER_Currency.AutoSize = True
        Me.ORDER_Currency.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_Currency.ForeColor = System.Drawing.Color.Black
        Me.ORDER_Currency.Location = New System.Drawing.Point(170, 40)
        Me.ORDER_Currency.Name = "ORDER_Currency"
        Me.ORDER_Currency.Size = New System.Drawing.Size(68, 18)
        Me.ORDER_Currency.TabIndex = 0
        Me.ORDER_Currency.Text = "Currency"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(90, 40)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 18)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Currency:"
        '
        'ORDER_SalesOrderNumber
        '
        Me.ORDER_SalesOrderNumber.AutoSize = True
        Me.ORDER_SalesOrderNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_SalesOrderNumber.ForeColor = System.Drawing.Color.Black
        Me.ORDER_SalesOrderNumber.Location = New System.Drawing.Point(169, 15)
        Me.ORDER_SalesOrderNumber.Name = "ORDER_SalesOrderNumber"
        Me.ORDER_SalesOrderNumber.Size = New System.Drawing.Size(136, 18)
        Me.ORDER_SalesOrderNumber.TabIndex = 0
        Me.ORDER_SalesOrderNumber.Text = "SalesOrderNumber"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(84, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(78, 18)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Order No.:"
        '
        'ORDER_TotalCost
        '
        Me.ORDER_TotalCost.BackColor = System.Drawing.Color.White
        Me.ORDER_TotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_TotalCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_TotalCost.ForeColor = System.Drawing.Color.Black
        Me.ORDER_TotalCost.Location = New System.Drawing.Point(172, 401)
        Me.ORDER_TotalCost.Name = "ORDER_TotalCost"
        Me.ORDER_TotalCost.ReadOnly = True
        Me.ORDER_TotalCost.Size = New System.Drawing.Size(533, 26)
        Me.ORDER_TotalCost.TabIndex = 1
        Me.ORDER_TotalCost.TabStop = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(28, 273)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(140, 18)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Customer P.O. No.:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(68, 341)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 18)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Variable Cost:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(122, 307)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 18)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Price:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(115, 405)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(51, 18)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Total:"
        '
        'ORDER_CustomerPONumber
        '
        Me.ORDER_CustomerPONumber.BackColor = System.Drawing.Color.White
        Me.ORDER_CustomerPONumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_CustomerPONumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_CustomerPONumber.ForeColor = System.Drawing.Color.Black
        Me.ORDER_CustomerPONumber.Location = New System.Drawing.Point(172, 265)
        Me.ORDER_CustomerPONumber.Name = "ORDER_CustomerPONumber"
        Me.ORDER_CustomerPONumber.Size = New System.Drawing.Size(533, 26)
        Me.ORDER_CustomerPONumber.TabIndex = 9
        '
        'ORDER_VariableCost
        '
        Me.ORDER_VariableCost.BackColor = System.Drawing.Color.White
        Me.ORDER_VariableCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_VariableCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_VariableCost.ForeColor = System.Drawing.Color.Black
        Me.ORDER_VariableCost.Location = New System.Drawing.Point(172, 333)
        Me.ORDER_VariableCost.Name = "ORDER_VariableCost"
        Me.ORDER_VariableCost.Size = New System.Drawing.Size(533, 26)
        Me.ORDER_VariableCost.TabIndex = 11
        '
        'ORDER_ItemPrice
        '
        Me.ORDER_ItemPrice.BackColor = System.Drawing.Color.White
        Me.ORDER_ItemPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ORDER_ItemPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ORDER_ItemPrice.ForeColor = System.Drawing.Color.Black
        Me.ORDER_ItemPrice.Location = New System.Drawing.Point(172, 299)
        Me.ORDER_ItemPrice.Name = "ORDER_ItemPrice"
        Me.ORDER_ItemPrice.Size = New System.Drawing.Size(533, 26)
        Me.ORDER_ItemPrice.TabIndex = 10
        '
        'TabPageEX2
        '
        Me.TabPageEX2.Controls.Add(Me.Label17)
        Me.TabPageEX2.Controls.Add(Me.Label16)
        Me.TabPageEX2.Controls.Add(Me.Label15)
        Me.TabPageEX2.Controls.Add(Me.Label14)
        Me.TabPageEX2.Controls.Add(Me.Label13)
        Me.TabPageEX2.Controls.Add(Me.Label12)
        Me.TabPageEX2.Controls.Add(Me.Label11)
        Me.TabPageEX2.Controls.Add(Me.Label10)
        Me.TabPageEX2.Controls.Add(Me.Label9)
        Me.TabPageEX2.Controls.Add(Me.Label7)
        Me.TabPageEX2.Controls.Add(Me.Label8)
        Me.TabPageEX2.ForeColor = System.Drawing.Color.Black
        Me.TabPageEX2.Location = New System.Drawing.Point(4, 34)
        Me.TabPageEX2.Name = "TabPageEX2"
        Me.TabPageEX2.Size = New System.Drawing.Size(776, 487)
        Me.TabPageEX2.TabIndex = 1
        Me.TabPageEX2.Text = "Board Specifications"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(71, 385)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(117, 18)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "Add Instructions:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(71, 352)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(106, 18)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Machine route:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(461, 168)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(92, 18)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "Actual Value:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(483, 136)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(97, 18)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Weight/piece:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(71, 289)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(85, 18)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Board Size:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(71, 234)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 18)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Creases:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(69, 200)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(125, 18)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Inside Dimension:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(69, 168)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(129, 18)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Pieces per bundle:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(69, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(114, 18)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Number per set:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(69, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 18)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Closure Type:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(69, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(130, 18)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Box Code/Format:"
        '
        'TabPageEX3
        '
        Me.TabPageEX3.Controls.Add(Me.cmdCancel)
        Me.TabPageEX3.Controls.Add(Me.cmdSaveOrder)
        Me.TabPageEX3.ForeColor = System.Drawing.Color.Black
        Me.TabPageEX3.Location = New System.Drawing.Point(4, 34)
        Me.TabPageEX3.Name = "TabPageEX3"
        Me.TabPageEX3.Size = New System.Drawing.Size(776, 487)
        Me.TabPageEX3.TabIndex = 2
        Me.TabPageEX3.Text = "Order Preview"
        '
        'CreateOrderForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(784, 525)
        Me.Controls.Add(Me.TabControlEX1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CreateOrderForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create Order"
        Me.TabControlEX1.ResumeLayout(False)
        Me.TabPageEX1.ResumeLayout(False)
        Me.TabPageEX1.PerformLayout()
        Me.TabPageEX2.ResumeLayout(False)
        Me.TabPageEX2.PerformLayout()
        Me.TabPageEX3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ORDER_CustomerName As System.Windows.Forms.TextBox
    Friend WithEvents ORDER_BoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents ORDER_PrintcardNumber As System.Windows.Forms.TextBox
    Friend WithEvents cmdSaveOrder As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ORDER_Quantity As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ORDER_DeliverAddress As System.Windows.Forms.TextBox
    Friend WithEvents TabControlEX1 As Dotnetrix.Controls.TabControlEX
    Friend WithEvents TabPageEX1 As Dotnetrix.Controls.TabPageEX
    Friend WithEvents TabPageEX2 As Dotnetrix.Controls.TabPageEX
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TabPageEX3 As Dotnetrix.Controls.TabPageEX
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ORDER_ItemPrice As System.Windows.Forms.TextBox
    Friend WithEvents ORDER_SalesOrderNumber As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ORDER_CustomerPONumber As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ORDER_VariableCost As System.Windows.Forms.TextBox
    Friend WithEvents ORDER_TotalCost As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents ORDER_Currency As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
