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

namespace Chat_video_app.Forms
{
    public partial class Room_host : Form
    {
        string id;
        string username;
        public Room_host(string username,string id)
        {
            InitializeComponent();
            usernameTextBox.Text = username;
            portTextBox.Text = id;
            this.id = id;
            this.username= username;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string name=textBox3.Text.Trim();
            if (name.Length == 0 || name==username) MessageBox.Show("Tên ko hợp lệ");
            else
            {
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(name);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    UserData data = snapshot.ConvertTo<UserData>();
                    data.Is_invited = data.Is_invited ?? new string[0]; // Đảm bảo rằng mảng đã được khởi tạo

                    List<string> invitedRooms = data.Is_invited.ToList();
                    invitedRooms.Add(id);
                    data.Is_invited = invitedRooms.ToArray();

                    // Ghi lại dữ liệu lên Firestore
                    await docRef.SetAsync(data);

                    // Hiển thị thông báo sau khi dữ liệu đã được ghi lại thành công
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng có tên là " + name);
                }
            }
        }
    }
}
