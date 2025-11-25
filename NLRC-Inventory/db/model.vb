Imports System.Text
Imports MySql.Data.MySqlClient
Imports Mysqlx


Public Class InvDevice
    Public Property Pointer As Integer
    Public Property DevCategoryPointer As Integer?
    Public Property BrandPointer As Integer?
    Public Property Model As String
    Public Property Specs As String
    Public Property Status As String
    Public Property SerialNumber As String

    Public Property NsocName As String
    Public Property PropertyNumber As String
    Public Property PurchaseDate As Date?
    Public Property WarrantyExpires As Date?
    Public Property Cost As Decimal?
    Public Property Notes As String
    Public Property CreatedAt As Date
    Public Property UpdatedAt As Date
    Public Property CreatedBy As Integer
    Public Property UpdatedBy As Integer
End Class


Public Class DeviceCategory
    Public Property Pointer As Integer
    Public Property CategoryName As String
    Public Property Description As String
    Public Property CreatedAt As Date
    Public Property UpdatedAt As Date
End Class

Public Class Brand
    Public Property Pointer As Integer
    Public Property BrandName As String
End Class

Public Class DeviceSpecification
    Public Property Pointer As Integer
    Public Property SpecName As String


End Class


Public Class specs
    Public Property Pointer As Integer

    Public Property Category_Id As String
    Public Property Specs As String
End Class

Public Class Session
    Public Shared LoggedInUserPointer As Integer
    Public Shared LoggedInRole As String
End Class

Public Class CategoryProperty
    Public Property Pointer As Integer
    Public Property CategoryPointer As Integer
    Public Property PropertyName As String
    Public Property Required As Boolean
    Public Property Active As Boolean
End Class


