Imports System.Collections.Specialized
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms.ListBox
Imports uplink.NET.Models
Imports uplink.NET.Services

Public Class BackgroundSyncClass
    Private BukServ As BucketService
    Private ObjServ As ObjectService
    Private Bkt As Bucket
    Private ObjList As ObjectList
    Public Accessgrant As String = ""
    Public Event cloudmessage(msgType As Integer, filename As String) ''1=download complete, 2= download error, 3= delete complete, 4= delete error
    Private syncoptions As String

    Public Sub New(acg As String)
        Accessgrant = acg

        InitConnection()




    End Sub
    Public Sub Start(items As ObjectCollection)
        For Each item As String In items
            Select Case CInt(item.Split("*").Last)
                Case 2
                    syncoptions = item
                    SyncObjectsToCloud()

                Case 3
                    syncoptions = item
                    SyncObjectsToPC()


            End Select
        Next
    End Sub
    Public Sub InitConnection()

        Dim access As New uplink.NET.Models.Access(Accessgrant)
        BukServ = New BucketService(access)
        ObjServ = New ObjectService(access)

    End Sub


    Private Async Sub SyncObjectsToPC()
        Dim Bukets = Await ListBucket()
        For Each bk As Bucket In Bukets.Items
            If bk.Name = syncoptions.Split("*")(0) Then
                Dim listoptions As New ListObjectsOptions
                listoptions.Prefix = syncoptions.Split("*")(1)
                listoptions.Recursive = True
                listoptions.System = True
                listoptions.Custom = True
                ObjList = Await ObjServ.ListObjectsAsync(bk, listoptions)
                Bkt = bk
            End If
        Next
        If ObjList IsNot Nothing Then
            For Each obj As [Object] In ObjList.Items

                Dim objectpath = obj.Key.Split("/")
                Dim realpath As String = syncoptions.Split("*")(2)
                If objectpath.Count > 1 Then

                    For i As Integer = 0 To objectpath.Count - 1
                        If i = objectpath.Count - 1 Then
                            If IO.File.Exists(realpath & "\" & objectpath(i)) Then
                            Else
                                DownloadFile(Bkt, obj.Key, realpath)
                            End If
                        Else
                            If IO.Directory.Exists(realpath & "\" & objectpath(i)) Then
                                realpath = realpath & "\" & objectpath(i)
                            Else
                                realpath = realpath & "\" & objectpath(i)
                                IO.Directory.CreateDirectory(realpath)
                            End If
                        End If
                    Next
                Else
                    DownloadFile(Bkt, obj.Key, realpath)
                End If
            Next

        End If

    End Sub
    Public Async Function ListBucket() As Task(Of BucketList)
        Dim bukets As BucketList
        Dim listOptions As ListBucketsOptions = New ListBucketsOptions()
        bukets = Await BukServ.ListBucketsAsync(listOptions)
        Return bukets
    End Function

    Private Async Sub DownloadFile(bk As Bucket, name As String, path As String)

        Try
            Dim DownloadOperation = Await ObjServ.DownloadObjectAsync(bk, name, New DownloadOptions, False)

            Await DownloadOperation.StartDownloadAsync
            If DownloadOperation.Completed Then
                File.WriteAllBytes(path & "\" & DownloadOperation.ObjectName.Split("/").Last, DownloadOperation.DownloadedBytes)
                RaiseEvent cloudmessage(1, DownloadOperation.ObjectName.Split("/").Last)
            End If
        Catch ex As Exception
            RaiseEvent cloudmessage(2, ex.Message)
        End Try

    End Sub
    Private Async Sub UploadFile(bk As Bucket, name As String, path As String)
        Try
            Dim fs As FileStream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.None)
            Dim uploadOperation = Await ObjServ.UploadObjectAsync(bk, path, New UploadOptions, fs, True)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Async Sub SyncObjectsToCloud()
        Dim Bukets = Await ListBucket()
        For Each bk As Bucket In Bukets.Items
            If bk.Name = syncoptions.Split("*")(0) Then
                Dim listoptions As New ListObjectsOptions
                listoptions.Prefix = syncoptions.Split("*")(1)
                listoptions.Recursive = True
                listoptions.System = True
                listoptions.Custom = True
                ObjList = Await ObjServ.ListObjectsAsync(bk, listoptions)
                Bkt = bk
            End If
        Next
        If ObjList IsNot Nothing Then
            Dim fileList = GetFilesRecursive(syncoptions.Split("*")(2))
            For Each file As String In fileList
                Dim cloudfile = file.Replace(syncoptions.Split("*")(2), "").Replace("\", "/")
                Dim match As Boolean = False
                For Each item As [Object] In ObjList.Items

                    If String.Compare(item.Key, cloudfile) = 0 Then
                        match = True
                        Exit For
                    End If
                Next
                If match Then
                Else
                    UploadFile(Bkt, file, syncoptions.Split("*")(1) & cloudfile.Replace("/", ""))
                End If
                Thread.Sleep(5000) ' to make some breath time for cpu and network
            Next

        End If

    End Sub
    Private Function GetFilesRecursive(ByVal path As String) As List(Of String)
        Dim lstResult As New List(Of String)
        Dim stkStack As New Stack(Of String)
        stkStack.Push(path)
        Do While (stkStack.Count > 0)
            Dim strDirectory As String = stkStack.Pop
            Try
                lstResult.AddRange(Directory.GetFiles(strDirectory))
                Dim strDirectoryName As String
                For Each strDirectoryName In Directory.GetDirectories(strDirectory)
                    stkStack.Push(strDirectoryName)
                Next
            Catch ex As Exception
            End Try
        Loop
        Return lstResult
    End Function

End Class
