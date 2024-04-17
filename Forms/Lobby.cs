using Chat_video_app.Classes;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Chat_video_app.Forms
{
    public partial class Lobby : Form
    {
        public Lobby()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Setting form = new Setting();
            form.ShowDialog();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
 
        }
    }
}
