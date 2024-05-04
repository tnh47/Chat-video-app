using Chat_video_app.Classes;
using Chat_video_app.Classes.Voice;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            clientsDataGridView.CellClick += new DataGridViewCellEventHandler(clientsDataGridView_CellClick);
            Check_Role(id);
            DisplayMem(id);
            
        }
        private void Check_Role(string id)
        {
            panel1.Hide();
            panel2.Hide();
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            var roomSnapshot = docRef.GetSnapshotAsync().Result;

            if (roomSnapshot.Exists)
            {
                RoomData data = roomSnapshot.ConvertTo<RoomData>();

                foreach (string i in data.Mem)
                {
                    if (i != username) continue;
                    DocumentReference docRef2 = db.Collection("UserData").Document(i);
                    var userSnapshot = docRef2.GetSnapshotAsync().Result;

                    if (userSnapshot.Exists)
                    {
                        UserData data2 = userSnapshot.ConvertTo<UserData>();
                        if (data2.Id == data.Host)
                        {
                            panel1.Show();
                        }
                        // Add else condition to handle case where user exists but not the host
                        else
                        {
                            panel2.Show();
                        }
                    }
                    else
                    {
                        // Handle case where UserData does not exist
                        MessageBox.Show("User does not exist!");
                        return; // Exit the method if user does not exist
                    }
                }
            }
            else
            {
                // Handle case where RoomData does not exist
                MessageBox.Show("Room does not exist!");
                return;
                // You might want to return or take appropriate action here
            }
        }
        private void DisplayMem(string id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            foreach (string i in data.Mem)
            {
                if (i==username) continue;
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
        private async void clientsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            var roomSnapshot = docRef.GetSnapshotAsync().Result;

            if (roomSnapshot.Exists)
            {
                RoomData data = roomSnapshot.ConvertTo<RoomData>();

                foreach (string i in data.Mem)
                {
                    if (i != username) continue;
                    DocumentReference docRef2 = db.Collection("UserData").Document(i);
                    var userSnapshot = docRef2.GetSnapshotAsync().Result;

                    if (userSnapshot.Exists)
                    {
                        UserData data2 = userSnapshot.ConvertTo<UserData>();
                        if (data2.Id == data.Host)
                        {
                            if (e.ColumnIndex >= 0 && clientsDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                            {
                                DataGridViewRow selectedRow = clientsDataGridView.Rows[e.RowIndex];
                                string memberName = selectedRow.Cells["Name"].Value.ToString();
                                clientsDataGridView.Rows.Remove(selectedRow);
                                List<string> mem = new List<string>(data.Mem);
                                mem.Remove(memberName);
                                data.Mem = mem.ToArray();
                                await docRef.SetAsync(data);
                                List<string> mem2 = new List<string>(data2.Mem);
                                mem2.Remove(id);
                                data2.Mem = mem2.ToArray();
                                await docRef2.SetAsync(data2);
                            }
                        }
                        // Add else condition to handle case where user exists but not the host
                        else
                        {
                            MessageBox.Show("You are not the host!");
                        }
                    }
                    else
                    {
                        // Handle case where UserData does not exist
                        MessageBox.Show("User does not exist!");
                        return; // Exit the method if user does not exist
                    }
                }
            }
            else
            {
                // Handle case where RoomData does not exist
                MessageBox.Show("Room does not exist!");
                return;
                // You might want to return or take appropriate action here
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
            this.Text = "Crap Chat 2 - " + ActiveUsername;

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
                else
                {
                    MessageBox.Show("Ban chua tham gia phong nay!");
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
        private void KickButton_Click(object sender, EventArgs e)
        {
            string name = GetSelectedUser();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (Net.IsServer)
                {
                    Net.Server.Kick(name);
                }
            }
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
    }
}
