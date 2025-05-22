Public Class RubberDieCreate
    Public RubberdieID As Integer
    Dim RackID As Integer
    Dim EditRubberdieNum As Integer
    Dim RackLocation As String
    Dim OrderListRacks As String
    Dim Customer_ID As Integer    
    Dim ERROR_SAVE As Short = 0

    'Variable to save rubberdie
    Public RUBBERDIE_Printcard_ID As Integer

    'Variable to update rubberdie
    Dim RUBBERDIE_UpdateTransactionType As Short = 0
    Dim OLD_RUBBERDIE_RackID As Integer

    Private Sub RubberDie_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainUI.Text = ApplicationTitle
    End Sub

    Private Sub RubberDie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainUI
        OrderListRacks = cmbOrder.Text
        LoadData()
        cmbOrder.Text = "ASC"
        tdescription.BackColor = Drawing.Color.White
        dDateCreated.CalendarMonthBackground = Drawing.Color.White
    End Sub
    Public Sub LoadData()
        Dim objGetRubberdieList As RubberDieClass = New RubberDieClass
        objGetRubberdieList.GetRackList(Me.cmbRack, OrderListRacks)
        objGetRubberdieList.PopulateRubberdie(Me.RubberdieGrid, 100)
        objGetRubberdieList.GetCustomer(Me.cmbCustomer)
        objGetRubberdieList.GetDieStatusValue(Me.cmbStatus)
        objGetRubberdieList = Nothing
        objGetRubberdieList = Nothing
    End Sub
    Private Sub RubberdieGrid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RubberdieGrid.SelectionChanged
        If RubberdieGrid.RowCount > 0 Then
            RUBBERDIE_UpdateList()
        Else
            EditToolStrip.Enabled = False
        End If
        If RubberdieGrid.SelectedRows.Count = 0 Then
            RepairHistoryToolStripMenuItem.Enabled = False
            MountHistoryToolStripMenuItem.Enabled = False
            EditToolStrip.Enabled = False
            MovementHistoryToolStripMenuItem.Enabled = False
        Else
            RepairHistoryToolStripMenuItem.Enabled = True
            MountHistoryToolStripMenuItem.Enabled = True
            EditToolStrip.Enabled = True
            MovementHistoryToolStripMenuItem.Enabled = True
        End If
    End Sub
    Public Sub AddRubberdie()
        Dim objDiecut As RubberDieClass = New RubberDieClass

        If objDiecut.AddRubberdie(RubberdieID, tdescription.Text, _
                                  CustID.Text, labelPrintNum.Text, _
                                  tMaxLife.Text, tBoxCount.Text, _
                                  cmbRack.Text, GetStatus(cmbStatus.Text), "N/A", tRubberdieNumber.Text, RUBBERDIE_Printcard_ID) Then 'Remarks enable at released!
            ERROR_SAVE = 0
        Else
            ERROR_SAVE = 1
        End If
        objDiecut = Nothing

    End Sub
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        AssignRubberdie.ShowDialog()

        If AssignRubberdie.DialogResult = System.Windows.Forms.DialogResult.OK Then
            Dim objGetRubberdieList As RubberDieClass = New RubberDieClass
            GroupInputRubberdie.Enabled = True
            dDateMounted.Enabled = False
            dDateRepair.Enabled = False

            cmbCustomer.Enabled = False
            tRubberdieNumber.Enabled = False

            RubberdieGrid.Enabled = False
            cmdAdd.Enabled = False
            cmdCancel.Enabled = True
            cmdSave.Enabled = True
            Me.Text = "Add Rubberdie"
            MainUI.Text = ApplicationTitle & " - " & Me.Text
            MainUI.StatusMessage.Text = MainUI.Text
            tMaxLife.Focus()
            objGetRubberdieList.GetRackListLimit(Me.cmbRack, OrderListRacks)
            objGetRubberdieList = Nothing
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        dDateCreated.Enabled = True
        GroupInputRubberdie.Enabled = False
        cmdCancel.Enabled = False
        cmdAdd.Enabled = True
        cmdSave.Enabled = False
        RubberdieGrid.Enabled = True
        RubberdieGrid.Focus()
        Me.Text = "Rubberdie Management"
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        MainUI.StatusMessage.Text = Me.Text
        Dim objGetAvailableRacks As RubberDieClass = New RubberDieClass
        objGetAvailableRacks.GetRackList(Me.cmbRack, OrderListRacks)
        objGetAvailableRacks = Nothing
        RUBBERDIE_UpdateList()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim objRubberdie As RubberDieClass = New RubberDieClass
            If tRubberdieNumber.Text = "" Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Rubberdie number is required!", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                tRubberdieNumber.Focus()
                Exit Sub
            End If

            If tdescription.Text = "" Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Description is required!", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                tdescription.Focus()
                Exit Sub
            End If

            If tMaxLife.Text = "" Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Please enter ageing.", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                tMaxLife.Focus()
                Exit Sub
            End If

            If tBoxCount.Text = "" Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Box count is required! Put zero if none.", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                tBoxCount.Focus()
                Exit Sub
            End If

            If Me.Text.Contains("Edit") Then
                'Mount = 1, Repair = 2, Move = 3, Status = 4            
                Select Case RUBBERDIE_UpdateTransactionType
                    Case 1
                        Dim ru_MountDate As String = dDateMounted.Value
                        objRubberdie.UpdateRubberdieItem("date_mounted", RubberdieID, VAR_RUBBERDIE_MOUNT, ru_MountDate, 0)
                    Case 2
                        Dim ru_RepairDate As String = dDateRepair.Value
                        objRubberdie.UpdateRubberdieItem("date_repair", RubberdieID, VAR_RUBBERDIE_REPAIR, ru_RepairDate, 0)
                    Case 3                        
                        Dim NEW_RUBBERDIE_RackID As Integer = objRubberdie.GetRackID(cmbRack.Text)
                        objRubberdie.UpdateRubberdieItem("rack_id", RubberdieID, VAR_RUBBERDIE_MOVE, "", NEW_RUBBERDIE_RackID)
                        objRubberdie.UpdateRackLocationStorageCount(NEW_RUBBERDIE_RackID)
                        objRubberdie.UpdateRackLocationStorageCount(OLD_RUBBERDIE_RackID)
                    Case 4
                        Dim ru_RUBBERDIE_StatusID As Integer = GetStatus(cmbStatus.Text)
                        objRubberdie.UpdateRubberdieItem("status_id", RubberdieID, VAR_RUBBERDIE_STATUS, "", ru_RUBBERDIE_StatusID)
                    Case Else
                        Throw New ApplicationException("Exception Occured! Value not valid for updating Rubberdie.")
                End Select

                MessageBox.Show("Rubberdie updated.", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                GroupInputRubberdie.Enabled = False
                cmdCancel.Enabled = False
                cmdAdd.Enabled = True

                cmdSave.Enabled = False
                RubberdieGrid.Enabled = True
                RubberdieGrid.Focus()
                Me.Text = "Rubberdie Management"
                MainUI.Text = ApplicationTitle & " - " & Me.Text
                MainUI.StatusMessage.Text = Me.Text

                objRubberdie.PopulateRubberdie(Me.RubberdieGrid, 100)

                RUBBERDIE_UpdateList()

                dDateCreated.Enabled = True
            Else ' New printcard/rubber die
                If objRubberdie.CheckExistingRubberdie(labelPrintNum.Text, Customer_ID) = True Then
                    MessageBox.Show("Customer " & cmbCustomer.Text & " Printcard/Rubberdie number: " & tRubberdieNumber.Text & " already exists. Press Ok to locate the row.", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    tRubberdieNumber.SelectAll()
                    tRubberdieNumber.Focus()
                    SearchGridValue(Me.RubberdieGrid, "die_number", tRubberdieNumber.Text)
                    Exit Sub
                End If

                AddRubberdie()

                If ERROR_SAVE = 1 Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                MessageBox.Show("Customer " & cmbCustomer.Text & " Printcard/Rubberdie number: " & tRubberdieNumber.Text & " was saved.", _
                                    ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                GroupInputRubberdie.Enabled = False
                cmdCancel.Enabled = False
                cmdAdd.Enabled = True
                cmdSave.Enabled = False
                RubberdieGrid.Enabled = True
                RubberdieGrid.Focus()
                Me.Text = "Rubberdie Management"
                MainUI.Text = ApplicationTitle & " - " & Me.Text
                MainUI.StatusMessage.Text = Me.Text

                objRubberdie.PopulateRubberdie(Me.RubberdieGrid, 100)

                RUBBERDIE_UpdateList()
            End If
            objRubberdie = Nothing
        Catch ex As ApplicationException
            MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Catch ex As Exception
            MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        

        Me.Cursor = Cursors.Default

    End Sub
    Private Sub RUBBERDIE_UpdateList()
        For Each AddressElement In RubberdieGrid.SelectedRows
            RubberdieID = AddressElement.cells("rubber_id").value
            If IsDBNull(AddressElement.cells("die_number").value) = False Then
                tRubberdieNumber.Text = AddressElement.cells("die_number").value
            End If
            If IsDBNull(AddressElement.cells("rack").value) = False Then
                cmbRack.Text = AddressElement.cells("rack").value
            End If
            If IsDBNull(AddressElement.cells("organization_name").value) = False Then
                cmbCustomer.Text = AddressElement.cells("organization_name").value
            End If
            If IsDBNull(AddressElement.cells("description").value) = False Then
                tdescription.Text = AddressElement.cells("description").value
            End If
            If IsDBNull(AddressElement.cells("boxes_count").value) = False Then
                tBoxCount.Text = AddressElement.cells("boxes_count").value
            End If
            If IsDBNull(AddressElement.cells("ageing").value) = False Then
                tMaxLife.Text = AddressElement.cells("ageing").value
            End If
            If IsDBNull(AddressElement.cells("date_created").value) = False Then
                dDateCreated.Value = AddressElement.cells("date_created").value
            End If
            If IsDBNull(AddressElement.cells("date_mounted").value) = False Then
                dDateMounted.Value = AddressElement.cells("date_mounted").value
            End If
            If IsDBNull(AddressElement.cells("date_repair").value) = False Then
                dDateRepair.Value = AddressElement.cells("date_repair").value
            End If
            If IsDBNull(AddressElement.cells("status").value) = False Then
                cmbStatus.Text = AddressElement.cells("status").value
            End If
        Next
    End Sub
    Private Function SearchGridValue(ByVal dtg As DataGridView, ByVal ColumnName As String, ByVal ValueToSearch As String) As Boolean
        Dim Found As Boolean = False
        Dim StringToSearch As String = ""
        Dim ValueToSearchFor As String = ValueToSearch.Trim.ToLower
        Dim CurrentRowIndex As Integer = 0
        Try
            If dtg.Rows.Count = 0 Then
                CurrentRowIndex = 0
            Else
                CurrentRowIndex = dtg.CurrentRow.Index + 1
            End If
            If CurrentRowIndex > dtg.Rows.Count Then
                CurrentRowIndex = dtg.Rows.Count - 1
            End If
            If dtg.Rows.Count > 0 Then
                For Each gRow As DataGridViewRow In dtg.Rows
                    StringToSearch = gRow.Cells(ColumnName).Value.ToString.Trim.ToLower
                    If StringToSearch = ValueToSearchFor Then
                        Dim myCurrentCell As DataGridViewCell = gRow.Cells(ColumnName)
                        dtg.CurrentCell = myCurrentCell
                        Found = True
                    End If
                    If Found Then
                        Exit For
                    End If
                Next
            End If
            If Not Found Then
                If dtg.Rows.Count > 0 Then
                    Dim myFirstCurrentCell As DataGridViewCell = dtg.Rows(0).Cells(ColumnName)
                    dtg.CurrentCell = myFirstCurrentCell
                End If
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Information)
        End Try
        Return Found
    End Function
    Private Sub tRubberdieNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tRubberdieNumber.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub
    Private Sub cmbCustomer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustomer.SelectedIndexChanged
        Dim objcustomer As RubberDieClass = New RubberDieClass
        'If cmbCustomer.Text <> "System.Data.DataRowView" Then
        '    Customer_ID = objcustomer.GetCustomerID(cmbCustomer.Text)
        '    CustID.Text = Customer_ID
        'End If    
        Dim DataRowView As System.Data.DataRowView = cmbCustomer.SelectedItem
        If IsDBNull(DataRowView) = False Then
            Dim sValue As String = DataRowView.Row("org_name")
            Customer_ID = objcustomer.GetCustomerID(sValue)
            CustID.Text = Customer_ID
        End If

        objcustomer = Nothing
        tdescription.Focus()

    End Sub

    Private Sub cmdAddLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLoc.Click
        AddRubberdieLoc.ShowDialog()
    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrder.SelectedIndexChanged
        Dim objGetRubberdieList As RubberDieClass = New RubberDieClass
        OrderListRacks = cmbOrder.Text
        objGetRubberdieList.GetRackList(Me.cmbRack, OrderListRacks)
        tdescription.Focus()
        objGetRubberdieList = Nothing
    End Sub
    Private Sub tMaxLife_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tMaxLife.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tBoxCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tBoxCount.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Function GetStatus(ByVal StatusType As String) As Integer
        Dim iRes As Integer
        Select Case StatusType
            Case "Active"
                iRes = DIE_ACTIVE
            Case "Development"
                iRes = DIE_DEVELOPMENT
            Case "Under Repair"
                iRes = DIE_UNDER_REPAIR
            Case "Broken"
                iRes = DIE_BROKEN
            Case "For Replacement"
                iRes = DIE_FOR_REPLACEMENT
            Case Else
                Throw New ApplicationException("Invalid Rubberdie status value!")
        End Select
        Return iRes
    End Function

    Private Sub MountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MountToolStripMenuItem.Click
        Me.Text = "Edit Rubberdie (Mount)"
        MainUI.Text = ApplicationTitle & " - " & Me.Text

        GroupInputRubberdie.Enabled = True
        RubberdieGrid.Enabled = False
        cmdSave.Enabled = True
        cmdCancel.Enabled = True
        cmdAdd.Enabled = False
        cmbStatus.Enabled = False
        cmbRack.Enabled = False
        cmbCustomer.Enabled = False
        tdescription.Enabled = False
        tRubberdieNumber.Enabled = False
        tMaxLife.Enabled = False
        tBoxCount.Enabled = False
        cmbRack.Enabled = False
        cmbOrder.Enabled = False
        cmdAddLoc.Enabled = False
        cmbStatus.Enabled = False
        dDateRepair.Enabled = False
        dDateCreated.Enabled = False

        dDateMounted.Enabled = True
        dDateMounted.Focus()

        RUBBERDIE_UpdateTransactionType = 1

    End Sub

    Private Sub RepairToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepairToolStripMenuItem.Click
        Me.Text = "Edit Rubberdie (Repair)"
        MainUI.Text = ApplicationTitle & " - " & Me.Text

        GroupInputRubberdie.Enabled = True
        RubberdieGrid.Enabled = False
        cmdSave.Enabled = True
        cmdCancel.Enabled = True
        cmdAdd.Enabled = False
        cmbStatus.Enabled = False
        cmbRack.Enabled = False
        cmbCustomer.Enabled = False
        tdescription.Enabled = False
        tRubberdieNumber.Enabled = False
        tMaxLife.Enabled = False
        tBoxCount.Enabled = False
        cmbRack.Enabled = False
        cmbOrder.Enabled = False
        cmdAddLoc.Enabled = False
        cmbStatus.Enabled = False
        dDateCreated.Enabled = False
        dDateMounted.Enabled = False

        dDateRepair.Enabled = True
        dDateRepair.Focus()

        RUBBERDIE_UpdateTransactionType = 2

    End Sub

    Private Sub MoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveToolStripMenuItem.Click
        Me.Text = "Edit Rubberdie (Move)"
        MainUI.Text = ApplicationTitle & " - " & Me.Text

        GroupInputRubberdie.Enabled = True
        RubberdieGrid.Enabled = False
        cmdSave.Enabled = True
        cmdCancel.Enabled = True
        cmdAdd.Enabled = False
        cmbStatus.Enabled = False
        cmbRack.Enabled = False
        cmbCustomer.Enabled = False
        tdescription.Enabled = False
        tRubberdieNumber.Enabled = False
        tMaxLife.Enabled = False
        tBoxCount.Enabled = False
        dDateRepair.Enabled = False
        cmbOrder.Enabled = True
        cmdAddLoc.Enabled = True
        cmbStatus.Enabled = False
        dDateCreated.Enabled = False
        dDateMounted.Enabled = False

        cmbRack.Enabled = True
        cmbRack.Focus()

        RUBBERDIE_UpdateTransactionType = 3
        Dim objGetOLD_RACKID As RubberDieClass = New RubberDieClass
        OLD_RUBBERDIE_RackID = objGetOLD_RACKID.GetRackID(cmbRack.Text)
        objGetOLD_RACKID = Nothing
    End Sub

    Private Sub StatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusToolStripMenuItem.Click
        Me.Text = "Edit Rubberdie (Status)"
        MainUI.Text = ApplicationTitle & " - " & Me.Text

        GroupInputRubberdie.Enabled = True
        RubberdieGrid.Enabled = False
        cmdSave.Enabled = True
        cmdCancel.Enabled = True
        cmdAdd.Enabled = False
        cmbRack.Enabled = False
        cmbRack.Enabled = False
        cmbCustomer.Enabled = False
        tdescription.Enabled = False
        tRubberdieNumber.Enabled = False
        tMaxLife.Enabled = False
        tBoxCount.Enabled = False
        dDateRepair.Enabled = False
        cmbOrder.Enabled = False
        cmdAddLoc.Enabled = False
        cmbStatus.Enabled = False
        dDateCreated.Enabled = False
        dDateMounted.Enabled = False

        cmbStatus.Enabled = True
        cmbStatus.Focus()

        RUBBERDIE_UpdateTransactionType = 4
    End Sub
    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        GroupSearch.Enabled = True
        cSearchThis.Focus()
    End Sub
    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Dim objGetRubberdieList As RubberDieClass = New RubberDieClass
        objGetRubberdieList.PopulateRubberdie(Me.RubberdieGrid, 100)
        objGetRubberdieList = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub RubberdieGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles RubberdieGrid.CellContentClick

    End Sub
End Class