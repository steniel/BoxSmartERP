Public Class PriceSet
    Dim PrintcardID As Integer
    Dim CustomerID As Integer
    Dim FluteID As Integer
    Dim PrintcardInsideDimensionID As Integer
    Dim CurrencyID As Integer
    Dim PaperCombinationID As Integer
    Dim Liner1, Medium1, Liner2, Medium2, Liner3 As Integer
    Dim cMouseHover As Drawing.Color = Drawing.Color.Wheat
    Dim L1, M1, L2, M2, L3 As Decimal
    Dim OuterColor As String = ""
    Dim InnerColor As String = ""
    Dim NewItem As Boolean
    Dim CurrentPrice As Decimal
    Dim BoxFormatID As Integer
    Private Sub PriceSet_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Get liner color
        Dim PaperCombinationID As Integer = GetTableValueInt("printcard", "paper_combination_id", PrintcardID)
        If tFlute.Text = "CB" Then
            OuterColor = UCase(GetTableValueStr("paper_combination", "outer_color", PaperCombinationID))
            InnerColor = UCase(GetTableValueStr("paper_combination", "inner_color", PaperCombinationID))
        Else
            OuterColor = UCase(GetTableValueStr("paper_combination", "outer_color", PaperCombinationID))
        End If

        ComputeWeightAndCost()
        tItemPrice.Focus()        
    End Sub
    Private Sub ComputeWeightAndCost()
        If CheckValues() = True Then
            'Compute            
            tBoardWidthA.Text = tBoardWidth.Text * mm_in_meters
            tBoardLengthA.Text = tBoardLength.Text * mm_in_meters
            tBoardAreaA.Text = tBoardWidthA.Text * tBoardLengthA.Text
            If BoxFormatID = 40 Then
                tBoard2WidthA.Text = tBoardWidth2.Text * mm_in_meters
                tBoard2LengthA.Text = tBoardLength2.Text * mm_in_meters
                tBoardAreaA.Text = tBoardAreaA.Text + (tBoard2WidthA.Text * tBoard2LengthA.Text)
            End If
            PaperCombinationCompute()
            If tFlute.Text <> "CB" Then
                If tFlute.Text = "C" Then
                    tWeightPerPiece.Text = ((tLiner1.Text + (tMedium1.Text * C_CFlute) + tLiner2.Text) / DIV_ppcom) * (tBoardWidthA.Text * tBoardLengthA.Text)
                    ComputeCost()
                ElseIf tFlute.Text = "B" Then
                    tWeightPerPiece.Text = ((tLiner1.Text + (tMedium1.Text * CoBFlute) + tLiner2.Text) / DIV_ppcom) * (tBoardWidthA.Text * tBoardLengthA.Text)
                    ComputeCost()
                End If
            Else
                tWeightPerPiece.Text = ((tLiner1.Text + ((tMedium1.Text * C_CFlute) + (tMedium2.Text * CoBFlute)) + tLiner2.Text) / DIV_ppcom) * (tBoardWidthA.Text * tBoardLengthA.Text)
                ComputeCost()
            End If
        End If
    End Sub

    Private Sub PaperCombinationCompute()
        If tFlute.Text <> "CB" Then
            If tFlute.Text = "C" Then               
                L1 = tLiner1.Text / DIV_ppcom
                M1 = (tMedium1.Text * C_CFlute) / DIV_ppcom
                L2 = tLiner2.Text / DIV_ppcom
            ElseIf tFlute.Text = "B" Then
                L1 = tLiner1.Text / DIV_ppcom
                M1 = (tMedium1.Text * CoBFlute) / DIV_ppcom
                L2 = tLiner2.Text / DIV_ppcom
            End If
            tMedium2.Text = 0
            tLiner3.Text = 0
            tMedium2.Enabled = False
            tLiner3.Enabled = False
            M2 = 0
            L3 = 0
        Else 'CB-Flute
            tMedium2.Enabled = True
            tLiner3.Enabled = True
            tMedium2.Text = GetTableValueInt("paper_combination", "medium2", PaperCombinationID)
            tLiner3.Text = GetTableValueInt("paper_combination", "liner3", PaperCombinationID)
            L1 = tLiner1.Text / DIV_ppcom
            M1 = (tMedium1.Text * C_CFlute) / DIV_ppcom
            L2 = tLiner2.Text / DIV_ppcom
            M2 = (tMedium2.Text * CoBFlute) / DIV_ppcom
            L3 = tLiner3.Text / DIV_ppcom
        End If
    End Sub
    Private Sub PriceSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tBoardLengthA.Clear()
        tBoardWidthA.Clear()
        tBoardAreaA.Clear()
        tLength.Clear()
        tWidth.Clear()
        tHeight.Clear()
        tBoardWidth.Clear()
        tBoardLength.Clear()
        tBoardWidth2.Clear()
        tBoardLength2.Clear()
        tLiner1.Clear()
        tLiner2.Clear()
        tLiner3.Clear()
        tMedium1.Clear()
        tMedium2.Clear()
        tWeightPerPiece.Clear()
        tPaperWeight.Clear()

        For Each PrintCard In BrowsePrintcard.PrintcardGrid.SelectedRows
            lCustomerName.Text = PrintCard.cells("organization_name").value
            lBoxDescription.Text = PrintCard.cells("box_description").value
            PrintcardID = PrintCard.cells("printcardid").value
            PaperCombinationID = PrintCard.cells("combinationid").value
            CustomerID = PrintCard.cells("customer_id").value
            BoxFormatID = PrintCard.cells("boxformat_id").value
        Next

        If BoxFormatID = 40 Then ' Partition
            GroupBox1.Text = "Partition"
            GroupBoard2.Visible = True
            label_length.Visible = True
            label_width.Visible = True
            label_Divider.Visible = True
            tBoard2LengthA.Visible = True
            tBoard2WidthA.Visible = True
            tBoardWidth.Text = GetTableValueInt("printcard", "board1width", PrintcardID)
            tBoardLength.Text = GetTableValueInt("printcard", "board1length", PrintcardID)
            tBoardWidth2.Text = GetTableValueInt("printcard", "board2width", PrintcardID)
            tBoardLength2.Text = GetTableValueInt("printcard", "board2length", PrintcardID)
            tLiner1.Text = GetTableValueInt("paper_combination", "liner1", PaperCombinationID)
            tMedium1.Text = GetTableValueInt("paper_combination", "medium1", PaperCombinationID)
            tLiner2.Text = GetTableValueInt("paper_combination", "liner2", PaperCombinationID)
            tLength.Text = 0
            tWidth.Text = 0
            tHeight.Text = 0
            FluteID = GetTableValueInt("printcard", "flute_id", PrintcardID)
            tCurrencyCode.Text = GetCurrencyCodeStr(CustomerID, "code")
            CurrentPrice = GetPrintcardInfoPrice("item_price", "price", "printcard_id", PrintcardID)
            tCurrentPrice.Text = CurrentPrice
            tFlute.Text = GetTableValueStr("flute", "flute", FluteID)
            CurrencyID = GetCurrencyCodeInt(CustomerID)
        Else
            label_length.Visible = False
            label_width.Visible = False
            label_Divider.Visible = False
            tBoard2LengthA.Visible = False
            tBoard2WidthA.Visible = False
            GroupBoard2.Visible = False
            PrintcardInsideDimensionID = GetTableValueInt("printcard", "dimension_id", PrintcardID)
            tLength.Text = GetTableValueInt("paper_dimension", "length", PrintcardInsideDimensionID)
            tWidth.Text = GetTableValueInt("paper_dimension", "width", PrintcardInsideDimensionID)
            tHeight.Text = GetTableValueInt("paper_dimension", "height", PrintcardInsideDimensionID)
            tBoardWidth.Text = GetTableValueInt("printcard", "boardwidth", PrintcardID)
            tBoardLength.Text = GetTableValueInt("printcard", "boardlength", PrintcardID)
            FluteID = GetTableValueInt("printcard", "flute_id", PrintcardID)
            tLiner1.Text = GetTableValueInt("paper_combination", "liner1", PaperCombinationID)
            tMedium1.Text = GetTableValueInt("paper_combination", "medium1", PaperCombinationID)
            tLiner2.Text = GetTableValueInt("paper_combination", "liner2", PaperCombinationID)
            tCurrencyCode.Text = GetCurrencyCodeStr(CustomerID, "code")
            CurrentPrice = GetPrintcardInfoPrice("item_price", "price", "printcard_id", PrintcardID)
            tCurrentPrice.Text = CurrentPrice
            tFlute.Text = GetTableValueStr("flute", "flute", FluteID)
            CurrencyID = GetCurrencyCodeInt(CustomerID)
        End If
                
        PaperCombinationCompute()

        tPaperWeight.Text = L1 + M1 + L2 + M2 + L3
        cWKL.Text = COST_WKL
        cBKL.Text = COST_BKL
        cSCM.Text = COST_SCM

        PriceError = "" 'reset error message

    End Sub
    Private Function CheckValues() As Boolean
        If tLiner1.Text <> "" And tMedium1.Text <> "" And tLiner2.Text <> "" And tMedium2.Text <> "" And tLiner3.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub tLiner1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLiner1.MouseHover
        tLiner1.BackColor = cMouseHover
    End Sub

    Private Sub tLiner1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tLiner1.TextChanged, _
        tLiner2.TextChanged, tLiner3.TextChanged, tMedium1.TextChanged, tMedium2.TextChanged
        If IsDBNull(tLiner1.Text) = False Or IsDBNull(tMedium1.Text) = False Or IsDBNull(tMedium1.Text) = False Or IsDBNull(tLiner2.Text) = False Or IsDBNull(tMedium2.Text) = False Or IsDBNull(tLiner3.Text) = False Then
            tPaperWeight.Text = ComputePaperWeight(CheckNullVal(tLiner1.Text), _
                                               CheckNullVal(tMedium1.Text), _
                                               CheckNullVal(tLiner2.Text), _
                                               CheckNullVal(tMedium2.Text), _
                                               CheckNullVal(tLiner3.Text))
            LimitCharsTo(Me.tLiner1, 3)
            LimitCharsTo(Me.tMedium1, 3)
            LimitCharsTo(Me.tLiner2, 3)
            LimitCharsTo(Me.tMedium2, 3)
            LimitCharsTo(Me.tLiner3, 3)
            ComputeWeightAndCost()

        End If

    End Sub

    Private Sub LimitCharsTo(ByVal TextC As TextBox, ByVal CharLimit As Integer)
        If TextC.TextLength > CharLimit Then
            SendKeys.Send(vbBack)
        End If
    End Sub
    Private Function CheckNullVal(ByVal kl As String) As Integer
        Dim iRes As Integer
        If kl <> "" Then
            iRes = Convert.ToInt32(kl)
        End If
        Return iRes
    End Function
    Private Function CheckEmptyString(ByVal kl As String) As Decimal
        Dim iRes As Integer
        If kl <> "" Then
            iRes = Convert.ToDecimal(kl)
        Else
            iRes = 0
        End If
        Return iRes
    End Function
    Private Function ComputePaperWeight(ByVal Liner1 As Decimal, ByVal Medium1 As Decimal, ByVal Liner2 As Decimal, ByVal Medium2 As Decimal, ByVal Liner3 As Decimal) As Decimal
        Dim iRes As Decimal

        If tFlute.Text <> "CB" Then
            If tFlute.Text = "C" Then
                iRes = (Liner1 / DIV_ppcom) + ((Medium1 * C_CFlute) / DIV_ppcom) + (Liner2 / DIV_ppcom)
            ElseIf tFlute.Text = "B" Then
                iRes = (Liner1 / DIV_ppcom) + ((Medium1 * CoBFlute) / DIV_ppcom) + (Liner2 / DIV_ppcom)
            End If
        Else 'CB-Flute
            iRes = (Liner1 / DIV_ppcom) + ((Medium1 * C_CFlute) / DIV_ppcom) + (Liner2 / DIV_ppcom) + ((Medium2 * CoBFlute) / DIV_ppcom) + (Liner3 / DIV_ppcom)
        End If

        Return iRes
    End Function

    Private Sub tLiner1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tLiner1.KeyPress, tLiner2.KeyPress, _
        tLiner3.KeyPress, tMedium1.KeyPress, tMedium2.KeyPress
        Dim allowedChars As String = "1234567890" & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub
    Private Sub tLiner2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLiner2.MouseHover
        tLiner2.BackColor = cMouseHover
    End Sub

    Private Sub tLiner2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLiner2.MouseLeave
        tLiner2.BackColor = Drawing.Color.White
    End Sub

    Private Sub tLiner3_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLiner3.MouseHover
        tLiner3.BackColor = cMouseHover
    End Sub

    Private Sub tLiner3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLiner3.MouseLeave
        tLiner3.BackColor = Drawing.Color.White
    End Sub

    Private Sub tMedium1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles tMedium1.MouseHover
        tMedium1.BackColor = cMouseHover
    End Sub

    Private Sub tMedium1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tMedium1.MouseLeave
        tMedium1.BackColor = Drawing.Color.White
    End Sub
    Private Sub tMedium2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles tMedium2.MouseHover
        tMedium2.BackColor = cMouseHover
    End Sub

    Private Sub tMedium2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tMedium2.MouseLeave
        tMedium2.BackColor = Drawing.Color.White
    End Sub

    Private Sub tItemPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tItemPrice.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub tItemPrice_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles tItemPrice.MouseHover
        tItemPrice.BackColor = cMouseHover
    End Sub

    Private Sub tItemPrice_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tItemPrice.MouseLeave
        tItemPrice.BackColor = Drawing.Color.White
    End Sub

    Private Sub cmdCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub ComputeCost()
        Dim L1Value As Decimal
        Dim M1Value As Decimal
        Dim L2Value As Decimal
        Dim M2Value As Decimal
        Dim L3Value As Decimal
        Try
            If CheckEmptyString(cWKL.Text) <> 0 And CheckEmptyString(cBKL.Text) <> 0 And CheckEmptyString(cSCM.Text) <> 0 Then
                If tFlute.Text <> "CB" Then
                    If tFlute.Text = "C" Then
                        tWeightPerPiece.Text = ((tLiner1.Text + (tMedium1.Text * C_CFlute) + tLiner2.Text) / DIV_ppcom) * (tBoardWidthA.Text * tBoardLengthA.Text)
                        'Compute Cost
                        If OuterColor = "WHITE" Then
                            L1Value = cWKL.Text * L1
                            M1Value = cSCM.Text * M1
                            L2Value = cBKL.Text * L2
                            tItemPrice.Text = (L1Value + M1Value + L2Value)
                        Else 'Brown
                            L1Value = cBKL.Text * L1
                            M1Value = cSCM.Text * M1
                            L2Value = cBKL.Text * L2
                            tItemPrice.Text = (L1Value + M1Value + L2Value)
                        End If
                    ElseIf tFlute.Text = "B" Then
                        tWeightPerPiece.Text = ((tLiner1.Text + (tMedium1.Text * CoBFlute) + tLiner2.Text) / DIV_ppcom) * (tBoardWidthA.Text * tBoardLengthA.Text)
                        'Compute Cost
                        If OuterColor = "WHITE" And InnerColor = "WHITE" Then
                            L1Value = cWKL.Text * L1
                            M1Value = cSCM.Text * M1
                            L2Value = cWKL.Text * L2
                            tItemPrice.Text = (L1Value + M1Value + L2Value)
                        ElseIf OuterColor = "WHITE" And InnerColor = "BROWN" Then
                            L1Value = cWKL.Text * L1
                            M1Value = cSCM.Text * M1
                            L2Value = cBKL.Text * L2
                            tItemPrice.Text = (L1Value + M1Value + L2Value)
                        Else ' Brown all, there is no Box that is white inside and brown outside
                            L1Value = cBKL.Text * L1
                            M1Value = cSCM.Text * M1
                            L2Value = cBKL.Text * L2
                            tItemPrice.Text = (L1Value + M1Value + L2Value)
                        End If
                    End If
                Else
                    tWeightPerPiece.Text = ((tLiner1.Text + ((tMedium1.Text * C_CFlute) + (tMedium2.Text * CoBFlute)) + tLiner2.Text) / DIV_ppcom) * (tBoardWidthA.Text * tBoardLengthA.Text)
                    'Compute cost
                    If OuterColor = "WHITE" And InnerColor = "WHITE" Then
                        L1Value = cWKL.Text * L1
                        M1Value = cSCM.Text * M1
                        L2Value = cBKL.Text * L2
                        M2Value = cSCM.Text * M2
                        L3Value = cWKL.Text * L3
                        tItemPrice.Text = (L1Value + M1Value + L2Value + M2Value + L3Value)
                    ElseIf OuterColor = "WHITE" And InnerColor = "BROWN" Then
                        L1Value = cWKL.Text * L1
                        M1Value = cSCM.Text * M1
                        L2Value = cBKL.Text * L2
                        M2Value = cSCM.Text * M2
                        L3Value = cBKL.Text * L3
                        tItemPrice.Text = (L1Value + M1Value + L2Value + M2Value + L3Value)
                    Else ' Brown all
                        L1Value = cBKL.Text * L1
                        M1Value = cSCM.Text * M1
                        L2Value = cBKL.Text * L2
                        M2Value = cSCM.Text * M2
                        L3Value = cBKL.Text * L3
                        tItemPrice.Text = (L1Value + M1Value + L2Value + M2Value + L3Value)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message & " Press OK to continue...", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        If tItemPrice.Text <> "" Then
            Dim DefaultComputation As Decimal = tItemPrice.Text
            tItemPrice.Text = Math.Round(DefaultComputation, 6)
        End If        
    End Sub

    Private Sub cWKL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cWKL.KeyPress, cBKL.KeyPress, cSCM.KeyPress, TextBox2.KeyPress, TextBox1.KeyPress
        Dim allowedChars As String = "1234567890." & ControlChars.Back
        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Dim objSetPrice As ClassPrice = New ClassPrice
        Try
            If CurrentPrice = 0 Then
                If tItemPrice.Text = 0 Then
                    MessageBox.Show("Cannot set the price for this item to zero.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return
                End If
                NewItem = True
                Me.Cursor = Cursors.WaitCursor
                If objSetPrice.SetPricePrintcard(NewItem, PaperCombinationID, PrintcardID, _
                                            CurrencyID, tItemPrice.Text, tLiner1.Text, tMedium1.Text, tLiner2.Text, tMedium2.Text, tLiner3.Text, _
                                            cWKL.Text, cBKL.Text, cSCM.Text, tBoardAreaA.Text, tWeightPerPiece.Text, tPaperWeight.Text) Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Price submission successfull.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    BrowsePrintcard.LoadPrintcards()
                    SearchGridValue(BrowsePrintcard.PrintcardGrid, "printcardid", PrintcardID)
                    Me.Close()
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("ERROR setting Price for this item. Message: " & PriceError, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If
            Else
                Dim UpdatedItemPrice As Decimal = tItemPrice.Text
                tItemPrice.Text = Math.Round(UpdatedItemPrice, 6)
                If tItemPrice.Text <> CurrentPrice Then
                    If tItemPrice.Text = 0 Then
                        MessageBox.Show("Cannot set the price for this item to zero.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Return
                    End If
                    NewItem = False
                    Me.Cursor = Cursors.WaitCursor
                    If objSetPrice.SetPricePrintcard(NewItem, PaperCombinationID, PrintcardID, _
                                                CurrencyID, tItemPrice.Text, tLiner1.Text, tMedium1.Text, tLiner2.Text, tMedium2.Text, tLiner3.Text, _
                                                cWKL.Text, cBKL.Text, cSCM.Text, tBoardAreaA.Text, tWeightPerPiece.Text, tPaperWeight.Text) Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Price submission successfull.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        BrowsePrintcard.LoadPrintcards()
                        SearchGridValue(BrowsePrintcard.PrintcardGrid, "printcardid", PrintcardID)
                        Me.Close()
                    Else
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("ERROR setting Price for this item. Message: " & PriceError, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End If
                Else
                    MessageBox.Show("No price changes was made. Not updating.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objSetPrice = Nothing
        End Try
    End Sub

    Private Sub cWKL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cWKL.TextChanged, cBKL.TextChanged, cSCM.TextChanged, TextBox2.TextChanged, TextBox1.TextChanged
        If CheckEmptyString(cWKL.Text) <> 0 Then
            ComputeCost()
        End If
    End Sub

    Private Sub tLiner1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tLiner1.MouseLeave
        tLiner1.BackColor = Drawing.Color.White
    End Sub

End Class