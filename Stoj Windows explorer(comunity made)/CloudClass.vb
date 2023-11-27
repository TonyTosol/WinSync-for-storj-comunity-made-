Imports System.IO
Imports uplink.NET.Models
Imports uplink.NET.Services

Public Class CloudClass
    Private BukServ As BucketService
    Private ObjServ As ObjectService
    Public CloudData As New List(Of CloudStructureClass.Buket)
    Public Event StructureUpdated()
    Public Event Buketname(bk As Bucket)
    Public InitState As Boolean = False
    Public Accessgrant As String = ""
    Public Event cloudmessage(msgType As Integer, filename As String) ''1=download complete, 2= download error, 3= delete complete, 4= delete error



    Public Sub New(grant As String)
        Accessgrant = grant
        InitConnection()
    End Sub
    Public Async Function ListBucket() As Task(Of BucketList)
        Dim listOptions As ListBucketsOptions = New ListBucketsOptions()
        Dim buckets = Await BukServ.ListBucketsAsync(listOptions)
        Return buckets
    End Function

    Public Sub InitConnection()
        Try
            Dim access As New uplink.NET.Models.Access(My.Settings.APIKey)
            BukServ = New BucketService(access)
            ObjServ = New ObjectService(access)
            InitState = True
        Catch ex As Exception

        End Try


    End Sub
    Public Async Sub GetBuketByname(name As String)
        Dim list = Await ListBucket()
        Dim bu As Bucket
        For Each bk As Bucket In list.Items
            If bk.Name = name Then
                bu = bk
                Exit For
            End If
        Next
        If bu IsNot Nothing Then RaiseEvent Buketname(bu)
    End Sub
    Public Async Function Getobjects(buket As Bucket, listoptions As ListObjectsOptions) As Task(Of List(Of CloudStructureClass.DataObjects))

        ''listoptions.Recursive = True

        Dim objects = Await ObjServ.ListObjectsAsync(buket, listoptions)
        Dim node As New List(Of CloudStructureClass.DataObjects)
        For Each buketObject As uplink.NET.Models.Object In objects.Items
            Dim obj As New CloudStructureClass.DataObjects
            If buketObject.IsPrefix = True Then

                obj.Name = buketObject.Key
                obj.Prefix = buketObject.IsPrefix
                Dim listoptions1 As New ListObjectsOptions
                listoptions1.System = True
                listoptions1.Custom = True
                listoptions1.Prefix = buketObject.Key
                obj.Objects = Await Getobjects(buket, listoptions1)
                Await Task.Yield
            Else
                obj.Name = buketObject.Key
                obj.Prefix = buketObject.IsPrefix
                obj.SystemMetadata = buketObject.SystemMetadata
                obj.CustomMetadata = buketObject.CustomMetadata
            End If


            node.Add(obj)

        Next
        Return node
    End Function

    Public Async Sub Syncobjects()
        If InitState Then
            Dim bukList = Await ListBucket()
            Dim data As New List(Of CloudStructureClass.Buket)
            For Each buket As Bucket In bukList.Items
                Dim bk As New CloudStructureClass.Buket
                bk.Name = buket.Name
                Dim listoptions As New ListObjectsOptions
                listoptions.System = True
                listoptions.Custom = True
                bk.Objects = Await Getobjects(buket, listoptions)
                Await Task.Yield
                data.Add(bk)
            Next
            CloudData = data
            RaiseEvent StructureUpdated()
        Else
            ''set some acctions 
        End If
    End Sub
    Public Async Sub DownloadFile(bk As Bucket, name As String, path As String)
        If InitState Then
            Try
                Dim DownloadOperation = Await ObjServ.DownloadObjectAsync(bk, name, New DownloadOptions, False)

                Await DownloadOperation.StartDownloadAsync
                If DownloadOperation.Completed Then
                    File.WriteAllBytes(path & DownloadOperation.ObjectName.Split("/").Last, DownloadOperation.DownloadedBytes)
                    RaiseEvent cloudmessage(1, DownloadOperation.ObjectName.Split("/").Last)
                End If
            Catch ex As Exception
                RaiseEvent cloudmessage(2, ex.Message)
            End Try

        End If
    End Sub
    Public Async Sub deleteObject(bk As Bucket, name As String)
        If InitState Then
            Try
                Await ObjServ.DeleteObjectAsync(bk, name)
                RaiseEvent cloudmessage(3, name.Split("/").Last)
            Catch ex As Exception
                RaiseEvent cloudmessage(4, ex.Message)
            End Try

        End If
    End Sub

End Class
