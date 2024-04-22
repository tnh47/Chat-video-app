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
    internal class RoomData
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public bool Type { get; set; }
        [FirestoreProperty]
        public string Host{ get; set; }
        [FirestoreProperty]
        public string[] Mem { get; set; }
        [FirestoreProperty]
        public string[]His { get; set; }
        [FirestoreProperty]
        public string URL { get; set; }
    }
}
