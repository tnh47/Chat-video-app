using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class Set_Del_room : Form
    {
        string username;
        public Set_Del_room(string username)
        {
            InitializeComponent();
            this.username = username;
            CheckRoom(username);
        }
        private void CheckRoom(string username)
        {
            listView1.Clear();
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();
            foreach (string h in data.Host)
            {
                listView1.Items.Add(h);
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                MessageBox.Show("Please choose room!");
                return;
            }
            foreach (ListViewItem i in listView1.SelectedItems)
            {
                string id = i.Text;
                var db = FirestoreHelper.Database;
                

                DocumentReference docRef2 = db.Collection("UserData").Document(username);
                UserData data = docRef2.GetSnapshotAsync().Result.ConvertTo<UserData>();
                List<string> host = new List<string>(data.Host);
                host.Remove(id);
                data.Host = host.ToArray();
                await docRef2.UpdateAsync(new Dictionary<string, object>
                {
                    { nameof(UserData.Host), data.Host },
                });

                var query = db.Collection("UserData").WhereArrayContains("Mem", id);
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        UserData otherUserData = documentSnapshot.ConvertTo<UserData>();
                        List<string> memList = otherUserData.Mem.ToList();

                        if (memList.Remove(id))
                        {
                            otherUserData.Mem = memList.ToArray();
                            await documentSnapshot.Reference.UpdateAsync(new Dictionary<string, object>
                            {
                                { nameof(UserData.Mem), otherUserData.Mem }
                            });
                        }
                    }
                }

                DocumentReference docRef = db.Collection("RoomData").Document(id);
                await docRef.DeleteAsync();
            }
            listView1.Clear();
            CheckRoom(username);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Setting form = new Setting(username);
            form.ShowDialog();
            Close();
        }
    }
}
