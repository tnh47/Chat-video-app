using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using Google.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            errorMsg.Visible = false;
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Text.Trim();
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                errorMsg.Text = "Please enter your username and password.";
                errorMsg.Visible = true;
                return;
            }
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            if (data != null )
            {
                if(password == Security.Decrypt(data.Password))
                {
                    //MessageBox.Show("Login Success");
                    Hide();
                    Lobby form = new Lobby(username);
                    form.ShowDialog();
                    Close();
                }
                else
                {
                    errorMsg.Text = "Incorrect Username or Password.";
                    errorMsg.Visible = true;
                }
            }
            else
            {
                errorMsg.Text = "Incorrect Username or Password.";
                errorMsg.Visible = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Hide();
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
            Close();
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked)
            {
                PasswordBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordBox.UseSystemPasswordChar = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Hide();
            ForgotPass form = new ForgotPass();
            form.ShowDialog();
            Close();
        }
    }
}
