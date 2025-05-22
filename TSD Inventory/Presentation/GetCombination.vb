Imports System.Drawing
Public Class GetCombination
    Dim objGetCombination As Customer
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim PaperCombStr As String = ""
        Dim ActivePrintcardWin As PrintCard = MainUI.ActiveMdiChild
        For Each CombinationList In Me.gridPaperCombination.SelectedRows
            Dim strOuterColor As Integer = CombinationList.cells("liner1_color_id").value
            ActivePrintcardWin.PC_PaperCombinationID = CombinationList.cells("id").value
            PaperCombStr = CombinationList.cells("liner1_val").value & " X " & _
                           CombinationList.cells("medium1_val").value & " X " & _
                           CombinationList.cells("liner2_val").value & " X " & _
                           CombinationList.cells("medium2_val").value & " X " & _
                           CombinationList.cells("liner3_val").value
            'Put Brown or White in that textbox
            If strOuterColor = 4 Then
                ActivePrintcardWin.tOuterLiner.Text = "White"
            Else 'Then it is brown
                ActivePrintcardWin.tOuterLiner.Text = "Brown"
            End If
        Next

        ActivePrintcardWin.tPaperCombination.Text = PaperCombStr

        Me.Close()
    End Sub

    Private Sub GetCombination_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        objGetCombination = Nothing
    End Sub

    Private Sub GetCombination_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objGetCombination = New Customer
        Dim ActivePrintcardWin As PrintCard = MainUI.ActiveMdiChild
        objGetCombination.GetPaperCombination(Me.gridPaperCombination, ActivePrintcardWin.cmbBoardType.Text)
        If gridPaperCombination.RowCount = 0 Then
            MsgBox("No paper combination for type: " & ActivePrintcardWin.cmbBoardType.Text & ", please add paper combinations of this type.", MsgBoxStyle.Critical, "TSD Inventory")
            ActivePrintcardWin.cmbBoardType.Text = ActivePrintcardWin.PreviousFlute
            Me.Close()
        End If
    End Sub

    Private Sub gridPaperCombination_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridPaperCombination.CellContentClick

    End Sub

    Private Sub gridPaperCombination_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles gridPaperCombination.CellFormatting
        For Each row As DataGridViewRow In gridPaperCombination.Rows
            If row.Cells("liner1_color_id").Value = 2 Then
                row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#AF804C") ' Corrugated box color
            End If
        Next
    End Sub
End Class