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
    Public Function SaveUnit(unitName As String, assignedPersonnel As Integer, devicePointer As Integer, remarks As String) As Boolean
        Try
            ' Step 1: Check if the device pointer exists and is in 'Working' status
            If Not IsDeviceValid(devicePointer) Then
                MessageBox.Show("The selected device pointer is invalid or not in 'Working' status.", "Invalid Device", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' 1️⃣ Insert the new unit into inv_units
                        Dim insertUnitQuery As String = "
                        INSERT INTO inv_units (unit_name, assigned_personnel, remarks, status, created_at)
                        VALUES (@unit_name, @assigned, @remarks, 'Active', NOW());"

                        Using cmdUnit As New MySqlCommand(insertUnitQuery, conn, transaction)
                            cmdUnit.Parameters.AddWithValue("@unit_name", unitName)
                            cmdUnit.Parameters.AddWithValue("@assigned", assignedPersonnel)
                            cmdUnit.Parameters.AddWithValue("@remarks", remarks)
                            cmdUnit.ExecuteNonQuery()
                        End Using

                        ' 2️⃣ Get the ID of the newly inserted unit
                        Dim newUnitId As Integer
                        Using cmdLastId As New MySqlCommand("SELECT LAST_INSERT_ID();", conn, transaction)
                            newUnitId = Convert.ToInt32(cmdLastId.ExecuteScalar())
                        End Using

                        ' 3️⃣ Link the unit to the selected device in inv_unit_devices
                        Dim insertLinkQuery As String = "
                        INSERT INTO inv_unit_devices (inv_units_pointer, inv_devices_pointer, created_at)
                        VALUES (@unit_pointer, @device_pointer, NOW());"

                        Using cmdLink As New MySqlCommand(insertLinkQuery, conn, transaction)
                            cmdLink.Parameters.AddWithValue("@unit_pointer", newUnitId)
                            cmdLink.Parameters.AddWithValue("@device_pointer", devicePointer)
                            cmdLink.ExecuteNonQuery()
                        End Using

                        ' 4️⃣ Update the device's status in inv_devices to 'Assigned'
                        Dim updateDeviceStatusQuery As String = "
                        UPDATE inv_devices 
                        SET status = 'Assigned' 
                        WHERE pointer = @devicePointer AND status = 'Working';"

                        Using cmdUpdateStatus As New MySqlCommand(updateDeviceStatusQuery, conn, transaction)
                            cmdUpdateStatus.Parameters.AddWithValue("@devicePointer", devicePointer)
                            cmdUpdateStatus.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        Return True

                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Error saving unit: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End Try
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    ' Check if the device pointer is valid and the status is 'Working'
    Public Function IsDeviceValid(devicePointer As Integer) As Boolean
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Query the device status using the device pointer
                Dim query As String = "SELECT status FROM inv_devices WHERE pointer = @devicePointer;"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@devicePointer", devicePointer)

                    ' Execute the query and get the status of the device
                    Dim result As Object = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        ' If the device status is 'Working', return True, otherwise return False
                        If result.ToString().Equals("Working", StringComparison.OrdinalIgnoreCase) Then
                            Return True
                        Else
                            ' Log the status if it's not 'Working'
                            Console.WriteLine($"Device with pointer {devicePointer} is not 'Working'. Status: {result}")
                            Return False
                        End If
                    Else
                        ' If the device pointer does not exist in the database, log and return False
                        Console.WriteLine($"Device with pointer {devicePointer} does not exist in the database.")
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Log any exceptions that occur during the database connection or execution
            Console.WriteLine($"Error checking device status: {ex.Message}")
            Return False
        End Try
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
    MAX(u.pointer) AS unit_id,
    u.unit_name AS `Unit Name`,
    COUNT(ud.inv_devices_pointer) AS `Device No`,
    CONCAT(i.LAST_M, ', ', i.FIRST_M) AS `Assigned To`,
    i.user_id AS personnel_id,
    DATE_FORMAT(MIN(u.created_at), '%Y-%m-%d') AS `Created Date`
FROM inv_units AS u
LEFT JOIN inv_unit_devices AS ud ON u.pointer = ud.inv_units_pointer
LEFT JOIN db_nlrc_intranet.user_info AS i ON u.assigned_personnel = i.user_id
GROUP BY u.unit_name, i.LAST_M, i.FIRST_M, i.user_id
ORDER BY `Created Date` DESC;
   
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











End Class







