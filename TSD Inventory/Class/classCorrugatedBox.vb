Imports Npgsql
Public Class classCorrugatedBox
    Dim SQLConn As DataConnection = New DataConnection
    Public Structure BoxFactors

        Dim factor1 As Short
        Dim factor2 As Short
        Dim factor3 As Short
        Dim factor4 As Short

        Dim FactorFlap As Single
        Dim FactorHeight As Single

        Dim BoxFormat As String

    End Structure
    Public Function ComputeBoxSize(ByVal BoardTypeID As Integer, _
                                   ByVal JointTypeID As Integer, _
                                   ByVal BoxFormatID As Integer, _
                                   Optional ByVal GlueTab As Single = 35) As BoxFactors


        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
            SQLConn.sql = "SELECT factor1, factor2, factor3, factor4, fflap, fheight FROM factor " & _
                          " WHERE flute_id=" & BoardTypeID & _
                          " AND joint_id=" & JointTypeID & _
                          " AND box_id=" & BoxFormatID
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While SQLConn.dr.Read
                ComputeBoxSize.factor1 = SQLConn.dr.GetValue(0)
                ComputeBoxSize.factor2 = SQLConn.dr.GetValue(1)
                ComputeBoxSize.factor3 = SQLConn.dr.GetValue(2)
                ComputeBoxSize.factor4 = SQLConn.dr.GetValue(3)
                ComputeBoxSize.FactorFlap = SQLConn.dr.GetValue(4)
                ComputeBoxSize.FactorHeight = SQLConn.dr.GetValue(5)
            End While

            SQLConn.conn.Close()
            SQLConn.conn.ClearPool()
        Catch ex As Exception
            MsgBox(ex.Message & " Error in classCorrugatedBox: Function ComputeBoxSize during query against Factor table.")
        End Try

        ComputeBoxSize.BoxFormat = GetBoxFormat(BoxFormatID)

        Return ComputeBoxSize

    End Function
    Public Function GetBoxFormat(ByVal id As Integer) As String
        Dim BoxForm As String = ""
        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
            SQLConn.sql = "SELECT description FROM boxformat WHERE id=" & id
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While SQLConn.dr.Read
                BoxForm = SQLConn.dr.GetValue(0)
            End While

            SQLConn.conn.Close()
            SQLConn.conn.ClearPool()
        Catch ex As Exception
            MsgBox(ex.Message & " Error in classCorrugatedBox class; function GetBoxFormat()")
        End Try

        Return BoxForm
    End Function
End Class
