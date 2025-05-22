Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Imports System.IO.Ports
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Security.Cryptography

Public Class MainUI
    Dim objPrintcard As ClassPrintcard
    'Local variables for Printcard
    Public NewPrintcard As PrintCard
    Dim CustomerID As Integer
    Dim BoxDescription As String
    Dim BoxFormatID As Integer
    Dim FluteID As Integer
    Dim TestID As Integer
    Dim JointID As Integer
    Dim Paper_combinationID As Integer
    Dim DimensionID As Integer
    Dim Color1 As String
    Dim Color2 As String
    Dim color3 As String
    Dim Color4 As String
    Dim Flap As Integer
    Dim UnitID As Integer
    Dim ScaleID As Integer
    Dim DiecutID As Integer
    Dim PrintcardNo As Integer
    Dim FilenameID As Integer
    Dim CustomerFileID As Integer 'If available, will set this to foreign id file else will default to 1(i.e., no customer file attached
    Dim PrintcardCreated As String
    Public DocumentType As String 'Variable to determine of whether the printcard is New, Revised or Copied during the 'Save' command.
    Dim GlueTab, Panel1, Panel2, Panel3, Panel4 As Single
    Dim BoxHeight, BoardWidth, BoardLength As Double
    Public PrintcardInstanceName As String

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub
    Public Printcard_ChildWindow As Integer
    Private Sub NewPrintcardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewPrintcardToolStripMenuItem.Click
        CreateNewPrintCard()
    End Sub
    Private Sub CreateNewPrintCard()
        Me.Cursor = Cursors.WaitCursor
        ' Create a new instance of the child form.
        'Dim ChildForm As New System.Windows.Forms.Form
        
        NewPrintcard = New PrintCard
        ' Make it a child of this MDI form before showing it.
        NewPrintcard.MdiParent = Me

        Printcard_ChildWindow += 1

        If Printcard_ChildWindow > 1 Then
            Me.Cursor = Cursors.Default
            MessageBox.Show("Multiple Printcard window was currently disabled.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        NewPrintcard.Name = "Printcard_num" & Printcard_ChildWindow
        NewPrintcard.Text = "New Printcard - # " & Printcard_ChildWindow

        NewPrintcard.Show()

        Me.Cursor = Cursors.Default

        'Me.Cursor = Cursors.WaitCursor
        'Dim p As ExportRptProgress = ExportRptProgress.ShowProgress(Me)
        'Dim objForms As Form
        'Dim n As Integer = Me.MdiChildren.Count
        'If n > 0 Then
        '    For Each objForms In Me.MdiChildren
        '        If objForms.cName = PrintCard.Name Then
        '            PrintCard.Activate()
        '            PrintCard.Text = "New Printcard"
        '        Else
        '            p.UpdateProgress(10, "10%", "Loading new printcard form...")
        '            PrintCard.Show()
        '            PrintCard.Text = "New Printcard"
        '            p.UpdateProgress(100, "100%", "Loading new printcard form...")
        '        End If
        '    Next
        'Else
        '    p.UpdateProgress(10, "10%", "Loading new printcard form...")
        '    PrintCard.Show()
        '    PrintCard.Text = "New Printcard"
        '    p.UpdateProgress(100, "100%", "Loading new printcard form...")
        'End If
        'p.CloseProgress()
        'Me.Cursor = Cursors.Default
    End Sub

    Private Sub RegisterColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterColorToolStripMenuItem.Click
        ColorRegistration.Show()
    End Sub

    Private Sub tbDocumentSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbDocumentSave.Click
       
        Dim WindowTitle As String = UCase(Me.Text)
        If WindowTitle.Contains("EDIT CUSTOMER") Then
            If CustomerSetup.ValidateEntries() = False Then
                MsgBox("Fields Customer name, country name and Printcard prefix are required!", MsgBoxStyle.Critical, "TSD Inventory System")
                Exit Sub
            End If
            SaveCustomer() 'If save successful then resume  
            tbDocumentSave.Enabled = False
            CustomerSetup.grCustomer.Enabled = False
            CustomerSetup.CustomerGrid.Enabled = True
            tbCancelEdit.Enabled = False
            bDocumentEdit.Enabled = True
            btAddCustomer.Enabled = True
            CustomerSetup.CustomerGrid.Focus()
            StatusMessage.Text = "Edit Customer saved."
            Exit Sub
        ElseIf WindowTitle.Contains("ADD CUSTOMER") Then
            If CustomerSetup.ValidateEntries() = False Then
                MsgBox("Fields Customer name, country name and Printcard prefix are required!", MsgBoxStyle.Critical, "TSD Inventory System")
                Exit Sub
            End If
            AddCustomer()
            If CustomerSetup.ErrorSavingCustomer = True Then
                Exit Sub
            End If
            tbDocumentSave.Enabled = False
            CustomerSetup.grCustomer.Enabled = False
            CustomerSetup.CustomerGrid.Enabled = True
            tbCancelEdit.Enabled = False
            bDocumentEdit.Enabled = True
            btAddCustomer.Enabled = True
            CustomerSetup.CustomerGrid.Focus()
            StatusMessage.Text = "New Customer saved."
            Exit Sub
        ElseIf WindowTitle.Contains(UCase(DiecutSetup.Text)) Then
            DiecutSetup.UpdateDiecut()
            DiecutSetup.GroupInputDiecut.Enabled = False
            DiecutSetup.DiecutGrid.Enabled = True
            tbDocumentSave.Enabled = False
            btAddCustomer.Enabled = True
            bDocumentEdit.Enabled = True
            tbCancelEdit.Enabled = False
            DiecutSetup.Text = "Diecut Setup"
            Me.Text = ApplicationTitle & " - " & DiecutSetup.Text
            StatusMessage.Text = DiecutSetup.Text
            DiecutSetup.DiecutGrid.Focus()
            Exit Sub
        End If

        Dim ActivePrincardWin As PrintCard = Me.ActiveMdiChild

        If WindowTitle.Contains("NEW PRINTCARD") Then
            'SavePrintcard()
            ActivePrincardWin.PC_SavePrintcard()
        ElseIf WindowTitle.Contains("COPY PRINTCARD") Then
            'SavePrintcard()
            ActivePrincardWin.PC_SavePrintcard()
        ElseIf WindowTitle.Contains("EDIT PRINTCARD") Then
            ActivePrincardWin.PC_UpdatePrintcard()
            Exit Sub
        End If
    End Sub
    Private Sub AddCustomer()
        Me.Cursor = Cursors.WaitCursor
        ' add procedure here to add customer
        CustomerSetup.AddNewCustomer()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub SaveCustomer()
        Me.Cursor = Cursors.WaitCursor
        CustomerSetup.SaveEditCustomer()
        Me.Cursor = Cursors.Default
    End Sub
  
    Private Sub SavePrintcardTransaction()

    End Sub
    '---------------------------------------------------------------------------------------------------------------------
    Private Sub SavePrintcard()
        Try            
            Dim PrePrintcardNumber As Integer

            objPrintcard = New ClassPrintcard

            If CheckVitalValues() = 1 Then
                Exit Sub
            End If

            Dim PrintcardNumber As Integer = PrintCard.tPrintcardNumber.Text
            CustomerID = PrintCard.PC_CUSTOMERID
            If objPrintcard.CheckExistingPrincardNumber(CustomerID, PrintcardNumber) = 1 Then
                MsgBox("Printcard number was already in used for this customer.", MsgBoxStyle.Exclamation, "TSD Inventory System")
                PrintCard.tPrintcardNumber.Focus()
                PrintCard.tPrintcardNumber.SelectAll()
                Exit Sub
            End If

            PrePrintcardNumber = Convert.ToInt32(PrintCard.tPrintcardNumber.Text)
            PrePrintcardNumber = PrePrintcardNumber - 1

            'Prepare to upload printcard file and get id from the uploaded file
            Dim p As ExportRptProgress = ExportRptProgress.ShowProgress(Me)

            Dim sFileToUpload As String = ""
            Dim cCustomerFile As String = ""
            Dim CustomerFileExtension As String = ""
            p.UpdateProgress(10, "10%", "Uploading file to PostgreSQL server...")
            sFileToUpload = LTrim(RTrim(PrintCard.tFilePath.Text))


            'Check file extension to determine the file type
            Dim InternalGraphicExtension As String = System.IO.Path.GetExtension(sFileToUpload)

            If PrintCard.tCustomerFile.Text <> "" Then
                cCustomerFile = LTrim(RTrim(PrintCard.tCustomerFile.Text))
                CustomerFileExtension = System.IO.Path.GetExtension(cCustomerFile)
                Dim CustomerFileExtensionType As String = GetExtensionType(CustomerFileExtension)
                p.UpdateProgress(30, "30%", "Uploading customer file to PostgreSQL server...")
                CustomerFileID = objPrintcard.UploadCustomerFile(cCustomerFile, CustomerFileExtensionType)
            Else
                CustomerFileID = 1 ' No customer attached file
            End If

            Dim FileExtension As String = GetExtensionType(InternalGraphicExtension)
            p.UpdateProgress(40, "40%", "Uploading file to PostgreSQL server...")
            FilenameID = objPrintcard.upLoadImageOrFile(sFileToUpload, FileExtension)

            If objPrintcard.ErrorFlag = 4 Then
                p.CloseProgress()
                MsgBox("Error in uploading file. Please contact MIS immediately!", MsgBoxStyle.Exclamation & vbCritical, "TSD Inventory System")
                objPrintcard.ErrorFlag = 0
                Exit Sub
            End If
            p.UpdateProgress(100, "100%", "Uploading file to PostgreSQL server...")

            'Upload finished, with table filename's table ID value
            ' -------------------------------------------------------------------- 

            If PrintCard.PC_PaperDimensionID = 0 Then ' prepare to insert Inside Dimension and set the table ID
                p.UpdateProgress(10, "10%", "Inserting new Box Inside Dimension...")
                PrintCard.PC_PaperDimensionID = objPrintcard.InsertNewID(PrintCard.tLength.Text, PrintCard.tWidth.Text, PrintCard.tHeight.Text, PrintCard.PC_UnitID)
                If objPrintcard.ErrorFlag = 3 Then
                    p.CloseProgress()
                    MsgBox("Error in Inserting New Inside Dimension. Press OK to rollback changes made to database.", MsgBoxStyle.Exclamation & vbCritical, "TSD Inventory System")
                    objPrintcard.DeleteData("graphicfiles", FilenameID) 'Delete the saved graphic file
                    If PrintCard.tCustomerFile.Text <> "" Then 'Attachment should be removed also
                        objPrintcard.DeleteData("customerfile", CustomerFileID) ' Delete the saved customer file
                    End If
                    objPrintcard.ErrorFlag = 0
                    Exit Sub
                End If
                p.UpdateProgress(100, "100%", "Inserting new Box Inside Dimension...")
            End If

            ''Save printcard series
            'p.UpdateProgress(10, "10%", "Saving printcard series...")
            'objPrintcard.InsertPrintcardSeries(PrintCard.customer_id, PrintCard.tPrintcardNumber.Text)
            'If objPrintcard.ErrorFlag = 2 Then
            '    p.CloseProgress()
            '    MsgBox("Error in Inserting Printcard series number. Please contact MIS immediately!", MsgBoxStyle.Exclamation & vbCritical, "TSD Inventory System")
            '    objPrintcard.DeleteData("graphicfiles", FilenameID)  'Delete the saved graphic file
            '    If PrintCard.tCustomerFile.Text <> "" Then 'Attachment should be remove also
            '        objPrintcard.DeleteData("customerfile", CustomerFileID) ' Delete the saved customer file
            '    End If
            '    objPrintcard.UpdateData("printcard_series", PrintcardSeriesID, "printcardno", PrePrintcardNumber) ' Update to the previous printcard number.
            '    objPrintcard.ErrorFlag = 0
            '    Exit Sub
            'End If

            'Finally save Printcard 

            BoxDescription = PrintCard.tBoxDescription.Text
            BoxFormatID = PrintCard.PC_BoxFormatID
            FluteID = PrintCard.PC_cBoardTypeID
            TestID = PrintCard.PC_TESTID
            JointID = PrintCard.PC_JointID
            Paper_combinationID = PrintCard.PC_PaperCombinationID
            DimensionID = PrintCard.PC_PaperDimensionID
            Color1 = PrintCard.tColor1.Text
            Color2 = PrintCard.tColor2.Text
            color3 = PrintCard.tColor3.Text
            Color4 = PrintCard.tColor4.Text
            Flap = PrintCard.tFlap.Text
            UnitID = PrintCard.PC_UnitID
            ScaleID = PrintCard.PC_ScaleID
            DiecutID = PrintCard.PC_Diecut_ID
            PrintcardNo = PrintCard.tPrintcardNumber.Text
            PrintcardCreated = PrintCard.cPrintcardCreated.Value.ToString
            'Box size
            BoardLength = PrintCard.tBoardLength.Text
            BoardWidth = PrintCard.tBoardWidth.Text
            BoxHeight = PrintCard.tBoxHeight.Text
            GlueTab = PrintCard.tGlueTab.Text
            Panel1 = PrintCard.tPanel1.Text
            Panel2 = PrintCard.tPanel2.Text
            Panel3 = PrintCard.tPanel3.Text
            Panel4 = PrintCard.tPanel4.Text

            'FilenameID see above.

            objPrintcard.SavePrintcard(CustomerID, _
                                       BoxDescription, _
                                       BoxFormatID, _
                                       FluteID, TestID, JointID, _
                                       Paper_combinationID, DimensionID, _
                                       Color1, Color2, color3, Color4, _
                                       Flap, UnitID, ScaleID, DiecutID, PrintcardNo, _
                                       FilenameID, PrintcardCreated, PrintCard.tPrintcardNotes.Text, _
                                       BoardLength, BoardWidth, BoxHeight, GlueTab, _
                                       Panel1, Panel2, Panel3, Panel4, CustomerFileID)
            If objPrintcard.ErrorFlag = 1 Then
                p.CloseProgress()
                MsgBox("Error in saving printcard. Please contact MIS immediately! Press OK to abort changes.", MsgBoxStyle.Exclamation & vbCritical, "TSD Inventory System")
                objPrintcard.DeleteData("graphicfiles", FilenameID) ' Delete the saved graphic file
                If PrintCard.tCustomerFile.Text <> "" Then 'Attachment should be removed also
                    objPrintcard.DeleteData("customerfile", CustomerFileID)
                End If
                objPrintcard.UpdateData("printcard_series", CustomerID, "printcardno", PrePrintcardNumber) ' Update to the previous printcard number.
                objPrintcard.ErrorFlag = 0
                Exit Sub
            End If
            p.UpdateProgress(100, "100%", "Saving printcard...")
            p.CloseProgress()

            'Reset printcard form
            PrintCard.ResetForm()
            Dim objNewPrintcardNumber As Customer = New Customer
            PrintCard.tPrintcardNumber.Text = objNewPrintcardNumber.GetPrintcardNum(CustomerID)

            objNewPrintcardNumber = Nothing
        Catch ex As ApplicationException
            MsgBox(ex.Message & " Error in SaveNewPrintcard @ MainUI.vb", MsgBoxStyle.Critical, "TSD Inventory System")
        End Try
        StatusMessage.Text = "Princard successfully saved."
    End Sub
    Private Function CheckVitalValues() As Integer
        Dim GoAheadSavePrintcard As Integer = 0
        If PrintCard.tCustomerName.Text = "" Then            
            Me.Cursor = Cursors.WaitCursor
            CustomerList.ShowDialog()
            Me.Cursor = Cursors.Default
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If

        If PrintCard.tBoxDescription.Text = "" Then
            MsgBox("Box description is required!")
            PrintCard.tBoxDescription.Focus()
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If PrintCard.tLength.Text = "" Or PrintCard.tWidth.Text = "" Or PrintCard.tHeight.Text = "" Then
            MsgBox("Inside dimension is required!")
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If PrintCard.tPaperCombination.Text = "" Then
            MsgBox("WARNING: No Paper combination entered!", MsgBoxStyle.Critical)
            PrintCard.cmdGetCombination.Focus()
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If PrintCard.tFilePath.Text = "" Then
            MsgBox("File(CDR/PDF) is required to save Printcard.")
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        Return GoAheadSavePrintcard
    End Function
    Private Function GetExtensionType(ByVal FileExtension As String) As String
        Dim ReturnExtension As String = ""
        FileExtension = LCase(FileExtension)
        If FileExtension = ".jpg" Or _
           FileExtension = ".bmp" Or _
           FileExtension = ".png" Or _
           FileExtension = ".jpeg" Or _
           FileExtension = ".tif" Then
            ReturnExtension = "Image"
        ElseIf FileExtension = ".cdr" Or _
           FileExtension = ".cdt" Or _
           FileExtension = ".cmx" Or _
           FileExtension = ".cdx" Or _
           FileExtension = ".cpx" Then
            ReturnExtension = "CorelDraw"
        ElseIf FileExtension = ".xcf" Then
            ReturnExtension = "GIMP"
        ElseIf FileExtension = ".cpt" Then
            ReturnExtension = "Photopaint"
        ElseIf FileExtension = ".ai" Then
            ReturnExtension = "Illustrator"
        ElseIf FileExtension = ".psd" Then
            ReturnExtension = "Photoshop"
        ElseIf FileExtension = ".pdf" Then
            ReturnExtension = "PDF File"
        Else
            ReturnExtension = "Unknown format"
        End If
        Return ReturnExtension
    End Function
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbCreateNewPrintcard.Click
        If LoginTSD.UserPermission <> AdminRole And LoginTSD.UserPermission <> GraphicArtistRole Then
            MessageBox.Show("Permission denied! Please login as a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        Else
            CreateNewPrintCard()
        End If
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Cursor = Cursors.WaitCursor
        cBrowsePrintcard()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cBrowsePrintcard()
        Dim objForms As Form
        Dim n As Integer = Me.MdiChildren.Count
        If n > 0 Then
            For Each objForms In Me.MdiChildren
                If objForms.Name = BrowsePrintcard.Name Then
                    BrowsePrintcard.Activate()
                Else
                    BrowsePrintcard.Show()
                End If
            Next
        Else
            BrowsePrintcard.Show()
        End If
    End Sub

    Private Sub MainUI_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        objPrintcard = Nothing
    End Sub

    Private Sub MainUI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = ApplicationTitle & " - Connected to: " & DataBaseHost & " Database: " & DatabaseName
        StatusMessage.Text = ""
    End Sub

    Private Sub BrowsePrintcardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowsePrintcardToolStripMenuItem.Click
        cBrowsePrintcard()
    End Sub

    Private Sub SavePrintcardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePrintcardToolStripMenuItem.Click
        'SavePrintcard()
        PrintCard.PC_SavePrintcard()
    End Sub


    Private Sub CustomerSettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerSettingToolStripMenuItem.Click
        'MessageBox.Show("Call MIS Support to add customer. OLIO/Platinum customers were already imported.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Exit Sub
        Me.Cursor = Cursors.WaitCursor
        CustomerSetup.Show()
        CustomerSetup.Activate()
        Me.Cursor = Cursors.Default
    End Sub
    Public Sub subEditCustomer()
        CustomerSetup.grCustomer.Enabled = True
        CustomerSetup.CustomerGrid.Enabled = False
        bDocumentEdit.Enabled = False
        tbCancelEdit.Enabled = True
        CustomerSetup.Text = "Edit Customer"
        Me.Text = ApplicationTitle & " - " & CustomerSetup.Text
        tbDocumentSave.Enabled = True
        CustomerSetup.tCustomerName.Focus()
        btAddCustomer.Enabled = False
        CustomerSetup.chkShowInActiveCust.Enabled = False
        StatusMessage.Text = CustomerSetup.Text
    End Sub
    Public Sub subEditDiecut()

    End Sub
    Private Sub bDocumentEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bDocumentEdit.Click
        If Me.Text.Contains(CustomerSetup.Text) Then
            subEditCustomer()
        ElseIf Me.Text.Contains(DiecutSetup.Text) Then
            subEditDiecut()
        Else
            MessageBox.Show("No document to edit!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub


    Private Sub tbCancelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbCancelEdit.Click
        If Me.Text.Contains(CustomerSetup.Text) Then
            CustomerSetup.grCustomer.Enabled = False
            CustomerSetup.CustomerGrid.Enabled = True
            bDocumentEdit.Enabled = True
            tbCancelEdit.Enabled = False
            tbDocumentSave.Enabled = False
            CustomerSetup.CustomerGrid.Focus()
            btAddCustomer.Enabled = True
            CustomerSetup.CancelAdd()
            CustomerSetup.chkShowInActiveCust.Enabled = True
            CustomerSetup.Text = "Customer Setup"
            Me.Text = ApplicationTitle & " - " & CustomerSetup.Text
            StatusMessage.Text = CustomerSetup.Text
        End If
    End Sub

    Private Sub bDocumentEdit_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles bDocumentEdit.MouseHover
        If Me.Text.Contains(CustomerSetup.Text) Then
            bDocumentEdit.ToolTipText = "Enables you to edit customer."
        End If
    End Sub
    Public Sub subAddCustomer()
        Dim objGetNewID As ClassManageCustomer = New ClassManageCustomer
        btAddCustomer.Enabled = False
        tbCancelEdit.Enabled = True
        tbDocumentSave.Enabled = True
        CustomerSetup.CustomerGrid.Enabled = False
        CustomerSetup.grCustomer.Enabled = True
        CustomerSetup.ResetFormNew()
        CustomerSetup.tCustomerName.Focus()
        CustomerSetup.Text = "Add Customer"
        Me.Text = ApplicationTitle & " - " & CustomerSetup.Text
        bDocumentEdit.Enabled = False
        CustomerSetup.chkShowInActiveCust.Enabled = False
        ' Get customer new ID
        CustomerSetup.lNewCustomerID.Text = objGetNewID.GetNewCustomerID()
        StatusMessage.Text = CustomerSetup.Text
        objGetNewID = Nothing
    End Sub
    Public Sub subAddDiecut()

    End Sub
    Private Sub btAddCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAddCustomer.Click
        If Me.Text.Contains(CustomerSetup.Text) Then
            subAddCustomer()
        ElseIf Me.Text.Contains(DiecutSetup.Text) Then
            subAddDiecut()
        Else
            MsgBox("No active document to add data for.", MsgBoxStyle.Critical, "You should not see me!")
        End If
    End Sub
    Private Sub DiecutManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiecutManagementToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        DiecutSetup.Show()
        DiecutSetup.Activate()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub MainUI_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim userName = UCase(My.User.Name)
        'uncomment when publish

        If userName <> "SMPC\ALLAN.REGISTOS" Then
            LoginTSD.ShowDialog() 'Uncomment when publish
        Else
            LoginTSD.UserPermission = 10
            LoginTSD.SystemUserID = 10
        End If

        If LoginTSD.UserPermission <> AdminRole And LoginTSD.UserPermission <> GraphicArtistRole Then
            tbCreateNewPrintcard.Enabled = False
            NewPrintcardToolStripMenuItem.Enabled = False
            DiecutManagementToolStripMenuItem.Enabled = False
        End If
    End Sub
    Private Sub tbDocumentSave_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbDocumentSave.MouseMove
        If Me.Text.Contains(PrintCard.Text) Then
            tbDocumentSave.Text = "Save Printcard"
        Else
            tbDocumentSave.Text = "Save Customer"
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        'Dim YearToday As Date
        'Dim YearVal As String
        'YearToday = Date.Now
        'YearVal = YearToday.ToString("yyyy")  
        'MsgBox(ApplicationTitle & Chr(13) & Chr(10) & "Version #: " & Application.ProductVersion & "" & Chr(13) & "Copyright (c) Steniel Mindanao Packaging Corporation - " & YearVal & "", MsgBoxStyle.Information, ApplicationTitle)
        AboutTSDInventory.ShowDialog()
    End Sub

    Private Sub RubberDieManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RubberDieManagementToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        If LoginTSD.UserPermission <> 10 And LoginTSD.UserPermission <> 20 Then
            MsgBox(LoginTSD.UserPermission)
            MessageBox.Show("Permission denied! Please login as a privileged user.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        RubberDieCreate.Show()
        RubberDieCreate.Activate()
        Me.Cursor = Cursors.Default
    End Sub

End Class
