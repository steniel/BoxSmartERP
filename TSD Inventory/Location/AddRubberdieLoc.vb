Public Class AddRubberdieLoc
    Private Sub tRackNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tRackNumber.KeyPress, tCapacity.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub AddRubberdieLoc_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        LoadData()
    End Sub
    Private Sub AddRubberdieLoc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    Private Sub LoadData()
        Dim objRacks As RubberDieClass = New RubberDieClass
        tRackNumber.Text = objRacks.GetLastRackNum()
        objRacks = Nothing
        cmbRackColumn.Text = "A"
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Dim objRacks As RubberDieClass = New RubberDieClass
        If tCapacity.Text = "" Then
            MessageBox.Show("Capacity must be set.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If
        If objRacks.CheckExistingRacknumberColumn(tRackNumber.Text, cmbRackColumn.Text) = False Then
            Dim iRes As Integer = objRacks.CheckLaktaw(tRackNumber.Text)
            If (tRackNumber.Text - iRes) > 1 Then
                MessageBox.Show("Current rack number is too high, please increment by 1 only.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                tRackNumber.Text = iRes + 1
                tRackNumber.Focus()
                Exit Sub
            End If
            If objRacks.NewRackNumber(tRackNumber.Text, cmbRackColumn.Text, tCapacity.Text) = True Then
                MsgBox("Rack location: " & tRackNumber.Text & cmbRackColumn.Text & " was created successfully.", MsgBoxStyle.Information, ApplicationTitle)
            Else
                MsgBox("Error creating rack location: " & tRackNumber.Text & cmbRackColumn.Text & "!", MsgBoxStyle.Critical, ApplicationTitle)
            End If
        Else
            MsgBox("Rack location: " & tRackNumber.Text & cmbRackColumn.Text & ", is already in used!", MsgBoxStyle.Critical, ApplicationTitle)
        End If
        objRacks = Nothing
        RubberDieCreate.LoadData()
    End Sub

    Private Sub tRackNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tRackNumber.TextChanged
        Dim objRacks As RubberDieClass = New RubberDieClass
        If IsNumeric(tRackNumber.Text) Then
            objRacks.UpdateRackLocation(tRackNumber.Text, Me.cmbRackColumn)
        End If
        objRacks = Nothing
    End Sub

End Class