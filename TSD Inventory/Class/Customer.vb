Imports Npgsql
Public Class Customer
    Dim SQLConn As DataConnection = New DataConnection
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port=" & DatabasePort)
    Dim sql As String
    Dim cmd As NpgsqlCommand
    Public da As NpgsqlDataAdapter
    Public ds As New DataSet
    Public Function GetBoxInfo(ByVal CustomerID As Integer, ByVal PrintcardNumber As Integer, ByVal ColumnName As String) As Double
        Dim dr As NpgsqlDataReader
        Dim iRes As Double
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT " & ColumnName & " FROM printcard WHERE customer_id=" & CustomerID & " AND printcardno=" & PrintcardNumber
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (dr.Read)
                If (dr.GetValue(0)) Is Nothing Then
                    iRes = 0
                ElseIf IsDBNull(dr.GetValue(0)) = False Then
                    iRes = dr.GetValue(0)
                End If
            End While
            conn.Close()
            conn.ClearPool()

        Catch ex As Npgsql.NpgsqlException
            MsgBox(ex.Message)
        End Try

        Return iRes
    End Function
    Public Sub GetCustomerList(ByVal TableGrid As DataGridView)

        Try

            sql = "SELECT contact.id as id, contact.organization_name as orgname FROM (contact INNER JOIN customer ON customer.contact_id=contact.id) " & _
                 " WHERE contact.deleted=0 AND customer.current_status='Active' ORDER BY contact.organization_name ASC;"

            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "customer")
            TableGrid.DataSource = ds.Tables("customer")

        Catch ex As Npgsql.NpgsqlException
            MsgBox(ex.Message)
        Catch ex As ApplicationException
            MsgBox(ex.Message)
        End Try
        conn.Close()
        conn.ClearPool()

    End Sub
    Public Sub SearchCustomerKeyword(ByVal TableGrid As DataGridView, ByVal SearchString As String)
        Try
            sql = "SELECT contact.id as id, contact.organization_name as orgname FROM (contact INNER JOIN customer ON customer.contact_id=contact.id) " & _
                 " WHERE contact.deleted=0 AND customer.current_status='Active' AND UPPER(contact.organization_name) like '%" & UCase(SearchString) & "%' ORDER BY contact.organization_name ASC;"

            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "customer")
            TableGrid.DataSource = ds.Tables("customer")

        Catch ex As Npgsql.NpgsqlException
            MsgBox(ex.Message)
        Catch ex As ApplicationException
            MsgBox(ex.Message)
        End Try
        conn.Close()
        conn.ClearPool()

    End Sub
    'Select Board type
    Public Sub GetBoardType(ByVal ComboBox As ComboBox)

        sql = "select (flute || '-') || description as boardtype FROM flute"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "flute")
        ComboBox.DataSource = ds.Tables("flute")
        ComboBox.DisplayMember = "boardtype"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()

    End Sub
    'Select Board type ID
    Public Function GetBoardTypeID(ByVal value As String) As Integer
        Dim dr As NpgsqlDataReader
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM flute WHERE (flute || '-') || description='" & value & "'"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    'GET PSI Data from table
    Public Sub GetPSI(ByVal ComboBox As ComboBox)

        sql = "select value FROM test"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "test")
        ComboBox.DataSource = ds.Tables("test")
        ComboBox.DisplayMember = "value"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    'Get Diecut list
    Public Sub GetDiecut(ByVal ComboBox As ComboBox)
        Dim sql As String
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        ds.Clear()
        sql = "SELECT CASE (diecut_number) WHEN 0 THEN 0 ELSE diecut_number END as diecut_number FROM diecut WHERE deleted=0;"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "diecut")
        ComboBox.DataSource = ds.Tables("diecut")
        ComboBox.DisplayMember = "diecut_number"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    'Get JOINT types
    Public Sub GetJoint(ByVal ComboBox As ComboBox)
        sql = "select description FROM joint;"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "joint")
        ComboBox.DataSource = ds.Tables("joint")
        ComboBox.DisplayMember = "description"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    Public Sub GetTestType(ByVal ComboBox As ComboBox)
        sql = "select code FROM test_type;"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "test_type")
        ComboBox.DataSource = ds.Tables("test_type")
        ComboBox.DisplayMember = "code"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub

    Public Sub GetPrintcardStatus(ByVal ComboBox As ComboBox)
        sql = " SELECT international_description.content as printcard_status " & _
               " FROM  international_description international_description INNER JOIN tsd_table tsd_table ON international_description.table_id=tsd_table.id " & _
                " WHERE(tsd_table.id = " & DATABASE_printcard & ");"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "international_description")
        ComboBox.DataSource = ds.Tables("international_description")
        ComboBox.DisplayMember = "printcard_status"
        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub

    Public Function GetJointID(ByVal Description As String) As Integer
        Dim dr As NpgsqlDataReader
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM joint WHERE description='" & Description & "'"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function

    'Get Diecut ID
    Public Function GetDiecutID(ByVal DiecutNumber As Integer) As Integer
        Dim id As Integer
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM diecut WHERE diecut_number=" & DiecutNumber & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    'Get PSI test ID
    Public Function GetTestID(ByVal value As Integer) As Integer
        Dim dr As NpgsqlDataReader
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM test WHERE value=" & value & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While      
        conn.Close()
        conn.ClearPool()
        Return id

    End Function
    Private Function GetCBTypeCombination(ByVal Flute As String) As Integer
        Dim cCount As Integer
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        If Flute = "CB-Flute" Then
            sql = "SELECT count(*) FROM paper_combination WHERE medium1 IS NOT NULL " & _
                      "AND medium2 IS NOT NULL " & _
                      "AND liner3 IS NOT NULL "
        Else
            sql = "SELECT count(*) FROM paper_combination WHERE medium1 IS NOT NULL "
        End If

        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            cCount = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return cCount
        MsgBox(cCount)
    End Function
    Public Sub GetPaperCombination(ByVal DataGrid As DataGridView, ByVal FluteType As String)      
        Try
            Select Case UCase(FluteType)
                Case "CB-FLUTE"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & FLUTE_CB & " ORDER by id ASC;"
                Case "C-FLUTE"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & FLUTE_C & " ORDER by id ASC;"
                Case "B-FLUTE"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & FLUTE_B & " ORDER by id ASC;"
                Case "E-FLUTE"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & FLUTE_E & " ORDER by id ASC;"
                Case "*-PAD"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & PAD & " ORDER by id ASC;"
                Case "*-LINER"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & KRAFT_LINER & " ORDER by id ASC;"
                Case "*-OTHER"
                    sql = "SELECT id, liner1_val, medium1_val, liner2_val, medium2_val, liner3_val, liner1_color_id FROM papercombination " & _
                      "WHERE flute_id=" & FLUTE_OTHER & " ORDER by id ASC;"
            End Select
            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "customer")
            DataGrid.DataSource = ds.Tables("customer")

        Catch ex As Npgsql.NpgsqlException
            MsgBox(ex.Message)
        Catch ex As ApplicationException
            MsgBox(ex.Message)
        End Try

        conn.ClearPool()

    End Sub
    'Get paper combination
    'Public Sub GetPaperCombination(ByVal DataGrid As DataGridView, ByVal FluteType As String)
    '    Try
    '        If FluteType = "CB-Flute" Then
    '            'If GetCBTypeCombination("CB-Flute") > 0 Then 'TODO: Query is correct but their is no result greater than zero, need debug to work
    '            '    DataGrid.DataSource = Nothing
    '            '    Exit Sub
    '            'End If
    '            sql = "SELECT id,(liner1 || " & _
    '                            "CASE outer_color WHEN 'Brown' THEN ' BKL' " & _
    '                                "WHEN 'White' THEN ' WKL' " & _
    '                                "ELSE 'other' " & _
    '                            "END)as OuterLiner, " & _
    '                        "medium1,medium2,liner3,(liner2 || " & _
    '                            "CASE inner_color WHEN 'Brown' THEN ' BKL' " & _
    '                                "WHEN 'White' THEN ' WKL' " & _
    '                                "ELSE 'other' " & _
    '                            "END) as InnerLiner " & _
    '            "FROM paper_combination WHERE medium1 IS NOT NULL AND medium2 IS NOT NULL AND liner3 IS NOT NULL;"
    '        Else
    '            If GetCBTypeCombination("C-Flute") = 0 Then 'Any text other than CB-Flute; Check for record count of paper board C-flute type
    '                DataGrid.DataSource = Nothing
    '                Exit Sub
    '            End If
    '            sql = "SELECT id,(liner1 || " & _
    '                            "CASE outer_color WHEN 'Brown' THEN ' BKL' " & _
    '                                "WHEN 'White' THEN ' WKL' " & _
    '                                "ELSE 'other' " & _
    '                            "END)as OuterLiner, " & _
    '                        "medium1,(liner2 || " & _
    '                            "CASE inner_color WHEN 'Brown' THEN ' BKL' " & _
    '                                "WHEN 'White' THEN ' WKL' " & _
    '                                "ELSE 'other' " & _
    '                            "END) as InnerLiner " & _
    '            "FROM paper_combination;"
    '        End If

    '        da = New NpgsqlDataAdapter(sql, conn)
    '        da.Fill(ds, "customer")
    '        DataGrid.DataSource = ds.Tables("customer")

    '    Catch ex As Npgsql.NpgsqlException
    '        MsgBox(ex.Message)
    '    Catch ex As ApplicationException
    '        MsgBox(ex.Message)
    '    End Try

    '    conn.ClearPool()

    'End Sub
    'Get Box format (RSC/HSC) etc.
    Public Sub GetBoxFormat(ByVal ComboBox As ComboBox)

        sql = "select description FROM boxformat;"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "boxformat")
        ComboBox.DataSource = ds.Tables("boxformat")
        ComboBox.DisplayMember = "description"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()

    End Sub

    Public Sub GetBoxCategory(ByVal ComboBox As ComboBox)

        sql = "select description FROM boxcategory;"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "boxcategory")
        ComboBox.DataSource = ds.Tables("boxcategory")
        ComboBox.DisplayMember = "description"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()

    End Sub

    Public Sub GetBoxOrientation(ByVal ComboBox As ComboBox)

        sql = "select description FROM boxorientation;"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "boxorientation")
        ComboBox.DataSource = ds.Tables("boxorientation")
        ComboBox.DisplayMember = "description"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()

    End Sub
    Public Function GetIndustryType(ByVal CustomerID As Integer) As Short
        Dim rIndType As Short
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT industry_type_id FROM customer WHERE id=" & CustomerID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            rIndType = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return rIndType

    End Function
    'Get Box Format ID
    Public Function GetBoxFormatID(ByVal value As String) As Integer
        Dim dr As NpgsqlDataReader
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM boxformat WHERE description='" & value & "';"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function

    Public Function GetTableID(ByVal TableName As String, ByVal value As String) As Integer
        Dim dr As NpgsqlDataReader
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM " & TableName & " WHERE description='" & value & "';"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function

    Public Function GetCustomerPrefix(ByVal UserID As Integer) As String
        Dim prefix As String = ""
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT printcard_prefix FROM customer WHERE contact_id=" & UserID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            prefix = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return prefix

    End Function

    Public Function GetPrintcardNum(ByVal UserID As Integer) As Integer
        Dim PrintcardNum As Integer
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT max(printcardno)+1 as PrintcardNumber FROM printcard WHERE customer_id=" & UserID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            If dr.GetValue(0) IsNot Nothing And IsDBNull(dr.GetValue(0)) = False Then
                PrintcardNum = dr.GetValue(0)
            End If
        End While
        'If customer doesn't yet exist in the table
        If PrintcardNum = 0 Then
            'Insert is not possible if user will cancel printcard creation
            'sql = "INSERT INTO printcard_series(customer_id,printcardno)VALUES(" & UserID & ",1)"
            'cmd = New NpgsqlCommand(sql, conn)
            'dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            PrintcardNum = 1 'Set printcard series as #1
        End If
        conn.Close()
        conn.ClearPool()

        Return PrintcardNum
    End Function

    Public Function GetInsideDimension(ByVal DimensionID As Integer, ByVal BoxSide As String) As Integer
        Dim idval As Integer
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT " & BoxSide & " FROM paper_dimension WHERE id=" & DimensionID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            idval = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()

        Return idval
    End Function

    Public Function GetCombination(ByVal PaperCombinationID As Integer) As String
        Dim PaperCombination As String = ""
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT acombination FROM papercombination WHERE id=" & PaperCombinationID & ";"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            PaperCombination = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()

        Return PaperCombination
    End Function
End Class
