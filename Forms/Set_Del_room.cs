using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class Set_Del_room : Form
    {
        string username;
        public Set_Del_room(string username)
        {
            InitializeComponent();
            this.username = username;
            CheckRoom(username);
        }
        private void CheckRoom(string username)
        {
            listView1.Clear();
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach (string h in data.Host)
            {
                listView1.Items.Add(h);
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                MessageBox.Show("Please choose room!");
                return;
            }
            foreach (ListViewItem i in listView1.SelectedItems)
            {
                string id = i.Text;
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("RoomData").Document(id);
                await docRef.DeleteAsync();

                DocumentReference docRef2 = db.Collection("UserData").Document(username);
                UserData data = docRef2.GetSnapshotAsync().Result.ConvertTo<UserData>();
                List<string> host = new List<string>(data.Host);
                List<string> mem=new List<string>(data.Mem);
                host.Remove(id);
                mem.Remove(id);
                data.Host = host.ToArray();
                data.Mem = mem.ToArray();
                await docRef2.SetAsync(data);
            }
            MessageBox.Show("Deleted successfully!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Setting form = new Setting(username);
            form.ShowDialog();
            Close();
        }
    }
}
