using Google.Cloud.Firestore;
using Google.Protobuf.Collections;
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
        public string Id { get; set; }
        [FirestoreProperty]
        public string Username { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }
        [FirestoreProperty]
        public string[] Host { get; set; }
        [FirestoreProperty]
        public string[] Mem { get; set; }
        [FirestoreProperty]
        public string[] Is_invited { get; set; }
    }
}
