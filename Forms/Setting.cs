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
    public partial class Setting : Form
    {
        string username;
        public Setting(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            Lobby form = new Lobby(username);
            form.ShowDialog();
            Close();
        }

        // Change username
        private async void button1_Click(object sender, EventArgs e)
        {
            string newUserName = textBox1.Text.Trim();
            MessageBox.Show(username.ToString());
            if(newUserName.Length > 0)
            {
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(username);
                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                if (data.Username != newUserName)
                {
                    data.Username = newUserName;
                    await docRef.SetAsync(data);
                    textBox1.Clear();
                    MessageBox.Show("Changed Username Successful!!!");
                }
                else
                {
                    MessageBox.Show("Username muốn thay đổi trùng với Username cũ! Vui lòng nhập Username mới!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập vào Username muốn thay đổi!");
            }
        }

        // Change email
        private async void button2_Click(object sender, EventArgs e)
        {
            string newEmail = textBox2.Text.Trim();
            if(newEmail.Length > 0)
            {
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(username);
                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                if(data.Email != newEmail)
                {
                    data.Email = newEmail;
                    await docRef.SetAsync(data);
                    textBox2.Clear();
                    MessageBox.Show("Changed Email Successful!!!");
                }
                else
                {
                    MessageBox.Show("Email muốn thay đổi trùng với Email cũ! Vui lòng nhập Email mới!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập vào email muốn thay đổi!");
            }
        }

        // Change password
        private async void button3_Click(object sender, EventArgs e)
        {
            string checkPassword = textBox3.Text.Trim();
            if(checkPassword.Length > 0)
            {
                string newPassword = Security.Encrypt(checkPassword);
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(username);
                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                if (data.Password != newPassword)
                {
                    data.Password = newPassword;
                    await docRef.SetAsync(data);
                    textBox3.Clear();
                    MessageBox.Show("Changed Password Successful!!!");
                }
                else
                {
                    MessageBox.Show("Password muốn thay đổi trùng với Password cũ! Vui lòng nhập Password mới!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập vào Password cần thay đổi!");
            }
        }
    }
}
