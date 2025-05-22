<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomerSetup
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
        Me.grCustomer = New System.Windows.Forms.GroupBox()
        Me.lNewCustomerID = New System.Windows.Forms.Label()
        Me.cmbIndustryType = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tPrintcardPrefix = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tEmail = New System.Windows.Forms.TextBox()
        Me.tMobileNumber = New System.Windows.Forms.TextBox()
        Me.tOfficeTelephone = New System.Windows.Forms.TextBox()
        Me.tLastname = New System.Windows.Forms.TextBox()
        Me.tFirstname = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.tCountryName = New System.Windows.Forms.TextBox()
        Me.tProvinceName = New System.Windows.Forms.TextBox()
        Me.tCityName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tBarangayName = New System.Windows.Forms.TextBox()
        Me.tStreetName = New System.Windows.Forms.TextBox()
        Me.tCustomerName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CustomerGrid = New System.Windows.Forms.DataGridView()
        Me.cxEnableDisableCustomer = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cxSubCustomerStatusText = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.chkShowInActiveCust = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tcompanyname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.street_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.barangay_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cityname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.province = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.country = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.firstname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lastname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.email = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.officephone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mobilephone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.printcard_prefix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.industry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.industryid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grCustomer.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.CustomerGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cxEnableDisableCustomer.SuspendLayout()
        Me.SuspendLayout()
        '
        'grCustomer
        '
        Me.grCustomer.Controls.Add(Me.lNewCustomerID)
        Me.grCustomer.Controls.Add(Me.cmbIndustryType)
        Me.grCustomer.Controls.Add(Me.GroupBox4)
        Me.grCustomer.Controls.Add(Me.GroupBox3)
        Me.grCustomer.Controls.Add(Me.tCustomerName)
        Me.grCustomer.Controls.Add(Me.Label8)
        Me.grCustomer.Controls.Add(Me.Label1)
        Me.grCustomer.Enabled = False
        Me.grCustomer.Location = New System.Drawing.Point(16, 13)
        Me.grCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.grCustomer.Name = "grCustomer"
        Me.grCustomer.Padding = New System.Windows.Forms.Padding(4)
        Me.grCustomer.Size = New System.Drawing.Size(912, 238)
        Me.grCustomer.TabIndex = 1
        Me.grCustomer.TabStop = False
        '
        'lNewCustomerID
        '
        Me.lNewCustomerID.AutoSize = True
        Me.lNewCustomerID.Location = New System.Drawing.Point(7, 7)
        Me.lNewCustomerID.Name = "lNewCustomerID"
        Me.lNewCustomerID.Size = New System.Drawing.Size(114, 16)
        Me.lNewCustomerID.TabIndex = 16
        Me.lNewCustomerID.Text = "New Customer ID:"
        Me.lNewCustomerID.Visible = False
        '
        'cmbIndustryType
        '
        Me.cmbIndustryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIndustryType.FormattingEnabled = True
        Me.cmbIndustryType.Location = New System.Drawing.Point(590, 19)
        Me.cmbIndustryType.Name = "cmbIndustryType"
        Me.cmbIndustryType.Size = New System.Drawing.Size(274, 24)
        Me.cmbIndustryType.TabIndex = 15
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.tPrintcardPrefix)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.tEmail)
        Me.GroupBox4.Controls.Add(Me.tMobileNumber)
        Me.GroupBox4.Controls.Add(Me.tOfficeTelephone)
        Me.GroupBox4.Controls.Add(Me.tLastname)
        Me.GroupBox4.Controls.Add(Me.tFirstname)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Location = New System.Drawing.Point(459, 48)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(411, 181)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Contact"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(73, 30)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Name:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(308, 9)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 15)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Lastname"
        '
        'tPrintcardPrefix
        '
        Me.tPrintcardPrefix.Location = New System.Drawing.Point(131, 144)
        Me.tPrintcardPrefix.Name = "tPrintcardPrefix"
        Me.tPrintcardPrefix.Size = New System.Drawing.Size(280, 22)
        Me.tPrintcardPrefix.TabIndex = 110
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 146)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 16)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Printcard Prefix:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(168, 9)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 15)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Firstname"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(76, 119)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Email:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 91)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Mobile Number:"
        '
        'tEmail
        '
        Me.tEmail.Location = New System.Drawing.Point(131, 116)
        Me.tEmail.Name = "tEmail"
        Me.tEmail.Size = New System.Drawing.Size(274, 22)
        Me.tEmail.TabIndex = 100
        '
        'tMobileNumber
        '
        Me.tMobileNumber.Location = New System.Drawing.Point(131, 88)
        Me.tMobileNumber.Name = "tMobileNumber"
        Me.tMobileNumber.Size = New System.Drawing.Size(274, 22)
        Me.tMobileNumber.TabIndex = 90
        '
        'tOfficeTelephone
        '
        Me.tOfficeTelephone.Location = New System.Drawing.Point(131, 58)
        Me.tOfficeTelephone.Name = "tOfficeTelephone"
        Me.tOfficeTelephone.Size = New System.Drawing.Size(274, 22)
        Me.tOfficeTelephone.TabIndex = 80
        '
        'tLastname
        '
        Me.tLastname.Location = New System.Drawing.Point(273, 27)
        Me.tLastname.Name = "tLastname"
        Me.tLastname.Size = New System.Drawing.Size(132, 22)
        Me.tLastname.TabIndex = 72
        '
        'tFirstname
        '
        Me.tFirstname.Location = New System.Drawing.Point(131, 27)
        Me.tFirstname.Name = "tFirstname"
        Me.tFirstname.Size = New System.Drawing.Size(136, 22)
        Me.tFirstname.TabIndex = 70
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 61)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(114, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Office Telephone:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.tCountryName)
        Me.GroupBox3.Controls.Add(Me.tProvinceName)
        Me.GroupBox3.Controls.Add(Me.tCityName)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.tBarangayName)
        Me.GroupBox3.Controls.Add(Me.tStreetName)
        Me.GroupBox3.Location = New System.Drawing.Point(42, 48)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(411, 181)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Address"
        '
        'tCountryName
        '
        Me.tCountryName.Location = New System.Drawing.Point(115, 133)
        Me.tCountryName.Name = "tCountryName"
        Me.tCountryName.Size = New System.Drawing.Size(274, 22)
        Me.tCountryName.TabIndex = 60
        '
        'tProvinceName
        '
        Me.tProvinceName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.tProvinceName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.tProvinceName.Location = New System.Drawing.Point(115, 105)
        Me.tProvinceName.Name = "tProvinceName"
        Me.tProvinceName.Size = New System.Drawing.Size(274, 22)
        Me.tProvinceName.TabIndex = 50
        '
        'tCityName
        '
        Me.tCityName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.tCityName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.tCityName.Location = New System.Drawing.Point(115, 77)
        Me.tCityName.Name = "tCityName"
        Me.tCityName.Size = New System.Drawing.Size(274, 22)
        Me.tCityName.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(51, 136)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Country:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 80)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "City/Town:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(37, 52)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 16)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Barangay:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(24, 24)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 16)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Street name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 108)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Province/State:"
        '
        'tBarangayName
        '
        Me.tBarangayName.Location = New System.Drawing.Point(115, 49)
        Me.tBarangayName.Name = "tBarangayName"
        Me.tBarangayName.Size = New System.Drawing.Size(274, 22)
        Me.tBarangayName.TabIndex = 30
        '
        'tStreetName
        '
        Me.tStreetName.Location = New System.Drawing.Point(115, 21)
        Me.tStreetName.Name = "tStreetName"
        Me.tStreetName.Size = New System.Drawing.Size(274, 22)
        Me.tStreetName.TabIndex = 20
        '
        'tCustomerName
        '
        Me.tCustomerName.Location = New System.Drawing.Point(157, 20)
        Me.tCustomerName.Name = "tCustomerName"
        Me.tCustomerName.Size = New System.Drawing.Size(274, 22)
        Me.tCustomerName.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(523, 20)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 16)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Industry:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Company Name:"
        '
        'CustomerGrid
        '
        Me.CustomerGrid.AllowUserToAddRows = False
        Me.CustomerGrid.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.CustomerGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.CustomerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomerGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.tcompanyname, Me.street_name, Me.barangay_name, Me.cityname, Me.province, Me.country, Me.firstname, Me.lastname, Me.email, Me.officephone, Me.mobilephone, Me.printcard_prefix, Me.industry, Me.industryid})
        Me.CustomerGrid.ContextMenuStrip = Me.cxEnableDisableCustomer
        Me.CustomerGrid.Location = New System.Drawing.Point(16, 279)
        Me.CustomerGrid.MultiSelect = False
        Me.CustomerGrid.Name = "CustomerGrid"
        Me.CustomerGrid.ReadOnly = True
        Me.CustomerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CustomerGrid.Size = New System.Drawing.Size(912, 268)
        Me.CustomerGrid.TabIndex = 150
        '
        'cxEnableDisableCustomer
        '
        Me.cxEnableDisableCustomer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cxSubCustomerStatusText, Me.ToolStripMenuItem1, Me.CancelToolStripMenuItem})
        Me.cxEnableDisableCustomer.Name = "cxEnableDisableCustomer"
        Me.cxEnableDisableCustomer.ShowCheckMargin = True
        Me.cxEnableDisableCustomer.Size = New System.Drawing.Size(209, 54)
        '
        'cxSubCustomerStatusText
        '
        Me.cxSubCustomerStatusText.Name = "cxSubCustomerStatusText"
        Me.cxSubCustomerStatusText.Size = New System.Drawing.Size(208, 22)
        Me.cxSubCustomerStatusText.Text = "Disable this customer"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(205, 6)
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.CancelToolStripMenuItem.Text = "Cancel"
        '
        'chkShowInActiveCust
        '
        Me.chkShowInActiveCust.AutoSize = True
        Me.chkShowInActiveCust.Location = New System.Drawing.Point(16, 258)
        Me.chkShowInActiveCust.Name = "chkShowInActiveCust"
        Me.chkShowInActiveCust.Size = New System.Drawing.Size(174, 20)
        Me.chkShowInActiveCust.TabIndex = 152
        Me.chkShowInActiveCust.Text = "Show inactive customers"
        Me.chkShowInActiveCust.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(217, 260)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(379, 16)
        Me.Label15.TabIndex = 153
        Me.Label15.Text = "Right-click to open menu for enabling and disabling customers."
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(264, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(329, 16)
        Me.Label16.TabIndex = 154
        Me.Label16.Text = "Customers with ID >= 400 needs to be updated."
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "ID"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 75
        '
        'tcompanyname
        '
        Me.tcompanyname.DataPropertyName = "tcompanyname"
        Me.tcompanyname.HeaderText = "Name"
        Me.tcompanyname.Name = "tcompanyname"
        Me.tcompanyname.ReadOnly = True
        Me.tcompanyname.Width = 250
        '
        'street_name
        '
        Me.street_name.DataPropertyName = "street_name"
        Me.street_name.HeaderText = "Street Name"
        Me.street_name.Name = "street_name"
        Me.street_name.ReadOnly = True
        Me.street_name.Visible = False
        '
        'barangay_name
        '
        Me.barangay_name.DataPropertyName = "barangay_name"
        Me.barangay_name.HeaderText = "Barangay"
        Me.barangay_name.Name = "barangay_name"
        Me.barangay_name.ReadOnly = True
        Me.barangay_name.Visible = False
        '
        'cityname
        '
        Me.cityname.DataPropertyName = "cityname"
        Me.cityname.HeaderText = "City / Town"
        Me.cityname.Name = "cityname"
        Me.cityname.ReadOnly = True
        '
        'province
        '
        Me.province.DataPropertyName = "province"
        Me.province.HeaderText = "Province"
        Me.province.Name = "province"
        Me.province.ReadOnly = True
        Me.province.Visible = False
        '
        'country
        '
        Me.country.DataPropertyName = "country"
        Me.country.HeaderText = "Country"
        Me.country.Name = "country"
        Me.country.ReadOnly = True
        '
        'firstname
        '
        Me.firstname.DataPropertyName = "firstname"
        Me.firstname.HeaderText = "Firstname"
        Me.firstname.Name = "firstname"
        Me.firstname.ReadOnly = True
        '
        'lastname
        '
        Me.lastname.DataPropertyName = "lastname"
        Me.lastname.HeaderText = "Lastname"
        Me.lastname.Name = "lastname"
        Me.lastname.ReadOnly = True
        '
        'email
        '
        Me.email.DataPropertyName = "email"
        Me.email.HeaderText = "Email Address"
        Me.email.Name = "email"
        Me.email.ReadOnly = True
        '
        'officephone
        '
        Me.officephone.DataPropertyName = "officephone"
        Me.officephone.HeaderText = "Office Tel. #"
        Me.officephone.Name = "officephone"
        Me.officephone.ReadOnly = True
        '
        'mobilephone
        '
        Me.mobilephone.DataPropertyName = "mobilephone"
        Me.mobilephone.HeaderText = "Mobile Phone No.#"
        Me.mobilephone.Name = "mobilephone"
        Me.mobilephone.ReadOnly = True
        '
        'printcard_prefix
        '
        Me.printcard_prefix.DataPropertyName = "printcard_prefix"
        Me.printcard_prefix.HeaderText = "Printcard Prefix"
        Me.printcard_prefix.Name = "printcard_prefix"
        Me.printcard_prefix.ReadOnly = True
        Me.printcard_prefix.Visible = False
        '
        'industry
        '
        Me.industry.DataPropertyName = "industry"
        Me.industry.HeaderText = "Industry Type"
        Me.industry.Name = "industry"
        Me.industry.ReadOnly = True
        Me.industry.Visible = False
        '
        'industryid
        '
        Me.industryid.DataPropertyName = "industryid"
        Me.industryid.HeaderText = "Industry ID"
        Me.industryid.Name = "industryid"
        Me.industryid.ReadOnly = True
        Me.industryid.Visible = False
        '
        'CustomerSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 561)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.grCustomer)
        Me.Controls.Add(Me.chkShowInActiveCust)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.CustomerGrid)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "CustomerSetup"
        Me.ShowIcon = False
        Me.Text = "Customer Setup"
        Me.grCustomer.ResumeLayout(False)
        Me.grCustomer.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.CustomerGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cxEnableDisableCustomer.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grCustomer As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tMobileNumber As System.Windows.Forms.TextBox
    Friend WithEvents tOfficeTelephone As System.Windows.Forms.TextBox
    Friend WithEvents tFirstname As System.Windows.Forms.TextBox
    Friend WithEvents CustomerGrid As System.Windows.Forms.DataGridView
    Friend WithEvents tBarangayName As System.Windows.Forms.TextBox
    Friend WithEvents tStreetName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tPrintcardPrefix As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tLastname As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbIndustryType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tCityName As System.Windows.Forms.TextBox
    Friend WithEvents tProvinceName As System.Windows.Forms.TextBox
    Friend WithEvents tCountryName As System.Windows.Forms.TextBox
    Friend WithEvents cxEnableDisableCustomer As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cxSubCustomerStatusText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CancelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkShowInActiveCust As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lNewCustomerID As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tcompanyname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents street_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents barangay_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cityname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents province As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents country As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents firstname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lastname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents officephone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mobilephone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents printcard_prefix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents industry As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents industryid As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
