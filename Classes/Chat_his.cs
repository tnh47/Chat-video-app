using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_video_app.Classes
{
    internal class Chat_his
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string[] content { get; set; }
        [FirestoreProperty]
        public string[] timestamp { get; set; }
        [FirestoreProperty]
        public string[] send_id { get; set; }
    }
}
