Public Class CustomerList
    Dim objCustomerList As Customer

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Me.Cursor = Cursors.WaitCursor
        ' Determine the active child form.
        Dim ActivePrintcardWin As PrintCard = MainUI.ActiveMdiChild

        For Each CustomerList In Me.CustomerGridList.SelectedRows
            ActivePrintcardWin.PC_CUSTOMERID = CustomerList.cells("id").value
            ActivePrintcardWin.Customer_Name = CustomerList.cells("orgname").value
        Next
        ActivePrintcardWin.tCustomerName.Text = ActivePrintcardWin.Customer_Name
        ActivePrintcardWin.tPrincardPrefix.Text = objCustomerList.GetCustomerPrefix(ActivePrintcardWin.PC_CUSTOMERID) & "-PC-"
        ActivePrintcardWin.tPrintcardNumber.Text = objCustomerList.GetPrintcardNum(ActivePrintcardWin.PC_CUSTOMERID)



        Me.Cursor = Cursors.Default

        Me.Close()

    End Sub

    Private Sub CustomerList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        tCustomerSearch.Focus()
    End Sub

    Private Sub CustomerList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        objCustomerList = Nothing
    End Sub

    Private Sub CustomerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objCustomerList = New Customer
        objCustomerList.GetCustomerList(Me.CustomerGridList)
        tCustomerSearch.Focus()
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        'SearchGridValue(Me.CustomerGridList, "orgname", tCustomerSearch.Text)
        objCustomerList = New Customer
        objCustomerList.SearchCustomerKeyword(CustomerGridList, tCustomerSearch.Text)
    End Sub

    Private Sub tCustomerSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tCustomerSearch.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            'SearchGridValue(Me.CustomerGridList, "orgname", tCustomerSearch.Text)
            objCustomerList = New Customer
            objCustomerList.SearchCustomerKeyword(CustomerGridList, tCustomerSearch.Text)
        End If
    End Sub

End Class