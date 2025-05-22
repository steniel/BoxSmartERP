Public Class CreateOrderForm

    Private Sub CreateOrderForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        MainUI.Text = ApplicationTitle & " - " & Me.Text
    End Sub

    Private Sub CreateOrderForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainUI.Text = ApplicationTitle
    End Sub

    Private Sub CreateOrderForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ORDER_CustomerName.Text = BrowsePrintcard.ORDER_CustomerName
        ORDER_BoxDescription.Text = BrowsePrintcard.ORDER_BoxDescription
        ORDER_PrintcardNumber.Text = BrowsePrintcard.ORDER_PrintcardNum

    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ORDER_Quantity.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmdSaveOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveOrder.Click

    End Sub

    Private Sub ORDER_ItemPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ORDER_ItemPrice.KeyPress, ORDER_CustomerPONumber.KeyPress, ORDER_VariableCost.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub ORDER_Quantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ORDER_Quantity.TextChanged
        Dim TotalCost As Double

        If ORDER_Quantity.Text <> "" Then
            TotalCost = (ORDER_Quantity.Text * ORDER_ItemPrice.Text)
        End If

        ORDER_TotalCost.Text = TotalCost
    End Sub
End Class