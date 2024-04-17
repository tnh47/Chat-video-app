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
        public Lobby(string username)
        {
            InitializeComponent();
            CheckRoom(username);
        }
        private void CheckRoom(string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach(string h in data.Host)
            {
                DocumentReference docRef2 = db.Collection("RoomData").Document(h);
                RoomData data2 = docRef2.GetSnapshotAsync().Result.ConvertTo<RoomData>();
                string nameroom= data2.Name;
                listView1.Items.Add(nameroom);
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
    }
}
