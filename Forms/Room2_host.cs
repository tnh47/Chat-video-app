using Chat_video_app.Classes.Voice;
using System;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class Room2_host : Form
    {
        public static Room2_host Instance;
        public static string ActiveUsername = null;

        public Room2_host(string username, string id)
        {
            Instance = this;
            InitializeComponent();
            NameInputBox.Text=username;
            NewServerPortBox.Text = id;
            this.FormClosing += AppClosing;
            ServerList.SelectedIndexChanged += SelectedServerChanged;
            ClientNameList.SelectedIndexChanged += SelectedUserChanged;
            SetStatus("Disconnected");

            SetButtonState(MuteButton, false);
            SetButtonState(KickButton, false);
            SetButtonState(DisconnectButton, false);
            SetButtonState(ConnectButton, false);
            SetButtonState(CreateNewServerButton, false);
            SetButtonState(RefreshButton, false);
            SetButtonState(ShutdownServer, false);
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

            bool worked = Net.StartServer(port,name);
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
            Instance.SetButtonState(Instance.KickButton, false);
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
                    SetButtonState(KickButton, false);
                    SetButtonState(MuteButton, true);
                    MuteButton.Text = "Mute Person";
                }
                else
                {
                    SetButtonState(KickButton, true);
                    SetButtonState(MuteButton, true);

                    bool muted = Net.Server.IsMuted(selected);
                    MuteButton.Text = muted ? "Un-mute Person" : "Mute Person";
                }
            }
            else
            {
                SetButtonState(KickButton, false);
                SetButtonState(MuteButton, false);
                MuteButton.Text = "Mute Person";
            }
        }
    }
}
