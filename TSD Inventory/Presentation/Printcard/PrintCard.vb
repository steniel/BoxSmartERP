Imports System.IO
Imports Npgsql
Imports System.Drawing

Public Class PrintCard
    'Store existing printcard var if in Edit mode
    Dim ExistingGraphicFile As String
    Dim ExistingCustFile As String
    Dim ExistingPrintcardNumber As Integer
    Public PC_CUSTOMERID As Integer
    Public Customer_Name As String
    Private BOX_FORMAT_DESCRIPTION As String
    Public PC_PaperCombinationID As Integer
    Public PreviousFlute As String 'If flute was changed to CB-Flute then save the previous flute to this variable
    Dim objCustomer As Customer = New Customer
    Dim objPaper As PaperDimension = New PaperDimension
    Dim objUnits As units = New units
    Public PC_BoxFormatID As Integer
    Public PC_PaperDimensionID As Integer
    Public PC_Diecut_ID As Integer
    Public PC_cBoardTypeID As Integer
    Public PC_TESTID As Integer
    Public PC_ScaleID As Integer
    Public PC_PrintcardStatusID As Integer
    Public PC_UnitID As Integer
    Public PC_JointID As Integer
    Public PC_BOX_CATEGORY_ID As Integer
    Dim PC_BoxOrientationID As Integer
    Dim NotesCharCount As Short = 255
    'Compute box, need to set this to reduce server usage tax, we only need to query the values where there are changes in Boxtype, Joint and Flute
    Dim factor1 As Short
    Dim factor2 As Short
    Dim factor3 As Short
    Dim factor4 As Short

    Dim GlueTab As Single
    Dim FactorFlap As Single
    Dim FactorHeight As Single
    Dim BoxFormatType As String = ""
    Dim Panel1, Panel2, Panel3, Panel4 As Single 'swap panels
    Dim BoxHeight, BoardWidth, BoardLength, BoxFlap As Double

    Public Edit_PrintcardID As Integer


    Private Sub PrintCard_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        MainUI.tbDocumentSave.Enabled = True
        MainUI.SavePrintcardToolStripMenuItem.Enabled = True
        MainUI.PrintcardInstanceName = Me.Text
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        MainUI.bDocumentEdit.Enabled = False
        MainUI.btAddCustomer.Enabled = False          
    End Sub

    Private Sub PrintCard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainUI.tbDocumentSave.Enabled = False
        MainUI.SavePrintcardToolStripMenuItem.Enabled = False
        MainUI.Text = ApplicationTitle
        MainUI.Printcard_ChildWindow = 0

    End Sub

    Private Sub PrintCard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Result As DialogResult

        If UCase(Me.Text) = "EDIT PRINTCARD" Then
            e.Cancel = False
        Else
            If tBoxDescription.Text <> "" Or tFilePath.Text <> "" Or tLength.Text <> "" Or tWidth.Text <> "" Or tHeight.Text <> "" Then
                Result = MessageBox.Show("Your changes will be lost, are you sure you want to cancel?", _
                            ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If Result = System.Windows.Forms.DialogResult.Yes Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        End If
        objCustomer = Nothing
        objPaper = Nothing
        objUnits = Nothing
    End Sub

    Private Sub PrintCard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainUI
        objCustomer.GetBoardType(Me.cmbBoardType)
        objCustomer.GetPSI(Me.cmbTest)
        objCustomer.GetBoxFormat(Me.cmbBoxFormat)
        objCustomer.GetBoxCategory(cmbBoxCategory)
        objCustomer.GetBoxOrientation(Me.cmbOrient)
        objUnits.UnitTable(Me.cmbUnits)
        objUnits.PageScale(Me.cmbScale)
        objCustomer.GetDiecut(Me.cmbDiecut)
        objCustomer.GetJoint(Me.cmbJoint)
        objCustomer.GetTestType(Me.cmbTestType)
        objCustomer.GetPrintcardStatus(Me.cmbPrintcardStatus)
        cmbPrintcardStatus.Text = "Active"
        cmbScale.Text = "1:8"
        tColor1.Text = "N/A"
        tColor2.Text = "N/A"
        tColor3.Text = "N/A"
        tColor4.Text = "N/A"
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        
    End Sub

    Private Sub LoadFiles()
        If Me.Text.Contains("EDIT") Then
            Dim FileExtension As String
            FileExtension = LTrim(RTrim(BrowsePrintcard.EDIT_FileName))
            Dim Extension As String = System.IO.Path.GetExtension(FileExtension)
            If StrComp(Extension, ".bmp", CompareMethod.Text) = 0 Or _
                       StrComp(Extension, ".jpg", CompareMethod.Text) = 0 Or _
                       StrComp(Extension, ".jpeg", CompareMethod.Text) = 0 Or _
                       StrComp(Extension, ".png", CompareMethod.Text) = 0 Or _
                       StrComp(Extension, ".tif", CompareMethod.Text) = 0 Then
                WebBrowser1.BringToFront()
                'WebBrowser1.Navigate(BrowsePrintcard.EDIT_FileName)
            ElseIf StrComp(Extension, ".pdf", CompareMethod.Text) = 0 Then
                AxPrintcardFile.BringToFront()
                AxPrintcardFile.OpenFile(BrowsePrintcard.EDIT_FileName)
            ElseIf StrComp(Extension, ".cdr", CompareMethod.Text) = 0 Or _
                StrComp(Extension, ".cmx", CompareMethod.Text) = 0 Or _
                StrComp(Extension, ".cdt", CompareMethod.Text) = 0 Then
                'WebBrowser1.Navigate(Application.StartupPath & "\html\coreldraw.htm")
                WebBrowser1.BringToFront()
                WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                        "<html><HEAD><b><center><h1><cr><cr>Use CorelDRAW to open the file.</h1></center></b>" & _
                                        " </HEAD><BODY></BODY></html>"
                'Dim vbres As MsgBoxResult
                'vbres = MsgBox("Document type is a CorelDraw graphic file format. Do you want to open the file using an external viewer?", vbYesNo, "TSD Inventory System")
                'If vbres = vbYes Then
                '    Process.Start(tFilePath.Text)
                'End If
            ElseIf StrComp(Extension, ".xcf", CompareMethod.Text) = 0 Then
                Dim vbres As MsgBoxResult
                vbres = MsgBox("Document type is a GIMP graphic file format. Do you want to open the file using GIMP?", vbYesNo, "TSD Inventory System")
                If vbres = vbYes Then
                    Process.Start(BrowsePrintcard.EDIT_FileName)
                End If
                WebBrowser1.BringToFront()
                WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                        "<html><HEAD><b><center><h1><cr><cr>Use GIMP to open the file.</h1></center></b>" & _
                                        " </HEAD><BODY></BODY></html>"
            ElseIf StrComp(Extension, ".ai", CompareMethod.Text) = 0 Then
                Dim vbres As MsgBoxResult
                vbres = MsgBox("Document type is an Adobe Illustrator file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
                If vbres = vbYes Then
                    Process.Start(BrowsePrintcard.EDIT_FileName)
                End If
                WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                        "<html><HEAD><b><center><h1><cr><cr>Use Adobe Illustrator to open the file.</h1></center></b>" & _
                                        " </HEAD><BODY></BODY></html>"
            ElseIf StrComp(Extension, ".psd", CompareMethod.Text) = 0 Then
                Dim vbres As MsgBoxResult
                vbres = MsgBox("Document type is an Adobe Photoshop file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
                If vbres = vbYes Then
                    Me.Cursor = Cursors.WaitCursor
                    Process.Start(BrowsePrintcard.EDIT_FileName)
                    Me.Cursor = Cursors.Default
                End If
                WebBrowser1.BringToFront()
                WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                        "<html><HEAD><b><center><h1><cr><cr>Use Adobe Photoshop to open the file.</h1></center></b>" & _
                                        " </HEAD><BODY></BODY></html>"
            Else
                WebBrowser1.BringToFront()
                WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Unknown file type.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
            End If
        Else
            WebBrowser1.BringToFront()
            WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>No attached file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        End If
        If Me.Text.Contains("EDIT") Then
            If BrowsePrintcard.CustomerFileID <> 1 Then
                Dim FileExtension As String
                FileExtension = LTrim(RTrim(BrowsePrintcard.EDIT_CustomerFile))
                Dim Extension As String = System.IO.Path.GetExtension(FileExtension)
                If StrComp(Extension, ".bmp", CompareMethod.Text) = 0 Or _
                           StrComp(Extension, ".jpg", CompareMethod.Text) = 0 Or _
                           StrComp(Extension, ".jpeg", CompareMethod.Text) = 0 Or _
                           StrComp(Extension, ".png", CompareMethod.Text) = 0 Or _
                           StrComp(Extension, ".tif", CompareMethod.Text) = 0 Then
                    WebCustomerFile.BringToFront()
                    WebCustomerFile.Navigate(BrowsePrintcard.EDIT_CustomerFile)
                ElseIf StrComp(Extension, ".pdf", CompareMethod.Text) = 0 Then
                    AxPrintcardFile.BringToFront()
                    AxPrintcardFile.OpenFile(BrowsePrintcard.EDIT_CustomerFile)
                ElseIf StrComp(Extension, ".cdr", CompareMethod.Text) = 0 Or _
                    StrComp(Extension, ".cmx", CompareMethod.Text) = 0 Or _
                    StrComp(Extension, ".cdt", CompareMethod.Text) = 0 Then
                    'WebBrowser1.Navigate(Application.StartupPath & "\html\coreldraw.htm")
                    WebCustomerFile.BringToFront()
                    WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                            "<html><HEAD><b><center><h1><cr><cr>Use CorelDRAW to open the file.</h1></center></b>" & _
                                            " </HEAD><BODY></BODY></html>"

                ElseIf StrComp(Extension, ".xcf", CompareMethod.Text) = 0 Then
                    Dim vbres As MsgBoxResult
                    vbres = MsgBox("Document type is a GIMP graphic file format. Do you want to open the file using GIMP?", vbYesNo, "TSD Inventory System")
                    If vbres = vbYes Then
                        Process.Start(BrowsePrintcard.EDIT_CustomerFile)
                    End If
                    WebCustomerFile.BringToFront()
                    WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                            "<html><HEAD><b><center><h1><cr><cr>Use GIMP to open the file.</h1></center></b>" & _
                                            " </HEAD><BODY></BODY></html>"
                ElseIf StrComp(Extension, ".ai", CompareMethod.Text) = 0 Then
                    Dim vbres As MsgBoxResult
                    vbres = MsgBox("Document type is an Adobe Illustrator file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
                    If vbres = vbYes Then
                        Process.Start(BrowsePrintcard.EDIT_CustomerFile)
                    End If
                    WebCustomerFile.BringToFront()
                    WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                            "<html><HEAD><b><center><h1><cr><cr>Use Adobe Illustrator to open the file.</h1></center></b>" & _
                                            " </HEAD><BODY></BODY></html>"
                ElseIf StrComp(Extension, ".psd", CompareMethod.Text) = 0 Then
                    Dim vbres As MsgBoxResult
                    vbres = MsgBox("Document type is an Adobe Photoshop file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
                    If vbres = vbYes Then
                        Me.Cursor = Cursors.WaitCursor
                        Process.Start(BrowsePrintcard.EDIT_CustomerFile)
                        Me.Cursor = Cursors.Default
                    End If
                    WebCustomerFile.BringToFront()
                    WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                            "<html><HEAD><b><center><h1><cr><cr>Use Adobe Photoshop to open the file.</h1></center></b>" & _
                                            " </HEAD><BODY></BODY></html>"
                Else
                    WebCustomerFile.BringToFront()
                    WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                        "<html><HEAD><b><center><h1><cr><cr>Unknown file type.</h1></center></b>" & _
                                        " </HEAD><BODY></BODY></html>"
                End If
            Else
                WebCustomerFile.BringToFront()
                WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                        "<html><HEAD><b><center><h1><cr><cr>No attached customer file.</h1></center></b>" & _
                                        " </HEAD><BODY></BODY></html>"
            End If
        End If

        If Me.Text.Contains("New Printcard") Then
            Me.Cursor = Cursors.WaitCursor
            CustomerList.ShowDialog()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub GetCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetCustomer.Click
        Me.Cursor = Cursors.WaitCursor
        CustomerList.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmbBoardType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBoardType.Click
        PreviousFlute = cmbBoardType.Text
    End Sub

    Private Sub cmbBoardType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBoardType.SelectedIndexChanged
        Dim DataRowView As System.Data.DataRowView = cmbBoardType.SelectedItem
        If IsDBNull(DataRowView) = False Then
            tPaperCombination.Text = ""
            tOuterLiner.Text = ""
            If cmbBoardType.Text = "CB-Flute" Then
                cmbTest.Text = "350"
            End If
            Dim sValue As String = DataRowView.Row("boardtype")
            PC_cBoardTypeID = objCustomer.GetBoardTypeID(sValue)
            SetFactorValues()
            ComputeBoxSize()
        End If
    End Sub

    Private Sub cmdGetCombination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetCombination.Click
        GetCombination.ShowDialog()
    End Sub

    Private Sub cmbBoxFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBoxFormat.SelectedIndexChanged
        Dim DataRowView As System.Data.DataRowView = cmbBoxFormat.SelectedItem
        If IsDBNull(DataRowView) = False Then
            BOX_FORMAT_DESCRIPTION = DataRowView.Row("description")
            PC_BoxFormatID = objCustomer.GetBoxFormatID(BOX_FORMAT_DESCRIPTION)
            If BOX_FORMAT_DESCRIPTION = "Partition" Or BOX_FORMAT_DESCRIPTION = "Corrugator Pads" Or BOX_FORMAT_DESCRIPTION = "Pads" Or BOX_FORMAT_DESCRIPTION = "Sleeve Liner" Then 'No box I.D. computation; tray will be added if needed
                tGlueTab.Text = 0
                tLength.Text = 0
                tWidth.Text = 0
                tHeight.Text = 0
                tPanel1.Text = 0
                tPanel2.Text = 0
                tPanel3.Text = 0
                tPanel4.Text = 0
                tBoardLength.Text = 0
                tBoardWidth.Text = 0
                tBoxHeight.Text = 0
                tFlap.Text = 0
                BoxDimensionGroup.Enabled = False
                cmbOrient.Text = "N/A"
                cPartitionGroup.Enabled = True
                PC_PaperDimensionID = SAVE_UPDATE_PartitionID
            Else
                SetFactorValues()
                ComputeBoxSize()
                tGlueTab.Text = 35
                BoxDimensionGroup.Enabled = True
                cPartitionGroup.Enabled = False
            End If
        End If
        'Box category update
        Select Case cmbBoxFormat.Text
            Case "Pads"
                cmbBoxCategory.Text = "Pads"
            Case "Regular Slotted Container"
                cmbBoxCategory.Text = "Cover"
            Case "Half Slotted Container"
                cmbBoxCategory.Text = "Body"
            Case "Sleeve Liner"
                cmbBoxCategory.Text = "Sleeve Liner"
            Case "Partition"
                cmbBoxCategory.Text = "Partition"
            Case Else
                cmbBoxCategory.Text = "Cover"
        End Select
    End Sub

    Private Function CheckID(ByVal Length As TextBox, ByVal Width As TextBox, ByVal Height As TextBox) As Boolean
        If Length.TextLength = 0 Or Width.TextLength = 0 Or Height.TextLength = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub theight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tHeight.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub theight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tHeight.LostFocus
        If CheckID(Me.tLength, Me.tWidth, Me.tHeight) = True Then
            PC_PaperDimensionID = objPaper.CheckExistingDimension(tLength.Text, tWidth.Text, tHeight.Text)
        Else
            PC_PaperDimensionID = 0
        End If
    End Sub

    Private Sub tLength_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tWidth.GotFocus, tLength.GotFocus, tHeight.GotFocus
        If tLength.Focused Then tLength.SelectAll()
        If tWidth.Focused Then tWidth.SelectAll()
        If tHeight.Focused Then tHeight.SelectAll()
    End Sub
    Private Sub cmdBrowseFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseFile.Click
        BrowseFile()
    End Sub

    Private Sub BrowseFile() 'Scalable Vector Graphics
        Dim ExistingFilename As String = tFilePath.Text

        UploadPrintcardFile.Filter = "Portable Document Format(PDF) File (*.pdf)|*.pdf;|" & _
                                     "CorelDraw Graphic Files(CDR) Files (*.cdr,*.cdt,*.cmx)|*.cdr;*.cdt;*.cmx;|" & _
                                     "Corel Photopaint (*.cpt)|*.cpt;|" & _
                                     "Adobe Illustrator (*.ai)|*.ai;|" & _
                                     "Adobe Photoshop (*.psd)|*.psd;|" & _
                                     "Images (*.bmp, *.jpg, *.jpeg, *.tif, *.png, *.tga)|*.bmp;*.jpg;*.jpeg;*.tif;*.png;*.tga;|" & _
                                     "GIMP (*.xcf)|*.xcf;|" & _
                                     "SVG (Scalable Vector Graphics) (*.svg)|*.svg;|" & _
                                     "CAD Files (*.amf, *.dwf, *.dwg, *.dxf, *.3ds, etc.)|*.amf;*.dwf;*.dwg;*.dxf;*.3ds;*.blend;|" & _
                                     "All Files (*.*)|*.*;"
        UploadPrintcardFile.FileName = ""
        UploadPrintcardFile.ShowDialog()
        tFilePath.Text = UploadPrintcardFile.FileName
        Dim FileExtension As String
        FileExtension = LTrim(RTrim(tFilePath.Text))
        Dim Extension As String = System.IO.Path.GetExtension(FileExtension)

        If StrComp(Extension, ".bmp", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".jpg", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".jpeg", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".png", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".tif", CompareMethod.Text) = 0 Then
            WebBrowser1.BringToFront()
            WebBrowser1.Navigate(tFilePath.Text)
        ElseIf StrComp(Extension, ".pdf", CompareMethod.Text) = 0 Then
            AxPrintcardFile.BringToFront()
            AxPrintcardFile.OpenFile(tFilePath.Text)
        ElseIf StrComp(Extension, ".cdr", CompareMethod.Text) = 0 Or _
            StrComp(Extension, ".cmx", CompareMethod.Text) = 0 Or _
            StrComp(Extension, ".cdt", CompareMethod.Text) = 0 Then
            'WebBrowser1.Navigate(Application.StartupPath & "\html\coreldraw.htm")
            WebBrowser1.BringToFront()
            WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use CorelDRAW to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
            'Dim vbres As MsgBoxResult
            'vbres = MsgBox("Document type is a CorelDraw graphic file format. Do you want to open the file using an external viewer?", vbYesNo, "TSD Inventory System")
            'If vbres = vbYes Then
            '    Process.Start(tFilePath.Text)
            'End If
        ElseIf StrComp(Extension, ".xcf", CompareMethod.Text) = 0 Then
            Dim vbres As MsgBoxResult
            vbres = MsgBox("Document type is a GIMP graphic file format. Do you want to open the file using GIMP?", vbYesNo, "TSD Inventory System")
            If vbres = vbYes Then
                Process.Start(tFilePath.Text)
            End If
            WebBrowser1.BringToFront()
            WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use GIMP to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        ElseIf StrComp(Extension, ".ai", CompareMethod.Text) = 0 Then
            Dim vbres As MsgBoxResult
            vbres = MsgBox("Document type is an Adobe Illustrator file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
            If vbres = vbYes Then
                Process.Start(tFilePath.Text)
            End If
            WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use Adobe Illustrator to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        ElseIf StrComp(Extension, ".psd", CompareMethod.Text) = 0 Then
            Dim vbres As MsgBoxResult
            vbres = MsgBox("Document type is an Adobe Photoshop file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
            If vbres = vbYes Then
                Me.Cursor = Cursors.WaitCursor
                Process.Start(tFilePath.Text)
                Me.Cursor = Cursors.Default
            End If
            WebBrowser1.BringToFront()
            WebBrowser1.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use Adobe Photoshop to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        Else
            'MsgBox("Unknown file format!", MsgBoxStyle.Exclamation, "TSD Inventory System")
            tFilePath.Text = ExistingFilename
            'WebBrowser1.BringToFront()
            'WebBrowser1.Navigate(Application.StartupPath & "\html\unknown.htm")
        End If
    End Sub
    Private Sub BrowseCustomerFile()
        Dim ExistingCustFile As String = tCustomerFile.Text
        UploadPrintcardFile.Filter = "Portable Document Format(PDF) File (*.pdf)|*.pdf;|" & _
                                     "CorelDraw Graphic Files(CDR) Files (*.cdr,*.cdt,*.cmx)|*.cdr;*.cdt;*.cmx;|" & _
                                     "Corel Photopaint (*.cpt)|*.cpt;|" & _
                                     "Adobe Illustrator (*.ai)|*.ai;|" & _
                                     "Adobe Photoshop (*.psd)|*.psd;|" & _
                                     "Images (*.bmp, *.jpg, *.jpeg, *.tif, *.png, *.tga)|*.bmp;*.jpg;*.jpeg;*.tif;*.png;*.tga;|" & _
                                     "GIMP (*.xcf)|*.xcf;|" & _
                                     "SVG (Scalable Vector Graphics) (*.svg)|*.svg;|" & _
                                     "CAD Files (*.amf, *.dwf, *.dwg, *.dxf, *.3ds, etc.)|*.amf;*.dwf;*.dwg;*.dxf;*.3ds;*.blend;|" & _
                                     "Text Files (*.txt)|*.txt;|" & _
                                     "All Files (*.*)|*.*;"
        UploadPrintcardFile.FileName = ""
        UploadPrintcardFile.ShowDialog()

        tCustomerFile.Text = UploadPrintcardFile.FileName
        Dim FileExtension As String
        FileExtension = LTrim(RTrim(tCustomerFile.Text))
        Dim Extension As String = System.IO.Path.GetExtension(FileExtension)

        If StrComp(Extension, ".bmp", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".jpg", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".jpeg", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".png", CompareMethod.Text) = 0 Or _
                   StrComp(Extension, ".tif", CompareMethod.Text) = 0 Then
            WebCustomerFile.BringToFront()
            WebCustomerFile.Navigate(tCustomerFile.Text)
        ElseIf StrComp(Extension, ".pdf", CompareMethod.Text) = 0 Then
            AxCustomerFile.BringToFront()
            AxCustomerFile.OpenFile(tCustomerFile.Text)
        ElseIf StrComp(Extension, ".cdr", CompareMethod.Text) = 0 Or _
            StrComp(Extension, ".cmx", CompareMethod.Text) = 0 Or _
            StrComp(Extension, ".cdt", CompareMethod.Text) = 0 Then
            'WebBrowser1.Navigate(Application.StartupPath & "\html\coreldraw.htm")
            WebBrowser1.BringToFront()
            WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use CorelDRAW to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
            'Dim vbres As MsgBoxResult
            'vbres = MsgBox("Document type is a CorelDraw graphic file format. Do you want to open the file using an external viewer?", vbYesNo, "TSD Inventory System")
            'If vbres = vbYes Then
            '    Process.Start(tFilePath.Text)
            'End If
        ElseIf StrComp(Extension, ".xcf", CompareMethod.Text) = 0 Then
            Dim vbres As MsgBoxResult
            vbres = MsgBox("Document type is a GIMP graphic file format. Do you want to open the file using GIMP?", vbYesNo, "TSD Inventory System")
            If vbres = vbYes Then
                Process.Start(tFilePath.Text)
            End If
            WebCustomerFile.BringToFront()
            WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use GIMP to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        ElseIf StrComp(Extension, ".ai", CompareMethod.Text) = 0 Then
            Dim vbres As MsgBoxResult
            vbres = MsgBox("Document type is an Adobe Illustrator file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
            If vbres = vbYes Then
                Process.Start(tFilePath.Text)
            End If
            WebCustomerFile.BringToFront()
            WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use Adobe Illustrator to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        ElseIf StrComp(Extension, ".psd", CompareMethod.Text) = 0 Then
            Dim vbres As MsgBoxResult
            vbres = MsgBox("Document type is an Adobe Photoshop file format. Do you want to open the file using Adobe Illustrator?", vbYesNo, "TSD Inventory System")
            If vbres = vbYes Then
                Me.Cursor = Cursors.WaitCursor
                Process.Start(tFilePath.Text)
                Me.Cursor = Cursors.Default
            End If
            WebCustomerFile.BringToFront()
            WebCustomerFile.DocumentText() = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0//EN'>" & _
                                    "<html><HEAD><b><center><h1><cr><cr>Use Adobe Photoshop to open the file.</h1></center></b>" & _
                                    " </HEAD><BODY></BODY></html>"
        Else
            tCustomerFile.Text = ExistingCustFile
            'MsgBox("Unknown file format!", MsgBoxStyle.Exclamation, "TSD Inventory System")
            'WebCustomerFile.BringToFront()
            'WebCustomerFile.Navigate(Application.StartupPath & "\html\unknown.htm")
        End If


    End Sub
    Private Sub cmbJoint_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbJoint.SelectedIndexChanged
        Dim DataRowView As System.Data.DataRowView = cmbJoint.SelectedItem
        If IsDBNull(DataRowView) = False Then
            Dim sValue As String = DataRowView.Row("description")
            PC_JointID = objCustomer.GetJointID(sValue)
            SetFactorValues()
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tLength_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tLength.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
        If e.KeyChar = ControlChars.Cr Then
            tWidth.Focus()
        End If
    End Sub

    Private Sub tWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tWidth.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
        If e.KeyChar = ControlChars.Cr Then
            tHeight.Focus()
        End If
    End Sub

    Private Sub tFlap_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub
    Public Sub ResetForm()
        tBoxDescription.Text = ""
        tLength.Text = ""
        tWidth.Text = ""
        tHeight.Text = ""
        cmbBoardType.ResetText()
        tPaperCombination.Text = ""
        tOuterLiner.Text = ""
        tColor1.Text = "N/A"
        tColor2.Text = "N/A"
        tColor3.Text = "N/A"
        tColor4.Text = "N/A"
        tPrintcardNumber.Text = ""
        tFilePath.Text = ""
        tPrintcardNotes.Text = ""
        tPanel1.Text = ""
        tPanel2.Text = ""
        tPanel3.Text = ""
        tPanel4.Text = ""
        tBoardLength.Text = ""
        tBoardWidth.Text = ""
        tBoxHeight.Text = ""
        tFlap.Text = ""
        tCustomerFile.Text = ""
        tFilePath.Text = ""
        WebBrowser1.Navigate("")
    End Sub

    Private Sub tColor1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tColor4.GotFocus, tColor3.GotFocus, tColor2.GotFocus, tColor1.GotFocus
        If tColor1.Focused Then tColor1.SelectAll()
        If tColor2.Focused Then tColor2.SelectAll()
        If tColor3.Focused Then tColor3.SelectAll()
        If tColor4.Focused Then tColor4.SelectAll()
    End Sub

    Private Sub tPrintcardNotes_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPrintcardNotes.GotFocus
        tPrintcardNotes.SelectAll()
    End Sub

    Private Sub tPrintcardNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tPrintcardNotes.TextChanged
        If tPrintcardNotes.TextLength > 255 Then
            SendKeys.Send(ControlChars.Back)
        End If
        MainUI.StatusMessage.Text = NotesCharCount - tPrintcardNotes.TextLength & " Chars left."
    End Sub

    Private Sub tColor1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tColor1.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            tColor2.Focus()
        End If
    End Sub

    Private Sub tColor1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tColor1.LostFocus
        If tColor1.Text = "" Then
            tColor1.Text = "N/A"
        End If
    End Sub

    Private Sub tColor2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tColor2.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            tColor3.Focus()
        End If
    End Sub

    Private Sub tColor2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tColor2.LostFocus
        If tColor2.Text = "" Then
            tColor2.Text = "N/A"
        End If
    End Sub

    Private Sub tColor3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tColor3.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            tColor4.Focus()
        End If
    End Sub

    Private Sub tColor3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tColor3.LostFocus
        If tColor3.Text = "" Then
            tColor3.Text = "N/A"
        End If
    End Sub

    Private Sub tColor4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tColor4.TextChanged
        If tColor4.Text = "" Then
            tColor4.Text = "N/A"
        End If
    End Sub

    Private Sub tGlueTab_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tGlueTab.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub


    Private Sub tPanel1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tPanel1.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tPanel2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tPanel2.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tPanel3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tPanel3.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tPanel4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tPanel4.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tBoardLength_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tBoardLength.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tBoardWidth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tBoardWidth.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tBoxHeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tBoxHeight.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tFlap_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tFlap.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub theight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tHeight.TextChanged
        ComputeBoxSize()
    End Sub
    Public Sub ComputeBoxSize()
        If PC_BoxFormatID <> 40 Then ' If Partition skip, no need to compute box id
            If tLength.Text <> "" And tWidth.Text <> "" And tHeight.Text <> "" And tGlueTab.Text <> "" Then
                GlueTab = tGlueTab.Text

                Panel1 = tLength.Text + factor1
                Panel2 = tWidth.Text + factor2
                Panel3 = tLength.Text + factor3
                Panel4 = tWidth.Text + factor4

                BoxHeight = tHeight.Text + FactorHeight
                BoxFlap = (tWidth.Text / 2) + FactorFlap

                'Check for standard 13KG Banana Box I.D.
                If tLength.Text = 510 And tWidth.Text = 339 And tHeight.Text = 213 Then
                    Panel4 += 1
                    BoxFlap = 125
                    cmbBoxFormat.Text = "Half Slotted Container"
                End If

                BoardLength = GlueTab + (Panel1 + Panel2 + Panel3 + Panel4)
                If BoxFormatType = "Regular Slotted Container" Then
                    BoardWidth = (BoxFlap * 2) + BoxHeight
                Else
                    BoardWidth = BoxFlap + BoxHeight
                End If

                If cmbOrient.Text = "Flap-Length-Width-Length-Width" Then
                    tPanel1.Text = Panel1
                    tPanel2.Text = Panel2
                    tPanel3.Text = Panel3
                    tPanel4.Text = Panel4
                ElseIf cmbOrient.Text = "Flap-Width-Length-Width-Length" Then
                    tPanel1.Text = Panel4
                    tPanel2.Text = Panel3
                    tPanel3.Text = Panel2
                    tPanel4.Text = Panel1
                Else 'default
                    tPanel1.Text = Panel1
                    tPanel2.Text = Panel2
                    tPanel3.Text = Panel3
                    tPanel4.Text = Panel4
                End If

                tBoardLength.Text = BoardLength
                tBoardWidth.Text = BoardWidth
                tFlap.Text = BoxFlap
                tBoxHeight.Text = BoxHeight
            End If
        End If
    End Sub

    Private Sub SetFactorValues()
        If PC_BoxFormatID <> 40 Then 'Partition then skip , no need for factors
            Dim objBoxCompute As classCorrugatedBox = New classCorrugatedBox
            factor1 = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).factor1
            factor2 = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).factor2
            factor3 = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).factor3
            factor4 = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).factor4
            FactorFlap = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).FactorFlap
            FactorHeight = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).FactorHeight
            BoxFormatType = objBoxCompute.ComputeBoxSize(PC_cBoardTypeID, PC_JointID, PC_BoxFormatID, tGlueTab.Text).BoxFormat
            objBoxCompute = Nothing
        End If
    End Sub

    Private Sub tLength_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tLength.TextChanged
        ComputeBoxSize()
    End Sub

    Private Sub tWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tWidth.TextChanged
        ComputeBoxSize()
    End Sub

    Private Sub tGlueTab_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tGlueTab.LostFocus
        If tGlueTab.Text <> "" Then
            If tGlueTab.Text < 20 Then 'TODO: Get value from database
                tGlueTab.Text = 35
            End If
        Else
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tGlueTab_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tGlueTab.TextChanged
        If tGlueTab.Text <> "" Then
            ' Set the BoardLength value again to make it accurate, because editing panels will not change BoardLength value
            ' that was set earlier in ComputeBoxSize() procedure.
            GlueTab = tGlueTab.Text 'Set GlueTab to the tGlueTab's new value
            BoardLength = GlueTab + (Panel1 + Panel2 + Panel3 + Panel4) ' Compute BoardLength again
            tBoardLength.Text = (BoardLength - GlueTab) + tGlueTab.Text ' Then add and display the correct value according to the changes made in the panels
        Else 'The user leaves the panel empty, so it is safe to compute and restore the default values
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tBoxHeight_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tBoxHeight.LostFocus
        If tBoxHeight.Text <> "" Then
            If tBoxHeight.Text < tHeight.Text Then
                ComputeBoxSize()
            End If
        Else
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tBoxHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tBoxHeight.TextChanged
        If tBoxHeight.Text <> "" Then
            BoxHeight = tBoxHeight.Text
            If BoxFormatType = "Regular Slotted Container" Then
                BoardWidth = (BoxFlap * 2) + BoxHeight
            Else
                BoardWidth = BoxFlap + BoxHeight
            End If
            tBoardWidth.Text = (BoardWidth - BoxHeight) + tBoxHeight.Text
        Else
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tFlap_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tFlap.TextChanged
        If tFlap.Text <> "" Then
            BoxFlap = tFlap.Text
            If BoxFormatType = "Regular Slotted Container" Then
                BoardWidth = (BoxFlap * 2) + BoxHeight
            Else
                BoardWidth = BoxFlap + BoxHeight
            End If
            tBoardWidth.Text = (BoardWidth - BoxFlap) + tFlap.Text
        Else
            ComputeBoxSize()
        End If

    End Sub

    Private Sub tPanel1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPanel1.LostFocus
        'If tPanel1.Text < tLength.Text Then
        '    ComputeBoxSize()
        'End If
    End Sub

    Private Sub tPanel1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tPanel1.TextChanged
        If tPanel1.Text <> "" Then
            ' Set the BoardLength value again to make it accurate, because editing panels will not change BoardLength value
            ' that was set earlier in ComputeBoxSize() procedure.
            Panel1 = tPanel1.Text 'Set Panel1 to the tPanel1's new value
            BoardLength = GlueTab + (Panel1 + Panel2 + Panel3 + Panel4) ' Compute BoardLength again
            tBoardLength.Text = (BoardLength - Panel1) + tPanel1.Text ' Then add and display the correct value according to the changes made in the panels
        Else 'The user leaves the panel empty, so it is safe to compute and restore the default values
            ComputeBoxSize()
        End If

        'tBoardLength.Clear()

        'tBoardLength.Text = tGlueTab.Text + (tPanel1.Text + tPanel2.Text + tPanel3.Text + tPanel4.Text)
    End Sub

    Private Sub tPanel2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPanel2.LostFocus
        'If tPanel2.Text <> "" Then
        '    If tPanel2.Text < tWidth.Text Then
        '        ComputeBoxSize()
        '    End If
        'Else
        '    ComputeBoxSize()
        'End If
    End Sub

    Private Sub tPanel2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tPanel2.TextChanged
        If tPanel2.Text <> "" Then
            ' Set the BoardLength value again to make it accurate, because editing panels will not change BoardLength value
            ' that was set earlier in ComputeBoxSize() procedure.
            Panel2 = tPanel2.Text 'Set Panel2 to the tPanel2's new value
            BoardLength = GlueTab + (Panel1 + Panel2 + Panel3 + Panel4) ' Compute BoardLength again
            tBoardLength.Text = (BoardLength - Panel2) + tPanel2.Text ' Then add and display the correct value according to the changes made in the panels
        Else 'The user leaves the panel empty, so it is safe to compute and restore the default values
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tPanel3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPanel3.LostFocus
        'If tPanel3.Text <> "" Then
        '    If tPanel3.Text < tLength.Text Then
        '        ComputeBoxSize()
        '    End If
        'Else
        '    ComputeBoxSize()
        'End If
    End Sub

    Private Sub tPanel3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tPanel3.TextChanged
        If tPanel3.Text <> "" Then
            ' Set the BoardLength value again to make it accurate, because editing panels will not change BoardLength value
            ' that was set earlier in ComputeBoxSize() procedure.
            Panel3 = tPanel3.Text 'Set Panel3 to the tPanel3's new value
            BoardLength = GlueTab + (Panel1 + Panel2 + Panel3 + Panel4) ' Compute BoardLength again
            tBoardLength.Text = (BoardLength - Panel3) + tPanel3.Text ' Then add and display the correct value according to the changes made in the panels
        Else 'The user leaves the panel empty, so it is safe to compute and restore the default values
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tPanel4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPanel4.LostFocus
        'If tPanel4.Text <> "" Then
        '    If tPanel4.Text < tWidth.Text Then
        '        ComputeBoxSize()
        '    End If
        'Else
        '    ComputeBoxSize()
        'End If

    End Sub

    Private Sub tPanel4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tPanel4.TextChanged
        If tPanel4.Text <> "" Then
            ' Set the BoardLength value again to make it accurate, because editing panels will not change BoardLength value
            ' that was set earlier in ComputeBoxSize() procedure.
            Panel4 = tPanel4.Text 'Set Panel3 to the tPanel4's new value
            BoardLength = GlueTab + (Panel1 + Panel2 + Panel3 + Panel4) ' Compute BoardLength again
            tBoardLength.Text = (BoardLength - Panel4) + tPanel4.Text ' Then add and display the correct value according to the changes made in the panels
        Else 'The user leaves the panel empty, so it is safe to compute and restore the default values
            ComputeBoxSize()
        End If
    End Sub

    Private Sub tFilePath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tFilePath.Click
        BrowseFile()
    End Sub

    Private Sub tCustomerFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tCustomerFile.Click
        BrowseCustomerFile()
    End Sub

    Private Sub cmdBrowseCustFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseCustFile.Click
        BrowseCustomerFile()
    End Sub

    Private Sub tCustomerName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tCustomerName.TextChanged
        Dim IndustryType As Short = objCustomer.GetIndustryType(PC_CUSTOMERID)
        If BrowsePrintcard.EDIT_DimensionID <> 90 Then
            If IndustryType = 1 Then ' Banana, then set to Half Slotted Container
                cmbBoxFormat.Text = "Half Slotted Container"
            ElseIf IndustryType > 1 Then
                cmbBoxFormat.Text = "Regular Slotted Container"
            End If
        End If
    End Sub

    Private Sub tPrintcardNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tPrintcardNumber.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub
    Private Sub tBoxDescription_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tBoxDescription.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            cmbBoxFormat.Focus()
        End If
    End Sub
    '---------------------------------------------------------------------------------------------------------------------
    'Refactoring save printcard code to use npgsqlTransaction
    'For code readability use this prefix 'PC_' to declare variables
    Public Sub PC_SavePrintcard()
        Dim PC_FLAP As Single
        Dim PC_BOARD_WIDTH As Double
        Dim PC_BOARD_HEIGHT As Double
        Dim PC_BOARD_LENGTH As Double
        Dim PC_GLUE_TAB As Integer
        Dim PC_PANEL1 As Single
        Dim PC_PANEL2 As Single
        Dim PC_PANEL3 As Single
        Dim PC_PANEL4 As Single
        Dim PC_TestTypeID As Integer
        Dim PC_NewTestTableID As Integer

        Dim cBoard1Width As Single
        Dim cBoard1Length As Single
        Dim cBoard2Width As Single
        Dim cBoard2Length As Single


        If CheckVitalValues() = 1 Then Exit Sub
        'PC_CUSTOMERID

        GetForeignKeyIDs()

        Dim PC_PRINTCARD_NUM As Integer

        PC_PRINTCARD_NUM = tPrintcardNumber.Text
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        'Check printcard number:
        If objPrintcard.CheckExistingPrincardNumber(PC_CUSTOMERID, PC_PRINTCARD_NUM) = 1 Then
            MsgBox("Printcard number was already in used for this customer.", MsgBoxStyle.Exclamation, "TSD Inventory System")
            tPrintcardNumber.Focus()
            tPrintcardNumber.SelectAll()
            Exit Sub
        End If

        Dim p As ExportRptProgress = ExportRptProgress.ShowProgress(Me)
        Using connTransaction As NpgsqlConnection = New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
            Try
                If connTransaction.State = ConnectionState.Closed Then connTransaction.Open()
                Using pgTransaction As NpgsqlTransaction = connTransaction.BeginTransaction
                    Try

                        p.UpdateProgress(10, "10%", "Saving printcard, please wait...")
                        'Customer File 
                        'Internal File
                        Dim PC_CUSTOMER_FILE_UPLOAD As String
                        Dim PC_CUSTOMER_FILE As String
                        Dim PC_CUSTOMER_FILE_EXT As String
                        Dim PC_CUSTOMER_ImageData As Byte()
                        Dim PC_CUSTOMER_sFileName As String
                        Dim PC_CUSTOMER_FILEUPLOAD_ID As Integer


                        'Upload internal file file
                        p.UpdateProgress(20, "20%", "Saving printcard, please wait...")
                        Dim PC_INTERNAL_FILE_UPLOAD As String = LTrim(RTrim(tFilePath.Text))
                        Dim PC_INTERNAL_FILE As String = System.IO.Path.GetExtension(PC_INTERNAL_FILE_UPLOAD)
                        Dim PC_INTERNAL_FILE_EXT As String = GetExtensionType(PC_INTERNAL_FILE)
                        Dim PC_INTERNAL_ImageData As Byte()
                        Dim PC_INTERNAL_sFileName As String
                        PC_INTERNAL_ImageData = ReadFile(PC_INTERNAL_FILE_UPLOAD)
                        PC_INTERNAL_sFileName = System.IO.Path.GetFileName(PC_INTERNAL_FILE_UPLOAD)
                        Dim PC_INTERNAL_FILEUPLOAD_ID = objPrintcard.GetTableFileID("graphicfiles") ' graphics id

                        p.UpdateProgress(30, "30%", "Saving printcard, please wait...")
                        Using UploadInternalFileCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO graphicfiles(id, fileloaded, filename, filetype, date_created)" & _
                                                                                  " SELECT @id, @fileloaded, @filename, @filetype, @date_created;", connTransaction, pgTransaction)
                            UploadInternalFileCMD.Parameters.AddWithValue("@id", PC_INTERNAL_FILEUPLOAD_ID)
                            UploadInternalFileCMD.Parameters.AddWithValue("@fileloaded", PC_INTERNAL_ImageData)
                            UploadInternalFileCMD.Parameters.AddWithValue("@filename", PC_INTERNAL_sFileName)
                            UploadInternalFileCMD.Parameters.AddWithValue("@filetype", PC_INTERNAL_FILE_EXT)
                            UploadInternalFileCMD.Parameters.AddWithValue("@date_created", Now())
                            UploadInternalFileCMD.ExecuteNonQuery() 'Virtually execute SQL command

                            p.UpdateProgress(40, "40%", "Saving printcard, please wait...")
                            'Upload Customer File
                            If tCustomerFile.Text <> "" Then
                                PC_CUSTOMER_FILE_UPLOAD = LTrim(RTrim(tCustomerFile.Text))
                                PC_CUSTOMER_FILE = System.IO.Path.GetExtension(PC_CUSTOMER_FILE_UPLOAD)
                                PC_CUSTOMER_FILE_EXT = GetExtensionType(PC_CUSTOMER_FILE)

                                PC_CUSTOMER_ImageData = ReadFile(PC_CUSTOMER_FILE_UPLOAD)
                                PC_CUSTOMER_sFileName = System.IO.Path.GetFileName(PC_CUSTOMER_FILE_UPLOAD)
                                PC_CUSTOMER_FILEUPLOAD_ID = objPrintcard.GetTableFileID("customerfile") ' customerfile id

                                Using UploadCustomerFileCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO customerfile(id, fileloaded, filename, filetype, date_created)" & _
                                                                                            " SELECT @id, @fileloaded, @filename, @filetype, @date_created;", connTransaction, pgTransaction)
                                    UploadCustomerFileCMD.Parameters.AddWithValue("@id", PC_CUSTOMER_FILEUPLOAD_ID)
                                    UploadCustomerFileCMD.Parameters.AddWithValue("@fileloaded", PC_CUSTOMER_ImageData)
                                    UploadCustomerFileCMD.Parameters.AddWithValue("@filename", PC_CUSTOMER_sFileName)
                                    UploadCustomerFileCMD.Parameters.AddWithValue("@filetype", PC_CUSTOMER_FILE_EXT)
                                    UploadCustomerFileCMD.Parameters.AddWithValue("@date_created", Now())
                                    UploadCustomerFileCMD.ExecuteNonQuery()
                                End Using
                            Else
                                PC_CUSTOMER_FILEUPLOAD_ID = 1 ' No customer file attached
                            End If
                            p.UpdateProgress(50, "50%", "Saving printcard, please wait...")
                            'Insert new Inside Dimension
                            If PC_PaperDimensionID = 0 Then
                                PC_PaperDimensionID = objPrintcard.GetTableFileID("paper_dimension")
                                Dim cLength As Integer = tLength.Text
                                Dim cWidth As Integer = tWidth.Text
                                Dim cHeight As Integer = tHeight.Text
                                Using InsertNewInsideDimensionCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO paper_dimension(id,length,width,height,unit_id,date_created) " & _
                                                                                                       " SELECT @id,@length,@width,@height,@unit_id,@date_created;", connTransaction, pgTransaction)
                                    InsertNewInsideDimensionCMD.Parameters.AddWithValue("@id", PC_PaperDimensionID)
                                    InsertNewInsideDimensionCMD.Parameters.AddWithValue("@length", cLength)
                                    InsertNewInsideDimensionCMD.Parameters.AddWithValue("@width", cWidth)
                                    InsertNewInsideDimensionCMD.Parameters.AddWithValue("@height", cHeight)
                                    InsertNewInsideDimensionCMD.Parameters.AddWithValue("@unit_id", PC_UnitID)
                                    InsertNewInsideDimensionCMD.Parameters.AddWithValue("@date_created", Now())
                                    InsertNewInsideDimensionCMD.ExecuteNonQuery()
                                End Using
                            End If
                            p.UpdateProgress(60, "60%", "Saving printcard, please wait...")
                            'Declare variables
                            Dim PC_COLOR1 As String = tColor1.Text
                            Dim PC_COLOR2 As String = tColor2.Text
                            Dim PC_COLOR3 As String = tColor3.Text
                            Dim PC_COLOR4 As String = tColor4.Text
                            Dim PC_PRINTCARD_NOTES As String = tPrintcardNotes.Text

                            'PC_PaperDimensionID
                            If PC_BoxFormatID = 40 Or PC_BoxFormatID = 50 Or PC_BoxFormatID = 60 Then 'partition
                                PC_FLAP = 0
                                PC_BOARD_WIDTH = 0
                                PC_BOARD_HEIGHT = 0
                                PC_BOARD_LENGTH = 0
                                PC_GLUE_TAB = 0
                                PC_PANEL1 = 0
                                PC_PANEL2 = 0
                                PC_PANEL3 = 0
                                PC_PANEL4 = 0
                                cBoard1Width = Board1Width.Text
                                cBoard1Length = Board1Length.Text
                                If Board2Width.Text = "" Then
                                    cBoard2Width = 0
                                Else
                                    cBoard2Width = Board2Width.Text
                                End If
                                If Board2Length.Text = "" Then
                                    cBoard2Length = 0
                                Else
                                    cBoard2Length = Board2Length.Text
                                End If

                            Else
                                PC_FLAP = tFlap.Text
                                PC_BOARD_WIDTH = tBoardWidth.Text
                                PC_BOARD_HEIGHT = tBoxHeight.Text
                                PC_BOARD_LENGTH = tBoardLength.Text
                                PC_GLUE_TAB = tGlueTab.Text
                                PC_PANEL1 = tPanel1.Text
                                PC_PANEL2 = tPanel2.Text
                                PC_PANEL3 = tPanel3.Text
                                PC_PANEL4 = tPanel4.Text
                                cBoard1Width = 0
                                cBoard1Length = 0
                                cBoard2Width = 0
                                cBoard2Length = 0
                            End If

                            Dim PC_DateCreated As String = cPrintcardCreated.Value
                            p.UpdateProgress(70, "70%", "Saving printcard, please wait...")

                            'Save Printcard
                            PC_TestTypeID = GetTableValueIntColumnCompare("test_type", "id", "code", cmbTestType.Text)

                            If PC_TESTID = 0 Then
                                'insert new value to test table
                                PC_NewTestTableID = GetTableNextIDIncrementBy("test", 1)
                                Using InsertNewTestIDCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO test(id,value,date_created) " & _
                                                                                                      " SELECT @id,@value,@date_created;", connTransaction, pgTransaction)
                                    InsertNewTestIDCMD.Parameters.AddWithValue("@id", PC_NewTestTableID)
                                    InsertNewTestIDCMD.Parameters.AddWithValue("@value", Convert.ToInt32(cmbTest.Text))
                                    InsertNewTestIDCMD.Parameters.AddWithValue("@date_created", Now())
                                    InsertNewTestIDCMD.ExecuteNonQuery()
                                End Using
                                PC_TESTID = PC_NewTestTableID
                            End If

                            Using SavePrintcardCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO printcard( " & _
                                                                                            " customer_id, box_description, boxformat_id, flute_id, test_id, " & _
                                                                                            " joint_id, paper_combination_id, dimension_id, color_1, color_2, " & _
                                                                                            " color_3, color_4, flap, unit_id, scale_id, diecut_id, printcardno, " & _
                                                                                            " filename_id, date_created, notes, boardlength, boardwidth, " & _
                                                                                            " boxheight, gluetab, panel1, panel2, panel3, panel4, customer_file_id, creator_id,boxorientation_id, date_createdtime, " & _
                                                                                            " board1width, board1length, board2width, board2length, printcard_status,test_type_id,box_category_id) " & _
                                                                                            " SELECT @customer_id, @box_description, @boxformat_id, @flute_id, @test_id, " & _
                                                                                                        " @joint_id, @paper_combination_id, @dimension_id, @color_1, @color_2, " & _
                                                                                                        " @color_3, @color_4, @flap, @unit_id, @scale_id, @diecut_id, @printcardno, " & _
                                                                                                        " @filename_id, @date_created, @notes, @boardlength, @boardwidth, @boxheight, " & _
                                                                                                        " @gluetab, @panel1, @panel2, @panel3, @panel4, @customer_file_id, @creator_id, @boxorientation_id, @date_createdtime, " & _
                                                                                                        " @board1width, @board1length, @board2width, @board2length, @printcard_status, @test_type_id, @box_category_id;", connTransaction, pgTransaction)
                                SavePrintcardCMD.Parameters.AddWithValue("@customer_id", PC_CUSTOMERID)
                                SavePrintcardCMD.Parameters.AddWithValue("@box_description", tBoxDescription.Text)
                                SavePrintcardCMD.Parameters.AddWithValue("@boxformat_id", PC_BoxFormatID)
                                SavePrintcardCMD.Parameters.AddWithValue("@flute_id", PC_cBoardTypeID)
                                SavePrintcardCMD.Parameters.AddWithValue("@test_id", PC_TESTID)
                                SavePrintcardCMD.Parameters.AddWithValue("@joint_id", PC_JointID)
                                SavePrintcardCMD.Parameters.AddWithValue("@paper_combination_id", PC_PaperCombinationID)
                                SavePrintcardCMD.Parameters.AddWithValue("@dimension_id", PC_PaperDimensionID)
                                SavePrintcardCMD.Parameters.AddWithValue("@color_1", PC_COLOR1)
                                SavePrintcardCMD.Parameters.AddWithValue("@color_2", PC_COLOR2)
                                SavePrintcardCMD.Parameters.AddWithValue("@color_3", PC_COLOR3)
                                SavePrintcardCMD.Parameters.AddWithValue("@color_4", PC_COLOR4)
                                SavePrintcardCMD.Parameters.AddWithValue("@flap", PC_FLAP)
                                SavePrintcardCMD.Parameters.AddWithValue("@unit_id", PC_UnitID)
                                SavePrintcardCMD.Parameters.AddWithValue("@scale_id", PC_ScaleID)
                                SavePrintcardCMD.Parameters.AddWithValue("@diecut_id", PC_Diecut_ID)
                                SavePrintcardCMD.Parameters.AddWithValue("@printcardno", PC_PRINTCARD_NUM)
                                SavePrintcardCMD.Parameters.AddWithValue("@filename_id", PC_INTERNAL_FILEUPLOAD_ID)
                                SavePrintcardCMD.Parameters.AddWithValue("@date_created", PC_DateCreated) 'Approved date.
                                SavePrintcardCMD.Parameters.AddWithValue("@notes", PC_PRINTCARD_NOTES)
                                SavePrintcardCMD.Parameters.AddWithValue("@boardlength", PC_BOARD_LENGTH)
                                SavePrintcardCMD.Parameters.AddWithValue("@boardwidth", PC_BOARD_WIDTH)
                                SavePrintcardCMD.Parameters.AddWithValue("@boxheight", PC_BOARD_HEIGHT)
                                SavePrintcardCMD.Parameters.AddWithValue("@gluetab", PC_GLUE_TAB)
                                SavePrintcardCMD.Parameters.AddWithValue("@panel1", PC_PANEL1)
                                SavePrintcardCMD.Parameters.AddWithValue("@panel2", PC_PANEL2)
                                SavePrintcardCMD.Parameters.AddWithValue("@panel3", PC_PANEL3)
                                SavePrintcardCMD.Parameters.AddWithValue("@panel4", PC_PANEL4)
                                SavePrintcardCMD.Parameters.AddWithValue("@customer_file_id", PC_CUSTOMER_FILEUPLOAD_ID)
                                SavePrintcardCMD.Parameters.AddWithValue("@creator_id", LoginTSD.SystemUserID)
                                SavePrintcardCMD.Parameters.AddWithValue("@boxorientation_id", PC_BoxOrientationID)
                                SavePrintcardCMD.Parameters.AddWithValue("@date_createdtime", Now())
                                SavePrintcardCMD.Parameters.AddWithValue("@board1width", cBoard1Width)
                                SavePrintcardCMD.Parameters.AddWithValue("@board1length", cBoard1Length)
                                SavePrintcardCMD.Parameters.AddWithValue("@board2width", cBoard2Width)
                                SavePrintcardCMD.Parameters.AddWithValue("@board2length", cBoard2Length)
                                SavePrintcardCMD.Parameters.AddWithValue("@printcard_status", PC_PrintcardStatusID)
                                SavePrintcardCMD.Parameters.AddWithValue("@test_type_id", PC_TestTypeID)
                                SavePrintcardCMD.Parameters.AddWithValue("@box_category_id", PC_BOX_CATEGORY_ID)
                                SavePrintcardCMD.ExecuteNonQuery()
                            End Using
                            p.UpdateProgress(100, "100%", "Saving printcard, please wait...")
                            'No exceptions encountered
                            pgTransaction.Commit()
                            pgTransaction.Dispose()
                            connTransaction.Close()
                            connTransaction.ClearPool()
                            Dim PrincardText As String = ""
                            If tPrintcardNumber.TextLength = 1 Then
                                PrincardText = tPrincardPrefix.Text & "000" & tPrintcardNumber.Text
                            ElseIf tPrintcardNumber.TextLength = 2 Then
                                PrincardText = tPrincardPrefix.Text & "00" & tPrintcardNumber.Text
                            ElseIf tPrintcardNumber.TextLength = 3 Then
                                PrincardText = tPrincardPrefix.Text & "0" & tPrintcardNumber.Text
                            Else
                                PrincardText = tPrincardPrefix.Text & tPrintcardNumber.Text
                            End If
                            p.CloseProgress()
                            MessageBox.Show("Princard number: " & PrincardText & " was successfully saved.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ResetForm()
                        End Using
                    Catch ex As Exception
                        pgTransaction.Rollback()
                        p.CloseProgress()
                        Throw
                    End Try
                End Using
            Catch ex As NpgsqlException
                MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Catch ex As Exception
                MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Throw
            End Try
        End Using
        objPrintcard = Nothing
        If cmbBoxFormat.Text = "Partition" Then
            tLength.Text = 0
            tWidth.Text = 0
            tHeight.Text = 0
            tGlueTab.Text = 0
            tPanel1.Text = 0
            tPanel2.Text = 0
            tPanel3.Text = 0
            tPanel4.Text = 0
            tBoxHeight.Text = 0
            tFlap.Text = 0
            tBoardWidth.Text = 0
            tBoardLength.Text = 0
        End If
        tPrintcardNumber.Text = objCustomer.GetPrintcardNum(PC_CUSTOMERID)
    End Sub

    Public Sub PC_UpdatePrintcard()
        Dim cBoard1Width As Single
        Dim cBoard1Length As Single
        Dim cBoard2Width As Single
        Dim cBoard2Length As Single
        Dim PC_TESTID_GetCMBtest As Integer
        Dim p As ExportRptProgress = ExportRptProgress.ShowProgress(Me)
        p.UpdateProgress(10, "10%", "Updating Printcard " & tBoxDescription.Text & ", please wait...")
        If CheckEditPrintcardValues() = 1 Then
            p.CloseProgress()
            Exit Sub
        End If
        GetForeignKeyIDs()
        Dim DateCreatedStr As String = cPrintcardCreated.Value
        Dim UPDATE_InsideDimensionID As Integer
        Dim objPrintcard As ClassPrintcard = New ClassPrintcard
        p.UpdateProgress(30, "30%", "Updating Printcard " & tBoxDescription.Text & ", please wait...")
        UPDATE_InsideDimensionID = objPrintcard.CheckExistingDimension(tLength.Text, tWidth.Text, tHeight.Text)
        p.UpdateProgress(60, "60%", "Updating Printcard " & tBoxDescription.Text & ", please wait...")
        If UPDATE_InsideDimensionID = 90 Then 'Partition
            cBoard1Width = Board1Width.Text
            cBoard1Length = Board1Length.Text
            cBoard2Width = Board2Width.Text
            cBoard2Length = Board2Length.Text
        Else
            cBoard1Width = 0
            cBoard1Length = 0
            cBoard2Width = 0
            cBoard2Length = 0
        End If

        If cmbTest.Text = "" Then
            p.CloseProgress()
            MessageBox.Show("Invalid or empty Test!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            cmbTest.Focus()
            Return
        End If

        If PC_TESTID = 0 Then
            PC_TESTID_GetCMBtest = Convert.ToInt32(cmbTest.Text)
        End If
        'Bug fixed, two ifs below for not saving during update because of the existing file string
        If ExistingCustFile = tCustomerFile.Text Then
            tCustomerFile.Text = ""
        End If
        If ExistingGraphicFile = tFilePath.Text Then
            tFilePath.Text = ""
        End If
        If objPrintcard.UpdatePrintcard(PC_CUSTOMERID, Edit_PrintcardID,
                                     tBoxDescription.Text, PC_BoxFormatID,
                                     PC_cBoardTypeID, PC_TESTID, PC_JointID, PC_PaperCombinationID,
                                     UPDATE_InsideDimensionID, tColor1.Text, tColor2.Text, tColor3.Text, tColor4.Text,
                                     tFlap.Text, PC_UnitID, PC_ScaleID, PC_Diecut_ID, tPrintcardNumber.Text,
                                     DateCreatedStr, tPrintcardNotes.Text, tBoardWidth.Text, tBoardLength.Text, tBoxHeight.Text,
                                     tGlueTab.Text, tPanel1.Text, tPanel2.Text, tPanel3.Text, tPanel4.Text, LoginTSD.SystemUserID, PC_BoxOrientationID,
                                     tLength.Text, tWidth.Text, tHeight.Text, cBoard1Width, cBoard1Length, cBoard2Width, cBoard2Length, cmbTestType.Text, PC_TESTID_GetCMBtest, PC_PrintcardStatusID, PC_BOX_CATEGORY_ID, tFilePath.Text, tCustomerFile.Text) = 1 Then
            p.UpdateProgress(100, "100%", "Updating Printcard " & tBoxDescription.Text & ", finished...")
            p.CloseProgress()
            ResetForm()
            MessageBox.Show("Printcard " & tBoxDescription.Text & " was successfully updated.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            BrowsePrintcard.LoadData()
        Else
            p.CloseProgress()
            MessageBox.Show("Printcard " & tBoxDescription.Text & " was NOT updated. Error is: " & Printcard_ErrorUpdateSave &
                            ".", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If

        objPrintcard = Nothing

    End Sub
    Private Function ReadFile(ByVal sPath As String) As Byte()

        'Initialize byte array with a null value initially. 
        Dim data As Byte() = Nothing

        'Use FileInfo object to get file size. 
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length

        'Open FileStream to read file 
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)

        'Use BinaryReader to read file stream into byte array. 
        Dim br As New BinaryReader(fStream)

        'When you use BinaryReader, you need to supply number of bytes to read from file. 
        'In this case we want to read entire file. So supplying total number of bytes. 
        data = br.ReadBytes(CInt(numBytes))

        Return data
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
    Private Function CheckVitalValues() As Integer
        Dim GoAheadSavePrintcard As Integer = 0
        If tCustomerName.Text = "" Then
            Me.Cursor = Cursors.WaitCursor
            CustomerList.ShowDialog()
            Me.Cursor = Cursors.Default
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If

        If tBoxDescription.Text = "" Then
            MessageBox.Show("Box description is required!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            PrintcardTabControl.SelectedTab = TabCustomer
            tBoxDescription.Focus()
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If tLength.Text = "" Or tWidth.Text = "" Or tHeight.Text = "" Then
            If cmbBoxFormat.Text = "Partition" Then
                PC_PaperDimensionID = 0
            Else
                MessageBox.Show("Inside dimension is required!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                GoAheadSavePrintcard = 1
                PrintcardTabControl.SelectedTab = TabBoxDetails
                tLength.Focus()
                Return GoAheadSavePrintcard
                Exit Function
            End If
        End If
        If tPaperCombination.Text = "" Then
            MessageBox.Show("WARNING: No Paper combination entered!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            PrintcardTabControl.SelectedTab = TabBoardSpecs
            cmdGetCombination.Focus()
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If tFilePath.Text = "" Then
            MessageBox.Show("File(CDR/PDF) is required to save Printcard!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            PrintcardTabControl.SelectedTab = TabCustomer
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If PC_BoxFormatID = 40 Then
            If Board1Width.Text = "" Or Board1Length.Text = "" Then
                MessageBox.Show("Partition needs to have boardwidth and boardlength values at least for Board 1!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                PrintcardTabControl.SelectedTab = TabBoxDetails
                Board1Width.Focus()
                GoAheadSavePrintcard = 1
                Return GoAheadSavePrintcard
                Exit Function
            End If
        End If

        Return GoAheadSavePrintcard
    End Function

    Private Function CheckEditPrintcardValues() As Integer
        Dim GoAheadSavePrintcard As Integer = 0
        If tCustomerName.Text = "" Then
            Me.Cursor = Cursors.WaitCursor
            CustomerList.ShowDialog()
            Me.Cursor = Cursors.Default
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If

        If tBoxDescription.Text = "" Then
            MessageBox.Show("Box description is required!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            tBoxDescription.Focus()
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        If tLength.Text = "" Or tWidth.Text = "" Or tHeight.Text = "" Then
            If cmbBoxFormat.Text = "Partition" Then
                PC_PaperDimensionID = 0
            Else
                MessageBox.Show("Inside dimension is required!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                GoAheadSavePrintcard = 1
                Return GoAheadSavePrintcard
                Exit Function
            End If
        End If
        If tPaperCombination.Text = "" Then
            MessageBox.Show("WARNING: No Paper combination entered!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            cmdGetCombination.Focus()
            GoAheadSavePrintcard = 1
            Return GoAheadSavePrintcard
            Exit Function
        End If
        Return GoAheadSavePrintcard
    End Function
    Private Sub GetForeignKeyIDs()
        'PC_BoxFormatID = objCustomer.GetBoxFormatID(cmbBoxFormat.Text)
        'PC_JointID = objCustomer.GetJointID(cmbJoint.Text)
        'PC_cBoardTypeID = objCustomer.GetBoardTypeID(cmbBoardType.Text)
        'PC_TESTID = objCustomer.GetTestID(cmbTest.Text)
        'PC_UnitID = objUnits.GetUnitID(cmbUnits.Text)
        'PC_ScaleID = objUnits.PageScaleID(cmbScale.Text)
        If cmbPrintcardStatus.Text <> "" Then
            PC_PrintcardStatusID = objUnits.GetPrintcardStatusID(cmbPrintcardStatus.Text)
        End If

        PC_BoxOrientationID = GetTableValueIntStr("boxorientation", "id", cmbOrient.Text)
        If cmbTest.Text <> "" Then
            PC_TESTID = objCustomer.GetTestID(cmbTest.Text)
        End If
        If cmbDiecut.Text = 0 Or cmbDiecut.Text = "N/A" Then
            PC_Diecut_ID = 1
        Else
            PC_Diecut_ID = objCustomer.GetDiecutID(cmbDiecut.Text)
        End If
        lDiecut.Text = "DiecutID: " & PC_Diecut_ID

    End Sub

    Private Sub cmbUnits_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnits.SelectedIndexChanged
        Dim DataRowView As System.Data.DataRowView = cmbUnits.SelectedItem
        If IsDBNull(DataRowView) = False Then
            Dim sValue As String = DataRowView.Row("prefix")
            PC_UnitID = objUnits.GetUnitID(sValue)
        End If
    End Sub

    'Private Sub cmdPlot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlot.Click

    '    Dim B As New Bitmap(Width, Height) 
    '    Dim G As Graphics = Graphics.FromImage(B)
    '    Dim OriginX As Short = 300
    '    Dim OriginY As Short = 450
    '    Dim cLength, cWidth, cHeight As Short

    '    cLength = Convert.ToInt32(tLength.Text) ' Length
    '    cWidth = Convert.ToInt32(tWidth.Text) ' Width
    '    cHeight = Convert.ToInt32(tHeight.Text) ' Height

    '    Dim BoxLine As Pen
    '    BoxLine = New Pen(Brushes.White, 0.1)

    '    G.PageUnit = GraphicsUnit.Millimeter
    '    G.PageScale = 0.1D

    '    G.DrawLine(BoxLine, OriginX, OriginY, OriginX - GlueTab, OriginY + 10)
    '    G.DrawLine(BoxLine, OriginX - GlueTab, OriginY + 10, OriginX - GlueTab, (OriginY + cHeight) - 10)
    '    G.DrawLine(BoxLine, OriginX, OriginY + cHeight, OriginX - GlueTab, (OriginY + cHeight) - 10)
    '    BoxLine.DashStyle = Drawing2D.DashStyle.Dash
    '    G.DrawLine(BoxLine, OriginX, OriginY, OriginX, OriginY + cHeight)
    '    'Bottom Perforated line from gluetab to last panel
    '    Dim BoardSize As Short = (cLength + factor1) + (cWidth + factor2) + (cLength + factor3) + (cWidth + factor4)
    '    G.DrawLine(BoxLine, OriginX, _
    '                           OriginY + cHeight, _
    '                           OriginX + BoardSize, _
    '                           OriginY + cHeight)
    '    'Top Perforated line from gluetab to last panel
    '    G.DrawLine(BoxLine, OriginX, _
    '                           OriginY, _
    '                           OriginX + BoardSize, OriginY)

    '    'First panel
    '    Dim panel1 As Short = (cLength + OriginX) + factor1
    '    G.DrawLine(BoxLine, panel1, _
    '                           OriginY, panel1, _
    '                           OriginY + cHeight)

    '    'Second panel
    '    Dim panel2 As Short = OriginX + (cLength + factor1) + (cWidth + factor2)
    '    G.DrawLine(BoxLine, panel2, _
    '                           OriginY, panel2, _
    '                           OriginY + cHeight)

    '    'Third panel
    '    Dim panel3 As Short = OriginX + (cLength + factor1) + (cWidth + factor2) + (cLength + factor3)
    '    G.DrawLine(BoxLine, panel3, _
    '                           OriginY, panel3, _
    '                           OriginY + cHeight)

    '    'Fourth panel
    '    BoxLine.DashStyle = Drawing2D.DashStyle.Solid
    '    Dim panel4 As Short = OriginX + (cLength + factor1) + (cWidth + factor2) + (cLength + factor3) + (cWidth + factor4)
    '    G.DrawLine(BoxLine, panel4, _
    '                           OriginY - FactorFlap, _
    '                           panel4, _
    '                           OriginY + cHeight + FactorFlap)

    '    '---------------------------------------------------------------------------------
    '    'Pair top/bottom flap line
    '    'Flap @ top 4th panel 
    '    G.DrawLine(BoxLine, panel4, _
    '                           OriginY - FactorFlap, _
    '                           panel4 - (cWidth - 6), _
    '                           OriginY - FactorFlap)


    '    'Flap @ bottom 4th panel 
    '    G.DrawLine(BoxLine, panel4, _
    '                           OriginY + (cHeight + FactorFlap), _
    '                           panel4 - (cWidth - 6), _
    '                           OriginY + (cHeight + FactorFlap))

    '    '---------------------------------------------------------------------------------


    '    '---------------------------------------------------------- -----------------------
    '    'Pair top/bottom flap line
    '    'Flap @ top 3rd panel 
    '    G.DrawLine(BoxLine, panel3 - 6, _
    '                           OriginY - FactorFlap, _
    '                           (panel3 - ((cLength - 6))) - factor3, _
    '                           OriginY - FactorFlap)

    '    'Flap @ bottom 3rd panel 
    '    G.DrawLine(BoxLine, panel3 - 6, _
    '                           OriginY + cHeight + FactorFlap, _
    '                           (panel3 - ((cLength - 6))) - factor3, _
    '                           OriginY + cHeight + FactorFlap)
    '    '---------------------------------------------------------------------------------


    '    '---------------------------------------------------------- -----------------------
    '    'Pair top/bottom flap line
    '    'Flap @ top 2nd panel 
    '    G.DrawLine(BoxLine, panel2 - 6, _
    '                           OriginY - FactorFlap, _
    '                           (panel2 - ((cWidth - 6))) - factor2, _
    '                           OriginY - FactorFlap)

    '    'Flap @ bottom 2nd panel 
    '    G.DrawLine(BoxLine, panel2 - 6, _
    '                           OriginY + cHeight + FactorFlap, _
    '                           (panel2 - ((cWidth - 6))) - factor2, _
    '                           OriginY + cHeight + FactorFlap)
    '    '---------------------------------------------------------------------------------

    '    '---------------------------------------------------------- -----------------------
    '    'Pair top/bottom flap line
    '    'Flap @ top First panel 
    '    G.DrawLine(BoxLine, panel1 - 6, _
    '                           OriginY - FactorFlap, _
    '                           (panel1 - ((cLength - 6))) - factor1, _
    '                           OriginY - FactorFlap)

    '    'Flap @ bottom First panel 
    '    G.DrawLine(BoxLine, panel1 - 6, _
    '                           OriginY + cHeight + FactorFlap, _
    '                           (panel1 - ((cLength - 6))) - factor1, _
    '                           OriginY + cHeight + FactorFlap)
    '    '---------------------------------------------------------------------------------




    '    'Flap line 2 @ bottom 4th panel 
    '    G.DrawLine(BoxLine, (panel4 - (cWidth + factor4)) + 6, _
    '                           OriginY + (cHeight + FactorFlap), _
    '                           (panel4 - (cWidth + factor4)) + 6, _
    '                           OriginY + cHeight)

    '    'Flap line 1 @ bottom 3rd panel 
    '    G.DrawLine(BoxLine, (panel4 - (cWidth + factor4)) - 6, _
    '                           OriginY + (cHeight + FactorFlap), _
    '                           (panel4 - (cWidth + factor4)) - 6, _
    '                           OriginY + cHeight)

    '    'Flap line 2 @ top 4th panel 
    '    G.DrawLine(BoxLine, (panel4 - (cWidth + factor4)) + 6, _
    '                           OriginY - FactorFlap, _
    '                           (panel4 - (cWidth + factor4)) + 6, _
    '                           OriginY)

    '    'Flap line 1 @ top 3rd panel 
    '    G.DrawLine(BoxLine, (panel4 - (cWidth + factor4)) - 6, _
    '                           OriginY - FactorFlap, _
    '                           (panel4 - (cWidth + factor4)) - 6, _
    '                           OriginY)


    '    '-------------------------------------------------------------
    '    'Pair Top/Bottom
    '    'Flap line 3 @ top 3rd panel 
    '    'Where factor3 is for the 3rd panel
    '    G.DrawLine(BoxLine, (panel3 - (cLength + factor3)) + 6, _
    '                           OriginY - FactorFlap, _
    '                           (panel3 - (cLength + factor3)) + 6, _
    '                           OriginY)
    '    'Flap line 3 @ bottom 3rd panel 
    '    'Where factor3 is for the 3rd panel
    '    G.DrawLine(BoxLine, (panel3 - (cLength + factor3)) + 6, _
    '                           OriginY + cHeight, _
    '                           (panel3 - (cLength + factor3)) + 6, _
    '                           OriginY + cHeight + FactorFlap)


    '    '-------------------------------------------------------------
    '    'Pair Top/Bottom
    '    'Flap line 1 @ top 2nd panel 
    '    'Where factor2 is for the 3rd panel
    '    G.DrawLine(BoxLine, (panel3 - (cLength + factor3)) - 6, _
    '                          OriginY - FactorFlap, _
    '                          (panel3 - (cLength + factor3)) - 6, _
    '                          OriginY)

    '    'Flap line 1 @ bottom 2nd panel 
    '    'Where factor2 is for the 3rd panel
    '    G.DrawLine(BoxLine, (panel3 - (cLength + factor3)) - 6, _
    '                          OriginY + cHeight, _
    '                          (panel3 - (cLength + factor3)) - 6, _
    '                          OriginY + cHeight + FactorFlap)
    '    '-------------------------------------------------------------

    '    '-------------------------------------------------------------
    '    'Pair Top/Bottom
    '    'Flap line 3 @ top 2nd panel 
    '    'Where factor1 is for the First panel
    '    G.DrawLine(BoxLine, panel1 + 6, _
    '                           OriginY - FactorFlap, _
    '                           panel1 + 6, _
    '                           OriginY)

    '    'Flap line 3 @ top 2nd panel 
    '    'Where factor1 is for the First panel
    '    G.DrawLine(BoxLine, panel1 + 6, _
    '                           OriginY + cHeight, _
    '                           panel1 + 6, _
    '                           OriginY + cHeight + FactorFlap)
    '    '-------------------------------------------------------------

    '    '-------------------------------------------------------------
    '    'Pair Top/Bottom
    '    'Flap line 1 @ top First panel 
    '    'Where factor1 is for the First panel
    '    G.DrawLine(BoxLine, panel1 - 6, _
    '                           OriginY - FactorFlap, _
    '                           panel1 - 6, _
    '                           OriginY)

    '    'Flap line 1 @ top First panel 
    '    'Where factor1 is for the First panel
    '    G.DrawLine(BoxLine, panel1 - 6, _
    '                           OriginY + cHeight, _
    '                           panel1 - 6, _
    '                           OriginY + cHeight + FactorFlap)
    '    '-------------------------------------------------------------


    '    '-------------------------------------------------------------
    '    'Pair for Top and Bottom
    '    'Flap line 3 @ top First panel 
    '    'Where factor1 is for the First panel
    '    G.DrawLine(BoxLine, OriginX + 6, _
    '                           OriginY - FactorFlap, _
    '                           OriginX + 6, _
    '                           OriginY)

    '    'Flap line 3 @ bottom First panel 
    '    'Where factor1 is for the First panel
    '    G.DrawLine(BoxLine, OriginX + 6, _
    '                           OriginY + cHeight, _
    '                           OriginX + 6, _
    '                           OriginY + cHeight + FactorFlap)
    '    '-------------------------------------------------------------
    'End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub cmbOrient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrient.SelectedIndexChanged
        If cmbOrient.Text = "Flap-Length-Width-Length-Width" Then
            tPanel1.Clear()
            tPanel1.Text = Panel1
            tPanel2.Clear()
            tPanel2.Text = Panel2
            tPanel3.Clear()
            tPanel3.Text = Panel3
            tPanel4.Clear()
            tPanel4.Text = Panel4
        ElseIf cmbOrient.Text = "Flap-Width-Length-Width-Length" Then
            tPanel1.Clear()
            tPanel1.Text = Panel4
            tPanel2.Clear()
            tPanel2.Text = Panel3
            tPanel3.Clear()
            tPanel3.Text = Panel2
            tPanel4.Clear()
            tPanel4.Text = Panel1
        Else 'default
            tPanel1.Clear()
            tPanel1.Text = Panel1
            tPanel2.Clear()
            tPanel2.Text = Panel2
            tPanel3.Clear()
            tPanel3.Text = Panel3
            tPanel4.Clear()
            tPanel4.Text = Panel4
        End If
        Dim lPanel1 As Integer = Convert.ToInt32(tPanel1.Text) 'Temporary fix for bug : board length not accurate when changing box orientation
        tPanel1.Text = 1010
        tPanel1.Text = lPanel1
    End Sub

    Private Sub cmdRefrehDiecut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefrehDiecut.Click
        objCustomer.GetDiecut(Me.cmbDiecut)
    End Sub

    Private Sub PrintCard_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        LoadFiles()
        If tPrintcardNumber.Text <> "" Then
            ExistingPrintcardNumber = Convert.ToInt32(tPrintcardNumber.Text)
        End If
        ExistingGraphicFile = tFilePath.Text
        ExistingCustFile = tCustomerFile.Text
    End Sub

    Private Sub Board1Width_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Board1Width.KeyPress, _
        Board1Length.KeyPress, Board2Width.KeyPress, Board2Length.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbTest_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTest.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbPSI_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTest.SelectedIndexChanged
        Dim DataRowView As System.Data.DataRowView = cmbTest.SelectedItem
        If IsDBNull(DataRowView) = False Then
            Dim sValue As String = DataRowView.Row("value")
            PC_TESTID = objCustomer.GetTestID(sValue)
        End If
    End Sub

    Private Sub cmbScale_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbScale.SelectedIndexChanged
        Dim DataRowView As System.Data.DataRowView = cmbScale.SelectedItem
        If IsDBNull(DataRowView) = False Then
            Dim sValue As String = DataRowView.Row("description")
            PC_ScaleID = objUnits.PageScaleID(sValue)
        End If
    End Sub

    Private Sub GetPrintNumList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetPrintNumList.Click
        tPrintcardNumber.Text = objCustomer.GetPrintcardNum(PC_CUSTOMERID)
    End Sub

    Private Sub tPrintcardNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPrintcardNumber.LostFocus
        Dim ChangedPrintcardNum As Integer = Convert.ToInt32(tPrintcardNumber.Text)
        Dim PrintcardNumMaximumVal As Integer = objCustomer.GetPrintcardNum(PC_CUSTOMERID)

        If Me.Text.Contains("EDIT") Then
            If ChangedPrintcardNum <> ExistingPrintcardNumber Then
                If MessageBox.Show("You've changed the printcard number from: " & ExistingPrintcardNumber & " to " & ChangedPrintcardNum & ", are you sure about this?", ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    tPrintcardNumber.Text = ExistingPrintcardNumber
                Else
                    If ChangedPrintcardNum > PrintcardNumMaximumVal Then
                        If MessageBox.Show("Last printcard number used for this customer is: " & PrintcardNumMaximumVal - 1 & ", do you want to use this new value instead?", ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                            tPrintcardNumber.Text = ExistingPrintcardNumber
                        End If
                    End If
                End If

            End If
        Else
            If ChangedPrintcardNum > PrintcardNumMaximumVal Then
                If MessageBox.Show("Last printcard number used for this customer is: " & PrintcardNumMaximumVal - 1 & ", do you want to use this new value instead?", ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    tPrintcardNumber.Text = ExistingPrintcardNumber
                End If
            End If
        End If
        
    End Sub

  
    Private Sub tPrintcardNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tPrintcardNumber.TextChanged

    End Sub

    Private Sub tFilePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tFilePath.TextChanged       
        If ExistingGraphicFile = tFilePath.Text Then
            tFilePath.Text = ""
        End If
    End Sub

    Private Sub tCustomerFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tCustomerFile.TextChanged
        
    End Sub

    Private Sub tFilePath_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tFilePath.Validated

    End Sub

    Private Sub tCustomerFile_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tCustomerFile.Validated
        ExistingCustFile = tCustomerFile.Text
    End Sub

    Private Sub cmbBoxCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBoxCategory.SelectedIndexChanged
        PC_BOX_CATEGORY_ID = objCustomer.GetTableID("boxcategory", cmbBoxCategory.Text)
    End Sub
End Class