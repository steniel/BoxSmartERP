Imports Npgsql
Public Class DataConnection
    Public cmd As NpgsqlCommand
    Public sql As String
    Public dr As NpgsqlDataReader
    Public da As NpgsqlDataAdapter
    Public ds As New DataSet
    Public conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port=" & DatabasePort)
End Class
