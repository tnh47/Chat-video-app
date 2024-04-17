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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        private async void RegisterBtn_Click(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            if (CheckIfUserAlreadyExist())
            {
                MessageBox.Show("User Already Exist");
                return;
            }
            var data = await GetWriteData();
            DocumentReference docRef = db.Collection("UserData").Document(data.Username);
            await docRef.SetAsync(data);
            MessageBox.Show("Success");
            
        }
        private async Task<UserData> GetWriteData()
        {
            string username = UsernameBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string password = Security.Encrypt(PasswordBox.Text.Trim());
            string confirmPassword = ConfirmPasswordBox.Text.Trim();
            int id = await GetUsersCount();
            return new UserData()
            {
                Id = id + 1,
                Username = username,
                Email = email,
                Password = password,
                Room_crt = 0
            };
        }
        private async Task<int> GetUsersCount()
        {
            var db = FirestoreHelper.Database;
            QuerySnapshot snapshot = await db.Collection("UserData").GetSnapshotAsync();
            return snapshot.Count();
        }
        private bool CheckIfUserAlreadyExist()
        {
            string username = UsernameBox.Text.Trim();

            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            if (data != null)
            {
                return true;
            }
            return false;
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
