Imports Npgsql

Module GlobalConst

    Public Printcard_ErrorUpdateSave As String

    Public Const DataBaseHost As String = "192.168.2.4"
    Public Const DatabaseName As String = "tsd_inventory"
    Public Const DatabaseUser As String = "tsd"
    Public Const DatabasePassword As String = "3137Y6s"
    Public Const DatabasePooling As Boolean = True
    Public Const DatabasePort As Integer = 5432
    Public Const ApplicationTitle As String = "SMPC - Technical Services Department Inventory System"
    Public Const AdminRole As Integer = 10
    Public Const SalesRole As Integer = 100
    Public Const GraphicArtistRole As Integer = 20
    'Box formats
    Public Const BOX_RSC As Integer = 10
    Public Const BOX_HSC As Integer = 20
    Public Const BOX_TRAY As Integer = 30

    'Table Constants 
    Public Const FLUTE_CB As Integer = 30
    Public Const FLUTE_C As Integer = 10
    Public Const FLUTE_B As Integer = 20
    Public Const FLUTE_E As Integer = 40
    Public Const PAD As Integer = 50
    Public Const KRAFT_LINER As Integer = 60    
    Public Const FLUTE_OTHER As Integer = 70
    'Box color
    Public Const BROWN_KRAFT As Short = 2
    Public Const WHITE_KRAFT As Short = 4    

    'Rubberdie / Diecut Status 
    Public Const DIE_ACTIVE As Integer = 14
    Public Const DIE_DEVELOPMENT As Integer = 15
    Public Const DIE_UNDER_REPAIR As Integer = 16
    Public Const DIE_BROKEN As Integer = 17
    Public Const DIE_FOR_REPLACEMENT As Integer = 18

    Public Const SAVE_UPDATE_PartitionID As Integer = 90

    'Costing
    Public Const COSTING_CB_FluteFactor As Single = 1.47
    Public Const C_CFlute As Single = 1.47
    Public Const CoBFlute As Single = 1.37
    Public Const mm_in_meters As Single = 0.001
    Public Const DIV_ppcom As Integer = 1000
    Public Const COST_SCM As Decimal = 38.0
    Public Const COST_BKL As Decimal = 38.62
    Public Const COST_WKL As Decimal = 41.66

    ''The Database
    'Public Const DATABASE_boxformat As Integer = 10
    'Public Const DATABASE_cities As Integer = 12
    'Public Const DATABASE_contact As Integer = 14
    'Public Const DATABASE_country As Integer = 16
    'Public Const DATABASE_currency As Integer = 18
    'Public Const DATABASE_customer As Integer = 20
    'Public Const DATABASE_customerfile As Integer = 22
    'Public Const DATABASE_department As Integer = 24
    'Public Const DATABASE_diecut As Integer = 26
    'Public Const DATABASE_domains As Integer = 28
    'Public Const DATABASE_factor As Integer = 30
    'Public Const DATABASE_flute As Integer = 32
    'Public Const DATABASE_generic_status As Integer = 34
    'Public Const DATABASE_generic_status_type As Integer = 36
    'Public Const DATABASE_graphicfiles As Integer = 38
    'Public Const DATABASE_industry As Integer = 40
    'Public Const DATABASE_international_description As Integer = 42
    'Public Const DATABASE_joint As Integer = 44
    'Public Const DATABASE_language As Integer = 46
    'Public Const DATABASE_paper_combination As Integer = 48
    'Public Const DATABASE_paper_dimension As Integer = 50
    'Public Const DATABASE_plate As Integer = 52
    'Public Const DATABASE_print_code As Integer = 54
    Public Const DATABASE_printcard As Integer = 56
    'Public Const DATABASE_printcard_series As Integer = 58
    'Public Const DATABASE_provinces As Integer = 60
    'Public Const DATABASE_racks As Integer = 62
    'Public Const DATABASE_roles As Integer = 64
    'Public Const DATABASE_scale As Integer = 66
    'Public Const DATABASE_schedule As Integer = 68
    'Public Const DATABASE_systemusers As Integer = 70
    'Public Const DATABASE_test As Integer = 72
    'Public Const DATABASE_unit_table As Integer = 74
    Public Const DATABASE_rubberdie As Integer = 76
    'Public Const DATABASE_rubberdie_racks As Integer = 78
    'Public Const DATABASE_transaction_history As Integer = 80

    'Rubberdie Update
    Public Const VAR_RUBBERDIE_MOUNT As Integer = 30
    Public Const VAR_RUBBERDIE_REPAIR As Integer = 31
    Public Const VAR_RUBBERDIE_MOVE As Integer = 32
    Public Const VAR_RUBBERDIE_STATUS As Integer = 33
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
    Dim sql As String

    Public PriceError As String

    Dim cmd As NpgsqlCommand
    Dim dr As NpgsqlDataReader
    Public Function GetDisplayHint(ByVal TABLE_ID As Integer) As Boolean
        Dim iRes As Boolean = False
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT hint FROM tsd_table WHERE id=" & TABLE_ID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes
    End Function
    Public Sub SetDisplayHint(ByVal TABLE_ID As Integer, ByVal BooleanValue As Boolean)
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "UPDATE tsd_table SET hint=" & BooleanValue & " WHERE id=" & TABLE_ID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        cmd.ExecuteNonQuery()

        conn.Close()
        conn.ClearPool()
    End Sub

    Public Function GetCurrencyCodeStr(ByVal CustomerID As Integer, ByVal CurrencyColumn As String) As String
        Dim iRes As String = ""
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & CurrencyColumn & " FROM currency WHERE id= " & _
              " (SELECT currency_id FROM customer WHERE contact_id=" & CustomerID & ");"

        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function

    Public Function GetCurrencyCodeInt(ByVal CustomerID As Integer) As Integer
        Dim iRes As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT currency_id FROM customer WHERE contact_id=" & CustomerID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function

    Public Function GetTableNextID(ByVal TableName As String) As Integer
        Dim iRes As Integer
        Dim dr As NpgsqlDataReader

        If conn.State = ConnectionState.Closed Then conn.Open()

        sql = "SELECT max(id)+1 FROM " & TableName & ";"

        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While (dr.Read)
            If IsDBNull(dr.GetValue(0)) = False Then
                iRes = dr.GetValue(0) + 1
            Else
                iRes = 1
            End If
        End While

        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function

    Public Function IsPrintcardValidForEdit(ByVal PrintcardID As Integer) As Boolean
        Dim iRes As Boolean
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT is_price_set FROM printcard WHERE id=" & PrintcardID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes
    End Function
    Public Function GetPrintcardInfoPrice(ByVal TableName As String, ByVal SelectColumn As String, ByVal WhereColumn As String, ByVal PrintcardID As Integer) As Decimal
        Dim iRes As Decimal
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & SelectColumn & " FROM " & TableName & " WHERE " & WhereColumn & "=" & PrintcardID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes
    End Function
    Public Function GetTableValueInt(ByVal TableName As String, ByVal ColumnName As String, ByVal TableID As Integer) As Integer

        Dim iRes As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & ColumnName & " FROM " & TableName & " WHERE id=" & TableID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes

    End Function
    Public Function GetPrintcardStatusStr(ByVal PrintcardStatusID As Integer) As String
        Dim iRes As String = ""
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = " SELECT content FROM international_description" & _
            " WHERE international_description.foreign_id=" & _
            " (SELECT status_value FROM generic_status WHERE dtype='printcard_status'" & _
            " AND id=" & PrintcardStatusID & ") and international_description.table_id=" & DATABASE_printcard & ";"

        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function
    Public Function GetTableValueIntStr(ByVal TableName As String, ByVal ColumnName As String, ByVal CompareString As String) As Integer

        Dim iRes As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & ColumnName & " FROM " & TableName & " WHERE description='" & CompareString & "';"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes

    End Function
    Public Function GetTableValueIntColumnCompare(ByVal TableName As String, ByVal SelectColumnName As String, ByVal WhereColumn As String, ByVal CompareString As String) As Integer

        Dim iRes As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & SelectColumnName & " FROM " & TableName & " WHERE " & WhereColumn & "='" & CompareString & "';"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes

    End Function
    Public Function GetTableNextIDIncrementBy(ByVal TableName As String, ByVal IncrementBy As Integer) As Integer
        Dim iRes As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM " & TableName & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0) + IncrementBy
        End While
        conn.Close()
        conn.ClearPool()
        Return iRes
    End Function
    Public Function SearchGridValue(ByVal dtg As DataGridView, ByVal ColumnName As String, ByVal ValueToSearch As String) As Boolean
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
                For Each gRow As DataGridViewRow In dtg.Rows
                    StringToSearch = gRow.Cells(ColumnName).Value.ToString.Trim.ToLower
                    If StringToSearch.Contains(ValueToSearchFor) Then
                        Dim myCurrentCell As DataGridViewCell = gRow.Cells(ColumnName)
                        dtg.CurrentCell = myCurrentCell
                        Found = True
                    End If
                    If Found Then
                        Exit For
                    End If
                Next
                If Not Found Then
                    If dtg.Rows.Count > 0 Then
                        Dim myFirstCurrentCell As DataGridViewCell = dtg.Rows(0).Cells(ColumnName)
                        dtg.CurrentCell = myFirstCurrentCell
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Information)
        End Try
        Return Found
    End Function

    Public Function GetTableValueStr(ByVal TableName As String, ByVal ColumnName As String, ByVal TableID As Integer) As String

        Dim iRes As String = ""
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & ColumnName & " FROM " & TableName & " WHERE id=" & TableID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            iRes = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return iRes

    End Function
End Module
