
Imports System.IO
Imports System.Management
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports System.Text.Json
Imports System.Threading

Imports uplink.NET.Models
Imports uplink.NET.Services



Public Class Form1
    Private WithEvents Cloud As CloudClass
    Private Delegate Sub LogInvoker(Logi As String)
    Private syncListSelected As Integer = -1
    Private SyncMode As Integer = -1
    Private selectedSyncListNode As Integer = -1
    Private AccessGrand As String = ""
    Private FileViewSelected As TreeNode
    Private BgSync As BackgroundSyncClass
    Private cloasing As Boolean = False

    Private Sub ConnectToolStripMenuItem_ClickAsync(sender As Object, e As EventArgs) Handles ConnectToolStripMenuItem.Click
        If My.Settings.APIKey = "" Then
            MsgBox("To connect to Satellite please setup access in Connection Setup section")
        Else

            sync()

        End If
    End Sub
    Private Sub checkcloasing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        cloasing = True
    End Sub
    Private Sub sync()
        If Cloud.Accessgrant = "" Then Cloud.Accessgrant = AccessGrand
        If Cloud.Accessgrant <> "" Then

            Dim thread As New Thread(AddressOf Cloud.Syncobjects)
            thread.Start()
        End If
    End Sub
    Private Sub renderSync()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf renderSync))
        Else
            FileView.Nodes.Clear()

            For Each buket As CloudStructureClass.Buket In Cloud.CloudData
                Dim node As New TreeNode
                node.Text = buket.Name
                node.Tag = 1
                node.Name = buket.Name & "-"
                If buket.Objects.Count > 0 Then
                    For Each obj As CloudStructureClass.DataObjects In buket.Objects
                        Dim node1 As New TreeNode
                        If obj.Prefix = True Then

                            node1.Text = obj.Name
                            node1.Nodes.AddRange(addObjects(obj.Objects, buket.Name))
                            node1.Name = buket.Name & "-" & obj.Name
                            node1.SelectedImageIndex = 0
                            node1.Tag = 1
                        Else
                            node1.Name = buket.Name & "-" & obj.Name
                            node1.Text = obj.Name
                            node1.ImageIndex = 1
                            node1.SelectedImageIndex = 1
                            node1.ContextMenuStrip = Filemenu
                        End If
                        node.Nodes.Add(node1)
                    Next

                End If
                FileView.Nodes.Add(node)
            Next
        End If


    End Sub
    Private Function addObjects(objects As List(Of CloudStructureClass.DataObjects), buket As String) As TreeNode()
        Dim node(objects.Count - 1) As TreeNode

        For i As Integer = 0 To objects.Count - 1
            Dim node1 As New TreeNode
            If objects(i).Prefix = True Then

                node1.Text = objects(i).Name.Split("/").GetValue(objects(i).Name.Split("/").Count - 2)
                node1.Nodes.AddRange(addObjects(objects(i).Objects, buket))
                node1.ImageIndex = 0
                node1.Name = buket & "-" & objects(i).Name
                node1.SelectedImageIndex = 0
                node1.Tag = 1
            Else
                node1.Name = buket & "-" & objects(i).Name
                node1.Text = objects(i).Name.Split("/").Last
                node1.ImageIndex = 1
                node1.SelectedImageIndex = 1
                node1.ContextMenuStrip = Filemenu
            End If
            node(i) = (node1)

        Next
        Return node

    End Function

    Private Sub Log(logi As String)
        If Me.InvokeRequired Then
            Me.Invoke(New LogInvoker(AddressOf Log), logi)
        Else
            LogiBox.Items.Insert(0, logi)
            If LogiBox.Items.Count > 100 Then LogiBox.Items.RemoveAt(LogiBox.Items.Count - 1)
        End If
    End Sub

    Private Sub FileView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles FileView.AfterSelect
        AddSyncBtn.Enabled = True

    End Sub

    Private Sub AddSyncBtn_Click(sender As Object, e As EventArgs) Handles AddSyncBtn.Click
        If FileView.SelectedNode.Tag = 1 Then
            Label2.Text = FileView.SelectedNode.Name.Split("-")(0)
            Label4.Text = FileView.SelectedNode.Name.Split("-")(1)
            Label6.Text = ""
            ChangeSyncMode(2)
            UpdateBtn.Enabled = False
            DelBtn.Enabled = False
        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog = New FolderBrowserDialog()
        dialog.SelectedPath = Application.StartupPath
        If DialogResult.OK = dialog.ShowDialog() Then
            Label6.Text = dialog.SelectedPath
        End If

    End Sub

    Private Sub AddBtn_Click(sender As Object, e As EventArgs) Handles AddBtn.Click
        If Label2.Text <> "-" And Label6.Text <> "" Then
            If Permission(Label6.Text) Then
                SyncList.Items.Add(Label2.Text & "*" & Label4.Text & "*" & Label6.Text & "*" & SyncMode)
                updateSyncStructure()
            Else
                MsgBox("Some folders windows restrikt to write, you can freely use something like download folder")
            End If
        Else
                MsgBox("You need to check a buket or mapp on storj side and select map on pc to sync with")
        End If
    End Sub

    Private Function Permission(directory As String) As Boolean
        Dim permit As Boolean = False
        Try

            Dim sid As SecurityIdentifier = New SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, Nothing)
            Dim myDirectoryInfo As DirectoryInfo = New DirectoryInfo(directory)
            Dim myDirectorySecurity As DirectorySecurity = myDirectoryInfo.GetAccessControl()

            myDirectorySecurity.AddAccessRule(New FileSystemAccessRule(sid, FileSystemRights.FullControl, AccessControlType.Allow))
            myDirectoryInfo.SetAccessControl(myDirectorySecurity)
            permit = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return permit
    End Function
    Private Sub SyncList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SyncList.SelectedIndexChanged
        If SyncList.SelectedIndex >= 0 Then
            selectedSyncListNode = SyncList.SelectedIndex

            Dim selecteddata = SyncList.Items.Item(selectedSyncListNode).ToString.Split("*")
            Label2.Text = selecteddata(0)
            Label4.Text = selecteddata(1)
            Label6.Text = selecteddata(2)
            ChangeSyncMode(CInt(selecteddata(3)))
            UpdateBtn.Enabled = True
            DelBtn.Enabled = True
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApiKeyBox.Text = My.Settings.APIKey
        AutoConnectCheckBox.Checked = My.Settings.Autoconnect
        AutoStartCheckBox.Checked = My.Settings.Autostart
        MinimizedCheckBox.Checked = My.Settings.Minimized
        AccessGrand = My.Settings.APIKey
        LoadSynclist()
        If My.Settings.Minimized = True Then
            NotifyIcon1.Visible = True
            'NotifyIcon1.Icon = SystemIcons.Application
            'NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info

            'NotifyIcon1.ShowBalloonTip(50000)
            Me.Hide()
            ShowInTaskbar = False
        End If
        Cloud = New CloudClass(AccessGrand)
        AddHandler Cloud.cloudmessage, AddressOf ShowCloudMessage
        AddHandler Cloud.StructureUpdated, AddressOf renderSync
        If My.Settings.Autoconnect Then
            ConnectToolStripMenuItem.Enabled = False
            sync()
        End If
        If ApiKeyBox.Text <> "" Then AutoConnectCheckBox.Enabled = True
        If SyncList.Items.Count > 0 Then

            BgSync = New BackgroundSyncClass(AccessGrand)
            AddHandler BgSync.SyncComplete, AddressOf resumeSync
            Dim thread As New Thread(AddressOf BgSync.Start)
            thread.Start(SyncList.Items)

        End If
    End Sub
    Private Sub resumeSync()
        sync()
        Threading.Thread.Sleep(60000) : Application.DoEvents()
        If cloasing Then
        Else
            Dim thread As New Thread(AddressOf BgSync.Start)
            thread.Start(SyncList.Items)
        End If

    End Sub
    Private Sub ShowCloudMessage(msgType As Integer, msg As String)
        Select Case msgType
            Case 1
                NotifyIcon1.BalloonTipText = "Download " & msg & " complete" & vbCrLf & "You can find it in Download folder"
            Case 2
                NotifyIcon1.BalloonTipText = "Download Failed" & vbCrLf & msg
            Case 3
                NotifyIcon1.BalloonTipText = "Delete " & msg & " complete"
            Case 4
                NotifyIcon1.BalloonTipText = "Delete Failed" & vbCrLf & msg
        End Select


        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.ShowBalloonTip(50000)
        sync()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        My.Settings.Autoconnect = AutoConnectCheckBox.Checked

        My.Settings.APIKey = ApiKeyBox.Text
        AccessGrand = ApiKeyBox.Text
        My.Settings.Minimized = MinimizedCheckBox.Checked
        If AutoConnectCheckBox.Checked = True Then
            My.Settings.Save()
            ConnectToolStripMenuItem.Enabled = False
            sync()
        End If
        Try


            If AutoStartCheckBox.Checked Then
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
                My.Settings.Autostart = True
            Else
                If My.Settings.Autostart Then


                    My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
                    My.Settings.Autostart = False
                End If
            End If
        Catch ex As Exception
            MsgBox("To add App to autostart you need to run as administrator")
        End Try
        My.Settings.Save()
        MsgBox("Settings Saved")

    End Sub

    Private Sub ApiKeyBox_TextChanged(sender As Object, e As EventArgs) Handles ApiKeyBox.TextChanged
        If ApiKeyBox.Text <> "" Then
            AutoConnectCheckBox.Enabled = True
        Else
            AutoConnectCheckBox.Enabled = False
            AutoConnectCheckBox.Checked = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        SyncMode = 1
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        SyncMode = 2
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        SyncMode = 3
    End Sub
    Private Sub ChangeSyncMode(mode As Integer)
        Select Case mode
            Case 0
                RadioButton1.Checked = False
                RadioButton2.Checked = False
                RadioButton3.Checked = False
            Case 1
                RadioButton1.Checked = True
                RadioButton2.Checked = False
                RadioButton3.Checked = False
            Case 2
                RadioButton1.Checked = False
                RadioButton2.Checked = True
                RadioButton3.Checked = False
            Case 3
                RadioButton1.Checked = False
                RadioButton2.Checked = False
                RadioButton3.Checked = True
        End Select

    End Sub

    Private Sub DelBtn_Click(sender As Object, e As EventArgs) Handles DelBtn.Click
        SyncList.Items.RemoveAt(selectedSyncListNode)
        updateSyncStructure()
    End Sub

    Private Sub UpdateBtn_Click(sender As Object, e As EventArgs) Handles UpdateBtn.Click
        SyncList.Items.Item(selectedSyncListNode) = Label2.Text & "*" & Label4.Text & "*" & Label6.Text & "*" & SyncMode
        updateSyncStructure()
    End Sub
    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            'NotifyIcon1.Icon = SystemIcons.Application
            'NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info

            'NotifyIcon1.ShowBalloonTip(50000)
            'Me.Hide()
            ShowInTaskbar = False
        End If
    End Sub
    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub
    Private Sub updateSyncStructure()
        My.Settings.SyncList = ""
        For Each row As String In SyncList.Items
            My.Settings.SyncList = My.Settings.SyncList & row & ","
        Next
        My.Settings.Save()
    End Sub

    Private Sub LoadSynclist()
        Dim listTosync = My.Settings.SyncList.Split(",")
        For Each sync As String In listTosync
            If sync <> "" Then SyncList.Items.Add(sync)
        Next
    End Sub


    Private Sub Download_Click_1(sender As Object, e As EventArgs) Handles Download.Click
        AddHandler Cloud.Buketname, AddressOf processDownload

        Dim tsk As New Task(Sub() Cloud.GetBuketByname(FileViewSelected.Name.Split("-")(0)))

        tsk.Start()


    End Sub

    Private Sub fileviewrightclick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles FileView.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            FileViewSelected = e.Node
        End If
    End Sub

    Private Sub processDownload(bk As Bucket)
        RemoveHandler Cloud.Buketname, AddressOf processDownload
        If bk IsNot Nothing Then
            Dim path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\Downloads\"
            Cloud.DownloadFile(bk, FileViewSelected.Name.Split("-").Last, path)
        End If

    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click
        AddHandler Cloud.Buketname, AddressOf processObjectDelete

        Dim tsk As New Task(Sub() Cloud.GetBuketByname(FileViewSelected.Name.Split("-")(0)))

        tsk.Start()

    End Sub
    Private Sub processObjectDelete(bk As Bucket)
        RemoveHandler Cloud.Buketname, AddressOf processObjectDelete
        If bk IsNot Nothing Then

            Cloud.deleteObject(bk, FileViewSelected.Text)
        End If
    End Sub

    Private Sub syncFiles()
        For Each item As String In SyncList.Items

        Next
    End Sub




End Class
