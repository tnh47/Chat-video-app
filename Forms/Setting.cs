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
                    MessageBox.Show("Changed Username Successful!!!");
                }
                else
                {
                    MessageBox.Show("Username muốn thay đổi trùng với Username cũ! Vui lòng nhập Username mới!");
                }
                textBox1.Clear();
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
                    MessageBox.Show("Changed Email Successful!!!");
                }
                else
                {
                    MessageBox.Show("Email muốn thay đổi trùng với Email cũ! Vui lòng nhập Email mới!");
                }
                textBox2.Clear();
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
                    MessageBox.Show("Changed Password Successful!!!");
                }
                else
                {
                    MessageBox.Show("Password muốn thay đổi trùng với Password cũ! Vui lòng nhập Password mới!");
                }
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập vào Password cần thay đổi!");
            }
        }

        // Delete room
        private void button5_Click(object sender, EventArgs e)
        {
            string text = ShowInputDialog_Del_Room("Nhập vào Id phòng mà bạn muốn xóa:", "Xử lý xóa phòng");
            if (text == "-1" || text == "-2") return;
            if (text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập Id phòng! Vui lòng nhập Id phòng để xóa!");
                button5_Click(sender, e);
            }
            else
            {
                // Xử lý sự kiện delete room
                MessageBox.Show("Id phòng bạn vừa nhập là: " + text);
            }
        }

        public static string ShowInputDialog_Del_Room(string prompt, string title)
        {
            string result = "";
            using (Form promptForm = new Form())
            {
                promptForm.Width = 400;
                promptForm.Height = 200;
                promptForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                promptForm.Text = title;
                promptForm.StartPosition = FormStartPosition.CenterScreen;

                Label textLabel = new Label() { Left = 50, Top = 20, Text = prompt };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 300, Height = 25 };
                Button confirmation = new Button() { Text = "OK", Left = 290, Width = 60, Top = 90, Height = 25, DialogResult = DialogResult.OK };
                Button cancelation = new Button() { Text = "Cancel", Left = 200, Width = 60, Top = 90, Height = 25, DialogResult = DialogResult.Cancel };

                confirmation.Click += (sender, e) => { result = textBox.Text; promptForm.Close(); };
                cancelation.Click += (sender, e) => { promptForm.Close(); };

                promptForm.Controls.Add(textBox);
                promptForm.Controls.Add(confirmation);
                promptForm.Controls.Add(cancelation);
                promptForm.Controls.Add(textLabel);
                promptForm.AcceptButton = confirmation;
                promptForm.CancelButton = cancelation;

                

                if (promptForm.ShowDialog() == DialogResult.Cancel)
                {
                    result = "-1";
                    promptForm.Close();
                }

                promptForm.FormClosing += (sender, e) =>
                {
                    if (e.CloseReason == CloseReason.UserClosing)
                    {
                        result = "-2";
                    }
                };
            }

            return result;
        }

        // Delete account
        private void button4_Click(object sender, EventArgs e)
        {
            string text = ShowInputDialog_Del_Account("Bạn có chắc muốn xóa vĩnh viễn tài khoản?", "Xác nhận xóa tài khoản!");
            if (text == "0")
            {
                // xu ly su kien khong xoa tai khoan
                MessageBox.Show("Delete Account Failure!");
            }
            else if(text == "1")
            {
                // xu ly su kien xoa tai khoan
                MessageBox.Show("Delete Account Successful!");
            }
        }


        public static string ShowInputDialog_Del_Account(string prompt, string title)
        {
            string result = "0";
            using (Form promptForm = new Form())
            {
                promptForm.Width = 300;
                promptForm.Height = 150;
                promptForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                promptForm.Text = title;
                promptForm.StartPosition = FormStartPosition.CenterScreen;

                Label textLabel = new Label() { Left = 20, Top = 20, Width = 300, Height = 30,  Text = prompt };
                Button confirmation = new Button() { Text = "OK", Left = 100, Width = 60, Top = 60, Height = 25, DialogResult = DialogResult.OK };
                Button cancelation = new Button() { Text = "Cancel", Left = 190, Width = 60, Top = 60, Height = 25, DialogResult = DialogResult.Cancel };

                confirmation.Click += (sender, e) => { result = "1"; promptForm.Close(); };
                cancelation.Click += (sender, e) => { result = "0"; promptForm.Close(); };

                promptForm.Controls.Add(confirmation);
                promptForm.Controls.Add(cancelation);
                promptForm.Controls.Add(textLabel);
                promptForm.AcceptButton = confirmation;
                promptForm.CancelButton = cancelation;

                promptForm.ShowDialog();
            }
            return result;
        }
    }
}