Public Class model

    Private ReadOnly connectionString As String = "Server=127.0.0.1;Port=3307;Database=main_nlrc_db;Uid=root;Pwd=;"

    Public Function EncryptPassword(ByVal password As String) As String
        password = password.Trim()
        Dim result As New StringBuilder(password.Length)
        For i As Integer = 1 To password.Length
            result.Append(ChrW(255 - (AscW(password.Chars(i - 1)) + i)))
        Next
        Return result.ToString()
    End Function

    ' ✅ Login Validation Function
    Public Function LoginUser(ByVal username As String, ByVal password As String) As Integer?
        Dim encryptedPass As String = EncryptPassword(password)

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT pointer
                FROM m_useraccounts
                WHERE UAUsername = @username
                  AND UAPassword = @password
                  AND UAActive = 1
                LIMIT 1;"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", encryptedPass)

                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return Convert.ToInt32(result)  ' <-- return the pointer
                    End If
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Database error: " & ex.Message)
        End Try

        Return Nothing
    End Function

    Public Function GetRoleByPointer(userPointer As Integer) As String
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                SELECT UAModule
                FROM m_useraccounts
                WHERE pointer = @pointer
                LIMIT 1;"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pointer", userPointer)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        Return result.ToString().Trim().ToUpper()
                    End If
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Database error: " & ex.Message)
        End Try
        Return Nothing
    End Function




    Public Function Save(device As InvDevice, userID As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' ============================
                ' 🔥 Get or Create SPEC POINTER
                ' ============================
                Dim specsPointer As Integer = GetSpecsPointer(device.Specs, device.DevCategoryPointer, conn)


                Dim query As String

                If device.Pointer > 0 Then
                    ' UPDATE
                    query = "UPDATE inv_devices 
                         SET dev_category_pointer=@category,
                             brands=@brand,
                             model=@model,
                             specs=@specsPointer,
                             status=@status,
                             nsoc_name=@nsoc,
                             serial_number=@serial,
                             property_number=@property,
                             purchase_date=@purchase,
                             warranty_expires=@warranty,
                             notes=@notes,
                             updated_at=NOW(),
                             updated_by=@userID
                         WHERE pointer=@pointer"
                Else
                    ' INSERT
                    query = "INSERT INTO inv_devices 
                         (dev_category_pointer, brands, model, specs, status, nsoc_name, serial_number, property_number, purchase_date, warranty_expires, notes, created_at, updated_at, created_by, updated_by)
                         VALUES (@category, @brand, @model, @specsPointer, @status, @nsoc, @serial, @property, @purchase, @warranty, @notes, NOW(), NOW(), @userID, @userID)"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pointer", device.Pointer)
                    cmd.Parameters.AddWithValue("@category", device.DevCategoryPointer)
                    cmd.Parameters.AddWithValue("@brand", device.BrandPointer)
                    cmd.Parameters.AddWithValue("@model", device.Model)
                    cmd.Parameters.AddWithValue("@nsoc", device.NsocName)
                    cmd.Parameters.AddWithValue("@specsPointer", specsPointer)
                    cmd.Parameters.AddWithValue("@status", device.Status)
                    cmd.Parameters.AddWithValue("@serial", device.SerialNumber)
                    cmd.Parameters.AddWithValue("@property", device.PropertyNumber)
                    cmd.Parameters.AddWithValue("@purchase", device.PurchaseDate)
                    cmd.Parameters.AddWithValue("@warranty", device.WarrantyExpires)
                    cmd.Parameters.AddWithValue("@notes", device.Notes)
                    cmd.Parameters.AddWithValue("@userID", userID)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show("Database save error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Function GetSpecsPointer(specText As String, categoryId As Integer, conn As MySqlConnection) As Integer
        ' CHECK IF SPECS ALREADY EXIST
        Dim checkSql As String = "SELECT pointer FROM inv_specs WHERE specs=@spec AND category_id=@cat LIMIT 1"
        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@spec", specText)
            cmd.Parameters.AddWithValue("@cat", categoryId)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return CInt(result)
            End If
        End Using

        ' INSERT NEW SPECS WITH CATEGORY ID
        Dim insertSql As String = "INSERT INTO inv_specs(category_id, specs) VALUES(@cat, @spec)"
        Using cmd As New MySqlCommand(insertSql, conn)
            cmd.Parameters.AddWithValue("@cat", categoryId)
            cmd.Parameters.AddWithValue("@spec", specText)
            cmd.ExecuteNonQuery()
        End Using

        Dim idCmd As New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
        Return CInt(idCmd.ExecuteScalar())
    End Function



    Public Function GetSpecsByPointer(pointer As Integer) As String
        Dim sql As String = "SELECT specs FROM inv_specs WHERE pointer=@p LIMIT 1"
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@p", pointer)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return result.ToString()
            End Using
        End Using
        Return ""
    End Function


    Public Function GetDeviceStock(pointer As Integer) As Integer
        Dim count As Integer = 0
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                        SELECT 
                            (
                                SELECT COUNT(*) 
                                FROM inv_devices AS d2
                                WHERE d2.brands = d1.brands 
                                  AND d2.model = d1.model 
                                  AND d2.status = 'Working'
                            ) AS total_working_same_model_brand
                        FROM inv_devices AS d1
                        WHERE d1.pointer = @pointer
                          AND d1.status = 'Working'
                        LIMIT 1;
                    "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pointer", pointer)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        count = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching stock count: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return count
    End Function








    Public Function GetDevicesByCategory(categoryPointer As Integer) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                    SELECT 
                        d.pointer,
                        d.dev_category_pointer,
                        CONCAT(b.brand_name, ' - ', d.model) AS display_name,
                        d.status
                    FROM inv_devices d
                    LEFT JOIN inv_brands b ON d.brands = b.pointer
                    WHERE d.dev_category_pointer = @categoryPointer
                    AND d.status = 'Working';"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryPointer", categoryPointer)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading devices: " & ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function

    Public Function GetDevicesByCategory2(categoryPointer As Integer) As DataTable
        Dim dt As New DataTable()

        Dim query As String = "
                SELECT 
            MIN(d.pointer) AS pointer,       -- pick one pointer for display
            b.brand_name AS brands,
            d.model,
            d.status,
            COUNT(*) AS total_devices,
            d.dev_category_pointer,
            d.specs                            -- Add the specs column here
        FROM inv_devices d
        LEFT JOIN inv_brands b ON d.brands = b.pointer
        WHERE d.dev_category_pointer = @categoryPointer
          AND d.status = 'Working'            -- <-- only available devices
        GROUP BY b.brand_name, d.model, d.status, d.dev_category_pointer, d.specs
        ORDER BY b.brand_name, d.model;

    "

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryPointer", categoryPointer)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading devices: " & ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function









    Public Function GetCategories() As List(Of DeviceCategory)
        Dim categories As New List(Of DeviceCategory)()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT pointer, category_name FROM inv_device_category"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            categories.Add(New DeviceCategory With {
                                .Pointer = reader.GetInt32("pointer"),
                                .CategoryName = reader.GetString("category_name")
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return categories
    End Function

    ' ========================
    ' 🔹 GET CATEGORY NAME
    ' ========================
    Public Function GetCategoryName(categoryPointer As Integer?) As String
        If categoryPointer Is Nothing Then Return ""
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand("SELECT category_name FROM inv_device_category WHERE pointer=@pointer", conn)
                    cmd.Parameters.AddWithValue("@pointer", categoryPointer)
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing, result.ToString(), "")
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching category name: " & ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function




    Public Function GetBrands() As List(Of Brand)
        Dim brands As New List(Of Brand)()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT pointer, brand_name FROM inv_brands ORDER BY brand_name"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            brands.Add(New Brand With {
                                    .Pointer = reader.GetInt32("pointer"),
                                    .BrandName = reader.GetString("brand_name")
                                })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading brands: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return brands
    End Function

    Public Function GetBrandObjects() As List(Of Brand)
        Dim brands As New List(Of Brand)()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT pointer, brand_name FROM inv_brands ORDER BY brand_name"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            brands.Add(New Brand With {
                                .Pointer = reader.GetInt32("pointer"),
                                .BrandName = reader.GetString("brand_name")
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading brands: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return brands
    End Function


    Public Function GetDevices() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String =
        "SELECT 
            d.pointer AS DevicePointer,

            -- NSOC NAME DIRECTLY FROM inv_devices
            d.nsoc_name AS `NSOC Name`,

            -- keep these for filtering
            c.category_name AS Category,
            b.brand_name AS Brand,

            -- COMBINED DEVICE COLUMN: CATEGORY - BRAND - MODEL
            CONCAT(
                IFNULL(c.category_name, ''), ' - ',
                IFNULL(b.brand_name, ''), ' - ',
                IFNULL(d.model, '')
            ) AS `Device`,

            -- PROPERTY NUMBER BETWEEN DEVICE AND SPECS
            d.property_number AS `Property Number`,

            -- specs from your specs table (as before)
            s.specs AS Specifications,  
            d.status AS Status,
            d.serial_number AS `Serial Number`,
            d.purchase_date AS `Purchase Date`,
            d.warranty_expires AS `Warranty Expires`

        FROM inv_devices d
        LEFT JOIN inv_device_category c ON d.dev_category_pointer = c.pointer
        LEFT JOIN inv_brands b ON d.brands = b.pointer
        LEFT JOIN inv_specs s ON d.specs = s.pointer
        ORDER BY d.pointer;"

                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading devices: " & ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function




    Public Function GetDeviceByPointer(pointer As Integer) As InvDevice
        Dim device As New InvDevice()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                SELECT 
                    d.pointer,
                    d.dev_category_pointer,
                    d.brands,
                    d.model,
                    d.serial_number,
                    d.property_number,
                    d.nsoc_name,
                    d.specs,
                    d.status,
                    d.purchase_date,
                    d.warranty_expires,
                    d.notes
                FROM inv_devices d
                WHERE d.pointer = @pointer
            "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pointer", pointer)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            device.Pointer = reader("pointer")
                            device.DevCategoryPointer = reader("dev_category_pointer")
                            device.BrandPointer = If(IsDBNull(reader("brands")), 0, reader("brands"))
                            device.Model = If(IsDBNull(reader("model")), "", reader("model").ToString())
                            device.SerialNumber = If(IsDBNull(reader("serial_number")), "", reader("serial_number").ToString())
                            device.PropertyNumber = If(IsDBNull(reader("property_number")), "", reader("property_number").ToString())
                            device.NsocName = If(IsDBNull(reader("nsoc_name")), "", reader("nsoc_name").ToString())
                            device.Specs = If(IsDBNull(reader("specs")), "", reader("specs").ToString())
                            device.Status = If(IsDBNull(reader("status")), "", reader("status").ToString())
                            device.PurchaseDate = If(IsDBNull(reader("purchase_date")), Nothing, reader("purchase_date"))
                            device.WarrantyExpires = If(IsDBNull(reader("warranty_expires")), Nothing, reader("warranty_expires"))
                            device.Notes = If(IsDBNull(reader("notes")), "", reader("notes").ToString())
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching device: " & ex.Message)
        End Try
        Return device
    End Function





    Public Function DeleteDevice(deviceId As Integer) As Boolean
        Dim success As Boolean = False
        Dim query As String = "DELETE FROM inv_devices WHERE pointer = @deviceId"

        Try
            Using conn As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@deviceId", deviceId)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                    success = True
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error deleting device: " & ex.Message)
        End Try

        Return success
    End Function

    Public Function GetDeviceById(deviceId As Integer) As InvDevice
        Dim device As New InvDevice()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT * FROM inv_devices WHERE pointer = @deviceId"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@deviceId", deviceId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            device.Pointer = Convert.ToInt32(reader("pointer"))
                            device.DevCategoryPointer = Convert.ToInt32(reader("dev_category_pointer"))

                            device.SerialNumber = reader("serial_number").ToString()
                            device.PropertyNumber = reader("property_number").ToString()
                            device.NsocName = reader("nsoc_name").ToString()

                            device.BrandPointer = Convert.ToInt32(reader("brands"))
                            device.Model = reader("model").ToString()
                            device.Specs = reader("specs").ToString()
                            device.Status = reader("status").ToString()

                            device.PurchaseDate = If(IsDBNull(reader("purchase_date")), Nothing, Convert.ToDateTime(reader("purchase_date")))
                            device.WarrantyExpires = If(IsDBNull(reader("warranty_expires")), Nothing, Convert.ToDateTime(reader("warranty_expires")))
                            device.Cost = If(IsDBNull(reader("cost")), Nothing, Convert.ToDecimal(reader("cost")))

                            device.Notes = reader("notes").ToString()

                            device.CreatedAt = Convert.ToDateTime(reader("created_at"))
                            device.UpdatedAt = Convert.ToDateTime(reader("updated_at"))
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error retrieving device by ID: " & ex.Message)
        End Try

        Return device
    End Function





    Public Function GetSpecificationsForCategory(categoryPointer As Integer) As List(Of DeviceSpecification)
        Dim specifications As New List(Of DeviceSpecification)()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT pointer, specs FROM inv_specs WHERE category_id = @categoryPointer"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryPointer", categoryPointer)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            specifications.Add(New DeviceSpecification With {
                                .Pointer = reader.GetInt32("pointer"),
                                .SpecName = reader.GetString("specs")
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading specifications: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return specifications
    End Function


    Public Function GetSpecPointerByName(specName As String) As Integer?
        Dim pointer As Integer? = Nothing

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT pointer FROM inv_specs WHERE spec_name = @specName LIMIT 1"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@specName", specName)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        pointer = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting spec pointer: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return pointer
    End Function


    Public Function GetBrandsByCategory(categoryPointer As Integer) As List(Of Brand)
        Dim brands As New List(Of Brand)()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT pointer, brand_name FROM inv_brands WHERE category_pointer = @categoryPointer ORDER BY brand_name"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryPointer", categoryPointer)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            brands.Add(New Brand With {
                            .Pointer = reader.GetInt32("pointer"),
                            .BrandName = reader.GetString("brand_name")
                        })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading brands: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return brands
    End Function




    ' =============================
    ' UNITS FUNCTIONS
    ' =============================

    ' Function to get assigned personnel
    Public Function GetAssignments() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;Port=3307;Database=db_nlrc_intranet;Uid=root;Pwd=;")
                conn.Open()
                Dim query As String = "SELECT user_id, CONCAT(LAST_M, ', ', FIRST_M) AS `Full name` FROM user_info ORDER BY LAST_M, FIRST_M;"
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading assigned personnel: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function










    Public Function GetDevicesForUnits() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                SELECT 
                d.pointer AS device_id,
                CONCAT(c.category_name, ' - ', b.brand_name, ' - ', d.model) AS Device,
                d.specs,
                d.status
            FROM inv_devices d
            INNER JOIN inv_brands b ON b.pointer = d.brands
            INNER JOIN inv_device_category c ON c.pointer = d.dev_category_pointer
            WHERE d.status = 'Working';
            "

                Using da As New MySqlDataAdapter(query, conn)
                    da.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading devices: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function




    ' Function to save a new unit and link to a device using pointer
    Public Function SaveUnit(unitName As String,
                         assignedPersonnel As Integer?,
                         devicePointers As List(Of Integer),
                         remarks As String,
                         createdBy As Integer) As Boolean
        Try
            If String.IsNullOrWhiteSpace(unitName) OrElse devicePointers Is Nothing OrElse devicePointers.Count = 0 Then
                MessageBox.Show("Unit Name or devices are missing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using transaction = conn.BeginTransaction()

                    ' ============================
                    ' 1. INSERT UNIT
                    ' ============================
                    Dim unitId As Integer = 0
                    Dim insertUnitQuery As String =
                    "INSERT INTO inv_units (unit_name, assigned_personnel, remarks, status, created_at) " &
                    "VALUES (@unitName, @assigned, @remarks, 'Active', NOW()); " &
                    "SELECT LAST_INSERT_ID();"

                    Using cmd As New MySqlCommand(insertUnitQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@unitName", unitName)
                        cmd.Parameters.AddWithValue("@assigned", If(assignedPersonnel.HasValue, CType(assignedPersonnel.Value, Object), DBNull.Value))
                        cmd.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(remarks), CType(DBNull.Value, Object), remarks))
                        unitId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    ' ============================
                    ' 2. INSERT ASSIGNED HISTORY ⭐
                    ' ============================
                    If assignedPersonnel.HasValue Then
                        Dim insertHistoryQuery As String =
                        "INSERT INTO inv_assigned_history " &
                        "    (units_pointer, assigned_from, date_assigned, assigned_to, remarks, created_by) " &
                        "VALUES (@unitId, @from, NOW(), @to, @remarks, @by);"

                        Using cmdHist As New MySqlCommand(insertHistoryQuery, conn, transaction)
                            cmdHist.Parameters.AddWithValue("@unitId", unitId)
                            ' first assignment, so we don't have an old person → NULL
                            cmdHist.Parameters.AddWithValue("@from", DBNull.Value)
                            cmdHist.Parameters.AddWithValue("@to", assignedPersonnel.Value)
                            cmdHist.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(remarks), CType(DBNull.Value, Object), remarks))
                            cmdHist.Parameters.AddWithValue("@by", createdBy)
                            cmdHist.ExecuteNonQuery()
                        End Using
                    End If

                    ' ============================
                    ' 3. LINK DEVICES + UPDATE STATUS
                    ' ============================
                    For Each devicePointer In devicePointers
                        ' Check if device exists and is 'Working'
                        Dim checkDeviceQuery As String = "SELECT status FROM inv_devices WHERE pointer=@devicePointer FOR UPDATE;"
                        Using checkCmd As New MySqlCommand(checkDeviceQuery, conn, transaction)
                            checkCmd.Parameters.AddWithValue("@devicePointer", devicePointer)
                            Dim status As Object = checkCmd.ExecuteScalar()
                            If status Is Nothing OrElse status.ToString().ToLower() <> "working" Then
                                MessageBox.Show($"Device {devicePointer} is invalid or not working.", "Invalid Device", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                transaction.Rollback()
                                Return False
                            End If
                        End Using

                        ' Insert into inv_unit_devices
                        Dim insertLinkQuery As String =
                        "INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_at) " &
                        "VALUES (@unitId, @devicePointer, NOW());"
                        Using cmdLink As New MySqlCommand(insertLinkQuery, conn, transaction)
                            cmdLink.Parameters.AddWithValue("@unitId", unitId)
                            cmdLink.Parameters.AddWithValue("@devicePointer", devicePointer)
                            cmdLink.ExecuteNonQuery()
                        End Using

                        ' Update device status
                        Dim updateDeviceQuery As String = "UPDATE inv_devices SET status='Assigned' WHERE pointer=@devicePointer;"
                        Using cmdUpdate As New MySqlCommand(updateDeviceQuery, conn, transaction)
                            cmdUpdate.Parameters.AddWithValue("@devicePointer", devicePointer)
                            cmdUpdate.ExecuteNonQuery()
                        End Using
                    Next

                    transaction.Commit()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show("Error saving unit: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    Public Function GetBrandPointerByName(brandName As String, categoryPointer As Integer) As Integer
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            Dim query As String = "
            SELECT pointer 
            FROM inv_brands 
            WHERE brand_name = @name 
              AND category_pointer = @category
            LIMIT 1;
        "

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", brandName)
                cmd.Parameters.AddWithValue("@category", categoryPointer)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return Convert.ToInt32(result)
                Else
                    Throw New Exception($"Brand not found: {brandName} (Category ID {categoryPointer})")
                End If
            End Using
        End Using
    End Function



    Public Function SaveInvUnitDevices(selectedDevices As DataTable, quantity As Integer, remark As String) As Boolean
        If selectedDevices Is Nothing OrElse selectedDevices.Rows.Count = 0 Then
            MessageBox.Show("No devices to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If quantity <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using trans As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' Loop for the number of units to create
                        For u As Integer = 1 To quantity
                            ' Insert a unit
                            Dim insertUnitCmd As New MySqlCommand("
                            INSERT INTO inv_units (unit_name, remarks, status, created_by, created_at)
                            VALUES (NULL, @remark, 'Active', @created_by, NOW());
                            SELECT LAST_INSERT_ID();
                        ", conn, trans)
                            insertUnitCmd.Parameters.AddWithValue("@remark", If(String.IsNullOrWhiteSpace(remark), DBNull.Value, remark))
                            insertUnitCmd.Parameters.AddWithValue("@created_by", 1)
                            Dim unitId As Integer = Convert.ToInt32(insertUnitCmd.ExecuteScalar())

                            ' Loop through selected devices
                            For Each row As DataRow In selectedDevices.Rows
                                Dim brandPointer As Integer = Convert.ToInt32(row("brands"))
                                Dim model As String = row("model").ToString().Trim()

                                ' Get one available device (matching brand, model, and category)
                                Dim getDeviceCmd As New MySqlCommand("
                                SELECT d.pointer
                                FROM inv_devices d
                                INNER JOIN inv_brands b ON d.brands = b.pointer
                                WHERE d.brands = @brand 
                                  AND d.model = @model 
                                  AND d.status = 'Working'
                                  AND d.dev_category_pointer = b.category_pointer
                                LIMIT 1
                            ", conn, trans)
                                getDeviceCmd.Parameters.AddWithValue("@brand", brandPointer)
                                getDeviceCmd.Parameters.AddWithValue("@model", model)
                                Dim deviceObj = getDeviceCmd.ExecuteScalar()

                                If deviceObj Is Nothing Then
                                    Dim brandName As String = GetBrandName(brandPointer)
                                    Throw New Exception($"Not enough stock for Brand {brandName}, Model {model}.")
                                End If

                                Dim devicePointer As Integer = Convert.ToInt32(deviceObj)

                                ' Insert into inv_unit_devices
                                Dim insertLinkCmd As New MySqlCommand("
                                INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_at, created_by)
                                VALUES (@unitId, @devicePointer, NOW(), @created_by)
                            ", conn, trans)
                                insertLinkCmd.Parameters.AddWithValue("@unitId", unitId)
                                insertLinkCmd.Parameters.AddWithValue("@devicePointer", devicePointer)
                                insertLinkCmd.Parameters.AddWithValue("@created_by", 1)
                                insertLinkCmd.ExecuteNonQuery()

                                ' Mark device as Assigned
                                Dim updateDeviceCmd As New MySqlCommand("
                                UPDATE inv_devices 
                                SET status = 'Assigned', updated_by = @updated_by, updated_at = NOW() 
                                WHERE pointer = @devicePointer
                            ", conn, trans)
                                updateDeviceCmd.Parameters.AddWithValue("@updated_by", 1)
                                updateDeviceCmd.Parameters.AddWithValue("@devicePointer", devicePointer)
                                updateDeviceCmd.ExecuteNonQuery()
                            Next
                        Next

                        trans.Commit()
                        Return True
                    Catch ex As Exception
                        trans.Rollback()
                        MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Connection error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    ' Helper to fetch brand name by ID
    Public Function GetBrandName(brandId As Integer) As String
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT brand_name FROM inv_brands WHERE pointer = @id LIMIT 1", conn)
            cmd.Parameters.AddWithValue("@id", brandId)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return result.ToString()
            End If
            Return brandId.ToString() ' fallback
        End Using
    End Function















    Public Function SaveUnit1(unitName As String, assignedPersonnel As Integer?, devicePointers As List(Of Integer), remarks As String) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using transaction = conn.BeginTransaction()

                    ' 1️⃣ Insert new unit into inv_units
                    Dim unitId As Integer
                    Dim insertUnitQuery As String = "
                    INSERT INTO inv_units (unit_name, assigned_personnel, remarks, status, created_at)
                    VALUES (@unitName, @assigned, @remarks, 'Active', NOW());
                    SELECT LAST_INSERT_ID();"
                    Using cmd As New MySqlCommand(insertUnitQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@unitName", If(String.IsNullOrWhiteSpace(unitName), DBNull.Value, unitName))
                        cmd.Parameters.AddWithValue("@assigned", If(assignedPersonnel.HasValue, assignedPersonnel.Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(remarks), DBNull.Value, remarks))
                        unitId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    ' 2️⃣ Insert each selected device into inv_unit_devices and update its status
                    For Each pointer As Integer In devicePointers
                        ' Insert link record
                        Dim insertLinkQuery As String = "
                        INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_at)
                        VALUES (@unitId, @devicePointer, NOW());"
                        Using cmdLink As New MySqlCommand(insertLinkQuery, conn, transaction)
                            cmdLink.Parameters.AddWithValue("@unitId", unitId)
                            cmdLink.Parameters.AddWithValue("@devicePointer", pointer)
                            cmdLink.ExecuteNonQuery()
                        End Using

                        ' Update device status to Assigned
                        Dim updateDeviceQuery As String = "
                        UPDATE inv_devices 
                        SET status='Assigned' 
                        WHERE pointer=@devicePointer;"
                        Using cmdUpdate As New MySqlCommand(updateDeviceQuery, conn, transaction)
                            cmdUpdate.Parameters.AddWithValue("@devicePointer", pointer)
                            cmdUpdate.ExecuteNonQuery()
                        End Using
                    Next

                    transaction.Commit()
                    Return True
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error saving unit: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    ' Helper to get existing unit pointer by name
    Private Function GetUnitPointerByName(unitName As String) As Integer
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand("SELECT pointer FROM inv_units WHERE unit_name=@name LIMIT 1;", conn)
                cmd.Parameters.AddWithValue("@name", unitName.Trim())
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function





    ' Check if the device pointer is valid and the status is 'Working'
    Public Function IsDeviceValid(devicePointer As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT status FROM inv_devices WHERE pointer=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", devicePointer)
                    Dim status As Object = cmd.ExecuteScalar()
                    If status Is Nothing Then Return False
                    ' Trim and compare ignoring case
                    Return String.Equals(status.ToString().Trim(), "Working", StringComparison.OrdinalIgnoreCase)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking device: " & ex.Message)
            Return False
        End Try
    End Function


    Public Function GetAvailableDevicesByCategory(categoryPointer As Integer) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    d.pointer AS device_id,
                    CONCAT(d.model, ' - ', b.brand_name) AS device_name
                FROM inv_devices d
                INNER JOIN inv_device_category c ON d.dev_category_pointer = c.pointer
                INNER JOIN inv_brands b ON d.brands = b.pointer
                WHERE d.dev_category_pointer = @categoryPointer
                  AND d.status = 'Working';"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryPointer", categoryPointer)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading devices: " & ex.Message)
        End Try
        Return dt
    End Function





    Public Function GetUnits() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                    SELECT 
                        u.pointer AS unit_id,
                        u.unit_name AS `UnitName`,
                        CONCAT('(', dc.category_name, ') ', b.brand_name, ' ', d.model) AS `Device`,
                        CONCAT(i.LAST_M, ', ', i.FIRST_M) AS `Assigned To`,
                        u.remarks AS `Remarks`,
                        u.created_at AS `Created`
                    FROM inv_units AS u
                    INNER JOIN inv_unit_devices AS ud ON u.pointer = ud.inv_units_pointer
                    INNER JOIN inv_devices AS d ON ud.inv_devices_pointer = d.pointer
                    INNER JOIN inv_device_category AS dc ON dc.pointer = d.dev_category_pointer
                    INNER JOIN inv_brands AS b ON b.pointer = d.brands
                    INNER JOIN db_nlrc_intranet.user_info AS i ON u.assigned_personnel = i.user_id
                    ORDER BY u.pointer DESC;"

                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching units: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function




    Public Function GetUnitsSummary() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    u.pointer AS unit_id,
                    u.unit_name AS `Unit Name`,
                    COUNT(DISTINCT ud.inv_devices_pointer) AS `Device No`,
                    CONCAT(i.LAST_M, ', ', i.FIRST_M) AS `Assigned To`,
                    i.user_id AS personnel_id,
                    DATE_FORMAT(u.created_at, '%Y-%m-%d') AS `Created At`,
                    DATE_FORMAT(u.updated_at, '%Y-%m-%d') AS `Updated At`
                FROM inv_units AS u
                LEFT JOIN inv_unit_devices AS ud 
                    ON u.pointer = ud.inv_units_pointer
                LEFT JOIN db_nlrc_intranet.user_info AS i 
                    ON u.assigned_personnel = i.user_id
                GROUP BY u.pointer
                ORDER BY u.pointer DESC;
            "


                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching unit summary: " & ex.Message)
        End Try

        Return dt
    End Function


    Public Function GetDeviceSpecsByUnitPointer(unitPointer As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    u.pointer AS UnitID,
                    ud.inv_devices_pointer AS DeviceID,
                    d.nsoc_name,
                    d.property_number,

                    CONCAT(
                        c.category_name, ' - ',
                        b.brand_name, ' - ',
                        d.model, ' - ',
                        IFNULL(s.specs, 'No specs available')
                    ) AS DeviceAndSpecs

                FROM inv_units AS u
                JOIN inv_unit_devices AS ud 
                    ON u.pointer = ud.inv_units_pointer
                JOIN inv_devices AS d
                    ON ud.inv_devices_pointer = d.pointer
                LEFT JOIN inv_brands AS b
                    ON d.brands = b.pointer
                LEFT JOIN inv_specs AS s
                    ON d.specs = s.pointer
                LEFT JOIN inv_device_category AS c
                    ON d.dev_category_pointer = c.pointer

                WHERE u.pointer = @unitPointer
                ORDER BY d.pointer;
            "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@unitPointer", unitPointer)

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error retrieving device specs: " & ex.Message)
        End Try

        Return dt
    End Function


    '




    Public Function GetDevicesByUnitPointer(unitPointer As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    u.inv_units_pointer AS UnitID,
                    d.pointer AS DeviceID,

                    -- New fields your UI requires
                    c.category_name,
                    b.brand_name,
                    d.model,
                    d.specs AS device_specs,
                    d.nsoc_name,
                    d.property_number,

                    CONCAT(
                        c.category_name, ' - ',
                        b.brand_name, ' - ',
                        d.model, ' - ',
                        d.specs
                    ) AS DeviceAndSpecs

                FROM inv_unit_devices u
                INNER JOIN inv_devices d 
                    ON d.pointer = u.inv_devices_pointer
                INNER JOIN inv_brands b 
                    ON b.pointer = d.brands
                INNER JOIN inv_device_category c 
                    ON c.pointer = d.dev_category_pointer

                WHERE u.inv_units_pointer = @unitPointer;

            "

                Using da As New MySqlDataAdapter(query, conn)
                    da.SelectCommand.Parameters.AddWithValue("@unitPointer", unitPointer)
                    da.Fill(dt)
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching devices by unit pointer: " & ex.Message)
        End Try

        Return dt
    End Function





    ' ✅ Update device status
    Public Function UpdateDeviceStatus(deviceId As Integer, newStatus As String) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "UPDATE inv_devices SET status = @status WHERE pointer = @id;"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@id", deviceId)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating device status: " & ex.Message)
            Return False
        End Try
    End Function


    Public Function GetDevicesByUnit(unitName As String, personnelID As Integer) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    d.pointer AS DeviceID,
                    dc.category_name AS Category,
                    b.brand_name AS Brand,
                    d.model AS Model,
                    d.specs AS Specs,
                    d.status AS Status
                FROM inv_units AS u
                LEFT JOIN inv_unit_devices AS ud ON u.pointer = ud.inv_units_pointer
                INNER JOIN inv_devices d ON ud.inv_devices_pointer = d.pointer
                LEFT JOIN inv_device_category dc ON d.dev_category_pointer = dc.pointer
                LEFT JOIN inv_brands b ON d.brands = b.pointer
                WHERE u.unit_name = @unitName AND u.assigned_personnel = @personnelID;
            "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@unitName", unitName)
                    cmd.Parameters.AddWithValue("@personnelID", personnelID)

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching devices: " & ex.Message)
        End Try

        Return dt
    End Function


    Public Function IsUnitNameExists(unitName As String) As Boolean
        If String.IsNullOrWhiteSpace(unitName) Then Return False ' Nothing cannot exist

        Dim exists As Boolean = False
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT COUNT(*) FROM inv_units WHERE unit_name = @name"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", unitName.Trim())
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    exists = (count > 0)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking Unit Name: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return exists
    End Function


    Public Function InsertUnitAndGetPointer(unitName As String) As Integer
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand("INSERT INTO inv_units (unit_name) VALUES (@name); SELECT LAST_INSERT_ID();", conn)
                cmd.Parameters.AddWithValue("@name", If(unitName, DBNull.Value))
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Sub InsertUnitDevices(unitPointer As Integer, deviceId As Integer, quantity As Integer, remarks As String)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using trans = conn.BeginTransaction()
                Using cmd As New MySqlCommand("INSERT INTO inv_unit_devices (unit_pointer, device_id, remarks) VALUES (@unit, @device, @remarks)", conn, trans)
                    cmd.Parameters.Add("@unit", MySqlDbType.Int32).Value = unitPointer
                    cmd.Parameters.Add("@device", MySqlDbType.Int32).Value = deviceId
                    cmd.Parameters.Add("@remarks", MySqlDbType.VarChar).Value = remarks

                    For i As Integer = 1 To quantity
                        cmd.ExecuteNonQuery()
                    Next
                End Using
                trans.Commit()
            End Using
        End Using
    End Sub


    ' Method to insert a new Device Category
    Public Function InsertDeviceCategory(categoryName As String, description As String, createdBy As Integer) As Boolean
        Dim query As String = "INSERT INTO inv_device_category (category_name, description, created_by) " &
                              "VALUES (@categoryName, @description, @createdBy)"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryName", categoryName)
                    cmd.Parameters.AddWithValue("@description", description)
                    cmd.Parameters.AddWithValue("@createdBy", createdBy)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            ' Handle any errors that occur during the insert
            MessageBox.Show("Error: " & ex.Message)
            Return False
        End Try
    End Function


    ' Method to retrieve device categories and populate the DataGridView
    Public Function GetDeviceCategories() As List(Of DeviceCategory)
        Dim categories As New List(Of DeviceCategory)()
        Dim query As String = "SELECT pointer, category_name, description, created_at, updated_at FROM inv_device_category ORDER BY pointer DESC"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ' Create a new DeviceCategory object and populate it
                            Dim category As New DeviceCategory() With {
                                .Pointer = reader.GetInt32("pointer"),
                                .CategoryName = reader.GetString("category_name"),
                                .Description = reader.GetString("description"),
                                .CreatedAt = reader.GetDateTime("created_at"),
                                .UpdatedAt = reader.GetDateTime("updated_at")
                            }

                            categories.Add(category)
                        End While
                    End Using
                End Using
            End Using

            Return categories
        Catch ex As Exception
            ' Handle any errors that occur during data retrieval
            MessageBox.Show("Error: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ' === Get all categories for ComboBox ===
    Public Function GetAllCategories() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT pointer, category_name FROM inv_device_category ORDER BY category_name ASC"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message)
        End Try

        Return dt
    End Function

    Public Function IsCategoryExists(categoryName As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM inv_device_category WHERE category_name = @categoryName"
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@categoryName", categoryName)
                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function



    ' === Insert new Brand ===
    Public Function InsertBrand(categoryPointer As Integer, brandName As String, createdBy As Integer) As Boolean
        ' Check if the brand already exists
        If BrandExists(categoryPointer, brandName) Then
            MessageBox.Show("This brand already exists in the selected category.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' Proceed with the insert if the brand does not exist
        Dim query As String = "INSERT INTO inv_brands (brand_name, created_by, category_pointer) " &
                          "VALUES (@brand_name, @created_by, @category_pointer)"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@brand_name", brandName)
                    cmd.Parameters.AddWithValue("@created_by", createdBy)
                    cmd.Parameters.AddWithValue("@category_pointer", categoryPointer)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Failed inserting brand: " & ex.Message)
            Return False
        End Try
    End Function

    ' === Check if Brand already exists in the selected Category ===
    Public Function BrandExists(categoryPointer As Integer, brandName As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM inv_brands WHERE category_pointer = @categoryPointer AND brand_name = @brandName"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@categoryPointer", categoryPointer)
                    cmd.Parameters.AddWithValue("@brandName", brandName)

                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking brand existence: " & ex.Message)
            Return False
        End Try
    End Function

    ' === Get all brands with category names for DataGridView ===
    Public Function GetAllBrands() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT b.pointer, c.category_name, b.brand_name, b.created_at, b.updated_at, b.category_pointer " &
                          "FROM inv_brands b " &
                          "LEFT JOIN inv_device_category c ON b.category_pointer = c.pointer " &
                          "ORDER BY b.pointer DESC, c.category_name, b.brand_name"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading brands: " & ex.Message)
        End Try

        Return dt
    End Function




    ' === Insert specs into inv_specs ===
    Public Function InsertSpecs(categoryId As Integer, specsText As String, createdBy As Integer) As Boolean
        Dim query As String = "INSERT INTO inv_specs (category_id, specs, created_by) VALUES (@category_id, @specs, @created_by)"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@category_id", categoryId)
                    cmd.Parameters.AddWithValue("@specs", specsText)
                    cmd.Parameters.AddWithValue("@created_by", createdBy)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Error inserting specs: " & ex.Message)
            Return False
        End Try
    End Function


    ' === Get all specs (with category name, newest first) ===
    Public Function GetAllSpecs() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT s.pointer, c.category_name, s.specs, s.created_at, s.updated_at, s.category_id AS category_pointer " &
                          "FROM inv_specs s " &
                          "LEFT JOIN inv_device_category c ON s.category_id = c.pointer " &
                          "ORDER BY s.pointer DESC;"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading specs: " & ex.Message)
        End Try

        Return dt
    End Function


    Public Function UpdateCategory(categoryId As Integer, categoryName As String, description As String) As Boolean
        Dim query As String = "UPDATE device_categories SET category_name=@name, description=@desc, updated_at=NOW() WHERE pointer=@id"
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", categoryName)
                cmd.Parameters.AddWithValue("@desc", description)
                cmd.Parameters.AddWithValue("@id", categoryId)
                conn.Open()
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function


    ' === Update Brand ===
    Public Function UpdateBrand(brandId As Integer, categoryPointer As Integer, brandName As String) As Boolean
        Dim query As String = "UPDATE inv_brands SET category_pointer=@cat, brand_name=@name, updated_at=NOW() WHERE pointer=@id"
        Try
            Using conn As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cat", categoryPointer)
                    cmd.Parameters.AddWithValue("@name", brandName)
                    cmd.Parameters.AddWithValue("@id", brandId)
                    conn.Open()
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating brand: " & ex.Message)
            Return False
        End Try
    End Function

    ' === Delete Brand ===
    Public Function DeleteBrand(brandId As Integer) As Boolean
        Dim query As String = "DELETE FROM inv_brands WHERE pointer=@id"
        Try
            Using conn As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", brandId)
                    conn.Open()
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error deleting brand: " & ex.Message)
            Return False
        End Try
    End Function

    ' === Update Specs ===
    Public Function UpdateSpecs(specsId As Integer, categoryId As Integer, combinedSpecs As String) As Boolean
        Dim query As String = "UPDATE inv_specs SET category_id=@cat, specs=@specs, updated_at=NOW() WHERE pointer=@id"
        Try
            Using conn As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cat", categoryId)
                    cmd.Parameters.AddWithValue("@specs", combinedSpecs)
                    cmd.Parameters.AddWithValue("@id", specsId)
                    conn.Open()
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating specs: " & ex.Message)
            Return False
        End Try
    End Function



    ' === Delete Specs ===
    Public Function DeleteSpecs(specsId As Integer) As Boolean
        Dim query As String = "DELETE FROM inv_specs WHERE pointer=@id"
        Try
            Using conn As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", specsId)
                    conn.Open()
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error deleting specs: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function GetSpecsByDevice(deviceId As Integer) As DataTable
        Dim query As String = "SELECT spec_name, spec_value FROM device_specs WHERE device_id=@deviceId"
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@deviceId", deviceId)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Function GetDevicesForEditUnit() As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String =
"
SELECT 
    d.pointer AS device_id,
    d.status,

    -- short name shown in ComboBox
    CONCAT(
        c.category_name, ' - ',
        b.brand_name, ' - ',
        d.model
    ) AS Device,

    -- full text used inside EditUnit flow
    CONCAT(
        c.category_name, ' - ',
        b.brand_name, ' - ',
        d.model, ' - ',
        IFNULL(s.specs, 'No specs available')
    ) AS DeviceAndSpecs

FROM inv_devices d
LEFT JOIN inv_brands b 
    ON b.pointer = d.brands
LEFT JOIN inv_device_category c 
    ON c.pointer = d.dev_category_pointer
LEFT JOIN inv_specs s 
    ON s.pointer = d.specs

WHERE d.status = 'Working'   -- Only available devices

ORDER BY c.category_name, b.brand_name, d.model;
"

                Using da As New MySqlDataAdapter(query, conn)
                    da.Fill(dt)
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("GetDevicesForEditUnit ERROR: " & ex.Message)
        End Try

        Return dt
    End Function

    Public Function InsertUnit(unitName As String, assignedPersonnel As Integer, createdBy As Integer) As Integer
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim cmd As New MySqlCommand("INSERT INTO inv_units (unit_name, assigned_personnel, status, created_by) 
                                         VALUES (@unitName, @assignedPersonnel, 'Active', @createdBy)", conn)
            cmd.Parameters.AddWithValue("@unitName", unitName)
            cmd.Parameters.AddWithValue("@assignedPersonnel", assignedPersonnel)
            cmd.Parameters.AddWithValue("@createdBy", createdBy)

            cmd.ExecuteNonQuery()
            Return Convert.ToInt32(cmd.LastInsertedId)
        End Using
    End Function

    '==========================
    ' INSERT INTO inv_unit_devices
    '==========================
    Public Sub InsertUnitDevice(unitId As Integer, deviceId As Integer, createdBy As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim cmd As New MySqlCommand("INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_by) 
                                         VALUES (@unitId, @deviceId, @createdBy)", conn)
            cmd.Parameters.AddWithValue("@unitId", unitId)
            cmd.Parameters.AddWithValue("@deviceId", deviceId)
            cmd.Parameters.AddWithValue("@createdBy", createdBy)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    '==========================
    ' INSERT INTO inv_unit_history
    '==========================
    Public Sub InsertUnitHistory(deviceId As Integer, unitId As Integer, createdBy As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim cmd As New MySqlCommand("INSERT INTO inv_unit_history (inv_devices_pointer, units_pointer, remarks, created_by) 
                                         VALUES (@deviceId, @unitId, 'Device added to unit', @createdBy)", conn)
            cmd.Parameters.AddWithValue("@deviceId", deviceId)
            cmd.Parameters.AddWithValue("@unitId", unitId)
            cmd.Parameters.AddWithValue("@createdBy", createdBy)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    '==========================
    ' UPDATE inv_devices STATUS
    '==========================
    Public Sub UpdateDeviceStatus(deviceId As Integer, updatedBy As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE inv_devices SET status = 'Assigned', updated_by = @updatedBy 
                                         WHERE pointer = @deviceId", conn)
            cmd.Parameters.AddWithValue("@updatedBy", updatedBy)
            cmd.Parameters.AddWithValue("@deviceId", deviceId)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' In model class
    Public Function UpdateUnitWithDevices(unitId As Integer, unitName As String, assignedPersonnel As Integer, deviceIds As List(Of Integer), updatedBy As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Step 1: Update the unit information
                Dim updateUnitCmd As New MySqlCommand("
            UPDATE inv_units 
            SET unit_name= @unitName, assigned_personnel = @assignedPersonnel, updated_by = @updatedBy 
            WHERE pointer = @unitId", conn)

                updateUnitCmd.Parameters.AddWithValue("@unitId", unitId)
                updateUnitCmd.Parameters.AddWithValue("@unitName", unitName)
                updateUnitCmd.Parameters.AddWithValue("@assignedPersonnel", assignedPersonnel)
                updateUnitCmd.Parameters.AddWithValue("@updatedBy", updatedBy)

                Dim rowsAffected As Integer = updateUnitCmd.ExecuteNonQuery()

                If rowsAffected = 0 Then
                    Return False
                End If

                ' Step 2: Remove devices that are no longer linked to this unit
                Dim removeDevicesCmd As New MySqlCommand("
            DELETE FROM inv_unit_devices WHERE inv_units_pointer = @unitId", conn)
                removeDevicesCmd.Parameters.AddWithValue("@unitId", unitId)
                removeDevicesCmd.ExecuteNonQuery()

                ' Step 3: Re-link the selected devices to the unit
                For Each deviceId In deviceIds
                    Dim insertDeviceCmd As New MySqlCommand("
                INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_by) 
                VALUES (@unitId, @deviceId, @createdBy)", conn)
                    insertDeviceCmd.Parameters.AddWithValue("@unitId", unitId)
                    insertDeviceCmd.Parameters.AddWithValue("@deviceId", deviceId)
                    insertDeviceCmd.Parameters.AddWithValue("@createdBy", updatedBy)
                    insertDeviceCmd.ExecuteNonQuery()
                Next

                ' Step 4: Optionally update device status to "Assigned"
                For Each deviceId In deviceIds
                    Dim updateDeviceCmd As New MySqlCommand("
                UPDATE inv_devices SET status = 'Assigned' WHERE device_id = @deviceId", conn)
                    updateDeviceCmd.Parameters.AddWithValue("@deviceId", deviceId)
                    updateDeviceCmd.ExecuteNonQuery()
                Next

                Return True
            End Using
        Catch ex As Exception
            ' Log or show the error
            Debug.WriteLine("Error: " & ex.Message)
            MessageBox.Show("Error updating unit: " & ex.Message)
            Return False
        End Try
    End Function

    Public Function GetDevicesByUnit(unitId As Integer) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "
                SELECT d.DeviceID, CONCAT(dc.CategoryName, ': ', d.Model, ', ', d.Specs) AS DeviceAndSpecs
                FROM devices d
                LEFT JOIN device_categories dc ON d.DevCategoryPointer = dc.Pointer
                WHERE d.UnitID = @UnitID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UnitID", unitId)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading devices: " & ex.Message)
            End Try
        End Using
        Return dt
    End Function

    ' ===============================================================
    ' SAVE UNIT CHANGES (FINAL VERSION)
    ' ===============================================================
    Public Sub SaveUnitChanges(unitId As Integer,
                           assignedPersonnelId As Object,
                           removedDevices As List(Of Integer),
                           addedDevices As List(Of Integer),
                           editedSpecs As Dictionary(Of Integer, Dictionary(Of String, String)),
                           unitName As String,
                           remarks As String)

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' =======================================================
                ' 0. GET ORIGINAL UNIT DATA
                ' =======================================================
                Dim originalUnitName As String = ""
                Dim originalAssignedId As Integer = 0

                Using cmd As New MySqlCommand("SELECT unit_name, assigned_personnel 
                                           FROM inv_units WHERE pointer=@uid", conn)
                    cmd.Parameters.AddWithValue("@uid", unitId)
                    Using r = cmd.ExecuteReader()
                        If r.Read() Then
                            originalUnitName = r("unit_name").ToString()
                            If r("assigned_personnel") IsNot DBNull.Value Then
                                originalAssignedId = CInt(r("assigned_personnel"))
                            End If
                        End If
                    End Using
                End Using

                ' Determine new assigned ID
                Dim finalAssignedId As Integer? = Nothing
                If assignedPersonnelId IsNot DBNull.Value AndAlso assignedPersonnelId IsNot Nothing Then
                    finalAssignedId = Convert.ToInt32(assignedPersonnelId)
                End If

                ' =======================================================
                ' 1. UPDATE UNIT NAME + ASSIGNED PERSONNEL
                ' =======================================================
                Dim updateQuery As String = "UPDATE inv_units SET unit_name=@nm"

                Dim assignedChanged As Boolean = False

                If originalAssignedId <> finalAssignedId.GetValueOrDefault(0) Then
                    updateQuery &= ", assigned_personnel=@assigned"
                    assignedChanged = True
                End If

                updateQuery &= " WHERE pointer=@uid"

                Using cmd As New MySqlCommand(updateQuery, conn)
                    cmd.Parameters.AddWithValue("@nm", unitName)
                    cmd.Parameters.AddWithValue("@uid", unitId)
                    If finalAssignedId.HasValue Then
                        cmd.Parameters.AddWithValue("@assigned", finalAssignedId.Value)
                    End If
                    cmd.ExecuteNonQuery()
                End Using

                ' =======================================================
                ' 1a. INSERT INTO ASSIGNED HISTORY IF PERSONNEL CHANGED
                ' =======================================================
                If assignedChanged Then
                    Using cmd As New MySqlCommand("
                    INSERT INTO inv_assigned_history
                    (units_pointer, assigned_from, assigned_to, date_assigned, created_by)
                    VALUES (@unit, @from, @to, NOW(), @user)", conn)

                        cmd.Parameters.AddWithValue("@unit", unitId)
                        cmd.Parameters.AddWithValue("@from", If(originalAssignedId = 0, DBNull.Value, originalAssignedId))
                        cmd.Parameters.AddWithValue("@to", finalAssignedId)
                        cmd.Parameters.AddWithValue("@user", Session.LoggedInUserPointer)

                        cmd.ExecuteNonQuery()
                    End Using
                End If


                ' =======================================================
                ' 2. REMOVE DEVICES (with updated_by / updated_at)
                ' =======================================================
                For Each devId In removedDevices
                    ' First, update updated_by and updated_at for audit
                    Using cmd As New MySqlCommand("
                    UPDATE inv_unit_devices
                    SET updated_by=@user, updated_at=NOW()
                    WHERE inv_units_pointer=@unit AND inv_devices_pointer=@dev", conn)
                        cmd.Parameters.AddWithValue("@unit", unitId)
                        cmd.Parameters.AddWithValue("@dev", devId)
                        cmd.Parameters.AddWithValue("@user", Session.LoggedInUserPointer)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Then delete the record
                    Using cmd As New MySqlCommand("
                    DELETE FROM inv_unit_devices
                    WHERE inv_units_pointer=@unit AND inv_devices_pointer=@dev", conn)
                        cmd.Parameters.AddWithValue("@unit", unitId)
                        cmd.Parameters.AddWithValue("@dev", devId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' history
                    InsertUnitHistory(conn, unitId, devId, "Device removed from unit")
                Next

                ' =======================================================
                ' 3. ADD DEVICES (with created_by / created_at and updated_by / updated_at)
                ' =======================================================
                For Each devId In addedDevices
                    Using cmd As New MySqlCommand("
                    INSERT INTO inv_unit_devices 
                    (inv_units_pointer, inv_devices_pointer, created_by, created_at, updated_by, updated_at)
                    VALUES (@unit, @dev, @user, NOW(), @user, NOW())", conn)
                        cmd.Parameters.AddWithValue("@unit", unitId)
                        cmd.Parameters.AddWithValue("@dev", devId)
                        cmd.Parameters.AddWithValue("@user", Session.LoggedInUserPointer)
                        cmd.ExecuteNonQuery()
                    End Using

                    InsertUnitHistory(conn, unitId, devId, "Device added to unit")
                Next

                ' =======================================================
                ' 4. EDITED SPECS AND INV_DEVICES FIELDS
                ' =======================================================
                For Each kvp In editedSpecs
                    Dim devId As Integer = kvp.Key
                    Dim specsDict = kvp.Value

                    ' Extract NSOC Name and Property Number
                    Dim nsocName As String = If(specsDict.ContainsKey("NSOC Name"), specsDict("NSOC Name"), Nothing)
                    Dim propertyNumber As String = If(specsDict.ContainsKey("Property No"), specsDict("Property No"), Nothing)

                    ' Remove these from specsDict before building actual specs string
                    Dim specsOnlyDict = specsDict.Where(Function(x) x.Key <> "NSOC Name" AndAlso x.Key <> "Property No").ToDictionary(Function(x) x.Key, Function(x) x.Value)

                    ' Build specs string for inv_specs table
                    Dim newSpecsString As String = String.Join(";", specsOnlyDict.Select(Function(x) $"{x.Key}: {x.Value}"))

                    ' (A) GET CATEGORY ID FOR THIS DEVICE
                    Dim categoryId As Integer
                    Using cmd As New MySqlCommand("
                    SELECT dev_category_pointer 
                    FROM inv_devices WHERE pointer=@dev", conn)
                        cmd.Parameters.AddWithValue("@dev", devId)
                        categoryId = CInt(cmd.ExecuteScalar())
                    End Using

                    ' (B) CHECK IF EXACT SAME SPECS ALREADY EXIST
                    Dim existingPointer As Object
                    Using cmd As New MySqlCommand("
                    SELECT pointer FROM inv_specs
                    WHERE category_id=@cat AND specs=@sp LIMIT 1", conn)
                        cmd.Parameters.AddWithValue("@cat", categoryId)
                        cmd.Parameters.AddWithValue("@sp", newSpecsString)
                        existingPointer = cmd.ExecuteScalar()
                    End Using

                    Dim specPointer As Integer
                    If existingPointer IsNot Nothing Then
                        specPointer = CInt(existingPointer)
                    Else
                        ' INSERT NEW SPECS ROW
                        Using cmd As New MySqlCommand("
                    INSERT INTO inv_specs (category_id, specs, created_by, created_at)
                    VALUES (@cat, @sp, @user, NOW()); 
                    SELECT LAST_INSERT_ID();", conn)
                            cmd.Parameters.AddWithValue("@cat", categoryId)
                            cmd.Parameters.AddWithValue("@sp", newSpecsString)
                            cmd.Parameters.AddWithValue("@user", Session.LoggedInUserPointer)
                            specPointer = CInt(cmd.ExecuteScalar())
                        End Using
                    End If

                    ' UPDATE DEVICE SPECS POINTER + NSOC Name + Property Number
                    Using cmd As New MySqlCommand("
                    UPDATE inv_devices 
                    SET specs=@p, nsoc_name=@nsoc, property_number=@prop, updated_by=@user, updated_at=NOW()
                    WHERE pointer=@dev", conn)
                        cmd.Parameters.AddWithValue("@p", specPointer)
                        cmd.Parameters.AddWithValue("@nsoc", If(nsocName, DBNull.Value))
                        cmd.Parameters.AddWithValue("@prop", If(propertyNumber, DBNull.Value))
                        cmd.Parameters.AddWithValue("@user", Session.LoggedInUserPointer)
                        cmd.Parameters.AddWithValue("@dev", devId)
                        cmd.ExecuteNonQuery()
                    End Using

                    InsertUnitHistory(conn, unitId, devId, "Device specs updated")
                Next


                ' =======================================================
                ' 5. UNIT NAME CHANGE HISTORY
                ' =======================================================
                If unitName <> originalUnitName Then
                    InsertUnitHistory(conn, unitId, Nothing, $"Unit Name changed from '{originalUnitName}' to '{unitName}'")
                End If

            End Using

        Catch ex As Exception
            Throw New Exception("Error saving unit changes: " & ex.Message)
        End Try
    End Sub


    Private Sub InsertUnitHistory(conn As MySqlConnection,
                              unitId As Integer,
                              deviceId As Object,
                              remarks As String)

        Using cmd As New MySqlCommand("
        INSERT INTO inv_unit_history
        (inv_devices_pointer, units_pointer, remarks, created_by)
        VALUES (@dev, @unit, @rem, @user)", conn)

            If deviceId Is Nothing Then
                cmd.Parameters.AddWithValue("@dev", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@dev", deviceId)
            End If

            cmd.Parameters.AddWithValue("@unit", unitId)
            cmd.Parameters.AddWithValue("@rem", remarks)
            cmd.Parameters.AddWithValue("@user", Session.LoggedInUserPointer)

            cmd.ExecuteNonQuery()
        End Using

    End Sub



    ' Update unit name and assigned personnel
    Public Sub UpdateUnit(unitId As Integer, unitName As String, assignedId As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE inv_units SET unit_name = @unitName, assigned_personnel = @assignedId WHERE pointer = @unitId"
                cmd.Parameters.AddWithValue("@unitName", unitName)
                cmd.Parameters.AddWithValue("@assignedId", assignedId)
                cmd.Parameters.AddWithValue("@unitId", unitId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function UpdateUnitsBulk(dt As DataTable, personnelNames As List(Of String), loggedInUserId As Integer) As String
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine("The following updates have been made:" & vbCrLf)

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                For Each row As DataRow In dt.Rows
                    Dim unitId As Integer = CInt(row("unit_id"))
                    Dim newUnitName As String = If(row("Unit Name") IsNot DBNull.Value, row("Unit Name").ToString().Trim(), "")
                    Dim assignedFullName As String = If(row("Assigned To") IsNot DBNull.Value, row("Assigned To").ToString().Trim(), "")

                    ' -------------------------------
                    ' Get original data for comparison
                    ' -------------------------------
                    Dim originalRow As DataRow = GetUnitsSummary().AsEnumerable().FirstOrDefault(Function(r) CInt(r("unit_id")) = unitId)
                    Dim originalUnitName As String = If(originalRow("Unit Name") IsNot DBNull.Value, originalRow("Unit Name").ToString(), "")
                    Dim originalAssignedId As Integer = If(originalRow("personnel_id") IsNot DBNull.Value, CInt(originalRow("personnel_id")), 0)
                    Dim originalAssignedName As String = If(originalRow("Assigned To") IsNot DBNull.Value, originalRow("Assigned To").ToString(), "")

                    ' -------------------------------
                    ' Validate personnel name
                    ' -------------------------------
                    Dim finalAssignedId As Integer = 0
                    If Not String.IsNullOrEmpty(assignedFullName) Then
                        If personnelNames.Contains(assignedFullName) Then
                            finalAssignedId = CInt(GetAssignments().AsEnumerable() _
                                           .First(Function(r) r("Full name").ToString() = assignedFullName)("user_id"))
                        Else
                            sb.AppendLine($"Unit ID {unitId}: Assigned personnel '{assignedFullName}' is invalid (not found). Skipped assigned personnel update.")
                        End If
                    End If

                    ' -------------------------------
                    ' Check for changes
                    ' -------------------------------
                    Dim unitNameChanged As Boolean = (newUnitName <> originalUnitName)
                    Dim assignedChanged As Boolean = (finalAssignedId <> originalAssignedId)

                    ' -------------------------------
                    ' Update inv_units table
                    ' -------------------------------
                    Using cmd As New MySqlCommand("
                    UPDATE inv_units 
                    SET unit_name=@name, assigned_personnel=@personnel
                    WHERE pointer=@unitId", conn)
                        cmd.Parameters.AddWithValue("@name", newUnitName)
                        cmd.Parameters.AddWithValue("@personnel", If(finalAssignedId = 0, DBNull.Value, finalAssignedId))
                        cmd.Parameters.AddWithValue("@unitId", unitId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' -------------------------------
                    ' Log changes to inv_unit_history
                    ' -------------------------------
                    If unitNameChanged Then
                        Using cmd As New MySqlCommand("
                        INSERT INTO inv_unit_history (units_pointer, remarks, created_by)
                        VALUES (@unitId, @remarks, @user)", conn)
                            cmd.Parameters.AddWithValue("@unitId", unitId)
                            cmd.Parameters.AddWithValue("@remarks", $"Unit Name changed from '{originalUnitName}' to '{newUnitName}'")
                            cmd.Parameters.AddWithValue("@user", loggedInUserId)
                            cmd.ExecuteNonQuery()
                        End Using
                        sb.AppendLine($"Unit ID {unitId}: Unit Name changed from '{originalUnitName}' to '{newUnitName}'")
                    End If

                    ' -------------------------------
                    ' Log assigned personnel change with remarks
                    ' -------------------------------
                    If assignedChanged And finalAssignedId <> 0 Then
                        ' Declare the remark string
                        Dim remarkStr As String = $"Assigned personnel changed from '{originalAssignedName}' to '{assignedFullName}'"

                        Using cmd As New MySqlCommand("
                            INSERT INTO inv_assigned_history 
                            (units_pointer, assigned_from, assigned_to, date_assigned, created_by, remarks)
                            VALUES (@unitId, @from, @to, NOW(), @user, @remarks)", conn)

                            cmd.Parameters.AddWithValue("@unitId", unitId)
                            cmd.Parameters.AddWithValue("@from", If(originalAssignedId = 0, DBNull.Value, originalAssignedId))
                            cmd.Parameters.AddWithValue("@to", finalAssignedId)
                            cmd.Parameters.AddWithValue("@user", loggedInUserId)
                            cmd.Parameters.AddWithValue("@remarks", remarkStr)

                            cmd.ExecuteNonQuery()
                        End Using

                        ' Also log in your StringBuilder for display
                        sb.AppendLine($"Unit ID {unitId}: {remarkStr}")
                    End If

                Next
            End Using

        Catch ex As Exception
            Throw New Exception("Error updating units: " & ex.Message)
        End Try

        Return sb.ToString()
    End Function


    Public Function GetSerialCounts() As (WithSerial As Integer, WithoutSerial As Integer)
        Dim withSerial As Integer = 0
        Dim withoutSerial As Integer = 0

        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            Dim query As String =
            "SELECT 
                 SUM(CASE WHEN serial_number IS NOT NULL AND serial_number <> '' THEN 1 ELSE 0 END) AS WithSerial,
                 SUM(CASE WHEN serial_number IS NULL OR serial_number = '' THEN 1 ELSE 0 END) AS WithoutSerial
             FROM inv_devices;"

            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Check for DBNull and assign default 0 if DBNull
                        If Not IsDBNull(reader("WithSerial")) Then
                            withSerial = Convert.ToInt32(reader("WithSerial"))
                        End If

                        If Not IsDBNull(reader("WithoutSerial")) Then
                            withoutSerial = Convert.ToInt32(reader("WithoutSerial"))
                        End If
                    End If
                End Using
            End Using
        End Using

        Return (withSerial, withoutSerial)
    End Function


    Public Function GetDeviceStatusCounts() As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer)

        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            Dim query As String =
            "SELECT status, COUNT(*) AS cnt
             FROM inv_devices
             GROUP BY status;"

            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim status As String = reader("status").ToString()
                        Dim count As Integer = Convert.ToInt32(reader("cnt"))
                        result(status) = count
                    End While
                End Using
            End Using
        End Using

        ' Ensure all statuses exist even if zero
        For Each s In New String() {"Working", "Assigned", "Maintenance", "Not Working"}
            If Not result.ContainsKey(s) Then
                result(s) = 0
            End If
        Next

        Return result
    End Function

    Public Function GetTotalDevices() As Integer
        Dim total As Integer = 0
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM inv_devices"
            Using cmd As New MySqlCommand(query, conn)
                total = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
        Return total
    End Function

    Public Function GetTotalUnits() As Integer
        Dim total As Integer = 0
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM inv_units"
            Using cmd As New MySqlCommand(query, conn)
                total = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
        Return total
    End Function

    Public Function GetDevicesPerCategory() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Category", GetType(String))
        dt.Columns.Add("Count", GetType(Integer))

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT dc.category_name, COUNT(d.pointer) AS DeviceCount
                               FROM inv_device_category dc
                               LEFT JOIN inv_devices d ON dc.pointer = d.dev_category_pointer
                               GROUP BY dc.pointer"
            Using cmd As New MySqlCommand(query, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        dt.Rows.Add(reader("category_name").ToString(), Convert.ToInt32(reader("DeviceCount")))
                    End While
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function GetRecentUnitActivities() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "
            SELECT 
                u.unit_name AS UnitName,
                IFNULL(d.model, '-') AS DeviceModel,
                CASE 
                    WHEN ah.pointer IS NOT NULL THEN 'Assigned'
                    WHEN uh.pointer IS NOT NULL THEN 'History'
                    ELSE 'N/A'
                END AS ActivityType,
                COALESCE(ah.remarks, uh.remarks, '-') AS Remarks,
                COALESCE(ah.date_assigned, uh.created_at) AS ActivityDate
            FROM inv_units u
            LEFT JOIN inv_assigned_history ah 
                ON u.pointer = ah.units_pointer
            LEFT JOIN inv_unit_history uh
                ON u.pointer = uh.units_pointer
            LEFT JOIN inv_devices d
                ON uh.inv_devices_pointer = d.pointer
            ORDER BY COALESCE(ah.date_assigned, uh.created_at) DESC
            LIMIT 20;
        "
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function


    ' ========================
    ' Get Unit Stats for Panel14 graph
    ' ========================
    Public Function GetUnitStats() As Object
        Dim result As Object = Nothing
        Dim query As String = "
        SELECT
            IFNULL(SUM(CASE WHEN unit_name IS NOT NULL AND unit_name <> '' THEN 1 ELSE 0 END), 0) AS WithName,
            IFNULL(SUM(CASE WHEN unit_name IS NULL OR unit_name = '' THEN 1 ELSE 0 END), 0) AS WithoutName,
            IFNULL(SUM(CASE WHEN assigned_personnel IS NOT NULL THEN 1 ELSE 0 END), 0) AS WithPersonnel,
            IFNULL(SUM(CASE WHEN assigned_personnel IS NULL THEN 1 ELSE 0 END), 0) AS WithoutPersonnel
        FROM inv_units;
    "

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        result = New With {
                        .WithName = Convert.ToInt32(reader("WithName")),
                        .WithoutName = Convert.ToInt32(reader("WithoutName")),
                        .WithPersonnel = Convert.ToInt32(reader("WithPersonnel")),
                        .WithoutPersonnel = Convert.ToInt32(reader("WithoutPersonnel"))
                    }
                    End If
                End Using
            End Using
        End Using

        Return result
    End Function


    Public Function GetRecentAddedDevicesAndUnits(Optional topCount As Integer = 10) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "
        SELECT 'Device' AS Type,
               CONCAT(dc.category_name, ' - ', b.brand_name, ' - ', d.model) AS Name,
               d.created_at AS DateAdded,
               u.UAUsername AS CreatedBy
        FROM inv_devices d
        LEFT JOIN inv_device_category dc ON d.dev_category_pointer = dc.pointer
        LEFT JOIN inv_brands b ON d.brands = b.pointer
        LEFT JOIN m_useraccounts u ON d.created_by = u.pointer

        UNION ALL

        SELECT 'Unit' AS Type,
               u.unit_name AS UnitName,
               u.created_at AS DateAdded,
               ua.UAUsername AS CreatedBy
        FROM inv_units u
        LEFT JOIN m_useraccounts ua ON u.created_by = ua.pointer

        ORDER BY DateAdded DESC
        LIMIT @TopCount;
    "

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TopCount", topCount)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Public Function GetUnitsSimple() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    u.unit_name AS `Unit Name`,
                    CONCAT(i.LAST_M, ', ', i.FIRST_M) AS `Assigned To`,
                    u.remarks AS `Remarks`,
                    u.status AS `Status`,
                    IFNULL(c.UAUsername, 'N/A') AS `Created By`,
                    DATE_FORMAT(u.created_at, '%Y-%m-%d %H:%i:%s') AS `Created At`
                FROM inv_units AS u
                LEFT JOIN db_nlrc_intranet.user_info AS i ON u.assigned_personnel = i.user_id
                LEFT JOIN m_useraccounts AS c ON u.created_by = c.pointer
                ORDER BY u.pointer DESC
            "

                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching units: " & ex.Message)
        End Try

        Return dt
    End Function

    Public Function GetNextPropertyNumber(prefix As String) As String
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Get the latest property number with the same prefix
                Dim query As String = "
                SELECT property_number 
                FROM inv_devices 
                WHERE property_number LIKE @prefixLike
                ORDER BY pointer DESC
                LIMIT 1
            "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@prefixLike", prefix & "%")

                    Dim lastProp As Object = cmd.ExecuteScalar()

                    If lastProp IsNot Nothing Then
                        Dim parts = lastProp.ToString().Split("-"c)
                        Dim lastNum As Integer = Integer.Parse(parts(parts.Length - 1))
                        Return prefix & "-" & (lastNum + 1).ToString()
                    Else
                        ' If none exists, start at 1
                        Return prefix & "-1"
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error generating property number: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetCategorySpecs(categoryId As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT specs_name
                FROM inv_category_specs
                WHERE category_pointer = @cat
                ORDER BY pointer ASC;
            "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cat", categoryId)

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading category specs: " & ex.Message)
        End Try

        Return dt
    End Function

    ' ============================
    ' CATEGORY PROPERTIES: INSERT ONE
    ' ============================
    Public Function InsertCategoryProperty(categoryId As Integer,
                                       propertyName As String,
                                       required As Boolean,
                                       active As Boolean,
                                       createdBy As Integer) As Boolean
        Dim ok As Boolean = False

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                "INSERT INTO inv_category_properties " &
                "    (category_pointer, property_name, required, active, created_by) " &
                "VALUES (@cat, @name, @req, @act, @by);"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@cat", categoryId)
                    cmd.Parameters.AddWithValue("@name", propertyName)
                    cmd.Parameters.AddWithValue("@req", If(required, 1, 0))
                    cmd.Parameters.AddWithValue("@act", If(active, 1, 0))
                    cmd.Parameters.AddWithValue("@by", createdBy)

                    ok = (cmd.ExecuteNonQuery() > 0)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error inserting category property: " & ex.Message)
        End Try

        Return ok
    End Function

    ' ============================
    ' CATEGORY: DELETE
    ' ============================
    Public Function DeleteCategory(categoryId As Integer) As Boolean
        Dim ok As Boolean = False

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                "DELETE FROM inv_device_category WHERE pointer = @id;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", categoryId)
                    ok = (cmd.ExecuteNonQuery() > 0)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error deleting category: " & ex.Message)
        End Try

        Return ok
    End Function

    ' ============================
    ' CATEGORY: UPDATE
    ' ============================
    Public Function UpdateDeviceCategory(categoryId As Integer,
                                     categoryName As String,
                                     description As String,
                                     updatedBy As Integer) As Boolean
        Dim ok As Boolean = False

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                "UPDATE inv_device_category " &
                "SET category_name = @name, " &
                "    description = @desc, " &
                "    updated_by = @by, " &
                "    updated_at = NOW() " &
                "WHERE pointer = @id;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@name", categoryName)
                    cmd.Parameters.AddWithValue("@desc", description)
                    cmd.Parameters.AddWithValue("@by", updatedBy)
                    cmd.Parameters.AddWithValue("@id", categoryId)

                    ok = (cmd.ExecuteNonQuery() > 0)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error updating category: " & ex.Message)
        End Try

        Return ok
    End Function

    Public Function UpdateCategoryProperty(propPointer As Integer, propName As String, required As Boolean, active As Boolean) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String =
                "UPDATE inv_category_properties 
                 SET property_name=@name,
                     required=@req,
                     active=@act
                 WHERE pointer=@pointer"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", propName)
                    cmd.Parameters.AddWithValue("@req", If(required, 1, 0))
                    cmd.Parameters.AddWithValue("@act", If(active, 1, 0))
                    cmd.Parameters.AddWithValue("@pointer", propPointer)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Return True

        Catch ex As Exception
            MessageBox.Show("Error updating property: " & ex.Message)
            Return False
        End Try
    End Function


    ' ============================
    ' CATEGORY: INSERT (RETURN ID)
    ' ============================
    Public Function InsertDeviceCategoryReturnId(categoryName As String,
                                             description As String,
                                             createdBy As Integer) As Integer
        Dim newId As Integer = 0

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                "INSERT INTO inv_device_category (category_name, description, created_by) " &
                "VALUES (@name, @desc, @by); " &
                "SELECT LAST_INSERT_ID();"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@name", categoryName)
                    cmd.Parameters.AddWithValue("@desc", description)
                    cmd.Parameters.AddWithValue("@by", createdBy)

                    newId = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error inserting category: " & ex.Message)
        End Try

        Return newId    ' 0 = failed
    End Function

    ' ============================
    ' CATEGORY PROPERTIES: GET ALL FOR CATEGORY
    ' ============================
    Public Function GetCategoryProperties(categoryPointer As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String =
                "SELECT pointer, category_pointer, property_name, required, active 
                 FROM inv_category_properties 
                 WHERE category_pointer = @cat 
                 ORDER BY pointer ASC"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@cat", categoryPointer)

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading category properties: " & ex.Message)
        End Try

        Return dt
    End Function


    ' ============================
    ' CATEGORY PROPERTIES: DELETE ALL FOR CATEGORY
    ' ============================
    Public Sub DeleteCategoryProperties(categoryId As Integer)
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                "DELETE FROM inv_category_properties " &
                "WHERE category_pointer = @cat;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@cat", categoryId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error deleting category properties: " & ex.Message)
        End Try
    End Sub

    Public Function GetCategoryPropertyName(propertyPointer As Integer) As String
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT property_name FROM inv_category_properties WHERE pointer=@p"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@p", propertyPointer)
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing, result.ToString(), "")
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching property name: " & ex.Message)
            Return ""
        End Try
    End Function

    Public Function UpdateCategoryPropertyFlags(pointer As Integer, required As Boolean, active As Boolean) As Boolean
        Dim sql As String = "
        UPDATE inv_category_properties 
        SET required=@r, active=@a 
        WHERE pointer=@p
    "

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@r", If(required, 1, 0))
                cmd.Parameters.AddWithValue("@a", If(active, 1, 0))
                cmd.Parameters.AddWithValue("@p", pointer)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Function IsCategorySpecExist(categoryPointer As Integer, specName As String) As Boolean
        Dim sql As String = "
        SELECT COUNT(*) 
        FROM inv_category_specs 
        WHERE category_pointer=@cat AND specs_name=@name
    "

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@cat", categoryPointer)
                cmd.Parameters.AddWithValue("@name", specName)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function



    Public Function InsertCategorySpec(categoryPointer As Integer, specName As String, createdBy As Integer) As Boolean
        Dim sql As String = "
        INSERT INTO inv_category_specs (category_pointer, specs_name, created_by)
        VALUES (@cat, @name, @created)
    "

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@cat", categoryPointer)
                cmd.Parameters.AddWithValue("@name", specName)
                cmd.Parameters.AddWithValue("@created", createdBy)

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Public Function GetDeviceDetails(deviceId As Integer) As DataRow
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                    SELECT 
                        d.pointer AS device_id,
                        c.category_name,
                        b.brand_name,
                        d.model,
                        d.specs AS device_specs,
                        d.nsoc_name,
                        d.property_number
                    FROM inv_devices d
                    INNER JOIN inv_brands b 
                        ON b.pointer = d.brands
                    INNER JOIN inv_device_category c 
                        ON c.pointer = d.dev_category_pointer
                    WHERE d.pointer = @deviceId
                    LIMIT 1;
                "

                Using da As New MySqlDataAdapter(query, conn)
                    da.SelectCommand.Parameters.AddWithValue("@deviceId", deviceId)
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading device details: " & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        End If

        Return Nothing
    End Function


    ' ================================
    ' AVAILABLE DEVICES FOR UNIT (COMBO)
    ' ================================
    Public Function GetDevicesForUnits1(unitId As Integer) As DataTable
Dim dt As New DataTable()

        Try
    Using conn As New MySqlConnection(connectionString)
        conn.Open()

        Dim query As String = "
           SELECT 
            d.pointer AS device_id,
            c.category_name,
            b.brand_name,
            d.model,
            s.specs AS actual_specs,
            d.nsoc_name,
            d.property_number,
            CONCAT(
                c.category_name, ' - ',
                b.brand_name, ' - ',
                d.model,
                IF(s.specs IS NOT NULL AND s.specs != '', CONCAT(' - ', s.specs), ''),
                IF(d.nsoc_name IS NOT NULL AND d.nsoc_name != '', CONCAT(' | NSOC: ', d.nsoc_name), ''),
                IF(d.property_number IS NOT NULL AND d.property_number != '', CONCAT(' | Property#: ', d.property_number), '')
            ) AS DeviceAndSpecs,
            COUNT(*) AS qty,
            CONCAT(
                c.category_name, ' - ',
                b.brand_name, ' - ',
                d.model, 
                IF(d.nsoc_name IS NOT NULL AND d.nsoc_name != '', CONCAT(' | NSOC: ', d.nsoc_name), ''),
                IF(d.property_number IS NOT NULL AND d.property_number != '', CONCAT(' | Property#: ', d.property_number), ''),
                ' (', COUNT(*), ')'
            ) AS DeviceDisplay
        FROM inv_devices d
        INNER JOIN inv_brands b ON b.pointer = d.brands
        INNER JOIN inv_device_category c ON c.pointer = d.dev_category_pointer
        LEFT JOIN inv_specs s ON d.specs = s.pointer
        WHERE d.status = 'Working'
          AND d.pointer NOT IN (
                SELECT inv_devices_pointer
                FROM inv_unit_devices
                WHERE inv_units_pointer = @unitId
          )
        GROUP BY 
            d.pointer, c.category_name, b.brand_name, d.model, s.specs, d.nsoc_name, d.property_number
        ORDER BY 
            c.category_name, b.brand_name, d.model;

        "

        Using da As New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@unitId", unitId)
            da.Fill(dt)
        End Using
    End Using

Catch ex As Exception
    MessageBox.Show("Error loading devices: " & ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
End Try

Return dt


End Function




    Public Function GetAssignedDevices(unitId As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
            SELECT 
                d.pointer AS DeviceID,
                c.category_name,
                b.brand_name,
                d.model,
                d.specs AS device_specs,
                d.nsoc_name,
                d.property_number,

                CONCAT(
                    c.category_name, ' - ',
                    b.brand_name, ' - ',
                    d.model, ' - ',
                    d.specs
                ) AS DeviceAndSpecs

            FROM inv_unit_devices ud
            INNER JOIN inv_devices d 
                ON d.pointer = ud.inv_devices_pointer
            INNER JOIN inv_brands b 
                ON b.pointer = d.brands
            INNER JOIN inv_device_category c 
                ON c.pointer = d.dev_category_pointer

            WHERE ud.inv_units_pointer = @unitId;
        "

                Using da As New MySqlDataAdapter(query, conn)
                    da.SelectCommand.Parameters.AddWithValue("@unitId", unitId)
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading assigned devices: " & ex.Message)
        End Try

        Return dt
    End Function


    Public Function GetDevicesWithNsocByUnitPointer(unitPointer As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                SELECT 
                    ud.inv_units_pointer AS UnitID,
                    d.pointer AS DeviceID,
                    c.category_name AS Category,
                    b.brand_name AS Brand,
                    d.model AS Model,
                    d.specs AS DeviceSpecs,
                    d.nsoc_name,
                    d.property_number,
                    CONCAT(
                        c.category_name, ' - ',
                        b.brand_name, ' - ',
                        d.model,
                        IF(d.specs IS NOT NULL AND d.specs != '', CONCAT(' - ', d.specs), ''),
                        IF(d.nsoc_name IS NOT NULL AND d.nsoc_name != '', CONCAT(' | NSOC: ', d.nsoc_name), ''),
                        IF(d.property_number IS NOT NULL AND d.property_number != '', CONCAT(' | Property#: ', d.property_number), '')
                    ) AS DeviceFullSpecs
                FROM inv_unit_devices ud
                INNER JOIN inv_devices d ON ud.inv_devices_pointer = d.pointer
                INNER JOIN inv_brands b ON b.pointer = d.brands
                INNER JOIN inv_device_category c ON c.pointer = d.dev_category_pointer
                WHERE ud.inv_units_pointer = @unitPointer
                ORDER BY d.pointer;
            "

                Using da As New MySqlDataAdapter(query, conn)
                    da.SelectCommand.Parameters.AddWithValue("@unitPointer", unitPointer)
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error fetching devices with NSOC: " & ex.Message)
        End Try

        Return dt
    End Function

    Public Function InsertDeviceHistory(devicePointer As Integer,
                                    fieldName As String,
                                    oldValue As String,
                                    newValue As String,
                                    updatedBy As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                    "INSERT INTO inv_device_history " &
                    " (device_pointer, updated_from, updated_to, remarks, updated_by) " &
                    "VALUES (@dev, @from, @to, @remarks, @by);"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@dev", devicePointer)
                    cmd.Parameters.AddWithValue("@from",
                                                If(String.IsNullOrEmpty(oldValue),
                                                   CType(DBNull.Value, Object),
                                                   oldValue))
                    cmd.Parameters.AddWithValue("@to",
                                                If(String.IsNullOrEmpty(newValue),
                                                   CType(DBNull.Value, Object),
                                                   newValue))
                    cmd.Parameters.AddWithValue("@remarks", fieldName & " changed")
                    cmd.Parameters.AddWithValue("@by", updatedBy)

                    Return (cmd.ExecuteNonQuery() > 0)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error inserting device history: " & ex.Message)
            Return False
        End Try
    End Function

    ' ============================
    ' DEVICE HISTORY: GET BY DEVICE
    ' ============================
    Public Function GetDeviceHistory(devicePointer As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim sql As String =
                    "SELECT pointer, device_pointer, updated_from, updated_to, remarks, updated_by " &
                    "FROM inv_device_history " &
                    "WHERE device_pointer = @dev " &
                    "ORDER BY pointer DESC;"

                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@dev", devicePointer)

                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading device history: " & ex.Message)
        End Try

        Return dt
    End Function






End Class