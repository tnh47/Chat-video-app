using Lidgren.Network;
using System;

namespace Chat_video_app.Classes.Voice
{
    internal class Net
    {
        public const string APP_ID = "Team7";
        public static CClient Client;
        public static CServer Server;

        public static bool IsClient
        {
            get
            {
                return Client != null;
            }
        }

        public static bool IsServer
        {
            get
            {
                return Server != null;
            }
        }

        public static bool IsConnected
        {
            get
            {
                return IsClient && Client.Status == NetPeerStatus.Running && Client.ConnectionStatus == NetConnectionStatus.Connected;
            }
        }

        public static bool IsConnecting
        {
            get
            {
                return IsClient && Client.Status == NetPeerStatus.Running && Client.ConnectionStatus != NetConnectionStatus.Disconnected;
            }
        }

        public static void StartClient()
        {
            if (IsClient)
            {
                Forms.Room2_host.Log("Client is already started, cannot start a new one.");
                return;
            }

            var config = new NetPeerConfiguration(APP_ID);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            Client = new CClient(null, config);
            Client.Start();

            Forms.Room2_host.Log("Started client...");
        }

        public static void StopClient()
        {
            if (!IsClient)
            {
                Forms.Room2_host.Log("Client is not running...");
                return;
            }

            Client.Shutdown("The user has disconnected from the server. Bye!");
            Client = null;
        }

        public static bool ConnectClient(string ip, int port)
        {
            if (!IsClient)
                return false;
            if (IsConnected || IsConnecting)
                return false;

            Forms.Room2_host.Log("Attempting to connect to " + ip + " on port " + port);

            try
            {
                Client.Connect(ip, port, Client.CreateMessage(Forms.Room2_host.ActiveUsername));
                return true;
            }
            catch (Exception e)
            {
                Forms.Room2_host.Log(e);
                return false;
            }
        }

        public static void UponClientConnected()
        {
            Audio.InitAll();
            Forms.Room2_host.UponConnected();
        }

        public static void UponClientDisconnected()
        {
            Client = null;
            Audio.ClearAll();
            Forms.Room2_host.UponDisconnected();
        }

        public static void DiscoverPeers(int port, CClient.ServerFound callback)
        {
            if (!IsClient)
            {
                Forms.Room2_host.Log("Cannot discover peers, client is not started.");
                return;
            }
            Forms.Room2_host.Log("Scanning for local peers on " + port + "...");
            Client.StartDiscovery(port, callback);
        }

        public static bool StartServer(int port, string name)
        {
            if (IsServer)
            {
                Forms.Room2_host.Log("Server already started, shut it down before you open a new one.");
                return false;
            }
            if (IsClient && (IsConnected || IsConnecting))
            {
                Forms.Room2_host.Log("Cannot host server while client is active.");
                return false;
            }

            var config = new NetPeerConfiguration(APP_ID);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.Port = port;

            Server = new CServer(name, config);
            Server.Start();

            Forms.Room2_host.Log("Started hosting server on port " + port + " called " + name);

            Forms.Room2_host.SetStatus("Hosting Server (" + name.Trim() + ", " + port + ")");
            Audio.InitAll();

            return true;
        }

        public static void StopServer()
        {
            if (!IsServer)
                return;

            Server.Shutdown("The host has shutdown the server.");
            Server = null;

            Forms.Room2_host.Log("The locally hosted server has been shut down.");
            Audio.ClearAll();
            Forms.Room2_host.UponServerStop();
        }
    }
    public enum DataType : byte
    {
        AUDIO,
        MUTED,
        NAME
    }
}
