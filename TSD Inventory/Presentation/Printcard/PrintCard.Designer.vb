<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintCard))
        Me.lDiecut = New System.Windows.Forms.Label()
        Me.lScaleID = New System.Windows.Forms.Label()
        Me.lBoxFormatId = New System.Windows.Forms.Label()
        Me.lUnitID = New System.Windows.Forms.Label()
        Me.UploadPrintcardFile = New System.Windows.Forms.OpenFileDialog()
        Me.cPrintcardCreated = New System.Windows.Forms.DateTimePicker()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.tCustomerFile = New System.Windows.Forms.TextBox()
        Me.cmdBrowseCustFile = New System.Windows.Forms.Button()
        Me.tCustomerName = New System.Windows.Forms.TextBox()
        Me.tPrintcardNotes = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tFilePath = New System.Windows.Forms.TextBox()
        Me.cmdBrowseFile = New System.Windows.Forms.Button()
        Me.GetCustomer = New System.Windows.Forms.Button()
        Me.tBoxDescription = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tPrincardPrefix = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tPrintcardNumber = New System.Windows.Forms.TextBox()
        Me.cmbBoxFormat = New System.Windows.Forms.ComboBox()
        Me.lDimensionID = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbJoint = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbBoardType = New System.Windows.Forms.ComboBox()
        Me.cmbTest = New System.Windows.Forms.ComboBox()
        Me.cmdGetCombination = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tPaperCombination = New System.Windows.Forms.TextBox()
        Me.tOuterLiner = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmdRefrehDiecut = New System.Windows.Forms.Button()
        Me.cmbOrient = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tHeight = New System.Windows.Forms.TextBox()
        Me.tColor1 = New System.Windows.Forms.TextBox()
        Me.tBoardLength = New System.Windows.Forms.TextBox()
        Me.tWidth = New System.Windows.Forms.TextBox()
        Me.tPanel4 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.boardID = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.tBoardWidth = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.tGlueTab = New System.Windows.Forms.TextBox()
        Me.cmbScale = New System.Windows.Forms.ComboBox()
        Me.cmbDiecut = New System.Windows.Forms.ComboBox()
        Me.tFlap = New System.Windows.Forms.TextBox()
        Me.tLength = New System.Windows.Forms.TextBox()
        Me.tColor4 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tBoxHeight = New System.Windows.Forms.TextBox()
        Me.tPanel1 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tPanel3 = New System.Windows.Forms.TextBox()
        Me.tPanel2 = New System.Windows.Forms.TextBox()
        Me.tColor3 = New System.Windows.Forms.TextBox()
        Me.tColor2 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PSI_id = New System.Windows.Forms.Label()
        Me.cmbUnits = New System.Windows.Forms.ComboBox()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.PrintcardTabControl = New System.Windows.Forms.TabControl()
        Me.TabCustomer = New System.Windows.Forms.TabPage()
        Me.cmbPrintcardStatus = New System.Windows.Forms.ComboBox()
        Me.GetPrintNumList = New System.Windows.Forms.Button()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TabBoardSpecs = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.cmbTestType = New System.Windows.Forms.ComboBox()
        Me.TabBoxDetails = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BoxDimensionGroup = New System.Windows.Forms.GroupBox()
        Me.ColorGroup = New System.Windows.Forms.GroupBox()
        Me.cPartitionGroup = New System.Windows.Forms.GroupBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Board2Length = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Board2Width = New System.Windows.Forms.TextBox()
        Me.Board1Length = New System.Windows.Forms.TextBox()
        Me.Board1Width = New System.Windows.Forms.TextBox()
        Me.TabPrintcardPreview = New System.Windows.Forms.TabPage()
        Me.TabCustomerFile = New System.Windows.Forms.TabPage()
        Me.WebCustomerFile = New System.Windows.Forms.WebBrowser()
        Me.AxPrintcardFile = New AxFOXITREADERLib.AxFoxitCtl()
        Me.AxCustomerFile = New AxFOXITREADERLib.AxFoxitCtl()
        Me.cmbBoxCategory = New System.Windows.Forms.ComboBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.PrintcardTabControl.SuspendLayout()
        Me.TabCustomer.SuspendLayout()
        Me.TabBoardSpecs.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabBoxDetails.SuspendLayout()
        Me.BoxDimensionGroup.SuspendLayout()
        Me.ColorGroup.SuspendLayout()
        Me.cPartitionGroup.SuspendLayout()
        Me.TabPrintcardPreview.SuspendLayout()
        Me.TabCustomerFile.SuspendLayout()
        CType(Me.AxPrintcardFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxCustomerFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'lDiecut
        '
        Me.lDiecut.AutoSize = True
        Me.lDiecut.BackColor = System.Drawing.Color.Transparent
        Me.lDiecut.Location = New System.Drawing.Point(1188, 19)
        Me.lDiecut.Name = "lDiecut"
        Me.lDiecut.Size = New System.Drawing.Size(65, 16)
        Me.lDiecut.TabIndex = 142
        Me.lDiecut.Text = "Diecut ID:"
        Me.lDiecut.Visible = False
        '
        'lScaleID
        '
        Me.lScaleID.AutoSize = True
        Me.lScaleID.BackColor = System.Drawing.Color.Transparent
        Me.lScaleID.Location = New System.Drawing.Point(1122, 19)
        Me.lScaleID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lScaleID.Name = "lScaleID"
        Me.lScaleID.Size = New System.Drawing.Size(59, 16)
        Me.lScaleID.TabIndex = 0
        Me.lScaleID.Text = "ScaleID:"
        Me.lScaleID.Visible = False
        '
        'lBoxFormatId
        '
        Me.lBoxFormatId.AutoSize = True
        Me.lBoxFormatId.BackColor = System.Drawing.Color.Transparent
        Me.lBoxFormatId.Location = New System.Drawing.Point(1071, 35)
        Me.lBoxFormatId.Name = "lBoxFormatId"
        Me.lBoxFormatId.Size = New System.Drawing.Size(56, 16)
        Me.lBoxFormatId.TabIndex = 3
        Me.lBoxFormatId.Text = "Label17"
        Me.lBoxFormatId.Visible = False
        '
        'lUnitID
        '
        Me.lUnitID.AutoSize = True
        Me.lUnitID.BackColor = System.Drawing.Color.Transparent
        Me.lUnitID.Location = New System.Drawing.Point(1071, 19)
        Me.lUnitID.Name = "lUnitID"
        Me.lUnitID.Size = New System.Drawing.Size(44, 16)
        Me.lUnitID.TabIndex = 141
        Me.lUnitID.Text = "UnitID"
        Me.lUnitID.Visible = False
        '
        'UploadPrintcardFile
        '
        Me.UploadPrintcardFile.FileName = "OpenFileDialog1"
        '
        'cPrintcardCreated
        '
        Me.cPrintcardCreated.Location = New System.Drawing.Point(137, 295)
        Me.cPrintcardCreated.Name = "cPrintcardCreated"
        Me.cPrintcardCreated.Size = New System.Drawing.Size(228, 22)
        Me.cPrintcardCreated.TabIndex = 146
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Location = New System.Drawing.Point(9, 298)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(112, 16)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Date of Approval:"
        '
        'tCustomerFile
        '
        Me.tCustomerFile.BackColor = System.Drawing.Color.White
        Me.tCustomerFile.Location = New System.Drawing.Point(137, 121)
        Me.tCustomerFile.Name = "tCustomerFile"
        Me.tCustomerFile.ReadOnly = True
        Me.tCustomerFile.Size = New System.Drawing.Size(714, 22)
        Me.tCustomerFile.TabIndex = 180
        '
        'cmdBrowseCustFile
        '
        Me.cmdBrowseCustFile.ForeColor = System.Drawing.Color.Black
        Me.cmdBrowseCustFile.Location = New System.Drawing.Point(33, 117)
        Me.cmdBrowseCustFile.Name = "cmdBrowseCustFile"
        Me.cmdBrowseCustFile.Size = New System.Drawing.Size(98, 30)
        Me.cmdBrowseCustFile.TabIndex = 150
        Me.cmdBrowseCustFile.Text = "Customer File"
        Me.cmdBrowseCustFile.UseVisualStyleBackColor = True
        '
        'tCustomerName
        '
        Me.tCustomerName.BackColor = System.Drawing.Color.White
        Me.tCustomerName.Location = New System.Drawing.Point(138, 19)
        Me.tCustomerName.Name = "tCustomerName"
        Me.tCustomerName.ReadOnly = True
        Me.tCustomerName.Size = New System.Drawing.Size(714, 22)
        Me.tCustomerName.TabIndex = 1
        Me.tCustomerName.TabStop = False
        '
        'tPrintcardNotes
        '
        Me.tPrintcardNotes.BackColor = System.Drawing.Color.White
        Me.tPrintcardNotes.Location = New System.Drawing.Point(137, 179)
        Me.tPrintcardNotes.Multiline = True
        Me.tPrintcardNotes.Name = "tPrintcardNotes"
        Me.tPrintcardNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tPrintcardNotes.Size = New System.Drawing.Size(714, 62)
        Me.tPrintcardNotes.TabIndex = 190
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(26, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer Name:"
        '
        'tFilePath
        '
        Me.tFilePath.BackColor = System.Drawing.Color.White
        Me.tFilePath.Location = New System.Drawing.Point(138, 81)
        Me.tFilePath.Name = "tFilePath"
        Me.tFilePath.ReadOnly = True
        Me.tFilePath.Size = New System.Drawing.Size(713, 22)
        Me.tFilePath.TabIndex = 170
        '
        'cmdBrowseFile
        '
        Me.cmdBrowseFile.ForeColor = System.Drawing.Color.Black
        Me.cmdBrowseFile.Location = New System.Drawing.Point(33, 77)
        Me.cmdBrowseFile.Name = "cmdBrowseFile"
        Me.cmdBrowseFile.Size = New System.Drawing.Size(98, 30)
        Me.cmdBrowseFile.TabIndex = 150
        Me.cmdBrowseFile.Text = "Upload File"
        Me.cmdBrowseFile.UseVisualStyleBackColor = True
        '
        'GetCustomer
        '
        Me.GetCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GetCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GetCustomer.ForeColor = System.Drawing.Color.White
        Me.GetCustomer.Location = New System.Drawing.Point(858, 17)
        Me.GetCustomer.Name = "GetCustomer"
        Me.GetCustomer.Size = New System.Drawing.Size(32, 27)
        Me.GetCustomer.TabIndex = 2
        Me.GetCustomer.Text = " ... "
        Me.GetCustomer.UseVisualStyleBackColor = True
        '
        'tBoxDescription
        '
        Me.tBoxDescription.Location = New System.Drawing.Point(138, 47)
        Me.tBoxDescription.Name = "tBoxDescription"
        Me.tBoxDescription.Size = New System.Drawing.Size(714, 22)
        Me.tBoxDescription.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(29, 50)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Box Description:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(87, 182)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(47, 16)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Notes:"
        '
        'tPrincardPrefix
        '
        Me.tPrincardPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPrincardPrefix.Location = New System.Drawing.Point(138, 248)
        Me.tPrincardPrefix.Name = "tPrincardPrefix"
        Me.tPrincardPrefix.ReadOnly = True
        Me.tPrincardPrefix.Size = New System.Drawing.Size(466, 21)
        Me.tPrincardPrefix.TabIndex = 120
        Me.tPrincardPrefix.TabStop = False
        Me.tPrincardPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(47, 250)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 16)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Printcard #:"
        '
        'tPrintcardNumber
        '
        Me.tPrintcardNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.tPrintcardNumber.Location = New System.Drawing.Point(610, 247)
        Me.tPrintcardNumber.Name = "tPrintcardNumber"
        Me.tPrintcardNumber.Size = New System.Drawing.Size(101, 22)
        Me.tPrintcardNumber.TabIndex = 121
        '
        'cmbBoxFormat
        '
        Me.cmbBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBoxFormat.FormattingEnabled = True
        Me.cmbBoxFormat.Location = New System.Drawing.Point(131, 30)
        Me.cmbBoxFormat.Name = "cmbBoxFormat"
        Me.cmbBoxFormat.Size = New System.Drawing.Size(479, 24)
        Me.cmbBoxFormat.TabIndex = 20
        '
        'lDimensionID
        '
        Me.lDimensionID.AutoSize = True
        Me.lDimensionID.Location = New System.Drawing.Point(1140, 35)
        Me.lDimensionID.Name = "lDimensionID"
        Me.lDimensionID.Size = New System.Drawing.Size(41, 16)
        Me.lDimensionID.TabIndex = 141
        Me.lDimensionID.Text = "ID_ID"
        Me.lDimensionID.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(30, 35)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Board Format:"
        '
        'cmbJoint
        '
        Me.cmbJoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJoint.FormattingEnabled = True
        Me.cmbJoint.Location = New System.Drawing.Point(131, 60)
        Me.cmbJoint.Name = "cmbJoint"
        Me.cmbJoint.Size = New System.Drawing.Size(479, 24)
        Me.cmbJoint.TabIndex = 35
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Location = New System.Drawing.Point(16, 64)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(107, 16)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Joint/Lock Type:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(40, 94)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Board Type:"
        '
        'cmbBoardType
        '
        Me.cmbBoardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBoardType.FormattingEnabled = True
        Me.cmbBoardType.Location = New System.Drawing.Point(131, 90)
        Me.cmbBoardType.Name = "cmbBoardType"
        Me.cmbBoardType.Size = New System.Drawing.Size(479, 24)
        Me.cmbBoardType.TabIndex = 40
        '
        'cmbTest
        '
        Me.cmbTest.FormattingEnabled = True
        Me.cmbTest.Location = New System.Drawing.Point(131, 120)
        Me.cmbTest.Name = "cmbTest"
        Me.cmbTest.Size = New System.Drawing.Size(178, 24)
        Me.cmbTest.TabIndex = 50
        '
        'cmdGetCombination
        '
        Me.cmdGetCombination.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdGetCombination.Location = New System.Drawing.Point(616, 148)
        Me.cmdGetCombination.Name = "cmdGetCombination"
        Me.cmdGetCombination.Size = New System.Drawing.Size(32, 27)
        Me.cmdGetCombination.TabIndex = 61
        Me.cmdGetCombination.Text = " ... "
        Me.cmdGetCombination.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(85, 124)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Test:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(37, 153)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 16)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Combination:"
        '
        'tPaperCombination
        '
        Me.tPaperCombination.Location = New System.Drawing.Point(131, 150)
        Me.tPaperCombination.Name = "tPaperCombination"
        Me.tPaperCombination.ReadOnly = True
        Me.tPaperCombination.Size = New System.Drawing.Size(479, 22)
        Me.tPaperCombination.TabIndex = 60
        Me.tPaperCombination.TabStop = False
        '
        'tOuterLiner
        '
        Me.tOuterLiner.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.tOuterLiner.Location = New System.Drawing.Point(131, 178)
        Me.tOuterLiner.Name = "tOuterLiner"
        Me.tOuterLiner.ReadOnly = True
        Me.tOuterLiner.Size = New System.Drawing.Size(479, 22)
        Me.tOuterLiner.TabIndex = 65
        Me.tOuterLiner.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(48, 182)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Outer Liner:"
        '
        'cmdRefrehDiecut
        '
        Me.cmdRefrehDiecut.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdRefrehDiecut.Image = CType(resources.GetObject("cmdRefrehDiecut.Image"), System.Drawing.Image)
        Me.cmdRefrehDiecut.Location = New System.Drawing.Point(223, 231)
        Me.cmdRefrehDiecut.Name = "cmdRefrehDiecut"
        Me.cmdRefrehDiecut.Size = New System.Drawing.Size(54, 32)
        Me.cmdRefrehDiecut.TabIndex = 169
        Me.cmdRefrehDiecut.UseVisualStyleBackColor = True
        '
        'cmbOrient
        '
        Me.cmbOrient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrient.FormattingEnabled = True
        Me.cmbOrient.Location = New System.Drawing.Point(91, 50)
        Me.cmbOrient.Name = "cmbOrient"
        Me.cmbOrient.Size = New System.Drawing.Size(352, 24)
        Me.cmbOrient.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(18, 24)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "I.D.: / Unit:"
        '
        'tHeight
        '
        Me.tHeight.Location = New System.Drawing.Point(271, 21)
        Me.tHeight.Name = "tHeight"
        Me.tHeight.Size = New System.Drawing.Size(60, 22)
        Me.tHeight.TabIndex = 2
        '
        'tColor1
        '
        Me.tColor1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tColor1.Location = New System.Drawing.Point(99, 17)
        Me.tColor1.Name = "tColor1"
        Me.tColor1.Size = New System.Drawing.Size(164, 22)
        Me.tColor1.TabIndex = 0
        '
        'tBoardLength
        '
        Me.tBoardLength.Location = New System.Drawing.Point(158, 111)
        Me.tBoardLength.Name = "tBoardLength"
        Me.tBoardLength.ReadOnly = True
        Me.tBoardLength.Size = New System.Drawing.Size(60, 22)
        Me.tBoardLength.TabIndex = 11
        '
        'tWidth
        '
        Me.tWidth.Location = New System.Drawing.Point(181, 21)
        Me.tWidth.Name = "tWidth"
        Me.tWidth.Size = New System.Drawing.Size(60, 22)
        Me.tWidth.TabIndex = 1
        '
        'tPanel4
        '
        Me.tPanel4.Location = New System.Drawing.Point(362, 80)
        Me.tPanel4.Name = "tPanel4"
        Me.tPanel4.Size = New System.Drawing.Size(60, 22)
        Me.tPanel4.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(40, 81)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 16)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Color 3:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(36, 209)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(87, 16)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Scale Factor:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(159, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(14, 16)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "x"
        '
        'boardID
        '
        Me.boardID.AutoSize = True
        Me.boardID.Location = New System.Drawing.Point(40, 455)
        Me.boardID.Name = "boardID"
        Me.boardID.Size = New System.Drawing.Size(56, 16)
        Me.boardID.TabIndex = 4
        Me.boardID.Text = "Label17"
        Me.boardID.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Location = New System.Drawing.Point(342, 114)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(35, 16)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = "Flap"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(9, 55)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 16)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Orientation:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(22, 83)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(62, 16)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Creases:"
        '
        'tBoardWidth
        '
        Me.tBoardWidth.Location = New System.Drawing.Point(91, 111)
        Me.tBoardWidth.Name = "tBoardWidth"
        Me.tBoardWidth.ReadOnly = True
        Me.tBoardWidth.Size = New System.Drawing.Size(60, 22)
        Me.tBoardWidth.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(40, 50)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Color 2:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Location = New System.Drawing.Point(226, 112)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(47, 16)
        Me.Label26.TabIndex = 1
        Me.Label26.Text = "Height"
        '
        'tGlueTab
        '
        Me.tGlueTab.Location = New System.Drawing.Point(91, 80)
        Me.tGlueTab.Name = "tGlueTab"
        Me.tGlueTab.Size = New System.Drawing.Size(60, 22)
        Me.tGlueTab.TabIndex = 5
        Me.tGlueTab.Text = "35"
        '
        'cmbScale
        '
        Me.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScale.FormattingEnabled = True
        Me.cmbScale.Location = New System.Drawing.Point(131, 206)
        Me.cmbScale.Name = "cmbScale"
        Me.cmbScale.Size = New System.Drawing.Size(86, 24)
        Me.cmbScale.TabIndex = 110
        '
        'cmbDiecut
        '
        Me.cmbDiecut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDiecut.FormattingEnabled = True
        Me.cmbDiecut.Location = New System.Drawing.Point(131, 236)
        Me.cmbDiecut.Name = "cmbDiecut"
        Me.cmbDiecut.Size = New System.Drawing.Size(86, 24)
        Me.cmbDiecut.TabIndex = 140
        '
        'tFlap
        '
        Me.tFlap.Location = New System.Drawing.Point(383, 111)
        Me.tFlap.Name = "tFlap"
        Me.tFlap.Size = New System.Drawing.Size(60, 22)
        Me.tFlap.TabIndex = 13
        '
        'tLength
        '
        Me.tLength.Location = New System.Drawing.Point(91, 21)
        Me.tLength.Name = "tLength"
        Me.tLength.Size = New System.Drawing.Size(60, 22)
        Me.tLength.TabIndex = 0
        '
        'tColor4
        '
        Me.tColor4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tColor4.Location = New System.Drawing.Point(99, 106)
        Me.tColor4.Name = "tColor4"
        Me.tColor4.Size = New System.Drawing.Size(164, 22)
        Me.tColor4.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(40, 109)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 16)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Color 4:"
        '
        'tBoxHeight
        '
        Me.tBoxHeight.Location = New System.Drawing.Point(277, 111)
        Me.tBoxHeight.Name = "tBoxHeight"
        Me.tBoxHeight.Size = New System.Drawing.Size(60, 22)
        Me.tBoxHeight.TabIndex = 12
        '
        'tPanel1
        '
        Me.tPanel1.Location = New System.Drawing.Point(158, 80)
        Me.tPanel1.Name = "tPanel1"
        Me.tPanel1.Size = New System.Drawing.Size(60, 22)
        Me.tPanel1.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(64, 239)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 16)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Diecut #:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(249, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 16)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "x"
        '
        'tPanel3
        '
        Me.tPanel3.Location = New System.Drawing.Point(294, 80)
        Me.tPanel3.Name = "tPanel3"
        Me.tPanel3.Size = New System.Drawing.Size(60, 22)
        Me.tPanel3.TabIndex = 8
        '
        'tPanel2
        '
        Me.tPanel2.Location = New System.Drawing.Point(226, 80)
        Me.tPanel2.Name = "tPanel2"
        Me.tPanel2.Size = New System.Drawing.Size(60, 22)
        Me.tPanel2.TabIndex = 7
        '
        'tColor3
        '
        Me.tColor3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tColor3.Location = New System.Drawing.Point(99, 78)
        Me.tColor3.Name = "tColor3"
        Me.tColor3.Size = New System.Drawing.Size(164, 22)
        Me.tColor3.TabIndex = 2
        '
        'tColor2
        '
        Me.tColor2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tColor2.Location = New System.Drawing.Point(99, 47)
        Me.tColor2.Name = "tColor2"
        Me.tColor2.Size = New System.Drawing.Size(164, 22)
        Me.tColor2.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Location = New System.Drawing.Point(12, 114)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 16)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = "Boardsize:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(40, 20)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Color 1:"
        '
        'PSI_id
        '
        Me.PSI_id.AutoSize = True
        Me.PSI_id.Location = New System.Drawing.Point(40, 423)
        Me.PSI_id.Name = "PSI_id"
        Me.PSI_id.Size = New System.Drawing.Size(56, 16)
        Me.PSI_id.TabIndex = 4
        Me.PSI_id.Text = "Label17"
        Me.PSI_id.Visible = False
        '
        'cmbUnits
        '
        Me.cmbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnits.FormattingEnabled = True
        Me.cmbUnits.Location = New System.Drawing.Point(339, 20)
        Me.cmbUnits.Name = "cmbUnits"
        Me.cmbUnits.Size = New System.Drawing.Size(104, 24)
        Me.cmbUnits.TabIndex = 3
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(1264, 566)
        Me.WebBrowser1.TabIndex = 155
        '
        'PrintcardTabControl
        '
        Me.PrintcardTabControl.Controls.Add(Me.TabCustomer)
        Me.PrintcardTabControl.Controls.Add(Me.TabBoardSpecs)
        Me.PrintcardTabControl.Controls.Add(Me.TabBoxDetails)
        Me.PrintcardTabControl.Controls.Add(Me.TabPrintcardPreview)
        Me.PrintcardTabControl.Controls.Add(Me.TabCustomerFile)
        Me.PrintcardTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintcardTabControl.HotTrack = True
        Me.PrintcardTabControl.ItemSize = New System.Drawing.Size(180, 30)
        Me.PrintcardTabControl.Location = New System.Drawing.Point(0, 0)
        Me.PrintcardTabControl.Name = "PrintcardTabControl"
        Me.PrintcardTabControl.SelectedIndex = 0
        Me.PrintcardTabControl.Size = New System.Drawing.Size(1278, 610)
        Me.PrintcardTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.PrintcardTabControl.TabIndex = 161
        '
        'TabCustomer
        '
        Me.TabCustomer.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TabCustomer.Controls.Add(Me.cmbPrintcardStatus)
        Me.TabCustomer.Controls.Add(Me.GetPrintNumList)
        Me.TabCustomer.Controls.Add(Me.cPrintcardCreated)
        Me.TabCustomer.Controls.Add(Me.Label1)
        Me.TabCustomer.Controls.Add(Me.cmdBrowseFile)
        Me.TabCustomer.Controls.Add(Me.Label22)
        Me.TabCustomer.Controls.Add(Me.GetCustomer)
        Me.TabCustomer.Controls.Add(Me.tPrintcardNumber)
        Me.TabCustomer.Controls.Add(Me.tFilePath)
        Me.TabCustomer.Controls.Add(Me.tCustomerFile)
        Me.TabCustomer.Controls.Add(Me.tBoxDescription)
        Me.TabCustomer.Controls.Add(Me.Label14)
        Me.TabCustomer.Controls.Add(Me.Label2)
        Me.TabCustomer.Controls.Add(Me.cmdBrowseCustFile)
        Me.TabCustomer.Controls.Add(Me.tPrintcardNotes)
        Me.TabCustomer.Controls.Add(Me.tPrincardPrefix)
        Me.TabCustomer.Controls.Add(Me.Label21)
        Me.TabCustomer.Controls.Add(Me.Label20)
        Me.TabCustomer.Controls.Add(Me.tCustomerName)
        Me.TabCustomer.Location = New System.Drawing.Point(4, 34)
        Me.TabCustomer.Name = "TabCustomer"
        Me.TabCustomer.Padding = New System.Windows.Forms.Padding(3)
        Me.TabCustomer.Size = New System.Drawing.Size(1270, 572)
        Me.TabCustomer.TabIndex = 0
        Me.TabCustomer.Text = "Customer"
        '
        'cmbPrintcardStatus
        '
        Me.cmbPrintcardStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrintcardStatus.FormattingEnabled = True
        Me.cmbPrintcardStatus.Location = New System.Drawing.Point(137, 149)
        Me.cmbPrintcardStatus.Name = "cmbPrintcardStatus"
        Me.cmbPrintcardStatus.Size = New System.Drawing.Size(714, 24)
        Me.cmbPrintcardStatus.TabIndex = 192
        '
        'GetPrintNumList
        '
        Me.GetPrintNumList.Location = New System.Drawing.Point(717, 243)
        Me.GetPrintNumList.Name = "GetPrintNumList"
        Me.GetPrintNumList.Size = New System.Drawing.Size(134, 30)
        Me.GetPrintNumList.TabIndex = 191
        Me.GetPrintNumList.Text = "Get Printcard # List"
        Me.GetPrintNumList.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(30, 153)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(104, 16)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Printcard Status:"
        '
        'TabBoardSpecs
        '
        Me.TabBoardSpecs.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TabBoardSpecs.Controls.Add(Me.GroupBox2)
        Me.TabBoardSpecs.Controls.Add(Me.lDimensionID)
        Me.TabBoardSpecs.Controls.Add(Me.lBoxFormatId)
        Me.TabBoardSpecs.Controls.Add(Me.lScaleID)
        Me.TabBoardSpecs.Controls.Add(Me.lDiecut)
        Me.TabBoardSpecs.Controls.Add(Me.lUnitID)
        Me.TabBoardSpecs.Location = New System.Drawing.Point(4, 34)
        Me.TabBoardSpecs.Name = "TabBoardSpecs"
        Me.TabBoardSpecs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabBoardSpecs.Size = New System.Drawing.Size(1270, 572)
        Me.TabBoardSpecs.TabIndex = 1
        Me.TabBoardSpecs.Text = "Board Specs"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Panel5)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.tPaperCombination)
        Me.GroupBox2.Controls.Add(Me.cmdRefrehDiecut)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.cmbBoxFormat)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cmbDiecut)
        Me.GroupBox2.Controls.Add(Me.cmbJoint)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.tOuterLiner)
        Me.GroupBox2.Controls.Add(Me.cmbScale)
        Me.GroupBox2.Controls.Add(Me.cmbBoardType)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cmbTestType)
        Me.GroupBox2.Controls.Add(Me.cmdGetCombination)
        Me.GroupBox2.Controls.Add(Me.cmbTest)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1070, 299)
        Me.GroupBox2.TabIndex = 170
        Me.GroupBox2.TabStop = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Location = New System.Drawing.Point(352, 123)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(73, 16)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "Test Type:"
        '
        'cmbTestType
        '
        Me.cmbTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTestType.FormattingEnabled = True
        Me.cmbTestType.Location = New System.Drawing.Point(432, 120)
        Me.cmbTestType.Name = "cmbTestType"
        Me.cmbTestType.Size = New System.Drawing.Size(178, 24)
        Me.cmbTestType.TabIndex = 50
        '
        'TabBoxDetails
        '
        Me.TabBoxDetails.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TabBoxDetails.Controls.Add(Me.GroupBox1)
        Me.TabBoxDetails.Controls.Add(Me.BoxDimensionGroup)
        Me.TabBoxDetails.Controls.Add(Me.ColorGroup)
        Me.TabBoxDetails.Controls.Add(Me.PSI_id)
        Me.TabBoxDetails.Controls.Add(Me.cPartitionGroup)
        Me.TabBoxDetails.Controls.Add(Me.boardID)
        Me.TabBoxDetails.Location = New System.Drawing.Point(4, 34)
        Me.TabBoxDetails.Name = "TabBoxDetails"
        Me.TabBoxDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.TabBoxDetails.Size = New System.Drawing.Size(1270, 572)
        Me.TabBoxDetails.TabIndex = 2
        Me.TabBoxDetails.Text = "Box Details"
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(11, 156)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1253, 408)
        Me.GroupBox1.TabIndex = 174
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Box 2D Preview (Under Development)"
        '
        'BoxDimensionGroup
        '
        Me.BoxDimensionGroup.Controls.Add(Me.tFlap)
        Me.BoxDimensionGroup.Controls.Add(Me.tBoxHeight)
        Me.BoxDimensionGroup.Controls.Add(Me.Label26)
        Me.BoxDimensionGroup.Controls.Add(Me.Label27)
        Me.BoxDimensionGroup.Controls.Add(Me.cmbOrient)
        Me.BoxDimensionGroup.Controls.Add(Me.tBoardWidth)
        Me.BoxDimensionGroup.Controls.Add(Me.Label4)
        Me.BoxDimensionGroup.Controls.Add(Me.tHeight)
        Me.BoxDimensionGroup.Controls.Add(Me.Label24)
        Me.BoxDimensionGroup.Controls.Add(Me.tWidth)
        Me.BoxDimensionGroup.Controls.Add(Me.tPanel4)
        Me.BoxDimensionGroup.Controls.Add(Me.tBoardLength)
        Me.BoxDimensionGroup.Controls.Add(Me.cmbUnits)
        Me.BoxDimensionGroup.Controls.Add(Me.Label17)
        Me.BoxDimensionGroup.Controls.Add(Me.tPanel2)
        Me.BoxDimensionGroup.Controls.Add(Me.Label15)
        Me.BoxDimensionGroup.Controls.Add(Me.tPanel3)
        Me.BoxDimensionGroup.Controls.Add(Me.Label23)
        Me.BoxDimensionGroup.Controls.Add(Me.Label18)
        Me.BoxDimensionGroup.Controls.Add(Me.tGlueTab)
        Me.BoxDimensionGroup.Controls.Add(Me.tPanel1)
        Me.BoxDimensionGroup.Controls.Add(Me.tLength)
        Me.BoxDimensionGroup.Location = New System.Drawing.Point(11, 7)
        Me.BoxDimensionGroup.Name = "BoxDimensionGroup"
        Me.BoxDimensionGroup.Size = New System.Drawing.Size(515, 143)
        Me.BoxDimensionGroup.TabIndex = 173
        Me.BoxDimensionGroup.TabStop = False
        Me.BoxDimensionGroup.Text = "Box Dimension"
        '
        'ColorGroup
        '
        Me.ColorGroup.Controls.Add(Me.Label9)
        Me.ColorGroup.Controls.Add(Me.tColor2)
        Me.ColorGroup.Controls.Add(Me.tColor3)
        Me.ColorGroup.Controls.Add(Me.Label12)
        Me.ColorGroup.Controls.Add(Me.tColor4)
        Me.ColorGroup.Controls.Add(Me.Label10)
        Me.ColorGroup.Controls.Add(Me.tColor1)
        Me.ColorGroup.Controls.Add(Me.Label11)
        Me.ColorGroup.Location = New System.Drawing.Point(944, 7)
        Me.ColorGroup.Name = "ColorGroup"
        Me.ColorGroup.Size = New System.Drawing.Size(318, 144)
        Me.ColorGroup.TabIndex = 172
        Me.ColorGroup.TabStop = False
        Me.ColorGroup.Text = "Colors"
        '
        'cPartitionGroup
        '
        Me.cPartitionGroup.Controls.Add(Me.Label31)
        Me.cPartitionGroup.Controls.Add(Me.Label29)
        Me.cPartitionGroup.Controls.Add(Me.Label30)
        Me.cPartitionGroup.Controls.Add(Me.Board2Length)
        Me.cPartitionGroup.Controls.Add(Me.Label33)
        Me.cPartitionGroup.Controls.Add(Me.Label32)
        Me.cPartitionGroup.Controls.Add(Me.Label28)
        Me.cPartitionGroup.Controls.Add(Me.Board2Width)
        Me.cPartitionGroup.Controls.Add(Me.Board1Length)
        Me.cPartitionGroup.Controls.Add(Me.Board1Width)
        Me.cPartitionGroup.Location = New System.Drawing.Point(539, 7)
        Me.cPartitionGroup.Name = "cPartitionGroup"
        Me.cPartitionGroup.Size = New System.Drawing.Size(393, 143)
        Me.cPartitionGroup.TabIndex = 171
        Me.cPartitionGroup.TabStop = False
        Me.cPartitionGroup.Text = "Partition / Other Box Formats"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(309, 54)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(48, 16)
        Me.Label31.TabIndex = 171
        Me.Label31.Text = "Length"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(119, 54)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(48, 16)
        Me.Label29.TabIndex = 171
        Me.Label29.Text = "Length"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(229, 54)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(42, 16)
        Me.Label30.TabIndex = 171
        Me.Label30.Text = "Width"
        '
        'Board2Length
        '
        Me.Board2Length.Location = New System.Drawing.Point(299, 77)
        Me.Board2Length.Name = "Board2Length"
        Me.Board2Length.Size = New System.Drawing.Size(68, 22)
        Me.Board2Length.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(258, 28)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(55, 16)
        Me.Label33.TabIndex = 171
        Me.Label33.Text = "Board 2"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(88, 28)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(55, 16)
        Me.Label32.TabIndex = 171
        Me.Label32.Text = "Board 1"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(39, 54)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(42, 16)
        Me.Label28.TabIndex = 171
        Me.Label28.Text = "Width"
        '
        'Board2Width
        '
        Me.Board2Width.Location = New System.Drawing.Point(216, 77)
        Me.Board2Width.Name = "Board2Width"
        Me.Board2Width.Size = New System.Drawing.Size(68, 22)
        Me.Board2Width.TabIndex = 2
        '
        'Board1Length
        '
        Me.Board1Length.Location = New System.Drawing.Point(109, 77)
        Me.Board1Length.Name = "Board1Length"
        Me.Board1Length.Size = New System.Drawing.Size(68, 22)
        Me.Board1Length.TabIndex = 1
        '
        'Board1Width
        '
        Me.Board1Width.Location = New System.Drawing.Point(26, 77)
        Me.Board1Width.Name = "Board1Width"
        Me.Board1Width.Size = New System.Drawing.Size(68, 22)
        Me.Board1Width.TabIndex = 0
        '
        'TabPrintcardPreview
        '
        Me.TabPrintcardPreview.Controls.Add(Me.AxPrintcardFile)
        Me.TabPrintcardPreview.Controls.Add(Me.WebBrowser1)
        Me.TabPrintcardPreview.Location = New System.Drawing.Point(4, 34)
        Me.TabPrintcardPreview.Name = "TabPrintcardPreview"
        Me.TabPrintcardPreview.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPrintcardPreview.Size = New System.Drawing.Size(1270, 572)
        Me.TabPrintcardPreview.TabIndex = 3
        Me.TabPrintcardPreview.Text = "Printcard Preview"
        Me.TabPrintcardPreview.UseVisualStyleBackColor = True
        '
        'TabCustomerFile
        '
        Me.TabCustomerFile.Controls.Add(Me.AxCustomerFile)
        Me.TabCustomerFile.Controls.Add(Me.WebCustomerFile)
        Me.TabCustomerFile.Location = New System.Drawing.Point(4, 34)
        Me.TabCustomerFile.Name = "TabCustomerFile"
        Me.TabCustomerFile.Padding = New System.Windows.Forms.Padding(3)
        Me.TabCustomerFile.Size = New System.Drawing.Size(1270, 572)
        Me.TabCustomerFile.TabIndex = 4
        Me.TabCustomerFile.Text = "Customer File"
        Me.TabCustomerFile.UseVisualStyleBackColor = True
        '
        'WebCustomerFile
        '
        Me.WebCustomerFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebCustomerFile.Location = New System.Drawing.Point(3, 3)
        Me.WebCustomerFile.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebCustomerFile.Name = "WebCustomerFile"
        Me.WebCustomerFile.Size = New System.Drawing.Size(1264, 566)
        Me.WebCustomerFile.TabIndex = 156
        '
        'AxPrintcardFile
        '
        Me.AxPrintcardFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxPrintcardFile.Enabled = True
        Me.AxPrintcardFile.Location = New System.Drawing.Point(3, 3)
        Me.AxPrintcardFile.Name = "AxPrintcardFile"
        Me.AxPrintcardFile.OcxState = CType(resources.GetObject("AxPrintcardFile.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPrintcardFile.Size = New System.Drawing.Size(1264, 566)
        Me.AxPrintcardFile.TabIndex = 156
        '
        'AxCustomerFile
        '
        Me.AxCustomerFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxCustomerFile.Enabled = True
        Me.AxCustomerFile.Location = New System.Drawing.Point(3, 3)
        Me.AxCustomerFile.Name = "AxCustomerFile"
        Me.AxCustomerFile.OcxState = CType(resources.GetObject("AxCustomerFile.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxCustomerFile.Size = New System.Drawing.Size(1264, 566)
        Me.AxCustomerFile.TabIndex = 157
        '
        'cmbBoxCategory
        '
        Me.cmbBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBoxCategory.FormattingEnabled = True
        Me.cmbBoxCategory.Location = New System.Drawing.Point(121, 15)
        Me.cmbBoxCategory.Name = "cmbBoxCategory"
        Me.cmbBoxCategory.Size = New System.Drawing.Size(257, 24)
        Me.cmbBoxCategory.TabIndex = 170
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label35.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label35.Location = New System.Drawing.Point(20, 20)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(90, 16)
        Me.Label35.TabIndex = 171
        Me.Label35.Text = "Box category:"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Red
        Me.Label36.Location = New System.Drawing.Point(632, 18)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(375, 20)
        Me.Label36.TabIndex = 172
        Me.Label36.Text = "Please select the correct category of this box."
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel5.Controls.Add(Me.Label35)
        Me.Panel5.Controls.Add(Me.cmbBoxCategory)
        Me.Panel5.Location = New System.Drawing.Point(629, 41)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(398, 55)
        Me.Panel5.TabIndex = 173
        '
        'PrintCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1278, 610)
        Me.Controls.Add(Me.PrintcardTabControl)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "PrintCard"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Printcard"
        Me.PrintcardTabControl.ResumeLayout(False)
        Me.TabCustomer.ResumeLayout(False)
        Me.TabCustomer.PerformLayout()
        Me.TabBoardSpecs.ResumeLayout(False)
        Me.TabBoardSpecs.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabBoxDetails.ResumeLayout(False)
        Me.TabBoxDetails.PerformLayout()
        Me.BoxDimensionGroup.ResumeLayout(False)
        Me.BoxDimensionGroup.PerformLayout()
        Me.ColorGroup.ResumeLayout(False)
        Me.ColorGroup.PerformLayout()
        Me.cPartitionGroup.ResumeLayout(False)
        Me.cPartitionGroup.PerformLayout()
        Me.TabPrintcardPreview.ResumeLayout(False)
        Me.TabCustomerFile.ResumeLayout(False)
        CType(Me.AxPrintcardFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxCustomerFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents tBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents cmbBoardType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdGetCombination As System.Windows.Forms.Button
    Friend WithEvents GetCustomer As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tOuterLiner As System.Windows.Forms.TextBox
    Friend WithEvents tPaperCombination As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tPrincardPrefix As System.Windows.Forms.TextBox
    Friend WithEvents cmbTest As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBoxFormat As System.Windows.Forms.ComboBox
    Friend WithEvents lBoxFormatId As System.Windows.Forms.Label
    Friend WithEvents lDimensionID As System.Windows.Forms.Label
    Friend WithEvents lUnitID As System.Windows.Forms.Label
    Friend WithEvents lScaleID As System.Windows.Forms.Label
    Friend WithEvents tPrintcardNumber As System.Windows.Forms.TextBox
    Friend WithEvents lDiecut As System.Windows.Forms.Label
    Friend WithEvents UploadPrintcardFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdBrowseFile As System.Windows.Forms.Button
    Friend WithEvents tFilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmbJoint As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cPrintcardCreated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tPrintcardNotes As System.Windows.Forms.TextBox
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents tCustomerFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowseCustFile As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cmbDiecut As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PSI_id As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbScale As System.Windows.Forms.ComboBox
    Friend WithEvents tBoardWidth As System.Windows.Forms.TextBox
    Friend WithEvents boardID As System.Windows.Forms.Label
    Friend WithEvents tPanel4 As System.Windows.Forms.TextBox
    Friend WithEvents tColor4 As System.Windows.Forms.TextBox
    Friend WithEvents tBoardLength As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tColor3 As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents tColor2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tColor1 As System.Windows.Forms.TextBox
    Friend WithEvents tFlap As System.Windows.Forms.TextBox
    Friend WithEvents tBoxHeight As System.Windows.Forms.TextBox
    Friend WithEvents tPanel3 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbUnits As System.Windows.Forms.ComboBox
    Friend WithEvents tPanel2 As System.Windows.Forms.TextBox
    Friend WithEvents tPanel1 As System.Windows.Forms.TextBox
    Friend WithEvents tLength As System.Windows.Forms.TextBox
    Friend WithEvents tGlueTab As System.Windows.Forms.TextBox
    Friend WithEvents tHeight As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tWidth As System.Windows.Forms.TextBox
    Friend WithEvents cmbOrient As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents PrintcardTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabCustomer As System.Windows.Forms.TabPage
    Friend WithEvents TabBoardSpecs As System.Windows.Forms.TabPage
    Friend WithEvents TabBoxDetails As System.Windows.Forms.TabPage
    Friend WithEvents TabPrintcardPreview As System.Windows.Forms.TabPage
    Friend WithEvents GetPrintNumList As System.Windows.Forms.Button
    Friend WithEvents cmdRefrehDiecut As System.Windows.Forms.Button
    Friend WithEvents TabCustomerFile As System.Windows.Forms.TabPage
    Friend WithEvents WebCustomerFile As System.Windows.Forms.WebBrowser
    Friend WithEvents AxPrintcardFile As AxFOXITREADERLib.AxFoxitCtl
    Friend WithEvents AxCustomerFile As AxFOXITREADERLib.AxFoxitCtl
    Friend WithEvents cPartitionGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Board1Width As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Board2Length As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Board2Width As System.Windows.Forms.TextBox
    Friend WithEvents Board1Length As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents ColorGroup As System.Windows.Forms.GroupBox
    Friend WithEvents BoxDimensionGroup As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPrintcardStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbTestType As System.Windows.Forms.ComboBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cmbBoxCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label36 As System.Windows.Forms.Label
End Class
