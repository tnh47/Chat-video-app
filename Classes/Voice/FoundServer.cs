using System.Net;


namespace Chat_video_app.Classes.Voice
{
    internal class FoundServer
    {
        public string Name;
        public IPEndPoint EndPoint;

        public override string ToString()
        {
            return Name == null ? "" : Name.Trim();
        }

        public bool IsInvalid()
        {
            return this.EndPoint == null;
        }
    }
}
