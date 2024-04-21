using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        }
        private void CheckRoom(string username)
        { 
            listView1.Clear();
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach(string h in data.Mem)
            {
                listView1.Items.Add(h);
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
                        MessageBox.Show("Room đã tồn tại.Vui lòng chọn ID khác");
                    }
                    else
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
                }
            }
        }
        private RoomData GetWriteData(string id, string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            List<string> mem2 = new List<string>();
            mem2.Add(username);
   
            data.Mem = data.Mem.Append(id).ToArray();
            data.Host = data.Host.Append(id).ToArray();

            
            var updateTask = docRef.UpdateAsync(new Dictionary<string, object>
{
            { nameof(UserData.Host), data.Host },
            { nameof(UserData.Mem), data.Mem }
});
            updateTask.Wait();


            return new RoomData()
            {
                Id = id,
                Mem = mem2.ToArray(),
                Host = data.Id
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn một phòng.");
                return;
            }

            ListViewItem selectedRoomItem = listView1.SelectedItems[0];
            string id= selectedRoomItem.Text;
            if (Check_host(id))
            {
                Hide();
                Room_host roomHostForm = new Room_host(username, id);
                roomHostForm.ShowDialog();
                Close();
            }
            else
            {
                Hide();
                Room_user roomUserForm = new Room_user(username, id);
                roomUserForm.ShowDialog();
                Close();
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
            List<string> mem = data.Mem.ToList();
            
            
            foreach (string i in listBox1.SelectedItems)
            {
                mem.Add(i);
                DeleteInvite(username, i);
                AddData(username,i);                
            }
            data.Mem = mem.ToArray();
            await docRef.SetAsync(data);            
            CheckInvite(username);
            CheckRoom(username);           
        }
        private async void AddData(string username, string id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            List<string>mem= data.Mem.ToList();
            mem.Add(username);
            data.Mem=mem.ToArray();
            await docRef.SetAsync(data);
        }
        private async void DeleteInvite(string username, string roomid)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            List<string> i = new List<string>(data.Is_invited);
            i.Remove(roomid);
            data.Is_invited = i.ToArray();
            await docRef.SetAsync(data);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            foreach (string i in listBox1.SelectedItems)
            {
                DeleteInvite(username,i);
            }
            CheckInvite(username);
        }
       
    }
}
