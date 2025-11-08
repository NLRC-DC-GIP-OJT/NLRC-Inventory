Imports System.Text
Imports MySql.Data.MySqlClient


Public Class InvDevice
    Public Property Pointer As Integer
    Public Property DevCategoryPointer As Integer?
    Public Property BrandPointer As Integer?
    Public Property Model As String
    Public Property Specs As String
    Public Property Status As String
    Public Property SerialNumber As String
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



Public Class model

    Private ReadOnly connectionString As String = "Server=127.0.0.1;Port=3306;Database=main_nlrc_db;Uid=root;Pwd=;"

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

                Dim query As String

                If device.Pointer > 0 Then
                    ' UPDATE
                    query = "UPDATE inv_devices 
                         SET dev_category_pointer=@category,
                             brands=@brand,
                             model=@model,
                             specs=@specs,
                             status=@status,
                             serial_number=@serial,
                             purchase_date=@purchase,
                             warranty_expires=@warranty,
                             notes=@notes,
                             updated_at=NOW(),
                             updated_by=@userID
                         WHERE pointer=@pointer"
                Else
                    ' INSERT
                    query = "INSERT INTO inv_devices 
                         (dev_category_pointer, brands, model, specs, status, serial_number, purchase_date, warranty_expires, notes, created_at, updated_at, created_by, updated_by)
                         VALUES (@category, @brand, @model, @specs, @status, @serial, @purchase, @warranty, @notes, NOW(), NOW(), @userID, @userID)"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pointer", device.Pointer)
                    cmd.Parameters.AddWithValue("@category", device.DevCategoryPointer)
                    cmd.Parameters.AddWithValue("@brand", device.BrandPointer)
                    cmd.Parameters.AddWithValue("@model", device.Model)
                    cmd.Parameters.AddWithValue("@specs", device.Specs)
                    cmd.Parameters.AddWithValue("@status", device.Status)
                    cmd.Parameters.AddWithValue("@serial", device.SerialNumber)
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
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
        SELECT 
            MIN(d.pointer) AS pointer,        -- any pointer (just for reference)
            b.brand_name AS brands,
            d.model,
            d.status,
            COUNT(*) AS total_devices,
            CONCAT(b.brand_name, ' - ', d.model) AS display_name
        FROM inv_devices d
        LEFT JOIN inv_brands b ON d.brands = b.pointer
        WHERE d.dev_category_pointer = @categoryPointer
        AND d.status = 'Working'
        GROUP BY b.brand_name, d.model, d.status
        ORDER BY b.brand_name, d.model;"

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
                    c.category_name AS Category,
                    b.brand_name AS Brand,
                    d.model AS Model,
                    s.specs AS Specifications,  
                    d.status AS Status,
                    d.serial_number AS Serial_Number,
                    d.purchase_date AS Purchase_Date,
                    d.warranty_expires AS Warranty_Expires
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
                            device.BrandPointer = Convert.ToInt32(reader("brands"))
                            device.Model = reader("model").ToString()
                            device.Specs = reader("specs").ToString()
                            device.Status = reader("status").ToString()
                            device.SerialNumber = reader("serial_number").ToString()
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
            Using conn As New MySqlConnection("Server=127.0.0.1;Port=3306;Database=db_nlrc_intranet;Uid=root;Pwd=;")
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

    ' Function to get devices for units
    Public Function GetDevicesForUnits() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "
                SELECT 
                    d.pointer AS device_id,
                    CONCAT(b.brand_name, ' ', d.model) AS Device,
                    d.status
                FROM inv_devices d
                INNER JOIN inv_brands b ON b.pointer = d.brands
                WHERE d.status = 'Working';"

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
    Public Function SaveUnit(unitName As String, assignedPersonnel As Integer?, devicePointers As List(Of Integer), remarks As String) As Boolean
        Try
            If String.IsNullOrWhiteSpace(unitName) OrElse devicePointers Is Nothing OrElse devicePointers.Count = 0 Then
                MessageBox.Show("Unit name or devices are missing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Start transaction
                Using transaction = conn.BeginTransaction()
                    ' Step 1: Insert unit
                    Dim unitId As Integer = 0
                    Dim insertUnitQuery As String = "
                        INSERT INTO inv_units (unit_name, assigned_personnel, remarks, status, created_at)
                        VALUES (@unitName, @assigned, @remarks, 'Active', NOW());
                        SELECT LAST_INSERT_ID();"

                    Using cmd As New MySqlCommand(insertUnitQuery, conn, transaction)
                        cmd.Parameters.AddWithValue("@unitName", unitName)
                        cmd.Parameters.AddWithValue("@assigned", If(assignedPersonnel.HasValue, assignedPersonnel.Value, DBNull.Value))
                        cmd.Parameters.AddWithValue("@remarks", If(String.IsNullOrWhiteSpace(remarks), DBNull.Value, remarks))
                        unitId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    ' Step 2: Link devices
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
                        Dim insertLinkQuery As String = "
                            INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_at)
                            VALUES (@unitId, @devicePointer, NOW());"
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



    Public Function GetBrandPointerByName(brandName As String) As Integer
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT pointer FROM inv_brands WHERE brand_name = @name LIMIT 1", conn)
            cmd.Parameters.AddWithValue("@name", brandName)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToInt32(result)
            Else
                Throw New Exception("Brand not found: " & brandName)
            End If
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
                            ' 1️⃣ Insert a unit
                            Dim insertUnitCmd As New MySqlCommand("
                            INSERT INTO inv_units (unit_name, remarks, status, created_by, created_at)
                            VALUES (NULL, @remark, 'Active', @created_by, NOW());
                            SELECT LAST_INSERT_ID();
                        ", conn, trans)
                            insertUnitCmd.Parameters.AddWithValue("@remark", If(String.IsNullOrWhiteSpace(remark), DBNull.Value, remark))
                            insertUnitCmd.Parameters.AddWithValue("@created_by", 1)
                            Dim unitId As Integer = Convert.ToInt32(insertUnitCmd.ExecuteScalar())

                            ' 2️⃣ Loop through selected devices to assign
                            For Each row As DataRow In selectedDevices.Rows
                                Dim brandPointer As Integer = Convert.ToInt32(row("brands"))
                                Dim model As String = row("model").ToString().Trim()

                                ' Get one available device of this brand/model
                                Dim getDeviceCmd As New MySqlCommand("
                                SELECT pointer 
                                FROM inv_devices 
                                WHERE brands = @brand AND model = @model AND status = 'Working'
                                LIMIT 1
                            ", conn, trans)
                                getDeviceCmd.Parameters.AddWithValue("@brand", brandPointer)
                                getDeviceCmd.Parameters.AddWithValue("@model", model)
                                Dim deviceObj = getDeviceCmd.ExecuteScalar()

                                If deviceObj Is Nothing Then
                                    ' Only show brand name in MsgBox
                                    Dim brandName As String = GetBrandName(brandPointer)
                                    Throw New Exception($"Not enough stock for Brand {brandName}, Model {model}.")
                                End If

                                Dim devicePointer As Integer = Convert.ToInt32(deviceObj)

                                ' 3️⃣ Insert into inv_unit_devices
                                Dim insertLinkCmd As New MySqlCommand("
                                INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_at, created_by)
                                VALUES (@unitId, @devicePointer, NOW(), @created_by)
                            ", conn, trans)
                                insertLinkCmd.Parameters.AddWithValue("@unitId", unitId)
                                insertLinkCmd.Parameters.AddWithValue("@devicePointer", devicePointer)
                                insertLinkCmd.Parameters.AddWithValue("@created_by", 1)
                                insertLinkCmd.ExecuteNonQuery()

                                ' 4️⃣ Mark device as Assigned
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
    Private Function GetBrandName(brandId As Integer) As String
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
                        u.unit_name AS `Unit Name`,
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
                    COUNT(ud.inv_devices_pointer) AS `Device No`,
                    CONCAT(i.LAST_M, ', ', i.FIRST_M) AS `Assigned To`,
                    i.user_id AS personnel_id,
                    DATE_FORMAT(u.created_at, '%Y-%m-%d') AS `Created Date`
                FROM inv_units AS u
                LEFT JOIN inv_unit_devices AS ud ON u.pointer = ud.inv_units_pointer
                LEFT JOIN db_nlrc_intranet.user_info AS i ON u.assigned_personnel = i.user_id
                GROUP BY u.pointer
                ORDER BY u.pointer ASC;
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

    Public Function GetDevicesByUnitPointer(unitPointer As Integer) As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Adjust the query to fetch devices based on the unit pointer
                Dim query As String = "
                SELECT 
                    d.device_id,
                    d.device_name,
                    d.device_category,
                    d.device_brand,
                    d.device_status
                FROM inv_unit_devices AS d
                WHERE d.unit_pointer = @unitPointer;"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@unitPointer", unitPointer)
                    Using da As New MySqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
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
            MessageBox.Show("Error checking unit name: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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










End Class







