using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class Search_mess : Form
    {
        string id, text;
        public Search_mess(string id,string text)
        {
            InitializeComponent();
            this.id = id;
            this.text = text;
            Search(id, text);
        }
        public void Search(string id,string text)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            foreach (string i in data.His)
            {
                if (i.Contains(text))
                {
                    listBox1.Items.Add(i);
                }
            }
        }

    }
}
