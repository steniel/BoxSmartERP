Imports Npgsql
Public Class AddRacks
    Dim SQLConn As DataConnection = New DataConnection
    Public Sub UpdateRackLocation(ByVal RackNumber As Integer, ByVal ComboBox1 As ComboBox)
        Dim CurrentColumn As Char
        Dim iRes As Integer
        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()

            SQLConn.sql = "SELECT count(*) FROM racks WHERE rack_number=" & RackNumber
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While SQLConn.dr.Read
                iRes = SQLConn.dr.GetValue(0)
            End While

            If iRes > 0 Then
                SQLConn.sql = "SELECT rack_column FROM racks WHERE rack_number=" & RackNumber
                SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
                SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
                While SQLConn.dr.Read
                    CurrentColumn = SQLConn.dr.GetValue(0)
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

        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
    End Sub
    Function GetLastRackNum() As Integer
        Dim NewRackNumber As Integer
        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()       

            SQLConn.sql = "SELECT max(rack_number)+1 FROM racks;"
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While SQLConn.dr.Read
                NewRackNumber = SQLConn.dr.GetValue(0)
            End While

        Catch ex As Exception            
            MsgBox(ex.Message & "! Error in GetLastRackNum, AddRacks.vb")
        End Try

        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()

        Return NewRackNumber
    End Function

    Function NewRackNumber(ByVal RackNumber As Integer, ByVal RackColumn As Char) As Boolean
        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
            SQLConn.sql = "INSERT INTO racks(rack_number,rack_column,date_created) " & _
                          "VALUES(@rack_number,@rack_column,@date_created)"
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)

            'Add the values
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@rack_number", RackNumber))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@rack_column", RackColumn))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@date_created", Now()))

            'Execute the Query
            SQLConn.cmd.ExecuteNonQuery()

            'Close connection
            SQLConn.conn.Close()
            SQLConn.conn.ClearPool()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    
    Function CheckExistingRacknumberColumn(ByVal RackNumber As Integer, ByVal RackColumn As Char) As Boolean   
        Dim ires As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()

        SQLConn.sql = "SELECT count(*) FROM racks WHERE rack_number=" & RackNumber & " AND rack_column='" & RackColumn & "'"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While SQLConn.dr.Read
            ires = SQLConn.dr.GetValue(0)
        End While

        If ires > 0 Then
            Return True
        Else
            Return False
        End If

        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()

    End Function
    Function CheckLaktaw(ByVal RackNumber As Integer) As Integer
        Dim ires As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()

        SQLConn.sql = "SELECT max(rack_number) FROM racks"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While SQLConn.dr.Read
            ires = SQLConn.dr.GetValue(0)
        End While

        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()

        Return ires
    End Function

End Class
