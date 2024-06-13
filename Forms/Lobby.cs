using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chat_video_app.Forms
{
    public partial class Lobby : Form
    {
        string username;
        public Lobby(string username)
        {
            InitializeComponent();
            this.username = username;
            CheckRoom(username);
            CheckInvite(username);
            label2.Text = username;
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            comboBox1.Items.Add("Text"); 
            comboBox1.Items.Add("Voice");
        }
        private void CheckRoom(string username)
        { 
            listView1.Clear();
            listView2.Clear();
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach(string h in data.Mem)
            {
                DocumentReference docRef2 = db.Collection("RoomData").Document(h);
                RoomData data2 = docRef2.GetSnapshotAsync().Result.ConvertTo<RoomData>();
                if (data2.Type == false)
                {
                    listView1.Items.Add(h);
                }
                else
                {
                    listView2.Items.Add(h);
                }
            }
        }
        private void CheckInvite(string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            if (data.Is_invited != null)
            {
                listBox1.Items.Clear();
                foreach (string i in data.Is_invited)
                {
                    listBox1.Items.Add(i);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Setting form = new Setting(username);
            form.ShowDialog();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            Close();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string id = textBox3.Text.Trim();
            string choice = comboBox1.Text;
            int tmp;
            if (int.TryParse(id, out tmp))
            {
                if (tmp < 49152 || tmp > 65535 || id.Length == 0) MessageBox.Show("ID invalid");
                else
                {
                    var db = FirestoreHelper.Database;
                    DocumentReference docRef = db.Collection("RoomData").Document(id);
                    RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
                    if (data != null)
                    {
                        MessageBox.Show("Room already exists, please choose another id!");
                    }
                    else
                    {
                        if (choice == "Text")
                        {
                            var infor = GetWriteData(id, username);
                            DocumentReference docRef2 = db.Collection("RoomData").Document(infor.Id);
                            await docRef2.SetAsync(infor);
                            MessageBox.Show("Success");
                            Hide();
                            Room_host form = new Room_host(username, infor.Id);
                            form.ShowDialog();
                            Close();
                        }
                        else
                        {
                            var infor = GetWriteData(id, username);
                            DocumentReference docRef2 = db.Collection("RoomData").Document(infor.Id);
                            await docRef2.SetAsync(infor);
                            MessageBox.Show("Success");
                            Hide();
                            Room2_host form = new Room2_host(username, infor.Id);
                            form.ShowDialog();
                            Close();
                        }

                    }
                }
            }
        }
        private RoomData GetWriteData(string id, string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            List<string> mem2 = new List<string>();
            List<string> his = new List<string>();
            mem2.Add(username);
   
            data.Mem = data.Mem.Append(id).ToArray();
            data.Host = data.Host.Append(id).ToArray();

            
            var updateTask = docRef.UpdateAsync(new Dictionary<string, object>
{
            { nameof(UserData.Host), data.Host },
            { nameof(UserData.Mem), data.Mem }
});
            updateTask.Wait();
            bool type = comboBox1.SelectedItem.ToString() == "Text" ? false : true;
            return new RoomData()
            {
                Id = id,
                Mem = mem2.ToArray(),
                Host = data.Id,
                His = his.ToArray(),
                Type=type,
                URL=""
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int a = listView1.SelectedItems.Count;
            int b = listView2.SelectedItems.Count;
            if ((a+b)>1)
            {
                MessageBox.Show("Please choose a room!");
                return;
            }
            ListViewItem selectedRoomItem;
            if (a==1)selectedRoomItem = listView1.SelectedItems[0];
            else selectedRoomItem = listView2.SelectedItems[0];
            string id= selectedRoomItem.Text;
            if (Check_host(id))
            {
                if (a == 1)
                {
                    Hide();
                    Room_host roomHostForm = new Room_host(username, id);
                    roomHostForm.ShowDialog();
                    Close();
                }
                else
                {
                    Hide();
                    Room2_host roomHostForm = new Room2_host(username, id);
                    roomHostForm.ShowDialog();
                    Close();
                }
            }
            else
            {
                if (a == 1)
                {
                    Hide();
                    Room_user roomUserForm = new Room_user(username, id);
                    roomUserForm.ShowDialog();
                    Close();
                }
                else
                {
                    Hide();
                    Room2_host roomHostForm = new Room2_host(username, id);
                    roomHostForm.ShowDialog();
                    Close();
                }
            }
        }
        private bool Check_host(string id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach (string i in data.Host)
            {
                if (i == id) return true;
            }
            return false;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            
            
            foreach (string i in listBox1.SelectedItems)
            {
                data.Mem = data.Mem.Append(i).ToArray();
                await AddData(username, i);
                await DeleteInvite(username, i);
            }
            await docRef.UpdateAsync(new Dictionary<string, object>
            {
                { nameof(UserData.Mem), data.Mem },
            });
            CheckInvite(username);
            CheckRoom(username);           
        }
        private async Task AddData(string username, string roomId)
        {
            var db = FirestoreHelper.Database;
            DocumentReference roomDocRef = db.Collection("RoomData").Document(roomId);
            DocumentSnapshot roomSnapshot = await roomDocRef.GetSnapshotAsync();
            RoomData roomData = roomSnapshot.ConvertTo<RoomData>();

            List<string> mem = roomData.Mem.ToList();
            mem.Add(username);
            roomData.Mem = mem.ToArray();

            await roomDocRef.UpdateAsync(new Dictionary<string, object>
    {
        { nameof(RoomData.Mem), roomData.Mem }
    });
        }

        private async Task DeleteInvite(string username, string roomId)
        {
            var db = FirestoreHelper.Database;
            DocumentReference userDocRef = db.Collection("UserData").Document(username);
            DocumentSnapshot userSnapshot = await userDocRef.GetSnapshotAsync();
            UserData userData = userSnapshot.ConvertTo<UserData>();

            List<string> invites = userData.Is_invited.ToList();
            invites.Remove(roomId);
            userData.Is_invited = invites.ToArray();

            await userDocRef.UpdateAsync(new Dictionary<string, object>
    {
        { nameof(UserData.Is_invited), userData.Is_invited }
    });
        }
        private async void button6_Click(object sender, EventArgs e)
        {
            foreach (string i in listBox1.SelectedItems)
            {
                await DeleteInvite(username,i);
            }
            CheckInvite(username);
        }
    }
}
