Imports Npgsql
Public Class ClassDiecut    
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
    Dim sql As String
    Public Sub PopulateDiecut(ByVal DiecutGrid As DataGridView)
        Dim da As NpgsqlDataAdapter
        Dim ds As DataSet = New DataSet
        Try
            sql = "SELECT diecut.id as id,diecut.diecut_number as diecutnum, racks.rack_number || ''|| racks.rack_column as rack," & _
                     " racks.id as rack_id" & _
                     " FROM diecut " & _
                     " INNER JOIN tsd_inventory.public.racks racks ON diecut.rack_id=racks.id  WHERE diecut.diecut_number <> 0 ;"

            da = New NpgsqlDataAdapter(Sql, conn)
            da.Fill(ds, "diecut")
            DiecutGrid.DataSource = ds.Tables("diecut")
        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & ". Error in PopulateDiecut @ ClassDiecut")
        End Try
    End Sub
    Public Function GetNewRackLocation() As Integer
        Dim id As Integer
        Dim dr As NpgsqlDataReader
        Dim cmd As NpgsqlCommand        
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

    Public Sub GetRackList(ByVal ComboBox As ComboBox, ByVal Order As String)
        Dim da As NpgsqlDataAdapter
        Dim ds As DataSet = New DataSet
        sql = "SELECT (racks.rack_number || ''|| racks.rack_column) as rack FROM racks WHERE racks.rack_number <> 0 ORDER BY racks.rack_number " & Order & ";"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "racks")
        ComboBox.DataSource = ds.Tables("racks")
        ComboBox.DisplayMember = "rack"
        da.Dispose()
        ds.Dispose()
        conn.ClearPool()
    End Sub
    Public Function GetRackID(ByVal racklocation As String) As Integer
        Dim id As Integer
        Dim cmd As NpgsqlCommand
        Dim dr As NpgsqlDataReader        
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM racks WHERE (racks.rack_number || ''|| UPPER(racks.rack_column))='" & UCase(racklocation) & "';"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
    Public Sub UpdateDiecut(ByVal DiecutID As Integer, ByVal DiecutNumber As Integer, ByVal RackLoc As String)
        Dim RackID As Integer = GetRackID(RackLoc)
        Dim cmd As NpgsqlCommand
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "UPDATE diecut SET diecut_number=@diecut_number, rack_id=@rack_id " & _
                           " WHERE id=@id;"

            'Initialize SqlCommand object for update. 
            cmd = New NpgsqlCommand(sql, conn)
            'Add the values                
            cmd.Parameters.Add(New NpgsqlParameter("@diecut_number", DiecutNumber))
            cmd.Parameters.Add(New NpgsqlParameter("@rack_id", RackID))
            cmd.Parameters.Add(New NpgsqlParameter("@id", DiecutID))

            'Execute the Query
            cmd.ExecuteNonQuery()

            conn.Close()
            conn.ClearPool()

        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & " Error in procedure UpdateDiecut @ Classdiecut")
        End Try
    End Sub
    Function CheckExistingDiecut(ByVal DiecutNumber As Integer) As Boolean
        Dim ires As Integer
        Dim cmd As NpgsqlCommand
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()

        sql = "SELECT diecut_number FROM diecut WHERE diecut_number=" & DiecutNumber & ";"
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
        Dim cmd As NpgsqlCommand
        Dim dr As NpgsqlDataReader
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
    Public Sub AddDiecut(ByVal DiecutNumber As Integer, ByVal RackLoc As String)
        Dim RackID As Integer = GetRackID(RackLoc)
        Dim DiecutID As Integer = GetDiecutID()
        Dim cmd As NpgsqlCommand
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "INSERT INTO diecut(" & _
                       " id, description, diecut_number, ageing, boxes_count, rack_id, " & _
                       " status_id, remarks, date_created " & _
                       " )" & _
                " VALUES(" & DiecutID & ", 'NYI', " & DiecutNumber & ", 900, 900," & RackID & ", " & _
                       " 14, 'Remarks here', '" & Now() & "');"
            'Initialize SqlCommand object for update. 
            cmd = New NpgsqlCommand(sql, conn)
            'Execute the Query
            cmd.ExecuteNonQuery()

            conn.Close()
            conn.ClearPool()

        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & " Error in procedure AddDiecut @ Classdiecut")
        End Try
    End Sub
    Private Function GetDiecutID() As Integer
        Dim id As Integer
        Dim cmd As NpgsqlCommand
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT max(id)+1 FROM diecut;"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()
        Return id
    End Function
End Class
