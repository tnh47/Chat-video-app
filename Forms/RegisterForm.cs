using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.RegularExpressions;

namespace Chat_video_app.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        public static string GenerateUniqueUserId(string username, string email)
        {
            // Kết hợp thông tin của người dùng để tạo chuỗi ngẫu nhiên
            string combinedInfo = username + email + DateTime.Now.ToString();

            // Sử dụng hàm băm SHA256 để tạo chuỗi băm từ thông tin kết hợp
            byte[] bytes = Encoding.UTF8.GetBytes(combinedInfo);
            byte[] hash;
            using (SHA256 sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(bytes);
            }

            // Chuyển đổi chuỗi băm thành một số nguyên
            int userId = BitConverter.ToInt32(hash, 0);

            // Đảm bảo số nguyên không âm bằng cách sử dụng giá trị tuyệt đối
            userId = Math.Abs(userId);

            return userId.ToString();
        }
        private async void RegisterBtn_Click(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            string username = UsernameBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string password = PasswordBox.Text.Trim();
            string confirmPassword = ConfirmPasswordBox.Text.Trim();

            // Kiểm tra nếu một trong các ô trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                errorMsg.Text = "Please fill in all fields.";
                errorMsg.Visible = true;
                return;
            }

            // Kiểm tra tính hợp lệ của email
            if (!IsValidEmail(email))
            {
                errorMsg.Text = "Invalid email address.";
                errorMsg.Visible = true;
                EmailBox.Clear();
                return;
            }

            // Kiểm tra tính hợp lệ của username
            string regexPattern = @"^[a-z0-9]{5,}$";

            if (!Regex.IsMatch(username, regexPattern))
            {
                errorMsg.Text = "Username must have at least 5 letters and\ncan only contain lowercase letters and numbers.";
                errorMsg.Visible = true;
                UsernameBox.Clear(); // Xóa nội dung của TextBox username
                return;

            }

            // Kiểm tra tính hợp lệ của password
            if (password.Length < 8)
            {
                errorMsg.Text = "Password must be at least 8 characters long.";
                errorMsg.Visible = true;
                PasswordBox.Clear();
                ConfirmPasswordBox.Clear();
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (password != confirmPassword)
            {
                errorMsg.Text = "Passwords do not match.";
                errorMsg.Visible = true;
                ConfirmPasswordBox.Clear();
                return;
            }
            if (CheckIfEmailAlreadyExist())
            {
                errorMsg.Text = "Email already exists.";
                errorMsg.Visible = true;
                EmailBox.Clear();
                return;
            }
            // Kiểm tra xem người dùng đã tồn tại chưa
            if (CheckIfUserAlreadyExist())
            {
                MessageBox.Show("User Already Exist");
                return;
            }
            var data = GetWriteData();
            DocumentReference docRef = db.Collection("UserData").Document(data.Username);
            await docRef.SetAsync(data);
            MessageBox.Show("Success");
            
        }
        private UserData GetWriteData()
        {
            string username = UsernameBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string password = Security.Encrypt(PasswordBox.Text.Trim());
            string confirmPassword = ConfirmPasswordBox.Text.Trim();
            string id = GenerateUniqueUserId(username, email); 
            List<string> mem=new List<string>();
            List<string> is_invited = new List<string>();
            List<string> host = new List<string>();
            return new UserData()
            {
                Id = id,
                Username = username,
                Email = email,
                Password = password,
                Is_invited = is_invited.ToArray(),
                Mem = mem.ToArray(),
                Host=host.ToArray(),
            };
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
        private bool CheckIfEmailAlreadyExist()
        {
            string email = EmailBox.Text.Trim();

            var db = FirestoreHelper.Database;
            var query = db.Collection("UserData").WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;

            // Nếu có bất kỳ tài khoản nào sử dụng địa chỉ email này, trả về true
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    return true;
                }
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
