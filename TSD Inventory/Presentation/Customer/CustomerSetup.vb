Imports Npgsql
Imports System.Drawing

Public Class CustomerSetup
    Dim objPopulateCustomer As ClassManageCustomer
    Public CustomerID As Integer
    Public ErrorSavingCustomer As Boolean
    Public Structure Customer
        Dim Name As String
        Dim Address As String
        Dim Telephone As String
        Dim Contact As String

    End Structure
    Public Property CustomerName() As String
        Get
            Return tCustomerName.Text
        End Get
        Set(ByVal Value As String)
            tCustomerName.Text = Value
        End Set
    End Property

    Private Sub CustomerSetup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        MainUI.Text = ApplicationTitle & " - " & Me.Text
        MainUI.btAddCustomer.Enabled = True
        MainUI.StatusMessage.Text = Me.Text
        MainUI.btAddCustomer.Text = "Add Customer"
        MainUI.bDocumentEdit.Text = "Edit Customer"
        MainUI.tbDocumentSave.Text = "Save Customer"
        MainUI.tbDocumentSave.Enabled = False
        MainUI.tbCancelEdit.Enabled = False
    End Sub

    Private Sub CustomerSetup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        objPopulateCustomer = Nothing
        MainUI.tbDocumentSave.Enabled = False
        MainUI.bDocumentEdit.Enabled = False
        MainUI.tbCancelEdit.Enabled = False
        MainUI.btAddCustomer.Enabled = False
        MainUI.Text = ApplicationTitle
    End Sub

    Private Sub CustomerSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load        
        Me.MdiParent = MainUI
        objPopulateCustomer = New ClassManageCustomer
        objPopulateCustomer.PopulateCustomer(Me.CustomerGrid, "Active")
        objPopulateCustomer.GetComboBoxItems(Me.cmbIndustryType, "industry", "industry_type")
        objPopulateCustomer.ListCity(Me.tCityName)
        objPopulateCustomer.ListProvince(Me.tProvinceName)
        objPopulateCustomer.ListCountry(Me.tCountryName)
        CustomerGrid.Focus()
    End Sub
    Public Sub AddNewCustomer()
        Dim objCustomer As ClassManageCustomer = New ClassManageCustomer
        If objPopulateCustomer.AddCustomer(lNewCustomerID.Text, _
                                           tCustomerName.Text, _
                                           tLastname.Text, tFirstname.Text, _
                                           tOfficeTelephone.Text, tMobileNumber.Text, _
                                           tEmail.Text, tStreetName.Text, _
                                           tBarangayName.Text, _
                                           tPrintcardPrefix.Text, _
                                           cmbIndustryType.Text, tCityName.Text) = False Then
            ErrorSavingCustomer = True
            Exit Sub
        Else
            ErrorSavingCustomer = False
        End If
        objCustomer.PopulateCustomer(Me.CustomerGrid, "Active")
        objCustomer = Nothing
    End Sub
    Public Function ValidateEntries() As Boolean
        If tPrintcardPrefix.Text = "" Or tCustomerName.Text = "" Or tCountryName.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub ResetFormNew()
        objPopulateCustomer = New ClassManageCustomer
        objPopulateCustomer.PopulateCustomer(Me.CustomerGrid, "Active")
        objPopulateCustomer.GetComboBoxItems(Me.cmbIndustryType, "industry", "industry_type")
        objPopulateCustomer.ListCity(Me.tCityName)
        objPopulateCustomer.ListProvince(Me.tProvinceName)
        objPopulateCustomer.ListCountry(Me.tCountryName)
        tCustomerName.Text = ""
        tStreetName.Text = ""
        tCityName.Text = ""
        tProvinceName.Text = ""
        tFirstname.Text = ""
        tLastname.Text = ""
        tOfficeTelephone.Text = ""
        tMobileNumber.Text = ""
        tEmail.Text = ""
        tPrintcardPrefix.Text = ""
    End Sub
    Public Sub CancelAdd()
        For Each AddressElement In CustomerGrid.SelectedRows
            CustomerID = AddressElement.cells("id").value
            If IsDBNull(AddressElement.cells("tcompanyname").value) = False Then
                tCustomerName.Text = AddressElement.cells("tcompanyname").value
            Else
                tCustomerName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("country").value) = False Then
                tCountryName.Text = AddressElement.cells("country").value
            Else
                tCountryName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("street_name").value) = False Then
                tStreetName.Text = AddressElement.cells("street_name").value
            Else
                tStreetName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("barangay_name").value) = False Then
                tBarangayName.Text = AddressElement.cells("barangay_name").value
            Else
                tBarangayName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("province").value) = False Then
                tProvinceName.Text = AddressElement.cells("province").value
            Else
                tProvinceName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("cityname").value) = False Then
                tCityName.Text = AddressElement.cells("cityname").value
            Else
                tCityName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("firstname").value) = False Then
                tFirstname.Text = AddressElement.cells("firstname").value
            Else
                tFirstname.Text = ""
            End If
            If IsDBNull(AddressElement.cells("lastname").value) = False Then
                tLastname.Text = AddressElement.cells("lastname").value
            Else
                tLastname.Text = ""
            End If
            If IsDBNull(AddressElement.cells("officephone").value) = False Then
                tOfficeTelephone.Text = AddressElement.cells("officephone").value
            Else
                tOfficeTelephone.Text = ""
            End If
            If IsDBNull(AddressElement.cells("mobilephone").value) = False Then
                tMobileNumber.Text = AddressElement.cells("mobilephone").value
            Else
                tMobileNumber.Text = ""
            End If
            If IsDBNull(AddressElement.cells("email").value) = False Then
                tEmail.Text = AddressElement.cells("email").value
            Else
                tEmail.Text = ""
            End If
            If IsDBNull(AddressElement.cells("printcard_prefix").value) = False Then
                tPrintcardPrefix.Text = AddressElement.cells("printcard_prefix").value
            Else
                tPrintcardPrefix.Text = ""
            End If
            If IsDBNull(AddressElement.cells("industry").value) = False Then
                cmbIndustryType.Text = AddressElement.cells("industry").value
            Else
                tPrintcardPrefix.Text = ""
            End If
        Next
    End Sub
    Public Sub SaveEditCustomer()
        Dim objCustomer As ClassManageCustomer = New ClassManageCustomer
        objPopulateCustomer.EditCustomer(CustomerID, tCustomerName.Text, tLastname.Text, tFirstname.Text, tOfficeTelephone.Text, tMobileNumber.Text, _
                                             tEmail.Text, tStreetName.Text, tBarangayName.Text, tPrintcardPrefix.Text, cmbIndustryType.Text, tCityName.Text)
        objCustomer.PopulateCustomer(Me.CustomerGrid, "Active")
        objCustomer = Nothing
    End Sub


    Private Sub CustomerGrid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CustomerGrid.SelectionChanged
        For Each AddressElement In CustomerGrid.SelectedRows
            CustomerID = AddressElement.cells("id").value
            If IsDBNull(AddressElement.cells("tcompanyname").value) = False Then
                tCustomerName.Text = AddressElement.cells("tcompanyname").value
            Else
                tCustomerName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("country").value) = False Then
                tCountryName.Text = AddressElement.cells("country").value
            Else
                tCountryName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("street_name").value) = False Then
                tStreetName.Text = AddressElement.cells("street_name").value
            Else
                tStreetName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("barangay_name").value) = False Then
                tBarangayName.Text = AddressElement.cells("barangay_name").value
            Else
                tBarangayName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("province").value) = False Then
                tProvinceName.Text = AddressElement.cells("province").value
            Else
                tProvinceName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("cityname").value) = False Then
                tCityName.Text = AddressElement.cells("cityname").value
            Else
                tCityName.Text = ""
            End If
            If IsDBNull(AddressElement.cells("firstname").value) = False Then
                tFirstname.Text = AddressElement.cells("firstname").value
            Else
                tFirstname.Text = ""
            End If
            If IsDBNull(AddressElement.cells("lastname").value) = False Then
                tLastname.Text = AddressElement.cells("lastname").value
            Else
                tLastname.Text = ""
            End If
            If IsDBNull(AddressElement.cells("officephone").value) = False Then
                tOfficeTelephone.Text = AddressElement.cells("officephone").value
            Else
                tOfficeTelephone.Text = ""
            End If
            If IsDBNull(AddressElement.cells("mobilephone").value) = False Then
                tMobileNumber.Text = AddressElement.cells("mobilephone").value
            Else
                tMobileNumber.Text = ""
            End If
            If IsDBNull(AddressElement.cells("email").value) = False Then
                tEmail.Text = AddressElement.cells("email").value
            Else
                tEmail.Text = ""
            End If
            If IsDBNull(AddressElement.cells("printcard_prefix").value) = False Then
                tPrintcardPrefix.Text = AddressElement.cells("printcard_prefix").value
            Else
                tPrintcardPrefix.Text = ""
            End If
            If IsDBNull(AddressElement.cells("industry").value) = False Then
                cmbIndustryType.Text = AddressElement.cells("industry").value
            Else
                tPrintcardPrefix.Text = ""
            End If
        Next

        If chkShowInActiveCust.Checked = True Then
            MainUI.bDocumentEdit.Enabled = False
            If CustomerGrid.SelectedRows.Count = 0 Then
                cxSubCustomerStatusText.Enabled = False
            Else
                cxSubCustomerStatusText.Enabled = True
                cxSubCustomerStatusText.Text = "Activate customer: " & tCustomerName.Text
            End If
        Else
            If CustomerGrid.SelectedRows.Count = 0 Then
                cxSubCustomerStatusText.Enabled = False
            Else
                cxSubCustomerStatusText.Enabled = True
                MainUI.bDocumentEdit.Enabled = True
                cxSubCustomerStatusText.Text = "Disable customer: " & tCustomerName.Text
            End If

        End If
    End Sub


    Private Sub tCityName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tCityName.TextChanged
        objPopulateCustomer.ListProvincesAccordingToCityName(Me.tProvinceName, tCityName.Text)
    End Sub

    Private Sub chkShowInActiveCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowInActiveCust.Click
        Dim objShowCust As ClassManageCustomer = New ClassManageCustomer
        If chkShowInActiveCust.Checked Then
            objShowCust.PopulateCustomer(Me.CustomerGrid, "Inactive")
            MainUI.bDocumentEdit.Enabled = False
            MainUI.btAddCustomer.Enabled = False
        Else
            objShowCust.PopulateCustomer(Me.CustomerGrid, "Active")
            MainUI.bDocumentEdit.Enabled = True
            MainUI.btAddCustomer.Enabled = True
        End If
        objShowCust = Nothing
    End Sub
    Private Sub cxSubCustomerStatusText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cxSubCustomerStatusText.Click
        Dim objSetStatus As ClassManageCustomer = New ClassManageCustomer
        Dim StatusType As String = ""
        If cxSubCustomerStatusText.Text.Contains("Disable") Then
            Dim resp As MsgBoxResult = MsgBox("Do you want to set customer: " & tCustomerName.Text & " as inactive?", vbYesNo, "TSD Inventory System")
            If resp = vbYes Then
                objSetStatus.SetCustomerStatus("Inactive", CustomerID)
                MsgBox("Customer: " & tCustomerName.Text & " was successfully disabled.", MsgBoxStyle.Information, "TSD Inventory System")
                objSetStatus.PopulateCustomer(Me.CustomerGrid, "Active")
            End If
        ElseIf cxSubCustomerStatusText.Text.Contains("Activate") Then
            objSetStatus.SetCustomerStatus("Active", CustomerID)
            MsgBox("Customer: " & tCustomerName.Text & " was successfully activated.", MsgBoxStyle.Information, "TSD Inventory System")
            objSetStatus.PopulateCustomer(Me.CustomerGrid, "Inactive")
        End If
        objSetStatus = Nothing
    End Sub


    Private Sub tCustomerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tCustomerName.GotFocus
        If tCustomerName.Text.Length > 0 Then
            tCustomerName.BackColor = Color.White
        End If
    End Sub

    Private Sub tCountryName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tCountryName.GotFocus
        If tCountryName.Text.Length > 0 Then
            tCountryName.BackColor = Color.White
        End If
    End Sub
    Private Sub tPrintcardPrefix_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tPrintcardPrefix.GotFocus
        If tPrintcardPrefix.Text.Length > 0 Then
            tPrintcardPrefix.BackColor = Color.White
        End If
    End Sub

    Private Sub CustomerGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CustomerGrid.CellContentClick

    End Sub
End Class