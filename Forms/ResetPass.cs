using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Windows.Forms;

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
                notimsg.Text = "Changed Password Successful!!!";
                errmsg.Visible = false;
                notimsg.Visible = true;
                PasswordBox.Visible = false;
                ConfirmPasswordBox.Visible = false;
                VerifyBtn.Visible = false;
                checkBoxShowPass.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
            }
            else
            {
                if (checkPassword.Length < 8)
                {
                    errmsg.Text = "Password must be at least 8 characters long.";
                    errmsg.Visible = true;
                }
                else
                {
                    errmsg.Text = "Passwords do not match.";
                    errmsg.Visible = true;
                }
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
