Imports System.Windows.Forms

Public Class AssignRubberdie

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        For Each Customer In CustomerAssignRubberdie.SelectedRows
            RubberDieCreate.cmbCustomer.Text = Customer.cells("organization_name").value
            RubberDieCreate.RUBBERDIE_Printcard_ID = Customer.cells("id").value
            RubberDieCreate.tdescription.Text = Customer.cells("box_description").value
            RubberDieCreate.tRubberdieNumber.Text = Customer.cells("printcardno").value
            RubberDieCreate.labelPrintNum.Text = Customer.cells("printnum").value
        Next
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub AssignRubberdie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objCustomerRubberdie As RubberDieClass = New RubberDieClass
        objCustomerRubberdie.GetCustomerRubberdieList(Me.CustomerAssignRubberdie, 300)
        objCustomerRubberdie = Nothing
    End Sub
End Class
