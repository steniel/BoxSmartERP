Imports Npgsql
Public Class PaperDimension
    Dim SQLConn As DataConnection

    Public Function CheckExistingDimension(ByVal cLength As Integer, ByVal cWidth As Integer, ByVal cHeight As Integer) As Integer

        Dim id As Integer
        SQLConn = New DataConnection
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT id FROM paper_dimension WHERE length=" & cLength & " AND width=" & cWidth & " AND height=" & cHeight
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        SQLConn = Nothing
        Return id

    End Function

End Class
