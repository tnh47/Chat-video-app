using Chat_video_app.Classes;
using Chat_video_app.Classes.Voice;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class Room2_host : Form
    {
        string id;
        string username;
        public static Room2_host Instance;
        public static string ActiveUsername = null;

        public Room2_host(string username, string id)
        {
            Instance = this;
            InitializeComponent();
            NameInputBox.Text=username;
            NewServerPortBox.Text = id;
            this.id = id;
            this.username= username;
            this.FormClosing += AppClosing;
            ServerList.SelectedIndexChanged += SelectedServerChanged;
            ClientNameList.SelectedIndexChanged += SelectedUserChanged;
            SetStatus("Disconnected");

            SetButtonState(MuteButton, false);
            SetButtonState(DisconnectButton, false);
            SetButtonState(ConnectButton, false);
            SetButtonState(CreateNewServerButton, false);
            SetButtonState(RefreshButton, false);
            SetButtonState(ShutdownServer, false);
            Check_Role(id);
            DisplayMem(id);
            
        }
        private void Check_Role(string Id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(Id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            if (username == data.Mem[0]) {

            }
            else
            {
                panel1.Hide();
                clientsDataGridView.Columns[2].Visible = false;
            }
        }
                    
        private void DisplayMem(string id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            foreach (string i in data.Mem)
            {
                if (i == data.Mem[0]) continue;
                DocumentReference docRef2 = db.Collection("UserData").Document(i);
                UserData data2 = docRef2.GetSnapshotAsync().Result.ConvertTo<UserData>();
                AddGrid(data2.Id, data2.Username);
            }
        }

        private void AddGrid(string id, string name)
        {
            string[] row = new string[] {id, name };//fix
            clientsDataGridView.Rows.Add(row);
        }
        private async void clientsDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string name = GetSelectedUser();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (Net.IsServer)
                {
                    Net.Server.Kick(name);
                }
            }
            if (e.ColumnIndex >= 0 && clientsDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = clientsDataGridView.Rows[e.RowIndex];
                string memberName = selectedRow.Cells["Name"].Value.ToString();
                clientsDataGridView.Rows.Remove(selectedRow);
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("RoomData").Document(id);
                RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
                List<string> mem = new List<string>(data.Mem);
                mem.Remove(memberName);
                data.Mem = mem.ToArray();
                await docRef.SetAsync(data);
                DocumentReference docRef2 = db.Collection("UserData").Document(memberName);
                UserData data2 = docRef2.GetSnapshotAsync().Result.ConvertTo<UserData>();
                List<string> mem2 = new List<string>(data2.Mem);
                mem2.Remove(id);
                data2.Mem = mem2.ToArray();
                await docRef2.SetAsync(data2);
            }
        }
        public static void AddUser(string name)
        {
            if (!Instance.ClientNameList.Items.Contains(name))
                Instance.ClientNameList.Items.Add(name);
        }

        public static void RemoveUser(string name)
        {
            if (Instance.ClientNameList.Items.Contains(name))
                Instance.ClientNameList.Items.Remove(name);
        }

        public static void ClearUsers()
        {
            Instance.ClientNameList.Items.Clear();
        }

        private void AppClosing(object sender, FormClosingEventArgs e)
        {
            Forms.Room2_host.Log("Closing...");

            if (Net.IsClient)
            {
                Net.StopClient();
            }
            if (Net.IsServer)
            {
                Net.StopServer();
            }
        }
        private void confirmNameButton_Click(object sender, EventArgs e)
        {
            ActiveUsername = this.NameInputBox.Text.Trim();
            Log("Confirmed new name - " + ActiveUsername);
            this.Text = "Team7 - " + ActiveUsername;

            SetButtonState(CreateNewServerButton, true);

            if (!RefreshButton.Enabled)
                RefreshButton_Click(null, null);
            SetButtonState(RefreshButton, true);

            if (!string.IsNullOrWhiteSpace(GetNewServerName()) && !Net.IsServer && !string.IsNullOrWhiteSpace(ActiveUsername))
            {
                SetButtonState(CreateNewServerButton, true);
            }
        }
        public static void Log(object o)
        {
            if (o != null)
            {
                Console.WriteLine(o.ToString());
            }
            else
            {
                Console.WriteLine("null");
            }
        }

        private FoundServer GetSelectedServer()
        {
            if (ServerList.SelectedItem != null)
                return (FoundServer)this.ServerList.SelectedItem;
            else
                return new FoundServer();
        }
        public static void SetStatus(string status)
        {
            Instance.StatusLabel.Text = "Status: " + status.Trim();
        }
        private void SetButtonState(Button b, bool enabled)
        {
            if (b != null)
            {
                if (b.Enabled != enabled)
                    b.Enabled = enabled;
            }
        }
        private int GetNewServerPort()
        {
            return int.Parse(NewServerPortBox.Text);
        }
        private string GetNewServerName()
        {
            return NewServerPortBox.Text;
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {

            if (!Net.IsClient)
            {
                Net.StartClient();
            }
            ServerList.Items.Clear();
            Net.DiscoverPeers(int.Parse(NewServerPortBox.Text), ServerDiscovered);
            SetButtonState(ConnectButton, false);
            System.GC.Collect();
        }
        private void ServerDiscovered(FoundServer s)
        {
            ServerList.Items.Add(s);
            Log("Discovered server '" + s.Name + "' @ " + s.EndPoint);
        }

        private void CreateNewServerButton_Click(object sender, EventArgs e)
        {
            var name = GetNewServerName();
            int port = GetNewServerPort();

            bool worked = Net.StartServer(port, name);
            if (worked)
            {
                SetButtonState(CreateNewServerButton, false);
                SetButtonState(ShutdownServer, true);
                RefreshButton_Click(null, null);
                ClearUsers();
                AddUser(ActiveUsername);
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            foreach (string i in data.Mem)
            {
                if (i==username)
                {
                    if (!Net.IsClient)
                        Net.StartClient();

                    var s = GetSelectedServer();
                    Log("Attempting to connect to " + s);
                    bool worked = Net.ConnectClient(s.EndPoint.Address.ToString(), s.EndPoint.Port);
                    if (worked)
                    {
                        SetButtonState(ConnectButton, false);
                    }
                }
            }
        }
        public static void UponDisconnected()
        {
            Instance.SetButtonState(Instance.DisconnectButton, false);
            Instance.SetButtonState(Instance.ConnectButton, false);
            Instance.ServerList.ClearSelected();
            SetStatus("Disconnected");
            ClearUsers();
            Instance.RefreshButton_Click(null, null);
        }

        public static void UponConnected()
        {
            Instance.SetButtonState(Instance.DisconnectButton, true);
            Instance.SetButtonState(Instance.ConnectButton, false);
        }

        public static void UponServerStop()
        {
            Instance.SetButtonState(Instance.MuteButton, false);
        }

        private void ShutdownServer_Click(object sender, EventArgs e)
        {
            Net.StopServer();
            RefreshButton_Click(null, null);
            SetButtonState(ShutdownServer, false);
            if (!Net.IsServer && !string.IsNullOrWhiteSpace(ActiveUsername))
            {
                SetButtonState(CreateNewServerButton, true);
            }
            ClearUsers();
            SetStatus("Disconnected");
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (Net.IsClient && Net.IsConnected)
            {
                SetButtonState(DisconnectButton, false);
                Net.StopClient();
            }
            else
            {
                SetButtonState(DisconnectButton, false);
            }
        }
        private string GetSelectedUser()
        {
            return ClientNameList.SelectedItem == null ? null : (string)ClientNameList.SelectedItem;
        }
        private void SelectedServerChanged(object sender, EventArgs e)
        {
            Log("Selected " + GetSelectedServer());

            if (!Net.IsServer && !Net.IsConnecting && !GetSelectedServer().IsInvalid())
                SetButtonState(ConnectButton, true);
        }
        private void MuteButton_Click(object sender, EventArgs e)
        {
            string name = GetSelectedUser();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (Net.IsServer)
                {
                    bool isMuted = Net.Server.IsMuted(name);
                    if (isMuted)
                    {
                        Net.Server.Unmute(name);
                        MuteButton.Text = "Mute Person";
                    }
                    else
                    {
                        Net.Server.Mute(name);
                        MuteButton.Text = "Un-mute Person";
                    }
                }
            }
        }
        private void SelectedUserChanged(object sender, EventArgs e)
        {
            if (!Net.IsServer)
                return;

            string selected = this.ClientNameList.SelectedItem as string;

            if (!string.IsNullOrWhiteSpace(selected))
            {
                if (selected == ActiveUsername)
                {
                    SetButtonState(MuteButton, true);
                    MuteButton.Text = "Mute Person";
                }
                else
                {
                    SetButtonState(MuteButton, true);

                    bool muted = Net.Server.IsMuted(selected);
                    MuteButton.Text = muted ? "Un-mute Person" : "Mute Person";
                }
            }
            else
            {
                SetButtonState(MuteButton, false);
                MuteButton.Text = "Mute Person";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text.Trim();
            if (name.Length == 0 || name == username) MessageBox.Show("Name invalid!");
            else
            {
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(name);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {

                    UserData data = snapshot.ConvertTo<UserData>();
                    if (!Check(data)) MessageBox.Show("Name invalid!");
                    else
                    {
                        List<string> invitedRooms = data.Is_invited.ToList();
                        invitedRooms.Add(id);
                        data.Is_invited = invitedRooms.ToArray();

                        await docRef.SetAsync(data);

                        MessageBox.Show("Success");
                    }
                }
                else
                {
                    MessageBox.Show("Can't find username: " + name);
                }
            }
        }
        private bool Check(UserData data)
        {
            foreach (string mem in data.Mem)
            {
                if (mem == id) return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Net.IsServer)
            {
                Net.StopServer();  // Tắt server
            }

            // Kiểm tra nếu đang chạy client
            if (Net.IsClient && Net.IsConnected)
            {
                Net.StopClient();  // Ngắt kết nối client
            }
            Hide();
            Lobby form = new Lobby(username);
            form.ShowDialog();
            Close();
        }
    }
}
