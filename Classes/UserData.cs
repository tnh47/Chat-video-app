using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_video_app.Classes
{
    [FirestoreData]
    internal class UserData
    {
        [FirestoreProperty]
        public int Id { get; set; }
        [FirestoreProperty]
        public string Username { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }
        [FirestoreProperty]
        public int Room_crt { get; set; }
    }
}
