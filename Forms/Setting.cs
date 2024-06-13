using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chat_video_app.Forms
{
    public partial class Setting : Form
    {
        string username;
        public Setting(string username)
        {
            InitializeComponent();
            this.username = username;
            WriteData(username);
        }
        private void WriteData(string username)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            textBox1.Text = data.Username;
            textBox2.Text = data.Email;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            Lobby form = new Lobby(username);
            form.ShowDialog();
            Close();
        }

        // Save change
        private async void button2_Click(object sender, EventArgs e)
        {
            string newEmail = textBox2.Text.Trim();
            string checkPassword = textBox3.Text.Trim();
            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Invalid email format!");
            }
            else if (newEmail.Length > 0 && (checkPassword.Length >= 8 || checkPassword.Length == 0))
            {
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(username);
                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                data.Email = newEmail;
                await docRef.SetAsync(data);

                if (checkPassword.Length >= 8)
                {
                    string newPassword = Security.Encrypt(checkPassword);
                    db = FirestoreHelper.Database;
                    docRef = db.Collection("UserData").Document(username);
                    data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                    data.Password = newPassword;
                    await docRef.SetAsync(data);
                    textBox3.Clear();
                }

                MessageBox.Show("Changed Succeed!");
            }
            else if (checkPassword.Length > 0 && checkPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 6 characters long!");
            }
            else if(newEmail.Length == 0)
            {
                MessageBox.Show("Input email that you need change!");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Delete room
        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            Set_Del_room form = new Set_Del_room(username);
            form.ShowDialog();
            Close();
        }
        // Delete account
        private async void button4_Click(object sender, EventArgs e)
        {

            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach (string i in data.Host)
            {
                DocumentReference docRef2 = db.Collection("RoomData").Document(i);
                await docRef2.DeleteAsync();
            }
            foreach (string i in data.Mem)
            {
                DocumentReference docRef2 = db.Collection("RoomData").Document(i);
                List<string> mem = new List<string>(data.Mem);
                mem.Remove(username);
                data.Mem = mem.ToArray();
                await docRef2.SetAsync(data);
            }
            await docRef.DeleteAsync();
            MessageBox.Show("Deleted successfully");
            Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            Close();
        }
    }
}
