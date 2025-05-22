Imports Npgsql

Public Class ClassManageCustomer
    Dim SQLConn As DataConnection = New DataConnection
    Dim conn As New NpgsqlConnection("SERVER=" & DataBaseHost & ";DATABASE=" & DatabaseName & ";USER ID=" & DatabaseUser & ";PASSWORD=" & DatabasePassword & ";pooling=" & DatabasePooling & "; port=" & DatabasePort)
    Public Function AddCustomer(ByVal NewCustomerID As Integer, ByVal CompanyName As String, _
                            ByVal LastName As String, _
                            ByVal FirstName As String, _
                            ByVal OfficePhone As String, _
                            ByVal MobilePhone As String, _
                            ByVal Email As String, _
                            ByVal StreetName As String, _
                            ByVal BarangayName As String, _
                            ByVal PrintcardPrefix As String, _
                            ByVal IndustryType As String, _
                            ByVal CityName As String) As Boolean
        Dim IndustryTypeID As Integer = GetIndustryID(IndustryType)
        Dim CityID As Integer = GetCityID(CityName)
        Dim CurrentStatus As String = "Active" 'Of course
        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
            SQLConn.sql = "INSERT INTO CONTACT (" & _
                          " id, organization_name, last_name, first_name, office_phone_number, " & _
                          " email, city_id, mobile_phone_number, " & _
                          " street_name, barangay_name, create_datetime) " & _
                          " VALUES (@id, @organization_name, @last_name, " & _
                          " @first_name, @office_phone_number, @email, @city_id, " & _
                          " @mobile_phone_number, @street_name, @barangay_name, @create_datetime)"

            'Initialize SqlCommand object for update. 
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            'Add the values                
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@id", NewCustomerID))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@organization_name", CompanyName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@last_name", LastName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@first_name", FirstName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@office_phone_number", OfficePhone))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@email", Email))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@city_id", CityID))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@mobile_phone_number", MobilePhone))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@street_name", StreetName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@barangay_name", BarangayName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@create_datetime", Now()))
            'Execute the Query
            SQLConn.cmd.ExecuteNonQuery()

            'Insert Printcard prefix
            SQLConn.sql = "INSERT INTO CUSTOMER(id, contact_id, current_status, printcard_prefix, industry_type_id) " & _
                          " VALUES( @id, @contact_id, @current_status, @printcard_prefix, @industry_type_id) "

            'Initialize SqlCommand object for update. 
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)

            'Add the values      
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@id", NewCustomerID))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@contact_id", NewCustomerID))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@current_status", CurrentStatus))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@printcard_prefix", PrintcardPrefix))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@industry_type_id", IndustryTypeID))

            'Execute the Query
            SQLConn.cmd.ExecuteNonQuery()

            'Close conn
            SQLConn.conn.Close()
            SQLConn.conn.ClearPool()
            Return True
        Catch ex As Exception            
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & " Error adding customer, AddCustomer() @ ClassManageCustomer")
            Return False
        End Try
    End Function
    Public Function GetNewCustomerID() As Integer
        Dim id As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT max(id)+5 FROM contact"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        Return id
    End Function
    Public Sub EditCustomer(ByVal CustomerID As Integer, ByVal CompanyName As String, _
                            ByVal LastName As String, _
                            ByVal FirstName As String, _
                            ByVal OfficePhone As String, _
                            ByVal MobilePhone As String, _
                            ByVal Email As String, _
                            ByVal StreetName As String, _
                            ByVal BarangayName As String, _
                            ByVal PrintcardPrefix As String, _
                            ByVal IndustryType As String, _
                            ByVal CityName As String)
        Dim IndustryTypeID As Integer = GetIndustryID(IndustryType)
        Dim CityID As Integer = GetCityID(CityName)
        Try
            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
            SQLConn.sql = "UPDATE CONTACT SET organization_name=@organization_name, " & _
                           " last_name=@last_name, " & _
                           " first_name=@first_name, " & _
                           " office_phone_number=@office_phone_number, " & _
                           " email=@email, " & _
                           " mobile_phone_number=@mobile_phone_number, " & _
                           " street_name=@street_name, " & _
                           " barangay_name=@barangay_name, " & _
                           " city_id=@city_id, " & _
                           " update_datetime=@update_datetime " & _
                            " WHERE id=@customer_id"

            'Initialize SqlCommand object for update. 
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            'Add the values                
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@organization_name", CompanyName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@last_name", LastName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@first_name", FirstName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@office_phone_number", OfficePhone))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@email", Email))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@mobile_phone_number", MobilePhone))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@street_name", StreetName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@barangay_name", BarangayName))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@city_id", CityID))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@update_datetime", Now()))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@customer_id", CustomerID))
            'Execute the Query
            SQLConn.cmd.ExecuteNonQuery()

            'Update Printcard prefix
            SQLConn.sql = "UPDATE customer SET " & _
                          " printcard_prefix=@printcard_prefix, " & _
                          " industry_type_id=@industry_type_id " & _
                           " WHERE contact_id=@customer_id"

            'Initialize SqlCommand object for update. 
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            'Add the values                            
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@printcard_prefix", PrintcardPrefix))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@industry_type_id", IndustryTypeID))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@customer_id", CustomerID))
            'Execute the Query
            SQLConn.cmd.ExecuteNonQuery()

            SQLConn.conn.Close()
            SQLConn.conn.ClearPool()
        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & " Error in EditCustomer, ClassManageCustomer form!", MsgBoxStyle.Critical, "TSD Inventory System")
        End Try
    End Sub
    Private Function GetIndustryID(ByVal IndustryType As String) As Integer
        Dim id As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT id FROM industry WHERE industry_type='" & IndustryType & "'"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        Return id
    End Function
    Public Sub SetCustomerStatus(ByVal StatusType As String, ByVal CustomerID As Integer)
        Try
            If StatusType <> "Active" Then
                If StatusType <> "Inactive" Then
                    Throw New System.Exception("An exception has occurred. Parameter not allowed, either 'Active' or 'Inactive' entry is allowed in function SetCustomerStatus(StatusType, CustomerID)")
                End If
            End If

            If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
            SQLConn.sql = "UPDATE customer SET current_status=@current_status " & _
                           " WHERE contact_id=@customer_id"

            'Initialize SqlCommand object for update. 
            SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
            'Add the values                
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@current_status", StatusType))
            SQLConn.cmd.Parameters.Add(New NpgsqlParameter("@customer_id", CustomerID))
            'Execute the Query
            SQLConn.cmd.ExecuteNonQuery()

            SQLConn.conn.Close()
            SQLConn.conn.ClearPool()

        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & " Error in procedure SetCustomerStatus @ ClassManageCustomer")
        End Try
    End Sub
    Public Sub DeleteCustomer()

    End Sub
    Public Sub PopulateCustomer(ByVal CustomerGrid As DataGridView, ByVal StatusType As String)
        Try
            If StatusType <> "Active" Then
                If StatusType <> "Inactive" Then
                    Throw New System.Exception("An exception has occurred. Parameter not allowed, either 'Active' or 'Inactive' entry is allowed in function PopulateCustomer(DataGrid, StatusType)")
                End If
            End If
            
            SQLConn.sql = " SELECT contact.id as id,contact.organization_name as tcompanyname, " & _
                        "  contact.street_name as street_name,contact.barangay_name as barangay_name, cities.cityname as cityname, provinces.name as province, " & _
                        "  country.countryname as country,contact.last_name as lastname, contact.first_name as firstname, " & _
                        " contact.office_phone_number as officephone, contact.mobile_phone_number as mobilephone,	" & _
                        " contact.email as email, customer.printcard_prefix as printcard_prefix, industry.industry_type as industry,customer.industry_type_id as industryid" & _
                        " FROM ((tsd_inventory.public.contact contact " & _
                         " INNER JOIN cities ON contact.city_id=cities.id) " & _
                         " INNER JOIN provinces ON cities.province_id=provinces.id " & _
                         " INNER JOIN customer ON contact.id=customer.contact_id " & _
                         " INNER JOIN industry ON customer.industry_type_id=industry.id) " & _
                         " INNER JOIN country ON provinces.country_id=country.id WHERE customer.current_status='" & StatusType & "' AND contact.deleted=0 ORDER BY id ASC;"
            SQLConn.da = New NpgsqlDataAdapter(SQLConn.sql, SQLConn.conn)
            SQLConn.da.Fill(SQLConn.ds, "graphicfiles")
            CustomerGrid.DataSource = SQLConn.ds.Tables("graphicfiles")
        Catch ex As Exception
            MainUI.StatusMessage.Text = ex.Message
            MsgBox(ex.Message & ". Error in PopulateCustomer @ ClassManageCustomer form")
        End Try
    End Sub
    Public Sub GetComboBoxItems(ByVal ComboBox As ComboBox, ByVal TableName As String, ByVal FieldName As String, Optional ByVal FieldID As Integer = 0)
        If FieldID = 0 Then
            SQLConn.sql = "select " & FieldName & " FROM " & TableName
        Else
            SQLConn.sql = "select " & FieldName & " FROM " & TableName & " WHERE id= " & FieldID
        End If
        SQLConn.da = New NpgsqlDataAdapter(SQLConn.sql, SQLConn.conn)
        SQLConn.da.Fill(SQLConn.ds, TableName)
        ComboBox.DataSource = SQLConn.ds.Tables(TableName)
        ComboBox.DisplayMember = FieldName
        SQLConn.da.Dispose()
        SQLConn.ds.Dispose()
        SQLConn.conn.ClearPool()
    End Sub
    Public Sub ListCity(ByVal TextBox1 As TextBox, Optional ByVal ProvinceName As String = "")
        Dim cmd As NpgsqlCommand
        Dim id As Integer = 0
        If ProvinceName = "" Then
            cmd = New NpgsqlCommand("SELECT cityname FROM cities", conn)
        Else
            id = GetProvinceID(ProvinceName)
            cmd = New NpgsqlCommand("SELECT cityname FROM cities WHERE province_id=" & id, conn)
        End If
        Dim ds As New DataSet
        Dim da As New NpgsqlDataAdapter(cmd)
        da.Fill(ds, "city")
        Dim col As New AutoCompleteStringCollection
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            col.Add(ds.Tables(0).Rows(i)("cityname").ToString())
        Next
        TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBox1.AutoCompleteCustomSource = col
        TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest
    End Sub
    Private Function GetProvinceID(ByVal ProvinceName As String) As Integer
        Dim id As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT id FROM provinces WHERE name='" & ProvinceName & "' limit 1"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        Return id
    End Function
    Private Function GetCityID(ByVal CityName As String) As Integer
        Dim id As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT id FROM cities WHERE cityname='" & CityName & "' limit 1"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        Return id
    End Function
    Public Sub ListProvince(ByVal TextBox1 As TextBox, Optional ByVal CountryName As String = "")
        Dim cmd As NpgsqlCommand
        Dim id As Integer = 0
        If CountryName = "" Then
            cmd = New NpgsqlCommand("SELECT name FROM provinces", conn)
        Else
            id = GetCountryID(CountryName)
            cmd = New NpgsqlCommand("SELECT name FROM provinces WHERE country_id=" & id, conn)
        End If
        Dim ds As New DataSet
        Dim da As New NpgsqlDataAdapter(cmd)
        da.Fill(ds, "province")
        Dim col As New AutoCompleteStringCollection
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            col.Add(ds.Tables(0).Rows(i)("name").ToString())
        Next
        TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBox1.AutoCompleteCustomSource = col
        TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest
    End Sub
    Public Sub ListProvincesAccordingToCityName(ByVal TextBox1 As TextBox, ByVal CityName As String)
        Dim cmd As New NpgsqlCommand("SELECT provinces.name as name" & _
                                     " FROM ((cities INNER JOIN provinces ON cities.province_id=provinces.id)) " & _
                                     " WHERE cities.cityname='" & CityName & "'", conn)
        Dim ds As New DataSet
        Dim da As New NpgsqlDataAdapter(cmd)
        da.Fill(ds, "provinces")
        Dim col As New AutoCompleteStringCollection
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            col.Add(ds.Tables(0).Rows(i)("name").ToString())
        Next
        TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBox1.AutoCompleteCustomSource = col
        TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest

    End Sub
    Private Function GetCountryID(ByVal CountryName As String) As Integer
        Dim id As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT id FROM country WHERE countryname='" & CountryName & "' limit 1"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        Return id
    End Function
    Public Sub ListCountry(ByVal TextBox1 As TextBox)
        Dim cmd As New NpgsqlCommand("SELECT countryname FROM country", conn)
        Dim ds As New DataSet
        Dim da As New NpgsqlDataAdapter(cmd)
        da.Fill(ds, "country")
        Dim col As New AutoCompleteStringCollection
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            col.Add(ds.Tables(0).Rows(i)("countryname").ToString())
        Next
        TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBox1.AutoCompleteCustomSource = col
        TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest
    End Sub
    Public Function GetTableID(ByVal TableName As String, ByVal FieldName As String, ByVal FieldSearched As String, ByVal SearchThis As String) As Integer
        Dim id As Integer
        If SQLConn.conn.State = ConnectionState.Closed Then SQLConn.conn.Open()
        SQLConn.sql = "SELECT " & FieldName & " FROM " & TableName & " WHERE " & FieldSearched & "='" & SearchThis & "'"
        SQLConn.cmd = New NpgsqlCommand(SQLConn.sql, SQLConn.conn)
        SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While (SQLConn.dr.Read)
            id = SQLConn.dr.GetValue(0)
        End While
        SQLConn.conn.Close()
        SQLConn.conn.ClearPool()
        Return id
    End Function
End Class
