<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Form1))
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        ConnectToolStripMenuItem = New ToolStripMenuItem()
        FileView = New TreeView()
        ImageList1 = New ImageList(components)
        SyncList = New ListBox()
        LogiBox = New ListBox()
        GroupBox1 = New GroupBox()
        RadioButton3 = New RadioButton()
        RadioButton2 = New RadioButton()
        RadioButton1 = New RadioButton()
        UpdateBtn = New Button()
        DelBtn = New Button()
        AddBtn = New Button()
        Button1 = New Button()
        Label6 = New Label()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        AddSyncBtn = New Button()
        GroupBox2 = New GroupBox()
        MinimizedCheckBox = New CheckBox()
        AutoStartCheckBox = New CheckBox()
        AutoConnectCheckBox = New CheckBox()
        Button6 = New Button()
        ApiKeyBox = New TextBox()
        Label7 = New Label()
        NotifyIcon1 = New NotifyIcon(components)
        Filemenu = New ContextMenuStrip(components)
        Download = New ToolStripMenuItem()
        Delete = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        Filemenu.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, ConnectToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1346, 28)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(46, 24)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' ConnectToolStripMenuItem
        ' 
        ConnectToolStripMenuItem.Name = "ConnectToolStripMenuItem"
        ConnectToolStripMenuItem.Size = New Size(77, 24)
        ConnectToolStripMenuItem.Text = "Connect"
        ' 
        ' FileView
        ' 
        FileView.ImageIndex = 0
        FileView.ImageList = ImageList1
        FileView.ImeMode = ImeMode.On
        FileView.Location = New Point(12, 248)
        FileView.Name = "FileView"
        FileView.SelectedImageIndex = 0
        FileView.Size = New Size(481, 383)
        FileView.TabIndex = 1
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth8Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "th.jpg")
        ImageList1.Images.SetKeyName(1, "R.png")
        ' 
        ' SyncList
        ' 
        SyncList.FormattingEnabled = True
        SyncList.ItemHeight = 20
        SyncList.Location = New Point(589, 31)
        SyncList.Name = "SyncList"
        SyncList.Size = New Size(745, 204)
        SyncList.TabIndex = 2
        ' 
        ' LogiBox
        ' 
        LogiBox.FormattingEnabled = True
        LogiBox.ItemHeight = 20
        LogiBox.Items.AddRange(New Object() {"For future use not implemented yet"})
        LogiBox.Location = New Point(589, 452)
        LogiBox.Name = "LogiBox"
        LogiBox.Size = New Size(745, 184)
        LogiBox.TabIndex = 4
        LogiBox.Visible = False
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(RadioButton3)
        GroupBox1.Controls.Add(RadioButton2)
        GroupBox1.Controls.Add(RadioButton1)
        GroupBox1.Controls.Add(UpdateBtn)
        GroupBox1.Controls.Add(DelBtn)
        GroupBox1.Controls.Add(AddBtn)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(589, 234)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(745, 207)
        GroupBox1.TabIndex = 3
        GroupBox1.TabStop = False
        GroupBox1.Text = "Selected"
        ' 
        ' RadioButton3
        ' 
        RadioButton3.AutoSize = True
        RadioButton3.Location = New Point(503, 86)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(142, 24)
        RadioButton3.TabIndex = 15
        RadioButton3.TabStop = True
        RadioButton3.Text = "Sync Down to PC"
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Location = New Point(503, 56)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(140, 24)
        RadioButton2.TabIndex = 14
        RadioButton2.TabStop = True
        RadioButton2.Text = "Sync Up from PC"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Location = New Point(503, 26)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(87, 24)
        RadioButton1.TabIndex = 13
        RadioButton1.TabStop = True
        RadioButton1.Text = "Full Sync"
        RadioButton1.UseVisualStyleBackColor = True
        RadioButton1.Visible = False
        ' 
        ' UpdateBtn
        ' 
        UpdateBtn.Enabled = False
        UpdateBtn.Location = New Point(527, 157)
        UpdateBtn.Name = "UpdateBtn"
        UpdateBtn.Size = New Size(94, 29)
        UpdateBtn.TabIndex = 11
        UpdateBtn.Text = "Update Sync"
        UpdateBtn.UseVisualStyleBackColor = True
        ' 
        ' DelBtn
        ' 
        DelBtn.Enabled = False
        DelBtn.Location = New Point(627, 157)
        DelBtn.Name = "DelBtn"
        DelBtn.Size = New Size(94, 29)
        DelBtn.TabIndex = 10
        DelBtn.Text = "Delete"
        DelBtn.UseVisualStyleBackColor = True
        ' 
        ' AddBtn
        ' 
        AddBtn.Location = New Point(427, 157)
        AddBtn.Name = "AddBtn"
        AddBtn.Size = New Size(94, 29)
        AddBtn.TabIndex = 9
        AddBtn.Text = "Add "
        AddBtn.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(28, 157)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 29)
        Button1.TabIndex = 6
        Button1.Text = "Select Path"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(193, 124)
        Label6.Name = "Label6"
        Label6.Size = New Size(15, 20)
        Label6.TabIndex = 5
        Label6.Text = "-"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(28, 124)
        Label5.Name = "Label5"
        Label5.Size = New Size(159, 20)
        Label5.TabIndex = 4
        Label5.Text = "Sync Folder Path on PC"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(90, 87)
        Label4.Name = "Label4"
        Label4.Size = New Size(15, 20)
        Label4.TabIndex = 3
        Label4.Text = "-"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(28, 87)
        Label3.Name = "Label3"
        Label3.Size = New Size(46, 20)
        Label3.TabIndex = 2
        Label3.Text = "Prefix"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(90, 48)
        Label2.Name = "Label2"
        Label2.Size = New Size(15, 20)
        Label2.TabIndex = 1
        Label2.Text = "-"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(28, 48)
        Label1.Name = "Label1"
        Label1.Size = New Size(53, 20)
        Label1.TabIndex = 0
        Label1.Text = "Bucket"
        ' 
        ' AddSyncBtn
        ' 
        AddSyncBtn.Enabled = False
        AddSyncBtn.Location = New Point(516, 317)
        AddSyncBtn.Name = "AddSyncBtn"
        AddSyncBtn.Size = New Size(41, 29)
        AddSyncBtn.TabIndex = 5
        AddSyncBtn.Text = ">>"
        AddSyncBtn.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(MinimizedCheckBox)
        GroupBox2.Controls.Add(AutoStartCheckBox)
        GroupBox2.Controls.Add(AutoConnectCheckBox)
        GroupBox2.Controls.Add(Button6)
        GroupBox2.Controls.Add(ApiKeyBox)
        GroupBox2.Controls.Add(Label7)
        GroupBox2.Location = New Point(12, 31)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(571, 204)
        GroupBox2.TabIndex = 6
        GroupBox2.TabStop = False
        GroupBox2.Text = " Settings"
        ' 
        ' MinimizedCheckBox
        ' 
        MinimizedCheckBox.AutoSize = True
        MinimizedCheckBox.Location = New Point(364, 104)
        MinimizedCheckBox.Name = "MinimizedCheckBox"
        MinimizedCheckBox.Size = New Size(136, 24)
        MinimizedCheckBox.TabIndex = 10
        MinimizedCheckBox.Text = "Start Minimized"
        MinimizedCheckBox.UseVisualStyleBackColor = True
        ' 
        ' AutoStartCheckBox
        ' 
        AutoStartCheckBox.AutoSize = True
        AutoStartCheckBox.Location = New Point(158, 104)
        AutoStartCheckBox.Name = "AutoStartCheckBox"
        AutoStartCheckBox.Size = New Size(181, 24)
        AutoStartCheckBox.TabIndex = 9
        AutoStartCheckBox.Text = "Auto Start on windows"
        AutoStartCheckBox.UseVisualStyleBackColor = True
        ' 
        ' AutoConnectCheckBox
        ' 
        AutoConnectCheckBox.AutoSize = True
        AutoConnectCheckBox.Enabled = False
        AutoConnectCheckBox.Location = New Point(17, 104)
        AutoConnectCheckBox.Name = "AutoConnectCheckBox"
        AutoConnectCheckBox.Size = New Size(121, 24)
        AutoConnectCheckBox.TabIndex = 8
        AutoConnectCheckBox.Text = "Auto Connect"
        AutoConnectCheckBox.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(451, 159)
        Button6.Name = "Button6"
        Button6.Size = New Size(94, 29)
        Button6.TabIndex = 7
        Button6.Text = "Save"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' ApiKeyBox
        ' 
        ApiKeyBox.Location = New Point(138, 30)
        ApiKeyBox.Multiline = True
        ApiKeyBox.Name = "ApiKeyBox"
        ApiKeyBox.Size = New Size(410, 52)
        ApiKeyBox.TabIndex = 5
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(17, 46)
        Label7.Name = "Label7"
        Label7.Size = New Size(92, 20)
        Label7.TabIndex = 4
        Label7.Text = "Access grant"
        ' 
        ' NotifyIcon1
        ' 
        NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), Icon)
        NotifyIcon1.Text = "Storj WinSync(comunity)"
        NotifyIcon1.Visible = True
        ' 
        ' Filemenu
        ' 
        Filemenu.ImageScalingSize = New Size(20, 20)
        Filemenu.Items.AddRange(New ToolStripItem() {Download, Delete})
        Filemenu.Name = "Download"
        Filemenu.Size = New Size(148, 52)
        ' 
        ' Download
        ' 
        Download.Name = "Download"
        Download.Size = New Size(147, 24)
        Download.Text = "Download"
        ' 
        ' Delete
        ' 
        Delete.Name = "Delete"
        Delete.Size = New Size(147, 24)
        Delete.Text = "Delete"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1346, 643)
        Controls.Add(GroupBox2)
        Controls.Add(AddSyncBtn)
        Controls.Add(LogiBox)
        Controls.Add(GroupBox1)
        Controls.Add(SyncList)
        Controls.Add(FileView)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        Text = "WinSync for Storj(comunity made)"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        Filemenu.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConnectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileView As TreeView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents SyncList As ListBox
    Friend WithEvents LogiBox As ListBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents AddSyncBtn As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents AddBtn As Button
    Friend WithEvents DelBtn As Button
    Friend WithEvents UpdateBtn As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ApiKeyBox As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents AutoStartCheckBox As CheckBox
    Friend WithEvents AutoConnectCheckBox As CheckBox
    Friend WithEvents MinimizedCheckBox As CheckBox
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents Filemenu As ContextMenuStrip
    Friend WithEvents Download As ToolStripMenuItem
    Friend WithEvents Delete As ToolStripMenuItem
End Class
