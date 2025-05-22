Imports Npgsql
Public Class ClassPrice
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)    
    Dim sql As String
    Dim cmd As NpgsqlCommand
    Public Function SetPricePrintcard(ByVal NewItem As Boolean, ByVal PaperCombID As Integer, ByVal PrintcardID As Integer, ByVal CurrencyID As Integer, ByVal ItemPrice As Decimal,
                                 ByVal Liner1 As Integer, ByVal Medium1 As Integer, ByVal Liner2 As Integer, ByVal Medium2 As Integer, ByVal Liner3 As Integer,
                                 ByVal wkl As Decimal, ByVal bkl As Decimal, ByVal scm As Decimal,
                                 ByVal area As Decimal, ByVal weight_piece As Decimal, ByVal weight_gms_sqm As Decimal) As Boolean
        Dim ReturnVal As Boolean = False
        Dim pcUserID As Integer = LoginTSD.SystemUserID

        Using connTransaction As NpgsqlConnection = New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
            Try
                If connTransaction.State = ConnectionState.Closed Then connTransaction.Open()
                Using pgTransaction As NpgsqlTransaction = connTransaction.BeginTransaction
                    Try
                        'Update paper combination
                        Using UpdatePaperCombinationCMD As NpgsqlCommand = New NpgsqlCommand("UPDATE paper_combination " & _
                                                                                             " SET liner1=@liner1, medium1=@medium1, liner2=@liner2, medium2=@medium2, liner3=@liner3, update_datetime=@update_datetime " & _
                                                                                             " WHERE id=@id;", connTransaction, pgTransaction)
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@liner1", Liner1)
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@medium1", Medium1)
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@liner2", Liner2)
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@medium2", Medium2)
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@liner3", Liner3)
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@update_datetime", Now())
                            UpdatePaperCombinationCMD.Parameters.AddWithValue("@id", PaperCombID)
                            UpdatePaperCombinationCMD.ExecuteNonQuery()
                        End Using

                        'Update is_price_set to TRUE
                        Using UpdatePrintcardPriceSetCMD As NpgsqlCommand = New NpgsqlCommand("UPDATE printcard SET is_price_set=@is_price_set WHERE id=@id;", connTransaction, pgTransaction)
                            UpdatePrintcardPriceSetCMD.Parameters.AddWithValue("@id", PrintcardID)
                            UpdatePrintcardPriceSetCMD.Parameters.AddWithValue("@is_price_set", True)
                            UpdatePrintcardPriceSetCMD.ExecuteNonQuery()
                        End Using

                        If NewItem = True Then
                            Dim NextItemPriceId As Integer = GetTableNextID("item_price")
                            sql = "INSERT INTO item_price(id, printcard_id, currency_id, price, wkl, bkl, scm, area, weight_piece, " & _
                                  " weight_gms_sqm, date_created, created_by) SELECT @id,@printcard_id,@currency_id,@price,@wkl,@bkl,@scm,@area,@weight_piece,@weight_gms_sqm,@date_created,@created_by;"
                            Using UpdateInsertPrintcardPriceCMD As NpgsqlCommand = New NpgsqlCommand(sql, connTransaction, pgTransaction)

                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@id", NextItemPriceId)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@printcard_id", PrintcardID)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@currency_id", CurrencyID)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@price", ItemPrice)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@wkl", wkl)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@bkl", bkl)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@scm", scm)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@area", area)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@weight_piece", weight_piece)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@weight_gms_sqm", weight_gms_sqm)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@date_created", Now())
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@created_by", pcUserID)
                                UpdateInsertPrintcardPriceCMD.ExecuteNonQuery()
                            End Using
                        Else
                            sql = "UPDATE item_price SET price=@price,wkl=@wkl,bkl=@bkl,scm=@scm,date_updated=@date_updated, updated_by=@updated_by WHERE printcard_id=@printcard_id;"
                            Using UpdateInsertPrintcardPriceCMD As NpgsqlCommand = New NpgsqlCommand(sql, connTransaction, pgTransaction)

                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@price", ItemPrice)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@wkl", wkl)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@bkl", bkl)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@scm", scm)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@date_updated", Now())
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@updated_by", pcUserID)
                                UpdateInsertPrintcardPriceCMD.Parameters.AddWithValue("@printcard_id", PrintcardID)
                                UpdateInsertPrintcardPriceCMD.ExecuteNonQuery()
                            End Using
                        End If

                        pgTransaction.Commit()
                        pgTransaction.Dispose()
                        connTransaction.Close()
                        connTransaction.ClearPool()
                        ReturnVal = True
                    Catch ex As Exception
                        pgTransaction.Rollback()
                        Throw
                    End Try
                End Using
            Catch ex As Exception
                PriceError = ex.Message
            End Try
        End Using

        Return ReturnVal

    End Function
End Class
