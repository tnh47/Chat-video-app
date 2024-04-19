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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chat_video_app.Forms
{
    public partial class ResetPass : Form
    {
        string username;
        public ResetPass(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private async void VerifyBtn_Click(object sender, EventArgs e)
        {
            string checkPassword = PasswordBox.Text.Trim();
            if (checkPassword.Length > 0 && (PasswordBox.Text.Trim() == ConfirmPasswordBox.Text.Trim())) 
            {
                string newPassword = Security.Encrypt(checkPassword);
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(username);
                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                data.Password = newPassword;
                await docRef.SetAsync(data);
                PasswordBox.Clear();
                ConfirmPasswordBox.Clear();
                MessageBox.Show("Changed Password Successful!!!");                
            }
            else
            {
                MessageBox.Show("Lỗi!");
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            Close();
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked)
            {
                PasswordBox.UseSystemPasswordChar = false;
                ConfirmPasswordBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordBox.UseSystemPasswordChar = true;
                ConfirmPasswordBox.UseSystemPasswordChar = true;
            }
        }
    }
}
