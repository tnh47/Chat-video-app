using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class ForgotPass : Form
    {
        Random rnd = new Random();
        public int code;
        public string username;

        public ForgotPass()
        {
            InitializeComponent();
        }
        private void mail(string Email)
        {
            code = rnd.Next(000000, 999999);
            const string p = "dtmiuysgrukpaerj";

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("ttn4704@gmail.com");
            message.To.Add(new MailAddress(Email));
            message.Subject = "Change password";
            message.Body = "Write this given code on text box\n" + code + "\nThank you!";

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("ttn4704@gmail.com", p);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            notmsg.Text = "Email has been sent.";
            errmsg1.Visible = false;
            notmsg.Visible = true;
        }
        private void SendCodeBtn_Click(object sender, EventArgs e)
        {
            string txtboxEmail = EmailBox.Text;
            var db = FirestoreHelper.Database;

            // Tạo truy vấn để tìm tài liệu có trường email giống với giá trị nhập từ hộp văn bản
            Query query = db.Collection("UserData").WhereEqualTo("Email", txtboxEmail);

            // Thực hiện truy vấn
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;

            // Kiểm tra xem có tài liệu nào khớp với địa chỉ email đã nhập không
            if (querySnapshot.Count > 0)
            {
                // Lấy tài liệu đầu tiên từ kết quả truy vấn
                DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];

                // Lấy giá trị của trường 'username' (hoặc bất kỳ trường nào bạn cần) từ tài liệu
                username = documentSnapshot.GetValue<string>("Username");

                // Gọi phương thức mail để gửi mã xác nhận qua email
                mail(txtboxEmail);
            }
            else
            {
                errmsg1.Text = "Email does not exist.";
                errmsg1.Visible = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            Close();
        }

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            if (code.ToString() == CodeBox.Text)
            {
                Hide();
                ResetPass form = new ResetPass(username);
                form.ShowDialog();
                Close();
            }
            else
            {
                errmsg2.Text = "Code does not match.";
                errmsg2.Visible = true;
            }
        }
    }
}
