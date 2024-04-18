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
            label2.Text = username;
        }
        private void CheckRoom(string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach(string h in data.Mem)
            {
                listView1.Items.Add(h);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Setting form = new Setting();
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
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            if (data!=null)
            {
                MessageBox.Show("Room đã tồn tại.Vui lòng chọn ID khác");
            }
            else{
                    var infor = GetWriteData(id,username);
                    AddData(id, username);
                    DocumentReference docRef2 = db.Collection("RoomData").Document(infor.Id);
                    await docRef2.SetAsync(infor);
                    MessageBox.Show("Success");
                    Hide();
                    Room_host form = new Room_host(username,infor.Id);
                    form.ShowDialog();
                    Close();
                }
            }
        private RoomData GetWriteData(string id, string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            data.Mem = data.Mem ?? new string[0]; // Đảm bảo rằng mảng đã được khởi tạo

            List<string> mem= data.Mem.ToList();
            mem.Add(id);
            return new RoomData()
            {
                Id = id,
                Mem = mem.ToArray(),
                Host = data.Id
            };
        }
        private void AddData(string id,string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            List<string> updatedMemList = new List<string>();

            if (data.Mem != null)
            {
                updatedMemList.AddRange(data.Mem);
            }
            updatedMemList.Add(id);

            data.Mem = updatedMemList.ToArray();

            var updateTask = docRef.UpdateAsync(nameof(UserData.Mem), data.Mem);
            updateTask.Wait(); // Chờ cho tác vụ cập nhật hoàn thành

        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool accept = false;
            string id = textBox3.Text.Trim();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == id)
                {
                    accept = true;
                    MessageBox.Show("Success");
                    
                    if (Check_host(id))
                    {
                        Hide();
                        Room_host form = new Room_host(username,id);
                        form.ShowDialog();
                        Close();
                    }
                    else
                    {
                        Hide();
                        Room_user form = new Room_user(username,id);
                        form.ShowDialog();
                        Close();
                    }
                }
            }
            if(!accept)MessageBox.Show("Bạn chưa vào phòng này hoặc ko tồn tại");
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
    }
}
