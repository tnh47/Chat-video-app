using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_video_app.Classes
{
    internal class RoomData
    {
        [FirestoreProperty]
        public int Id { get; set; }
        [FirestoreProperty]
        public bool Type { get; set; }
    }
}
