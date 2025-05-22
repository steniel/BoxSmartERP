Public Class DiecutSetup
    Public DiecutID As Integer
    Dim RackID As Integer
    Dim EditDiecutNum As Integer
    Dim RackLocation As String
    Dim OrderListRacks As String
    Private Sub DiecutSetup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        MainUI.StatusMessage.Text = Me.Text
        MainUI.btAddCustomer.Enabled = False
        MainUI.bDocumentEdit.Enabled = False
        MainUI.tbDocumentSave.Enabled = False
        MainUI.tbCancelEdit.Enabled = False
        LoadData()
    End Sub
    Private Sub DiecutSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainUI
        cmbOrder.Text = "ASCENDING"
        If cmbOrder.Text = "ASCENDING" Then
            OrderListRacks = "ASC"
        Else
            OrderListRacks = "DESC"
        End If
        LoadData()
    End Sub
    Private Sub LoadData()
        Dim objGetDiecutList As ClassDiecut = New ClassDiecut
        objGetDiecutList.GetRackList(Me.cmbRack, OrderListRacks)
        objGetDiecutList.PopulateDiecut(Me.DiecutGrid)
        objGetDiecutList = Nothing
        If DiecutGrid.RowCount = 0 Then
            cmdEdit.Enabled = False
        Else
            If DiecutID = 1 Then
                cmdEdit.Enabled = False
            Else
                cmdEdit.Enabled = True
            End If
        End If
    End Sub
    Private Sub DiecutGrid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DiecutGrid.SelectionChanged
        For Each AddressElement In DiecutGrid.SelectedRows
            DiecutID = AddressElement.cells("id").value
            If IsDBNull(AddressElement.cells("diecutnum").value) = False Then
                tDiecutNumber.Text = AddressElement.cells("diecutnum").value
            End If
            If IsDBNull(AddressElement.cells("rack").value) = False Then
                cmbRack.Text = AddressElement.cells("rack").value
            End If
            If IsDBNull(AddressElement.cells("rack_id").value) = False Then
                RackID = AddressElement.cells("rack_id").value
            End If
        Next
        
        If DiecutGrid.RowCount = 0 Then
            cmdEdit.Enabled = False
        Else
            If DiecutID = 1 Then
                cmdEdit.Enabled = False
            Else
                cmdEdit.Enabled = True
            End If            
        End If
    End Sub
    Public Sub UpdateDiecut()
        Dim objDiecut As ClassDiecut = New ClassDiecut
        objDiecut.UpdateDiecut(DiecutID, tDiecutNumber.Text, cmbRack.Text)
        objDiecut = Nothing
    End Sub
    Public Sub AddDiecut()
        Dim objDiecut As ClassDiecut = New ClassDiecut        
        objDiecut.AddDiecut(tDiecutNumber.Text, cmbRack.Text)
        objDiecut = Nothing
    End Sub
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        EditDiecutNum = tDiecutNumber.Text
        RackLocation = cmbRack.Text
        cmdEdit.Enabled = False
        cmdAdd.Enabled = False
        cmdCancel.Enabled = True
        cmdSave.Enabled = True
        DiecutGrid.Enabled = False
        GroupInputDiecut.Enabled = True
        Me.Text = "Edit Diecut"
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        tDiecutNumber.Focus()
        MainUI.StatusMessage.Text = Me.Text
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        DiecutGrid.Enabled = False
        cmdEdit.Enabled = False
        cmdAdd.Enabled = False
        cmdCancel.Enabled = True
        GroupInputDiecut.Enabled = True                
        Me.Text = "Add Diecut"
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        tDiecutNumber.Focus()        
        tDiecutNumber.Text = ""
        MainUI.StatusMessage.Text = MainUI.Text
        Dim objGetAvailableRacks As ClassDiecut = New ClassDiecut
        objGetAvailableRacks.GetRackList(Me.cmbRack, OrderListRacks)
        cmdSave.Enabled = True
        objGetAvailableRacks = Nothing
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        GroupInputDiecut.Enabled = False
        cmdCancel.Enabled = False
        cmdAdd.Enabled = True
        If DiecutGrid.RowCount = 0 Then
            cmdEdit.Enabled = False
        Else
            If DiecutID = 1 Then
                cmdEdit.Enabled = False
            Else
                cmdEdit.Enabled = True
            End If
        End If
        cmdSave.Enabled = False
        DiecutGrid.Enabled = True
        DiecutGrid.Focus()
        Me.Text = "Diecut Setup"
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        MainUI.StatusMessage.Text = Me.Text
        Dim objGetAvailableRacks As ClassDiecut = New ClassDiecut
        objGetAvailableRacks.GetRackList(Me.cmbRack, OrderListRacks)
        objGetAvailableRacks = Nothing        
        For Each AddressElement In DiecutGrid.SelectedRows
            DiecutID = AddressElement.cells("id").value
            If IsDBNull(AddressElement.cells("diecutnum").value) = False Then
                tDiecutNumber.Text = AddressElement.cells("diecutnum").value
            End If
            If IsDBNull(AddressElement.cells("rack").value) = False Then
                cmbRack.Text = AddressElement.cells("rack").value
            End If
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim objDiecut As ClassDiecut = New ClassDiecut
        If tDiecutNumber.Text = "" Then
            MsgBox("Diecut number is required!", MsgBoxStyle.Critical, "TSD Inventory System")
            tDiecutNumber.Focus()
            Exit Sub
        End If

        If Me.Text.Contains("Edit") Then
            If tDiecutNumber.Text = EditDiecutNum And RackLocation = cmbRack.Text Then
                MsgBox("Nothing was changed. No changes was made to database.", MsgBoxStyle.Information, ApplicationTitle)
                Exit Sub
            End If
            If tDiecutNumber.Text = EditDiecutNum And RackLocation <> cmbRack.Text Then
                UpdateDiecut()
            Else
                If objDiecut.CheckExistingDiecut(tDiecutNumber.Text) = True Then
                    MsgBox("Diecut number conflict! Diecut number: " & tDiecutNumber.Text & " was located at rack location: " & objDiecut.GetLocation(tDiecutNumber.Text) & "!" & Chr(10) & "Press OK to locate the row.", MsgBoxStyle.Exclamation, ApplicationTitle)
                    tDiecutNumber.SelectAll()
                    tDiecutNumber.Focus()
                    SearchGridValue(Me.DiecutGrid, "diecutnum", tDiecutNumber.Text)
                    Exit Sub
                End If
            End If
           
            MsgBox("Diecut updated!", MsgBoxStyle.Information, "TSD Information")
            GroupInputDiecut.Enabled = False
            cmdCancel.Enabled = False
            cmdAdd.Enabled = True            
            If DiecutGrid.RowCount = 0 Then
                cmdEdit.Enabled = False
            Else
                If DiecutID = 1 Then
                    cmdEdit.Enabled = False
                Else
                    cmdEdit.Enabled = True
                End If
            End If
            cmdSave.Enabled = False
            DiecutGrid.Enabled = True
            DiecutGrid.Focus()
            Me.Text = "Diecut Setup"
            MainUI.Text = ApplicationTitle & " - " & Me.Text
            MainUI.StatusMessage.Text = Me.Text
            Dim objGetAvailableRacks As ClassDiecut = New ClassDiecut
            objGetAvailableRacks.PopulateDiecut(Me.DiecutGrid)
            objGetAvailableRacks = Nothing
            For Each AddressElement In DiecutGrid.SelectedRows
                DiecutID = AddressElement.cells("id").value
                If IsDBNull(AddressElement.cells("diecutnum").value) = False Then
                    tDiecutNumber.Text = AddressElement.cells("diecutnum").value
                End If
                If IsDBNull(AddressElement.cells("rack").value) = False Then
                    cmbRack.Text = AddressElement.cells("rack").value
                End If
            Next
        Else
            If objDiecut.CheckExistingDiecut(tDiecutNumber.Text) = True Then
                MsgBox("Diecut number conflict! Diecut number: " & tDiecutNumber.Text & " was located at rack location: " & objDiecut.GetLocation(tDiecutNumber.Text) & "!" & Chr(10) & "Press OK to locate the row.", MsgBoxStyle.Exclamation, ApplicationTitle)
                tDiecutNumber.SelectAll()
                tDiecutNumber.Focus()
                SearchGridValue(Me.DiecutGrid, "diecutnum", tDiecutNumber.Text)
                Exit Sub
            End If
            AddDiecut()
            MsgBox("New Diecut saved!", MsgBoxStyle.Information, "TSD Information")
            GroupInputDiecut.Enabled = False
            cmdCancel.Enabled = False
            cmdAdd.Enabled = True
            cmdEdit.Enabled = True
            cmdSave.Enabled = False
            DiecutGrid.Enabled = True
            DiecutGrid.Focus()
            Me.Text = "Diecut Setup"
            MainUI.Text = ApplicationTitle & " - " & Me.Text
            MainUI.StatusMessage.Text = Me.Text
            Dim objGetAvailableRacks As ClassDiecut = New ClassDiecut
            objGetAvailableRacks.PopulateDiecut(Me.DiecutGrid)
            objGetAvailableRacks = Nothing
            For Each AddressElement In DiecutGrid.SelectedRows
                DiecutID = AddressElement.cells("id").value
                If IsDBNull(AddressElement.cells("diecutnum").value) = False Then
                    tDiecutNumber.Text = AddressElement.cells("diecutnum").value
                End If
                If IsDBNull(AddressElement.cells("rack").value) = False Then
                    cmbRack.Text = AddressElement.cells("rack").value
                End If
            Next
        End If
        objDiecut = Nothing
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
    Private Sub tDiecutNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tDiecutNumber.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub


    Private Sub cmdAddLoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLoc.Click
        AddLocation.ShowDialog()
    End Sub

    Private Sub DiecutGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DiecutGrid.CellContentClick

    End Sub

    Private Sub cmbRack_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRack.SelectedIndexChanged
        If cmbRack.Text = "0N/A" Then
            cmbRack.Text = "1A"
        End If
    End Sub

    Private Sub DiecutGrid_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles DiecutGrid.Sorted

    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOrder.SelectedIndexChanged
        If cmbOrder.Text = "ASCENDING" Then
            OrderListRacks = "ASC"
        Else
            OrderListRacks = "DESC"
        End If
        LoadData()
        tDiecutNumber.Focus()
    End Sub
End Class