Imports Npgsql
Public Class units    
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
    Public da As NpgsqlDataAdapter
    Public ds As New DataSet
    Dim cmd As NpgsqlCommand
    Dim sql As String
    'Get Box format (RSC/HSC) etc.
    Public Sub UnitTable(ByVal ComboBox As ComboBox)

        sql = "select prefix FROM unit_table"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "unit_table")
        ComboBox.DataSource = ds.Tables("unit_table")
        ComboBox.DisplayMember = "prefix"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()

    End Sub

    Public Sub PageScale(ByVal ComboBox As ComboBox)

        sql = "select description FROM scale"
        da = New NpgsqlDataAdapter(sql, conn)
        da.Fill(ds, "scale")
        ComboBox.DataSource = ds.Tables("scale")
        ComboBox.DisplayMember = "description"

        da.Dispose()
        ds.Dispose()
        conn.ClearPool()

    End Sub

    Public Function PageScaleID(ByVal value As String) As Integer
        Dim id As Integer
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM scale WHERE description='" & value & "'"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return id

    End Function

    Public Function GetPrintcardStatusID(ByVal value As String) As Integer
        Dim id As Integer
        Dim dr As NpgsqlDataReader
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "select generic_status.id as id from generic_status, international_description where " & _
                          " generic_status.dtype='printcard_status' and international_description.foreign_id=generic_status.status_value" & _
                          " and international_description.table_id=" & DATABASE_printcard & " and international_description.content='" & value & "';"
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (dr.Read)
                id = dr.GetValue(0)
            End While
            conn.Close()
            conn.ClearPool()
        Catch ex As Exception
            MessageBox.Show(ex.Message & " Error in GetPrintcardStatusID class @units.", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        
        Return id
    End Function

    Public Function GetUnitID(ByVal value As String) As Integer
        Dim dr As NpgsqlDataReader
        Dim id As Integer
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM unit_table WHERE prefix='" & value & "'"
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
