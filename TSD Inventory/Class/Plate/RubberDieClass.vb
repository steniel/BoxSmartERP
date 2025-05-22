Imports Npgsql

Public Class RubberDieClass
    Public conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port=" & DatabasePort)
    Dim sql As String
    Dim cmd As NpgsqlCommand
    Dim dr As NpgsqlDataReader
    Public Sub PopulateRubberdie(ByVal DiecutGrid As DataGridView, ByVal LimitResult As Integer)
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        Try
            sql = "SELECT " & _
                    " rubberdie.id as rubber_id, " & _
                    " contact.organization_name, " & _
                    " rubberdie.description, " & _
                    " international_description.content as status, " & _
                    " rubberdie.rubberdie_number as die_number, " & _
                    " rubberdie.rubberdie_string_num as rubberdie_string_num, " & _
                    " rubberdie.ageing, " & _
                    " rubberdie.boxes_count, " & _
                    " rubberdie_racks.rack_number || ''|| rubberdie_racks.rack_column as rack, " & _
                    " rubberdie_racks.id as rubberdie_racks_id," & _
                    " RubberDie.date_created," & _
                    " RubberDie.date_mounted," & _
                    " RubberDie.date_repair" & _
                    " FROM RubberDie " & _
                    " INNER JOIN rubberdie_racks ON rubberdie.rack_id=rubberdie_racks.id   " & _
                    " INNER JOIN contact ON rubberdie.customer_id = contact.id  " & _
                    " INNER JOIN generic_status ON generic_status.id = rubberdie.status_id " & _
                    " INNER JOIN international_description ON " & _
                    " (international_description.table_id = " & DATABASE_rubberdie & " And " & _
                    " international_description.foreign_id = generic_status.status_value ) " & _
                    " WHERE rubberdie.rubberdie_number <> 0 LIMIT " & LimitResult & ";"

            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "diecut")
            DiecutGrid.DataSource = ds.Tables("diecut")
        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & ". Error in PopulateRubberdie @ RubberDieClass")
        End Try
    End Sub
    'Get Diecut ID
    Public Function GetCustomerID(ByVal CustomerName As String) As Integer                
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        Sql = "SELECT id FROM contact WHERE UPPER(organization_name)='" & UCase(CustomerName) & "';"
        cmd = New NpgsqlCommand(Sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    Public Sub GetDieStatusValue(ByVal ComboBox As ComboBox)
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        sql = "SELECT name FROM status_table WHERE table_id=" & DATABASE_rubberdie & ";"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "status_table")
        ComboBox.DataSource = ds.Tables("status_table")
        ComboBox.DisplayMember = "name"
        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    Public Sub GetCustomer(ByVal ComboBox As ComboBox)
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        sql = "select contact.organization_name as org_name FROM contact Inner join customer ON (customer.contact_id = contact.id) WHERE customer.current_status='Active';"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "contact")
        ComboBox.DataSource = ds.Tables("contact")
        ComboBox.DisplayMember = "org_name"
        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    Public Function GetNewRackLocation() As Integer
        Dim id As Integer        
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT max(rack_number)+1 FROM racks;"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    Public Sub GetRackListLimit(ByVal ComboBox As ComboBox, ByVal Order As String)
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        sql = "SELECT (rack_number || '' || rack_column) as rack FROM rubberdie_racks WHERE rack_number <> 0 AND storage_count < storage_capacity ORDER BY rubberdie_racks.rack_number " & Order & ";"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "rubberdie_racks")
        ComboBox.DataSource = ds.Tables("rubberdie_racks")
        ComboBox.DisplayMember = "rack"
        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    Public Sub GetRackList(ByVal ComboBox As ComboBox, ByVal Order As String)
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        sql = "SELECT (rack_number || '' || rack_column) as rack FROM rubberdie_racks WHERE rack_number <> 0 ORDER BY rubberdie_racks.rack_number " & Order & ";"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "rubberdie_racks")
        ComboBox.DataSource = ds.Tables("rubberdie_racks")
        ComboBox.DisplayMember = "rack"
        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    Public Function GetRackID(ByVal racklocation As String) As Integer
        Dim id As Integer        
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM rubberdie_racks WHERE (rubberdie_racks.rack_number || ''|| UPPER(rubberdie_racks.rack_column))='" & UCase(racklocation) & "';"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    Private Function cGetNewTableID(ByVal TableName As String) As Integer
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT max(id) FROM " & TableName & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            If IsDBNull(dr.GetValue(0)) = False Then
                id = dr.GetValue(0) + 1
            Else
                id = 1
            End If
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    Function CheckExistingRubberdie(ByVal RubberdieNumber As Integer, ByVal CustomerID As Integer) As Boolean
        Dim ires As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()

        sql = "SELECT rubberdie_number FROM rubberdie WHERE rubberdie_number=" & RubberdieNumber & " AND customer_id= " & CustomerID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            ires = dr.GetValue(0)
        End While

        If ires > 0 Then
            Return True
        Else
            Return False
        End If

        conn.Close()
        conn.ClearPool()
    End Function
    Function GetLocation(ByVal rack_number As Integer) As String
        Dim RackLocation As String = ""
        sql = "SELECT (rack_number || '' || UPPER(rack_column)) FROM racks WHERE id=(SELECT rack_id FROM diecut WHERE diecut_number=" & rack_number & ");"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            RackLocation = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()
        Return RackLocation

    End Function
    Public Function AddRubberdie(ByVal ADD_RUBBERDIE_RubberdieID As Integer, _
                                   ByVal ADD_RUBBERDIE_Description As String, _
                                   ByVal ADD_RUBBERDIE_Customer_ID As Integer, _
                                   ByVal ADD_RUBBERDIE_RubberdieNumber As Integer, _
                                   ByVal ADD_RUBBERDIE_Ageing As Integer, _
                                   ByVal ADD_RUBBERDIE_Boxes_count As Integer, _
                                   ByVal ADD_RUBBERDIE_RackLoc As String, _
                                   ByVal ADD_RUBBERDIE_Status_ID As Integer, _
                                   ByVal ADD_RUBBERDIE_Remarks As String, _
                                   ByVal ADD_RUBBERDIE_RubberdieNumberStr As String, _
                                   ByVal UPDATE_Printcard_Rubberdie_ID As Integer) As Boolean
        Dim ADD_RUBBERDIE_RackID As Integer = GetRackID(ADD_RUBBERDIE_RackLoc)
        Dim ADD_RUBBERDIE_ID As Integer = cGetNewTableID("rubberdie")
        Dim ADD_RUBBERDIE_SYSTEM_USER_ID As Integer = LoginTSD.SystemUserID
        Dim ReturnVal As Boolean = False
        Using connTransaction As NpgsqlConnection = New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
            Try
                If connTransaction.State = ConnectionState.Closed Then connTransaction.Open()
                Using pgTransaction As NpgsqlTransaction = connTransaction.BeginTransaction
                    Try
                        Using SAVE_Rubberdie As NpgsqlCommand = New NpgsqlCommand("INSERT INTO rubberdie(" & _
                                                                                       " id, description, rubberdie_number, ageing, boxes_count, customer_id, rack_id, status_id, remarks, date_created, created_by, rubberdie_string_num) " & _
                                                                                       " VALUES(@id, @description, @rubberdie_number, @ageing, @boxes_count, @customer_id, @rack_id, @status_id, @remarks, @date_created, @created_by, @rubberdie_string_num);", connTransaction, pgTransaction)
                            SAVE_Rubberdie.Parameters.AddWithValue("@id", ADD_RUBBERDIE_ID)
                            SAVE_Rubberdie.Parameters.AddWithValue("@description", ADD_RUBBERDIE_Description)
                            SAVE_Rubberdie.Parameters.AddWithValue("@rubberdie_number", ADD_RUBBERDIE_RubberdieNumber)
                            SAVE_Rubberdie.Parameters.AddWithValue("@ageing", ADD_RUBBERDIE_Ageing)
                            SAVE_Rubberdie.Parameters.AddWithValue("@boxes_count", ADD_RUBBERDIE_Boxes_count)
                            SAVE_Rubberdie.Parameters.AddWithValue("@customer_id", ADD_RUBBERDIE_Customer_ID)
                            SAVE_Rubberdie.Parameters.AddWithValue("@rack_id", ADD_RUBBERDIE_RackID)
                            SAVE_Rubberdie.Parameters.AddWithValue("@status_id", ADD_RUBBERDIE_Status_ID)
                            SAVE_Rubberdie.Parameters.AddWithValue("@remarks", ADD_RUBBERDIE_Remarks)
                            SAVE_Rubberdie.Parameters.AddWithValue("@date_created", Now())
                            SAVE_Rubberdie.Parameters.AddWithValue("@created_by", ADD_RUBBERDIE_SYSTEM_USER_ID)
                            SAVE_Rubberdie.Parameters.AddWithValue("@rubberdie_string_num", ADD_RUBBERDIE_RubberdieNumberStr)
                            SAVE_Rubberdie.ExecuteNonQuery()

                        End Using

                        Using SAVE_RUBBERDIE_ID As NpgsqlCommand = New NpgsqlCommand("UPDATE printcard SET rubberdie_id=@rubberdie_id WHERE id= " & UPDATE_Printcard_Rubberdie_ID & ";", connTransaction, pgTransaction)
                            SAVE_RUBBERDIE_ID.Parameters.AddWithValue("@rubberdie_id", ADD_RUBBERDIE_ID)
                            SAVE_RUBBERDIE_ID.ExecuteNonQuery()
                        End Using
                        pgTransaction.Commit()
                        pgTransaction.Dispose()
                        connTransaction.Close()
                        connTransaction.ClearPool()
                        UpdateRackLocationStorageCount(ADD_RUBBERDIE_RackID)
                        ReturnVal = True
                    Catch ex As Exception
                        pgTransaction.Rollback()
                        Throw
                    End Try
                End Using
            Catch ex As NpgsqlException
                MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Catch ex As Exception
                MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Throw
            End Try

        End Using

        Return ReturnVal
    End Function

    Function GetLastRackNum() As Integer
        Dim NewRackNumber As Integer        
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT max(rack_number)+1 FROM rubberdie_racks;"
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                NewRackNumber = dr.GetValue(0)
            End While

        Catch ex As Exception
            MsgBox(ex.Message & "! Error in GetLastRackNum, AddRacks.vb")
        End Try

        conn.Close()
        conn.ClearPool()

        Return NewRackNumber
    End Function
    Function CheckExistingRacknumberColumn(ByVal RackNumber As Integer, ByVal RackColumn As Char) As Boolean
        Dim ires As Integer        
        If conn.State = ConnectionState.Closed Then conn.Open()

        sql = "SELECT count(*) FROM rubberdie_racks WHERE rack_number=" & RackNumber & " AND rack_column='" & RackColumn & "'"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            ires = dr.GetValue(0)
        End While

        If ires > 0 Then
            Return True
        Else
            Return False
        End If

        conn.Close()
        conn.ClearPool()

    End Function
    Function CheckLaktaw(ByVal RackNumber As Integer) As Integer        
        Dim ires As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT max(rack_number) FROM rubberdie_racks"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            ires = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return ires
    End Function
    Function NewRackNumber(ByVal RackNumber As Integer, ByVal RackColumn As Char, ByVal LocCapacity As Integer) As Boolean
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "INSERT INTO rubberdie_racks(rack_number,rack_column,storage_capacity,date_created) " & _
                          "VALUES(@rack_number,@rack_column,@storage_capacity,@date_created)"
            cmd = New NpgsqlCommand(sql, conn)

            'Add the values
            cmd.Parameters.Add(New NpgsqlParameter("@rack_number", RackNumber))
            cmd.Parameters.Add(New NpgsqlParameter("@rack_column", RackColumn))
            cmd.Parameters.Add(New NpgsqlParameter("@storage_capacity", LocCapacity))
            cmd.Parameters.Add(New NpgsqlParameter("@date_created", Now()))

            'Execute the Query
            cmd.ExecuteNonQuery()

            'Close connection
            conn.Close()
            conn.ClearPool()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub UpdateRackLocation(ByVal RackNumber As Integer, ByVal ComboBox1 As ComboBox)
        Dim CurrentColumn As Char
        Dim iRes As Integer
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT count(*) FROM rubberdie_racks WHERE rack_number=" & RackNumber
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                iRes = dr.GetValue(0)
            End While

            If iRes > 0 Then
                sql = "SELECT rack_column FROM rubberdie_racks WHERE rack_number=" & RackNumber
                cmd = New NpgsqlCommand(sql, conn)
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                While dr.Read
                    CurrentColumn = dr.GetValue(0)
                End While
                Select Case CurrentColumn
                    Case "A"
                        ComboBox1.Items.Clear()
                        ComboBox1.Items.Add("B")
                        ComboBox1.Text = "B"
                    Case "B"
                        ComboBox1.Items.Clear()
                        ComboBox1.Items.Add("C")
                        ComboBox1.Text = "C"
                    Case "C"
                        ComboBox1.Items.Clear()
                        ComboBox1.Items.Add("D")
                        ComboBox1.Text = "D"
                    Case "D"
                        ComboBox1.Items.Clear()
                        ComboBox1.Items.Add("E")
                        ComboBox1.Text = "E"
                    Case "E"
                        ComboBox1.Items.Clear()
                        ComboBox1.Items.Add("F")
                        ComboBox1.Text = "F"
                    Case Else
                        MsgBox("Rack columns must be from A to F only!", MsgBoxStyle.Exclamation, ApplicationTitle)
                End Select
            Else
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("A")
                ComboBox1.Text = "A"
            End If

        Catch ex As Exception
            MsgBox(ex.Message & "! Error in GetLastRackNum, AddRacks.vb")
        End Try

        conn.Close()
        conn.ClearPool()
    End Sub
    Private Function GetStringValue(ByVal Fieldname As String, ByVal gRubberdieID As Integer) As String
        Dim iRes As String = ""
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & Fieldname & " FROM rubberdie WHERE id=" & gRubberdieID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            iRes = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()
        Return iRes
    End Function
    Private Function GetIntValue(ByVal Fieldname As String, ByVal gRubberdieID As Integer) As Integer
        Dim iRes As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & Fieldname & " FROM rubberdie WHERE id=" & gRubberdieID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            iRes = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()
        Return iRes
    End Function
    Public Sub UpdateRackLocationStorageCount(ByVal Rack_Location_ID As Integer)
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "UPDATE rubberdie_racks SET storage_count=(SELECT count(*) FROM rubberdie WHERE rack_id=" & Rack_Location_ID & ") WHERE id=" & Rack_Location_ID & ";"

        'Initialize SqlCommand object for update. 
        cmd = New NpgsqlCommand(sql, conn)

        cmd.ExecuteNonQuery()

        conn.Close()
        conn.ClearPool()

    End Sub
    Public Sub UpdateRubberdieItem(ByVal FieldName As String, ByVal Update_RubberdieID As Integer, ByVal TransactionTYPE As Integer, Optional ByVal ValueString As String = "", Optional ByVal ValueInt As Integer = 0)
        Using connTransaction As NpgsqlConnection = New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
            Try
                If connTransaction.State = ConnectionState.Closed Then connTransaction.Open()
                Using pgTransaction As NpgsqlTransaction = connTransaction.BeginTransaction
                    Try

                        If ValueString <> "" Then 'If ValueString != "" then ValueInt must be 0 (String var)
                            sql = "UPDATE rubberdie SET " & FieldName & "='" & ValueString & "' WHERE id=" & Update_RubberdieID & ";"
                        Else ' Then ValueInt value must be set (Integer var)
                            sql = "UPDATE rubberdie SET " & FieldName & "=" & ValueInt & " WHERE id=" & Update_RubberdieID & ";"
                        End If

                        Using UpdateRubberdieCMD As NpgsqlCommand = New NpgsqlCommand(sql, connTransaction, pgTransaction)
                            UpdateRubberdieCMD.ExecuteNonQuery()
                        End Using

                        Dim INSERT_TRANS_HISTORY_NEWID As Integer = cGetNewTableID("transaction_history")                    
                        Dim INSERT_TRANS_HISTORY_UserID As Integer = GetIntValue("created_by", Update_RubberdieID)                        
                        Dim INSERT_TRANS_HISTORY_RUBBERDIE_RACKS_ID As Integer = GetIntValue("rack_id", Update_RubberdieID)

                        Using TriggerUpdateTransactionCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO transaction_history(id, transaction_type_id, user_id, rubberdie_id, rubberdie_racks_id, transaction_date) " & _
                                                                                               " SELECT @id, @transaction_type_id, @user_id, @rubberdie_id, @rubberdie_racks_id, @transaction_date;", connTransaction, pgTransaction)
                            TriggerUpdateTransactionCMD.Parameters.AddWithValue("@id", INSERT_TRANS_HISTORY_NEWID)
                            TriggerUpdateTransactionCMD.Parameters.AddWithValue("@transaction_type_id", TransactionTYPE)
                            TriggerUpdateTransactionCMD.Parameters.AddWithValue("@user_id", INSERT_TRANS_HISTORY_UserID)
                            TriggerUpdateTransactionCMD.Parameters.AddWithValue("@rubberdie_id", Update_RubberdieID)
                            TriggerUpdateTransactionCMD.Parameters.AddWithValue("@rubberdie_racks_id", INSERT_TRANS_HISTORY_RUBBERDIE_RACKS_ID)
                            TriggerUpdateTransactionCMD.Parameters.AddWithValue("@transaction_date", Now())
                            TriggerUpdateTransactionCMD.ExecuteNonQuery()
                        End Using

                        pgTransaction.Commit()
                        pgTransaction.Dispose()
                        connTransaction.Close()
                        connTransaction.ClearPool()

                    Catch ex As Exception
                        pgTransaction.Rollback()
                        Throw
                    End Try
                End Using
            Catch ex As NpgsqlException
                MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Catch ex As Exception
                MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Throw
            End Try
        End Using

    End Sub
    Public Sub GetCustomerRubberdieList(ByVal CustRubberdieGrid As DataGridView, ByVal LimitResult As Integer)
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        Try
            sql = "SELECT printcard.id," & _
                       " contact.organization_name," & _
                       " printcard.box_description," & _
                    " (customer.printcard_prefix ||  " & _
                     " CASE char_length(trim(to_char(printcardno,'999999'))) WHEN 1 Then '-PC-000'  " & _
                     " WHEN 2 Then '-PC-00'  " & _
                          "  WHEN 3 Then '-PC-0'  " & _
                     " ELSE '-PC-'  " & _
                     " END || printcard.printcardno) as printcardno, printcard.printcardno as printnum, " & _
                    " trim(to_char(printcard.boardwidth,'999999')) || ' X ' || " & _
                    " trim(to_char(printcard.boardlength,'999999')) as boardsize," & _
                    " (trim(to_char(paper_dimension.length,'999999')) || ' X ' || " & _
                    " trim(to_char(paper_dimension.width,'99999')) || ' X ' || " & _
                    " trim(to_char(paper_dimension.height,'99999'))) as insidedimension " & _
                    " FROM ((printcard  " & _
                    " INNER JOIN customer ON printcard.customer_id=customer.id) " & _
                    " INNER JOIN contact ON customer.contact_id=contact.id " & _
                    " INNER JOIN paper_dimension ON printcard.dimension_id=paper_dimension.id) " & _
                    " WHERE rubberdie_id=1 ORDER BY printcard.id LIMIT " & LimitResult & ";"

            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "printcard")
            CustRubberdieGrid.DataSource = ds.Tables("printcard")

        Catch ex As Exception            
            MsgBox(ex.Message & ". Error in GetCustomerRubberdieList @ RubberDieClass")
        End Try
    End Sub
End Class
