Imports Npgsql
Imports System.IO

Public Class ClassPrintcard    
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
    Public ErrorFlag As Short = 0 'Trigger to 1 and abort any transaction
    Dim sql As String
    Dim cmd As NpgsqlCommand
    'ErrorFlag for SavePrintcard = 1

    Public Function GetTableValueInt(ByVal TableName As String, ByVal ColumnName As String, ByVal TableID As Integer) As Integer

        Dim iRes As Integer
        Dim dr As NpgsqlDataReader

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
    Public Function GetTableValueStr(ByVal TableName As String, ByVal ColumnName As String, ByVal TableID As Integer) As String

        Dim iRes As String = ""
        Dim dr As NpgsqlDataReader

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
    Public Sub SavePrintcard(ByVal CustomerID As Integer, ByVal BoxDescription As String, _
                             ByVal BoxFormatID As Integer, ByVal FluteID As Integer, _
                             ByVal TestID As Integer, ByVal JointID As Integer, ByVal Paper_combinationID As Integer, _
                             ByVal DimensionID As Integer, ByVal Color1 As String, ByVal Color2 As String, _
                             ByVal Color3 As String, ByVal Color4 As String, ByVal Flap As String, ByVal UnitID As String, _
                             ByVal ScaleID As Integer, ByVal DiecutID As Integer, ByVal PrintcardNo As Integer, _
                             ByVal FilenameID As Integer, ByVal DateCreated As String, ByVal PrintCardNotes As String, _
                             ByVal BoardLength As Double, ByVal BoardWidth As Double, _
                             ByVal BoxHeight As Single, ByVal GlueTab As Single, _
                             ByVal Panel1 As Single, ByVal Panel2 As Single, ByVal Panel3 As Single, ByVal Panel4 As Single, _
                             ByVal CustomerFileID As Integer)


        Try

        Catch ex As Exception
            ErrorFlag = 1
            'MsgBox(ex.Message & " Error in SavePrintcard Class: ClassPrintcard")
        End Try

        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "INSERT INTO printcard( " & _
                        "customer_id, box_description, boxformat_id, flute_id, test_id, " & _
                        " joint_id, paper_combination_id, dimension_id, color_1, color_2, " & _
                        " color_3, color_4, flap, unit_id, scale_id, diecut_id, printcardno, " & _
                        " filename_id, date_created, notes, boardlength, boardwidth, " & _
                        " boxheight, gluetab, panel1, panel2, panel3, panel4, customer_file_id) " & _
                        " VALUES (@customer_id, @box_description, @boxformat_id, @flute_id, @test_id, " & _
                                    "@joint_id, @paper_combination_id, @dimension_id, @color_1, @color_2, " & _
                                    "@color_3, @color_4, @flap, @unit_id, @scale_id, @diecut_id, @printcardno, " & _
                                    "@filename_id, @date_created, @notes, @boardlength, @boardwidth, @boxheight, " & _
                                    "@gluetab, @panel1, @panel2, @panel3, @panel4, @customer_file_id);"

        'Initialize SqlCommand object for insert. 
        cmd = New NpgsqlCommand(sql, conn)
        'Add the values
        cmd.Parameters.Add(New NpgsqlParameter("@customer_id", CustomerID))
        cmd.Parameters.Add(New NpgsqlParameter("@box_description", BoxDescription))
        cmd.Parameters.Add(New NpgsqlParameter("@boxformat_id", BoxFormatID))
        cmd.Parameters.Add(New NpgsqlParameter("@flute_id", FluteID))
        cmd.Parameters.Add(New NpgsqlParameter("@test_id", TestID))

        cmd.Parameters.Add(New NpgsqlParameter("@joint_id", JointID))
        cmd.Parameters.Add(New NpgsqlParameter("@paper_combination_id", Paper_combinationID))
        cmd.Parameters.Add(New NpgsqlParameter("@dimension_id", DimensionID))
        cmd.Parameters.Add(New NpgsqlParameter("@color_1", Color1))
        cmd.Parameters.Add(New NpgsqlParameter("@color_2", Color2))

        cmd.Parameters.Add(New NpgsqlParameter("@color_3", Color3))
        cmd.Parameters.Add(New NpgsqlParameter("@color_4", Color4))
        cmd.Parameters.Add(New NpgsqlParameter("@flap", Flap))
        cmd.Parameters.Add(New NpgsqlParameter("@unit_id", UnitID))
        cmd.Parameters.Add(New NpgsqlParameter("@scale_id", ScaleID))

        cmd.Parameters.Add(New NpgsqlParameter("@diecut_id", DiecutID))
        cmd.Parameters.Add(New NpgsqlParameter("@printcardno", PrintcardNo))
        cmd.Parameters.Add(New NpgsqlParameter("@filename_id", FilenameID))
        cmd.Parameters.Add(New NpgsqlParameter("@date_created", DateCreated))
        cmd.Parameters.Add(New NpgsqlParameter("@notes", PrintCardNotes))
        cmd.Parameters.Add(New NpgsqlParameter("@boardlength", BoardLength))
        cmd.Parameters.Add(New NpgsqlParameter("@boardwidth", BoardWidth))
        cmd.Parameters.Add(New NpgsqlParameter("@boxheight", BoxHeight))
        cmd.Parameters.Add(New NpgsqlParameter("@gluetab", GlueTab))
        cmd.Parameters.Add(New NpgsqlParameter("@panel1", Panel1))
        cmd.Parameters.Add(New NpgsqlParameter("@panel2", Panel2))
        cmd.Parameters.Add(New NpgsqlParameter("@panel3", Panel3))
        cmd.Parameters.Add(New NpgsqlParameter("@panel4", Panel4))
        cmd.Parameters.Add(New NpgsqlParameter("@customer_file_id", CustomerFileID))
        '@boardlength, @boardwidth, @boxheight, " & _
        '                            "@gluetab, @panel1, @panel2, @panel3, @panel4, @customer_file_id
        'Execute the Query
        cmd.ExecuteNonQuery()

        conn.Close()
        conn.ClearPool()
    End Sub
    'ErrorFlag for InsertPrintcardSeries = 2
    Public Sub InsertPrintcardSeries(ByVal CustomerID As Integer, ByVal PrintCardNo As Integer)

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            If PrintCardNo = 1 Then 'We will use insert since this is the first printcard created for this customer.
                sql = "INSERT INTO printcard_series(customer_id,printcardno,date_updated) " & _
                              "VALUES(@customer_id,@printcardno,@date_created)"
                'Initialize SqlCommand object for insert. 
                cmd = New NpgsqlCommand(sql, conn)
                'Add the values
                cmd.Parameters.Add(New NpgsqlParameter("@customer_id", CustomerID))
                cmd.Parameters.Add(New NpgsqlParameter("@printcardno", PrintCardNo))
                cmd.Parameters.Add(New NpgsqlParameter("@date_created", Now()))
                'Execute the Query
                cmd.ExecuteNonQuery()

            Else ' Else we will update the existing printcard series 
                sql = "UPDATE printcard_series SET printcardno=@printcardno, date_updated=@date_updated " & _
                              "WHERE customer_id=@customer_id"

                'Initialize SqlCommand object for update. 
                cmd = New NpgsqlCommand(sql, conn)
                'Add the values                
                cmd.Parameters.Add(New NpgsqlParameter("@printcardno", PrintCardNo))
                cmd.Parameters.Add(New NpgsqlParameter("@date_updated", Now()))
                cmd.Parameters.Add(New NpgsqlParameter("@customer_id", CustomerID))
                'Execute the Query
                cmd.ExecuteNonQuery()

            End If


            conn.Close()
            conn.ClearPool()
        Catch ex As Exception
            ErrorFlag = 2
            'MsgBox(ex.Message & "! Error in ClassPrintcard.vb Module InsertPrintcardSeries.")
        End Try

    End Sub

    'Error flag = 3
    Public Function InsertNewID(ByVal Length As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal unit_id As Integer) As Integer
        'If the defined Inside Dimension have no existing record, then we will insert the new I.D. and get its table ID
        Dim NewDimensionID As Integer
        Dim dr As NpgsqlDataReader
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "INSERT INTO paper_dimension(length,width,height,unit_id,date_created) " & _
                          "VALUES(@length,@width,@height,@unit_id,@date_created);"
            cmd = New NpgsqlCommand(sql, conn)

            'Add the values
            cmd.Parameters.Add(New NpgsqlParameter("@length", Length))
            cmd.Parameters.Add(New NpgsqlParameter("@width", Width))
            cmd.Parameters.Add(New NpgsqlParameter("@height", Height))
            cmd.Parameters.Add(New NpgsqlParameter("@unit_id", unit_id))
            cmd.Parameters.Add(New NpgsqlParameter("@date_created", Now()))

            'Execute the Query
            cmd.ExecuteNonQuery()

            sql = "SELECT lastval() FROM paper_dimension;"
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                NewDimensionID = dr.GetValue(0)
            End While

        Catch ex As Exception
            ErrorFlag = 3
            'MsgBox(ex.Message & "! Error in InsertNewID, ClassPrintcard.vb")
        End Try

        conn.Close()
        conn.ClearPool()

        Return NewDimensionID
    End Function

    'Error flag is 4
    Public Function upLoadImageOrFile(ByVal sFilePath As String, ByVal sFileType As String) As Integer
        Dim imageData As Byte()
        Dim sFileName As String
        Dim FileID As Integer
        Dim dr As NpgsqlDataReader
        Try

            'Read Image Bytes into a byte array 

            'Initialize SQL Server Connection 
            If conn.State = ConnectionState.Closed Then conn.Open()

            'Convert File to bytes Array
            imageData = ReadFile(sFilePath)

            sFileName = System.IO.Path.GetFileName(sFilePath)

            'Set insert query 
            sql = "INSERT INTO graphicfiles(fileloaded, filename, filetype, date_created)" & _
                          "VALUES (@fileloaded, @filename, @filetype, @date_created)"

            'Initialize SqlCommand object for insert. 
            cmd = New NpgsqlCommand(sql, conn)

            'We are passing File Name and Image byte data as sql parameters. 

            cmd.Parameters.Add(New NpgsqlParameter("@filename", sFileName)) 'File path/name
            cmd.Parameters.Add(New NpgsqlParameter("@fileloaded", DirectCast(imageData, Object))) ' actual file contents

            cmd.Parameters.Add(New NpgsqlParameter("@filetype", sFileType))
            cmd.Parameters.Add(New NpgsqlParameter("@date_created", Now()))

            'Execute the Query
            cmd.ExecuteNonQuery()

            sql = "SELECT lastval() FROM graphicfiles;"
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                FileID = dr.GetValue(0)
            End While


        Catch ex As Exception
            ErrorFlag = 4
            'MessageBox.Show(ex.ToString())
            'MsgBox("File could not uploaded: ClassPrintcard: upLoadImageOrFile")

        End Try

        conn.Close()
        conn.ClearPool()

        Return FileID
    End Function
    Public Function GetTableFileID(ByVal TableName As String) As Integer
        Dim iRes As Integer
        Dim dr As NpgsqlDataReader

        If conn.State = ConnectionState.Closed Then conn.Open()

        sql = "SELECT max(id)+1 FROM " & TableName & ";"

        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            iRes = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function
    Public Function GetUserInformation(ByVal FieldName As String, ByVal UserName As String) As Integer
        Dim iRes As Integer

        Try
            Dim dr As NpgsqlDataReader

            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT " & FieldName & " FROM systemusers WHERE UPPER(username)='" & UCase(UserName) & "';"

            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                If IsDBNull(dr.GetValue(0)) = False Then
                    iRes = dr.GetValue(0)
                Else
                    iRes = 0
                End If
            End While

            conn.Close()
            conn.ClearPool()

        Catch ex As NpgsqlException
            MessageBox.Show(ex.Message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

        Return iRes

    End Function
    Public Function UploadCustomerFile(ByVal cCustomerFile As String, ByVal sFileType As String) As Integer
        Dim imageData As Byte()
        Dim sFileName As String
        Dim FileID As Integer
        Dim dr As NpgsqlDataReader
        Try

            'Read Image Bytes into a byte array 

            'Initialize SQL Server Connection 
            If conn.State = ConnectionState.Closed Then conn.Open()

            'Convert File to bytes Array
            imageData = ReadFile(cCustomerFile)

            sFileName = System.IO.Path.GetFileName(cCustomerFile)

            'Set insert query 
            sql = "INSERT INTO customerfile(fileloaded, filename, filetype, date_created)" & _
                          "VALUES (@fileloaded, @filename, @filetype, @date_created);"

            'Initialize SqlCommand object for insert. 
            cmd = New NpgsqlCommand(sql, conn)

            'We are passing File Name and Image byte data as sql parameters. 

            cmd.Parameters.Add(New NpgsqlParameter("@filename", sFileName)) 'File path/name
            cmd.Parameters.Add(New NpgsqlParameter("@fileloaded", DirectCast(imageData, Object))) ' actual file contents

            cmd.Parameters.Add(New NpgsqlParameter("@filetype", sFileType))
            cmd.Parameters.Add(New NpgsqlParameter("@date_created", Now()))

            'Execute the Query
            cmd.ExecuteNonQuery()

            sql = "SELECT lastval() FROM customerfile;"
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                FileID = dr.GetValue(0)
            End While


        Catch ex As Exception
            ErrorFlag = 4
            'MessageBox.Show(ex.ToString())
            'MsgBox("File could not uploaded: ClassPrintcard: upLoadImageOrFile")

        End Try

        conn.Close()
        conn.ClearPool()

        Return FileID
    End Function

    Private Function ReadFile(ByVal sPath As String) As Byte()

        'Initialize byte array with a null value initially. 
        Dim data As Byte() = Nothing

        'Use FileInfo object to get file size. 
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length

        'Open FileStream to read file 
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)

        'Use BinaryReader to read file stream into byte array. 
        Dim br As New BinaryReader(fStream)

        'When you use BinaryReader, you need to supply number of bytes to read from file. 
        'In this case we want to read entire file. So supplying total number of bytes. 
        data = br.ReadBytes(CInt(numBytes))

        Return data
    End Function

    Public Sub downLoadFile(ByVal id As Long, ByVal sFileName As String, ByVal sFileExtension As String)

        'For Document
        Try
            'Get image data from gridview column. 
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select fileloaded from graphicfiles WHERE id=" & id

            cmd = New NpgsqlCommand(sql, conn)

            'Get image data from DB
            Dim fileData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            Dim sTempFileName As String = Path.GetTempPath & "\" & sFileName

            If Not fileData Is Nothing Then

                'Read image data into a file stream 
                Using fs As New FileStream(sTempFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using

                'Open File

                Process.Start(sTempFileName)

            End If

        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

        conn.Close()
        conn.ClearPool()

    End Sub

    Public Sub EditdownLoadFile(ByVal id As Long, ByVal sFileName As String, ByVal sFileExtension As String)

        'For Document
        Try
            'Get image data from gridview column. 
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select fileloaded from graphicfiles WHERE id=" & id

            cmd = New NpgsqlCommand(sql, conn)

            'Get image data from DB
            Dim fileData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            Dim sTempFileName As String = Application.LocalUserAppDataPath & "\" & sFileName

            If Not fileData Is Nothing Then

                'Read image data into a file stream 
                Using fs As New FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using
            End If

        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

        conn.Close()
        conn.ClearPool()

    End Sub
    Public Sub ViewCustomerFile(ByVal CustomerFileID As Long, ByVal sFileName As String, ByVal sFileExtension As String)

        'For Document
        Try
            'Get image data from gridview column. 
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select fileloaded from customerfile WHERE id=" & CustomerFileID

            cmd = New NpgsqlCommand(sql, conn)

            'Get image data from DB
            Dim fileData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            Dim sTempFileName As String = Path.GetTempPath & "\" & sFileName

            If Not fileData Is Nothing Then

                'Read image data into a file stream 
                Using fs As New FileStream(sTempFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using

                Process.Start(sTempFileName)
                'FilePreview.Show()
                'FilePreview.WebBrowser1.Navigate(sFileName)

            End If

        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

        conn.Close()
        conn.ClearPool()

    End Sub
    Public Sub EditViewCustomerFile(ByVal CustomerFileID As Long, ByVal sFileName As String, ByVal sFileExtension As String)

        'For Document
        Try
            'Get image data from gridview column. 
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select fileloaded from customerfile WHERE id=" & CustomerFileID

            cmd = New NpgsqlCommand(sql, conn)

            'Get image data from DB
            Dim fileData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            Dim sTempFileName As String = Application.LocalUserAppDataPath & "\" & sFileName

            If Not fileData Is Nothing Then

                'Read image data into a file stream 
                Using fs As New FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using
            End If

        Catch ex As Exception

            MsgBox(ex.Message)
        End Try

        conn.Close()
        conn.ClearPool()

    End Sub

    Private Function GetRubberdieLocation(ByVal RackID As Integer) As String
        Dim iRes As String = ""
        Dim dr As NpgsqlDataReader
        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "Select ((rack_number) || rack_column) FROM  rubberdie_racks WHERE id =(SELECT rack_id FROM rubberdie WHERE id=(SELECT rubberdie_id FROM printcard WHERE id=" & RackID & ");"
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            iRes = dr.GetValue(0)
        End While

        If iRes = "0N/A" Then
            iRes = "N/A"
        End If

        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function
    
    Public Sub GetPrintcardList(ByVal TableGrid As DataGridView, ByVal DateYear As String, ByVal DateMonth As String, Optional ByVal NumOfRecords As Integer = 50)
        Dim param As String
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        Try
            If DateMonth = "" Then
                param = "YYYY"
            Else
                param = "YYYYMM"
            End If
            Dim YearAndMonth As String = DateYear + DateMonth           
            sql = "SELECT * FROM browseprintcard WHERE (to_char(date_created,'" & param & "')='" & YearAndMonth & "') AND deleted=0" & _
                        " ORDER BY fileid ASC LIMIT " & NumOfRecords & ";"
            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "graphicfiles")
            TableGrid.DataSource = ds.Tables("graphicfiles")

        Catch ex As Npgsql.NpgsqlException
            MsgBox(ex.Message)
        Catch ex As ApplicationException
            MsgBox(ex.Message)
        End Try

        conn.ClearPool()

    End Sub

    Public Function DeleteData(ByVal TableName As String, ByVal TableID As Integer) As Boolean
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "DELETE FROM " & TableName & " WHERE id= " & TableID
            'Initialize SqlCommand object for insert. 
            cmd = New NpgsqlCommand(sql, conn)
            'Execute the Query
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
        conn.Close()
        conn.ClearPool()
        Return True
    End Function
    Private Function GetPrintcardCustFileID(ByVal PrintcardID As Integer, ByVal ColumnName As String) As Integer
        Dim iRes As Integer
        Dim dr As NpgsqlDataReader

        If conn.State = ConnectionState.Closed Then conn.Open()

        sql = "SELECT " & ColumnName & " FROM printcard WHERE id=" & PrintcardID & ";"

        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            iRes = dr.GetValue(0)
        End While

        conn.Close()
        conn.ClearPool()

        Return iRes
    End Function

    Public Function CheckExistingDimension(ByVal cLength As Integer, ByVal cWidth As Integer, ByVal cHeight As Integer) As Integer

        Dim id As Integer
        Dim dr As NpgsqlDataReader

        If conn.State = ConnectionState.Closed Then conn.Open()
        sql = "SELECT id FROM paper_dimension WHERE length=" & cLength & " AND width=" & cWidth & " AND height=" & cHeight
        cmd = New NpgsqlCommand(sql, conn)
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (dr.Read)
            id = dr.GetValue(0)
        End While
        conn.Close()
        conn.ClearPool()

        Return id

    End Function
    Public Function UpdatePrintcard(ByVal UPDATE_CustomerID As Integer,
                               ByVal UPDATE_PrintcardID As Integer,
                               ByVal UDPATE_BoxDescription As String,
                               ByVal UPDATE_BoxFormatID As Integer,
                               ByVal UPDATE_FluteID As Integer,
                               ByVal UPDATE_TestID As Integer,
                               ByVal UPDATE_JointID As Integer,
                               ByVal UPDATE_PaperCombinationID As Integer,
                               ByVal UPDATE_InsideDimensionID As Integer,
                               ByVal UPDATE_Color1 As String, ByVal UPDATE_Color2 As String, ByVal UPDATE_Color3 As String, ByVal UPDATE_Color4 As String,
                               ByVal UPDATE_Flap As Integer,
                               ByVal UPDATE_UnitID As Integer,
                               ByVal UPDATE_ScaleID As Integer,
                               ByVal UPDATE_DiecutID As Integer,
                               ByVal UPDATE_PrintcardNo As Integer,
                               ByVal UPDATE_DateCreated As String,
                               ByVal UPDATE_Notes As String,
                               ByVal UPDATE_BoardWidth As Integer,
                               ByVal UPDATE_BoardLength As Integer,
                               ByVal UPDATE_BoxHeight As Integer,
                               ByVal UPDATE_GlueTab As Integer,
                               ByVal UPDATE_Panel1 As Integer, ByVal UPDATE_Panel2 As Integer, ByVal UPDATE_Panel3 As Integer, ByVal UPDATE_Panel4 As Integer,
                               ByVal UPDATE_EditorID As Integer, ByVal UPDATE_BoxOrientationID As Integer, ByVal UPDATE_IN_Length As Integer, ByVal UPDATE_IN_Width As Integer, ByVal UPDATE_IN_Height As Integer,
                               ByVal _Board1Width As Single, ByVal _Board1Length As Single, ByVal _Board2Width As Single, ByVal _Board2Length As Single, ByVal UPDATE_TestTypeIDStr As String, ByVal UPDATE_PCTESTID_GETCmbTest As Integer, ByVal UPDATE_PrintcardStatusID As Integer, ByVal Category_ID As Integer,
                               Optional ByVal UPDATE_GraphicFilePath As String = "",
                               Optional ByVal UPDATE_CustomerFilePath As String = "") As Integer

        Dim iResult As Integer = 0
        Dim UPDATE_INTERNAL_FILEUPLOAD_ID As Integer
        Dim PC_CUSTOMER_FILEUPLOAD_ID As Integer
        Dim UPDATE_NewTestTableID As Integer
        Dim UPDATE_TestTypeID As Integer

        Dim OLDGraphicFileID As Integer = GetPrintcardCustFileID(UPDATE_PrintcardID, "filename_id") 'Get old graphic file ID and literally delete it from the table

        Dim OldCustomerFileID As Integer = GetPrintcardCustFileID(UPDATE_PrintcardID, "customer_file_id") ' Get old customer file id

        Using connTransaction As NpgsqlConnection = New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port= " & DatabasePort)
            Try
                If connTransaction.State = ConnectionState.Closed Then connTransaction.Open()
                Using pgTransaction As NpgsqlTransaction = connTransaction.BeginTransaction
                    Try
                        If UPDATE_GraphicFilePath <> "" Then
                            Dim UPDATE_INTERNAL_FILE_UPLOAD As String = LTrim(RTrim(UPDATE_GraphicFilePath))
                            Dim UPDATE_INTERNAL_FILE As String = System.IO.Path.GetExtension(UPDATE_INTERNAL_FILE_UPLOAD)
                            Dim UPDATE_INTERNAL_FILE_EXT As String = GetExtensionType(UPDATE_INTERNAL_FILE)
                            Dim UPDATE_INTERNAL_ImageData As Byte()
                            Dim UPDATE_INTERNAL_sFileName As String
                            UPDATE_INTERNAL_ImageData = ReadFile(UPDATE_INTERNAL_FILE_UPLOAD)
                            UPDATE_INTERNAL_sFileName = System.IO.Path.GetFileName(UPDATE_INTERNAL_FILE_UPLOAD)
                            'New Graphic file id
                            UPDATE_INTERNAL_FILEUPLOAD_ID = GetTableFileID("graphicfiles")

                            Using UploadGraphicFileCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO graphicfiles(id, fileloaded, filename, filetype, date_created)" & _
                                                                                  " SELECT @id, @fileloaded, @filename, @filetype, @date_created;", connTransaction, pgTransaction)
                                UploadGraphicFileCMD.Parameters.AddWithValue("@id", UPDATE_INTERNAL_FILEUPLOAD_ID)
                                UploadGraphicFileCMD.Parameters.AddWithValue("@fileloaded", UPDATE_INTERNAL_ImageData)
                                UploadGraphicFileCMD.Parameters.AddWithValue("@filename", UPDATE_INTERNAL_sFileName)
                                UploadGraphicFileCMD.Parameters.AddWithValue("@filetype", UPDATE_INTERNAL_FILE_EXT)
                                UploadGraphicFileCMD.Parameters.AddWithValue("@date_created", Now())
                                UploadGraphicFileCMD.ExecuteNonQuery()
                            End Using
                        Else
                            UPDATE_INTERNAL_FILEUPLOAD_ID = GetPrintcardCustFileID(UPDATE_PrintcardID, "filename_id")
                        End If

                        If UPDATE_CustomerFilePath <> "" Then
                            Dim PC_CUSTOMER_FILE_UPLOAD As String = LTrim(RTrim(UPDATE_CustomerFilePath))
                            Dim PC_CUSTOMER_FILE As String = System.IO.Path.GetExtension(PC_CUSTOMER_FILE_UPLOAD)
                            Dim PC_CUSTOMER_FILE_EXT As String = GetExtensionType(PC_CUSTOMER_FILE)
                            Dim PC_CUSTOMER_ImageData As Byte() = ReadFile(PC_CUSTOMER_FILE_UPLOAD)
                            Dim PC_CUSTOMER_sFileName As String = System.IO.Path.GetFileName(PC_CUSTOMER_FILE_UPLOAD)
                            PC_CUSTOMER_FILEUPLOAD_ID = GetTableFileID("customerfile") ' customerfile id

                            Using UploadCustomerFileCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO customerfile(id, fileloaded, filename, filetype, date_created)" & _
                                                                                            " SELECT @id, @fileloaded, @filename, @filetype, @date_created;", connTransaction, pgTransaction)
                                UploadCustomerFileCMD.Parameters.AddWithValue("@id", PC_CUSTOMER_FILEUPLOAD_ID)
                                UploadCustomerFileCMD.Parameters.AddWithValue("@fileloaded", PC_CUSTOMER_ImageData)
                                UploadCustomerFileCMD.Parameters.AddWithValue("@filename", PC_CUSTOMER_sFileName)
                                UploadCustomerFileCMD.Parameters.AddWithValue("@filetype", PC_CUSTOMER_FILE_EXT)
                                UploadCustomerFileCMD.Parameters.AddWithValue("@date_created", Now())
                                UploadCustomerFileCMD.ExecuteNonQuery()
                            End Using

                        Else
                            PC_CUSTOMER_FILEUPLOAD_ID = GetPrintcardCustFileID(UPDATE_PrintcardID, "customer_file_id")
                        End If

                        If UPDATE_InsideDimensionID = 0 Then 'New I.D.
                            UPDATE_InsideDimensionID = GetTableFileID("paper_dimension")
                            Using InsertNewInsideDimensionCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO paper_dimension(id,length,width,height,unit_id,date_created) " & _
                                                                                                   " SELECT @id,@length,@width,@height,@unit_id,@date_created;", connTransaction, pgTransaction)
                                InsertNewInsideDimensionCMD.Parameters.AddWithValue("@id", UPDATE_InsideDimensionID)
                                InsertNewInsideDimensionCMD.Parameters.AddWithValue("@length", UPDATE_IN_Length)
                                InsertNewInsideDimensionCMD.Parameters.AddWithValue("@width", UPDATE_IN_Width)
                                InsertNewInsideDimensionCMD.Parameters.AddWithValue("@height", UPDATE_IN_Height)
                                InsertNewInsideDimensionCMD.Parameters.AddWithValue("@unit_id", UPDATE_UnitID)
                                InsertNewInsideDimensionCMD.Parameters.AddWithValue("@date_created", Now())
                                InsertNewInsideDimensionCMD.ExecuteNonQuery()
                            End Using
                        End If

                        'Save Printcard

                        UPDATE_TestTypeID = GetTableValueIntColumnCompare("test_type", "id", "code", UPDATE_TestTypeIDStr)

                        If UPDATE_TestID = 0 Then
                            'insert new value to test table
                            UPDATE_NewTestTableID = GetTableNextIDIncrementBy("test", 1)
                            Using InsertNewTestIDCMD As NpgsqlCommand = New NpgsqlCommand("INSERT INTO test(id,value,date_created) " & _
                                                                                                  " SELECT @id,@value,@date_created;", connTransaction, pgTransaction)
                                InsertNewTestIDCMD.Parameters.AddWithValue("@id", UPDATE_NewTestTableID)
                                InsertNewTestIDCMD.Parameters.AddWithValue("@value", UPDATE_PCTESTID_GETCmbTest)
                                InsertNewTestIDCMD.Parameters.AddWithValue("@date_created", Now())
                                InsertNewTestIDCMD.ExecuteNonQuery()
                            End Using
                            UPDATE_TestID = UPDATE_NewTestTableID
                        End If


                        Using UpdatePrintcardCMD As NpgsqlCommand = New NpgsqlCommand("UPDATE printcard " & _
                                                               " SET customer_id=@customer_id, box_description=@box_description, boxformat_id=@boxformat_id, flute_id=@flute_id,  " & _
                                                                 "  test_id=@test_id, joint_id=@joint_id, paper_combination_id=@paper_combination_id, dimension_id=@dimension_id,  " & _
                                                                  " color_1=@color_1, color_2=@color_2, color_3=@color_3, color_4=@color_4, flap=@flap, unit_id=@unit_id,  " & _
                                                                  " scale_id=@scale_id, diecut_id=@diecut_id, printcardno=@printcardno, filename_id=@filename_id, date_created=@date_created,  " & _
                                                                  " notes=@notes, boardlength=@boardlength, boardwidth=@boardwidth, boxheight=@boxheight,  " & _
                                                                  " gluetab=@gluetab, panel1=@panel1, panel2=@panel2, panel3=@panel3, panel4=@panel4, customer_file_id=@customer_file_id, " & _
                                                                  " editor_id=@editor_id,boxorientation_id=@boxorientation_id, date_updated=@date_updated, " & _
                                                                  " board1width=@board1width, board1length=@board1length, board2width=@board2width, board2length=@board2length, test_type_id=@test_type_id, printcard_status=@printcard_status,box_category_id=@box_category_id " & _
                                                             " WHERE id=@id;", connTransaction, pgTransaction)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@id", UPDATE_PrintcardID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@customer_id", UPDATE_CustomerID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@box_description", UDPATE_BoxDescription)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@boxformat_id", UPDATE_BoxFormatID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@flute_id", UPDATE_FluteID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@test_id", UPDATE_TestID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@joint_id", UPDATE_JointID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@paper_combination_id", UPDATE_PaperCombinationID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@dimension_id", UPDATE_InsideDimensionID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@color_1", UPDATE_Color1)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@color_2", UPDATE_Color2)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@color_3", UPDATE_Color3)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@color_4", UPDATE_Color4)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@flap", UPDATE_Flap)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@unit_id", UPDATE_UnitID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@scale_id", UPDATE_ScaleID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@diecut_id", UPDATE_DiecutID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@printcardno", UPDATE_PrintcardNo)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@filename_id", UPDATE_INTERNAL_FILEUPLOAD_ID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@date_created", UPDATE_DateCreated)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@notes", UPDATE_Notes)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@boardlength", UPDATE_BoardLength)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@boardwidth", UPDATE_BoardWidth)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@boxheight", UPDATE_BoxHeight)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@gluetab", UPDATE_GlueTab)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@panel1", UPDATE_Panel1)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@panel2", UPDATE_Panel2)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@panel3", UPDATE_Panel3)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@panel4", UPDATE_Panel4)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@customer_file_id", PC_CUSTOMER_FILEUPLOAD_ID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@editor_id", UPDATE_EditorID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@boxorientation_id", UPDATE_BoxOrientationID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@date_updated", Now())
                            UpdatePrintcardCMD.Parameters.AddWithValue("@board1width", _Board1Width)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@board1length", _Board1Length)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@board2width", _Board2Width)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@board2length", _Board2Length)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@test_type_id", UPDATE_TestTypeID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@printcard_status", UPDATE_PrintcardStatusID)
                            UpdatePrintcardCMD.Parameters.AddWithValue("@box_category_id", Category_ID)                            
                            UpdatePrintcardCMD.ExecuteNonQuery()
                        End Using

                        'OPTION: Comment out below if replaced graphic/customer file needs to be deleted, however, files need to be kept for auditing purposes.
                        ''Delete if there is an existing customer file, we cannot delete old graphic/customer files unless Printcard is updated to new id constraints
                        'If UPDATE_CustomerFilePath <> "" Then
                        '    If OldCustomerFileID <> 1 Then
                        '        Using ReplaceDelCustomerFileCMD As NpgsqlCommand = New NpgsqlCommand("DELETE FROM customerfile WHERE id=" & OldCustomerFileID & ";", connTransaction, pgTransaction)
                        '            ReplaceDelCustomerFileCMD.ExecuteNonQuery()
                        '        End Using
                        '    End If
                        'End If

                        'If UPDATE_GraphicFilePath <> "" Then ' if graphic file needs to be updated.
                        '    Using ReplaceDelGraphicFileCMD As NpgsqlCommand = New NpgsqlCommand("DELETE FROM graphicfiles WHERE id=" & OLDGraphicFileID & ";", connTransaction, pgTransaction)
                        '        ReplaceDelGraphicFileCMD.ExecuteNonQuery()
                        '    End Using
                        'End If

                        pgTransaction.Commit()
                        pgTransaction.Dispose()
                        connTransaction.Close()
                        connTransaction.ClearPool()
                        iResult = 1
                    Catch ex As Exception
                        pgTransaction.Rollback()
                        Throw
                    End Try
                End Using
            Catch ex As NpgsqlException
                'MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Printcard_ErrorUpdateSave = ex.Message
            Catch ex As Exception
                'MessageBox.Show("Exemption occurred: " & ex.Message & " !!!", ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Printcard_ErrorUpdateSave = ex.Message
                Throw
            End Try
        End Using

        Return iResult

    End Function
    Private Function GetExtensionType(ByVal FileExtension As String) As String
        Dim ReturnExtension As String = ""
        FileExtension = LCase(FileExtension)
        If FileExtension = ".jpg" Or _
           FileExtension = ".bmp" Or _
           FileExtension = ".png" Or _
           FileExtension = ".jpeg" Or _
           FileExtension = ".tif" Then
            ReturnExtension = "Image"
        ElseIf FileExtension = ".cdr" Or _
           FileExtension = ".cdt" Or _
           FileExtension = ".cmx" Or _
           FileExtension = ".cdx" Or _
           FileExtension = ".cpx" Then
            ReturnExtension = "CorelDraw"
        ElseIf FileExtension = ".xcf" Then
            ReturnExtension = "GIMP"
        ElseIf FileExtension = ".cpt" Then
            ReturnExtension = "Photopaint"
        ElseIf FileExtension = ".ai" Then
            ReturnExtension = "Illustrator"
        ElseIf FileExtension = ".psd" Then
            ReturnExtension = "Photoshop"
        ElseIf FileExtension = ".pdf" Then
            ReturnExtension = "PDF File"
        Else
            ReturnExtension = "Unknown format"
        End If
        Return ReturnExtension
    End Function

    Public Function UpdateData(ByVal TableName As String, ByVal CustomerID As Integer, ByVal FieldName As String, ByVal Value As Integer) As Boolean
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "UPDATE " & TableName & " SET " & FieldName & "=" & Value & " WHERE customer_id=" & CustomerID
            'Initialize SqlCommand object for insert. 
            cmd = New NpgsqlCommand(sql, conn)
            'Execute the Query
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
        conn.Close()
        conn.ClearPool()
        Return True
    End Function

    Public Function CreateCopyFromFile(ByVal id As Long, ByVal sFileName As String, ByVal sFileExtension As String) As String
        'For Document
        Dim sTempFileName As String = ""
        Try
            'Get image data from gridview column. 
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select fileloaded from graphicfiles WHERE id=" & id

            cmd = New NpgsqlCommand(sql, conn)

            'Get image data from DB
            Dim fileData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            sTempFileName = Application.StartupPath & "\" & sFileName

            If Not fileData Is Nothing Then

                'Read image data into a file stream 
                Using fs As New FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        conn.Close()
        conn.ClearPool()

        Return sTempFileName
    End Function

    'Copy printcard funtions
    Function GetStringFromTable(ByVal id As Integer, ByVal ColumnName As String, ByVal TableName As String) As String
        Dim dr As NpgsqlDataReader
        Dim iResStr As String = ""
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT " & ColumnName & " FROM " & TableName & " WHERE id=" & id
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (dr.Read)
                iResStr = dr.GetValue(0)
            End While
            conn.Close()
            conn.ClearPool()
        Catch ex As Exception
            MsgBox(ex.Message & " Error in GetStringFromTable function: Class ClassPrintcard")
        End Try

        Return iResStr
    End Function
    Public Function GetFlute(ByVal id As Integer) As String
        Dim iResStr As String = ""
        Dim dr As NpgsqlDataReader
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select (flute || '-') || description as boardtype FROM flute WHERE id =" & id
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (dr.Read)
                iResStr = dr.GetValue(0)
            End While
            conn.Close()
            conn.ClearPool()
        Catch ex As Exception
            MsgBox(ex.Message & " Error in GetFlute function: Class ClassPrintcard")
        End Try

        Return iResStr
    End Function

    Public Sub Search(ByVal TableGrid As DataGridView, ByVal DateYear As String, ByVal DateMonth As String, ByVal Column As String, ByVal TextToSearch As String, ByVal chkDateSearch As CheckBox, Optional ByVal NumOfRecords As Integer = 50)
        Dim param As String
        Dim da As NpgsqlDataAdapter
        Dim ds As New DataSet
        Try
            If DateMonth = "" Then
                param = "YYYY"
            Else
                param = "YYYYMM"
            End If
            Dim YearAndMonth As String = DateYear + DateMonth
            If TextToSearch = "*" Then
                If chkDateSearch.Checked = True Then
                    sql = "SELECT * FROM browseprintcard WHERE (to_char(date_created,'" & param & "')='" & YearAndMonth & "') " & _
                            " ORDER BY fileid ASC LIMIT " & NumOfRecords & ";"
                Else
                    sql = "SELECT * FROM browseprintcard ORDER BY fileid ASC LIMIT " & NumOfRecords & ";"
                End If
            Else
                If chkDateSearch.Checked = True Then
                    sql = "SELECT * FROM browseprintcard WHERE (to_char(date_created,'" & param & "')='" & YearAndMonth & "') AND UPPER(" & Column & ") ::text ilike UPPER('%" & TextToSearch & "%')" & _
                        " ORDER BY fileid ASC LIMIT " & NumOfRecords & ";"
                Else
                    sql = "SELECT * FROM browseprintcard WHERE UPPER(" & Column & ") ::text ilike UPPER('%" & TextToSearch & "%')" & _
                        " ORDER BY fileid ASC LIMIT " & NumOfRecords & ";"
                End If                
            End If

            da = New NpgsqlDataAdapter(sql, conn)
            da.Fill(ds, "graphicfiles")
            TableGrid.DataSource = ds.Tables("graphicfiles")

        Catch ex As Npgsql.NpgsqlException
            MsgBox(ex.Message)
        Catch ex As ApplicationException
            MsgBox(ex.Message)
        End Try

        conn.ClearPool()

    End Sub

    Public Function CheckExistingPrincardNumber(ByVal CustomerID As Integer, ByVal PrintcardNumber As Integer) As Integer
        CheckExistingPrincardNumber = 0
        Dim dr As NpgsqlDataReader
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "Select count(*) FROM printcard WHERE customer_id =" & CustomerID & " AND printcardno=" & PrintcardNumber
            cmd = New NpgsqlCommand(sql, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (dr.Read)
                CheckExistingPrincardNumber = dr.GetValue(0)
            End While
            conn.Close()
            conn.ClearPool()
        Catch ex As Exception
            MsgBox(ex.Message & " Error in CheckExistingPrincardNumber function: Class ClassPrintcard")
        End Try
        Return CheckExistingPrincardNumber
    End Function
End Class
