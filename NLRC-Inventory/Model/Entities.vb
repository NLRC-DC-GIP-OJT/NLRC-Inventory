Imports System

Public Class InvDevice
    Public Property Pointer As Integer
    Public Property DevCategoryPointer As Integer?
    Public Property BrandPointer As Integer?
    Public Property Model As String
    Public Property Specs As String
    Public Property Status As String
    Public Property Ass_Status As String
    Public Property Unit_Status As String
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

Public Class CategoryProperty
    Public Property Pointer As Integer
    Public Property CategoryPointer As Integer
    Public Property PropertyName As String
    Public Property Required As Boolean
    Public Property Active As Boolean
End Class

Public Class Session
    Public Shared LoggedInUserPointer As Integer
    Public Shared LoggedInRole As String
End Class
