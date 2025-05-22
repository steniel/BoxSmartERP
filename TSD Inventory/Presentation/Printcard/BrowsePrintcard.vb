Imports CompletIT.Windows.Forms
Imports CompletIT.Windows.Forms.Export
Imports CompletIT.Windows.Forms.Export.Excel
Imports CompletIT.Windows.DataGridViewExtension
Imports CompletIT.Windows.Forms.Printing
Imports System.Drawing

Public Class BrowsePrintcard
    Dim MonthNumber As String
    Public YearToday As DateTime
    Public YearVal As String
    Public YearMonth As String
    Public CustomerFileID As Integer = 0
    Public ORDER_fileid As Integer
    Public ORDER_FileType As String
    Public ORDER_FileName As String
    Public ORDER_CustomerName As String
    Public ORDER_BoxDescription As String
    Public ORDER_BoxesRun As Integer
    Public ORDER_PlateNum As Integer ' Rubber die reference number
    Public ORDER_PrintcardNum As String
    Public EDIT_FileName As String = ""
    Public EDIT_CustomerFile As String = ""
    Public EDIT_DimensionID As Integer = 0
    Public EDIT_BoxFormatID As Integer 'RSC;HSC;Tray;Partition;etc.  

    Private Sub BrowsePrintcard_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        If txtYear.Text = "" Then
            YearToday = Date.Now
            YearVal = YearToday.ToString("yyyy")
            YearMonth = YearToday.ToString("MMMM")
            cmbNumRecords.Text = "100"
            txtYear.Text = YearVal
            'cmbMonth.Text = YearMonth
            cmbMonth.Text = "All Year Round"
        End If
        MainUI.tbDocumentSave.Enabled = False
        MainUI.PrintcardInstanceName = Me.Text
        MainUI.bDocumentEdit.Enabled = False
        MainUI.btAddCustomer.Enabled = False
    End Sub
    Private Sub BrowsePrintcard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SetDisplayHint(DATABASE_printcard, chkDisplayHint.Checked)
        MainUI.Text = ApplicationTitle
    End Sub

    Private Sub BrowsePrintcard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Stop()
    End Sub

    Private Sub BrowsePrintcard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainUI
        LoadPrintcards()
        For cols As Integer = 2 To 9
            gridColumn.Items.Add(PrintcardGrid.Columns(cols).HeaderText)
        Next
        gridColumn.Items.Add("Box Description")
        gridColumn.Text = "Customer"
        Timer1.Start()

        MainUI.PrintcardInstanceName = Me.Text
        If LoginTSD.UserPermission = 30 Then
            CreateCopyToolStripMenuItem.Enabled = False
            cmdCreateCopy.Enabled = False
            DeletePrintcardToolStripMenuItem.Enabled = False
            'cmdEditPrintcard.Enabled = False
            EditPrintcardToolStripMenuItem.Enabled = False
        End If


    End Sub
    Public Sub LoadPrintcards()
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        objPrintcard.GetPrintcardList(PrintcardGrid, txtYear.Text, MonthNumber, cmbNumRecords.Text)
        chkDisplayHint.Checked = GetDisplayHint(DATABASE_printcard)
        objPrintcard = Nothing
    End Sub
    Private Sub DataGridColumnHeaderImage()

    End Sub
    Public Sub LoadData()
        Dim objPrintcard As ClassPrintcard
        objPrintcard = New ClassPrintcard
        objPrintcard.GetPrintcardList(Me.PrintcardGrid, txtYear.Text, MonthNumber, cmbNumRecords.Text)
        lCountRows.Text = "Rowcount: " & PrintcardGrid.RowCount
        objPrintcard = Nothing
        If PrintcardGrid.RowCount > 0 Then
            PanelFunction1.Enabled = True
        Else
            PanelFunction1.Enabled = False
        End If
    End Sub

    Private Sub cmdViewPrintcard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdViewPrintcard.Click
        ViewPrintcardCMD()
    End Sub
    Private Sub ViewPrintcardCMD()
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        Dim id As Integer = 0
        Dim FileType As String = ""
        Dim FileName As String = ""
        For Each cPrintcards In PrintcardGrid.SelectedRows
            id = cPrintcards.cells("fileid").value 'Filename ID
            FileType = cPrintcards.cells("filetype").value
            FileName = cPrintcards.cells("filename").value
        Next
        objPrintcard.downLoadFile(id, FileName, FileType)
        objPrintcard = Nothing
    End Sub


    Private Sub txtYear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtYear.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtYear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged
        If txtYear.TextLength > 4 Then
            SendKeys.Send(ControlChars.Back)
        ElseIf txtYear.TextLength = 4 Then
            LoadData()
        End If
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        Select Case cmbMonth.Text
            Case "January"
                MonthNumber = "01"
                LoadData()
            Case "February"
                MonthNumber = "02"
                LoadData()
            Case "March"
                MonthNumber = "03"
                LoadData()
            Case "April"
                MonthNumber = "04"
                LoadData()
            Case "May"
                MonthNumber = "05"
                LoadData()
            Case "June"
                MonthNumber = "06"
                LoadData()
            Case "July"
                MonthNumber = "07"
                LoadData()
            Case "August"
                MonthNumber = "08"
                LoadData()
            Case "September"
                MonthNumber = "09"
                LoadData()
            Case "October"
                MonthNumber = "10"
                LoadData()
            Case "November"
                MonthNumber = "11"
                LoadData()
            Case "December"
                MonthNumber = "12"
                LoadData()
            Case Else
                MonthNumber = ""
                LoadData()
        End Select
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim ColumnName As String = ""
        Select Case gridColumn.Text
            Case "Customer"
                ColumnName = "organization_name"        
            Case "Box Description"
                ColumnName = "box_description"
            Case "Inside Dimension"
                ColumnName = "insidedimension"
            Case "Board Size"
                ColumnName = "boardsize"
            Case "Printcard No."
                ColumnName = "printcardno"
            Case "Diecut #"
                ColumnName = "diecut_number"
            Case "Rack Location"
                ColumnName = "racklocation"
            Case "Rubberdie Location"
                ColumnName = "rubber_location"
            Case "ID"
                ColumnName = "printcardid"
            Case "Status"
                ColumnName = "status"
        End Select

        If SearchGridValue(Me.PrintcardGrid, ColumnName, cSearchThis.Text) = False Then
            MainUI.StatusMessage.Text = "'" & cSearchThis.Text & "' cannot be found using column: " & gridColumn.Text & "!"
            MainUI.StatusMessage.ForeColor = Color.Red
        Else
            MainUI.StatusMessage.ForeColor = Color.Black
            MainUI.StatusMessage.Text = "'" & cSearchThis.Text & "' was found at row #: " & PrintcardGrid.CurrentRow.Index + 1
        End If
    End Sub

    Private Sub cmbNumRecords_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbNumRecords.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PrintcardGrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles PrintcardGrid.CellFormatting
        For Each row As DataGridViewRow In PrintcardGrid.Rows
            If row.Cells("status").Value = "Obsolete" Then
                row.DefaultCellStyle.BackColor = Color.DimGray
            End If
        Next
    End Sub

    Private Sub PrintcardGrid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintcardGrid.SelectionChanged
        Try
            If PrintcardGrid.SelectedRows.Count = 0 Then
                PanelFunction1.Enabled = False
                lCurrentSel.Text = "File ID selected: None"
                ViewCustomerFileToolStripMenuItem.Enabled = False                
                ViewPrintcardToolStripMenuItem.Enabled = False


                EditPrintcardToolStripMenuItem.Enabled = False
                CreateCopyToolStripMenuItem.Enabled = False
                DeletePrintcardToolStripMenuItem.Enabled = False
                EmailToCustomerToolStripMenuItem.Enabled = False
                ViewPlateUsageToolStripMenuItem.Enabled = False
                PropertiesToolStripMenuItem.Enabled = False
                ViewCustomerFileToolStripMenuItem.Enabled = False
                cmdSetPrice.Enabled = False
            Else
                cmdSetPrice.Enabled = True
                PanelFunction1.Enabled = True
                EditPrintcardToolStripMenuItem.Enabled = True
                CreateCopyToolStripMenuItem.Enabled = True
                DeletePrintcardToolStripMenuItem.Enabled = True
                EmailToCustomerToolStripMenuItem.Enabled = True
                ViewPlateUsageToolStripMenuItem.Enabled = True
                PropertiesToolStripMenuItem.Enabled = True
                For Each cPrintcards In PrintcardGrid.SelectedRows
                    lCurrentSel.Text = "File ID selected: " & cPrintcards.cells("fileid").value
                    CustomerFileID = cPrintcards.cells("customer_file_id").value
                Next
                ViewCustomerFileToolStripMenuItem.Enabled = True
                ViewPrintcardToolStripMenuItem.Enabled = True
            End If
            If CustomerFileID = 1 Then
                cmdViewCustomerFile.Enabled = False
                ViewCustomerFileToolStripMenuItem.Enabled = False
            Else
                If PrintcardGrid.SelectedRows.Count = 0 Then
                    cmdViewCustomerFile.Enabled = False
                    ViewCustomerFileToolStripMenuItem.Enabled = False
                Else
                    cmdViewCustomerFile.Enabled = True
                    ViewCustomerFileToolStripMenuItem.Enabled = True
                End If
            End If
        Catch ex As ArgumentException
            MsgBox(ex.Message & Chr(13) & "! Error in PrintcardGrid_SelectionChanged @BrowsePrintcard.vb form", MsgBoxStyle.Critical, "TSD Inventory System")
        End Try
    End Sub

    Private Sub cmdCreateCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateCopy.Click
        If LoginTSD.UserPermission <> AdminRole And LoginTSD.UserPermission <> GraphicArtistRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        CreatePrintcardCopyCMD()
    End Sub
    Private Sub NewEditPrintcard()
        'Check revision # @table revision           
        Dim objCustomerList As Customer = New Customer
        Dim p As ExportRptProgress = ExportRptProgress.ShowProgress(Me)
        Dim EditPrintcardWindow As PrintCard = New PrintCard
        Me.Cursor = Cursors.WaitCursor

        p.UpdateProgress(10, "10%", "Loading EDIT PRINTCARD form...")
        EditPrintcardWindow.Text = "EDIT PRINTCARD"
        EditPrintcardWindow.Show()
        EditPrintcardWindow.Activate()

        Dim id As Integer = 0
        Dim FileType As String = ""
        Dim CustomerID As Integer = 0        
        Dim PrintcardStatus_id As Integer

        Dim CustomerName As String = ""
        Dim BoxDesc As String = ""
        Dim boardtypeid As String = ""
        Dim testid As Short = 0
        Dim boxformatid As Integer = 0
        Dim CombinationID As Integer = 0      
        Dim revisionid As Integer = 0
        Dim fluteid As String = ""
        Dim diecutid As Integer = 0
        Dim jointid As String = ""
        Dim scale_id As String = ""
        Dim color1 As String = ""
        Dim color2 As String = ""
        Dim color3 As String = ""
        Dim color4 As String = ""
        Dim PrintcardNumber As Integer
        p.UpdateProgress(50, "50%", "Loading EDIT PRINTCARD form...")
        For Each cPrintcards In PrintcardGrid.SelectedRows
            EditPrintcardWindow.Edit_PrintcardID = cPrintcards.cells("printcardid").value
            CustomerFileID = cPrintcards.cells("customer_file_id").value 'Filename ID
            If CustomerFileID <> 1 Then
                EDIT_CustomerFile = cPrintcards.cells("customerfile").value
            Else
                EDIT_CustomerFile = ""
            End If
            id = cPrintcards.cells("fileid").value 'Filename ID
            FileType = cPrintcards.cells("filetype").value
            EDIT_FileName = cPrintcards.cells("filename").value            
            CustomerName = cPrintcards.cells("organization_name").value
            CustomerID = cPrintcards.cells("id").value
            BoxDesc = cPrintcards.cells("box_description").value
            color1 = cPrintcards.cells("color1").value
            color2 = cPrintcards.cells("color2").value
            color3 = cPrintcards.cells("color3").value
            color4 = cPrintcards.cells("color4").value
            boardtypeid = cPrintcards.cells("boardtypeid").value
            testid = cPrintcards.cells("testid").value
            fluteid = cPrintcards.cells("fluteid").value
            PrintcardStatus_id = cPrintcards.cells("printcard_status_id").value
            If IsNumeric(cPrintcards.cells("diecut_number").value) = True Then
                diecutid = cPrintcards.cells("diecut_number").value
            Else
                diecutid = 0
            End If
            jointid = cPrintcards.cells("jointid").value
            CombinationID = cPrintcards.cells("combinationid").value
            EDIT_DimensionID = cPrintcards.cells("dimensionid").value
            scale_id = cPrintcards.cells("scaleid").value
            PrintcardNumber = cPrintcards.cells("printcopyno").value
            EditPrintcardWindow.tPrintcardNumber.Text = PrintcardNumber
            EditPrintcardWindow.cPrintcardCreated.Text = cPrintcards.cells("date_created").value
            EditPrintcardWindow.tFilePath.Text = cPrintcards.cells("filename").value
            If cPrintcards.cells("customerfile").value <> "" Then
                EditPrintcardWindow.tCustomerFile.Text = cPrintcards.cells("customerfile").value
            Else
                EditPrintcardWindow.tCustomerFile.Text = "N/A"
            End If
        Next

        'Disable Get printcard number button
        EditPrintcardWindow.GetPrintNumList.Enabled = False

        EditPrintcardWindow.tColor1.Text = color1
        EditPrintcardWindow.tColor2.Text = color2
        EditPrintcardWindow.tColor3.Text = color3
        EditPrintcardWindow.tColor4.Text = color4
        EditPrintcardWindow.tBoxDescription.Text = BoxDesc
        EditPrintcardWindow.cmbBoardType.Text = fluteid
        EditPrintcardWindow.cmbDiecut.Text = diecutid        
        EditPrintcardWindow.cmbTest.Text = testid
        EditPrintcardWindow.cmbJoint.Text = jointid
        EditPrintcardWindow.cmbScale.Text = scale_id
        Dim BoxTestTypeID As Integer = GetTableValueInt("printcard", "test_type_id", EditPrintcardWindow.Edit_PrintcardID)
        Dim BoxTestTypeIDString As String = GetTableValueStr("test_type", "code", BoxTestTypeID)
        EditPrintcardWindow.cmbTestType.Text = BoxTestTypeIDString
        Dim BoxOrientationID As Integer = GetTableValueInt("printcard", "boxorientation_id", EditPrintcardWindow.Edit_PrintcardID)
        Dim BoxOrientationString As String = GetTableValueStr("boxorientation", "description", BoxOrientationID)
        EditPrintcardWindow.cmbOrient.Text = BoxOrientationString

        EditPrintcardWindow.PC_PaperCombinationID = CombinationID

        EDIT_BoxFormatID = GetTableValueInt("printcard", "boxformat_id", EditPrintcardWindow.Edit_PrintcardID)
        EditPrintcardWindow.cmbPrintcardStatus.Text = GetPrintcardStatusStr(PrintcardStatus_id)
        If EDIT_BoxFormatID = 40 Or EDIT_BoxFormatID = 50 Or EDIT_BoxFormatID = 60 Then  'Partition
            EditPrintcardWindow.cmbBoxFormat.Text = "Partition"
            EditPrintcardWindow.BoxDimensionGroup.Enabled = False
            EditPrintcardWindow.cPartitionGroup.Enabled = True
            EditPrintcardWindow.tPanel1.Text = 0
            EditPrintcardWindow.tPanel2.Text = 0
            EditPrintcardWindow.tPanel3.Text = 0
            EditPrintcardWindow.tPanel4.Text = 0
            EditPrintcardWindow.tFlap.Text = 0
            EditPrintcardWindow.tBoxHeight.Text = 0
            EditPrintcardWindow.tBoardLength.Text = 0
            EditPrintcardWindow.tBoardWidth.Text = 0
            EditPrintcardWindow.tLength.Text = 0
            EditPrintcardWindow.tWidth.Text = 0
            EditPrintcardWindow.tHeight.Text = 0
            EditPrintcardWindow.Board1Width.Text = GetTableValueInt("printcard", "Board1Width", EditPrintcardWindow.Edit_PrintcardID)
            EditPrintcardWindow.Board1Length.Text = GetTableValueInt("printcard", "Board1Length", EditPrintcardWindow.Edit_PrintcardID)
            EditPrintcardWindow.Board2Width.Text = GetTableValueInt("printcard", "Board2Width", EditPrintcardWindow.Edit_PrintcardID)
            EditPrintcardWindow.Board2Length.Text = GetTableValueInt("printcard", "Board2Length", EditPrintcardWindow.Edit_PrintcardID)
        Else
            EditPrintcardWindow.cmbBoxFormat.Text = boardtypeid
            EditPrintcardWindow.BoxDimensionGroup.Enabled = True
            EditPrintcardWindow.cPartitionGroup.Enabled = False
            EditPrintcardWindow.Board1Width.Text = 0
            EditPrintcardWindow.Board1Length.Text = 0
            EditPrintcardWindow.Board2Width.Text = 0
            EditPrintcardWindow.Board2Length.Text = 0
            EditPrintcardWindow.tLength.Text = objCustomerList.GetInsideDimension(EDIT_DimensionID, "length")
            EditPrintcardWindow.tWidth.Text = objCustomerList.GetInsideDimension(EDIT_DimensionID, "width")
            EditPrintcardWindow.tHeight.Text = objCustomerList.GetInsideDimension(EDIT_DimensionID, "height")

            EditPrintcardWindow.tPanel1.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel1")
            EditPrintcardWindow.tPanel2.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel2")
            EditPrintcardWindow.tPanel3.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel3")
            EditPrintcardWindow.tPanel4.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel4")
            EditPrintcardWindow.tBoardLength.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "boardlength")
            EditPrintcardWindow.tBoardWidth.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "boardwidth")
            EditPrintcardWindow.tBoxHeight.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "boxheight")
            EditPrintcardWindow.tFlap.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "flap")
            If objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "gluetab") = 0 Then
                EditPrintcardWindow.ComputeBoxSize()
            End If
        End If

        EditPrintcardWindow.tPaperCombination.Text = objCustomerList.GetCombination(CombinationID)


        EditPrintcardWindow.PC_CUSTOMERID = CustomerID

        p.UpdateProgress(60, "60%", "Loading EDIT PRINTCARD form...")
        EditPrintcardWindow.tCustomerName.Text = CustomerName
        EditPrintcardWindow.tPrincardPrefix.Text = objCustomerList.GetCustomerPrefix(CustomerID) & "-PC-"
        p.UpdateProgress(80, "80%", "Loading EDIT PRINTCARD form...")

        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        objPrintcard.EditdownLoadFile(id, EDIT_FileName, FileType)
        If CustomerFileID <> 1 Then
            objPrintcard.EditViewCustomerFile(CustomerFileID, EDIT_CustomerFile, FileType)
        End If
        objPrintcard = Nothing
        objCustomerList = Nothing
        EditPrintcardWindow.tPrintcardNotes.Text = GetTableValueStr("printcard", "notes", EditPrintcardWindow.Edit_PrintcardID)
        p.UpdateProgress(100, "100%", "Loading EDIT PRINTCARD form...")
        Me.Cursor = Cursors.Default
        p.CloseProgress()
        MainUI.StatusMessage.Text = EditPrintcardWindow.Text
    End Sub
    Private Sub CreatePrintcardCopyCMD()
        'Check revision # @table revision          
        Dim objCustomerList As Customer = New Customer
        Dim p As ExportRptProgress = ExportRptProgress.ShowProgress(Me)
        Dim BoxOrientationString As String = "" ' F-LWLW / F-WLWL
        Dim BoxOrientationID As Integer
        Me.Cursor = Cursors.WaitCursor

        p.UpdateProgress(10, "10%", "Loading printcard form...")
        PrintCard.Show()
        PrintCard.Activate()
        PrintCard.Text = "Copy Printcard"

        Dim PrintcardStatus_id As Integer

        Dim id As Integer = 0
        Dim FileType As String = ""
        Dim CustomerID As Integer = 0
        Dim FileName As String = ""
        Dim CustomerName As String = ""
        Dim BoxDesc As String = ""
        Dim boardtypeid As String = ""
        Dim BoxCategoryStr As String = ""
        Dim testid As Short = 0
        Dim boxformatid As Integer = 0
        Dim CombinationID As Integer = 0
        Dim revisionid As Integer = 0
        Dim fluteid As String = ""
        Dim diecutid As Integer = 0
        Dim jointid As String = ""
        Dim scale_id As String = ""
        Dim color1 As String = ""
        Dim color2 As String = ""
        Dim color3 As String = ""
        Dim color4 As String = ""
        Dim PrintcardNumber As Integer
        p.UpdateProgress(50, "50%", "Loading printcard form...")
        For Each cPrintcards In PrintcardGrid.SelectedRows
            PrintCard.Edit_PrintcardID = cPrintcards.cells("printcardid").value
            id = cPrintcards.cells("fileid").value 'Filename ID
            FileType = cPrintcards.cells("filetype").value
            FileName = cPrintcards.cells("filename").value
            CustomerName = cPrintcards.cells("organization_name").value
            CustomerID = cPrintcards.cells("id").value
            BoxDesc = cPrintcards.cells("box_description").value
            color1 = cPrintcards.cells("color1").value
            color2 = cPrintcards.cells("color2").value
            color3 = cPrintcards.cells("color3").value
            color4 = cPrintcards.cells("color4").value
            boardtypeid = cPrintcards.cells("boardtypeid").value
            BoxCategoryStr = cPrintcards.cells("boxcategory").value
            testid = cPrintcards.cells("testid").value
            fluteid = cPrintcards.cells("fluteid").value
            PrintcardStatus_id = cPrintcards.cells("printcard_status_id").value
            If IsNumeric(cPrintcards.cells("diecut_number").value) = True Then
                diecutid = cPrintcards.cells("diecut_number").value
            Else
                diecutid = 0
            End If
            jointid = cPrintcards.cells("jointid").value
            CombinationID = cPrintcards.cells("combinationid").value
            EDIT_DimensionID = cPrintcards.cells("dimensionid").value
            scale_id = cPrintcards.cells("scaleid").value
            PrintcardNumber = cPrintcards.cells("printcopyno").value
        Next

        PrintCard.tColor1.Text = color1
        PrintCard.tColor2.Text = color2
        PrintCard.tColor3.Text = color3
        PrintCard.tColor4.Text = color4
        PrintCard.tBoxDescription.Text = BoxDesc
        PrintCard.cmbBoardType.Text = fluteid
        PrintCard.cmbDiecut.Text = diecutid
        PrintCard.cmbBoxFormat.Text = boardtypeid
        PrintCard.cmbBoxCategory.Text = BoxCategoryStr
        PrintCard.cmbTest.Text = testid
        PrintCard.cmbJoint.Text = jointid
        PrintCard.cmbScale.Text = scale_id
        BoxOrientationID = GetTableValueInt("printcard", "boxorientation_id", PrintCard.Edit_PrintcardID)
        BoxOrientationString = GetTableValueStr("boxorientation", "description", BoxOrientationID)
        PrintCard.cmbOrient.Text = BoxOrientationString

        Dim BoxTestTypeID As Integer = GetTableValueInt("printcard", "test_type_id", PrintCard.Edit_PrintcardID)
        Dim BoxTestTypeIDString As String = GetTableValueStr("test_type", "code", BoxTestTypeID)
        PrintCard.cmbTestType.Text = BoxTestTypeIDString


        PrintCard.PC_PaperCombinationID = CombinationID
        PrintCard.tPrintcardNumber.Text = objCustomerList.GetPrintcardNum(CustomerID)
        EDIT_BoxFormatID = GetTableValueInt("printcard", "boxformat_id", PrintCard.Edit_PrintcardID)

        PrintCard.cmbPrintcardStatus.Text = GetPrintcardStatusStr(PrintcardStatus_id)

        If EDIT_BoxFormatID = 40 Or EDIT_BoxFormatID = 50 Or EDIT_BoxFormatID = 60 Then 'Partition
            PrintCard.Board1Width.Text = GetTableValueInt("printcard", "Board1Width", PrintCard.Edit_PrintcardID)
            PrintCard.Board1Length.Text = GetTableValueInt("printcard", "Board1Length", PrintCard.Edit_PrintcardID)
            PrintCard.Board2Width.Text = GetTableValueInt("printcard", "Board2Width", PrintCard.Edit_PrintcardID)
            PrintCard.Board2Length.Text = GetTableValueInt("printcard", "Board2Length", PrintCard.Edit_PrintcardID)
        Else
            PrintCard.tLength.Text = objCustomerList.GetInsideDimension(EDIT_DimensionID, "length")
            PrintCard.tWidth.Text = objCustomerList.GetInsideDimension(EDIT_DimensionID, "width")
            PrintCard.tHeight.Text = objCustomerList.GetInsideDimension(EDIT_DimensionID, "height")

            PrintCard.tPanel1.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel1")
            PrintCard.tPanel2.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel2")
            PrintCard.tPanel3.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel3")
            PrintCard.tPanel4.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "panel4")
            PrintCard.tBoardLength.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "boardlength")
            PrintCard.tBoardWidth.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "boardwidth")
            PrintCard.tBoxHeight.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "boxheight")
            PrintCard.tFlap.Text = objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "flap")

        End If

        PrintCard.tPaperCombination.Text = objCustomerList.GetCombination(CombinationID)

        If objCustomerList.GetBoxInfo(CustomerID, PrintcardNumber, "gluetab") = 0 Then
            PrintCard.ComputeBoxSize()
        End If
        PrintCard.PC_CUSTOMERID = CustomerID

        p.UpdateProgress(60, "60%", "Loading printcard form...")
        PrintCard.tCustomerName.Text = CustomerName
        PrintCard.tPrincardPrefix.Text = objCustomerList.GetCustomerPrefix(CustomerID) & "-PC-"
        PrintCard.tPrintcardNumber.Text = objCustomerList.GetPrintcardNum(CustomerID)
        p.UpdateProgress(80, "80%", "Loading printcard form...")
        objCustomerList = Nothing
        p.UpdateProgress(100, "100%", "Loading printcard form...")
        Me.Cursor = Cursors.Default
        p.CloseProgress()
        MainUI.StatusMessage.Text = PrintCard.Text
        MainUI.Text = ApplicationTitle & " - Copy Printcard"
    End Sub
    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        LoadData()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        LoadData()
    End Sub

    Private Sub cmdSearchDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearchDB.Click
        Dim ColumnName As String = ""
        Select Case gridColumn.Text
            Case "Customer"
                ColumnName = "organization_name"
            Case "Filename"
                ColumnName = "filename"
            Case "Box Description"
                ColumnName = "box_description"
            Case "Inside Dimension"
                ColumnName = "insidedimension"
            Case "Board Size"
                ColumnName = "boardsize"
            Case "Printcard No."
                ColumnName = "printcardno"
            Case "Diecut"
                ColumnName = "diecut_number"
            Case "Rack Location"
                ColumnName = "racklocation"
            Case "Rubberdie Location"
                ColumnName = "rubber_location"
            Case "Box Description"
                ColumnName = "box_description"
            Case "Status"
                ColumnName = "status"
            Case "*"
                ColumnName = "*"
            Case Else
                MessageBox.Show("Column name not yet supported.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        If cSearchThis.Text = "*" Then
            If chkDisplayHint.Checked = True Then
                MessageBox.Show("TIP: Change the value at Limit Results if you want less or more rows to display.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If            
        End If
        objPrintcard.Search(Me.PrintcardGrid, txtYear.Text, MonthNumber, ColumnName, cSearchThis.Text, Me.chkSeachByDate, cmbNumRecords.Text)
        objPrintcard = Nothing
        lCountRows.Text = "Rowcount: " & PrintcardGrid.RowCount
    End Sub

    Private Sub CreateOrder()
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        For Each OrderItem In PrintcardGrid.SelectedRows
            ORDER_fileid = OrderItem.cells("fileid").value 'Filename ID
            ORDER_FileType = OrderItem.cells("filetype").value
            ORDER_FileName = OrderItem.cells("filename").value
            ORDER_CustomerName = OrderItem.cells("organization_name").value
            ORDER_BoxDescription = OrderItem.cells("box_description").value
            ORDER_PrintcardNum = OrderItem.cells("printcardno").value
        Next
        CreateOrderForm.ShowDialog()
        objPrintcard = Nothing
    End Sub
    Private Sub cmdViewCustomerFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdViewCustomerFile.Click
        ViewCustomerFileCMD()
    End Sub
    Private Sub ViewCustomerFileCMD()
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        Dim FileType As String = ""
        Dim FileName As String = ""
        Dim BoxDescription As String = ""
        Dim PrintcardNumber As String = ""
        For Each cPrintcards In PrintcardGrid.SelectedRows
            CustomerFileID = cPrintcards.cells("customer_file_id").value 'Filename ID
            BoxDescription = cPrintcards.cells("box_description").value
            PrintcardNumber = cPrintcards.cells("printcardno").value
            If CustomerFileID = 1 Then
                MsgBox("There is no customer file attachment for Printcard #: " & PrintcardNumber & " with description: " & BoxDescription, MsgBoxStyle.Exclamation, "TSD Inventory System")
                Exit Sub
            End If
            FileType = cPrintcards.cells("customerfiletype").value
            FileName = cPrintcards.cells("customerfile").value
        Next
        objPrintcard.ViewCustomerFile(CustomerFileID, FileName, FileType)
        objPrintcard = Nothing
    End Sub
    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        ExportToExcelCMD()
    End Sub
    Private Sub ExportToExcelCMD()
        Dim editor As DGVEExcelExportSettingsEditor = New DGVEExcelExportSettingsEditor()
        Dim settings As DGVEExcelExportSettings = New DGVEExcelExportSettings()
        Dim dialog As DGVEBaseExportSettingsEditorForm = DGVEExportSettingsEditorFormBuilder.CreateWrappingForm(editor)
        dialog.Settings = settings
        If (DialogResult.OK <> dialog.ShowDialog()) Then
            Return
        End If
        Dim exporter As DGVEExcelExporter = New DGVEExcelExporter()
        AddHandler exporter.ExportFailed, AddressOf exporter_ExportFailed
        exporter.Export(PrintcardGrid, settings)
    End Sub
    Private Sub exporter_ExportFailed(ByVal sender As Object, ByVal e As CompletIT.Windows.Forms.Export.ExportFailedEventArgs)
        'Show message box with the occured exception 
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DGVEPrintManager.PrintPreview(PrintcardGrid)
    End Sub

    Private Sub ViewCustomerFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewCustomerFileToolStripMenuItem.Click
        ViewCustomerFileCMD()
    End Sub

    Private Sub ViewPrintcardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPrintcardToolStripMenuItem.Click
        ViewPrintcardCMD()
    End Sub

    Private Sub CreateCopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateCopyToolStripMenuItem.Click
        If LoginTSD.UserPermission <> AdminRole Or LoginTSD.UserPermission <> GraphicArtistRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        CreatePrintcardCopyCMD()
    End Sub

    Private Sub DeletePrintcardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePrintcardToolStripMenuItem.Click
        If LoginTSD.UserPermission <> AdminRole And LoginTSD.UserPermission <> GraphicArtistRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        Else
            If MessageBox.Show("Do you want to delete this row?", ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                MessageBox.Show("Delete row confirmed!")
            End If
        End If
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        ExportToExcelCMD()
    End Sub

    Private Sub cmdEditPrintcard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditPrintcard.Click
        If LoginTSD.UserPermission <> AdminRole And LoginTSD.UserPermission <> GraphicArtistRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        'Check if price was set 
        Dim CheckPrintcardID As Integer
        For Each cPrintcards In PrintcardGrid.SelectedRows
            CheckPrintcardID = cPrintcards.cells("printcardid").value
        Next
        'If IsPrintcardValidForEdit(CheckPrintcardID) = True Then
        '    MessageBox.Show("Edit is no longer possible, because the price has been already set for this printcard.  " & Chr(13) & _
        '                    "Please use the Create Copy instead or request that the price be invalidated for this item.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Return
        'End If
        NewEditPrintcard()
    End Sub

    Private Sub EditPrintcardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditPrintcardToolStripMenuItem.Click
        If LoginTSD.UserPermission <> AdminRole And LoginTSD.UserPermission <> GraphicArtistRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        NewEditPrintcard()
    End Sub


    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        LoadData()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub gridColumn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridColumn.SelectedIndexChanged
        cSearchThis.Clear()
        cSearchThis.Focus()
    End Sub


    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertiesToolStripMenuItem.Click
        PrintcardProperties.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateOrder.Click
        If LoginTSD.UserPermission <> AdminRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        'Check if price was set 
        Dim CheckPrintcardID As Integer
        Dim PrintcardStatus As String = ""
        For Each cPrintcards In PrintcardGrid.SelectedRows
            CheckPrintcardID = cPrintcards.cells("printcardid").value
            PrintcardStatus = cPrintcards.cells("status").value
        Next

        If PrintcardStatus = "Obsolete" Then
            MessageBox.Show("This Printcard is obsolete.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        If IsPrintcardValidForEdit(CheckPrintcardID) = False Then
            MessageBox.Show("Creating an order is not possible, because the price was not set for this printcard.  " & Chr(13) & _
                            "Please set the price for this item before creating an Sales Order.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Me.Cursor = Cursors.WaitCursor
        CreateOrder()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdSetPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetPrice.Click
        If LoginTSD.UserPermission <> AdminRole Then
            MessageBox.Show("Permission denied! Please select a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        'Check if price was set 

        'For Each cPrintcards In PrintcardGrid.SelectedRows
        '    BoxFormatID = cPrintcards.cells("boxformat_id").value
        'Next
        'If BoxFormatID = 40 Then
        '    MessageBox.Show("Pricing for partition not yet implemented. Please come back soon...", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return
        'End If

        Me.Cursor = Cursors.WaitCursor
        PriceSet.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkSeachByDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeachByDate.CheckedChanged
        If chkSeachByDate.Checked = True Then
            cmbMonth.Enabled = True
            txtYear.Enabled = True
        Else
            cmbMonth.Enabled = False
            txtYear.Enabled = False
        End If
    End Sub

    Private Sub PrintcardGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles PrintcardGrid.CellContentClick

    End Sub
End Class