using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_video_app.Forms
{
    public partial class Room_user : Form
    {
        string id;
        string username;
        public Room_user(string username,string id)
        {
            InitializeComponent();
            usernameTextBox.Text = username;
            portTextBox.Text = id;
            this.id = id;
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Lobby form = new Lobby(username);
            form.ShowDialog();
            Close();
        }
    }
}
