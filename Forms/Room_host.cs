using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using Google.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Chat_video_app.Forms
{
    public partial class Room_host : Form
    {
        string id;
        string username;
        private bool active = false;
        private Thread listener = null;
        private struct MyClient
        {
            public long id;
            public StringBuilder username;
            public TcpClient client;
            public NetworkStream stream;
            public byte[] buffer;
            public StringBuilder data;
            public EventWaitHandle handle;
        };
        private ConcurrentDictionary<long, MyClient> clients = new ConcurrentDictionary<long, MyClient>();
        private Task send = null;
        private Thread disconnect = null;
        private bool exit = false;

        private Process ngrok;
        public Room_host(string username,string id)
        {
            InitializeComponent();
            usernameTextBox.Text = username;
            portTextBox.Text = id;
            this.id = id;
            this.username= username;
            sendTextBox.KeyDown += SendTextBox_KeyDown;
            clientsDataGridView.CellClick += new DataGridViewCellEventHandler(clientsDataGridView_CellClick);
            DisplayMem(id);
            Startngrok();
        }
        private void Startngrok()
        {
            string ngrokPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ngrok", "ngrok.exe");
            string arguments = "tcp "+id;
            ngrok = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{ngrokPath}\" {arguments}",
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };
            ngrok.Start();
        }
        private void DisplayMem(string id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            foreach (string i in data.Mem)
            {
                if(i==username) continue;
                DocumentReference docRef2 = db.Collection("UserData").Document(i);
                UserData data2 = docRef2.GetSnapshotAsync().Result.ConvertTo<UserData>();
                AddGrid(data2.Id, data2.Username);
            }
        }
        private void AddGrid(string id,string name)
        {
            string[] row = new string[] {id, name };//fix
            clientsDataGridView.Rows.Add(row);
        }
        private async void clientsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && clientsDataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = clientsDataGridView.Rows[e.RowIndex];
                string memberName = selectedRow.Cells["Name"].Value.ToString();
                MyClient? clientToRemove = clients.Values.FirstOrDefault(c => c.username.ToString() == memberName);
                if (clientToRemove != null)
                {
                    Disconnect(clientToRemove.Value.id);
                }
                clientsDataGridView.Rows.Remove(selectedRow);
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("RoomData").Document(id);
                RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
                List<string> mem = new List<string>(data.Mem);
                mem.Remove(memberName);
                data.Mem = mem.ToArray();
                await docRef.SetAsync(data);
                DocumentReference docRef2 = db.Collection("UserData").Document(memberName);
                UserData data2 = docRef2.GetSnapshotAsync().Result.ConvertTo<UserData>();
                List<string> mem2 = new List<string>(data2.Mem);
                mem2.Remove(id);
                data2.Mem = mem2.ToArray();
                await docRef2.SetAsync(data2);
            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            string name=textBox3.Text.Trim();
            if (name.Length == 0 || name == username) MessageBox.Show("Name invalid!");
            else
            {
                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(name);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {

                    UserData data = snapshot.ConvertTo<UserData>();
                    if (!Check(data)) MessageBox.Show("Name invalid!");
                    else {
                        List<string> invitedRooms = data.Is_invited.ToList();
                        invitedRooms.Add(id);
                        data.Is_invited = invitedRooms.ToArray();

                        await docRef.SetAsync(data);

                        MessageBox.Show("Success");
                    }
                }
                else
                {
                    MessageBox.Show("Cann't find username:  " + name);
                }
            }
        }
        private bool Check(UserData data)
        {
            foreach(string mem in data.Mem)
            {
                if (mem == id) return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Del_url(id);
            Disconnect();
            Hide();
            Lobby form = new Lobby(username);
            form.ShowDialog();
            Close();
        }
        private async void AddData(string text)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data=docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            List<string> his = data.His.ToList();
            his.Add(text);
            data.His = his.ToArray();
            await docRef.SetAsync(data);
        }
        private void Log(string msg = "") 
        {
            if (!exit)
            {
                logTextBox.Invoke((MethodInvoker)delegate
                {
                    if (msg.Length > 0)
                    {
                        string text = string.Format("[ {0} ] {1}", System.DateTime.Now.ToString("HH:mm"), msg);
                        if(active==true)AddData(text);
                        logTextBox.AppendText(text);
                        logTextBox.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        logTextBox.Clear();
                    }
                });
            }
        }
        private string ErrorMsg(string msg)
        {
            return string.Format("ERROR: {0}", msg);
        }
        private string SystemMsg(string msg)
        {
            return string.Format("SYSTEM: {0}", msg);
        }
        private void Active(bool status)
        {
            if (!exit)
            {
                startButton.Invoke((MethodInvoker)delegate
                {
                    active = status;
                    if (status)
                    {
                        startButton.Text = "Stop";
                        Log(SystemMsg("Server has started"));
                    }
                    else
                    {
                        startButton.Text = "Start";
                        Log(SystemMsg("Server has stopped"));
                    }
                });
            }
        }
        private void Read(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
            if (bytes > 0)
            {
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                    }
                    else
                    {
                        string msg = string.Format("{0}: {1}", obj.username, obj.data);
                        Log(msg);
                        Send(msg, obj.id);
                        obj.data.Clear();
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    obj.data.Clear();
                    Log(ErrorMsg(ex.Message));
                    obj.handle.Set();
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
            }
        }
        private void ReadAuth(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
            if (bytes > 0)
            {
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), obj);
                    }
                    else
                    {
                        var settings = new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All // This line is for demo, adjust as needed
                        };

                        // Deserialize using Newtonsoft.Json
                        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(obj.data.ToString(), settings);

                        if (!data.ContainsKey("username") || data["username"].Length < 1)
                        {
                            obj.client.Close();
                        }
                        else
                        {
                            obj.username.Append(data["username"].Length > 200 ? data["username"].Substring(0, 200) : data["username"]);
                            Send("{\"status\": \"authorized\"}", obj);
                        }
                        obj.data.Clear();
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    obj.data.Clear();
                    Log(ErrorMsg(ex.Message));
                    obj.handle.Set();
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
            }
        }
        private bool Authorize(MyClient obj) // no
        {
            bool success = false;
            while (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), obj);
                    obj.handle.WaitOne();
                    if (obj.username.Length > 0)
                    {
                        success = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
            return success;
        }
        private void Connection(MyClient obj)
        {
            if (Authorize(obj))
            {
                clients.TryAdd(obj.id, obj);
                string msg = string.Format("{0} has connected", obj.username);
                Log(SystemMsg(msg));
                Send(SystemMsg(msg), obj.id);
                while (obj.client.Connected)
                {
                    try
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                        obj.handle.WaitOne();
                    }
                    catch (Exception ex)
                    {
                        Log(ErrorMsg(ex.Message));
                    }
                }
                obj.client.Close();
                clients.TryRemove(obj.id, out MyClient tmp);
                msg = string.Format("{0} has disconnected", tmp.username);
                Log(SystemMsg(msg));
                Send(msg, tmp.id);
            }
        }
        private void Listener(IPAddress ip, int port)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(ip, port);
                listener.Start();
                Active(true);
                while (active)
                {
                    if (listener.Pending())
                    {
                        try //fix
                        {
                            MyClient obj = new MyClient();
                            obj.id = int.Parse(id);
                            obj.username = new StringBuilder();
                            obj.client = listener.AcceptTcpClient();
                            obj.stream = obj.client.GetStream();
                            obj.buffer = new byte[obj.client.ReceiveBufferSize];
                            obj.data = new StringBuilder();
                            obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                            Thread th = new Thread(() => Connection(obj))
                            {
                                IsBackground = true
                            };
                            th.Start();
                        }
                        catch (Exception ex)
                        {
                            Log(ErrorMsg(ex.Message));
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
                Active(false);
            }
            catch (Exception ex)
            {
                Log(ErrorMsg(ex.Message));
            }
            finally
            {
                if (listener != null)
                {
                    listener.Server.Close();
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (active)
            {
                active = false;
                Disconnect();// Dừng server hiện tại nếu đang chạy
            }
            else if (listener == null || !listener.IsAlive)
            {
                string address = "127.0.0.1";
                string number = portTextBox.Text.Trim();
                string username = usernameTextBox.Text.Trim();
                bool error = false;
                IPAddress ip = null;
                if (address.Length < 1)
                {
                    error = true;
                    Log(SystemMsg("Address is required"));
                }
                else
                {
                    try
                    {
                        ip = Dns.GetHostEntry(address).AddressList[0];
                    }
                    catch
                    {
                        error = true;
                        Log(SystemMsg("Address is not valid"));
                    }
                }
                int port = -1;
                if (number.Length < 1)
                {
                    error = true;
                    Log(SystemMsg("Port number is required"));
                }
                else if (!int.TryParse(number, out port))
                {
                    error = true;
                    Log(SystemMsg("Port number is not valid"));
                }
                else if (port < 49152 || port > 65535)
                {
                    error = true;
                    Log(SystemMsg("Port number is out of range"));
                }
                if (username.Length < 1)
                {
                    error = true;
                    Log(SystemMsg("Username is required"));
                }
                if (!error)
                {
                    listener = new Thread(() => Listener(ip, port))
                    {
                        IsBackground = true
                    };
                    listener.Start();
                }
            }
        }
        private void Send(string msg, long id=-1)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWrite(msg, id));
            }
            else
            {
                send.ContinueWith(antecendent => BeginWrite(msg, id));
            }
        }
        private void Send(string msg, MyClient obj)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWrite(msg, obj));
            }
            else
            {
                send.ContinueWith(antecendent => BeginWrite(msg, obj));
            }
        }
        private void BeginWrite(string msg, long id = -1) // send the message to everyone except the sender or set ID to lesser than zero to send to everyone
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            foreach (KeyValuePair<long, MyClient> obj in clients)
            {
                if (id != obj.Value.id && obj.Value.client.Connected)
                {
                    try
                    {
                        obj.Value.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj.Value);
                    }
                    catch (Exception ex)
                    {
                        Log(ErrorMsg(ex.Message));
                    }
                }
            }
        }
        private void BeginWrite(string msg, MyClient obj) // send the message to a specific client
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
        }
        private void Write(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.EndWrite(result);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
        }
        private void Disconnect(long id = -1) // disconnect everyone if ID is not supplied or is lesser than zero
        {
            if (disconnect == null || !disconnect.IsAlive)
            {
                disconnect = new Thread(() =>
                {
                    if (id >= 0)
                    {
                        clients.TryGetValue(id, out MyClient obj);
                        obj.client.Close();
                    }
                    else
                    {
                        foreach (KeyValuePair<long, MyClient> obj in clients)
                        {
                            obj.Value.client.Close();
                        }
                    }
                })
                {
                    IsBackground = true
                };
                disconnect.Start();
            }
        }
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
            active = false;
            Del_url(id);
            Disconnect();
        }
        private void SendTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (sendTextBox.Text.Length > 0)
                {
                    string msg = sendTextBox.Text;
                    sendTextBox.Clear();
                    Log(string.Format("{0} (You): {1}", usernameTextBox.Text.Trim(), msg));
                    Send(string.Format("{0}: {1}", usernameTextBox.Text.Trim(), msg));
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            string ip = "0.tcp.ap.ngrok.io";
            string port=textBox5.Text.Trim();
            data.URL = ip + port;
            await docRef.SetAsync(data);
        }
        private async void Del_url(string id)
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("RoomData").Document(id);
            RoomData data = docRef.GetSnapshotAsync().Result.ConvertTo<RoomData>();
            data.URL = "";
            await docRef.UpdateAsync(new Dictionary<string, object>
{
                { nameof(RoomData.URL), data.URL},
            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Search_mess form = new Search_mess(id,textBox6.Text);
            form.ShowDialog();        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ImageList emojiImageList = new ImageList();
            emojiImageList.ImageSize = new Size(20, 20);

            string emojiDirectory = "Emoji";
            string[] emojiFiles = Directory.GetFiles(emojiDirectory);

            foreach (string emojiFile in emojiFiles)
            {
                emojiImageList.Images.Add(Image.FromFile(emojiFile));
            }

            listView1.LargeImageList = emojiImageList;

            for (int i = 0; i < emojiImageList.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i; // Sử dụng chỉ số của hình ảnh trong ImageList
                listView1.Items.Add(item);
            }
        }
        // Hàm này chuyển đổi chỉ số hình ảnh trong ImageList thành ký tự emoji tương ứng
        private string GetEmojiText(int imageIndex)
        {
            // Dựa vào chỉ số hình ảnh trong ImageList, chúng ta sẽ trả về emoji tương ứng
            switch (imageIndex)
            {
                case 4:
                    return "😀"; 
                case 1: 
                    return "😢"; 
                case 0: 
                    return "😡";
                case 6: 
                    return "😎"; 
                case 8: 
                    return "🤔"; 
                case 7: 
                    return "😲"; 
                case 9: 
                    return "😜"; 
                case 5: 
                    return "😏"; 
                case 2: 
                    return "😍"; 
                case 3: 
                    return "😱"; 
                default:
                    return "";
            }
        }

        private void listView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            // Lấy ra mục đang được chọn trong ListView
            ListViewItem selectedItem = listView1.GetItemAt(e.X, e.Y);

            // Kiểm tra xem mục đã được chọn hay không
            if (selectedItem != null)
            {
                // Lấy ra chỉ số của hình ảnh trong ImageList
                int imageIndex = selectedItem.ImageIndex;

                // Kiểm tra xem chỉ số hình ảnh có hợp lệ không
                if (imageIndex >= 0 && imageIndex < listView1.LargeImageList.Images.Count)
                {
                    // Lấy hình ảnh từ ImageList dựa trên chỉ số
                    Image emojiImage = listView1.LargeImageList.Images[imageIndex];

                    // Hiển thị emoji trong textbox
                    if (emojiImage != null)
                    {
                        // Chèn emoji vào vị trí hiện tại của con trỏ trong textbox
                        int selectionStart = sendTextBox.SelectionStart;
                        sendTextBox.Text = sendTextBox.Text.Insert(selectionStart, GetEmojiText(imageIndex));
                        sendTextBox.SelectionStart = selectionStart + 2; // Di chuyển con trỏ đến phía sau emoji vừa chèn
                    }
                }
            }
        }
    }

}
