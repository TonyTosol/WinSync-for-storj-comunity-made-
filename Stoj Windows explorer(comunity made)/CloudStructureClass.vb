

Public Class CloudStructureClass

    Public Structure Buket
        Public Name As String
        Public Objects As List(Of DataObjects)

    End Structure
    Public Structure DataObjects
        Public Name As String
        Public Prefix As Boolean
        Public SystemMetadata As uplink.NET.Models.SystemMetadata
        Public CustomMetadata As uplink.NET.Models.CustomMetadata
        Public Objects As List(Of DataObjects)
    End Structure



End Class
